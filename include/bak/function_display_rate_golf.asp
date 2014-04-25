<%
FUNCTION function_display_rate_golf(intDayCheckIn,intMonthCheckIn,intYearCheckIn,intProductID,arrAllot,intType)

	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim intNumRate
	Dim bolRate
	Dim strPrice
	Dim strPriceDetail
	Dim j,k,u
	Dim strColor
	Dim strRoomType
	Dim dateStartTmp
	Dim dateEndTmp
	Dim dateCheckIn
	Dim dateCheckOut
	Dim intRateCount
	Dim sqlType
	Dim recType
	Dim arrType
	Dim bolType
	Dim sqlPromotion
	Dim recPromotion
	Dim arrPromotion
	Dim bolPromotion
	Dim intCount
	Dim intPrice
	Dim dateCurrent
	Dim intRatetype
	
	SELECT CASE intType
		Case 1 '### Without date input ###
			sqlRate = "SELECT op.date_start, op.date_end, o.title_en, op.price,op.price_rack,p.breakfast,o.show_option,o.option_id,p.files_name,p.destination_id,"
			sqlRate = sqlRate & " (SELECT COUNT(sa.option_id) FROM tbl_allotment sa WHERE sa.status=1 AND sa.option_id=o.option_id AND sa.date_allotment>="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&") AS num_allot,"
			sqlRate = sqlRate & "op.price_own,op.sup_weekend,op.sup_long"
			sqlRate = sqlRate & " FROM tbl_option_price op, tbl_product_option o, tbl_product p"
			sqlRate = sqlRate & " WHERE op.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND p.product_id=o.product_id AND o.option_id=op.option_id AND o.status=1 AND o.option_cat_id=48 AND p.product_id=" & intProductID
			sqlRate = sqlRate & " ORDER BY date_start ASC, date_end ASC, option_priority ASC, price ASC"
			
			Set recRate = Server.CreateObject ("ADODB.Recordset")
			recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
				IF NOT recRate.EOF Then
					arrRate = recRate.GetRows()
					intNumRate = Ubound(arrRate,2)
					intNumRate = intNumRate + 1
					bolRate = True
				Else
					bolRate = False
				End IF
			recRate.Close
			Set recRate = Nothing
			
			IF bolRate Then
				strPrice  = "<table width=""100%"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strPrice  = strPrice  & "<tr>" & VbCrlf
				strPrice  = strPrice  & "<td rowspan=""2"" align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">Period</font></strong></td>" & VbCrlf
				strPrice  = strPrice  & "<td rowspan=""2"" align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">Type</font></strong></td>" & VbCrlf
				strPrice  = strPrice  & "<td colspan=""2"" align=""center"" bgcolor=""#EDF5FE""><font color=""#346494""><strong>Rate</strong> ("&Session("currency_title")&" <img src=""/images/flag_"&Session("currency_code")&".gif"" /> )</font></td>" & VbCrlf
				strPrice  = strPrice  & "</tr>" & VbCrlf
				strPrice  = strPrice  & "<tr>" & VbCrlf
				strPrice  = strPrice  & "<td width=""120"" align=""center"" bgcolor=""#EDF5FE""><font color=""#990066"">Week Day </font></td>" & VbCrlf
				strPrice  = strPrice  & "<td width=""120"" align=""center"" bgcolor=""#EDF5FE""><font color=""#990066"">Week End &amp; Holiday </font></td>" & VbCrlf
				strPrice  = strPrice  & "</tr>" & VbCrlf

  
    			For k=0 To Ubound(arrRate,2)
					'###Change option title to link ###
					strRoomType = function_gen_option_title_golf(arrRate(7,k),arrRate(9,k),arrRate(8,k),arrRate(2,k),arrRate(6,k),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,arrRate(10,k),1)
					'###Change option title to link ###
					intRateType=1
    				IF arrRate(12,k)<>"" or arrRate(12,k)<> 0 Then
						intRateType=2
					End IF
					
					IF arrRate(13,k)<>"" or arrRate(13,k)<> 0 Then
						intRateType=3
					End IF
    			
					IF dateStartTmp=arrRate(0,k) AND dateEndTmp=arrRate(1,k) Then '### Other Rows Without Date ###
						strPriceDetail = strPriceDetail & "<tr>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td bgcolor="&strColor&"><font color=""#666600"">"&strRoomType&"</font></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td align=""center"" bgcolor="&strColor&"><strong><font color=""#990000"">"& function_display_price(arrRate(3,k),1) &"</font></strong></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td align=""center"" bgcolor="&strColor&"><strong><font color=""#990000"">"& function_display_price(function_gen_price_golf(arrRate(3,k),arrRate(11,k),arrRate(12,k),arrRate(13,k),2),1)&"</font></strong></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "</tr>" & VbCrlf

						u = u + 1
						
					Else '### First Row With Date ###
					
						IF j MOD 2 = 0 Then
							strColor = """#FFFFFF"""
						Else
							strColor = """#F8FCF3"""
						End IF
						
						strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
						strPriceDetail = strPriceDetail & "<tr>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td rowspan=""intRowSpan"" bgcolor="&strColor&">"& function_date(arrRate(0,k),3) &" - " & function_date(arrRate(1,k),3) & "</td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td bgcolor="&strColor&"><font color=""#666600"">"&strRoomType&"</font></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td align=""center"" bgcolor="&strColor&"><strong><font color=""#990000"">"& function_display_price(arrRate(3,k),1) &"</font></strong></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td align=""center"" bgcolor="&strColor&"><strong><font color=""#990000"">"& function_display_price(function_gen_price_golf(arrRate(3,k),arrRate(11,k),arrRate(12,k),arrRate(13,k),2),1)&"</font></strong></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "</tr>" & VbCrlf

						u = 1
						j = j + 1
					End IF
					
					dateStartTmp = DateSerial(Year(arrRate(0,k)),Month(arrRate(0,k)),Day(arrRate(0,k)))
					dateEndTmp = DateSerial(Year(arrRate(1,k)),Month(arrRate(1,k)),Day(arrRate(1,k)))
					
				Next
				
				strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
				strPrice = strPrice & strPriceDetail
				strPrice = strPrice & "</table>" & VbCrlf
				
				function_display_rate_golf = strPrice
			End IF
			
		Case 2 '### With date input ###
			dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
			dateCheckOut = DateAdd("d",1,dateCheckIn)
			dateCurrent = dateCheckIn
			intRateCount = 0

			sqlType = "SELECT DISTINCT op.option_id,o.title_en,"
			sqlType = sqlType & " (SELECT 'none') AS meal"
			sqlType = sqlType & " ,o.option_priority,p.breakfast,o.show_option,p.files_name,p.destination_id"
			sqlType = sqlType & " FROM tbl_option_price op, tbl_product_option o,tbl_product p"
			sqlType = sqlType & " WHERE p.product_id=o.product_id AND op.option_id=o.option_id AND o.status=1 AND o.option_cat_id=48 AND   ((op.date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&")) AND p.product_id=" & intProductID
			sqlType = sqlType & " ORDER BY o.option_priority,op.option_id ASC"
			
			Set recType = Server.CreateObject ("ADODB.Recordset")
			recType.Open SqlType, Conn,adOpenStatic,adLockreadOnly
				IF NOT recType.EOF Then
					arrType = recType.GetRows()
					bolType = True
				Else
					bolType = False
				End IF
			recType.Close
			Set recType = Nothing 
			
			IF bolType Then 'IF have selected course
			
				sqlRate = "SELECT op.option_id,op.date_start,op.date_end,op.price,op.price_rack,price_own,sup_weekend,sup_holiday,sup_long "
				sqlRate = sqlRate & " FROM tbl_product_option po,tbl_option_price op"
				sqlRate = sqlRate & " WHERE po.option_id=op.option_id AND po.status=1 AND po.option_cat_id=48 AND ( op.date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&") AND po.product_id=" & intProductID
				'response.write sqlRate
				
				Set recRate = Server.CreateObject ("ADODB.Recordset")
				recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
					arrRate = recRate.GetRows()
				recRate.Close
				Set recRate = Nothing 
				
				sqlPromotion = "SELECT promotion_id,pm.option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num"
				sqlPromotion = sqlPromotion & " FROM tbl_promotion pm, tbl_product_option po"
				sqlPromotion = sqlPromotion & " WHERE po.option_id=pm.option_id AND (date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND date_end>="&function_date_sql(intDayCheckIn,intMonthCheckIn,intYearCheckIn,1)&") AND pm.status=1 AND day_min<=1 AND po.product_id=" & intProductID
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
			End IF
			
			strPrice = "<table width=""100%"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
			strPrice = strPrice & "<tr>" & VbCrlf
			strPrice = strPrice & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">Type</font></strong></td>" & VbCrlf
			strPrice = strPrice & "<td align=""center"" bgcolor=""#EDF5FE""><font color=""#346494""><strong>Rate</strong> ("&Session("currency_title")&" <img src=""/images/flag_"&Session("currency_code")&".gif"" /> ) <font color=""#990066"">Date: "&function_date(dateCheckIn,5)&"</font></font><br /></td>" & VbCrlf
			strPrice = strPrice & "</tr>" & VbCrlf

			For intCount = 0 To Ubound(arrType,2)
				
				strRoomType = function_gen_option_title_golf(arrType(0,intCount),arrType(7,intCount),arrType(6,intCount),arrType(1,intCount),arrType(5,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,0,2)
			 	
				intPrice = function_gen_room_price(arrType(0,intCount),dateCurrent,arrRate,1)
				
				IF bolPromotion Then
					intPrice = function_get_price_promotion(intPrice,arrType(0,intCount),dateCheckIn,dateCheckOut,dateCurrent,0,arrPromotion,1)
				End IF
	
				IF intCount MOD 2 =0 Then
					strColor = """#FFFFFF"""
				Else
					strColor = """#F8FCF3"""
				End IF
	
				strPrice = strPrice & "<tr>" & VbCrlf
				strPrice = strPrice & "<td bgcolor="&strColor&"><font color=""#666600"">"&strRoomType&"</font></td>" & VbCrlf
				strPrice = strPrice & "<td align=""center"" bgcolor="&strColor&"><strong><font color=""#990000"">"&function_display_price(intPrice,1)&"</font></strong></td>" & VbCrlf
				strPrice = strPrice & "</tr>" & VbCrlf
				
    		Next

			strPrice = strPrice & "</table>" & VbCrlf

			function_display_rate_golf = strPrice
	
	END SELECT
END FUNCTION
%>