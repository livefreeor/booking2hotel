<%
FUNCTION function_display_rate_quantity(dateCheckIn,intProductID,intType)

	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim sqlSup
	Dim recSup
	Dim arrSup
	Dim intCount
	Dim intCountColum
	Dim intCountRow
	Dim strTable
	Dim bolRate
	Dim bolSup
	Dim intPriceIDTmp
	Dim strPerson
	Dim intColum
	Dim intRow
	Dim intPrice
	Dim intColumPosition
	Dim strDayCond
	
	SELECT CASE intType
		Case 1 '### Without date Input ###
			
			sqlRate = "SELECT op.price_id,op.option_id,op.date_start,op.date_end,op.price,op.price_child,op.price_child_own,op.price_own,op.price_rack,op.sup_weekend,op.sup_holiday,op.sup_long,po.title_en,po.show_option,"
			sqlRate = sqlRate & "p.day_sun,p.day_mon,p.day_tue,p.day_wed,p.day_thu,p.day_fri,p.day_sat"
			sqlRate = sqlRate & " FROM tbl_product p, tbl_product_option po, tbl_option_price op"
			sqlRate = sqlRate & " WHERE p.product_id=po.product_id AND po.option_id=op.option_id AND po.status=1 AND op.date_end>=getdate() AND p.product_id=" & intProductID
			sqlRate = sqlRate & " ORDER BY po.option_priority ASC"
			Set recRate = Server.CreateObject ("ADODB.Recordset")
			recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
				IF NOT recRate.EOF Then
					arrRate = recRate.GetRows()
					bolRate = True
					strDayCond = "<div class=""s"">" & function_display_day_condition(arrRate(14,0),arrRate(15,0),arrRate(16,0),arrRate(17,0),arrRate(18,0),arrRate(19,0),arrRate(20,0)) & "</div>"
				Else
					bolRate = False
				End IF
			recRate.Close
			Set recRate = Nothing 

			sqlSup = "SELECT opq.price_id,opq.quantity_min,opq.quantity_max,opq.supplement"
			sqlSup = sqlSup & " FROM tbl_product p, tbl_product_option po, tbl_option_price op, tbl_option_price_quantity opq"
			sqlSup = sqlSup & " WHERE p.product_id=po.product_id AND po.option_id=op.option_id AND op.price_id=opq.price_id AND po.status=1 AND op.date_end>=getdate() AND p.product_id=" & intProductID
			sqlSup = sqlSup & " ORDER BY opq.price_id ASC, opq.quantity_min ASC, opq.quantity_max ASC"
			Set recSup = Server.CreateObject ("ADODB.Recordset")
			recSup.Open SqlSup, Conn,adOpenStatic,adLockreadOnly
				IF NOT recSup.EOF Then
					arrSup = recSup.GetRows()
					bolSup = True
				Else
					bolSup = False
				End IF
			recSup.Close
			Set recSup = Nothing 

			IF bolRate AND bolSup Then
				
				'### Find Number of Colum ###
				intPriceIDTmp = arrRate(0,0)
				
				For intCount=0 To Ubound(arrSup,2)
					IF arrSup(0,intCount)=intPriceIDTmp Then
						intColum = intColum+1
					Else
						Exit For
					End IF
				Next
				'### Find Number of Colum ###

				strTable = "<table width=""98%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				
				'### Colum Head ###
				strTable = strTable & "<tr>" & VbCrlf
				strTable = strTable & "<td bgcolor=""#EDF5FE"" align=""center""><strong><font color=""#346494"">Title</font></strong></td><td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">Period</font></strong></td>" & VbCrlf
				
				For intCountColum=0 To intColum-1
					IF arrSup(2,intCountColum)<100 Then
						IF arrSup(1,intCountColum)>0 Then
							IF (arrSup(1,intCountColum) +1) = arrSup(2,intCountColum) Then
								strPerson = arrSup(2,intCountColum) & " Persons"
							Else
								strPerson = (arrSup(1,intCountColum) +1) & "-" & arrSup(2,intCountColum) & " Persons"
							End IF
						Else
							strPerson = (arrSup(1,intCountColum) +1) & " Person"
						End IF
					Else
						strPerson = (arrSup(1,intCountColum) +1) & " Persons Up"
					End IF
					strTable = strTable & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">"&strPerson&"</font></strong></td>" & VbCrlf
				Next
				
				strTable = strTable & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">Child</font></strong></td>" & VbCrlf
				strTable = strTable & "<td rowspan="""&Ubound(arrRate,2)+2&""" align=""center"" valign=""middle"" bgcolor=""#FFFFFF""><font color=""#990066"">"&Session("currency_title") & "&nbsp; <img src=""/images/flag_"&Session("currency_code")&".gif"">"&"</font> </td>"
				strTable = strTable & "</tr>" & VbCrlf
				'### Colum Head ###
				
				'### Body ###
				For intCountRow=0 To Ubound(arrRate,2)
					strTable = strTable & "<tr>" & VbCrlf
					strTable = strTable & "<td bgcolor=""#FFFFFF"">"&arrRate(12,0)&"</td><td align=""center""  bgcolor=""#FFFFFF""><font color=""#990000"">"&function_date(arrRate(2,intCountRow),3)&" - "&function_date(arrRate(3,intCountRow),3)&"</font></td>" & VbCrlf
					For intCountColum=0 To intColum-1
						intColumPosition = ((intCountColum+1) + (intCountRow*intColum)) - 1
						intPrice = arrRate(4,intCountRow) + arrSup(3,intColumPosition)
						strTable = strTable & "<td align=""center"" bgcolor=""#FFFFFF"">"&function_display_price(intPrice,1)&"</td>" & VbCrlf
					Next
					strTable = strTable & "<td align=""center"" bgcolor=""#FFFFFF"">"&function_display_price(arrRate(5,intCountRow),1)&"</td>" & VbCrlf
					strTable = strTable & "</tr>" & VbCrlf
				Next
				'### Body ###
				
				strTable = strTable & "</table>" & VbCrlf
				
				IF strDayCond <> "" Then
					strTable = strTable & "<br /> " & strDayCond & "<br />"
				End IF
			Else
				strTable = "<font color=""red"">Sorry, We don't have rate for this product. Please contact us at <a href=""mailto:support@hotels2thailand.com"">support@hotels2thailand.com</a></font>"
			End IF

		Case 2 '### With date intput ###
		
		sqlRate = "SELECT op.price_id,op.option_id,op.date_start,op.date_end,op.price,op.price_child,op.price_child_own,op.price_own,op.price_rack,op.sup_weekend,op.sup_holiday,op.sup_long,po.title_en,po.show_option,"
		sqlRate = sqlRate & " p.day_sun,p.day_mon,p.day_tue,p.day_wed,p.day_thu,p.day_fri,p.day_sat"
		sqlRate = sqlRate & " FROM tbl_product p, tbl_product_option po, tbl_option_price op "
		sqlRate = sqlRate & " WHERE p.product_id=po.product_id AND po.option_id=op.option_id AND po.status=1 AND (op.date_start<="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&" AND op.date_end>="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&") AND p.product_id=" & intProductID
		sqlRate = sqlRate & " ORDER BY po.option_priority ASC"
			Set recRate = Server.CreateObject ("ADODB.Recordset")
			recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
				IF NOT recRate.EOF Then
					arrRate = recRate.GetRows()
					bolRate = True
					strDayCond = "<div class=""s"">" & function_display_day_condition(arrRate(14,0),arrRate(15,0),arrRate(16,0),arrRate(17,0),arrRate(18,0),arrRate(19,0),arrRate(20,0)) & "</div>"
				Else
					bolRate = False
				End IF
			recRate.Close
			Set recRate = Nothing 
			
			sqlSup = "SELECT opq.price_id,opq.quantity_min,opq.quantity_max,opq.supplement"
			sqlSup = sqlSup & " FROM tbl_product p, tbl_product_option po, tbl_option_price op, tbl_option_price_quantity opq"
			sqlSup = sqlSup & " WHERE p.product_id=po.product_id AND po.option_id=op.option_id AND op.price_id=opq.price_id AND po.status=1 AND (op.date_start<="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&" AND op.date_end>="&function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1)&") AND p.product_id=" & intProductID
			sqlSup = sqlSup & " ORDER BY opq.price_id ASC, opq.quantity_min ASC, opq.quantity_max ASC"
			Set recSup = Server.CreateObject ("ADODB.Recordset")
			recSup.Open SqlSup, Conn,adOpenStatic,adLockreadOnly
				IF NOT recSup.EOF Then
					arrSup = recSup.GetRows()
					bolSup = True
				Else
					bolSup = False
				End IF
			recSup.Close
			Set recSup = Nothing 
			
			IF bolRate AND bolSup Then
			
				'### Find Number of Colum ###
				intPriceIDTmp = arrRate(0,0)
				
				For intCount=0 To Ubound(arrSup,2)
					IF arrSup(0,intCount)=intPriceIDTmp Then
						intColum = intColum+1
					Else
						Exit For
					End IF
				Next
				'### Find Number of Colum ###
				
				strTable = "<table width=""98%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				
				'### Colum Head ###
				strTable = strTable & "<tr>" & VbCrlf
				strTable = strTable & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">Date</font></strong></td>" & VbCrlf
				
				For intCountColum=0 To intColum-1
					IF arrSup(2,intCountColum)<100 Then
						IF arrSup(1,intCountColum)>0 Then
							strPerson = (arrSup(1,intCountColum) +1) & "-" & (arrSup(2,intCountColum)+1) & " Persons"
						Else
							strPerson = (arrSup(1,intCountColum) +1) & " Person"
						End IF
					Else
						strPerson = (arrSup(1,intCountColum) +1) & " Persons Up"
					End IF
					strTable = strTable & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">"&strPerson&"</font></strong></td>" & VbCrlf
				Next
				
				strTable = strTable & "<td align=""center"" bgcolor=""#EDF5FE""><strong><font color=""#346494"">Child</font></strong></td>" & VbCrlf
				strTable = strTable & "<td rowspan="""&Ubound(arrRate,2)+2&""" align=""center"" valign=""middle"" bgcolor=""#FFFFFF""><font color=""#990066"">"&Session("currency_title") & "&nbsp; <img src=""/images/flag_"&Session("currency_code")&".gif"">"&"</font> </td>"
				strTable = strTable & "</tr>" & VbCrlf
				'### Colum Head ###
				
				'### Body ###
				strTable = strTable & "<tr>" & VbCrlf
				strTable = strTable & "<td bgcolor=""#FFFFFF"" align=""center""><font color=""#990000"">"&function_date(dateCheckIn,5) & "</font></td>" & VbCrlf
				For intCountColum=0 To intColum-1
					intPrice = arrRate(4,0) + arrSup(3,intCountColum)
					strTable = strTable & "<td align=""center"" bgcolor=""#FFFFFF"">"&function_display_price(intPrice,1)&"</td>" & VbCrlf
				Next
				strTable = strTable & "<td align=""center"" bgcolor=""#FFFFFF"">"&function_display_price(arrRate(5,0),1)&"</td>" & VbCrlf
				strTable = strTable & "</tr>" & VbCrlf
				'### Body ###
				
				strTable = strTable & "</table>" & VbCrlf
				
				IF strDayCond <> "" Then
					strTable = strTable & "<br /> " & strDayCond & "<br />"
				End IF
				
			Else
				strTable = "<font color=""red"">Sorry, We don't have rate for this product. Please contact us at support@hotels2thailand.com</font>"
			End IF
			
			
		Case 3 '### Temporary ###
	END SELECT
	
	function_display_rate_quantity = strTable

END FUNCTION
%>