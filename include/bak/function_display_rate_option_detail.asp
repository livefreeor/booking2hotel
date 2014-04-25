<%
FUNCTION function_display_rate_option_detail(intOptionID)

	Dim sqlRate
	Dim recRate
	Dim arrRate
	Dim intCountRate
	Dim bolRate
	
	sqlRate = "SELECT date_start,date_end,price"
	sqlRate = sqlRate & " FROM tbl_option_price "
	sqlRate = sqlRate & " WHERE date_end>=getdate() AND option_id=" & intOptionID
	sqlRate = sqlRate & " ORDER BY date_start ASC"
	
	Set recRate = Server.CreateObject ("ADODB.Recordset")
	recRate.Open SqlRate, Conn,adOpenStatic,adLockreadOnly
		IF NOT recRate.EOF Then
			bolRate = True
			arrRate = recRate.GetRows()
		Else
			bolRate = False
		End IF
	recRate.Close
	Set recRate = Nothing
	
	IF bolRate Then
%>
<table width="95%" border="0" cellpadding="2" cellspacing="1" bgcolor="#E4E4E4">
            <tr>
              <td align="center" bgcolor="#EDF5FE"><strong><font color="#346494">Period</font></strong></td>
              <td align="center" bgcolor="#EDF5FE"><strong><font color="#346494">Price</font></strong><br>
                <font color="#990066" class="s1">( <%=Session("currency_title")%>&nbsp;<img src="/images/flag_<%=Session("currency_code")%>.gif"> )</font></td>
            </tr>
<%
For intCountRate=0 To Ubound(arrRate,2)
%>
            <tr>
              <td align="center"  bgcolor="#FFFFFF"><%=function_date(arrRate(0,intCountRate),3)%> - <%=function_date(arrRate(1,intCountRate),3)%></td>
              <td align="center" bgcolor="#FFFFFF"><%=function_display_price(arrRate(2,intCountRate),1) %></td>
            </tr>
<%
Next
%>
          </table>
<%
	End IF
END FUNCTION
%>