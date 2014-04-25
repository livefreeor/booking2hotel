<%
SUB subCurrencyCheck

	IF Session("currency_id") = "" AND Request.QueryString("cur")="" Then
	
		Session("currency_id") = 25
		Session("currency_pr") = 1
		Session("currency_title") = "Thai Baht"
		
		intCurrencyPrefix = 1
		strCurrencyTitle = "Thai Baht"
		
	ElseIF Request.QueryString("cur")<>"" Then
		
		Dim sqlCheckCurrency
		Dim recCheckCurrency
		
		sqlCheckCurrency = "SELECT currency_id,title,prefix,code FROM tbl_currency WHERE code LIKE '%"& Request.QueryString("cur") &"%'"
		Set recCheckCurrency = Server.CreateObject ("ADODB.Recordset")
		recCheckCurrency.Open sqlCheckCurrency, Conn,adOpenForwardOnly, adLockReadOnly
			Session("currency_id") = recCheckCurrency("currency_id")
			Session("currency_pr") = recCheckCurrency("prefix")
			Session("currency_title") = recCheckCurrency("title")
			intCurrencyPrefix = recCheckCurrency("prefix")
			strCurrencyTitle = recCheckCurrency("title")
		recCheckCurrency.Close
		Set recCheckCurrency = Nothing
	ElseIF Session("currency_id") <> "" Then
		intCurrencyPrefix = Session("currency_pr")
		strCurrencyTitle = Session("currency_title")
	End IF
	
END SUB
%>