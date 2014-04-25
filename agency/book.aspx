<%@ Page Language="C#" AutoEventWireup="true" CodeFile="book.aspx.cs" Inherits="agency_book" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" type="text/css" href="http://www.supernoom.com/engineBooking2/css/redmond/jquery-ui-1.8.11.custom.css" media="screen" />
<script  src="../scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script  src="http://www.supernoom.com/engineBooking2/js/jquery-ui-1.8.11.custom.min.js" type="text/javascript"></script>
<script  src="/scripts/reservationEngine.js?ver=017" type="text/javascript"></script>
<link href="http://www.supernoom.com/engineBooking2/css/rateInfo.css" type="text/css" rel="stylesheet" />
<script  src="http://www.supernoom.com/engineBooking2/js/fancybox/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
<link rel="stylesheet" href="http://www.supernoom.com/engineBooking2/js/fancybox/jquery.fancybox-1.3.4.css" type="text/css" media="screen" />


<link href="http://www.hotels2thailand.com/theme_color/blue/style_rate.css" rel="stylesheet" type="text/css" />
<link href="http://www.hotels2thailand.com/theme_color/blue/style_detail.css" rel="stylesheet" type="text/css" />
<link href="http://www.hotels2thailand.com/theme_color/blue/layout.css?v=14082012" rel="stylesheet" type="text/css" />




    <style>
body{
	padding:0px;
	margin:0px auto;
	font-size:14px;
	font-family:Verdana, Geneva, sans-serif;
	background:#ffffff !important;
}

.fontWhite
{
	color:#FFFFFF;
}
.fontMedium{
	font-family:helvetica;
	font-size:32px;
}
#boxRate{ background-color:#ffffff; padding:10px;}
        #product_information {
            width:715px;
            padding:0px;
            margin:auto auto;
        }
        #product_rate {
            width:715px;
            padding:0px;
            margin:auto auto;
        }
        #divBoxSerch{
            width:715px;
            padding:0px;
            margin:auto auto;
        }
        .product_detail {
            background-color:#ffffff !important;
            margin:0px !important;
        }
        /*#dateci, #dateco{
            background:none !important;
        }*/
        table tr td {
            background-color:#ffffff !important;
        }
        .product_footer{
            width: 972px;
            margin:0 auto;
            padding:0px !important;
        }
        .content_tophotel_footer_b{
            margin-left:0px !important;
        }
        table#content_tophotel_footer_b{
            margin-left:0px !important;
        }
         table#content_tophotel_footer_b tr td{
            background-color:#fafafc !important;
        }
        .latest_reviews_more{
            margin:10px 0 10px 0px !important;
        }
        #imgFloat
        { 
            position:absolute;
 
        }
        .divBoxSerch_item{
            float:left;
        }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Main">
        <div id="product_information"></div>
        <div id="divBoxSerch">
          
          <div id="checkin_pan" class="divBoxSerch_item"> 
              <span id="chkin_wording">Check in</span>  
              <input  name="dateci" id="dateci" size="20" autocomplete="off" value="" readonly="readonly" rel="datepicker"  type="text"/>

          </div> 
          <div id="checkout_pan" class="divBoxSerch_item"> 
              <span id="chkout_wording">Check out</span>   
              <input  name="dateco" id="dateco" size="20" autocomplete="off" value="" readonly="readonly"  type="text"/>
           </div> 
            <div class="divBoxSerch_item"><input type="button" name="btnCheck" id="btnCheck" value="Check"/></div>
           <%-- <input type="hidden" name="pid" id="pid" value="3605" />--%>
            <input type="hidden" name="rateExchange" id="rateExchange" value="25" />
            
            <asp:HiddenField ID="hd_isSingle" Value="False" runat="server" ClientIDMode="Static" />
           
        </div>
        <div id="product_rate">

            <table id="roomRatePanel" width="100%">
	        <tr>
            <td valign="top"><div class="b2hRateResult"></div></td>
            </tr>
        </table>
        </div>
        <div id="product_footer1" class="product_footer"></div>
        <div id="product_footer2" class="product_footer"></div>
        <div id="product_footer3" class="product_footer"></div>
        <div id="product_footer4" class="product_footer"></div>
        <div id="product_review" class="product_footer"></div>
        <asp:Label ID="lbltext" runat="server" ClientIDMode="Static" ></asp:Label>
        <%--<asp:Literal ID="lbltext" runat="server" ></asp:Literal>--%>


    </div>
    </form>
    
</body>
</html>
