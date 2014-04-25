<%
FUNCTION function_error_check_water_activity()

	Dim strQuery
	Dim strQueryFull
	Dim strQueryPeople
	Dim strQueryPeriod
	Dim sqlProduct
	Dim recProduct
	Dim arrProduct
	Dim strRedirectPath
	Dim strErrorDate
	Dim dateCheckIn
	Dim dateCheclOut
	Dim strErrorRateOut
	Dim intWeekDay
	
	'### SET ADULT AND CHILDREN ###
	strQueryPeople = "adult=" & Request("adult") & "&children=" & Request("children")
	'### SET ADULT AND CHILDREN ###
	
	'### SET PERIOD ###
	strQueryPeriod = "period_id=" & Request("period_id")
	'### SET PERIOD ###

	'### SET QUERY STRING ###
	strQuery = strQueryPeriod & "&" & strQueryPeople
	strQueryFull = strQuery  & "&product_id=" & Request("product_id") & "&ch_in_date=" & Request("ch_in_date") & "&ch_in_month=" & Request("ch_in_month") & "&ch_in_year=" & Request("ch_in_year")
	strQueryFull = Replace(strQueryFull,"blank","")
	'### SET QUERY STRING ###
	
	'### SET PRPDUCT DETAIL ###
	sqlProduct = "SELECT product_id,title_en,destination_id,files_name,allotment_type,product_cat_id,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat FROM tbl_product WHERE product_id=" & Request("product_id")
	Set recProduct = Server.CreateObject ("ADODB.Recordset")
	recProduct.Open SqlProduct, Conn,adOpenStatic,adLockreadOnly
		arrProduct = recProduct.GetRows()
	recProduct.Close
	Set recProduct = Nothing 
	'### SET PRPDUCT DETAIL ###
	
	'### SET REDIRECT PATH ###
	strRedirectPath = "/"& function_generate_water_activity_link(arrProduct(2,0),"",1)& "/"& arrProduct(3,0) & "?" & strQueryFull
	'### SET REDIRECT PATH ###
	'### Check Outing ###
	IF function_check_date_outing(1) Then
		Response.Redirect strRedirectPath & "&error=error09"
	End IF
	'### Check Outing ###
	'### ERROR ON GENERAL DATE ###
	dateCheckIn = DateSerial(Request("ch_in_year"),Request("ch_in_month"),Request("ch_in_date"))
	dateCheckOut = DateAdd("d",1,dateCheckIn)
	strErrorDate = function_date_error_general(dateCheckIn,dateCheckOut,arrProduct(5,0))
	
	SELECT CASE strErrorDate
		Case "error01" 'Check in 30 on 29 Day (Feb)
			Response.Redirect strRedirectPath & "&error=error02"
		Case "error02" 'Check in 29 on 28 Day (Feb)
			Response.Redirect strRedirectPath & "&error=error03"
		Case "error03" 'Check out 30 on 29 Day (Feb)
			Response.Redirect strRedirectPath & "&error=error04"
		Case "error04" 'Check out 29 on 28 Day (Feb)
			Response.Redirect strRedirectPath & "&error=error05"
		Case "error05" 'Check in 31 on 30 day 
			Response.Redirect strRedirectPath & "&error=error06"
		Case "error06" 'Check out 31 on 30 day 
			Response.Redirect strRedirectPath & "&error=error07"
		Case "error07" 'Check in <= Current Date
			Response.Redirect strRedirectPath & "&error=error09"
		Case "error08" 'Check out <= check in
			Response.Redirect strRedirectPath & "&error=error08"
	END SELECT
	'### ERROR ON GENERAL DATE ###
	
	'### ERROR OUT OF RATE ###
	strErrorRateOut = function_date_error_rateout(dateCheckIn,dateCheckOut,Request("option_id"),arrProduct(5,0))
	IF strErrorRateOut Then
		Response.Redirect strRedirectPath & "&error=error14"
	End IF
	'### ERROR OUT OF RATE ###
	
	'### ERROR N UNTRIP DATE ###
	intWeekDay = WeekDay(dateCheckIn)
	SELECT CASE intWeekDay 
		Case 1 'Sunday
			IF NOT arrProduct(6,0) Then
				Response.Redirect strRedirectPath & "&error=error28"
			End IF
		Case 2 'Monday
			IF NOT arrProduct(7,0) Then
				Response.Redirect strRedirectPath & "&error=error28"
			End IF
		Case 3 'Tuesday
			IF NOT arrProduct(8,0) Then
				Response.Redirect strRedirectPath & "&error=error28"
			End IF
		Case 4 'Wednesday
			IF NOT arrProduct(9,0) Then
				Response.Redirect strRedirectPath & "&error=error28"
			End IF
		Case 5 'Thursday
			IF NOT arrProduct(10,0) Then
				Response.Redirect strRedirectPath & "&error=error28"
			End IF
		Case 6 'Friday
			IF NOT arrProduct(11,0) Then
				Response.Redirect strRedirectPath & "&error=error28"
			End IF
		Case 7 'Saturday
			IF NOT arrProduct(12,0) Then
				Response.Redirect strRedirectPath & "&error=error28"
			End IF
	END SELECT
	'### ERROR N UNTRIP DATE ###
	
	function_error_check_water_activity = Request("option_id")
	
END FUNCTION
%>