<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsListByCategory.ascx.cs" Inherits="CustomControl_NewsListByCategory" %>



<div class="NewsListByCategory">
            <div class="biaoti">
                <h3 class="tit"><asp:Label ID="lbl_CategoryName" runat="server" Text="新闻类别" CssClass="title"></asp:Label></h3>
                <div class="more_btn" frag="按钮" type="更多"><asp:LinkButton ID="lbtn_MoreCategoryUrl" runat="server">更多</asp:LinkButton></div>
            </div>

            <div class="NewsList10">

                    <ul class="wp_article_list">

                        <asp:PlaceHolder ID="ph_CategoryNewsList" runat="server"></asp:PlaceHolder>

                    </ul>

            </div>
</div>
