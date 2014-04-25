<%
FUNCTION function_currency_set(strCode)
	
	Dim sqlCurrency
	Dim recCurrency
	
	IF strCode="" AND Session("currency_id")="" Then
		strCode = ConstCurrency
	ElseIF strCode="" AND Session("currency_code")<>"" Then
		strCode = Session("currency_code")
	End IF
	
	IF Len(strCode)>3 Then
		Response.End()
		Response.Redirect "/"
	End IF
	
	sqlCurrency = "SELECT currency_id,title,prefix,code FROM tbl_currency WHERE code='"& strCode &"'" 
	
	Set recCurrency  = Server.CreateObject ("ADODB.Recordset")
	recCurrency.Open sqlCurrency , Conn,adOpenForwardOnly,adLockreadOnly
		Session("currency_id") = recCurrency.Fields("currency_id")
		Session("currency_title") = recCurrency.Fields("title")
		Session("currency_prefix") = recCurrency.Fields("prefix")
		Session("currency_code") = recCurrency.Fields("code")
	recCurrency.Close
	Set recCurrency  = Nothing 
	
END FUNCTION
%>