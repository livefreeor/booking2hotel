<%
FUNCTION function_cart_product_check_adult(intCartProductID)
	
	Dim sqlCheck
	Dim arrCheck
	Dim recCheck
	Dim intAdult
	
	sqlCheck = "SELECT SUM(po.max_adult*ci.quantity) AS adult"
	sqlCheck = sqlCheck & " FROM tbl_cart_item ci, tbl_product_option po"
	sqlCheck = sqlCheck & " WHERE ci.option_id=po.option_id AND ci.cart_product_id=" & intCartProductID

	Set recCheck  = Server.CreateObject ("ADODB.Recordset")
	recCheck.Open SqlCheck, Conn,adOpenStatic,adLockreadOnly
		intAdult = recCheck.Fields("adult")
	recCheck.Close
	Set recCheck = Nothing 

	IF ISNULL(intAdult) Then
		intAdult = 100
	End IF

	function_cart_product_check_adult = intAdult

END FUNCTION
%>