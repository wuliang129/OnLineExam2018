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

public partial class Web_PaperSetup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitData();  //��ʼ�����Կ�Ŀ          
        }
    }
    //��ʼ�����Կ�Ŀ
    protected void InitData()
    {
        string strTeacherID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
        Course course = new Course();       //�������Կ�Ŀ����
        DataSet ds = course.QueryCourse(strTeacherID);  //��ѯ���Կ�Ŀ��Ϣ

        ddlCourse.DataSource = ds;          //ָ�����Կ�Ŀ�б������Դ
        ddlCourse.DataTextField = "Name";   //DataTextField��ʾName�ֶ�ֵ
        ddlCourse.DataValueField = "ID";    //DataValueField��ʾID�ֶ�ֵ
        ddlCourse.DataBind();               //������
    }

    


    public int GetIDInsert(string XSqlString)
    {
        //SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //Connection.Open();
        //SqlCommand cmd = new SqlCommand(XSqlString, Connection);
        //int Id = Convert.ToInt32(cmd.ExecuteScalar());
        DBHelper db = new DBHelper();
        int Id = db.ExecuteSelect(XSqlString,null);

        return Id;       
    }


    /// <summary>
    /// ���������Զ������Ծ�
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtn_Conirm_Click(object sender, EventArgs e)
    {
        DBHelper db = new DBHelper();
        #region �ж��Ծ������Ƿ���ڴ��ڷ��� ����ʾ�û�
        string strSQL = "SELECT COUNT([PaperID]) FROM [Paper] where CourseID =@CourseID and PaperName=@PaperName;";
        SqlParameter[] Params = new SqlParameter[2];
        Params[0] = db.MakeInParam("@CourseID", SqlDbType.Int, 4,Convert.ToInt32(ddlCourse.SelectedValue));
        Params[1] = db.MakeInParam("@PaperName", SqlDbType.VarChar, 200, txtPaperName.Text);
        if (db.ExecuteSelect(strSQL, Params) >= 1)
        {
            this.RequiredFieldValidator3.ErrorMessage = "�Ѵ��ڸ��Ծ�";
            this.RequiredFieldValidator3.IsValid = false;
            this.txtPaperName.Focus();
            return;
        }
        else
        {
            this.RequiredFieldValidator3.ErrorMessage = "����Ϊ�գ�";
            this.RequiredFieldValidator3.IsValid = true;
        }
        #endregion
        Panel1.Visible = true;
       
        string GridView1Str = "select top " + int.Parse(txtSingleNum.Text.Trim()) + " * from SingleProblem order by newid()";//���ݲ������ò�ѯ��ѡ��Sql���
        DataSet ds1 = db.GetDataSet(GridView1Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView1.DataSource = ds1.Tables[0].DefaultView;//Ϊ��ѡ��GridView�ؼ�ָ������Դ
        GridView1.DataBind();//������
        string GridView2Str = "select top " + int.Parse(txtMultiNum.Text.Trim()) + " * from MultiProblem order by newid()";//���ݲ������ò�ѯ��ѡ��Sql���
        DataSet ds2 = db.GetDataSet(GridView2Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView2.DataSource = ds2.Tables[0].DefaultView;//Ϊ��ѡ��GridView�ؼ�ָ������Դ
        GridView2.DataBind();//������
        string GridView3Str = "select top " + int.Parse(txtJudgeNum.Text.Trim()) + " * from JudgeProblem order by newid()";//���ݲ������ò�ѯ�ж���Sql���
        DataSet ds3 = db.GetDataSet(GridView3Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView3.DataSource = ds3.Tables[0].DefaultView;//Ϊ�ж���GridView�ؼ�ָ������Դ
        GridView3.DataBind();//������
        string GridView4Str = "select top " + int.Parse(txtFillNum.Text.Trim()) + " * from FillBlankProblem order by newid()";//���ݲ������ò�ѯ�����Sql���
        DataSet ds4 = db.GetDataSet(GridView4Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView4.DataSource = ds4.Tables[0].DefaultView;//Ϊ�����GridView�ؼ�ָ������Դ
        GridView4.DataBind();//������
        string GridView5Str = "select top " + int.Parse(txtQuestionNum.Text.Trim()) + " * from QuestionProblem order by newid()";//���ݲ������ò�ѯ�����Sql���
        DataSet ds5 = db.GetDataSet(GridView5Str);//����DataBase�෽��GetDataSetSql������ѯ����
        GridView5.DataSource = ds5.Tables[0].DefaultView;//Ϊ�����GridView�ؼ�ָ������Դ
        GridView5.DataBind();//������
    }

    /// <summary>
    /// �����Ծ�
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtn_SavePaper_Click(object sender, EventArgs e)
    {
        DBHelper db = new DBHelper();
        #region �ж��Ծ������Ƿ���ڴ��ڷ��� ����ʾ�û�
        string strSQL = "SELECT COUNT([PaperID]) FROM [Paper] where CourseID =@CourseID and PaperName=@PaperName;";
        SqlParameter[] Params = new SqlParameter[2];
        Params[0] = db.MakeInParam("@CourseID", SqlDbType.Int, 4, Convert.ToInt32(ddlCourse.SelectedValue));
        Params[1] = db.MakeInParam("@PaperName", SqlDbType.VarChar, 200, txtPaperName.Text);
        if (db.ExecuteSelect(strSQL, Params) >= 1)
        {
            this.RequiredFieldValidator3.ErrorMessage = "�Ѵ��ڸ��Ծ�";
            this.RequiredFieldValidator3.IsValid = false;
            this.txtPaperName.Focus();
            return;
        }
        else
        {
            this.RequiredFieldValidator3.ErrorMessage = "����Ϊ�գ�";
            this.RequiredFieldValidator3.IsValid = true;
        }
        #endregion

        string strTeacherID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
        string insertpaper = "insert into Paper(CourseID,PaperName,PaperState,TeacherId) values(" + int.Parse(ddlCourse.SelectedValue) + ",'" + txtPaperName.Text + "',1,'" + strTeacherID + "') SELECT @@IDENTITY as id";
        int afterID = GetIDInsert(insertpaper);//�����Ծ��������Զ����ɵ��Ծ���
        if (afterID > 0)
        {
            foreach (GridViewRow dr in GridView1.Rows)//�����Ծ�ѡ����Ϣ
            {
                string single = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'��ѡ��'," + int.Parse(((Label)dr.FindControl("Label3")).Text) + "," + int.Parse(txtSingleFen.Text) + ")";
                db.ExecuteSql(single);
            }
            foreach (GridViewRow dr in GridView2.Rows)//�����Ծ��ѡ����Ϣ
            {
                string multi = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'��ѡ��'," + int.Parse(((Label)dr.FindControl("Label6")).Text) + "," + int.Parse(txtMultiFen.Text) + ")";
                db.ExecuteSql(multi);
            }
            foreach (GridViewRow dr in GridView3.Rows)//�����Ծ��ж�����Ϣ
            {
                string judge = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'�ж���'," + int.Parse(((Label)dr.FindControl("Label7")).Text) + "," + int.Parse(txtJudgeFen.Text) + ")";
                db.ExecuteSql(judge);
            }
            foreach (GridViewRow dr in GridView4.Rows)//�����Ծ��������Ϣ
            {
                string fill = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'�����'," + int.Parse(((Label)dr.FindControl("Label8")).Text) + "," + int.Parse(txtFillFen.Text) + ")";
                db.ExecuteSql(fill);
            }
            foreach (GridViewRow dr in GridView5.Rows)//�����Ծ��������Ϣ
            {
                string que = "insert into PaperDetail(PaperID,Type,TitleID,Mark) values(" + afterID + ",'�ʴ���'," + int.Parse(((Label)dr.FindControl("Label23")).Text) + "," + int.Parse(txtQuestionFen.Text) + ")";
                db.ExecuteSql(que);
            }
            Response.Write("<script language=javascript>alert('����ɹ�');location='PaperLists.aspx'</script>");
        }
    }
}
