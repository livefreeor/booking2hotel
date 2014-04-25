<%
FUNCTION function_order_move(intOrderID,intType)

	Dim sqlOrder
	Dim recOrder
	Dim arrOrder
	Dim sqlAddOrder
	Dim recAddOrder
	Dim sqlOrderProduct
	Dim recOrderProduct
	Dim arrOrderProduct
	Dim sqlAddOrderProduct
	Dim recAddOrderProduct
	Dim sqlOrderItem
	Dim recOrderItem
	Dim arrOrderItem
	Dim sqlAddOrderItem
	Dim recAddOrderItem
	Dim sqlOrderAct
	Dim sqlOrderProductAct
	Dim intOrderIDNEW
	Dim intCountProduct
	Dim intOrderProductID
	Dim intOrderProductIDNEW
	Dim intCountItem
	
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
	
	sqlOrderProduct = "SELECT order_product_id,order_id,product_id,num_adult,num_child,num_golfer,confirm_fax_return,confirm_fax_return_time,confirm_available,confirm_available_time,confirm_check_in,confirm_check_in_time,confirm_hotel_payment,confirm_hotel_payment_time,confirm_receipt,date_time_check_in,date_time_check_out,date_time_check_in_confirm,detail,status_id,period_id"
	sqlOrderProduct = sqlOrderProduct & " FROM tbl_order_product"
	sqlOrderProduct = sqlOrderProduct & " WHERE order_id=" & intOrderID
	sqlOrderProduct = sqlOrderProduct & " ORDER BY order_product_id ASC"
	Set recOrderProduct = Server.CreateObject ("ADODB.Recordset")
	recOrderProduct.Open SqlOrderProduct, Conn,adOpenStatic,adLockreadOnly
		arrOrderProduct = recOrderProduct.GetRows()
	recOrderProduct.Close
	Set recOrderProduct = Nothing
	
	sqlAddOrderProduct = "SELECT TOP 1 * FROM tbl_order_product"
	Set recAddOrderProduct = Server.CreateObject ("ADODB.Recordset")
	recAddOrderProduct.Open SqlOrderProduct, Conn,1,3
	
	For intCountProduct = 0 To Ubound(arrOrderProduct,2)
	
		'### Move Order Product ###
		recAddOrderProduct.AddNew
		intOrderProductID = arrORderProduct(0,intCountProduct)
		recAddOrderProduct.Fields("order_id") = intOrderIDNEW
		recAddOrderProduct.Fields("product_id") = arrORderProduct(2,intCountProduct)
		recAddOrderProduct.Fields("num_adult") = arrORderProduct(3,intCountProduct)
		recAddOrderProduct.Fields("num_child") = arrORderProduct(4,intCountProduct)
		recAddOrderProduct.Fields("num_golfer") = arrORderProduct(5,intCountProduct)
		recAddOrderProduct.Fields("confirm_fax_return") = arrORderProduct(6,intCountProduct)
		recAddOrderProduct.Fields("confirm_fax_return_time") = arrORderProduct(7,intCountProduct)
		recAddOrderProduct.Fields("confirm_available") = arrORderProduct(8,intCountProduct)
		recAddOrderProduct.Fields("confirm_available_time") = arrORderProduct(9,intCountProduct)
		recAddOrderProduct.Fields("confirm_check_in") = arrORderProduct(10,intCountProduct)
		recAddOrderProduct.Fields("confirm_check_in_time") = arrORderProduct(11,intCountProduct)
		recAddOrderProduct.Fields("confirm_hotel_payment") = arrORderProduct(12,intCountProduct)
		recAddOrderProduct.Fields("confirm_hotel_payment_time") = arrORderProduct(13,intCountProduct)
		recAddOrderProduct.Fields("confirm_receipt") = arrORderProduct(14,intCountProduct)
		recAddOrderProduct.Fields("date_time_check_in") = arrORderProduct(15,intCountProduct)
		recAddOrderProduct.Fields("date_time_check_out") = arrORderProduct(16,intCountProduct)
		recAddOrderProduct.Fields("date_time_check_in_confirm") = arrORderProduct(17,intCountProduct)
		recAddOrderProduct.Fields("detail") = arrORderProduct(18,intCountProduct)
		recAddOrderProduct.Fields("status_id") = arrORderProduct(19,intCountProduct)
		recAddOrderProduct.Fields("period_id") = arrORderProduct(20,intCountProduct)
		recAddOrderProduct.Update
		intOrderProductIDNEW = recAddOrderProduct.Fields("order_product_id")
		'### Move Order Product ###
		
		'### Move Order Item ###
		sqlOrderItem = "SELECT item_id,order_id,order_product_id,option_id,unit,price_rack,price,price_display,price_display_currency,confirm_check_in,date_check_in,date_check_out,date_submit,date_modify,promotion_id,promotion_title,promotion_detail,promotion_offer_person,detail"
		sqlOrderItem = sqlOrderItem & " FROM tbl_order_item"
		sqlOrderItem = sqlOrderItem & " WHERE order_product_id=" & intOrderProductID
		sqlOrderItem = sqlOrderItem & " ORDER BY item_id ASC"
		Set recOrderItem = Server.CreateObject ("ADODB.Recordset")
		recOrderItem.Open SqlOrderItem, Conn,adOpenStatic,adLockreadOnly
			arrOrderItem = recOrderItem.GetRows()
		recOrderItem.Close
		Set recOrderItem = Nothing
		
		



		'### Move Order Item ###
		
	Next
	
	recAddOrderProduct.Close
	Set recAddOrderProduct = Nothing
	
END FUNCTION
%>