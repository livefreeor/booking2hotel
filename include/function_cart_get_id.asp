<%
Function function_cart_get_id()
	
	Dim cartID
	Dim sqlCheckCart
	Dim recCheckCart
	
	IF Request.Cookies("cart_id") <> "" AND NOT ISNULL(Request.Cookies("cart_id"))  Then
		cartID = Request.Cookies("cart_id")
	ElseIF Session("cart_id") <> "" AND NOT ISNULL(Session("cart_id")) Then
		cartID = Session("cart_id")
	Else
		function_cart_get_id = Null
	End IF
	
	'### Check For Existing ID ###
	IF cartID<>"" AND NOT ISNULL(cartID) Then
		sqlCheckCart = "SELECT cart_id FROM tbl_cart WHERE cart_id=" & cartID
		Set recCheckCart = Server.CreateObject ("ADODB.Recordset")
		recCheckCart.Open SqlCheckCart, Conn,adOpenStatic,adLockreadOnly
			IF NOT recCheckCart.EOF Then
				function_cart_get_id = cartID
			Else
				'Response.Cookies("cart_id") = ""
				'Session("cart_id") = ""
				function_cart_get_id = Null
			End IF
		recCheckCart.Close
		SET recCheckCart = Nothing
	End IF
	'### Check For Existing ID ###

End Function
%>