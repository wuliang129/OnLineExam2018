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
            lblMessage.Text = "����δ�����Ծ�!";
        }
    }
    //GridView�ؼ�RowCanceling�¼�
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitData();
    }
    //GridView�ؼ�RowDeleting�¼�
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Paper paper = new Paper();      //����Paper����
        int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Values[0].ToString()); //ȡ��Ҫɾ����¼������ֵ
        if (paper.DeleteByPID(ID))
        {
            Response.Write("<script language=javascript>alert('�ɹ�ɾ�����Ծ�')</script>");          
        }
        else
        {
            Response.Write("<script language=javascript>alert('ɾ���Ծ�ʧ�ܣ�')</script>");
            
        }
        //InitData();
        Response.Redirect("PaperLists.aspx");
       
    }
    //GridView�ؼ�RowUpdating�¼�
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Values[0].ToString()); //ȡ��Ҫɾ����¼������ֵ
        Paper paper = new Paper();      //����Paper����
        paper.PaperState = byte.Parse(((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlPaperState")).SelectedValue);
        if (paper.UpdatePaperState(ID, paper.PaperState))//ʹ��Paper��UpdateByProc�����޸��Ծ�״̬
        {
            Response.Write("<script language=javascript>alert('�޸ĳɹ�!')</script>");            
        }
        else
        {
            Response.Write("<script language=javascript>alert('�޸�ʧ��!')</script>");
        }
        GridView1.EditIndex = -1;
        InitData();
    }
    //GridView�ؼ�RowEditing�¼�
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;  //GridView�༭���������ڵ����е�����
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
                ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('��ȷ��Ҫɾ����?')");
           }

        }
        int i;
        //ִ��ѭ������֤ÿ�����ݶ����Ը���
        for (i = 0; i < GridView1.Rows.Count; i++)
        {
            //�����ж��Ƿ���������
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //�����ͣ��ʱ���ı���ɫ
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Aqua'");
                //������ƿ�ʱ��ԭ����ɫ
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }
}
