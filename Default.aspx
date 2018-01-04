<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/User_MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/CustomControl/NewsListByCategory.ascx" TagName="ucNewsListByCategory" TagPrefix="uc" %>
<%@ Register Src="~/CustomControl/NewsListByCategory_TimeLeft.ascx" TagName="ucNewsListByCategory_TimeLeft" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>首页-河南暖男信息技术有限责任公司</title>
    <link rel="stylesheet" type="text/css" href="CSS/NewsStyle.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Default.css" />

    <script language="javascript" src="./scripts/jquery.min.js" sudy-wp-context="" sudy-wp-siteid="3" style="color: rgb(0, 0, 0);"></script>
    <script language="javascript" src="./scripts/jquery.sudy.wp.visitcount.js"></script>
    <script type="text/javascript" src="./scripts/jquery.sudy.js"></script>

    <script type="text/javascript">
        $(function () {
            $(".scroll1").sudyScroll({
                //width: 294,		// 单元格宽度
                height: 65,		// 单元格高度
                display: 3,		// 显示几个单元
                step: 3,			// 每次交替增加几个单元，值不能大于display
                dir: "y",		// 交替方向，纵向为"y"，水平为"x"，默认为"y"纵向交替
                auto: true,		// 是否自动交替,默认为自动
                speed: 500,		// 交替速度
                hoverPause: 5000,		// 交替暂留时间
                navigation: false,		// 是否显示导航按钮
                navTrigger: "click", 	// 导航按钮事件
                pagination: true,		// 是否显示索引按钮
                pagTrigger: "mouseenter"  //索引按钮事件
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".scroll").sudyScroll({
                width: 653,		// 单元格宽度
                height: 171,		// 单元格高度
                display: 1,		// 显示几个单元
                step: 1,			// 每次交替增加几个单元，值不能大于display
                dir: "x",		// 交替方向，纵向为"y"，水平为"x"，默认为"y"纵向交替
                auto: true,		// 是否自动交替,默认为自动
                speed: 500,		// 交替速度
                hoverPause: 5000,		// 交替暂留时间
                navigation: true,		// 是否显示导航按钮
                navTrigger: "click", 	// 导航按钮事件
                pagination: false,		// 是否显示索引按钮
                pagTrigger: "mouseenter"  //索引按钮事件
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".scroll2").sudyScroll({
                width: 653,		// 单元格宽度
                height: 28,		// 单元格高度
                display: 4,		// 显示几个单元
                step: 4,			// 每次交替增加几个单元，值不能大于display
                dir: "y",		// 交替方向，纵向为"y"，水平为"x"，默认为"y"纵向交替
                auto: true,		// 是否自动交替,默认为自动
                speed: 500,		// 交替速度
                hoverPause: 5000,		// 交替暂留时间
                navigation: false,		// 是否显示导航按钮
                navTrigger: "click", 	// 导航按钮事件
                pagination: true,		// 是否显示索引按钮
                pagTrigger: "mouseenter"  //索引按钮事件
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="banner">

        <div class="wrapper" id="necker">
            <div class="inner">
                <!-- Picture Transparency Effects Start -->
                <div id="yc-mod-slider">
                    <div class="wrapper" frag="面板20">
                        <div class="box_skitter fn-clear" id="slideshow"><a href="#" class="prev_button" style="opacity: 0; display: block;">prev</a><a href="#" class="next_button" style="opacity: 0; display: none;">next</a><span class="info_slide" style="display: none;"><span class="image_number" rel="0" id="image_n_1_0" style="background-color: rgb(51, 51, 51); color: rgb(255, 255, 255);">1</span> <span class="image_number" rel="1" id="image_n_2_0" style="background-color: rgb(51, 51, 51); color: rgb(255, 255, 255);">2</span> <span class="image_number" rel="2" id="image_n_3_0" style="background-color: rgb(51, 51, 51); color: rgb(255, 255, 255);">3</span> <span class="image_number" rel="3" id="image_n_4_0" style="background-color: rgb(51, 51, 51); color: rgb(255, 255, 255);">4</span> <span class="image_number" rel="4" id="image_n_5_0" style="background-color: rgb(51, 51, 51); color: rgb(255, 255, 255);">5</span> <span class="image_number image_number_select" rel="5" id="image_n_6_0" style="background-color: rgb(204, 51, 51); color: rgb(255, 255, 255);">6</span> </span>
                            <div class="container_skitter" style="width: 1250px; height: 448px;">
                                <div class="image"><a href="#">
                                    <img class="image_main" src="/_upload/article/images/92/a5/38e26dd64bf987688ef8a68b1fc4/52d854c5-e602-4fe9-8499-8d66295c13f4.jpg" style="display: inline;"></a></div>
                                <div class="box_clone" style="left: -78.125px; top: -78.125px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: 0px; top: 0px;"></div>
                                <div class="box_clone" style="left: -78.125px; top: 71.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: 0px; top: -150px;"></div>
                                <div class="box_clone" style="left: -78.125px; top: 221.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: 0px; top: -300px;"></div>
                                <div class="box_clone" style="left: 78.875px; top: -78.125px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -157px; top: 0px;"></div>
                                <div class="box_clone" style="left: 78.875px; top: 71.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -157px; top: -150px;"></div>
                                <div class="box_clone" style="left: 78.875px; top: 221.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -157px; top: -300px;"></div>
                                <div class="box_clone" style="left: 235.875px; top: -78.125px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -314px; top: 0px;"></div>
                                <div class="box_clone" style="left: 235.875px; top: 71.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -314px; top: -150px;"></div>
                                <div class="box_clone" style="left: 235.875px; top: 221.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -314px; top: -300px;"></div>
                                <div class="box_clone" style="left: 392.875px; top: -78.125px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -471px; top: 0px;"></div>
                                <div class="box_clone" style="left: 392.875px; top: 71.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -471px; top: -150px;"></div>
                                <div class="box_clone" style="left: 392.875px; top: 221.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -471px; top: -300px;"></div>
                                <div class="box_clone" style="left: 549.875px; top: -78.125px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -628px; top: 0px;"></div>
                                <div class="box_clone" style="left: 549.875px; top: 71.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -628px; top: -150px;"></div>
                                <div class="box_clone" style="left: 549.875px; top: 221.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -628px; top: -300px;"></div>
                                <div class="box_clone" style="left: 706.875px; top: -78.125px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -785px; top: 0px;"></div>
                                <div class="box_clone" style="left: 706.875px; top: 71.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -785px; top: -150px;"></div>
                                <div class="box_clone" style="left: 706.875px; top: 221.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -785px; top: -300px;"></div>
                                <div class="box_clone" style="left: 863.875px; top: -78.125px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -942px; top: 0px;"></div>
                                <div class="box_clone" style="left: 863.875px; top: 71.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -942px; top: -150px;"></div>
                                <div class="box_clone" style="left: 863.875px; top: 221.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -942px; top: -300px;"></div>
                                <div class="box_clone" style="left: 1020.88px; top: -78.125px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -1099px; top: 0px;"></div>
                                <div class="box_clone" style="left: 1020.88px; top: 71.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -1099px; top: -150px;"></div>
                                <div class="box_clone" style="left: 1020.88px; top: 221.875px; width: 157px; height: 150px; display: none;">
                                    <img src="/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg" style="left: -1099px; top: -300px;"></div>
                            </div>
                            <div class="label_skitter" style="width: 1250px; right: 625px; opacity: 1; display: block;">建功十三五 迈向高水平</div>
                            <div class="slide-month clearfix"><a class="month-nav" id="prev-month">&lt;</a><a class="month-nav" id="next-month">&gt;</a><div class="month-text"><span id="cur-year">2017</span><span id="cur-month">05</span>月<span id="cur-day">15</span></div>
                                <div class="month-texts"><span id="cur-year">2017</span>-<span id="cur-month">05</span></div>
                                <div class="month-days" id="month-days"><a title="2017-05-01" class="has-none">1</a><a title="2017-05-02" class="has-none">2</a><a title="2017-05-03" class="has-none">3</a><a title="2017-05-04" class="has-none">4</a><a title="2017-05-05" class="has-none">5</a><a title="2017-05-06" class="has-none">6</a><a title="2017-05-07" class="has-none">7</a><a title="2017-05-08" class="has-none">8</a><a title="2017-05-09" class="has-none">9</a><a title="2017-05-10" class="has-none">10</a><a title="2017-05-11" class="has-none">11</a><a title="2017-05-12" class="has-none">12</a><a title="2017-05-13" class="has-none">13</a><a title="2017-05-14" class="has-none">14</a><a title="2017-05-15" class="has-event cur-event">15</a><a title="2017-05-16" class="has-none">16</a><a title="2017-05-17" class="has-none">17</a><a title="2017-05-18" class="has-none">18</a><a title="2017-05-19" class="has-none">19</a><a title="2017-05-20" class="has-none">20</a><a title="2017-05-21" class="has-none">21</a><a title="2017-05-22" class="has-none">22</a><a title="2017-05-23" class="has-none">23</a><a title="2017-05-24" class="has-none">24</a><a title="2017-05-25" class="has-none">25</a><a title="2017-05-26" class="has-none">26</a><a title="2017-05-27" class="has-none">27</a><a title="2017-05-28" class="has-none">28</a><a title="2017-05-29" class="has-none">29</a><a title="2017-05-30" class="has-none">30</a><a title="2017-05-31" class="has-none">31</a></div>
                            </div>
                        </div>
                        <div style="display: none;" frag="窗口20" portletmode="simpleNews" configs="{'c42':'320','c25':'320','c30':'0','c29':'1','c23':'1','c34':'300','c20':'0','c31':'0','c16':'1','c3':'6','c2':'序号,标题,发布时间','c27':'480','c43':'0','c17':'0','c5':'_blank','c24':'240','c32':'','c26':'1','c37':'1','c28':'640','c40':'1','c15':'0','c14':'1','c44':'0','c33':'500','c10':'50','c18':'yyyy-MM-dd','c36':'0','c1':'1','c6':'15','c19':'yyyy-MM-dd','c21':'0','c4':'1','c35':'-1:-1','c39':'300','c38':'100','c7':'1','c12':'0','c9':'0','c11':'1','c13':'200','c41':'240'}" contents="{'c2':'0', 'c1':'/2017改版/首页大图'}">
                            <div id="wp_news_w20">

                                <script type="text/javascript">
                                    var dataJson = [

                                                {
                                                    "date": "2017-12-13",
                                                    "src": "/_upload/article/images/35/d3/1951f49a40ebaf4fbaa4a216d79f/fc633a4f-9129-47bf-9c14-edfae65a7ba6.jpg",
                                                    "url": "/_redirect?siteId=3&columnId=9003&articleId=109224",
                                                    "text": "全国文明单位"
                                                },
                                                {
                                                    "date": "2017-10-31",
                                                    "src": "/_upload/article/images/f1/0c/a06d0120466db61270fca99f0ba3/bab659bf-362c-4ecb-ba1f-d8aa1a1f69d8.jpg",
                                                    "url": "/_redirect?siteId=3&columnId=9003&articleId=105100",
                                                    "text": "党的十九大"
                                                },
                                                {
                                                    "date": "2017-11-30",
                                                    "src": "/_upload/article/images/d7/66/306875984eb08b2affb946b61dbd/9ad6928f-a3e1-4b4f-a70b-aedcb0004de8.jpg",
                                                    "url": "#",
                                                    "text": "12月"
                                                },
                                                {
                                                    "date": "2017-11-17",
                                                    "src": "/_upload/article/images/4f/ac/a5c414c740189b376e4fed53d1a7/2881c142-1cc9-40ef-b314-a661b7349897.jpg",
                                                    "url": "/2017/1117/c9003a106888/page.htm",
                                                    "text": "秋"
                                                },
                                                {
                                                    "date": "2017-09-29",
                                                    "src": "/_upload/article/images/d6/d9/3b633a55490e980498537c1dda4c/ef798a50-bdaf-4575-b40f-cf579830a2c7.jpg",
                                                    "url": "/2017/0929/c9003a103034/page.htm",
                                                    "text": "师大欢迎你"
                                                },
                                                {
                                                    "date": "2017-05-15",
                                                    "src": "/_upload/article/images/92/a5/38e26dd64bf987688ef8a68b1fc4/52d854c5-e602-4fe9-8499-8d66295c13f4.jpg",
                                                    "url": "#",
                                                    "text": "建功十三五 迈向高水平"
                                                },
                                                {}
                                    ];
                                    dataJson.pop();
                                </script>
                            </div>

                        </div>
                        <script type="text/javascript">
                            (function ($) {
                                $('#slideshow').skitter({
                                    width: 1250,
                                    height: 448,
                                    animation: 'random',
                                    velocity: 1.3,
                                    interval: 3500,
                                    navigation: 1,
                                    json: dataJson,
                                    caption: "auto",
                                    dateIndex: true
                                });

                            })(jQuery);
                        </script>
                    </div>
                </div>
                <!-- Picture Transparency Effects End -->
            </div>
        </div>
    </div>
    <div style="width: 100%; height: 368px; clear: both;">
        <uc:ucNewsListByCategory ID="ucNewsListByCategory1" runat="server" />

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div style="background-color: #F0F8FF; margin: 0 auto; height: 255px; clear: both;">
        <div class="NewsListByCategory_Second" style="width: 1200px; margin: 0 auto;">

            <uc:ucNewsListByCategory ID="ucNewsListByCategory_Second" runat="server" />

            <uc:ucNewsListByCategory_TimeLeft ID="ucNewsListByCategory_Second_TimeLeft" runat="server" />


        </div>
    </div>

</asp:Content>

