<%
Function functionGenXmlOrderStat()
Dim sqlAllDay
Dim sqlPaymentDay
Dim sqlCompleteDay
Dim arrAllDay
Dim arrPaymentDay
Dim arrCompleteDay
Dim intAllDay
Dim strAllDay
Dim strPaymentDay
Dim strCompleteDay
Dim strXml2
Dim intCount
Dim date_start
Dim date_end


	date_start="2009-8-1"
	date_end="2009-8-17"


sqlAllDay="select count(order_id) as total_order,cast(cast(year(date_submit) as varchar)+'-'+cast(month(date_submit) as varchar)+'-'+cast(day(date_submit) as varchar) as datetime) as date_submit"
sqlAllDay=sqlAllDay&" from tbl_order where date_submit between '"&date_start&"' and '"&date_end&"' and (full_name NOT  LIKE '%test%' and email NOT LIKE '%hotels2thailand%')"
sqlAllDay=sqlAllDay&" group by cast(cast(year(date_submit) as varchar)+'-'+cast(month(date_submit) as varchar)+'-'+cast(day(date_submit) as varchar) as datetime)"

sqlPaymentDay="select count(order_id) as total_order,cast(cast(year(date_submit) as varchar)+'-'+cast(month(date_submit) as varchar)+'-'+cast(day(date_submit) as varchar) as datetime) as date_submit"
sqlPaymentDay=sqlPaymentDay&" from tbl_order where date_submit between  '"&date_start&"' and '"&date_end&"' and (full_name NOT  LIKE '%test%' and email NOT LIKE '%hotels2thailand%') and confirm_payment=1"
sqlPaymentDay=sqlPaymentDay&" group by cast(cast(year(date_submit) as varchar)+'-'+cast(month(date_submit) as varchar)+'-'+cast(day(date_submit) as varchar) as datetime)"

sqlCompleteDay="select count(order_id) as total_order,cast(cast(year(date_submit) as varchar)+'-'+cast(month(date_submit) as varchar)+'-'+cast(day(date_submit) as varchar) as datetime) as date_submit"
sqlCompleteDay=sqlCompleteDay&" from tbl_order where date_submit between  '"&date_start&"' and '"&date_end&"' and (full_name NOT  LIKE '%test%' and email NOT LIKE '%hotels2thailand%') and confirm_payment=1 and confirm_open=1 and status_id IN (1,2,3,4,5,6,7,14,15,16,19,23,24,25)"
sqlCompleteDay=sqlCompleteDay&" group by cast(cast(year(date_submit) as varchar)+'-'+cast(month(date_submit) as varchar)+'-'+cast(day(date_submit) as varchar) as datetime) "


arrAllDay=getArray(sqlAllDay)
arrPaymentDay=getArray(sqlPaymentDay)
arrCompleteDay=getArray(sqlCompleteDay)

strXml2="<graph xaxisname='Continent' yaxisname='Export' hovercapbg='DEDEBE' hovercapborder='889E6D' rotateNames='0' yAxisMaxValue='100' numdivlines='9' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='0' showAlternateHGridColor='1' AlternateHGridAlpha='30' AlternateHGridColor='CCCCCC' caption='Global Export' subcaption='In Millions Tonnes per annum pr Hectare' >"&vbcrlf
strXml2=strXml2&"<categories font='Arial' fontSize='11' fontColor='000000'>"&vbcrlf
For intCount=1 to 17
	strXml2=strXml2&"<category name='"&intCount&"' hoverText='"&intCount&"'/>"&vbcrlf
Next
strXml2=strXml2&"</categories>"&vbcrlf
strAllDay="<dataset seriesname='All' color='FDC12E'>"&vbcrlf
strPaymentDay="<dataset seriesname='Payment' color='56B9F9'>"&vbcrlf
strCompleteDay=" <dataset seriesname='Complete' color='C9198D'>"&vbcrlf
For intAllDay=0 to Ubound(arrAllDay,2)
	strAllDay=strAllDay&"<set value='"&arrAllDay(0,intAllDay)&"' />"&vbcrlf
	strPaymentDay=strPaymentDay&"<set value='"&arrPaymentDay(0,intAllDay)&"' />"&vbcrlf
	strCompleteDay=strCompleteDay&"<set value='"&arrCompleteDay(0,intAllDay)&"' />"&vbcrlf
	'response.write arrAllDay(0,intAllDay)&"--"&arrPaymentDay(0,intAllDay)&"--"&arrCompleteDay(0,intAllDay)&"<br>"
Next
strAllDay=strAllDay&"</dataset>"
strPaymentDay=strPaymentDay&"</dataset>"&vbcrlf
strCompleteDay=strCompleteDay&"</dataset>"&vbcrlf

strXml2=strXml2&strAllDay&strPaymentDay&strCompleteDay&"</graph>"&vbcrlf
functionGenXmlOrderStat=strXml2
End Function
%>