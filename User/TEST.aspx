<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST.aspx.cs" Inherits="User_TEST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="../CSS/MenuStylet.css" />
    <script style="color: rgb(0, 0, 0);" language="javascript" src="../scripts/jquery.js" sudy-wp-context="" sudy-wp-siteid="3"></script>
    <script type="text/javascript" src="../scripts/jquery_003.js"></script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div_menu">
            <div class="header">
                <div class="head">
                    <div class="head_top">
                        <ul class="wp_nav" data-nav-config="{drop_v: &#39;down&#39;, drop_w: &#39;right&#39;, dir: &#39;y&#39;, opacity_main: &#39;-1&#39;, opacity_sub: &#39;-1&#39;, dWidth: &#39;0&#39;}">

                            <li class="nav-item i1">
                                <a href="http://www.htu.cn/sdnw/list.htm" target="_self" class=""><span class="item-name">师大内网</span></a><i class="mark"></i>

                            </li>

                            <li class="nav-item i2">
                                <a href="http://www.htu.edu.cn/nc/3903/list.htm" target="blank" class=""><span class="item-name">校外访问通道</span></a><i class="mark"></i>

                            </li>

                            <li class="nav-item i3">
                                <a href="http://my.htu.edu.cn/" target="blank" class=""><span class="item-name">信息门户</span></a><i class="mark"></i>

                            </li>

                            <li class="nav-item i4">
                                <a href="http://www.htu.cn/english/" target="_self"><span class="item-name">English</span></a><i class="mark"></i>

                            </li>

                        </ul>
                    </div>
                    <div class="head_middle">
                        <img src="../Image/head/logo.png" />
                    </div>
                </div>
            </div>

            <div class="nav">
                <div class="menu">
                    <div frag="面板2">
                        <div frag="窗口2" portletmode="simpleSudyNavi" configs="{'c1':'1','c7':'2','c4':'_self','c3':'12','c8':'2','c9':'0','c2':'1','c5':'1'}" contents="{'c2':'0', 'c1':'/2017改版/网站首页,/2017改版/学校概况,/2017改版/机构设置,/2017改版/学科建设,/2017改版/人才引进,/2017改版/数字校园,/2017改版/招生就业,/2017改版/信息公开,/2017改版/校长信箱,/2017改版/校友网'}">
                            <div id="wp_nav_w2">


                                <ul class="wp_nav" data-nav-config="{drop_v: 'down', drop_w: 'right', dir: 'y', opacity_main: '-1', opacity_sub: '-1', dWidth: '0'}">

                                    <asp:PlaceHolder ID="plh_MenuInfo" runat="server"></asp:PlaceHolder>


                                </ul>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="footer_bottom">
            <div class="foot_bottom">
                
            </div>
        </div>
        <div class="footer_copyright">
            <div class="foot_copyright">
                <div class="foot_left">
                    <p>版权所有©河南暖男信息技术有限公司  地址：河南省安阳市文明大道436号图书馆三楼   邮编：455000  豫ICP备：xxxxx号  豫公网安备：xxxxxxxxxxxxx号</p>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
