<%
Function function_check_guest_name(intOrderID)
	call connOpen()
	Dim sqlCommand
	sqlCommand="select count(op.order_product_id)"
	sqlCommand=sqlCommand&" from tbl_order_product op"
	sqlCommand=sqlCommand&" where op.order_id="&intOrderID&" and ((select count(guest_id) from tbl_order_product_guest sopg where sopg.order_product_id=op.order_product_id and sopg.guest_name<>'')<(num_adult+num_child))"
	IF int(trim(Conn.Execute(sqlCommand).getString))<>0 Then
		function_check_guest_name="<br><img src=""/images/guest_warning.gif""><font color='red'>check guest name</font>"
	End IF
End Function
%>