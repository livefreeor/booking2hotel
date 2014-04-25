<%
FUNCTION function_search_error_golf(intDestination,intDayCheckin,intMonthCheckin,intYearCheckin)
	
	Dim dateCheckIn
	Dim dateCheckOut
	Dim strQuery
	Dim dateCurrent
	
	strQuery = "destination="& intDestination &"&ch_in_date="& intDayCheckIn &"&ch_in_month="& intMonthCheckIn &"&ch_in_year="& intYearCheckIn 
	
	'### Blank Destination ###
	IF intDestination="" OR intDestination="none" OR Cstr(intDestination)="0"Then
		Response.Redirect "thailand-golf-courses.asp?error=error01&" & strQuery
	End IF
	'### Blank Destination ###
	
	'### Error on Feb ###
	IF  Cint(intMonthCheckIn) = 2 Then
		IF Cint(intYearCheckIn) MOD 4 = 0 Then
			IF Cint(intDayCheckIn) > 29 Then
				Response.Redirect "thailand-golf-courses.asp?error=error02&" & strQuery
			End IF
		Else
			IF Cint(intDayCheckIn) > 28 Then
				Response.Redirect "thailand-golf-courses.asp?error=error03&" & strQuery
			End IF
		End IF
	End IF
	'### Error on Feb ###
	
	'### Error on 30 Day Month ###
	SELECT CASE intMonthCheckIn
		Case 4,6,9,11
			IF intDayCheckin>30 Then
				Response.Redirect "thailand-golf-courses.asp?error=error06&" & strQuery
			End IF
	END SELECT

	dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
	dateCurrent = DateSerial(Year(DateAdd("h",14,Now)),Month(DateAdd("h",14,Now)),Day(DateAdd("h",14,Now)))
	
	'### Date Check In < Current Date ###
	IF dateCheckIn < dateCurrent Then
		Response.Redirect "thailand-golf-courses.asp?error=error09&" & strQuery
	End IF
	'### Date Check In < Current Date ###
	
END FUNCTION
%>