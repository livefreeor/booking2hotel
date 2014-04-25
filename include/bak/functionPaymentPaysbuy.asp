<%
Function functionPaymentPaysbuy()
Dim strPaysbuyMsg
	IF Request("result")<>"" AND NOT ISNULL(Request("result")) Then
		strPaysbuyMsg = Request("result")
		IF functionPaysbuyMsgRead(strPaysbuyMsg,1)="00" Then
			functionPaymentPaysbuy=functionPaysbuyMsgRead(strPaysbuyMsg,2)
		Else
			functionPaymentPaysbuy=0
		End IF
	Else
		functionPaymentPaysbuy=0
	End IF
End Function
%>