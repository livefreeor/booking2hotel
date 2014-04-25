<%
FUNCTION function_display_bol(strTrue,strFalse,bolDefault,strName,intType)
	
	Dim strTrueCheck
	Dim strFalseCheck
	
	SELECT CASE intType
		Case 1 'Radio
			IF bolDefault Then
				strTrueCheck = "checked"
			Else
				strFalseCheck = "checked"
			End IF
			function_display_bol = "<input type=""radio"" name="""& strName &""" value=""1"" "& strTrueCheck &">" & strTrue & VbCrlf
			function_display_bol = function_display_bol & "<input type=""radio"" name="""& strName &""" value=""0"" "& strFalseCheck &">" & strFalse & VbCrlf
	
	Case 2 'Normal Text
		IF NOT ISNULL(bolDefault) Then
			IF bolDefault Then
				function_display_bol = strTrue
			Else
				function_display_bol = strFalse
			End IF
		Else
			function_display_bol = "N/A"
		End IF
	END SELECT
END FUNCTION
%>