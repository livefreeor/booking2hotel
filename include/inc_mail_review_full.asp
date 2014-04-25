<%
FUNCTION inc_mail_review_full (intOrderID,intCusID,intType)

	Dim sqlOrder
	Dim recOrder
	Dim arrOrder
	Dim strQuery
	Dim strOnWeb
	Dim strContent
	
	sqlOrder = "SELECT full_name,"
	sqlOrder = sqlOrder & " (SELECT TOP 1 sp.product_id FROM tbl_product sp WHERE sp.product_cat_id=29 AND sp.product_id=po.product_id) AS product_id,"
	sqlOrder = sqlOrder & " (SELECT TOP 1 sp.title_en FROM tbl_product sp WHERE sp.product_cat_id=29 AND sp.product_id=po.product_id) AS product_title"
	sqlOrder = sqlOrder & " FROM tbl_order o,tbl_order_item ot,tbl_product_option po"
	sqlOrder = sqlOrder & " WHERE o.order_id=ot.order_id AND ot.option_id=po.option_id AND o.order_id=" & intOrderID
	Set recOrder = Server.CreateObject ("ADODB.Recordset")
	recOrder.Open SqlOrder, Conn,adOpenStatic,adLockreadOnly
		arrOrder = recOrder.GetRows()
	recOrder.Close
	Set recOrder = Nothing 

	SELECT CASE intType
		Case 1 'Full Review
			strOnWeb = "If you can not read this email, please copy following url and paste on to your browser" & VbCrlf
			strOnWeb = strOnWeb & "http://www.hotels2thailand.com/review/mail_review_full.asp?customer=14378&od=154"& intOrderID &"6958&type=1&review=true&pd=23784" & VbCrlf
			strContent = "<br/>" & VbCrlf
			strContent = strContent & "<p>"
			strContent = strContent & "<font color=""green""><strong>Dear "&arrOrder(0,0)&"</strong></font>,	</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "&emsp;Thank you for choosing to use <a href=""http://www.hotels2thailand.com"">www.hotels2thailand.com  </a>"& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "&emsp;As valued customer, your opinions on all aspects of your stay are very important to us. We would be grateful if you would complete this questionnaire and send us back in order to improve our service to all customer's satisfaction.	"& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "&emsp;<a href=""http://www.hotels2thailand.com/review/full_review.asp?od=154"&intOrderID&"85478&cus_id="&intCusID&""" target=""_blank""><font color=""#0000FF"">Please click here to start your review</font></a>"& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "&emsp;Please note that you do not have to answer all the questions and we will keep your detail private."& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "<font color=""green""><strong>Best Regards,<br>"& VbCrlf
			strContent = strContent & "<a href=""http://www.hotels2thailand.com""><font color=""green"">www.hotels2thailand.com</font></a>	</strong></font><strong>  </strong></p>"& VbCrlf

		Case 2 'Hotel Review
			strOnWeb = "If you can not read this email, please copy following url and paste on to your browser" & VbCrlf
			strOnWeb = strOnWeb & "http://www.hotels2thailand.com/review_write.asp?id="&arrOrder(1,0)& VbCrlf
			strContent = "<br/>" & VbCrlf
			strContent = strContent & "<p>"
			strContent = strContent & "<font color=""green""><strong>Dear "&arrOrder(0,0)&"</strong></font>,	</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "&emsp;Thank you for choosing to use <a href=""http://www.hotels2thailand.com"">www.hotels2thailand.com  </a>"& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "&emsp;As valued customer, your opinions on all aspects of your stay at "&arrOrder(2,0)&" are very important to us. Please write your review for further hotels' service  improvement continuously. "& VbCrlf
			strContent = strContent & "</p>"& VbCrlf
			strContent = strContent & "<p>"& VbCrlf
			strContent = strContent & "&emsp;<a href=""http://www.hotels2thailand.com/review_write.asp?id="&arrOrder(1,0)&"&cus_id="&intCusID&""" target=""_blank""><font color=""#0000FF"">Please click here to start your review</font></a>"& VbCrlf
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

	inc_mail_review_full = strBody
END FUNCTION
%>