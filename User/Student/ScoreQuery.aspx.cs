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

public partial class Web_student_ScoreQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            /// <summary>
            /// 初始化科目列表框
            /// </summary>
            InitData();

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
            this.btn_ScoreQuery.Enabled = false;
            lblMessage.Text = "没有可供考试试卷！";
        }
    }

    /// <summary>
    /// //初始化成绩
    /// </summary>
    protected void ScoreInitData()
    {
        Scores score = new Scores();        //创建Scores对象       
        DataSet ds = score.StudentQueryScore(HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8));
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.gv_Score.DataSource = ds;          //为GridView控件指名数据源        
            this.gv_Score.DataBind();               //绑定数据
        }
        else
        {
            lblScore.Text = "没有成绩记录!";
        }
    }

    protected void gv_Score_RowDataBound(object sender, GridViewRowEventArgs e)
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
        for (i = 0; i < this.gv_Score.Rows.Count; i++)
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

    protected void gv_Score_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gv_Score.PageIndex = e.NewPageIndex;
        ScoreInitData();
    }


    /// <summary>
    /// 根据考试科目查询成绩
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_ScoreQuery_Click(object sender, EventArgs e)
    {
        if (this.ddlPaper.SelectedItem.Text.Length < 1)
        {
            this.lblMessage.Text = "请选择考试科目";
            return;
        }
        Scores score = new Scores();        //创建Scores对象       
        DataSet ds = score.StudentQueryScore(HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8), this.ddlPaper.SelectedItem.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.gv_Score.DataSource = ds;          //为GridView控件指名数据源        
            this.gv_Score.DataBind();               //绑定数据
        }
        else
        {
            lblScore.Text = "没有成绩记录!";
        }
    }
}