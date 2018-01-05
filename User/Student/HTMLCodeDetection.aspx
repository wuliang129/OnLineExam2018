<%@ Page validateRequest="false"  Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="HTMLCodeDetection.aspx.cs" Inherits="Web_student_HTMLCodeDetection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>HTML代码在线检测</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
     <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">HTML代码在线检测</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <table cellpadding="0" cellspacing="0" border="1"  bordercolor="#cccccc" style="border-collapse:collapse" width="100%" frame="below">                 

                
                <tr>
                    <td colspan="2">
                        <p style="color:#9932CC; font-weight: bold; font-size:15px;">&nbsp;&nbsp;w3school权威在线检测：<a href="http://validator.w3.org/" target="_blank">前往检测</a></p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p style="color: #09F; font-weight: bold; font-size:13px;">&nbsp;&nbsp;请把要检测的代码输入下框</p>
                    </td>
                </tr>
                 
                 <tr>
                   <td colspan="2" >
                     <div class="question"  style="border: 1px dashed rgb(255, 255, 255);">
                      <asp:TextBox ID="txt_UserCode" runat="server" TextMode="MultiLine" Width="85%" 
                           Rows="20" Font-Size="10pt"></asp:TextBox>
                     </div>

                   </td>
                 </tr>

                 <tr>
                    <td colspan="2">
                        <table style="width:600px; height:35px; margin-left:30px;">
                            <tr>
                                <td > 
                                    <asp:Button ID="btn_CodeDetection" runat="server" Text="代码检测" BorderStyle="None" 
                                        CssClass="btn_change" onclick="btn_CodeDetection_Click"  /></td>
                                <td > 
                                    <asp:Button ID="btn_RunCode" runat="server" Text="运行代码" BorderStyle="None" 
                                        CssClass="btn_change" onclick="btn_RunCode_Click"  /></td>
                                <td > 
                                    <asp:Button ID="btn_SaveCode" runat="server" Text="保存代码" BorderStyle="None" 
                                        CssClass="btn_change" onclick="btn_SaveCode_Click"  /></td>
                                 <td > 
                                    <asp:Button ID="btn_Clear" runat="server" Text="清空代码" BorderStyle="None" 
                                        CssClass="btn_change" onclick="btn_Clear_Click"  /></td>
                                <td >
                                        <asp:CustomValidator ID="cv_DisplayMsg" runat="server"><a class="click_detection" style="background:url(/Image/User/waringicon.png) no-repeat left center; display:inline-block; padding-left:20px; color:#09F;">请填入要检测代码！</a></asp:CustomValidator>

                                    </td>
                            </tr>
                        </table>
                    </td>
                 </tr>

                 <tr>
                    <td colspan="2">
                        <p style="color: #09F; font-weight: bold; font-size:13px;">&nbsp;&nbsp;解析结果</p>
                    </td>
                </tr>

                 <tr>
                    <td colspan="2">
                        <div class="question"  style="border:1px dashed rgb(255, 255, 255);">
                        <asp:TextBox ID="txt_DetectionResult" runat="server" Rows="15" TextMode="MultiLine" 
                            Width="85%" ReadOnly="True" Font-Size="10pt"></asp:TextBox>
                        </div>
                    </td>
                 </tr>
                 
              </table>
	    </td>
        </tr>
    </table>

     
</asp:Content>

