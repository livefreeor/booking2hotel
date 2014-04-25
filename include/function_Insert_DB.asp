<%
Function function_Insert_DB(strInput,intType)
	
	strInput = Trim(strInput)
	
	SELECT CASE intType
		Case 1 ' Insert Null
			IF strInput="" OR ISNULL(strInput) Then
				strInput = NULL
			End IF
		Case 2 ' Insert ""
			IF strInput="" OR ISNULL(strInput) Then
				strInput = ""
			End IF
		Case 3 ' Insert 0
			IF strInput="" OR ISNULL(strInput) Then
				strInput = 0
			End IF
	END SELECT

	function_Insert_DB = strInput

End Function
%>