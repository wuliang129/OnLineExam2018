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

public partial class Web_SingleSelectAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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
        int SingleProblemID = int.Parse(Request["ID"].ToString());  //ȡ�����ݹ�����������
        SingleProblem singleproblem = new SingleProblem();          //������ѡ�����
        if (singleproblem.LoadData(SingleProblemID))                //���ȡ����Ŀ��Ϣ���ֱ������Ӧ�ؼ���ʾ
        {
            ddlCourse.SelectedValue = singleproblem.CourseID.ToString();
            txtTitle.Text = singleproblem.Title;
            txtAnswerA.Text = singleproblem.AnswerA;
            txtAnswerB.Text = singleproblem.AnswerB;
            txtAnswerC.Text = singleproblem.AnswerC;
            txtAnswerD.Text = singleproblem.AnswerD;
            ddlAnswer.SelectedItem.Text = singleproblem.Answer;
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
            SingleProblem singleproblem = new SingleProblem();          //������ѡ�����
            singleproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//Ϊ��ѡ���������Ը�ֵ
            singleproblem.Title = txtTitle.Text;
            singleproblem.AnswerA = txtAnswerA.Text;
            singleproblem.AnswerB = txtAnswerB.Text;
            singleproblem.AnswerC = txtAnswerC.Text;
            singleproblem.AnswerD = txtAnswerD.Text;
            singleproblem.Answer = ddlAnswer.SelectedItem.Text;
            if (Request["ID"] != null)                                  //������޸���Ŀ��Ϣ
            {
                singleproblem.ID = int.Parse(Request["ID"].ToString()); //ȡ����������
                if (singleproblem.UpdateByProc(int.Parse(Request["ID"].ToString())))//�����޸����ⷽ���޸�����
                {
                    lblMessage.Text = "�ɹ��޸ĸõ�ѡ�⣡";
                }
                else
                {
                    lblMessage.Text = "�޸ĸõ�ѡ��ʧ�ܣ�";
                }
            }
            else                                                        //������������
            {
                if (singleproblem.InsertByProc())                       //����������ⷽ���������
                {
                    lblMessage.Text = "�ɹ���Ӹõ�ѡ�⣡";
                }
                else
                {
                    lblMessage.Text = "��Ӹõ�ѡ��ʧ�ܣ�";
                }
            }
        }
    }
    protected void imgBtnReturn_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("SingleSelectManage.aspx");
    }
}
