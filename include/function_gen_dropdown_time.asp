<%
FUNCTION function_gen_dropdown_time(strHourName,strMinName,intHour,intMin,intType)

	Dim intHourCount
	Dim intMinCount
	Dim strHour
	Dim strHourSelect
	Dim strHourTemp
	Dim strMin
	Dim strMinSelect
	Dim strMinTemp
	
	SELECT CASE intType
	
		Case 1 'Start At 00:00
		'### Hour ###
			strHour = "<SELECT name="""&strHourName&""">"
			For intHourCount=0 To 23
				IF Cstr(intHourCount) = Cstr(intHour) Then
					strHourSelect = "selected"
				Else
					strHourSelect = ""
				End IF
				
				IF intHourCount<10 Then
					strHourTemp = "0" & intHourCount
				Else
					strHourTemp  = intHourCount
				End IF
				
				strHour = strHour & "<Option value="""& intHourCount &""" "&strHourSelect&">"&strHourTemp &"</Option>"
			Next
			strHour = strHour & "</SELECT>"
		'### Hour ###
		
		
		'### Min ###
			strMin = "<SELECT name="""&strMinName&""">"
			For intMinCount=0 To 55 STEP 5
				IF Cstr(intMinCount) = Cstr(intMin) Then
					strMinSelect = "selected"
				Else
					strMinSelect = ""
				End IF
				
				IF intMinCount<10 Then
					strMinTemp = "0" & intMinCount
				Else
					strMinTemp  = intMinCount
				End IF
				
				strMin= strMin & "<Option value="""& intMinCount &""" "&strMinSelect&">"&strMinTemp &"</Option>"
			Next
			strMin = strMin& "</SELECT>"
		'### Min ###
			
			function_gen_dropdown_time = "Hour: " & strHour & " Min: " & strMin
			
		Case 2 'Start At 06:00
		'### Hour ###
			strHour = "<SELECT name="""&strHourName&""">"
			For intHourCount=6 To 23
				IF Cstr(intHourCount) = Cstr(intHour) Then
					strHourSelect = "selected"
				Else
					strHourSelect = ""
				End IF
				
				IF intHourCount<10 Then
					strHourTemp = "0" & intHourCount
				Else
					strHourTemp  = intHourCount
				End IF
				
				strHour = strHour & "<Option value="""& intHourCount &""" "&strHourSelect&">"&strHourTemp &"</Option>"
			Next
			strHour = strHour & "</SELECT>"
		'### Hour ###
		
		
		'### Min ###
			strMin = "<SELECT name="""&strMinName&""">"
			For intMinCount=0 To 50 STEP 10
				IF Cstr(intMinCount) = Cstr(intMin) Then
					strMinSelect = "selected"
				Else
					strMinSelect = ""
				End IF
				
				IF intMinCount<10 Then
					strMinTemp = "0" & intMinCount
				Else
					strMinTemp  = intMinCount
				End IF
				
				strMin= strMin & "<Option value="""& intMinCount &""" "&strMinSelect&">"&strMinTemp &"</Option>"
			Next
			strMin = strMin& "</SELECT>"
		'### Min ###
			
			function_gen_dropdown_time = "Hour: " & strHour & " Min: " & strMin
			
		Case 3 'Start At 00:00 Step 1 Min
		'### Hour ###
			strHour = "<SELECT name="""&strHourName&""">"
			For intHourCount=0 To 23
				IF Cstr(intHourCount) = Cstr(intHour) Then
					strHourSelect = "selected"
				Else
					strHourSelect = ""
				End IF
				
				IF intHourCount<10 Then
					strHourTemp = "0" & intHourCount
				Else
					strHourTemp  = intHourCount
				End IF
				
				strHour = strHour & "<Option value="""& intHourCount &""" "&strHourSelect&">"&strHourTemp &"</Option>"
			Next
			strHour = strHour & "</SELECT>"
		'### Hour ###
		
		
		'### Min ###
			strMin = "<SELECT name="""&strMinName&""">"
			For intMinCount=0 To 59 STEP 1
				IF Cstr(intMinCount) = Cstr(intMin) Then
					strMinSelect = "selected"
				Else
					strMinSelect = ""
				End IF
				
				IF intMinCount<10 Then
					strMinTemp = "0" & intMinCount
				Else
					strMinTemp  = intMinCount
				End IF
				
				strMin= strMin & "<Option value="""& intMinCount &""" "&strMinSelect&">"&strMinTemp &"</Option>"
			Next
			strMin = strMin& "</SELECT>"
		'### Min ###
			
			function_gen_dropdown_time = "Hour: " & strHour & " Min: " & strMin
	END SELECT

END FUNCTION
%>