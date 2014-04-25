<%
Function function_cart_check_date(intDayIn,intMonthIn,intYearIn,intHourIn,intMinIn,intDayOut,intMonthOut,intYearOut,intHourOut,intMinOut,intProductCatID,intCartProductID)
	
	Dim bolDateError
	Dim strError
	Dim dateTimeIn
	Dim dateTimeOut
	Dim sqlPriceAvai
	Dim recPriceAvai
	Dim intPriceAvai
	Dim sqlPriceSelect
	Dim recPriceSelect
	Dim intPriceSelect
	
	bolDateError = False
	strError = ""
	
	'### Error on Feb ###
	IF  Cint(intMonthIn) = 2 Then
		IF Cint(intYearIn) MOD 4 = 0 Then
			IF Cint(intDayIn) > 29 Then
				strError = "error04"
				bolError = True
			End IF
		Else
			IF Cint(intDayIn) > 28 Then
				strError = "error04"
				bolError = True
			End IF
		End IF
	End IF
	
	IF  Cint(intMonthOut) = 2 Then
		IF Cint(intYearOut) MOD 4 = 0 Then
			IF Cint(intDayOut) > 29 Then
				strError = "error05"
				bolError = True
			End IF
		Else
			IF Cint(intDayOut) > 28 Then
				strError = "error05"
				bolError = True
			End IF
		End IF
	End IF
	'### Error on Feb ###

	'### Error on 30 Day Month ###
	IF NOT bolError Then
		SELECT CASE intMonthIn
			Case 4,6,9,11
				IF intDayIn>30 Then
				strError = "error06"
				bolError = True
				End IF
		END SELECT
		
		SELECT CASE intMonthOut
			Case 4,6,9,11
				IF intDayOut>30 Then
				strError = "error07"
				bolError = True
				End IF
		END SELECT
	End IF
	'### Error on 30 Day Month ###
	
	'### Check in and Check out Error ###
	IF NOT bolError Then
		dateTimeIn = DateSerial(intYearIn,intMonthIn,intDayIn)
		dateTimeOut = DateSerial(intYearOut,intMonthOut,intDayOut)
		
		SELECT CASE intProductCatID
			Case 29 'Hotel
				IF dateTimeIn =< dateCurrentConstant Then
					boldateError = True
					strError = "error02"
				ElseIF dateTimeIn >= dateTimeOut Then
					boldateError = True
					strError = "error03"
				End IF
			Case 30 'Air Tricket
			Case 31 'Car Rent 
				IF dateTimeIn <= dateCurrentConstant Then
					boldateError = True
					strError = "error02"
				ElseIF dateTimeIn > dateTimeOut Then
					boldateError = True
					strError = "error03"
				End IF
			Case 32 'Goft Course
				IF dateTimeIn =< dateCurrentConstant Then
					boldateError = True
					strError = "error02"
				End IF
				
			Case 33 'Tour Package
		END SELECT
	
	End IF
	'### Check in and Check out Error ###

	'### Check Out of Date ###
	IF strError="" Then
	
		SELECT CASE intProductCatID
			Case 29 'Hotel
				sqlPriceAvai = "SELECT COUNT(op.price_id) AS num_price_avai"
				sqlPriceAvai = sqlPriceAvai & " FROM tbl_option_price op"
				sqlPriceAvai = sqlPriceAvai & " WHERE op.date_end>="&function_date_sql(intDayOut,intMonthOut,intYearOut,1)&" AND op.option_id IN "
				sqlPriceAvai = sqlPriceAvai & " ("
				sqlPriceAvai = sqlPriceAvai & " SELECT sci.option_id FROM tbl_cart_item sci WHERE sci.cart_product_id=" & intCartProductID
				sqlPriceAvai = sqlPriceAvai & " )"
			Case Else
				sqlPriceAvai = "SELECT COUNT(op.price_id) AS num_price_avai"
				sqlPriceAvai = sqlPriceAvai & " FROM tbl_option_price op"
				sqlPriceAvai = sqlPriceAvai & " WHERE op.date_end>="&function_date_sql(intDayIn,intMonthIn,intYearIn,1)&" AND op.option_id IN "
				sqlPriceAvai = sqlPriceAvai & " ("
				sqlPriceAvai = sqlPriceAvai & " SELECT sci.option_id FROM tbl_cart_item sci WHERE sci.cart_product_id=" & intCartProductID
				sqlPriceAvai = sqlPriceAvai & " )"
		END SELECT
		
		Set recPriceAvai = Server.CreateObject ("ADODB.Recordset")
		recPriceAvai.Open sqlPriceAvai, Conn,adOpenForwardOnly,adLockReadOnly
			intPriceAvai = recPriceAvai.Fields("num_price_avai")
		recPriceAvai.Close
		Set recPriceAvai = Nothing
	
		sqlPriceSelect = "SELECT COUNT(ci.option_id) AS num_price_select "
		sqlPriceSelect = sqlPriceSelect & " FROM tbl_cart_item ci "
		sqlPriceSelect = sqlPriceSelect & " WHERE ci.cart_product_id=" & intCartProductID
		Set recPriceSelect = Server.CreateObject ("ADODB.Recordset")
		recPriceSelect.Open sqlPriceSelect, Conn,adOpenForwardOnly,adLockReadOnly
			intPriceSelect = recPriceSelect.Fields("num_price_select")
		recPriceSelect.Close
		Set recPriceSelect = Nothing
		
		IF intPriceAvai<intPriceSelect Then
			strError = "error14"
		End IF
	End IF
	'### Check Out of Date ###

	function_cart_check_date = strError
End Function
%>