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
using OnLineExam.DataAccessLayer;
using System.Data.SqlClient;

public partial class Web_PaperSetup2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginName = Session["userID"].ToString();
            Users user = new Users();
            user.LoadData(loginName);
            labUser.Text = user.UserName;
            InitData();  //��ʼ�����Կ�Ŀ     
            GVbind();
        }
    }
    //��ʼ�����Կ�Ŀ
    protected void InitData()
    {
        Course course = new Course();       //�������Կ�Ŀ����
        DataSet ds = course.QueryCourse();  //��ѯ���Կ�Ŀ��Ϣ
        ddlCourse.DataSource = ds;          //ָ�����Կ�Ŀ�б������Դ
        ddlCourse.DataTextField = "Name";   //DataTextField��ʾName�ֶ�ֵ
        ddlCourse.DataValueField = "ID";    //DataValueField��ʾID�ֶ�ֵ
        ddlCourse.DataBind();               //������
    }
    //���������Զ������Ծ�
    protected void GVbind()
    {       
        DataBase db = new DataBase();//����DataBase�����
        string GridView1Str = "select * from SingleProblem";//���ݲ������ò�ѯ��ѡ��Sql���
        DataSet ds1 = db.GetDataSetSql(GridView1Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView1.DataSource = ds1.Tables[0].DefaultView;//Ϊ��ѡ��GridView�ؼ�ָ������Դ
        GridView1.DataBind();//������
        string GridView2Str = "select * from MultiProblem";//���ݲ������ò�ѯ��ѡ��Sql���
        DataSet ds2 = db.GetDataSetSql(GridView2Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView2.DataSource = ds2.Tables[0].DefaultView;//Ϊ��ѡ��GridView�ؼ�ָ������Դ
        GridView2.DataBind();//������
        string GridView3Str = "select * from JudgeProblem";//���ݲ������ò�ѯ�ж���Sql���
        DataSet ds3 = db.GetDataSetSql(GridView3Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView3.DataSource = ds3.Tables[0].DefaultView;//Ϊ�ж���GridView�ؼ�ָ������Դ
        GridView3.DataBind();//������
        string GridView4Str = "select * from FillBlankProblem";//���ݲ������ò�ѯ�����Sql���
        DataSet ds4 = db.GetDataSetSql(GridView4Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView4.DataSource = ds4.Tables[0].DefaultView;//Ϊ�����GridView�ؼ�ָ������Դ
        GridView4.DataBind();//������
        string GridView5Str = "select * from QuestionProblem";//���ݲ������ò�ѯ�ʴ���Sql���
        DataSet ds5 = db.GetDataSetSql(GridView5Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView5.DataSource = ds5.Tables[0].DefaultView;//Ϊ�ʴ���GridView�ؼ�ָ������Դ
        GridView5.DataBind();//������
    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("chkSelect1")).Checked = this.chkSelectAll.Checked;
        }
        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            ((CheckBox)GridView2.Rows[i].FindControl("chkSelect2")).Checked = this.chkSelectAll.Checked;
        }
        for (int i = 0; i <= GridView3.Rows.Count - 1; i++)
        {
            ((CheckBox)GridView3.Rows[i].FindControl("chkSelect3")).Checked = this.chkSelectAll.Checked;
        }
        for (int i = 0; i <= GridView4.Rows.Count - 1; i++)
        {
            ((CheckBox)GridView4.Rows[i].FindControl("chkSelect4")).Checked = this.chkSelectAll.Checked;
        }
        for (int i = 0; i <= GridView5.Rows.Count - 1; i++)
        {
            ((CheckBox)GridView5.Rows[i].FindControl("chkSelect5")).Checked = this.chkSelectAll.Checked;
        }
    }

    //�������Ծ��浽���ݿ�
    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {
        DataBase db = new DataBase();
        string insertpaper = "insert into Paper(CourseID,PaperName,PaperState) values(" + int.Parse(ddlCourse.SelectedValue) + ",'" + txtPaperName.Text + "',1) SELECT @@IDENTITY as id";
        int afterID = GetIDInsert(insertpaper);//�����Ծ��������Զ����ɵ��Ծ���
        if (afterID > 0)
        {
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                bool isChecked = ((CheckBox)GridView1.Rows[i].FindControl("chkSelect1")).Checked;
                if (isChecked)
                {
                    string str1 = ((Label)GridView1.Rows[i].FindControl("Label3")).Text;
                    string single = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'��ѡ��'," + str1 + "," + int.Parse(txtSingleFen.Text) + ")";
                    db.Insert(single);
                }

            }
            for (int i = 0; i < this.GridView2.Rows.Count; i++)
            {
                bool isChecked = ((CheckBox)GridView2.Rows[i].FindControl("chkSelect2")).Checked;
                if (isChecked)
                {
                    string str2 = ((Label)GridView2.Rows[i].FindControl("Label6")).Text;
                    string multi = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'��ѡ��'," + str2 + "," + int.Parse(txtMultiFen.Text) + ")";
                    db.Insert(multi);
                }

            }
            for (int i = 0; i < this.GridView3.Rows.Count; i++)
            {
                bool isChecked = ((CheckBox)GridView3.Rows[i].FindControl("chkSelect3")).Checked;
                if (isChecked)
                {
                    string str3 = ((Label)GridView3.Rows[i].FindControl("Label7")).Text;
                    string judge = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'�ж���'," + str3 + "," + int.Parse(txtJudgeFen.Text) + ")";
                    db.Insert(judge);
                }

            }
            for (int i = 0; i < this.GridView4.Rows.Count; i++)
            {
                bool isChecked = ((CheckBox)GridView4.Rows[i].FindControl("chkSelect4")).Checked;
                if (isChecked)
                {
                    string str4 = ((Label)GridView4.Rows[i].FindControl("Label8")).Text;
                    string fill = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'�����'," + str4 + "," + int.Parse(txtFillFen.Text) + ")";
                    db.Insert(fill);
                }

            }
            for (int i = 0; i < this.GridView5.Rows.Count; i++)
            {
                bool isChecked = ((CheckBox)GridView5.Rows[i].FindControl("chkSelect5")).Checked;
                if (isChecked)
                {
                    string str5 = ((Label)GridView5.Rows[i].FindControl("Label23")).Text;
                    string que = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'�ʴ���'," + str5 + "," + int.Parse(txtQuestionFen.Text) + ")";
                    db.Insert(que);
                }

            }   
        }
        Response.Write("<script language=javascript>alert('����ɹ�');location='PaperLists.aspx'</script>");

    }


    public int GetIDInsert(string XSqlString)
    {
        SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        Connection.Open();
        SqlCommand cmd = new SqlCommand(XSqlString, Connection);
        int Id = Convert.ToInt32(cmd.ExecuteScalar());
        return Id;
    }
   
}
