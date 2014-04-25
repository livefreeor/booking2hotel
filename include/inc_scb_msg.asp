<%
FUNCTION function_scb_gen_price(intPrice)
	
	Dim intLenPrice
	Dim intPriceCount
	Dim strResult
	
	IF intPrice<>"" AND NOT ISNULL(intPrice) Then
		intPrice = formatnumber(IntPrice,2)
	Else
		intPrice = 0
	End IF

	intPrice = intPrice * 100
	
	function_scb_gen_price = intPrice
	
END FUNCTION
%>