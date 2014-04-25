<!--#include file="../admin/include/fnDate.asp"--> 
<%
FUNCTION function_gen_mail_confirm(intOrderID,intType)

Dim sqlDetail, recDetail
Dim intNight
Dim i
Dim intSubTotal
Dim intTotal
Dim strDetail
Dim strOrderID
Dim strMerchantID
Dim strBody
Dim strEmail
Dim strName
Dim strLink
Dim intProductID


strOrderID = intOrderID
IF Len(strOrderID)=6 Then
	strOrderID = Right(strOrderID,5)
End IF

strLink = "http://www.hotels2thailand.com/voucher.asp?referid=2216218&serial=ttocpad&order_id=002"& strOrderID &"3345&read=true"
sqlDetail = "SELECT o.order_id, o.confirm_payment , o.full_name, o.email, ot.option_id, ot.unit, ot.price, ot.date_check_in, ot.date_check_out, p.title_en AS product_title, po.title_en AS option_title,p.product_id,po.option_cat_id"
sqlDetail = sqlDetail & " FROM tbl_order o, tbl_order_item ot, tbl_product p, tbl_product_option po"
sqlDetail = sqlDetail & " WHERE p.product_id=po.product_id AND po.option_id=ot.option_id AND o.order_id=ot.order_id AND o.order_id=" & strOrderID

Set recDetail = Server.CreateObject ("ADODB.Recordset")
recDetail.Open sqlDetail, Conn,adOpenStatic,adLockreadOnly

intProductId = recDetail.Fields("product_id")
intNight = DateDiff("d", recDetail.Fields("date_check_in"), recDetail.Fields("date_check_out"))
strDetail = "ORDER ID:"&recDetail.Fields("order_id")&": Hotel Reservation Price (all inclusive) for "& recDetail.Fields("full_name") &" from hotels2thailand.com"
strMerchantID = "41900261"
strEmail = recDetail.Fields("email")
strName = recDetail.Fields("full_name")

strBody = "<html>" & VbCrlf
strBody = strBody & "<head>" & VbCrlf
strBody = strBody & "<title>Reservation Confirm</title>" & VbCrlf
strBody = strBody & "<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>" & VbCrlf
strBody = strBody & "<style type='text/css'>" & VbCrlf
strBody = strBody & "</style>" & VbCrlf
strBody = strBody & "<link href='http://www.hotels2thailand.com/text.css' rel='stylesheet' type='text/css'>" & VbCrlf
strBody = strBody & "</head>" & VbCrlf
strBody = strBody & "<body bgcolor='#FFFFFF' leftmargin='0' topmargin='0' marginwidth='0' marginheight='0'>" & VbCrlf
strBody = strBody & "<table width='606' border='0' cellspacing='1' cellpadding='2' align='center'>" & VbCrlf
strBody = strBody & "<tr>" & VbCrlf
strBody = strBody & "<td align='left' valign='top'> <div align='center'> " & VbCrlf
strBody = strBody & "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" & VbCrlf
strBody = strBody & "<tr>" & VbCrlf
strBody = strBody & "<td align='left' valign='top'><a href='http://www.hotels2thailand.com'><img src='http://www.hotels2thailand.com/image/head.gif' width='606' height='84' border='0'></a></td>" & VbCrlf
strBody = strBody & "</tr>" & VbCrlf
strBody = strBody & "</table>" & VbCrlf
strBody = strBody & "<table width='600' border='0' cellpadding='2' cellspacing='1' bgcolor='#0099FF'>" & VbCrlf
strBody = strBody & "<tr align='left' valign='top'> " & VbCrlf
strBody = strBody & "<td height='35' valign='middle' bgcolor='#0099CC' class='mb1' align='center'><a href='http://www.hotels2thailand.com/default.asp'><font color='#FFFFFF'><b>Home</b></font></a></td>" & VbCrlf
strBody = strBody & "<td height='35' valign='middle' bgcolor='#0099CC' class='mb1' align='center'><a href='http://www.hotels2thailand.com/thailand-hotels-travel.asp'><font color='#FFFFFF'><b>Travel Info</b></font></a></td>" & VbCrlf
strBody = strBody & "<td height='35' valign='middle' bgcolor='#0099CC' class='mb1' align='center'><a href='http://www.hotels2thailand.com/thailand-hotels-faq.asp'><font color='#FFFFFF'><b>FAQ</b></font></a></td>" & VbCrlf
strBody = strBody & "<td height='35' valign='middle' bgcolor='#0099CC' class='mb1' align='center'><a href='http://www.hotels2thailand.com/thailand-hotels-testimonial.asp'><font color='#FFFFFF'><b>Tesimonials</b></font></a></td>" & VbCrlf
strBody = strBody & "<td height='35' valign='middle' bgcolor='#0099CC' class='mb1' align='center'><a href='http://www.hotels2thailand.com/thailand-hotels-contact.asp'><font color='#FFFFFF'><b>Contact Us</b></font></a></td>" & VbCrlf
strBody = strBody & "</tr>" & VbCrlf
strBody = strBody & "</table>" & VbCrlf
strBody = strBody & "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" & VbCrlf
strBody = strBody & "<tr>" & VbCrlf
strBody = strBody & "<td align='left' valign='top'><img src='http://www.hotels2thailand.com/image/blue.gif' width='606' height='19'></td>" & VbCrlf
strBody = strBody & "</tr>" & VbCrlf
strBody = strBody & "</table>" & VbCrlf
strBody = strBody & "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" & VbCrlf
strBody = strBody & "<tr align='left' valign='top'>" & VbCrlf
strBody = strBody & "<td width='1%' background='image/l01.gif'><img src='http://www.hotels2thailand.com/image/spacer.gif' width='4' height='200'></td>" & VbCrlf
strBody = strBody & "<td width='96%'> <table width='100%' border='0' cellspacing='0' cellpadding='2'>" & VbCrlf
strBody = strBody & "<tr>" & VbCrlf
strBody = strBody & "<td><img src='http://www.hotels2thailand.com/image/spacer.gif' width='594' height='3'></td>" & VbCrlf
strBody = strBody & "</tr>" & VbCrlf
strBody = strBody & "<tr>" & VbCrlf
strBody = strBody & "<td align='left' class='m1'> <font color='#33CCCC'><b>Dear Sir or Madam, "& recDetail.Fields("full_name") &" ("& recDetail.Fields("email") &") </b></font> " & VbCrlf
strBody = strBody & "<p>We would like to thank you for taking a service on the www.hotels2thailand.com. We are pleased to inform you that your room reservation is available according to your requirement (ORDER ID: "& recDetail.Fields("order_id") &"). </p>" & VbCrlf
strBody = strBody & "<p>You can print voucher and take to the hotel by <a href='"& strLink &"' target='_blank'><b>click here</b></a>.<br><br> Thank you for your payment to http://www.hotels2thailand.com . Your payment for <font color='#33CCCC'><u>"& recDetail.Fields("product_title") &"</u> (Check in: "& convert_date(recDetail.Fields("date_check_in"),16) &" Check out: "& convert_date(recDetail.Fields("date_check_out"),16) &")</font> detail see below</p><br>" & VbCrlf
strBody = strBody & "</td>" & VbCrlf
strBody = strBody & "</tr>" & VbCrlf
strBody = strBody & "<tr>" & VbCrlf
strBody = strBody & "<td align='center'> " & VbCrlf
strBody = strBody & "<table width='500' border='1' cellspacing='0' cellpadding='4' class='m' bordercolor='#C9E0ED'>" & VbCrlf        
strBody = strBody & "<tr bgcolor='#E8ECFF'>" & VbCrlf
strBody = strBody & "<td align='center' class='text'><b><font color='#000066'>No.</font></b></td>" & VbCrlf
strBody = strBody & "<td align='center' bgcolor='#E8ECFF' class='text'><b><font color='#000066'>Item Details</font></b></td>" & VbCrlf
strBody = strBody & "<td align='center' class='text'><b><font color='#000066'>Rates</font></b></td>" & VbCrlf
strBody = strBody & "<td align='center' class='text'><b><font color='#000066'>Quantity</font></b></td>" & VbCrlf
strBody = strBody & "<td align='center' class='text'><b><font color='#000066'>Night</font></b></td>" & VbCrlf        
strBody = strBody & "<td align='right' class='text'><b><font color='#000066'>Total </font></b></td>" & VbCrlf
strBody = strBody & "</tr>" & VbCrlf
		  

While NOT recDetail.EOF
	i = i+1
	intSubtotal = recDetail.Fields("price")		
	intTotal = intTotal + intSubTotal
	IF recDetail.Fields("option_cat_id")=38 OR recDetail.Fields("option_cat_id")=39 Then
		strBody = strBody & "<tr bgcolor='#FFFFFF'>" & VbCrlf
		strBody = strBody & "<td valign='top' class='text'><font color='#000000'><div align='center'>"& i &".</div></font></td>" & VbCrlf
		strBody = strBody & "<td class='text' valign='top'><font color='#000000'>"& recDetail.Fields("option_title") &" <br> </font></td>" & VbCrlf        
		strBody = strBody & "<td class='text' valign='top'><font color='#000000'> <p align='center'>"& FormatNumber((recDetail.Fields("price")/(recDetail.Fields("unit")*intNight)),0) &" Baht</p></font></td>" & VbCrlf
		strBody = strBody & "<td class='text' valign='top'><font color='#000000'> <p align='center'>"& recDetail.Fields("unit") &" </p></font></td>" & VbCrlf
		strBody = strBody & "<td align='center' valign='top' class='text'><font color='#000000'>"& intNight &"</td>" & VbCrlf
		strBody = strBody & "<td align='right' class='text' valign='top'><font color='#000000'><nobr>"& FormatNumber(intSubtotal,0) &" Baht</nobr></font></td>" & VbCrlf   
		strBody = strBody & "</tr>" & VbCrlf  
	Else
		strBody = strBody & "<tr bgcolor='#FFFFFF'>" & VbCrlf
		strBody = strBody & "<td valign='top' class='text'><font color='#000000'><div align='center'>"& i &".</div></font></td>" & VbCrlf
		strBody = strBody & "<td class='text' valign='top'><font color='#000000'>"& recDetail.Fields("option_title") &" <br> </font></td>" & VbCrlf        
		strBody = strBody & "<td class='text' valign='top'><font color='#000000'> <p align='center'>"& FormatNumber((recDetail.Fields("price")/(recDetail.Fields("unit"))),0) &" Baht</p></font></td>" & VbCrlf
		strBody = strBody & "<td class='text' valign='top'><font color='#000000'> <p align='center'>"& recDetail.Fields("unit") &" </p></font></td>" & VbCrlf
		strBody = strBody & "<td align='center' valign='top' class='text'><font color='#000000'> - </td>" & VbCrlf
		strBody = strBody & "<td align='right' class='text' valign='top'><font color='#000000'><nobr>"& FormatNumber(intSubtotal,0) &" Baht</nobr></font></td>" & VbCrlf   
		strBody = strBody & "</tr>" & VbCrlf  
	End IF
	recDetail.MoveNext
Wend

strBody = strBody & "<tr bgcolor='#FFFFFF'> " & VbCrlf
strBody = strBody & "<td colspan='5' align='right' bgcolor='#FFFFFF'class='text'><b><font color='#FF6600'>Total: </font></b></td>" & VbCrlf        
strBody = strBody & "<td align='right' class='text'><b><font color='#FF6600'><nobr>"&  FormatNumber(intTotal,0)&" Baht</nobr></font></b></td>" & VbCrlf
strBody = strBody & "</tr>" & VbCrlf
strBody = strBody & "</table><br>" & VbCrlf
strBody = strBody & "</td>" &VbCrlf
strBody = strBody & "</tr>" &VbCrlf
strBody = strBody & "<tr>" &VbCrlf
strBody = strBody & "<td align='left' class='text'>If you need answer to any questions, please feel free to contact our customer service department <a href='http://www.hotels2thailand.com/thailand-hotels-contact.asp' target='_blank'>here</a>.<br>" &VbCrlf
strBody = strBody & "Thanks again for shopping at <a href='http://www.hotels2thailand.com'>Hotels2Thailand.com</a>" &VbCrlf
strBody = strBody & "<p>You can see hotel map by <a href='http://www.hotels2thailand.com/thailand-hotels-map.asp?id="& intProductID &"' target='_blank'><b>Click Here</b></a></p>" &VbCrlf
strBody = strBody & "<p> <font color='#FF6600'><b>Note: </b>Dont forget to print voucher and take to the hotel by <a href='"& strLink &"' target='_blank'><b>click here</b></a>.</font></p></td>" &VbCrlf
strBody = strBody & "</tr>" &VbCrlf
strBody = strBody & "<tr>" &VbCrlf
strBody = strBody & "<td align='left' class='text'>&nbsp;</td>" &VbCrlf
strBody = strBody & "</tr>" &VbCrlf
strBody = strBody & "</table></td>" &VbCrlf
strBody = strBody & "<td width='3%' background='image/l03.gif'><img src='http://www.hotels2thailand.com/image/spacer.gif' width='4' height='200'></td>" &VbCrlf
strBody = strBody & "</tr>" &VbCrlf
strBody = strBody & "</table>" &VbCrlf
strBody = strBody & "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" &VbCrlf
strBody = strBody & "<tr align='left' valign='top'>" &VbCrlf
strBody = strBody & "<td width='2%'><img src='http://www.hotels2thailand.com/image/spacer.gif' width='3' height='10'></td>" &VbCrlf
strBody = strBody & "<td width='65%' bgcolor='#A8DBFB'><img src='http://www.hotels2thailand.com/image/spacer.gif' width='600' height='3'></td>" &VbCrlf                
strBody = strBody & "<td width='33%'><img src='http://www.hotels2thailand.com/image/spacer.gif' width='3' height='8'></td>" &VbCrlf
strBody = strBody & "</tr>" &VbCrlf
strBody = strBody & "</table>" &VbCrlf
strBody = strBody & "</div></td>" &VbCrlf
strBody = strBody & "</tr>" &VbCrlf
strBody = strBody & "</table>" &VbCrlf
strBody = strBody & "</body>" &VbCrlf
strBody = strBody & "</html>" &VbCrlf               

recDetail.Close
Set recDetail = Nothing

SELECT CASE intType
	Case 1
		function_gen_mail_confirm = strBody
	Case 2
		function_gen_mail_confirm = strEmail
	Case 3
		function_gen_mail_confirm = ""
END SELECT

END FUNCTION
%>