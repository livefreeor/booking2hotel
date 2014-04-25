<%
FUNCTION function_currency_list(intCurrency, intdisplayType)

	Dim sqlCurrency
	Dim recCurrency
	Dim strSelect
	Dim strBgSelect
	
	IF intCurrency = "" Then
		intCurrency = 25 'Set to Thai Baht
	End IF
			
	SELECT CASE intdisplayType	
		Case 1
			
			sqlCurrency = "SELECT currency_id,title,prefix,code FROM tbl_currency WHERE status=1 ORDER BY code ASC"
			
			Set recCurrency = Server.CreateObject ("ADODB.Recordset")
			recCurrency.Open sqlCurrency, Conn,adOpenStatic, adLockReadOnly
			
				function_currency_list = function_currency_list & "<p><table border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#FFFFFF"" width=""100%"" align=""center""><tr><td bgcolor=""#FFFFFF""><table align=""center"" width=""100%"">" &vbcrlf
				function_currency_list = function_currency_list & "<tr><td colspan=""20"" align=""center""><font color=""#BC0E31""><b>To change the currency, click on the flag below:</b></font></td></tr>" & VbCrlf
				function_currency_list = function_currency_list & "<tr>"&VbCrlf
				While NOT recCurrency.EOF
					
					IF Cint(recCurrency.Fields("currency_id")) = Cint(intCurrency) Then
						strBgSelect = " bgcolor=""#FFFF99"""
					Else
						strBgSelect = " bgcolor=""#FFFFFF"""
					End IF

					function_currency_list = function_currency_list & "<td"& strBgSelect &" align=""center""><a href="""& Request.ServerVariables("URL") &"?cur="& recCurrency.Fields("code") & "#currency" &""" class=""CurrencyLink""><img src='/images/flag_"& Lcase(recCurrency.Fields("code")) &".gif' border=""0""><br>"
					function_currency_list = function_currency_list & recCurrency.Fields("code") & "</a></td>" & VbCrlf
					recCurrency.MoveNext
				Wend
				function_currency_list = function_currency_list & "</tr>" &vbcrlf
				
				function_currency_list = function_currency_list & "</table></tr></td></table></p>" &vbcrlf
				
			recCurrency.Close
			Set recCurrency = Nothing
	
	END SELECT
END FUNCTION
%>