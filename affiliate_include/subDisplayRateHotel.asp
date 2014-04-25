<%
IF request.QueryString("psid")=6242 Then
%>
document.write('<div style="font-size:14px; color:#C00; text-align:center; font-family:Verdana, Geneva, sans-serif; font-weight:bold; height:50px; line-height:50px">This website is not official website.</div>');
<%
Else
%>
document.write('<iframe src="http://www.hotels2thailand.com/affiliate_include/rateTable.asp?pid=<%=request.querystring("pdid")%>&cat_id=29" width="760" height="600" VSPACE="0" frameborder="0" SCROLL="no"></iframe>');
<%
End IF
%>

