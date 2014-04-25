<%
Function functionCheckPaymentResult(strMsg,intBank)
Dim bolCheck

bolCheck=false

Select Case intBank
	Case 1 'Kbank
		IF Mid(strMsg,1,2)="00" Then
			bolCheck=true
		End IF
	Case 2 'Boa
		IF Mid(strMsg, 77, 2)="00" Then
			bolCheck=true
		End IF
	Case 3 ' BBL
		IF instr(strMsg,"successcode=0")>0 Then
			bolCheck=true
		End IF
End Select

functionCheckPaymentResult=bolCheck
End Function
%>