<%
FUNCTION function_display_text(strInput,strLink,intMaxChar,intType)

	Dim strOutput
	
	SELECT CASE intType
		Case 1 '### Lastest Review & Forum On Home Page ###
			IF Len(strInput)<=intMaxChar Then
				strOutput = strInput
			Else
				strOutPut = Left(strInput,intMaxChar)
			End IF
			
			strOutPut = strOutPut & "... " & strLink
			
		Case 2 '### Lastest Review On Hotel Detail Page ###
			IF Len(strInput)<=intMaxChar Then
				strOutput = strInput
			Else
				strOutPut = Left(strInput,intMaxChar)
			End IF
			
			strOutPut = strOutPut & "... " & strLink
			
			strOutPut = Replace(strOutPut,VbCrlf,"<br />")
			
		Case 3
	END SELECT
	
		function_display_text = strOutput
	
END FUNCTION
%>