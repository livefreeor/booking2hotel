<%
Function functionDisplayPriceDailyCalendar(myDate,product_id,strLink,strQuery)
Dim CurrentDay
Dim CurrentWeekDay
Dim temp
Dim FirstWeekday
Dim CurrentMonth
Dim CurrentYear
Dim prev_month 
Dim next_month
Dim DayCount
Dim bolDate
Dim Date_Check
Dim c_date
Dim i
Dim CurrentDay2
Dim LastDay
Dim FirstDay
Dim endDay

Dim intHotelID
intHotelID=session("hotel_id")
LastDay=function_lastday(month(mydate))
firstDay=dateserial(year(myDate),month(myDate),1)
endDay=dateserial(year(myDate),month(myDate),LastDay)

Dim sqlList
Dim rsList
Dim arrList

sqlList="select product_id,date_rate,price from tbl_price_daily where product_id="&product_id&" and date_rate between "&functionGenSqlDate(firstDay,1)&" and "&functionGenSqlDate(endDay,1)

Set rsList=conn.execute(sqlList)
IF Not rsList.Eof Then
	arrList=rsList.getRows()
End IF
rsList.close()
Set rsList=Nothing
'##########
'### Rate ###
		Dim sqlRate
		Dim recRate
		Dim arrRate
		Dim sqlWeekEnd
		Dim recWeekEnd
		Dim arrWeekEnd
		Dim sqlHoliday
		Dim recHoliday
		Dim arrHoliday
		Dim sqlHolidayLong
		Dim recHolidayLong
		Dim arrHolidayLong
		Dim sqlDaily
		Dim recDaily
		Dim arrDaily
		Dim arrAuto
		Dim arrAllot
		
		sqlRate = "SELECT pr.product_id,pr.date_start,pr.date_end,pr.price,pr.price_rack"
		sqlRate = sqlRate & " FROM tbl_product_price_period pr, tbl_product p"
		sqlRate = sqlRate & " WHERE pr.product_id=p.product_id AND p.hotel_id=" & intHotelID
		sqlRate = sqlRate & " And pr.date_end >= "&functionGenSqlDate(firstDay,1)
		'sqlRate = sqlRate & " AND ((pr.date_start<="&functionGenSqlDate(firstDay,1)&" AND pr.date_end>="&functionGenSqlDate(endDay,1)&") OR (pr.date_start<="&functionGenSqlDate(endDay,1)&" AND pr.date_end>="&functionGenSqlDate(endDay,1)&") OR (pr.date_start>="&functionGenSqlDate(firstDay,1)&" AND pr.date_end<="&functionGenSqlDate(endDay,1)&"))"
		sqlRate = sqlRate & " ORDER BY pr.product_id ASC, pr.date_start ASC"
		
		Set recRate = Server.CreateObject ("ADODB.Recordset")
		recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
			IF NOT recRate.EOF Then
				arrRate = recRate.GetRows()
			End IF
		recRate.Close
		Set recRate = Nothing 
		'### Rate ###
		
		'### WeekEnd ###
		sqlWeekEnd = "SELECT day_mon,day_tue,day_wed,day_thu,day_fri,day_sat,day_sun,day_holiday,day_holiday_long"
		sqlWeekEnd = sqlWeekEnd & " FROM tbl_hotel "
		sqlWeekEnd = sqlWeekEnd & " WHERE hotel_id=" & intHotelID
		
		Set recWeekEnd = Server.CreateObject ("ADODB.Recordset")
		recWeekEnd.Open SqlWeekEnd, Conn,adOpenStatic,adLockreadOnly
			IF NOT recWeekEnd.Eof Then
				arrWeekEnd = recWeekEnd.GetRows()
			End IF
		recWeekEnd.Close
		Set recWeekEnd = Nothing 
		'### WeekEnd ###
		
		'### Holiday ###
		sqlHoliday = "SELECT date_holiday "
		sqlHoliday = sqlHoliday & " FROM tbl_holiday"
		sqlHoliday = sqlHoliday & " WHERE hotel_id="&intHotelID&" AND date_holiday>="&functionGenSqlDate(firstDay,1)&" AND date_holiday<="&functionGenSqlDate(endDay,1)&""
		sqlHoliday = sqlHoliday & " ORDER BY date_holiday ASC"
		
		Set recHoliday = Server.CreateObject ("ADODB.Recordset")
		recHoliday.Open SqlHoliday, Conn,adOpenStatic,adLockreadOnly
			IF NOT recHoliday.EOF Then
				arrHoliday = recHoliday.GetRows()
			End IF
		recHoliday.Close
		Set recHoliday = Nothing 
		'### Holiday ###

		'### Long ###
		sqlHolidayLong = "SELECT date_holiday"
		sqlHolidayLong = sqlHolidayLong & " FROM tbl_holiday_long"
		sqlHolidayLong = sqlHolidayLong & " WHERE hotel_id="&intHotelID&" AND date_holiday>="&functionGenSqlDate(firstDay,1)&" AND date_holiday<="&functionGenSqlDate(endDay,1)&""
		sqlHolidayLong = sqlHolidayLong & " ORDER BY date_holiday ASC"
		
		Set recHolidayLong = Server.CreateObject ("ADODB.Recordset")
		recHolidayLong.Open SqlHolidayLong, Conn,adOpenStatic,adLockreadOnly
			IF NOT recHolidayLong.EOF Then
				arrHolidayLong = recHolidayLong.GetRows()
			End IF
		recHolidayLong.Close
		Set recHolidayLong = Nothing 
		'### Long ###
		
		'### Daily Rate ###
		sqlDaily = "SELECT pd.product_id,pd.date_rate,pd.price,pd.comment"
		sqlDaily = sqlDaily & " FROM tbl_price_daily pd, tbl_product p"
		sqlDaily = sqlDaily & " WHERE pd.product_id=p.product_id AND p.hotel_id="&intHotelID&" AND pd.date_rate>="&functionGenSqlDate(firstDay,1)&" AND pd.date_rate<="&functionGenSqlDate(endDay,1)&""
		sqlDaily = sqlDaily & " ORDER BY pd.date_rate ASC"
		
		Set recDaily = Server.CreateObject ("ADODB.Recordset")
		recDaily.Open SqlDaily, Conn,adOpenStatic,adLockreadOnly
			IF NOT recDaily.EOF Then
				arrDaily = recDaily.GetRows()
			End IF
		recDaily.Close
		Set recDaily = Nothing 
		'### Daily Rate ###
		
'##########
%>
<a name="calendar"></a>
<%
CurrentDay=mydate
CurrentDay=DatePart("d", currentday)
CurrentWeekday=Weekday(mydate)
temp=CurrentDay mod 7
FirstWeekday=CurrentWeekday-temp
If (FirstWeekday) >= 0 Then
   FirstWeekday=FirstWeekday+1
Else
   FirstWeekday=FirstWeekday+8
End If
CurrentMonth=Month(mydate)
CurrentYear=Year(mydate)
IF month(mydate)=1 Then
	'prev_month=12&"/1/"&year(dateadd("yyyy",-1,mydate))
	prev_month=dateserial(year(dateadd("yyyy",-1,mydate)),12,1)
Else
	'prev_month=month(dateadd("m",-1,mydate))&"/1/"&year(mydate)
	prev_month=dateserial(year(mydate),month(dateadd("m",-1,mydate)),1)
End IF
prev_month=strLink&"?change_month="&prev_month&"&"&strQuery&"#calendar"
IF month(mydate)=12 Then
	'next_month=1&"/1/"&year(dateadd("yyyy",1,mydate))
	next_month=dateserial(year(dateadd("yyyy",1,mydate)),1,1)
Else
	'next_month=month(dateadd("m",1,mydate))&"/1/"&year(mydate)
	next_month=dateserial(year(mydate),month(dateadd("m",1,mydate)),1)
End IF
next_month=strLink&"?change_month="&next_month&"&"&strQuery&"#calendar"
%>

<a name="calendar"></a>
<table border="0" cellspacing="1" cellpadding="3" bgcolor="#f2f2f2" width="160" id="tbl_calendar">
<tr  bgcolor=#DDDDFF>
   <td colspan="2"><a href="<%=prev_month%>">Prev</a></td><td align="center" colspan="3"><b><%=monthname(month(mydate))%>  <%=year(firstday)%></b></td><td align="right" colspan="2"><a href="<%=next_month%>">Next</a></td>
</tr>
<tr bgcolor="#A5EB57" style="color:#FFFFFF;font-weight:bold"> 
          <td align=center bgcolor="#D72F3C"> S</td>
          <td align=center bgcolor="#F4E518"> M</td>
          <td align=center bgcolor="#D871DA">T</td>
          <td align=center bgcolor="#49C91E"> W</td>
          <td align=center bgcolor="#EF823D"> T</td>
          <td align=center bgcolor="#509BEE"> F</td>
          <td align=center bgcolor="#8044C8"> S</td>
</tr>

<%
If CurrentMonth=12 Then 
   CurrentMonth=1
   CurrentYear=CurrentYear+1
Else CurrentMonth=CurrentMonth+1
End If

DayCount=1
CurrentDay2=DatePart("d", mydate)
Do While DayCount<=LastDay
Response.Write "<tr bgcolor='#ffffff'>"
For i=1 To 7
   If (i<>FirstWeekday) And (DayCount=1) Then
      Response.Write "<td></td>"
   Else
      If DayCount<=LastDay Then
		  
		  	c_date=DayCount
		  
         If DayCount=CurrentDay2 Then
            Response.Write "<td bgcolor=#DDFFDD align=right valign=""top"">" &c_date&"<br>"& functionCheckDailyDate(product_id,arrList,dateserial(year(myDate),month(myDate),c_date),arrRate,arrWeekend,arrHoliday,arrHolidayLong,arrDaily,arrAuto,arrAllot,1) & "</td>"
         Else
            Response.Write "<td  bgcolor=#FFFFFF align=right valign=""top"">"&c_date&"<br>"& functionCheckDailyDate(product_id,arrList,dateserial(year(myDate),month(myDate),c_date),arrRate,arrWeekend,arrHoliday,arrHolidayLong,arrDaily,arrAuto,arrAllot,1)& "</td>"
         End If
         DayCount=DayCount+1
      End If
   End If
Next
Response.Write "</tr>"
Loop
%>
<tr><td bgcolor="#EB5E55" height="2"></td><td bgcolor="#F8F08E"></td><td bgcolor="#F6B5DF"></td><td bgcolor="#9EF58C"></td><td bgcolor="#F2BB94"></td><td bgcolor="#87C2FE"></td><td bgcolor="#B485E3"></td></tr>
</table>

<%
End Function
%>