<%
FUNCTION function_display_rate_room_java(intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,intPriceMin,intPriceMax,intProductID,intNight,arrAllot,intType)
	
	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim intRate
	Dim intCount
	Dim strColor
	Dim intRoomType
	Dim intRoomTypeTemp
	Dim intOptionID
	Dim strRowTemp
	Dim arrRoomType()
	Dim intNumType
	Dim intNumTypeTemp
	Dim intTypeCount
	Dim intRowType
	Dim intRowSpan
	Dim dateCheckIn
	Dim dateCheckOut
	Dim intDayCount
	Dim dateCurrent
	Dim sqlType
	Dim recType
	Dim arrType
	Dim intPriceTemp
	Dim intPrice
	Dim intPriceRack
	Dim intPriceAvg
	Dim intWeek
	Dim intWeekCount
	Dim intRateCount
	Dim strWeekAvg
	Dim strWeekRack
	Dim bolType
	Dim sqlPromotion
	Dim recPromotion
	Dim arrPromotion
	Dim bolPromotion
	Dim intPriceAverage
	Dim intPriceAverageRack
	Dim intNumRate
	Dim bolRate
	Dim j,k,u
	Dim strPrice
	Dim strPriceDetail
	Dim dateStartTmp
	Dim dateEndTmp
	Dim strRowColor
	Dim strRoomType
	
	SELECT CASE intType
		Case 1 'Hotel Rate Without DateInput
			sqlRate = "st_hotel_rate_list_rate_1 "&intProductID&"," & function_date_sql(Day(Date),Month(Date),Year(Date),1)

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
				strPrice = "document.write('<table width=""100%"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">');" & VbCrlf
				strPrice = strPrice & "document.write('<tr bgcolor=""#EDF5FE"" class=""s2"" align=""center"">');" & VbCrlf
				strPrice = strPrice & "document.write('<td rowspan=""2""><div align=""center""><font color=""#346494""><b>Period</b></font></div></td>');" & VbCrlf
				strPrice = strPrice & "document.write('<td rowspan=""2""> <div align=""center""><font color=""#346494""><b>Room Types</b></font></div></td>');" & VbCrlf
				strPrice = strPrice & "document.write('<td> <div align=""center""><font color=""#346494""><b>Our Rates</b></font></div></td>');" & VbCrlf
				strPrice = strPrice & "document.write('<td> <div align=""center""><font color=""#346494""><b>Rack Rates</b></font></div></td>');" & VbCrlf
				strPrice = strPrice & "document.write('<td rowspan=""2""><font color=""#346494""><b>Breakfast</font></b></td>');" & VbCrlf
				strPrice = strPrice & "document.write('</tr>');" & VbCrlf
				strPrice = strPrice & "document.write('<tr>');" & VbCrlf
				strPrice = strPrice & "document.write('<td colspan=""2"" align=""center"" bgcolor=""#EDF5FE""><font color=""#990066"">("&Session("currency_title") & " <img src=""/images/flag_"&Session("currency_code")&".gif"">"&")</font></td>');" & VbCrlf
				strPrice = strPrice & "document.write('</tr>');" & VbCrlf

				For k=0 To Ubound(arrRate,2)
					
					'###Change option title to link ###
					strRoomType = function_gen_option_title(arrRate(7,k),arrRate(9,k),arrRate(8,k),arrRate(2,k),arrRate(6,k),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,arrRate(10,k),1)
					strRoomType = Replace(strRoomType,"'","\'")
					'###Change option title to link ###
				
					IF dateStartTmp=arrRate(0,k) AND dateEndTmp=arrRate(1,k) Then
						strPriceDetail = strPriceDetail & "document.write('<tr align=""left"" valign=""middle"" bgcolor="& strColor &">');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td> <div align=""left"">"& strRoomType &"</div></td>');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td> <div align=""center""><font color=""#990000""><b>"& function_display_price(arrRate(3,k),1) &"</b></font></div></td>');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td><div align=""center"">"& function_display_price(arrRate(4,k),2) &"</div></td>');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td rowspan=""1"" class=""s1"" align=""center"">"& function_display_bol("<img src=""/images/ok.gif"">","-",arrRate(5,k),"",2) &"</td>');" &VbCrlf
						strPriceDetail = strPriceDetail & "document.write('</tr>');" & VbCrlf
						u = u + 1
					Else
						
						IF j MOD 2 = 0 Then
							strColor = """#FFFFFF"""
						Else
							strColor = """#F8FCF3"""
						End IF
						
						strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
						strPriceDetail = strPriceDetail & "document.write('<tr align=""left"" valign=""middle"" bgcolor="&strColor&">');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td rowspan=""intRowSpan""><div align=""center"">"& function_date(arrRate(0,k),3) &" - " & function_date(arrRate(1,k),3) & "</div></td>');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td> <div align=""left"">"&strRoomType&"</div></td>');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td> <div align=""center""><font color=""#990000""><b>"& function_display_price(arrRate(3,k),1) &"</b></font></div></td>');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td><div align=""center"">"& function_display_price(arrRate(4,k),2) &"</div></td>');" & VbCrlf
						strPriceDetail = strPriceDetail & "document.write('<td rowspan=""1"" class=""s1"" align=""center"">"& function_display_bol("<img src=""/images/ok.gif"">","-",arrRate(5,k),"",2) &"</td>');" &VbCrlf
						strPriceDetail = strPriceDetail & "document.write('</tr>');" & VbCrlf
							
						u = 1
						j = j + 1
						
						
					End IF			
						
					dateStartTmp = DateSerial(Year(arrRate(0,k)),Month(arrRate(0,k)),Day(arrRate(0,k)))
					dateEndTmp = DateSerial(Year(arrRate(1,k)),Month(arrRate(1,k)),Day(arrRate(1,k)))
						
				Next
					
				strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
				strPrice = strPrice & strPriceDetail
				strPrice = strPrice & "document.write('</table>');" & VbCrlf

			End IF
			
			
			function_display_rate_room_java = strPrice
			
		Case 2 'With Date Input
	END SELECT

END FUNCTION
%>