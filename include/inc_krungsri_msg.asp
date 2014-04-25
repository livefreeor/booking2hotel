<%
FUNCTION function_krungsri_gen_price(intPrice)
	
	Dim intLenPrice
	Dim intPriceCount
	Dim strResult
	
	IF intPrice<>"" AND NOT ISNULL(intPrice) Then
		intPrice = Int(IntPrice)
	Else
		intPrice = 0
	End IF

	intPrice = intPrice * 100
	
	function_krungsri_gen_price = intPrice
	
END FUNCTION
%>