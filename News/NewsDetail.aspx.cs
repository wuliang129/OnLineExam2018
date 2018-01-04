using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class News_NewsDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(!IsPostBack)
        //{
            string str = Request.QueryString["articleId"];
            if (str != null)
            {
                LoadNewsInfo(str);
            }
            
        //}
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="strNewsID"></param>
    protected void LoadNewsInfo(string strNewsID)
    {
        //string strSQL = "SELECT NewsInfoTable.ID as ID,[NewsTitle],[NewsTime],[NewsSource],[NewsSourceURL],[NewsSubtitle],[NewsContent],[NewsCategory],[NewsRemark],NewsCategoryUrl FROM NewsInfoTable,NewsCategory where NewsInfoTable.NewsCategory = NewsCategory.ID and NewsInfoTable.NewsAuditSuccess =1 and NewsInfoTable.ID=@NewsID ;";
        string strSQL = "SELECT NewsInfoTable.ID as ID,[NewsTitle],[NewsTime],[NewsSource],[NewsSourceURL],[NewsSubtitle],[NewsContent],NewsCategoryName,[NewsRemark],NewsCategoryUrl FROM NewsInfoTable,NewsCategory where NewsInfoTable.NewsCategory = NewsCategory.ID and NewsInfoTable.NewsAuditSuccess =1 and NewsInfoTable.ID=@NewsID;";
        
        SqlParameter[] cmdParms = new SqlParameter[1];
        cmdParms[0] = new SqlParameter("@NewsID", System.Data.SqlDbType.NVarChar, 20);
        cmdParms[0].SqlValue = strNewsID;

        DBHelper db = new DBHelper();
        DataSet ds = new DataSet();
        ds = db.GetDataSet(strSQL,cmdParms);

        if (ds.Tables[0].Rows.Count > 0)
        {
            this.hl_NewsCategory.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();
            this.hl_NewsCategory.NavigateUrl = ds.Tables[0].Rows[0].ItemArray[9].ToString();

            this.lbl_Title.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            this.lbl_NewsTime.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();

            this.hl_NewsSource.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            this.hl_NewsSource.NavigateUrl = ds.Tables[0].Rows[0].ItemArray[4].ToString();

            Literal NewsInfo = new Literal();
            NewsInfo.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
            NewsInfo.Text = Server.HtmlDecode(NewsInfo.Text);
            this.ph_NewsInfo.Controls.Add(NewsInfo);
        }
    }
}