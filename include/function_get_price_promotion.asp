<%
FUNCTION function_get_price_promotion(intPrice,intOptionID,dateCheckIn,dateCheckOut,dateCurrent,intQty,arrPromotion,intType)
	'arrPromotion = promotion_id,option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num
	'Type1= Normal price use for display
	'Type2=Price use for calculate average
	Dim intCount
	
	For intCount=0 To Ubound(arrPromotion,2)
		IF Cint(arrPromotion(1,intCount))=Cint(intOptionID) Then
			function_get_price_promotion = function_cal_promotion(intPrice,dateCheckIn,dateCheckOut,dateCurrent,intQty,arrPromotion(2,intCount),arrPromotion(3,intCount),arrPromotion(4,intCount),arrPromotion(5,intCount),arrPromotion(6,intCount),arrPromotion(7,intCount),arrPromotion(8,intCount),arrPromotion(9,intCount),intType,arrPromotion(12,intCount))
			Exit For
		Else
			SELECT CASE intType
				Case 1 'Use for display
					function_get_price_promotion = Round(intPrice)
				Case 2 'Use for calculate average price
					function_get_price_promotion = Round(intPrice)
			END SELECT
		End IF
	Next

END FUNCTION


FUNCTION function_cal_promotion(intPrice,dateCheckIn,dateCheckOut,dateCurrent,intQty,intTypeID,intQtyMin,intDayMin,intDayDiscountStart,intDayDiscountNum,intDiscount,intFreeOptpionID,intFreeOptionQty,intType,intDayAdvanceMin)
	
	Dim intNight
	Dim bolQty
	Dim bolDay
	Dim bolDayCurrent
	Dim bolDayAdvance
	Dim strPrice
	Dim intCount
	Dim intDayCurrent
	Dim intDiscountSet
	Dim intSetCount
	Dim intWeek
	Dim intDiscountPrice
	Dim intDayAdvence
	
	intNight = DateDiff("d",dateCheckIn,dateCheckOut)
	intDayDiscountStart = Cint(intDayDiscountStart)
	intDayCurrent = DateDiff("d",dateCheckin,dateCurrent) + 1
	intDiscountSet = intNight/intDayMin
	IF intDiscountSet>Round(intDiscountSet) Then
		intDiscountSet = Round(intDiscountSet)
	ElseIF intDiscountSet<Round(intDiscountSet) Then
		intDiscountSet = Round(intDiscountSet) -1
	End IF
	intDayAdvence = DateDiff("d",Date(),dateCheckin)
	
	For intCount=1 To 20
		IF intDayMin*intCount>=intDayCurrent Then
			intWeek = intCount
			EXIT FOR
		End IF
	Next

	'### Check Minimum Quantity ###
		bolQty = True
	'### Check Minimum Quantity ###
	
	'## Check Minimum Day ###
	IF Cint(intNight)>=Cint(intDayMin) Then
		bolDay =True
	Else
		bolDay =False
	End IF
	'## Check Minimum Day ###

	'### Check Current Date ###
	For intCount=0 To intDayDiscountNum-1
		IF ((intDayCurrent - (intDayMin*(intWeek-1))) = (intDayDiscountStart+intCount)) AND (intDiscountSet>= intWeek) Then
			bolDayCurrent = True
			EXIT FOR
		Else
			bolDayCurrent = False
		End IF
	Next
	'### Check Current Date ###
	
	'### Check Number of Advece Day ###
	IF intDayAdvence>=intDayAdvanceMin Then
		bolDayAdvance = True
	Else
		bolDayAdvance = False
	End IF
	'### Check Number of Advece Day ###
	
	SELECT CASE Cint(intTypeID)
		Case 1 '### FREE DAY (book 5 free 1) ###
			IF bolQty AND bolDay AND bolDayCurrent AND bolDayAdvance Then 'Discount Date
				SELECT CASE intType
					Case 1'Use for display
						strPrice = "<div class=""ss"">" & function_display_price(intPrice,intType) & "</div><b><font color=""green"">Free</font></b>" 
					Case 2'Use for calculate average price
						strPrice = 0
				END SELECT
			Else 'Non Discount Date
				SELECT CASE intType
					Case 1'Use for display
						strPrice = intPrice
					Case 2'Use for calculate average price
						strPrice = intPrice
					END SELECT
			End IF
			
		Case 2 '### % DISCOUNT FOR ALL DAY (BOOK 3 GET 50% OFF YOUR BOOKING) ### 
			IF bolQty AND bolDay AND bolDayAdvance Then 'Discount date
				intDiscountPrice = intPrice * (100-intDiscount)/100
				SELECT CASE intType
					Case 1 'Use for display
						strPrice = "<div class=""ss"">" & function_display_price(intPrice,intType) & "</div><b><font color=""green"">"& function_display_price(intDiscountPrice,intType) &"</font></b>" 
					Case 2 'Use for calculate average price
						strPrice = intDiscountPrice
				END SELECT
			Else 'None Discount Date
				SELECT CASE intType
					Case 1'Use for display
						strPrice = intPrice
					Case 2'Use for calculate average price
						strPrice = intPrice
					END SELECT
			End IF
			
			
		Case 3 '### % DISCOUNT FOR SPECIFIC DAY (BOOK 3 GET 50% OFF ON 3th DAY) ### 
			IF bolQty AND bolDay AND bolDayCurrent AND bolDayAdvance Then 'Discount Date
				intDiscountPrice = intPrice * (100-intDiscount)/100
				SELECT CASE intType
					Case 1 'Use for display
						strPrice = "<div class=""ss"">" & function_display_price(intPrice,intType) & "</div><b><font color=""green"">"& function_display_price(intDiscountPrice,intType) &"</font></b>" 
					Case 2 'Use for calculate average price
						strPrice = intDiscountPrice
				END SELECT
			Else 'None Discount Date
				SELECT CASE intType
					Case 1'Use for display
						strPrice = intPrice
					Case 2'Use for calculate average price
						strPrice = intPrice
					END SELECT
			End IF
			
			
		Case 4 '### CASH FOR SPECIFIC DAY (BOOK 3 GET 500 BAHT OFF ON 3th DAY) ### 
			IF bolQty AND bolDay AND bolDayCurrent AND bolDayAdvance Then 'Discount Date
				intDiscountPrice = intPrice - intDiscount
				SELECT CASE intType
					Case 1 'Use for display
						strPrice = "<div class=""ss"">" & function_display_price(intPrice,intType) & "</div><b><font color=""green"">"& function_display_price(intDiscountPrice,intType) &"</font></b>" 
					Case 2 'Use for calculate average price
						strPrice = intDiscountPrice
				END SELECT
			Else 'None Discount Date
				SELECT CASE intType
					Case 1'Use for display
						strPrice = intPrice
					Case 2'Use for calculate average price
						strPrice = intPrice
					END SELECT
			End IF
			
		Case 5 '### GET FREE EXTRA OPTION (BOOK 5 NIGHT PAY ONLY 3 NIGHT PLUS 300 BAHT ON EXTRA NIGHT) ### 
			IF bolQty AND bolDay AND bolDayCurrent AND bolDayAdvance Then 'Discount Date
				intDiscountPrice = intDiscount
				SELECT CASE intType
					Case 1 'Use for display
						strPrice = "<div class=""ss"">" & function_display_price(intPrice,intType) & "</div><b><font color=""green"">"& function_display_price(intDiscountPrice,intType) &"</font></b>" 
					Case 2 'Use for calculate average price
						strPrice = intDiscountPrice
				END SELECT
			Else 'None Discount Date
				SELECT CASE intType
					Case 1'Use for display
						strPrice = intPrice
					Case 2'Use for calculate average price
						strPrice = intPrice
					END SELECT
			End IF
			
		Case 6 '### BAHT DISCOUNT FOR ALL DAY (BOOK 3 GET 500 BAHT OFF YOUR BOOKING) ### 
			IF bolQty AND bolDay AND bolDayAdvance Then 'Discount date
				intDiscountPrice = intPrice - intDiscount
				SELECT CASE intType
					Case 1 'Use for display
						strPrice = "<div class=""ss"">" & function_display_price(intPrice,intType) & "</div><b><font color=""green"">"& function_display_price(intDiscountPrice,intType) &"</font></b>" 
					Case 2 'Use for calculate average price
						strPrice = intDiscountPrice
				END SELECT
			Else 'None Discount Date
				SELECT CASE intType
					Case 1'Use for display
						strPrice = intPrice
					Case 2'Use for calculate average price
						strPrice = intPrice
					END SELECT
			End IF
			
		Case 7 '### GET FREE EXTRA OPTION (BOOK 5 NIGHT GET FREE AIRPORT TRANSFER) ### 
		Case 8 '### GET % DISCOUNT FOR EXTRA OPTION (BOOK 2 NIGHT GET 50% OFF AIRPORT TRANSFER) ### 
		Case 9 '### CASH DISCOUNT FOR ALL DAY (BOOK 3 GET 500 Baht OFF YOUR BOOKING) ### 
		Case 10 '### GET CASH DISCOUNT FOR EXTRA OPTION (BOOK 2 NIGHT GET 500 BAHT OFF AIRPORT TRANSFER) ### 
	END SELECT
	
	function_cal_promotion = strPrice

END FUNCTION
%>