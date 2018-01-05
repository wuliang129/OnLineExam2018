<%@ Page Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="FillBlankManage.aspx.cs" Inherits="Web_FillBlankManage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>填空题管理</title>  
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
            <td  valign="top" align="left"  width="960px">
           
                                      <h4>&gt;&gt;填空题管理</h4>  
                                        <hr/> <p align="left"><asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" Width="130px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList></p>
                      <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="12" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="13px" OnRowDeleting="GridView1_RowDeleting">
                    <Columns>                       
                        <asp:TemplateField HeaderText="编号" Visible=false>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server"><%# Eval("ID") %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                 <center><asp:Label id="Label10" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></center>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField DataField="FrontTitle" HeaderText="题目前部分" />
                         <asp:BoundField DataField="BackTitle" HeaderText="题目后部分" />                                                                                  
                        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="FillBlankAdd.aspx?ID={0}" HeaderText="详细..." Text="详细..." />                                           
                        
                        <asp:CommandField ShowDeleteButton="True" HeaderText="删除" />
                    </Columns>
                       <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                      <br /><a href="FillBlankAdd.aspx" style="font-size:medium;"><font color=red><u>添加填空题</u></font></a>          
              
            </td>
        </tr>
    </table>  
</asp:Content>
