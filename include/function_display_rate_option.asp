<%
FUNCTION function_display_rate_option(intProductID,intDestinationID,intLocationID,intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,intType)

	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim intRate
	Dim intCount
	Dim strColor
	Dim bolRate
	Dim strTransfer
	Dim bolTransfer
	Dim strExtraName
	Dim strExtraID
	Dim arrExtraID
	Dim strExtraCheck
	Dim strBedID
	Dim arrBedID
	Dim strBedCheck
	Dim strAirportID
	Dim arrAirportID
	Dim strAirportCheck3740
	Dim strAirportCheck3741
	Dim strAirportCheck3742
	Dim strAirportCheck3743
	Dim strAirportCheck3745
	Dim strAirportCheck3746
	Dim strAirportCheck3749
	Dim strAirportCheck3750
	Dim strAirportCheck3752
	Dim strAirportCheck3754
	Dim strAirportCheck4113
	Dim strAirportCheck4114
	Dim strAirportCheck4119
	Dim strAirportCheck4120
	Dim strAirportCheck4121
	Dim strAirportCheck4122
	Dim strAirportCheck4123
	Dim strAirportCheck4124
	Dim strAirportCheck5427
	Dim strAirportCheck5428
	Dim strAirportCheck5429
	Dim strAirportCheck5430
	
	Dim strAirportCheck22759
	Dim strAirportCheck22763
	Dim strAirportCheck22764
	Dim strAirportCheck22765
	
	
	bolTransfer = function_check_airport(intProductID,"",intDayCheckin,intMonthCheckin,intYearCheckin,intDayCheckout,intMonthCheckout,intYearCheckout,1)
	
	IF Request("airport_id")<>"" Then
		strAirportID = Request("airport_id")
		strAirportID = Replace(strAirportID," ","")
		arrAirportID = Split(strAirportID,",")
	End IF
	
	SELECT CASE intDestinationID
		Case 30 'Bangkok
					IF function_array_check("4113",arrAirportID,2) Then
						strAirportCheck4113 = "checked"
					Else
						strAirportCheck4113 =""
					End IF
					
					IF function_array_check("4114",arrAirportID,2) Then
						strAirportCheck4114= "checked"
					Else
						strAirportCheck4114 =""
					End IF
						
					IF function_array_check("4119",arrAirportID,2) Then
						strAirportCheck4119 = "checked"
					Else
						strAirportCheck4119 =""
					End IF
					
					IF function_array_check("4120",arrAirportID,2) Then
						strAirportCheck4120= "checked"
					Else
						strAirportCheck4120 =""
					End IF
						
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""4113"" "&strAirportCheck4113&"></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4113"),"airport4113",1)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*1320/Session("currency_prefix"),0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
					
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""4114"" "&strAirportCheck4114&"></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4114"),"airport4114",1)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*2600/Session("currency_prefix"),0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
					
					
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""4119"" "&strAirportCheck4119&"></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4119"),"airport4119",1)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>One way airport transfer Per Van <br><font color=""green"">(Contain 8-10 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*1830/Session("currency_prefix"),0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
					
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""4120"" "&strAirportCheck4120&"></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4120"),"airport4120",1)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>Round trip airport transfer Per Van <br><font color=""green"">(Contain 8-10 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*3600/Session("currency_prefix"),0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
                    
		Case 31 'Phuket
'				IF function_array_check("5427",arrAirportID,2) Then
'						strAirportCheck5427 = "checked"
'					Else
'						strAirportCheck5427 =""
'					End IF
'					
'					IF function_array_check("5428",arrAirportID,2) Then
'						strAirportCheck5428= "checked"
'					Else
'						strAirportCheck5428 =""
'					End IF
'						
'					IF function_array_check("5429",arrAirportID,2) Then
'						strAirportCheck5429 = "checked"
'					Else
'						strAirportCheck5429 =""
'					End IF
'					
'					IF function_array_check("5430",arrAirportID,2) Then
'						strAirportCheck5430= "checked"
'					Else
'						strAirportCheck5430 =""
'					End IF
'					
'					IF Cstr(intLocationID) <> "173" AND Cstr(intLocationID) <> "188" AND Cstr(intLocationID) <> "267" Then 'Exclude Koh Yao and Koh Lon and Coral Island
'					
'						strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
'						strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""5427"" "&strAirportCheck5427&"></td>" & VbCrlf
'						strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport5427"),"airport5427",1)&"</td>" & VbCrlf
'						strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
'						strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*1060/Session("currency_prefix"),0) & "</td>" & VbCrlf
'						strTransfer = strTransfer & "</tr>" & VbCrlf
'						
'						strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
'						strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""5428"" "&strAirportCheck5428&"></td>" & VbCrlf
'						strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport5428"),"airport5428",1)&"</td>" & VbCrlf
'						strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
'						strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*2100/Session("currency_prefix"),0) & "</td>" & VbCrlf
'						strTransfer = strTransfer & "</tr>" & VbCrlf
'						
'						strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
'						strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""5429"" "&strAirportCheck5429&"></td>" & VbCrlf
'						strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport5429"),"airport5429",1)&"</td>" & VbCrlf
'						strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 4-5 People)</font></td>" & VbCrlf
'						strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*1760/Session("currency_prefix"),0) & "</td>" & VbCrlf
'						strTransfer = strTransfer & "</tr>" & VbCrlf
'						
'						strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
'						strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""5430"" "&strAirportCheck5430&"></td>" & VbCrlf
'						strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport5430"),"airport5430",1)&"</td>" & VbCrlf
'						strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 4-5 People)</font></td>" & VbCrlf
'						strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*3500/Session("currency_prefix"),0) & "</td>" & VbCrlf
'						strTransfer = strTransfer & "</tr>" & VbCrlf
'						
'					End IF
'					
		Case 32 'Chiang Mai

					IF function_array_check("3745",arrAirportID,2) Then
						strAirportCheck3745 = "checked"
					Else
						strAirportCheck3745 =""
					End IF
					
					IF function_array_check("3746",arrAirportID,2) Then
						strAirportCheck3746= "checked"
					Else
						strAirportCheck3746 =""
					End IF

					IF Cstr(intLocationID) <> "226" AND Cstr(intLocationID) <> "233" AND Cstr(intLocationID) <> "88" AND Cstr(intLocationID) <> "77" AND Cstr(intLocationID) <> "81" AND Cstr(intLocationID) <> "83" AND Cstr(intLocationID) <> "231" AND Cstr(intLocationID) <> "263" AND Cstr(intLocationID) <> "85" AND Cstr(intLocationID) <> "175" Then
						strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
						strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""3745"" "&strAirportCheck3745&"></td>" & VbCrlf
						strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport3745"),"airport3745",1)&"</td>" & VbCrlf
						strTransfer = strTransfer & "<td>One way airport transfer Per Van <br><font color=""green"">(Contain 6-12 People)</font></td>" & VbCrlf
						strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*400/Session("currency_prefix"),0) & "</td>" & VbCrlf
						strTransfer = strTransfer & "</tr>" & VbCrlf
						
						strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
						strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""3746"" "&strAirportCheck3746&"></td>" & VbCrlf
						strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport3746"),"airport3746",1)&"</td>" & VbCrlf
						strTransfer = strTransfer & "<td>Round trip airport transfer Per Van <br><font color=""green"">(Contain 6-12 People)</font></td>" & VbCrlf
						strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*750/Session("currency_prefix"),0) & "</td>" & VbCrlf
						strTransfer = strTransfer & "</tr>" & VbCrlf
					End IF
		Case 33 'Pattaya
		
'					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
'					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""4121"" "&strAirportCheck4121&"></td>" & VbCrlf
'					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4121"),"airport4121",1)&"</td>" & VbCrlf
'					strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
'					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*2750/Session("currency_prefix"),0) & "</td>" & VbCrlf
'					strTransfer = strTransfer & "</tr>" & VbCrlf
'					
'					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
'					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""4122"" "&strAirportCheck4122&"></td>" & VbCrlf
'					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4122"),"airport4122",1)&"</td>" & VbCrlf
'					strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
'					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*4370/Session("currency_prefix"),0) & "</td>" & VbCrlf
'					strTransfer = strTransfer & "</tr>" & VbCrlf
'					
'					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
'					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""4123"" "&strAirportCheck4123&"></td>" & VbCrlf
'					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4123"),"airport4123",1)&"</td>" & VbCrlf
'					strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 8-10 People)</font></td>" & VbCrlf
'					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*4300/Session("currency_prefix"),0) & "</td>" & VbCrlf
'					strTransfer = strTransfer & "</tr>" & VbCrlf
'					
'					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
'					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""4124"" "&strAirportCheck4124&"></td>" & VbCrlf
'					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport4124"),"airport4124",1)&"</td>" & VbCrlf
'					strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 8-10 People)</font></td>" & VbCrlf
'					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*5200/Session("currency_prefix"),0) & "</td>" & VbCrlf
'					strTransfer = strTransfer & "</tr>" & VbCrlf
'		
		Case 34 'Koh Samui
		
					IF function_array_check("22759",arrAirportID,2) Then
						strAirportCheck22759 = "checked"
					Else
						strAirportCheck22759 =""
					End IF
					
					IF function_array_check("22763",arrAirportID,2) Then
						strAirportCheck22763= "checked"
					Else
						strAirportCheck22763 =""
					End IF
						
					IF function_array_check("22764",arrAirportID,2) Then
						strAirportCheck22764 = "checked"
					Else
						strAirportCheck22764 =""
					End IF
					
					IF function_array_check("22765",arrAirportID,2) Then
						strAirportCheck22765= "checked"
					Else
						strAirportCheck22765 =""
					End IF
								
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""22759"" "&strAirportCheck22759&"></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport22759"),"airport22759",1)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*750/Session("currency_prefix"),0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
					
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""22764"" "&strAirportCheck22764&"></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport22764"),"airport22764",1)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*1400/Session("currency_prefix"),0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
					
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""22763"" "&strAirportCheck22763&"></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport22763"),"airport22763",1)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>One way airport transfer Per Van <br><font color=""green"">(Contain 8-10 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*900/Session("currency_prefix"),0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
					
					strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
					strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""22765"" "&strAirportCheck22765&"></td>" & VbCrlf
					strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport22765"),"airport22765",1)&"</td>" & VbCrlf
					strTransfer = strTransfer & "<td>Round trip airport transfer Per Van <br><font color=""green"">(Contain 8-10 People)</font></td>" & VbCrlf
					strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*1700/Session("currency_prefix"),0) & "</td>" & VbCrlf
					strTransfer = strTransfer & "</tr>" & VbCrlf
			
		
		Case 35 'Krabi
					IF function_array_check("3752",arrAirportID,2) Then
						strAirportCheck3752 = "checked"
					Else
						strAirportCheck3752 =""
					End IF
					
					IF function_array_check("3754",arrAirportID,2) Then
						strAirportCheck3754= "checked"
					Else
						strAirportCheck3754 =""
					End IF
					
					IF Cstr(intLocationID) <> "117" AND Cstr(intLocationID) <> "118" AND Cstr(intLocationID) <> "242" AND Cstr(intLocationID) <> "269" Then 'Exclude Koh Lanta and Phi Phi and Railai and Koh Ngai
					
						strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
						strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""3752"" "&strAirportCheck3752&"></td>" & VbCrlf
						strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport3752"),"airport3752",1)&"</td>" & VbCrlf
						strTransfer = strTransfer & "<td>One way airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
						strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*700/Session("currency_prefix"),0) & "</td>" & VbCrlf
						strTransfer = strTransfer & "</tr>" & VbCrlf
						
						strTransfer = strTransfer & "<tr align=""center"" bgcolor=""#FFFFFF"">" & VbCrlf
						strTransfer = strTransfer & "<td><input type=""checkbox"" name=""airport_id"" value=""3754"" "&strAirportCheck3754&"></td>" & VbCrlf
						strTransfer = strTransfer & "<td>"&function_gen_dropdown_number(1,5,Request("airport3754"),"airport3754",1)&"</td>" & VbCrlf
						strTransfer = strTransfer & "<td>Round trip airport transfer Per Car <br><font color=""green"">(Contain 2-3 People)</font></td>" & VbCrlf
						strTransfer = strTransfer & "<td>" & FormatNumber(intVatFactor*1400/Session("currency_prefix"),0) & "</td>" & VbCrlf
						strTransfer = strTransfer & "</tr>" & VbCrlf
					
					End IF
					
	END SELECT
	
	SELECT CASE intType
		Case 1
			sqlRate = "SELECT op.price_id,op.option_id,op.date_start,op.date_end,op.price,op.price_rack,po.title_en AS option_title,po.option_cat_id"
			sqlRate = sqlRate & " FROM tbl_product p,tbl_product_option po, tbl_option_price op"
			sqlRate = sqlRate & " WHERE p.product_id=po.product_id AND po.option_id=op.option_id AND po.status=1 AND po.option_cat_id IN (39,47) AND op.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND p.product_id=" & intProductID
			sqlRate = sqlRate & " ORDER BY op.date_end ASC ,op.price ASC"

			Set recRate = Server.CreateObject ("ADODB.Recordset")
			recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
				IF NOT recRate.EOF Then
					arrRate = recRate.GetRows()
					intRate = Ubound(arrRate,2)
					bolRate = True
				End IF
			recRate.Close
			Set recRate = Nothing 
			
			IF Request("extra_id")<>""Then
				strExtraID = Request("extra_id")
				strExtraID = Replace(strExtraID," ","")
				arrExtraID = Split(strExtraID,",")
			End IF
			
			IF Request("bed_id")<>""Then
				strBedID = Request("bed_id")
				strBedID = Replace(strBedID," ","")
				arrBedID = Split(strBedID,",")
			End IF
			
%>

<table width="70%" cellpadding="2"  cellspacing="1" bgcolor="#E4E4E4">
                    <tr bgcolor="#EDF5FE">
                      <td width="9%">&nbsp;</td>
                      <td width="19%" align="center"><font color="#346494">Quantity</font></td>
                      <td width="48%" align="center"><font color="#346494">Option</font></td>
                      <td width="24%" align="center"><font color="#346494">Rate<br>(<%=Session("currency_title")%>)</font></td>
                    </tr>
					<%
					IF bolRate Then
					For intCount=0 To intRate
						IF intCount MOD 2 = 0 Then
							strColor = "#FFFFFF"
						Else
							strColor = "#FCFCFC"
						End IF
						
						IF Cint(arrRate(7,intCount))=39 Then
							strExtraName = "bed"
						Else
							strExtraName = "extra"
						End IF
						
						
						'### SET CECHKED EXTRA ###
						IF function_array_check(arrRate(1,intCount),arrExtraID,2) Then
							strExtraCheck = "checked"
						Else
							strExtraCheck =""
						End IF
						'### SET CECHKED EXTRA###
						
						'### SET CECHKED EXTRA BED ###
						IF function_array_check(arrRate(1,intCount),arrBedID,2) Then
							strBedCheck = "checked"
						Else
							strBedCheck =""
						End IF
						'### SET CECHKED EXTRA BED ###
					%>
                    <tr align="center" bgcolor="<%=strColor%>">
                      <td><input type="checkbox" name="<%=strExtraName%>_id" value="<%=arrRate(1,intCount)%>" <%=strExtraCheck%> <%=strBedCheck%>></td>
                      <td>
					  <%=function_gen_dropdown_number(1,20,Request(strExtraName&arrRate(1,intCount)),strExtraName&arrRate(1,intCount),1)%>
					  </td>
                      <td><%=arrRate(6,intCount)%></td>
                      <td><%=FormatNumber(arrRate(4,intCount)*intVatFactor/Session("currency_prefix"),0)%></td>
                    </tr>
					<%
					Next
					End IF
					
					IF NOT bolTransfer Then
						Response.Write strTransfer
					End IF
					%>
                  </table>

<%
		Case 2
		
	End SELECT
	
END FUNCTION
%>