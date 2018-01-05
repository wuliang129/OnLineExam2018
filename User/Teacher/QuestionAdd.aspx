<%@ Page Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="QuestionAdd.aspx.cs" Inherits="Web_QuestionAdd" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    <title>问答题</title>  
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
      
        <tr style="background: url(~/Images/lineS.jpg) repeat-x;">
            <td style="height:25;" colspan="3">
                &nbsp;&nbsp;&nbsp;欢迎您：<asp:Label ID="labUser" runat="server" Text="Label" Width="70px"></asp:Label>&nbsp;&nbsp;
                                <script type="text/javascript">getDate();</script>

                <span id="ShowTime"></span></td>         
        </tr>


          <tr>
            <td  style="width: 130px" align="center" valign="top">
               
           </td>          
              <td  style="width: 4px;  background: url(~/Images/line.gif) repeat-y;"> 
            </td>   
            <td  valign="top" align="left" width="960px">
            
           
                       <table cellpadding="0" cellspacing="0" border="1"  bordercolor="#cccccc" style="border-collapse:collapse" width="100%" frame="below">
               <tr>
                    <td bgcolor="#eeeeee" colspan="2"> <div class="title" align="left"><h4>问答题</h4></div></td>                    
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;" width="80px">科目：</td>
                    <td >&nbsp;<div align="left"><asp:dropdownlist id="ddlCourse" runat="server" Font-Size="9pt" Width="88px"></asp:dropdownlist></div>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;">题目：</td>
                    <td >&nbsp;<div align="left"><asp:textbox id="txtTitle"  runat="server" Width="100%" TextMode="MultiLine"	Height="80px"></asp:textbox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"
                            ErrorMessage="不能为空！"></asp:RequiredFieldValidator></div></td>
                </tr>
                
                  <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;">答案</td>
                    <td >&nbsp;<div align="left"> <asp:textbox id="txtAnswer"  runat="server"  Width="100%" TextMode="MultiLine"
							Height="80px"></asp:textbox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnswer"
                            ErrorMessage="不能为空！"></asp:RequiredFieldValidator></div></td>
                </tr>
                <tr>
                    <td></td>
                    <td ><asp:Label ID="lblMessage" runat="server" ForeColor=red></asp:Label><br />
                       <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/Images/Save.GIF" OnClick="imgBtnSave_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
					   <asp:ImageButton ID="imgBtnReturn" runat="server" CausesValidation="false" ImageUrl="~/Images/Return.GIF" OnClick="imgBtnReturn_Click" /></td>
                </tr>
            </table>         
              
            </td>
        </tr>
    </table>  
</asp:Content>