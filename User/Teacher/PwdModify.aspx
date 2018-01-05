<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/common_head.master" AutoEventWireup="true" CodeFile="PwdModify.aspx.cs" Inherits="Web_teacher_PwdModify" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>修改密码</title>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="UserCenter">
        <thead>
            <tr>
                <th class="thtitle">修改密码</th>
            </tr>
        </thead>

        <tr>
            <td valign="top" align="left">


                <table cellpadding="0" cellspacing="0" border="1" bordercolor="#cccccc" style="border-collapse: collapse" width="100%" frame="below">
                    <tr style="height: 35px;">
                        <td bgcolor="#eeeeee" style="text-align: right;">原密码：</td>
                        <td>&nbsp;<div align="left" style="margin-left: 2px; height: 35px;">
                            <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password" Width="170px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPwd" ErrorMessage="不能为空！" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#eeeeee" style="text-align: right;">新密码：</td>
                        <td>&nbsp;<div align="left" style="margin-left: 2px; height: 35px;">
                            <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" Width="170px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPwd"
                                ErrorMessage="不能为空！" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#eeeeee" style="text-align: right;">确认密码：</td>
                        <td>&nbsp;<div align="left" style="margin-left: 2px; height: 35px;">
                            <asp:TextBox ID="txtConfirmPwd" runat="server" MaxLength="20" TextMode="password" Width="168px"></asp:TextBox>
                            <asp:CompareValidator ID="cpv_newpassword" runat="server" ErrorMessage="确认密码不一致" ControlToValidate="txtConfirmPwd" ControlToCompare="txtNewPwd" ForeColor="Red"></asp:CompareValidator>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>&nbsp;
                            <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label><br />
                            </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <ul class="toolbar">
                                <li class="click">
                                    <div class="oneLine_update"><asp:LinkButton ID="lbtn_ModifyPwd" runat="server" OnClick="lbtn_ModifyPwd_Click">修改</asp:LinkButton></div></li>
                                <li class="click"><div class="oneLine_cancel"><input style="border:none; font-family: '微软雅黑'; font-size: 14px; background: url(/User/CommonPage//images/toolbg.gif) repeat-x; height:33px; "  type="reset" /></div></li>
                            </ul>
                        </td>
                    </tr>

                </table>

            </td>
        </tr>
    </table>
</asp:Content>

