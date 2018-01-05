<%@ Page Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="StudentPaperList.aspx.cs" Inherits="Web_StudentPaperList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>�ɼ�����</title>  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">ѧ���Ծ�����</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" DataKeyNames ="StudentId,PaperID" 
                                          OnRowDataBound="GridView1_RowDataBound" 
                                          OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="8" 
                                          AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                                          BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="13px" 
                                          Width="100%" OnRowDeleting="GridView1_RowDeleting">
                    <Columns> 
                           <asp:BoundField DataField="StudentId" HeaderText="ѧ��ID" ReadOnly="True"/>
                            <asp:BoundField DataField="PaperID" HeaderText="�Ծ�ID" ReadOnly="True"/>
                            <asp:TemplateField HeaderText="ѧ������">                               
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("StudentName","{0}") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="���ڲ���">                               
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server"><%# Eval("DepartmentName")%></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                            </asp:TemplateField>
                             <asp:HyperLinkField DataNavigateUrlFields="StudentId,PaperID" 
                               DataNavigateUrlFormatString="StudentPaper.aspx?StudentId={0}&amp;PaperID={1}" 
                               DataTextField="PaperName" HeaderText="�Ծ�(����鿴)" ItemStyle-Font-Bold="true">                       
<ItemStyle Font-Bold="True"></ItemStyle>
                           </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="����ʱ��">                                
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("ExamTime","{0}") %>' ></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                            </asp:TemplateField>    
                            <asp:BoundField HeaderText="����ʱ��" DataField="JudgeTime" /> 
                           <asp:BoundField DataField="Score" HeaderText="�ɼ�" />
                           <asp:TemplateField HeaderText="ѧ���ɼ��ɼ�">
                               <ItemTemplate>
                                   <asp:Label ID="Label6" runat="server" 
                                       Text='<%# Convert.ToInt32(Eval("IsUserView"))>0?"True":"False" %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                               <asp:CommandField ShowDeleteButton="True" HeaderText="ɾ��" />
                    </Columns>
                       <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>

                <br /><asp:Label ID="LabelPageInfo" runat="server"></asp:Label>
	    </td>
        </tr>
    </table>

      
</asp:Content>