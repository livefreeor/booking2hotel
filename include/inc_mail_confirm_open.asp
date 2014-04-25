<%
FUNCTION inc_mail_confirm_open(intOrderID)

	Dim strBody
	Dim sqlOrder
	Dim recOrder
	Dim arrOrder
	Dim sqlOrderProduct
	Dim recOrderProduct
	Dim arrOrderProduct
	Dim sqlOrderItem
	Dim recOrderItem
	Dim arrOrderItem
	Dim intCountItem
	Dim intPrice
	Dim intNight
	Dim intPriceSub
	Dim intPriceTotal
	Dim strFlightA
	Dim strFlightD
	Dim strTeeOff
	Dim strVoucherLink
	Dim strVoucherLinkAll
	Dim intRndNum1
	Dim intRndNum2
	Dim intRndNum3
	Dim intRndNum4
	Dim intRndNum5
	Dim intRndNum6
	Dim intOrderProductID
	Dim strEmail
	Dim strName
	Dim sqlHealth
	Dim rsHealth
	
	sqlOrder = "SELECT o.order_id,o.full_name,o.email,flight_arrival_number,o.flight_arrival_time,o.flight_departure_number,o.flight_departure_time FROM tbl_order o WHERE order_id=" & intOrderID
	Set recOrder = Server.CreateObject ("ADODB.Recordset")
	recOrder.Open sqlOrder, Conn,AdOpenForwardOnly,AdLockReadOnly
	
	strEmail = recOrder.Fields("email")
	strName = recOrder.Fields("full_name")
	
	IF recOrder.Fields("flight_arrival_time")<>"" AND NOT ISNULL(recOrder.Fields("flight_arrival_time")) Then
		strFlightA = recOrder.Fields("flight_arrival_number") & " " & function_date(recOrder.Fields("flight_arrival_time"),4)
	Else
		strFlightA = "Not Confirm"
	End IF
	
	IF recOrder.Fields("flight_departure_time")<>"" AND NOT ISNULL(recOrder.Fields("flight_departure_time")) Then
		strFlightD = recOrder.Fields("flight_departure_number") & " " & function_date(recOrder.Fields("flight_departure_time"),4)
	Else
		strFlightD = "Not Confirm"
	End IF
	
	sqlOrderproduct = "SELECT op.order_product_id,op.num_adult,op.num_child,op.num_golfer,op.date_time_check_in,op.date_time_check_out,op.date_time_check_in_confirm,op.detail,p.title_real as title_en,p.product_cat_id,p.product_code,p.product_id,op.period_id"
	sqlOrderproduct = sqlOrderproduct & " FROM tbl_order_product op, tbl_product p"
	sqlOrderproduct = sqlOrderproduct & " WHERE p.product_id=op.product_id AND op.order_id=" & intOrderID
	sqlOrderproduct = sqlOrderproduct & " ORDER BY op.date_time_check_in ASC,p.product_cat_id ASC"
	Set recOrderProduct = Server.CreateObject ("ADODB.Recordset")
	recOrderProduct.Open sqlOrderProduct, Conn,AdOpenForwardOnly,AdLockReadOnly
	
	strBody = "<html>" & VbCrlf
	strBody = strBody & "<head>" & VbCrlf
	strBody = strBody & "<title>Reservation Confirm</title>" & VbCrlf
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
	strBody = strBody & "<TD width=""9""><IMG height=""24"" src=""http://www.booking2hotels.com/images/spacer_o_o.gif"" width=""9"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "<TD vAlign=""center"" align=""center"" width=""75"" background=""http://www.booking2hotels.com/images/bg_b_orange.gif""><A title=""Thailand Travel Guide"" href=""http://www.hotels2thailand.com/thailand-hotels-travel.asp""><strong><font color=""#FE5400"">Guides</font></strong></A></TD>" & VbCrlf
	strBody = strBody & "<TD width=""9""><IMG height=""24"" src=""http://www.booking2hotels.com/images/spacer_o_o.gif"" width=""9"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "<TD vAlign=""center"" align=""center"" width=""95"" background=""http://www.booking2hotels.com/images/bg_b_orange.gif""><A title=""Hot Promotion Hotels in Thailand"" href=""http://www.hotels2thailand.com/discount_thaialand_hotels.asp""><strong><font color=""#FE5400"">Promotoins</font></strong></A> </TD>" & VbCrlf
	strBody = strBody & "<TD width=""9""><IMG height=""24"" src=""http://www.booking2hotels.com/images/b_orange_R.gif"" width=""6""></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>" & VbCrlf
	strBody = strBody & "</TABLE></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>" & VbCrlf
	strBody = strBody & "</TABLE></TD>" & VbCrlf
	strBody = strBody & "<TD width=""12""><IMG height=""82"" src=""http://www.booking2hotels.com/images/h_l_002.gif"" width=""12"" align=""absMiddle""></TD>" & VbCrlf
	strBody = strBody & "</TR>" & VbCrlf
	strBody = strBody & "</TBODY>" & VbCrlf
	strBody = strBody & "</TABLE>" & VbCrlf
	strBody = strBody & "<TABLE width=""100%"" bgColor=""#ffffff"">" & VbCrlf
	strBody = strBody & "<TBODY>" & VbCrlf
	strBody = strBody & "<TR>" & VbCrlf
	strBody = strBody & "<TD bgColor=""#ffffff""><BR>" & VbCrlf
	strBody = strBody & "<p><STRONG>Dear "&recOrder.Fields("full_name")&"</STRONG>, </p>" & VbCrlf
	strBody = strBody & "<p>&emsp;We would like to thank you for taking a service on the www.hotels2thailand.com. We are pleased to inform you that your booking is available according to your requirement (ORDER ID: "&recOrder.Fields("order_id")&"). </p>" & VbCrlf
	strBody = strBody & "<p>&emsp;Thank you for your payment to http://www.hotels2thailand.com. Your payment for ORDER ID: "&recOrder.Fields("order_id")&" detail see below</p>" & VbCrlf
	strBody = strBody & "<div align=""center"">" & VbCrlf
	
	'### Item Detail ###
	
	While NOT recOrderProduct.EOF
	
		intCountItem = 0
		intPriceSub = 0
		
		IF recOrderProduct.Fields("date_time_check_in_confirm")<>"" AND NOT ISNULL(recOrderProduct.Fields("date_time_check_in_confirm")) Then
			strTeeOff = function_date(recOrderProduct.Fields("date_time_check_in_confirm"),4)
		Else
			strTeeOff = function_date(recOrderProduct.Fields("date_time_check_in"),4) & " <strong><font color=""red"">(Not Confirm)</font></strong>"
		End IF
	
		sqlOrderItem = "SELECT ot.item_id,ot.order_product_id,ot.option_id,ot.unit,ot.price,ot.price_display,ot.price_display_currency,ot.promotion_id,ot.promotion_title,ot.detail,po.title_en,po.option_cat_id"
		sqlOrderItem = sqlOrderItem & " FROM tbl_order_item ot, tbl_product_option po"
		sqlOrderItem = sqlOrderItem & " WHERE po.option_id=ot.option_id AND ot.order_product_id=" & recOrderProduct.Fields("order_product_id")
		sqlOrderItem = sqlOrderItem & " ORDER BY po.option_cat_id ASC"
	
		Set recOrderItem = Server.CreateObject ("ADODB.Recordset")
		recOrderItem.Open sqlOrderItem, Conn,AdOpenForwardOnly,AdLockReadOnly
	
		SELECT CASE recOrderProduct.Fields("product_cat_id")
			Case 29 'Hotel
				'### Hotel Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&recOrderProduct.Fields("title_en")&"</font></strong> </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Check-In:</font></strong> "&function_date(recOrderProduct.Fields("date_time_check_in"),5)&"</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Adult:</font></strong> "&recOrderProduct.Fields("num_adult")&" <font color=""#FC8B5C""><strong>Children:</strong></font> "&recOrderProduct.Fields("num_child")&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Check-Out:</font></strong> "&function_date(recOrderProduct.Fields("date_time_check_out"),5)&"</td>" & VbCrlf
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
				
				intPrice = 0
				intPriceSub = 0
				While NOT recOrderItem.EOF
				
					intCountItem = intCountItem + 1
					
					IF recOrderItem.Fields("option_cat_id") = 38 OR recOrderItem.Fields("option_cat_id") = 39 Then
						intNight = DateDiff("d",recOrderProduct.Fields("date_time_check_in"),recOrderProduct.Fields("date_time_check_out"))
						intPrice = recOrderItem.Fields("price")/(recOrderItem.Fields("unit")*intNight)
					Else
						intNight = "-"
						intPrice = recOrderItem.Fields("price")/(recOrderItem.Fields("unit"))
					End IF
					
					intPriceSub = intPriceSub + recOrderItem.Fields("price")
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountItem&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&recOrderItem.Fields("title_en")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(intPrice,0)&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&recOrderItem.Fields("unit")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intNight&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(recOrderItem.Fields("price"),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
					recOrderItem.MoveNext
				Wend
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""5"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Hotel Detail ###
	
			Case 31 'Transfer
				'### Transfer Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td rowspan=""2"" valign=""middle"" bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&recOrderProduct.Fields("title_en")&"</font></strong></td>" & VbCrlf
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
				
				While NOT recOrderItem.EOF
				
					intCountItem = intCountItem + 1
					intPrice = recOrderItem.Fields("price")/(recOrderItem.Fields("unit"))
					
					intPriceSub = intPriceSub + recOrderItem.Fields("price")
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountItem&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&recOrderItem.Fields("title_en")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(intPrice,0)&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&recOrderItem.Fields("unit")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(recOrderItem.Fields("price"),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
					recOrderItem.MoveNext
				Wend
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""4"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Transfer Detail ###
	
			Case 32 'Golf Course
				'### Golf Course Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C""> "&recOrderProduct.Fields("title_en")&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Tee-Off:</font></strong> "&strTeeOFf&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Golfer:</font></strong> "&recOrderProduct.Fields("num_golfer")&"</td>" & VbCrlf
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
				
				While NOT recOrderItem.EOF
				
					intCountItem = intCountItem + 1
					intPrice = recOrderItem.Fields("price")/(recOrderItem.Fields("unit"))
					
					intPriceSub = intPriceSub + recOrderItem.Fields("price")
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountItem&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&recOrderItem.Fields("title_en")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(intPrice,0)&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&recOrderItem.Fields("unit")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(recOrderItem.Fields("price"),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
					recOrderItem.MoveNext
				Wend
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""4"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Golf Course Detail ###
				
			Case 34 'Sightseeing
				'### Sightseeing Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&recOrderProduct.Fields("title_en")&"</font></strong> </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Check-In:</font></strong> "&function_date(recOrderProduct.Fields("date_time_check_in"),5)&"</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Adult:</font></strong> "&recOrderProduct.Fields("num_adult")&" <font color=""#FC8B5C""><strong>Children:</strong></font> "&recOrderProduct.Fields("num_child")&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Period:</font></strong> "&function_display_product_period(recOrderProduct.Fields("product_id"),recOrderProduct.Fields("period_id"),"",3)&"</td>" & VbCrlf
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
				
				While NOT recOrderItem.EOF
				
					intCountItem = intCountItem + 1
					intPrice = recOrderItem.Fields("price")
					
					intPriceSub = intPriceSub + recOrderItem.Fields("price")
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountItem&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&recOrderItem.Fields("title_en")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">Adult: "&recOrderProduct.Fields("num_adult")&" Chilren: "&recOrderProduct.Fields("num_child")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(recOrderItem.Fields("price"),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
					recOrderItem.MoveNext
				Wend
				
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
				'### Water Activity Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&recOrderProduct.Fields("title_en")&"</font></strong> </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Check-In:</font></strong> "&function_date(recOrderProduct.Fields("date_time_check_in"),5)&"</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Adult:</font></strong> "&recOrderProduct.Fields("num_adult")&" <font color=""#FC8B5C""><strong>Children:</strong></font> "&recOrderProduct.Fields("num_child")&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Period:</font></strong> "&function_display_product_period(recOrderProduct.Fields("product_id"),recOrderProduct.Fields("period_id"),"",3)&"</td>" & VbCrlf
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
				
				While NOT recOrderItem.EOF
				
					intCountItem = intCountItem + 1
					intPrice = recOrderItem.Fields("price")
					
					intPriceSub = intPriceSub + recOrderItem.Fields("price")
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountItem&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&recOrderItem.Fields("title_en")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">Adult: "&recOrderProduct.Fields("num_adult")&" Chilren: "&recOrderProduct.Fields("num_child")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(recOrderItem.Fields("price"),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
					recOrderItem.MoveNext
				Wend
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""3"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Water Activity Detail ###
			
			Case 38 'Shows & Events
				'### Water Activity Detail ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">"&recOrderProduct.Fields("title_en")&"</font></strong> </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Date:</font></strong> "&function_date(recOrderProduct.Fields("date_time_check_in"),5)&"</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Adult:</font></strong> "&recOrderProduct.Fields("num_adult")&" <font color=""#FC8B5C""><strong>Children:</strong></font> "&recOrderProduct.Fields("num_child")&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Period:</font></strong> "&function_display_product_period(recOrderProduct.Fields("product_id"),recOrderProduct.Fields("period_id"),"",3)&"</td>" & VbCrlf
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
				
				While NOT recOrderItem.EOF
				
					intCountItem = intCountItem + 1
					intPrice = recOrderItem.Fields("price")
					
					intPriceSub = intPriceSub + recOrderItem.Fields("price")
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountItem&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&recOrderItem.Fields("title_en")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">Adult: "&recOrderProduct.Fields("num_adult")&" Chilren: "&recOrderProduct.Fields("num_child")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(recOrderItem.Fields("price"),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
					recOrderItem.MoveNext
				Wend
				
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td colspan=""3"" align=""right"" bgcolor=""#FFFFFF"" class=""l1""><font color=""#B07E40"">Total</font></td>" & VbCrlf
				strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF""><strong><font color=""#B07E40"">"&FormatNumber(intPriceSub,0)&" Baht</font></strong></td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</td>" & VbCrlf
				strBody = strBody & "</tr>" & VbCrlf
				strBody = strBody & "</table>" & VbCrlf
				strBody = strBody & "</p>" & VbCrlf
				'### Water Activity Detail ###
				
			Case 39 'Health Check Up
				'### Health Check Up ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C""> "&recOrderProduct.Fields("title_en")&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Program:</font></strong> "&strTeeOFf&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Person:</font></strong> "&recOrderProduct.Fields("num_adult")&"</td>" & VbCrlf
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
				
				While NOT recOrderItem.EOF
				
					intCountItem = intCountItem + 1
					intPrice = recOrderItem.Fields("price")/(recOrderItem.Fields("unit"))
					
					intPriceSub = intPriceSub + recOrderItem.Fields("price")
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountItem&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&recOrderItem.Fields("title_en")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(intPrice,0)&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&recOrderItem.Fields("unit")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(recOrderItem.Fields("price"),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
					recOrderItem.MoveNext
				Wend
				
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
			Case 40 'Spa
				'### Spa ###
				strBody = strBody & "<p>" & VbCrlf
				strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strBody = strBody & "<tr>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C""> "&recOrderProduct.Fields("title_en")&"</font></strong></td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Program:</font></strong> "&strTeeOFf&" </td>" & VbCrlf
				strBody = strBody & "<td bgcolor=""#FFFFFF""><strong><font color=""#FC8B5C"">Person:</font></strong> "&recOrderProduct.Fields("num_adult")&"</td>" & VbCrlf
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
				
				While NOT recOrderItem.EOF
				
					intCountItem = intCountItem + 1
					intPrice = recOrderItem.Fields("price")/(recOrderItem.Fields("unit"))
					
					intPriceSub = intPriceSub + recOrderItem.Fields("price")
					
					strBody = strBody & "<tr>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&intCountItem&"</td>" & VbCrlf
					strBody = strBody & "<td bgcolor=""#FFFFFF"">"&recOrderItem.Fields("title_en")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(intPrice,0)&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&recOrderItem.Fields("unit")&"</td>" & VbCrlf
					strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"">"&FormatNumber(recOrderItem.Fields("price"),0)&" Baht</td>" & VbCrlf
					strBody = strBody & "</tr>" & VbCrlf
					
					recOrderItem.MoveNext
				Wend
				
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
		
		intPriceTotal = intPriceTotal + intPriceSub
		
		recOrderItem.Close
		SET recOrderItem = Nothing
		
		recOrderProduct.MoveNext
	Wend
	
	strBody = strBody & "<p>" & VbCrlf
	strBody = strBody & "<table width=""95%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FCB45C"">" & VbCrlf
	strBody = strBody & "<tr>" & VbCrlf
	strBody = strBody & "<td align=""center"" bgcolor=""#FFFFFF"" class=""l2""><font color=""#B07E40"">Grand Total:</font> "&FormatNumber(intPriceTotal,0)&" Baht </td>" & VbCrlf
	strBody = strBody & "</tr>" & VbCrlf
	strBody = strBody & "</table>" & VbCrlf
	strBody = strBody & "</p>" & VbCrlf
	'### Item Detail ###
	
	strBody = strBody & "</div>" & VbCrlf
	
	strBody = strBody & "<p>&emsp;In case you have any other enquiries, please click <A href=""http://www.hotels2thailand.com/thailand-hotels-contact.asp"">Contact Us</A> and your booking ID is necessarily to insert for reference as well. </p>" & VbCrlf
	
	'### Vouchre Click Version ###
	strBody = strBody & "<p>&emsp; <strong><font color=""green"">Voucher for your booking.</font></strong>" & VbCrlf
	
	recOrderProduct.MoveFirst
	
	While NOT recOrderProduct.EOF
		'IF recOrderProduct.Fields("product_cat_id")<>31 Then
			intRndNum1 = function_random_number(0,50000,1)
			intRndNum2 = function_random_number(0,100000,1)
			intRndNum3 = function_random_number(0,7500,1)
			intRndNum4 = function_random_number(0,900000000,1)
			intRndNum5 = function_random_number(0,2500,1)
			intRndNum6 = function_random_number(0,70000,1)
			intRndNum6 = function_add_zero(intRndNum6, 5)
			intOrderProductID = function_add_zero(recOrderProduct.Fields("order_product_id"), 6)
			'strVoucherLink = "http://www.hotels2thailand.com/"&function_gen_order_link(recOrder.Fields("order_id"),recOrderProduct.Fields("product_cat_id"),recOrderProduct.Fields("order_product_id"),NULL,3)&"?session="&intRndNum1&"&hotel="&intRndNum2&"&order="&intOrderID& intRndNum5&"&link=hotels2thailand"&intRndNum4&"&pr=" & intRndNum6 &intOrderProductID&intRndNum3&"&read=true&serial="&intRndNum3
			'strVoucherLinkAll = strVoucherLinkAll & "<font color=""#B07E40"">"&recOrderProduct.Fields("title_en")&":</font> <br>" & VbCrlf & strVoucherLink & "<br>" & VbCrlf
			IF recOrderProduct.Fields("product_cat_id")<>39 Then
				strVoucherLink = "http://www.hotels2thailand.com/"&function_gen_order_link(recOrder.Fields("order_id"),recOrderProduct.Fields("product_cat_id"),recOrderProduct.Fields("order_product_id"),NULL,3)&"?session="&intRndNum1&"&hotel="&intRndNum2&"&order="&intOrderID& intRndNum5&"&link=hotels2thailand"&intRndNum4&"&pr=" & intRndNum6 &intOrderProductID&intRndNum3&"&read=true&serial="&intRndNum3
				strVoucherLinkAll = strVoucherLinkAll & "<font color=""#B07E40"">"&recOrderProduct.Fields("title_en")&":</font> <br>" & VbCrlf & strVoucherLink & "<br>" & VbCrlf
				strBody = strBody & "<li><a href="""&strVoucherLink&""" target=""_blank"" >"&recOrderProduct.Fields("title_en")&"</a></li>" & VbCrlf
			Else
				sqlHealth="SELECT opg.guest_id,po.title_en,opg.guest_name,po.option_id,ot.date_check_in"
				sqlHealth=sqlHealth&" FROM tbl_order_item ot,tbl_product_option po,tbl_order_product_guest opg,tbl_order_item_require_health irh"
				sqlHealth=sqlHealth&" WHERE po.option_id=ot.option_id AND irh.item_id=ot.item_id AND irh.guest_id=opg.guest_id AND ot.order_product_id="&recOrderProduct("order_product_id")
				Set rsHealth=conn.execute(sqlHealth)
					While Not rsHealth.Eof
					IF recOrderProduct("product_id")<>1754 Then
					strVoucherLink = "http://www.hotels2thailand.com/"&function_gen_order_link(recOrder.Fields("order_id"),recOrderProduct.Fields("product_cat_id"),recOrderProduct.Fields("order_product_id"),NULL,3)&"?session="&intRndNum1&"&hotel="&intRndNum2&"&order="&intOrderID& intRndNum5&"&link=hotels2thailand"&intRndNum4&"&pr=" & intRndNum6 &intOrderProductID&intRndNum3&"&read=true&serial="&intRndNum3
					Else
					strVoucherLink = "http://www.hotels2thailand.com/voucher_health_check_up_bumrungrat.asp?session="&intRndNum1&"&hotel="&intRndNum2&"&order="&intOrderID& intRndNum5&"&link=hotels2thailand"&intRndNum4&"&pr=" & intRndNum6 &rsHealth("guest_id")&intRndNum3&"&read=true&serial="&intRndNum3
					End IF
					strVoucherLinkAll = strVoucherLinkAll & "<font color=""#B07E40"">"&recOrderProduct.Fields("title_en")&":</font> <br>" & VbCrlf & strVoucherLink & "<br>" & VbCrlf
					strBody = strBody & "<li><a href="""&strVoucherLink&""" target=""_blank"" >"&rsHealth.Fields("title_en")&"("&rsHealth("guest_name")&")</a></li>" & VbCrlf
					rsHealth.Movenext
					Wend
					rsHealth.close
					Set rsHealth=Nothing
			End IF
		'End IF
		recOrderProduct.MoveNext
	Wend
	
	strBody = strBody & "</p>" & VbCrlf
	'### Vouchre Click Version ###
	
	'### Map Click Version ###
	strBody = strBody & "<p>&emsp; <strong><font color=""green"">Map for your booking.</font></strong>" & VbCrlf
	recOrderProduct.MoveFirst
	
	While NOT recOrderProduct.EOF
		SELECT CASE recOrderProduct.Fields("product_cat_id")
			Case 29 'Hotel
				strBody = strBody & "<li><a href=""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&recOrderProduct.Fields("product_id")&""" target=""_blank"" >"&recOrderProduct.Fields("title_en")&"</a></li>" & VbCrlf
			Case 32 'Golf Courses
				strBody = strBody & "<li><a href=""http://www.hotels2thailand.com/thailand-golf-map.asp?id="&recOrderProduct.Fields("product_id")&""" target=""_blank"" >"&recOrderProduct.Fields("title_en")&"</a></li>" & VbCrlf
		END SELECT
	
		recOrderProduct.MoveNext
	Wend
	strBody = strBody & "</p>" & VbCrlf
	'### Map Click Version ###
	
	strBody = strBody & "<p>&emsp;<strong><font color=""#FC8B5C"">Note:</font></strong> Print discount coupon to earn benefits at our corporated stores <a href=""http://www.hotels2thailand.com/coupon/coupon_member.aspx?m_id=5421587&coupon_id=54875154&o=14"&intOrderID&"65214&ref=pool7785&beta=2"" target=""_blank"">click here</a>.</p>" & VbCrlf
	
	strBody = strBody & "<p>" & VbCrlf
	strBody = strBody & "<font color=""#FF0000""><strong>* </strong></font>If you can not click the links, please copy this URL and paste on to your browser <br>" & VbCrlf
	strBody = strBody & "<strong><font color=""green"">Voucher </font></strong><br>" & VbCrlf
	strBody = strBody & strVoucherLinkAll
	
	strBody = strBody & "<br><strong><font color=""green"">Map </font></strong><br>" & VbCrlf
	
	recOrderProduct.MoveFirst
	While NOT recOrderProduct.EOF
	
		SELECT CASE recOrderProduct.Fields("product_cat_id")
			Case 29 'Hotel
				strBody = strBody & "<font color=""#B07E40"">" & recOrderProduct.Fields("title_en") & "</font><br>" & VbCrlf & "http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&recOrderProduct.Fields("product_id") & "<br>" & VbCrlf
			Case 32 'Golf Courses
				strBody = strBody & "<font color=""#B07E40"">" & recOrderProduct.Fields("title_en") & "</font><br>" & VbCrlf & "http://www.hotels2thailand.com/thailand-golf-map.asp?id="&recOrderProduct.Fields("product_id") & "<br>" & VbCrlf
		END SELECT
	
		recOrderProduct.MoveNext
	Wend
	strBody = strBody & "</p>" & VbCrlf
	
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
	strBody = strBody & "</html>	" & VbCrlf
	
	
	recOrder.Close
	Set recOrder = Nothing
	
	recOrderProduct.Close
	Set recOrderProduct = Nothing

	inc_mail_confirm_open = strBody

END FUNCTION
%>