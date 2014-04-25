<%
FUNCTION function_display_day_trips_popular(intDestinationID,intType)

	Dim sqlPop
	Dim recPop
	Dim arrPop
	Dim intWidth
	Dim bolPop
	
	SELECT CASE intType
		Case 1 'Thailand day Trips Page
			sqlPop = "SELECT TOP 4 product_id,destination_id,files_name,title_en,product_code"
			sqlPop = sqlPop & " FROM tbl_product "
			sqlPop = sqlPop & " WHERE destination_id="&intDestinationID&" AND product_cat_id=34"
			sqlPop = sqlPop & " ORDER BY point DESC"
			Set recPop = Server.CreateObject ("ADODB.Recordset")
			recPop.Open SqlPop, Conn,adOpenStatic,adLockreadOnly
				IF NOT recPop.EOF Then
					bolPop = True
					arrPop = recOp.GetRows()
				Else
					bolPop = False
				End IF
			recPop.Close
			Set recPop = Nothing

		Case 2
		Case 999 'Temp
	END SELECT

END FUNCTION
%>