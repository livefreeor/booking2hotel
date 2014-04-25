<%
FUNCTION function_display_day_condition(bolSon,bolMon,bolTue,bolWed,bolThu,bolFri,bolSat)

	Dim strSun
	Dim strMon
	Dim strTue
	Dim strWed
	Dim strThu
	Dim strFri
	Dim strSat
	Dim strReturn
	
	IF NOT (bolSon AND bolMon AND bolTue AND bolWed AND bolThu AND bolFri AND bolSat) Then
		IF bolSon Then
			strSun = "Sunday, "
		End IF
		
		IF bolMon Then
			strMon = "Monday, "
		End IF
		
		IF bolTue Then
			strTue = "Tuesday, "
		End IF
		
		IF bolWed Then
			strWed = "Wednesday, "
		End IF
		
		IF bolThu Then
			strThu = "Thursday, "
		End IF
		
		IF bolFri Then
			strFri = "Friday, "
		End IF
		
		IF bolSat Then
			strSat = "Saturday, "
		End IF
		
		strReturn = strSun & strMon & strTue & strWed & strThu & strFri & strSat
	
		IF Len(strReturn)>0 Then
			strReturn = Left(strReturn,Len(strReturn)-2)
			function_display_day_condition = "<font color=""red""><b>*</b></font> <font color=""#fe5400""> This rate is available only on " & strReturn & "</font>"
		End IF
	Else
		function_display_day_condition = ""
	End IF


END FUNCTION
%>