<%
FUNCTION function_display_rate_affiliate_res_step3()

	Dim strStep3
	Dim sqlItem
	Dim recItem
	Dim arrItem
	Dim dateCheckIn
	Dim dateCheckOut
	Dim intCountGuestAll
	Dim intCountGuestAdult
	Dim intCountGuestChildren
	Dim intOptionNumber
	Dim intCountOption
	Dim intCountItem
	
	dateCheckIn = DateSerial(Request.Form("ch_in_year"),Request.Form("ch_in_month"),Request.Form("ch_in_date"))
	dateCheckOut = DateSerial(Request.Form("ch_out_year"),Request.Form("ch_out_month"),Request.Form("ch_out_date"))
	
	'### Item ###
	sqlItem = "SELECT p.product_id,p.product_code,p.title_en,p.address_en,po.option_id,po.option_cat_id,po.title_en,po.max_adult"
	sqlItem = sqlItem & " FROM tbl_product_option po,tbl_product p"
	sqlItem = sqlItem & " WHERE p.product_id=po.product_id AND po.option_id IN ("&Request.Form("option_id")&")"
	Set recItem = Server.CreateObject ("ADODB.Recordset")
	recItem.Open sqlItem, Conn,adOpenStatic,adLockreadOnly
			arrItem = recItem.GetRows()
	recItem.Close
	Set recItem = Nothing 
	'### Item ###

	strStep3 = "<table width=""100%"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td class=""step_bg_color""><span class=""step_num"">Step 3 :</span> <span class=""step_text"">Guest Information & Preferences</span> (<em>Who is travelling?</em>) </td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td align=""center"" bgcolor=""#FFFFFF"">	 " & VbCrlf
	strStep3 = strStep3 & "<table width=""85%"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td><table width=""100%"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td width=""20%"" align=""left""><img src=""http://www.hotels2thailand.com/thailand-hotels-pic/HBK318_b_1.jpg""></td>" & VbCrlf
	strStep3 = strStep3 & "<td><table width=""100%"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td colspan=""2""><span class=""price_text"">"&arrItem(2,0)&"</span><br />" & VbCrlf
	strStep3 = strStep3 & arrItem(3,0) & VbCrlf
	strStep3 = strStep3 & "</td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td>&nbsp;</td>" & VbCrlf
	strStep3 = strStep3 & "<td>&nbsp;</td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td><span class=""room_text"">Check in:</span> <span class=""valid_text"">"&function_date(dateCheckIn,5)&"</span><br>" & VbCrlf
	strStep3 = strStep3 & "<span class=""room_text"">Check out:</span> <span class=""valid_text"">"&function_date(dateCheckOut,5)&"</span></td>" & VbCrlf
	strStep3 = strStep3 & "<td><span class=""room_text"">Adult:</span> <span class=""valid_text"">"&Request.Form("adult")&"</span><br>" & VbCrlf
	strStep3 = strStep3 & "<span class=""room_text"">Children:</span> <span class=""valid_text"">"&Request.Form("children")&"</span></td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf
	strStep3 = strStep3 & "</table></td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf
	strStep3 = strStep3 & "</table></td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td>" & VbCrlf
	strStep3 = strStep3 & "<table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FBFBFB"">" & VbCrlf
	strStep3 = strStep3 & "<tr>" & VbCrlf
	strStep3 = strStep3 & "<td colspan=""2""><strong>Full Guest Name List</strong></td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf

	intCountGuestAll = 1
	For intCountGuestAdult=1 To Cint(Request.Form("adult"))
		strStep3 = strStep3 & "<tr>" & VbCrlf
		strStep3 = strStep3 & "<td><span class=""room_text"">Guest "& intCountGuestAll & " (Adult) </span></td>" & VbCrlf
		strStep3 = strStep3 & "<td><input name=""adult_"&arrItem(0,0)&"_"& intCountGuestAdult & """ type=""text"" size=""50""></td>" & VbCrlf
		strStep3 = strStep3 & "</tr>" & VbCrlf
		intCountGuestAll = intCountGuestAll + 1
	Next

	For intCountGuestChildren=1 To Cint(Request.Form("children"))
		strStep3 = strStep3 & "<tr>" & VbCrlf
		strStep3 = strStep3 & "<td><span class=""room_text"">Guest "& intCountGuestAll & " (Children) </span></td>" & VbCrlf
		strStep3 = strStep3 & "<td><input name=""children_"&arrItem(0,0)&"_"& intCountGuestChildren & """ type=""text"" size=""50""></td>" & VbCrlf
		strStep3 = strStep3 & "</tr>" & VbCrlf
		intCountGuestAll = intCountGuestAll + 1
	Next

	strStep3 = strStep3 & "</table>" & VbCrlf
	strStep3 = strStep3 & "<br /><br />" & VbCrlf
	

	For intCountItem=0 To Ubound(arrItem,2)
		intOptionNumber  = 0
		IF Cstr(arrItem(5,intCountItem))= "38" Then 'Room
			For intCountOption=1 To Cint(Request.Form("qty" & arrItem(4,intCountItem)))
				intOptionNumber = intOptionNumber+1

				strStep3 = strStep3 & "<table width=""100%"" cellpadding=""2""  cellspacing=""0"" bgcolor=""#FBFBFB"">" & VbCrlf
				strStep3 = strStep3 & "<tr align=""left"">" & VbCrlf
				strStep3 = strStep3 & "<td colspan=""2""><strong>Room "& intOptionNumber &" # <span class=""price_text"">"& arrItem(6,intCountItem) &"</font></span></td>" & VbCrlf
				strStep3 = strStep3 & "</tr>" & VbCrlf
				strStep3 = strStep3 & "<tr align=""left"">" & VbCrlf
				strStep3 = strStep3 & "<td><span class=""room_text"">Room Requirement:</span><br>" & VbCrlf
				strStep3 = strStep3 & "(<font color=""red"">subject to availability, can not guarantee</font>) </td>" & VbCrlf
				strStep3 = strStep3 & "<td>" & function_gen_room_require(arrItem(4,intCountItem),intOptionNumber,0,0,0,3) & "</td>" & VbCrlf
				strStep3 = strStep3 & "</tr>" & VbCrlf
				strStep3 = strStep3 & "<tr align=""left"">" & VbCrlf
				strStep3 = strStep3 & "<td><font color=""346494"">Special Request: </font></td>" & VbCrlf
				strStep3 = strStep3 & "<td>" & VbCrlf
				strStep3 = strStep3 & "<textarea name=""comment_"& arrItem(4,intCountItem) & "_" & intOptionNumber & """ cols=""50"" rows=""5""></textarea><br />" & VbCrlf
				strStep3 = strStep3 & "<em>Hotels2Thailand.com will forward your requests to the property. These requests are not guaranteed.</em></td>" & VbCrlf
				strStep3 = strStep3 & "</tr>" & VbCrlf
				strStep3 = strStep3 & "</table>" & VbCrlf
				strStep3 = strStep3 & "<br /><br />" & VbCrlf
			Next
		End IF
	Next

	
	

	strStep3 = strStep3 & "</td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf
	strStep3 = strStep3 & "</table>" & VbCrlf
	strStep3 = strStep3 & "<hr>" & VbCrlf
	strStep3 = strStep3 & "<br />" & VbCrlf
	strStep3 = strStep3 & "</td>" & VbCrlf
	strStep3 = strStep3 & "</tr>" & VbCrlf
	strStep3 = strStep3 & "</table>" & VbCrlf
	
	function_display_rate_affiliate_res_step3 = strStep3
	
END FUNCTION
%>




		                            


