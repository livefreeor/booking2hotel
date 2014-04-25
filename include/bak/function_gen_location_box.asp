<%
FUNCTION function_gen_location_box(intDestination,strDestination,intType)

	Dim sqlLocation
	Dim recLocation
	Dim arrLocation
	Dim intCount
	
	sqlLocation = "SELECT title_en,files_name FROM tbl_location WHERE status=1 AND destination_id=" & intDestination & " ORDER BY title_en ASC"

	Set recLocation = Server.CreateObject ("ADODB.Recordset")
	recLocation.Open SqlLocation, Conn,adOpenStatic,adLockreadOnly
		arrLocation = recLocation.GetRows()
	recLocation.Close
	Set recLocation = Nothing 

%>

<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/b_blue_155.gif">
	<tr> 
		<td height="24" align="center"><b><font color="346494"><%=strDestination%> Location</font></b> </td>
	</tr>
</table>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="97BFEC" id="hotels_list">
              <tr> 
                <td bgcolor="#FFFFFF">
				<p>
				<%For intCount=0 to Ubound(arrLocation,2)%>
					<li><a href="/<%=arrLocation(1,intCount)%>"><%=arrLocation(0,intCount)%></a></li>
				<%Next%>
				  </p>
                </td>
              </tr>
</table>
<%
END FUNCTION
%>