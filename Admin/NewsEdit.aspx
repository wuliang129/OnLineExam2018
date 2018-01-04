<%@ Page Language="C#" validaterequest="false" AutoEventWireup="true" CodeFile="NewsEdit.aspx.cs" Inherits="Admin_NewsEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="../ckeditor/ckeditor.js"></script>
    <style>
        select {
            border: 1px solid;
            height: 25px;
            width: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="place">
                <span>位置：</span>
                <ul class="placeul">
                    <li><a href="main.html">后台首页</a></li>
                    <li><a href="#">添加编辑新闻</a></li>
                </ul>
            </div>

            <div class="formbody">

                <div class="formtitle"><span>基本信息</span></div>

                <ul class="forminfo">
                    <li>
                        <label>文章类别</label><asp:DropDownList ID="ddl_NewsCategory" runat="server" OnInit="ddl_NewsCategory_Init">
                        </asp:DropDownList><i>文章类别</i></li>
                    <li>
                        <label>文章标题</label><asp:TextBox ID="txt_NewsTitle" runat="server" class="dfinput"></asp:TextBox><i>标题不能超过30个字符</i></li>
                    <li>
                        <label>文章副标题</label><asp:TextBox ID="txt_NewsSubtitle" runat="server" class="dfinput"></asp:TextBox></li>
                    <li>
                        <label>文章来源</label><asp:TextBox ID="txt_NewsSource" runat="server" class="dfinput"></asp:TextBox>
                    </li>
                    <li>
                        <label>文章来源URL</label><asp:TextBox ID="txt_NewsSourceURL" runat="server" class="dfinput"></asp:TextBox></li>
                    <li>
                        <label>关键字</label><asp:TextBox ID="txt_NewsKeyword" runat="server" class="dfinput"></asp:TextBox><i>多个关键字用,隔开</i></li>
                    <li>
                        <label>是否审核</label>
                            <div id="special_radio">
                                <asp:RadioButton ID="radiobtn_YES" runat="server"   GroupName="check" Checked="True" />是
                                <asp:RadioButton ID="radiobtn_NO" runat="server" GroupName="check"  />否
                                
                            </div></li>
                   
                     <li>
                        <label>文章内容</label></li>
                    <li>
                        <asp:TextBox ID="tbContent" runat="server" TextMode="MultiLine" class="ckeditor"></asp:TextBox>
                        <script>
                            CKEDITOR.replace('tbContent');
                        </script>
                    </li>

                    <li>
                        <label>&nbsp;</label><asp:Button ID="btn_SaveNews" runat="server" class="btn" Text="保存新闻" OnClick="btn_SaveNews_Click"  /></li>
                </ul>


            </div>
        </div>
    </form>
</body>
</html>
