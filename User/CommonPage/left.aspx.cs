using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OnLineExam.CommonComponent;

public partial class User_left : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //HttpUtility.UrlDecode(Request.Cookies["UserName_CK"].Value, System.Text.Encoding.UTF8);
            if (Request.Cookies["UserType_CK"] != null)
            {
                string strSQL = "";
                string strUserType = HttpUtility.UrlDecode(Request.Cookies["UserType_CK"].Value, System.Text.Encoding.UTF8);
                if (strUserType == EnumHelper.GetDescription(UserType.Teacher))
                {
                    strSQL = "SELECT [NodeId],[ParentId],[Text],[Url],[Duty],[AdminUrl]  FROM TeacherTreeMenu";
                }
                else
                {
                    if (strUserType == EnumHelper.GetDescription(UserType.Student))
                    {
                        strSQL = "SELECT [NodeId],[ParentId],[Text],[Url],[Duty],[AdminUrl]  FROM StudentTreeMenu";
                    }
                    else
                    {
                        //管理员尚未处理
                    }
                    
                }
                InitUserCenterLeft(strSQL);
            }
        }
    }

    protected void InitUserCenterLeft(string strSQL)
    {
        DBHelper db = new DBHelper();
        DataSet ds = db.GetDataSet(strSQL);

        DataTable dt = ds.Tables[0].Copy();

        Literal Menu = new Literal();
       
       //一级菜单
       DataRow[] dtMenuLevel1 = dt.Select("ParentId=0");
       if (dtMenuLevel1.Length > 0)
       {
           foreach (DataRow dr in dtMenuLevel1)
           {
               Menu.Text += "<dd><div class=\"title\"><div style=\"background:url(images/leftico01.png) no-repeat left center; padding-left:25px;margin-left:10px;\"><a href=\"" + dr.ItemArray[3].ToString() + "\" >" + dr.ItemArray[2].ToString() + "</a></div></div>";
               
               #region 构建子菜单
               DataRow[] dtSubMenu = dt.Select("ParentId=" + dr.ItemArray[0].ToString());
               if (dtSubMenu.Length > 0)
               {
                   Menu.Text += "<ul class=\"menuson\">";
                   foreach (DataRow drSub in dtSubMenu)
                   {
                       Menu.Text += "<li><cite></cite><a href=\"" + drSub.ItemArray[3].ToString() + "\" target=\"rightFrame\">" + drSub.ItemArray[2].ToString() + "</a><i></i></li>";
                   }
                   Menu.Text += "</ul>";
               }
               #endregion

               Menu.Text += "</dd>";
           }
           this.ph_UserLeftMenu.Controls.Add(Menu);
       }
    }

    /// <summary>
    /// 退出系统
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtn_Exit_Click(object sender, EventArgs e)
    {
        #region 所有的session 和 cookie都取消

        for (int i = 0; i < this.Request.Cookies.Count; i++)
        {
            this.Response.Cookies[this.Request.Cookies[i].Name].Expires = DateTime.Now.AddDays(-1);
        }


        #endregion
        Response.Write("<script>window.parent.location.href='/Default.aspx'</script>");
    }

}