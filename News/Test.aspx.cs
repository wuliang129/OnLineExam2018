using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class News_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucNewsListByCategory.CategoryName = "国际新闻";

        this.ucNewsListByCategory1.CategoryName = "国内新闻";
        this.ucNewsListByCategory1.NewsListCount = 5;
    }
}