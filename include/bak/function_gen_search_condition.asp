<%
FUNCTION function_gen_search_condition (intDestination,intlocation,intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,intPriceMin,intPriceMax,intNight,intType)
	
	Dim strDestination
	Dim strLocation
	Dim strPriceMin
	Dim strPriceMax
	Dim strCheckIn
	Dim strCheckOut
	Dim strNight
	
	strDestination = function_generate_hotel_link(intDestination,intLocation,2)
	
	IF Cstr(intLocation)="0" OR Cstr(intLocation)="none" OR Cstr(intLocation)="" Then
		strLocation = "Any"
	Else
		strLocation = function_generate_hotel_link(intDestination,intLocation,3)
	End IF
	
	IF Cstr(intPriceMin)="400" Then
		strPriceMin = "Any"
	Else
		strPriceMin = FormatNumber(Cint(intPriceMin),0)
	End IF
	
	IF Cstr(intPriceMax)="510000" Then
		strPriceMax = "Any"
	ElseIF Cstr(intPriceMax)="500000" Then
		strPriceMax = "Over 4,000"
	Else
		strPriceMax = FormatNumber(Cint(intPriceMax),0)
	End IF
	
	strCheckIn = function_date(DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn),1)
	strCheckOut = function_date(DateSerial(intYearCheckOut,intMonthCheckOut,intDayCheckOut),1)
	
	IF Cint(intNight)>1 Then
		strNight = intNight & " Nights"
	Else
		strNight = intNight & " Night"
	End IF
	
	function_gen_search_condition = "<table width=""100%""  border=""0"" cellpadding=""1"" cellspacing=""1"" bgcolor=""#F4F4F4"" class=""s"">" & VbCrlf
	function_gen_search_condition = function_gen_search_condition & "<tr align=""center"" bgcolor=""#FFFDF9"">" & VbCrlf
	function_gen_search_condition = function_gen_search_condition & "<td bgcolor=""#FFFDF9""><b><font color=""#666666"">Location:</font></b><br>"& strDestination &" &gt; "& strLocation &" </td>" & VbCrlf
	function_gen_search_condition = function_gen_search_condition & "<td><b><font color=""#666666"">Price Range:</font></b><br>"& strPriceMin &" - "& strPriceMax &" Baht</td>" & VbCrlf
	function_gen_search_condition = function_gen_search_condition & "<td><b><font color=""#666666"">Date:</font></b><br>"& strCheckIn &"-"& strCheckOut &" ("& strNight &")</td>" & VbCrlf
	function_gen_search_condition = function_gen_search_condition & "</tr></table>" & VbCrlf

END FUNCTION
%>






