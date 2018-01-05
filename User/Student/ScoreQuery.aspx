<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="ScoreQuery.aspx.cs" Inherits="Web_student_ScoreQuery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>成绩查询</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">成绩查询</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <table cellpadding="0" cellspacing="0" border="1"  bordercolor="#cccccc" style="border-collapse:collapse" width="100%" frame="below">
               
                <tr>
                    <td  style="text-align:right; width:250px;">
                        考试科目：<asp:DropDownList id="ddlPaper" runat="server" Width="200px"></asp:DropDownList></td>
                    <td width="567px" ><div align="left">
              
                     <asp:Button ID="btn_ScoreQuery" runat="server" Text="成绩查询" 
                               CausesValidation="false" onclick="btn_ScoreQuery_Click" /><asp:Label ID="lblMessage" runat="server" ForeColor=red></asp:Label>
                       </div>
                    </td>
                </tr>
                
                                
                 <tr>
                    <td  style="text-align:left;width:100%; font-weight: bold; padding-top:20px;" colspan="2"> 
                        考试记录：<asp:Label ID="lblScore" runat="server" Text="" Width="126px"></asp:Label>
                    </td>                    
                </tr>
                <tr>
                    <td  style="text-align:right;" colspan="2">
                        <div align="left">
                        <asp:GridView ID="gv_Score" runat="server" AllowPaging="True" 
                                OnRowDataBound="gv_Score_RowDataBound" 
                                OnPageIndexChanging="gv_Score_PageIndexChanging" PageSize="8" 
                                AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="3" 
                                Font-Size="13px" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                    <Columns>                       
                            <%--<asp:BoundField DataField="PingYu" HeaderText="评语" />--%>
                        <asp:TemplateField HeaderText="成绩编号" Visible="False">
                                <ItemTemplate>
                                    <span style="color: rgb(17, 17, 17); font-family: 'HanHei SC', 'PingFang SC', 'Helvetica Neue', Helvetica, STHeitiSC-Light, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 22.4px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal;  word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">
                                    &nbsp;<asp:Label ID="Label1" runat="server"><%# Eval("ID") %></asp:Label>
                                    </span>&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="StudentId" HeaderText="学生ID" />
                        <asp:BoundField DataField="PaperID" HeaderText="PaperID" />
                        <asp:BoundField DataField="StudentName" HeaderText="姓名" />
                        <asp:BoundField DataField="PaperName" HeaderText="试卷" />
                        <asp:BoundField DataField="Score" HeaderText="成绩" />
                        <asp:BoundField DataField="ExamTime" HeaderText="考试时间" />
                        <asp:BoundField DataField="JudgeTime" HeaderText="评卷时间" />
                        <asp:HyperLinkField DataNavigateUrlFields="StudentId,PaperID,PaperName" 
                            DataNavigateUrlFormatString="StudentPaper.aspx?StudentId={0}&amp;PaperID={1}&amp;PaperName={2}" 
                            HeaderText="试卷详情..." NavigateUrl="~/User/Student/UserPaper.aspx" 
                            Text="详细" />
                    </Columns>
                       <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                       </div>
                        </td>
                </tr>
            </table>
	    </td>
        </tr>
    </table>


</asp:Content>

