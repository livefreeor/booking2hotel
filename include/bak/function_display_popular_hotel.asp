<%
FUNCTION function_display_popular_hotel(intDestination,intLocation,strDestination,strLocation,intDisplay,intType)

	Dim sqlPopular
	Dim recPopular
	Dim arrPopular
	Dim intCount
	Dim strName
	Dim bolPopular
	Dim strDateCurrentSql
	
	IF intLocation<>"" AND Cstr(intLocation)<>"0" Then
		strName = strLocation
	Else
		strName = strDestination
	End IF
	
	strDateCurrentSql = function_date_sql(Day(Date),Month(Date),Year(Date),1)
	
	sqlPopular = "SELECT TOP "& intDisplay &" p.title_en,p.files_name,p.product_code, "
	sqlPopular = sqlPopular & " ISNULL((SELECT TOP 1 op.price FROM tbl_option_price op,tbl_product_option o WHERE p.product_id=o.product_id AND o.option_id=op.option_id AND o.option_cat_id=38 AND op.date_end>="& strDateCurrentSql &" ORDER BY op.price ASC),0) AS price_min,"
	sqlPopular = sqlPopular & " ISNULL((SELECT TOP 1 op.price FROM tbl_option_price op,tbl_product_option o WHERE p.product_id=o.product_id AND o.option_id=op.option_id AND o.option_cat_id=38 AND op.date_end>="& strDateCurrentSql &" ORDER BY op.price DESC),0) AS price_max"
	sqlPopular = sqlPopular & " FROM tbl_product p,tbl_product_location pl"
	sqlPopular = sqlPopular & " WHERE p.destination_id="& intDestination &" AND p.product_id=pl.product_id AND p.status=1 AND pl.location_id IN ("& intLocation &")"
	sqlPopular = sqlPopular & " ORDER BY point DESC"

	Set recPopular = Server.CreateObject ("ADODB.Recordset")
	recPopular.Open SqlPopular, Conn,adOpenStatic,adLockreadOnly
		IF NOT recPopular.EOF Then
			arrPopular = recPopular.GetRows()
			bolPopular = True
		Else
			bolPopular = False
		End IF
	recPopular.Close
	Set recPopular = Nothing 
	
	IF bolPopular Then
%>
			<table width="90%" cellpadding="0"  cellspacing="1" bgcolor="#FEDDB8">
              <tr>
                <td bgcolor="#FBFBFB"><div align="center">Popular <%=strName%> Hotels </div></td>
              </tr>
              <tr>
                <td bgcolor="#FFFFFF"><div align="center">
				<%For intCount=0 To Ubound(arrPopular,2)%>
				<p>
				<a href="/<%=arrPopular(1,intCount)%>"><img src="thailand-hotels-pic/<%=arrPopular(2,intCount)%>_1.jpg" width="65" height="65" alt="<%=arrPopular(0,intCount)%>" border="0"><br>
                  <%=arrPopular(0,intCount)%></a><br>
                  From <%=function_display_price(arrPopular(3,intCount),1)%> - <%=function_display_price(arrPopular(4,intCount),1)%>&nbsp;<%=Session("currency_code")%>
				  Hello
				  </p>
				  <%Next%>
				</div>
				</td>
              </tr>
            </table>
<%
	End IF
END FUNCTION
%>