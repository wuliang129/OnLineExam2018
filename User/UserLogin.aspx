<%@ Page Language="C#" MasterPageFile="~/MasterPage/User_MasterPage.master"  AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="User_UserLogin" %>


    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <title>用户登录</title>
    <link href="../CSS/UserLogin.css" rel="stylesheet" type="text/css" />
    </asp:Content>
    

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="login">

            <div class="login-con">
				<div class="login-a">
                      <div id="div_control_backimg"></div>
                        <div class="login-b">
                      	<div class="login-b-a">用户登陆</div>
                      	<div class="login-b-b">
                      		<ul>
                      			<li>
                      				<label for="userName" class="label_font_15">账 &nbsp;&nbsp;号：</label><asp:TextBox ID="txt_UserID" runat="server"></asp:TextBox>
                      				<h1 id="m-tip">
                                        <asp:RequiredFieldValidator ID="rfv_UserName" runat="server" ControlToValidate="txt_UserID" Display="Dynamic" ForeColor="Red"><img src="../Image/User/waringicon.png" />用户名不能为空！</asp:RequiredFieldValidator>
                                    </h1>
                      			</li>
                      			<li>
                      				<label for="password" class="label_font_15">密 &nbsp;&nbsp;码：</label><asp:TextBox ID="txt_Password" runat="server" TextMode="Password"  AUTOCOMPLETE="OFF"></asp:TextBox>
                      				
                      				<h1 id="p-tip"> <asp:RequiredFieldValidator ID="rfv_Password" runat="server" ControlToValidate="txt_Password" Display="Dynamic" ForeColor="Red"><img src="../Image/User/waringicon.png" />密码不能为空！</asp:RequiredFieldValidator></h1>
                      			</li>
                                
                      			<li>
                      				<label class="label_font_15">验证码：</label><asp:TextBox ID="txt_Check" runat="server" Width="85px"></asp:TextBox> 
                                          
                                    <img style=" cursor:hand;width: 76px; height: 32px"  id="imgCode" src="CheckCode.aspx?new   Date().getTime()" alt="看不清，请点击我！" onclick="this.src=this.src+'?'" />
                                        
                                    <h1 id="c-tip"> <asp:RequiredFieldValidator ID="rfv_Check" runat="server" ControlToValidate="txt_Check" Display="Dynamic" ForeColor="Red"><img src="../Image/User/waringicon.png" />验证码不能为空！</asp:RequiredFieldValidator></h1>
                      					
                      				<!--输入错误时的输入框样式<input type="text" name="textfield" id="textfield" class="receSum-error" />-->
                                   <!--当输入的手机号码发生错误时，出现如下提示信息
                                   <p class="error-tips-1 spanRed">在此处显示错误的信息提示——如：对不起，您输入的手机号码格式有误，请重新输入！</p>-->
                      				
                      			</li>
                      			<li id="rbtn_UserType_Student" style="height:42px; line-height:42px;margin:0px 0px 10px 0px; padding:0px;">
                      				<label  class="label_font_15">用户类型：</label><asp:RadioButton ID="rbtn_UserType_Student" runat="server" GroupName="UserType" Text="学生" Checked="True" CssClass="radio" /><asp:RadioButton ID="rbtn_UserType_Teacher" runat="server" GroupName="UserType" Text="教师" CssClass="radio" />
                                      <asp:RadioButton ID="rbtn_UserType_Manager" runat="server" GroupName="UserType" Text="管理员" CssClass="radio" />
                      			</li>
                      			<li class="login-x">
		                          
                                  <div class="login-x-a"><asp:ImageButton ID="ibtn_Login" runat="server" ImageUrl="~/Image/User/User_Login.jpg" OnClick="ibtn_Login_Click" /></div>
		                          <div class="login-x-a"><asp:ImageButton ID="ibtn_Register" runat="server" ImageUrl="~/Image/User/User_Register.jpg" /></div>
	                            </li>
                      			
                      		</ul>
                      	</div>
                      	<div class="login-b-c">
                      		<a href="#">免费接入</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#">忘记登录密码？</a>
                      	</div>
                      </div>
				</div>
			</div>
			
		</div>
    </asp:Content>
