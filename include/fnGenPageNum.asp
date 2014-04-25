<%
FUNCTION fnGenPageNum(intCurrent, intTotal,URL,strVarName)
	
	Dim intCount
	
	For intCount = 1 To intTotal
		IF intCount = intCurrent Then
			fnGenPageNum = fnGenPageNum & " | <b>" & intCount & "</b>"
		Else
			fnGenPageNum = fnGenPageNum & " | <a href='"& URL &"&"& strVarName &"="& intCount &"' class='hotel'>" & intCount & "</a>"
		End IF
	Next
	
	fnGenPageNum = fnGenPageNum & " | "
	
	IF intCurrent > 1 Then 'Previous
		fnGenPageNum = "<a href='"& URL &"&"& strVarName &"="& intCurrent-1&"' class='hotel'>&lt;&lt; Previous</a>" & fnGenPageNum
	End IF
	
	IF intCurrent < intTotal Then 'Next
		fnGenPageNum = fnGenPageNum & "<a href='"& URL &"&"& strVarName &"="& intCurrent+1&"' class='hotel'>Next &gt;&gt;</a>"
	End IF
	
END FUNCTION
%>