<%
FUNCTION function_cart_product_check_trip_date(intProductID,dateCheckIn,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat,intType)

	Dim intWeekday
	Dim bolErrorSub
	
	bolErrorSub = False

	SELECT CASE intType
		Case 1 'Input with date check in and bol day
			intWeekday = WeekDay(dateCheckIn)
		Case 2
	END SELECT
	
	SELECT CASE intWeekDay 
		Case 1 'Sunday
			IF NOT day_sun Then
				bolErrorSub = True
			End IF
		Case 2 'Monday
			IF NOT day_mon Then
				bolErrorSub = True
			End IF
		Case 3 'Tuesday
			IF NOT day_tue Then
				bolErrorSub = True
			End IF
		Case 4 'Wednesday
			IF NOT day_wed Then
				bolErrorSub = True
			End IF
		Case 5 'Thursday
			IF NOT day_thu Then
				bolErrorSub = True
			End IF
		Case 6 'Friday
			IF NOT day_fri Then
				bolErrorSub = True
			End IF
		Case 7 'Saturday
			IF NOT day_sat Then
				bolErrorSub = True
			End IF
	END SELECT
	
	function_cart_product_check_trip_date = bolErrorSub
	
END FUNCTION
%>