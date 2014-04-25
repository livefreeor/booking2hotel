<%
FUNCTION function_gen_room_price_average_promotion(intOptionID,dateCheckIn,dateCheckOut,intQty,arrPromotion,arrRate,intType)
	
	Dim intNight
	Dim intCount
	Dim intPriceAll
	Dim dateCurrent
	Dim intPrice
	
	intNight = DateDiff("d",dateCheckIn,dateCheckOut)
	dateCurrent = dateCheckIn
	
	SELECT CASE intType
	
		Case 1 'Price
			For intCount=1 To intNight
				intPrice = function_gen_room_price(intOptionID,dateCurrent,arrRate,1)
				intPriceAll = intPriceAll + function_get_price_promotion(intPrice,intOptionID,dateCheckIn,dateCheckOut,dateCurrent,intQty,arrPromotion,2)
				dateCurrent = DateAdd("d",1,dateCurrent)
			Next
			
			intPriceAll = intPriceAll/intNight
			function_gen_room_price_average_promotion = intPriceAll
		
		Case 2 'Price Rack
			For intCount=1 To intNight
				intPrice = function_gen_room_price(intOptionID,dateCurrent,arrRate,2)
				intPriceAll = intPriceAll + function_get_price_promotion(intPrice,intOptionID,dateCheckIn,dateCheckOut,dateCurrent,intQty,arrPromotion,2)
				dateCurrent = DateAdd("d",1,dateCurrent)
			Next
			
			intPriceAll = intPriceAll/intNight
			function_gen_room_price_average_promotion = intPriceAll
			
		Case 3 'Price Own
			For intCount=1 To intNight
				intPrice = function_gen_room_price(intOptionID,dateCurrent,arrRate,3)
				intPriceAll = intPriceAll + function_get_price_promotion(intPrice,intOptionID,dateCheckIn,dateCheckOut,dateCurrent,intQty,arrPromotion,2)
				dateCurrent = DateAdd("d",1,dateCurrent)
			Next
			
			intPriceAll = intPriceAll/intNight
			function_gen_room_price_average_promotion = intPriceAll
			
	END SELECT
END FUNCTION
%>