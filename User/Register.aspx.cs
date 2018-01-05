using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing;

public partial class user_loginRegister_Register : System.Web.UI.Page
{
    DBHelper db = new DBHelper();
    protected void user_loginRegister_Load(object sender, EventArgs e)
    {

    }
    protected void ibtn_Register_Click(object sender, ImageClickEventArgs e)
    {
        //调用IsUsserNameAccordRegular自定义方法判断用户名输入的是否满足正则表达式要求
        if (IsUsserNameAccordRegular())
        {
            //调用自定义IsUserNameExit方法判断用户名是否已存在
            if (IsUserNameExit())
            {
                this.labUser.Text = "<span style=\"background-image:url(../Image/User/waringicon.png); background-repeat:no-repeat; background-position:left center; padding-left:20px;\"> 用户名已存在 <span>";
                this.labUser.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                #region 所有验证通过
                //获取用户填写的会员名
                string userName = txtName.Text.Trim();
                //获取用户填写的密码并使用MD5进行加密
                string userPass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text, "MD5");

                string sex = "";
                //获取用户选择的性别
                if (radlistSex.SelectedValue.Trim() == "男")
                {
                    sex = "男";
                }
                else
                {
                    sex = "女";
                }
                //获取电话号
                string phone = txtPhone.Text.Trim();
                //获取电子邮件地址
                string email = txtEmail.Text.Trim();
                //获取所在城市
                string city = txtCity.Text.Trim();
                //找回密码时的问题和答案
                string str_question = this.txt_Question.Text.Trim();
                string str_answer = this.txt_Answer.Text.Trim();
                //创建SQL语句，该语句用来添加用户的详细信息
                string strSQL = "INSERT INTO UserInfo (UserName,UserPWD,UserSex,UserTelephone,UserEmail,UserCity,UserRetrievePWDQuestion,UserRetrievePWDAnswer,UserCreateDate) " +
                                              " VALUES(@UserName,@UserPWD,@UserSex,@UserTelephone,@UserEmail,@UserCity,@UserRetrievePWDQuestion,@UserRetrievePWDAnswer,@UserCreateDate)";
          
                SqlParameter[] cmdParms = new SqlParameter[9];
                cmdParms[0] = new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar, 50);
                cmdParms[0].SqlValue = this.txtName.Text.Trim();

                cmdParms[1] = new SqlParameter("@UserPWD", System.Data.SqlDbType.NVarChar, 50);
                cmdParms[1].SqlValue = userPass;

                cmdParms[2] = new SqlParameter("@UserSex", System.Data.SqlDbType.NVarChar, 10);
                cmdParms[2].SqlValue = sex;

                cmdParms[3] = new SqlParameter("@UserTelephone", System.Data.SqlDbType.NVarChar, 20);
                cmdParms[3].SqlValue = phone;

                cmdParms[4] = new SqlParameter("@UserEmail", System.Data.SqlDbType.NVarChar, 80);
                cmdParms[4].SqlValue = email;

                cmdParms[5] = new SqlParameter("@UserCity", System.Data.SqlDbType.NVarChar, 50);
                cmdParms[5].SqlValue = city;

                cmdParms[6] = new SqlParameter("@UserRetrievePWDQuestion", System.Data.SqlDbType.NVarChar, 200);
                cmdParms[6].SqlValue = str_question;

                cmdParms[7] = new SqlParameter("@UserRetrievePWDAnswer", System.Data.SqlDbType.NVarChar, 200);
                cmdParms[7].SqlValue = str_answer;

                cmdParms[8] = new SqlParameter("@UserCreateDate", System.Data.SqlDbType.DateTime);
                cmdParms[8].SqlValue = System.DateTime.Now;


                int iResult = db.ExecuteSql(strSQL, cmdParms);
                if (iResult > 0)
                {
                    //RegisterStartupScript("", "<script>alert('注册成功！')</script>");
                    //清空文本框中的信息
                    txtName.Text  = txtPhone.Text = txtEmail.Text = txtCity.Text = this.txt_Question.Text = this.txt_Answer.Text = "";

                    ///////写入cookie
                    //记忆用户的登录状态
                    HttpCookie cookie = new HttpCookie("UserName_CK");
                    cookie.Value = this.txtName.Text.Trim();
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie);
                    //页面跳转
                    Response.Redirect("~/Default.aspx");
                    //////////////////////////////
                }
                else
                {
                    //RegisterStartupScript("", "<script>alert('请正确填写信息！')</script>");
                    //
                }
                #endregion
            }
        }
        else
        {
            this.labUser.Text = "<span style=\"background-image:url(../Image/User/waringicon.png); background-repeat:no-repeat; background-position:left center; padding-left:20px;\"> 请输入数字、字母、下划线 <span>";
            this.labUser.ForeColor = System.Drawing.Color.Red;
        }
    }


    /// <summary>
    /// 读取数据库 判断用户名是否已存在
    /// 存在返回真 否则假
    /// </summary>
    /// <returns></returns>
    protected bool IsUserNameExit()
    {
        //创建一个布尔型变量并初始化为false;
        bool blIsName = false;
        //创建SQL语句，该语句用来判断用户名是否存在
        string strSQL = "select COUNT(ID)  from UserInfo where UserName = @UserName;";
        SqlParameter[] cmdParms = new SqlParameter[1];
        cmdParms[0] = new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar, 50);
        cmdParms[0].SqlValue = this.txtName.Text.Trim();

        int iResult = db.ExecuteSelect(strSQL, cmdParms);
        if (iResult > 0)
        {
            blIsName = true;
        }
        else
        {
            blIsName = false;
        }
        //返回布尔值变量
        return blIsName;
        

    }


    /// <summary>
    /// 判断用户名输入的是否满足要求
    /// </summary>
    /// <returns></returns>
    protected bool IsUsserNameAccordRegular()
    {
        //创建一个布尔型变量并初始化为false;
        bool blNameFormar = false;
        //设置正则表达式
        Regex re = new Regex("^\\w+$");
        //使用Regex对象中的IsMatch方法判断用户名是否满足正则表达式
        if (re.IsMatch(txtName.Text))
        {
            //设置布尔变量为true
            blNameFormar = true;
            //设置label控件的颜色
            labUser.ForeColor = System.Drawing.Color.Black;
        }
        else
        {
            labUser.ForeColor = System.Drawing.Color.Red;
            blNameFormar = false;
        }
        //返回布尔型变量
        return blNameFormar;
    }


    /// <summary>
    /// 根据用户输入内容进行相应提示
    /// 1、用户名已存在
    /// 2、可以注册
    /// 3、用户名不能为空
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtName_TextChanged(object sender, EventArgs e)
    {

        //判断用户名是否为空
        if (txtName.Text == "")
        {
            //使用Label控件给出提示
            this.labUser.Text = "<span style=\"background-image:url(../Image/User/waringicon.png); background-repeat:no-repeat; background-position:left center; padding-left:20px;\"> 用户名不能为空 <span>";
            this.labUser.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            //调用自定义IsUsserNameAccordRegular方法判断用户名是否满足格式要求
            if (IsUsserNameAccordRegular())
            {
                //调用isName自定义方法判断用户名是否已注册
                if (IsUserNameExit())
                {
                    this.labUser.Text = "<span style=\"background-image:url(../Image/User/waringicon.png); background-repeat:no-repeat; background-position:left center; padding-left:20px;\"> 用户名已存在 <span>";
                    this.labUser.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.labUser.Text = "可以注册!";
                    this.labUser.ForeColor = System.Drawing.Color.Blue;
                }
            }
            else
            {
                this.labUser.Text = "<span style=\"background-image:url(../Image/User/waringicon.png); background-repeat:no-repeat; background-position:left center; padding-left:20px;\"> 请输入数字、字母、下划线 <span>";
                this.labUser.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
    
    protected void ibtn_Return_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/User/UserLogin.aspx");
    }
}
