<%
Function function_checkbox_value(strValue,strOn,strOff,intType)
Select Case intType
Case 1
	IF strValue="on" Then
		function_checkbox_value=strOn
	Else
		function_checkbox_value=strOff
	End IF
Case 2
	IF strValue=True Then
		function_checkbox_value="checked"
	End IF
	End Select
End Function
%>