<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/User_MasterPage.master" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="News_NewsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/NewsStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="div_body_maincontent_NewsContent">
        <div class="NewsContent_logo">
            <asp:HyperLink ID="hl_NewsCategory" runat="server" NavigateUrl="#" Target="_self">国内新闻</asp:HyperLink>
        </div>

        <div class="NewsContent_title">
            <h1><asp:Label ID="lbl_Title" runat="server" Text="贡献中国智慧"></asp:Label></h1>
        </div>

        <div class="NewsContent_sourcelink">
            <table style="width:100%;">
                <tr>
                    <td class="NewsContent_sourcelink_left">
                        <img src="../Image/News/News_shareIcon.jpg" />
                    </td>
                    <td  class="NewsContent_sourcelink_right">
                        <asp:Label ID="lbl_NewsTime" runat="server" Text="2017年11月30日20:53" style="margin-right:25px;"></asp:Label>
                        新闻来源：<asp:HyperLink ID="hl_NewsSource" runat="server" NavigateUrl="#">人民日报海外版-海外网</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr style="width:100%; margin:15px 0px 25px 0px;" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="NewsContent_conteandAD">
            <div class="NewsContent_conteandAD_left">
                <asp:PlaceHolder ID="ph_NewsInfo" runat="server"></asp:PlaceHolder>
            </div>
            <div class="NewsContent_conteandAD_right">
                <img border="0" src="//d9.sina.com.cn/pfpghc2/201710/18/1e454486d03f45f9acf2e38fbe021aad.jpg" style="width:300px;height:500px;border:0" alt="//d9.sina.com.cn/pfpghc2/201710/18/1e454486d03f45f9acf2e38fbe021aad.jpg">
            </div>
        </div>
    </div>
</asp:Content>

