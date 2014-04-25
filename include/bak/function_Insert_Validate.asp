<%
FUNCTION function_Insert_Validate(strInput)

	IF strInput="" OR strInput = NULL Then
		strInput = NULL
	End IF
	
	function_Insert_Validate= strInput
	
END FUNCTION
%>