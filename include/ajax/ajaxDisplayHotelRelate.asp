<%OPTION EXPLICIT%>
<!--#include virtual="/include/constant.asp"-->
<!--#include virtual="/include/function_gen_hotel_relate.asp"-->
<%
Call connOpen()
Response.Write function_gen_hotel_relate(Request.QueryString("intProduct"),Request.QueryString("intLocation"),Request.QueryString("intStar"),Request.QueryString("intType"))
Call connClose()
%>