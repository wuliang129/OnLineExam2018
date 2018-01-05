<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="CourseAdd.aspx.cs" Inherits="Web_teacher_CourseAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<title>考试科目添加</title>      

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
      
        <tr style="background: url(~/Images/lineS.jpg) repeat-x;">
            <td style="height:25;" colspan="3">
                &nbsp;&nbsp;&nbsp;欢迎您：<asp:Label ID="labUser" runat="server" Text="Label" Width="70px"></asp:Label>&nbsp;&nbsp;
                                <script type="text/javascript">                                    getDate();</script>

                <span id="ShowTime"></span></td>         
        </tr>


          <tr>
            <td  style="width: 130px" align="center" valign="top">
                
           </td>          
              <td  style="width: 4px;  background: url(~/Images/line.gif) repeat-y;"> 
            </td>   
            <td  valign="top" align="left"  width="960px">
            
           
                       <table cellpadding="0" cellspacing="0" border="1"  bordercolor="#cccccc" style="border-collapse:collapse" width="100%" frame="below">
               <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;width:100%;" colspan="2"> <div class="title" align="left"><h4>
                        添加考试科目</h4></div></td>                    
                </tr>               
                <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;">科目名称：</td>
                    <td >&nbsp;<div align="left"><asp:TextBox id="txtName"  runat="server" MaxLength="20" Width="128px"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="不能为空！"></asp:RequiredFieldValidator></div>
                     </td>
                </tr>                
                <tr>
                    <td></td>
                    <td >&nbsp; <asp:Label ID="lblMessage" runat="server" ForeColor=red></asp:Label><br />
                       <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/Images/Save.GIF" OnClick="imgBtnSave_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
					   <asp:ImageButton ID="imgBtnReturn" runat="server" CausesValidation="false" ImageUrl="~/Images/Return.GIF" OnClick="imgBtnReturn_Click" /></td>
                </tr>
            </table>         
              
            </td>
        </tr>
    </table>

</asp:Content>

