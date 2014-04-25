<%OPTION EXPLICIT%>
<!--#include virtual="/include/constant.asp"-->
<!--#include virtual="/include/function_date.asp"-->
<!--#include virtual="/include/function_display_bol.asp"-->
<!--#include virtual="/include/function_display_hotel_detail.asp"-->
<!--#include virtual="/include/function_date_sql.asp"-->
<!--#include virtual="/include/function_display_product_period.asp"-->
<!--#include virtual="/sub_check_bht_access.asp"--> 
<!--#include virtual="/include/function_gen_dropdown_sql.asp"--> 
<!--#include virtual="/include/function_gen_dropdowm_room_require.asp"--> 
<!--#include virtual="/include/function_date.asp"-->
<!--#include virtual="/include/function_display_bol.asp"-->
<!--#include virtual="/admin/include/Fn_Re_Id_List_Box.asp"-->

<%
Call connOpenRW()

Session.CodePage = 874
Dim intOrderProductID
Dim sqlUpdateGuest
Dim recUpdateGuest
Dim sqlUpdateRequire
Dim recUpdateRequire
Dim sqlProduct
Dim recProduct
Dim arrProduct
Dim sqlItem
Dim recItem
Dim arrItem
Dim intCount
Dim intCountItem
Dim intPriceSub
Dim intPriceTotal
Dim arrGuest
Dim sqlReq
Dim recReq
Dim arrReq
Dim strSmoking
Dim strBed
Dim strFloor
Dim sqlStaff
Dim recStaff
Dim arrStaff
Dim strPromotion

intOrderProductID = Request.Form("order_product_id")

'### Update Guest List ###
sqlUpdateGuest = "SELECT guest_id,guest_cat_id,guest_name FROM tbl_order_product_guest WHERE order_product_id=" & intOrderProductID & " ORDER BY guest_cat_id ASC, guest_id ASC"
Set recUpdateGuest = Server.CreateObject ("ADODB.Recordset")
recUpdateGuest.Open SqlUpdateGuest, Conn,adOpenStatic,adLockOptimistic
	While NOT recUpdateGuest.EOF
		recUpdateGuest.Fields("guest_name") = Server.HTMLEncode(Request.Form(Cstr(recUpdateGuest.Fields("guest_id"))))
		recUpdateGuest.Update
		recUpdateGuest.MoveNext
	Wend
	recUpdateGuest.MoveFirst
	arrGuest = recUpdateGuest.GetRows()
recUpdateGuest.Close
Set recUpdateGuest = Nothing
'### Update Guest List ###

'### Update Room Requirement ###
sqlUpdateRequire= "SELECT otr.*"
sqlUpdateRequire = sqlUpdateRequire & " FROM tbl_order_item_require_hotel otr, tbl_order_item ot"
sqlUpdateRequire = sqlUpdateRequire & " WHERE ot.item_id=otr.item_id AND ot.order_product_id=" & intOrderProductID
Set recUpdateRequire = Server.CreateObject ("ADODB.Recordset")
recUpdateRequire.Open SqlUpdateRequire, Conn,adOpenStatic,adLockOptimistic
	While NOT recUpdateRequire.EOF
		recUpdateRequire.Fields("comment") = Request.Form("comment_" & recUpdateRequire.Fields("require_id"))
		recUpdateRequire.Fields("type_smoking") = Request.Form("smoke_" & recUpdateRequire.Fields("require_id"))
		recUpdateRequire.Fields("type_bed") = Request.Form("bed_" & recUpdateRequire.Fields("require_id"))
		recUpdateRequire.Fields("type_floor") = Request.Form("floor_" & recUpdateRequire.Fields("require_id"))
		recUpdateRequire.Update
		recUpdateRequire.MoveNext
	Wend
recUpdateRequire.Close
Set recUpdateRequire = Nothing
'### Update Room Requirement ###

sqlProduct = "SELECT op.order_product_id,op.order_id,op.product_id,op.num_adult,op.num_child,op.date_time_check_in,op.date_time_check_out,p.title_real,s.phone,s.fax,o.full_name,c.title,p.breakfast"
sqlProduct = sqlProduct & " FROM tbl_order_product op, tbl_product p, tbl_supplier s,tbl_order o, tbl_country c"
sqlProduct = sqlProduct & " WHERE o.country_id=c.country AND o.order_id=op.order_id AND s.supplier_id=p.supplier_id AND op.product_id=p.product_id AND op.order_product_id=" & intOrderProductID
Set recProduct = Server.CreateObject ("ADODB.Recordset")
recProduct.Open SqlProduct, Conn,adOpenStatic,adLockReadOnly
	arrProduct = recProduct.GetRows()
recProduct.Close
Set recProduct = Nothing

sqlItem = "SELECT ot.item_id,po.option_cat_id,ot.unit,ot.price_rack,ot.price_display_currency,ot.promotion_title,ot.promotion_detail,ot.promotion_offer_person,po.title_en,ot.promotion_id"
sqlItem = sqlItem & " FROM tbl_order_item ot,tbl_product_option po"
sqlItem = sqlItem & " WHERE po.option_id=ot.option_id AND ot.order_product_id=" & intOrderProductID
Set recItem = Server.CreateObject ("ADODB.Recordset")
recItem.Open SqlItem, Conn,adOpenStatic,adLockReadOnly
	arrItem = recItem.GetRows()
recItem.Close
Set recItem = Nothing

sqlReq = "SELECT orh.require_id,orh.comment,orh.type_smoking,orh.type_bed,orh.type_floor,po.title_en"
sqlReq = sqlReq & " FROM tbl_order_item ot, tbl_product_option po, tbl_order_item_require_hotel orh"
sqlReq = sqlReq & " WHERE po.option_id=ot.option_id AND ot.item_id=orh.item_id AND ot.order_product_id=" & intOrderProductID
sqlReq = sqlReq & " ORDER BY po.option_id ASC, orh.require_id ASC"
Set recReq = Server.CreateObject ("ADODB.Recordset")
recReq.Open SqlReq, Conn,adOpenStatic,adLockReadOnly
	arrReq = recReq.GetRows()
recReq.Close
Set recReq = Nothing

sqlStaff = "SELECt full_sur_name,signature FROM tbl_staff WHERE staff_id=" & Request.Form("staff_id")
Set recStaff = Server.CreateObject ("ADODB.Recordset")
recStaff.Open SqlStaff, Conn,adOpenStatic,adLockReadOnly
	arrStaff = recStaff.GetRows()
recStaff.Close
Set recStaff = Nothing

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Booking No. <%=arrProduct(1,0)%>/<%=arrProduct(0,0)%></title>
<link rel="stylesheet" href="../css/order.css" type="text/css">
</head>

<body>
<%
Dim strVch
strVch = ""
strVch = strVch &"<div align='center'>"
strVch = strVch &"<table width='650' border='0' cellpadding='4' cellspacing='1' bgcolor='#000000'>"
strVch = strVch &"<tr><td align='center' bgcolor='#FFFFFF'><table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#444444'>"
strVch = strVch &"<tr><td align='left' bgcolor='#FFFFFF'><img src='../images/bht_logo.jpg' width='65' height='65' /></td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'><span class='l3'>BLUE HOUSE TRAVEL CO., LTD</span><br/>"
strVch = strVch &"254/10 Soi Ratchadapisek 42 Ratchadapisek Rd., Ladyao Chatuchak , Bangkok 10900 <br />"
strVch = strVch &"Tel. 66-2- 9300973 , 66-2-9306050 Fax. 66-2-9306514 , 66-2-9306825,(Account) 66-2-5135419</td>"
strVch = strVch &"</tr></table></td></tr>"
strVch = strVch &"<tr><td align='center' bgcolor='#FFFFFF' class='l2'>Booking no. "&arrProduct(1,0)&"/"&arrProduct(0,0)&" </td>"
strVch = strVch &"</tr><tr><td bgcolor='#FFFFFF'><table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#444444' class='m1'>"
strVch = strVch &"<tr><td align='left' bgcolor='#FFFFFF' class='mStrong'>Name :</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&arrProduct(7,0)&" </td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF' class='mStrong'>Issue Date: </td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&function_date(dateCurrentConstant,4)&"</td>"
strVch = strVch &"</tr><tr>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF' class='mStrong'>Phone:</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&arrProduct(8,0)&"</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF' class='mStrong'>Fax:</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&arrProduct(9,0)&"</td>"
strVch = strVch &"</tr><tr>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF' class='mStrong'>Attn:</td>"
strVch = strVch &"<td colspan='3' align='left' bgcolor='#FFFFFF'>"&Request.Form("attn")&"</td>"
strVch = strVch &"</tr></table></td></tr>"
strVch = strVch &"<tr><td bgcolor='#FFFFFF'><table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#444444' class='m1'>"
strVch = strVch &"<tr><td align='left' bgcolor='#FFFFFF' class='l2'>Booking Detail:</td>"
strVch = strVch &"</tr><tr>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'><table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#3A3A3A'>"
strVch = strVch &"<tr><td align='left' bgcolor='#FFFFFF' class='mStrong'>Booked Name :</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&arrProduct(10,0)&"<br />"
strVch = strVch &"( Country : "&arrProduct(11,0)&" )</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF' class='mStrong'>Check In :</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&function_date(arrProduct(5,0),5)&"</td>"
strVch = strVch &"</tr><tr>"
strVch = strVch &"<td colspan='2' align='left' bgcolor='#FFFFFF'><strong>Adult</strong> :  "&arrProduct(3,0)&" <strong>Child</strong> : "&arrProduct(4,0)&"<strong> Breakfast</strong> : "&function_display_bol("Included","-",arrProduct(12,0),"",2)&"</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF' class='mStrong'>Check Out :</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&function_date(arrProduct(6,0),5)&"</td>"
strVch = strVch &"</tr></table><br />"
strVch = strVch &"<table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#3A3A3A' class='m1'>"
strVch = strVch &"<tr class='mStrong'>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>No.</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>Room Type </td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>Night</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>Room(s)</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>Price</td></tr>"

intCountItem = 0
intPriceSub = 0
For intCount=0 To Ubound(arrItem,2)
	IF arrItem(1,intCount)=38 OR arrItem(1,intCount)=39 Then
		intCountItem = intCountItem + 1
		intPriceSub = intPriceSub + arrItem(3,intCount)
		intPriceTotal = intPriceTotal + arrItem(3,intCount)
		
		IF arrItem(5,intCount)<>"" AND NOT ISNULL(arrItem(5,intCount)) Then
			if Fn_Re_Id_List_Box("promotion_id|offer_id","tbl_promotion",arrItem(9,intCount))=2 then		
				strPromotion = "<br /><br /> <font color=""#ff0000"">(" & arrItem(5,intCount) & ")</font> <br />"  &  arrItem(6,intCount)
			else
				strPromotion = "<br /><br /> (" & arrItem(5,intCount) & ") <br />"  &  arrItem(6,intCount)
			end if
 		Else
			strPromotion = ""
		End IF
		

strVch = strVch &"<tr>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>"&intCountItem&".</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&arrItem(8,intCount)& strPromotion&"</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>"&DateDiff("d", arrProduct(5,0), arrProduct(6,0))&"</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>"&arrItem(2,intCount)&"</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>"&FormatNumber(arrItem(3,intCount),2)&"</td></tr>"

	End IF
Next

strVch = strVch &"<tr><td colspan='4' align='center' bgcolor='#FFFFFF' class='l1'>Sub Total </td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'><strong>"&FormatNumber(intPriceSub,2)&"</strong></td>"
strVch = strVch &"</tr></table><br />"
strVch = strVch &"<table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#3A3A3A'>"
strVch = strVch &"<tr class='mStrong'>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>No.</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>Extra Option</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>Quantity</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>Price</td></tr>"

intCountItem = 0
intPriceSub = 0
For intCount=0 To Ubound(arrItem,2)
	IF arrItem(1,intCount)<>38 AND arrItem(1,intCount)<>39 Then
		intCountItem = intCountItem + 1
		intPriceSub = intPriceSub + arrItem(3,intCount)
		intPriceTotal = intPriceTotal + arrItem(3,intCount)

strVch = strVch &"<tr><td align='center' bgcolor='#FFFFFF'>"&intCountItem&".</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&arrItem(8,intCount)&"</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>"&arrItem(2,intCount)&"</td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'>"&FormatNumber(arrItem(3,intCount),2)&"</td></tr>"

	End IF
Next

strVch = strVch &"<tr><td colspan='3' align='center' bgcolor='#FFFFFF' class='l1'>Sub Total </td>"
strVch = strVch &"<td align='center' bgcolor='#FFFFFF'><strong>"&FormatNumber(intPriceSub,2)&"</strong></td>"
strVch = strVch &"</tr></table><br />"
strVch = strVch &"<table width='75%' border='0' cellpadding='2' cellspacing='1' bgcolor='#3A3A3A'>"
strVch = strVch &"<tr><td align='center' bgcolor='#FFFFFF' class='l2'>Total Price: "&FormatNumber(intPriceTotal,2)&" </td>"
strVch = strVch &"</tr></table></td></tr></table></td></tr>"
strVch = strVch &"<tr><td bgcolor='#FFFFFF'><table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#444444' class='m1'>"
strVch = strVch &"<tr><td bgcolor='#FFFFFF' class='l2'>Room Requiement &amp; Guest Name </td></tr>"
strVch = strVch &"<tr><td bgcolor='#FFFFFF'><table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#3A3A3A'>"

intCountItem = 0
For intCount=0 To Ubound(arrGuest,2)
	IF arrGuest(1,intCount)=1 Then
		intCountItem = intCountItem + 1

strVch = strVch &"<tr><td width='15%' align='left' bgcolor='#FFFFFF' class='mStrong'>Adult "&intCountItem&": </td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&arrGuest(2,intCount)&"</td></tr>"

	End IF
Next

intCountItem = 0
For intCount=0 To Ubound(arrGuest,2)
	IF arrGuest(1,intCount)=2 Then
		intCountItem = intCountItem + 1

strVch = strVch &"<tr><td width='15%' align='left' bgcolor='#FFFFFF' class='mStrong'>Children "&intCountItem&": </td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&arrGuest(2,intCount)&"</td></tr>"

	End IF
Next

strVch = strVch &"</table><br />"

intCountItem = 0
For intCount=0 To Ubound(arrReq,2)
	intCountItem = intCountItem + 1

	SELECT CASE arrReq(2,intCount)
		Case 1
			strSmoking = "None Smoking"
		Case 2
			strSmoking = "Smoking"
		Case 3
			strSmoking = "-"
	END SELECT
						
	SELECT CASE arrReq(3,intCount)
		Case 1
			strBed = "King Size Bed"
		Case 2
			strBed = "Twin Bed"
		Case 3
			strBed = "-"
	END SELECT
				
	SELECT CASE arrReq(4,intCount)
		Case 1
			strFloor = "High Floor"
		Case 2
			strFloor = "Low Floor"
		Case 3
			strFloor = "-"
	END SELECT

strVch = strVch &"<table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#3A3A3A'>"
strVch = strVch &"<tr><td colspan='2' align='left' bgcolor='#FFFFFF' class='l1'>Room "&intCountItem&" # "&arrReq(5,intCount)&" </td>"
strVch = strVch &"</tr><tr><td width='20%' align='left' bgcolor='#FFFFFF' class='mStrong'>Requirement</td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&strSmoking&" , "&strBed&" , "&strFloor&" </td>"
strVch = strVch &"</tr><tr>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF' class='mStrong'>Special Request </td>"
strVch = strVch &"<td align='left' bgcolor='#FFFFFF'>"&Replace(arrReq(1,intCount),VbCrlf,"<br />")&"</td>"
strVch = strVch &"</tr></table><br />"

Next

strVch = strVch &"</td></tr></table></td></tr><tr>"
strVch = strVch &"<td bgcolor='#FFFFFF'><table width='100%' border='0' cellpadding='2' cellspacing='1' bgcolor='#444444'>"
strVch = strVch &"<tr><td width='50%' bgcolor='#FFFFFF'>"
strVch = strVch &"<table width='100%' height='100%' border='0' cellpadding='0' cellspacing='1'>"
strVch = strVch &"<tr><td height='143' align='left' valign='top' bgcolor='#FFFFFF'><u class='l1'>Hotel" 
strVch = strVch &" Confirmation</u> :<br>"
strVch = strVch &"<div align='left'><strong>Confirmamtion Accepted </strong>:"
strVch = strVch &"<input type='checkbox' name='checkbox' value='checkbox'>"
strVch = strVch &"Yes&nbsp;&nbsp; "
strVch = strVch &"<input type='checkbox' name='checkbox2' value='checkbox'>"
strVch = strVch &"No<br><br></div>"
strVch = strVch &"<div align='left'><strong>Confirm by Name</strong> : ........................................<br>"
strVch = strVch &"<br></div>"
strVch = strVch &"<div align='left'><strong>Signature</strong> : .....................................................<br>"
strVch = strVch &"<br><strong>Date</strong> : .............................................................</div></td></tr>"
strVch = strVch &"</table></td><td bgcolor='#FFFFFF'>"
strVch = strVch &"<table width='343' height='143' cellpadding='0' cellspacing='0' class='style12'>"
strVch = strVch &"<tr><td height='143' colspan='2' rowspan='3' valign='middle' background='picture/stemp.gif'><div align='right'><strong>Signature</strong> "
strVch = strVch &" :</div><div align='right'></div>"
strVch = strVch &"<div align='right'></div></td>"
strVch = strVch &"<td width='203' align='center'><img src='"&arrStaff(1,0)&"'></td>"
strVch = strVch &"</tr><tr><td align='center'>("&arrStaff(0,0)&")</td>"
strVch = strVch &"</tr><tr><td><div align='center'></div></td>"
strVch = strVch &"</tr></table></td></tr></table></td></tr>"
strVch = strVch &"<tr><td align='center' bgcolor='#FFFFFF'>Remark: "
strVch = strVch &" Appreciate to receive your confirmation of this booking via" 
strVch = strVch &" fax asap.</td></tr></table></div>"

response.Write(strVch)

%>
<form action="mail_booking_hotel2_send.asp" method="post">
	<input type="submit" value="Send Mail" />
    <input type="hidden" value="<%=strVch%>" name="strVch" />
</form>
<%

Call connClose()
%>
</body>
</html>
