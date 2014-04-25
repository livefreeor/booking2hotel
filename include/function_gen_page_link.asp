<%
FUNCTION function_gen_page_link(intCurrent,intAllRecord,strURL,intType)

	Dim intPageAll
	Dim intPageCount
	Dim strNext
	Dim strPrev
	
	intCurrent = Cint(intCurrent)
	
	intAllRecord = intAllRecord + 1
	IF intAllRecord MOD ConstPageSearchMain=0 Then
		intPageAll = intAllRecord/ConstPageSearchMain
	Else
		intPageAll = (((intAllRecord)- (intAllRecord MOD ConstPageSearchMain))/ConstPageSearchMain) + 1
	End IF
	
	For intPageCount=1 To intPageAll
		IF intPageCount=intCurrent Then
			function_gen_page_link = function_gen_page_link & "<b><font color=""#CC0000"">"& intCurrent &"</font></b> "
		Else
			function_gen_page_link = function_gen_page_link & "<a href="""& strURL & "&page=" & intPageCount &""">"& intPageCount &"</a> "
		End IF
	Next

	IF intCurrent < intPageAll AND NOT (intCurrent = intPageAll) Then
		strNext = " <a href="""& strURL & "&page=" & intCurrent+1 &"""> Next <img src=""/images/arrow_next_blue_10.gif"" border=""0"" /></a>"
	End IF
	
	IF intCurrent>1 AND intCurrent <= intPageAll Then
		strPrev = "<a href="""& strURL & "&page=" & intCurrent-1 &"""> <img src=""/images/arrow_previous_blue_10.gif"" border=""0"" /> Previous</a> "
	End IF
	
	function_gen_page_link = strPrev & function_gen_page_link & strNext

END FUNCTION
%>