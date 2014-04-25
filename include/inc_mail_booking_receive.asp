
<%

FUNCTION inc_mail_booking_receive(intOrderID)

	Dim strBody
	Dim sqlOrderProduct
	Dim recOrderProduct
	Dim arrOrderProduct
	Dim intCountOrderProdutSub
	Dim strFlightA
	Dim strFlightD
	Dim sqlOrderItem
	Dim recOrderItem
	Dim arrOrderItem
	Dim intCountOrderItemSub
	Dim intNight
	Dim intPriceSub
	Dim intPriceTotal
	Dim intRate
	Dim strNight
	
	sqlOrderProduct = "SELECT o.order_id,o.full_name,o.email,p.title_en,op.date_time_check_in,op.date_time_check_out,op.num_adult,op.num_child,p.product_cat_id,num_golfer,o.flight_arrival_number,o.flight_arrival_time,o.flight_departure_number,o.flight_departure_time,op.order_product_id,p.product_id,op.period_id "
	sqlOrderProduct = sqlOrderProduct & " FROM tbl_order_product op, tbl_order o, tbl_product p"
	sqlOrderProduct = sqlOrderProduct & " WHERE o.order_id=op.order_id AND p.product_id=op.product_id AND o.order_id=" & intOrderID
	sqlOrderProduct = sqlOrderProduct & " ORDER BY op.date_time_check_in ASC,op.order_product_id ASC"
	Set recOrderProduct = Server.CreateObject ("ADODB.Recordset")
	recOrderProduct.Open SqlOrderProduct, Conn,adOpenStatic,adLockreadOnly
		arrOrderProduct = recOrderProduct.GetRows()
	recOrderProduct.Close
	Set recOrderProduct = Nothing 

	IF arrOrderProduct(10,0)<> "" AND NOT ISNULL(arrOrderProduct(10,0)) Then
		strFlightA = arrOrderProduct(10,0) & "&nbsp;" & function_date(arrOrderProduct(11,0),4)
	Else
		strFlightA = "N/A"
	End IF
	
	IF arrOrderProduct(12,0)<> "" AND NOT ISNULL(arrOrderProduct(12,0)) Then
		strFlightD = arrOrderProduct(12,0) & "&nbsp;" & function_date(arrOrderProduct(13,0),4)
	Else
		strFlightD = "N/A"
	End IF
	
	intPriceSub = 0
	intPriceTotal = 0
	
	strBody = "<html>" & VbCrlf
	strBody = strBody & "<head>" & VbCrlf
	strBody = strBody & "<title>Booking Received</title>" & VbCrlf
	strBody = strBody & "<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-1"">" & VbCrlf
	strBody = strBody & "<link href=""http://www.hotels2thailand.com/css/css.css"" rel=""stylesheet"" type=""text/css"">" & VbCrlf
	strBody = strBody & "</head>" & VbCrlf
	strBody = strBody & "<body bgcolor=""#FFFFFF"" leftmargin=""0"" topmargin=""0"" marginwidth=""0"" marginheight=""0"">" & VbCrlf
	strBody = strBody & "<DIV align=""center"">" & VbCrlf
	strBody = strBody & "<TABLE width=""600"">" & VbCrlf
	strBody = strBody & "<TBODY>" & VbCrlf
	strBody = strBody & "<TR>" & VbCrlf
	strBody = strBody & "<TD><TABLE height=""82"" cellSpacing=""0"" cellPadding=""0"" width=""100%"" bgColor=""#ffffff"" border=""0"">" & VbCrlf
	strBody = strBody & "<TBODY>" & VbCrlf
	strBody = strBody & "<TR>" & VbCrlf
	strBody = strBody & "<TD width=""12""><IMG height=""82"" src=""http://www.booking2hotels.com/images/h_l_001-1.gif"" width=""12"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "<TD background=""http://www.booking2hotels.com/images/bg_h-1.gif""><TABLE cellSpacing=""0"" cellPadding=""0"" width=""100%"" border=""0"">" & VbCrlf
	strBody = strBody & "<TBODY>" & VbCrlf
	strBody = strBody & "<TR>" & VbCrlf
	strBody = strBody & "<TD vAlign=""center"" height=""57""><TABLE cellSpacing=""0"" cellPadding=""0"" width=""100%"" border=""0"">" & VbCrlf
	strBody = strBody & "<TBODY>" & VbCrlf
	strBody = strBody & "<TR>" & VbCrlf
	strBody = strBody & "<TD width=""250""><A href=""http://www.hotels2thailand.com/""><IMG height=""57"" alt=""Thailand Hotels"" src=""http://www.booking2hotels.com/images/logo.gif"" width=""200"" border=""0""></A></TD>" & VbCrlf
	strBody = strBody & "<TD align=""right"">&nbsp;</TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>" & VbCrlf
	strBody = strBody & "</TABLE></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "<TR>" & VbCrlf
	strBody = strBody & "<TD vAlign=""bottom"" align=""center"" height=""25""><TABLE cellSpacing=""0"" cellPadding=""0"" border=""0"">" & VbCrlf
	strBody = strBody & "<TBODY>" & VbCrlf
	strBody = strBody & "<TR vAlign=""bottom"">" & VbCrlf
	strBody = strBody & "<TD width=""9"" height=""24""><IMG height=""24"" src=""http://www.booking2hotels.com/images/b_blue_L.gif"" width=""9"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "<TD vAlign=""center"" align=""center"" width=""60"" background=""http://www.booking2hotels.com/images/bg_b_blue.gif""><A title=""Thailand Hotels Home"" href=""http://www.hotels2thailand.com/""><strong><font color=""#346494"">Home</font></strong></A></TD>" & VbCrlf
	strBody = strBody & "<TD width=""9""><IMG height=""24"" src=""http://www.booking2hotels.com/images/spacer_b_o.gif"" width=""9"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "<TD vAlign=""center"" align=""center"" width=""70"" background=""http://www.booking2hotels.com/images/bg_b_orange.gif""><A title=""Hotels in Thailand"" href=""http://www.hotels2thailand.com/thailand-hotels.asp""><strong><font color=""#FE5400"">Hotels</font></strong></A></TD>" & VbCrlf
	strBody = strBody & "<TD width=""9""><IMG; height=""24"" src=""http://www.booking2hotels.com/images/spacer_o_o.gif"" width=""9"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "<TD vAlign=""center"" align=""center"" width=""75"" background=""http://www.booking2hotels.com/images/bg_b_orange.gif""><A title=""Thailand Travel Guide"" href=""http://www.hotels2thailand.com/thailand-hotels-travel.asp""><strong><font color=""#FE5400"">Guides</font></strong></A></TD>" & VbCrlf
	strBody = strBody & "<TD width=""9""><IMG height=""24"" src=""http://www.booking2hotels.com/images/spacer_o_o.gif"" width=""9"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "<TD vAlign=""center"" align=""center"" width=""95"" background=""http://www.booking2hotels.com/images/bg_b_orange.gif""><A title=""Hot Promotion Hotels in Thailand"" href=""http://www.hotels2thailand.com/discount_thaialand_hotels.asp""><strong><font color=""#FE5400"">Top Deals</font></strong></A> </TD>" & VbCrlf
	strBody = strBody & "<TD width=""9""><IMG height=""24"" src=""http://www.booking2hotels.com/images/b_orange_R.gif"" width=""6""></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>" & VbCrlf
	strBody = strBody & "</TABLE></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>" & VbCrlf
	strBody = strBody & "</TABLE></TD>" & VbCrlf
	strBody = strBody & "<TD width=""12""><IMG height=""82"" src=""http://www.booking2hotels.com/images/h_l_002.gif"" width=""12"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>  " & VbCrlf
	strBody = strBody & "</TABLE>" & VbCrlf
	strBody = strBody & "<TABLE width=""100%"" bgColor=""#ffffff"">" & VbCrlf
	strBody = strBody & "<TBODY>" & VbCrlf
	strBody = strBody & "<TR>" & VbCrlf
	strBody = strBody & "<TD bgColor=""#ffffff""><BR>" & VbCrlf
	strBody = strBody & "<p><STRONG>Dear "&arrOrderProduct(1,0)&"</STRONG>, </p>" & VbCrlf
	strBody = strBody & "<p>&emsp;Thank you very much for using our service, <a href=""http://www.hotels2thailand.com"" target=""_blank"">www.hotels2thailand.com</a> </p>" & VbCrlf
	strBody = strBody & "<p>&emsp;We received your enquiry for making the reservation with Hotels2thailand.com with many thanks. <strong>Your Booking ID:</strong> "&arrOrderProduct(0,0)&"</p>" & VbCrlf
	strBody = strBody & "<p>&emsp;<strong><font color=""red"">Important:</font></strong> This email DOES NOT represent as booking confirmation. Our staff will contact you to confirm the room availability again within 12 hours. </p>" & VbCrlf
	strBody = strBody & "<div align=""center"">" & VbCrlf
	
	For intCountOrderProdutSub=0 To Ubound(arrOrderProduct,2)
	
		sqlOrderItem = "SELECT ot.item_id,ot.order_product_id,ot.option_id,ot.unit,ot.price,ot.promotion_id,ot.promotion_title,ot.promotion_detail,ot.promotion_offer_person,ot.detail, po.title_en AS option_title,po.option_cat_id"
		sqlOrderItem = sqlOrderItem & " FROM tbl_order_item ot,tbl_product_option po"
		sqlOrderItem = sqlOrderItem & " WHERE po.option_id=ot.option_id AND ot.order_product_id=" & arrOrderProduct(14,intCountOrderProdutSub)
		sqlOrderItem = sqlOrderItem & " ORDER BY po.option_cat_id ASC"
		Set recOrderItem = Server.CreateObject ("ADODB.Recordset")
		recOrderItem.Open SqlOrderItem, Conn,adOpenStatic,adLockreadOnly
			arrOrderItem = recOrderItem.GetRows()
		recOrderItem.Close
		Set recOrderItem = Nothing
		
		intNight = DateDiff("d",arrOrderProduct(4,intCountOrderProdutSub),arrOrderProduct(5,intCountOrderProdutSub))
		intPriceSub = 0
		
		SELECT CASE arrOrderProduct(8,intCountOrderProdutSub)
			
			Case 29 'Hotel
			
				'### Room Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&arrOrderProduct(3,intCountOrderProdutSub)&"</font></strong> </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Check-In:</font></strong> "& function_date(arrOrderProduct(4,intCountOrderProdutSub),5) & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Adult:</font></strong> "&arrOrderProduct(6,intCountOrderProdutSub)&" <font color=""#FC8B5C""><strong>Children</strong></font>: "&arrOrderProduct(7,intCountOrderProdutSub)&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Check-Out:</font></strong> "& function_date(arrOrderProduct(5,intCountOrderProdutSub),5) & " </td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">No.</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Item Details</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Rates</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Quantity</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Night</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Sub Total</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				
				For intCountOrderItemSub=0 To Ubound(arrOrderItem,2)
				
					SELECT CASE Cstr(arrOrderItem(11,intCountOrderItemSub))
						Case "38","39","46" 'Room and Extrabed
							intRate = arrOrderItem(4,intCountOrderItemSub)/(arrOrderItem(3,intCountOrderItemSub)*intNight)
							strNight = intNight
						Case Else 'Other Product
							intRate = arrOrderItem(4,intCountOrderItemSub)/(arrOrderItem(3,intCountOrderItemSub))
							strNight = "-"
					END SELECT
					
					intPriceSub = intPriceSub + arrOrderItem(4,intCountOrderItemSub)
					intPriceTotal = intPriceTotal + arrOrderItem(4,intCountOrderItemSub)
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountOrderItemSub+1&".</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&arrOrderItem(10,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(intRate,0)&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&arrOrderItem(3,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&strNight&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(arrOrderItem(4,intCountOrderItemSub),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
				
				Next
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""5"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>	" & VbCrlf
				'### Room Detail ###
			
			Case 31 'Airport Transfer
			
				'### Transfer Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td rowspan=""2"" valign=""middle"" bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&arrOrderProduct(3,intCountOrderProdutSub)&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Arrival Flight:</font></strong> "&strFlightA&" </td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Departure Flight:</font></strong> "&strFlightD&" </td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">No.</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Item</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Rate</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Quantity</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Sub Total </font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				For intCountOrderItemSub=0 To Ubound(arrOrderItem,2)
					
					intPriceSub = intPriceSub + arrOrderItem(4,intCountOrderItemSub)
					intPriceTotal = intPriceTotal + arrOrderItem(4,intCountOrderItemSub)
					intRate = arrOrderItem(4,intCountOrderItemSub)/(arrOrderItem(3,intCountOrderItemSub))
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&intCountOrderItemSub+1&".</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&arrOrderItem(10,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(intRate,0)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&arrOrderItem(3,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(arrOrderItem(4,intCountOrderItemSub),0)&" Baht </td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
				Next
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""4"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Transfer Detail ###
				
			Case 32 'Golf Courses
				'### GolfCourses Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&arrOrderProduct(3,intCountOrderProdutSub)&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Requested Tee-Off:</font></strong> "& function_date(arrOrderProduct(4,intCountOrderProdutSub),4) & " </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Golfer:</font></strong> "&arrOrderProduct(9,intCountOrderProdutSub)&" </td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">No.</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Course</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Rate</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Quantity</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Sub Total </font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf

				For intCountOrderItemSub=0 To Ubound(arrOrderItem,2)
					
					intPriceSub = intPriceSub + arrOrderItem(4,intCountOrderItemSub)
					intPriceTotal = intPriceTotal + arrOrderItem(4,intCountOrderItemSub)
					intRate = arrOrderItem(4,intCountOrderItemSub)/(arrOrderItem(3,intCountOrderItemSub))
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&intCountOrderItemSub+1&".</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&arrOrderItem(10,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(intRate,0)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&arrOrderItem(3,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(arrOrderItem(4,intCountOrderItemSub),0)&" Baht </td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
				
				Next
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""4"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### GolfCourses Detail ###
			
			Case 34 'Sightseeing
				'### Sightseeingl ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&arrOrderProduct(3,intCountOrderProdutSub)&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Trip Date:</font></strong> "& function_date(arrOrderProduct(4,intCountOrderProdutSub),5) & " </td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Adult:</font></strong> "&arrOrderProduct(6,intCountOrderProdutSub)&" <font color=""#FC8B5C""><strong>Children</strong></font>: "&arrOrderProduct(7,intCountOrderProdutSub)&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Period:</font></strong> "&function_display_product_period(arrOrderProduct(15,intCountOrderProdutSub),arrOrderProduct(16,intCountOrderProdutSub),"",3)&"</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">No.</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Trip</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Quantity</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Sub Total </font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf

				For intCountOrderItemSub=0 To Ubound(arrOrderItem,2)
					
					intPriceSub = intPriceSub + arrOrderItem(4,intCountOrderItemSub)
					intPriceTotal = intPriceTotal + arrOrderItem(4,intCountOrderItemSub)
					intRate = arrOrderItem(4,intCountOrderItemSub)
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&intCountOrderItemSub+1&".</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&arrOrderItem(10,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">Adult: "&arrOrderProduct(6,intCountOrderProdutSub)&" Children: "&arrOrderProduct(7,intCountOrderProdutSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(arrOrderItem(4,intCountOrderItemSub),0)&" Baht </td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
				
				Next
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""3"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Sightseeing Detail ###
			
			Case 36 'Water Activity
				'### Water Activity ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&arrOrderProduct(3,intCountOrderProdutSub)&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Trip Date:</font></strong> "& function_date(arrOrderProduct(4,intCountOrderProdutSub),5) & " </td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Adult:</font></strong> "&arrOrderProduct(6,intCountOrderProdutSub)&" <font color=""#FC8B5C""><strong>Children</strong></font>: "&arrOrderProduct(7,intCountOrderProdutSub)&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Period:</font></strong> "&function_display_product_period(arrOrderProduct(15,intCountOrderProdutSub),arrOrderProduct(16,intCountOrderProdutSub),"",3)&"</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">No.</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Trip</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Quantity</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Sub Total </font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf

				For intCountOrderItemSub=0 To Ubound(arrOrderItem,2)
					
					intPriceSub = intPriceSub + arrOrderItem(4,intCountOrderItemSub)
					intPriceTotal = intPriceTotal + arrOrderItem(4,intCountOrderItemSub)
					intRate = arrOrderItem(4,intCountOrderItemSub)
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&intCountOrderItemSub+1&".</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&arrOrderItem(10,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">Adult: "&arrOrderProduct(6,intCountOrderProdutSub)&" Children: "&arrOrderProduct(7,intCountOrderProdutSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(arrOrderItem(4,intCountOrderItemSub),0)&" Baht </td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
				
				Next
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""3"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Water Activity ###
				
				
			Case 38' Shows & Events
				'### Water Activity ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&arrOrderProduct(3,intCountOrderProdutSub)&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Date:</font></strong> "& function_date(arrOrderProduct(4,intCountOrderProdutSub),5) & " </td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Adult:</font></strong> "&arrOrderProduct(6,intCountOrderProdutSub)&" <font color=""#FC8B5C""><strong>Children</strong></font>: "&arrOrderProduct(7,intCountOrderProdutSub)&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Period:</font></strong> "&function_display_product_period(arrOrderProduct(15,intCountOrderProdutSub),arrOrderProduct(16,intCountOrderProdutSub),"",3)&"</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">No.</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Show or Event</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Quantity</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Sub Total </font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf

				For intCountOrderItemSub=0 To Ubound(arrOrderItem,2)
					
					intPriceSub = intPriceSub + arrOrderItem(4,intCountOrderItemSub)
					intPriceTotal = intPriceTotal + arrOrderItem(4,intCountOrderItemSub)
					intRate = arrOrderItem(4,intCountOrderItemSub)
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&intCountOrderItemSub+1&".</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&arrOrderItem(10,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">Adult: "&arrOrderProduct(6,intCountOrderProdutSub)&" Children: "&arrOrderProduct(7,intCountOrderProdutSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(arrOrderItem(4,intCountOrderItemSub),0)&" Baht </td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
				
				Next
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""3"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Water Activity ###
			Case 39 'Health Check Up
				'### Health Check Up ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&arrOrderProduct(3,intCountOrderProdutSub)&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Requested Program:</font></strong> "& function_date(arrOrderProduct(4,intCountOrderProdutSub),4) & " </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Person:</font></strong> "&arrOrderProduct(9,intCountOrderProdutSub)&" </td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">No.</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Program</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Rate</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Quantity</font></strong></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#406EB0"">Sub Total </font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf

				For intCountOrderItemSub=0 To Ubound(arrOrderItem,2)
					
					intPriceSub = intPriceSub + arrOrderItem(4,intCountOrderItemSub)
					intPriceTotal = intPriceTotal + arrOrderItem(4,intCountOrderItemSub)
					intRate = arrOrderItem(4,intCountOrderItemSub)/(arrOrderItem(3,intCountOrderItemSub))
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&intCountOrderItemSub+1&".</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&arrOrderItem(10,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(intRate,0)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&arrOrderItem(3,intCountOrderItemSub)&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"" align=""center"">"&FormatNumber(arrOrderItem(4,intCountOrderItemSub),0)&" Baht </td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
				
				Next
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""4"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Health Check Up ###
			Case 999 'Temp
		END SELECT
	Next
	
	'### Total Price ###
	strBody = strBody & "<p>" & VbCrlf
	strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
	strBody = strBody & "<tr>" & VbCrlf
	strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"" class=""l2""><font color=""#B07E40"">Grand Total:</font> "&FormatNumber(intPriceTotal,0)&" Baht </td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "</table>" & VbCrlf
	strBody = strBody & "</p>" & VbCrlf
	'### Total Price ###
	
	strBody = strBody & "</div>" & VbCrlf
		
	
	'### Annoucemennt
	'strBody = strBody & "<p><table width=""100%""  align=""center"" border=""0"" cellspacing=""1"" cellpadding=""5"" bgcolor=""#FFBA17"">" & VbCrlf
'	strBody = strBody & "<tr>" & VbCrlf
'    strBody = strBody & "<td bgcolor=""#FFFFEA"" class=""m""  height=""30"">" & VbCrlf
'	strBody = strBody & "<font color=""#CC0000"">" & VbCrlf
'	strBody = strBody & "<img src=""http://www.booking2hotels.com/images/alert.jpg"" align=""absmiddle"">" & VbCrlf
'	strBody = strBody & "Since we will have an annual company trip between 27-29 August, 09. For any booking stay during this mentined period may not be able to be reserved. We will accept only bookings check in from 29 August, 09 afterwards. Our office will be resumed working as usual from 30 August, 2009 onwards.  Please note that all email enquiries will be replied to you immediately on August 30, 2009. " & VbCrlf
'	strBody = strBody & "<p>In case of any emergency case, you can dial to mobile phone, 66-8-91716825 (24 Hours) </p>" & VbCrlf
'	strBody = strBody & "<p>We are so sorry for any inconvenience you experienced and hope that you will come back to use our service on your next trip again.</p>" & VbCrlf
'	strBody = strBody & "</font>" & VbCrlf
'	strBody = strBody & "</td>" & VbCrlf
'	strBody = strBody & "</tr>" & VbCrlf
'	strBody = strBody & "</table>" & VbCrlf
'	strBody = strBody & "</p>" & VbCrlf
	'### Annoucemennt

	strBody = strBody & "<p>&emsp;In case you have any other enquiries, please click <A href=""http://www.hotels2thailand.com/thailand-hotels-contact.asp"">Contact Us</A> and your booking ID is necessarily to insert for reference as well. </p>" & VbCrlf
	strBody = strBody & "<p><STRONG>Best Regards,<BR>" & VbCrlf
	strBody = strBody & "<A href=""http://www.hotels2thailand.com"">www.hotels2thailand.com</A> </STRONG><STRONG></STRONG></p></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>" & VbCrlf
	strBody = strBody & "</TABLE></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>" & VbCrlf
	strBody = strBody & "</TABLE>" & VbCrlf
	strBody = strBody & "</DIV>" & VbCrlf
	strBody = strBody & "</body>" & VbCrlf
	strBody = strBody & "</html>" & VbCrlf

	inc_mail_booking_receive = strBody

END FUNCTION

%>