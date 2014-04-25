<%
FUNCTION function_display_destination_sightseeing(intDestinationID,intType)

	Dim strReturn
	Dim sqlActivity
	Dim recActivity
	Dim arrActivity
	Dim bolActivity
	Dim intCountActivity
	Dim intCountBlank
	Dim intCoutRow
	Dim intAllRow
	Dim intAllActivity
	Dim intActivityCount
	
	sqlActivity = "SELECT p.product_id,p.product_cat_id,p.title_en,p.files_name,d.title_en"
	sqlActivity = sqlActivity & " FROM tbl_product p, tbl_destination d"
	sqlActivity = sqlActivity & " WHERE p.product_cat_id=34 AND p.destination_id=d.destination_id AND p.status=1 AND p.destination_id="&intDestinationID&" AND p.product_cat_id IN (34,35,36,37,38)"
	sqlActivity = sqlActivity & " ORDER BY p.product_cat_id ASC, p.title_en ASC"

	Set recActivity = Server.CreateObject ("ADODB.Recordset")
	recActivity.Open sqlActivity, Conn,adOpenStatic, adLockReadOnly
		IF NOT recActivity.EOF Then
			bolActivity = True
			arrActivity = recActivity.GetRows()
			intAllRow = (Ubound(arrActivity,2)+1)/4 + 1
			intAllActivity = Ubound(arrActivity,2) + 1
		Else
			bolActivity = False
		End IF
	recActivity.Close
	Set recActivity = Nothing
	
	strReturn = strReturn & "<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""2"" id=""hotels_list"">" & VbCrlf
	
	For intCoutRow=1 To intAllRow
	
		strReturn = strReturn & "<tr>" & VbCrlf
		
		For intCountActivity = 1 To 4
			intActivityCount = intActivityCount + 1
			IF intActivityCount=<intAllActivity Then
				strReturn = strReturn & "<td><li> <a href="""&function_generate_sightseeing_link(intDestinationID,"",1) & "/" & arrActivity(3,intActivityCount-1)&""">" & arrActivity(2,intActivityCount-1) & "</a></li></td>" & VbCrlf
			Else
				EXIT For
			End IF
		Next
		
		IF intCountActivity<4 Then
			For intCountBlank=intCountActivity To 4
				strReturn = strReturn & "<td>&nbsp;</td>" & VbCrlf
			Next
		End IF
		
		strReturn = strReturn & "</tr>" & VbCrlf
	
	Next

	strReturn = strReturn & "</table>" & VbCrlf

	function_display_destination_sightseeing = strReturn
	
END FUNCTION
%>