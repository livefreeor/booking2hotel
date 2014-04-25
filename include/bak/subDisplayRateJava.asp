<%OPTION EXPLICIT%>
<!--#include virtual="/include/constant.asp"-->
<!--#include virtual="/include/function_date_sql.asp"-->
<!--#include virtual="/include/function_display_rate_room_java.asp"-->
<!--#include virtual="/include/function_gen_option_title.asp"-->
<!--#include virtual="/include/function_generate_hotel_link.asp"-->
<!--#include virtual="/include/function_date.asp"-->
<!--#include virtual="/include/function_display_price.asp"-->
<!--#include virtual="/include/function_display_bol.asp"-->

<%
Dim ProductID
ProductID = Request.QueryString("product_id")
Call ConnOpen()
Response.Write function_display_rate_room_java("","","","","","",0,0,ProductID,0,"",1)
Call ConnClose()
%>
