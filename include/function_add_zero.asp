<%
Function function_add_zero(strInput, intTotalLength)

	Dim strOutput, intCurrentLength, intLengthDifference, i

	strOutput = strInput
	intCurrentLength = CInt(Len(strInput))
	intTotalLength = CInt(intTotalLength)
	intLengthDifference = intTotalLength - intCurrentLength

	If intLengthDifference > 0 Then
		For i=1 To intLengthDifference
			strOutput = "0" & strOutput
		Next
	End If

	'return result to function
	function_add_zero  = strOutput

End Function
%>