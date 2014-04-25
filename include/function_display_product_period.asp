<%
FUNCTION function_display_product_period(intProductID,intPeriodID,strName,intType)

	Dim sqlPeriod
	Dim recPeriod
	Dim arrPeriod
	Dim intCountPeriod
	Dim bolPeriod
	Dim strReturn
	Dim strChecked
	Dim strPickUp
	
	sqlPeriod = "SELECT period_id,time_start,time_end,time_pick,time_send,pickup_include"
	sqlPeriod = sqlPeriod & " FROM tbl_product_period"
	sqlPeriod = sqlPeriod & " WHERE product_id=" & intProductID
	'sqlPeriod = sqlPeriod & " AND status=1 ORDER BY time_start ASC"
	sqlPeriod = sqlPeriod & " ORDER BY time_start ASC"
	Set recPeriod = Server.CreateObject ("ADODB.Recordset")
	recPeriod.Open SqlPeriod, Conn,adOpenStatic,adLockreadOnly
		IF NOT recPeriod.EOF Then
			arrPeriod = recPeriod.GetRows()
			bolPeriod = True
		Else
			bolPeriod = False
		End IF
	recPeriod.Close
	Set recPeriod = Nothing 

	IF bolPeriod Then
	
		SELECT CASE intType
			Case 1 '### Product Detail ###
				strReturn = "<table width=""70%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strReturn = strReturn & "<tr>" & VbCrlf
				strReturn = strReturn & "<td bgcolor=""#EDF5FE""><strong><font color=""#346494"">Trip Period</font></strong></td>" & VbCrlf
				strReturn = strReturn & "</tr>" & VbCrlf
				
				For intCountPeriod=0 to Ubound(arrPeriod,2)
		
					IF intPeriodID <> "" AND NOT ISNULL(intPeriodID) Then 'Set Defaul Period ID
						IF Cstr(arrPeriod(0,intCountPeriod)) = Cstr(intPeriodID) Then
							strChecked = "checked"
						End IF
					Else
						IF intCountPeriod=0 Then
							strChecked = "checked"
						Else
							strChecked = ""
						End IF
					End IF
					IF arrPeriod(5,intCountPeriod) Then
						strPickUp="( <font color=""green"">Pickup around "&function_date(arrPeriod(3,intCountPeriod),7)&" and return around "&function_date(arrPeriod(4,intCountPeriod),7)&"</font> )"
					End IF
					strReturn = strReturn & "<tr>" & VbCrlf
					strReturn = strReturn & "<td bgcolor=""#FFFFFF""><input name="""&strName&""" type=""radio"" value="""&arrPeriod(0,intCountPeriod)&""" "&strChecked&" /> "&function_date(arrPeriod(1,intCountPeriod),7)&" - "&function_date(arrPeriod(2,intCountPeriod),7)&strPickUp&"</td>" & VbCrlf
					strReturn = strReturn & "</tr>" & VbCrlf
				
				Next
				
				strReturn = strReturn & "</table>" & VbCrlf
				
			Case 2 '### Shopping Cart ###
				strReturn = "<table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				
				For intCountPeriod=0 to Ubound(arrPeriod,2)
		
					IF intPeriodID <> "" AND NOT ISNULL(intPeriodID) Then 'Set Defaul Period ID
						IF Cstr(arrPeriod(0,intCountPeriod)) = Cstr(intPeriodID) Then
							strChecked = "checked"
						Else
							strChecked = ""
						End IF
					Else
						IF intCountPeriod=0 Then
							strChecked = "checked"
						Else
							strChecked = ""
						End IF
					End IF

					strReturn = strReturn & "<tr>" & VbCrlf
					strReturn = strReturn & "<td bgcolor=""#FFFFFF""><input name="""&strName&""" type=""radio"" value="""&arrPeriod(0,intCountPeriod)&""" "&strChecked&" /> "&function_date(arrPeriod(1,intCountPeriod),7)&" - "&function_date(arrPeriod(2,intCountPeriod),7) & VbCrlf
					strReturn = strReturn & "</tr>" & VbCrlf
				
				Next
				
				strReturn = strReturn & "</table>" & VbCrlf
				
			Case 3 'Normal Text
				
				For intCountPeriod=0 to Ubound(arrPeriod,2)
					IF intPeriodID <> "" AND NOT ISNULL(intPeriodID) Then
						IF Cstr(arrPeriod(0,intCountPeriod)) = Cstr(intPeriodID) Then
							strReturn = strReturn & function_date(arrPeriod(1,intCountPeriod),7)&" - "&function_date(arrPeriod(2,intCountPeriod),7)
							Exit For
						End IF
					End IF
				Next

			Case 4 'Normal text With Estimate Time
				For intCountPeriod=0 to Ubound(arrPeriod,2)
					IF intPeriodID <> "" AND NOT ISNULL(intPeriodID) Then
						IF Cstr(arrPeriod(0,intCountPeriod)) = Cstr(intPeriodID) Then
								IF arrPeriod(5,intCountPeriod) Then
									strReturn = strReturn & function_date(arrPeriod(1,intCountPeriod),7)&" - "&function_date(arrPeriod(2,intCountPeriod),7) & " ( Pickup around "&function_date(arrPeriod(3,intCountPeriod),7)&" and return around "&function_date(arrPeriod(4,intCountPeriod),7)&")</td>" & VbCrlf
								Else
									strReturn = strReturn & function_date(arrPeriod(1,intCountPeriod),7)&" - "&function_date(arrPeriod(2,intCountPeriod),7)&"</td>" & VbCrlf
								End IF
							Exit For
						End IF
					End IF
				Next
				
				
			Case 5 '### Product Detail Without Pickup and return time###
				strReturn = "<table width=""70%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#E4E4E4"">" & VbCrlf
				strReturn = strReturn & "<tr>" & VbCrlf
				strReturn = strReturn & "<td bgcolor=""#EDF5FE""><strong><font color=""#346494""> Period </font></strong></td>" & VbCrlf
				strReturn = strReturn & "</tr>" & VbCrlf
				
				
				
				For intCountPeriod=0 to Ubound(arrPeriod,2)
		
					IF intPeriodID <> "" AND NOT ISNULL(intPeriodID) Then 'Set Defaul Period ID
						IF Cstr(arrPeriod(0,intCountPeriod)) = Cstr(intPeriodID) Then
							strChecked = "checked"
						End IF
					Else
						IF intCountPeriod=0 Then
							strChecked = "checked"
						Else
							strChecked = ""
						End IF
					End IF
					IF arrPeriod(5,intCountPeriod) Then
						strPickUp="( <font color=""green"">Pickup around "&function_date(arrPeriod(3,intCountPeriod),7)&" and return around "&function_date(arrPeriod(4,intCountPeriod),7)&"</font> )"
					End IF
					strReturn = strReturn & "<tr>" & VbCrlf
					strReturn = strReturn & "<td bgcolor=""#FFFFFF""><input name="""&strName&""" type=""radio"" value="""&arrPeriod(0,intCountPeriod)&""" "&strChecked&" /> "&function_date(arrPeriod(1,intCountPeriod),7)&" - "&function_date(arrPeriod(2,intCountPeriod),7)&strPickUp&"</td>" & VbCrlf
					strReturn = strReturn & "</tr>" & VbCrlf
				
				Next
				
				strReturn = strReturn & "</table>" & VbCrlf
				
			Case 6 'temp
			
		END SELECT
	End IF


	function_display_product_period = strReturn

END FUNCTION
%>


