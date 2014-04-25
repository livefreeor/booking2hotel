<%
Function functoin_date_check(DateInput)

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
	arrHoliday = Array(DateSerial(2010,01,01),DateSerial(2010,02,28),DateSerial(2010,03,01),DateSerial(2010,04,06),DateSerial(2010,04,13),DateSerial(2010,04,14),DateSerial(2010,04,15),DateSerial(2010,05,01),DateSerial(2010,05,03),DateSerial(2010,05,05),DateSerial(2010,05,28),DateSerial(2010,07,01),DateSerial(2010,07,26),DateSerial(2010,08,12),DateSerial(2010,10,23),DateSerial(2010,10,25),DateSerial(2010,12,5),DateSerial(2010,12,06),DateSerial(2010,12,10),DateSerial(2010,12,31),DateSerial(2011,01,01),DateSerial(2011,01,03),DateSerial(2011,02,03),DateSerial(2011,02,18),DateSerial(2011,04,06),DateSerial(2011,04,13),DateSerial(2011,04,14),DateSerial(2011,04,15),DateSerial(2011,05,01),DateSerial(2011,05,02),DateSerial(2011,05,05),DateSerial(2011,05,17),DateSerial(2011,07,15),DateSerial(2011,08,12),DateSerial(2011,10,23),DateSerial(2011,10,24),DateSerial(2011,11,10),DateSerial(2011,12,05),DateSerial(2011,12,10),DateSerial(2011,12,12),DateSerial(2011,12,31))
	
	For intHolCount=0 To UBound(arrHoliday)
		IF arrHoliday(intHolCount) = DateInput Then
			bolHoliday = True
			Exit For
		End IF
	Next
	'### Cechk for holiday ###

	'### Check Long Weekend ###
	arrLong=Array(DateSerial(2010,01,01),DateSerial(2010,01,02),DateSerial(2010,01,03),DateSerial(2010,02,12),DateSerial(2010,02,13),DateSerial(2010,02,14),DateSerial(2010,02,26),DateSerial(2010,02,27),DateSerial(2010,02,28),DateSerial(2010,03,01),DateSerial(2010,04,10),DateSerial(2010,04,11),DateSerial(2010,04,12),DateSerial(2010,04,13),DateSerial(2010,04,14),DateSerial(2010,04,15),DateSerial(2010,04,16),DateSerial(2010,04,17),DateSerial(2010,04,18),DateSerial(2010,05,01),DateSerial(2010,05,02),DateSerial(2010,05,03),DateSerial(2010,05,04),DateSerial(2010,05,05),DateSerial(2010,05,28),DateSerial(2010,05,29),DateSerial(2010,05,30),DateSerial(2010,07,24),DateSerial(2010,07,25),DateSerial(2010,07,26),DateSerial(2010,08,12),DateSerial(2010,08,13),DateSerial(2010,08,14),DateSerial(2010,08,15),DateSerial(2010,10,23),DateSerial(2010,10,24),DateSerial(2010,10,25),DateSerial(2010,11,20),DateSerial(2010,11,21),DateSerial(2010,12,04),DateSerial(2010,12,05),DateSerial(2010,12,06),DateSerial(2010,12,10),DateSerial(2010,12,11),DateSerial(2010,12,12),DateSerial(2010,12,28),DateSerial(2010,12,29),DateSerial(2010,12,30),DateSerial(2010,12,31),DateSerial(2011,01,01),DateSerial(2011,01,02),DateSerial(2011,01,03),DateSerial(2011,01,04),DateSerial(2011,02,18),DateSerial(2011,02,19),DateSerial(2011,02,20),DateSerial(2011,04,10),DateSerial(2011,04,11),DateSerial(2011,04,12),DateSerial(2011,04,13),DateSerial(2011,04,14),DateSerial(2011,04,15),DateSerial(2011,04,16),DateSerial(2011,04,17),DateSerial(2011,04,18),DateSerial(2011,04,30),DateSerial(2011,05,01),DateSerial(2011,05,02),DateSerial(2011,05,13),DateSerial(2011,05,14),DateSerial(2011,05,15),DateSerial(2011,05,16),DateSerial(2011,05,17),DateSerial(2011,07,15),DateSerial(2011,07,16),DateSerial(2011,07,17),DateSerial(2011,08,12),DateSerial(2011,08,13),DateSerial(2011,08,14),DateSerial(2011,10,22),DateSerial(2011,10,23),DateSerial(2011,10,24),DateSerial(2011,12,03),DateSerial(2011,12,04),DateSerial(2011,12,05),DateSerial(2011,12,10),DateSerial(2011,12,11),DateSerial(2011,12,12),DateSerial(2011,12,31),DateSerial(2012,01,01),DateSerial(2012,01,02),DateSerial(2012,01,03))

	For intHolCount=0 To UBound(arrLong)
		IF arrLong(intHolCount) = DateInput Then
			bolLong = True
			Exit For
		End IF
	Next
	'### Check Long Weekend ###

	functoin_date_check = 1 'NormalDay
	
	IF bolWeekEnd AND bolHoliday Then 
		functoin_date_check = 4 'Holiday And Week End
	End IF
	
	IF bolWeekEnd Then
		functoin_date_check = 2 'WeekEnd
	End IF

	IF bolHoliday Then
		functoin_date_check = 3 'Holiday
	End IF
	
	
	
	IF bolLong Then
		functoin_date_check = 5 'Long Weekend
	End IF
End Function
%>