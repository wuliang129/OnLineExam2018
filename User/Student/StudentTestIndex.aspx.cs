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

public partial class Web_student_StudentTestIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //在新窗口或 tab 显示网页
        this.btn_StartExam.Attributes.Add("onclick", "this.form.target='_TestWindows'");

        if (!IsPostBack)
        {
            InitData();//初始化科目列表框
            ScoreInitData();  //初始化成绩
        }
        
    }

    /// <summary>
    /// 初始化考试科目下拉列表框
    /// </summary>
    protected void InitData()
    {
        Paper paper = new Paper();              //创建Paper对象
        DataSet ds = paper.QueryPaper();        //查询所有可用试卷
        if (ds.Tables[0].Rows.Count >= 1)
        {
            ddlPaper.DataSource = ds;           //指名考试科目列表框数据源
            ddlPaper.DataTextField = "PaperName";   //DataTextField显示Name字段值
            ddlPaper.DataValueField = "PaperID";    //DataValueField显示ID字段值
            ddlPaper.DataBind();                //绑定数据

        }
        else
        {
            ddlPaper.Enabled = false;
            btn_StartExam.Enabled = false;
            lblMessage.Text = "没有试卷！";
        }
    }
   
    /// <summary>
    /// 初始化成绩
    /// </summary>
    protected void ScoreInitData()
    {
        Scores score = new Scores();        //创建Scores对象       
        DataSet ds = score.QueryUserScore(HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8));
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;          //为GridView控件指名数据源        
            GridView1.DataBind();               //绑定数据
        }
        else
        {
            lblScore.Text = "没有成绩!";
        }
    }
   
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ScoreInitData();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].ToolTip = e.Row.Cells[5].Text;
            if ((e.Row.Cells[5].Text).Length > 20)
            {
                e.Row.Cells[5].Text = (e.Row.Cells[5].Text).Substring(0, 20) + "...";
            }
        }
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
    /// 开始考试 转到考试页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_StartExam_Click(object sender, EventArgs e)
    {
        string strUserID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
        Students stuCurrent = new Students();
        if (stuCurrent.IsStudentTest(strUserID, Convert.ToInt32(ddlPaper.SelectedValue)))
        {
            lblMessage.Text = "您已经考试过了,不能再考试！";
            //Response.Write("<script language=javascript>location='/User/CommonPage/UserMain.html'</script>");
            
            //Response.Write("<script language=javascript>location='/User/Student/StudentTestPaper.aspx'</script>");
            return;
        }
        else
        {
            Session["userID"] = strUserID;
            Session["PaperID"] = ddlPaper.SelectedValue;
            Session["PaperName"] = ddlPaper.SelectedItem.Text;
            //Response.Redirect("~/User/Student/StudentTestPaper.aspx"); //转向考试界面 
            //Server.Transfer("~/User/Student/StudentTestPaper.aspx");
            Response.Write("<script language=javascript>location='/User/Student/StudentTestPaper.aspx'</script>");
        }
    }
}