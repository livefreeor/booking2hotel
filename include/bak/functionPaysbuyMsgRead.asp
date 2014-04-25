<%
Function functionPaysbuyMsgRead(strQuery,intType)
	Select Case intType
		Case 1 'Check Result
			functionPaysbuyMsgRead=mid(strQuery,1,2)
		Case 2 'Invoice ID
			functionPaysbuyMsgRead=mid(strQuery,3,len(strQuery))
	End Select
End Function
%>