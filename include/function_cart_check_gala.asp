<%
Function function_cart_check_gala(intDayIn,intMonthIn,intYearIn,intDayOut,intMonthOut,intYearOut,intProductID,intCartProductID,intAdult,intChild)

	Dim dateCheckIn
	Dim dateCheckOut
	Dim sqlGalaChristAdult
	Dim recGalaChristAdult
	Dim sqlGalaChristChild
	Dim recGalaChristchild
	Dim bolGalaChristChild
	Dim sqlGalaNewAdult
	Dim recGalaNewAdult
	Dim sqlGalaNewChild
	Dim recGalaNewchild
	Dim bolGalaNewChild
	Dim intGalaID
	Dim sqlCartItem
	Dim recCartItem
	Dim arrCartItem
	Dim intCountItem
	Dim intItemID
	Dim intGalaQty
	
	dateCheckIn = DateSerial(intYearIn,intMonthIn,intDayIn)
	dateCheckOut = DateSerial(intYearOut,intMonthOut,intDayOut)

	sqlCartItem = "SELECT ci.cart_item_id,ci.option_id,ci.quantity"
	sqlCartItem = sqlCartItem & " FROM tbl_cart_item ci"
	sqlCartItem = sqlCartItem & " WHERE ci.cart_product_id=" & intCartProductID
	
	Set recCartItem = Server.CreateObject ("ADODB.Recordset")
	recCartItem.Open sqlCartItem, Conn,adOpenStatic,adLockreadOnly
		arrCartItem = recCartItem.GetRows()
	recCartItem.Close
	SET recCartItem = Nothing
	
	'### Christmas Check ###
	IF (datecheckIn<=DateSerial(Year(Date),12,24) AND datecheckOut>DateSerial(Year(Date),12,24)) Then 
		
		bolGalaChristChild = False

		sqlGalaChristAdult = "SELECT option_id,title_en FROM tbl_product_option WHERE option_cat_id=47 AND status=1 AND (title_en LIKE '%christ%' OR title_en LIKE '%mas%') AND product_id=" & intProductID & " order BY option_id ASC"

			Set recGalaChristAdult = Server.CreateObject ("ADODB.Recordset")
			recGalaChristAdult.Open sqlGalaChristAdult, Conn,adOpenStatic,adLockreadOnly
				
				IF NOT recGalaChristAdult.EOF Then 'ORDER GALA
					
					IF recGalaChristAdult.RecordCount>1 Then 'Find Adult Gala ID
						bolGalaChristChild = True
						recGalaChristAdult.MoveFirst
						While NOT recGalaChristAdult.EOF
							IF NOT InStr(1,recGalaChristAdult.Fields("title_en"), "child",1)>0  Then
								intGalaID = recGalaChristAdult.Fields("option_id")
							End IF
							recGalaChristAdult.MoveNext
						Wend
					Else
						intGalaID = recGalaChristAdult.Fields("option_id")
						bolGalaChristChild = False
					End IF
			recGalaChristAdult.Close
			Set recGalaChristAdult = Nothing 
			
				'### Find New Quantity And Check###
				For intCountItem=0 To Ubound(arrCartItem,2)
					IF Cstr(arrCartItem(1,intCountItem)) = Cstr(intGalaID) Then
							intItemID = arrCartItem(0,intCountItem)
							intGalaQty = Request.Form("qty_" &intItemID )
						Exit For
					End IF
				Next
				
				IF intGalaQty<intAdult Then
					function_cart_check_gala = "error10"
				End IF
				'### Find New Quantity And Check###

		End IF 'ORDER GALA

		IF bolGalaChristChild AND Int(intChild) >0 Then 'Require Gala For Children
					
			sqlGalaChristChild = "SELECT TOP 1 * FROM tbl_product_option WHERE option_cat_id=47 AND status=1 AND (title_en LIKE '%mas%' AND title_en LIKE '%chil%') AND product_id=" & intProductID
			Set recGalaChristChild = Server.CreateObject ("ADODB.Recordset")
			recGalaChristChild.Open sqlGalaChristChild, Conn,adOpenStatic,adLockreadOnly
	
			IF NOT recGalaChristChild.EOF Then 'ORDER GALA
				intGalaID = recGalaChristChild.Fields("option_id")
				For intCountItem=0 To Ubound(arrCartItem,2)
					IF Cstr(arrCartItem(1,intCountItem)) = Cstr(intGalaID) Then
							intItemID = arrCartItem(0,intCountItem)
							intGalaQty = Request.Form("qty_" &intItemID )
						Exit For
					End IF
				Next
			End IF
			
				IF intGalaQty<intChild Then
					function_cart_check_gala = "error11"
				End IF
			
			recGalaChristChild.Close
			Set recGalaChristChild = Nothing 
				
		End IF 'Require Gala For Children

	End IF
	'### Christmas Check ###
	
	'### New Year Check ###
	IF (datecheckIn<=DateSerial(Year(Date),12,31) AND datecheckOut>DateSerial(Year(Date),12,31)) Then 

		bolGalaNewChild = False

		sqlGalaNewAdult = "SELECT option_id,title_en FROM tbl_product_option WHERE option_cat_id=47 AND status=1 AND (title_en LIKE '%New%' AND title_en NOT LIKE '%Chinese%' ) AND product_id=" & intProductID & " order BY option_id ASC"
			Set recGalaNewAdult = Server.CreateObject ("ADODB.Recordset")
			recGalaNewAdult.Open sqlGalaNewAdult, Conn,adOpenStatic,adLockreadOnly
				
				IF NOT recGalaNewAdult.EOF Then 'ORDER GALA
					
					IF recGalaNewAdult.RecordCount>1 Then 'Find Adult Gala ID
						bolGalaNewChild = True
						recGalaNewAdult.MoveFirst
						While NOT recGalaNewAdult.EOF
							IF NOT InStr(1,recGalaNewAdult.Fields("title_en"), "child",1)>0  Then
								intGalaID = recGalaNewAdult.Fields("option_id")
							End IF
							recGalaNewAdult.MoveNext
						Wend
					Else
						intGalaID = recGalaNewAdult.Fields("option_id")
						bolGalaNewChild = False
					End IF
			recGalaNewAdult.Close
			Set recGalaNewAdult = Nothing 

				'### Find New Quantity And Check###
				For intCountItem=0 To Ubound(arrCartItem,2)
					IF Cstr(arrCartItem(1,intCountItem)) = Cstr(intGalaID) Then
							intItemID = arrCartItem(0,intCountItem)
							intGalaQty = Request.Form("qty_" &intItemID )
						Exit For
					End IF
				Next

				IF intGalaQty<intAdult Then
					function_cart_check_gala = "error12"
				End IF
				'### Find New Quantity And Check###

		End IF 'ORDER GALA

		IF bolGalaNewChild AND Int(intChild) >0 Then 'Require Gala For Children
					
			sqlGalaNewChild = "SELECT TOP 1 * FROM tbl_product_option WHERE option_cat_id=47 AND status=1 AND (title_en LIKE '%new%' AND title_en LIKE '%chil%' AND title_en NOT LIKE '%Chinese%' ) AND product_id=" & intProductID
			Set recGalaNewChild = Server.CreateObject ("ADODB.Recordset")
			recGalaNewChild.Open sqlGalaNewChild, Conn,adOpenStatic,adLockreadOnly
	
			IF NOT recGalaNewChild.EOF Then 'ORDER GALA
				intGalaID = recGalaNewChild.Fields("option_id")
				For intCountItem=0 To Ubound(arrCartItem,2)
					IF Cstr(arrCartItem(1,intCountItem)) = Cstr(intGalaID) Then
							intItemID = arrCartItem(0,intCountItem)
							intGalaQty = Request.Form("qty_" &intItemID )
						Exit For
					End IF
				Next
			End IF
			
				IF intGalaQty<intChild Then
					function_cart_check_gala = "error13"
				End IF
			
			recGalaNewChild.Close
			Set recGalaNewChild = Nothing 
				
		End IF 'Require Gala For Children

	End IF
	'### New Year Check ###
	
End Function
%>