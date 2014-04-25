<%
FUNCTION function_gen_list_sql(strSql,strQuery,strURL,intType)

	Dim sqlList
	Dim arrList
	Dim recList
	Dim intList
	Dim intCount
	Dim bolList
	Dim strJavaLink
	
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
	
	IF bolList=1 Then
		SELECT CASE intType
			
			Case 1 'HTML Link
				
				IF (INSTR("?",strURL)>0) Then
					strURL = strURL & "&"
				Else
					strURL = strURL & "?"
				End IF
				
				For intCount=0 To intList
					function_gen_list_sql = function_gen_list_sql & "<li> <a href="""& strURL & strQuery & "=" & arrList(0,intCount) &""">" & arrList(1,intCount) & "</a> <br>" & VbCrlf
				Next
						
			Case 2 'Only Text Non Link
				
				IF (INSTR("?",strURL)>0) Then
					strURL = strURL & "&"
				Else
					strURL = strURL & "?"
				End IF
				
				For intCount=0 To intList
					function_gen_list_sql = function_gen_list_sql & "<li> "& arrList(0,intCount) & " <br>" & VbCrlf
				Next
			
			Case 3	'Java Link
				IF Ubound(arrList,1)<2 Then
					For intCount=0 To intList
						strJavaLink = Replace(strURL,"replace_id",arrList(0,intCount))
						function_gen_list_sql = function_gen_list_sql & "<li> <a href="""& strJavaLink &""">" & arrList(1,intCount) & "</a> <br>" & VbCrlf
					Next
				Else
					For intCount=0 To intList
						IF arrList(2,intCount) Then
							strJavaLink = Replace(strURL,"replace_id",arrList(0,intCount))
							function_gen_list_sql = function_gen_list_sql & "<li> <a href="""& strJavaLink &""">" & arrList(1,intCount) & "</a> <br>" & VbCrlf
						Else
							strJavaLink = Replace(strURL,"replace_id",arrList(0,intCount))
							function_gen_list_sql = function_gen_list_sql & "<li> <a href="""& strJavaLink &"""><font color=""#CCCCCC"">" & arrList(1,intCount) & "</font></a> <br>" & VbCrlf
						End IF
					Next
				End IF
				
			Case 4 'Java Link with status
				For intCount=0 To intList
					strJavaLink = Replace(strURL,"replace_id",arrList(0,intCount))
					IF arrList(2,intCount) Then
						function_gen_list_sql = function_gen_list_sql & "<li> <a href="""& strJavaLink &""">" & arrList(1,intCount) & "</a> <br>" & VbCrlf
					Else
						function_gen_list_sql = function_gen_list_sql & "<li> <a href="""& strJavaLink &"""><font color=""#CCCCCC"">" & arrList(1,intCount) & "</font></a> <br>" & VbCrlf
					End IF
				Next
				
			Case 5 'HTML Link With New Window
				
				IF (INSTR("?",strURL)>0) Then
					strURL = strURL & "&"
				Else
					strURL = strURL & "?"
				End IF
				
				For intCount=0 To intList
					IF arrList(2,intCount) Then
						function_gen_list_sql = function_gen_list_sql & "<li> <a href="""& strURL & strQuery & "=" & arrList(0,intCount) &""" target=""_blank"">" & arrList(1,intCount) & "</a> <br>" & VbCrlf
					Else
						function_gen_list_sql = function_gen_list_sql & "<li> <a href="""& strURL & strQuery & "=" & arrList(0,intCount) &""" target=""_blank""><font color=""#CCCCCC"">" & arrList(1,intCount) & "</font></a> <br>" & VbCrlf
					End IF
				Next
				
		END SELECT
	Else
		function_gen_list_sql = function_gen_list_sql & "<div align=""center"">N/A</div>"
	End IF
	
END FUNCTION
%>