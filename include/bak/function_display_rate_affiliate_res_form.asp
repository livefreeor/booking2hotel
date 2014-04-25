<%
FUNCTION function_display_rate_affiliate_res_form()

	Dim strForm
	Dim strLogo
	Dim strStep1
	Dim strStep2
	Dim strStep3
	Dim strStep4
	
	strLogo =  function_display_logo(Request.Cookies("site_id"),"","",11)
	strStep1 = function_display_rate_affiliate_res_step1()
	strStep2 = function_display_rate_affiliate_res_step2
	strStep3 = function_display_rate_affiliate_res_step3()
	strStep4 = function_display_rate_affiliate_res_step4()
	
	strForm = "<html>" & VbCrlf
	strForm = strForm & "<head>" & VbCrlf
	strForm = strForm & "<title>Complete Your Reservation With 4 Steps</title>" & VbCrlf
	strForm = strForm & "<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-1"">" & VbCrlf
	strForm = strForm & "<link rel=""stylesheet"" href=""/affiliate_include/css/21339.css"" type=""text/css"">" & VbCrlf
	strForm = strForm & "<script language=""JavaScript"" src=""/java/popup.js"" type=""text/javascript""></script>" & VbCrlf
    strForm = strForm & "<script language=""JavaScript"" src=""/affiliate_include/java/fnCheckReservation.js"" type=""text/javascript""></script>" & VbCrlf
	strForm = strForm & "</head>" & VbCrlf
	strForm = strForm & "<body background=""/images/bg_main.jpg"" bgcolor=""#FFFFFF"" marginheight=""0"" leftmargin=""0"" topmargin=""10"" marginwidth=""0"" >	" & VbCrlf
	strForm = strForm & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center"" id=""tbl_hotel_price"">" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>" & VbCrlf

	strForm = strForm & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#FFFFFF"">" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>" & strLogo &"</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</table>" & VbCrlf
	
	strForm = strForm & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"" bgcolor=""#FFFFFF"">" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>" & VbCrlf
	strForm = strForm & "&nbsp;<img src=""http://www.booking2hotels.com/images/hd_reservation.gif"" alt=""""> <br /><br />" & VbCrlf
	strForm = strForm & "</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</table>" & VbCrlf
	strForm = strForm & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"" bgcolor=""#FFFFFF"">" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>" & VbCrlf
	strForm = strForm & "<table width=""100%"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#C4DBFF"">" & VbCrlf
	strForm = strForm & "<form action="""" method=""post"" onSubmit=""return fnValidForm(this);"">" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td bgcolor=""#FFFFFF""><table width=""100%"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>" & VbCrlf
	strForm = strForm & "<p><font color=""#003366"">Reservation Information</font><BR>" & VbCrlf
	strForm = strForm & "&nbsp; <br />" & VbCrlf
	strForm = strForm & "Please complete the   form below to enable us to process your reservation. We guarantee that we will   not sell, trade, or rent your personal information to 3rd parties. <A href=""/thailand-hotels-privacy.asp"" target=""_blank"">Security &amp; Privacy policy</A><BR>" & VbCrlf
	strForm = strForm & "<br />" & VbCrlf
	strForm = strForm & "<strong>Fields marked with a red   asterisk</strong>(<strong><font color=""#FF0000"">*</font></strong>) <strong>are   required.</strong></p>" & VbCrlf
	strForm = strForm & "</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>"&strStep1&"</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>&nbsp;</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>"&strStep2&"</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>&nbsp;</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>" & VbCrlf
	strForm = strForm & strStep3 & VbCrlf
	strForm = strForm & "</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>&nbsp;</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>" & VbCrlf
	strForm = strForm & strStep4 & VbCrlf
	strForm = strForm & "</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>&nbsp;</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>&nbsp;</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</table></td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</form>" & VbCrlf
	strForm = strForm & "</table></td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</table>" & VbCrlf
	strForm = strForm & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"" bgcolor=""#FFFFFF"">" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td>&nbsp;</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</table>" & VbCrlf
	strForm = strForm & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"" bgcolor=""97BFEC"">" & VbCrlf
	strForm = strForm & "<tr>" & VbCrlf
	strForm = strForm & "<td height=""1""><img src=""http://www.booking2hotels.com/images/spacer.gif"" width=""1"" height=""1""></td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</table>" & VbCrlf
	strForm = strForm & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
	strForm = strForm & "<tr> " & VbCrlf
	strForm = strForm & "<td height=""24"" background=""/images/bg_bar.gif"" align=""center""><font color=""346494"">Copyright &copy; 1996-"&Year(Date)&" Hotels2Thailand.com. All rights reserved.</font></td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</table>" & VbCrlf
	strForm = strForm & "</td>" & VbCrlf
	strForm = strForm & "</tr>" & VbCrlf
	strForm = strForm & "</table>" & VbCrlf
	strForm = strForm & "</body>" & VbCrlf
	strForm = strForm & "</html>" & VbCrlf
	
	function_display_rate_affiliate_res_form = strForm
	
END FUNCTION
%>