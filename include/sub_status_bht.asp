<%
Sub sub_status_bht()
	IF session("status_bht")<>"yes" Then
		response.Redirect("http://10.1.1.10/bht_login.asp")
	End IF
End Sub
%>