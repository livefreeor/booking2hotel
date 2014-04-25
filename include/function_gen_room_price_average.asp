<%
FUNCTION function_gen_room_price_average(intOptionID,dateCheckIn,dateCheckOut,arrRate,intType)
	'arrRate(option_id,date_start,date_end,price,price_rack)
	Dim intCount
	Dim intPrice
	Dim dateCurrent
	Dim intNight
	Dim intPriceTemp
	
	intNight = DateDiff("d",dateCheckIn,dateCheckOut)
	dateCurrent = dateCheckIn
	intPrice = 0
	intPriceTemp=0
	
	SELECT CASE intType
		Case 1 'Price
			For intCount=1 To intNight
				intPrice = function_gen_room_price(intOptionID,dateCurrent,arrRate,1)
				IF intPrice="" Then
					intPrice=0
				Else
					'intPrice = Cint(intPrice)
				End IF
				intPriceTemp = intPriceTemp + intPrice
				dateCurrent = DateAdd("d",1,dateCurrent)
			Next
			
		Case 2 'Price Rack
			For intCount=1 To intNight
				intPrice = function_gen_room_price(intOptionID,dateCurrent,arrRate,2)
				IF intPrice="" Then
					intPrice=0
				Else
					'intPrice = Cint(intPrice)
				End IF
				intPriceTemp = intPriceTemp + intPrice
				dateCurrent = DateAdd("d",1,dateCurrent)
			Next
			
		Case 3 'Price Own
			For intCount=1 To intNight
				intPrice = function_gen_room_price(intOptionID,dateCurrent,arrRate,3)
				IF intPrice="" Then
					intPrice=0
				Else
					'intPrice = Cint(intPrice)
				End IF
				intPriceTemp = intPriceTemp + intPrice
				dateCurrent = DateAdd("d",1,dateCurrent)
			Next
			
	END SELECT
	
	intPriceTemp = intPriceTemp/intNight
	function_gen_room_price_average = intPriceTemp
	
END FUNCTION
%>