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
using OnLineExam.DataAccessLayer;
using System.Data.SqlClient;
using OnLineExam.BusinessLogicLayer;

public partial class Web_student_StudentTestPaper : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strUserName = HttpUtility.UrlDecode(Request.Cookies["UserName_CK"].Value, System.Text.Encoding.UTF8);
        this.lbl_UserInfo.Text = "姓名：" + strUserName + "&nbsp;&nbsp;&nbsp;&nbsp;学号：" + Session["userID"].ToString();
        this.lbl_PaperName.Text = Session["PaperName"].ToString();

        if (!IsPostBack)
        {
            InitData();
        }
       

        //if (!IsPostBack)
        //{
        //    string strUserName = HttpUtility.UrlDecode(Request.Cookies["UserName_CK"].Value, System.Text.Encoding.UTF8);
        //    this.lbl_UserInfo.Text = "姓名：" + strUserName + "&nbsp;&nbsp;&nbsp;&nbsp;学号：" + Session["userID"].ToString() + Session["userID"].ToString();
        //    this.lbl_PaperName.Text = Session["PaperName"].ToString();
        //    InitData();
        //}
    }
    //初始化试卷，从数据库中将试题取出
    protected void InitData()
    {
        SqlParameter[] Params1 = new SqlParameter[2];
        DataBase DB = new DataBase();
        int paperID = int.Parse(Session["PaperID"].ToString());
        Params1[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperID);               //试卷编号
        Params1[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "单选题");            //题目类型        
        DataSet ds1 = DB.GetDataSet("Proc_PaperDetail", Params1);
        gv_SingleSelection.DataSource = ds1;
        gv_SingleSelection.DataBind();
        ((Label)gv_SingleSelection.HeaderRow.FindControl("Label27")).Text = ((Label)gv_SingleSelection.Rows[0].FindControl("Label4")).Text;

        SqlParameter[] Params2 = new SqlParameter[2];
        Params2[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperID);               //试卷编号
        Params2[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "多选题");            //题目类型        
        DataSet ds2 = DB.GetDataSet("Proc_PaperDetail", Params2);
        gv_MutiSelection.DataSource = ds2;
        gv_MutiSelection.DataBind();
        ((Label)gv_MutiSelection.HeaderRow.FindControl("Label28")).Text = ((Label)gv_MutiSelection.Rows[0].FindControl("Label8")).Text;

        SqlParameter[] Params3 = new SqlParameter[2];
        Params3[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperID);               //试卷编号
        Params3[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "判断题");            //题目类型        
        DataSet ds3 = DB.GetDataSet("Proc_PaperDetail", Params3);
        gv_Judge.DataSource = ds3;
        gv_Judge.DataBind();
        ((Label)gv_Judge.HeaderRow.FindControl("Label29")).Text = ((Label)gv_Judge.Rows[0].FindControl("Label12")).Text;

        SqlParameter[] Params4 = new SqlParameter[2];
        Params4[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperID);               //试卷编号
        Params4[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "填空题");            //题目类型        
        DataSet ds4 = DB.GetDataSet("Proc_PaperDetail", Params4);
        gv_FillBlank.DataSource = ds4;
        gv_FillBlank.DataBind();
        ((Label)gv_FillBlank.HeaderRow.FindControl("Label30")).Text = ((Label)gv_FillBlank.Rows[0].FindControl("Label17")).Text;

        SqlParameter[] Params5 = new SqlParameter[2];
        Params5[0] = DB.MakeInParam("@PaperID", SqlDbType.Int, 4, paperID);               //试卷编号
        Params5[1] = DB.MakeInParam("@Type", SqlDbType.VarChar, 10, "问答题");            //题目类型        
        DataSet ds5 = DB.GetDataSet("Proc_PaperDetail", Params5);
        gv_Question.DataSource = ds5;
        gv_Question.DataBind();
        ((Label)gv_Question.HeaderRow.FindControl("Label31")).Text = ((Label)gv_Question.Rows[0].FindControl("Label37")).Text;
    }
   
    /// <summary>
    /// 提交试卷 考试记录信息到 学生答案表StudentAnswer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        string strUserID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
        int paperid = Convert.ToInt32(Session["PaperID"].ToString());
        DBHelper db = new DBHelper();
        int singlemark = int.Parse(((Label)gv_SingleSelection.Rows[0].FindControl("Label4")).Text);//取出单选题的每题分值

        ArrayList SQLStringList = new ArrayList();
        //string single = "";//记录插入单选题的sql语句集合
        //string Multi = "";//记录插入多选题的sql语句集合
        //string Judge = "";//记录插入判断题的sql语句集合
        //string Fill = "";//记录插入填空题的sql语句集合
        //string Que = "";//记录插入问答题的sql语句集合

        foreach (GridViewRow dr in gv_SingleSelection.Rows)
        {
            string str = "";
            if (((RadioButton)dr.FindControl("RadioButton1")).Checked)
            {
                str = "A";
            }
            else if (((RadioButton)dr.FindControl("RadioButton2")).Checked)
            {
                str = "B";
            }
            else if (((RadioButton)dr.FindControl("RadioButton3")).Checked)
            {
                str = "C";
            }
            else if (((RadioButton)dr.FindControl("RadioButton4")).Checked)
            {
                str = "D";
            }

            int titleid = int.Parse(((Label)dr.FindControl("Label40")).Text);
            string single = "insert into StudentAnswer(StudentID,PaperID,Type,TitleID,Mark,UserAnswer) values('" + strUserID + "','" + paperid + "','单选题','" + titleid + "','" + singlemark + "','" + str + "');";
            //db.ExecuteSql(single);
            SQLStringList.Add(single);
        }
        int multimark = int.Parse(((Label)gv_MutiSelection.Rows[0].FindControl("Label8")).Text);//取出多选题每题分值
        foreach (GridViewRow dr in gv_MutiSelection.Rows)//对多选题每题进行判断用户选择答案
        {
            string str = "";
            if (((CheckBox)dr.FindControl("CheckBox1")).Checked)
            {
                str += "A";
            }
            if (((CheckBox)dr.FindControl("CheckBox2")).Checked)
            {
                str += "B";
            }
            if (((CheckBox)dr.FindControl("CheckBox3")).Checked)
            {
                str += "C";
            }
            if (((CheckBox)dr.FindControl("CheckBox4")).Checked)
            {
                str += "D";
            }
            int titleid = int.Parse(((Label)dr.FindControl("Label41")).Text);
            string Multi = "insert into StudentAnswer(StudentID,PaperID,Type,TitleID,Mark,UserAnswer) values('" + strUserID + "','" + paperid + "','多选题','" + titleid + "','" + multimark + "','" + str + "');";
            //db.ExecuteSql(Multi);
            SQLStringList.Add(Multi);
        }
        int judgemark = int.Parse(((Label)gv_Judge.Rows[0].FindControl("Label12")).Text);//取出判断题每题分值
        foreach (GridViewRow dr in gv_Judge.Rows)//对判断题每题进行判断用户选择答案
        {
            string str = Convert.ToString(false);
            if (((CheckBox)dr.FindControl("CheckBox5")).Checked)
            {
                str = Convert.ToString(true);
            }
            int titleid = int.Parse(((Label)dr.FindControl("Label42")).Text);
            string Judge = "insert into StudentAnswer(StudentID,PaperID,Type,TitleID,Mark,UserAnswer) values('" + strUserID + "','" + paperid + "','判断题','" + titleid + "','" + judgemark + "','" + str + "');";
            //db.ExecuteSql(Judge);
            SQLStringList.Add(Judge);
        }
        int fillmark = int.Parse(((Label)gv_FillBlank.Rows[0].FindControl("Label17")).Text);//取出填空题每题分值
        foreach (GridViewRow dr in gv_FillBlank.Rows)
        {
            string str = "";
            str = ((TextBox)dr.FindControl("TextBox1")).Text.Trim();
            int titleid = int.Parse(((Label)dr.FindControl("Label43")).Text);
            string Fill = "insert into StudentAnswer(StudentID,PaperID,Type,TitleID,Mark,UserAnswer) values('" + strUserID + "','" + paperid + "','填空题','" + titleid + "','" + fillmark + "','" + str + "');";
            //db.ExecuteSql(Fill);
            SQLStringList.Add(Fill);
        }

        int quemark = int.Parse(((Label)gv_Question.Rows[0].FindControl("Label37")).Text);//取出问答题每题分值
        foreach (GridViewRow dr in gv_Question.Rows)
        {
            string str = "";
            str = ((TextBox)dr.FindControl("TextBox2")).Text.Trim();
            int titleid = int.Parse(((Label)dr.FindControl("Label44")).Text);
            string Que = "insert into StudentAnswer(StudentID,PaperID,Type,TitleID,Mark,UserAnswer) values('" + strUserID + "','" + paperid + "','问答题','" + titleid + "','" + quemark + "','" + str + "');";
            //db.ExecuteSql(Que);
            SQLStringList.Add(Que);
        }

        #region 统一执行数据库插入操作 用事务执行
        if (db.ExecuteSqlTran(SQLStringList))
        {
            Response.Write("<script language=javascript>alert('试卷提交成功!');location='/User/CommonPage/UserMain.html'</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('试卷提交失败，请联系老师处理!');</script>");
        }
            
        #endregion
        

    }
}