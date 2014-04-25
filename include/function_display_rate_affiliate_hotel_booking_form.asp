<%
FUNCTION function_display_rate_affiliate_hotel_booking_form(intProductID,intPsID,intType)

	Dim strForm
	Dim strRate
	Dim strOption
	Dim strDate
	Dim strGuest
	Dim strRemark
	Dim sqlProduct
	Dim recProduct
	Dim arrProduct
	Dim sqlSite
	Dim recSite
	Dim arrSite
	Dim strCss
	
	sqlSite = "SELECT site_id,partner_id,bol_css_rate"
	sqlSite = sqlSite & " FROM tbl_aff_sites "
	sqlSite = sqlSite & " WHERE site_id=" & intPsID
	Set recSite = Server.CreateObject ("ADODB.Recordset")
	recSite.Open SqlSite, Conn,adOpenStatic,adLockreadOnly
		arrSite = recSite.GetRows()
	recSite.Close
	Set recSite = Nothing 
	
	IF arrSite(2,0) Then
		strCss = "http://www.hotels2thailand.com/affiliate_include/css/" & arrSite(1,0) & intPsID & ".css"
	Else '### Default Class ###
		strCss = "http://www.hotels2thailand.com/affiliate_include/css/21339.css"
	End IF
	
	strRate = function_display_rate_affiliate_hotel_booking(intProductID,intPsID,intType)

	IF strRate <>"" Then
	
		sqlProduct = "SELECT product_id,title_en,title_th,destination_id,"
		sqlProduct = sqlProduct & " (SELECT TOP 1 spl.location_id FROM tbl_product_location spl WHERE spl.product_id=p.product_id) AS location_id"
		sqlProduct = sqlProduct & " FROM tbl_product p"
		sqlProduct = sqlProduct & " WHERE product_id=" & intProductID
		Set recProduct = Server.CreateObject ("ADODB.Recordset")
		recProduct.Open SqlProduct, Conn,adOpenStatic,adLockreadOnly
			arrProduct = recProduct.GetRows()
		recProduct.Close
		Set recProduct = Nothing 

		strOption = function_display_rate_affiliate_option_booking(arrProduct(3,0),arrProduct(4,0),intProductID,intPsID,intType)
		strDate = function_display_rate_affiliate_date_booking("","",intPsID,intType)
		strGuest = function_display_rate_affiliate_guest(intPsID,intType)
		strRemark = function_display_rate_affiliate_remark_booking(intProductID,intPsID,intType)
		
		strForm= strForm & "<body>"
		strForm= strForm & "<script language=""javascript"" src=""http://www.hotels2thailand.com/affiliate_include/java/fnCheckBooking.js""></script>" & VbCrlf
		strForm= strForm & "<script language=""javaScript"" src=""http://www.hotels2thailand.com/java/popup.js"" type=""text/javascript""></script>" & VbCrlf
		strForm = strForm & "<link rel=""stylesheet"" href="""&strCss&""" type=""text/css"">" & VbCrlf
		strForm = strForm & "<div style=""width:710px"" id=""tbl_hotel_price"" align=""left"">" & VbCrlf
		strForm = strForm & "<fieldset>" & VbCrlf
		strForm = strForm & "<legend>Book "&arrProduct(1,0)&"</legend>" & VbCrlf
		strForm = strForm & "<form method=""post"" name=""form1"" action=""http://www.hotels2thailand.com/cart_add_pcs.asp"">" & VbCrlf
		strForm = strForm & "<input type=""hidden"" name=""product_id"" value="""&intProductID&""">" & VbCrlf
		strForm = strForm & "<input type=""hidden"" name=""psid"" value="""&intPsID&""">" & VbCrlf
		strForm = strForm & "<input type=""hidden"" name=""desid"" value="""&arrProduct(3,0)&""">" & VbCrlf
		strForm = strForm & "<input type=""hidden"" name=""booking_type"" value=""2"">" & VbCrlf
		strForm = strForm & "<input type=""hidden"" name=""product_cat_id"" value=""29"">" & VbCrlf
		strForm = strForm & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
		strForm = strForm & "<tr>" & VbCrlf
		strForm = strForm & "<td>" & VbCrlf
		strForm = strForm & "<br />" & VbCrlf
		strForm = strForm & strRate & VbCrlf
		strForm = strForm & "<br />" & VbCrlf
		strForm = strForm & "</td>" & VbCrlf
		strForm = strForm & "</tr>" & VbCrlf
		strForm = strForm & "<tr>" & VbCrlf
		strForm = strForm & "<td>" & VbCrlf
		strForm = strForm & strOption  & VbCrlf
		strForm = strForm & "<br />" & VbCrlf
		strForm = strForm & "</td>" & VbCrlf
		strForm = strForm & "</tr>" & VbCrlf
		strForm = strForm & "<tr>" & VbCrlf
		strForm = strForm & "<td>" & VbCrlf
		strForm = strForm & strDate & VbCrlf
		strForm = strForm & "<br />" & VbCrlf
		strForm = strForm & "</td>" & VbCrlf
		strForm = strForm & "</tr>" & VbCrlf
		strForm = strForm & "<tr>" & VbCrlf
		strForm = strForm & "<td>" & VbCrlf
		strForm = strForm & strGuest & VbCrlf
		strForm = strForm & "<br />" & VbCrlf
		strForm = strForm & "</td>" & VbCrlf
		strForm = strForm & "</tr>" & VbCrlf
		strForm = strForm & "<tr>" & VbCrlf
		strForm = strForm & "<td>&nbsp;</td>" & VbCrlf
		strForm = strForm & "</tr>" & VbCrlf
		strForm = strForm & "<tr>" & VbCrlf
		strForm = strForm & "<td align=""center"">" & VbCrlf
		strForm = strForm & "<input type=""button"" name=""btnClick"" onClick=""fnCheck()"" value=""Book""/>" & VbCrlf
		'strForm = strForm & "<img src=""http://www.booking2hotels.com/images/but_add_to_booking_list.gif"" name=""btnClick"" onClick=""fnCheck()"" value=""Book"">" & VbCrlf
		'strForm = strForm & "<input type=image src=""http://www.booking2hotels.com/images/but_add_to_booking_list.gif"" name=""btnClick"" onClick=""fnCheck()"" value=""Book"" />" & VbCrlf
		strForm = strForm & "</td>" & VbCrlf
		strForm = strForm & "</tr>" & VbCrlf
		strForm = strForm & "<tr>" & VbCrlf
		strForm = strForm & "<td>&nbsp; </td>" & VbCrlf
		strForm = strForm & "</tr>" & VbCrlf
		strForm = strForm & "<tr>" & VbCrlf
		strForm = strForm & "<td>" & VbCrlf
		strForm = strForm & strRemark & VbCrlf
		strForm = strForm & "</td>" & VbCrlf
		strForm = strForm & "</tr>" & VbCrlf
		strForm = strForm & "</table>" & VbCrlf
		strForm = strForm & "</form>" & VbCrlf
		strForm = strForm & "</fieldset>" & VbCrlf
		strForm = strForm & "</div>" & VbCrlf
		strForm= strForm & "</body>"
	Else
		strForm = strForm & "Sorry"
	End IF
	function_display_rate_affiliate_hotel_booking_form = strForm
	
END FUNCTION
%>
