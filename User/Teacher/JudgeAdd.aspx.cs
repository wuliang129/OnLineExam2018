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

public partial class Web_JudgeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string loginName = Session["userID"].ToString();
            Users user = new Users();
            user.LoadData(loginName);
            labUser.Text = user.UserName;
            InitDDLData();              //��ʼ�����Կ�Ŀ�����б��          
            if (Request["ID"] != null)  //������޸���Ŀ����ʼ������
            {
                InitData();
            }
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
    }
    //��ʼ������
    protected void InitData()
    {
        int judgeProblemID = int.Parse(Request["ID"].ToString());   //ȡ�����ݹ�����������
        JudgeProblem judgeproblem = new JudgeProblem();             //�����ж������
        if (judgeproblem.LoadData(judgeProblemID))                  //���ȡ����Ŀ��Ϣ���ֱ������Ӧ�ؼ���ʾ
        {
            ddlCourse.SelectedValue = judgeproblem.CourseID.ToString();
            txtTitle.Text = judgeproblem.Title;
            rblAnswer.SelectedValue = judgeproblem.Answer.ToString();           
        }
        else                //��ѯ����������ʾ
        {
            lblMessage.Text = "�������ݳ���";
        }
    }
    //��ӻ��޸��¼�
    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            JudgeProblem judgeproblem = new JudgeProblem();          //�����ж������
            judgeproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//Ϊ�ж����������Ը�ֵ
            judgeproblem.Title = txtTitle.Text;
            judgeproblem.Answer = bool.Parse(rblAnswer.SelectedValue);
            if (Request["ID"] != null)                                  //������޸���Ŀ��Ϣ
            {
                judgeproblem.ID = int.Parse(Request["ID"].ToString()); //ȡ����������
                if (judgeproblem.UpdateByProc(int.Parse(Request["ID"].ToString())))//�����޸����ⷽ���޸�����
                {
                    lblMessage.Text = "�ɹ��޸ĸ��ж��⣡";
                }
                else
                {
                    lblMessage.Text = "�޸ĸ��ж���ʧ�ܣ�";
                }
            }
            else                                                        //������������
            {
                if (judgeproblem.InsertByProc())                       //����������ⷽ���������
                {
                    lblMessage.Text = "�ɹ���Ӹ��ж��⣡";
                }
                else
                {
                    lblMessage.Text = "��Ӹ��ж���ʧ�ܣ�";
                }
            }
        }
    }
    protected void imgBtnReturn_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("JudgeManage.aspx");
    }
}
