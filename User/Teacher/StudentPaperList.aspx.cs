using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OnLineExam.BusinessLogicLayer;

public partial class Web_StudentPaperList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateStudentScore();

            InitData();         
        }
    }
    

    /// <summary>
    /// ���ɿ����ɼ� 
    /// �� StudentAnswer ����ѡȡ ��δ���ɳɼ��ļ�¼ ���� Score �ɼ���¼
    /// </summary>
    protected void CreateStudentScore()
    {
        DBHelper db = new DBHelper();
        //ȡ�����Թ�����û�����ɳɼ��Ŀ�����¼
        string strSQL = "select distinct StudentAnswer.StudentID,StudentAnswer.PaperID from StudentAnswer where StudentID not in(select distinct Score.StudentId from Score)" + 
                        " and PaperID not in(select distinct Score.PaperID from Score)";
        DataSet ds = db.GetDataSet(strSQL);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string strStudentID = "";
            int iPaperID =0;

            ArrayList SQLStringList = new ArrayList();//���������гɼ���С��sql

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strStudentID = dr.ItemArray[0].ToString();
                iPaperID = Convert.ToInt32(dr.ItemArray[1].ToString());
                DateTime ExamTime = DateTime.Now;//����ʱ��
                int iSumScore = 0;//�ܳɼ�

                #region ��������ÿƳɼ�+����ÿ����ĳɼ���¼

                SqlParameter[] Params1 = new SqlParameter[3];
                Params1[0] = db.MakeInParam("@PaperID", SqlDbType.Int, 4, iPaperID);
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "��ѡ��");
                Params1[2] = db.MakeInParam("@StudentID", SqlDbType.VarChar, 50, strStudentID);
                DataTable dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach(DataRow drItem in dt1.Rows)
                {
                    string strUserAnswer = drItem["UserAnswer"].ToString();
                    string strAnswer = drItem["Answer"].ToString();
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (strUserAnswer == strAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }
                }

                //��ѡ
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "��ѡ��");
                dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach (DataRow drItem in dt1.Rows)
                {
                    string strUserAnswer = drItem["UserAnswer"].ToString();
                    string strAnswer = drItem["Answer"].ToString();
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (strUserAnswer == strAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }
                }

                //�ж���
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "�ж���");
                dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach (DataRow drItem in dt1.Rows)
                {
                    bool bUserAnswer = Convert.ToBoolean(drItem["UserAnswer"].ToString());
                    bool bAnswer = (drItem["Answer"].ToString() == "true")? true:false;
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (bUserAnswer == bAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }
                }

                //�����
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "�����");
                dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach (DataRow drItem in dt1.Rows)
                {
                    string strUserAnswer = drItem["UserAnswer"].ToString();
                    string strAnswer = drItem["Answer"].ToString();
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (strUserAnswer == strAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }
                }

                //�ʴ��� �����Ҫ��ʦ�������
                Params1[1] = db.MakeInParam("@Type", SqlDbType.VarChar, 10, "�ʴ���");
                dt1 = (db.RunProcedureGetDataSet("Proc_StudentAnswer", Params1)).Tables[0];
                foreach (DataRow drItem in dt1.Rows)
                {
                    string strUserAnswer = drItem["UserAnswer"].ToString();
                    string strAnswer = drItem["Answer"].ToString();
                    int iMark = Convert.ToInt32(drItem["Mark"].ToString());
                    string strTitID = drItem["TitleID"].ToString();
                    if (strUserAnswer == strAnswer)
                    {
                        iSumScore += iMark;
                        strSQL = "update StudentAnswer set CurrentScore='" + iMark.ToString() + "' where StudentID='" + strStudentID + "' and PaperID='" + iPaperID.ToString() + "' and Type='" + Params1[1].SqlValue.ToString() + "' and TitleID='" + strTitID + "';";
                        SQLStringList.Add(strSQL);
                    }

                    ExamTime = Convert.ToDateTime(drItem["ExamTime"].ToString());
                }

                #endregion


                Scores scoreItem = new Scores();
                scoreItem.StudentId = strStudentID;
                scoreItem.PaperID = iPaperID;
                scoreItem.Score = iSumScore;
                scoreItem.ExamTime = ExamTime;
                scoreItem.JudgeTime = DateTime.Now;

                scoreItem.InsertScore();//����ѧ���ɼ�
            }

            //���ô洢���� ���� ѧ�����Լ�¼��ÿ����÷�
            db.ExecuteSqlTran(SQLStringList);
        }

    }
    /// <summary>
    /// ��ʼ���Ծ���
    /// </summary>
    protected void InitData()
    {
        string strTeacherID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);

        Paper paper = new Paper();
        DataSet ds = paper.QueryStudentPaperList(strTeacherID);
        GridView1.DataSource = ds;         
        GridView1.DataBind();

        LabelPageInfo.Text = "��ǰ����" + (GridView1.PageIndex + 1).ToString() + "ҳ ��" + GridView1.PageCount.ToString() + "ҳ��";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        InitData();
       //Convert.ToInt32
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
   
    /// <summary>
    /// ɾ���û����Լ�¼
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string StudentId = GridView1.DataKeys[e.RowIndex].Values[0].ToString(); //ȡ��Ҫɾ����¼������ֵ1  userid
        int PaperID = int.Parse(GridView1.DataKeys[e.RowIndex].Values[1].ToString().Trim());//ȡ��Ҫɾ����¼������ֵ2
        Paper paper = new Paper();
        if (paper.DeleteByProc(StudentId, PaperID))
        {
            Response.Write("<script language=javascript>alert('�ɹ�ɾ����')</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('ɾ��ʧ�ܣ�')</script>");
        }
        InitData();
    }
}
