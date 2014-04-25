<%
FUNCTION fnInsertValidate(strInput)

	IF strInput="" OR strInput = NULL Then
		strInput = NULL
	End IF
	
	fnInsertValidate = strInput
	
END FUNCTION
%>