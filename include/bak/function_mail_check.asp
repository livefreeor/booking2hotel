<%
Function function_mail_check(strEmail)

	Dim bIsValid
	bIsValid = True
	
	If Len(strEmail) < 5 Then
		bIsValid = False
	Else
		If Instr(1, strEmail, " ") <> 0 Then
			bIsValid = False
		Else
			If InStr(1, strEmail, "@", 1) < 2 Then
				bIsValid = False
			Else
				If InStrRev(strEmail, ".") < InStr(1, strEmail, "@", 1) + 2 Then
					bIsValid = False
				End If
			End If
		End If
	End If

	function_mail_check = bIsValid
	
End Function
%>