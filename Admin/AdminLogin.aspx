<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="Admin_AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>欢迎登录后台管理系统</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="js/jquery.js"></script>
    <script src="js/cloud.js" type="text/javascript"></script>


    <script language="javascript">
        $(function () {
            $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            $(window).resize(function () {
                $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            })
        });
    </script>

</head>

<body style="background-color: #1c77ac; background-image: url(images/light.png); background-repeat: no-repeat; background-position: center top; overflow: hidden;">



    <div id="mainBody">
        <div id="cloud1" class="cloud"></div>
        <div id="cloud2" class="cloud"></div>
    </div>


    <div class="logintop">
        <span>欢迎登录后台管理界面平台</span>
        <ul>
            <li><a href="../Default.aspx">回首页</a></li>
            <li><a href="#">帮助</a></li>
            <li><a href="#">关于</a></li>
        </ul>
    </div>

    <div class="loginbody">

        <span class="systemlogo"></span>


        <div class="loginbox">
            <form runat="server" id="loginForm">
                <table>
                    <tr>
                        <td style="height: 70px; width: 230px;"></td>
                        <td style="height: 70px; width: 450px;"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 330px; width: 440px;">
                            <table id="table_adminlogin">
                                <tr>
                                    <td class="left_td">用户名：</td>
                                    <td class="right_td">
                                        <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="m-tip" colspan="2">
                                        <asp:RequiredFieldValidator ID="rfv_UserName" runat="server" ControlToValidate="txt_UserName" ForeColor="Red">用户名不能为空！</asp:RequiredFieldValidator>
                                    </td>
                                    <tr>
                                        <td class="left_td">密&nbsp;&nbsp;&nbsp;码：</td>
                                        <td class="right_td_pwd">
                                            <asp:TextBox ID="txt_Password" runat="server" TextMode="Password"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="m-tip" colspan="2">
                                            <asp:RequiredFieldValidator ID="rfv_Password" runat="server" ControlToValidate="txt_Password" ForeColor="Red">密码不能为空！</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left_td">验证码：</td>
                                        <td class="right_td_chk">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txt_Check" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img id="imgCode" src="../User/CheckCode.aspx?new   Date().getTime()" alt="看不清，请点击我！" onclick="this.src=this.src+'?'" /></td>
                                                </tr>
                                            </table>


                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="m-tip" colspan="2">
                                            <asp:RequiredFieldValidator ID="rfv_Check" runat="server" ControlToValidate="txt_Check" ForeColor="Red">验证码不能为空！</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="right_td_btn" colspan="2">
                                            <table style="width: 100%; height: 48px;margin-top:15px;">
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtn_Login" runat="server" ImageUrl="~/Image/User/User_Login.jpg" OnClick="ibtn_Login_Click" /></td>
                                                    <td style="text-align:left;">
                                                        <label><input name="" type="checkbox" value="" checked="checked" />记住密码</label>
                                                        </td>
                                                    <td style="width: 80px;"><label><a href="#">忘记密码？</a></label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </form>

        </div>

    </div>



    <div class="loginbm"><p>版权所有©河南暖男信息技术有限公司  地址：河南省安阳市文明大道436号图书馆三楼   邮编：455000  豫ICP备：xxxxx号  豫公网安备：xxxxxxxxxxxxx号</p></div>
</body>

</html>
