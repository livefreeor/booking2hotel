<%
SUB sub_supplier_authorize(intProductID,intType)

	Dim arrProductID
	Dim bol
	SELECT CASE intType
		Case 1 'Check Login Level
			IF NOT Session("login")=True Then
				Response.Redirect "default.asp?err=" & "login"
			End IF
		Case 2 'Check Product Level
			arrProductID = Split(Session("productID"),",")
			
			IF NOT function_array_check(intProductID,arrProductID,2) Then
				Response.Redirect "default.asp?err=" & "product"
			End IF
	END SELECT
	
END SUB
%>