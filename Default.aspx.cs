using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            this.ucNewsListByCategory_Second.NewsListCount = 6;

            this.ucNewsListByCategory_Second_TimeLeft.CategoryName = "体育新闻";
        }
    }
}