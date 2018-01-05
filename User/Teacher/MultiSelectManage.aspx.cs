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

public partial class Web_MultiSelectManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginName = Session["userID"].ToString();
            Users user = new Users();
            user.LoadData(loginName);
            labUser.Text = user.UserName;     
            InitDDLData();  //��ʼ�����Կ�Ŀ          
        }
    }
    //��ʼ�����Կ�Ŀ
    protected void InitDDLData()
    {
        Course course = new Course();       //�������Կ�Ŀ����
        DataSet ds = course.QueryCourse();  //��ѯ���Կ�Ŀ��Ϣ
        ddlCourse.DataSource = ds;          //ָ�����Կ�Ŀ�б������Դ
        ddlCourse.DataTextField = "Name";   //DataTextField��ʾName�ֶ�ֵ
        ddlCourse.DataValueField = "ID";    //DataValueField��ʾID�ֶ�ֵ
        ddlCourse.DataBind();               //������
        GridViewBind();
    }
    //��ʾѡ���Ŀ�ĵ�ѡ��
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBind();//ΪGridView������
    }
    protected void GridViewBind()
    {
        MultiProblem multiproblem = new MultiProblem();  //������ѡ�����
        DataSet ds = multiproblem.QueryMultiProblem(int.Parse(ddlCourse.SelectedValue));//���ݿ��Կ�Ŀ��ѯ��ѡ����Ϣ
        GridView1.DataSource = ds.Tables[0].DefaultView;    //ΪGridView�ؼ�ָ������Դ
        GridView1.DataBind();                               //������
    }
    //ɾ�������¼�
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        MultiProblem multiproblem = new MultiProblem();  //������ѡ�����
        int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Values[0].ToString()); //ȡ��Ҫɾ����¼������ֵ
        if (multiproblem.DeleteByProc(ID))
        {
            Response.Write("<script language=javascript>alert('�ɹ�ɾ�����⣡')</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('ɾ������ʧ�ܣ�')</script>");
        }
        GridViewBind();//���°�����
    }
    //ɾ��������¼
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        MultiProblem multi = new MultiProblem();//����MultiProblem����              
        foreach (GridViewRow dr in GridView1.Rows)//��GridView�е�ÿһ�н����ж�
        {
            if (((CheckBox)dr.FindControl("xuanze")).Checked)//���ѡ���˽���ɾ��
            {
                int ID = int.Parse(((Label)dr.FindControl("Label1")).Text);
                multi.ID = ID;
                multi.DeleteByProc(ID);
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridViewBind();//���°�����
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].ToolTip = e.Row.Cells[2].Text;
            if ((e.Row.Cells[2].Text).Length > 30)
            {
                e.Row.Cells[2].Text = (e.Row.Cells[2].Text).Substring(0, 30) + "...";
            }
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton)e.Row.Cells[4].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('��ȷ��Ҫɾ��\"" + e.Row.Cells[2].Text + "\"��?')");
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
