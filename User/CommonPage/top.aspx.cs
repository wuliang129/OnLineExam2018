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
        //在新窗口或 tab 显示网页
        //this.lbtn_Exit.Attributes.Add("onclick", "this.form.target='_NewWindows'");

        if (!IsPostBack)
        {
            if (Request.Cookies["UserName_CK"] != null && Request.Cookies["UserID_CK"] != null)
            {
                string strUserID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
                string strUserName = HttpUtility.UrlDecode(Request.Cookies["UserName_CK"].Value, System.Text.Encoding.UTF8);
                this.lbl_top_name.Text = strUserName + strUserID;
                InitNewInfoCount();
            }
            else
            {
                Response.Write("<script>window.parent.location.href='/User/UserLogin.aspx'</script>");
            }
            
        }
    }

    /// <summary>
    ///初始化 信息数
    /// </summary>
    protected void InitNewInfoCount()
    {

    }

    /// <summary>
    /// 退出系统
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtn_Exit_Click(object sender, EventArgs e)
    {
        #region 所有的session 和 cookie都取消

        for (int i = 0; i < this.Request.Cookies.Count;i++ )
        {
            this.Response.Cookies[this.Request.Cookies[i].Name].Expires = DateTime.Now.AddDays(-1);
        }

        
        #endregion
        Response.Write("<script>window.parent.location.href='/Default.aspx'</script>");
    }
}