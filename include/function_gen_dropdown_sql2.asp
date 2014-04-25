<%
FUNCTION function_gen_dropdown_sql2(strSql,strName,strDefault,intType)
	
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
	
	Select Case intType
		Case 1
			function_gen_dropdown_sql = "<select name="""& strName &""">" & VbCrlf
			
			For intCount=0 To intList
				IF Cstr(strDefault) = Cstr(arrList(0,intCount)) Then
					strSelected = "selected"
				Else
					strSelected = ""
				End IF
			
				function_gen_dropdown_sql = function_gen_dropdown_sql & "<option value="""& arrList(0,intCount) &""" "& strSelected &">"& arrList(1,intCount) &"</option>" & VbCrlf
			Next
		Case 2

 		Case 3
		
	End Select
	function_gen_dropdown_sql = function_gen_dropdown_sql & "</select>" & VbCrlf
END FUNCTION
%>