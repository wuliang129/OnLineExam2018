<%@ Page Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="UserScore.aspx.cs" Inherits="Web_UserScore" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>成绩管理</title>  
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
      
    
        <tr style="background: url(../Images/lineS.jpg) repeat-x;">
            <td style="height:25;" colspan="3">
                &nbsp;&nbsp;&nbsp;欢迎您：<asp:Label ID="labUser" runat="server" Text="Label" Width="70px"></asp:Label>&nbsp;&nbsp;
                                <script type="text/javascript">getDate();</script>

                <span id="ShowTime"></span></td>         
        </tr>


          <tr>
            <td  style="width: 130px" align="center" valign="top">
               
           </td>          
              <td  style="width: 4px;  background: url(../Images/line.gif) repeat-y;"> 
            </td>   
            <td  valign="top" align="left" width="750px">
           
                                      <h4>&gt;&gt;成绩管理</h4>  
                      <asp:GridView ID="GridView1" runat="server" AllowPaging="true" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="12" AutoGenerateColumns="False" DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="13px" Width="100%"  OnRowDeleting="GridView1_RowDeleting" >
                    <Columns>                       
                        <asp:TemplateField HeaderText="成绩编号" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"><%# Eval("ID") %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="用户ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server"><%# Eval("UserID") %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:BoundField DataField="UserName" HeaderText="姓名" />
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
                        <asp:CommandField ShowDeleteButton="True" HeaderText="删除" >
                            <HeaderStyle Wrap="False" />
                        </asp:CommandField>
                    </Columns>
                       <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
              <br /> <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Excel.GIF" OnClick="ImageButton2_Click" />
            </td>
        </tr>
     
    </table>  
</asp:Content>
