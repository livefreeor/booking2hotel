<!--#include virtual="/include/functionDisplayDropDownSQL.asp"-->
<!--#include virtual="/include/functionDisplayDropDownDate.asp"-->
<!--#include virtual="/include/functionDisplayDropDownTime.asp"-->

<%
FUNCTION functionDisplayFormBooking(intType)
%>
<table width="100%" cellpadding="2" cellspacing="1" class="booking_table_border_color">
	<tr>
	  <td class="booking_table_head_bg"><font class="booking_step_color">Step 2 :</font> <font class="booking_title_color">Your Information</font> <font class="booking_title_detail_color">(<em>Who is making this reservation?</em>)</font> <font color=red><strong>*Mandatory fields</strong></font></td>
  </tr>
	<tr>
		<td align="center" class="booking_table_body_bg">
			<table width="85%" cellpadding="2"  cellspacing="0" class="booking_detail_detail_bg">
						  
						  <tr align="left">
							<td><font class="booking_detail_detail_color">Full Name</font> <font color="red">*</font> </td>
							<td>
							<input name="prefix" type="radio" value="1" checked="checked" />
							Mr. <input name="prefix" type="radio" value="2" />Miss <input name="prefix" type="radio" value="3" />Mrs.
							<br />
<input name="full_name" type="text" size="30" id="full_name" value="<%=Session("full_name")%>">
</td>
						  </tr>
						  <tr align="left">
							<td><font class="booking_detail_detail_color">Email</font> <font color="red">*</font></td>
							<td><input name="email" type="text" id="email" size="30" value="<%=Session("email")%>"></td>
						  </tr>
						  <tr align="left">
							<td><font class="booking_detail_detail_color">Repeat Email</font> <font color="red">*</font></td>
							<td><input name="email2" type="text" id="email2" size="30" value="<%=Session("email2")%>"></td>
						  </tr>
						  <tr align="left">
							<td><font class="booking_detail_detail_color">Address</font></td>
							<td><textarea name="address" cols="40" rows="5" id="address"><%=Session("address")%></textarea></td>
						  </tr>
						  <tr align="left">
							<td><font class="booking_detail_detail_color">Contact no. </font> <font color="red">*</font></td>
							<td><input type="radio" name="phone_type" value="1" checked>Mobile
			<input type="radio" name="phone_type" value="0" >Phone
			
							  <input name="phone" type="text" id="phone" value="<%=Session("phone")%>" size="20"> Mobile is preferable.<br><font color="#FF0000">( Use to contact in case of urgency only.)</font></td>
						  </tr>
						  <tr align="left">
							<td><font class="booking_detail_detail_color">Country </font><font color="red">*</font></td>
							<td><%=functionDisplayDropDownSQL("SELECT country,title FROM tbl_country ORDER BY title ASC","country_id",Session("country_id"),1)%> <font color="red">(Refer to your passport)</font></td>
						  </tr>
						  <%
						  IF Request("flag_time_check_in")="True" Then
						  %>
						  <tr>
						  <td>
						  <font class="booking_detail_detail_color">Time Check In</font>
						  </td>
						  <td>
						  <%=functionDisplayDropDownTime("hour_check_in","min_check_in","","",5)%>
						  </td>
						  </tr>
						  <%
						  End IF
						  %>
						  <tr align="left">
						    <td><font class="booking_detail_detail_color">Receive information from us </font></td>
						    <td><input name="receive" type="radio" value="1" checked="checked" />
					        Yes	<input name="receive" type="radio" value="0" />No					</td>
		      </tr>
<%
IF Request("transfer_id") <> "" or Request("hotel_id")=73 Then
%>
<tr><td colspan="2"><strong><font color="#FF0000">Your flight detail is required.</font></strong></td></tr>
							  <tr align="left">
								<td><font class="booking_detail_detail_color">Flight Arrival Detail: </font></td>
								<td><table width="100%" cellpadding="2"  cellspacing="1" class="booking_flight_border_color">
									<tr>
									  <td width="100" class="booking_flight_bg"><font color="1B56BC">Flight Number: </font></td>
									  <td class="booking_flight_bg"><input type="text" name="a_flight" value="<%=Session("a_flight")%>"></td>
									</tr>
									<tr>
									  <td width="100" class="booking_flight_bg"><font color="#cc3300">Arrival Local Time (On Ticket)</font> </td>
									  <td class="booking_flight_bg">Date: <%=functionDisplayDropDownDate(Session("a_day"),Session("a_month"),Session("a_year"),"a_day","a_month","a_year",1)%><br /><br /><%=functionDisplayDropDownTime("a_hour","a_min",0,0,1)%></td>
									</tr>
								</table></td>
							  </tr>
							  
							  <tr align="left">
								<td><font class="booking_detail_detail_color">Flight Departure Detail: </font></td>
								<td><table width="100%" cellpadding="2"  cellspacing="1" class="booking_flight_border_color">
									<tr>
									  <td width="100" class="booking_flight_bg"><font color="1B56BC">Flight Number: </font></td>
									  <td class="booking_flight_bg"><input type="text" name="d_flight" value="<%=Session("d_flight")%>"></td>
									</tr>
									<tr>
									  <td width="100" class="booking_flight_bg"><font color="#cc3300">Departure Time</font></td>
									  <td class="booking_flight_bg">Date: <%=functionDisplayDropDownDate(Session("d_day"),Session("d_month"),Session("d_year"),"d_day","d_month","d_year",1)%><br /><br /><%=functionDisplayDropDownTime("d_hour","d_min",0,0,1)%></td>
									</tr>
								</table></td>
							  </tr>
<%
End IF
%>
									

		  </table>	  </td>
	</tr>
</table>
<%
END FUNCTION
%>