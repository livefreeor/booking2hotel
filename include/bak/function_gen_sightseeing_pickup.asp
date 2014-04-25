<%
FUNCTION function_gen_sightseeing_pickup(arrProduct,dateTrip,intType)
	
	Dim intCountSub
	Dim strReturn
	
	strReturn = ""
	
	For intCountSub=0 To Ubound(arrProduct,2)
	
		IF arrProduct(2,intCountSub)=29 AND (arrProduct(3,intCountSub)<=dateTrip AND arrProduct(4,intCountSub)>dateTrip)Then
			strReturn = "Pickup at " & arrProduct(8,intCountSub)
			Exit For
		End IF
	
	Next
	
	function_gen_sightseeing_pickup = strReturn
	
END FUNCTION
%>