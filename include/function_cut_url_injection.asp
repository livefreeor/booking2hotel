<%
Function function_cut_url_injection(strDetail)
strDetail=lcase(strDetail)
Dim intKey
Dim intStart

arrKey=array(";","declare","and","select")
	For intKey=0 to Ubound(arrKey)
		intStart=inStr(strDetail,arrKey(intKey))
		function_cut_url_injection=mid(strDetail,1,intStart)
		Exit For
	Next
End Function
%>