<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/User_MasterPage.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="News_Test" %>

<%@ Register Src="~/CustomControl/NewsListByCategory.ascx" TagName="ucNewsListByCategory"  TagPrefix="uc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>用户自定义控件测试</title>
    <link rel="stylesheet" type="text/css" href="../CSS/NewsStyle.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="width:100%; background-color:#0f0">
    <uc:ucNewsListByCategory ID="ucNewsListByCategory" runat="server" />

     <uc:ucNewsListByCategory ID="ucNewsListByCategory1" runat="server" />
        </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <div style="background-color:#F00;height:500px;">
        <uc:ucNewsListByCategory ID="ucNewsListByCategory2" runat="server" />
        </div>

</asp:Content>

