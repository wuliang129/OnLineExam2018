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
using OnLineExam.BusinessLogicLayer;
using System.Data.SqlClient;

public partial class Web_PaperLists : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitData();
        }
    }
    protected void InitData()
    {
        string strTeacherID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
        Paper paper = new Paper();
        DataSet ds = paper.QueryAllPaper(strTeacherID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();

            lblMessage.Text = "";
        }
        else
        {
            lblMessage.Text = "您尚未构建试卷!";
        }
    }
    //GridView控件RowCanceling事件
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitData();
    }
    //GridView控件RowDeleting事件
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Paper paper = new Paper();      //创建Paper对象
        int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Values[0].ToString()); //取出要删除记录的主键值
        if (paper.DeleteByPID(ID))
        {
            Response.Write("<script language=javascript>alert('成功删除该试卷！')</script>");          
        }
        else
        {
            Response.Write("<script language=javascript>alert('删除试卷失败！')</script>");
            
        }
        //InitData();
        Response.Redirect("PaperLists.aspx");
       
    }
    //GridView控件RowUpdating事件
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Values[0].ToString()); //取出要删除记录的主键值
        Paper paper = new Paper();      //创建Paper对象
        paper.PaperState = byte.Parse(((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlPaperState")).SelectedValue);
        if (paper.UpdatePaperState(ID, paper.PaperState))//使用Paper类UpdateByProc方法修改试卷状态
        {
            Response.Write("<script language=javascript>alert('修改成功!')</script>");            
        }
        else
        {
            Response.Write("<script language=javascript>alert('修改失败!')</script>");
        }
        GridView1.EditIndex = -1;
        InitData();
    }
    //GridView控件RowEditing事件
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;  //GridView编辑项索引等于单击行的索引
        InitData();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        InitData();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
           if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除吗?')");
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
}
