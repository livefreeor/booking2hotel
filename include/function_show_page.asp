<%
Function function_show_page(intRec,intPagesize,page_num,strQuery)
Dim intList
Dim total_page
Dim i
Dim pagelist
Dim bgDefault
Dim bgActive
'bgDefault="style='border:1px solid #4DACF0' bgcolor='#D0E9FD' width='18' onmouseover=""bgColor='#f5f5f5'"" onmouseout=""bgColor='#D0E9FD'"""
'bgActive=" style='border:1px solid #AA0004' bgcolor='#ffffff' "
page_num=int(page_num)
	intList=intRec
	total_page=intList\intPagesize
	IF total_page=0 Then
		total_page=1
	Else
		IF intList mod intPagesize<>0 Then
			total_page=total_page+1
		End IF
	End IF
	IF strQuery="" Then
		strQuery=request.ServerVariables("SCRIPT_NAME")
	End IF
	IF inStr(strQuery,"?")<>0 Then
		strQuery=strQuery&"&"
	Else
		strQuery=strQuery&"?"
	End IF
	'response.write intList mod intPagesize
	pagelist= "<table id=""sel_page""><tr>"
	IF (page_num>1) Then
		pagelist=pagelist& "<td><a href='"&strQuery&"page_num="&page_num-1&"'><font color=""#999999"">Previous</font> </a></td>"
	End IF	
	
	For i=1 to total_page
		IF i=page_num Then
				pagelist=pagelist& "<td align='center'><a href='"&strQuery&"page_num="&i&"'><strong><font color=""red"">"&i&"</font></strong></a></td>"
		Else
			pagelist=pagelist& "<td align='center'><a href='"&strQuery&"page_num="&i&"'><strong>"&i&"</strong></a></td>"
		End IF
	Next
	
	IF ((page_num=1) or (page_num<total_page)) and total_page<>1Then
		pagelist=pagelist& "<td><a href='"&strQuery&"page_num="&page_num+1&"'> <font color=""#999999"">Next</font></a></td>"
	End IF	
	pagelist=pagelist& "</td></tr></table>"
	function_show_page=pagelist
End Function
%>