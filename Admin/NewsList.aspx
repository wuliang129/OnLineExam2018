<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="Admin_NewsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".click").click(function () {
                $(".tip").fadeIn(200);
            });

            $(".tiptop a").click(function () {
                $(".tip").fadeOut(200);
            });

            $(".sure").click(function () {
                $(".tip").fadeOut(100);
                $(".tip_DelNoInfo").fadeOut(100);
            });

            $(".cancel").click(function () {
                $(".tip").fadeOut(100);
            });

            $(".click_DelNoInfo").click(function () {
                $(".tip_DelNoInfo").fadeIn(200);
            });
            

        });
    </script>


</head>
<body>

    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="main.html">首页</a></li>
            <li><a href="#">管理信息</a></li>
            <li><a href="#">新闻列表</a></li>
        </ul>
    </div>

    <div class="rightinfo">
        <form id="form1" runat="server">
            <div class="tools">

                <ul class="toolbar">
                    <li><a href="NewsEdit.aspx" target="rightFrame"><span>
                        <img src="images/t01.png" /></span>添加</a></li>
                    <li ><span>
                        <img src="images/t02.png" /></span>修改</li>
                    <li><span>
                        <img src="images/t03.png" /></span><asp:LinkButton ID="linkBtn_Del" runat="server" OnClick="linkBtn_Del_Click">删除</asp:LinkButton></li>
                    <li class="click"><span>
                        <img src="images/t04.png" /></span>统计</li>
                </ul>


                <ul class="toolbar1">
                    <li><span>
                        <img src="images/t05.png" /></span>设置</li>
                </ul>

            </div>

            <div class="tip">
                <div class="tiptop"><span>提示信息</span><a></a></div>

                <div class="tipinfo">
                    <span>
                        <img src="images/ticon.png" /></span>
                    <div class="tipright">
                        <p>是否确认对信息的修改 ？</p>
                        <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
                    </div>
                </div>

                <div class="tipbtn">
                    <input name="" type="button" class="sure" value="确定" />&nbsp;
        <input name="" type="button" class="cancel" value="取消" />
                </div>

            </div>

            <div class="tip_DelNoInfo">
                <div class="tiptop"><span>提示信息</span><a></a></div>

                <div class="tipinfo">
                    <span>
                        <img src="images/ticon.png" /></span>
                    <div class="tipright">
                        <p>没有选中要删除的记录，请先选中记录再删除！</p>
                    </div>
                </div>

                <div class="tipbtn">
                    <input name="" type="button" class="sure" value="确定" />
                </div>

            </div>

            

            <div style="margin-top:10px;">
                <asp:GridView ID="gv_NewsList" runat="server" AllowSorting="True" OnInit="gv_NewsList_Init"  CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="tablelist" AutoGenerateColumns="False" OnRowDeleting="gv_NewsList_RowDeleting" OnRowEditing="gv_NewsList_RowEditing" PageSize="2">
                    <Columns>
                        <asp:TemplateField HeaderText="全选">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chk_AllItem" runat="server" Text="全选" AutoPostBack="True" OnCheckedChanged="chk_AllItem_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Item" runat="server" />
                            </ItemTemplate>
                            <ControlStyle  />
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="新闻编号" >
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NewsTitle" HeaderText="新闻标题" >
                        <ControlStyle  />
                        </asp:BoundField>
                        <asp:BoundField DataField="NewsAuthor" HeaderText="用户" >
                        <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NewsTime" HeaderText="发布时间" >
                        <ItemStyle Width="250px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="NewsAuditSuccess" HeaderText="是否审核" ShowHeader="False" Text="通过审核" >
                        <ControlStyle BorderColor="#003366" BorderStyle="Solid" />
                        <ItemStyle Width="100px" />
                        </asp:CheckBoxField>
                        <asp:CommandField HeaderText="操作"  ShowEditButton="True"  ShowDeleteButton="True" SelectText="查看">
                        <ItemStyle Width="150px" ForeColor="#0066FF" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="pagin">
                <div class="message">共<i class="blue"><asp:Label ID="lbl_NewsTotalCount" runat="server" Text="Label"></asp:Label></i>条记录，当前显示第<i class="blue"><asp:Label ID="lbl_PageIndex" runat="server" Text="Label"></asp:Label></i>页</div>
                <ul class="paginList">
                    <%--<li class="paginItem"><a href="javascript:;"><span class="pagepre"></span></a></li>--%>
                   
                    <asp:PlaceHolder ID="ph_PageList" runat="server"></asp:PlaceHolder>

                    <%--<li class="paginItem"><a href="javascript:;">1<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton></a></li>
                    <li class="paginItem current"><a href="javascript:;">2</a></li>
                    <li class="paginItem"><a href="javascript:;">3</a></li>
                    <li class="paginItem"><a href="javascript:;">4</a></li>
                    <li class="paginItem"><a href="javascript:;">5</a></li>
                    <li class="paginItem more"><a href="javascript:;">...</a></li>
                    <li class="paginItem"><a href="javascript:;"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" >LinkButton</asp:LinkButton>10</a></li>--%>




                    <%--<li class="paginItem"><a href="javascript:;"><span class="pagenxt"></span></a></li>--%>
                </ul>
            </div>
        </form>

    </div>

    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>


    
</body>
</html>
