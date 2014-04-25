<%
Function function_display_hotel_of_week()
Dim sqlHW
Dim rsHW
Dim arrHW
Dim intCount
Dim intHW
Dim strProductID
'#############Add Product Special Promotion

strProductID="162,893,204,153,280,716,1877,836,248,590,222,185,216,1838,447,1729,1284,574,257,1667,1995,2034,843,933,1657,2069,252,901"

'########################

sqlHW="select p.product_id,p.title_en,"
sqlHW=sqlHW&" (select top 1 spr.title"
sqlHW=sqlHW&" from tbl_product_option spo,tbl_promotion spr"
sqlHW=sqlHW&" where spo.option_id=spr.option_id and spo.product_id=p.product_id and spr.status=1 and date_end>getdate()"
sqlHW=sqlHW&" order by spr.day_min asc,spr.date_start asc) as pr_title,"
sqlHW=sqlHW&" (select top 1 sd.title_en from tbl_destination sd where sd.destination_id=p.destination_id)as destination,p.files_name,p.product_code,p.destination_id"
sqlHW=sqlHW&" from tbl_product p"
sqlHW=sqlHW&" where p.product_id IN ("&strProductID&")"
sqlHW=sqlHW&" Order by p.destination asc,p.title_en asc"

Set rsHW=server.CreateObject("adodb.recordset")
rsHW.Open sqlHW,conn,1,3
arrHW=rsHW.getRows()

%>
<!--<table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="52"><img src="../images/hw_left.gif" width="52" height="38"></td>
    <td width="147"><img src="../images/hw_title.gif" width="147" height="38"></td>
    <td background="../images/hw_bg.gif">&nbsp;</td>
    <td width="10"><img src="../images/hw_right.gif" width="10" height="38"></td>
  </tr>
</table>-->
<%
intCount=0
response.write "<table width=""452"" border=""0"" align=""center""><tr>"
For intHW=0 to Ubound(arrHW,2)
	IF intCount mod 2=0 Then
		response.write "</tr><tr>"
	End IF
	response.write"<td width=""226"" valign=""top"">"
'	response.write"<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"">"
'  	response.write"<tr>"
'    response.write"<td valign=""top"" width=""65""><br><a href=""/"&function_generate_hotel_link(arrHW(6,intHW),"",1)&"/"&arrHW(4,intHW)&"""><img src=""/thailand-hotels-pic/"&arrHW(5,intHW)&"_1.jpg"" border=""0"" alt="""&arrHW(1,intHW)&""" style=""border:1px solid #999999"" width=""62"" height=""52""></a></td>"
'    response.write"<td bgcolor=""#FFFFFF"" height=""20"" valign=""top""><br><strong><font color=""#804040"">"&arrHW(3,intHW)&"</font></strong><br><font color=""#0066CC""><a href=""/"&function_generate_hotel_link(arrHW(6,intHW),"",1)&"/"&arrHW(4,intHW)&""">"&arrHW(1,intHW)&"</a></font></td></tr>"
'	response.write"<tr><td colspan=""2""><img src=""/images/arrow06.gif"">&nbsp;<font color=""#006600"">"&arrHW(2,intHW)&"...</font></td></tr>"
'	response.write"</tr>"
'	response.write"</table>"
	response.write"<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"">"
	response.write"<tr>"
	response.write"<td valign=""top"" width=""65""><br><a href=""/"&function_generate_hotel_link(arrHW(6,intHW),"",1)&"/"&arrHW(4,intHW)&"""><img src=""/thailand-hotels-pic/"&arrHW(5,intHW)&"_1.jpg"" border=""0"" alt="""&arrHW(1,intHW)&""" style=""border:1px solid #999999"" width=""62"" height=""52""></a></td>"
	response.write"<td width=""154""><br><strong><font color=""#804040"">"&arrHW(3,intHW)&"</font></strong><br><font color=""#0066CC""><a href=""/"&function_generate_hotel_link(arrHW(6,intHW),"",1)&"/"&arrHW(4,intHW)&""">"&arrHW(1,intHW)&"</a></font></td>"
	response.write"</tr>"
	response.write"<tr>"
	response.write"<td colspan=""2"" width=""226""><img src=""/images/arrow06.gif"">&nbsp;<font color=""#006600"">"&arrHW(2,intHW)&"...</font></td>"
	response.write"</tr>"
	response.write"</table>"
	response.write"</td>"
intCount=intCount+1
Next
response.write"</tr></table>"
End Function
%>


