<%
Function_display_special_promotion()

%>
<table width="56%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="208"><img src="images/pr_head.gif" width="208" height="23"></td>
      </tr>
      <tr>
        <td bgcolor="#FDFEF9" style="border-left:1px solid #CCCCCC;border-right:1px solid #cccccc"><table width="100%" border="0" cellspacing="0" cellpadding="3">
          <tr>
            <td align="center"><a href="<%=recfirst_page.fields("url")%>"><font color="#D75600"><b><%=recfirst_page.fields("title")%></b></font></a></td>
          </tr>
          <tr>
            <td align="center"><table width="160" border="0" cellspacing="0" cellpadding="0" class="f11">
                    <tr>
                      <td height="120" valign="top" background="./images/bg_h_week.gif">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="f11">
                          <tr>
                            <td height="6"><img src="images/spacer.gif" width="1" height="1"></td>
                          </tr>
                        </table>
                        <table width="151" border="0" cellspacing="0" cellpadding="0" class="f11">
                          <tr>
                            <td height="96" valign="top" width="6">&nbsp;</td>
                            <td height="96" valign="top"><a href="<%=recfirst_page.fields("url")%>"><img src="<%=recfirst_page.fields("l_image")%>" alt="<%=recfirst_page.fields("alt")%>" width="145" height="96" border="0"></a></td>
                          </tr>
                      </table></td>
                    </tr>
                </table></td>
          </tr>
          <tr>
            <td><%'=mid(recfirst_page.fields("detail"),1,50)%></td>
          </tr>
          <tr>
            <td height="35" align="center"><strong><font color="#FF0000"><%=recfirst_page.fields("title_special")%></font></strong><br></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><img src="images/pro_footer.gif" width="208" height="13"></td>
      </tr>
    </table>
<%
End Function
%>