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
using System.Data.SqlClient;
using OnLineExam.DataAccessLayer;

/*
 * 导入学生成绩数据时有误需要 重新导入学生成绩信息
 * */
public partial class Web_StudentPaper : System.Web.UI.Page
{
    private int paperid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            InitData();
        }
    }

    /// <summary>
    /// 初始化试卷，从数据库中将试题取出
    /// </summary>
    protected void InitData()
    {
        lbl_XStudentId.Text = Request.QueryString["PaperID"].ToString();
        this.lbl_XPaperID.Text = Request.QueryString["PaperID"].ToString();

        string strStudentId = Request.QueryString["StudentId"].ToString();
        paperid = Convert.ToInt32(Request.QueryString["PaperID"].ToString());

        Students stuCurrent = new Students();
        stuCurrent.LoadData(strStudentId);
        this.lbl_StuInfo.Text = "学号：" + stuCurrent.UserID + "   &nbsp;&nbsp;&nbsp;&nbsp;姓名：" + stuCurrent.UserName + "   &nbsp;&nbsp;&nbsp;&nbsp;所在学院：" + stuCurrent.DepartmentName;
        
        DBHelper DB = new DBHelper();

        DataSet ds = DB.GetDataSet("SELECT StudentAnswer.[ID],StudentAnswer.[StudentID],StudentAnswer.[PaperID],StudentAnswer.[Type],StudentAnswer.[TitleID],StudentAnswer.[Mark],StudentAnswer.[UserAnswer],StudentAnswer.[ExamTime],StudentAnswer.[CurrentScore],Paper.PaperName" +
            " from StudentAnswer,Paper where StudentAnswer.StudentID='" + strStudentId + "' and StudentAnswer.PaperID ='" + paperid + "' and StudentAnswer.PaperID = Paper.PaperID  order by Type ASC, ExamTime DESC;");
        DataRow[] row = ds.Tables[0].Select();

        DataRow rs = row[0];
        lblExamtime.Text = rs["ExamTime"].ToString();
        lbl_PaperName.Text = rs["PaperName"].ToString();

        string strSQL = "select Score.PingYu from Score where Score.PaperID='" + lbl_XStudentId.Text + "' and Score.StudentId='" + strStudentId + "'";
        if (DB.ExecuteScalar(strSQL) != null)
        {
            this.tbxPingyu.Text = DB.ExecuteScalar(strSQL).ToString();
        }
        
        //foreach (DataRow rs in row)
        //{
        //    lblExamtime.Text = rs["ExamTime"].ToString();
        //    lbl_PaperName.Text = rs["PaperName"].ToString();
        //}


        SqlParameter[] Params1 = new SqlParameter[3];
        Params1[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperid);
        Params1[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "单选题");
        Params1[2] = DB.MakeInParam("@StudentId", SqlDbType.VarChar, 50, strStudentId);
        DataSet ds1 = DB.RunProcedureGetDataSet("Proc_StudentAnswer", Params1);
        gv_Single.DataSource = ds1;
        gv_Single.DataBind();
        ((Label)gv_Single.HeaderRow.FindControl("Label27")).Text = ((Label)gv_Single.Rows[0].FindControl("Label4")).Text;


        SqlParameter[] Params2 = new SqlParameter[3];
        Params2[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperid);
        Params2[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "多选题");
        Params2[2] = DB.MakeInParam("@StudentId", SqlDbType.VarChar, 50, strStudentId);
        DataSet ds2 = DB.RunProcedureGetDataSet("Proc_StudentAnswer", Params2);
        GridView2.DataSource = ds2;
        GridView2.DataBind();
        ((Label)GridView2.HeaderRow.FindControl("Label28")).Text = ((Label)GridView2.Rows[0].FindControl("Label8")).Text;


        SqlParameter[] Params3 = new SqlParameter[3];
        Params3[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperid);
        Params3[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "判断题");
        Params3[2] = DB.MakeInParam("@StudentId", SqlDbType.VarChar, 50, strStudentId);
        DataSet ds3 = DB.RunProcedureGetDataSet("Proc_StudentAnswer", Params3);
        GridView3.DataSource = ds3;
        GridView3.DataBind();
        ((Label)GridView3.HeaderRow.FindControl("Label29")).Text = ((Label)GridView3.Rows[0].FindControl("Label12")).Text;

        SqlParameter[] Params4 = new SqlParameter[3];
        Params4[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperid);
        Params4[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "填空题");
        Params4[2] = DB.MakeInParam("@StudentId", SqlDbType.VarChar, 50, strStudentId);
        DataSet ds4 = DB.RunProcedureGetDataSet("Proc_StudentAnswer", Params4);
        GridView4.DataSource = ds4;
        GridView4.DataBind();
        ((Label)GridView4.HeaderRow.FindControl("Label30")).Text = ((Label)GridView4.Rows[0].FindControl("Label17")).Text;

        SqlParameter[] Params5 = new SqlParameter[3];
        Params5[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperid);
        Params5[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "问答题");
        Params5[2] = DB.MakeInParam("@StudentId", SqlDbType.VarChar, 50, strStudentId);
        DataSet ds5 = DB.RunProcedureGetDataSet("Proc_StudentAnswer", Params5);
        GridView5.DataSource = ds5;
        GridView5.DataBind();
        ((Label)GridView5.HeaderRow.FindControl("Label31")).Text = ((Label)GridView5.Rows[0].FindControl("Label21")).Text;

        int score1 = 0;
        int singlemark = int.Parse(((Label)gv_Single.Rows[0].FindControl("Label4")).Text);//取出单选题的每题分值
        foreach (GridViewRow dr in gv_Single.Rows)
        {
            if (((Label)dr.FindControl("Label3")).Text.Trim() == "A")
            {
                ((RadioButton)dr.FindControl("RadioButton1")).Checked = true;
            }
            else if (((Label)dr.FindControl("Label3")).Text.Trim() == "B")
            {
                ((RadioButton)dr.FindControl("RadioButton2")).Checked = true;
            }
            else if (((Label)dr.FindControl("Label3")).Text.Trim() == "C")
            {
                ((RadioButton)dr.FindControl("RadioButton3")).Checked = true;
            }
            else if (((Label)dr.FindControl("Label3")).Text.Trim() == "D")
            {
                ((RadioButton)dr.FindControl("RadioButton4")).Checked = true;
            }
            if (((Label)dr.FindControl("Label3")).Text.Trim() == ((Label)dr.FindControl("Label23")).Text.Trim())
            {
                score1 = score1 + singlemark;
                sinScore.Text = Convert.ToString(score1);
            }
        }

        int score2 = 0;
        int multimark = int.Parse(((Label)GridView2.Rows[0].FindControl("Label8")).Text);//取出多选题每题分值
        foreach (GridViewRow dr in GridView2.Rows)
        {
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "A")
            {
                ((CheckBox)dr.FindControl("CheckBox1")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "B")
            {
                ((CheckBox)dr.FindControl("CheckBox2")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "C")
            {
                ((CheckBox)dr.FindControl("CheckBox3")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "D")
            {
                ((CheckBox)dr.FindControl("CheckBox4")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "AB")
            {
                ((CheckBox)dr.FindControl("CheckBox1")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox2")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "AC")
            {
                ((CheckBox)dr.FindControl("CheckBox1")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox3")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "AD")
            {
                ((CheckBox)dr.FindControl("CheckBox1")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox4")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "BC")
            {
                ((CheckBox)dr.FindControl("CheckBox2")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox3")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "BD")
            {
                ((CheckBox)dr.FindControl("CheckBox2")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox4")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "CD")
            {
                ((CheckBox)dr.FindControl("CheckBox3")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox4")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "ABC")
            {
                ((CheckBox)dr.FindControl("CheckBox1")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox2")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox3")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "ABD")
            {
                ((CheckBox)dr.FindControl("CheckBox1")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox2")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox4")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "ACD")
            {
                ((CheckBox)dr.FindControl("CheckBox1")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox3")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox4")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == "ABCD")
            {
                ((CheckBox)dr.FindControl("CheckBox1")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox2")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox3")).Checked = true;
                ((CheckBox)dr.FindControl("CheckBox4")).Checked = true;
            }
            if (((Label)dr.FindControl("Label7")).Text.Trim() == ((Label)dr.FindControl("Label27")).Text.Trim())
            {
                score2 = score2 + multimark;
                mulScore.Text = Convert.ToString(score2);
            }
        }
        int score3 = 0;
        int judgemark = int.Parse(((Label)GridView3.Rows[0].FindControl("Label12")).Text);//取出判断题每题分值
        foreach (GridViewRow dr in GridView3.Rows)//对判断题每题进行判断用户选择答案
        {
            if (bool.Parse(((Label)dr.FindControl("Label11")).Text.Trim()))
            {
                ((CheckBox)dr.FindControl("CheckBox5")).Checked = true;
            }
            if (((Label)dr.FindControl("Label11")).Text.Trim() == ((Label)dr.FindControl("Label41")).Text.Trim())
            {
                score3 = score3 + judgemark;
                judScore.Text = Convert.ToString(score3);
            }
        }

        int score4 = 0;
        int fillmark = int.Parse(((Label)GridView4.Rows[0].FindControl("Label17")).Text);//取出填空题每题分值
        foreach (GridViewRow dr in GridView4.Rows)//对填空题每题进行判断用户选择答案
        {
            string str = "";
            str = ((TextBox)dr.FindControl("TextBox1")).Text.Trim();
            if (str == ((Label)dr.FindControl("Label26")).Text.Trim())
            {
                score4 = score4 + fillmark;
                filScore.Text = Convert.ToString(score4);
            }
        }

        int iSumScore = 0;
        #region 初始化总成绩和问答题成绩（已判断）
        Scores score = new Scores();        //创建Scores对象  
        //调用QueryScore方法查询成绩并将查询结果放到DataSet数据集中
        DataSet ds_score = score.QueryUserScore(strStudentId, paperid);
        if (ds_score.Tables[0].Rows.Count > 0)
        {
            iSumScore = Convert.ToInt32(ds_score.Tables[0].Rows[0].ItemArray[3].ToString());
            this.sumScore.Text = iSumScore.ToString();
            this.queScore.Text = (iSumScore - score1 - score2 - score3 - score4).ToString();
            this.lblQuestion.Text = "";
        }
        #endregion

    }

   

   
    
    /// <summary>
    /// 问答题计分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int quemark = 0;
        int maxMark = int.Parse(((Label)GridView5.Rows[0].FindControl("Label21")).Text);
        int flag = 1;
        foreach (GridViewRow dr in GridView5.Rows)
        {
            if (int.Parse(((TextBox)dr.FindControl("tbxqueScore")).Text.Trim()) > maxMark)
            {
                lblQuestion.Text = "问答题每道的得分不能超过每道题的分数!";
                flag = 0;
            }
        }
        if (flag == 1)
        {
            string strUserAnswerCurrentScoreUpdata = "";
            foreach (GridViewRow dr in GridView5.Rows)
            {
                string strCurrentScore = ((Label)dr.FindControl("lbl_UserAnswerID")).Text.Trim();//当前记录小题ID
                int iCurrentScore = int.Parse(((TextBox)dr.FindControl("tbxqueScore")).Text.Trim());//当前记录小题得分
                strUserAnswerCurrentScoreUpdata += "update [StudentAnswer] set CurrentScore = '" + iCurrentScore + "' where ID ='" + strCurrentScore + "';";
                quemark = quemark + iCurrentScore;
                queScore.Text = Convert.ToString(quemark);
                sumScore.Text = Convert.ToString(Convert.ToInt32(sinScore.Text) + Convert.ToInt32(mulScore.Text) + Convert.ToInt32(judScore.Text) + Convert.ToInt32(filScore.Text) + Convert.ToInt32(queScore.Text));
            }

            lblQuestion.Text = "";
            #region 保存问答题每题得分
            DBHelper db = new DBHelper();
            //SqlParameter[] Params10 = new SqlParameter[1];
            //Params10[0] = db.MakeInParam("@sql", SqlDbType.NVarChar, 2000, strUserAnswerCurrentScoreUpdata);
            db.ExecuteSql(strUserAnswerCurrentScoreUpdata);
            //DB.RunProc("Proc_UserAnswerSetQueScore", Params10);
            #endregion
        }

        lbtn_Save_Click(sender, e);//默认调用保存成绩
    }

    /// <summary>
    /// 保存成绩
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtn_Save_Click(object sender, EventArgs e)
    {
        Scores UpdateScore = new Scores();  //创建Scores类对象
        UpdateScore.StudentId = Request.QueryString["StudentId"].ToString();
        UpdateScore.ExamTime = Convert.ToDateTime(lblExamtime.Text);
        UpdateScore.JudgeTime = DateTime.Now;
        UpdateScore.PaperID = Convert.ToInt32(Request.QueryString["PaperID"].ToString());
        UpdateScore.Score = Convert.ToInt32(sumScore.Text);
        UpdateScore.PingYu = tbxPingyu.Text;
        UpdateScore.IsUserView = 1;//用户是否可见成绩


        Paper paper = new Paper();
        paper.StudentId = UpdateScore.StudentId;
        paper.PaperID = UpdateScore.PaperID;
        paper.state = "已评阅";

        //if (!insertScore.CheckScore(insertScore.StudentId, insertScore.PaperID)) //使用CheckScore方法验证成绩是否存在
        //{
        //    if (insertScore.UpdateScore(insertScore)) //调用InsertByProc方法向数据库中插入成绩
        //    {
        //        lblMessage.Text = "成绩保存成功!";
        //    }
        //    else
        //    {
        //        lblMessage.Text = "成绩保存失败!";
        //    }
        //}
        //else
        //{
        //    lblMessage.Text = "该用户的成绩已存在,请先删除成绩再评阅!";
        //}
        if (UpdateScore.UpdateScore(UpdateScore)) //调用  Update 方法向数据库中插入成绩
        {
            lblMessage.Text = "成绩保存成功!";
        }
        else
        {
            lblMessage.Text = "成绩保存失败!";
        }
    }

    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtn_Back_Click(object sender, EventArgs e)
    {
        Server.Transfer("StudentPaperList.aspx");
    }
}