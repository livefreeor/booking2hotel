<%
Sub fnRecentShow(intType,intDestinationID)
	
	Dim sqlProduct
	Dim recProduct
	Dim strFilePath
	
	IF Session("recent")<>"" Then
	
		sqlProduct = "SELECT files_name,title_en,destination_id FROM tbl_product WHERE product_id IN ("& Session("recent") &")"
		
		Set recProduct = Server.CreateObject ("ADODB.Recordset")
		recProduct.Open SqlProduct, Conn,adOpenForwardOnly, adLockReadOnly
		
		SELECT CASE intType
			Case 1
			
				Response.Write "<div align='center'><table width='100%' cellpadding='2' cellspacing='1' bgcolor='#0066FF'>" & VbCrlf
				Response.Write "<tr>" & VbCrlf
				Response.Write "<td><font color='#FFFFFF'><b>Recently Viewed Hotels</b></font></td>" & VbCrlf
				Response.Write "</tr>" & VbCrlf
				Response.Write "<tr>" & VbCrlf
				Response.Write "<td bgcolor='#FFFFFF'>" & VbCrlf
  
    				While NOT recProduct.EOF
				
						IF Cstr(recProduct.Fields("destination_id")) = Cstr(intDestinationID) Then
							strFilePath = "./" & recProduct.Fields("files_name")
						Else
							strFilePath = "../"  & fnDestinationString(recProduct.Fields("destination_id"),1) & "/" & recProduct.Fields("files_name")
						End IF

						Response.Write "<li><a href='"& strFilePath &"' class='hotel'>"& recProduct.Fields("title_en") &"</a><br>"
						
					recProduct.MoveNext
				Wend
		END SELECT
		
		recProduct.Close
		Set recProduct = Nothing
		
		Response.Write "</td>" & VbCrlf
		Response.Write "</tr>" & VbCrlf
		Response.Write "<form action='../recent.asp' method=get><tr>" & VbCrlf
		Response.Write "<td bgcolor='#FFFFFF' align='center'>" & VbCrlf
		Response.Write "<input type='hidden' name='sort' value='destination'>" & VbCrlf		
		Response.Write "<input type='submit' class='BlueBtn' name='Compare' value='Compare'>" & VbCrlf
		Response.Write "</td>" & VbCrlf
		Response.Write "</tr></form>" & VbCrlf
		Response.Write "</table></div>" & VbCrlf	
	End IF
	
End Sub
%>

