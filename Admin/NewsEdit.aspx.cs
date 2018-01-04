using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_NewsEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str = Request.QueryString["Admin_articleId"];
            if (str != null)
            {
                EditNewsByID(str);
            }

        }
    }

    protected void EditNewsByID(string strNewsID)
    {
        string strSQL = "SELECT NewsInfoTable.ID as ID,[NewsTitle],[NewsTime],[NewsSource],[NewsSourceURL],[NewsSubtitle],[NewsContent],[NewsCategory],[NewsRemark],NewsCategoryUrl,NewsKeyword,NewsAuditSuccess FROM NewsInfoTable,NewsCategory where NewsInfoTable.NewsCategory = NewsCategory.ID and NewsInfoTable.NewsAuditSuccess =1 and NewsInfoTable.ID=@NewsID ;";
        
        SqlParameter[] cmdParms = new SqlParameter[1];
        cmdParms[0] = new SqlParameter("@NewsID", System.Data.SqlDbType.NVarChar, 20);
        cmdParms[0].SqlValue = strNewsID;

        DBHelper db = new DBHelper();
        DataSet ds = new DataSet();
        ds = db.GetDataSet(strSQL,cmdParms);

        if (ds.Tables[0].Rows.Count > 0)
        {
            this.ddl_NewsCategory.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();

            this.txt_NewsTitle.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();

            this.txt_NewsSubtitle.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();

            this.txt_NewsSource.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();

            this.txt_NewsSourceURL.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();

            this.txt_NewsKeyword.Text = ds.Tables[0].Rows[0].ItemArray[10].ToString();

           if( (bool)(ds.Tables[0].Rows[0].ItemArray[11]) == false )
           {
               this.radiobtn_YES.Checked = true;
               this.radiobtn_NO.Checked = false;
           }
            else
           {
               this.radiobtn_YES.Checked = false;
               this.radiobtn_NO.Checked = true;
           }

           this.tbContent.Text = Server.HtmlDecode(ds.Tables[0].Rows[0].ItemArray[6].ToString());
        }
    }

    /// <summary>
    /// 初始化新闻类别
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_NewsCategory_Init(object sender, EventArgs e)
    {
        string strSQL = "SELECT [ID],[NewsCategoryName] FROM [News].[dbo].[NewsCategory];";
        DBHelper db = new DBHelper();
        DataSet ds =  db.GetDataSet(strSQL);
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            for(int i=0; i<dt.Rows.Count;i++)
            {
                ListItem lt = new ListItem();
                lt.Value = dt.Rows[i].ItemArray[0].ToString();
                lt.Text = dt.Rows[i].ItemArray[1].ToString();

                this.ddl_NewsCategory.Items.Add(lt);
            }
        }
    }

    /// <summary>
    /// 保存新闻
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_SaveNews_Click(object sender, EventArgs e)
    {
        string str = Request.QueryString["Admin_articleId"];
        if (str != null)
        {
            #region 更新记录

            string strSQL = "UPDATE [News].[dbo].[NewsInfoTable] SET [NewsTitle] = @NewsTitle,[NewsSource] = @NewsSource,[NewsSourceURL] = @NewsSourceURL" + 
                        ",[NewsSubtitle] = @NewsSubtitle,[NewsContent] = @NewsContent,[NewsCategory] = @NewsCategory,[NewsAuditSuccess] = @NewsAuditSuccess" +
                        ",[NewsKeyword] = @NewsKeyword,[NewsAuthor] = @NewsAuthor WHERE ID=@ID";

            SqlParameter[] cmdParms = new SqlParameter[10];
            cmdParms[0] = new SqlParameter("@NewsTitle", System.Data.SqlDbType.NVarChar, 50);
            cmdParms[0].SqlValue = this.txt_NewsTitle.Text;

            cmdParms[1] = new SqlParameter("@NewsSource", System.Data.SqlDbType.NVarChar, 50);
            cmdParms[1].SqlValue = this.txt_NewsSource.Text;

            cmdParms[2] = new SqlParameter("@NewsSourceURL", System.Data.SqlDbType.NVarChar, 1000);
            cmdParms[2].SqlValue = this.txt_NewsSourceURL.Text;

            cmdParms[3] = new SqlParameter("@NewsSubtitle", System.Data.SqlDbType.NVarChar, 50);
            cmdParms[3].SqlValue = this.txt_NewsSubtitle.Text;

            cmdParms[4] = new SqlParameter("@NewsContent", System.Data.SqlDbType.Text);
            //cmdParms[4].SqlValue = this.tbContent.Text;
            cmdParms[4].SqlValue = Server.HtmlEncode(this.tbContent.Text);


            cmdParms[5] = new SqlParameter("@NewsCategory", System.Data.SqlDbType.NVarChar, 10);
            cmdParms[5].SqlValue = this.ddl_NewsCategory.SelectedValue.ToString();

            cmdParms[6] = new SqlParameter("@NewsAuditSuccess", System.Data.SqlDbType.Int);
            if (this.radiobtn_YES.Checked)
            { cmdParms[6].SqlValue = 0; }
            else
            { cmdParms[6].SqlValue = 1; }


            cmdParms[7] = new SqlParameter("@NewsKeyword", System.Data.SqlDbType.NVarChar, 100);
            cmdParms[7].SqlValue = this.txt_NewsKeyword.Text;

            cmdParms[8] = new SqlParameter("@NewsAuthor", System.Data.SqlDbType.NVarChar, 50);
            if (Request.Cookies["AdminName_CK"] != null)
            {
                cmdParms[8].SqlValue = Request.Cookies["AdminName_CK"].Value;
            }
            else
            {
                cmdParms[8].SqlValue = "";
            }
            cmdParms[9] = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            cmdParms[9].SqlValue = Convert.ToInt32(str);



            DBHelper db = new DBHelper();
            int iResult = db.ExecuteSql(strSQL, cmdParms);
            if (iResult > 0)
            {
                //Response.Write("插入成功！"); 
                Response.Redirect("NewsList.aspx");
            }
            else
            {
                Response.Write("插入失败！");
            }

            #endregion
        }
        else
        {
            #region 插入记录
            string strSQL = "INSERT INTO [News].[dbo].[NewsInfoTable]([NewsTitle],[NewsSource],[NewsSourceURL],[NewsSubtitle],[NewsContent],[NewsCategory],[NewsAuditSuccess],[NewsKeyword],[NewsAuthor])" +
                        " VALUES(@NewsTitle,@NewsSource,@NewsSourceURL,@NewsSubtitle,@NewsContent,@NewsCategory,@NewsAuditSuccess,@NewsKeyword,@NewsAuthor);";
            SqlParameter[] cmdParms = new SqlParameter[9];
            cmdParms[0] = new SqlParameter("@NewsTitle", System.Data.SqlDbType.NVarChar, 50);
            cmdParms[0].SqlValue = this.txt_NewsTitle.Text;

            cmdParms[1] = new SqlParameter("@NewsSource", System.Data.SqlDbType.NVarChar, 50);
            cmdParms[1].SqlValue = this.txt_NewsSource.Text;

            cmdParms[2] = new SqlParameter("@NewsSourceURL", System.Data.SqlDbType.NVarChar, 1000);
            cmdParms[2].SqlValue = this.txt_NewsSourceURL.Text;

            cmdParms[3] = new SqlParameter("@NewsSubtitle", System.Data.SqlDbType.NVarChar, 50);
            cmdParms[3].SqlValue = this.txt_NewsSubtitle.Text;

            cmdParms[4] = new SqlParameter("@NewsContent", System.Data.SqlDbType.Text);
            //cmdParms[4].SqlValue = this.tbContent.Text;
            cmdParms[4].SqlValue = Server.HtmlEncode(this.tbContent.Text);


            cmdParms[5] = new SqlParameter("@NewsCategory", System.Data.SqlDbType.NVarChar, 10);
            cmdParms[5].SqlValue = this.ddl_NewsCategory.SelectedValue.ToString();

            cmdParms[6] = new SqlParameter("@NewsAuditSuccess", System.Data.SqlDbType.Int);
            if (this.radiobtn_YES.Checked)
            { cmdParms[6].SqlValue = 0; }
            else
            { cmdParms[6].SqlValue = 1; }


            cmdParms[7] = new SqlParameter("@NewsKeyword", System.Data.SqlDbType.NVarChar, 100);
            cmdParms[7].SqlValue = this.txt_NewsKeyword.Text;

            cmdParms[8] = new SqlParameter("@NewsAuthor", System.Data.SqlDbType.NVarChar, 50);
            if (Request.Cookies["AdminName_CK"] != null)
            {
                cmdParms[8].SqlValue = Request.Cookies["AdminName_CK"].Value;
            }
            else
            {
                cmdParms[8].SqlValue = "";
            }


            DBHelper db = new DBHelper();
            int iResult = db.ExecuteSql(strSQL, cmdParms);
            if (iResult > 0)
            {
                //Response.Write("插入成功！"); 
                Response.Redirect("NewsList.aspx");
            }
            else
            {
                Response.Write("插入失败！");
            }
            #endregion
        }
        

    }
}