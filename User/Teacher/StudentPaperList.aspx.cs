using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OnLineExam.BusinessLogicLayer;

public partial class Web_StudentPaperList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateStudentScore();

            InitData();         
        }
    }
    

    /// <summary>
    /// 生成考生成绩 
    /// 从 StudentAnswer 表中选取 尚未生成成绩的记录 生成 Score 成绩记录
    /// </summary>
    protected void CreateStudentScore()
    {
        DBHelper db = new DBHelper();
        //取出考试过但还没有生成成绩的考生记录
        string strSQL = "select distinct StudentAnswer.StudentID,StudentAnswer.PaperID from StudentAnswer where StudentID not in(select distinct Score.StudentId from Score)" + 
                        " and PaperID not in(select distinct Score.PaperID from Score)";
        DataSet ds = db.GetDataSet(strSQL);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string strStudentID = "";
            int iPaperID =0;

            ArrayList SQLStringList = new ArrayList();//保存所有有成绩的小题sql

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strStudentID = dr.ItemArray[0].ToString();
                iPaperID = Convert.ToInt32(dr.ItemArray[1].ToString());
                DateTime ExamTime = DateTime.Now;//考试时间
                int iSumScore = 0;//总成绩

                #region 计算该生该科成绩+生成每道题的成绩记录

                SqlParameter[] Params1 = new SqlParameter[3];
                Params1[0] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, iPaperID);
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "单选题");
                Params1[2] = db.MakeInParam("@StudentID", SqlDbType.VarChar, 50, strStudentID);
                DataTable dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach(DataRow drItem in dt1.Rows)
                {
                    string strUserAnswer = drItem["UserAnswer"].ToString();
                    string strAnswer = drItem["Answer"].ToString();
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (strUserAnswer == strAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }
                }

                //多选
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "多选题");
                dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach (DataRow drItem in dt1.Rows)
                {
                    string strUserAnswer = drItem["UserAnswer"].ToString();
                    string strAnswer = drItem["Answer"].ToString();
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (strUserAnswer == strAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }
                }

                //判断题
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "判断题");
                dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach (DataRow drItem in dt1.Rows)
                {
                    bool bUserAnswer = Convert.ToBoolean(drItem["UserAnswer"].ToString());
                    bool bAnswer = (drItem["Answer"].ToString() == "true")? true:false;
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (bUserAnswer == bAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }
                }

                //填空题
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "填空题");
                dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach (DataRow drItem in dt1.Rows)
                {
                    string strUserAnswer = drItem["UserAnswer"].ToString();
                    string strAnswer = drItem["Answer"].ToString();
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (strUserAnswer == strAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }
                }

                //问答题 这个需要老师参与给分
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "问答题");
                dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach (DataRow drItem in dt1.Rows)
                {
                    string strUserAnswer = drItem["UserAnswer"].ToString();
                    string strAnswer = drItem["Answer"].ToString();
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (strUserAnswer == strAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }

                    ExamTime = Convert.ToDateTime(drItem["ExamTime"].ToString());
                }

                #endregion


                Scores scoreItem = new Scores();
                scoreItem.StudentId = strStudentID;
                scoreItem.PaperID = iPaperID;
                scoreItem.Score = iSumScore;
                scoreItem.ExamTime = ExamTime;
                scoreItem.JudgeTime = DateTime.Now;

                scoreItem.InsertScore();//插入学生成绩
            }

            //调用存储过程 更新 学生考试记录的每道题得分
            db.ExecuteSqlTran(SQLStringList);
        }

    }
    /// <summary>
    /// 初始化试卷表格
    /// </summary>
    protected void InitData()
    {
        string strTeacherID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);

        Paper paper = new Paper();
        DataSet ds = paper.QueryStudentPaperList(strTeacherID);
        GridView1.DataSource = ds;         
        GridView1.DataBind();

        LabelPageInfo.Text = "当前（第" + (GridView1.PageIndex + 1).ToString() + "页 共" + GridView1.PageCount.ToString() + "页）";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        InitData();
       //Convert.ToInt32
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i;
        //执行循环，保证每条数据都可以更新
        for (i = 0; i < GridView1.Rows.Count; i++)
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
    /// 删除用户考试记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string StudentId = GridView1.DataKeys[e.RowIndex].Values[0].ToString(); //取出要删除记录的主键值1  userid
        int PaperID = int.Parse(GridView1.DataKeys[e.RowIndex].Values[1].ToString().Trim());//取出要删除记录的主键值2
        Paper paper = new Paper();
        if (paper.DeleteByProc(StudentId, PaperID))
        {
            Response.Write("<script language=javascript>alert('成功删除！')</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('删除失败！')</script>");
        }
        InitData();
    }
}
