<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="User_left" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
    <title>�ޱ����ĵ�</title>
    <script language="JavaScript" src="/Admin/js/jquery.js"></script>
    <link href="/Admin/css/style.css" rel="stylesheet" type="text/css" />


<script type="text/javascript">
$(function(){	
	//�����л�
	$(".menuson li").click(function(){
		$(".menuson li.active").removeClass("active")
		$(this).addClass("active");
	});
	
	$('.title').click(function(){
		var $ul = $(this).next('ul');
		$('dd').find('ul').slideUp();
		if($ul.is(':visible')){
			$(this).next('ul').slideUp();
		}else{
			$(this).next('ul').slideDown();
		}
	});
})	
</script>

</head>
<body style="background:#f0f9fd;">
   
    <form id="form1" runat="server">

    <div class="lefttop"><span></span>���˹�������</div>
    
    <dl class="leftmenu">
        

          
    
    <asp:PlaceHolder ID="ph_UserLeftMenu" runat="server"></asp:PlaceHolder>
    
        <dd><div class="title"><div style="background:url(images/leftico01.png) no-repeat left center; padding-left:25px;margin-left:10px;"><asp:LinkButton ID="lbtn_Exit" runat="server" OnClick="lbtn_Exit_Click" >�˳�</asp:LinkButton></div></div></dd>

    </dl>

     
    </form>
</body>
</html>
