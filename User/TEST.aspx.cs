using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class User_TEST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MenuLoad();
        }
    }

    /// <summary>
    /// 初始化 菜单
    /// </summary>
    protected void MenuLoad()
    {
        DataSet dsMenuCollection = new DataSet();
        DBHelper db = new DBHelper();
        dsMenuCollection = db.GetDataSet("SELECT [ID] ,[MenuName] ,[MenuAlt] ,[MenuFather] ,[MenuURL] ,[MenuRemark]  FROM [News].[dbo].[MenuInfo];");

        DataTable dtFatherMenu = dsMenuCollection.Tables[0];

        DataRow[] drFatherMenu = dtFatherMenu.Select("MenuFather=0");

        //先拿一级菜单
        Literal FatherMenuContent = new Literal();

        int iFatherMenuIndex = 1;
        foreach (DataRow ItemMenu in drFatherMenu)
        {
            string MenuURL = ItemMenu.ItemArray[4].ToString();
            string MenuName = ItemMenu.ItemArray[1].ToString();

            FatherMenuContent.Text += "<li class=\"nav-item i" + iFatherMenuIndex + "\">" +
                                      " <a class=\"\" href=\"" + MenuURL + "\" target=\"_self\"><span class=\"item-name\">" + MenuName + "</span></a><i class=\"mark\"></i>";

            #region 加载二级菜单
            string ItemMenuID = ItemMenu.ItemArray[0].ToString();
            DataRow[] drSubMenu = dtFatherMenu.Select("MenuFather=" + ItemMenuID);
            if (drSubMenu.Length > 0)
            {
                FatherMenuContent.Text += "<ul style=\"width: 110px; height: 258px; top: 43px; left: 0px; visibility: hidden;\" class=\"sub-nav\"> ";

                int iSubMenuIndex = 1;
                foreach (DataRow CurrentSubMenu in drSubMenu)
                {
                    string SubMenuURL = CurrentSubMenu.ItemArray[4].ToString();
                    string SubMenuName = CurrentSubMenu.ItemArray[1].ToString();

                    FatherMenuContent.Text += "<li style=\"display: block; width: 100%;\" class=\"nav-item i" + iFatherMenuIndex + "-" + iSubMenuIndex + " \">" +
                                           "<a style=\"display: block; width: auto;\" href=\"" + SubMenuURL + "\" target=\"_self\"><span class=\"item-name\">" + SubMenuName + "</span></a><i class=\"mark\"></i>" +
                                       "</li>";
                    iSubMenuIndex++;

                }


                FatherMenuContent.Text += "</ul>";
            }
            #endregion

            FatherMenuContent.Text += "</li>";
           

            iFatherMenuIndex++;
        }
        this.plh_MenuInfo.Controls.Add(FatherMenuContent);

    }
}