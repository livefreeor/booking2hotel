<%OPTION EXPLICIT%>
<!--#include virtual="/include/constant.asp"-->
<!--#include virtual="/include/function_gen_hotel_feature.asp"-->
<!--#include virtual="/include/function_date_sql.asp"-->
<!--#include virtual="/include/function_generate_hotel_link.asp"-->
<!--#include virtual="/include/function_display_price.asp"-->
<%
Call connOpen()
Response.Write function_gen_hotel_feature(Request("desId"),Request("locId"),Request("strDes"),Request("strLoc"),Request("intType"))
Call connClose()
%>