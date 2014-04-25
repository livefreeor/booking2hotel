<%
FUNCTION function_display_destination_spa(intDestinationID,strAnchor,intType)

	Dim strReturn
	Dim sqlSpa
	Dim recSpa
	Dim arrSpa
	Dim bolSpa
	Dim intCountSpa
	Dim intCountBlank
	Dim intCoutRow
	Dim intAllRow
	Dim intAllSpa
	Dim intSpaCount
	
	sqlSpa = "SELECT p.product_id,p.product_cat_id,p.title_en,p.files_name,d.title_en"
	sqlSpa = sqlSpa & " FROM tbl_product p, tbl_destination d"
	sqlSpa = sqlSpa & " WHERE p.destination_id=d.destination_id AND p.status=1 AND p.destination_id="&intDestinationID&" AND p.product_cat_id IN (40)"
	sqlSpa = sqlSpa & " ORDER BY p.product_cat_id ASC, p.title_en ASC"

	Set recSpa = Server.CreateObject ("ADODB.Recordset")
	recSpa.Open sqlSpa, Conn,adOpenStatic, adLockReadOnly
		IF NOT recSpa.EOF Then
			bolSpa = True
			arrSpa = recSpa.GetRows()
			intAllRow = (Ubound(arrSpa,2)+1)/4 + 1
			intAllSpa = Ubound(arrSpa,2) + 1
		Else
			bolSpa = False
		End IF
	recSpa.Close
	Set recSpa = Nothing
	
	strReturn = strReturn & "<table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#F0EFFE"">" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td><strong><font color=""#00589F"" class=""l1""> All Spa in "&arrSpa(4,0)&"</font></strong></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
	
	For intCoutRow=1 To intAllRow
	
		strReturn = strReturn & "<tr>" & VbCrlf
		
		For intCountSpa = 1 To 4
			intSpaCount = intSpaCount + 1
			IF intSpaCount=<intAllSpa Then
				strReturn = strReturn & "<td><li> <a href="""&arrSpa(3,intSpaCount-1)&""">" & arrSpa(2,intSpaCount-1) & "</a></li></td>" & VbCrlf
			Else
				EXIT For
			End IF
		Next
		
		IF intCountSpa<4 Then
			For intCountBlank=intCountSpa To 4
				strReturn = strReturn & "<td>&nbsp;</td>" & VbCrlf
			Next
		End IF
		
		strReturn = strReturn & "</tr>" & VbCrlf
	
	Next
	
	strReturn = strReturn & "<tr>"
	strReturn = strReturn & "<td colspan=""4"" align=""right""><a href=""#"&strAnchor&"""><font color=""FE5400"">Back To Top</font></a>"
	strReturn = strReturn & "</td>"
	strReturn = strReturn & "</tr>"
	strReturn = strReturn & "</table></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "</table>" & VbCrlf

	function_display_destination_spa = strReturn

END FUNCTION
%>


