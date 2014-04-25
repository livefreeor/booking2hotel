<%
FUNCTION function_display_price(ByVal strPrice,ByVal intType)

	Dim intCurrencyPrefix
	Dim sqlCurrency
	Dim sqlDefaultCurrency
	Dim recCurrency
	
	SELECT CASE intType
	
		Case 1,2 'Normal Case
		IF Session("currency_prefix")<>"" Then
			intCurrencyPrefix = Session("currency_prefix")
		Else
			sqlCurrency = "SELECT currency_id,title,prefix,code FROM tbl_currency WHERE code='"& ConstCurrency &"'" 
			
			Set recCurrency  = Server.CreateObject ("ADODB.Recordset")
			recCurrency.Open sqlCurrency , Conn,adOpenForwardOnly,adLockreadOnly
			Session("currency_id") = recCurrency.Fields("currency_id")
			Session("currency_title") = recCurrency.Fields("title")
			Session("currency_prefix") = recCurrency.Fields("prefix")
			Session("currency_code") = recCurrency.Fields("code")
			recCurrency.Close
			Set recCurrency  = Nothing 
			
			intCurrencyPrefix = Session("currency_prefix")
		End IF
		
		Case 3 'Default Currency
			sqlCurrency = "SELECT currency_id,title,prefix,code FROM tbl_currency WHERE code='"& ConstCurrency &"'" 
			
			Set recCurrency  = Server.CreateObject ("ADODB.Recordset")
			recCurrency.Open sqlCurrency , Conn,adOpenForwardOnly,adLockreadOnly
				intCurrencyPrefix = recCurrency.Fields("prefix")
			recCurrency.Close
			Set recCurrency  = Nothing 
			
	END SELECT
	
	
	SELECT CASE intType
		Case 1,3 'Normal Price
			IF IsNumeric(strPrice) Then
				strPrice = strPrice/intCurrencyPrefix 
				strPrice = strPrice*intVatFactor 'Exclude VAT and Service Charge
				strPrice = FormatNumber(strPrice,0)
			Else
				strPrice = strPRice
			End IF
		Case 2 'Rack Price
			IF Int(strPrice)=0 Then
				strPrice = "N/A"
			Else
				strPrice = strPrice/intCurrencyPrefix 
				strPrice = strPrice*intVatFactor 'Exclude VAT and Service Charge
				strPrice = FormatNumber(strPrice,0)
				strPrice = "<div class=""ss"">"& strPrice &"</div>"
			End IF
	END SELECT
	
	function_display_price = strPrice
	
END FUNCTION
%>