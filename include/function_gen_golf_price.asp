<%
FUNCTION function_gen_golf_price(intOptionID,datePrice,arrRate,intType)
	'arrRate(option_id,date_start,date_end,price,price_rack,price_own,sup_weekend,sup_holiday,sup_long)
	Dim intCount
	Dim intPrice
	Dim intDayType
	Dim price_sup_id
	
	datePrice=dateserial(year(datePrice),month(datePrice),day(datePrice))
	intDayType = functoin_date_check(datePrice)
	'price_sup_id=trim(conn.execute("select top 1 price_sup_id from tbl_product_option po,tbl_product p where po.product_id=p.product_id and option_id="&intOptionID).getString())
	SELECT CASE intType
		Case 1 'Price
			For intCount=0 To Ubound(arrRate,2)
				IF (datePrice>=arrRate(1,intCount) AND datePrice<=arrRate(2,intCount)) AND Cstr(intOptionID)=Cstr(arrRate(0,intCount))Then
					intPrice = arrRate(3,intCount)
					EXIT FOR
				End IF
			Next
			
			'### Out of range check ###
			IF intCount>Ubound(arrRate,2) Then
				intPrice = 0
				intCount = Ubound(arrRate,2)
			End IF
			'### Out of range check ###
			
				SELECT CASE intDayType
					Case 1 'Normal Day
					Case 2 'Weekend
						IF arrRate(6,intCount) >0 Then
							intPrice = (((arrRate(3,intCount)-arrRate(5,intCount))/arrRate(5,intCount))+1)*(arrRate(5,intCount) + arrRate(6,intCount))
						End IF
					Case 3 'Holiday
						IF arrRate(7,intCount) >0 Then
							intPrice = (((arrRate(3,intCount)-arrRate(5,intCount))/arrRate(5,intCount))+1)*(arrRate(5,intCount) + arrRate(7,intCount))
						End IF
					Case 4 'Holiday and Weekend
						IF arrRate(7,intCount) >0 Then 'Holiday
							intPrice = (((arrRate(3,intCount)-arrRate(5,intCount))/arrRate(5,intCount))+1)*(arrRate(5,intCount) + arrRate(7,intCount))
						ElseIF arrRate(6,intCount) >0 Then 'Week End
							intPrice = (((arrRate(3,intCount)-arrRate(5,intCount))/arrRate(5,intCount))+1)*(arrRate(5,intCount) + arrRate(6,intCount))
						End IF
					Case 5
						IF arrRate(8,intCount) >0 Then 'Long Week End
							intPrice = (((arrRate(3,intCount)-arrRate(5,intCount))/arrRate(5,intCount))+1)*(arrRate(5,intCount) + arrRate(8,intCount))
						ElseIF arrRate(7,intCount) >0 Then 'Holiday
							intPrice = (((arrRate(3,intCount)-arrRate(5,intCount))/arrRate(5,intCount))+1)*(arrRate(5,intCount) + arrRate(7,intCount))
						ElseIF arrRate(6,intCount) >0 Then 'Week End
							IF WeekDay(datePrice,VBMonday)=5 OR WeekDay(datePrice,VBMonday)=6 Then
								intPrice = (((arrRate(3,intCount)-arrRate(5,intCount))/arrRate(5,intCount))+1)*(arrRate(5,intCount) + arrRate(6,intCount))
							Else
								intPrice = (((arrRate(3,intCount)-arrRate(5,intCount))/arrRate(5,intCount))+1)*(arrRate(5,intCount))
							End IF
							
						End IF
				END SELECT

		Case 2 'Price Rack
			For intCount=0 To Ubound(arrRate,2)
				IF (datePrice>=arrRate(1,intCount) AND datePrice<=arrRate(2,intCount)) AND Cstr(intOptionID)=Cstr(arrRate(0,intCount))Then
					intPrice = arrRate(4,intCount)
					EXIT FOR
				End IF
			Next
				IF intPrice>0 Then '### Use for detect price rack=0 ###
					intDayType = functoin_date_check(datePrice)
							
					SELECT CASE intDayType
						Case 1 'Normal Day
						Case 2 'Weekend
							IF arrRate(6,intCount) >0 Then
								intPrice = arrRate(5,intCount) + arrRate(6,intCount)
							End IF
						Case 3 'Holiday
							IF arrRate(7,intCount) >0 Then
								intPrice = arrRate(5,intCount) + arrRate(7,intCount)
							End IF
					Case 4 'Holiday and Weekend
						IF arrRate(7,intCount) >0 Then
							intPrice = arrRate(5,intCount) + arrRate(7,intCount) 'Holiday
						ElseIF arrRate(6,intCount) >0 Then
							intPrice = arrRate(5,intCount) + arrRate(6,intCount) 'Weekend
						End IF
					Case 5 'Long Week End
						IF arrRate(8,intCount) >0 Then 'Long Week End
							intPrice = arrRate(5,intCount) + arrRate(8,intCount)
						ElseIF arrRate(7,intCount) >0 Then 'Holiday
							intPrice = arrRate(5,intCount) + arrRate(7,intCount)
						ElseIF arrRate(6,intCount) >0 Then 'Week End
							intPrice = arrRate(5,intCount) + arrRate(6,intCount)
						End IF
					END SELECT
				End IF
			
		Case 3 'Price Own
			For intCount=0 To Ubound(arrRate,2)
				IF (datePrice>=arrRate(1,intCount) AND datePrice<=arrRate(2,intCount)) AND Cstr(intOptionID)=Cstr(arrRate(0,intCount))Then
					intPrice = arrRate(5,intCount)
					EXIT FOR
				End IF
			Next
				
				SELECT CASE intDayType
					Case 1 'Normal Day
					Case 2 'Weekend
						IF arrRate(6,intCount) >0 Then
							intPrice = arrRate(5,intCount) + arrRate(6,intCount)
						End IF
					Case 3 'Holiday
						IF arrRate(7,intCount) >0 Then
							intPrice = arrRate(5,intCount) + arrRate(7,intCount)
						End IF
					Case 4 'Holiday and Weekend
						IF arrRate(7,intCount) >0 Then
							intPrice = arrRate(5,intCount) + arrRate(7,intCount) 'Holiday
						ElseIF arrRate(6,intCount) >0 Then
							intPrice = arrRate(5,intCount) + arrRate(6,intCount) 'Weekend
						End IF
					Case 5 'Long Week End
						IF arrRate(8,intCount) >0 Then 'Long Week End
							intPrice = arrRate(5,intCount) + arrRate(8,intCount)
						ElseIF arrRate(7,intCount) >0 Then 'Holiday
							intPrice = arrRate(5,intCount) + arrRate(7,intCount)
						ElseIF arrRate(6,intCount) >0 Then 'Week End
							intPrice = arrRate(5,intCount) + arrRate(6,intCount)
						End IF
						
				END SELECT
	
	END SELECT
	

	
	function_gen_room_price = Round(intPrice)

	
END FUNCTION
%>