<%
FUNCTION fnSubscribeMember(website_id,country_id,full_name,email,address,phone,fax,zip)
	
	Dim sqlSub
	Dim recSub
	Dim sqlCheck
	Dim recCheck
	Dim intCusID
	Dim sqlCheckID
	
	sqlcheck = "SELECT cus_id FROM tbl_customer WHERE email='"& Trim(email) &"'"
	Set recCheck = Server.CreateObject ("ADODB.Recordset")
	recCheck.Open SqlCheck, Conn,adOpenForwardOnly, adLockReadOnly
		IF NOT recCheck.EOF Then
			intCusID = recCheck.Fields("cus_id")
		End IF
	recCheck.Close
	Set recCheck = Nothing
	
	IF intCusID <> "" Then 'Old Member
		fnSubscribeMember = intCusID
	Else 'New Member
		sqlSub = "SELECT TOP 1 * FROM tbl_customer ORDER BY cus_id DESC"
		Set recSub = Server.CreateObject ("ADODB.Recordset")
		recSub.Open SqlSub, Conn,1,3
			recSub.AddNew
			recSub.Fields("website_id") = website_id
			recSub.Fields("country") = country_id
			recSub.Fields("full_name") = full_name
			recSub.Fields("email") = email
			recSub.Fields("address") = address
			recSub.Fields("phone") = phone
			recSub.Fields("fax") = fax
			recSub.Fields("zip") = zip
			recSub.Update
			intCusID = recSub.Fields("cus_id")
		recsub.Close
		Set recSub = Nothing
		
		fnSubscribeMember = intCusID
	End IF
	
END FUNCTION
%>