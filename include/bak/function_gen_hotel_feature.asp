<%
FUNCTION function_gen_hotel_feature (intDestination,intLocation,strDestination,strLocation,intType)

	Dim sqlFeature 
	Dim recFeature 
	Dim arrFeature 
	Dim bolFeature 
	Dim intCount
	Dim dateNextDay
	
	dateNextDay = DateAdd("d",1,Date)
	
	SELECT CASE intType
	
		Case 1 'Destination
		
		IF Session("check_in_day")="" Then
			sqlFeature = "SELECT TOP 15 p.title_en,p.files_name,p.destination_id,p.product_code,l.title_en AS location,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&" AND op.date_end>="&function_date_sql(Day(dateNextDay),Month(dateNextDay),Year(dateNextDay),1)&" ORDER BY price ASC),0) AS price_min,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&" AND op.date_end>="&function_date_sql(Day(dateNextDay),Month(dateNextDay),Year(dateNextDay),1)&" ORDER BY price DESC),0) AS price_max"
			sqlFeature =sqlFeature &  " FROM tbl_product p,tbl_product_location pl, tbl_location l "
			sqlFeature =sqlFeature &  " WHERE p.product_id=pl.product_id AND pl.location_id=l.location_id AND p.status=1 AND p.flag_hot=1 AND p.destination_id=" & intDestination
			sqlFeature =sqlFeature &  " ORDER BY p.point DESC"
		Else
			sqlFeature = "SELECT TOP 15 p.title_en,p.files_name,p.destination_id,p.product_code,l.title_en AS location,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Session("check_in_day"),Session("check_in_month"),Session("check_in_year"),1)&" AND op.date_end>="&function_date_sql(Session("check_out_day"),Session("check_out_month"),Session("check_out_year"),1)&" ORDER BY price ASC),0) AS price_min,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Session("check_in_day"),Session("check_in_month"),Session("check_in_year"),1)&" AND op.date_end>="&function_date_sql(Session("check_out_day"),Session("check_out_month"),Session("check_out_year"),1)&" ORDER BY price DESC),0) AS price_max"
			sqlFeature =sqlFeature &  " FROM tbl_product p,tbl_product_location pl, tbl_location l "
			sqlFeature =sqlFeature &  " WHERE p.product_id=pl.product_id AND pl.location_id=l.location_id AND p.status=1 AND p.flag_hot=1 AND p.destination_id=" & intDestination
			sqlFeature =sqlFeature &  " ORDER BY p.point DESC"
		End IF
		
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
		<td height="24" align="center"><b><font color="346494">Featured <%=strDestination%> Hotels</font></b> </td>
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
				    <td bgcolor="#FFFAEF" align="center" class="f11"><font color="#ff6600"><b><%=arrFeature(4,intCount)%></b></font></td>
			      </tr>
				  <tr>
					<td bgcolor="#FFFFFF" align="center">
					<br>
						<a href="/<%=function_generate_hotel_link(arrFeature(2,intCount),"",1)%>/<%=arrFeature(1,intCount)%>"><img src="thailand-hotels-pic/<%=arrFeature(3,intCount)%>_1.jpg" border="0" alt="<%=arrFeature(0,intCount)%>" width="60" height="64"><br>
						<%=arrFeature(0,intCount)%></a><br>
						<font color="green">(<%=function_display_price(arrFeature(5,intCount),1)%> - <%=function_display_price(arrFeature(6,intCount),1)%>&nbsp;<%=Session("currency_code")%>)</font>
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
			
		Case 2 'Location
		IF Session("check_in_day")="" Then
			sqlFeature = "SELECT p.title_en,p.files_name,p.destination_id,p.product_code,l.title_en AS location,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&" AND op.date_end>="&function_date_sql(Day(dateNextDay),Month(dateNextDay),Year(dateNextDay),1)&" ORDER BY price ASC),0) AS price_min,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&" AND op.date_end>="&function_date_sql(Day(dateNextDay),Month(dateNextDay),Year(dateNextDay),1)&" ORDER BY price DESC),0) AS price_max"
			sqlFeature =sqlFeature &  " FROM tbl_product p,tbl_product_location pl, tbl_location l "
			sqlFeature =sqlFeature &  " WHERE p.product_id=pl.product_id AND pl.location_id=l.location_id AND p.status=1 AND p.flag_hot=1 AND pl.location_id=" & intLocation
			sqlFeature =sqlFeature &  " ORDER BY p.point DESC"
		Else
			sqlFeature = "SELECT p.title_en,p.files_name,p.destination_id,p.product_code,l.title_en AS location,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Session("check_in_day"),Session("check_in_month"),Session("check_in_year"),1)&" AND op.date_end>="&function_date_sql(Session("check_out_day"),Session("check_out_month"),Session("check_out_year"),1)&" ORDER BY price ASC),0) AS price_min,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Session("check_in_day"),Session("check_in_month"),Session("check_in_year"),1)&" AND op.date_end>="&function_date_sql(Session("check_out_day"),Session("check_out_month"),Session("check_out_year"),1)&" ORDER BY price DESC),0) AS price_max"
			sqlFeature =sqlFeature &  " FROM tbl_product p,tbl_product_location pl, tbl_location l "
			sqlFeature =sqlFeature &  " WHERE p.product_id=pl.product_id AND pl.location_id=l.location_id AND p.status=1 AND p.flag_hot=1 AND pl.location_id=" & intLocation
			sqlFeature =sqlFeature &  " ORDER BY p.point DESC"
		End IF
		
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
		<td height="24" align="center"><b><font color="346494">Feature <%=strLocation%> Hotels</font></b> </td>
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
						<a href="/<%=function_generate_hotel_link(arrFeature(2,intCount),"",1)%>/<%=arrFeature(1,intCount)%>"><img src="thailand-hotels-pic/<%=arrFeature(3,intCount)%>_1.jpg" border="0" alt="<%=arrFeature(0,intCount)%>" width="60" height="64"><br>
						<%=arrFeature(0,intCount)%></a><br>
						<font color="green">(<%=function_display_price(arrFeature(5,intCount),1)%> - <%=function_display_price(arrFeature(6,intCount),1)%>&nbsp;<%=Session("currency_code")%>)</font>
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
		
		Case 3 'Thailand
		IF Session("check_in_day")="" Then
			sqlFeature = "SELECT TOP 15 p.title_en,p.files_name,p.destination_id,p.product_code,d.title_en AS location,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&" AND op.date_end>="&function_date_sql(Day(dateNextDay),Month(dateNextDay),Year(dateNextDay),1)&" ORDER BY price ASC),0) AS price_min,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Day(Date),Month(Date),Year(Date),1)&" AND op.date_end>="&function_date_sql(Day(dateNextDay),Month(dateNextDay),Year(dateNextDay),1)&" ORDER BY price DESC),0) AS price_max"
			sqlFeature =sqlFeature &  " FROM tbl_product p,tbl_destination d"
			sqlFeature =sqlFeature &  " WHERE p.destination_id=d.destination_id AND p.status=1 AND p.flag_hot=1"
			sqlFeature =sqlFeature &  " ORDER BY p.point DESC"
		Else
			sqlFeature = "SELECT TOP 15 p.title_en,p.files_name,p.destination_id,p.product_code,d.title_en AS destination,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Session("check_in_day"),Session("check_in_month"),Session("check_in_year"),1)&" AND op.date_end>="&function_date_sql(Session("check_out_day"),Session("check_out_month"),Session("check_out_year"),1)&" ORDER BY price ASC),0) AS price_min,"
			sqlFeature =sqlFeature &  "ISNULL((SELECT TOP 1 price FROM tbl_option_price op, tbl_product_option o WHERE o.status=1 AND op.option_id=o.option_id AND o.product_id=p.product_id AND o.option_cat_id=38 AND op.date_start<="&function_date_sql(Session("check_in_day"),Session("check_in_month"),Session("check_in_year"),1)&" AND op.date_end>="&function_date_sql(Session("check_out_day"),Session("check_out_month"),Session("check_out_year"),1)&" ORDER BY price DESC),0) AS price_max"
			sqlFeature =sqlFeature &  " FROM tbl_product p,tbl_destination d"
			sqlFeature =sqlFeature &  " WHERE p.destination_id=d.destination_id AND p.status=1 AND p.flag_hot=1"
			sqlFeature =sqlFeature &  " ORDER BY p.point DESC"
		End IF
		
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
		<td height="24" align="center"><b><font color="346494">Feature Thailand Hotels</font></b> </td>
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
				    <td bgcolor="#FFFAEF" align="center"><font color="#FF6600"><b><%=arrFeature(4,intCount)%></b></font></td>
			      </tr>
				  <tr>
					<td bgcolor="#FFFFFF" align="center">
					<br>
						<a href="/<%=function_generate_hotel_link(arrFeature(2,intCount),"",1)%>/<%=arrFeature(1,intCount)%>"><img src="thailand-hotels-pic/<%=arrFeature(3,intCount)%>_1.jpg" border="0" alt="<%=arrFeature(0,intCount)%>" width="60" height="64"><br>
						<%=arrFeature(0,intCount)%></a><br>
						<font color="green">(<%=function_display_price(arrFeature(5,intCount),1)%> - <%=function_display_price(arrFeature(6,intCount),1)%>&nbsp;<%=Session("currency_code")%>)</font>
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