<%
FUNCTION function_date(dateInput,intDisplayType)

	Dim arrMonth
	Dim arrMonthSemi
	Dim arrDay
	Dim arrDaySemi
	Dim strYear
	Dim strYearSemi
	
	arrMonth = array("","January","February","March","April","May","June","July","August","September","October","November","December")
	arrMonthSemi = array("","Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec")
	arrDay = array("","Sunday","Monday","Tuesday","Wendsday","Thursday","Friday","Saturday")
	arrDaySemi = array("","Sun","Mon","Tue","Wed","Thu","Fri","Sat")
	strYear = Year(dateInput)
	strYearSemi = Right(strYear,2)
	
	IF NOT (ISNULL(dateInput)  AND dateInput<>"") Then
		SELECT CASE intDisplayType
			Case 1 'Jan 25
				function_date = arrMonthSemi(Month(dateInput)) & " " & Day(dateInput)
			Case 2 'Sun
				function_date = arrDaySemi(WeekDay(dateInput))
			Case 3 'Jan. 25, 05
				function_date = arrMonthSemi(Month(dateInput)) & ". " & Day(dateInput) & ", " & strYearSemi
			Case 4 '8:05 January 25, 05
				function_date = FormatDateTime(dateInput,4) & " " & arrMonth(Month(dateInput)) & " " & Day(dateInput) & ", " & strYearSemi
			Case 5 'January 25, 2005
				function_date = arrMonth(Month(dateInput)) & " " & Day(dateInput) & ", " & strYear
			Case 6 '2005-09-20
				function_date = strYear & "-" & Month(dateInput) & "-" & Day(dateInput)
			Case 7 '8:05
				function_date = FormatDateTime(dateInput,4)
			Case 8 '25 Jan. 05
				function_date = Day(dateInput) & " " & arrMonthSemi(Month(dateInput)) & ". " & strYearSemi
			Case 9 'Sunday Jan 17, 05
				function_date = arrDay(WeekDay(dateInput)) & " " & arrMonthSemi(Month(dateInput)) & " " & Day(dateInput)  & ", " & strYearSemi
			Case 10 'Sun Jan 17, 05
				function_date = arrDaySemi(WeekDay(dateInput)) & " " & arrMonthSemi(Month(dateInput)) & " " & Day(dateInput)  & ", " & strYearSemi
			Case 11 'January  2005
				function_date = arrMonth(Month(dateInput)) & "  " & strYear
		END SELECT
	Else
		function_date = "N/A"
	End IF
END FUNCTION
%>