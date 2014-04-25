<%
Function function_mid_string_rich_box(strDetail)
	IntStart=InStr(Lcase(strDetail),"<body>") +6
	IntEnd=InStr(Lcase(strDetail),"</body>") -1
	strDetail=Mid(strDetail,IntStart,IntEnd-IntStart)
	function_mid_string_rich_box=(strDetail)
End Function
%>