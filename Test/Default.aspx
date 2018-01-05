<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Test_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        body, div{background-color: #CCC;margin: 0;padding: 0;}
        a{ text-decoration:none;}
        .container{width: 1000px;margin: 100px auto;padding: 0;}
        .btn-test{font-size: 30px;cursor: pointer;width: 250px;height: 100px;}
        .shade_new{width: 100%;height: 100%;background: rgba(0,0,0,0.6);position: fixed;top: 0px;left: 0px;z-index: 1000; display:none;}
        .pop1{background:url(../images/popbg1.png) no-repeat;width:473px; height:272px; position:relative;margin: 200px auto; }
        .close{ width:48px; height:48px; background:url(../images/close.png) no-repeat; display:inline-block; position:absolute; top:-14px; right:-12px; text-indent:-9999px; _font-size:0; _text-indent:0px; +font-size:0; +text-indent:0px;}
        .title1{ text-align:center; font-size:27px; color:#ffc286; padding:90px 0 30px; }
        .pop1box{ text-align:center;}
        .btn-confirm{background:url(../images/btn3.jpg) no-repeat; display:inline-block; text-indent:-9999px;_font-size:0; _text-indent:0px; +font-size:0; +text-indent:0px;width:95px; height:41px; background-position:0 0;position: absolute;left: 178px;bottom:42px; }
    </style>
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        $(function () {
            //签到
            $(".btn-test").on("click", function () {
                //假设ajax请求成功,并返回如下json；
                var json = eval('[{"Code":200,"State":"Success","Message":"签到成功"}]');
                ShowMsg(json[0].Message);
            });

            //关闭
            $(".btn-confirm,.close").on("click", function () {
                //关闭
                $(".shade_new").hide();
                //由于原生js的alert是阻塞线程的，所以此处做判断进行模拟阻塞。---仅仅是为了客户的一句话“他觉得。。。”
                if ($(".shade_new").css("display") == "none"&&$(".title1").html()=="签到成功") {
                    setTimeout("ShowMsg('第二次弹出')", 2000);
                }
            });
        });
        //显示弹窗
        function ShowMsg(msg) {
            $(".title1").html(msg);
            $(".shade_new").show();
        }

        //显示弹窗
        function ShowMyMsg() {
            $(".title1").html("测试1");
            $(".shade_new").show();
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="container">
        <input type="button" class="btn-test" value="签到" />
    </div>
    <div class="shade_new">
        <div class="pop1" id="pop1">
            <a href="javascript:void(0)" class="close">关闭</a>
            <p class="title1">
            </p>
            <p class="pop1box">
                <a href="javascript:void(0)" class="btn-confirm" ></a>
            </p>
        </div>
    </div>
    </div>

        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    </form>
</body>
</html>
