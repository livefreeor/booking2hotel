<%
FUNCTION function_gen_location_hotel(intLocationID,strLocation,strAnchorTop,intType)

	Dim sqlLocation
	Dim recLocation
	Dim arrLocation
	Dim intCount
	Dim intColumcount
	Dim  bolLocation
	
'	sqlLocation = "SELECT p.title_en,p.files_name,p.destination_id"
'	sqlLocation = sqlLocation & " FROM tbl_product p,tbl_product_location pl"
'	sqlLocation = sqlLocation & " WHERE (p.files_name IS NOT NULL) AND p.status=1 AND p.product_id=pl.product_id AND pl.location_id=" & intLocationID
'	sqlLocation = sqlLocation & " ORDER BY title_en ASC"
	sqlLocation = "st_hotel_detail_all_hotel_location " & intLocationID
	Set recLocation = Server.CreateObject ("ADODB.Recordset")
	recLocation.Open SqlLocation, Conn,adOpenStatic,adLockreadOnly
		IF NOT recLocation.EOF Then
			arrLocation = recLocation.GetRows()
			bolLocation = True
		Else
			bolLocation = False
		End IF
	recLocation.Close
	Set recLocation = Nothing 
	
SELECT CASE intType
	Case 1
%>

<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/b_blue_155.gif">
	<tr> 
		<td height="24" align="center"><b><font color="346494">All Hotels in <%=strLocation%></font></b> </td>
	</tr>
</table>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="97BFEC" id="hotels_list">
              <tr> 
                <td bgcolor="#FFFFFF">
				<p>
				<%IF bolLocation Then%>
				<%For intCount=0 to Ubound(arrLocation,2)%>
					<li><a href="/<%=function_generate_hotel_link(arrLocation(2,intCount),"",1)%>/<%=arrLocation(1,intCount)%>"><%=arrLocation(0,intCount)%></a></li>
				<%Next%>
				<%End IF%>
				  </p>
                </td>
              </tr>
</table>
<%
	Case 2
%>
<table cellspacing=1 cellpadding=2 width="100%" class="f11" border="0" bgcolor="#e4e4e4">
              <tbody> 
              <tr> 
                <td bgcolor="#FFFFFF"> 
                  <table width="100%" border="0" cellspacing="0" cellpadding="3" class="f11">
                    <tr> 
                      <td height="25" bgcolor="EDF5FE"><div id="hoteldetail"><h4>All hotels In <%=strLocation%></h4></div></td>
                    </tr>
                  </table>
				  
				  <table width="100%"  cellspacing="1" cellpadding="2">
				  <%
				  IF bolLocation Then
				  For intCount=0 To Ubound(arrLocation,2)
				  %>
					  <tr>
					  <%IF intCount<=Ubound(arrLocation,2)Then%>
						<td><li><a href="/<%=function_generate_hotel_link(arrLocation(2,intCount),"",1)%>/<%=arrLocation(1,intCount)%>"><%=arrLocation(0,intCount)%></a></td>
						<%
						Else
							Response.Write "<td>&nbsp;</td>"
						End IF
						%>
						<%IF intCount+1<=Ubound(arrLocation,2)Then%>
						<td><li><a href="/<%=function_generate_hotel_link(arrLocation(2,intCount+1),"",1)%>/<%=arrLocation(1,intCount+1)%>"><%=arrLocation(0,intCount+1)%></a></td>
						<%
						Else
							Response.Write "<td>&nbsp;</td>"
						End IF
						%>
					  </tr>
				<%
						intCount = intCount + 1
					Next
				End IF
				%>
				</table>

                  <div align=right><a href="#<%=strAnchortop%>"><font color="FE5400">Back To Top</font></a></div>
                </td>
              </tr>
              </tbody> 
            </table>
<%
	Case 3
	END SELECT
END FUNCTION
%>