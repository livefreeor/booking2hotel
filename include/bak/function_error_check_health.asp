<%
FUNCTION function_error_check_health()

	Dim bolOption
	Dim strQueryOption
	Dim strOptionID
	Dim arrOptionID 
	Dim strAllOption
	Dim strQueryPeople
	Dim strQuery
	Dim strQueryFull
	Dim sqlProduct
	Dim recProduct
	Dim arrProduct
	Dim strRedirectPath
	Dim dateCheckIn
	Dim dateCurrent
	Dim intDayCheckIn
	Dim intMonthCheckIn
	Dim intYearCheckIn
	Dim intMaxAdult
	Dim intOption
	Dim sqlRateOut
	Dim recRateOut
	Dim arrRateOut
	Dim bolRateOut
	
	'### SET OPTION ID ###
	IF Request("option_id")<>"" Then
		bolOption = True
		strQueryOption = Replace(Request("option_id")," ","")  'Destroy Space
		strOptionID = strQueryOption
		arrOptionID = Split(strQueryOption,",")
		intOption = Ubound(arrOptionID)
		strQueryOption = "option_id=" & strQueryOption
		strAllOption = strOptionID
		
		For intCount=0 To Ubound(arrOptionID)
			IF Request("qty"&arrOPtionID(intCount))<>"" Then
				strQueryOption = strQueryOption & "&qty"&arrOPtionID(intCount) & "=" & Request("qty"&arrOPtionID(intCount))
			End IF
		Next
	Else
		bolOption = False
		strQueryOption = "option=blank"
		strOptionID = "0"
	End IF

	'### SET OPTION ID ###
	
	'### SET ADULT AND CHILDREN ###
	'strQueryPeople = "golfer=" & Request("golfer")
	'### SET ADULT AND CHILDREN ###
	
	'### SET QUERY STRING ###
	strQuery = "a156=ths"
	strQueryFull = strQuery  & "&product_id=" & Request("product_id") & "&ch_in_date=" & Request("ch_in_date") & "&ch_in_month=" & Request("ch_in_month") & "&ch_in_year=" & Request("ch_in_year")
	strQueryFull = Replace(strQueryFull,"blank","")
	'### SET QUERY STRING ###
	
	'### SET PRPDUCT DETAIL ###
	sqlProduct = "SELECT product_id,title_en,destination_id,files_name,allotment_type FROM tbl_product WHERE product_id=" & Request("product_id")
	Set recProduct = Server.CreateObject ("ADODB.Recordset")
	recProduct.Open SqlProduct, Conn,adOpenStatic,adLockreadOnly
		arrProduct = recProduct.GetRows()
	recProduct.Close
	Set recProduct = Nothing 
	'### SET PRPDUCT DETAIL ###
	
	'### SET REDIRECT PATH ###
	strRedirectPath = "/"& function_generate_health_check_up_link(arrProduct(2,0),"",1)& "/"& arrProduct(3,0) & "?" & strQuery
	'### SET REDIRECT PATH ###	
	'### Check Outing ###
	IF function_check_date_outing(1) Then
		Response.Redirect strRedirectPath & "&error=error09"
	End IF
	'### Check Outing ###
	
	'### ERROR ChHECK BLANK ###
	IF bolOption=FALSE Then
		Response.Redirect strRedirectPath & "&error=error10"
	End IF
	'### ERROR ChHECK BLANK ###
	
	'### ERROR CHECK DATE ###
	intDayCheckin = Request("ch_in_date")
	intMonthCheckin = Request("ch_in_month")
	intYearCheckin = Request("ch_in_year")
		'### Error on Feb ###
		IF  Cint(intMonthCheckIn) = 2 Then
			IF Cint(intYearCheckIn) MOD 4 = 0 Then
				IF Cint(intDayCheckIn) > 29 Then
					Response.Redirect strRedirectPath & "&error=error02"
				End IF
			Else
				IF Cint(intDayCheckIn) > 28 Then
					Response.Redirect strRedirectPath & "&error=error03"
				End IF
			End IF
		End IF
		'### Error on Feb ###
		
		'### Error on 30 Day Month ###
		SELECT CASE intMonthCheckIn
			Case 4,6,9,11
				IF intDayCheckin>30 Then
					Response.Redirect strRedirectPath & "&error=error06"
				End IF
		END SELECT
		'### Error on 30 Day Month ###
		
		dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
		dateCurrent = DateSerial(Year(DateAdd("h",14,Now)),Month(DateAdd("h",14,Now)),Day(DateAdd("h",14,Now)))

		'### Date Check In =< Current Date ###
		IF dateCheckIn =< dateCurrent Then
			Response.Redirect strRedirectPath & "&error=error09"
		End IF
		'### Date Check In < Current Date ###
		
		Call function_set_session_date_golf(intDayCheckin,intMonthCheckin,intYearCheckin)	
	'### ERROR CHECK DATE ###
	
	'### ERROR CHECK OUT OF RATE ###
	IF bolOption Then
		sqlRateOut = "SELECT option_id  "
		sqlRateOut = sqlRateOut & " FROM tbl_option_price"
		sqlRateOut = sqlRateOut & " WHERE option_id IN ("&Request("option_id")&") AND (date_start<="&function_date_sql(Day(dateCheckin),Month(dateCheckin),Year(dateCheckin),1)&" AND date_end>="&function_date_sql(Day(dateCheckin),Month(dateCheckin),Year(dateCheckin),1)&")"
		response.write sqlRateOut
		'response.end
		Set recRateOut = Server.CreateObject ("ADODB.Recordset")
		recRateOut.Open sqlRateOut, Conn,adOpenStatic,adLockreadOnly
			IF NOT recRateOut.EOF Then
				arrRateOut = recRateOut.GetRows()
				bolRateOut = True
			Else
				bolRateOut = False
			End IF
		recRateOut.Close
		Set recRateOut = Nothing 
		
		IF NOT bolRateOut Then
			Response.Redirect strRedirectPath & "&error=error14"
		End IF
		
		IF bolRateOut Then 'IF Number of Price < Number of Option (1 Option 1 Price)
			IF Ubound(arrRateOut,2)<intOption Then
				Response.Redirect strRedirectPath & "&error=error14"
			End IF
		End IF
	End IF
	
	
	'### ERROR CHECK OUT OF RATE ###
	
	
	
	'### ERROR CHECK MAX GOLFERS ###
'	arrOptionID = Split(strAllOption,",")
'	For intCount=0 To Ubound(arrOptionID)
'		intMaxAdult = intMaxAdult + Cint(Request("qty"&arrOptionID(intCount)))
'	Next
'		
'	IF Cint(Request("golfer")) > intMaxAdult Then
'		Response.Redirect strRedirectPath & "&error=error11"
'	End IF
	'### ERROR CHECK MAX GOLFERS ###
	
	function_error_check_health = strAllOption
	
END FUNCTION
%>