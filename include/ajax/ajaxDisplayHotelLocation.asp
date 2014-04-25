<%OPTION EXPLICIT%>
<!--#include virtual="/include/constant.asp"-->
<!--#include virtual="/include/function_gen_location_hotel.asp"-->
<!--#include virtual="/include/function_generate_hotel_link.asp"-->
<%
Call connOpen()
Response.Write function_gen_location_hotel(Request.QueryString("intLocationID"),Request.QueryString("strLocation"),Request.QueryString("strAnchorTop"),Request.QueryString("intType"))
Call connClose()
%>