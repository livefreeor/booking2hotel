<%
Function function_display_char(strInput,intLenDisplay)

	IF Len(strInput)<intLenDisplay Then
		function_display_char = strInput
	Else
		strInput = Left(strInput,intLenDisplay) & "..."
		function_display_char = strInput
	End IF

End Function
%>