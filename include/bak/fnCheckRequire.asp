<%
Function fnCheckRequire(strName)

	Dim arrName
	Dim u
	Dim bolVal
	
	arrName = Split(strName,",")
	bolVal = True
	
	For u=0 To Ubound(arrName)
		
		IF Trim(Request.Form(arrName(u)))="" OR ISNULL(Request.Form(arrName(u))) Then
			fnCheckRequire = fnCheckRequire & arrName(u) & ","
			bolVal = False
		End IF
		
	Next 
	
	IF NOT bolVal Then
		fnCheckRequire = Left(fnCheckRequire,Len(fnCheckRequire)-1)
	Else
		fnCheckRequire = True
	End IF

End Function
%>
