using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;
using OnLineExam.CommonComponent;


public partial class User_UserLogin : System.Web.UI.Page
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
            string strSQL = "";
            if (this.rbtn_UserType_Student.Checked)
            {
                strSQL = "SELECT [Sid],[StudentId],[StudentName],[StudentPWD] FROM Students where [StudentId] = @UserID and [StudentPWD] = @UserPWD;";
            }
            else
            {
                if (this.rbtn_UserType_Teacher.Checked)
                {
                    strSQL = "SELECT [ID],[TeacherId],[TeacherName],[TeacherPWD] FROM Teacher_InfoTable where [TeacherId] = @UserID and [TeacherPWD] = @UserPWD;";
                }
                else
                {
                    strSQL = "SELECT [ID],[UserID],[UserName],[UserPwd] FROM Users where [UserID] = @UserID and [UserPwd] = @UserPWD;";
                }
            }
            
            SqlParameter[] cmdParms = new SqlParameter[2];
            cmdParms[0] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar, 50);
            cmdParms[0].SqlValue = this.txt_UserID.Text.Trim();

            cmdParms[1] = new SqlParameter("@UserPWD", System.Data.SqlDbType.NVarChar, 100);
            string strMD5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txt_Password.Text, "MD5");
            cmdParms[1].SqlValue = strMD5Password;


           DataSet ds  = db.GetDataSet(strSQL, cmdParms);
           if (ds.Tables[0].Rows.Count > 0)
            {
               //首先清空cookies 
                Clear_Cookies();

               //创建cookie 记录用户的信息
               CreateCookie(ds);
               //页面跳转
               Response.Redirect("~/User/CommonPage/UserMain.html");
            }
            else
           {
               this.rfv_Check.Text = "<img src=\"../Image/User/waringicon.png\" />用户名或密码输入有误！";
               this.rfv_Check.IsValid = false;
           }
        }
        else
        {
            this.rfv_Check.Text = "<img src=\"../Image/User/waringicon.png\" />验证码输入有误！";
            this.rfv_Check.IsValid = false;         
        }
    
    }


    /// <summary>
    /// 记录用的状态 登录状态  姓名  ID
    /// </summary>
    private void CreateCookie(DataSet ds)
    {
        //记忆用户的登录状态  保存姓名
        HttpCookie cookieUserName = new HttpCookie("UserName_CK");
        cookieUserName.Value = HttpUtility.UrlEncode(ds.Tables[0].Rows[0].ItemArray[2].ToString(),System.Text.Encoding.UTF8);//编码出错
        cookieUserName.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookieUserName);

        //记忆用户的类型状态
        HttpCookie cookieUserType = new HttpCookie("UserType_CK");
        if (this.rbtn_UserType_Student.Checked)
        {
            cookieUserType.Value = HttpUtility.UrlEncode(EnumHelper.GetDescription(UserType.Student), System.Text.Encoding.UTF8);//编码出错

        }
        else
        {
            cookieUserType.Value = HttpUtility.UrlEncode(EnumHelper.GetDescription(UserType.Teacher), System.Text.Encoding.UTF8);//编码出错
        }
        cookieUserType.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookieUserType);


        //记忆用户的 ID 学号或工号
        HttpCookie cookieUserID = new HttpCookie("UserID_CK");
        cookieUserID.Value = HttpUtility.UrlEncode(ds.Tables[0].Rows[0].ItemArray[1].ToString(), System.Text.Encoding.UTF8);//编码出错
        cookieUserID.Expires = DateTime.Now.AddDays(1);
        Response.AppendCookie(cookieUserID);

        //Session["userID"] = ds.Tables[0].Rows[0].ItemArray[1].ToString();//存储用户学号或工号
        
    }



    
    /// <summary>
    /// 清空所有cookie
    /// </summary>
    protected void Clear_Cookies()
    {
        for (int i = 0; i < this.Request.Cookies.Count; i++)
        {
            this.Response.Cookies[this.Request.Cookies[i].Name].Expires = DateTime.Now.AddDays(-1);
        }
    }
}