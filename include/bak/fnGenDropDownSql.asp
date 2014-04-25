<%
FUNCTION fnGenDropDownSql(strSql,strName,strDefault,intType)
	
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
	
	fnGenDropDownSql = "<select name="""& strName &""" id="""& strName &""">" & VbCrlf
	
	Select Case inttype
	Case 1
		For intCount=0 To intList
			IF Cstr(strDefault) = Cstr(arrList(0,intCount)) Then
				strSelected = "selected"
			Else
				strSelected = ""
			End IF
		
			fnGenDropDownSql = fnGenDropDownSql & "<option value="""& arrList(0,intCount) &""" "& strSelected &">"& arrList(1,intCount) &"</option>" & VbCrlf
		Next
		
	Case 2
		fnGenDropDownSql = fnGenDropDownSql & "<option value=""00"">All Category</option>"
		For intCount=0 To intList
			IF Cstr(strDefault) = Cstr(arrList(0,intCount)) Then
				strSelected = "selected"
			Else
				strSelected = ""
			End IF
		
			fnGenDropDownSql = fnGenDropDownSql & "<option value="""& arrList(0,intCount) &""" "& strSelected &">"& arrList(1,intCount) &"</option>" & VbCrlf
		Next
	End Select
	fnGenDropDownSql = fnGenDropDownSql & "</select>" & VbCrlf
END FUNCTION
%>