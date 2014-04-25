<%
FUNCTION inc_mail_reply (intOrderID,intType)

	Dim sqlOrder
	Dim recOrder
	Dim arrOrder
	Dim strQuery
	Dim strOnWeb
	Dim strContent
	Dim strBody
	
	sqlOrder = "SELECT o.full_name,"
	sqlOrder = sqlOrder & " (SELECT TOP 1 sp.product_id FROM tbl_product sp WHERE sp.product_cat_id=29 AND sp.product_id=po.product_id) AS product_id,"
	sqlOrder = sqlOrder & " (SELECT TOP 1 sp.title_en FROM tbl_product sp WHERE sp.product_cat_id=29 AND sp.product_id=po.product_id) AS product_title,"
	sqlOrder = sqlOrder & " (SELECT TOP 1 sot.date_check_in FROM tbl_order_item sot WHERE sot.order_id=o.order_id) AS date_check_in,"
	sqlOrder = sqlOrder & " (SELECT TOP 1 sot.date_check_out FROM tbl_order_item sot WHERE sot.order_id=o.order_id) AS date_check_out"
	sqlOrder = sqlOrder & " FROM tbl_order o,tbl_order_item ot,tbl_product_option po"
	sqlOrder = sqlOrder & " WHERE o.order_id=ot.order_id AND ot.option_id=po.option_id AND o.order_id=" & intOrderID
	Set recOrder = Server.CreateObject ("ADODB.Recordset")
	recOrder.Open SqlOrder, Conn,adOpenStatic,adLockreadOnly
		arrOrder = recOrder.GetRows()
	recOrder.Close
	Set recOrder = Nothing 

	SELECT CASE intType
		Case 1 'Full Review
			'strOnWeb = "If you can not read this email, please copy following url and paste on to your browser" & VbCrlf
			'strOnWeb = strOnWeb & "http://www.hotels2thailand.com/review/mail_review_full.asp?customer=14378&od=154"& intOrderID &"6958&type=1&review=true&pd=23784" & VbCrlf
			strContent = "<br/>" & VbCrlf
			strContent = strContent & "<p>"
			strContent = strContent & "<font color=""green""><strong>Dear "&arrOrder(0,0)&"</strong></font>,	</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "Thank you very much for using our service, <a href=""http://www.hotels2thailand.com"">www.hotels2thailand.com  </a>"& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "We received your enquiry for making the reservation at "&arrOrder(2,0)&" , check in "&function_date(arrOrder(3,0),5)&"   and check out "&function_date(arrOrder(4,0),5)&"  with many thanks. <strong>Your Booking ID:</strong> "& intOrderID & VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "Important: This email <strong>DOES NOT</strong> represent as booking confirmation. Our staff will contact you to confirm the room availability again within 12 hours."& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & " In case you have any other enquiries, please click <a href=""http://www.hotels2thailand.com/thailand-hotels-contact.asp"">Contact Us</a> and your booking ID is necessarily to insert for reference as well."& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "<font color=""green""><strong>Best Regards,<br>"& VbCrlf
			strContent = strContent & "<a href=""http://www.hotels2thailand.com""><font color=""green"">www.hotels2thailand.com</font></a>	</strong></font><strong>  </strong></p>"& VbCrlf
			
	END SELECT
	strBody = strOnWeb & VbCrlf
	strBody = strBody &  "<html>" & VbCrlf
	strBody = strBody & "<head>" & VbCrlf
	strBody = strBody & "<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-1"" />" & VbCrlf
	strBody = strBody & "<title>Please Write Review For Hotels2Thailand.com</title>" & VbCrlf
	strBody = strBody & "<link href=""http:www/hotels2thailand.com/css/css.css"" rel=""stylesheet"" type=""text/css"">" & VbCrlf
	strBody = strBody & "</head>" & VbCrlf
	strBody = strBody & "<body background=""http://www.booking2hotels.com/images/bg_main.jpg"" bgcolor=""#FFFFFF"" marginheight=""0"" leftmargin=""0"" topmargin=""10"" marginwidth=""0"" >" & VbCrlf
	strBody = strBody & "<div align=""center"">" & VbCrlf
	strBody = strBody & "<table width=""600"">" & VbCrlf
	strBody = strBody & "<tr>" & VbCrlf
	strBody = strBody & "<td>" & VbCrlf
	strBody = strBody & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
	strBody = strBody & "<tr>" & VbCrlf
	strBody = strBody & "<td width=""12""><img src=""http://www.booking2hotels.com/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
	strBody = strBody & "<td background=""http://www.booking2hotels.com/images/bg_h-1.gif"">" & VbCrlf
	strBody = strBody & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
	strBody = strBody & "<tr>" & VbCrlf
	strBody = strBody & "<td height=""57"" valign=""middle"">" & VbCrlf
	strBody = strBody & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
	strBody = strBody & "<tr>" & VbCrlf
	strBody = strBody & "<td width=""250""><a href=""http://www.hotels2thailand.com/""><img src=""http://www.booking2hotels.com/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt=""Thailand Hotels""></a></td>" & VbCrlf
	strBody = strBody & "<td align=""right"">&nbsp;</td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "</table></td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "<tr>" & VbCrlf
	strBody = strBody & "<td height=""25"" valign=""bottom"" align=""center"">" & VbCrlf
	strBody = strBody & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
	strBody = strBody & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
	strBody = strBody & "<td height=""24"" width=""9""><img src=""http://www.booking2hotels.com/images/b_blue_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
	strBody = strBody & "<td background=""http://www.booking2hotels.com/images/bg_b_blue.gif"" width=""60"" valign=""middle"" align=""center""><a href=""http://www.hotels2thailand.com/"" title=""Thailand Hotels Home""><font color=""346494""><b>Home</b></font></a></td>" & VbCrlf
	strBody = strBody & "<td width=""9""><img src=""http://www.booking2hotels.com/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
	strBody = strBody & "<td width=""70"" background=""http://www.booking2hotels.com/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""http://www.hotels2thailand.com/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
	strBody = strBody & "<td width=""9""><img src=""http://www.booking2hotels.com/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
	strBody = strBody & "<td width=""75"" background=""http://www.booking2hotels.com/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""http://www.hotels2thailand.com/thailand-hotels-travel.asp"" title=""Thailand Travel Guide""><font color=""FE5400""><b>Guides</b></font></a></td>" & VbCrlf
	strBody = strBody & "<td width=""9""><img src=""http://www.booking2hotels.com/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
	strBody = strBody & "<td width=""95"" background=""http://www.booking2hotels.com/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""http://www.hotels2thailand.com/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Top" & VbCrlf
	strBody = strBody & "Deals</b></font></a> </td>" & VbCrlf
	strBody = strBody & "<td width=""9""><img src=""http://www.booking2hotels.com/images/b_orange_R.gif"" width=""6"" height=""24""></td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "</table></td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "</table></td>" & VbCrlf
	strBody = strBody & "<td width=""12""><img src=""http://www.booking2hotels.com/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "</table>" & VbCrlf
	strBody = strBody & "<table width=""100%"" bgcolor=""#FFFFFF"">" & VbCrlf
	strBody = strBody & "<tr>" & VbCrlf
	strBody = strBody & "<td bgcolor=""#FFFFFF"" class=""mail"">" & VbCrlf
	strBody = strBody & strContent & VbCrlf
	strBody = strBody & "</td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "</table>" & VbCrlf
	strBody = strBody & "</td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "</table>" & VbCrlf
	strBody = strBody & "</div>" & VbCrlf
	strBody = strBody & "</body>" & VbCrlf
	strBody = strBody & "</html>" & VbCrlf

	inc_mail_reply = strBody
END FUNCTION
%>