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
using System.IO;
using System.Data.OleDb;
using OnLineExam.CommonComponent;

public partial class Web_StudentScore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                 
            InitData();  
        }
    }
    /// <summary>
    /// ��ʼ���ɼ����
    /// </summary>
    protected void InitData()
    {
        string strTeacherID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
        Scores score = new Scores();         //����Scores����       
        DataSet ds = score.QueryScore(strTeacherID);     //��ʦ����QueryScore������ѯ�ɼ�������ѯ����ŵ�DataSet���ݼ���
        gv_StudentScore.DataSource = ds;     //ΪGridView�ؼ�ָ������Դ        
        gv_StudentScore.DataBind();          //������
    }

    protected void gv_StudentScore_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_StudentScore.PageIndex = e.NewPageIndex;
        InitData();  
    }

    //GridView�ؼ�RowDeleting�¼�
    protected void gv_StudentScore_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Scores score = new Scores();          //����Scores����
        int ID = int.Parse(gv_StudentScore.DataKeys[e.RowIndex].Values[0].ToString()); //ȡ��Ҫɾ����¼������ֵ
        if (score.DeleteByProc(ID))
        {
            Response.Write("<script language=javascript>alert('�ɹ�ɾ����')</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('ɾ��ʧ�ܣ�')</script>");
        }
        gv_StudentScore.EditIndex = -1;
        InitData();
    }
  


    protected void gv_StudentScore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton)e.Row.Cells[7].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('��ȷ��Ҫɾ���ɼ���?')");
            }

        }    
        int i;
        //ִ��ѭ������֤ÿ�����ݶ����Ը���
        for (i = 0; i < gv_StudentScore.Rows.Count; i++)
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
    /// �������ݵ�EXCEL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtn_ExcelOut_Click(object sender, EventArgs e)
    {
        HTMLHelpClass fileHelp = new HTMLHelpClass();//
        string strTeacherID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);

        Scores score = new Scores();        //����Scores����       
        DataSet ds = score.QueryScore();     //����QueryScore������ѯ�ɼ�������ѯ����ŵ�DataSet���ݼ���
        DataTable DT = ds.Tables[0];

        
        //���ɽ�Ҫ��Ž����Excel�ļ�������  �ʺ�+����
        string NewFileName = strTeacherID + "-" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
        string strFullPath = fileHelp.strTeacherUpfilesPath + NewFileName;
        
        //ת��Ϊ����·��
        string physicalFullPath = Server.MapPath(strFullPath);
        //����ģ����ʽ���ɸ�Excel�ļ�
        File.Copy(Server.MapPath(fileHelp.strTeacherUpfilesPath + "UsersScoreModule.xls"), physicalFullPath, true);
        //����ָ���Excel�ļ������ݿ�����
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + physicalFullPath + ";Extended Properties='Excel 8.0;'";
        OleDbConnection Conn = new OleDbConnection(strConn);
        //�����ӣ�Ϊ�������ļ���׼��
        Conn.Open();
        OleDbCommand Cmd = new OleDbCommand("", Conn);

        foreach (DataRow DR in DT.Rows)
        {
            string XSqlString = "insert into [Sheet1$]";
            XSqlString += "([ѧ��ID],[ѧ������],[�Ծ�],[�ɼ�],[����ʱ��],[�ľ�ʱ��]) values(";
            XSqlString += "'" + DR["StudentId"] + "',";
            XSqlString += "'" + DR["StudentName"] + "',";
            XSqlString += "'" + DR["PaperName"] + "',";
            XSqlString += "'" + DR["Score"] + "',";
            XSqlString += "'" + DR["ExamTime"] + "',";
            XSqlString += "'" + DR["JudgeTime"] + "')";
            Cmd.CommandText = XSqlString;
            Cmd.ExecuteNonQuery();
        }

        //�����������ر�����
        Conn.Close();
        //��Ҫ���ص��ļ������Ѹ��ļ������FileStream��
        System.IO.FileStream Reader = System.IO.File.OpenRead(physicalFullPath);
        //�ļ����͵�ʣ���ֽ�������ʼֵΪ�ļ����ܴ�С
        long Length = Reader.Length;

        Response.Buffer = false;
        Response.AddHeader("Connection", "Keep-Alive");
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("ѧ���ɼ�.xls"));
        Response.AddHeader("Content-Length", Length.ToString());

        byte[] Buffer = new Byte[10000];		//������������ݵĻ�����
        int ByteToRead;											//ÿ��ʵ�ʶ�ȡ���ֽ���

        while (Length > 0)
        {
            //ʣ���ֽ�����Ϊ�㣬��������
            if (Response.IsClientConnected)
            {
                //�ͻ�������������ţ���������
                ByteToRead = Reader.Read(Buffer, 0, 10000);					//����������������
                Response.OutputStream.Write(Buffer, 0, ByteToRead);	//�ѻ�����������д��ͻ��������
                Response.Flush();																		//����д��ͻ���
                Length -= ByteToRead;																//ʣ���ֽ�������
            }
            else
            {
                //�ͻ���������Ѿ��Ͽ�����ֹ����ѭ��
                Length = -1;
            }
        }

        //�رո��ļ�
        Reader.Close();
        //ɾ����Excel�ļ�
       // File.Delete(NewFileName);

    }
}
