<%
Sub subAllotmentUse(intOrderProductID)

	Dim sqlItem
	Dim recItem
	Dim arrItem
	Dim intCount
	Dim sqlUpdate
	
	sqlItem = "SELECT ot.option_id,ot.unit,op.date_time_check_in,op.date_time_check_out"
	sqlItem = sqlItem & " FROM tbl_order_item ot, tbl_order_product op"
	sqlItem = sqlItem & " WHERE op.order_product_id=ot.order_product_id AND op.order_product_id=" & intOrderProductID
	sqlItem = sqlItem & " ORDER BY op.date_time_check_in ASC"
	Set recItem = Server.CreateObject ("ADODB.Recordset")
	recItem.Open sqlItem, Conn,AdOpenStatic,AdLockReadOnly
		arrItem = recItem.GetRows()
	recItem.Close
	Set recItem = Nothing

	For intCount=0 To Ubound(arrItem,2)
		sqlUpdate = "UPDATE tbl_allotment SET sol_allotment=sol_allotment+"&arrItem(1,intCount)&" WHERE option_id="&arrItem(0,intCount)&" AND date_allotment>="&function_date_sql(Day(arrItem(2,intCount)),Month(arrItem(2,intCount)),Year(arrItem(2,intCount)),1)&" AND date_allotment<" &function_date_sql(Day(arrItem(3,intCount)),Month(arrItem(3,intCount)),Year(arrItem(3,intCount)),1)
		conn.Execute(sqlUpdate)
	Next

End Sub
%>