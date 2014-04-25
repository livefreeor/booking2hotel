<%
FUNCTION function_gen_dropdown_number(intMin,intMax,intDefault,strName,intType)
	
	Dim intCount
	Dim strSelected
	
	Select Case intType
	
		Case 1
			IF ISNULL(intDefault) OR intDefault="" Then
				intDefault=0
			Else
				intDefault = Cint(intDefault)
			End IF
		
			function_gen_dropdown_number = "<select name="""& strName &""">" & VbCrlf
			
			For intCount=intMin To intMax
			
				IF intDefault=intCount Then
					function_gen_dropdown_number = function_gen_dropdown_number & "<option value="""& intCount &""" selected>"& intCount &"</option>" & VbCrlf
				Else
					function_gen_dropdown_number = function_gen_dropdown_number & "<option value="""& intCount &""">"& intCount &"</option>" & VbCrlf
				End IF
			
			Next
			
			function_gen_dropdown_number = function_gen_dropdown_number & "</select>" & VbCrlf
		
		Case 2
		Dim starRate
		Dim arrStar
		starRate="1,1.5,2,2.5,3,3.5,4,4.5,5"
		arrStar=split(starRate,",")
		
		IF ISNULL(intDefault) OR intDefault="" Then
			intDefault=0
		Else
			intDefault = Cdbl(intDefault)
		End IF
		function_gen_dropdown_number = "<select name="""& strName &""">" & VbCrlf
		
		For intCount=0 To Ubound(arrStar)
		
			IF intDefault=Cdbl(arrStar(intCount)) Then
				function_gen_dropdown_number = function_gen_dropdown_number & " <option value='"&arrStar(intCount)&"' selected>"&arrStar(intCount)&vbcrlf
			Else
				function_gen_dropdown_number = function_gen_dropdown_number & " <option value='"&arrStar(intCount)&"'>"&arrStar(intCount)&vbcrlf
			End IF
		
		Next
		
		function_gen_dropdown_number = function_gen_dropdown_number & "</select>" & VbCrlf
		
		Case 3
			IF ISNULL(intDefault) OR intDefault="" Then
				intDefault=0
			Else
				intDefault = Cint(intDefault)
			End IF
		
			function_gen_dropdown_number = "<select name="""& strName &""">" & VbCrlf
			
			For intCount=intMin To intMax
			
				IF intDefault=intCount Then
						function_gen_dropdown_number = function_gen_dropdown_number & "<option value="""& intCount &""" selected>"& intCount &"</option>" & VbCrlf
				Else
					function_gen_dropdown_number = function_gen_dropdown_number & "<option value="""& intCount &""">"& intCount &"</option>" & VbCrlf
				End IF
			
			Next
			
			IF int(intDefault)=100 Then
				function_gen_dropdown_number = function_gen_dropdown_number & "<option value=""100"" selected>Infinity</option>" & VbCrlf
			Else
				function_gen_dropdown_number = function_gen_dropdown_number & "<option value=""100"">Infinity</option>" & VbCrlf
			End IF
			function_gen_dropdown_number = function_gen_dropdown_number & "</select>" & VbCrlf
		
		Case 4 '### Same case1 plus with id use in express checkout ###
			IF ISNULL(intDefault) OR intDefault="" Then
				intDefault=0
			Else
				intDefault = Cint(intDefault)
			End IF
		
			function_gen_dropdown_number = "<select name="""& strName &""" id="""& strName &""">" & VbCrlf
			
			For intCount=intMin To intMax
			
				IF intDefault=intCount Then
					function_gen_dropdown_number = function_gen_dropdown_number & "<option value="""& intCount &""" selected>"& intCount &"</option>" & VbCrlf
				Else
					function_gen_dropdown_number = function_gen_dropdown_number & "<option value="""& intCount &""">"& intCount &"</option>" & VbCrlf
				End IF
			
			Next
			
			function_gen_dropdown_number = function_gen_dropdown_number & "</select>" & VbCrlf
			
		Case 5
	End Select
END FUNCTION
%>