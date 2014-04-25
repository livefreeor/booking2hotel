<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<title>Affilate Rate</title>
<link href="/theme_color/blue/style_rate_aff.css" rel="stylesheet" type="text/css" />


<link rel="stylesheet" type="text/css" href="/js/fancybox/jquery.fancybox-1.3.4.css" media="screen" />

<!--Box Search Control Script-->
<link rel="stylesheet" type="text/css" href="/css/redmond/jquery-ui-1.8.11.custom.css">
<script language="javascript" src="/js/jquery-1.5.1.min.js"></script>
<script type="text/javascript" src="/js/jquery-ui-1.8.11.custom.min.js"></script>   

<script type="text/javascript" src="/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
<script type="text/javascript" src="/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
<SCRIPT type=text/javascript src="/js/visa.ui-2.js"></SCRIPT>
<script type="text/javascript" src="/js/front.ui.rate_affiliate.js"></script> 
<style>
#imgFloat{
	position:absolute;
	border:1px solid #5fa1c2;
	background-color:#5fa1c2;
	padding:2px;
	color:#333;
}
.preload{ background: url(/images/loader-img.gif) no-repeat 50% 50%; }
.chack_rate{

}

</style>
<script language="javascript">
    $().ready(function () {
        CreateCalendar();
        tooltip();
        imgFloat();
        DisplayRate();

    });
</script>
</head>
<body>
<table cellpadding="10" cellspacing="0" id="ht2th_box_search" width="700">
<tr><td><strong>Check in</strong></td><td><input type="text" rel="datepicker" id="dateStart2ci" /></td>
<td><strong>Check out</strong></td><td><input type="text" id="dateStart2co" /></div></td>
<td>
<input type="hidden" id="productDefault" value="<%=request.QueryString("pid")%>" />
<input type="hidden" id="category" value="<%=request.QueryString("cat_id")%>" />
<input type="button" id="btnCheckRate" value="check rate" onClick="checkRate(<%=request.QueryString("pid")%>)" />
</td></tr>
</table>





<br style="clear:both" />

<table width="700">
<tr><td height="100%" id="RoomPeriod"></td></tr>
</table>

</body>
</html>