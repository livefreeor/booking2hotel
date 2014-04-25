<%
FUNCTION function_display_rate_affiliate_res_step1()

	Dim strStep1
	Dim sqlProduct
	Dim recProduct
	Dim arrProduct
	Dim sqlItem
	Dim recItem
	Dim arrItem
	Dim dateCheckIn
	Dim dateCheckOut
	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim sqlPromotion
	Dim recPromotion
	Dim arrPromotion
	Dim bolPromotion
	Dim intNight
	Dim strOptionID
	Dim intCountItem
	Dim strNight
	Dim intQuantity
	Dim intAvgRoomRate
	Dim intAvgRoomRateOwn
	Dim strAllProductID
	Dim intPriceSubTotal
	Dim intPriceSubTotalOwn
	Dim intPriceTotal
	Dim strHiddenPriceOwn
	Dim strHiddenPrice
	Dim strHiddenPriceDisplay
	Dim strHiddenPromotion 
	
	dateCheckIn = DateSerial(Request.Form("ch_in_year"),Request.Form("ch_in_month"),Request.Form("ch_in_date"))
	dateCheckOut = DateSerial(Request.Form("ch_out_year"),Request.Form("ch_out_month"),Request.Form("ch_out_date"))
	intNight = DateDiff("d",dateCheckIn,dateCheckOut)
	strOptionID = Request.Form("option_id")
	
	IF Request.Form("airport_id")<>"" Then
		strOptionID = strOptionID & "," & Request.Form("airport_id")
	End IF
	
	IF Request.Form("bed_id")<>"" Then
		strOptionID = strOptionID & "," & Request.Form("bed_id")
	End IF
	
	IF Request.Form("gala_id")<>"" Then
		strOptionID = strOptionID & "," & Request.Form("gala_id")
	End IF
	
	IF Request.Form("extra")<>"" Then
		strOptionID = strOptionID & "," & Request.Form("extra")
	End IF
	
	SELECT CASE Cint(REquest.Form("desid"))
		Case 30 '### Bangkok ###
			strAllProductID = Request.Form("pdid") & ",609"
		Case 31 '### Phuket ###
			strAllProductID = Request.Form("pdid") & ",799"
		Case 32 '### Chiang Mai ###
			strAllProductID = Request.Form("pdid") & ",6094"
		Case 33 '### Pattaya ###
			strAllProductID = Request.Form("pdid") & ",610"
		'Case 34 '### Samui ###
		Case 35 '### Kabi ###
			strAllProductID = Request.Form("pdid") & ",606"
		CAse Else
			strAllProductID = Request.Form("pdid")
	END SELECT
	
	'### Product ###
	sqlProduct = "SELECT p.product_id,p.product_cat_id,p.title_en,p.title_th,p.product_code,p.destination_id,p.files_name,p.address_en"
	sqlProduct = sqlProduct & " FROM tbl_product p"
	sqlProduct = sqlProduct & " WHERE p.product_id=" & Request.Form("pdid")
	sqlProduct = sqlProduct & " ORDER BY p.product_cat_id,p.product_id ASC"
	Set recProduct = Server.CreateObject ("ADODB.Recordset")
	recProduct.Open sqlProduct, Conn,adOpenStatic,adLockreadOnly
			arrProduct = recProduct.GetRows()
	recProduct.Close
	Set recProduct = Nothing 
	'### Product ###
	
	'### Item ###
	sqlItem = "SELECT po.option_id,po.option_cat_id,po.title_en,po.max_adult"
	sqlItem = sqlItem & " FROM tbl_product_option po"
	sqlItem = sqlItem & " WHERE po.option_id IN ("&strOptionID&")"
	sqlItem = sqlItem & " ORDER BY po.option_cat_id ASC"
	Set recItem = Server.CreateObject ("ADODB.Recordset")
	recItem.Open sqlItem, Conn,adOpenStatic,adLockreadOnly
			arrItem = recItem.GetRows()
	recItem.Close
	Set recItem = Nothing 
	'### Item ###

	'### Rate ###
	sqlRate = "SELECT op.option_id,op.date_start,op.date_end,op.price,op.price_rack,price_own,sup_weekend,sup_holiday,sup_long"
	sqlRate = sqlRate & " FROM tbl_product_option po,tbl_option_price op"
	sqlRate = sqlRate & " WHERE po.option_id=op.option_id AND po.status=1  AND ((op.date_start<="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&" AND op.date_end>="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&") OR (op.date_start<="&function_date_sql(Day(dateCheckOut),Month(dateCheckOut),Year(dateCheckOut),1)&" AND date_end>="&function_date_sql(Day(dateCheckOut),Month(dateCheckOut),Year(dateCheckOut),1)&")OR (op.date_start>="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&" AND op.date_end<="&function_date_sql(Day(dateCheckOut),Month(dateCheckOut),Year(dateCheckOut),1)&")) AND po.product_id IN ("&strAllProductID &")"
	Set recRate = Server.CreateObject ("ADODB.Recordset")
	recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
		arrRate = recRate.GetRows()
	recRate.Close
	Set recRate = Nothing 
	'### Rate ###

	'### Promotion ###
	sqlPromotion = "SELECT promotion_id,pm.option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num,offer_id"
	sqlPromotion = sqlPromotion & " FROM tbl_promotion pm, tbl_product_option po"
	sqlPromotion = sqlPromotion & " WHERE po.option_id=pm.option_id AND (date_start<="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&" AND date_end>="&function_date_sql(Day(dateCheckOut),Month(dateCheckOut),Year(dateCheckOut),1)&") AND pm.status=1 AND day_min<="& intNight &" AND po.product_id=" & Request.Form("pdid")
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
	'### Promotion ###

	strStep1 = "<table width=""98%"" cellspacing=""1"" cellpadding=""2"" bgcolor=""#E4E4E4"">" & VbCrlf
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & "<td align=""left"" class=""step_bg_color""><span class=""step_num"">Step 1 :</span> <span class=""step_text"">Review Your Detail</span></td>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
	
	
	strStep1 = strStep1 & "<p>" & VbCrlf
	strStep1 = strStep1 & "<table width=""85%"" cellpadding=""2"" cellspacing=""0"" bgcolor=""#FBFBFB"">" & VbCrlf
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & "<td><span class=""room_text"">Hotel:</span> <span class=""valid_text"">"&arrProduct(2,0)&"</span></td>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & "<td><span class=""room_text"">Period:</span> <span class=""valid_text"">"&function_date(dateCheckIn,5)&" - "&function_date(datecheckOut,5)&"</span></td>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & "<td><span class=""room_text"">Adult(s):</span> <span class=""valid_text"">"&Request.Form("adult")&"</span>   , <span class=""room_text"">Children:</span> <span class=""valid_text"">"&Request.Form("children")&"</span></td>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & "<td align=""center"">" & VbCrlf
	strStep1 = strStep1 & "<table width=""90%"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & "<th align=""center"">Product</th>" & VbCrlf
	strStep1 = strStep1 & "<th align=""center"">Quantity</th>" & VbCrlf
	strStep1 = strStep1 & "<th align=""center"">Night(s)</th>" & VbCrlf
	strStep1 = strStep1 & "<th align=""center"">Avg. Rate</th>" & VbCrlf
	strStep1 = strStep1 & "<th align=""center"">Subtotal</th>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	
	For intCountItem=0 To Ubound(arrITem,2)

	IF bolPromotion Then
		intAvgRoomRate = function_gen_room_price_average_promotion(arrItem(0,intCountItem),dateCheckIn,dateCheckOut,1,arrPromotion,arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average_promotion(arrItem(0,intCountItem),dateCheckIn,dateCheckOut,1,arrPromotion,arrRate,3)
	Else
		intAvgRoomRate = function_gen_room_price_average(arrItem(0,intCountItem),dateCheckIn,dateCheckOut,arrRate,1)
		intAvgRoomRateOwn = function_gen_room_price_average(arrItem(0,intCountItem),dateCheckIn,dateCheckOut,arrRate,3)
	End IF

		SELECT CASE arrItem(1,intCountItem)
			Case 38 '## Room ###
				strNight = intNight
				intQuantity = Request.Form("qty" & arrItem(0,intCountItem))
				intPriceSubTotal = intAvgRoomRate*intNight*intQuantity
				intPriceSubTotalOwn = intAvgRoomRateOwn*intNight*intQuantity
			Case 39 '## Extra Bed ###
				strNight = intNight
				intQuantity = Request.Form("bed" & arrItem(0,intCountItem))
				intPriceSubTotal = intAvgRoomRate*intNight*intQuantity
				intPriceSubTotalOwn = intAvgRoomRateOwn*intNight*intQuantity
			Case 44 '## Transfer ###
				strNight = "-"
				intQuantity = Request.Form("airport" & arrItem(0,intCountItem))
				intPriceSubTotal = intAvgRoomRate*intQuantity
				intPriceSubTotalOwn = intAvgRoomRateOwn*intQuantity
				Session("transfer") = 1
			Case 47 '## Gala ###
				strNight = "-"
				intQuantity = Request.Form("qty" & arrItem(0,intCountItem))
				intPriceSubTotal = intAvgRoomRate*intQuantity
				intPriceSubTotalOwn = intAvgRoomRateOwn*intQuantity
			Case Else '## Other Option ###
				strNight = "-"
				intQuantity = Request.Form("qty" & arrItem(0,intCountItem))
				intPriceSubTotal = intAvgRoomRate*intQuantity
				intPriceSubTotalOwn = intAvgRoomRateOwn*intQuantity
		END SELECT
		
		intPriceTotal = intPriceTotal + intPriceSubTotal
		
		strHiddenPriceOwn = strHiddenPriceOwn & "<input type=""hidden"" name=""priceown"&arrItem(0,intCountItem)&""" value="""&intPriceSubTotalOwn &""">" & VbCrlf
		strHiddenPrice = strHiddenPrice & "<input type=""hidden"" name=""price"&arrItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
		strHiddenPriceDisplay = strHiddenPriceDisplay & "<input type=""hidden"" name=""pricedisplay"&arrItem(0,intCountItem)&""" value="""&intPriceSubTotal&""">" & VbCrlf
		
		IF bolPromotion Then
			strHiddenPromotion = strHiddenPromotion & "<input type=""hidden"" name=""promotion"&arrItem(0,intCountItem)&""" value="""&function_get_promotion_id(arrItem(0,intCountItem),dateCheckIn,dateCheckOut,intQuantity,arrPromotion,1)&""">" & VbCrlf
		End IF
		
		strStep1 = strStep1 & "<tr>" & VbCrlf
		strStep1 = strStep1 & "<td bgcolor=""#FFFFFF"">"&arrItem(2,intCountItem)&"</td>" & VbCrlf
		strStep1 = strStep1 & "<td align=""center"" bgcolor=""#FFFFFF"">"&intQuantity&"</td>" & VbCrlf
		strStep1 = strStep1 & "<td align=""center"" bgcolor=""#FFFFFF"">"&strNight&"</td>" & VbCrlf
		strStep1 = strStep1 & "<td align=""center"" bgcolor=""#FFFFFF"">"&function_display_price(intAvgRoomRate,3)&"</td>" & VbCrlf
		strStep1 = strStep1 & "<td align=""center"" bgcolor=""#FFFFFF"">"&function_display_price(intPriceSubTotal,3)&"</td>" & VbCrlf
		strStep1 = strStep1 & "</tr>" & VbCrlf
	Next
	
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & strHiddenPriceOwn
	strStep1 = strStep1 & strHiddenPrice
	strStep1 = strStep1 & strHiddenPriceDisplay
	strStep1 = strStep1 & strHiddenPromotion
	strStep1 = strStep1 & "<input type=""hidden"" name=""price_total_all"" value="""&intPriceTotal&""">" & VbCrlf
	strStep1 = strStep1 & "<td colspan=""4"" align=""right"" bgcolor=""#FFFFFF""><span class=""price_text"">Total Price:</span></td>" & VbCrlf
	strStep1 = strStep1 & "<td align=""center"" bgcolor=""#FFFFFF"">"&function_display_price(intPriceTotal,3)&"&nbsp;Baht</td>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	strStep1 = strStep1 & "</table></td>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	strStep1 = strStep1 & "</table>" & VbCrlf
	strStep1 = strStep1 & "</p>" & VbCrlf
	strStep1 = strStep1 & "<table width=""90%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#FF6600"">" & VbCrlf
	strStep1 = strStep1 & "<tr>" & VbCrlf
	strStep1 = strStep1 & "<td align=""center"" bgcolor=""#FFFFFF""><span class=""price_text"">Total Price</span> : <b><span class=""valid_text"">"&function_display_price(intPriceTotal,3)&"8&nbsp;Baht&nbsp;  </span></b></td>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	strStep1 = strStep1 & "</table>" & VbCrlf
	strStep1 = strStep1 & "<p>If this information is not correct, Click <a href=""/cart_view.asp"">Back</a> to Edit your Booking Detail</p>" & VbCrlf
	strStep1 = strStep1 & "</td>" & VbCrlf
	strStep1 = strStep1 & "</tr>" & VbCrlf
	strStep1 = strStep1 & "</table>" & VbCrlf
	
	Session("intPriceTotal") = intPriceTotal
	function_display_rate_affiliate_res_step1 = strStep1
	
END FUNCTION
%>


		                    

		                    
