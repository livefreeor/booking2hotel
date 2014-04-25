<%
Function function_cart_clear()

	Dim intCartID
	Dim sqlDelItem
	Dim sqlDelProduct
	Dim sqlDelCart
	
	intCartID = function_cart_get_id()

	IF intCartID<>"" AND NOT ISNULL(intCartID) Then
		sqlDelItem = "DELETE tbl_cart_item WHERE cart_product_id IN (SELECT scp.cart_product_id FROM tbl_cart_product scp WHERE scp.cart_id="&intCartID&")"
		sqlDelProduct = "DELETE tbl_cart_product WHERE cart_id=" & intCartID
		sqlDelCart = "DELETE tbl_cart WHERE cart_id=" & intCartID

		Conn.ExeCute(sqlDelItem)
		Conn.ExeCute(sqlDelProduct)
		'Conn.ExeCute(sqlDelCart)
		
		Response.Cookies("cart_id") = ""
		Session("cart_id") = ""
	End IF

End Function
%>