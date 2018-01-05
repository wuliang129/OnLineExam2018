<%@ Page Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="StudentScore.aspx.cs" Inherits="Web_StudentScore" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>成绩管理</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">学生成绩管理</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <asp:GridView ID="gv_StudentScore" runat="server" AllowPaging="True" OnRowDataBound="gv_StudentScore_RowDataBound" OnPageIndexChanging="gv_StudentScore_PageIndexChanging" PageSize="12" AutoGenerateColumns="False" DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="13px" Width="100%" OnRowDeleting="gv_StudentScore_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="成绩编号" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server"><%# Eval("ID") %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学生ID">
                            <ItemTemplate>
                                <asp:Label ID="lblUserID" runat="server"><%# Eval("StudentId") %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StudentName" HeaderText="姓名" />
                        <asp:TemplateField HeaderText="试卷">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("PaperName") %>'></asp:Label>
                                <asp:Label ID="lblPaperID" runat="server" Text='<%# Eval("PaperID") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
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
                        <asp:TemplateField HeaderText="评卷时间">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server"><%# Eval("JudgeTime") %></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" HeaderText="删除">
                            <HeaderStyle Wrap="False" />
                        </asp:CommandField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <ul class="toolbar">
                    <li class="click_nodisplay" style="width:85px;">
                        <div class="oneLine_ExcelOut" >
                            <asp:LinkButton ID="lbtn_ExcelOut" runat="server" CausesValidation="False" OnClick="lbtn_ExcelOut_Click">导出数据</asp:LinkButton>
                        </div>
                    </li>
                </ul>
            </td>
        </tr>
    </table>


</asp:Content>
