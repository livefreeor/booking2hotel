<%
FUNCTION function_add_customer_order()

	Dim sqlAddCus
	Dim recAddCus
	Dim intCusID
	Dim strEmail
	
	Dim ConnW
	'strEmail = Replace(Trim(Request.Form("email")),','')
	
	Set ConnW= server.CreateObject("ADODB.Connection")
	ConnW.Open "Provider=MSDASQL; Driver={SQL Server}; Server=74.86.253.60; Database=hotels2; UID=hotels2thailandwrite; PWD=kfoutdk$or3$;"
	
	IF len(Request.Form("email"))>100 Then
		response.Redirect("http://www.hotels2thailand.com")
	End IF
	sqlAddCus = "SELECT * FROM tbl_customer WHERE email='"&destroy_qoute(Trim(Request.Form("email")))&"'"
	Set recAddCus = Server.CreateObject ("ADODB.Recordset")
	recAddCus.Open sqlAddCus, ConnW,1,3

		IF recAddCus.EOF Then
			recAddCus.AddNew
		End IF
		
		recAddCus.Fields("website_id") = 1
		recAddCus.Fields("country") = Server.HTMLEncode(Request.Form("country"))
		recAddCus.Fields("full_name") = Server.HTMLEncode(Request.Form("full_name"))
		recAddCus.Fields("email") = Server.HTMLEncode(Request.Form("email"))
		recAddCus.Fields("address") = Server.HTMLEncode(Request.Form("address"))
		
		IF Request.form("phone_type")=1 Then
			recAddCus.Fields("mobile") = Server.HTMLEncode(Request("phone"))
		Else
			recAddCus.Fields("phone") = Server.HTMLEncode(Request("phone"))
		End IF
		
		IF Request.Form("receive_mail")="yes" Then
			recAddCus.Fields("mail") = 1
		Else
			recAddCus.Fields("mail") = 0
		End IF
		
		recAddCus.Update
		
		intCusID = recAddCus.Fields("cus_id")
		
	recAddCus.Close
	Set recAddCus = Nothing
	
	ConnW.Close
	Set ConnW  = Nothing
	
	function_add_customer_order = intCusID
	
END FUNCTION
%>