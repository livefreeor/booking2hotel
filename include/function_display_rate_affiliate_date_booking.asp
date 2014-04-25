<%
FUNCTION function_display_rate_affiliate_date_booking(dateCheckIn,dateCheckOut,intPsID,intType)

	Dim strDate
	Dim strDateCheckIn
	Dim strDateCheckOut
	
	IF dateCheckIn = "" Then
		dateCheckIn = dateNextConstant 
		dateCheckOut = DateAdd("d",dateCheckIn,3)
	End IF
	
	strDateCheckIn = function_gen_dropdown_date(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),"ch_in_date","ch_in_month","ch_in_year",1)
	strDateCheckOut = function_gen_dropdown_date(Day(dateCheckOut),Month(dateCheckOut),Year(dateCheckOut),"ch_out_date","ch_out_month","ch_out_year",1)
	
	strDate = "<table width=""100%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#e4e4e4"">" & VbCrlf
	strDate = strDate & "<tr>" & VbCrlf
	strDate = strDate & "<td bgcolor=""#FFFFFF"">" & VbCrlf
	strDate = strDate & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" class=""f11"">" & VbCrlf
	strDate = strDate & "<tr>" & VbCrlf
	strDate = strDate & "<td height=""25"" class=""step_bg_color"">" & VbCrlf
	strDate = strDate & "<span class=""step_num"">3.)</span>&nbsp;<span class=""step_text"">Select Your Date</span>" & VbCrlf
	strDate = strDate & "</td>" & VbCrlf
	strDate = strDate & "</tr>" & VbCrlf
	strDate = strDate & "</table>" & VbCrlf
	strDate = strDate & "<table cellspacing=1 cellpadding=2 width=""60%"" class=""f11"">" & VbCrlf
	strDate = strDate & "<tbody>" & VbCrlf
	strDate = strDate & "<tr>" & VbCrlf
	strDate = strDate & "<td width=""30%"" class=""room_text"">Check-in:</td>" & VbCrlf
	strDate = strDate & "<td width=""70%"">" & VbCrlf
	strDate = strDate & strDateCheckIn & VbCrlf
	strDate = strDate & "</td>" & VbCrlf
	strDate = strDate & "</tr>" & VbCrlf
	strDate = strDate & "<tr>" & VbCrlf
	strDate = strDate & "<td class=""valid_text"">Check-out </font></td>" & VbCrlf
	strDate = strDate & "<td>" & VbCrlf
	strDate = strDate & strDateCheckOut & VbCrlf
	strDate = strDate & "</td>" & VbCrlf
	strDate = strDate & "</tr>" & VbCrlf
	strDate = strDate & "</tbody>" & VbCrlf
	strDate = strDate & "</table>" & VbCrlf
	strDate = strDate & "</td>" & VbCrlf
	strDate = strDate & "</tr>" & VbCrlf
	strDate = strDate & "</table>" & VbCrlf

	function_display_rate_affiliate_date_booking = strDate

END FUNCTION
%>