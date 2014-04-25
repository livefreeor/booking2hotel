<%
FUNCTION functionGenDropDownSql(strSql,strName,strDefault,intType)
	
	Dim sqlList
	Dim arrList
	Dim recList
	Dim intList
	Dim intCount
	Dim strSelected
	
	sqlList = strSql
	
	Set recList = Server.CreateObject ("ADODB.Recordset")
	recList.Open SqlList, Conn,adOpenStatic,adLockreadOnly
		arrList = recList.GetRows()
		intList = Ubound(arrList,2)
	recList.Close
	Set recList = Nothing 
	
	functionGenDropDownSql = "<select name="""& strName &""" id="""& strName &""">" & VbCrlf
	Select Case intType
	Case 1
		functionGenDropDownSql = functionGenDropDownSql&"<option value=""0"">Select Promotion Category</option>"
	End Select
	For intCount=0 To intList
		IF Cstr(strDefault) = Cstr(arrList(0,intCount)) Then
			strSelected = "selected"
		Else
			strSelected = ""
		End IF
	
		functionGenDropDownSql = functionGenDropDownSql & "<option value="""& arrList(0,intCount) &""" "& strSelected &">"& arrList(1,intCount) &"</option>" & VbCrlf
	Next
	
	functionGenDropDownSql = functionGenDropDownSql & "</select>" & VbCrlf
END FUNCTION
%>