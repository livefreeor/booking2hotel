<%
FUNCTION function_check_max(arrMax,intType)
	'arrMax option_id,option_cat_id,max_adult,extrabed
	
	Dim intMax
	Dim intCount
	
	SELECT CASE intType
		Case 1 'Max Adult
			For intCount=0 To Ubound(arrMax,2)
				IF Cint(arrMax(1,intCount)) = 38 Then
					intMax = intMax + Cint(arrMax(2,intCount))*Cint(Request("qty"&arrMax(0,intCount)))
				ElseIF Cint(arrMax(1,intCount)) = 39 Then
					intMax = intMax + Cint(arrMax(2,intCount))*Cint(Request("bed"&arrMax(0,intCount)))
				End IF
			Next
			
		Case 2 'Max Extrabed
			For intCount=0 To Ubound(arrMax,2)
				IF Cint(arrMax(1,intCount)) = 38 Then
					intMax = intMax + Cint(arrMax(3,intCount))*Cint(Request("qty"&arrMax(0,intCount)))
				End IF
			Next
			
		Case 3 'Total Extrabed
			For intCount=0 To Ubound(arrMax,2)
				IF Cint(arrMax(1,intCount)) = 39 Then
					intMax = intMax + Cint(Request("bed"&arrMax(0,intCount)))
				End IF
			Next
		
		Case 4 'Airport Transfer
			intMax = 100
			For intCount=0 To Ubound(arrMax,2)
				IF Cint(arrMax(2,intCount))*Cint(Request("airport"&Trim(arrMax(0,intCount))))<intMax Then
					intMax = Cint(arrMax(2,intCount))*Cint(Request("airport"&Trim(arrMax(0,intCount))))
				End IF
			Next
		
	END SELECT
	
		function_check_max = intMax
	
END FUNCTION
%>