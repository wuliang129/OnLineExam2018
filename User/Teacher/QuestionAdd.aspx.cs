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
public partial class Web_QuestionAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginName = Session["userID"].ToString();
            Users user = new Users();
            user.LoadData(loginName);
            labUser.Text = user.UserName;
            InitDDLData();          //��ʼ�����Կ�Ŀ�����б��          
            if (Request["ID"] != null)//������޸���Ŀ����ʼ������
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
        int QuestionProblemID = int.Parse(Request["ID"].ToString());  //ȡ�����ݹ�����������
        QuestionProblem questionproblem = new QuestionProblem();          //������ѡ�����
        if (questionproblem.LoadData(QuestionProblemID))                //���ȡ����Ŀ��Ϣ���ֱ������Ӧ�ؼ���ʾ
        {
            ddlCourse.SelectedValue = questionproblem.CourseID.ToString();
            txtTitle.Text = questionproblem.Title;
            txtAnswer.Text = questionproblem.Answer;
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
            QuestionProblem questionproblem = new QuestionProblem();          //������ѡ�����
            questionproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//Ϊ��ѡ���������Ը�ֵ
            questionproblem.Title = txtTitle.Text;
            questionproblem.Answer = txtAnswer.Text;
            if (Request["ID"] != null)                                  //������޸���Ŀ��Ϣ
            {
                questionproblem.ID = int.Parse(Request["ID"].ToString()); //ȡ����������
                if (questionproblem.UpdateByProc(int.Parse(Request["ID"].ToString())))//�����޸����ⷽ���޸�����
                {
                    lblMessage.Text = "�ɹ��޸ĸ��ʴ��⣡";
                }
                else
                {
                    lblMessage.Text = "�޸ĸ��ʴ���ʧ�ܣ�";
                }
            }
            else                                                        //������������
            {
                if (questionproblem.InsertByProc())                       //����������ⷽ���������
                {
                    lblMessage.Text = "�ɹ���Ӹ��ʴ��⣡";
                }
                else
                {
                    lblMessage.Text = "��Ӹ��ʴ���ʧ�ܣ�";
                }
            }
        }
    }
    protected void imgBtnReturn_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("QuestionManage.aspx");
    }
}
