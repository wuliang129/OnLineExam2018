<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="StudentTestIndex.aspx.cs" Inherits="Web_student_StudentTestIndex" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>在线考试系统-学生</title>  
    <script src="/OnLineExam/JS/Morning_JS.js" type="text/javascript"></script>
    <link href="/OnLineExam/CSS/CSS.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">在线考试系统-学生</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <table cellpadding="0" cellspacing="0" border="1"  bordercolor="#cccccc" style="border-collapse:collapse" width="100%" frame="below">
                <tr>
                    <td  style="text-align:right;">
                        选择试卷：</td>
                    <td ><div align="left"><asp:DropDownList id="ddlPaper" runat="server" Width="220px"></asp:DropDownList>
                    <asp:Button ID="btn_StartExam" runat="server" Text="开始考试" 
                               CausesValidation="false" onclick="btn_StartExam_Click" Width="80px" />
                     <asp:Label ID="lblMessage" runat="server" ForeColor=red></asp:Label>
                        
                       </div>
                    </td>
                </tr>                    
                
                
                 <tr>
                    <td bgcolor="#EDF1F6" style="text-align:right;width:100%;" colspan="2"> <div class="title" align="left"><h4>
                        考试记录：<asp:Label ID="lblScore" runat="server" Text="" Width="126px"></asp:Label></h4></div></td>                    
                </tr>
                <tr>
                    <td  style="text-align:right;" colspan="2">
                        <div align="left">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="8" AutoGenerateColumns="False" DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="13px" Width="100%">
                    <Columns>                       
                        <asp:TemplateField HeaderText="成绩编号" Visible="False">
                                <ItemTemplate>
                                    <span style="color: rgb(17, 17, 17); font-family: 'HanHei SC', 'PingFang SC', 'Helvetica Neue', Helvetica, STHeitiSC-Light, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 22.4px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal;  word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">
                                    &nbsp;<asp:Label ID="Label1" runat="server"><%# Eval("ID") %></asp:Label>
                                    </span>&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">                               
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server"><%# Eval("StudentName") %></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="试卷">                                
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server"><%# Eval("PaperName") %></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="成绩">                                
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server"><%# Eval("Score") %></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="考试时间">                                
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server"><%# Eval("ExamTime") %></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                            </asp:TemplateField>  
                            <asp:BoundField DataField="JudgeTime" HeaderText="评卷时间" />                     
                            <%--<asp:BoundField DataField="PingYu" HeaderText="评语" />--%>
                    </Columns>
                       <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                       </div>
                        </td>
                </tr>
            </table>
	    </td>
        </tr>
    </table>

</asp:Content>

