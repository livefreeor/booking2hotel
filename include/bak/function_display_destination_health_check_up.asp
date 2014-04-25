<%
FUNCTION function_display_destination_health_check_up(intDestinationID,strAnchor,intType)

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
	sqlActivity = sqlActivity & " WHERE p.destination_id=d.destination_id AND p.status=1 AND p.destination_id="&intDestinationID&" AND p.product_cat_id IN (39)"
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
	
	strReturn = strReturn & "<table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""1"" bgcolor=""#F0EFFE"">" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td><strong><font color=""#00589F"" class=""l1""> All Health Check Up in "&arrActivity(4,0)&"</font></strong></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td bgcolor=""#FFFFFF""><table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
	
	For intCoutRow=1 To intAllRow
	
		strReturn = strReturn & "<tr>" & VbCrlf
		
		For intCountActivity = 1 To 4
			intActivityCount = intActivityCount + 1
			IF intActivityCount=<intAllActivity Then
				strReturn = strReturn & "<td><li> <a href="""&arrActivity(3,intActivityCount-1)&""">" & arrActivity(2,intActivityCount-1) & "</a></li></td>" & VbCrlf
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
	
	strReturn = strReturn & "<tr>"
	strReturn = strReturn & "<td colspan=""4"" align=""right""><a href=""#"&strAnchor&"""><font color=""FE5400"">Back To Top</font></a>"
	strReturn = strReturn & "</td>"
	strReturn = strReturn & "</tr>"
	strReturn = strReturn & "</table></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "</table>" & VbCrlf

	function_display_destination_health_check_up = strReturn

END FUNCTION
%>


