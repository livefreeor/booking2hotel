<%
FUNCTION fnAlternative (intStar, intDestinationID, intProductID,intAlter1,intAlter2)
	
	Dim sqlAlt
	Dim recAlt
	Dim strAlt1
	Dim strAlt2
	Dim strAlt1Select 
	Dim strAlt2Select 
	
	sqlAlt = "SELECT TOP 3 p.product_id , title_en "
	sqlAlt = sqlAlt & " FROM tbl_product p, tbl_product_location pl"
	sqlAlt = sqlAlt & " WHERE p.status=1 AND p.star="& intstar &" AND p.destination_id="& intdestinationID &" AND p.product_id<>"& intProductID &" AND p.product_id=pl.product_id AND pl.location_id IN (SELECT location_id FROM tbl_product_location WHERE product_id="& intProductID &")"

	Set recAlt= Server.CreateObject ("ADODB.Recordset")
	recAlt.Open SqlAlt, Conn,adOpenForwardOnly, adLockReadOnly
	
		IF NOT recAlt.EOF Then
		
			strAlt1 = "<select name=""alternative1"">"
			strAlt1 = strAlt1 & "<option value="""">Alternative 1&nbsp;</option>"
			strAlt1 = strAlt1 & ""
			strAlt1 = strAlt1 & ""
			strAlt1 = strAlt1 & ""
			
			While NOT recAlt.EOF
				IF Cstr(recAlt.Fields("product_id"))=Cstr(intAlter1) Then
					strAlt1Select = "selected"
				Else
					strAlt1Select = ""
				End IF
				strAlt1 = strAlt1 & "<option value='"& recAlt.Fields("product_id") &"' "& strAlt1Select &">"&recAlt.fields("title_en")&"</option>"
				recAlt.MoveNext
			Wend
			strAlt1 = strAlt1 & "</select>"
			
			strAlt2 = "<select name=""alternative2"">"
			strAlt2 = strAlt2 & "<option value="""">Alternative 2&nbsp;</option>"
			strAlt2 = strAlt2 & ""
			strAlt2 = strAlt2 & ""
			strAlt2 = strAlt2 & ""
			
			recAlt.MoveFirst
			While NOT recAlt.EOF
				IF Cstr(recAlt.Fields("product_id"))=Cstr(intAlter2) Then
					strAlt2Select = "selected"
				Else
					strAlt2Select = ""
				End IF
				strAlt2 = strAlt2 & "<option value='"& recAlt.Fields("product_id") &"' "& strAlt2Select &">"&recAlt.fields("title_en")&"</option>"
				recAlt.MoveNext
			Wend
			
			strAlt2 = strAlt2 & "</select>"
			
			fnAlternative = strAlt1 & "<br>" & VbCrlf & strAlt2
		
		Else
			fnAlternative = ""
		End IF
		
	recAlt.Close
	Set recAlt = Nothing
	
	
	
	
END FUNCTION
%>