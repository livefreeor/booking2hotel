<%OPTION EXPLICIT%>
<!--#include virtual="/include/function_box_why_choose.asp"-->

<%
Response.Write function_box_why_choose(Request.QueryString("intPercent"),Request.QueryString("intType"))
%>