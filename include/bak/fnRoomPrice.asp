<%
FUNCTION fnRoomPrice(intProductID)

	Dim sqlRoom, recRoom, arrRoom, strRoom, strPrice, strPriceDetail, strColor
	Dim k, l, m, j, u
	Dim dateStartTmp, dateEndTmp
	Dim strBreakfast
	Dim sqlFreeOption, recFreeOption, strFreeOption
	Dim sqlOptionPrice, recOptionPrice, strOptionPrice
	
	strBreakfast = "Our rate is INCLUDED Goverment TAX, Service Charge and Breakfast."
	
	dateStartTmp = DateSerial("2000",1,1)
	dateEndTmp = DateSerial("2000",1,1)

	sqlRoom = "SELECT op.date_start, op.date_end, o.title_en, op.price"
	sqlRoom = sqlRoom & " FROM tbl_option_price op, tbl_product_option o, tbl_product p"
	sqlRoom = sqlRoom & " WHERE op.date_end>="& fnConvertRawSqlDate(Date) &" AND p.product_id=o.product_id AND o.option_id=op.option_id AND o.status=1 AND o.option_cat_id=38 AND p.product_id=" & intProductID
	sqlRoom = sqlRoom & " ORDER BY date_start ASC, date_end ASC, price ASC"

	Set recRoom= Server.CreateObject ("ADODB.Recordset")
	recRoom.Open sqlRoom, Conn,adOpenForwardOnly,adLockreadOnly
		IF NOT recRoom.EOF Then
			arrRoom = recRoom.GetRows
			strRoom = "yes"
		End IF
	recRoom.Close
	Set recRoom = NoThing

'##### Room Price #####
	IF strRoom="yes" Then
	
		strPrice = "<font color='#0033FF' class='m'><br><b>Our special <font color='#FF0000' >NETT</font> rates: </b></font>"
		strPrice = strPrice & "<table width='588' cellpadding='2' cellspacing='1' bgcolor='#CCE0FF'>" & VbCrlf
		strPrice = strPrice & "<tr align='left' valign='middle' bgcolor='#C4DBFF'>" & VbCrlf
		strPrice = strPrice & "<td width='200' class='s1'><div align='center'><b><font color='#0066FF'>Period</font></b></div></td>" & VbCrlf
		strPrice = strPrice & "<td width='268' height='24' class='s1'> <div align='center'><font color='#0066FF'><b>Room Types</b></font></div></td>" & VbCrlf
		strPrice = strPrice & "<td width='112' class='s1'> <div align='center'><b><font color='#0066FF'>Rates</font> <br><font color=""#0066FF"">("& strCurrencyTitle &")</font></b></div></td>" & VbCrlf
		strPrice = strPrice & "</tr>" & VbCrlf
	
		For k=0 To Ubound(arrRoom,2)
		
			IF dateStartTmp=arrRoom(0,k) AND dateEndTmp=arrRoom(1,k) Then
				strPriceDetail = strPriceDetail & "<tr align='left' valign='middle' bgcolor='"& strColor &"'> " & VbCrlf
				strPriceDetail = strPriceDetail & "<td class='m'> <div align='left'>"& arrRoom(2,k) &"</div></td>" & VbCrlf
				strPriceDetail = strPriceDetail & "<td class='m'> <div align='center'><b>"& fnCurrencyDisplay(arrRoom(3,k)) &"</b></div></td>" & VbCrlf
				strPriceDetail = strPriceDetail & "</tr>" & VbCrlf
				u = u + 1
			Else
			
				IF j MOD 2 = 0 Then
					strColor = "#FFFFFF"
				Else
					strColor = "#BFFFFF"
				End IF
			
				strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
				strPriceDetail = strPriceDetail & "<tr align='left' valign='middle' bgcolor='"&strColor&"'> " & VbCrlf
				strPriceDetail = strPriceDetail & "<td rowspan='intRowSpan' class='m'><div align='center'>"& convert_date(arrRoom(0,k),16) &" - " & convert_date(arrRoom(1,k),16) & "</div></td>" & VbCrlf
				strPriceDetail = strPriceDetail & "<td class='m'> <div align='left'>"& arrRoom(2,k)&"</div></td>" & VbCrlf
				strPriceDetail = strPriceDetail & "<td class='m'> <div align='center'><b>"& fnCurrencyDisplay(arrRoom(3,k)) &"</b></div></td>" & VbCrlf
				strPriceDetail = strPriceDetail & "</tr>" & VbCrlf
				
				u = 1
				j = j + 1
			
			
			End IF			
			
			dateStartTmp = DateSerial(Year(arrRoom(0,k)),Month(arrRoom(0,k)),Day(arrRoom(0,k)))
			dateEndTmp = DateSerial(Year(arrRoom(1,k)),Month(arrRoom(1,k)),Day(arrRoom(1,k)))
			
		Next
		
		strPriceDetail = Replace(strPriceDetail,"intRowSpan",u)
		strPrice = strPrice & strPriceDetail
		strPrice = strPrice & "</tr>" & VbCrlf
		strPrice = strPrice & "</table>" & VbCrlf
		strPrice = strPrice & "<div align='center' class='s'><br><b>*<font color='#FF0000' >Breakfast Option</b></font><br></div>"
			
	End IF
	
	fnRoomPrice = strPrice
'##### Room Price #####

'##### Option Price #####
	sqlOptionPrice = "SELECT o.title_en, (SELECT TOP 1 pr.price FROM tbl_option_price pr WHERE pr.option_id=o.option_id AND pr.date_end>= "& fnConvertRawSqlDate(Date) &") AS price"
	sqlOptionPrice = sqlOptionPrice & " FROM tbl_product_option o"
	sqlOptionPrice = sqlOptionPrice & " WHERE o.status=1 AND o.option_cat_id IN (39,40) AND product_id="& intProductID &" AND (SELECT TOP 1 pr.price FROM tbl_option_price pr WHERE pr.option_id=o.option_id AND pr.date_end>= "& fnConvertRawSqlDate(Date) &")>0"

	Set recOptionPrice = Server.CreateObject ("ADODB.Recordset")
	recOptionPrice.Open sqlOptionPrice, Conn,adOpenForwardOnly,adLockReadOnly
	
	IF NOT recOptionPrice.EOF Then
		strOptionPrice = "<font color='#0033FF' class='m'><br><b>Option Price: </b></font>"
		strOptionPrice = strOptionPrice & "<table width='57%' cellpadding='2' cellspacing='1' bgcolor='#CCE0FF'>"
		strOptionPrice = strOptionPrice & "<tr bgcolor='#C4DBFF' class='s1'> "
		strOptionPrice = strOptionPrice & "<td width='60%'><div align='center'><font color='#0066FF'><b>Option</b></font></div></td>"
		strOptionPrice = strOptionPrice & "<td width='40%'><div align='center'><b><font color='#0066FF'>Rates<br> <font color=""#0066FF"">("& strCurrencyTitle &")</font></font></b></div></td>"
		strOptionPrice = strOptionPrice & "</tr>"
		
		While NOT recOptionPrice.EOF
			strOptionPrice = strOptionPrice & "<tr bgcolor='#FFFFFF' class='m'> "
			strOptionPrice = strOptionPrice & "<td>"& recOptionPrice.Fields("title_en") &"</td>"
			strOptionPrice = strOptionPrice & "<td align='center'><b>"& fnCurrencyDisplay(recOptionPrice.Fields("price")) &"</b></td>"
			strOptionPrice = strOptionPrice & "</tr>"
			
			recOptionPrice.MoveNext
		Wend
		
		strOptionPrice = strOptionPrice & "</table>"
		
	End IF

	recOptionPrice.Close
	Set recOptionPrice = Nothing

	fnRoomPrice = fnRoomPrice  & strOptionPrice
'##### Option Price #####
	
'##### Free Option #####
	sqlFreeOption = sqlFreeOption & "SELECT o.title_en"
	sqlFreeOption = sqlFreeOption & " FROM tbl_product_option o"
	sqlFreeOption = sqlFreeOption & " WHERE  o.status=1 AND o.option_cat_id <> 38 AND o.status=1 AND product_id="& intProductID &" AND ISNULL((SELECT TOP 1 pr.price FROM tbl_option_price pr WHERE pr.option_id=o.option_id AND pr.date_end>= "& fnConvertRawSqlDate(Date) &"),1)=0"

	Set recFreeOption = Server.CreateObject ("ADODB.Recordset")
	recFreeOption.Open sqlFreeOption, Conn,adOpenForwardOnly,adLockReadOnly
	
	IF NOT recFreeOption.EOF Then
	
		strFreeOption = "<font color='#0033FF' class='m'><br><b>Free Option: </b></font>"
		strFreeOption = strFreeOption & "<table width='57%' cellpadding='2' cellspacing='1' bgcolor='#CCE0FF'>"
		strFreeOption = strFreeOption & "<tr bgcolor='#C4DBFF' class='s1'> "
		strFreeOption = strFreeOption & "<td><div align='center'><font color='#0066FF'><b>Option</b></font></div></td>"
		strFreeOption = strFreeOption & "</tr>"
		
		While NOT recFreeOption.EOF
			strFreeOption = strFreeOption & "<tr bgcolor='#FFFFFF' class='m'> "
			strFreeOption = strFreeOption & "<td align='center'>"& recFreeOption.Fields("title_en") &"</td>"
			strFreeOption = strFreeOption & "</tr>"
			
			recFreeOption.MoveNext
		Wend
		
		strFreeOption = strFreeOption & "</table>"
	Else
		strBreakfast = "Our rate is INCLUDED Goverment TAX and Service Charge."
	End IF

	recFreeOption.Close
	Set recFreeOption = Nothing
	
	fnRoomPrice = fnRoomPrice  & strFreeOption
	fnRoomPrice = Replace(fnRoomPrice,"Breakfast Option",strBreakfast)
'##### Free Option #####



END FUNCTION
%>