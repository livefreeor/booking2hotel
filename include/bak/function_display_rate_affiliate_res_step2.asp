<%
FUNCTION function_display_rate_affiliate_res_step2()

	Dim strStep2
	
	strStep2 = "<table width=""100%"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
	strStep2 = strStep2 & "<tr>" & VbCrlf
	strStep2 = strStep2 & "<td class=""step_bg_color""><span class=""step_num"">Step 2 :</span> <span class=""step_text"">Your Information</span> (<em>Who is making this reservation?</em>) <font color=""red"">*Mandatory fields</font></td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	strStep2 = strStep2 & "<tr>" & VbCrlf
	strStep2 = strStep2 & "<td align=""center"" bgcolor=""#FFFFFF""><table width=""85%"" cellpadding=""2""  cellspacing=""0"" bgcolor=""#FBFBFB"">" & VbCrlf
	strStep2 = strStep2 & "<tr align=""left"">" & VbCrlf
	strStep2 = strStep2 & "<td><span class=""room_text"">Guest Name</span> <font color=""red"">*</font> </td>" & VbCrlf
	strStep2 = strStep2 & "<td><input name=""full_name"" type=""text"" size=""30"" value=""""></td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	strStep2 = strStep2 & "<tr align=""left"">" & VbCrlf
	strStep2 = strStep2 & "<td><span class=""room_text"">Email</span> <font color=""red"">*</font></td>" & VbCrlf
	strStep2 = strStep2 & "<td><input name=""email"" type=""text"" id=""email1"" value="""" size=""30"" maxlength=""100""></td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	strStep2 = strStep2 & "<tr align=""left"">" & VbCrlf
	strStep2 = strStep2 & "<td><span class=""room_text"">Repeat Email</span> <font color=""red"">*</font></td>" & VbCrlf
	strStep2 = strStep2 & "<td><input name=""email2"" type=""text"" id=""email2"" value="""" size=""30"" maxlength=""100""></td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	strStep2 = strStep2 & "<tr align=""left"">" & VbCrlf
	strStep2 = strStep2 & "<td><span class=""room_text"">Address</span></td>" & VbCrlf
	strStep2 = strStep2 & "<td><textarea name=""address"" cols=""40"" rows=""5"" id=""textarea""></textarea></td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	strStep2 = strStep2 & "<tr align=""left"">" & VbCrlf
	strStep2 = strStep2 & "<td><span class=""room_text"">Contact no. </span> <font color=""red"">*</font></td>" & VbCrlf
	strStep2 = strStep2 & "<td><input type=""radio"" name=""phone_type"" value=""1"" checked>Mobile" & VbCrlf
	strStep2 = strStep2 & "<input type=""radio"" name=""phone_type"" value=""0"" >Phone" & VbCrlf
	strStep2 = strStep2 & "<input name=""phone"" type=""text"" id=""phone2"" value="""" size=""20""><br><font color=""#FF0000"">( We will use to contact in case of  emergency occurred.)</font></td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	strStep2 = strStep2 & "<tr align=""left"">" & VbCrlf
	strStep2 = strStep2 & "<td><span class=""room_text"">Country </span><font color=""red"">*</font></td>" & VbCrlf
	strStep2 = strStep2 & "<td>" & VbCrlf
	strStep2 = strStep2 & function_gen_box_sql("SELECT country,title FROM tbl_country ORDER BY title ASC","country",Session("country"),1,4) & VbCrlf
	strStep2 = strStep2 & "</td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	
	IF Session("transfer")=1 Then
		strStep2 = strStep2 & "<tr align=""left"" bgcolor=""#FFFFFF"">" & VbCrlf
		strStep2 = strStep2 & "<td bgcolor=""#FFFFFF""><span class=""room_text"">Flight Arrival Detail: </span></td>" & VbCrlf
		strStep2 = strStep2 & "<td><table width=""100%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#C4DBFF"">" & VbCrlf
		strStep2 = strStep2 & "<tr bgcolor=""#FFFFFF"">" & VbCrlf
		strStep2 = strStep2 & "<td width=""100"" bgcolor=""#FFFFFF""><font color=""1B56BC"">Flight Number: </font></td>" & VbCrlf
		strStep2 = strStep2 & "<td><input type=""text"" name=""a_flight"" value=""""></td>" & VbCrlf
		strStep2 = strStep2 & "</tr>" & VbCrlf
		strStep2 = strStep2 & "<tr bgcolor=""#FFFFFF"">" & VbCrlf
		strStep2 = strStep2 & "<td width=""100"" bgcolor=""#FFFFFF""><font color=""#cc3300"">Arrival Local Time (On Ticket)</font> </td>" & VbCrlf
		strStep2 = strStep2 & "<td> " & VbCrlf
		strStep2 = strStep2 & "Date: " & VbCrlf
		strStep2 = strStep2 & function_gen_dropdown_date(Day(dateCurrentConstant),Month(dateCurrentConstant),Year(dateCurrentConstant),"a_date","a_month","a_year",1)
		strStep2 = strStep2 & "<br /><br />" & function_gen_dropdown_time("a_hour","a_min",0,0,1) & VbCrlf
		strStep2 = strStep2 & "</td>" & VbCrlf
		strStep2 = strStep2 & "</tr>" & VbCrlf
		strStep2 = strStep2 & "</table>" & VbCrlf
		strStep2 = strStep2 & "</td>" & VbCrlf
		strStep2 = strStep2 & "</tr>" & VbCrlf
		strStep2 = strStep2 & "<tr align=""left"" bgcolor=""#FFFFFF"">" & VbCrlf
		strStep2 = strStep2 & "<td bgcolor=""#FFFFFF""><span class=""room_text"">Flight Departure Detail: </span></td>" & VbCrlf
		strStep2 = strStep2 & "<td><table width=""100%"" cellpadding=""2""  cellspacing=""1"" bgcolor=""#C4DBFF"">" & VbCrlf
		strStep2 = strStep2 & "<tr>" & VbCrlf
		strStep2 = strStep2 & "<td width=""100"" bgcolor=""#FFFFFF""><font color=""1B56BC"">Flight Number: </font></td>" & VbCrlf
		strStep2 = strStep2 & "<td bgcolor=""#FFFFFF""><input type=""text"" name=""d_flight"" value=""""></td>" & VbCrlf
		strStep2 = strStep2 & "</tr>" & VbCrlf
		strStep2 = strStep2 & "<tr>" & VbCrlf
		strStep2 = strStep2 & "<td width=""100"" bgcolor=""#FFFFFF""><font color=""#cc3300"">Departure Time</font></td>" & VbCrlf
		strStep2 = strStep2 & "<td bgcolor=""#FFFFFF"">" & VbCrlf
		strStep2 = strStep2 & "Date: " & VbCrlf
		strStep2 = strStep2 & function_gen_dropdown_date(Day(dateNextConstant),Month(dateNextConstant),Year(dateNextConstant),"d_date","d_month","d_year",1)
		strStep2 = strStep2 & "<br /><br />" & function_gen_dropdown_time("d_hour","d_min",0,0,1) & VbCrlf
		strStep2 = strStep2 & "</td>" & VbCrlf
		strStep2 = strStep2 & "</tr>" & VbCrlf
		strStep2 = strStep2 & "</table></td>" & VbCrlf
		strStep2 = strStep2 & "</tr>" & VbCrlf
	End IF
	
	strStep2 = strStep2 & "<tr align=""left"" bgcolor=""#FFFFFF"">" & VbCrlf
	strStep2 = strStep2 & "<td colspan=""2"" bgcolor=""#FFFFFF""><input name=""receive_mail"" type=""checkbox"" value=""yes"" checked> " & VbCrlf
	strStep2 = strStep2 & "<span class=""room_text"">Receive information from hotels2thailand.com </span></td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	strStep2 = strStep2 & "</table></td>" & VbCrlf
	strStep2 = strStep2 & "</tr>" & VbCrlf
	strStep2 = strStep2 & "</table>" & VbCrlf
	
	function_display_rate_affiliate_res_step2 = strStep2
END FUNCTION
%>




                            



