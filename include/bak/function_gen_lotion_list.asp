<%
FUNCTION function_gen_lotion_list(intDestinationID,intType)
	
	Dim sqlList
	Dim recList
	Dim arrList
	Dim intCount
	Dim bolList
	Dim strList
	
	sqlList = "SELECT title_en,files_name FROM Tbl_location WHERE destination_id="&intDestinationID&" AND status=1 ORDER BY title_en ASC"
	Set recList = Server.CreateObject ("ADODB.Recordset")
	recList.Open SqlList, Conn,adOpenStatic,adLockreadOnly
		IF NOT recList.EOF Then
			arrList = recList.GetRows()
			bolList= True
		Else
			bolList = False
		End IF
	recList.Close
	Set recList = Nothing 
	
	IF bolList Then
	Select Case intType
	Case 1
		For intCount=0 To UBound(arrList,2)
			Response.Write "<a href=""/"& arrList(1,intCount) &""">"&arrList(0,intCount)&"</a><br>" & VbCrlf
		Next
	Case 2
		For intCount=0 To UBound(arrList,2)
			strList=strList& "<a href=""/"& arrList(1,intCount) &""">"&arrList(0,intCount)&"</a><br>" & VbCrlf
		Next
		function_gen_lotion_list=strList
	End Select
	End IF
END FUNCTION
%>