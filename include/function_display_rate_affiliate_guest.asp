<%
FUNCTION function_display_rate_affiliate_guest(intPsID,intType)
	
	Dim strGuest
	Dim strAdult
	Dim strChildren
	
	strAdult = function_gen_dropdown_number(1,20,1,"adult",4)
	strChildren = function_gen_dropdown_number(0,20,1,"children",4)
	
	strGuest = "<table width=""100%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#e4e4e4"">" & VbCrlf
	strGuest = strGuest & "<tr>" & VbCrlf
	strGuest = strGuest & "<td bgcolor=""#FFFFFF"">" & VbCrlf
	strGuest = strGuest & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" class=""f11"">" & VbCrlf
	strGuest = strGuest & "<tr>" & VbCrlf
	strGuest = strGuest & "<td height=""25"" class=""step_bg_color"">" & VbCrlf
	strGuest = strGuest & "<span class=""step_num"">4.)</span>&nbsp;<span class=""step_text"">Select Number of Guests</span>" & VbCrlf
	strGuest = strGuest & "</td>" & VbCrlf
	strGuest = strGuest & "</tr>" & VbCrlf
	strGuest = strGuest & "</table>" & VbCrlf
	strGuest = strGuest & "<table cellspacing=1 cellpadding=2 width=""40%"" class=""f11"">" & VbCrlf
	strGuest = strGuest & "<tbody>" & VbCrlf
	strGuest = strGuest & "<tr>" & VbCrlf
	strGuest = strGuest & "<td width=""54%""><font color=""434343"">Adult (age 12+): </font></td>" & VbCrlf
	strGuest = strGuest & "<td width=""46%"">	" & VbCrlf
	strGuest = strGuest & strAdult & VbCrlf	
	strGuest = strGuest & "</td>" & VbCrlf
	strGuest = strGuest & "</tr>" & VbCrlf
	strGuest = strGuest & "<tr>" & VbCrlf
	strGuest = strGuest & "<td><font color=""434343"">Children (0-12) </font></td>" & VbCrlf
	strGuest = strGuest & "<td>" & VbCrlf
	strGuest = strGuest & strChildren & VbCrlf	
	strGuest = strGuest & "</td>" & VbCrlf
	strGuest = strGuest & "</tr>" & VbCrlf
	strGuest = strGuest & "</tbody>" & VbCrlf
	strGuest = strGuest & "</table>" & VbCrlf
	strGuest = strGuest & "</td>" & VbCrlf
	strGuest = strGuest & "</tr>" & VbCrlf
	strGuest = strGuest & "</table>" & VbCrlf             

	function_display_rate_affiliate_guest = strGuest
END FUNCTION
%>


