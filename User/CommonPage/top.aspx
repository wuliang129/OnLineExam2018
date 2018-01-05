<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="Admin_top" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>无标题文档</title>
    <link href="/Admin/css/style.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="/Admin/js/jquery.js"></script>
    <script language="JavaScript" src="/scripts/Morning_JS.js"></script>
    <script type="text/javascript">
        $(function () {
            //顶部导航切换
            $(".nav li a").click(function () {
                $(".nav li a.selected").removeClass("selected")
                $(this).addClass("selected");
            })
        })
    </script>

</head>
<body style="background: url(images/topbg.gif) repeat-x;">
    <form id="form1" runat="server">
        <div class="topleft">
            <a href="UserMain.html" target="_parent">
                <img src="images/UserLogo.png" title="系统首页〉" /></a>
        </div>

        

        <div class="topright">
            <ul>
                <li><span id="ShowTime" style="font-size: 13px; color: #e9f2f7;"><script type="text/javascript">getDate()</script></span></li>
                <li><span>
                    <img src="images/help.png" title="帮助" class="helpimg" /></span><a href="#">帮助</a></li>
                <li><a href="#">关于</a></li>
                <li>
                    <asp:LinkButton ID="lbtn_Exit" runat="server" OnClick="lbtn_Exit_Click" >退出</asp:LinkButton></li>
            </ul>

            <div class="user">
                <asp:Label ID="lbl_top_name" runat="server" Text="用户名"></asp:Label>
                <i>消息</i>
                <b>
                    <asp:LinkButton ID="lbtn_top_infoCount" runat="server">5</asp:LinkButton></b>
            </div>

        </div>
    </form>
</body>
</html>
