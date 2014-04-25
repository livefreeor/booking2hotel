<%
Function function_cart_clear()

	Dim intCartID
	Dim sqlDelItem
	Dim sqlDelProduct
	Dim sqlDelCart
	Dim strConn2
	Dim Conn2
	
	strConn2 = "Provider=SQLOLEDB.1;User ID=hotels2thailand;Password=FdC$sdor#$;database=hotels2thailand;Server=74.86.253.58;NETWORK=DBMSSOCN;"
	
	Set Conn2= server.CreateObject("ADODB.Connection")
	Conn2.Open strConn2

	intCartID = function_cart_get_id()

	IF intCartID<>"" AND NOT ISNULL(intCartID) Then
		sqlDelItem = "DELETE tbl_cart_item WHERE cart_product_id IN (SELECT scp.cart_product_id FROM tbl_cart_product scp WHERE scp.cart_id="&intCartID&")"
		sqlDelProduct = "DELETE tbl_cart_product WHERE cart_id=" & intCartID
		sqlDelCart = "DELETE tbl_cart WHERE cart_id=" & intCartID

		Conn2.ExeCute(sqlDelItem)
		Conn2.ExeCute(sqlDelProduct)
		'Conn.ExeCute(sqlDelCart)
		
		Response.Cookies("cart_id") = ""
		Session("cart_id") = ""
	End IF
	
	Conn2.Close()
	Set Conn2 = Nothing
End Function
%>