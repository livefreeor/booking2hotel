<%
FUNCTION function_search_error(intDestination,intLocation,intDayCheckin,intMonthCheckin,intYearCheckin,intDayCheckout,intMonthCheckout,intYearCheckout,intPriceMin,intPriceMax,strSort)
	
	Dim dateCheckIn
	Dim dateCheckOut
	Dim strQuery
	Dim dateCurrent
	
	strQuery = "destination="& intDestination &"&location="& intLocation &"&start_price="& intPriceMin &"&end_price="& intPriceMax &"&ch_in_date="& intDayCheckIn &"&ch_in_month="& intMonthCheckIn &"&ch_in_year="& intYearCheckIn &"&ch_out_date="& intDayCheckOut &"&ch_out_month="& intMonthCheckOut &"&ch_out_year="& intYearCheckOut &"&sort=" & strSort
	
	'### Blank Destination ###
	IF intDestination="" OR intDestination="none" Then
		Call connClose()
		Response.Redirect "default.asp?error=error01&" & strQuery
	End IF
	'### Blank Destination ###
	
	'### Error on Feb ###
	IF  Cint(intMonthCheckIn) = 2 Then
		IF Cint(intYearCheckIn) MOD 4 = 0 Then
			IF Cint(intDayCheckIn) > 29 Then
				Call connClose()
				Response.Redirect "default.asp?error=error02&" & strQuery
			End IF
		Else
			IF Cint(intDayCheckIn) > 28 Then
				Call connClose()
				Response.Redirect "default.asp?error=error03&" & strQuery
			End IF
		End IF
	End IF
	
	IF  Cint(intMonthCheckOut) = 2 Then
		IF Cint(intYearCheckOut) MOD 4 = 0 Then
			IF Cint(intDayCheckOut) > 29 Then
				Call connClose()
				Response.Redirect "default.asp?error=error04&" & strQuery
			End IF
		Else
			IF Cint(intDayCheckOut) > 28 Then
				Call connClose()
				Response.Redirect "default.asp?error=error05&" & strQuery
			End IF
		End IF
	End IF
	'### Error on Feb ###
	
	'### Error on 30 Day Month ###
	SELECT CASE intMonthCheckIn
		Case 4,6,9,11
			IF intDayCheckin>30 Then
				Call connClose()
				Response.Redirect "default.asp?error=error06&" & strQuery
			End IF
	END SELECT
	
	SELECT CASE intMonthCheckout
		Case 4,6,9,11
			IF intDayCheckOut>30 Then
				Call connClose()
				Response.Redirect "default.asp?error=error07&" & strQuery
			End IF
	END SELECT
	'### Error on 30 Day Month ###

	dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
	dateCheckOut = DateSerial(intYearCheckOut,intMonthCheckOut,intDayCheckOut)
	dateCurrent = DateSerial(Year(DateAdd("h",14,Now)),Month(DateAdd("h",14,Now)),Day(DateAdd("h",14,Now)))
	
	'### Date Check Out < Date Check In ###
	IF dateCheckOut <= dateCheckIn Then
		Call connClose()
		Response.Redirect "default.asp?error=error08&" & strQuery
	End IF
	'### Date Check Out < Date Check In ###
	
	'### Date Check In < Current Date ###
	IF dateCheckIn < dateCurrent Then
		Call connClose()
		Response.Redirect "default.asp?error=error09&" & strQuery
	End IF
	'### Date Check In < Current Date ###
	
END FUNCTION
%>