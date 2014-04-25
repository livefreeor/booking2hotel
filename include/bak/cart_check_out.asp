<%OPTION EXPLICIT%>
<!--#include virtual="/include/constant.asp"-->
<!--#include virtual="/include/function_generate_hotel_link.asp"-->
<!--#include virtual="/include/function_set_session_date.asp"-->
<!--#include virtual="/include/function_date_sql.asp"-->
<!--#include virtual="/include/function_gen_box_sql.asp"-->
<!--#include virtual="/include/function_date.asp"-->
<!--#include virtual="/include/function_gen_dropdown_time.asp"-->
<!--#include virtual="/include/function_gen_dropdown_date.asp"-->
<!--#include virtual="/include/function_display_error.asp"-->
<!--#include virtual="/include/function_gen_room_require.asp"-->
<!--#include virtual="/include/function_check_airport.asp"-->
<!--#include virtual="/include/function_display_rate_confirm.asp"-->
<!--#include virtual="/include/function_gen_option_title.asp"-->
<!--#include virtual="/include/function_display_price.asp"-->
<!--#include virtual="/include/function_display_bol.asp"-->
<!--#include virtual="/include/function_gen_room_price_average_promotion.asp"-->
<!--#include virtual="/include/function_gen_room_price_average.asp"-->
<!--#include virtual="/include/function_gen_room_price.asp"-->
<!--#include virtual="/include/function_get_price_promotion.asp"-->
<!--#include virtual="/include/function_array_check.asp"-->
<!--#include virtual="/include/functoin_date_check.asp"-->
<!--#include virtual="/include/function_display_price.asp"-->
<!--#include virtual="/include/function_check_airport.asp"-->
<!--#include virtual="/include/function_get_promotion_id.asp"-->
<!--#include virtual="/include/function_gen_price_sightseeing.asp"-->
<!--#include virtual="/include/function_generate_sightseeing_link.asp"-->
<!--#include virtual="/include/function_gen_sightseeing_pickup.asp"-->
<!--#include virtual="/include/function_cart_get_id.asp"-->
<!--#include virtual="/include/function_gen_payment.asp"-->
<!--#include virtual="/include/function_birthday_input.asp"-->
<!--#include virtual="/include/sub_display_logo.asp"-->
<%
Call connOpen()

Dim strQueryFull
Dim sqlCartProduct
Dim sqlCartItem
Dim recCartProduct
Dim recCartItem
Dim arrCartProduct
Dim arrCartItem
Dim intCartID
Dim intCountProduct
Dim intCountItem
Dim intCountOption
Dim intOptionNumber
Dim intDayArrival
Dim intMonthArrival
Dim intYearArrival
Dim intDayDepart
Dim intMonthDepart
Dim intYearDepart
Dim intNight
Dim sqlRate
Dim recRate
Dim arrRate
Dim sqlPromotion
Dim recPromotion
Dim arrPromotion
Dim bolPromotion
Dim strPromotion
Dim intCountItemPrice
Dim intAvgRoomRate
Dim intPriceSubTotal
Dim strOtherPriceTotal
Dim intPriceTotal
Dim intPriceTotalAll
Dim strNight
Dim bolAirportDisplay
Dim intPriceSubTotalOwn
Dim intAvgRoomRateOwn
Dim strHiddenPriceOwn
Dim strHiddenPrice
Dim strHiddenPriceDisplay
Dim strHiddenPromotion
Dim intCountAdult
Dim intCountChild
Dim strPickup
Dim intCountGuestAll
Dim intCountGuestAdult
Dim intCountGuestChildren

intCartID = function_cart_get_id()
'response.write intCartID
sqlCartProduct = "SELECT cp.cart_product_id,cp.product_id,cp.product_cat_id,cp.time_in,cp.time_out,cp.num_adult,cp.num_children,cp.num_golfer,p.title_en,p.product_code,p.destination_id,p.files_name,p.address_en,p.star"
sqlCartProduct = sqlCartProduct & " FROM tbl_cart_product cp, tbl_product p"
sqlCartProduct = sqlCartProduct & " WHERE p.product_id=cp.product_id AND cart_id=" & intCartID
sqlCartProduct = sqlCartProduct & " ORDER BY cp.cart_product_id ASC"
Set recCartProduct = Server.CreateObject ("ADODB.Recordset")
recCartProduct.Open sqlCartProduct, Conn,adOpenStatic,adLockreadOnly
		arrCartProduct = recCartProduct.GetRows()
recCartProduct.Close
Set recCartProduct = Nothing 

'### Manage Flight Date ###
IF Session("a_date")<>"" Then
	intDayArrival = Session("a_date")
	intMonthArrival = Session("a_month")
	intYearArrival =Session("a_year")
	intDayDepart = Session("d_date")
	intMonthDepart = Session("d_month")
	intYearDepart =Session("d_year")
Else
	intDayArrival = Day(arrCartProduct(3,0))
	intMonthArrival = Month(arrCartProduct(3,0))
	intYearArrival = Year(arrCartProduct(3,0))
	intDayDepart = Day(arrCartProduct(4,0))
	intMonthDepart = Month(arrCartProduct(4,0))
	intYearDepart = Year(arrCartProduct(4,0))
End IF
'### Manage Flight Date ###

IF session("phone_type")="" Then
	session("phone_type")=1
End IF
%>
<html><!-- InstanceBegin template="/Templates/hotel-booking.dwt" codeOutsideHTMLIsLocked="false" -->
<head>
<!-- InstanceBeginEditable name="Hotel Title" -->
<title>Complete Your Reservation With 4 Steps</title>
<!-- InstanceEndEditable -->
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="css/css.css" type="text/css">
<link rel="stylesheet" href="css/optimize.css" type="text/css">
<script language="JavaScript" src="/java/popup.js" type="text/javascript"></script>
</head>
<body background="/images/bg_main.jpg" bgcolor="#FFFFFF" marginheight="0" leftmargin="0" topmargin="10" marginwidth="0" >
<table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" align="center">
  <tr>
    <td>
<%Call sub_display_logo(Request.Cookies("site_id"),"","",10)%>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11">
        <tr> 
          <td height="24" background="/images/bg_bar.gif" align="center">
		  <a href="/hotels-help-why-choose.asp"><u>Why choose Hotels2Thailand.com?</u></a>
		  <font color="346494"> | </font>
		  <a href="/hotels-help-why-low.asp"><u>Why our prices are low?</u></a>
		  <font color="346494"> | </font>
		  <a href="/thailand-hotels-testimonial.asp"><u>User Testimonial</u></a>
		  <font color="346494"> | </font>
		  <a href="/thailand-hotels-faq.asp"><u>FAQ</u></a>
		  <font color="346494"> | </font>
		  <a href="/thailand-hotels-contact.asp"><u>Contact us</u></a>
		  </td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" bgcolor="#FFFFFF">
        <tr> 
          <td><!-- InstanceBeginEditable name="Booking Head" --><br /><br />&nbsp;<img src="/images/hd_reservation.gif" alt=""> <br /><br /><!-- InstanceEndEditable --></td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" bgcolor="#FFFFFF">
        <tr> 
          <td><!-- InstanceBeginEditable name="Body" -->
		  <table width="100%" cellpadding="2" cellspacing="1" bgcolor="#C4DBFF">
		  <form action="/cart_process.asp" method="post">
  <tr>
    <td bgcolor="#FFFFFF"><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td><p><span class="l2"><font color="#003366">Reservation Information</font></span><BR>
          &nbsp; <BR>
          Please complete the   form below to enable us to process your reservation. We guarantee that we will   not sell, trade, or rent your personal information to 3rd parties. <A href="/thailand-hotels-privacy.asp" target="_blank">Security &amp; Privacy policy</A><BR>
          <BR>
          <strong>Fields marked with a red   asterisk</strong>(<strong><font color="#FF0000">*</font></strong>) <strong>are   required.</strong></p></td>
      </tr>
      <tr>
        <td><table width="98%" cellspacing="1" cellpadding="2" bgcolor="#FCB45C">
          <tr>
            <td align="left" bgcolor="#FFFAEF" class="f13"><strong>Step 1 : <font color="#fe5400">Review Your Detail</font></strong></td>
          </tr>
          <tr>
            <td align="center" bgcolor="#FFFFFF">
			
<%
For intCountProduct=0 To Ubound(arrCartProduct,2)
		
		IF NOT bolAirportDisplay=TRUE Then
			bolAirportDisplay = function_check_airport(arrCartProduct(1,intCountProduct),arrCartProduct(2,intCountProduct),Day(arrCartProduct(3,intCountProduct)),Month(arrCartProduct(3,intCountProduct)),Year(arrCartProduct(3,intCountProduct)),Day(arrCartProduct(4,intCountProduct)),Month(arrCartProduct(4,intCountProduct)),Year(arrCartProduct(4,intCountProduct)),1)
		End IF
		
		intOptionNumber = 0
		intNight = DateDiff("d",arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct))
		
		sqlRate = "SELECT op.option_id,op.date_start,op.date_end,op.price,op.price_rack,price_own,sup_weekend,sup_holiday,sup_long"
		sqlRate = sqlRate & " FROM tbl_product_option po,tbl_option_price op"
		sqlRate = sqlRate & " WHERE po.option_id=op.option_id AND po.status=1  AND ((op.date_start<="&function_date_sql(Day(arrCartProduct(3,intCountProduct)),Month(arrCartProduct(3,intCountProduct)),Year(arrCartProduct(3,intCountProduct)),1)&" AND op.date_end>="&function_date_sql(Day(arrCartProduct(3,intCountProduct)),Month(arrCartProduct(3,intCountProduct)),Year(arrCartProduct(3,intCountProduct)),1)&") OR (op.date_start<="&function_date_sql(Day(arrCartProduct(4,intCountProduct)),Month(arrCartProduct(4,intCountProduct)),Year(arrCartProduct(4,intCountProduct)),1)&" AND date_end>="&function_date_sql(Day(arrCartProduct(4,intCountProduct)),Month(arrCartProduct(4,intCountProduct)),Year(arrCartProduct(4,intCountProduct)),1)&")OR (op.date_start>="&function_date_sql(Day(arrCartProduct(3,intCountProduct)),Month(arrCartProduct(3,intCountProduct)),Year(arrCartProduct(3,intCountProduct)),1)&" AND op.date_end<="&function_date_sql(Day(arrCartProduct(4,intCountProduct)),Month(arrCartProduct(4,intCountProduct)),Year(arrCartProduct(4,intCountProduct)),1)&")) AND po.product_id=" & arrCartProduct(1,intCountProduct)
		
		Set recRate = Server.CreateObject ("ADODB.Recordset")
		recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
			arrRate = recRate.GetRows()
		recRate.Close
		Set recRate = Nothing 
	
		sqlPromotion = "SELECT promotion_id,pm.option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num,offer_id"
		sqlPromotion = sqlPromotion & " FROM tbl_promotion pm, tbl_product_option po"
		sqlPromotion = sqlPromotion & " WHERE po.option_id=pm.option_id AND (date_start<="&function_date_sql(Day(arrCartProduct(3,intCountProduct)),Month(arrCartProduct(3,intCountProduct)),Year(arrCartProduct(3,intCountProduct)),1)&" AND date_end>="&function_date_sql(Day(arrCartProduct(4,intCountProduct)),Month(arrCartProduct(4,intCountProduct)),Year(arrCartProduct(4,intCountProduct)),1)&") AND pm.status=1 AND day_min<="& intNight &" AND po.product_id=" & arrCartProduct(1,intCountProduct)
		sqlPromotion = sqlPromotion & " ORDER BY po.option_id ASC, day_min DESC"

		Set recPromotion  = Server.CreateObject ("ADODB.Recordset")
		recPromotion.Open SqlPromotion, Conn,adOpenStatic,adLockreadOnly
			IF NOT recPromotion.EOF Then
				arrPromotion = recPromotion.GetRows()
				bolPromotion = True
			Else
				bolPromotion = False
			End IF
		recPromotion.Close
		Set recPromotion = Nothing 
		
		sqlCartItem = "SELECT ci.cart_item_id,ci.cart_product_id,ci.option_id,ci.quantity,po.option_cat_id,po.title_en,po.max_adult,ci.detail"
		sqlCartItem = sqlCartItem & " FROM tbl_cart_item ci, tbl_product_option po"
		sqlCartItem = sqlCartItem & " WHERE po.option_id=ci.option_id AND ci.cart_product_id=" & arrCartProduct(0,intCountProduct)
		sqlCartItem = sqlCartItem & " ORDER BY po.option_cat_id ASC"
		Set recCartItem = Server.CreateObject ("ADODB.Recordset")
		recCartItem.Open sqlCartItem, Conn,adOpenStatic,adLockreadOnly
				arrCartItem = recCartItem.GetRows()
		recCartItem.Close
		Set recCartItem = Nothing

	SELECT CASE Cint(arrCartProduct(2,intCountProduct))
		Case 29 ' ### Hotels ###
%>
			
			<p>
			<table width="85%" cellpadding="2" cellspacing="0" bgcolor="#FBFBFB">
              <tr>
                <td class="l2"><font color="#990066">Product #<%=intCountProduct+1%> </font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Hotel:</font></STRONG> <font color="green"><%=arrCartProduct(8,intCountProduct)%></font></td>
                </tr>
              <tr>
                <td><STRONG><font color="346494">Period:</font></STRONG> <font color="green"><%=function_date(arrCartProduct(3,intCountProduct),5)%> - <%=function_date(arrCartProduct(4,intCountProduct),5)%></font></td>
                </tr>
              <tr>
                <td><STRONG><font color="346494">Adult(s):</font></STRONG> <font color="green"><%=arrCartProduct(5,intCountProduct)%></font>   , <STRONG><font color="346494">Children:</font></STRONG> <font color="green"><%=arrCartProduct(6,intCountProduct)%></font></td>
                </tr>
				
              <tr>
                <td><STRONG><font color="346494">Room(s):</font></STRONG></td>
                </tr>
              <tr>
                <td align="center">
				
				
				<table width="90%" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
                  <tr>
                    <td align="center" bgcolor="#FFEAFF"><font color="#990066">Option</font></td>
                    <td align="center" bgcolor="#FFEAFF"><font color="#990066">Quantity</font></td>
                    <td align="center" bgcolor="#FFEAFF"><font color="#990066">Night(s)</font></td>
                    <td align="center" bgcolor="#FFEAFF"><font color="#990066">Avg. Rate</font></td>
                    <td align="center" bgcolor="#FFEAFF"><font color="#990066">Subtotal</font></td>
                  </tr>
<%
intPriceTotal = 0
For intCountItem=0 To Ubound(arrCartItem,2)
	IF bolPromotion Then
		intAvgRoomRate = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,3)
	Else
		intAvgRoomRate = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,3)
	End IF

	IF Cint(arrCartItem(4,intCountItem))=38 OR Cint(arrCartItem(4,intCountItem))=39 Then
		strNight = intNight
		intPriceSubTotal = intAvgRoomRate*intNight*arrCartItem(3,intCountItem)
		intPriceSubTotalOwn = intAvgRoomRateOwn*intNight*arrCartItem(3,intCountItem)
	ElseIF Cint(arrCartItem(4,intCountItem))=47 Then
		strNight = "-"
		intPriceSubTotal = intAvgRoomRate*arrCartItem(3,intCountItem)
		intPriceSubTotalOwn = intAvgRoomRateOwn*arrCartItem(3,intCountItem)
	End IF
	
	intPriceTotal = intPriceTotal + intPriceSubTotal
	
	strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
	strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	
	IF bolPromotion Then
		strHiddenPromotion = strHiddenPromotion & "<input type=""hidden"" name=""promotion"&arrCartItem(0,intCountItem)&""" value="""&function_get_promotion_id(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrCartItem(3,intCountItem),arrPromotion,1)&""">" & VbCrlf
	End IF
%>
                  <tr>
                    <td bgcolor="#FFFFFF"><%=arrCartItem(5,intCountItem)%></td>
                    <td align="center" bgcolor="#FFFFFF"><%=arrCartItem(3,intCountItem)%></td>
                    <td align="center" bgcolor="#FFFFFF"><%=strNight%></td>
                    <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intAvgRoomRate,3)%></td>
                    <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceSubTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                  </tr>
<%
Next
intPriceTotalAll = intPriceTotalAll + intPriceTotal
%>
                  <tr>
                    <td colspan="4" align="right" bgcolor="#FFFFFF"><strong><font color="#FF6600">Total Price:</font></strong></td>
                    <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                  </tr>
                </table></td>
              </tr>
            </table>
			</p>
<%
		Case 31 '### Transfer ###
%>
			<p>
			<table width="85%" cellpadding="2" cellspacing="0" bgcolor="#FBFBFB">
              <tr>
                <td class="l2"><font color="#990066">Product #<%=intCountProduct+1%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Transfer:</font></STRONG> <font color="green"><%=arrCartProduct(8,intCountProduct)%></font></td>
              </tr>

              <tr>
                <td><STRONG><font color="346494">Adult(s):</font></STRONG> <font color="green"><%=arrCartProduct(5,intCountProduct)%></font> , <STRONG><font color="346494">Children:</font></STRONG> <font color="green"><%=arrCartProduct(6,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Transfer Detail:</font></STRONG></td>
              </tr>
              <tr>
                <td align="center"><table width="90%" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
                    <tr>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Transfer</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Quantity</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Avg. Rate</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Subtotal</font></td>
                    </tr>
<%
intPriceTotal = 0
For intCountItem=0 To Ubound(arrCartItem,2)
	IF bolPromotion Then
		intAvgRoomRate = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,3)
	Else
		intAvgRoomRate = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,3)
	End IF

	intPriceSubTotal = intAvgRoomRate*arrCartItem(3,intCountItem)
	intPriceSubTotalOwn = intAvgRoomRateOwn*arrCartItem(3,intCountItem)
	intPriceTotal = intPriceTotal + intPriceSubTotal
	
	strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
	strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
%>
                    <tr>
                      <td bgcolor="#FFFFFF"><%=arrCartItem(5,intCountItem)%><br />(<%=arrCartItem(7,intCountItem)%>)</td>
                      <td align="center" bgcolor="#FFFFFF"><%=arrCartItem(3,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intAvgRoomRate,3)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceSubTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                   </tr>
<%
Next
intPriceTotalAll = intPriceTotalAll + intPriceTotal
%>
                    <tr>
                      <td colspan="3" align="right" bgcolor="#FFFFFF"><strong><font color="#FF6600">Total Price:</font></strong></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
                </table></td>
              </tr>
            </table>
			</p>
<%
		Case 32 ' ### Green Fee ###
%>
			<p>
			<table width="85%" cellpadding="2" cellspacing="0" bgcolor="#FBFBFB">
              <tr>
                <td class="l2"><font color="#990066">Product #<%=intCountProduct+1%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Golf Course :</font></STRONG> <font color="green"><%=arrCartProduct(8,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Tee-Off Date and Time :</font></STRONG> <font color="green"><%=function_date(arrCartProduct(3,intCountProduct),4)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Golfer(s):</font></STRONG><font color="green"> <%=arrCartProduct(7,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Course Detail:</font></STRONG></td>
              </tr>
              <tr>
                <td align="center"><table width="90%" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
                    <tr>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Course </font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Quantity</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Avg. Rate</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Subtotal</font></td>
                    </tr>
<%
intPriceTotal = 0
For intCountItem=0 To Ubound(arrCartItem,2)
	IF bolPromotion Then
		intAvgRoomRate = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,3)
	Else
		intAvgRoomRate = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,3)
	End IF
	
	intPriceSubTotal = intAvgRoomRate*arrCartItem(3,intCountItem)
	intPriceSubTotalOwn = intAvgRoomRateOwn*arrCartItem(3,intCountItem)
	intPriceTotal = intPriceTotal + intPriceSubTotal
	
	strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
	strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
%>
                    <tr>
                      <td bgcolor="#FFFFFF"><%=arrCartItem(5,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=arrCartItem(3,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intAvgRoomRate,3)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceSubTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
<%
Next
intPriceTotalAll = intPriceTotalAll + intPriceTotal
%>
                    <tr>
                      <td colspan="3" align="right" bgcolor="#FFFFFF"><strong><font color="#FF6600">Total Price:</font></strong></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
                </table></td>
              </tr>
            </table>
			</p>

<%
	Case 34 '### SightSeeing ###
%>
			<p>
			<table width="85%" cellpadding="2" cellspacing="0" bgcolor="#FBFBFB">
              <tr>
                <td class="l2"><font color="#990066">Product #<%=intCountProduct+1%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Trip :</font></STRONG> <font color="green"><%=arrCartProduct(8,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Trip Date :</font></STRONG> <font color="green"><%=function_date(arrCartProduct(3,intCountProduct),5)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Adult(s):</font></STRONG> <font color="green"><%=arrCartProduct(5,intCountProduct)%></font>   , <STRONG><font color="346494">Children:</font></STRONG> <font color="green"><%=arrCartProduct(6,intCountProduct)%></font></td>
                </tr>
              <tr>
                <td><STRONG><font color="346494">Trip Detail:</font></STRONG></td>
              </tr>
              <tr>
                <td align="center"><table width="90%" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
                    <tr>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Trip</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Quantity</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Subtotal</font></td>
                    </tr>
<%
intPriceTotal = 0
For intCountItem=0 To Ubound(arrCartItem,2)

	intPriceSubTotal = function_gen_price_sightseeing(arrCartProduct(3,intCountProduct),arrCartItem(2,intCountItem),arrCartProduct(5,intCountProduct),arrCartProduct(6,intCountProduct),1)
	intPriceSubTotalOwn = function_gen_price_sightseeing(arrCartProduct(3,intCountProduct),arrCartItem(2,intCountItem),arrCartProduct(5,intCountProduct),arrCartProduct(6,intCountProduct),2)
	intPriceTotal = intPriceTotal + intPriceSubTotal
	intAvgRoomRate = 0
	strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
	strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
%>
                    <tr>
                      <td bgcolor="#FFFFFF"><%=arrCartItem(5,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF">Adult: <%=arrCartProduct(5,intCountProduct)%> Children: <%=arrCartProduct(6,intCountProduct)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceSubTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
<%
Next
intPriceTotalAll = intPriceTotalAll + intPriceTotal
%>
                    <tr>
                      <td colspan="2" align="right" bgcolor="#FFFFFF"><strong><font color="#FF6600">Total Price:</font></strong></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
                </table></td>
              </tr>
            </table>
			</p>
<%
	Case 36 'Water Activity
%>
			<p>
			<table width="85%" cellpadding="2" cellspacing="0" bgcolor="#FBFBFB">
              <tr>
                <td class="l2"><font color="#990066">Product #<%=intCountProduct+1%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Trip :</font></STRONG> <font color="green"><%=arrCartProduct(8,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Trip Date :</font></STRONG> <font color="green"><%=function_date(arrCartProduct(3,intCountProduct),5)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Adult(s):</font></STRONG> <font color="green"><%=arrCartProduct(5,intCountProduct)%></font>   , <STRONG><font color="346494">Children:</font></STRONG> <font color="green"><%=arrCartProduct(6,intCountProduct)%></font></td>
                </tr>
              <tr>
                <td><STRONG><font color="346494">Trip Detail:</font></STRONG></td>
              </tr>
              <tr>
                <td align="center"><table width="90%" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
                    <tr>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Trip</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Quantity</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Subtotal</font></td>
                    </tr>
<%
intPriceTotal = 0
For intCountItem=0 To Ubound(arrCartItem,2)

	intPriceSubTotal = function_gen_price_sightseeing(arrCartProduct(3,intCountProduct),arrCartItem(2,intCountItem),arrCartProduct(5,intCountProduct),arrCartProduct(6,intCountProduct),1)
	intPriceSubTotalOwn = function_gen_price_sightseeing(arrCartProduct(3,intCountProduct),arrCartItem(2,intCountItem),arrCartProduct(5,intCountProduct),arrCartProduct(6,intCountProduct),2)
	intPriceTotal = intPriceTotal + intPriceSubTotal
	intAvgRoomRate = 0
	strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
	strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
%>
                    <tr>
                      <td bgcolor="#FFFFFF"><%=arrCartItem(5,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF">Adult: <%=arrCartProduct(5,intCountProduct)%> Children: <%=arrCartProduct(6,intCountProduct)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceSubTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
<%
Next
intPriceTotalAll = intPriceTotalAll + intPriceTotal
%>
                    <tr>
                      <td colspan="2" align="right" bgcolor="#FFFFFF"><strong><font color="#FF6600">Total Price:</font></strong></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
                </table></td>
              </tr>
            </table>
			</p>
<%
	Case 38 '### Shows & Events ###
%>
			<p>
			<table width="85%" cellpadding="2" cellspacing="0" bgcolor="#FBFBFB">
              <tr>
                <td class="l2"><font color="#990066">Product #<%=intCountProduct+1%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Show or Event :</font></STRONG> <font color="green"><%=arrCartProduct(8,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Show or Event Date :</font></STRONG> <font color="green"><%=function_date(arrCartProduct(3,intCountProduct),5)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Adult(s):</font></STRONG> <font color="green"><%=arrCartProduct(5,intCountProduct)%></font>   , <STRONG><font color="346494">Children:</font></STRONG> <font color="green"><%=arrCartProduct(6,intCountProduct)%></font></td>
                </tr>
              <tr>
                <td><STRONG><font color="346494">Show or Event Detail:</font></STRONG></td>
              </tr>
              <tr>
                <td align="center"><table width="90%" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
                    <tr>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Show or Event</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Quantity</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Subtotal</font></td>
                    </tr>
<%
intPriceTotal = 0
For intCountItem=0 To Ubound(arrCartItem,2)

	intPriceSubTotal = function_gen_price_sightseeing(arrCartProduct(3,intCountProduct),arrCartItem(2,intCountItem),arrCartProduct(5,intCountProduct),arrCartProduct(6,intCountProduct),1)
	intPriceSubTotalOwn = function_gen_price_sightseeing(arrCartProduct(3,intCountProduct),arrCartItem(2,intCountItem),arrCartProduct(5,intCountProduct),arrCartProduct(6,intCountProduct),2)
	intPriceTotal = intPriceTotal + intPriceSubTotal
	intAvgRoomRate = 0
	strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
	strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
%>
                    <tr>
                      <td bgcolor="#FFFFFF"><%=arrCartItem(5,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF">Adult: <%=arrCartProduct(5,intCountProduct)%> Children: <%=arrCartProduct(6,intCountProduct)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceSubTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
<%
Next
intPriceTotalAll = intPriceTotalAll + intPriceTotal
%>
                    <tr>
                      <td colspan="2" align="right" bgcolor="#FFFFFF"><strong><font color="#FF6600">Total Price:</font></strong></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
                </table></td>
              </tr>
            </table>
			</p>
<%
		Case 39 ' ### Health Check Up ###
%>
			<p>
			<table width="85%" cellpadding="2" cellspacing="0" bgcolor="#FBFBFB">
              <tr>
                <td class="l2"><font color="#990066">Product #<%=intCountProduct+1%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Hospital :</font></STRONG> <font color="green"><%=arrCartProduct(8,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Date and Time :</font></STRONG> <font color="green"><%=function_date(arrCartProduct(3,intCountProduct),4)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Quantity:</font></STRONG><font color="green"> <%=arrCartProduct(7,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Course Detail:</font></STRONG></td>
              </tr>
              <tr>
                <td align="center"><table width="90%" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
                    <tr>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Course </font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Quantity</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Avg. Rate</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Subtotal</font></td>
                    </tr>
<%
intPriceTotal = 0
For intCountItem=0 To Ubound(arrCartItem,2)
	IF bolPromotion Then
		intAvgRoomRate = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,3)
	Else
		intAvgRoomRate = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,3)
	End IF
	
	intPriceSubTotal = intAvgRoomRate*arrCartItem(3,intCountItem)
	intPriceSubTotalOwn = intAvgRoomRateOwn*arrCartItem(3,intCountItem)
	intPriceTotal = intPriceTotal + intPriceSubTotal
	
	strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
	strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
%>
                    <tr>
                      <td bgcolor="#FFFFFF"><%=arrCartItem(5,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=arrCartItem(3,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intAvgRoomRate,3)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceSubTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
<%
Next
intPriceTotalAll = intPriceTotalAll + intPriceTotal
%>
                    <tr>
                      <td colspan="3" align="right" bgcolor="#FFFFFF"><strong><font color="#FF6600">Total Price:</font></strong></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
                </table></td>
              </tr>
            </table>
			</p>
<%
		Case 40 ' ### Spa ###
%>
			<p>
			<table width="85%" cellpadding="2" cellspacing="0" bgcolor="#FBFBFB">
              <tr>
                <td class="l2"><font color="#990066">Product #<%=intCountProduct+1%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Hospital :</font></STRONG> <font color="green"><%=arrCartProduct(8,intCountProduct)%></font></td>
              </tr>
              <tr>
                <td><STRONG><font color="346494">Date and Time :</font></STRONG> <font color="green"><%=function_date(arrCartProduct(3,intCountProduct),4)%></font></td>
              </tr>
              <tr>
                <td align="center"><table width="90%" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
                    <tr>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Course </font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Quantity</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Avg. Rate</font></td>
                      <td align="center" bgcolor="#FFEAFF"><font color="#990066">Subtotal</font></td>
                    </tr>
<%
intPriceTotal = 0
For intCountItem=0 To Ubound(arrCartItem,2)
	IF bolPromotion Then
		intAvgRoomRate = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),1,arrPromotion,arrRate,3)
	Else
		intAvgRoomRate = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average(arrCartItem(2,intCountItem),arrCartProduct(3,intCountProduct),arrCartProduct(4,intCountProduct),arrRate,3)
	End IF
	
	intPriceSubTotal = intAvgRoomRate*arrCartItem(3,intCountItem)
	intPriceSubTotalOwn = intAvgRoomRateOwn*arrCartItem(3,intCountItem)
	intPriceTotal = intPriceTotal + intPriceSubTotal
	
	strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
	strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
	strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrCartItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
%>
                    <tr>
                      <td bgcolor="#FFFFFF"><%=arrCartItem(5,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=arrCartItem(3,intCountItem)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intAvgRoomRate,3)%></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceSubTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
<%
Next
intPriceTotalAll = intPriceTotalAll + intPriceTotal
%>
                    <tr>
                      <td colspan="3" align="right" bgcolor="#FFFFFF"><strong><font color="#FF6600">Total Price:</font></strong></td>
                      <td align="center" bgcolor="#FFFFFF"><%=function_display_price(intPriceTotal,3)%>&nbsp;<%=ConstCurrencyDisplay%></td>
                    </tr>
                </table></td>
              </tr>
            </table>
			</p>
<%
	Case 999 '### Temp ###
	END SELECT
Next

IF Cstr(Session("currency_code"))<>"THB" AND Cstr(Session("currency_code"))<>"" Then		
	strOtherPriceTotal = "(<em><font color=""#662C55"">"&FormatNumber(intPriceTotalAll/Session("currency_prefix"),0) & " " & Session("currency_title")&"</font></em>)"
End IF
%>
            <table width="90%" cellpadding="2"  cellspacing="1" bgcolor="#FF6600">
              <tr>
                <td align="center" bgcolor="#FFFFFF"><b><font color="#990066">Total Price</font></b> : <b><font color="#009900"><%=FormatNumber(intPriceTotalAll,0)%>&nbsp;<%=ConstCurrencyDisplay%>&nbsp; <%=strOtherPriceTotal %> </font></b></td>
              </tr>
          </table>
		  <p>If this information is not correct, Click <a href="/cart_view.asp">Back</a> to Edit your Booking Detail</p>

			</td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td><table width="100%" cellpadding="2" cellspacing="1" bgcolor="#FCB45C">
          <tr>
            <td bgcolor="#FFFAEF"><span class="l1">Step 2 : <font color="#fe5400">Your Information</font></span> (<em>Who is making this reservation?</em>) <font color="red">*Mandatory fields</font></td>
          </tr>
          <tr>
            <td align="center" bgcolor="#FFFFFF"><table width="85%" cellpadding="2"  cellspacing="0" bgcolor="#FBFBFB">
              <%IF Request("error")<>"" Then%>
              <tr align="left">
                <td colspan="2" align="center"><a name="error"></a><%=function_display_error_box (Request("error"),2)%></td>
              </tr>
              <%End IF%>
              <tr align="left">
                <td><font color="346494">Guest Name</font> <font color="red">*</font> </td>
                <td><input name="full_name" type="text" size="30" value="<%=Session("full_name")%>"></td>
              </tr>
              <tr align="left">
                <td><font color="346494">Email</font> <font color="red">*</font></td>
                <td><input name="email" type="text" id="email3" value="<%=Session("email")%>" size="30" maxlength="100"></td>
              </tr>
              <tr align="left">
                <td><font color="346494">Repeat Email</font> <font color="red">*</font></td>
                <td><input name="email2" type="text" id="email22" value="<%=Session("email2")%>" size="30" maxlength="100"></td>
              </tr>
              <tr align="left">
                <td><font color="346494">Address</font></td>
                <td><textarea name="address" cols="40" rows="5" id="textarea"><%=Session("address")%></textarea></td>
              </tr>
              <tr align="left">
                <td><font color="346494">Contact no. </font> <font color="red">*</font></td>
                <td><%=function_display_bol("Mobile","Phone",session("phone_type"),"phone_type",1)%>
                  <input name="phone" type="text" id="phone2" value="<%=Session("phone")%>" size="20"><br><font color="#FF0000">( We will use to contact in case of  emergency occurred.)</font></td>
              </tr>
              <tr align="left">
                <td><font color="346494">Country </font><font color="red">*</font></td>
                <td><%=function_gen_box_sql("SELECT country,title FROM tbl_country ORDER BY title ASC","country",Session("country"),1,4)%> </td>
              </tr>
				  <%IF bolAirportDisplay Then%>
                  <tr align="left" bgcolor="#FFFFFF">
                    <td bgcolor="#FFFFFF"><font color="346494">Flight Arrival Detail: </font></td>
                    <td><table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#C4DBFF">
                        <tr bgcolor="#FFFFFF">
                          <td width="100" bgcolor="#FFFFFF"><font color="1B56BC">Flight Number: </font></td>
                          <td><input type="text" name="a_flight" value="<%=Session("a_flight")%>"></td>
                        </tr>
                        <tr bgcolor="#FFFFFF">
                          <td width="100" bgcolor="#FFFFFF"><font color="#cc3300">Arrival Local Time (On Ticket)</font> </td>
                          <td>Date: <%=function_gen_dropdown_date(intDayArrival,intMonthArrival,intYearArrival,"a_date","a_month","a_year",1)%> <br><br><%=function_gen_dropdown_time("a_hour","a_min",Session("a_hour"),Session("a_min"),1)%></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr align="left" bgcolor="#FFFFFF">
                    <td bgcolor="#FFFFFF"><font color="346494">Flight Departure Detail: </font></td>
                    <td><table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#C4DBFF">
                        <tr>
                          <td width="100" bgcolor="#FFFFFF"><font color="1B56BC">Flight Number: </font></td>
                          <td bgcolor="#FFFFFF"><input type="text" name="d_flight" value="<%=Session("d_flight")%>"></td>
                        </tr>
                        <tr>
                          <td width="100" bgcolor="#FFFFFF"><font color="#cc3300">Departure Time</font></td>
                          <td bgcolor="#FFFFFF">Date: <%=function_gen_dropdown_date(intDayDepart,intMonthDepart,intYearDepart,"d_date","d_month","d_year",1)%> <br><br><%=function_gen_dropdown_time("d_hour","d_min",Session("d_hour"),Session("d_min"),1)%></td>
                        </tr>
                    </table></td>
                  </tr>
				  <%End IF%>
                  <tr align="left" bgcolor="#FFFFFF">
                    <td colspan="2" bgcolor="#FFFFFF"><input name="receive_mail" type="checkbox" value="yes" checked> 
                      <font color="346494">Receive information from hotels2thailand.com </font></td>
                    </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>
		<table width="100%" cellpadding="2" cellspacing="1" bgcolor="#FCB45C">
          <tr>
            <td bgcolor="#FFFAEF"><span class="l1">Step 3 : <font color="#fe5400">Guest Information & Preferences</font></span> (<em>Who is travelling?</em>) </td>
          </tr>
          <tr>
            <td align="center" bgcolor="#FFFFFF">
			<!--<p><br><table width="90%" cellpadding="5" cellspacing="1" bgcolor="#FFFFF4" style="border:1px solid #CA4200">
<tr><td><p><font color="#CA4200"><strong>IMPORTANT:</strong></font>   Please read for your benefit of Insurance Coverage.</p>
    <p>All Guest Names of your booking will be forwarded to Insurance Company.   Every person&rsquo;s guest names are required to declare in Guest Name List . This   insurance commences immediately upon your first date arrival in Thailand until departure date of this   booking basis.(<a href="/thailand-hotels-insurance.asp" target="_blank">Click here for read More Detail</a>). </p>
    <p>This insurance covers all customers who reserve all products and service   completely with Hotels2thailand.com <strong>ONLY</strong>.</p></td>
</tr>
</table>
</p>-->
<%
For intCountProduct=0 To Ubound(arrCartProduct,2)

	sqlCartItem = "SELECT ci.cart_item_id,ci.cart_product_id,ci.option_id,ci.quantity,po.option_cat_id,po.title_en,po.max_adult,ci.detail"
	sqlCartItem = sqlCartItem & " FROM tbl_cart_item ci, tbl_product_option po"
	sqlCartItem = sqlCartItem & " WHERE po.option_id=ci.option_id AND ci.cart_product_id=" & arrCartProduct(0,intCountProduct)
	sqlCartItem = sqlCartItem & " ORDER BY po.option_cat_id ASC"
	'response.write sqlCartItem&"<br>"
	Set recCartItem = Server.CreateObject ("ADODB.Recordset")
	recCartItem.Open sqlCartItem, Conn,adOpenStatic,adLockreadOnly
			arrCartItem = recCartItem.GetRows()
	recCartItem.Close
	Set recCartItem = Nothing

	IF arrCartProduct(2,intCountProduct)=29 Then '### Hotel ###
%>


			<table width="85%" cellspacing="1" cellpadding="2">
  <tr>
    <td><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="20%" align="left"><img src="thailand-hotels-pic/<%=arrCartProduct(9,intCountProduct)%>_b_1.jpg"></td>
        <td><table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td colspan="2"><strong><font color="#2C5266"><%=arrCartProduct(8,intCountProduct)%></font></strong><br>
              <%=arrCartProduct(12,intCountProduct)%>		  </td>
            </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><strong><font color="#66482C">Check in:</font></strong> <%=function_date(arrCartProduct(3,intCountProduct),5)%><br>
              <font color="#66482C"><strong>Check out:</strong></font> <%=function_date(arrCartProduct(4,intCountProduct),5)%></td>
            <td><strong><font color="#3D2E66">Adult:</font></strong> <%=arrCartProduct(5,intCountProduct)%><br>
              <font color="#3D2E66"><strong>Children:</strong></font> <%=arrCartProduct(6,intCountProduct)%></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>
	<table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FBFBFB">
  <tr>
    <td colspan="2"><strong>Full Guest Name List For <font color="003663"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
    </tr>
<%
intCountGuestAll = 1
For intCountGuestAdult=1 To arrCartProduct(5,intCountProduct)
%>
  <tr>
    <td><font color="346494">Guest <%=intCountGuestAll%> (Adult) </font></td>
    <td><input name="adult_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("adult_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestAdult)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
<%
For intCountGuestChildren=1 To arrCartProduct(6,intCountProduct)
%>
  <tr>
    <td><font color="346494">Guest <%=intCountGuestAll%> (Children) </font></td>
    <td><input name="children_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestChildren%>" type="text" size="50" value="<%=Session("children_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestChildren)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
</table>

	<br />
	<br />
<%
	For intCountItem=0 To Ubound(arrCartItem,2)
		intOptionNumber  = 0
		For intCountOption=1 To arrCartItem(3,intCountItem)
			IF Cstr(arrCartItem(4,intCountItem))= "38" Then 'Room
				intOptionNumber = intOptionNumber+1
%>
		  <table width="100%" cellpadding="2"  cellspacing="0" bgcolor="#FBFBFB">
            <tr align="left">
              <td colspan="2"><strong>Room <%=intOptionNumber%> # <font color="003663"><%=arrCartItem(5,intCountItem)%></font></strong></td>
              </tr>
            <tr align="left">
              <td><font color="346494">Room Requirement:</font><br>
                (<font color="red">subject to availability, can not guarantee</font>) </td>
              <td><%=function_gen_room_require(arrCartItem(0,intCountItem),intOptionNumber,Session("smoking_"&arrCartItem(0,intCountItem)&"_"&intOptionNumber),Session("bed_type_"&arrCartItem(0,intCountItem)&"_"&intOptionNumber),Session("floor_"&arrCartItem(0,intCountItem)&"_"&intOptionNumber),2)%></td>
            </tr>
            <tr align="left">
              <td><font color="346494">Special Request: </font></td>
              <td>
			  <textarea name="comment_<%=arrCartItem(0,intCountItem)%>_<%=intOptionNumber%>" cols="50" rows="5"><%=Session("comment_"&arrCartItem(0,intCountItem)&"_"&intOptionNumber)%></textarea><br>
			  <em>Hotels2Thailand.com will forward your requests to the property. These requests are not guaranteed.			  </em></td>
            </tr>
          </table>
		  <br /><br />
<%
			End IF
		Next
	Next
%>	</td>
  </tr>
</table>

<hr>
<br>
<%
	ElseIF arrCartProduct(2,intCountProduct)=32 Then '### Golf Course ###
%>
			<table width="85%" cellspacing="1" cellpadding="2">
  <tr>
    <td><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="20%" align="left"><img src="thailand-golf-pic/<%=arrCartProduct(9,intCountProduct)%>_b_1.jpg"></td>
        <td><table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td colspan="2"><strong><font color="#2C5266"><%=arrCartProduct(8,intCountProduct)%></font></strong><br>
              <%=arrCartProduct(12,intCountProduct)%>		  </td>
            </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><strong><font color="#66482C">Tee-Off Date:</font></strong> <%=function_date(arrCartProduct(3,intCountProduct),5)%><br>
              <font color="#66482C"><strong>Tee-Off Time:</strong></font> <%=function_date(arrCartProduct(3,intCountProduct),7)%> (<font color="green"><em>Requested Time, Can not gaurantee</em></font>)</td>
            <td><strong><font color="#3D2E66">Golfer(s):</font></strong> <%=arrCartProduct(7,intCountProduct)%></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>

<table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FBFBFB">
  <tr>
    <td colspan="2"><strong>Full Golfer Name List For <font color="003663"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
    </tr>
<%
intCountGuestAll = 1
For intCountGuestAdult=1 To arrCartProduct(7,intCountProduct)
%>
  <tr>
    <td><font color="346494">Golfer <%=intCountGuestAll%></font></td>
    <td><input name="adult_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("adult_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestAdult)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
</table>
<br /><br />
<%
	intOptionNumber = 0
	For intCountItem=0 To Ubound(arrCartItem,2)
			intOptionNumber = intOptionNumber+1
%>
		  <table width="100%" cellpadding="2"  cellspacing="0" bgcolor="#FBFBFB">
            <tr align="left">
              <td colspan="2"><strong>Course <%=intOptionNumber%> # <font color="003663"><%=arrCartItem(5,intCountItem)%></font></strong></td>
              </tr>
            <tr align="left">
              <td><font color="346494">Comment: </font></td>
              <td><textarea name="comment_<%=arrCartItem(0,intCountItem)%>" cols="50" rows="5"><%=Session("comment_"&arrCartItem(0,intCountItem))%></textarea></td>
            </tr>
          </table>
		  <br /><br />
<%
	Next
%>	</td>
  </tr>
</table>

<hr>
<br>

<%
	ElseIF arrCartProduct(2,intCountProduct)=34 Then '### SightSeeing ###
%>

			<table width="85%" cellspacing="1" cellpadding="2">
  <tr>
    <td><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="20%" align="left"><img src="thailand-day-trips-pic/<%=arrCartProduct(9,intCountProduct)%>_b_1.jpg"></td>
        <td><table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td colspan="2"><strong><font color="#2C5266"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
            </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><strong><font color="#66482C">Trip Date:</font></strong> <%=function_date(arrCartProduct(3,intCountProduct),5)%></td>
            <td><strong><font color="#3D2E66">Adult:</font></strong> <%=arrCartProduct(5,intCountProduct)%><br>
              <font color="#3D2E66"><strong>Children:</strong></font> <%=arrCartProduct(6,intCountProduct)%></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>
<table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FBFBFB">
  <tr>
    <td colspan="2"><strong>Full Guest Name List For <font color="003663"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
    </tr>
<%
intCountGuestAll = 1
For intCountGuestAdult=1 To arrCartProduct(5,intCountProduct)
%>
  <tr>
    <td><font color="346494">Guest <%=intCountGuestAll%> (Adult) </font></td>
    <td><input name="adult_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("adult_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestAdult)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
<%
For intCountGuestChildren=1 To arrCartProduct(6,intCountProduct)
%>
  <tr>
    <td><font color="346494">Guest <%=intCountGuestAll%> (Children) </font></td>
    <td><input name="children_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestChildren%>" type="text" size="50" value="<%=Session("children_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestChildren)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
</table>
<br /><br />
		  <table width="100%" cellpadding="2"  cellspacing="0" bgcolor="#FBFBFB">
            <tr align="left">
              <td colspan="2"><strong>Trip <%=intCountAdult%> # <font color="003663"><%=arrCartItem(5,0)%></font></strong></td>
              </tr>
<%
'### Create Pickup Place ###
IF Session("comment_"&arrCartItem(0,0))= "" OR ISNULL(Session("comment_"&arrCartItem(0,0))) Then
	strPickup = function_gen_sightseeing_pickup(arrCartProduct,arrCartProduct(3,intCountProduct),1)
Else
	strPickup = Session("comment_"&arrCartItem(0,0))
End IF
'### Create Pickup Place ###
%>
            <tr align="left">
              <td><font color="346494">Pickup Place</font></td>
              <td><textarea name="comment_<%=arrCartItem(0,0)%>" cols="50" rows="5"><%=strPickup%></textarea></td>
            </tr>
          </table>
		  <br /><br />
	
	</td>
  </tr>
</table>

<hr>
<br>

<%
	ElseIF arrCartProduct(2,intCountProduct)=36 Then '### Water Activity ###
%>
			<table width="85%" cellspacing="1" cellpadding="2">
  <tr>
    <td><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="20%" align="left"><img src="thailand-water-activity-pic/<%=arrCartProduct(9,intCountProduct)%>_b_1.jpg"></td>
        <td><table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td colspan="2"><strong><font color="#2C5266"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
            </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><strong><font color="#66482C">Trip Date:</font></strong> <%=function_date(arrCartProduct(3,intCountProduct),5)%></td>
            <td><strong><font color="#3D2E66">Adult:</font></strong> <%=arrCartProduct(5,intCountProduct)%><br>
              <font color="#3D2E66"><strong>Children:</strong></font> <%=arrCartProduct(6,intCountProduct)%></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>
<table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FBFBFB">
  <tr>
    <td colspan="2"><strong>Full Guest Name List For <font color="003663"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
    </tr>
<%
intCountGuestAll = 1
For intCountGuestAdult=1 To arrCartProduct(5,intCountProduct)
%>
  <tr>
    <td><font color="346494">Guest <%=intCountGuestAll%> (Adult) </font></td>
    <td><input name="adult_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("adult_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestAdult)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
<%
For intCountGuestChildren=1 To arrCartProduct(6,intCountProduct)
%>
  <tr>
    <td><font color="346494">Guest <%=intCountGuestAll%> (Children) </font></td>
    <td><input name="children_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestChildren%>" type="text" size="50" value="<%=Session("children_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestChildren)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
</table>
<br /><br />
		  <table width="100%" cellpadding="2"  cellspacing="0" bgcolor="#FBFBFB">
            <tr align="left">
              <td colspan="2"><strong>Trip <%=intCountAdult%> # <font color="003663"><%=arrCartItem(5,0)%></font></strong></td>
              </tr>
<%
'### Create Pickup Place ###
IF Session("comment_"&arrCartItem(0,0))= "" OR ISNULL(Session("comment_"&arrCartItem(0,0))) Then
	strPickup = function_gen_sightseeing_pickup(arrCartProduct,arrCartProduct(3,intCountProduct),1)
Else
	strPickup = Session("comment_"&arrCartItem(0,0))
End IF
'### Create Pickup Place ###
%>
            <tr align="left">
              <td><font color="346494">Pickup Place</font></td>
              <td><textarea name="comment_<%=arrCartItem(0,0)%>" cols="50" rows="5"><%=strPickup%></textarea></td>
            </tr>
          </table>
		  <br /><br />
	
	</td>
  </tr>
</table>

<%
	ElseIF arrCartProduct(2,intCountProduct)=38 Then '### Shows & Events ###
%>
			<table width="85%" cellspacing="1" cellpadding="2">
  <tr>
    <td><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="20%" align="left"><img src="thailand-show-event-pic/<%=arrCartProduct(9,intCountProduct)%>_b_1.jpg"></td>
        <td><table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td colspan="2"><strong><font color="#2C5266"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
            </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><strong><font color="#66482C">Show or Event Date:</font></strong> <%=function_date(arrCartProduct(3,intCountProduct),5)%></td>
            <td><strong><font color="#3D2E66">Adult:</font></strong> <%=arrCartProduct(5,intCountProduct)%><br>
              <font color="#3D2E66"><strong>Children:</strong></font> <%=arrCartProduct(6,intCountProduct)%></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>
<table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FBFBFB">
  <tr>
    <td colspan="2"><strong>Full Guest Name List For <font color="003663"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
    </tr>
<%
intCountGuestAll = 1
For intCountGuestAdult=1 To arrCartProduct(5,intCountProduct)
%>
  <tr>
    <td><font color="346494">Guest <%=intCountGuestAll%> (Adult) </font></td>
    <td><input name="adult_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("adult_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestAdult)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
<%
For intCountGuestChildren=1 To arrCartProduct(6,intCountProduct)
%>
  <tr>
    <td><font color="346494">Guest <%=intCountGuestAll%> (Children) </font></td>
    <td><input name="children_<%=arrCartProduct(0,intCountProduct)%>_<%=intCountGuestChildren%>" type="text" size="50" value="<%=Session("children_"&arrCartProduct(0,intCountProduct)&"_"&intCountGuestChildren)%>"></td>
  </tr>
<%
	intCountGuestAll = intCountGuestAll + 1
Next
%>
</table>
<br /><br />
	
	</td>
  </tr>
</table>
<%
	ElseIF arrCartProduct(2,intCountProduct)=39 Then '### Health Check Up ###
%>
			<table width="85%" cellspacing="1" cellpadding="2">
  <tr>
    <td><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="20%" align="left"><img src="thailand-health-pic/<%=arrCartProduct(9,intCountProduct)%>_b_1.jpg"></td>
        <td><table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td colspan="2"><strong><font color="#2C5266"><%=arrCartProduct(8,intCountProduct)%></font></strong><br>
              <%=arrCartProduct(12,intCountProduct)%>		  </td>
            </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><strong><font color="#66482C">Date & Time:</font></strong> <%=function_date(arrCartProduct(3,intCountProduct),5)%><br>
              <%=function_date(arrCartProduct(3,intCountProduct),7)%> (<font color="green"><em>Requested Time, Can not gaurantee</em></font>)</td>
            <td><strong><font color="#3D2E66">Adult(s):</font></strong> <%=arrCartProduct(5,intCountProduct)%></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>

<table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FBFBFB">
 <%IF Request("err")<>"" Then%>
              <tr align="left">
                <td colspan="2" align="center"><a name="err"></a><%=function_display_error_box (Request("err"),2)%></td>
              </tr>
              <%End IF%>
  <tr>
    <td colspan="2"><strong>Full Person Name List For <font color="003663"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
    </tr>
<%
intCountGuestAll = 1
For intCountItem=0 to Ubound(arrCartItem,2)
%>
 <tr>
	<td><font color="003663">Person <%=arrCartItem(5,intCountItem)%></font></td>
</tr>
<%
	For intCountGuestAdult=1 To arrCartItem(3,intCountItem)
	
	%>
	  <tr>
		<td><font color="346494">Person <%=intCountGuestAdult%></font></td>
		<td><input name="adult_<%=arrCartItem(0,intCountItem)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("adult_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult)%>"></td>
	</tr>
	<tr>
	<td>Birthday</td>
	<td><%=function_birthday_input(session("b_date_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult),session("b_month_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult),session("b_year_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult),"b_date_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult,"b_month_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult,"b_year_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult,1)%></td>
	</tr>
	<tr>
	<td>Passport No.</td>
	<td><input name="passport_<%=arrCartItem(0,intCountItem)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("passport_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult)%>"></td>
	</tr>
	<tr>	
		<td valign="top"><font color="346494">Comment: </font></td><td><textarea name="comment_<%=arrCartItem(0,intCountItem)&"_"&intCountGuestAdult%>" cols="50" rows="5"><%=Session("comment_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult)%></textarea></td>
	  </tr>
	<%
		intCountGuestAll = intCountGuestAll + 1
	Next
Next
%>
</table>
</td>
  </tr>
</table>
<%
	ElseIF arrCartProduct(2,intCountProduct)=40 Then '### Spa ###
%>
			<table width="85%" cellspacing="1" cellpadding="2">
  <tr>
    <td><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="20%" align="left"><img src="thailand-spa-pic/<%=arrCartProduct(9,intCountProduct)%>_b_1.jpg"></td>
        <td><table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td colspan="2"><strong><font color="#2C5266"><%=arrCartProduct(8,intCountProduct)%></font></strong><br>
              <%=arrCartProduct(12,intCountProduct)%>		  </td>
            </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><strong><font color="#66482C">Date & Time:</font></strong> <%=function_date(arrCartProduct(3,intCountProduct),5)%><br>
              <%=function_date(arrCartProduct(3,intCountProduct),7)%> (<font color="green"><em>Requested Time, Can not gaurantee</em></font>)</td>
            <td><strong><font color="#3D2E66">&nbsp;</td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>

<table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FBFBFB">
 <%IF Request("err")<>"" Then%>
              <tr align="left">
                <td colspan="2" align="center"><a name="err"></a><%=function_display_error_box (Request("err"),2)%></td>
              </tr>
              <%End IF%>
  <tr>
    <td colspan="2"><strong>Full Person Name List For <font color="003663"><%=arrCartProduct(8,intCountProduct)%></font></strong></td>
    </tr>
<%
intCountGuestAll = 1
For intCountItem=0 to Ubound(arrCartItem,2)
%>
 <tr>
	<td><font color="003663">Person <%=arrCartItem(5,intCountItem)%></font></td>
</tr>
<%
	For intCountGuestAdult=1 To arrCartItem(3,intCountItem)
	
	%>
	  <tr>
		<td><font color="346494">Person <%=intCountGuestAdult%></font></td>
		<td><input name="adult_<%=arrCartItem(0,intCountItem)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("adult_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult)%>"></td>
	</tr>
	<tr>
	<td>Birthday</td>
	<td><%=function_birthday_input(session("b_date_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult),session("b_month_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult),session("b_year_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult),"b_date_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult,"b_month_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult,"b_year_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult,1)%></td>
	</tr>
	<tr>
	<td>Passport No.</td>
	<td><input name="passport_<%=arrCartItem(0,intCountItem)%>_<%=intCountGuestAdult%>" type="text" size="50" value="<%=Session("passport_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult)%>"></td>
	</tr>
	<tr>	
		<td valign="top"><font color="346494">Comment: </font></td><td><textarea name="comment_<%=arrCartItem(0,intCountItem)&"_"&intCountGuestAdult%>" cols="50" rows="5"><%=Session("comment_"&arrCartItem(0,intCountItem)&"_"&intCountGuestAdult)%></textarea></td>
	  </tr>
	<%
		intCountGuestAll = intCountGuestAll + 1
	Next
Next
%>
</table>
</td>
  </tr>
</table>
<hr>
<br>
<%
	ElseIF arrCartProduct(2,intCountProduct)=999 Then '### xxx ###
	End IF
Next
%>			</td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align="center"><table width="100%" cellpadding="2"  cellspacing="1" bgcolor="#FCB45C">
          <tr>
            <td bgcolor="#FFFAEF"><span class="l1">Step 4 : <font color="#fe5400">Click &quot;Make Payment&quot; to finish your booking process.</font></span></td>
          </tr>
          <tr>
            <td align="center" bgcolor="#FFFFFF">
                <table width="100%" cellspacing="1" cellpadding="6">
                  <tr>
                    <td align="left" class="m"><b><font color="#003366">Total Amount: </font><font color="#990000"  class="l2"><%=function_display_price(intPriceTotalAll,3)%>&nbsp;<%=ConstCurrencyDisplay %></font></b>&nbsp; <i><%=strOtherPriceTotal%></i> 
<%= strHiddenPriceOwn%>
<%= strHiddenPrice %>
<% =strHiddenPriceDisplay%>
<%=strHiddenPromotion%>
<input type="hidden" name="price_total_all" value="<%=intPriceTotalAll%>">					
</td>
                  </tr>

					<%=function_gen_payment(intCartID,1)%>
                  
                  <!--   <tr>
                    <td align="left"><font color="#000000">Please click here for Visa, Master Card, Discover Card and   Paypal Account&nbsp;User (VISA and Master Card holder&nbsp;with paypal account is   acceptable)</font></td>
                  </tr>
                  <tr>
                    <td align="left"><input type="image" src="images/b_make_payment.gif" width="112" height="26" name="cmdpaypal" value="2">
                        <img src="/images/img_creditcard_visa.gif" alt="Visa Card"> <img src="/images/img_creditcard_master.gif" alt="Master Card"> <img src="/images/img_creditcard_amex.gif" alt="Amex"> <img src="/images/img_creditcard_discover.gif" alt="Dsicover"> <img src="/images/img_creditcard_paypal.gif" alt="Paypal Account"></td>
                  </tr>-->
                  <tr>
                    <td>
					<strong> <font color="#FF0000">*</font> <font color="green">Your fund will be held with us until your reservation is confirmed. In case your reservation is not available, we will release all funds to your credit card account immediately. </font> </strong> <br /> <br />
					<strong> <font color="#FF0000">*</font> </strong> Your payment will be secured protection by a validated <font color="#003366"><strong>Secure Sockets Layer (SSL)</strong></font>,   certificates capable of 128-bit encryption where you can safely enter your credit card details in an <font color="#003366"><strong>encrypted</strong></font> environment. We value your privacy and will not share your personal information. <br /> 
					<br />
                 <strong> <font color="#FF0000">*</font></strong>  Hotels2Thailand.com is a travel agent registered under name of <font color="#003366"><strong>Blue House Travel Co.,Ltd</strong></font> by Tourism Authority of Thailand.<font color="#003366"><strong>TAT License No. 11/3240</strong> </font><br /><br />
				<strong> <font color="#FF0000">*</font></strong>  Please review our <a href="javascript:popup('/thailand-hotels-cancel-2.asp',700,750)"><u>Cancellation Policy</u></a>
				</td>
				  </tr>
              </table></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  </form>
</table>

		  <!-- InstanceEndEditable --></td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" bgcolor="#FFFFFF">
        <tr> 
          <td>&nbsp;</td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11" bgcolor="97BFEC">
        <tr> 
          <td height="1"><img src="/images/spacer.gif" width="1" height="1"></td>
        </tr>
      </table>
      <table width="780" border="0" cellspacing="0" cellpadding="0" class="f11">
        <tr> 
          <td height="24" background="/images/bg_bar.gif" align="center"><font color="346494">Copyright 
            &copy; 1996-<%=Year(Date)%> Hotels 2 Thailand .com. All rights reserved.</font></td>
        </tr>
      </table>
    </td>
  </tr>
</table>
</body>
<!-- InstanceEnd --></html>
<%
Call connClose()
%>
