<%
FUNCTION function_gen_option_title_golf(intOptionID,intDestinationID,strFileName,strOptionTitle,bolShowOption,dateCheckIn,dateCheckOut,intQty,arrPromotion,arrAllot,intAllotment,intType)
'arrPromotion = promotion_id,option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num
	Dim strRoomFile
	Dim strRoomType
	Dim intCount
	Dim intNight
	Dim intDayAdvance
	Dim bolAllot
	Dim intAllotCount
	
	SELECT CASE intType
		Case 1 'Without date input
			strRoomType = strOptionTitle

		IF intAllotment>0 AND IsArray(arrAllot) Then
			For intAllotCount=0 To Ubound(arrAllot,2)
				IF Cstr(intOptionID)=Cstr(arrAllot(0,intAllotCount)) Then
					strRoomType = strRoomType & "<br><a href=""javascript:popup('/hotel_allotment_pre.asp?option_id="&intOptionID&"',370,420)""><img src=""/images/i_instant.gif"" alt=""Instant Confirmation"" border=""0""> <font color=""red"" class=""s"">Instant Confirmation <font color=""blue""><u>?</u></font></a>"
					Exit For
				End IF
			Next
		End IF


		Case 2 'With Date and promotion input
			
			intQty = 1
			
			strRoomType = strOptionTitle
			
			'###Check Promotion###
			IF IsArray(arrPromotion) Then
				intNight = DateDiff("d",dateCheckIn,dateCheckOut)
				intDayAdvance = DateDiff("d",Date,dateCheckIn)
				
				For intCount=0 To Ubound(arrPromotion,2)
					IF Cstr(intOptionID)=Cstr(arrPromotion(1,intCount)) AND Cint(arrPromotion(4,intCount))<=intNight AND Cint(arrPromotion(3,intCount))>=Cint(intQty) AND (Cint(intDayAdvance)>=Cint(arrPromotion(12,intCount)))Then
						strRoomType = strRoomType & "<br><img src=""/images/i_promotion.gif"" alt="""& Trim(arrPromotion(11,intCount)) &""">" 
						Exit For
					End IF
				Next
			End IF
			'###Check Promotion###
			
			'###Check Room Alottment###
			IF IsArray(arrAllot) Then
				bolAllot = function_allot_check_valid("",intOptionID,arrAllot,dateCheckIn,dateCheckOut,intQty,1)
				IF bolAllot Then
					strRoomType = strRoomType & "<br><img src=""/images/i_instant.gif"" alt=""Instant Confirmation""> <font color=""red"" class=""s"">Instant Confirmation "
					'strRoomType = strRoomType & "("& function_allotment_min("",intOptionID,arrAllot,1) &" Room(s))</font>"
				End IF
			End IF
			'###Check Room Alottment###
	END SELECT

	function_gen_option_title_golf = strRoomType

END FUNCTION
%>