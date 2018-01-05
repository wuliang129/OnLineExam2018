<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentTestPaper.aspx.cs" Inherits="Web_student_StudentTestPaper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线考试界面</title>
    <script language="JavaScript" src="/scripts/Morning_JS.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <!--头部标签-->
        <div style="background: url('/Image/User/ayrjxy_bg.jpg') repeat-x;">
            <table border="0" cellspacing="0px" cellpadding="0px" width="100%" align="center">
                <tbody>
                    <tr>
                        <td height="110px">
                            <img alt="" src="/Image/User/ayrjxy_logo.jpg" /></td>
                        <td
                            style="width: 661px; background: url('/Image/User/ayrjxy_logo_topheader.jpg') repeat-x 50% top;" valign="top" align="right"></td>
                    </tr>
                </tbody>
            </table>
        </div>


        <div align="center">

            <table cellspacing="0" style="font-size: 12px; font-family: Tahoma; border-collapse: collapse; width: 100%; height: 100%;" cellpadding="0" align="center"
                bgcolor="#ffffff" border="0" bordercolor="gray">
                <tr style="height: 40px;">
                    <td align="left" valign="middle" style="font-size: 12px; width: 200px;">
                        <ul style="list-style: none; float: left; margin: 0px; padding: 0px;">
                            <li style="line-height: 18px;">
                                <asp:Label ID="lbl_UserInfo" runat="server" Text="Label"></asp:Label></li>
                            <li style="line-height: 18px;"><span id="ShowTime">
                                <script type="text/javascript">getDate()</script>
                            </span></li>
                        </ul>
                    </td>
                    <td align="center" style="font-size: 22px; color: #4D2600; font-weight: bold;">
                        <asp:Label ID="lbl_PaperName" runat="server" Text="考试试题："></asp:Label>
                    </td>
                    <td align="right" valign="middle" style="font-size: 12px; width: 200px; padding-right:20px;">
                        <script type="text/javascript" language="JavaScript">
                            var maxtime;
                            if (window.name == '' || window.name == '_TestWindows') {
                                maxtime = 120 * 60;//考试结束时间 120分钟 目前以秒来刷新 后期以分钟来刷新
                            } else {
                                maxtime = window.name;
                            }

                            function CountDown() {
                                if (maxtime >= 0) {
                                    minutes = Math.floor(maxtime / 60);
                                    seconds = Math.floor(maxtime % 60);
                                    msg = "距考试结束还有" + minutes + "分" + seconds + "秒";
                                    document.all["timer"].innerHTML = msg;
                                    if (maxtime == 5 * 60) alert('注意，还有5分钟!');
                                    --maxtime;
                                    window.name = maxtime;
                                }
                                else {
                                    clearInterval(timer);
                                    alert("考试时间到，结束考试!");
                                    document.getElementById("imgBtnSubmit").click(); //规定时间结束后自动提交按钮 提交试卷 暂时禁止自动提交
                                }
                                //getDate();//更新日期
                            }
                            timer = setInterval("CountDown()", 1000);
                        </script>
                        <div id="timer" style="color: red; font-family: 宋体; font-size: 12px; font-weight: bold;"></div>

                    </td>
                </tr>
            </table>

            <table cellspacing="0" style="font-size: 12px; font-family: Tahoma; border-collapse: collapse; width: 90%; height: 100%;" cellpadding="0" align="center"
                bgcolor="#ffffff" border="1" bordercolor="gray">

                <tr>
                    <td>
                        <asp:GridView ID="gv_SingleSelection" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="Label24" runat="server" Text="一、单选题(每题">
                                        </asp:Label>
                                        <asp:Label ID="Label27" runat="server">
                                        </asp:Label>
                                        <asp:Label ID="Label25" runat="server" Text="分)">
                                        </asp:Label>
                                        <br />
                                        <br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table id="Table2" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Mark") %>' Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="Label40" runat="server" Text='<%# Eval("TitleID") %>' Visible="False"></asp:Label>
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
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:GridView ID="gv_MutiSelection" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="Label22" runat="server" Text="二、多选题(每题">
                                        </asp:Label>
                                        <asp:Label ID="Label28" runat="server">
                                        </asp:Label>
                                        <asp:Label ID="Label23" runat="server" Text="分)">
                                        </asp:Label><br />
                                        <br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table id="Table3" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Mark") %>' Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="Label41" runat="server" Text='<%# Eval("TitleID") %>' Visible="False"></asp:Label>
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
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" BackColor="#4A3C8C" ForeColor="#E7E7FF" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#594B9C" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#33276A" />
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:GridView ID="gv_Judge" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="2" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="Label20" runat="server" Text="三、判断题(每题">
                                        </asp:Label>
                                        <asp:Label ID="Label29" runat="server">
                                        </asp:Label>
                                        <asp:Label ID="Label21" runat="server" Text="分)">
                                        </asp:Label><br />
                                        <br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table id="Table4" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                            <tr>
                                                <td width="85%">
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("Mark") %>' Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="Label42" runat="server" Text='<%# Eval("TitleID") %>' Visible="False"></asp:Label>
                                                </td>
                                                <td width="15%">
                                                    <asp:CheckBox ID="CheckBox5" runat="server" Text="正确"></asp:CheckBox></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:GridView ID="gv_FillBlank" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="Label18" runat="server" Text="四、填空题(每题">
                                        </asp:Label>
                                        <asp:Label ID="Label30" runat="server">
                                        </asp:Label>
                                        <asp:Label ID="Label19" runat="server" Text="分)">
                                        </asp:Label><br />
                                        <br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table id="Table5" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("FrontTitle","、{0}") %>'>
                                                    </asp:Label>
                                                    <asp:TextBox ID="TextBox1" runat="server" Width="100px" Style="border-bottom: gray   1px   solid" BorderStyle="None" TextMode="MultiLine" Height="25px" Rows="1"></asp:TextBox>
                                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("BackTitle") %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label17" runat="server" Text='<%# Eval("Mark") %>' Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="Label43" runat="server" Text='<%# Eval("TitleID") %>' Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" ForeColor="#003399" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:GridView ID="gv_Question" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="Label32" runat="server" Text="五、问答题(每题">
                                        </asp:Label>
                                        <asp:Label ID="Label31" runat="server">
                                        </asp:Label>
                                        <asp:Label ID="Label33" runat="server" Text="分">
                                        </asp:Label><br />
                                        <br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table id="Table6" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label33" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                    </asp:Label>
                                                    <asp:Label ID="Label34" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                    </asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TextBox2" runat="server" Width="95%" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:Label ID="Label37" runat="server" Text='<%# Eval("Mark") %>' Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="Label44" runat="server" Text='<%# Eval("TitleID") %>' Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#000065" />
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td style="background:url(/Image/User/TestBottombg.png) repeat;" height="20px"></td>
                </tr>
                <tr>
                    <td align="center">
                        <br />
                        <asp:ImageButton ID="imgBtnSubmit" runat="server" ImageUrl="/Image/User/Submit.GIF" OnClick="imgBtnSubmit_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <br />
                        <a href="#top">返回顶端</a>
                    </td>
                </tr>

            </table>
        </div>

    </form>
</body>
</html>
