using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class CustomControl_NewsListByCategory : System.Web.UI.UserControl
{
    //public CustomControl_NewsListByCategory()
    //{
    //    CategoryName = "国内新闻";
    //    NewsListCount = 10;
    //}
    private string strCategoryName = "国内新闻";
    private int iNewsListCount = 10;
         
    public string CategoryName
    {
        get { return strCategoryName; }
        set { strCategoryName = value; }
    }
    public int NewsListCount
    {
        get { return iNewsListCount; }
        set { iNewsListCount = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (CategoryName != null )
            {
                InitCategoryByName(CategoryName, NewsListCount);
            }
            else
            {
                InitCategoryByName("国内新闻");
            }
            
        }
    }

    /// <summary>
    /// 根据新闻类别初始化 默认显示前10条记录
    /// </summary>
    /// <param name="strCategoryByName">新闻类别名称</param>
    /// <param name="iNewsDisplayCount">默认显示前10条记录</param>
    protected void InitCategoryByName(string strCategoryByName, int iNewsDisplayCount =10)
    {
        string strSQL = "SELECT top(@iNewsDisplayCount) NewsInfoTable.[ID] as ID,[NewsTitle],[NewsTime],NewsCategoryUrl FROM NewsInfoTable,NewsCategory where NewsInfoTable.NewsCategory = NewsCategory.ID and NewsCategory.NewsCategoryName = @NewsCategoryName order by NewsInfoTable.NewsTime DESC;";

        SqlParameter[] cmdParms = new SqlParameter[2];
        cmdParms[0] = new SqlParameter("@iNewsDisplayCount", System.Data.SqlDbType.Int);
        cmdParms[0].SqlValue = iNewsDisplayCount;

        cmdParms[1] = new SqlParameter("@NewsCategoryName", System.Data.SqlDbType.NVarChar,50);
        cmdParms[1].SqlValue = strCategoryByName;

        DBHelper db = new DBHelper();
        DataSet ds = db.GetDataSet(strSQL, cmdParms);

        if(ds.Tables[0].Rows.Count > 0)
        {
            this.lbtn_MoreCategoryUrl.PostBackUrl = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            this.lbl_CategoryName.Text = strCategoryByName;

            foreach( DataRow dr in ds.Tables[0].Rows)
            {
                Literal NewsListUp = new Literal();
                Literal NewsListDown = new Literal();

                string strID = dr.ItemArray[0].ToString();
                string strNewsTitle = dr.ItemArray[1].ToString();
                strNewsTitle = (strNewsTitle.Length > 22) ? strNewsTitle.Substring(0, 22) + "..." : strNewsTitle;

                DateTime dt = Convert.ToDateTime(dr.ItemArray[2].ToString());
                string NewsTime = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();

                NewsListUp.Text = "<li class=\"list_item i1\">" + 
                            "<div class=\"pr_fields\">" +
                             "   <span class=\"Article_Index\">" + strID + "</span>" +
                              "  <span class=\"Article_Title\">";

                              //<a href=\"http://www.htu.cn/2017/1123/c8955a107478/page.htm\" target=\"_blank\" >" + strNewsTitle + "</a>
                LinkButton lb = new LinkButton();
                lb.Text = strNewsTitle;
                lb.PostBackUrl = "~/News/NewsDetail.aspx?articleId=" + strID;
                NewsListDown.Text = "</span>" +
                            "</div>" +
                            "<div class=\"fields ex_fields\">" +
                             "   <span class=\"Article_PublishDate\">" + NewsTime + "</span>" +
                            "</div>" +
                        "</li>";
                ph_CategoryNewsList.Controls.Add(NewsListUp);
                ph_CategoryNewsList.Controls.Add(lb);
                ph_CategoryNewsList.Controls.Add(NewsListDown);
                
            }
        }


    }


}