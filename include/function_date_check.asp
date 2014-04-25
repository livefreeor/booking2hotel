<%
Function function_date_check(DateInput)

	Dim bolHoliday
	Dim bolWeekEnd
	Dim bolLong
	Dim arrHoliday
	Dim intHolCount
	Dim arrLong
	
	bolHoliday = False
	bolWeekEnd = False
	bolLong = False

	'### Cechk for weekend ###
	IF WeekDay(DateInput,VBMonday)=5 OR WeekDay(DateInput,VBMonday)=6 Then
		bolWeekEnd = True
	End IF
	'### Cechk for weekend ###
	
	'### Cechk for holiday ###
	arrHoliday = Array(DateSerial(2007,10,23),DateSerial(2007,12,5),DateSerial(2007,12,10),DateSerial(2007,12,31),DateSerial(2008,1,1))
	
	For intHolCount=0 To UBound(arrHoliday)
		IF arrHoliday(intHolCount) = DateInput Then
			bolHoliday = True
			Exit For
		End IF
	Next
	'### Cechk for holiday ###

	'### Check Long Weekend ###
	arrLong=Array(DateSerial(2007,12,8),DateSerial(2007,12,9),DateSerial(2007,12,10),DateSerial(2007,12,28),DateSerial(2007,12,29),DateSerial(2007,12,30),DateSerial(2007,12,31),DateSerial(2008,1,1))
	
	For intHolCount=0 To UBound(arrLong)
		IF arrLong(intHolCount) = DateInput Then
			bolLong = True
			Exit For
		End IF
	Next
	'### Check Long Weekend ###

	function_date_check = 1 'NormalDay

	IF bolWeekEnd Then
		function_date_check = 2 'WeekEnd
	End IF

	IF bolHoliday Then
		function_date_check = 3 'Holiday
	End IF
	
	IF bolWeekEnd AND bolHoliday Then 
		function_date_check = 4 'Holiday And Week End
	End IF
	
	IF bolLong Then
		function_date_check = 5 'Long Weekend
	End IF
End Function
%>