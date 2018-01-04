using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_top : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["AdminName_CK"] != null)
            {
                string strUserName = Request.Cookies["AdminName_CK"].Value;
                this.lbl_top_name.Text = strUserName;
                InitNewInfoCount();
            }
            else
            {
                //Response.Write("<script>window.parent.location.href='AdminLogin.aspx'</script>");
            }
            
        }
    }

    /// <summary>
    /// 初始化新消息数据
    /// </summary>
    protected void InitNewInfoCount()
    {

    }
}