<%
FUNCTION function_display_rate_affiliate_option_booking(intDestinationID,intLocationID,intProductID,intPsID,intType)
	
	Dim bolTransfer
	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim intCountRate
	Dim strTransfer
	Dim strRate
	Dim bolRate
	Dim strExtrabedID
	
	bolTransfer = function_check_airport(intProductID,"",Day(Date),Month(Date),Year(Date),Day(Date),Month(Date),Year(Date),1)
	
	sqlRate = "SELECT op.price_id,op.option_id,op.date_start,op.date_end,op.price,op.price_rack,po.title_en AS option_title,po.option_cat_id"
	sqlRate = sqlRate & " FROM tbl_product p,tbl_product_option po, tbl_option_price op"
	sqlRate = sqlRate & " WHERE p.product_id=po.product_id AND po.option_id=op.option_id AND po.status=1 AND po.option_cat_id IN (39,47) AND op.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND p.product_id=" & intProductID
	sqlRate = sqlRate & " ORDER BY op.date_end ASC ,op.price ASC"
	Set recRate = Server.CreateObject ("ADODB.Recordset")
	recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRate.EOF Then
			bolRate = True
			arrRate = recRate.GetRows()
		Else
			bolRate = False
		End IF
	recRate.Close
	Set recRate = Nothing 
	
	SELECT CASE intDestinationID
		Case 30 '###Bangkok###
			strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport"" id=""tran_airport"" value=""4113,4114,4119,4120"" />" & VbCrlf
			strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport_qty"" id=""tran_airport_qty"" value=""3,3,11,11"" />" & VbCrlf
			strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
			strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_4113"" value=""4113""></td>" & VbCrlf
			strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4113"),"airport4113",4)&"</td>" & VbCrlf
			strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
			strTransfer = strTransfer & "<td>" & FormatNumber(1400,0) & "</td>" & VbCrlf
			strTransfer = strTransfer & "</tr>" & VbCrlf
					
			strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
			strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_4114"" value=""4114""></td>" & VbCrlf
			strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4114"),"airport4114",4)&"</td>" & VbCrlf
			strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
			strTransfer = strTransfer & "<td>" & FormatNumber(2800,0) & "</td>" & VbCrlf
			strTransfer = strTransfer & "</tr>" & VbCrlf
					
					
			strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
			strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_4119"" value=""4119""></td>" & VbCrlf
			strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4119"),"airport4119",4)&"</td>" & VbCrlf
			strTransfer = strTransfer & "<td>One way airport transfer Per Van <br><font color=""green"">(Contain 10-11 People)</font></td>" & VbCrlf
			strTransfer = strTransfer & "<td>" & FormatNumber(1300,0) & "</td>" & VbCrlf
			strTransfer = strTransfer & "</tr>" & VbCrlf
					
			strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
			strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_4120"" value=""4120""></td>" & VbCrlf
			strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4120"),"airport4120",4)&"</td>" & VbCrlf
			strTransfer = strTransfer & "<td>Round trip airport transfer Per Van <br><font color=""green"">(Contain 10-11 People)</font></td>" & VbCrlf
			strTransfer = strTransfer & "<td>" & FormatNumber(2600,0) & "</td>" & VbCrlf
			strTransfer = strTransfer & "</tr>" & VbCrlf
			
		Case 31 '###Phuket###
			IF Cstr(intLocationID) <> "173" AND Cstr(intLocationID) <> "188" AND Cstr(intLocationID) <> "267" Then 'Exclude Koh Yao and Koh Lon and Coral Island
				strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport"" id=""tran_airport"" value=""5427,5428,5429,5430"" />" & VbCrlf
				strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport_qty"" id=""tran_airport_qty"" value=""3,3,5,5"" />" & VbCrlf
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_5427"" value=""5427""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport5427"),"airport5427",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(1060,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
						
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_5428"" value=""5428""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport5428"),"airport5428",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(2100,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
						
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_5429"" value=""5429""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport5429"),"airport5429",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 4-5 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(1760,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
						
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_5430"" value=""5430""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport5430"),"airport5430",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 4-5 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(3500,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
			End IF
					
			Case 32 '###Chiang Mai###
			IF Cstr(intLocationID) <> "226" AND Cstr(intLocationID) <> "233" AND Cstr(intLocationID) <> "88" AND Cstr(intLocationID) <> "77" AND Cstr(intLocationID) <> "81" AND Cstr(intLocationID) <> "83" AND Cstr(intLocationID) <> "231" AND Cstr(intLocationID) <> "263" AND Cstr(intLocationID) <> "85" AND Cstr(intLocationID) <> "175" Then
				strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport"" id=""tran_airport"" value=""3745,3746"" />" & VbCrlf
				strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport_qty"" id=""tran_airport_qty"" value=""3,3"" />" & VbCrlf
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_3745"" value=""3745""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport3745"),"airport3745",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(300,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
						
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_3746"" value=""3746""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport3746"),"airport3746",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(580,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
			End IF
					
			Case 33 '###Pattaya###
				strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport"" id=""tran_airport"" value=""4121,4122,4123,4124"" />" & VbCrlf
				strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport_qty"" id=""tran_airport_qty"" value=""3,3,11,11"" />" & VbCrlf
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_4121"" value=""4121""</td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4121"),"airport4121",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(4280,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
					
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_4122"" value=""4122""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4122"),"airport4122",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(8550,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
					
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_4123"" value=""4123""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4123"),"airport4123",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 10-11 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(3700,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
					
				strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
				strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_4124"" value=""4124""></td>" & VbCrlf
				strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4124"),"airport4124",4)&"</td>" & VbCrlf
				strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 10-11 People)</font></td>" & VbCrlf
				strTransfer = strTransfer & "<td>" & FormatNumber(7400,0) & "</td>" & VbCrlf
				strTransfer = strTransfer & "</tr>" & VbCrlf
					
			Case 34
			Case 35 '###Krabi###
				IF Cstr(intLocationID) <> "117" AND Cstr(intLocationID) <> "118" AND Cstr(intLocationID) <> "242" AND Cstr(intLocationID) <> "269" Then 'Exclude Koh Lanta and Phi Phi and Railai and Koh Ngai
					strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport"" id=""tran_airport"" value=""3752,3754"" />" & VbCrlf
					strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport_qty"" id=""tran_airport_qty"" value=""3,3"" />" & VbCrlf
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_3752"" value=""3752""></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport3752"),"airport3752",4)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(700,0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
						
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" id=""airport_id_3754"" value=""3754""></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport3754"),"airport3754",4)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(1400,0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
					End IF
					
			'Case 36
			Case Else
				strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport"" id=""tran_airport"" value=""0"" />" & VbCrlf
				strTransfer = strTransfer & "<input type=""hidden"" name=""tran_airport_qty"" id=""tran_airport_qty"" value=""0"" />" & VbCrlf
				
	END SELECT
	
	'### Table Head###
	strRate = "<table width=""100%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#e4e4e4"">" & VbCrlf
	strRate = strRate & "<tr>" & VbCrlf
	strRate = strRate & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" class=""f11"">" & VbCrlf
	strRate = strRate & "<tr> " & VbCrlf
	strRate = strRate & "<td height=""25"" class=""step_bg_color"">" & VbCrlf
	strRate = strRate & "<span class=""step_num"">2.)</span>&nbsp;<span class=""step_text"">Select Extra Option</span>" & VbCrlf
	strRate = strRate & "</td>" & VbCrlf
	strRate = strRate & "</tr>" & VbCrlf
	strRate = strRate & "</table>" & VbCrlf
	'### Table Head###
	
	'### Price ###
		'### Price Head####
	strRate = strRate & "<table width=""70%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
	strRate = strRate & "<tr>" & VbCrlf
	strRate = strRate & "<th width=""9%"">&nbsp;</th>" & VbCrlf
	strRate = strRate & "<th width=""19%"" align=""center"">Quantity</th>" & VbCrlf
	strRate = strRate & "<th width=""48%"" align=""center"">Option</th>" & VbCrlf
	strRate = strRate & "<th width=""24%"" align=""center"" class="""">Rate<br><span class=""price_text"">(Thai Baht <img src=""http://www.booking2hotels.com/images/flag_THB.gif"">)</span></th>" & VbCrlf
	strRate = strRate & "</tr>" & VbCrlf
		'### Price Head####
		
		'### Price Body####
	strRate = strRate & strTransfer
	IF bolRate Then
		For intCountRate=0 To Ubound(arrRate,2)
			IF arrRate(7,intCountRate)=39 Then '### Extra Bed ###
				strExtrabedID = strExtrabedID & arrRate(1,intCountRate) & ","
				strRate = strRate & "<tr align=""center"" class=""price_bg_color_non"">" & VbCrlf
				strRate = strRate & "<td><input type=""checkbox"" name=""bed_id"" id=""bed_id_"&arrRate(1,intCountRate)&""" value="""&arrRate(1,intCountRate)&""" ></td>" & VbCrlf
				strRate = strRate & "<td>" & VbCrlf
				strRate = strRate & function_gen_dropdown_number(1,10,1,"bed"&arrRate(1,intCountRate),4) & VbCrlf
				strRate = strRate & "</td>" & VbCrlf
				strRate = strRate & "<td class=""room_text"">"&arrRate(6,intCountRate)&"</td>" & VbCrlf
				strRate = strRate & "<td>"&FormatNumber(arrRate(4,intCountRate),0)&"</td>" & VbCrlf
				strRate = strRate & "</tr>" & VbCrlf
			ElseIF arrRate(7,intCountRate)=47 Then '### Gala Dinner ###
				strRate = strRate & "<tr align=""center"" class=""price_bg_color_non"">" & VbCrlf
				strRate = strRate & "<td><input type=""checkbox"" name=""gala_id"" value="""&arrRate(1,intCountRate)&""" ></td>" & VbCrlf
				strRate = strRate & "<td>" & VbCrlf
				strRate = strRate & function_gen_dropdown_number(1,10,1,"qty"&arrRate(1,intCountRate),1) & VbCrlf
				strRate = strRate & "</td>" & VbCrlf
				strRate = strRate & "<td class=""room_text"">"&arrRate(6,intCountRate)&"</td>" & VbCrlf
				strRate = strRate & "<td>"&FormatNumber(arrRate(4,intCountRate),0)&"</td>" & VbCrlf
				strRate = strRate & "</tr>" & VbCrlf
			Else
				strRate = strRate & "<tr align=""center"" class=""price_bg_color_non"">" & VbCrlf
				strRate = strRate & "<td><input type=""checkbox"" name=""extra_id"" value="""&arrRate(1,intCountRate)&""" ></td>" & VbCrlf
				strRate = strRate & "<td>" & VbCrlf
				strRate = strRate & function_gen_dropdown_number(1,10,1,"qty"&arrRate(1,intCountRate),1) & VbCrlf
				strRate = strRate & "</td>" & VbCrlf
				strRate = strRate & "<td class=""room_text"">"&arrRate(6,intCountRate)&"</td>" & VbCrlf
				strRate = strRate & "<td>"&FormatNumber(arrRate(4,intCountRate),0)&"</td>" & VbCrlf
				strRate = strRate & "</tr>" & VbCrlf
			End IF
		Next
	End IF
		'### Price Body####

	'###Table Bottom###
	IF Len(strExtrabedID)>0 Then
		strExtrabedID = Left(strExtrabedID,Len(strExtrabedID)-1)
		strRate = strRate & "<input type=""hidden"" name=""bed_qty"" id=""bed_qty"" value="""&strExtrabedID&""">" & VbCrlf
	Else
		strRate = strRate & "<input type=""hidden"" name=""bed_qty"" id=""bed_qty"" value=""0"">" & VbCrlf
	End IF
	strRate = strRate & "</table>" & VbCrlf
	strRate = strRate & "</td>" & VbCrlf
	strRate = strRate & "</tr>" & VbCrlf
	strRate = strRate & "</table>" & VbCrlf
	'###Table Bottom###

	function_display_rate_affiliate_option_booking = strRate 
END FUNCTION
%>

