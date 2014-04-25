<%
FUNCTION fnCheckCusID(email)

	Dim sqlCheck
	Dim recCheck
	
	sqlcheck = "SELECT cus_id FROM tbl_customer WHERE email='"& Trim(email) &"'"
	Set recCheck = Server.CreateObject ("ADODB.Recordset")
	recCheck.Open SqlCheck, Conn,adOpenForwardOnly, adLockReadOnly
		IF NOT recCheck.EOF Then
			fnCheckCusID = recCheck.Fields("cus_id")
		Else
			fnCheckCusID = ""
		End IF
	recCheck.Close
	Set recCheck = Nothing
END FUNCTION
%>