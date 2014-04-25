<%
FUNCTION function_display_rate_confirm(intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,intPriceMin,intPriceMax,intProductID,intNight,arrAllot,strOptionID,intType)
	
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
	
			dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
			dateCheckOut =DateSerial(intYearCheckOut,intMonthCheckOut,intDayCheckOut)
			intNight = DateDiff("d",dateCheckIn,dateCheckOut)
			dateCurrent = dateCheckIn
			intRateCount = 0
			
			sqlType = "SELECT DISTINCT op.option_id,o.title_en,"
			sqlType = sqlType & " ISNULL((SELECT TOP 1 so.title_en FROM tbl_product_option_category soc, tbl_product_option so, tbl_option_price sop  WHERE sop.option_id=so.option_id AND soc.option_cat_id=so.option_cat_id AND sop.price=0 AND soc.option_cat_id=40 AND so.product_id=o.product_id AND so.status=1 AND sop.date_end>="& function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)  &"),'none') AS meal"
			sqlType = sqlType & " ,o.option_priority,p.breakfast,o.show_option,p.files_name,p.destination_id,"
			sqlType = sqlType & " (SELECT COUNT(sa.option_id) FROM tbl_allotment sa WHERE sa.status=1 AND sa.option_id=o.option_id AND sa.date_allotment>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&") AS num_allot"
			sqlType = sqlType & " FROM tbl_option_price op, tbl_product_option o,tbl_product p"
			sqlType = sqlType & " WHERE p.product_id=o.product_id AND op.option_id=o.option_id AND o.status=1 AND o.option_cat_id=38 AND   ((op.date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&") OR (op.date_start<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&" AND date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&")) AND p.product_id=" & intProductID & " AND o.option_id IN ("&strOptionID&")"
			sqlType = sqlType & " ORDER BY o.option_priority,op.option_id ASC"

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
			
				sqlRate = "SELECT op.option_id,op.date_start,op.date_end,op.price,op.price_rack,op.price_own,sup_weekend,sup_holiday,sup_long "
				sqlRate = sqlRate & " FROM tbl_product_option po,tbl_option_price op"
				sqlRate = sqlRate & " WHERE po.option_id=op.option_id AND po.status=1 AND po.option_cat_id=38 AND ((op.date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&") OR (op.date_start<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&" AND date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&")OR (op.date_start>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&")) AND po.product_id=" & intProductID
				
				Set recRate = Server.CreateObject ("ADODB.Recordset")
				recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
					arrRate = recRate.GetRows()
				recRate.Close
				Set recRate = Nothing 
				
				sqlPromotion = "SELECT promotion_id,pm.option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num"
				sqlPromotion = sqlPromotion & " FROM tbl_promotion pm, tbl_product_option po"
				sqlPromotion = sqlPromotion & " WHERE po.option_id=pm.option_id AND (date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&") AND pm.status=1 AND day_min<="& intNight &" AND po.product_id=" & intProductID
				sqlPromotion = sqlPromotion & " ORDER BY po.option_id ASC, day_min DESC"
				
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
  <td rowspan="2"><font color="#346494"><b>Room Type</b><br>
    (<%=function_date(dateCheckIn,1)%> - <%=function_date(dateCheckOut,1)%>)</font></td>
  
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
	
	strRoomType = function_gen_option_title(arrType(0,intCount),arrType(7,intCount),arrType(6,intCount),arrType(1,intCount),arrType(5,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,arrType(8,intCount),2)
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
  <td rowspan="2"><font color="#346494"><b>Room Type</b><br>
    (<%=function_date(dateCheckIn,1)%> - <%=function_date(dateCheckOut,1)%>)</font></td>
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
	strRoomType = function_gen_option_title(arrType(0,intCount),arrType(7,intCount),arrType(6,intCount),arrType(1,intCount),arrType(5,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,arrType(8,intCount),2)
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
END FUNCTION
%>