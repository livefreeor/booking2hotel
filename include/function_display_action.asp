<%
FUNCTION function_display_action(intType)

	Dim strOutPut

	SELECT CASE intType
		Case 1 '### Home Page ###
			strOutput = "<fieldset id=""tbl_action"">" & VbCRlf
			strOutput = strOutput & "<legend>Lastest Action</legend>" & VbCrlf
			strOutput = strOutput & "<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
			strOutput = strOutput & "<tr>" & VbCrlf
			strOutput = strOutput & "<td width=""50%"" valign=""top"">" & VbCrlf
			strOutput = strOutput & "<fieldset id=""tbl_last_review"">" & VbCrlf
			strOutput = strOutput & "<legend><strong>Lastest Review</strong></legend>" & VbCrlf
			strOutput = strOutput & function_gen_review_last(0,1) & VbCrlf
			strOutput = strOutput & "</fieldset>" & VbCrlf
			strOutput = strOutput & "</td>" & VbCrlf
			strOutput = strOutput & "<td valign=""top"">" & VbCrlf
			strOutput = strOutput & "<fieldset id=""tbl_last_forum"">" & VbCrlf
			strOutput = strOutput & "<legend><strong>Lastest Topic</strong></legend><br>" & VbCrlf
			strOutput = strOutput & function_gen_forum_last(0,1) & VbCrlf
			strOutput = strOutput & "</fieldset>" & VbCrlf
			strOutput = strOutput & "</td>" & VbCrlf
			strOutput = strOutput & "</tr>" & VbCrlf
			strOutput = strOutput & "</table>" & VbCrlf
			strOutput = strOutput & "</fieldset>" & VbCrlf

		Case 2 '### Home Page With Form Action ###
			strOutput = "<fieldset id=""tbl_action"">" & VbCRlf
			strOutput = strOutput & "<legend>Lastest Action</legend>" & VbCrlf
			strOutput = strOutput & "<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
			strOutput = strOutput & "<tr>" & VbCrlf
			strOutput = strOutput & "<td width=""50%"" valign=""top"">" & VbCrlf
			strOutput = strOutput & "<fieldset id=""tbl_last_review"">" & VbCrlf
			strOutput = strOutput & "<legend><strong>Lastest Review</strong></legend>" & VbCrlf
			strOutput = strOutput & function_gen_review_last(0,3) & VbCrlf
			strOutput = strOutput & "</fieldset>" & VbCrlf
			strOutput = strOutput & "</td>" & VbCrlf
			strOutput = strOutput & "<td valign=""top"">" & VbCrlf
			strOutput = strOutput & "<fieldset id=""tbl_last_forum"">" & VbCrlf
			strOutput = strOutput & "<legend><strong>Lastest Topic</strong></legend><br>" & VbCrlf
			strOutput = strOutput & function_gen_forum_last(0,2) & VbCrlf
			strOutput = strOutput & "</fieldset>" & VbCrlf
			strOutput = strOutput & "</td>" & VbCrlf
			strOutput = strOutput & "</tr>" & VbCrlf
			strOutput = strOutput & "</table>" & VbCrlf
			strOutput = strOutput & "</fieldset>" & VbCrlf
			
		Case 3
	END SELECT

	function_display_action = strOutput

END FUNCTION
%>