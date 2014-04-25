<%
FUNCTION function_gen_golf_popular_destination()
                
				Dim sqlTblDest2
				Dim rsTblDest2
				Dim arrTblDest2
				Dim intTblDest2				
				
				sqlTblDest2 = "select distinct(select d.title_en from tbl_destination d where d.destination_id = p.destination_id) dest, p.destination_id"
				sqlTblDest2 = sqlTblDest2&" from tbl_option_price op, tbl_product p, tbl_product_option po"
				sqlTblDest2 = sqlTblDest2&" where op.option_id = po.option_id and po.product_id = p.product_id"
				sqlTblDest2 = sqlTblDest2&" and p.status = 1 and op.date_end >= getDate() and p.product_cat_id = 32 order by dest"
					
				Set rsTblDest2=server.CreateObject("adodb.recordset")
				rsTblDest2.Open sqlTblDest2,Conn,adOpenStatic,adLockReadOnly
				IF Not rsTblDest2.EOF Then
					arrTblDest2=rsTblDest2.getRows()
				End IF
				rsTblDest2.close
				Set rsTblDest2=Nothing
								

%>
<table width="163" border="0" cellspacing="0" cellpadding="0" class="f13" background="/images/b_blue_155.gif">
              <tr> 
                <td height="24" align="center"><b><font color="346494"><span class="f11">Golf Destinations</span></font></b> </td>
              </tr>
            </table>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="97BFEC" id="hotels_list">
              <tr> 
                <td bgcolor="#FFFFFF">
				<p>
                	<%
					For intTblDest2 = 0 To Ubound(arrTblDest2,2)
						response.Write("<li><a  href='/"&arrTblDest2(0,intTblDest2)&"-golf.asp'>"&arrTblDest2(0,intTblDest2)&" Golf</a></li>")
					Next
					%>
                  <!--<li><a  href="/ayutthaya-golf.asp">Ayutthaya Golf</a></li>
				  <li><a  href="/bangkok-golf.asp">Bangkok Golf</a></li>
				  <li><a  href="/cha-am-golf.asp">Cha Am Golf</a></li>
				  <li><a  href="/chiang-mai-golf.asp">Chiang Mai Golf</a></li>
                  <li><a  href="/chiang-rai-golf.asp">Chiang Rai Golf</a></li>
				  <li><a  href="/hua-hin-golf.asp">Hua Hin Golf</a></li>
				  <li><a  href="/kanchanaburi-golf.asp">Kanchanaburi Golf</a></li>
				  <li><a  href="/khao-yai-golf.asp">Khao Yai Golf</a></li>
                  <li><a  href="/koh-samui-golf.asp">Koh Samui Golf</a></li>
				  <li><a  href="/pattaya-golf.asp">Pattaya Golf</a></li>
				  <li><a  href="/phang-nga-golf.asp">Phang Nga Golf</a></li>
				  <li><a  href="/phuket-golf.asp">Phuket Golf</a></li>
                  <li><a  href="/rayong-golf.asp">Rayong Golf</a></li>-->
				  </p>
                </td>
              </tr>
            </table>
<%
END FUNCTION
%>