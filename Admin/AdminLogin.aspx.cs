using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Admin_AdminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ibtn_Login_Click(object sender, ImageClickEventArgs e)
    {
        //获取验证码
        string code = this.txt_Check.Text;
        //判断用户输入的验证码是否正确
        if (Request.Cookies["CheckCode"].Value == code)
        {
            DBHelper db = new DBHelper();
            string strSQL = "select COUNT(ID)  from AdminInfo where UserName = @UserName and UserPWD = @UserPWD;";

            SqlParameter[] cmdParms = new SqlParameter[2];
            cmdParms[0] = new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar, 50);
            cmdParms[0].SqlValue = this.txt_UserName.Text.Trim();

            cmdParms[1] = new SqlParameter("@UserPWD", System.Data.SqlDbType.NVarChar, 50);
            string strMD5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txt_Password.Text, "MD5");
            cmdParms[1].SqlValue = strMD5Password;


            int iResult = db.ExecuteSelect(strSQL, cmdParms);
            if (iResult > 0)
            {
                //记忆用户的登录状态
                HttpCookie cookie = new HttpCookie("AdminName_CK");
                cookie.Value = this.txt_UserName.Text.Trim();
                cookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(cookie);

                //页面跳转
                Response.Redirect("~/Admin/main.html");
            }
            else
            {
                this.rfv_Check.Text = "用户名或密码输入有误！";
                this.rfv_Check.IsValid = false;
            }
        }
        else
        {
            this.rfv_Check.Text = "验证码输入有误！";
            this.rfv_Check.IsValid = false;
        }

    }
}