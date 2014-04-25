<%
Function function_destroy_script(strMessage)
	Dim intEnd
	Dim strReturn
	IF Not ISNULL(strMessage) Then
		intEnd=instr(strMessage,"<!--")
		IF intEnd=0 Then
			strReturn=strMessage
		Else
			'strReturn=mid(strMessage,1,intEnd-1)
			strReturn=Right(strMessage,Len(strMessage)-intEnd-3)
		End IF
	Else
		strReturn=" "
	End IF
	function_destroy_script=Replace(strReturn,"'","&acute;")
End Function
%>