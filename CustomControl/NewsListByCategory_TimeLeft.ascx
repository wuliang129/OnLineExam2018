<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsListByCategory_TimeLeft.ascx.cs" Inherits="NewsListByCategory_TimeLeft" %>



<div class="NewsListByCategory" style="padding: 0px 25px 0px 15px; width: 350px;">
    <div class="biaoti">
        <h3 class="tit">
            <asp:Label ID="lbl_CategoryName" runat="server" Text="新闻类别" CssClass="title"></asp:Label></h3>
        <div class="more_btn" frag="按钮" type="更多">
            <asp:LinkButton ID="lbtn_MoreCategoryUrl" runat="server">更多</asp:LinkButton></div>
    </div>

    <div class="sudy-scroll" >
        <div  class="sudy-scroll-wrap">

            <ul class="scroll1">
                <%-- <li style="height: 65px;" class="i1"><div class="ybdt_list clearfix"><div class="tab_left"><div class="time_top">12月</div><div class="time_bottom">15</div></div><div class="tab_right"><a href="/2017/1215/c8957a109399/page.htm" target="_blank" >我校教师应邀赴凤泉区做学习十九大精神法治讲座</a></div></div></li>--%>

                <asp:PlaceHolder ID="ph_CategoryNewsList" runat="server"></asp:PlaceHolder>

            </ul>

        </div>

        <div class="sudy-scroll-page">
            <a class="page-index page-0 " href="javascript:;"><span>0</span></a>
            <a class="page-index page-1 " href="javascript:;"><span>1</span></a>
        </div>

    </div>
    


</div>
