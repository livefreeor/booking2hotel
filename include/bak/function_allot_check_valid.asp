<%
FUNCTION function_allot_check_valid(intProductID,intOptionID,arrAllot,dateCheckIn,dateCheckOut,intQty,intType)
	'arrAllot=option_id,date_allotment,date_cut_off,allotment
	
	Dim intNight
	Dim intAllot
	Dim bolAllot
	Dim intCount
	
	intNight = DateDiff("d",dateCheckIn,dateCheckOut)
	
	SELECT CASE intType
	
		Case 1 'Check in option scope
			'###Check Number of allot day###
			intAllot = 0
			For intCount=0 To Ubound(arrAllot,2)
				IF Cstr(arrAllot(0,intCount))=Cstr(intOptionID) Then
					intAllot = intAllot + 1
				End IF
			Next

			IF intNight=intAllot Then
				bolAllot = True
			Else
				bolAllot = False
			End IF
			'###Check Number of allot day###

			'###Check sum allotment###
			IF bolAllot Then
				For intCount=0 To Ubound(arrAllot,2)
					IF (Cint(arrAllot(3,intCount))<intQty) AND (Cstr(arrAllot(0,intCount))=Cstr(intOptionID)) Then
						bolAllot = False
					End IF
				Next
			End IF
			'###Check sum allotment###
			
			function_allot_check_valid = bolAllot
			
		Case 2
		Case 3
	END SELECT

END FUNCTION

FUNCTION function_allotment_min(intProductID,intOptionID,arrAllot,intType)

	Dim intCount
	Dim intMin
	
	SELECT CASE intType
		Case 1
			intMin = 1000
			For intCount=0 To Ubound(arrAllot,2)
				IF Cstr(arrAllot(0,intCount))=Cstr(intOptionID) AND arrAllot(3,intCount)<intMin Then
					intMin = arrAllot(3,intCount)
				End IF
			Next
			function_allotment_min = intMin
		Case 2
	END SELECT
END FUNCTION
%>