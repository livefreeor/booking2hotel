<%
FUNCTION function_gen_dropdown_date(intDay,intMonth,intYear,strDayName,strMonthName,strYearName,intType)

	Dim arrMonth
	Dim arrMonthSemi
	Dim intCount
	Dim strDay
	Dim strMonth
	Dim strYear
	Dim intYearStart
	Dim intYearCount
	Dim strYearTemp
	Dim intYearStep

	arrMonth = array("","January","February","March","April","May","June","July","August","September","October","November","December")
	arrMonthSemi = array("","Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec")
	
	SELECT CASE intType
		Case 1 'Jan May 05
			intYearStart = int(year(now()))
			intYearCount = 3
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
			
		'### Day ###
			strDay = "<select name="""& strDayName &""" id="""& strDayName &""">" & VbCrlf
			For intCount=1 To 31
				IF intDay=intCount Then
					strDay = strDay & "<option value="""& intCount &""" selected>"& intCount &"</option>" &VbCrlf
				Else
					strDay = strDay & "<option value="""& intCount &""">"& intCount &"</option>" &VbCrlf
				End IF
			Next
			strDay = strDay & "</select>" & VbCrlf
		'### Day ###
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""" id="""& strMonthName &""">" & VbCrlf
			For intCount=1 To 12
				IF intMonth=intCount Then
					strMonth = strMonth & "<option value="""& intCount &""" selected>"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				Else
					strMonth = strMonth & "<option value="""& intCount &""">"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				End IF
			Next
			strMonth = strMonth& "</select>" & VbCrlf
		'### Month ###
		
		'### Year ###
			strYear = "<select name="""& strYearName &""" id="""& strYearName &""">" & VbCrlf
			For intCount=0 To intYearCount
				strYearTemp = Right(Cstr(intYearStart),2)
				IF intYear=intYearStart Then
					strYear = strYear& "<option value="""& intYearStart &""" selected>"& strYearTemp &"</option>" &VbCrlf
				Else
					strYear = strYear & "<option value="""& intYearStart &""">"& strYearTemp &"</option>" &VbCrlf
				End IF
				intYearStart = intYearStart + intYearStep
			Next
			strYear = strYear & "</select>" & VbCrlf
		'### Year ###
		
		
		function_gen_dropdown_date = strMonth & " " & strDay & " " & strYear
		
		Case 2	'Jan May 05 (Year Start at 2003)
		
			intYearStart = 2003
			intYearCount = 9
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
		'### Day ###
			strDay = "<select name="""& strDayName &""" id="""& strDayName &""">" & VbCrlf
			For intCount=1 To 31
				IF intDay=intCount Then
					strDay = strDay & "<option value="""& intCount &""" selected>"& intCount &"</option>" &VbCrlf
				Else
					strDay = strDay & "<option value="""& intCount &""">"& intCount &"</option>" &VbCrlf
				End IF
			Next
			strDay = strDay & "</select>" & VbCrlf
		'### Day ###
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""" id="""& strMonthName &""">" & VbCrlf
			For intCount=1 To 12
				IF intMonth=intCount Then
					strMonth = strMonth & "<option value="""& intCount &""" selected>"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				Else
					strMonth = strMonth & "<option value="""& intCount &""">"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				End IF
			Next
			strMonth = strMonth& "</select>" & VbCrlf
		'### Month ###
		
		'### Year ###
			strYear = "<select name="""& strYearName &""" id="""& strYearName &""">" & VbCrlf
			For intCount=0 To intYearCount
				strYearTemp = Right(Cstr(intYearStart),2)
				IF intYear=intYearStart Then
					strYear = strYear& "<option value="""& intYearStart &""" selected>"& strYearTemp &"</option>" &VbCrlf
				Else
					strYear = strYear & "<option value="""& intYearStart &""">"& strYearTemp &"</option>" &VbCrlf
				End IF
				intYearStart = intYearStart + intYearStep
			Next
			strYear = strYear & "</select>" & VbCrlf
		'### Year ###
		
		
		function_gen_dropdown_date = strMonth & " " & strDay & " " & strYear
		
		
		Case 3 '20 May 2005
			intYearStart = 1900
			intYearCount = 106
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
			
		'### Day ###
			strDay = "<select name="""& strDayName &""" id="""& strDayName &""">" & VbCrlf
			For intCount=1 To 31
				IF intDay=intCount Then
					strDay = strDay & "<option value="""& intCount &""" selected>"& intCount &"</option>" &VbCrlf
				Else
				
					strDay = strDay & "<option value="""& intCount &""">"& intCount &"</option>" &VbCrlf
				End IF
			Next
			strDay = strDay & "</select>" & VbCrlf
		'### Day ###
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""" id="""& strMonthName &""">" & VbCrlf
			For intCount=1 To 12
				IF intMonth=intCount Then
					strMonth = strMonth & "<option value="""& intCount &""" selected>"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				Else
					strMonth = strMonth & "<option value="""& intCount &""">"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				End IF
			Next
			strMonth = strMonth& "</select>" & VbCrlf
		'### Month ###
		
		'### Year ###
			strYear = "<select name="""& strYearName &""" id="""& strYearName &""">" & VbCrlf
			For intCount=0 To intYearCount
			
				IF intYear=intYearStart Then
					strYear = strYear& "<option value="""& intYearStart &""" selected>"& intYearStart &"</option>" &VbCrlf
				Else
					strYear = strYear & "<option value="""& intYearStart &""">"& intYearStart &"</option>" &VbCrlf
				End IF
				intYearStart = intYearStart + intYearStep
			Next
			strYear = strYear & "</select>" & VbCrlf
		'### Year ###
		
		function_gen_dropdown_date = strMonth & " " & strDay & " " & strYear
		Case 4 'May 05
			intYearStart = 2003
			intYearCount = 8
			intYearStep = 1
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""" id="""& strMonthName &""">" & VbCrlf
			For intCount=1 To 12
				IF intMonth=intCount Then
					strMonth = strMonth & "<option value="""& intCount &""" selected>"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				Else
					strMonth = strMonth & "<option value="""& intCount &""">"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				End IF
			Next
			strMonth = strMonth& "</select>" & VbCrlf
		'### Month ###
		
		'### Year ###
			strYear = "<select name="""& strYearName &""" id="""& strYearName &""">" & VbCrlf
			For intCount=0 To intYearCount
				strYearTemp = Right(Cstr(intYearStart),2)
				IF intYear=intYearStart Then
					strYear = strYear& "<option value="""& intYearStart &""" selected>"& strYearTemp &"</option>" &VbCrlf
				Else
					strYear = strYear & "<option value="""& intYearStart &""">"& strYearTemp &"</option>" &VbCrlf
				End IF
				intYearStart = intYearStart + intYearStep
			Next
			strYear = strYear & "</select>" & VbCrlf
		'### Year ###
		
		
		function_gen_dropdown_date = strMonth & " " & strYear
		Case 5 'Jan May 05
			intYearStart = 2006
			intYearCount = 2
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
			
		'### Day ###
			strDay = "<select name="""& strDayName &""" id="""& strDayName &""">" & VbCrlf
			For intCount=1 To 31
				IF intDay=intCount Then
					strDay = strDay & "<option value="""& intCount &""" selected>"& intCount &"</option>" &VbCrlf
				Else
					strDay = strDay & "<option value="""& intCount &""">"& intCount &"</option>" &VbCrlf
				End IF
			Next
			strDay = strDay & "</select>" & VbCrlf
		'### Day ###
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""" id="""& strMonthName &""">" & VbCrlf
			For intCount=1 To 12
				IF intMonth=intCount Then
					strMonth = strMonth & "<option value="""& intCount &""" selected>"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				Else
					strMonth = strMonth & "<option value="""& intCount &""">"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				End IF
			Next
			strMonth = strMonth& "</select>" & VbCrlf
		'### Month ###
		
		'### Year ###
			strYear = "<select name="""& strYearName &""" id="""& strYearName &""">" & VbCrlf
			For intCount=0 To intYearCount
				strYearTemp = Right(Cstr(intYearStart),2)
				IF intYear=intYearStart Then
					strYear = strYear& "<option value="""& intYearStart &""" selected>"& strYearTemp &"</option>" &VbCrlf
				Else
					strYear = strYear & "<option value="""& intYearStart &""">"& strYearTemp &"</option>" &VbCrlf
				End IF
				intYearStart = intYearStart + intYearStep
			Next
			strYear = strYear & "</select>" & VbCrlf
		'### Year ###
		
		
		function_gen_dropdown_date = strMonth & " " & strDay & " " & strYear
		
		Case 6 'default day, month, year
			intYearStart = 1930
			intYearCount = 70
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
			
		'### Day ###

			strDay = "<select name="""& strDayName &""" id="""& strDayName &""">" & VbCrlf
			strDay = strDay & "<option value=""0"" selected=""selected"">Day</option>"
			For intCount=1 To 31
				IF intDay=intCount Then
					strDay = strDay & "<option value="""& intCount &""" selected>"& intCount &"</option>" &VbCrlf
				Else
					strDay = strDay & "<option value="""& intCount &""">"& intCount &"</option>" &VbCrlf
				End IF
			Next
			strDay = strDay & "</select>" & VbCrlf
		'### Day ###
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""" id="""& strMonthName &""">" & VbCrlf
			strMonth = strMonth & "<option value=""0"" selected=""selected"">Month</option>"
			For intCount=1 To 12
				IF intMonth=intCount Then
					strMonth = strMonth & "<option value="""& intCount &""" selected>"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				Else
					strMonth = strMonth & "<option value="""& intCount &""">"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				End IF
			Next
			strMonth = strMonth& "</select>" & VbCrlf
		'### Month ###
		
		'### Year ###
			strYear = "<select name="""& strYearName &""" id="""& strYearName &""">" & VbCrlf
			strYear = strYear & "<option value=""0"" selected=""selected"">Year</option>"
			For intCount=0 To intYearCount
			
				IF intYear=intYearStart Then
					strYear = strYear& "<option value="""& intYearStart &""" selected>"& intYearStart &"</option>" &VbCrlf
				Else
					strYear = strYear & "<option value="""& intYearStart &""">"& intYearStart &"</option>" &VbCrlf
				End IF
				intYearStart = intYearStart + intYearStep
			Next
			strYear = strYear & "</select>" & VbCrlf
		'### Year ###
		
		function_gen_dropdown_date = strDay & " " & strMonth & " " & strYear
		
		Case 7 'default day, month, year
			intYearStart = 2005
			intYearCount = 6
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
			
		'### Day ###
			strDay = "<select name="""& strDayName &""" id="""& strDayName &""">" & VbCrlf
			strDay = strDay & "<option value=""0"" selected=""selected"">Day</option>"
			For intCount=1 To 31
				IF intDay=intCount Then
					strDay = strDay & "<option value="""& intCount &""" selected>"& intCount &"</option>" &VbCrlf
				Else
					strDay = strDay & "<option value="""& intCount &""">"& intCount &"</option>" &VbCrlf
				End IF
			Next
			strDay = strDay & "</select>" & VbCrlf
		'### Day ###
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""" id="""& strMonthName &""">" & VbCrlf
			strMonth = strMonth & "<option value=""0"" selected=""selected"">Month</option>"
			For intCount=1 To 12
				IF intMonth=intCount Then
					strMonth = strMonth & "<option value="""& intCount &""" selected>"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				Else
					strMonth = strMonth & "<option value="""& intCount &""">"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				End IF
			Next
			strMonth = strMonth& "</select>" & VbCrlf
		'### Month ###
		
		'### Year ###
			strYear = "<select name="""& strYearName &""" id="""& strYearName &""">" & VbCrlf
			strYear = strYear & "<option value=""0"" selected=""selected"">Year</option>"
			For intCount=0 To intYearCount
			
				IF intYear=intYearStart Then
					strYear = strYear& "<option value="""& intYearStart &""" selected>"& intYearStart &"</option>" &VbCrlf
				Else
					strYear = strYear & "<option value="""& intYearStart &""">"& intYearStart &"</option>" &VbCrlf
				End IF
				intYearStart = intYearStart + intYearStep
			Next
			strYear = strYear & "</select>" & VbCrlf
		'### Year ###
		
		function_gen_dropdown_date = strDay & " " & strMonth & " " & strYear

        Case 8 'Day Month Year
			intYearStart = int(year(now()))
			intYearCount = 3
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
			
		'### Day ###
			strDay = "<select name="""& strDayName &""" id="""& strDayName &""">" & VbCrlf
			For intCount=1 To 31
				IF intDay=intCount Then
					strDay = strDay & "<option value="""& intCount &""" selected>"& intCount &"</option>" &VbCrlf
				Else
					strDay = strDay & "<option value="""& intCount &""">"& intCount &"</option>" &VbCrlf
				End IF
			Next
			strDay = strDay & "</select>" & VbCrlf
		'### Day ###
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""" id="""& strMonthName &""">" & VbCrlf
			For intCount=1 To 12
				IF intMonth=intCount Then
					strMonth = strMonth & "<option value="""& intCount &""" selected>"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				Else
					strMonth = strMonth & "<option value="""& intCount &""">"& arrMonthSemi(intCount) &"</option>" &VbCrlf
				End IF
			Next
			strMonth = strMonth& "</select>" & VbCrlf
		'### Month ###
		
		'### Year ###
			strYear = "<select name="""& strYearName &""" id="""& strYearName &""">" & VbCrlf
			For intCount=0 To intYearCount
				strYearTemp = Right(Cstr(intYearStart),2)
				IF intYear=intYearStart Then
					strYear = strYear& "<option value="""& intYearStart &""" selected>"& strYearTemp &"</option>" &VbCrlf
				Else
					strYear = strYear & "<option value="""& intYearStart &""">"& strYearTemp &"</option>" &VbCrlf
				End IF
				intYearStart = intYearStart + intYearStep
			Next
			strYear = strYear & "</select>" & VbCrlf
		'### Year ###
		
		
		function_gen_dropdown_date = strDay & " " & strMonth & " " & strYear
	END SELECT
	
	
END FUNCTION
%>