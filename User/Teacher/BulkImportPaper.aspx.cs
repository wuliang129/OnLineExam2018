using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OnLineExam.BusinessLogicLayer;
using OnLineExam.DataAccessLayer;
using System.Data.SqlClient;
using System.IO;

public partial class Web_teacher_BulkImportPaper : System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginName = Session["userID"].ToString();
            Users user = new Users();
            user.LoadData(loginName);
            labUser.Text = user.UserName;
            InitData();  //初始化考试科目
            InitHtml();//初始化网页
        }
    }
    /// <summary>
    /// 初始化考试科目
    /// </summary>
    protected void InitData()
    {
        Course course = new Course();       //创建考试科目对象
        DataSet ds = course.QueryCourse();  //查询考试科目信息
        ddlCourse.DataSource = ds;          //指名考试科目列表框数据源
        ddlCourse.DataTextField = "Name";   //DataTextField显示Name字段值
        ddlCourse.DataValueField = "ID";    //DataValueField显示ID字段值
        ddlCourse.DataBind();               //绑定数据
    }

    protected void InitHtml()
    {
            //构建网页文件 开始部分
            string str_HTML = "<html><head><title> 所有出错记录列表 </title> <style type='text/css'>table{border:solid 1px #B23AEE;cellspacing:0px;text-align:center;}" +
"td,th{padding:5px; border:solid 1px #B23AEE;}</style> </head> <body bgcolor='#F0FFFF'>";
                str_HTML += "<h1>导入失败记录列表</h1><table  cellspacing='0px'><tr><th>题目</th><th>选项</th><th>答案</th></tr>";
                str_HTML += " </table><br/><br/>";


                str_HTML += "<h1>数据库已存在记录（题目）列表</h1><table  cellspacing='0px'><tr><th>题目</th><th>选项</th><th>答案</th></tr>";
                str_HTML += " </table>";

            //网页文件结尾部分
            str_HTML += "</body></html>";
            //Upfiles 目录创建网页
            string path = "~/Upfiles";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            }
            path = HttpContext.Current.Server.MapPath(path) + "\\temp.html"; //E:\\在线考试系统源码\\ayrjxy_OnLineExam\\OnLineExam\\Upfiles\\temp.html"

            if (!System.IO.File.Exists(path))
            {
                System.IO.FileStream f = System.IO.File.Create(path);
                f.Close();
                f.Dispose();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(path, false,//true为追加,false为覆盖

            System.Text.Encoding.GetEncoding("gb2312"));
            f2.WriteLine(str_HTML);
            f2.Close();
            f2.Dispose();
    }

    /// <summary>
    /// 导入单选题到页面（DGV）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_SingleImport_Click(object sender, EventArgs e)
    {
        #region 导入EXCEL单选记录到 数据集（当前网页）
        ImportEXCEL importExcel = new ImportEXCEL();//
        try
        {
            string newFile = importExcel.UploadFile(this.FileUpload_Single);//保存到本地服务器
            DataTable dt_FromExcelALL = importExcel.ImportToDataSet(newFile);//导入EXCEL所有记录到 数据集
            DataTable dt_InsertToDataSuccess = dt_FromExcelALL.Copy();  //复制dt_FromExcelALL表数据结构 导入数据库成功的记录集合
            dt_InsertToDataSuccess.Rows.Clear();
            DataTable dt_InsertToDataFail = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 导入数据库失败的记录集合
            dt_InsertToDataFail.Clear();
            DataTable dt_InsertToDataRepeat = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 已经存在的记录集合
            dt_InsertToDataRepeat.Clear();

            while (dt_FromExcelALL.Rows.Count > 0)
            {
                SingleProblem singleproblem = new SingleProblem();          //创建单选题对象
                singleproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//为单选题对象各属性赋值
                singleproblem.Title = dt_FromExcelALL.Rows[0].ItemArray[0].ToString();
                singleproblem.AnswerA = dt_FromExcelALL.Rows[0].ItemArray[1].ToString();
                singleproblem.AnswerB = dt_FromExcelALL.Rows[0].ItemArray[2].ToString();
                singleproblem.AnswerC = dt_FromExcelALL.Rows[0].ItemArray[3].ToString();
                singleproblem.AnswerD = dt_FromExcelALL.Rows[0].ItemArray[4].ToString();
                singleproblem.Answer = dt_FromExcelALL.Rows[0].ItemArray[5].ToString();
                singleproblem.Explain = dt_FromExcelALL.Rows[0].ItemArray[6].ToString();

                if (singleproblem.IsRecord_Exit_ByTitle(singleproblem.Title))//是否存在记录
                {
                    dt_InsertToDataRepeat.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                    //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                }
                else
                {
                    if (singleproblem.InsertByProc())                       //调用添加试题方法添加试题
                    {
                        dt_InsertToDataSuccess.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                    else
                    {
                        dt_InsertToDataFail.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                }
                dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
            }

            //显示导出成功记录到页面
            this.gv_ExcelData.DataSource = dt_InsertToDataSuccess.DefaultView;
            this.gv_ExcelData.DataBind();

            //组织导入失败记录（重复+失败）到网页中
            DataTableToHTML(dt_InsertToDataFail, dt_InsertToDataRepeat);
            string strResult = "\\n导入成功记录条数：" + dt_InsertToDataSuccess.Rows.Count.ToString()
                + "\\n导入失败记录条数：" + dt_InsertToDataFail.Rows.Count.ToString() + "\\n发现重复记录条数：" + dt_InsertToDataRepeat.Rows.Count.ToString();
            Response.Write("<script language='javascript'>alert('" + strResult + "');</script>");
        }
        catch(Exception ex)//输出错误
        {
            Response.Write("<script language='javascript'>alert('" + ex.Message + "');</script>");
        }
        #endregion

    }

    /// <summary>
    /// datatable 内容转成html网页
    /// </summary>
    /// <param name="dt_InsertToDataFail">导入数据库失败的记录集合</param>
    /// <param name="dt_InsertToDataRepeat">已经存在的记录集合</param>
    /// <returns></returns>
    protected void DataTableToHTML(DataTable dt_InsertToDataFail, DataTable dt_InsertToDataRepeat)
    {
        if (dt_InsertToDataFail.Rows.Count > 0 || dt_InsertToDataRepeat.Rows.Count > 0)
        {
            //构建网页文件 开始部分
            string str_HTML = "<html><head><title> 所有出错记录列表 </title> <style type='text/css'>table{border:solid 1px #B23AEE;cellspacing:0px;text-align:center;}"+
"td,th{padding:5px; border:solid 1px #B23AEE;}</style> </head> <body bgcolor='#F0FFFF'>";
            if (dt_InsertToDataFail.Rows.Count > 0)
            {
                str_HTML += "<h1>导入失败记录列表</h1><table  cellspacing='0px'><tr><th>题目</th><th>选项</th><th>答案</th></tr>";
                for(int i=0;i<dt_InsertToDataFail.Rows.Count;i++)
                {
                    str_HTML += "<tr><td>" + dt_InsertToDataFail.Rows[i].ItemArray[0].ToString() + "</td>";//题目
                    str_HTML += "<td><table><tr>";//选项
                    int j = 1;
                    while (j < dt_InsertToDataFail.Rows[i].ItemArray.Length - 1)
                    {
                        str_HTML += "<td>" + dt_InsertToDataFail.Rows[i].ItemArray[j].ToString() + "</td>";
                        j++;
                    }
                    str_HTML += "</tr></table></td>";
                    str_HTML += "<td>" + dt_InsertToDataFail.Rows[i].ItemArray[j].ToString() + "</td></tr>";//答案
                    //for (int j = 1; j < dt_InsertToDataFail.Rows[i].ItemArray.Length -1; j++)
                    //{
                       
                    //}
                    //str_HTML += "</tr></table></td>";
                    //str_HTML += "<td>" + dt_InsertToDataFail.Rows[i].ItemArray[j].ToString() + "</td>";//题目
                }
                str_HTML += " </table><br/><br/>";
            }
            

            if (dt_InsertToDataRepeat.Rows.Count > 0)
            {
                str_HTML += "<h1>数据库已存在记录（题目）列表</h1><table  cellspacing='0px'><tr><th>题目</th><th>选项</th><th>答案</th></tr>";
                for(int i=0;i<dt_InsertToDataRepeat.Rows.Count;i++)
                {
                    str_HTML += "<tr><td>" + dt_InsertToDataRepeat.Rows[i].ItemArray[0].ToString() + "</td>";//题目
                    str_HTML += "<td><table><tr>";//选项
                    int j = 1;
                    while (j < dt_InsertToDataRepeat.Rows[i].ItemArray.Length - 1)
                    {
                        str_HTML += "<td>" + dt_InsertToDataRepeat.Rows[i].ItemArray[j].ToString() + "</td>";
                        j++;
                    }
                    str_HTML += "</tr></table></td>";
                    str_HTML += "<td>" + dt_InsertToDataRepeat.Rows[i].ItemArray[j].ToString() + "</td></tr>";//答案

                }
                str_HTML += " </table>";
            }
            


            //网页文件结尾部分
             str_HTML += "</body></html>";
            //Upfiles 目录创建网页
            string path = "~/Upfiles";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            }
            path = HttpContext.Current.Server.MapPath(path) + "\\temp.html"; //E:\\在线考试系统源码\\ayrjxy_OnLineExam\\OnLineExam\\Upfiles\\temp.html"

            if (!System.IO.File.Exists(path))
            {
                System.IO.FileStream f = System.IO.File.Create(path);
                f.Close();
                f.Dispose();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(path, false,//true为追加,false为覆盖

            System.Text.Encoding.GetEncoding("gb2312"));
            f2.WriteLine(str_HTML);
            f2.Close();
            f2.Dispose();  
        }
        //this.HyperLink_FailandRepeat.NavigateUrl = "~/Upfiles/temp.html";
    }

    /// <summary>
    /// 当鼠标停留时更改背景色
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_ExcelData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i;
        //执行循环，保证每条数据都可以更新
        for (i = 0; i < this.gv_ExcelData.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Aqua'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }

    /// <summary>
    /// 导入多选题到页面（DGV）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_MultiSelectImport_Click(object sender, EventArgs e)
    {
        #region 导入EXCEL多选记录到 数据集（当前网页）
        ImportEXCEL importExcel = new ImportEXCEL();//
        try
        {
            string newFile = importExcel.UploadFile(this.FileUpload_MultiSelect);//保存到本地服务器
            DataTable dt_FromExcelALL = importExcel.ImportToDataSet(newFile);//导入EXCEL所有记录到 数据集
            DataTable dt_InsertToDataSuccess = dt_FromExcelALL.Copy();  //复制dt_FromExcelALL表数据结构 导入数据库成功的记录集合
            dt_InsertToDataSuccess.Rows.Clear();
            DataTable dt_InsertToDataFail = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 导入数据库失败的记录集合
            dt_InsertToDataFail.Clear();
            DataTable dt_InsertToDataRepeat = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 已经存在的记录集合
            dt_InsertToDataRepeat.Clear();

            while (dt_FromExcelALL.Rows.Count > 0)
            {
                MultiProblem multiproblem = new MultiProblem();          //创建单选题对象
                multiproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//为单选题对象各属性赋值
                multiproblem.Title = dt_FromExcelALL.Rows[0].ItemArray[0].ToString();
                multiproblem.AnswerA = dt_FromExcelALL.Rows[0].ItemArray[1].ToString();
                multiproblem.AnswerB = dt_FromExcelALL.Rows[0].ItemArray[2].ToString();
                multiproblem.AnswerC = dt_FromExcelALL.Rows[0].ItemArray[3].ToString();
                multiproblem.AnswerD = dt_FromExcelALL.Rows[0].ItemArray[4].ToString();
                multiproblem.Answer = dt_FromExcelALL.Rows[0].ItemArray[5].ToString();
                multiproblem.Explain = dt_FromExcelALL.Rows[0].ItemArray[6].ToString();

                if (multiproblem.IsRecord_Exit_ByTitle(multiproblem.Title))//是否存在记录
                {
                    dt_InsertToDataRepeat.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                    //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                }
                else
                {
                    if (multiproblem.InsertByProc())                       //调用添加试题方法添加试题
                    {
                        dt_InsertToDataSuccess.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                    else
                    {
                        dt_InsertToDataFail.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                }
                dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
            }

            //显示导出成功记录到页面
            this.gv_ExcelData.DataSource = dt_InsertToDataSuccess.DefaultView;
            this.gv_ExcelData.DataBind();

            //组织导入失败记录（重复+失败）到网页中
            DataTableToHTML(dt_InsertToDataFail, dt_InsertToDataRepeat);
            string strResult = "\\n导入成功记录条数：" + dt_InsertToDataSuccess.Rows.Count.ToString()
                + "\\n导入失败记录条数：" + dt_InsertToDataFail.Rows.Count.ToString() + "\\n发现重复记录条数：" + dt_InsertToDataRepeat.Rows.Count.ToString();
            Response.Write("<script language='javascript'>alert('" + strResult + "');</script>");
        }
        catch (Exception ex)//输出错误
        {
            Response.Write("<script language='javascript'>alert('" + ex.Message + "');</script>");
        }
        #endregion
    }

    /// <summary>
    /// JudgeProblem 判断题批量导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_JudgeProblem_Click(object sender, EventArgs e)
    {
        #region 导入EXCEL判断题记录到 数据集（当前网页）
        ImportEXCEL importExcel = new ImportEXCEL();//
        try
        {
            string newFile = importExcel.UploadFile(this.FileUpload_JudgeProblem);//保存到本地服务器
            DataTable dt_FromExcelALL = importExcel.ImportToDataSet(newFile);//导入EXCEL所有记录到 数据集
            DataTable dt_InsertToDataSuccess = dt_FromExcelALL.Copy();  //复制dt_FromExcelALL表数据结构 导入数据库成功的记录集合
            dt_InsertToDataSuccess.Rows.Clear();
            DataTable dt_InsertToDataFail = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 导入数据库失败的记录集合
            dt_InsertToDataFail.Clear();
            DataTable dt_InsertToDataRepeat = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 已经存在的记录集合
            dt_InsertToDataRepeat.Clear();

            while (dt_FromExcelALL.Rows.Count > 0)
            {
                JudgeProblem judgeproblem = new JudgeProblem();          //创建单选题对象
                judgeproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//为单选题对象各属性赋值
                judgeproblem.Title = dt_FromExcelALL.Rows[0].ItemArray[0].ToString();
                
                if (dt_FromExcelALL.Rows[0].ItemArray[1].ToString().ToLower() == "true" || dt_FromExcelALL.Rows[0].ItemArray[1].ToString().ToLower() == "t")
                {
                    judgeproblem.Answer = true;
                }
                else
                {
                    judgeproblem.Answer = false;
                }
                judgeproblem.Explain = dt_FromExcelALL.Rows[0].ItemArray[2].ToString();
               

                if (judgeproblem.IsRecord_Exit_ByTitle(judgeproblem.Title))//是否存在记录
                {
                    dt_InsertToDataRepeat.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                    //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                }
                else
                {
                    if (judgeproblem.InsertByProc())                       //调用添加试题方法添加试题
                    {
                        dt_InsertToDataSuccess.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                    else
                    {
                        dt_InsertToDataFail.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                }
                dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
            }

            //显示导出成功记录到页面
            this.gv_ExcelData.DataSource = dt_InsertToDataSuccess.DefaultView;
            this.gv_ExcelData.DataBind();

            //组织导入失败记录（重复+失败）到网页中
            DataTableToHTML(dt_InsertToDataFail, dt_InsertToDataRepeat);
            string strResult = "\\n导入成功记录条数：" + dt_InsertToDataSuccess.Rows.Count.ToString()
                + "\\n导入失败记录条数：" + dt_InsertToDataFail.Rows.Count.ToString() + "\\n发现重复记录条数：" + dt_InsertToDataRepeat.Rows.Count.ToString();
            Response.Write("<script language='javascript'>alert('" + strResult + "');</script>");
        }
        catch (Exception ex)//输出错误
        {
            Response.Write("<script language='javascript'>alert('" + ex.Message + "');</script>");
        }
        #endregion
    }

    /// <summary>
    /// FillBlankProblem 填空题批量导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_FillBlankProblem_Click(object sender, EventArgs e)
    {
        #region 导入EXCEL判断题记录到 数据集（当前网页）
        ImportEXCEL importExcel = new ImportEXCEL();//
        try
        {
            string newFile = importExcel.UploadFile(this.FileUpload_FillBlankProblem);//保存到本地服务器
            DataTable dt_FromExcelALL = importExcel.ImportToDataSet(newFile);//导入EXCEL所有记录到 数据集
            DataTable dt_InsertToDataSuccess = dt_FromExcelALL.Copy();  //复制dt_FromExcelALL表数据结构 导入数据库成功的记录集合
            dt_InsertToDataSuccess.Rows.Clear();
            DataTable dt_InsertToDataFail = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 导入数据库失败的记录集合
            dt_InsertToDataFail.Clear();
            DataTable dt_InsertToDataRepeat = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 已经存在的记录集合
            dt_InsertToDataRepeat.Clear();

            while (dt_FromExcelALL.Rows.Count > 0)
            {
                FillBlankProblem fillBlankproblem = new FillBlankProblem();          //创建单选题对象
                fillBlankproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//为单选题对象各属性赋值
                fillBlankproblem.FrontTitle = dt_FromExcelALL.Rows[0].ItemArray[0].ToString();
                fillBlankproblem.BackTitle = dt_FromExcelALL.Rows[0].ItemArray[1].ToString();
                fillBlankproblem.Answer = dt_FromExcelALL.Rows[0].ItemArray[2].ToString();
                fillBlankproblem.Explain = dt_FromExcelALL.Rows[0].ItemArray[3].ToString();


                if (fillBlankproblem.IsRecord_Exit_ByTitle(fillBlankproblem.FrontTitle, fillBlankproblem.BackTitle))//是否存在记录
                {
                    dt_InsertToDataRepeat.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                    //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                }
                else
                {
                    if (fillBlankproblem.InsertByProc())                       //调用添加试题方法添加试题
                    {
                        dt_InsertToDataSuccess.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                    else
                    {
                        dt_InsertToDataFail.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                }
                dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
            }

            //显示导出成功记录到页面
            this.gv_ExcelData.DataSource = dt_InsertToDataSuccess.DefaultView;
            this.gv_ExcelData.DataBind();

            //组织导入失败记录（重复+失败）到网页中
            DataTableToHTML(dt_InsertToDataFail, dt_InsertToDataRepeat);
            string strResult = "\\n导入成功记录条数：" + dt_InsertToDataSuccess.Rows.Count.ToString()
                + "\\n导入失败记录条数：" + dt_InsertToDataFail.Rows.Count.ToString() + "\\n发现重复记录条数：" + dt_InsertToDataRepeat.Rows.Count.ToString();
            Response.Write("<script language='javascript'>alert('" + strResult + "');</script>");
        }
        catch (Exception ex)//输出错误
        {
            Response.Write("<script language='javascript'>alert('" + ex.Message + "');</script>");
        }
        #endregion
    }

    /// <summary>
    /// 问答题批量导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_QuestionProblem_Click(object sender, EventArgs e)
    {
        #region 导入EXCEL判断题记录到 数据集（当前网页）
        ImportEXCEL importExcel = new ImportEXCEL();//
        try
        {
            string newFile = importExcel.UploadFile(this.FileUpload_QuestionProblem);//保存到本地服务器
            DataTable dt_FromExcelALL = importExcel.ImportToDataSet(newFile);//导入EXCEL所有记录到 数据集
            DataTable dt_InsertToDataSuccess = dt_FromExcelALL.Copy();  //复制dt_FromExcelALL表数据结构 导入数据库成功的记录集合
            dt_InsertToDataSuccess.Rows.Clear();
            DataTable dt_InsertToDataFail = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 导入数据库失败的记录集合
            dt_InsertToDataFail.Clear();
            DataTable dt_InsertToDataRepeat = dt_FromExcelALL.Copy(); //复制dt_FromExcelALL表数据结构 已经存在的记录集合
            dt_InsertToDataRepeat.Clear();

            while (dt_FromExcelALL.Rows.Count > 0)
            {
                QuestionProblem questionproblem = new QuestionProblem();          //创建单选题对象
                questionproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//为单选题对象各属性赋值
                questionproblem.Title = dt_FromExcelALL.Rows[0].ItemArray[0].ToString();
                questionproblem.Answer = dt_FromExcelALL.Rows[0].ItemArray[1].ToString();
                questionproblem.Explain = dt_FromExcelALL.Rows[0].ItemArray[2].ToString();


                if (questionproblem.IsRecord_Exit_ByTitle(questionproblem.Title))//是否存在记录
                {
                    dt_InsertToDataRepeat.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                    //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                }
                else
                {
                    if (questionproblem.InsertByProc())                       //调用添加试题方法添加试题
                    {
                        dt_InsertToDataSuccess.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                    else
                    {
                        dt_InsertToDataFail.Rows.Add(dt_FromExcelALL.Rows[0].ItemArray);
                        //dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
                    }
                }
                dt_FromExcelALL.Rows.RemoveAt(0);//删除记录
            }

            //显示导出成功记录到页面
            this.gv_ExcelData.DataSource = dt_InsertToDataSuccess.DefaultView;
            this.gv_ExcelData.DataBind();

            //组织导入失败记录（重复+失败）到网页中
            DataTableToHTML(dt_InsertToDataFail, dt_InsertToDataRepeat);
            string strResult = "\\n导入成功记录条数：" + dt_InsertToDataSuccess.Rows.Count.ToString()
                + "\\n导入失败记录条数：" + dt_InsertToDataFail.Rows.Count.ToString() + "\\n发现重复记录条数：" + dt_InsertToDataRepeat.Rows.Count.ToString();
            Response.Write("<script language='javascript'>alert('" + strResult + "');</script>");
        }
        catch (Exception ex)//输出错误
        {
            Response.Write("<script language='javascript'>alert('" + ex.Message + "');</script>");
        }
        #endregion
    }
}