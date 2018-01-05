<%@ Page Language="C#" MasterPageFile="~/MasterPage/User_MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="user_loginRegister_Register" Title="用户注册" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>用户注册</title>
    <script type="text/javascript" language="javascript">
        //JS判断密码强度

        //判断输入密码的类型  
        function CharMode(iN) {
            if (iN >= 48 && iN <= 57) //数字  
                return 1;
            if (iN >= 65 && iN <= 90) //大写  
                return 2;
            if (iN >= 97 && iN <= 122) //小写  
                return 4;
            else
                return 8;
        }
        //bitTotal函数  
        //计算密码模式  
        function bitTotal(num) {
            modes = 0;
            for (i = 0; i < 4; i++) {
                if (num & 1) modes++;
                num >>>= 1;
            }
            return modes;
        }
        //返回强度级别  
        function checkStrong(sPW) {
            if (sPW.length < 5)
                return 0; //密码太短，不检测级别
            Modes = 0;
            for (i = 0; i < sPW.length; i++) {
                //密码模式  
                Modes |= CharMode(sPW.charCodeAt(i));
            }
            return bitTotal(Modes);
        }

        //显示颜色  
        function pwStrength(pwd) {
            Dfault_color = "#eeeeee";		//默认颜色
            L_color = "#FF0000";		//低强度的颜色，且只显示在最左边的单元格中
            M_color = "#FF9900";		//中等强度的颜色，且只显示在左边两个单元格中
            H_color = "#33CC00";		//高强度的颜色，三个单元格都显示
            if (pwd == null || pwd == '') {
                Lcolor = Mcolor = Hcolor = Dfault_color;
            }
            else {
                S_level = checkStrong(pwd);
                switch (S_level) {
                    case 0:
                        Lcolor = Mcolor = Hcolor = Dfault_color;
                        break;
                    case 1:
                        Lcolor = L_color;
                        Mcolor = Hcolor = Dfault_color;
                        break;
                    case 2:
                        Lcolor = Mcolor = M_color;
                        Hcolor = Dfault_color;
                        break;
                    default:
                        Lcolor = Mcolor = Hcolor = H_color;
                }
            }
            document.getElementById("strength_L").style.background = Lcolor;
            document.getElementById("strength_M").style.background = Mcolor;
            document.getElementById("strength_H").style.background = Hcolor;
            return;
        }
    </script>

    <script type="text/javascript">
        function PassIsSame() {
            var pwd1 = document.getElementById("<%= txtPass.ClientID %>").value;
            var pwd2 = document.getElementById("<%= txtQpass.ClientID %>").value;		//<!-- 对比两次输入的密码 -->
            if (pwd1 == pwd2) {
                document.getElementById("PassTishi").innerHTML = "";
            }
            else {
                document.getElementById("PassTishi").innerHTML = "<font color='red'>两次密码不相同</font>";
            }
            return;
        }

        //验证手机号码 
        function checkPhone() {  
            var phone = document.getElementById("<%= txtPhone.ClientID %>").value;
            if (!(/^1[34578]\d{9}$/.test(phone))) {
                document.getElementById("tPhoneTishi").innerHTML = "<font color='red'>手机号码验证出错！</font>";
                return false;
            }
            else
            {
                document.getElementById("tPhoneTishi").innerHTML = "";
                return true;
            }
        }

        //验证邮箱格式 
        function checktxtEmail() {
            var phone = document.getElementById("<%= txtEmail.ClientID %>").value;
            if (!(/\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/.test(phone))) {
                document.getElementById("txtEmailTishi").innerHTML = "<font color='red'>邮箱格式验证出错！</font>";
                return false;
            }
            else {
                document.getElementById("txtEmailTishi").innerHTML = "";
                return true;
            }
        }
    </script>

    <script type="text/javascript">
	//显示会员名输入提示
function tName()
	    {
	       document.getElementById("sp").innerHTML="只能输入数字、字母下划线,<br>例如：mr_2008";	       
	    }
	  //显示密码输入提示  
	 function tPass()
	    {
	         document.getElementById("sp").innerHTML="为了提供密码的安全性。<br>建议密码在6位以上。";
	 }
	 //显示密码输入提示  
	 function tQPass() {
	     document.getElementById("sp").innerHTML = "再次输入密码。<br>建议密码在6位以上。";
	 }
	   
	  //显示电话码输入提示
	 function tPhone()
	 {
	        document.getElementById("sp").innerHTML="输入手机号，以方便联系您<br>手机号应为11位";
	 }
	 //显示电子邮件输入提示
	function tEmail()
	{
	        document.getElementById("sp").innerHTML="请输入正确的电子邮件。<br>例如：mr2008@mr.com";
	}
	//显示所在城市输入提示
	function tCity()
	{
	    document.getElementById("sp").innerHTML="输入所在城市。<br>例如：长春市";
	}
	//显示找回密码问题提示
	function tQuestion() {
	    document.getElementById("sp").innerHTML = "输入找回密码问题。<br>例如：我的小学名称";
	}
        //显示找回密码问题答案提示
	function tQuestionAnswer() {
	    document.getElementById("sp").innerHTML = "输入找回密码问题答案。<br>例如：中所屯小学";
	}
    </script>
    
    <style type="text/css">
        #table_User_Register{
            border:0px;
            border:0px;
            margin-left:90px;
            border-collapse:collapse;
            background-image:url("../Image/User/User_Register_bg.jpg");
            width: 1003px; 
            background-repeat: no-repeat;
            height: 460px;
            font-size:14px;
        }
        span{
            margin:0px;
            padding:0px;
        }
        #table_User_Register .marginleft{
            margin-left:5px;
            color: #ff0000; 
        }
        #table_User_Register .table_Info td{
            height:28px;
        }
       #tab td{
            height:18px;
            font-size: 10pt;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div style="text-align: center; margin:0 auto;">
            <table id="table_User_Register">
                <tr style="height:80px;">
                    <td style="width: 200px;">
                    </td>
                    <td style="width: 455px;">
                    </td>
                    <td style="width: 200px;">
                    </td>
                    <td style="width: 10px;">
                    </td>
                    <td style="width: 130px;">
                    </td>
                </tr>
                <tr>
                    <td >
                    </td>
                    <td style="height:200px; text-align: left" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table class="table_Info" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 20px;">
                                    <tr>
                                        <td style="width: 138px; text-align: right">
                                            <span >用户名： </span>
                                        </td>
                                        <td style="text-align: left; width: 360px;">
                                            <asp:TextBox onFocus="tName();" ID="txtName" runat="server" Width="115px" AutoPostBack="True"
                                                OnTextChanged="txtName_TextChanged"></asp:TextBox><span class="marginleft">*</span>
                                            <asp:Label ID="labUser" runat="server" Text="只能输入数字、字母、下划线" Width="180px" Font-Size="13px"></asp:Label>
                                            
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table class="table_Info" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="width: 150px; text-align: right;">
                                    <span >密&nbsp;&nbsp;码：</span></td>
                                <td style="width: 140px; text-align: left">
                                    <asp:TextBox ID="txtPass" runat="server" onFocus="tPass();" 
                                        TextMode="Password" Width="115px" onKeyUp="pwStrength(this.value)" onBlur="pwStrength(this.value)" ></asp:TextBox><span class="marginleft">*</span></td>
                                <td style="width:230px;" align="left">
                                    <table id="tab" border="0"align="left" cellpadding="0" cellspacing="2" bordercolor="#eeeeee" width="90%">
                                        <tr align="center" >
			                                <td width="40%">密码强度:</td>
			                                <td width="20%" id="strength_L" >弱</td>  
			                                <td width="20%" id="strength_M" >中</td>  
			                                <td width="20%" id="strength_H" >强</td>  
		                                </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td style="text-align: right;">
                                    <span >确认密码：</span></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtQpass" runat="server" TextMode="Password" Width="115px" onFocus="tQPass();" onKeyUp="PassIsSame();"></asp:TextBox><span
                                        class="marginleft">*</span></td>
                                <td>
                                    <div style="width: 100%;"><span id="PassTishi" style="font-size: 12px; color: red;"></span></div></td>
                            </tr>
                            
                            <tr>
                                <td style="text-align: right;">
                                    <span >性&nbsp;&nbsp;别：</span></td>
                                <td style="text-align: left">
                                    <asp:RadioButtonList ID="radlistSex" runat="server" RepeatDirection="Horizontal"
                                        Width="115px" Font-Size="12px">
                                        <asp:ListItem Selected="True"> 男</asp:ListItem>
                                        <asp:ListItem>女</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td style=" height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <span >电&nbsp;&nbsp;话：</span></td>
                                <td style="text-align: left;">
                                    <asp:TextBox onFocus="tPhone();" ID="txtPhone" runat="server" Width="115px" onKeyUp="checkPhone();"></asp:TextBox></td>
                                <td >
                                    <div style="width: 100%;"><span id="tPhoneTishi" style="font-size: 12px; color: red;"></span></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; ">
                                    <span >E-mail：</span></td>
                                <td style="width: 13px; text-align: left; ">
                                    <asp:TextBox ID="txtEmail" onFocus="tEmail();" runat="server" Width="115px" onKeyUp="checktxtEmail();"></asp:TextBox></td>
                                <td>
                                    <div style="width: 100%;"><span id="txtEmailTishi" style="font-size: 12px; color: red;"></span></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <span >所在城市：</span></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCity" onFocus="tCity();" runat="server" Width="115px"></asp:TextBox></td>
                                <td >
                                </td>
                            </tr>
                            <tr>
                                <td style="width:150px; text-align: right; font-size:13px;">
                                    <span >找回密码时的问题：</span></td>
                                <td style="text-align: left" colspan="2">
                                    <asp:TextBox ID="txt_Question" onFocus="tQuestion();" runat="server" Width="235px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style=" text-align: right;">
                                    <span >问题答案：</span></td>
                                <td style="text-align: left" colspan="2">
                                    <asp:TextBox ID="txt_Answer" onFocus="tQuestionAnswer();" runat="server" Width="235px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height: 50px; text-align: center;">
                                    <table style=" width:100%; margin-top:20px;">
                                        <tr>
                                            <td style="width:130px;"></td>
                                            <td style="width:60px;"><asp:ImageButton ID="ibtn_Register" runat="server" 
                                        ImageUrl="~/Image/User/User_Register_btn.gif" onclick="ibtn_Register_Click" /></td>
                                            <td style="width:60px;"></td>
                                            <td  style="width:60px;"><asp:ImageButton ID="ibtn_Return" runat="server" 
                                        ImageUrl="~/Image/User/User_Register_btnback.gif" 
                                        onclick="ibtn_Return_Click" /></td>
                                            <td style="width:200px;"></td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                   
                     <td align="center" style="height: 200px; text-align: left;" valign="top">
                        <div style="width: 200px;">
                            <span id="sp" style="font-size: 14px; color: BlueViolet;"></span>
                        </div>
                    </td>
                    <td>
                    </td>
                    <td >
                    </td>
                </tr>
                
            </table>
        </div>

</asp:Content>

