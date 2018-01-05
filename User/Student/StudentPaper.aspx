<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="StudentPaper.aspx.cs" Inherits="Web_StudentPaper" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>试卷评阅</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">试卷分析</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">
                <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse" width="100%" frame="below">
                    <tr>
                        <td  bgcolor="#eeeeee" style="text-align: right;">考生信息：</td>
                         <td colspan="3" style="text-align: left;">
                            &nbsp;<asp:Label ID="lbl_StuInfo" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#eeeeee" style="text-align: right;">试卷：</td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbl_PaperName" runat="server" ></asp:Label>
                            <asp:Label ID="lbl_XStudentId" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                        <td bgcolor="#eeeeee" style="text-align: right;">
                            <asp:Label ID="Label16" runat="server" Text="考试时间：" Width="103px"></asp:Label></td>
                        <td>
                            <asp:Label ID="lblExamtime" runat="server" Width="236px"></asp:Label>
                            <asp:Label ID="lbl_XPaperID" runat="server" Text="" Visible="false"></asp:Label></td>
                    </tr>

                    <tr>
                        <td bgcolor="#eeeeee" style="text-align: right;">单选题得分：</td>
                        <td align="left"><asp:Label ID="sinScore" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#eeeeee" style="text-align: right;">多选题得分：</td>
                        <td align="left"><asp:Label ID="mulScore" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td bgcolor="#eeeeee" style="text-align: right;">判断题得分：</td>
                        <td align="left"><asp:Label ID="judScore" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#eeeeee" style="text-align: right;">填空题得分：</td>
                        <td align="left"><asp:Label ID="filScore" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td bgcolor="#eeeeee" style="text-align: right;">问答题得分：</td>
                        <td align="left">
                            <asp:Label ID="queScore" runat="server" Font-Bold="true"></asp:Label>
                            &nbsp; <asp:Label ID="lblQuestion" runat="server" Text="(>>请在下面对问答题进行判分<<)" Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#eeeeee" style="text-align: right; font-weight:bold;">
                            总分：
                        </td>
                        <td align="left">
                            <asp:Label ID="sumScore" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td bgcolor="#eeeeee" style="text-align: right;">教师评语：</td>
                        <td colspan="3">
                            <div align="left">
                                <asp:TextBox ID="tbxPingyu" runat="server" Font-Bold="False" Width="100%"></asp:TextBox>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <table cellspacing="0" style="font-size: 12px; font-family: Tahoma; border-collapse: collapse;" cellpadding="0" width="100%" bgcolor="#ffffff" border="1" bordercolor="gray">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_Single" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label24" runat="server" Text="一、单选题(每题"></asp:Label>
                                                        <asp:Label ID="Label27" runat="server"></asp:Label>
                                                        <asp:Label ID="Label25" runat="server" Text="分)"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table id="Table2" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                            <br />
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("UserAnswer") %>' Visible="False"></asp:Label>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Mark") %>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="35%">
                                                                    <asp:RadioButton ID="RadioButton1" runat="server" Text='<%# Eval("AnswerA") %>' GroupName="Sl"></asp:RadioButton></td>
                                                                <td width="35%">
                                                                    <asp:RadioButton ID="RadioButton2" runat="server" Text='<%# Eval("AnswerB") %>' GroupName="Sl"></asp:RadioButton></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="35%">
                                                                    <asp:RadioButton ID="RadioButton3" runat="server" Text='<%# Eval("AnswerC") %>' GroupName="Sl"></asp:RadioButton></td>
                                                                <td width="35%">
                                                                    <asp:RadioButton ID="RadioButton4" runat="server" Text='<%# Eval("AnswerD") %>' GroupName="Sl"></asp:RadioButton></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">参考答案：
                                                                    <asp:Label ID="Label23" runat="server" Text='<%# Eval("Answer") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label22" runat="server" Text="二、多选题(每题"></asp:Label>
                                                        <asp:Label ID="Label28" runat="server"></asp:Label>
                                                        <asp:Label ID="Label23" runat="server" Text="分)"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table id="Table3" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                            <br />
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("UserAnswer") %>' Visible="False"></asp:Label>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Mark") %>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 22px" width="35%">
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" Text='<%# Eval("AnswerA") %>'></asp:CheckBox></td>
                                                                <td style="height: 22px" width="35%">
                                                                    <asp:CheckBox ID="CheckBox2" runat="server" Text='<%# Eval("AnswerB") %>'></asp:CheckBox></td>
                                                                <td style="height: 22px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="35%">
                                                                    <asp:CheckBox ID="CheckBox3" runat="server" Text='<%# Eval("AnswerC") %>'></asp:CheckBox></td>
                                                                <td width="350%">
                                                                    <asp:CheckBox ID="CheckBox4" runat="server" Text='<%# Eval("AnswerD") %>'></asp:CheckBox></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">参考答案：<asp:Label ID="Label27" runat="server" Text='<%# Eval("Answer") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label20" runat="server" Text="三、判断题(每题"></asp:Label>
                                                        <asp:Label ID="Label29" runat="server"></asp:Label>
                                                        <asp:Label ID="Label21" runat="server" Text="分)"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table id="Table4" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                            <br />
                                                            <tr>
                                                                <td width="85%">
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("UserAnswer")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("Mark") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td width="15%">
                                                                    <asp:CheckBox ID="CheckBox5" runat="server" Text="正确"></asp:CheckBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">参考答案：
                                                                    <asp:Label ID="Label21" runat="server" Text='<%# Eval("Answer").ToString()=="True"?"正确":"错误" %>'></asp:Label>
                                                                    <asp:Label ID="Label41" runat="server" Text='<%# Eval("Answer")%>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView4" runat="server" Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text="四、填空题(每题"></asp:Label>
                                                        <asp:Label ID="Label30" runat="server"></asp:Label>
                                                        <asp:Label ID="Label19" runat="server" Text="分)"> </asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table id="Table5" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                            <br />
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("FrontTitle","、{0}") %>'>
                                                                    </asp:Label>
                                                                    <asp:TextBox ID="TextBox1" Text='<%# Eval("UserAnswer") %>' runat="server" Width="150px" Style="border-bottom: gray   1px   solid" BorderStyle="None"></asp:TextBox>
                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("BackTitle") %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="Label17" runat="server" Text='<%# Eval("Mark") %>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>参考答案：
                                                                    <asp:Label ID="Label26" runat="server" Text='<%# Eval("Answer") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView5" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label32" runat="server" Text="五、问答题(每题"></asp:Label>
                                                        <asp:Label ID="Label31" runat="server"></asp:Label>
                                                        <asp:Label ID="Label33" runat="server" Text="分)"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table id="Table6" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                            <caption>
                                                                <br>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label18" runat="server" Text="<%# Container.DataItemIndex+1 %>">
                                                                            </asp:Label>
                                                                            <asp:Label ID="Label19" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                                            </asp:Label>
                                                                            (本题得分：<asp:TextBox ID="tbxqueScore" runat="server"
                                                                                Text='<%# Eval("CurrentScore","{0}") %>' Width="50px" ReadOnly="True"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                                ControlToValidate="tbxqueScore" Display="dynamic" ErrorMessage="只能为正整数或0"
                                                                                ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                                ControlToValidate="tbxqueScore" Display="dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                                                                            )
                                                                        <asp:Label ID="lbl_UserAnswerID" runat="server" Text='<%# Eval("ID","{0}") %>'
                                                                            Visible="False"></asp:Label>
                                                                            <br />
                                                                            <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true"
                                                                                Text='<%# Eval("UserAnswer") %>' TextMode="multiLine" Width="100%"></asp:TextBox>
                                                                            <asp:Label ID="Label21" runat="server" Text='<%# Eval("Mark") %>'
                                                                                Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>参考答案： 
                                                                        <br />
                                                                            <asp:TextBox ID="TextBox3" runat="server" Height="60px" ReadOnly="true"
                                                                                Text='<%#Eval("Answer") %>' TextMode="multiLine" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                <caption>
                                                                    <br></br>
                                                                </caption>
                                                                </br>
                                                            </caption>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>

                            </table>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


</asp:Content>

