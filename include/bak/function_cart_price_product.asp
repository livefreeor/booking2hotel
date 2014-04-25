<%
Function function_cart_price_product(intCartProduct,intProductID,intProductCatID,dateStart,dateEnd,intAdult,intChild,intGolfer,intPriceType)

	Dim sqlCartItem
	Dim recCartItem
	Dim arrCartItem
	Dim intCountItem
	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim sqlPromotion
	Dim recPromotion
	Dim arrPromotion
	Dim bolPromotion
	Dim intNight
	Dim intAvgRate
	Dim intPrice
	Dim intPriceTotal
	
	intNight = DateDiff("d",dateStart,dateEnd)
	
		sqlRate = "SELECT op.option_id,op.date_start,op.date_end,op.price,op.price_rack,price_own,sup_weekend,sup_holiday,sup_long"
		sqlRate = sqlRate & " FROM tbl_product_option po,tbl_option_price op"
		sqlRate = sqlRate & " WHERE po.option_id=op.option_id AND po.status=1  AND ((op.date_start<="&function_date_sql(Day(dateStart),Month(dateStart),Year(dateStart),1)&" AND op.date_end>="&function_date_sql(Day(dateStart),Month(dateStart),Year(dateStart),1)&") OR (op.date_start<="&function_date_sql(Day(dateEnd),Month(dateEnd),Year(dateEnd),1)&" AND date_end>="&function_date_sql(Day(dateEnd),Month(dateEnd),Year(dateEnd),1)&")OR (op.date_start>="&function_date_sql(Day(dateStart),Month(dateStart),Year(dateStart),1)&" AND op.date_end<="&function_date_sql(Day(dateEnd),Month(dateEnd),Year(dateEnd),1)&")) AND po.product_id=" & intProductID
		'response.Write(sqlRate)
		'response.End()
		Set recRate = Server.CreateObject ("ADODB.Recordset")
		recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
			arrRate = recRate.GetRows()
		recRate.Close
		Set recRate = Nothing 
	
		sqlPromotion = "SELECT promotion_id,pm.option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num"
		sqlPromotion = sqlPromotion & " FROM tbl_promotion pm, tbl_product_option po"
		sqlPromotion = sqlPromotion & " WHERE po.option_id=pm.option_id AND (date_start<="&function_date_sql(Day(dateStart),Month(dateStart),Year(dateStart),1)&" AND date_end>="&function_date_sql(Day(dateEnd),Month(dateEnd),Year(dateEnd),1)&") AND pm.status=1 AND day_min<="& intNight &" AND po.product_id=" & intProductID
		sqlPromotion = sqlPromotion & " ORDER BY po.option_id ASC, day_min DESC"

		Set recPromotion  = Server.CreateObject ("ADODB.Recordset")
		recPromotion.Open SqlPromotion, Conn,adOpenStatic,adLockreadOnly
			IF NOT recPromotion.EOF Then
				arrPromotion = recPromotion.GetRows()
				bolPromotion = True
			Else
				bolPromotion = False
			End IF
		recPromotion.Close
		Set recPromotion = Nothing 
		
		Dim strConn2
		Dim Conn2
		
		strConn2 = "Provider=MSDASQL; Driver={SQL Server}; Server=74.86.253.60; Database=hotels2; UID=hotels2thailand; PWD=FdC$sdor#$;"
		
		Set Conn2= server.CreateObject("ADODB.Connection")
		Conn2.Open strConn2
		
		sqlCartItem = "SELECT ci.cart_item_id,ci.cart_product_id,ci.option_id,ci.quantity,po.option_cat_id,po.title_en,po.max_adult,ci.detail"
		sqlCartItem = sqlCartItem & " FROM tbl_cart_item ci, tbl_product_option po"
		sqlCartItem = sqlCartItem & " WHERE po.option_id=ci.option_id AND ci.cart_product_id=" & intCartProduct
		sqlCartItem = sqlCartItem & " ORDER BY po.option_cat_id ASC"
		Set recCartItem = Server.CreateObject ("ADODB.Recordset")
		'recCartItem.Open sqlCartItem, Conn,adOpenStatic,adLockreadOnly
		recCartItem.Open sqlCartItem, Conn2,adOpenStatic,adLockreadOnly
			arrCartItem = recCartItem.GetRows()
		recCartItem.Close
		Set recCartItem = Nothing
		
		Conn2.Close()
		Set Conn2 = Nothing
		
		For intCountItem=0 to Ubound(arrCartItem,2)
			IF bolPromotion Then
				intAvgRate = function_gen_room_price_average_promotion(arrCartItem(2,intCountItem),dateStart,dateEnd,1,arrPromotion,arrRate,1)
			Else
				intAvgRate = function_gen_room_price_average(arrCartItem(2,intCountItem),dateStart,dateEnd,arrRate,1)
			End IF
			
			SELECT CASE arrCartItem(4,intCountItem)
				Case 38 'Rooms
					intPrice = intAvgRate * arrCartItem(3,intCountItem) * intNight
					
				Case 39 'Extra bed
					intPrice = intAvgRate * arrCartItem(3,intCountItem) * intNight
					
				Case 40 'Breakfast
					intPrice = intAvgRate * arrCartItem(3,intCountItem) * intNight
					
				Case 43 'Airport Transfer ( Round - trip , per person )
					intPrice = intAvgRate * arrCartItem(3,intCountItem)
					
				Case 44 'Airport Transfer
					intPrice = intAvgRate * arrCartItem(3,intCountItem)
					
				Case 45 'Khantoke Dinner
					intPrice = intAvgRate * arrCartItem(3,intCountItem)
					
				Case 46 'Rollaway Bed
					intPrice = intAvgRate * arrCartItem(3,intCountItem) * intNight
					
				Case 47 'Gala Dinner
					intPrice = intAvgRate * arrCartItem(3,intCountItem)
					
				Case 48 'Green Fee
					intPrice = intAvgRate * arrCartItem(3,intCountItem)
					
				Case 52 'Sight Seeing
					intPrice = function_gen_price_sightseeing(dateStart,arrCartItem(2,intCountItem),intAdult,intChild,1)
					
				Case 53 'Water Activity
					intPrice = function_gen_price_sightseeing(dateStart,arrCartItem(2,intCountItem),intAdult,intChild,1)
					
				Case 54 'Shows & Events
					intPrice = function_gen_price_sightseeing(dateStart,arrCartItem(2,intCountItem),intAdult,intChild,1)
				Case 55 'Health & Check Up
					intPrice = intAvgRate * arrCartItem(3,intCountItem)
					
			END SELECT

				intPriceTotal = intPriceTotal + intPrice
		Next

		function_cart_price_product = intPriceTotal
	
End Function
%>