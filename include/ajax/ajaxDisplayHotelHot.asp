<%OPTION EXPLICIT%>
<!--#include virtual="/include/constant.asp"-->
<!--#include virtual="/include/function_display_hotel_hot.asp"-->
<!--#include virtual="/include/function_generate_hotel_link.asp"-->

<%
Call connOpen()
Response.Write function_display_hotel_hot(Request.QueryString("desId"),Request.QueryString("LocID"),Request.QueryString("intType"))
Call connClose()
%>