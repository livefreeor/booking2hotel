<%
FUNCTION function_gen_price_golf(intPrice,intPriceOwn,intWeekEnd,intLongWeekEnd,intType)
	
	Dim intPriceTotal

	SELECT CASE intType
		Case 1 'Normal Price
			intPriceTotal = intPrice
		Case 2 'WeekEnd
			intPriceTotal = (((intPrice-intPriceOwn)/intPriceOwn)+1) * (intPriceOwn+intWeekEnd)
		Case 3 'Long Weekend
			intPriceTotal = (((intPrice-intPriceOwn)/intPriceOwn)+1) * (intPriceOwn+intLongWeekEnd)
	END SELECT
	

	function_gen_price_golf = intPriceTotal
END FUNCTION
%>