<%
FUNCTION function_gen_dropdown_date_ads(intDay,intMonth,intYear,strDayName,strMonthName,strYearName,intType)

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
			IF (intYear ="") Then
				intYearStart = Year(now)
			Else
				intYearStart = Cint(intYear)
			End IF
			
			intYearCount = 5
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			IF (intYear <> "") Then
				intYear= Cint(intYear)
			End IF
			
		'### Day ###
			strDay = "<select name="""& strDayName &""">" & VbCrlf
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
			strMonth = "<select name="""& strMonthName &""">" & VbCrlf
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
			strYear = "<select name="""& strYearName &""">" & VbCrlf
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
		
		
		function_gen_dropdown_date_ads = strMonth & " " & strDay & " " & strYear
		
		Case 2	'Jan May 05 (Year Start at 2003)
		
			intYearStart = 2003
			intYearCount = 6
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
		'### Day ###
			strDay = "<select name="""& strDayName &""">" & VbCrlf
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
			strMonth = "<select name="""& strMonthName &""">" & VbCrlf
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
			strYear = "<select name="""& strYearName &""">" & VbCrlf
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
		
		
		function_gen_dropdown_date_ads = strMonth & " " & strDay & " " & strYear
		
		
		Case 3 '20 May 2005
			intYearStart = 1900
			intYearCount = 106
			intYearStep = 1
			intDay = Cint(intDay)
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
			
		'### Day ###
			strDay = "<select name="""& strDayName &""" id=""selDay"">" & VbCrlf
			strDay = strDay & "<option value=""selDay"" selected>Day</option>" &VbCrlf
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
			strMonth = "<select name="""& strMonthName &""" id=""selMonth"">" & VbCrlf
			strMonth = strMonth & "<option value=""selMonth"" selected>Month</option>" &VbCrlf
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
			strYear = "<select name="""& strYearName &""" id=""selYear"">" & VbCrlf
			strYear = strYear& "<option value=""selYear"" selected>Year</option>" &VbCrlf
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
		
		function_gen_dropdown_date_ads = strMonth & " " & strDay & " " & strYear
		Case 4 'May 05
			intYearStart = 2003
			intYearCount = 5
			intYearStep = 1
			intMonth = Cint(intMonth)
			intYear= Cint(intYear)
		
		'### Month ###
			strMonth = "<select name="""& strMonthName &""">" & VbCrlf
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
			strYear = "<select name="""& strYearName &""">" & VbCrlf
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
		
		
		function_gen_dropdown_date_ads = strMonth & " " & strYear
	END SELECT
	
	
END FUNCTION
%>