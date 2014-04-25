<%
FUNCTION function_order_move(intOrderID,intType)

	Dim sqlOrder
	Dim recOrder
	Dim arrOrder
	Dim sqlAddOrder
	Dim recAddOrder
	Dim sqlProduct
	Dim sqlItem
	Dim sqlOrderAct
	Dim sqlDelOrder
	Dim sqlInsertAct
	Dim sqlOrderSite
	Dim intOrderIDNEW
	
	sqlOrder = "SELECT order_id,country_id,status_id,cus_id,payment_id,lang_id,referer_id,full_name,guest_name,email,address,zip,phone,mobile,fax,refer_ip,num_adult,num_child,confirm_fax_return,confirm_fax_return_time,confirm_payment,confirm_payment_time,confirm_open,confirm_open_time,confirm_available,confirm_available_time,confirm_check_in,confirm_check_in_time,confirm_hotel_payment,confirm_hotel_payment_time,confirm_order_completed,confirm_order_completed_time,date_submit,date_modify,status,comment,website_id,flight_arrival_number,flight_departure_number,flight_arrival_time,flight_departure_time,confirm_settle,confirm_print_receipt,gateway_id,confirm_voucher,confirm_receipt,confirm_paypal,aff_site_id,ad_google,cart_id"
	sqlOrder = sqlOrder & " FROM tbl_order "
	sqlOrder = sqlOrder & " WHERE order_id=" & intOrderID
	Set recOrder = Server.CreateObject ("ADODB.Recordset")
	recOrder.Open SqlOrder, Conn,adOpenStatic,adLockreadOnly
		arrOrder = recOrder.GetRows()
	recOrder.Close
	Set recOrder = Nothing
	
	sqlAddOrder = "SELECT TOP 1 * FROM tbl_order"
	Set recAddOrder = Server.CreateObject ("ADODB.Recordset")
	recAddOrder.Open SqlAddOrder, Conn,1,3
		recAddOrder.AddNew
		
		recAddOrder.Fields("country_id") = arrOrder(1,0)
		recAddOrder.Fields("status_id") = arrOrder(2,0)
		recAddOrder.Fields("cus_id") = arrOrder(3,0)
		recAddOrder.Fields("payment_id") = arrOrder(4,0)
		recAddOrder.Fields("lang_id") = arrOrder(5,0)
		recAddOrder.Fields("referer_id") = arrOrder(6,0)
		recAddOrder.Fields("full_name") = arrOrder(7,0)
		recAddOrder.Fields("guest_name") = arrOrder(8,0)
		recAddOrder.Fields("email") = arrOrder(9,0)
		recAddOrder.Fields("address") = arrOrder(10,0)
		recAddOrder.Fields("zip") = arrOrder(11,0)
		recAddOrder.Fields("phone") = arrOrder(12,0)
		recAddOrder.Fields("mobile") = arrOrder(13,0)
		recAddOrder.Fields("fax") = arrOrder(14,0)
		recAddOrder.Fields("refer_ip") = arrOrder(15,0)
		recAddOrder.Fields("num_adult") = arrOrder(16,0)
		recAddOrder.Fields("num_child") = arrOrder(17,0)
		recAddOrder.Fields("confirm_fax_return") = arrOrder(18,0)
		recAddOrder.Fields("confirm_fax_return_time") = arrOrder(19,0)
		recAddOrder.Fields("confirm_payment") = arrOrder(20,0)
		recAddOrder.Fields("confirm_payment_time") = arrOrder(21,0)
		recAddOrder.Fields("confirm_open") = arrOrder(22,0)
		recAddOrder.Fields("confirm_open_time") = arrOrder(23,0)
		recAddOrder.Fields("confirm_available") = arrOrder(24,0)
		recAddOrder.Fields("confirm_available_time") = arrOrder(25,0)
		recAddOrder.Fields("confirm_check_in") = arrOrder(26,0)
		recAddOrder.Fields("confirm_check_in_time") = arrOrder(27,0)
		recAddOrder.Fields("confirm_hotel_payment") = arrOrder(28,0)
		recAddOrder.Fields("confirm_hotel_payment_time") = arrOrder(29,0)
		recAddOrder.Fields("confirm_order_completed") = arrOrder(30,0)
		recAddOrder.Fields("confirm_order_completed_time") = arrOrder(31,0)
		recAddOrder.Fields("date_submit") = arrOrder(32,0)
		recAddOrder.Fields("date_modify") = arrOrder(33,0)
		recAddOrder.Fields("status") = arrOrder(34,0)
		recAddOrder.Fields("comment") = arrOrder(35,0)
		recAddOrder.Fields("website_id") = arrOrder(36,0)
		recAddOrder.Fields("flight_arrival_number") = arrOrder(37,0)
		recAddOrder.Fields("flight_departure_number") = arrOrder(38,0)
		recAddOrder.Fields("flight_arrival_time") = arrOrder(39,0)
		recAddOrder.Fields("flight_departure_time") = arrOrder(40,0)
		recAddOrder.Fields("confirm_settle") = arrOrder(41,0)
		recAddOrder.Fields("confirm_print_receipt") = arrOrder(42,0)
		recAddOrder.Fields("gateway_id") = arrOrder(43,0)
		recAddOrder.Fields("confirm_voucher") = arrOrder(44,0)
		recAddOrder.Fields("confirm_receipt") = arrOrder(45,0)
		recAddOrder.Fields("confirm_paypal") = arrOrder(46,0)
		recAddOrder.Fields("aff_site_id") = arrOrder(47,0)
		recAddOrder.Fields("ad_google") = arrOrder(48,0)
		recAddOrder.Fields("cart_id") = arrOrder(49,0)
		
		recAddOrder.Update
		intOrderIDNEW = recAddOrder.Fields("order_id")
	recAddOrder.Close
	Set recAddOrder = Nothing
	
	'### Update Order Product ###
	sqlProduct = "UPDATE tbl_order_product SET order_id="&intOrderIDNEW&" WHERE order_id=" & intOrderID
	conn.Execute(sqlProduct)
	
	'### Update Order Item ###
	sqlItem = "UPDATE tbl_order_item SET order_id="&intOrderIDNEW&" WHERE order_id=" & intOrderID
	conn.Execute(sqlItem)
	
	'### Update Order Activity ###
	sqlOrderAct = "UPDATE tbl_order_activity SET order_id="&intOrderIDNEW&" WHERE order_id=" & intOrderID
	conn.Execute(sqlOrderAct)
	
		'### Add Order Activity ###
	sqlInsertAct = "INSERT INTO tbl_order_activity (order_id,staff_id,comment) VALUES ("&intOrderIDNEW&",33,'Move from order: "&intOrderID&"')"
	conn.Execute(sqlInsertAct)
	
	'### Update Site Order ###
	sqlOrderSite = "UPDATE tbl_site_order SET order_id="&intOrderIDNEW&" WHERE order_id=" & intOrderID
	conn.Execute(sqlOrderSite)
	
	'### Delete Old Order ###
	sqlDelOrder = "DELETE tbl_order WHERE order_id=" & intOrderID
	conn.Execute(sqlDelOrder)
	
	function_order_move = intOrderIDNEW
END FUNCTION
%>