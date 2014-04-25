<%
Function fnGenHidden(strName)

	Dim arrName
	Dim i
	Dim strValue
	
	arrName = Split(strName,",")
	
	For i=0 to Ubound(arrName)
		if request.form(arrName(i))<> "" then
			strValue=request.form(arrName(i))
		else
		    strValue=request.querystring(arrName(i)) 
		end if
			If NOt strValue = "" then
				fnGenHidden= fnGenHidden &"<input type=" &chr(34)&"hidden"&chr(34)& "name="&chr(34)&arrName(i)&chr(34)&" value="&chr(34)&fnConvertChr(trim(strValue),0)&chr(34)&">" &vbcrlf
			End If
	Next
End Function	
%>