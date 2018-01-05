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

public partial class Web_teacher_CourseManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            InitData();  //初始化考试科目          
        }
    }
    //初始化考试科目
    protected void InitData()
    {
        string strTeacherID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
        Course course = new Course();       //创建考试科目对象
        DataSet ds = course.QueryCourse(strTeacherID);  //查询考试科目信息
        gv_Course.DataSource = ds;          //为GridView控件指名数据源        
        gv_Course.DataBind();               //绑定数据
    }
   
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Course.PageIndex = e.NewPageIndex;
        InitData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i;
        //执行循环，保证每条数据都可以更新
        for (i = 0; i < gv_Course.Rows.Count; i++)
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
    //删除考试科目事件
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Course course = new Course();           //创建Course对象
        int ID = int.Parse(gv_Course.DataKeys[e.RowIndex].Values[0].ToString()); //取出要删除记录的主键值
        if (course.DeleteByID(ID))
        {
            Response.Write("<script language=javascript>alert('成功删除考试科目！')</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('删除考试科目失败！')</script>");
        }
        gv_Course.EditIndex = -1;
        InitData();
    }
    //GridView控件RowUpdating事件
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int ID = int.Parse(gv_Course.DataKeys[e.RowIndex].Values[0].ToString()); //取出要删除记录的主键值
        //Course course = new Course();           //创建Course对象
        //course.Name = ((TextBox)gv_Course.Rows[e.RowIndex].FindControl("txtName")).Text;
        //if (course.UpdateByProc(ID))//使用Users类UpdateByProc方法修改用户信息
        //{
        //    Response.Write("<script language=javascript>alert('修改成功!')</script>");
        //}
        //else
        //{
        //    Response.Write("<script language=javascript>alert('修改失败!')</script>");
        //}
        //gv_Course.EditIndex = -1;
        //InitData();
    }
    //GridView控件RowCanceling事件
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_Course.EditIndex = -1;
        InitData();
    }
    //GridView控件RowEditing事件
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_Course.EditIndex = e.NewEditIndex;  //GridView编辑项索引等于单击行的索引
        InitData();
    }

}