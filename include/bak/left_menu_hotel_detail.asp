<%
Dim sqlRelate, recRelate

sqlRelate = "pr_hotel_relate " & intProductID

Set recRelate = Server.CreateObject ("ADODB.Recordset")
recRelate.Open SqlRelate, Conn,adOpenForwardOnly, adLockReadOnly
%>
<table width="95%" border="0" cellspacing="0" cellpadding="0">
  <tr> 
    <td colspan="2" align="center" valign="top" bgcolor="#66CCFF" class="h-text"> 
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr align="left" valign="top"> 
          <td width="6%"><img src="image/index-popular.gif" width="19" height="18"></td>
          <td width="94%" bgcolor="#77D5FE" align="left" valign="middle" class="vs"><font color="#0033FF"><b>THAILAND HOTELS</b> <br> <b>POPULAR DESTINATIONS</b></font></td>
        </tr>
      </table></td>
  </tr>
  <tr> 
    <td width="10%" align="left" valign="top" class="m">&nbsp;</td>
    <td width="90%" align="left" valign="top" class="m"> 
	  <img src="./image/bullet01.gif" width="7" height="9"> <a href="bangkok.asp?hotels=30" title="Bangkok Hotels"><b>Bangkok Hotels</b></a><br>
      <img src="./image/bullet01.gif" width="7" height="9"> <a href="phuket.asp?hotels=31" title="Phuket Hotels"><b>Phuket Hotels</b></a><br>
      <img src="./image/bullet01.gif" width="7" height="9"> <a href="pattaya.asp?hotels=33" title="Pattaya Hotels"><b>Pattaya Hotels</b></a><br>
      <img src="./image/bullet01.gif" width="7" height="9"> <a href="krabi.asp?hotels=35" title="Krabi Hotels"><b>Krabi Hotels</b></a><br>
      <br>
	  </td>
  </tr>
  <tr> 
    <td colspan="2" align="left" valign="top" ><img src="image/line-03.gif" width="170" height="3"></td>
  </tr>
</table>
<br>
<%IF NOT recRelate.EOF Then%>
<table width="95%" border="0" cellspacing="0" cellpadding="0">
  <tr> 
    <td align="left" valign="top" class="h-text"> <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr align="left" valign="top"> 
          <td width="6%" bgcolor="#77D5FE"><img src="image/index-popular.gif" width="19" height="18"></td>
          <td width="94%" bgcolor="#77D5FE" align="left" valign="middle" class="vs"><font color="#FF3300"><b><%=recDetail.Fields("destination")%> Hotels In</b><br>
            <b><%=UCase(recRelate.Fields("location"))%></b></font></td>
        </tr>
      </table></td>
  </tr>
  <tr> 
    <td align="left" valign="top" class="s"> <br>
	<%
	While NOT recRelate.EOF
		Response.Write "<img src=""./image/bullet01.gif""> <a href=""hotels.asp?pd="& recRelate.Fields("product_id") &"&destination="& Request.QueryString("destination") &"&ds=" & Request.QueryString("ds") & """>" & recRelate.Fields("product")& "</a><br>"
		recRelate.MoveNext
	Wend
	%>
	</td>
  </tr>
</table>
<%End IF%>
<br>
<table width="95%" cellspacing="1" cellpadding="2">
  <tr> 
    <td><div align="center"><br>
        <img src="image/img_creditcard.gif"><br>
        <br>
        <img src="image/verisign.gif"> </div></td>
  </tr>
</table>
<%
recRelate.Close
Set recRelate = Nothing
%>
