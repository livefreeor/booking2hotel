<br>
<%
Dim recList  , sqlList 
Dim destination , product_id
Dim ch_in_date ,ch_in_month, ch_in_year , ch_out_date , ch_out_month  , ch_out_year ,check_in , check_out

if request.querystring("product_id")="" then
	product_id=request.form("product_id")
else
	product_id=request.querystring("product_id")
end if

if request.querystring("destination")="" then
	destination=request.form("destination")
else
	destination=request.querystring("destination")
end if

if request.querystring("ch_in_date")="" then
	ch_in_date=request.form("ch_in_date")
else
	ch_in_date=request.querystring("ch_in_date")
end if

if request.querystring("ch_in_month")="" then
	ch_in_month=request.form("ch_in_month")
else
	ch_in_month=request.querystring("ch_in_month")
end if

if request.querystring("ch_in_year")="" then
	ch_in_year=request.form("ch_in_year")
else
	ch_in_year=request.querystring("ch_in_year")
end if

if request.querystring("ch_out_date")="" then
	ch_out_date=request.form("ch_out_date")
else
	ch_out_date=request.querystring("ch_out_date")
end if

if request.querystring("ch_out_month")="" then
	ch_out_month=request.form("ch_out_month")
else
	ch_out_month=request.querystring("ch_out_month")
end if

if request.querystring("ch_out_year")="" then
	ch_out_year=request.form("ch_out_year")
else
	ch_out_year=request.querystring("ch_out_year")
end if
%>			
            <table width="200" border="0" cellspacing="0" cellpadding="2">
              <tr> 
                <td bgcolor="#99CCFF" class="h-text"><b class="copy">Accommodation 
                  Facilities :</b></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="2">
              <tr align="left" valign="middle"> 
                <td>
<%
	Response.write Fn_Show_Facilities("feature_id|title_en","tbl_product_option_feature","select feature_id from tbl_feature_product_option where option_id in (select option_id from tbl_product_option where product_id="&product_id&")","title_en","450")
%>
                  <br>
                </td>
              </tr>
            </table>
            <table width="200" border="0" cellspacing="0" cellpadding="2">
              <tr> 
                <td bgcolor="#99CCFF" class="h-text"><b class="copy">Amenities 
                  andServices:</b></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="2">
              <tr align="left" valign="middle"> 
                <td>
<%
	Response.write Fn_Show_Facilities("feature_id|title_en","tbl_product_feature","select feature_id from tbl_feature_product where product_id="&product_id,"title_en","450")
%>
                  <br>
                </td>
              </tr>
            </table>
            <table width="200" border="0" cellspacing="0" cellpadding="2">
              <tr> 
                <td bgcolor="#99CCFF" class="h-text"><b class="copy">Nearby Facitlities:</b></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="2">
              <tr align="left" valign="middle"> 
                <td>
<%
	Response.write Fn_Show_Facilities("nearby_id|title_en","tbl_nearby","select nearby_id from tbl_product_nearby where product_id="&product_id,"title_en","450")
%>
                </td>
              </tr>
            </table>
<%
	Response.write Fn_Gen_Hidden("destination,product_id,ch_in_date,ch_in_month,ch_in_year,ch_out_date,ch_out_month,ch_out_year")
%>
            <br>
