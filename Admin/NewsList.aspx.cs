using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


public partial class Admin_NewsList : System.Web.UI.Page
{

    /// <summary>
    /// 当前选中页码  保存在服务器端
    /// 第0页  前台 第1页
    /// </summary>
   static  int iPageIndex = 1;

    /// <summary>
   /// /默认页面有 10 记录
    /// </summary>
    int iPageSize = 10;

    DataSet dsAllRecord = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateNewsInfoByPageIndex(iPageIndex);//显示第一页记录

            //InitPageList();
        }

        //UpdateNewsInfoByPageIndex(iPageIndex);
        //更新页码 列表 CSS
        InitPageList();
       
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Load_AllRecord_gv_NewsList()
    {
        string strSQL = "SELECT [ID],[NewsTitle],[NewsTime],[NewsSource],[NewsSourceURL],[NewsSubtitle],[NewsContent],[NewsCategory]," +
                        "[NewsAuditSuccess],[NewsKeyword],[NewsAuthor],[NewsRemark]  FROM [News].[dbo].[NewsInfoTable] order by [NewsTime] DESC;";
        DBHelper db = new DBHelper();
        DataSet ds = db.GetDataSet(strSQL);
        dsAllRecord = ds;

        this.lbl_NewsTotalCount.Text = ds.Tables[0].Rows.Count.ToString();
        this.lbl_PageIndex.Text = (iPageIndex + 1).ToString();


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_NewsList_Init(object sender, EventArgs e)
    {
        UpdateNewsInfoByPageIndex(iPageIndex);//显示第一页记录
    }

    /// <summary>
    /// 是否全选
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chk_AllItem_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chAll = (CheckBox)sender;
        foreach(GridViewRow gvRow in this.gv_NewsList.Rows)
        {
            CheckBox chkItem = (CheckBox)gvRow.FindControl("chk_Item");
            chkItem.Checked = chAll.Checked;
        }
    }

    /// <summary>
    /// 删除当前新闻
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_NewsList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gv_NewsList.Rows[e.RowIndex].Cells[1].Text);
        string strSQL = "DELETE FROM [News].[dbo].[NewsInfoTable] WHERE ID=@ID;";

        DBHelper db = new DBHelper();
        SqlParameter[] cmdParms = new SqlParameter[1];
        cmdParms[0] = new SqlParameter("@ID", System.Data.SqlDbType.Int);
        cmdParms[0].SqlValue = id;

        if (db.ExecuteSql(strSQL, cmdParms) > 0)
       {
           //gv_NewsList_Init(sender, e);//更新
           UpdateNewsInfoByPageIndex(iPageIndex);
       }
        else
       {

        }
    }

    /// <summary>
    /// 删除选中记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkBtn_Del_Click(object sender, EventArgs e)
    {
        ArrayList arDelCollection = new ArrayList();

        foreach (GridViewRow gvRow in this.gv_NewsList.Rows)
        {
            CheckBox chkItem = (CheckBox)gvRow.FindControl("chk_Item");
            //CheckBox chkItem = (CheckBox)gvRow.Cells[0].FindControl("chk_Item");
            if (chkItem.Checked)
            {
                arDelCollection.Add(gvRow.Cells[1].Text);
            }
        }

        if(arDelCollection.Count > 0)
        {
            DBHelper db = new DBHelper();

            string strSQL = "DELETE FROM [News].[dbo].[NewsInfoTable] WHERE ";
            for(int i=0;i<arDelCollection.Count; i++)
            {
                strSQL += "ID" + "=@ID" + i.ToString() + " or ";
            }
            strSQL = strSQL.Substring(0, strSQL.Length - 3) + ";";

            SqlParameter[] cmdParms = new SqlParameter[arDelCollection.Count];

            for(int j=0;j<arDelCollection.Count;j++)
            {
                cmdParms[j] = new SqlParameter("@ID" + j.ToString(), System.Data.SqlDbType.Int);
                cmdParms[j].SqlValue = Convert.ToInt32(arDelCollection[j]);
            }

            if ( db.ExecuteSql(strSQL, cmdParms) > 0)
            {
                //gv_NewsList_Init(sender, e);//更新
                Load_AllRecord_gv_NewsList();

                UpdateNewsInfoByPageIndex(iPageIndex);//更新
                Page_Load( sender,  e);
            }
            else
            {

            }


        }
        else
        {
            //Response.Write("<script type='text/javascript'>$(document).ready(function () {$(\".tip_DelNoInfo\").fadeIn(200);});</script>");
            //this.Page.RegisterStartupScript("dw_ed", "<script>Displaytip_DelNoInfo();</script>");
        }
    }

    protected void gv_NewsList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("NewsEdit.aspx?Admin_articleId=" + gv_NewsList.Rows[e.NewEditIndex].Cells[1].Text);
    }



   /// <summary>
   /// 根据 输入的页码  更新前台 GrideView新闻信息
   /// </summary>
    /// <param name="PageIndex">前台显示页码 1页 开始   对应于后台的 0页</param>
    protected void UpdateNewsInfoByPageIndex(int PageIndex)
    {
        iPageIndex = PageIndex;//记录当前页码
        //更新前台 页码
        this.lbl_PageIndex.Text = PageIndex.ToString();

        //更新页码 列表 CSS
        //InitPageList();


        #region 更新新闻 列表内容
        DataTable dt = new DataTable();
        if(dsAllRecord.Tables.Count < 1)
        {
            Load_AllRecord_gv_NewsList();//
        }
        dt = dsAllRecord.Tables[0].Copy();

        int iEndIndex = PageIndex * iPageSize;

        int iStartIndex = iEndIndex - (iPageSize - 1);

        iEndIndex = (PageIndex * iPageSize) > dt.Rows.Count ? dt.Rows.Count : (PageIndex * iPageSize);

        iStartIndex--;//前台到后台转换  1 0
        iEndIndex--;

        if (dt.Rows.Count > 0)
        {
            DataTable dtTemp = new DataTable();
            dtTemp = dt.Clone();
            dtTemp.Rows.Clear();

            for(int i= iStartIndex;i<=iEndIndex;i++)
            {
                dtTemp.ImportRow(dt.Rows[i]);
            }

            this.gv_NewsList.DataSource = dtTemp;
            this.gv_NewsList.DataBind();
        }

        #endregion

        
    }


    /// <summary>
    /// 初始化 页码列表
    /// </summary>
    protected void InitPageList()
    {

        this.ph_PageList.Controls.Clear();//删除所有已有控件

        double dCount = dsAllRecord.Tables[0].Rows.Count;
        int PageCount = Convert.ToInt32((Math.Ceiling(dCount / iPageSize)));//获取页码总数

        #region  设置页码开始和结束值
        //for(int i = 0;i<= (PageCount + 1);i++)
        int iStartPage = iPageIndex - 1 ;  //前台的 从 1开始  后台 从 0 开始 
        int iEndIndex = PageCount;

        /*如果 开始页码 <= 5 从 0 开始  */
        iStartPage = (iStartPage <= 5) ? 0 : iStartPage;

        /*如果 结束页码 比开始页码 大 10个 结束页码是=开始页码 + 10 否则 结束页码= 总页码数 */
        iEndIndex = ((PageCount - iStartPage) >= 10) ? (iStartPage + 10 ): PageCount;

        /*  结束页码= 总页码数 时 开始页码=  总页码数 -10  保证前台显示最多10个页码列表（包含上一个“<”） */
        if (PageCount == iEndIndex)
            iStartPage = ((PageCount - 10)>=0)?(PageCount - 10):0;

        #endregion

        // int i = (iStartPage == 0) ? 0 : iStartPage - 1;

        #region  逐项构建 页码列表  第一个和最后一个特殊处理
        for (int i = iStartPage; i <= (iEndIndex + 1); i++)
        {
            Literal PageListInfoUp = new Literal();
            Literal PageListInfoDown = new Literal();

            if(iPageIndex == i) //当前选中页码 css 区别于其他的
            {
                PageListInfoUp.Text = " <li class=\"paginItem current\">";
            }
            else
            {
                PageListInfoUp.Text = " <li class=\"paginItem\">";
            }
            
            
           LinkButton lbtn = new LinkButton();
            lbtn.ID = "LinkButton" + i.ToString();


            if ((i == 0) || (i == iStartPage))
            {
                /* 第一个列表：上一个"<"  在用户选中 第一个时 上一个显示为灰色 否则 为 蓝色   */
                if (iPageIndex == 1)
                    lbtn.CssClass = "pagepre";
                else
                    lbtn.CssClass = "pagepre_back";
            }
            else
            {
                if (i == (PageCount + 1) || i == (iEndIndex + 1))
                {
                    /* 最后一个列表：下一个"<"  在用户选中 最后一个时 下一个显示为灰色 否则 为 蓝色   */
                    if (iPageIndex == PageCount)
                        lbtn.CssClass = "pagenxt_end";
                    else
                        lbtn.CssClass = "pagenxt";
                }
                else
                {
                    lbtn.Text = i.ToString();
                    lbtn.CssClass = "paginItem";
                }
                
            }
            
            
            lbtn.Click += new System.EventHandler(LinkButton_Click);
                
            PageListInfoDown.Text = "</li>";

            this.ph_PageList.Controls.Add(PageListInfoUp);
            this.ph_PageList.Controls.Add(lbtn);
            this.ph_PageList.Controls.Add(PageListInfoDown);
        }
        #endregion

    }

    protected void LinkButton_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        string strIndex = lbtn.ID.ToString().Substring(10, lbtn.ID.ToString().Length - 10);
        int iIndex = Convert.ToInt32(strIndex);

        
        if (iIndex == 0)
        {
            /*用户单击上一个列表： 如果已经是第一个 显示第一页 否则 显示前一页*/
            iPageIndex = (iPageIndex > 1) ? --iPageIndex : 1;
            UpdateNewsInfoByPageIndex(iPageIndex);
        }
        else
        {
            double dCount = dsAllRecord.Tables[0].Rows.Count;
            int PageCount = Convert.ToInt32((Math.Ceiling(dCount / iPageSize)));
            if (iIndex == (PageCount + 1))
            {
                /*用户单击下一个列表： 如果已经是最后一个 显示最后一页 否则 显示下一页*/
                iPageIndex = (iPageIndex >= PageCount) ? PageCount : ++iPageIndex;
                UpdateNewsInfoByPageIndex(iPageIndex);

            }
            else
            {
                iPageIndex = iIndex;
                UpdateNewsInfoByPageIndex(iPageIndex);
            }

        }
        
        /*触发页面更新  本函数处理完 前台页码不会自动刷新  需要手动触发  */
        Page_Load(sender, e);
    }

}



