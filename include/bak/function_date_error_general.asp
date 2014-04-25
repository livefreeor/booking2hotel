<%
FUNCTION function_date_error_general(dateCheckIn,dateCheckOut,intProductCatID)

	Dim intDayCheckin
	Dim intMonthCheckin
	Dim intYearCheckin
	Dim intDayCheckout
	Dim intMonthCheckout
	Dim intYearCheckout
	Dim strError
	Dim dateCurrent
	
	intDayCheckin = Request("ch_in_date")
	intMonthCheckin = Request("ch_in_month")
	intYearCheckin = Request("ch_in_year")
	intDayCheckout = Request("ch_out_date")
	intMonthCheckout = Request("ch_out_month")
	intYearCheckout = Request("ch_out_year")
	dateCurrent = DateSerial(Year(DateAdd("h",14,Now)),Month(DateAdd("h",14,Now)),Day(DateAdd("h",14,Now)))
	'### Error on Feb ###
		IF  Cint(intMonthCheckIn) = 2 Then
			IF Cint(intYearCheckIn) MOD 4 = 0 Then
				IF Cint(intDayCheckIn) > 29 Then
					strError = "error01"
				End IF
			Else
				IF Cint(intDayCheckIn) > 28 Then
					strError = "error02"
				End IF
			End IF
		End IF
		
		IF  Cint(intMonthCheckOut) = 2 Then
			IF Cint(intYearCheckOut) MOD 4 = 0 Then
				IF Cint(intDayCheckOut) > 29 Then
					strError = "error03"
				End IF
			Else
				IF Cint(intDayCheckOut) > 28 Then
					strError = "error04"
				End IF
			End IF
		End IF
	'### Error on Feb ###
	
	'### Error on 30 Day Month ###
	SELECT CASE intMonthCheckIn
		Case 4,6,9,11
			IF intDayCheckin>30 Then
				strError = "error05"
			End IF
	END SELECT
	
	SELECT CASE intMonthCheckout
		Case 4,6,9,11
			IF intDayCheckOut>30 Then
				strError = "error06"
			End IF
	END SELECT
	'### Error on 30 Day Month ###
			
	'### Date Check In < Current Date ###
	IF dateCheckIn <= dateCurrent Then
		strError = "error07"
	End IF
	'### Date Check In < Current Date ###

	
	SELECT CASE intProductCatID
		Case 29 'Hotel
			'### Check in> Check out ###
			IF dateCheckOut <= dateCheckIn Then
				strError = "error08"
			End IF
			'### Check in> Check out ###
			
		Case 30 'Air Tricket
		Case 31 'Airport Transfer
		Case 32 'Golf Courses
		Case 33 'Tour Package
		Case 34 'Sightseeing
		Case 35 'Adventure
		Case 36 'Water Activity
		Case 37 'Restaurant
		Case 38 'Show
		
	END SELECT

	function_date_error_general = strError

END FUNCTION
%>