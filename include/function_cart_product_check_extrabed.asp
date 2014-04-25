<%
Function function_cart_product_check_extrabed(intCartProductID)

	Dim bolError
	Dim sqlCheck
	Dim recCheck
	Dim arrCheck
	Dim intCountCheck
	Dim intExtaAllow
	Dim strExtraID
	Dim arrExtraID
	Dim intExtraRequest
	
	bolError = False
	intExtaAllow = 0
	intExtraRequest = 0
	strExtraID = ""
	
	sqlCheck = "SELECT ci.option_id,ci.quantity,po.option_cat_id,extra_bed,ci.cart_item_id"
	sqlCheck = sqlCheck & " FROM tbl_cart_product cp, tbl_cart_item ci, tbl_product_option po"
	sqlCheck = sqlCheck & " WHERE po.option_id=ci.option_id AND ci.cart_product_id=cp.cart_product_id AND cp.cart_product_id=" & intCartProductID
	
	Set recCheck  = Server.CreateObject ("ADODB.Recordset")
	recCheck.Open SqlCheck, Conn,adOpenStatic,adLockreadOnly
		arrCheck = recCheck.GetRows()
	recCheck.Close
	Set recCheck = Nothing 
	
	For intCountCheck=0 To Ubound(arrCheck,2)
		IF arrCheck(2,intCountCheck)=38 Then '### Type= Room ###
			intExtaAllow = intExtaAllow + (arrCheck(1,intCountCheck) * arrCheck(3,intCountCheck))
		End IF
		
		IF arrCheck(2,intCountCheck)=39 Then '### Type= Extrabed ###
			strExtraID = strExtraID & arrCheck(4,intCountCheck) & ","
		End IF
	Next
	
	IF strExtraID <> "" Then
		strExtraID = Left(strExtraID,Len(strExtraID)-1)
		arrExtraID = Split(strExtraID,",")
		
		For intCountCheck=0 To Ubound(arrExtraID)
			intExtraRequest = intExtraRequest + Cint(Request.Form("qty_" & arrExtraID(intCountCheck)))
		Next
	End IF
	
	IF intExtraRequest>intExtaAllow  Then
		bolError = True
	End IF
	
	function_cart_product_check_extrabed = bolError
	
End Function
%>