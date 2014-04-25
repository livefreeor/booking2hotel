<%
FUNCTION function_gen_dropdown_sql_new(strSql,strName,strDefault,intType)
	
	Dim sqlList
	Dim arrList
	Dim recList
	Dim intList
	Dim intCount
	'Dim maxCountry
	

	sqlList = strSql
	
	Set recList = Server.CreateObject ("ADODB.Recordset")
	recList.Open SqlList, Conn,adOpenStatic,adLockreadOnly
		arrList = recList.GetRows()
		intList = Ubound(arrList,2)
	recList.Close
	Set recList = Nothing 
	
	'maxCountry = Ubound(arrList,2) + 1
	
	function_gen_dropdown_sql_new = "<select id="""& strName &""" name="""& strName &""">" & VbCrlf
	function_gen_dropdown_sql_new = function_gen_dropdown_sql_new & "<option value=""-1"" selected=""selected"">Select</option>" & VbCrlf

	Select Case int(intType)
		Case 1
			For intCount=0 To intList
				function_gen_dropdown_sql_new = function_gen_dropdown_sql_new & "<option value="""& arrList(0,intCount) &""">"& arrList(1,intCount) &"</option>" & VbCrlf
			Next
		Case 2

 		Case 3
		
	End Select

	function_gen_dropdown_sql_new = function_gen_dropdown_sql_new & "</select>" & VbCrlf
END FUNCTION
%>
