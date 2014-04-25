<%
FUNCTION function_gen_golf_feature (intDestination,strDestination,intType)

	Dim sqlFeature 
	Dim recFeature 
	Dim arrFeature 
	Dim bolFeature 
	Dim intCount
	Dim dateNextDay
	
	dateNextDay = DateAdd("d",1,Date)
	
	SELECT CASE intType
	
		Case 1 'Destination
		
		sqlFeature = "SELECT TOP 15 p.title_en,p.files_name,p.destination_id,p.product_code"
		sqlFeature =sqlFeature &  " FROM tbl_product p "
		sqlFeature =sqlFeature &  " WHERE p.status=1 AND p.flag_hot=1 AND p.product_cat_id=32 AND p.destination_id=" & intDestination
		sqlFeature =sqlFeature &  " ORDER BY p.point DESC"

			Set recFeature  = Server.CreateObject ("ADODB.Recordset")
			recFeature.Open SqlFeature , Conn,adOpenStatic,adLockreadOnly
				IF NOT recFeature.EOF Then
					arrFeature  = recFeature.GetRows()
					bolFeature = True
				End IF
			recFeature.Close
			Set recFeature  = Nothing 
			
			IF bolFeature Then
%>
<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/b_blue_155.gif">
	<tr> 
		<td height="24" align="center"><b><font color="346494">Featured <%=strDestination%> Golf</font></b> </td>
	</tr>
</table>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="97BFEC">
              <tr> 
                <td bgcolor="#FFFFFF" align="center">
				<div style="PADDING-LEFT: 8px;">
				<%For intCount=0 to Ubound(arrFeature,2)%>
				<p>
				<table width="120" cellpadding="2"  cellspacing="1" bgcolor="97BFEC">
				  <tr>
					<td bgcolor="#FFFFFF" align="center">
					<br>
						<a href="/<%=function_generate_golf_link(arrFeature(2,intCount),"",1)%>/<%=arrFeature(1,intCount)%>"><img src="thailand-golf-pic/<%=arrFeature(3,intCount)%>_1.jpg" border="0" alt="<%=arrFeature(0,intCount)%>" width="60" height="64"><br>
						<%=arrFeature(0,intCount)%></a><br>
					</td>
				  </tr>
				</table>
				</p>
				<%Next%>
				  </div>
                </td>
              </tr>
</table>
<%
			End IF
		
		Case 2 'Thailand
			sqlFeature = "SELECT TOP 15 p.title_en,p.files_name,p.destination_id,p.product_code"
			sqlFeature =sqlFeature &  " FROM tbl_product p "
			sqlFeature =sqlFeature &  " WHERE p.status=1 AND p.flag_hot=1 AND p.product_cat_id=32"
			sqlFeature =sqlFeature &  " ORDER BY p.point DESC"
		
			Set recFeature  = Server.CreateObject ("ADODB.Recordset")
			recFeature.Open SqlFeature , Conn,adOpenStatic,adLockreadOnly
				IF NOT recFeature.EOF Then
					arrFeature  = recFeature.GetRows()
					bolFeature = True
				End IF
			recFeature.Close
			Set recFeature  = Nothing 
			
			IF bolFeature Then
%>
<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/b_blue_155.gif">
	<tr> 
		<td height="24" align="center"><b><font color="346494">Feature Thailand Golf</font></b> </td>
	</tr>
</table>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="97BFEC">
              <tr> 
                <td bgcolor="#FFFFFF" align="center">
				<div style="PADDING-LEFT: 8px;">
				<%For intCount=0 to Ubound(arrFeature,2)%>
				<p>
				<table width="120" cellpadding="2"  cellspacing="1" bgcolor="97BFEC">
				  <tr>
					<td bgcolor="#FFFFFF" align="center">
					<br>
						<a href="/<%=function_generate_golf_link(arrFeature(2,intCount),"",1)%>/<%=arrFeature(1,intCount)%>"><img src="thailand-golf-pic/<%=arrFeature(3,intCount)%>_1.jpg" border="0" alt="<%=arrFeature(0,intCount)%>" width="60" height="64"><br>
						<%=arrFeature(0,intCount)%></a>
					</td>
				  </tr>
				</table>
				</p>
				<%Next%>
				  </div>
                </td>
              </tr>
</table>
<%
			End IF
	END SELECT

END FUNCTION
%>