<%
FUNCTION function_display_rate(intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,intPriceMin,intPriceMax,intProductID,intNight,arrAllot,intType)
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
'			sqlRate = "SELECT op.date_start, op.date_end, o.title_en, op.price,op.price_rack,p.breakfast,o.show_option,o.option_id,p.files_name,p.destination_id,"
'			sqlRate = sqlRate & " (SELECT COUNT(sa.option_id) FROM tbl_allotment sa WHERE sa.status=1 AND sa.option_id=o.option_id AND sa.date_allotment>="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&") AS num_allot"
'			sqlRate = sqlRate & " FROM tbl_option_price op, tbl_product_option o, tbl_product p"
'			sqlRate = sqlRate & " WHERE op.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND p.product_id=o.product_id AND o.option_id=op.option_id AND o.status=1 AND o.option_cat_id=38 AND p.product_id=" & intProductID
'			sqlRate = sqlRate & " ORDER BY date_start ASC, date_end ASC, option_priority ASC, price ASC"
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
				strPrice = "<table width='100%' cellpadding='2' cellspacing='1' bgcolor='#E4E4E4'>" & VbCrlf
				strPrice = strPrice & "<tr bgcolor=""#EDF5FE"" class=""s2"" align=""center"">" & VbCrlf
				strPrice = strPrice & "<td rowspan=""2""><div align='center'><font color='#346494'><b>Period</b></font></div></td>" & VbCrlf
				strPrice = strPrice & "<td rowspan=""2""> <div align='center'><font color='#346494'><b>Room Types</b></font></div></td>" & VbCrlf
				strPrice = strPrice & "<td> <div align='center'><font color='#346494'><b>Our Rates</b></font></div></td>" & VbCrlf
				strPrice = strPrice & "<td> <div align='center'><font color='#346494'><b>Rack Rates</b></font></div></td>" & VbCrlf
				strPrice = strPrice & "<td rowspan=""2""><font color=""#346494""><b>Breakfast</font></b></td>" & VbCrlf
				strPrice = strPrice & "</tr>" & VbCrlf
				strPrice = strPrice & "<tr>" & VbCrlf
				strPrice = strPrice & "<td colspan=""2"" align=""center"" bgcolor=""#EDF5FE""><font color=""#990066"">("&Session("currency_title") & " <img src=""/images/flag_"&Session("currency_code")&".gif"">"&")</font></td>" & VbCrlf
				strPrice = strPrice & "</tr>" & VbCrlf

				For k=0 To Ubound(arrRate,2)
					
					'###Change option title to link ###
					strRoomType = function_gen_option_title(arrRate(7,k),arrRate(9,k),arrRate(8,k),arrRate(2,k),arrRate(6,k),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,arrRate(10,k),1)
					'###Change option title to link ###
				
					IF dateStartTmp=arrRate(0,k) AND dateEndTmp=arrRate(1,k) Then
						strPriceDetail = strPriceDetail & "<tr align='left' valign='middle' bgcolor="& strColor &"> " & VbCrlf
						strPriceDetail = strPriceDetail & "<td> <div align='left'>"& strRoomType &"</div></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td> <div align='center'><font color=""#990000""><b>"& function_display_price(arrRate(3,k),1) &"</b></font></div></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td><div align='center'>"& function_display_price(arrRate(4,k),2) &"</div></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">"& function_display_bol("<img src=""/images/ok.gif"">","-",arrRate(5,k),"",2) &"</td>" &VbCrlf
						strPriceDetail = strPriceDetail & "</tr>" & VbCrlf
						u = u + 1
					Else
						IF j MOD 2 = 0 Then
							strColor = """#FFFFFF"""
						Else
							strColor = """#F8FCF3"""
						End IF
						
						strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
						strPriceDetail = strPriceDetail & "<tr align='left' valign='middle' bgcolor="&strColor&"> " & VbCrlf
						strPriceDetail = strPriceDetail & "<td rowspan='intRowSpan'><div align='center'>"& function_date(arrRate(0,k),3) &" - " & function_date(arrRate(1,k),3) & "</div></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td> <div align='left'>"&strRoomType&"</div></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td> <div align='center'><font color=""#990000""><b>"& function_display_price(arrRate(3,k),1) &"</b></font></div></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td><div align='center'>"& function_display_price(arrRate(4,k),2) &"</div></td>" & VbCrlf
						strPriceDetail = strPriceDetail & "<td rowspan=""1"" class=""s1"" align=""center"">"& function_display_bol("<img src=""/images/ok.gif"">","-",arrRate(5,k),"",2) &"</td>" &VbCrlf
						strPriceDetail = strPriceDetail & "</tr>" & VbCrlf
							
						u = 1
						j = j + 1
						
						
					End IF			
						
					dateStartTmp = DateSerial(Year(arrRate(0,k)),Month(arrRate(0,k)),Day(arrRate(0,k)))
					dateEndTmp = DateSerial(Year(arrRate(1,k)),Month(arrRate(1,k)),Day(arrRate(1,k)))
						
				Next
					
				strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
				strPrice = strPrice & strPriceDetail
				'strPrice = strPrice & "</tr>" & VbCrlf
				strPrice = strPrice & "</table>" & VbCrlf
			
				function_display_rate = strPrice
			End IF
%>

<%
		Case 2 'Hotel Rate With date input
			dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
			dateCheckOut =DateSerial(intYearCheckOut,intMonthCheckOut,intDayCheckOut)
			intNight = DateDiff("d",dateCheckIn,dateCheckOut)
			dateCurrent = dateCheckIn
			intRateCount = 0

'			sqlType = "SELECT DISTINCT op.option_id,o.title_en,"
'			sqlType = sqlType & " (SELECT 'none') AS meal"
'			sqlType = sqlType & " ,o.option_priority,p.breakfast,o.show_option,p.files_name,p.destination_id"
'			sqlType = sqlType & " FROM tbl_option_price op, tbl_product_option o,tbl_product p"
'			sqlType = sqlType & " WHERE p.product_id=o.product_id AND op.option_id=o.option_id AND o.status=1 AND o.option_cat_id=38 AND " 
'			sqlType = sqlType & " (op.date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&")"
'			sqlType = sqlType & " AND o.option_id IN("
'			sqlType = sqlType & " SELECT sop.option_id"
'			sqlType = sqlType & " FROM tbl_option_price sop, tbl_product_option so,tbl_product sp "
'			sqlType = sqlType & " WHERE sp.product_id=so.product_id AND sop.option_id=so.option_id AND so.status=1 AND so.option_cat_id=38 AND "
'			sqlType = sqlType & " (sop.date_start<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&" AND sop.date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&")"
'			sqlType = sqlType & " AND sp.product_id=" & intProductID
'			sqlType = sqlType & ") "
'			sqlType = sqlType & " AND p.product_id=" & intProductID
'			sqlType = sqlType & " ORDER BY o.option_priority,op.option_id ASC"
			sqlType = "st_hotel_rate_list_type "&intProductID&","&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&","& function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)

			Set recType = Server.CreateObject ("ADODB.Recordset")
			recType.Open SqlType, Conn,adOpenStatic,adLockreadOnly
				IF NOT recType.EOF Then
					arrType = recType.GetRows()
					bolType = True
				Else
					bolType = False
				End IF
			recType.Close
			Set recType = Nothing 
			
			IF bolType Then 'IF have selected room type
			
'				sqlRate = "SELECT op.option_id,op.date_start,op.date_end,op.price,op.price_rack,price_own,sup_weekend,sup_holiday,sup_long "
'				sqlRate = sqlRate & " FROM tbl_product_option po,tbl_option_price op"
'				sqlRate = sqlRate & " WHERE po.option_id=op.option_id AND po.status=1 AND po.option_cat_id=38 AND ((op.date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&") OR (op.date_start<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&" AND date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&") OR (date_start>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND date_end<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&")) AND po.product_id=" & intProductID
				sqlRate = "st_hotel_rate_list_rate_2 "&intProductID&","&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&","&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)

				Set recRate = Server.CreateObject ("ADODB.Recordset")
				recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
					arrRate = recRate.GetRows()
				recRate.Close
				Set recRate = Nothing 
				
'				sqlPromotion = "SELECT promotion_id,pm.option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num"
'				sqlPromotion = sqlPromotion & " FROM tbl_promotion pm, tbl_product_option po"
'				sqlPromotion = sqlPromotion & " WHERE po.option_id=pm.option_id AND (date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&") AND pm.status=1 AND day_min<="& intNight &" AND po.product_id=" & intProductID
'				sqlPromotion = sqlPromotion & " ORDER BY po.option_id ASC, day_min DESC"
				sqlPromotion =  "st_hotel_rate_list_promotion "&intProductID&","&intNight&","&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&","&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)

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

				IF intNight<=7 Then 'Display As Day
%>		

<table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4" class="s">
<tr bgcolor="#EDF5FE" class="s2" align="center">
  <td rowspan="2"><font color="#346494"><b>Room Type</b><br></font>
    <font color="#990066">(<%=function_date(dateCheckIn,1)%> - <%=function_date(dateCheckOut,1)%>)</font></td>
  
 <%For intDayCount=1 To intNight%>
<td rowspan="2"><font color="#346494"><%=function_date(dateCurrent,2)%><br>(<%=function_date(dateCurrent,1)%>)</font></td>
<%
		dateCurrent = DateAdd("d",1,DateCurrent)
	Next
%>

<td><font color="#346494"><b>Average Rate</b></font></td>
<td><font color="#346494">Rack Rate</font></td>
<td rowspan="2"><font color="#346494" class="fsmall"><b>Breakfast</b></font></td>
</tr>
  <tr>
    <td colspan="2" align="center" bgcolor="#EDF5FE"><font color="#990066">(<%=Session("currency_title")%> <img src="/images/flag_<%=Session("currency_code")%>.gif">)</font></td>
  </tr>
<%
For intCount = 0 To Ubound(arrType,2)
	intPriceTemp = 0
	
	IF intCount MOD 2 =0 Then
		strColor = "#FFFFFF"
	Else
		strColor = "#F8FCF3"
	End IF
	
	strRoomType = function_gen_option_title(arrType(0,intCount),arrType(7,intCount),arrType(6,intCount),arrType(1,intCount),arrType(5,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,0,2)
%>
<tr bgcolor="<%=strColor%>" align="left">
  <td><%=strRoomType%></td>

 <%
 dateCurrent = dateCheckIn
 For intDayCount=1 To intNight
 	intPrice = function_gen_room_price(arrType(0,intCount),dateCurrent,arrRate,1)
	IF bolPromotion Then
		intPrice = function_get_price_promotion(intPrice,arrType(0,intCount),dateCheckIn,dateCheckOut,dateCurrent,0,arrPromotion,1)
	End IF
	
intPrice = function_display_price(intPrice,1)
 %>
<td align="center"><%=intPrice%></td>
<%
	dateCurrent = DateAdd("d",1,DateCurrent)
Next

IF bolPromotion Then
	intPriceAverage = function_gen_room_price_average_promotion(arrType(0,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrRate,1)
Else
	intPriceAverage = function_gen_room_price_average(arrType(0,intCount),dateCheckIn,dateCheckOut,arrRate,1)
End IF

intPriceAverage = function_display_price(intPriceAverage,1)

intPriceAverageRack = function_gen_room_price_average(arrType(0,intCount),dateCheckIn,dateCheckOut,arrRate,2)
intPriceAverageRack = function_display_price(intPriceAverageRack,2)
%>

<td align="center" class="m2"><font color="#990000"><b><%=intPriceAverage%></b></font></td>
<td align="center"><%=intPriceAverageRack%></td>
<td align="center"><%=function_display_bol("<img src=""/images/ok.gif"">","-",arrType(4,0),"",2)%></td>
</tr>
<%Next%>
</table>


<%		
		Else 'Display As Week
		
			intWeek = intNight/7
			IF intWeek>Round(intWeek) Then
				intWeek = Round(intWeek) + 1
			ElseIF intWeek<Round(intWeek) Then
				intWeek = Round(intWeek)
			End IF
%>

<table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4" class="s">
<tr bgcolor="#EDF5FE" class="s2" align="center">
  <td rowspan="2"><font color="#346494"><b>Room Type</b><br></font>
    <font color="#990066">(<%=function_date(dateCheckIn,1)%> - <%=function_date(dateCheckOut,1)%>)</font></td>
<td rowspan="2">&nbsp;</font></td>

<%
dateCurrent = dateCheckIn
For intDayCount=1 To 7
%>
<td rowspan="2"><font color="#346494"><%=function_date(dateCurrent,2)%></font></td>
<%
	dateCurrent = DateAdd("d",1,DateCurrent)
Next
%>

<td><font color="#346494"><b>Average Rate</b></font></td>
<td><font color="#346494">Rack Rate</font></td>
<td class="fsmall" rowspan="2"><font color="#346494"><b>Breakfast</b></font></td>
</tr>
  <tr>
    <td colspan="2" align="center" bgcolor="#EDF5FE"><font color="#990066">(<%=Session("currency_title")%> <img src="/images/flag_<%=Session("currency_code")%>.gif">)</font></td>
  </tr>
<%
For intCount=0 To Ubound(arrType,2)
	strRoomType = function_gen_option_title(arrType(0,intCount),arrType(7,intCount),arrType(6,intCount),arrType(1,intCount),arrType(5,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,0,2)
	dateCurrent = dateCheckIn 'Clear Date Current Buffer
	
	IF intCount MOD 2 =0 Then
		strColor = "#FFFFFF"
	Else
		strColor = "#F8FCF3"
	End IF
	
	For intWeekCount= 1 To intWeek 
		IF intWeekCount=1 Then 'First Week
%>

<tr bgcolor="<%=strColor%>" align="center">
  <td rowspan="<%=intWeek%>" align="left"><%=strRoomType%></td>
<td align="center"><font color="#990000">wk<%=intWeekCount%></font></td>
<%
For intDayCount=1 To 7
	 intPrice = function_gen_room_price(arrType(0,intCount),dateCurrent,arrRate,1)
	IF bolPromotion Then
		intPrice = function_get_price_promotion(intPrice,arrType(0,intCount),dateCheckIn,dateCheckOut,dateCurrent,0,arrPromotion,1)
	End IF
	intPrice = function_display_price(intPrice,1)
%>
<td align="center"><%=intPrice%></td>
<%
	dateCurrent = DateAdd("d",1,DateCurrent)
Next

IF bolPromotion Then
	intPriceAverage = function_gen_room_price_average_promotion(arrType(0,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrRate,1)
Else
	intPriceAverage = function_gen_room_price_average(arrType(0,intCount),dateCheckIn,dateCheckOut,arrRate,1)
End IF

intPriceAverage = function_display_price(intPriceAverage,1)

intPriceAverageRack = function_gen_room_price_average(arrType(0,intCount),dateCheckIn,dateCheckOut,arrRate,2)
intPriceAverageRack = function_display_price(intPriceAverageRack,2)
%>
<td align="center" class="m2" rowspan="<%=intWeek%>"><font color="#990000"><b><%=intPriceAverage%></b></font></td>
<td align="center" rowspan="<%=intWeek%>"><%=intPriceAverageRack%></td>
<td align="center" rowspan="<%=intWeek%>"><%=function_display_bol("<img src=""/images/ok.gif"">","-",arrType(4,0),"",2)%></td>
</tr>
<%
		Else 'Week more than 1
%>
<tr bgcolor="<%=strColor%>" align="center">
<td align="center"><font color="#990000">wk<%=intWeekCount%></font></td>
<%
For intDayCount=1 To 7
	IF (((intWeekCount-1)*7) + intDayCount) <= intNight Then
		intPrice = function_gen_room_price(arrType(0,intCount),dateCurrent,arrRate,1)
		IF bolPromotion Then
			intPrice = function_get_price_promotion(intPrice,arrType(0,intCount),dateCheckIn,dateCheckOut,dateCurrent,0,arrPromotion,1)
		End IF
	Else
		intPrice = "&nbsp;"
	End IF
	intPrice = function_display_price(intPrice,1)
%>
<td align="center"><%=intPrice%></td>
<%
	dateCurrent = DateAdd("d",1,DateCurrent)
Next
%>
</tr>

<%
		End IF 'Display week type (week1 or wek2>)
	Next 'Week Count
Next
%>
</table>

<%
		End IF 'Displat As Week or Day
		
End IF 'Selected Room Type
	END SELECT

END FUNCTION
%>