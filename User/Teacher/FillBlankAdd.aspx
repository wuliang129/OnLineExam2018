<%@ Page Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="FillBlankAdd.aspx.cs" Inherits="Web_FillBlankAdd" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>�����</title>  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
      
   
        <tr style="background: url(~/Images/lineS.jpg) repeat-x;">
            <td style="height:25;" colspan="3">
                &nbsp;&nbsp;&nbsp;��ӭ����<asp:Label ID="labUser" runat="server" Text="Label" Width="70px"></asp:Label>&nbsp;&nbsp;
                                <script type="text/javascript">getDate();</script>

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
                    <td bgcolor="#eeeeee" colspan="2"> <div class="title" align="left"><h4>�����</h4></div></td>                    
                </tr>
                <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;" width="80px">��Ŀ��</td>
                    <td >&nbsp;<div align="left"><asp:dropdownlist id="ddlCourse" runat="server" Font-Size="9pt" Width="88px"></asp:dropdownlist></div>
                    </td>
                </tr>
               
                <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;"> ǰ������</td>
                    <td >&nbsp;<div align="left"><asp:textbox id="txtFrontTitle"  runat="server" Width="100%" TextMode="MultiLine"
								Height="60px"></asp:textbox>
                       </div></td>
                </tr>
                 <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;">��������</td>
                    <td >&nbsp;<div align="left"><asp:textbox id="txtBackTitle"  runat="server"  Width="100%" TextMode="MultiLine"
							Height="60px"></asp:textbox></div></td>
                </tr>
                 
                  <tr>
                    <td bgcolor="#eeeeee" style="text-align:right;">��</td>
                    <td >&nbsp;<div align="left"><asp:textbox id="txtAnswer"  runat="server"  Width="100%"></asp:textbox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnswer"
                            ErrorMessage="����Ϊ�գ�"></asp:RequiredFieldValidator></div></td>
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