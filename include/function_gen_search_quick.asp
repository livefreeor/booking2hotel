<%
FUNCTION function_gen_search_quick(strKeyword,intType)
%>
<table width="163" border="0" cellspacing="0" cellpadding="0" background="/images/b_blue_155.gif">
<tr> 
	<td height="24" align="center" class="f11"><b><font color="#346494">Hotels Quick Search</font></b> </td>
</tr>
</table>
<table width="163" border="0" cellspacing="1" cellpadding="3" class="f11" bgcolor="97BFEC">
              <tr> 
                <td bgcolor="#FFFFFF">

                    
                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
				  <form action="/hotels_search.asp" method="get">
				  <input type="hidden" name="page" value="1">
                    <tr> 
                      <td> 
                        <input type="text" name="keyword" size="14" value="<%=strKeyword%>">
                      </td>
                      <td width="31"><input name="Hotels Quick Search" type="image" src="/images/b_go.gif"></td>
                    </tr>
					</form>
                  </table>
 
                </td>
              </tr>
            </table>
<%
END FUNCTION
%>