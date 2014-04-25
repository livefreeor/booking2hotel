<%
FUNCTION function_date_sql_2(InputTime,intType)

	Dim intYear
	Dim intMonth
	Dim intDay
	Dim intHour
	Dim intMin
	
	intYear = Year(InputTime)
	intMonth = Month(InputTime)
	intDay = Day(InputTime)
	intHour = Hour(InputTime)
	intMin = Minute(InputTime)
	
	SELECT CASE intType
	
		Case 1
			function_date_sql_2 = "'" & intYear & "-" & intMonth & "-" & intDay & "'"
		
		Case 2 ' Use For XML
		
			IF intMonth<10 Then
				intMonth = "0" & intMonth
			End IF
			
			IF intDay<10 Then
				intDay = "0" & intDay
			End IF
			
			function_date_sql_2 = intYear & "-" & intMonth & "-" & intDay 
		
		Case 3 'Date and Time for SQL Query String
			function_date_sql_2 = "'" & intYear & "-" & intMonth & "-" & intDay & " "& intHour & ":" & intMin & ":" & "00" & "'"
			
	END SELECT

END FUNCTION

FUNCTION function_date_sql_3(intDay,intMonth,intYear,intHour,intMin,intType)

	IF intHour = "" Then
		intHour = "0"
	End IF
	
	IF intMin = "" Then
		intMin = "0"
	End IF
	
	SELECT CASE intType
		Case 1
			function_date_sql_3 = "'" & intYear & "-" & intMonth & "-" & intDay & " "& intHour & ":" & intMin & ":" & "00" & "'"
	END SELECT
END FUNCTION
%>