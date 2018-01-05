<%@ Page Language="C#"  MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="PaperLists.aspx.cs" Inherits="Web_PaperLists" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title> ‘æÌπ‹¿Ì</title>  
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle"> ‘æÌπ‹¿Ì<br/><asp:Label runat="server" ID="lblMessage" ForeColor="#0000CC"></asp:Label></th>
                
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <asp:GridView ID="GridView1" runat="server"°°AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="12" OnRowDataBound="GridView1_RowDataBound" Width=100% AutoGenerateColumns="False" DataKeyNames="PaperID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="13px" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" >
                    <Columns>
                        <asp:TemplateField HeaderText="±‡∫≈" Visible=False>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server"><%# Eval("PaperID") %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="øº ‘ø∆ƒø">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server"><%# Eval("Name") %></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText=" ‘æÌ√˚≥∆">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server"><%# Eval("PaperName") %></asp:Label>
                            </ItemTemplate>                            
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText=" ‘æÌ◊¥Ã¨">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server"><%# Eval("state") %></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>   
                            <asp:DropDownList ID="ddlPaperState" AutoPostBack="true" runat="server"><asp:ListItem Value="1">ø…”√</asp:ListItem><asp:ListItem Value="0">≤ªø…”√</asp:ListItem></asp:DropDownList>                         
                               
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>                                                              
                        <asp:HyperLinkField DataNavigateUrlFields="PaperID" DataNavigateUrlFormatString="PaperDetail.aspx?PaperID={0}" HeaderText="œÍœ∏..." Text="œÍœ∏..." >
                            <HeaderStyle Wrap="False" />
                        </asp:HyperLinkField>                                           
                        <asp:CommandField ShowEditButton=True HeaderText=±‡º≠ >
                            <HeaderStyle Wrap="False" />
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" HeaderText="…æ≥˝" >
                            <HeaderStyle Wrap="False" />
                        </asp:CommandField>
                    </Columns>
                       <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
	    </td>
        </tr>
    </table>

      
</asp:Content>
