<%
FUNCTION function_allot_check_order(intOrderProductID)
	
	Dim sqlItem
	Dim recItem
	Dim arrItem
	Dim intCountItemSub
	Dim sqlAllot
	Dim recAllot
	Dim arrAllot
	Dim bolAllotField
	Dim dateCheckIn
	Dim dateCheckOut
	Dim bolAllotment
	Dim bolItem
	
	sqlItem = "SELECT ot.item_id,ot.order_product_id,op.product_id,ot.option_id,ot.unit,op.date_time_check_in,op.date_time_check_out"
	sqlItem = sqlItem & " FROM tbl_order_item ot, tbl_order_product op, tbl_product_option po"
	sqlItem = sqlItem & " WHERE po.option_id=ot.option_id AND op.order_product_id=ot.order_product_id AND NOT po.option_cat_id IN (39,40,43,44,45,46,47) AND op.order_product_id=" & intOrderProductID
	sqlItem = sqlItem & " ORDER BY op.date_time_check_in ASC"
	Set recItem = Server.CreateObject ("ADODB.Recordset")
	recItem.Open sqlItem, Conn,AdOpenStatic,AdLockReadOnly
		IF NOT recItem.EOF Then
			arrItem = recItem.GetRows()
			dateCheckIn = arrItem(5,0)
			dateCheckOut = arrItem(6,0)
			bolAllotment = False
			bolItem = True
		Else
			bolAllotment = True
		End IF
	recItem.Close
	Set recItem = Nothing

	IF bolItem Then '### Item Scope

		sqlAllot = "SELECT a.option_id,a.date_allotment,a.date_cut_off,(a.base_allotment+a.add_allotment-a.sol_allotment) AS allotment"
		sqlAllot = sqlAllot & " FROM tbl_allotment a"
		sqlAllot = sqlAllot & " WHERE a.status=1 AND a.option_id IN (SELECT sot.option_id FROM tbl_order_item sot WHERE sot.order_product_id="&  intOrderProductID &")"
		sqlAllot = sqlAllot & " AND a.date_allotment>=" & function_date_sql(Day(dateCheckIn),Month(dateCheckIn),Year(dateCheckIn),1) & " AND a.date_allotment<" & function_date_sql(Day(dateCheckOut),Month(dateCheckOut),Year(dateCheckOut),1) & "AND date_cut_off>" & function_date_sql(Day(dateCurrentConstant),Month(dateCurrentConstant),Year(dateCurrentConstant),1)
	
		Set recAllot = Server.CreateObject ("ADODB.Recordset")
		recAllot.Open sqlAllot, Conn,AdOpenStatic,AdLockReadOnly
			IF NOT recAllot.EOF Then
				arrAllot = recAllot.GetRows()
				bolAllotField = True
			Else
				bolAllotField = False
			End IF
		recAllot.Close
		Set recAllot = Nothing
	
		IF bolAllotField Then
			For intCountItemSub=0 To Ubound(arrItem,2)
				bolAllotment = function_allot_check_valid(arrItem(2,intCountItemSub),arrItem(3,intCountItemSub),arrAllot,arrItem(5,intCountItemSub),arrItem(6,intCountItemSub),arrItem(4,intCountItemSub),1)
				IF NOT bolAllotment Then
					EXIT FOR
				End IF
			Next
		Else
			bolAllotment = False
		End IF
		
	End IF '### Item Scope
	
	function_allot_check_order = bolAllotment
	
END FUNCTION
%>