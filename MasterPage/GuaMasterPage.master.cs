using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class _64Gua_GuaMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //读取cookie
        /////////////////////////
        string str_welcome;
        if (Request.Cookies["UserName_CK"] != null)
        {
            str_welcome = "欢迎：" + Request.Cookies["UserName_CK"].Value;
        }
        else
        {
            str_welcome = "游客登录"; 
        }
        this.lab_username.Text = str_welcome;
        //////////////////
    }
}
