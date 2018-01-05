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

public partial class Web_MultiSelectAdd : System.Web.UI.Page
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
        int multiProblemID = int.Parse(Request["ID"].ToString());  //ȡ�����ݹ�����������
        MultiProblem multiproblem = new MultiProblem();          //������ѡ�����
        if (multiproblem.LoadData(multiProblemID))                //���ȡ����Ŀ��Ϣ���ֱ������Ӧ�ؼ���ʾ
        {
            ddlCourse.SelectedValue = multiproblem.CourseID.ToString();
            txtTitle.Text = multiproblem.Title;
            txtAnswerA.Text = multiproblem.AnswerA;
            txtAnswerB.Text = multiproblem.AnswerB;
            txtAnswerC.Text = multiproblem.AnswerC;
            txtAnswerD.Text = multiproblem.AnswerD;
            string answer = multiproblem.Answer;
            for (int i = 0; i < answer.Length; i++)
            {
                string item = answer[i].ToString();
                for (int j = 0; j < cblAnswer.Items.Count; j++)
                {
                    if (item == cblAnswer.Items[i].Text)
                    {
                        cblAnswer.Items[i].Selected = true;
                    }
                }
            }
        }
        else                //��ѯ����������ʾ
        {
            lblMessage.Text = "�������ݳ���";
        }
    }
    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            MultiProblem multiproblem = new MultiProblem();          //������ѡ�����
            multiproblem.CourseID = int.Parse(ddlCourse.SelectedValue);//Ϊ��ѡ���������Ը�ֵ
            multiproblem.Title = txtTitle.Text;
            multiproblem.AnswerA = txtAnswerA.Text;
            multiproblem.AnswerB = txtAnswerB.Text;
            multiproblem.AnswerC = txtAnswerC.Text;
            multiproblem.AnswerD = txtAnswerD.Text;
            string answer = "";
            for (int i = 0; i < cblAnswer.Items.Count; i++)
            {
                if (cblAnswer.Items[i].Selected)
                {
                    answer += cblAnswer.Items[i].Text;
                }
            }
            multiproblem.Answer = answer;
            if (Request["ID"] != null)                                  //������޸���Ŀ��Ϣ
            {
                multiproblem.ID = int.Parse(Request["ID"].ToString()); //ȡ����������
                if (multiproblem.UpdateByProc(int.Parse(Request["ID"].ToString())))//�����޸����ⷽ���޸�����
                {
                    lblMessage.Text = "�ɹ��޸ĸö�ѡ�⣡";
                }
                else
                {
                    lblMessage.Text = "�޸ĸö�ѡ��ʧ�ܣ�";
                }
            }
            else                                                        //������������
            {
                if (multiproblem.InsertByProc())                       //����������ⷽ���������
                {
                    lblMessage.Text = "�ɹ���Ӹö�ѡ�⣡";
                }
                else
                {
                    lblMessage.Text = "��Ӹö�ѡ��ʧ�ܣ�";
                }
            }
        }
    }
    protected void imgBtnReturn_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("MultiSelectManage.aspx");
    }
}
