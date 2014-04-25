<%
Function destroy_qoute(strDetail)
	Dim arrCheck
	Dim intCheck
	arrCheck=array("'",";","--")
	For intCheck=0 to Ubound(arrCheck)
		strDetail=replace(strDetail,arrCheck(intCheck),"")
	Next
	destroy_qoute=strDetail
End Function
%>