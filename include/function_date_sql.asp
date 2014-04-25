<%
FUNCTION function_date_sql(intDay,intMonth,intYear,intType)

	SELECT CASE intType
	
		Case 1
			function_date_sql = "'" & intYear & "-" & intMonth & "-" & intDay & "'"
		
		Case 2 ' Use For XML
		
			IF intMonth<10 Then
				intMonth = "0" & intMonth
			End IF
			
			IF intDay<10 Then
				intDay = "0" & intDay
			End IF
			
			function_date_sql = intYear & "-" & intMonth & "-" & intDay 
			
	END SELECT

END FUNCTION
%>