<%
FUNCTION function_display_rate(intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,intPriceMin,intPriceMax,intProductID,intNight,intDisplayType)

	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim intRateCount
	Dim strDateCheckin
	Dim strDateCheckOut
	Dim intNumDisplay
	Dim intNumRate
	Dim intRateDisplay
	Dim dateCheckin
	Dim dateCheckOut
	Dim intDayCount
	Dim strBorderColor
	Dim strRowColor
	Dim intWeek
	Dim intWeekCount
	Dim intNightCount
	Dim intTemp
	Dim intBlank
	Dim intRateType
	Dim arrOptionID
	Dim arrOptionTitle
	Dim strOptionID
	Dim strOptionTitle
	Dim intOptionCount
	Dim strOptionIDTemp
	Dim dateCurrent
	Dim intPrice
	Dim intPriceTemp
	Dim strBreakfast
	Dim strPrice
	Dim strPriceDetail
	Dim bolRate
	Dim k, l, m, j, u
	Dim dateStartTmp
	Dim dateEndTmp
	Dim strColor
	Dim strRackRate
	Dim intRoomTypeCount
	Dim intRoomTypeDisplay
	
	intRoomTypeCount = 1
	intRoomTypeDisplay = 3
	intNumDisplay = 5
	strDateCheckin = function_date_sql(intDayCheckIn,intMonthCheckIn,intYearCheckIn,1)
	strDateCheckOut = function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)
	dateCheckin = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
	dateCheckout = DateSerial(intYearCheckout,intMonthCheckout,intDayCheckout)
	strBorderColor = """#E4E4E4"""
	strRowColor = """#FCFCFC"""
	
SELECT CASE intDisplayType
	
	Case 1,2 'Table with date input	
	
	sqlRate = "SELECT TOP "& intNumDisplay &" o.title_en,op.price,"
	sqlRate = sqlRate & " ISNULL((SELECT so.title_en FROM tbl_product_option_category soc, tbl_product_option so, tbl_option_price sop  WHERE sop.option_id=so.option_id AND soc.option_cat_id=so.option_cat_id AND sop.price=0 AND soc.option_cat_id=40 AND so.product_id=o.product_id AND so.status=1 AND sop.date_start<="& strDateCheckin &" AND sop.date_end>="& strDateCheckout &"),'none') AS meal"
	sqlRate = sqlRate & " FROM tbl_option_price op, tbl_product_option o"
	sqlRate = sqlRate & " WHERE op.option_id=o.option_id AND o.option_cat_id=38 AND (op.price>= "& intPriceMin &" AND op.price<="& intPriceMax &") AND op.date_start<="& strDateCheckin &" AND op.date_end>="& strDateCheckout &"  AND o.product_id=" & intProductID
	sqlRate = sqlRate & " ORDER BY price ASC"

	Set recRate = Server.CreateObject ("ADODB.Recordset")
	recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRate.EOF Then
			arrRate = recRate.GetRows()
			intNumRate = Ubound(arrRate,2)
			intNumRate = intNumRate + 1
			intRateType = 1
			strBreakfast = arrRate(2,0)
			IF strBreakfast="none" Then
				strBreakfast = "-"
			Else
				strBreakfast = "Inc."
			End IF
		Else
			intRateType = 2
		End IF
	recRate.Close
	Set recRate = Nothing
			
	IF intNumRate>intNumDisplay Then
			intRateDisplay = intNumDisplay
		Else
			intRateDisplay = intNumRate
	End IF
	
	
IF intRateType=1 Then 'Continue Rate
	
	SELECT CASE intDisplayType
		
		Case 1 'Display for search with check in, check out and night<=7

			'### Generate Rate Table ###
			function_display_rate = "<table width=""100%""  border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor="& strBorderColor &" class=""s"">" & VbCrlf
			function_display_rate = function_display_rate & "<tr bgcolor=""#FFFBFF"" class=""s2"" align=""center""><td><font color=""#990066"">Room Type<br>("& function_date(dateCheckIn,1) &" - "& function_date(dateCheckOut,1) &")</font></td>" &VbCrlf
			
			For intDayCount=0 To intNight-1
			function_display_rate = function_display_rate & "<td><font color=""#990066"">"& function_date(DateAdd("d",intDayCount,dateCheckin),2) & "<br>(" & function_date(DateAdd("d",intDayCount,dateCheckin),1) &")</font></td>" &VbCrlf
			Next
			
			function_display_rate = function_display_rate & "<td><font color=""#990066"">Avg. Rate</font></td><td><font color=""#990066"">Vat &<br>SVC</font></td><td><font color=""#990066"">BF</font></td></tr>" &VbCrlf

			For intRateCount=0 To intRateDisplay-1
				
				IF intRateCount MOD 2 =0 Then
					strRowColor = """#FFFFFF"""
				Else
					strRowColor = """#FCFCFC"""
				End IF
			
				function_display_rate = function_display_rate & "<tr bgcolor="& strRowColor &" align=""center"">" &VbCrlf
				function_display_rate = function_display_rate & "<td>"& arrRate(0,intRateCount) &"</td>" &VbCrlf
				
				For intDayCount=0 To intNight-1
				function_display_rate = function_display_rate & "<td align=""center"">"& FormatNumber(arrRate(1,intRateCount),0) &"</td>" &VbCrlf
				Next
				
				function_display_rate = function_display_rate & "<td align=""center"" class=""m2""><font color=""#990000"">"& FormatNumber(arrRate(1,intRateCount),0) &"</font></td>" &VbCrlf
				function_display_rate = function_display_rate & "<td rowspan=""1"" class=""s1"" align=""center"">Inc.</td>" &VbCrlf
				function_display_rate = function_display_rate & "<td rowspan=""1"" class=""s1"" align=""center"">"& strBreakfast &"</td>" &VbCrlf
				function_display_rate = function_display_rate & "</tr>" &VbCrlf
			Next
			
			function_display_rate = function_display_rate & "</table>	" &VbCrlf
			'### Generate Rate Table ###
			
		Case 2 'Display for search with check in, check out and night>=7

			intWeek = intNight/7
			IF intWeek>Round(intWeek) Then
				intWeek = Round(intWeek) + 1
			ElseIF intWeek<Round(intWeek) Then
				intWeek = Round(intWeek)
			End IF
			
			'### Generate Rate Table ###
			function_display_rate = "<table width=""100%""  border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor="& strBorderColor &" class=""s"">" & VbCrlf
			function_display_rate = function_display_rate & "<tr bgcolor=""#FFFBFF"" class=""s2"" align=""center""><td><font color=""#990066"">Room Type<br>("& function_date(dateCheckIn,1) &" - "& function_date(dateCheckOut,1) &")</font></td>" &VbCrlf
			function_display_rate = function_display_rate & "<td>&nbsp;</font></td>" &VbCrlf 'Blank TD Use for week
			
			For intDayCount=0 To 6
			function_display_rate = function_display_rate & "<td><font color=""#990066"">"& function_date(DateAdd("d",intDayCount,dateCheckin),2) & "</font></td>" &VbCrlf
			Next
			
			function_display_rate = function_display_rate & "<td><font color=""#990066"">Avg. Rate</font></td><td><font color=""#990066"">Vat &<br>SVC</font></td><td><font color=""#990066"">BF</font></td></tr>" &VbCrlf
			
			For intRateCount=0 To intRateDisplay-1
				
				intNightCount =0
				
				IF intRateCount MOD 2 =0 Then
					strRowColor = """#FFFFFF"""
				Else
					strRowColor = """#FCFCFC"""
				End IF
			
				For intWeekCount=1 To intWeek
				
					intBlank = 0

					IF intWeekCount=1 Then
						function_display_rate = function_display_rate & "<tr bgcolor="& strRowColor &" align=""center"">" &VbCrlf
						function_display_rate = function_display_rate & "<td rowspan="""& intWeek &""">"& arrRate(0,intRateCount) &"</td>" &VbCrlf
						function_display_rate = function_display_rate & "<td align=""center""><font color=""#990000"">wk1</font></td>"
						
						For intTemp=1 To 7
							intNightCount = intNightCount+1
							IF intNightCount>intNight Then Exit For End IF
							function_display_rate = function_display_rate & "<td align=""center"">"& FormatNumber(arrRate(1,intRateCount),0) &"</td>" &VbCrlf
						Next
						
						function_display_rate = function_display_rate & "<td align=""center"" class=""m2"" rowspan="""& intWeek &"""><font color=""#990000"">"& FormatNumber(arrRate(1,intRateCount),0) &"</font></td>" &VbCrlf
						function_display_rate = function_display_rate & "<td rowspan="""& intWeek &""" class=""s1"" align=""center"">Inc.</td>" &VbCrlf
						function_display_rate = function_display_rate & "<td rowspan="""& intWeek &""" class=""s1"" align=""center"">"& strBreakfast &"</td>" &VbCrlf
						function_display_rate = function_display_rate & "</tr>" &VbCrlf
					Else 'More Than 1 Week
						
						function_display_rate = function_display_rate & "<tr bgcolor="& strRowColor &" align=""center"">" &VbCrlf
						function_display_rate = function_display_rate & "<td align=""center""><font color=""#990000"">wk"& intWeekCount &"</font></td>"
						
						For intTemp=1 To 7
							intNightCount = intNightCount+1
							IF intNightCount>intNight Then
								intBlank = 8-intTemp
								Exit For 
							End IF
							function_display_rate = function_display_rate & "<td align=""center"">"& FormatNumber(arrRate(1,intRateCount),0) &"</td>" &VbCrlf
						Next
						
						IF intBlank>0 Then 'Fill TD
							For intTemp=1 To intBlank
								function_display_rate = function_display_rate & "<td></td>" & VbCrlf
							Next
						End IF

						function_display_rate = function_display_rate & "</tr>" &VbCrlf
						
					End IF

				Next
				
			Next
			
			function_display_rate = function_display_rate & "</table>	" &VbCrlf
			'### Generate Rate Table ###
			
	END SELECT
	
ElseIF intRateType=2 Then 'Discontinue Rate

	sqlRate = "SELECT o.title_en,op.price,op.date_start,op.date_end,op.option_id,"
	sqlRate = sqlRate & " ISNULL((SELECT so.title_en FROM tbl_product_option_category soc, tbl_product_option so, tbl_option_price sop  WHERE sop.option_id=so.option_id AND soc.option_cat_id=so.option_cat_id AND sop.price=0 AND soc.option_cat_id=40 AND so.product_id=o.product_id AND so.status=1 AND (sop.date_start<="& strDateCheckin &" OR sop.date_end>="& strDateCheckout &")),'none') AS meal"
	sqlRate = sqlRate & " FROM tbl_option_price op, tbl_product_option o"
	sqlRate = sqlRate & " WHERE op.option_id=o.option_id AND o.option_cat_id=38 AND (op.price>= "& intPriceMin &" AND op.price<="& intPriceMax &") AND (op.date_start>="& strDateCheckin &" OR op.date_end<="& strDateCheckout &")  AND product_id=" & intProductID
	sqlRate = sqlRate & " ORDER BY op.option_id ASC,op.date_start ASC, price ASC"

	Set recRate = Server.CreateObject ("ADODB.Recordset")
	recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
		arrRate = recRate.GetRows()
		intNumRate = Ubound(arrRate,2)
		intNumRate = intNumRate + 1
		strBreakfast = arrRate(5,0)
		IF strBreakfast="none" Then
			strBreakfast = "-"
		Else
			strBreakfast = "Inc."
		End IF
	recRate.Close
	Set recRate = Nothing

	'### Get Option ID and Option Title to String Separate with | ###
	For intOptionCount=0 To Ubound(arrRate,2)
		IF strOptionIDTemp <> Cstr(arrRate(4,intOptionCount)) Then
			strOptionID = strOptionID & "|" & arrRate(4,intOptionCount)
			strOptionTitle = strOptionTitle & "|" & arrRate(0,intOptionCount)
		End IF
		strOptionIDTemp = Cstr(arrRate(4,intOptionCount))
	Next
	'### Get Option ID and Option Title to String Separate with | ###
	
	strOptionID = Right(strOptionID,Len(strOptionID)-1)
	strOptionTitle = Right(strOptionTitle,Len(strOptionTitle)-1)
	
	arrOptionID = Split(strOptionID,"|")
	arrOptionTitle = Split(strOptionTitle,"|")
	
	SELECT CASE intDisplayType
		Case 1 'Display As Day
			'### Generate Rate Table ###
			function_display_rate = "<table width=""100%""  border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor="& strBorderColor &" class=""s"">" & VbCrlf
			function_display_rate = function_display_rate & "<tr bgcolor=""#FFFBFF"" class=""s2"" align=""center""><td><font color=""#990066"">Room Type<br>("& function_date(dateCheckIn,1) &" - "& function_date(dateCheckOut,1) &")</font></td>" &VbCrlf
			
			For intDayCount=0 To intNight-1
			function_display_rate = function_display_rate & "<td><font color=""#990066"">"& function_date(DateAdd("d",intDayCount,dateCheckin),2) & "<br>(" & function_date(DateAdd("d",intDayCount,dateCheckin),1) &")</font></td>" &VbCrlf
			Next
			
			function_display_rate = function_display_rate & "<td><font color=""#990066"">Avg. Rate</font></td><td><font color=""#990066"">Vat &<br>SVC</font></td><td><font color=""#990066"">BF</font></td></tr>" &VbCrlf

			For intRateCount=0 To Ubound(arrOptionID)
				
				IF intRateCount MOD 2 =0 Then
					strRowColor = """#FFFFFF"""
				Else
					strRowColor = """#FCFCFC"""
				End IF
			
				function_display_rate = function_display_rate & "<tr bgcolor="& strRowColor &" align=""center"">" &VbCrlf
				function_display_rate = function_display_rate & "<td>"& arrOptionTitle(intRateCount) &"</td>" &VbCrlf
				
				dateCurrent = dateCheckin
				intPriceTemp = 0
				For intDayCount=0 To intNight-1
					intPrice = function_get_price(arrRate,dateCurrent,arrOptionID(intRateCount))
					function_display_rate = function_display_rate & "<td align=""center"">"& FormatNumber(intPrice,0) &"</td>" &VbCrlf
					intPriceTemp = intPriceTemp + intPrice
					dateCurrent = DateAdd("d",1,dateCurrent)
				Next
				
				function_display_rate = function_display_rate & "<td align=""center"" class=""m2""><font color=""#990000"">"& FormatNumber(intPriceTemp/(intNight),0) &"</font></td>" &VbCrlf
				function_display_rate = function_display_rate & "<td rowspan=""1"" class=""s1"" align=""center"">Inc.</td>" &VbCrlf
				function_display_rate = function_display_rate & "<td rowspan=""1"" class=""s1"" align=""center"">"& strBreakfast &"</td>" &VbCrlf
				function_display_rate = function_display_rate & "</tr>" &VbCrlf
			Next
			
			function_display_rate = function_display_rate & "</table>	" &VbCrlf
			
			Case 2 'Display As Week
			intWeek = intNight/7
			IF intWeek>Round(intWeek) Then
				intWeek = Round(intWeek) + 1
			ElseIF intWeek<Round(intWeek) Then
				intWeek = Round(intWeek)
			End IF
			
			'### Generate Rate Table ###
			function_display_rate = "<table width=""100%""  border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor="& strBorderColor &" class=""s"">" & VbCrlf
			function_display_rate = function_display_rate & "<tr bgcolor=""#FFFBFF"" class=""s2"" align=""center""><td><font color=""#990066"">Room Type<br>("& function_date(dateCheckIn,1) &" - "& function_date(dateCheckOut,1) &")</font></td>" &VbCrlf
			function_display_rate = function_display_rate & "<td>&nbsp;</font></td>" &VbCrlf 'Blank TD Use for week
			
			For intDayCount=0 To 6
			function_display_rate = function_display_rate & "<td><font color=""#990066"">"& function_date(DateAdd("d",intDayCount,dateCheckin),2) & "</font></td>" &VbCrlf
			Next
			
			function_display_rate = function_display_rate & "<td><font color=""#990066"">Avg. Rate</font></td><td><font color=""#990066"">Vat &<br>SVC</font></td><td><font color=""#990066"">BF</font></td></tr>" &VbCrlf
			
			For intRateCount=0 To Ubound(arrOptionID)
				
				intNightCount =0
				
				IF intRateCount MOD 2 =0 Then
					strRowColor = """#FFFFFF"""
				Else
					strRowColor = """#FCFCFC"""
				End IF
			
				For intWeekCount=1 To intWeek
				
					intBlank = 0

					IF intWeekCount=1 Then
						function_display_rate = function_display_rate & "<tr bgcolor="& strRowColor &" align=""center"">" &VbCrlf
						function_display_rate = function_display_rate & "<td rowspan="""& intWeek &""">"& arrOptionTitle(intRateCount) &"</td>" &VbCrlf
						function_display_rate = function_display_rate & "<td align=""center""><font color=""#990000"">wk1</font></td>"

						dateCurrent = dateCheckin
						intPriceTemp = 0
						For intTemp=1 To 7
							intNightCount = intNightCount+1
							IF intNightCount>intNight Then Exit For End IF
							intPrice = function_get_price(arrRate,dateCurrent,arrOptionID(intRateCount))
							function_display_rate = function_display_rate & "<td align=""center"">"& FormatNumber(intPrice,0) &"</td>" &VbCrlf
							intPriceTemp = intPriceTemp + intPrice
							dateCurrent = DateAdd("d",1,dateCurrent)
						Next
						
						function_display_rate = function_display_rate & "<td align=""center"" class=""m2"" rowspan="""& intWeek &"""><font color=""#990000"">intPriceTemp</font></td>" &VbCrlf
						function_display_rate = function_display_rate & "<td rowspan="""& intWeek &""" class=""s1"" align=""center"">Inc.</td>" &VbCrlf
						function_display_rate = function_display_rate & "<td rowspan="""& intWeek &""" class=""s1"" align=""center"">"& strBreakfast &"</td>" &VbCrlf
						function_display_rate = function_display_rate & "</tr>" &VbCrlf
					Else 'More Than 1 Week
						
						function_display_rate = function_display_rate & "<tr bgcolor="& strRowColor &" align=""center"">" &VbCrlf
						function_display_rate = function_display_rate & "<td align=""center""><font color=""#990000"">wk"& intWeekCount &"</font></td>"

						For intTemp=1 To 7
							intNightCount = intNightCount+1
							IF intNightCount>intNight Then
								intBlank = 8-intTemp
								Exit For 
							End IF
							intPrice = function_get_price(arrRate,dateCurrent,arrOptionID(intRateCount))
							function_display_rate = function_display_rate & "<td align=""center"">"& FormatNumber(intPrice,0) &"</td>" &VbCrlf
							intPriceTemp = intPriceTemp + intPrice
							dateCurrent = DateAdd("d",1,dateCurrent)
						Next
						
						IF intBlank>0 Then 'Fill TD
							For intTemp=1 To intBlank
								function_display_rate = function_display_rate & "<td></td>" & VbCrlf
							Next
						End IF

						function_display_rate = function_display_rate & "</tr>" &VbCrlf
						function_display_rate = Replace(function_display_rate,"intPriceTemp",FormatNumber(intPriceTemp/intNight,0))

					End IF

				Next
				
			Next
			
			function_display_rate = function_display_rate & "</table>	" &VbCrlf
			'### Generate Rate Table ###
			
	END SELECT
End IF

Case 3 'Table Without Date

sqlRate = "SELECT op.date_start, op.date_end, o.title_en, op.price,op.price_rack,"
sqlRate = sqlRate & " ISNULL((SELECT so.title_en FROM tbl_product_option_category soc, tbl_product_option so, tbl_option_price sop  WHERE sop.option_id=so.option_id AND soc.option_cat_id=so.option_cat_id AND sop.price=0 AND soc.option_cat_id=40 AND so.product_id=o.product_id AND so.status=1 AND sop.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &"),'none') AS meal"
sqlRate = sqlRate & " FROM tbl_option_price op, tbl_product_option o, tbl_product p"
sqlRate = sqlRate & " WHERE op.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND p.product_id=o.product_id AND o.option_id=op.option_id AND o.status=1 AND o.option_cat_id=38 AND p.product_id=" & intProductID
sqlRate = sqlRate & " ORDER BY date_start ASC, date_end ASC, price ASC"

	Set recRate = Server.CreateObject ("ADODB.Recordset")
	recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRate.EOF Then
			arrRate = recRate.GetRows()
			intNumRate = Ubound(arrRate,2)
			intNumRate = intNumRate + 1
			bolRate = True
			strBreakfast = arrRate(5,0)
			IF strBreakfast="none" Then
				strBreakfast = "-"
			Else
				strBreakfast = "Inc."
			End IF		
		Else
			bolRate = False
		End IF
	recRate.Close
	Set recRate = Nothing
	
IF bolRate Then
	strPrice = "<table width='100%' cellpadding='2' cellspacing='1' bgcolor="& strBorderColor &">" & VbCrlf
	strPrice = strPrice & "<tr bgcolor=""#FFFBFF"" class=""s2"" align=""center"">" & VbCrlf
	strPrice = strPrice & "<td><div align='center'><font color='#990066'>Period</font></div></td>" & VbCrlf
	strPrice = strPrice & "<td> <div align='center'><font color='#990066'>Room Types</font></div></td>" & VbCrlf
	strPrice = strPrice & "<td '> <div align='center'><font color='#990066'>Our Rates</font></div></td>" & VbCrlf
	strPrice = strPrice & "<td '> <div align='center'><font color='#990066'>Rack Rates</font></div></td>" & VbCrlf
	strPrice = strPrice & "<td><font color=""#990066"">Vat &<br>SVC</font></td><td><font color=""#990066"">BF</font></td>" & VbCrlf
	strPrice = strPrice & "</tr>" & VbCrlf
	
	For k=0 To Ubound(arrRate,2)
		
		IF NOT ISNULL(arrRate(4,k)) AND arrRate(4,k)<>0 AND arrRate(4,k)<>"" Then
			strRackRate = "<td class=""ss""><div align='center'>"& FormatNumber(arrRate(4,k),0) & "</div></td>"
		Else
			strRackRate = "<td><div align='center'>n/a</div></td>"
		End IF
		
		IF dateStartTmp=arrRate(0,k) AND dateEndTmp=arrRate(1,k) Then
			strPriceDetail = strPriceDetail & "<tr align='left' valign='middle' bgcolor="& strColor &"> " & VbCrlf
			strPriceDetail = strPriceDetail & "<td> <div align='left'>"& arrRate(2,k) &"</div></td>" & VbCrlf
			strPriceDetail = strPriceDetail & "<td> <div align='center'><font color=""#990000"">"& FormatNumber(arrRate(3,k),0) &"</font></div></td>" & VbCrlf
			strPriceDetail = strPriceDetail & strRackRate & VbCrlf
			strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">Inc.</td>" &VbCrlf
			strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">"& strBreakfast &"</td>" &VbCrlf
			strPriceDetail = strPriceDetail & "</tr>" & VbCrlf
			u = u + 1
		Else
			
			IF j MOD 2 = 0 Then
				strColor = """#FFFFFF"""
			Else
				strColor = strRowColor
			End IF
			
			strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
			strPriceDetail = strPriceDetail & "<tr align='left' valign='middle' bgcolor="&strColor&"> " & VbCrlf
			strPriceDetail = strPriceDetail & "<td rowspan='intRowSpan'><div align='center'>"& function_date(arrRate(0,k),3) &" - " & function_date(arrRate(1,k),3) & "</div></td>" & VbCrlf
			strPriceDetail = strPriceDetail & "<td> <div align='left'>"& arrRate(2,k)&"</div></td>" & VbCrlf
			strPriceDetail = strPriceDetail & "<td> <div align='center'><font color=""#990000"">"& FormatNumber(arrRate(3,k),0) &"</font></div></td>" & VbCrlf
			strPriceDetail = strPriceDetail & strRackRate & VbCrlf
			strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">Inc.</td>" &VbCrlf
			strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">"& strBreakfast &"</td>" &VbCrlf
			strPriceDetail = strPriceDetail & "</tr>" & VbCrlf
				
			u = 1
			j = j + 1
			
			
		End IF			
			
		dateStartTmp = DateSerial(Year(arrRate(0,k)),Month(arrRate(0,k)),Day(arrRate(0,k)))
		dateEndTmp = DateSerial(Year(arrRate(1,k)),Month(arrRate(1,k)),Day(arrRate(1,k)))
			
	Next
		
	strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
	strPrice = strPrice & strPriceDetail
	strPrice = strPrice & "</tr>" & VbCrlf
	strPrice = strPrice & "</table>" & VbCrlf

	function_display_rate = strPrice
End IF


Case 4 'Table Without Date Display Some Rate

sqlRate = "SELECT op.date_start, op.date_end, o.title_en, op.price,op.price_rack,"
sqlRate = sqlRate & " ISNULL((SELECT so.title_en FROM tbl_product_option_category soc, tbl_product_option so, tbl_option_price sop  WHERE sop.option_id=so.option_id AND soc.option_cat_id=so.option_cat_id AND sop.price=0 AND soc.option_cat_id=40 AND so.product_id=o.product_id AND so.status=1 AND sop.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &"),'none') AS meal"
sqlRate = sqlRate & " FROM tbl_option_price op, tbl_product_option o, tbl_product p"
sqlRate = sqlRate & " WHERE op.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND p.product_id=o.product_id AND o.option_id=op.option_id AND o.status=1 AND o.option_cat_id=38 AND p.product_id=" & intProductID
sqlRate = sqlRate & " ORDER BY date_start ASC, date_end ASC, price ASC"

	Set recRate = Server.CreateObject ("ADODB.Recordset")
	recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRate.EOF Then
			arrRate = recRate.GetRows()
			intNumRate = Ubound(arrRate,2)
			intNumRate = intNumRate + 1
			bolRate = True
			strBreakfast = arrRate(5,0)
			IF strBreakfast="none" Then
				strBreakfast = "-"
			Else
				strBreakfast = "Inc."
			End IF		
		Else
			bolRate = False
		End IF
	recRate.Close
	Set recRate = Nothing
	
IF bolRate Then
	strPrice = "<table width='100%' cellpadding='2' cellspacing='1' bgcolor="& strBorderColor &">" & VbCrlf
	strPrice = strPrice & "<tr bgcolor=""#FFFBFF"" class=""s2"" align=""center"">" & VbCrlf
	strPrice = strPrice & "<td><div align='center'><font color='#990066'>Period</font></div></td>" & VbCrlf
	strPrice = strPrice & "<td> <div align='center'><font color='#990066'>Room Types</font></div></td>" & VbCrlf
	strPrice = strPrice & "<td '> <div align='center'><font color='#990066'>Our Rates</font></div></td>" & VbCrlf
	strPrice = strPrice & "<td '> <div align='center'><font color='#990066'>Rack Rates</font></div></td>" & VbCrlf
	strPrice = strPrice & "<td><font color=""#990066"">Vat &<br>SVC</font></td><td><font color=""#990066"">BF</font></td>" & VbCrlf
	strPrice = strPrice & "</tr>" & VbCrlf
	
	For k=0 To Ubound(arrRate,2)
		
		IF NOT ISNULL(arrRate(4,k)) AND arrRate(4,k)<>0 AND arrRate(4,k)<>"" Then
			strRackRate = "<td class=""ss""><div align='center'>"& FormatNumber(arrRate(4,k),0) & "</div></td>"
		Else
			strRackRate = "<td><div align='center'>n/a</div></td>"
		End IF
		
		IF dateStartTmp=arrRate(0,k) AND dateEndTmp=arrRate(1,k) Then
		
			intRoomTypeCount = intRoomTypeCount + 1
			
			IF intRoomTypeCount<=intRoomTypeDisplay Then
				strPriceDetail = strPriceDetail & "<tr align='left' valign='middle' bgcolor="& strColor &"> " & VbCrlf
				strPriceDetail = strPriceDetail & "<td> <div align='left'>"& arrRate(2,k) &"</div></td>" & VbCrlf
				strPriceDetail = strPriceDetail & "<td> <div align='center'><font color=""#990000"">"& FormatNumber(arrRate(3,k),0) &"</font></div></td>" & VbCrlf
				strPriceDetail = strPriceDetail & strRackRate & VbCrlf
				strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">Inc.</td>" &VbCrlf
				strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">"& strBreakfast &"</td>" &VbCrlf
				strPriceDetail = strPriceDetail & "</tr>" & VbCrlf
				u = u + 1
			End IF
			
		Else
			
			IF j MOD 2 = 0 Then
				strColor = """#FFFFFF"""
			Else
				strColor = strRowColor
			End IF
			
			intRoomTypeCount = 1
			
			strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
			strPriceDetail = strPriceDetail & "<tr align='left' valign='middle' bgcolor="&strColor&"> " & VbCrlf
			strPriceDetail = strPriceDetail & "<td rowspan='intRowSpan'><div align='center'>"& function_date(arrRate(0,k),3) &" - " & function_date(arrRate(1,k),3) & "</div></td>" & VbCrlf
			strPriceDetail = strPriceDetail & "<td> <div align='left'>"& arrRate(2,k)&"</div></td>" & VbCrlf
			strPriceDetail = strPriceDetail & "<td> <div align='center'><font color=""#990000"">"& FormatNumber(arrRate(3,k),0) &"</font></div></td>" & VbCrlf
			strPriceDetail = strPriceDetail & strRackRate & VbCrlf
			strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">Inc.</td>" &VbCrlf
			strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">"& strBreakfast &"</td>" &VbCrlf
			strPriceDetail = strPriceDetail & "</tr>" & VbCrlf
				
			u = 1
			j = j + 1
			
			
		End IF			
			
		dateStartTmp = DateSerial(Year(arrRate(0,k)),Month(arrRate(0,k)),Day(arrRate(0,k)))
		dateEndTmp = DateSerial(Year(arrRate(1,k)),Month(arrRate(1,k)),Day(arrRate(1,k)))
			
	Next
		
	strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
	strPrice = strPrice & strPriceDetail
	strPrice = strPrice & "</tr>" & VbCrlf
	strPrice = strPrice & "</table>" & VbCrlf

	function_display_rate = strPrice
End IF

END SELECT
END FUNCTION


'### Function Get Room Price With Specific Date ###
FUNCTION function_get_price(arrRate,DateInput,intOptionID)

	Dim intCount

	For intCount=0 To Ubound(arrRate,2)
	
		IF (DateInput>=arrRate(2,intCount) AND DateInput<=arrRate(3,intCount)) AND Cstr(intOptionID)=Cstr(arrRate(4,intCount))Then
			function_get_price = arrRate(1,intCount)
		End IF
	
	Next
END FUNCTION
'### Function Get Room Price With Specific Date ###
%>