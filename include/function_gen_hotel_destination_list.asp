<!--#include virtual="/include/function_generate_hotel_link.asp"-->
<%
Function function_gen_hotel_destination_list(intType)
Dim sqlList
Dim rsList

sqlList="select destination_id,title_en"
sqlList=sqlList&" from tbl_destination"
sqlList=sqlList&" where destination_id Not IN (56,64)"
sqlList=sqlList&" order by title_en asc"
Set rsList=server.CreateObject("adodb.recordset")
rsList.Open sqlList,conn,1,3
Dim intCount
intCount=1
%>
<table width="780" border="0" cellspacing="0" cellpadding="0" class="f11">
        <tr> 
          <td height="24" align="center" class="f13" background="/images/bg_bar_2.gif">
		  <%
		  Do while Not rsList.Eof
			if intCount<>rsList.recordcount then
			response.write "<a href=""/"&function_generate_hotel_link(int(rsList("destination_id")),"",1)&".asp"" title="&rsList("title_en")&" Hotels><u>"&rsList("title_en")&"</u><a> <font color=""346494"">| </font>"
			else
			response.write "<a href=""/"&function_generate_hotel_link(rsList("destination_id"),"",1)&".asp"" title="&rsList("title_en")&" Hotels><u>"&rsList("title_en")&"</u><a>"
			end if
			intCount=intCount+1
		  rsList.Movenext
		  Loop
		  %>
		  </td>
        </tr>
      </table>
<%
End Function
%>