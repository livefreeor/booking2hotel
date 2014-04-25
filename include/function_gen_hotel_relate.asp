<%
FUNCTION function_gen_hotel_relate(intProduct,intLocation,intStar,intType)

	Dim sqlRelate
	Dim recRelate
	Dim arrRelate
	Dim bolRelate
	Dim intCount
	
	sqlRelate = "st_hotel_detail_relate " & intProduct & ", "& intLocation & ",'" & intStar & "'"

	Set recRelate = Server.CreateObject ("ADODB.Recordset")
	recRelate.Open SqlRelate, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRelate.EOF Then
			arrRelate = recRelate.GetRows()
			bolRelate = True
		End IF
	recRelate.Close
	Set recRelate = Nothing 

	SELECT CASE Cint(intType)
		Case 1
			IF bolRelate Then
				For intCount=0 To Ubound(arrRelate,2)
					Response.Write "<li><a href=""./"& arrRelate(1,intCount) &""">"&arrRelate(0,intCount)&"</a></li>"
				Next
			End IF
		Case 2
			IF bolRelate Then
				For intCount=0 To Ubound(arrRelate,2)
					Response.Write "<li><a href=""/" & function_generate_hotel_link(arrRelate(2,0),"",1) & "/" & arrRelate(1,intCount) &""">"&arrRelate(0,intCount)&"</a></li>"
				Next
			End IF
	END SELECT
END FUNCTION
%>