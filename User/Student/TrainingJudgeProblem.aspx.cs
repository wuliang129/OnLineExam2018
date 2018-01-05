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

public partial class Web_student_TrainingJudgeProblem : System.Web.UI.Page
{
    string strSelectCourseID = string.Empty;//用户选择的课程ID
    int iProblemIndex = 0;//保存grideview中的下标 以0开始 不是ID
    bool bDisplayExplain = false;//记录用户 是否显示答案解析
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /// <summary>
            /// 初始化练习科目和用户选择记录
            /// </summary>
            InitCourseData();

           
            
            ddliProblems_SelectedIndexChanged( sender, e);//触发选择
            
            if (object.Equals(Request.Cookies["JudgeProblemSelectedIndex"], null))
            {
                iProblemIndex = 0;//默认第0题
                LoadProblemByIndex(0);//根据 grideview 索引 加载题目 默认为0 第一行
            }
            else
            {
                string str = HttpUtility.UrlDecode(Request.Cookies["JudgeProblemSelectedIndex"].Value, System.Text.Encoding.UTF8);
                iProblemIndex = Convert.ToInt32(str);
                LoadProblemByIndex(iProblemIndex);
            }

            /// <summary>
            /// 记录用户的 答案是否出现选择状态
            /// </summary>
            InitDisplayExplain();
            
        }
    }

    /// <summary>
    /// 初始化练习科目和用户选择记录
    /// </summary>
    protected void InitCourseData()
    {
        ddliProblems_Init(null, null);

        if (object.Equals(Request.Cookies["SelectCourseName"], null))
        {
            strSelectCourseID = this.ddliProblems.Items[0].Value;
            iProblemIndex = 0;//默认第一
            this.ddliProblems.Items[0].Selected = true;
        }
        else
        {
            string str = HttpUtility.UrlDecode(Request.Cookies["SelectCourseName"].Value, System.Text.Encoding.UTF8);
            this.ddliProblems.Items.FindByText(str).Selected = true;
            strSelectCourseID = this.ddliProblems.SelectedItem.Value;
        }
    }

    /// <summary>
    /// 记录用户的 答案是否出现选择状态
    /// </summary>
    protected void InitDisplayExplain()
    {
        if (object.Equals(Request.Cookies["DisplayExplain"], null))
        {
            if (this.chk_DisplayExplain.Checked == true)
                bDisplayExplain = true;
            else
                bDisplayExplain = false;

            #region 保存用户选择的科目到 cookie
            HttpCookie cookie = new HttpCookie("DisplayExplain");
            cookie.Value = HttpUtility.UrlEncode(bDisplayExplain.ToString(), System.Text.Encoding.UTF8);
            cookie.Expires = DateTime.MaxValue;
            Response.AppendCookie(cookie);
            #endregion

            this.lbl_Explain.Visible = bDisplayExplain;
        }
        else
        {
            string str = HttpUtility.UrlDecode(Request.Cookies["DisplayExplain"].Value, System.Text.Encoding.UTF8);
            bDisplayExplain = Convert.ToBoolean(str);
            this.lbl_Explain.Visible = bDisplayExplain;
        }
    }

    /// <summary>
    /// 初始化练习科目
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddliProblems_Init(object sender, EventArgs e)
    {
        Course course = new Course();              //创建Course对象
        DataSet ds = course.QueryCourse();        //查询所有科目
        if (ds.Tables[0].Rows.Count >= 1)
        {
            ddliProblems.DataSource = ds;           //指名考试科目列表框数据源
            ddliProblems.DataTextField = "Name";   //DataTextField显示Name字段值
            ddliProblems.DataValueField = "ID";    //DataValueField显示ID字段值
            ddliProblems.DataBind();                //绑定数据

        }
        else
        {
            ddliProblems.Enabled = false;
            lblMessage.Text = "没有可供练习科目！";
        }
    }

    /// <summary>
    /// 练习科目发生改变时操作
    /// 题目类型：单选题，多选题，判断题，问答题，填空题（默认）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddliProblems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddliProblems.SelectedItem.Text.Length < 1)
        {
            this.lblMessage.Text = "请选择练习科目";
            return;
        }
        #region 保存用户选择的科目到 cookie
        HttpCookie cookie = new HttpCookie("SelectCourseName");
        cookie.Value = HttpUtility.UrlEncode(this.ddliProblems.SelectedItem.Text, System.Text.Encoding.UTF8);
        cookie.Expires = DateTime.MaxValue;
        Response.AppendCookie(cookie);
        #endregion


        JudgeProblem Problems = new JudgeProblem();        //创建 JudgeProblem 对象       
        DataSet ds = Problems.GetAllProblemsByTypeandCourseID("判断题", Convert.ToInt32(this.ddliProblems.SelectedItem.Value));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMessage.Text = "";
            gv_Problem.DataSource = ds;          //为GridView控件指名数据源      
            gv_Problem.DataBind();               //绑定数据
            
        }
        else
        {
            gv_Problem.DataSource = null;          //为GridView清空数据      
            gv_Problem.DataBind();               
            lblMessage.Text = "没有练习科目记录!";
        }
    }

    /// <summary>
    /// 根据 grideview 索引 加载题目
    /// </summary>
    /// <param name="gvIndex"></param>
    protected void LoadProblemByIndex(int gvIndex)
    {
        if (gvIndex >= 0 && gvIndex < this.gv_Problem.Rows.Count)
        {
            this.lbl_ProblemIndex.Text = (gvIndex + 1).ToString();//题目序号
            this.lbl_ProblemTitle.Text = this.gv_Problem.Rows[gvIndex].Cells[2].Text;
         

            this.image_correct.Visible = false;
            this.image_error.Visible = false;

            this.radiobtn_AnswerTrue.Checked = this.radiobtn_AnswerFalse.Checked =  false;
            
            this.lbl_indexVScount.Text = this.lbl_ProblemIndex.Text + "/" + this.gv_Problem.Rows.Count;

            chk_DisplayExplain_CheckedChanged(this, null);//根据用户选择的选项：是否显示解析内容

            string strCorrect = "";
            CheckBox cb = (CheckBox)gv_Problem.Rows[gvIndex].Cells[3].Controls[0];
            if (cb.Checked)
                strCorrect = "正确（True）";
            else
                strCorrect = "错误（false）";

            this.lbl_Explain.Text = "题目解析：<br/>正确答案：" + strCorrect + "<br/>" + this.gv_Problem.Rows[gvIndex].Cells[4].Text;

            #region 保存用户选择的索引下标到 cookie
            iProblemIndex = gvIndex;//再次记录索引
            HttpCookie cookie = new HttpCookie("JudgeProblemSelectedIndex");
            cookie.Value = HttpUtility.UrlEncode(gvIndex.ToString(), System.Text.Encoding.UTF8);
            cookie.Expires = DateTime.MaxValue;
            Response.AppendCookie(cookie);
            #endregion

        }
        else
        {
            this.lblMessage.Text = "请选择正确的记录索引！";

            this.lbl_ProblemIndex.Text = this.lbl_ProblemTitle.Text = this.lbl_Explain.Text ="";
            this.image_correct.Visible = false;
            this.image_error.Visible = false;
            this.lbl_Explain.Text = this.lbl_indexVScount.Text = "";
        }
    }

    /// <summary>
    /// 根据那个按钮触发的事件来载入相应的 题目
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LoadProblemByButtonSender(object sender, EventArgs e)
    {
        #region 第一个和最后一个特殊处理
        Button btn = (Button)sender;
        if (btn.ID == "btn_First" || btn.ID == "btn_Last")
        {

            switch (btn.ID)
            {
                case "btn_First":
                    iProblemIndex = 0;
                    break;
                case "btn_Last":
                    iProblemIndex = this.gv_Problem.Rows.Count - 1;
                    break;
            }

            LoadProblemByIndex(iProblemIndex);
            return;
        }
        #endregion

        #region 判断用户的答案是否正确
        string strAnswer = "";
        if (this.radiobtn_AnswerTrue.Checked)
        {
            strAnswer = "1";
        }
        if (this.radiobtn_AnswerFalse.Checked)
        {
            strAnswer = "0";
        }

        #region 没选提示用户选择选项
        if (strAnswer == "")
        {
            this.cv_DisplayMsg.IsValid = false;
            return;
        }
        else
        {
            this.cv_DisplayMsg.IsValid = true;
        }
        #endregion

        
        string str = Request.Cookies["JudgeProblemSelectedIndex"].Value.ToString();
        iProblemIndex = Convert.ToInt32(str);


        string strCorrect = "";
        CheckBox cb = (CheckBox)gv_Problem.Rows[iProblemIndex].Cells[3].Controls[0];
        if (cb.Checked)
            strCorrect = "1";
        else
            strCorrect = "0";

        if (strAnswer == strCorrect)
        {
            this.image_correct.Visible = true;
            this.image_error.Visible = false;
        }
        else
        {
            this.image_correct.Visible = false;
            this.image_error.Visible = true;
            this.lbl_Explain.Visible = true;
            return;
        }
        #endregion


        //btn = (Button)sender;
        switch (btn.ID)
        {
            case "btn_First":
                iProblemIndex = 0;
                LoadProblemByIndex(iProblemIndex);
                break;
            case "btn_Previous":
                if (object.Equals(Request.Cookies["JudgeProblemSelectedIndex"], null))
                {
                    iProblemIndex = 0;//默认第一题
                    LoadProblemByIndex(0);//根据 grideview 索引 加载题目 默认为0 第一行
                }
                else
                {
                    str = HttpUtility.UrlDecode(Request.Cookies["JudgeProblemSelectedIndex"].Value, System.Text.Encoding.UTF8);
                    iProblemIndex = Convert.ToInt32(str);
                    if (iProblemIndex > 0) iProblemIndex--;
                    LoadProblemByIndex(iProblemIndex);
                }
                break;
            case "btn_Next":
                if (object.Equals(Request.Cookies["JudgeProblemSelectedIndex"], null))
                {
                    iProblemIndex = 0;//默认第一题
                    LoadProblemByIndex(0);//根据 grideview 索引 加载题目 默认为0 第一行
                }
                else
                {
                    str = HttpUtility.UrlDecode(Request.Cookies["JudgeProblemSelectedIndex"].Value, System.Text.Encoding.UTF8);
                    iProblemIndex = Convert.ToInt32(str);
                    if (iProblemIndex < (this.gv_Problem.Rows.Count - 1)) iProblemIndex++;
                    LoadProblemByIndex(iProblemIndex);
                }
                
                break;
            case "btn_Last":
                iProblemIndex = this.gv_Problem.Rows.Count - 1;
                LoadProblemByIndex(iProblemIndex);
                break;
        }
    }

    /// <summary>
    /// 根据用户选择的选项：是否显示解析内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chk_DisplayExplain_CheckedChanged(object sender, EventArgs e)
    {
        bool bDisplayExplain = this.chk_DisplayExplain.Checked;//记录用户选项
        this.lbl_Explain.Visible = bDisplayExplain;//再次记录索引
        #region 保存用户选择的科目到 cookie
        HttpCookie cookie = new HttpCookie("DisplayExplain");
        cookie.Value = HttpUtility.UrlEncode(bDisplayExplain.ToString(), System.Text.Encoding.UTF8);
        cookie.Expires = DateTime.MaxValue;
        Response.AppendCookie(cookie);
        #endregion
       
    }

}