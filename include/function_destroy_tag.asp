<%
Function function_destroy_tag(strDetail)
	strDetail=replace(strDetail,"<","&lt;")
	strDetail=replace(strDetail,">","&gt;")
	strDetail=replace(strDetail,"'"," ")
	function_destroy_tag=strDetail
End Function
%>
&