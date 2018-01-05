<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="TrainingQuestionProblem.aspx.cs" Inherits="Web_student_TrainingQuestionProblem" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>问答练习</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">问答练习</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <table cellpadding="0" cellspacing="0" border="1"  bordercolor="#cccccc" style="border-collapse:collapse" width="100%" frame="below">
                 <tr>
                    <td  style="text-align:right; width:250px;">
                        选择练习科目：<asp:DropDownList id="ddliProblems" runat="server" Width="200px" 
                            oninit="ddliProblems_Init" 
                            onselectedindexchanged="ddliProblems_SelectedIndexChanged" 
                            AutoPostBack="True"></asp:DropDownList></td>
                    <td width="567px" ><div align="left">
              
                        <asp:Label ID="lblMessage" runat="server" ForeColor=red></asp:Label>
                       </div>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <div align="left">
                        <asp:GridView ID="gv_Problem" runat="server" PageSize="8" DataKeyNames="ID" BackColor="White" 
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                Font-Size="13px" Width="100%" oninit="ddliProblems_Init" Visible="False">
                       <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                       </div>
                        </td>
                </tr>

                                
                 <tr>
                   <td colspan="2" height="60px">
                      <div class="question"  style="border: 1px dashed rgb(255, 255, 255);">
                            <p><span style="color: #09F; font-weight: bold;">第&nbsp;<asp:Label ID="lbl_ProblemIndex" runat="server"
                                    Text="题目序号"></asp:Label>&nbsp;题</span>:&nbsp;&nbsp;<asp:Label ID="lbl_ProblemTitle" runat="server"
                                        Text="题目标题"></asp:Label></p>
                                                        
                            <table width="100%"  border="0" bgcolor="#F1F1F1">
                                <tbody><tr >
                                    <td width="85px" ><span style="font-weight: bold">[填写答案]</span>    
                                    </td>
                                    <td width="70%">
                                        <asp:TextBox 
                                                ID="txt_UserAnswer" runat="server" TextMode="MultiLine" Width="100%" 
                                            Rows="8" Font-Size="10pt"></asp:TextBox>
                                    </td>
                                        
                                    <td >
                                        <asp:Image ID="image_correct" runat="server" 
                                            ImageUrl="/Image/User/right_color.png" Width="26px" Height="26px" />
                                        <asp:Image ID="image_error" runat="server" 
                                            ImageUrl="/Image/User/error-color@2x.png" Width="26px" Height="26px" />
                                        <asp:CustomValidator ID="cv_DisplayMsg" runat="server"><a class="click" style="background:url(/Image/User/waringicon.png) no-repeat left center; display:inline-block; padding-left:20px; color:#09F;">请填写答案！</a></asp:CustomValidator>

                                    </td>
                                </tr>
                            </tbody></table>
                        </div>
                   </td>
                 </tr>

                 <tr>
                    <td colspan="2">
                        <table style="width:540px; height:35px; margin-left:15px;">
                            <tr>
                                <td > 
                                    <asp:Button ID="btn_First" runat="server" Text="|&lt;&lt;第一题" BorderStyle="None" 
                                        CssClass="btn_change" onclick="LoadProblemByButtonSender"  /></td>
                                <td > 
                                    <asp:Button ID="btn_Previous" runat="server" Text="&lt;&lt;上一题" BorderStyle="None" 
                                        CssClass="btn_change" onclick="LoadProblemByButtonSender"  /></td>
                                <td > 
                                    <asp:Button ID="btn_Next" runat="server" Text="下一题&gt;&gt;" BorderStyle="None" 
                                        CssClass="btn_change" onclick="LoadProblemByButtonSender"  /></td>
                                <td > 
                                    <asp:Button ID="btn_Last" runat="server" Text="最后一题&gt;&gt;|" BorderStyle="None" 
                                        CssClass="btn_change" onclick="LoadProblemByButtonSender"  /></td>
                                <td > 
                                     <p><span style="color: #09F; font-weight: bold; font-size:14px;"><asp:Label ID="lbl_indexVScount" runat="server"
                                    Text="题目序号"></asp:Label></span>&nbsp;&nbsp;</p></td>
                                <td style="background-color:#FAFAD2; text-align:center;"> 
                                     <p><span style="color: #09F; font-weight: bold; font-size:14px; ">
                                         <asp:CheckBox ID="chk_DisplayExplain" runat="server" Text="显示答案" 
                                             AutoPostBack="True" Checked="True" 
                                             oncheckedchanged="chk_DisplayExplain_CheckedChanged" /></span>&nbsp;&nbsp;</p></td>
                            </tr>
                        </table>
                    </td>
                 </tr>
                 <tr>
                    <td colspan="2" style="padding:15px;">
                        <asp:Label ID="lbl_cankaodaan" runat="server" Text="参考答案" CssClass="teacher-explain"></asp:Label>
                        <asp:TextBox ID="txt_Explain" runat="server" Rows="8" TextMode="MultiLine" 
                            Width="100%" ReadOnly="True" Font-Size="10pt"></asp:TextBox>
                        <asp:Label ID="lbl_Explain" runat="server" Text="Label<br/>你好" CssClass="teacher-explain"></asp:Label>
                    </td>
                 </tr>
                 
              </table>
	    </td>
        </tr>
    </table>

</asp:Content>

