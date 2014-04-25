<%
Function function_check_require(strName)

	Dim arrName
	Dim u
	Dim bolVal
	
	arrName = Split(strName,",")
	bolVal = True
	
	For u=0 To Ubound(arrName)
		
		IF Trim(Request.Form(arrName(u)))="" OR ISNULL(Request.Form(arrName(u))) Then
			function_check_require = function_check_require & arrName(u) & ","
			bolVal = False
		End IF
		
	Next 
	
	IF NOT bolVal Then
		'function_check_require = Left(function_check_require,Len(function_check_require)-1)
		function_check_require = False
	Else
		function_check_require = True
	End IF

End Function
%>
