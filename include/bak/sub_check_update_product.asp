<%
Sub sub_check_update_product(intProduct,intType)
	Dim strField
	
	Select Case intType
	Case 1 'Product Update
		strField="update_product=1"
	Case 2 'Option
		strField="update_option=1"
	Case 3 'Price
		strField="update_price=1"
	Case 4 'Facility
		strField="update_facility=1"
	End Select
	sqlUpdate="update tbl_visa_product_update set "&strField&" where product_id="&intProduct
	conn.execute(sqlUpdate)
End Sub
%>