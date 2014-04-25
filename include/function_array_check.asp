<%
FUNCTION function_array_check(strInput,arrRefer,intType)
	
	Dim intCount
	
	function_array_check = False

	SELECT CASE intType
	
		Case 1 'Use 2 Dimension Array
			IF ISArray(arrRefer) Then
				For intCount=0 To Ubound(arrRefer,2)
					IF Cstr(strInput)=Cstr(arrRefer(0,intCount)) Then
						function_array_check = True
						Exit For
					End IF
				Next
			End IF
			
		Case 2 'Use 1 Dimension Array
			IF ISArray(arrRefer) Then
				function_array_check= False
				For intCount=0 To Ubound(arrRefer)
					IF Cstr(Trim(strInput))=Cstr(Trim(arrRefer(intCount))) Then
						function_array_check = True
						Exit For
					End IF
				Next
			End IF
			
		Case 3
		
	END SELECT

END FUNCTION
%>