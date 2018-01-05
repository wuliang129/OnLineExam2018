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

public partial class Web_UserScore : System.Web.UI.Page
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
        Scores score = new Scores();        //����Scores����       
        DataSet ds = score.QueryScore();     //����QueryScore������ѯ�ɼ�������ѯ����ŵ�DataSet���ݼ���
        GridView1.DataSource = ds;          //ΪGridView�ؼ�ָ������Դ        
        GridView1.DataBind();               //������
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        InitData();  
    }

    //GridView�ؼ�RowDeleting�¼�
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Scores score = new Scores();          //����Scores����
        int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Values[0].ToString()); //ȡ��Ҫɾ����¼������ֵ
        if (score.DeleteByProc(ID))
        {
            Response.Write("<script language=javascript>alert('�ɹ�ɾ����')</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('ɾ��ʧ�ܣ�')</script>");
        }
        GridView1.EditIndex = -1;
        InitData();
    }
  
    /// <summary>
    /// ������Excel�¼�
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Scores score = new Scores();        //����Scores����       
        DataSet ds = score.QueryScore();     //����QueryScore������ѯ�ɼ�������ѯ����ŵ�DataSet���ݼ���
        DataTable DT = ds.Tables[0];
        //���ɽ�Ҫ��Ž����Excel�ļ�������
        string NewFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        //ת��Ϊ����·��
        NewFileName = Server.MapPath("~") + "\\Upfiles\\" + NewFileName;
        //����ģ����ʽ���ɸ�Excel�ļ�
        File.Copy(Server.MapPath("~") + "\\Upfiles\\UsersScoreModule.xls", NewFileName, true);
        //����ָ���Excel�ļ������ݿ�����
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + NewFileName + ";Extended Properties='Excel 8.0;'";
        OleDbConnection Conn = new OleDbConnection(strConn);
        //�����ӣ�Ϊ�������ļ���׼��
        Conn.Open();
        OleDbCommand Cmd = new OleDbCommand("", Conn);

        foreach (DataRow DR in DT.Rows)
        {
            string XSqlString = "insert into [Sheet1$]";
            XSqlString += "([�û�ID],[�û�����],[�Ծ�],[�ɼ�],[����ʱ��],[�ľ�ʱ��]) values(";
            XSqlString += "'" + DR["UserID"] + "',";
            XSqlString += "'" + DR["UserName"] + "',";
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
        System.IO.FileStream Reader = System.IO.File.OpenRead(NewFileName);
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
        File.Delete(NewFileName);
    }
    
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
