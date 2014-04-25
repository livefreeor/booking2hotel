<%
FUNCTION function_get_promotion_id(intOptionID,dateCheckIn,dateCheckOut,intQty,arrPromotion,intType)
'arrPromotion = promotion_id,option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num
	
	Dim intNight
	Dim intDayAdvance
	Dim intCount
	
	'###Check Promotion###
	IF IsArray(arrPromotion) Then
		intNight = DateDiff("d",dateCheckIn,dateCheckOut)
		intDayAdvance = DateDiff("d",Date,dateCheckIn)
		
		For intCount=0 To Ubound(arrPromotion,2)
			IF Cstr(intOptionID)=Cstr(arrPromotion(1,intCount)) AND Cint(arrPromotion(4,intCount))<=intNight AND Cint(intQty)>=Cint(arrPromotion(3,intCount)) AND (Cint(intDayAdvance)>=Cint(arrPromotion(12,intCount)))Then
				function_get_promotion_id =  arrPromotion(0,intCount)
				Exit For
			End IF
		Next
	End IF
	'###Check Promotion###
END FUNCTION
%>