<%
FUNCTION function_date_error_rateout(dateCheckIn,dateCheckOut,strOptionID,intProductCatID)

	Dim sqlRateOut
	Dim recRateOut
	Dim arrRateOut
	Dim bolCheckOut
	Dim bolRateOut
	Dim bolError
	Dim arrOption
	Dim intOption
	
	bolCheckOut = False
	bolError = False
	arrOption = Split(strOptionID,",")
	intOption = Ubound(arrOption)
	
	SELECT CASE intProductCatID
		Case 29 'Hotel
			bolCheckOut = True
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
	
	'### Check For Check In ###
	sqlRateOut = "SELECT option_id  "
	sqlRateOut = sqlRateOut & " FROM tbl_option_price"
	sqlRateOut = sqlRateOut & " WHERE option_id IN ("&Request("option_id")&") AND (date_start<="&function_date_sql(Day(dateCheckin),Month(dateCheckin),Year(dateCheckin),1)&" AND date_end>="&function_date_sql(Day(dateCheckin),Month(dateCheckin),Year(dateCheckin),1)&")"
	Set recRateOut = Server.CreateObject ("ADODB.Recordset")
	recRateOut.Open sqlRateOut, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRateOut.EOF Then
			arrRateOut = recRateOut.GetRows()
			bolRateOut = True
		Else
			bolRateOut = False
			bolError = True
		End IF
	recRateOut.Close
	Set recRateOut = Nothing 
	
	IF bolRateOut Then 'IF Number of Price < Number of Option (1 Option 1 Price)
		IF Ubound(arrRateOut,2)<intOption Then
			bolError = True
		End IF
	End IF
	'### Check For Check In ###
	

	'### Check For Check Out ###
	IF bolCheckOut Then
		sqlRateOut = "SELECT option_id  "
		sqlRateOut = sqlRateOut & " FROM tbl_option_price"
		sqlRateOut = sqlRateOut & " WHERE option_id IN ("&Request("option_id")&") AND (date_start<="&function_date_sql(Day(dateCheckOut),Month(dateCheckOut),Year(dateCheckOut),1)&" AND date_end>="&function_date_sql(Day(dateCheckOut),Month(dateCheckOut),Year(dateCheckOut),1)&")"
		Set recRateOut = Server.CreateObject ("ADODB.Recordset")
		recRateOut.Open sqlRateOut, Conn,adOpenStatic,adLockreadOnly
			IF NOT recRateOut.EOF Then
				arrRateOut = recRateOut.GetRows()
				bolRateOut = True
			Else
				bolRateOut = False
				bolError = True
			End IF
		recRateOut.Close
		Set recRateOut = Nothing 
		
		IF bolRateOut Then 'IF Number of Price < Number of Option (1 Option 1 Price)
			IF Ubound(arrRateOut,2)<intOption Then
				bolError = True
			End IF
		End IF
	End IF
	'### Check For Check Out ###
	
	function_date_error_rateout = bolError
	
END FUNCTION
%>