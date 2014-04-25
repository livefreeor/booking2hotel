<%
FUNCTION function_gen_show_event_menu(intDestination,strDestination,strHotelName,strFileName,intType)

	Dim strFileNameInfo
	Dim strfileNamePhoto
	Dim strFileNameReview
	Dim strFielNameWeather
	Dim strFileNameWhy
	Dim strFileNameContact
	
	strFileNameInfo = Replace(strFileName,".asp","_information.asp")
	strfileNamePhoto = Replace(strFileName,".asp","_photo.asp")
	strFileNameReview = Replace(strFileName,".asp","_review.asp")
	strFielNameWeather = Replace(strFileName,".asp","_weather.asp")
	strFileNameWhy = Replace(strFileName,".asp","_why.asp")
	strFileNameContact = Replace(strFileName,".asp","_contact.asp")

	SELECT CASE intType
		Case 1 'Book Now
%>
<table width="650" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="7" height="35" rowspan="2"><img src="/images/blue_L.gif" width="7" height="35" /></td>
    <td width="100" rowspan="2" align="center" background="/images/bg_blue.gif"><div align="center"><a href="./<%=strFileName%>#booking" title="Book <%=strHotelName%> Now"><b>Book Now</b></a></div></td>
    <td width="10" height="35" rowspan="2"><img src="/images/blue_orange.gif" width="14" height="35" /></td>
    <td width="114" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="10" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="115" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="10" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="103" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="10" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="91" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="8" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
  </tr>
  <tr>
    <td width="175" align="center" background="/images/bg_orange.gif"><a href="./<%=strfileNamePhoto%>#photo" title="<%=strHotelName%> Photo Gallery"><font color="#fe5400"><b>Photo Gallery </b></font></a></td>
    <td width="10" height="24"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="175" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameReview%>#review" title="<%=strHotelName%> Reviews"><font color="#fe5400"><b>Traveler Reviews</b></font></a></td>
    <td width="10" height="24" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameWhy%>#why" title="Why Make Booking With Hotels2Thailand.com ?"><font color="#fe5400"><b>Why Us?</b></font></a></td>
    <td width="10" height="24" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameContact%>#contact" title="Contact Us"><font color="#fe5400"><b>Contact Us</b></font></a></td>
    <td width="8" height="24"><img src="/images/orange_R.gif" width="6" height="24" /></td>
  </tr>
</table>
<%
		Case 2 'Infomation
		
%>
<%
		Case 3 'Photo
%>
<table width="650" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="88" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="12" height="35" rowspan="2" valign="bottom"><img src="/images/orange_blue.gif" width="12" height="35" /></td>
    <td width="175" rowspan="2" align="center" background="/images/bg_blue.gif"><div align="center"><a href="./<%=strfileNamePhoto%>#photo" title="<%=strHotelName%> Photo gallery"><b>Photo Gallery  </b></a></div></td>
    <td width="11" rowspan="2" valign="bottom"><img src="/images/blue_orange.gif" width="11" height="35" /></td>
    <td width="118" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="10" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="83" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="10" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="99" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="6" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
  </tr>
  <tr>
    <td width="4" height="24"><img src="/images/orange_L.gif" width="7" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileName%>#booking" title="Book <%=strHotelName%> Now"><font color="#fe5400"><b>Book Now </b></font></a></td>
    <td width="175" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameReview%>#review" title="<%=strHotelName%> Reviews"><font color="#fe5400"><b>Traveler Reviews</b></font></a></td>
    <td width="10" height="24"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameWhy%>#why" title="Why Make Booking With Hotels2Thailand.com ?"><font color="#fe5400"><b>Why Us?</b></font></a></td>
    <td width="10" height="24" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameContact%>#contact" title="Contact Us"><font color="#fe5400"><b>Contact Us</b></font></a></td>
    <td width="6" height="24"><img src="/images/orange_R.gif" width="6" height="24" /></td>
  </tr>
</table>
<%
		Case 4 'Review
%>
<table width="650" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="5" height="11"><img width="5" height="11" /></td>
    <td width="88" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="130" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="10" height="35" rowspan="2" valign="bottom"><img src="/images/orange_blue.gif" width="10" height="35" /></td>
    <td width="175" rowspan="2" align="center" background="/images/bg_blue.gif"><div align="center"><a href="./<%=strFileNameReview%>#review" title="<%=strHotelName%> Reviews"><b>Traveller Reviews  </b></a></div></td>
    <td width="11" height="35" rowspan="2" valign="bottom"><img src="/images/blue_orange.gif" width="11" height="35" /></td>
    <td width="112" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="83" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
  </tr>
  <tr>
    <td width="4" height="24"><img src="/images/orange_L.gif" width="7" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileName%>#booking" title="Book <%=strHotelName%> Now"><font color="#fe5400"><b>Book Now </b></font></a></td>
    <td width="10" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="175" align="center" background="/images/bg_orange.gif"><a href="./<%=strfileNamePhoto%>#photo" title="<%=strHotelName%> Photo Gallery"><font color="#fe5400"><b>Photo Gallery </b></font></a></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameWhy%>#why" title="Why Make Booking With Hotels2Thailand.com ?"><font color="#fe5400"><b>Why Us?</b></font></a></td>
    <td width="10" height="24" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameContact%>#contact" title="Contact Us"><font color="#fe5400"><b>Contact Us</b></font></a></td>
    <td width="10" height="24" align="center"><img src="/images/orange_R.gif" width="10" height="24" /></td>
  </tr>
</table>
<%
		Case 5 'Weather
%>
<%
		Case 6 'Why Us
%>
<table width="650" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="88" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="130" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="130" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="10" height="35" rowspan="2" valign="bottom"><img src="/images/orange_blue.gif" width="12" height="35" /></td>
    <td width="100" rowspan="2" align="center" background="/images/bg_blue.gif"><div align="center"><a href="./<%=strFileNameWhy%>#why" title="Why Make Booking With Hotels2Thailand.com ?"><b>Why Us?  </b></a></div></td>
    <td width="11" height="35" rowspan="2" valign="bottom"><img src="/images/blue_orange.gif" width="11" height="35" /></td>
    <td width="83" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
  </tr>
  <tr>
    <td width="7" height="24"><img src="/images/orange_L.gif" width="7" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileName%>#booking" title="Book <%=strHotelName%> Now"><font color="#fe5400"><b>Book Now </b></font></a></td>
    <td width="10" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="175" align="center" background="/images/bg_orange.gif"><a href="./<%=strfileNamePhoto%>#photo" title="<%=strHotelName%> Photo Gallery"><font color="#fe5400"><b>Photo Gallery </b></font></a></td>
    <td width="10" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="175" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameReview%>#review" title="<%=strHotelName%> Reviews"><font color="#fe5400"><b>Traveler Reviews</b></font></a></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameContact%>#contact" title="Contact Us"><font color="#fe5400"><b>Contact Us</b></font></a></td>
    <td width="10" height="24" align="center"><img src="/images/orange_R.gif" width="10" height="24" /></td>
  </tr>
</table>
<%
		Case 7 'Contact US
%>
<table width="650" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="88" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="130" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="130" height="11"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="5"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="83"><img src="/images/spacer_1.gif" width="5" height="11" /></td>
    <td width="11" rowspan="2" valign="bottom"><img src="/images/orange_blue.gif" width="12" height="35" /></td>
    <td width="100" rowspan="2" align="center" background="/images/bg_blue.gif"><div align="center"><a href="./<%=strFileNameContact%>#contact"  title="Contact Us"><b>Contact </b></a></div></td>
    <td width="11" rowspan="2" valign="bottom"><img src="/images/blue_R.gif" width="8" height="35" /></td>
  </tr>
  <tr>
    <td width="3" height="24"><img src="/images/orange_L.gif" width="4" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileName%>#booking" title="Book <%=strHotelName%> Now"><font color="#fe5400"><b>Book Now </b></font></a></td>
    <td width="10" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="175" align="center" background="/images/bg_orange.gif"><a href="./<%=strfileNamePhoto%>#photo" title="<%=strHotelName%> Photo Gallery"><font color="#fe5400"><b>Photo Gallery </b></font></a></td>
    <td width="10" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="175" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameReview%>#review" title="<%=strHotelName%> Reviews"><font color="#fe5400"><b>Traveler Reviews</b></font></a></td>
    <td width="10" align="center"><img src="/images/orange_orange.gif" width="10" height="24" /></td>
    <td width="100" align="center" background="/images/bg_orange.gif"><a href="./<%=strFileNameWhy%>#why" title="Why Make Booking With Hotels2Thailand.com ?"><font color="#fe5400"><b>Why Us?</b></font></a></td>
  </tr>
</table>
<%
		Case 8 '....
	END SELECT
END FUNCTION
%>