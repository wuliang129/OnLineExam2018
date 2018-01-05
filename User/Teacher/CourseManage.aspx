<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="CourseManage.aspx.cs" Inherits="Web_teacher_CourseManage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>考试科目管理</title>  
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">考试科目管理</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                 <asp:GridView ID="gv_Course" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="12" OnRowDataBound="GridView1_RowDataBound"　 BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="13px" Width="541px" DataKeyNames="ID"
                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" GridLines="Vertical" BackColor="White" >
                        <AlternatingRowStyle BackColor="#DCDCDC"  />
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"><%# Eval("ID") %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课程编号">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" ><%# Eval("CourseNo") %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="授课教师">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" ><%# Eval("TeacherName") %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="考试科目名称">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>' Width="80"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server"><%# Eval("Name") %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                                                     
                    </Columns>
                       <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#EEEEEE" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
                      <br /><a href="CourseAdd.aspx" style="font-size: medium;"><font color=red><u>添加考试科目</u></font></a> 
	    </td>
        </tr>
    </table>



</asp:Content>

