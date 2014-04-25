<%
FUNCTION function_check_airport(intProductID,strAirportID,intDayCheckin,intMonthCheckin,intYearCheckin,intDayCheckout,intMonthCheckout,intYearCheckout,intType)

	Dim bolAirport
	Dim sqlFree
	Dim recFree
	
	bolAirport = False

	'IF strAirportID <> "" Then
		'bolAirport = True
	'End IF

	IF Cstr(strAirportID)="31" Then
		bolAirport = True
	End IF

	sqlFree = "SELECT TOP 1 *"
	sqlFree = sqlFree & " FROM tbl_product_option o, tbl_option_price op"
	sqlFree = sqlFree & " WHERE o.status=1 AND o.option_id=op.option_id AND o.option_cat_id=44 AND op.price=0 AND op.date_start<=" & function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1) & " AND op.date_end>=" & function_date_sql(intDayCheckout,intMonthCheckout,intYearCheckout,1) & " AND o.product_id=" & intProductID
	Set recFree = Server.CreateObject ("ADODB.Recordset")
	recFree.Open sqlFree, Conn,adOpenStatic,adLockreadOnly
		IF NOT recFree.EOF Then
			bolAirport = True
		End IF
	recFree.Close
	Set recFree = Nothing 
	
	function_check_airport = bolAirport
	
END FUNCTION
%>