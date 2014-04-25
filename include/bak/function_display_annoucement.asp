<%'OPTION EXPLICIT%>
<!--include virtual="/include/constant.asp"-->

<%
'Call connOpen()

Dim sqlAnnouce
Dim rsAnnouce
Dim arrAnnouce
Dim intAnnouce

Function function_display_annoucement(intProductID)
	sqlAnnouce = "Select annoucement_id , title , detail From tbl_product_annoucement Where date_end >= getDate() And product_id = "&intProductID&" Order by date_start,title" 
	'response.Write(sqlAnnouce)
	'response.End()
	Set rsAnnouce = Server.CreateObject ("ADODB.Recordset")
	rsAnnouce.Open sqlAnnouce, Conn,adOpenStatic,adLockreadOnly
	IF not(rsAnnouce.eof)  then
		arrAnnouce = rsAnnouce.GetRows()
	End IF
	rsAnnouce.Close
	Set rsAnnouce = Nothing 
	
	IF isArray(arrAnnouce) Then 

	%>
    <tr>
        <td align="center">
			<table width="100%"  border="0" align="center" cellpadding="4" cellspacing="3" bgcolor="#AD5700">
				 <tr>
                 <td bgcolor="#FFFFF9" class="s">
                 
	<table align="center" width="100%" border="0">
		<tr>
			<td width="13%" align="right" valign="top">
				<img src="/images/annouce_img.jpg">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</td>
			<td width="87%" align="left">
				<%
				IF (Ubound(arrAnnouce,2) = 0) Then
					Response.Write("<table border='0' align='left' cellpadding='0' cellspacing='0'>")
					Response.Write("<tr>")
					Response.Write("<td>"&arrAnnouce(2,intAnnouce)&"</td>")
					Response.Write("</tr>")
					Response.Write("</table>")
				Else
					%>
					<table>
                    	<tr>
                        	<td>
                            <%
							For intAnnouce = 0 To Ubound(arrAnnouce,2)
							%>
                            <strong><font color='#783E03'><%=arrAnnouce(1,intAnnouce)%></font></strong>&nbsp;&nbsp;&nbsp;<a href='javascript:swapState(<%=intAnnouce%>);' id='close_detail<%=intAnnouce%>'>More detail...</a><br>
                            <div id='innermenu<%=intAnnouce%>' style='display:none;'>
                            <table>
                            	<tr>
                                	<td>
                                    <%=arrAnnouce(2,intAnnouce)%>
                                    </td>
                                </tr>
                            </table>
                            </div><br />
                            <%
							Next
							%>
                            </td>
                        </tr>
                    </table>
					<%
'						Response.Write("<table border='0' align='left' cellpadding='0' cellspacing='0'>")
'						Response.Write("<tr><td>")
'					For intAnnouce = 0 To Ubound(arrAnnouce,2)
'						
'						Response.Write("<strong><font color='#783E03'>"&arrAnnouce(1,intAnnouce))&"</font></strong>&nbsp;&nbsp;&nbsp;<a href='javascript:swapState("&intAnnouce&");' id='close_detail"&intAnnouce&"'>More detail...</a><br>"
'						Response.Write("<div id='innermenu"&intAnnouce&"' style='display:none;'>")
'						Response.Write("<table border='0' align='left' cellpadding='0' cellspacing='0'>")
'						Response.Write("<tr>")
'						Response.Write("<td>"&arrAnnouce(2,intAnnouce)&"</td>")
'						Response.Write("</tr>")
'						Response.Write("</table><br>")
'						Response.Write("</div><br>")
'						
'
''						Response.Write("<strong><font color='#783E03'>"&arrAnnouce(1,intAnnouce))&"</font></strong>&nbsp;&nbsp;&nbsp;<a href='javascript:swapState("&intAnnouce&");' id='close_detail"&intAnnouce&"'>More detail...</a>"
''						Response.Write("<div id='innermenu"&intAnnouce&"' style='display:none;position:relative;left:15px'>")
''						Response.Write("<table border='0' align='left' cellpadding='0' cellspacing='0'>")
''						Response.Write("<tr>")
''						Response.Write("<td>"&arrAnnouce(2,intAnnouce)&"</td>")
''						Response.Write("</tr>")
''						Response.Write("</table><br>")
''						Response.Write("</div><br>")
'					Next 
'						Response.Write("</td></tr>")
'						Response.Write("</table><br><br>")
				End IF
				
				%>
			</td>
		</tr>
	</table>
    
    </td>
				 </tr>
			</table><br>		</td>
      </tr>
	<%
	End IF
End Function
%>