<%
FUNCTION function_gen_link_sql(strSql,strName,strLink,intColum,intType)
	
	Dim sqlList
	Dim arrList
	Dim recList
	Dim intList
	Dim intCount
	Dim bolList
	Dim intRow
	Dim intRowCount
	Dim intColumCount
	Dim strChecked
	Dim strRealLink
	
	sqlList = strSql
	
	Set recList = Server.CreateObject ("ADODB.Recordset")
	recList.Open SqlList, Conn,adOpenStatic,adLockreadOnly
		IF NOT recList.EOF Then
			arrList = recList.GetRows()
			intList = Ubound(arrList,2)
			bolList = 1
		Else
			bolList = 0
		End IF
	recList.Close
	Set recList = Nothing 

	IF (intList+1) MOD intColum > 0 Then
		intRow = ((intList+1)+ (intColum-((intList+1) MOD intColum)))/intColum
	Else
		intRow = (intList+1)/intColum
	End IF

IF bolList=1 Then
	SELECT CASE intType
		Case 1
			function_gen_link_sql = "<table width=""100%"">" & VbCrlf
			For intRowCount=1 To intRow
				function_gen_link_sql = function_gen_link_sql & "<tr>" & VbCrlf
					For intColumCount=1 To intColum
						function_gen_link_sql = function_gen_link_sql & "<td>" & VbCrlf
							IF intCount<=intList Then
								strRealLink = Replace(strLink,"replace_id",arrList(0,intCount))
								function_gen_link_sql = function_gen_link_sql & "<a href="""& strRealLink &""">" & arrList(1,intCount) & "</a>" & VbCrlf
							End IF
						function_gen_link_sql = function_gen_link_sql & "</td>" & VbCrlf
						intCount=intCount+1
					Next
				function_gen_link_sql = function_gen_link_sql & "<tr>" & VbCrlf
			Next
			function_gen_link_sql = function_gen_link_sql & "</table>" & VbCrlf

		Case 2
	END SELECT
End IF

END FUNCTION
%>