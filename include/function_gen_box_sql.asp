<%
FUNCTION function_gen_box_sql(strSql,strName,arrDefault,intColum,intType)
	
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
		Case 1 'Check Box
			function_gen_box_sql = "<table width=""100%"">" & VbCrlf
			For intRowCount=1 To intRow
				function_gen_box_sql = function_gen_box_sql & "<tr>" & VbCrlf
					For intColumCount=1 To intColum
						function_gen_box_sql = function_gen_box_sql & "<td>" & VbCrlf
							IF intCount<=intList Then
								IF function_array_check(arrList(0,intCount),arrDefault,1) Then
									strChecked = "checked"
								Else
									strChecked = ""
								End IF
								function_gen_box_sql = function_gen_box_sql & "<input type=""checkbox"" name="""& strName &""" value="""& arrList(0,intCount) &""" "& strChecked &">" & arrList(1,intCount) & VbCrlf
							End IF
						function_gen_box_sql = function_gen_box_sql & "</td>" & VbCrlf
						intCount=intCount+1
					Next
				function_gen_box_sql = function_gen_box_sql & "<tr>" & VbCrlf
			Next
			function_gen_box_sql = function_gen_box_sql & "</table>" & VbCrlf
	
		Case 2 'Radio
			function_gen_box_sql = "<table width=""100%"">" & VbCrlf
			For intRowCount=1 To intRow
				function_gen_box_sql = function_gen_box_sql & "<tr>" & VbCrlf
					For intColumCount=1 To intColum
						function_gen_box_sql = function_gen_box_sql & "<td>" & VbCrlf
							IF intCount<=intList Then
								IF function_array_check(arrList(0,intCount),arrDefault,1) Then
									strChecked = "checked"
								Else
									strChecked = ""
								End IF
								function_gen_box_sql = function_gen_box_sql & "<input type=""radio"" name="""& strName &""" value="""& arrList(0,intCount) &""" "& strChecked &">" & arrList(1,intCount) & VbCrlf
							End IF
						function_gen_box_sql = function_gen_box_sql & "</td>" & VbCrlf
						intCount=intCount+1
					Next
				function_gen_box_sql = function_gen_box_sql & "<tr>" & VbCrlf
			Next
			function_gen_box_sql = function_gen_box_sql & "</table>" & VbCrlf
			
		Case 3 'Dropdown
			function_gen_box_sql = "<SELECT name="""&strName&""">"
			
			For intCount=0 To intList
			
				IF Cint(arrDefault) = Cint(arrList(0,intCount)) Then
					strChecked = "selected"
				Else
					strChecked = ""
				End IF
				
				function_gen_box_sql = function_gen_box_sql  &  "<option value="""& arrList(0,intCount) &""" "&strChecked&">"&arrList(1,intCount) &"</option>"
			Next
			
			function_gen_box_sql = function_gen_box_sql  &  "</SELECT>"
			
		Case 4 'DropDown Country
			function_gen_box_sql = "<SELECT name="""&strName&""">"
			
			function_gen_box_sql = function_gen_box_sql & "<option value=""999"">Please select your country</option>"
			
			For intCount=0 To intList
			
				IF Cint(arrDefault) = Cint(arrList(0,intCount)) Then
					strChecked = "selected"
				Else
					strChecked = ""
				End IF
				
				function_gen_box_sql = function_gen_box_sql  &  "<option value="""& arrList(0,intCount) &""" "&strChecked&">"&arrList(1,intCount) &"</option>"
			Next
			
			function_gen_box_sql = function_gen_box_sql  &  "</SELECT>"
			
	END SELECT
End IF

END FUNCTION
%>