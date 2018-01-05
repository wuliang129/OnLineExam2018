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
using OnLineExam.CommonComponent;
using DoctypeEncodingValidation;
using System.IO;


/*
 *  问答题的模糊判断还没处理  
 * 
 * */
public partial class Web_student_HTMLCodeDetection : System.Web.UI.Page
{

    string strURL = "";//当前用户保存的网页相对路径
    protected void Page_Load(object sender, EventArgs e)
    {
       // string WindowName = "win" + System.DateTime.Now.Ticks.ToString();
       //this.Page.RegisterOnSubmitStatement("js", "window.open('','" + WindowName + "','width=800,height=600')");
       // this.Form.Target = WindowName;

        //在新窗口或 tab 显示网页
        this.btn_RunCode.Attributes.Add("onclick", "this.form.target='_newName'");


        if (!IsPostBack)
        {
            this.txt_UserCode.Text = @"<html xmlns='http://www.w3.org/1999/xhtml'>" + System.Environment.NewLine +
   "  <head>" + System.Environment.NewLine +
       "    <meta http-equiv='Content-Type' content='text/html' charset='gb2312' />" + System.Environment.NewLine +
        "    <title>无标题文档</title>" + System.Environment.NewLine +
        "    <script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script>" + System.Environment.NewLine +
           "    <script src='../JS/tecate.js' type='text/javascript'></script>" + System.Environment.NewLine +
    "  </head>" + System.Environment.NewLine +

    "  <body>" + System.Environment.NewLine +
        "  <!--代码写入此处下方!--><br/>测试页面" + System.Environment.NewLine +
   "  </body>" + System.Environment.NewLine +
"</html>";
        }
    }

    /// <summary>
    /// 运行代码  
    /// 生成一个新的网页 运行用户提交HTML代码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_RunCode_Click(object sender, EventArgs e)
    {
        if (!IsUserInputNull())
        {
            if (strURL == "")
            {
                HTMLHelpClass help = new HTMLHelpClass();
                strURL = help.CreateHtml(this.txt_UserCode.Text, HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8));//    
                //返回路径 显示网页
                Response.Redirect(strURL);


            }
            else
            {
                //返回路径 显示网页
                Response.Redirect(strURL);

            }
        }
    }

    /// <summary>
    /// 检测用户输入代码是否为空 为空 返回 true  否则为 false
    /// </summary>
    protected bool IsUserInputNull()
    {
        if (this.txt_UserCode.Text.Trim().Length < 1)
        {
            //alert("请选择答案！");
            this.cv_DisplayMsg.IsValid = false;
            return true;
        }
        else
        {
            this.cv_DisplayMsg.IsValid = true;
            return false;
        }
    }

    /// <summary>
    /// 代码检测 本地自己检测
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_CodeDetection_Click(object sender, EventArgs e)
    {
        string strUserID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);

        HTMLHelpClass htmlhelp = new HTMLHelpClass();
        if (!htmlhelp.IsFileExit(strUserID))
        {
            ////不存在此文件
            //Response.Write("<script language=javascript>alert('请先保存代码！')</script>");
            //return;
            btn_SaveCode_Click(sender, e);
        }

        //相对路径
        //string urlpath = System.Web.HttpContext.Current.Request.Url.AbsoluteUri + this.lblUser.Text + "-" + DateTime.Now.ToString("yyyyMMdd") + ".html";
        string urlpath = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;//当前页面逻辑路径
        urlpath = urlpath.Substring(0, urlpath.Length - "/User/student/HTMLCodeDetection.aspx".Length);//当前服务器逻辑路径 
        urlpath += htmlhelp.GetFilePath(strUserID);
        HTMLValidate htmlValidate = new HTMLValidate(urlpath);

        this.txt_DetectionResult.Text = htmlValidate.GetaCheckResult(htmlValidate);
    }

    /// <summary>
    /// 保存代码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_SaveCode_Click(object sender, EventArgs e)
    {
        string strUserID = HttpUtility.UrlDecode(Request.Cookies["UserID_CK"].Value, System.Text.Encoding.UTF8);
        HTMLHelpClass help = new HTMLHelpClass();
        strURL = help.CreateHtml(this.txt_UserCode.Text, strUserID);//

        if (strURL == null)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "scriptKey", "alert('保存网页失败！');", true);
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "scriptKey", "alert('保存网页成功！');", true);
        }
    }



    /// <summary>
    /// 清空代码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        this.txt_UserCode.Text = "";
    }
}