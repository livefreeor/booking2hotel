<%
Function function_destroy_script(strMessage)
	Dim intEnd
	Dim strReturn
	IF Not ISNULL(strMessage) Then
		intEnd=instr(strMessage,"<ifram")
		IF intEnd<>0 Then
			response.redirect("http://www.hotels2thailand.com")
			strReturn=mid(strMessage,1,intEnd-1)
		End IF
		
	Else
		strReturn=" "
	End IF
	function_destroy_script=Replace(strReturn,"'","&acute;")
End Function
%>