using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OnLineExam.BusinessLogicLayer;

public partial class Web_teacher_PwdModify : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            this.txtOldPwd.Focus();       
        } 
    }

    protected void lbtn_ModifyPwd_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["UserID_CK"].Value != null && Request.Cookies["UserType_CK"].Value != null)
        {
            Teachers teacherCurrent = new Teachers();//创建Teacher


            teacherCurrent.LoadData(HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8));
            string txtOldPwdMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtOldPwd.Text.Trim(), "MD5").ToString();
            if (teacherCurrent.UserPwd == txtOldPwdMD5)//验证用户输入原密码是否正确
            {
                string txtNewPwdMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPwd.Text.Trim(), "MD5").ToString();
                //stu.UserPwd = txtNewPwdMD5.ToString().Trim();
                if (teacherCurrent.ModifyPassword(teacherCurrent.UserID, txtOldPwdMD5, txtNewPwdMD5))//更改用户密码
                {
                    lblMessage.Text = "成功修改密码!";
                }
                else//修改密码失败
                {
                    lblMessage.Text = "修改密码失败!";
                }
            }
            else//原密码错误
            {
                lblMessage.Text = "输入原密码错误,请重新输入!";
            }
        }
        
    }
   
    protected void lbtn_Reset_Click(object sender, EventArgs e)
    {
        txtOldPwd.Text = txtNewPwd.Text = txtConfirmPwd.Text = "";
    }
}