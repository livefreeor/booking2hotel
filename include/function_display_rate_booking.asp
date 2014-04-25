<%
FUNCTION function_display_rate_booking(intProductID,intDayCheckIn,intMonthCheckIn,intYearCheckIn,intDayCheckOut,intMonthCheckOut,intYearCheckOut,arrAllot,intType)

	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim bolRate
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
	Dim intNight
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
	Dim strRoomType
	Dim strOptionID
	Dim arrOptionID
	Dim strOptionCheck
	
	Dim intPriceDiscount '### Modify for disocunt on night ###
	
	IF Request("option_id")<>"" Then
		strOptionID = Request("option_id")
		strOptionID = Replace(strOptionID," ","")
		arrOptionID = Split(strOptionID,",")
	End IF
	
	SELECT CASE intType
	
		Case 1  'Rate Without input date (Separate By Type)
		
'			sqlRate = "SELECT op.price_id,op.option_id,op.date_start,op.date_end,op.price,op.price_rack,po.title_en AS option_title,p.breakfast,po.show_option,p.files_name,p.destination_id,"
'			sqlRate = sqlRate & " (SELECT COUNT(sa.option_id) FROM tbl_allotment sa WHERE sa.status=1 AND sa.option_id=po.option_id AND sa.date_allotment>="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&") AS num_allot,"
'			sqlRate = sqlRate & " price_sup_id"
'			sqlRate = sqlRate & " FROM tbl_product p,tbl_product_option po, tbl_option_price op"
'			sqlRate = sqlRate & " WHERE p.product_id=po.product_id AND po.option_id=op.option_id AND po.status=1 AND po.option_cat_id=38 AND op.date_end>="& function_date_sql(Day(Date),Month(Date),Year(Date),1) &" AND p.product_id=" & intProductID
'			sqlRate = sqlRate & " ORDER BY po.option_priority ASC, op.option_id ASC, op.date_start ASC"
			sqlRate = "st_hotel_rate_booking_rate_1 "&intProductID & "," & function_date_sql(Day(Date),Month(Date),Year(Date),1)

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
			
			IF BolRate Then
			
			'### Check Number of Room Type And Generate Room Type Array ###
			For intCount=0 To intRate
				IF intOptionID <> arrRate(1,intCount) Then
					intRoomType = intRoomType + 1
				End IF
				intOptionID = arrRate(1,intCount)
			Next

			ReDim arrRoomType(intRoomType,2)
			intNumType = 1
			intRoomTypeTemp = 0
			
			For intCount=0 To intRate
				IF intOptionID <> arrRate(1,intCount) Then
					intRoomTypeTemp = intRoomTypeTemp + 1
					intNumType = 1
				Else
					intNumType = intNumType + 1
				End IF

				intOptionID = arrRate(1,intCount)
				arrRoomType(intRoomTypeTemp,1) = intOptionID
				arrRoomType(intRoomTypeTemp,2) = intNumType
			Next
			'### Check Number of Room Type And Generate Room Type Array ###
			
%>
<div align="left">
<img src="/images/icon_hot.gif"> <a href="/coupon/CouponCardShow.aspx" target="_blank"><strong>Free Discount Coupon</strong></a><br>
<font color="red">*</font> <font color="green"><strong>Hotel rates per room per night are excluded of tax and service charge.<br>
<font color="red">*</font> Single room = One person, Twin/Double room = Two persons, Triple room = Three persons, including extra bed.</strong></font><br></div>
<table width="98%" cellpadding="2"  cellspacing="1" bgcolor="#E4E4E4">
  <tr align="center" bgcolor="#EDF5FE">
    <td width="5%" rowspan="2" bgcolor="#EDF5FE">&nbsp;</td>
    <td width="10%" rowspan="2" bgcolor="#EDF5FE"><font color="#346494"><b>Rooms</b></font></td>
    <td width="31%" rowspan="2" bgcolor="#EDF5FE"><font color="#346494"><b>Room Type</b></font></td>
    <td width="20%" rowspan="2" bgcolor="#EDF5FE"><font color="#346494"><b>Period</b></font></td>
    <td width="11%" bgcolor="#EDF5FE"><font color="#346494"><b>Our Rate</b></font> </td>
    <td width="12%" bgcolor="#EDF5FE"><font color="#346494"><b>Rack Rate</b></font> </td>
    <td width="4%" rowspan="2" bgcolor="#EDF5FE" class="fsmall"><font color="#346494"><b>Breakfast</b></font></td>
  </tr>
  <tr>
    <td colspan="2" align="center" bgcolor="#EDF5FE"><font color="#990066">(<%=Session("currency_title")%> <img src="/images/flag_<%=Session("currency_code")%>.gif">) </font></td>
  </tr>
  
 <%
 
	 For intTypeCount=0 To intRoomType
	
		intRowType =0
		IF intTypeCount MOD 2 = 0 Then
			strColor = "#FFFFFF"
		Else
			strColor = "#F8FCF3"
		End IF
	
		For intCount=0 To intRate
			
			'###Change option title to link ###
			strRoomType = function_gen_option_title(arrRate(1,intCount),arrRate(10,intCount),arrRate(9,intCount),arrRate(6,intCount),arrRate(8,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,arrRate(10,intCount),1)
			'###Change option title to link ###
	
			IF Cstr(arrRoomType(intTypeCount,1))=Cstr(arrRate(1,intCount)) Then
				intRowType = intRowType + 1
				intRowSpan = arrRoomType(intTypeCount,2)
				
				intPriceDiscount = arrRate(4,intCount)
				
				'### Temporary discount on night time ###
				IF Hour(dateCurrentConstant)>=20 OR Hour(dateCurrentConstant)<8 Then
				
					IF intPriceDiscount>=500 AND intPriceDiscount<700 Then
						intPriceDiscount = intPriceDiscount-15
					End IF
					
					IF intPriceDiscount>=700 AND intPriceDiscount<900 Then
						intPriceDiscount = intPriceDiscount-20
					End IF
					
					IF intPriceDiscount>=900 AND intPriceDiscount<1100 Then
						intPriceDiscount = intPriceDiscount-24
					End IF
					
					IF intPriceDiscount>=1100 AND intPriceDiscount<1300 Then
						intPriceDiscount = intPriceDiscount-28
					End IF
					
					IF intPriceDiscount>=1300 AND intPriceDiscount<1500 Then
						intPriceDiscount = intPriceDiscount-35
					End IF
					
					IF intPriceDiscount>=1500 AND intPriceDiscount<1700 Then
						intPriceDiscount = intPriceDiscount-60
					End IF
					
					IF intPriceDiscount>=1700 AND intPriceDiscount<1900 Then
						intPriceDiscount = intPriceDiscount-68
					End IF
					
					IF intPriceDiscount>=1900 AND intPriceDiscount<2100 Then
						intPriceDiscount = intPriceDiscount-76
					End IF
					
					IF intPriceDiscount>=2100 AND intPriceDiscount<2300 Then
						intPriceDiscount = intPriceDiscount-84
					End IF
					
					IF intPriceDiscount>=2300 AND intPriceDiscount<2500 Then
						intPriceDiscount = intPriceDiscount-92
					End IF
					
					IF intPriceDiscount>=2500 AND intPriceDiscount<2700 Then
						intPriceDiscount = intPriceDiscount-100
					End IF
					
					IF intPriceDiscount>=2700 AND intPriceDiscount<2900 Then
						intPriceDiscount = intPriceDiscount-108
					End IF
					
					IF intPriceDiscount>=2900 AND intPriceDiscount<3200 Then
						intPriceDiscount = intPriceDiscount-116
					End IF
					
					IF intPriceDiscount>=3200 AND intPriceDiscount<3500 Then
						intPriceDiscount = intPriceDiscount-128
					End IF
					
					IF intPriceDiscount>=3500 AND intPriceDiscount<3800 Then
						intPriceDiscount = intPriceDiscount-140
					End IF
					
					IF intPriceDiscount>=3800 AND intPriceDiscount<4100 Then
						intPriceDiscount = intPriceDiscount-152
					End IF
					
					IF intPriceDiscount>=4100 Then
						intPriceDiscount = intPriceDiscount-164
					End IF
				
				END IF
				'### Temporary discount on night time ###
				
				IF intRowType=1 Then
 %>
  <tr bgcolor="<%=strColor%>" class="m">
    <td rowspan="<%=intRowSpan%>" align="center"><input type="checkbox" name="option_id" value="<%=arrRate(1,intCount)%>"></td>
    <td rowspan="<%=intRowSpan%>" align="center">
		<%=function_gen_dropdown_number(1,20,Request("qty"&arrRate(1,intCount)),"qty"&arrRate(1,intCount),1)%>
	</td>
    <td rowspan="<%=intRowSpan%>"><%=strRoomType%></td>
    <td align="center"><%=function_date(arrRate(2,intCount),3)%> - <%=function_date(arrRate(3,intCount),3)%></td>
    <td align="center"><font color="#990000"><b><%=function_display_price(intPriceDiscount,1)%></b></font></td>
    <td align="center"><%=function_display_price(arrRate(5,intCount),2)%></td>
    <td rowspan="<%=intRowSpan%>" align="center"><%=function_display_bol("<img src=""/images/ok.gif"">","-",arrRate(7,intCount),"",2)%></td>
  </tr>
<%
	Else 'Row Type
%>
  <tr bgcolor="<%=strColor%>" class="m">
    <td align="center"><%=function_date(arrRate(2,intCount),3)%> - <%=function_date(arrRate(3,intCount),3)%></td>
    <td align="center"><font color="#990000"><b><%=function_display_price(intPriceDiscount,1)%></b></font></td>
    <td align="center"><%=function_display_price(arrRate(5,intCount),2)%></td>
  </tr>
	
<%
			End IF 'Row Type
		End IF
	Next 'All
Next 'RoomType
%>
</table>
<%
	IF Cint(arrRate(12,0))<>1 Then 'Show weekend and holiday rate comment
		Response.Write "<br /><div aling=""left""><font color=""red"">*</font> <font color=""#FF6633"">This hotel has supplemental charge for public holidays and long weekend. Please Click at ""Click Here To Change Your Date"" to view correct rate upon your arrival period.</font></div>"
	End IF

Else 'BolRate
Response.Write "<p><font color=""red""><b>Sorry, We don't have rate for this hotel.</b></font><br><br> Please contact us at <a href=""mailto:support@hotels2thailand.com"">support@hotels2thailand.com</a>"		
Response.Write "<br><br>If you want to change to other hotel, please click below.<br><br><a href=""http://www.hotels2thailand.com""><img src=""/images/icon_back_home.jpg"" style=""border:0px;""></a></p>"

End IF
%>

<%
		Case 2 'Rate With date input
			dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
			dateCheckOut =DateSerial(intYearCheckOut,intMonthCheckOut,intDayCheckOut)
			intNight = DateDiff("d",dateCheckIn,dateCheckOut)
			dateCurrent = dateCheckIn
			intRateCount = 0

			sqlType = "SELECT DISTINCT op.option_id,o.title_en,"
			sqlType = sqlType & " (SELECT 'none') AS meal"
			sqlType = sqlType & " ,o.option_priority,p.breakfast,o.show_option,p.files_name,p.destination_id"
			sqlType = sqlType & " FROM tbl_option_price op, tbl_product_option o,tbl_product p"
			sqlType = sqlType & " WHERE p.product_id=o.product_id AND op.option_id=o.option_id AND o.status=1 AND o.option_cat_id=38 AND " 
			sqlType = sqlType & " (op.date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&")"
			sqlType = sqlType & " AND o.option_id IN("
			sqlType = sqlType & " SELECT sop.option_id"
			sqlType = sqlType & " FROM tbl_option_price sop, tbl_product_option so,tbl_product sp "
			sqlType = sqlType & " WHERE sp.product_id=so.product_id AND sop.option_id=so.option_id AND so.status=1 AND so.option_cat_id=38 AND "
			sqlType = sqlType & " (sop.date_start<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&" AND sop.date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&")"
			sqlType = sqlType & " AND sp.product_id=" & intProductID
			sqlType = sqlType & ") "
			sqlType = sqlType & " AND p.product_id=" & intProductID
			sqlType = sqlType & " ORDER BY o.option_priority,op.option_id ASC"
			'sqlType = "st_hotel_rate_booking_type "&intProductID&","&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&"," & function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)

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
			
'				sqlRate = "SELECT op.option_id,op.date_start,op.date_end,op.price,op.price_rack,price_own,sup_weekend,sup_holiday,sup_long"
'				sqlRate = sqlRate & " FROM tbl_product_option po,tbl_option_price op"
'				sqlRate = sqlRate & " WHERE po.option_id=op.option_id AND po.status=1 AND po.option_cat_id=38 AND ((op.date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&") OR (op.date_start<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&" AND date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&")OR (op.date_start>="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND op.date_end<="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&")) AND po.product_id=" & intProductID
				sqlRate = "st_hotel_rate_booking_rate_2 "&intProductID&","&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&","&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)

				Set recRate = Server.CreateObject ("ADODB.Recordset")
				recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
					arrRate = recRate.GetRows()
				recRate.Close
				Set recRate = Nothing 
				
'				sqlPromotion = "SELECT promotion_id,pm.option_id,type_id,qty_min,day_min,day_discount_start,day_discount_num,discount,free_option_id,free_option_qty,title,detail,day_advance_num"
'				sqlPromotion = sqlPromotion & " FROM tbl_promotion pm, tbl_product_option po"
'				sqlPromotion = sqlPromotion & " WHERE po.option_id=pm.option_id AND (date_start<="&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&" AND date_end>="&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)&") AND pm.status=1 AND day_min<="& intNight &" AND po.product_id=" & intProductID
'				sqlPromotion = sqlPromotion & " ORDER BY po.option_id ASC, day_min DESC"
				sqlPromotion = "st_hotel_rate_booking_promotion "&intProductID&","&intNight&","&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&","&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)
				'sqlPromotion = "st_hotel_rate_booking_promotion "&intProductID&",3,"&function_date_sql(intDayCheckin,intMonthCheckin,intYearCheckin,1)&","&function_date_sql(intDayCheckOut,intMonthCheckOut,intYearCheckOut,1)
				'response.Write(sqlPromotion)
				'response.End()
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
<div align="left">
<img src="/images/icon_hot.gif"> <a href="/coupon/CouponCardShow.aspx" target="_blank"><strong>Free Discount Coupon</strong></a><br>
<font color="red">*</font> <font color="green"><strong>Hotel rates per room per night are excluded of tax and service charge.<br>
<font color="red">*</font> Single room = One person, Twin/Double room = Two persons, Triple room = Three persons, including extra bed.</strong></font><br></div>
<table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4" class="s">
<tr bgcolor="#EDF5FE" class="s2" align="center">
  <td rowspan="2">&nbsp;</td>
  <td rowspan="2"><font color="#346494"><b>Rooms</b></font></td>
  <td rowspan="2"><font color="#346494"><b>Room Type</b><br><font color="#990066">(<%=function_date(dateCheckIn,1)%> - <%=function_date(dateCheckOut,1)%>)</font></font></td>
  
 <%For intDayCount=1 To intNight%>
<td rowspan="2"><font color="#346494"><b><%=function_date(dateCurrent,2)%></b><br>(<%=function_date(dateCurrent,1)%>)</font></td>
<%
		dateCurrent = DateAdd("d",1,DateCurrent)
	Next
%>

<td><font color="#346494"><b>Average Rate</b></font></td>
<td><font color="#346494"><b>Rack Rate</b></font></td>
<td class="fsmall" rowspan="2"><font color="#346494"><b>Breakfast</b></font></td>
</tr>
  <tr>
    <td colspan="2" align="center" bgcolor="#EDF5FE"><font color="#990066">(<%=Session("currency_title")%> <img src="/images/flag_<%=Session("currency_code")%>.gif">) </font></td>
  </tr>
<%
For intCount = 0 To Ubound(arrType,2)

	IF intCount MOD 2 = 0 Then
		strColor = "#FFFFFF"
	Else
		strColor = "#F8FCF3"
	End IF
	
	strRoomType = function_gen_option_title(arrType(0,intCount),arrType(7,intCount),arrType(6,intCount),arrType(1,intCount),arrType(5,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,0,2)
	
	'### SET CECHKED OPTION ###
	IF function_array_check(arrType(0,intCount),arrOptionID,2) Then
		strOptionCheck = "checked"
	Else
		strOptionCheck =""
	End IF
	'### SET CECHKED OPTION ###
	
	intPriceTemp = 0
%>
<tr bgcolor="<%=strColor%>" align="center">
  <td><input type="checkbox" name="option_id" value="<%=arrType(0,intCount)%>" <%=strOptionCheck%>></td>
  <td><%=function_gen_dropdown_number(1,20,Request("qty"&arrType(0,intCount)),"qty"&arrType(0,intCount),1)%></td>
<td align="left"><%=strRoomType%></td>

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
<div align="left">
<img src="/images/icon_hot.gif"> <a href="/coupon/CouponCardShow.aspx" target="_blank"><strong>Free Discount Coupon</strong></a><br>
<font color="red">*</font> <font color="green"><strong>Hotel rates per room per night are excluded of tax and service charge.<br>
<font color="red">*</font> Single room = One person, Twin/Double room = Two persons, Triple room = Three persons, including extra bed.</strong></font><br></div>
<table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4" class="s">
<tr bgcolor="#EDF5FE" class="s2" align="center">
  <td rowspan="2">&nbsp;</td>
  <td rowspan="2"><font color="#346494"><b>Rooms</b></font></td>
  <td rowspan="2"><font color="#346494"><b>Room Type</b></font><br><font color="#990066">(<%=function_date(dateCheckIn,1)%> - <%=function_date(dateCheckOut,1)%>)</font></td>
<td rowspan="2">&nbsp;</td>

<%
dateCurrent = dateCheckIn
For intDayCount=1 To 7
%>
<td rowspan="2"><font color="#346494"><b><%=function_date(dateCurrent,2)%></b></font></td>
<%
	dateCurrent = DateAdd("d",1,DateCurrent)
Next
%>

<td><font color="#346494"><b>Average Rate</b></font></td>
<td><font color="#346494"><b>Rack Rate</b></font></td>
<td class="fsmall" rowspan="2"><font color="#346494"><b>Breakfast</b></font></td>
</tr>
</tr>
  <tr>
    <td colspan="2" align="center" bgcolor="#EDF5FE"><font color="#990066">(<%=Session("currency_title")%> <img src="/images/flag_<%=Session("currency_code")%>.gif">) </font></td>
  </tr>
<%
For intCount=0 To Ubound(arrType,2)
	strRoomType = function_gen_option_title(arrType(0,intCount),arrType(7,intCount),arrType(6,intCount),arrType(1,intCount),arrType(5,intCount),dateCheckIn,dateCheckOut,1,arrPromotion,arrAllot,0,2)
	dateCurrent = dateCheckIn 'Clear Date Current Buffer
	
	IF intCount MOD 2 = 0 Then
		strColor = "#FFFFFF"
	Else
		strColor = "#F8FCF3"
	End IF
	
	For intWeekCount= 1 To intWeek 
		IF intWeekCount=1 Then 'First Week
		
		'### SET CECHKED OPTION ###
		IF function_array_check(arrType(0,intCount),arrOptionID,2) Then
			strOptionCheck = "checked"
		Else
			strOptionCheck =""
		End IF
		'### SET CECHKED OPTION ###
%>

<tr bgcolor="<%=strColor%>" align="center">
  <td rowspan="<%=intWeek%>"><input type="checkbox" name="option_id" value="<%=arrType(0,intCount)%>" <%=strOptionCheck%>></td>
  <td rowspan="<%=intWeek%>"><%=function_gen_dropdown_number(1,20,Request("qty"&arrType(0,intCount)),"qty"&arrType(0,intCount),1)%></td>
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
<td align="center" rowspan="<%=intWeek%>"><b><%=intPriceAverageRack%></b></td>
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
		intPrice = function_display_price(intPrice,1)
	Else
		intPrice = "&nbsp;"
	End IF
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
Else
Response.Write "<p><font color=""red""><b>Sorry, We don't have rate for this hotel.</b></font><br><br> Please contact us at <a href=""mailto:support@hotels2thailand.com"">support@hotels2thailand.com</a>"		
Response.Write "<br><br>If you want to change to other hotel, please click below.<br><br><a href=""http://www.hotels2thailand.com""><img src=""/images/icon_back_home.jpg"" style=""border:0px;""></a></p>"

End IF 'Selected Room Type
	END SELECT

END FUNCTION
%>