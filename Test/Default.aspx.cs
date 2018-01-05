using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Windows.Forms;

public partial class Test_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //下面执行js代码  
        //this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "ShowMyMsg", "ShowMyMsg()", true);

       // Response.Write("<script>alert('查询语句执行出错！');window.location.href=DisplayData.html</script>");

        Response.Write("<script>window.showModelessDialog('ModelDialog.html')</script>");

        
    }
}