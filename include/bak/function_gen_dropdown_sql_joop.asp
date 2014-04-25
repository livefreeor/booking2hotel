<%
FUNCTION function_gen_dropdown_sql(strSql,strName,strDefault)
	
	Dim sqlList
	Dim arrList
	Dim recList
	Dim intList
	Dim intCount
	Dim strSelected
	
	sqlList = strSql
	response.Write(sqlList)
	response.End()
	Set recList = Server.CreateObject ("ADODB.Recordset")
	recList.Open SqlList, Conn,1,3
		arrList = recList.GetRows()
		intList = Ubound(arrList,2)
	recList.Close
	Set recList = Nothing 
	
	function_gen_dropdown_sql = "<select name="""& strName &""" id="""& strName &""">" & VbCrlf
	
	For intCount=0 To intList
		IF Cstr(strDefault) = Cstr(arrList(0,intCount)) Then
			strSelected = "selected"
		Else
			strSelected = ""
		End IF
	
		function_gen_dropdown_sql = function_gen_dropdown_sql & "<option value="""& arrList(0,intCount) &""" "& strSelected &">"& arrList(1,intCount) &"</option>" & VbCrlf
	Next
	
	function_gen_dropdown_sql = function_gen_dropdown_sql & "</select>" & VbCrlf
END FUNCTION
%>