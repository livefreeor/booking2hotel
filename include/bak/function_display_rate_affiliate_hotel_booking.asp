<%
FUNCTION function_display_rate_affiliate_hotel_booking(intProductID,intPsID,intType)

	Dim sqlRate
	Dim recRate
	Dim bolRate
	Dim arrRate
	Dim intRate
	Dim strRate
	Dim strPriceTmp
	Dim intCountRate
	Dim intCountRow
	Dim intCountRoom
	Dim intOptionIDTmp
	Dim intRowSpan
	Dim strRowColor
	Dim strOptionID
	Dim strAdultMax
	Dim strExtraBedQty
	Dim strPromotion
	
	sqlRate = "st_hotel_rate_booking_rate_3 "&intProductID & "," & function_date_sql(Day(Date),Month(Date),Year(Date),1)
	Set recRate = Server.CreateObject ("ADODB.Recordset")
	recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRate.EOF Then
			arrRate = recRate.GetRows()
			intRate = Ubound(arrRate,2)
			bolRate = True
		Else
			bolRate = False
		End IF
	recRate.Close
	Set recRate = Nothing 

	IF bolRate Then '### IF1 ### Have Rate
	
		'### Promotion ###
		strPromotion = function_display_rate_affiliate_promotion_booking(intProductID,intPsID,"","",1)
		'### Promotion ###
	
		'### Table Head ###
		strRate = "<table width=""100%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#e4e4e4"">" & VbCrlf
		strRate = strRate & "<tr>" & VbCrlf
		strRate = strRate & "<td bgcolor=""#FFFFFF"">" & VbCrlf
		strRate = strRate & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" class=""f11"">" & VbCrlf
		strRate = strRate & "<tr> " & VbCrlf
		strRate = strRate & "<td height=""25"" class=""step_bg_color"">" & VbCrlf
		strRate = strRate & "<span class=""step_num"">1.)</span>&nbsp;<span class=""step_text"">Select Room Type And Quantity</span></td>" & VbCrlf
		strRate = strRate & "</tr>" & VbCrlf
		strRate = strRate & "</table>" & VbCrlf
		'### Table Head ###
		
		'### Promotion ###
		strRate = strRate & strPromotion
		'### Promotion ###
		
		'### Inclusive Text ###
		strRate = strRate & "<div align=""left"" class=""incusive_text"">" & VbCrlf
		'strRate = strRate & "<font color=""red"">*</font> Hotel rates per room per night are inclusive of 10% service charge and 7% of government tax. <br />" & VbCrlf
		strRate = strRate & "<font color=""red"">*</font> Hotel rates per room per night are EXCLUDED of Tax and Service Charge. <br />" & VbCrlf
		
		strRate = strRate & "<font color=""red"">*</font> Single room=One person, Twin/Double room=Two persons, Triple room=Three persons, including extra bed. <br />" & VbCrlf
		strRate = strRate & "</div>" & VbCrlf
		'### Inclusive Text ###
		
		'### Price Table ###
		'### Head ###
		strRate = strRate & "<table width=""98%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
		strRate = strRate & "<tr align=""center"">" & VbCrlf
		strRate = strRate & "<th width=""5%"" rowspan=""2"">&nbsp;</th>" & VbCrlf
		strRate = strRate & "<th width=""10%"" rowspan=""2"">Rooms</th>" & VbCrlf
		strRate = strRate & "<th width=""31%"" rowspan=""2"">Room Type</th>" & VbCrlf
		strRate = strRate & "<th width=""20%"" rowspan=""2"">Period</th>" & VbCrlf
		strRate = strRate & "<th width=""11%"">Our Rate </th>" & VbCrlf
		strRate = strRate & "<th width=""12%"">Public Rate</th>" & VbCrlf
		strRate = strRate & "<th width=""4%"" rowspan=""2"">Breakfast</th>" & VbCrlf
		strRate = strRate & "</tr>" & VbCrlf
		strRate = strRate & "<tr>" & VbCrlf
		strRate = strRate & "<th colspan=""2"" align=""center"" class=""price_text"">(Thai Baht <img src=""http://www.booking2hotels.com/images/flag_THB.gif"">)</th>" & VbCrlf
		strRate = strRate & "</tr>" & VbCrlf
		'### Head ###
		
		'### Price Body ###
		For intCountRate=0 To Ubound(arrRate,2)
		
			IF intOptionIDTmp<>arrRate(1,intCountRate) Then
				'### First Row Of Room Type ###
				strPriceTmp = Replace(strPriceTmp,"intCountRoom",intCountRoom)
				strRate = strRate & strPriceTmp

				IF strRowColor = "price_bg_color" Then
					strRowColor = "price_bg_color_non"
				Else
					strRowColor = "price_bg_color"
				End IF
				strOptionID = strOptionID & arrRate(1,intCountRate) & ","
				strAdultMax = strAdultMax & arrRate(13,intCountRate) & ","
				strExtraBedQty = strExtraBedQty & arrRate(14,intCountRate) & ","
				intCountRoom = 1
				strPriceTmp = ""
				strPriceTmp = strPriceTmp & "<tr class="""&strRowColor&""">" & VbCrlf
				strPriceTmp = strPriceTmp & "<td rowspan=""intCountRoom"" align=""center""><input type=""checkbox"" name=""option_id"" id=""option_id_"&arrRate(1,intCountRate)&""" value="""&arrRate(1,intCountRate)&"""></td>" & VbCrlf
				strPriceTmp = strPriceTmp & "<td rowspan=""intCountRoom"" align=""center"">" & VbCrlf
				strPriceTmp = strPriceTmp & function_gen_dropdown_number(1,10,1,"qty"&arrRate(1,intCountRate),4) & VbCrlf
				strPriceTmp = strPriceTmp & "</td>" & VbCrlf
				strPriceTmp = strPriceTmp & "<td rowspan=""intCountRoom"" class=""room_text"">"&arrRate(6,intCountRate)&"</td>" & VbCrlf
				strPriceTmp = strPriceTmp & "<td align=""center"" class=""period"">"&function_date(arrRate(2,intCountRate),3)&" - "&function_date(arrRate(3,intCountRate),3)&"</td>" & VbCrlf
				strPriceTmp = strPriceTmp & "<td align=""center""><span class=""price_text"">"&function_display_price(arrRate(4,intCountRate),1)&"</span></td>" & VbCrlf
				strPriceTmp = strPriceTmp & "<td align=""center"">"&function_display_price(arrRate(5,intCountRate),2)&"</td>" & VbCrlf
				strPriceTmp = strPriceTmp & "<td rowspan=""intCountRoom"" align=""center"">"&function_display_bol("<img src=""http://www.booking2hotels.com/images/ok.gif"">","-",arrRate(7,intCountRate),"",2)&"</td>" & VbCrlf
				strPriceTmp = strPriceTmp & "</tr>" & VbCrlf
				intOptionIDTmp=arrRate(1,intCountRate)
				'### First Row Of Room Type ###
			Else
				intCountRoom = intCountRoom+1
				strPriceTmp = strPriceTmp & "<tr class="""&strRowColor&""">" & VbCrlf
				strPriceTmp = strPriceTmp & "<td align=""center"" class=""period"">"&function_date(arrRate(2,intCountRate),3)&" - "&function_date(arrRate(3,intCountRate),3)&"</td>" & VbCrlf
				strPriceTmp = strPriceTmp & "<td align=""center""><span class=""price_text"">"&function_display_price(arrRate(4,intCountRate),1)&"</span></td>" & VbCrlf
				strPriceTmp = strPriceTmp & "<td align=""center"">"&function_display_price(arrRate(5,intCountRate),2)&"</td>" & VbCrlf
				strPriceTmp = strPriceTmp & "</tr>" & VbCrlf
				intOptionIDTmp=arrRate(1,intCountRate)
			End IF
		Next
		
		'### For Last Row ###
		strPriceTmp = Replace(strPriceTmp,"intCountRoom",intCountRoom)
		strRate = strRate & strPriceTmp
		'### For Last Row ###
		'### Price Body ###
		
		'### Bottom ###
		strOptionID = Left(strOptionID,Len(strOptionID)-1)
		strAdultMax = Left(strAdultMax ,Len(strAdultMax )-1)
		strExtraBedQty = Left(strExtraBedQty ,Len(strExtraBedQty)-1)
		strRate = strRate & "<input type=""hidden"" name=""room_id"" id=""room_id"" value="""&strOptionID&""">" & VbCrlf
		strRate = strRate & "<input type=""hidden"" name=""room_adult_max"" id=""room_adult_max"" value="""&strAdultMax&""">" & VbCrlf
		strRate = strRate & "<input type=""hidden"" name=""extrabed"" id=""extrabed"" value="""&strExtraBedQty &""">" & VbCrlf
		strRate = strRate & "</table>" & VbCrlf
		'### Bottom ###
    	'### Price Table ###

  
		'### Table Bottom ###
		strRate = strRate & "</td>" & VbCrlf
		strRate = strRate & "</tr>" & VbCrlf
		strRate = strRate & "</table>" & VbCrlf
		'### Table Bottom ###
                        

	
	Else '### IF1 ### NOT HAVE RATE
	
		strRate = ""
		
	End IF '### IF1 ###

	function_display_rate_affiliate_hotel_booking = strRate

END FUNCTION
%>