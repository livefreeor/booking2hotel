<%
FUNCTION fnConvertSqlDate(inputDate)
'--------for record set
	Dim intYear, intMonth, intDay
	
	intYear = year(inputDate)
	intMonth = month(inputDate)
	intDay = day(inputDate)
	
	IF Cint(intYear) > 2400 Then
		intYear = intYear - 543
	else
		intYear = intYear			
	End IF

	fnConvertSqlDate = dateserial(intYear,intMonth,intDay)

END FUNCTION

FUNCTION fnConvertRawSqlDate(inputDate)
'--------for sql command ------------
	Dim intYear, intMonth, intDay
	
	intYear = year(inputDate)
	intMonth = month(inputDate)
	intDay = day(inputDate)
	
	IF Cint(intYear) > 2400 Then
		intYear = intYear - 543
	else
		intYear = intYear			
	End IF

	fnConvertRawSqlDate = Chr(39) & intYear & "-" & intMonth & "-" & intDay  & Chr(39)

END FUNCTION
%>