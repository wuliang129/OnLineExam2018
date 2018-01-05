<%@ Page Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="PaperSetup.aspx.cs" Inherits="Web_PaperSetup" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>试卷制定</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">试卷制定</th>
            </tr>
        </thead>

        <tr>
            <td >
                <div style="min-width:800px;">
                    <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse" width="950px">
                    <tr>
                        <td class="tdtitle_left_bold" colspan="4">
                                试卷制定—自动生成(随机出题) &nbsp;| &nbsp; <a href="papersetup2.aspx"><font color="red">人工出题</font></a> &nbsp;| &nbsp; <a href="BulkImportPaper.aspx"><font color="red">批量导入试卷</font></a>
                        </td>
                    </tr>
                    <tr>
                        <td  class="tdtitle_right_normal">考试科目：</td>
                        <td  class="tdtitle_left_normal">
                            <asp:DropDownList ID="ddlCourse" runat="server" Width="250px"></asp:DropDownList>
                        </td>
                        <td  class="tdtitle_right_normal">试卷名称：</td>
                        <td  class="tdtitle_left_normal">
                            <asp:TextBox ID="txtPaperName" runat="server" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPaperName" ErrorMessage="不能为空！" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        
                    </tr>

                    <tr>
                        <td class="tdtitle_left_bold" colspan="4">单选题：</td>
                    </tr>
                    <tr>
                        <td class="tdtitle_right_normal" >题目数目：</td>
                        <td class="tdtitle_left_normal" >
                            <asp:TextBox ID="txtSingleNum" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtSingleNum" ValidationExpression="^[1-9]\d{0,1}"
                                ErrorMessage="取值范围:1~99" Display="Dynamic"  ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空" ControlToValidate="txtSingleNum" Display="Dynamic"  ForeColor="Red"></asp:RequiredFieldValidator>

                        </td>
                        <td class="tdtitle_right_normal" >每题分值：</td>
                        <td class="tdtitle_left_normal">
                            <asp:TextBox ID="txtSingleFen" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSingleFen" ValidationExpression="^[1-9]\d{0,1}" ErrorMessage="取值范围:1~99" Display="Dynamic"  ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="不能为空" ControlToValidate="txtSingleFen" Display="Dynamic"  ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle_left_bold" colspan="4" >多选题：</td>
                    </tr>
                    <tr>
                        <td class="tdtitle_right_normal" >题目数目：</td>
                        <td class="tdtitle_left_normal" >
                            <asp:TextBox ID="txtMultiNum" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ControlToValidate="txtMultiNum" ValidationExpression="^[1-9]\d{0,1}"
                                ErrorMessage="取值范围:1~99" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="不能为空" ControlToValidate="txtMultiNum" ForeColor="Red" ></asp:RequiredFieldValidator>
                        </td>
                        <td class="tdtitle_right_normal" >每题分值：</td>
                        <td class="tdtitle_left_normal">
                            <asp:TextBox ID="txtMultiFen" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMultiFen" ValidationExpression="^[1-9]\d{0,1}" ErrorMessage="取值范围:1~99" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="不能为空" ControlToValidate="txtMultiFen" ForeColor="Red"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle_left_bold" colspan="4">
                            判断题：
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle_right_normal" >题目数目：</td>
                        <td class="tdtitle_left_normal" >
                            <asp:TextBox ID="txtJudgeNum" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                ControlToValidate="txtJudgeNum" ValidationExpression="^[1-9]\d{0,1}"
                                ErrorMessage="取值范围:1~99" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="不能为空" ControlToValidate="txtJudgeNum" ForeColor="Red"></asp:RequiredFieldValidator>
                       
                        </td>
                        <td class="tdtitle_right_normal" >每题分值：</td>
                        <td class="tdtitle_left_normal">
                            <asp:TextBox ID="txtJudgeFen" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtJudgeFen" ValidationExpression="^[1-9]\d{0,1}" ErrorMessage="取值范围:1~99" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="不能为空" ControlToValidate="txtJudgeFen" ForeColor="Red"></asp:RequiredFieldValidator>
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle_left_bold" colspan="4">
                            填空题：
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle_right_normal" >题目数目：</td>
                        <td class="tdtitle_left_normal" >
                            <asp:TextBox ID="txtFillNum" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                ControlToValidate="txtFillNum" ValidationExpression="^[1-9]\d{0,1}"
                                ErrorMessage="取值范围:1~99" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="不能为空" ControlToValidate="txtFillNum" ForeColor="Red"></asp:RequiredFieldValidator>
                        
                        </td>
                        <td class="tdtitle_right_normal" >每题分值：</td>
                        <td class="tdtitle_left_normal">
                            <asp:TextBox ID="txtFillFen" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtFillFen" ValidationExpression="^[1-9]\d{0,1}" ErrorMessage="取值范围:1~99" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="不能为空" ControlToValidate="txtFillFen" ForeColor="Red"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle_left_bold" colspan="4">
                            问答题：
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle_right_normal" >题目数目：</td>
                        <td class="tdtitle_left_normal" >
                            <asp:TextBox ID="txtQuestionNum" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                                ControlToValidate="txtQuestionNum" ValidationExpression="^[1-9]\d{0,1}"
                                ErrorMessage="取值范围:1~99" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="不能为空" ControlToValidate="txtQuestionNum" ForeColor="Red"></asp:RequiredFieldValidator>
                       
                        </td>
                        <td class="tdtitle_right_normal" >每题分值：</td>
                        <td class="tdtitle_left_normal">
                            <asp:TextBox ID="txtQuestionFen" runat="server" Width="120px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10"
                                runat="server" ControlToValidate="txtQuestionFen"
                                ValidationExpression="^[1-9]\d{0,1}" ErrorMessage="取值范围:1~99" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="不能为空" ControlToValidate="txtQuestionFen" ForeColor="Red"></asp:RequiredFieldValidator>
                       
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" align="center">

                            <table>
                                <tr>
                                    <td></td>
                                    <td>
                                        <ul class="toolbar">
                                            <li class="click_nodisplay">
                                                <div class="oneLine_confirm">
                                                    <asp:LinkButton ID="lbtn_Conirm" runat="server" OnClick="lbtn_Conirm_Click">确认</asp:LinkButton>
                                                </div>
                                            </li>

                                        </ul>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>




                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="False">
                                <table cellspacing="0" style="font-size: 12px; font-family: Tahoma; border-collapse: collapse;" cellpadding="0" width="100%" bgcolor="#ffffff" border="1" bordercolor="gray">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="一、单选题">
                                                        <ItemTemplate>
                                                            <table id="Table2" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                                <br />
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ID") %>' Visible="False">
                                                                        </asp:Label>
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
                                                <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="二、多选题">
                                                        <ItemTemplate>
                                                            <table id="Table3" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                                <br />
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("ID") %>' Visible="False">
                                                                        </asp:Label>
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
                                                <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="三、判断题">
                                                        <ItemTemplate>
                                                            <table id="Table4" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                                <br />
                                                                <tr>
                                                                    <td width="85%">
                                                                        <asp:Label ID="Label19" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label20" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("ID") %>' Visible="False">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td width="15%">
                                                                        <asp:CheckBox ID="CheckBox5" runat="server" Text="正确"></asp:CheckBox></td>
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
                                                    <asp:TemplateField HeaderText="四、填空题">
                                                        <ItemTemplate>
                                                            <table id="Table5" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                                <br />
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label16" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label17" runat="server" Text='<%# Eval("FrontTitle","、{0}") %>'>
                                                                        </asp:Label>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Width="100px" Style="border-bottom: gray   1px   solid" BorderStyle="None"></asp:TextBox>
                                                                        <asp:Label ID="Label18" runat="server" Text='<%# Eval("BackTitle") %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("ID") %>' Visible="False">
                                                                        </asp:Label>
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
                                                    <asp:TemplateField HeaderText="四、问答题">
                                                        <ItemTemplate>
                                                            <table id="Table6" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                                                <br>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label21" runat="server" Text='<%# Container.DataItemIndex+1 %>'>
                                                                        </asp:Label>
                                                                        <asp:Label ID="Label22" runat="server" Text='<%# Eval("Title","、{0}") %>'>
                                                                        </asp:Label>
                                                                        <br />
                                                                        <asp:TextBox ID="txtAnswer" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:Label ID="Label23" runat="server" Text='<%# Eval("ID") %>' Visible="False">
                                                                        </asp:Label>
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
                                        <td align="center" style="height: 31px">
                                            <table>
                                                <tr>
                                                    <td></td>
                                                    <td style="width: 50px; height: 40px;">
                                                        <ul class="toolbar">
                                                            <li class="click" style="width:80px;">
                                                                <div class="oneLine_save">
                                                                    <asp:LinkButton ID="lbtn_SavePaper" runat="server" OnClick="lbtn_SavePaper_Click">保存试卷</asp:LinkButton>
                                                                </div>
                                                            </li>

                                                        </ul>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>

                </table>
                </div>
                

            </td>
        </tr>
    </table>



</asp:Content>
