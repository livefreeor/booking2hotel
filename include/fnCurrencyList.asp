<%
FUNCTION fnCurrencyList(intCurrency, intdisplayType)

	Dim sqlCurrency
	Dim recCurrency
	Dim strSelect
	Dim strBgSelect
	
	IF intCurrency = "" Then
		intCurrency = 25 'Set to Thai Baht
	End IF
			
	SELECT CASE intdisplayType
		Case 1,2
			
			sqlCurrency = "SELECT currency_id, title, prefix FROM tbl_currency ORDER BY title ASC"
			
			Set recCurrency = Server.CreateObject ("ADODB.Recordset")
			recCurrency.Open sqlCurrency, Conn,adOpenForwardOnly, adLockReadOnly
			
			IF intDisplayType = 1 Then	
				fnCurrencyList = fnCurrencyList & "<p><table border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#99CC00""><tr><td bgcolor=""#FFFFFF""><table>" &vbcrlf
				fnCurrencyList = fnCurrencyList & "<form action=""hotels-currency-set.asp"" method=""post"">" &vbcrlf
				fnCurrencyList = fnCurrencyList & "<input type=""hidden"" name=""url"" value="""& Request.ServerVariables("URL") & "?" & Request.ServerVariables("QUERY_STRING") &""">"
				fnCurrencyList = fnCurrencyList & "<tr>" &vbcrlf
				fnCurrencyList = fnCurrencyList & "<td class=""s"">" &vbcrlf
				fnCurrencyList = fnCurrencyList & "<b><font color=""#0000FF"">View this page in</font> </b><select name=""currency_id"" >" &vbcrlf
				
				While NOT recCurrency.EOF
					IF Cint(recCurrency.Fields("currency_id")) = Cint(intCurrency) Then
						strSelect = "selected"
					Else
						strSelect = ""
					End IF
					fnCurrencyList = fnCurrencyList  & "<option value="""& recCurrency.Fields("currency_id") &""" "& strSelect &">"&recCurrency.Fields("title")&"</option>" & VbCrlf
					recCurrency.MoveNext
				Wend
				
				fnCurrencyList = fnCurrencyList & "</select>" &vbcrlf
				fnCurrencyList = fnCurrencyList & "<input name=""Submit"" type=""submit"" class=""greenBtn"" value=""Convert"">" &vbcrlf
				fnCurrencyList = fnCurrencyList & "</td>" &vbcrlf
				fnCurrencyList = fnCurrencyList & "</tr>" &vbcrlf
				fnCurrencyList = fnCurrencyList & "</form>" &vbcrlf
				fnCurrencyList = fnCurrencyList & "</table></tr></td></table></p>" &vbcrlf
				
			ElseIF intDisplayType = 2 Then
			
				fnCurrencyList = "<table width='100%' cellpadding='2' cellspacing='1' bgcolor='#E0FF84'>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "<form action=""hotels-currency-set.asp"" method=""post"">" &vbcrlf
				fnCurrencyList = fnCurrencyList & "<input type=""hidden"" name=""url"" value="""& Request.ServerVariables("URL") & "?" & Request.ServerVariables("QUERY_STRING") &""">"
				fnCurrencyList = fnCurrencyList & "<tr>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "<td><b><font color='#0066FF'>Viwe This page in</font></b></td>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "</tr>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "<tr>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "<td bgcolor='#FFFFFF'><select name='currency_id'>" & VbCrlf
				
				While NOT recCurrency.EOF
					IF Cint(recCurrency.Fields("currency_id")) = Cint(intCurrency) Then
						strSelect = "selected"
					Else
						strSelect = ""
					End IF
					fnCurrencyList = fnCurrencyList  & "<option value="""& recCurrency.Fields("currency_id") &""" "& strSelect &">"&recCurrency.Fields("title")&"</option>" & VbCrlf
					recCurrency.MoveNext
				Wend
				
				fnCurrencyList = fnCurrencyList & "</select>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "(Current Currency: "& Session("currency_title") &")" & VbCrlf
				fnCurrencyList = fnCurrencyList & "</td>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "</tr>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "<tr>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "<td bgcolor='#FFFFFF'><input name=""Submit"" type=""submit"" class=""greenBtn"" value=""Convert""></td>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "</tr>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "</form>" &vbcrlf
				fnCurrencyList = fnCurrencyList & "</table>" & VbCrlf
			End IF
			recCurrency.Close
			Set recCurrency = Nothing
	
		Case 3
			
			sqlCurrency = "SELECT currency_id,title,prefix,code FROM tbl_currency WHERE status=1 ORDER BY code ASC"
			
			Set recCurrency = Server.CreateObject ("ADODB.Recordset")
			recCurrency.Open sqlCurrency, Conn,adOpenStatic, adLockReadOnly
			
				fnCurrencyList = fnCurrencyList & "<p><table border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#0066FF""><tr><td bgcolor=""#FFFFFF""><table>" &vbcrlf
				fnCurrencyList = fnCurrencyList & "<tr><td colspan=""20"" align=""center""><font color=""#BC0E31""><b>To change the currency, click on the flag below:</b></font></td></tr>" & VbCrlf
				fnCurrencyList = fnCurrencyList & "<tr>"&VbCrlf
				While NOT recCurrency.EOF
					
					IF Cint(recCurrency.Fields("currency_id")) = Cint(intCurrency) Then
						strBgSelect = " bgcolor=""#FFFF99"""
					Else
						strBgSelect = " bgcolor=""#FFFFFF"""
					End IF

					fnCurrencyList = fnCurrencyList & "<td"& strBgSelect &"><a href="""& Request.ServerVariables("URL") &"?cur="& recCurrency.Fields("code") & "#currency" &""" class=""CurrencyLink""><img src='/image/flag_"& Lcase(recCurrency.Fields("code")) &".gif' border=""0""><br>"
					fnCurrencyList = fnCurrencyList & recCurrency.Fields("code") & "</a></td>" & VbCrlf
					recCurrency.MoveNext
				Wend
				fnCurrencyList = fnCurrencyList & "</tr>" &vbcrlf
				
				fnCurrencyList = fnCurrencyList & "</table></tr></td></table></p>" &vbcrlf
				
			recCurrency.Close
			Set recCurrency = Nothing
	
	END SELECT
END FUNCTION
%>