<%
Function function_mid_string_rich_box(strDetail)
strDetail=server.HTMLEncode(myEW.GetValue(false))
strDetail=replace(strDetail,server.HTMLEncode("<META content=""MSHTML 6.00.2900.2523"" name=GENERATOR>"),"")
strDetail=replace(strDetail,"&lt;HTML&gt;","")
strDetail=replace(strDetail,"&lt;/HTML&gt;","")
strDetail=replace(strDetail,"&lt;HEAD&gt;","")
strDetail=replace(strDetail,"&lt;/HEAD&gt;","")
strDetail=replace(strDetail,"&lt;BODY&gt;","")
strDetail=replace(strDetail,"&lt;/BODY&gt;","")
strDetail=replace(strDetail,"&lt;","<")
strDetail=replace(strDetail,"&gt;",">")
function_mid_string_rich_box=strDetail
End Function
%>