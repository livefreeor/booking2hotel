<%
FUNCTION function_display_day_trips_destination(intType)
Select Case intType
Case 1
%>
<table width="163" border="0" cellspacing="0" cellpadding="0" class="f11" background="/images/b_blue_155.gif">
              <tr> 
                <td height="24" align="center"><b><font color="346494"><span class="f11">Day Trips Destinations</span></font></b> </td>
              </tr>
            </table>
<table width="163" border="0" cellspacing="1" cellpadding="3" bgcolor="97BFEC" id="hotels_list">
              <tr> 
                <td bgcolor="#FFFFFF">
                  <li> <a  href="/bangkok-day-trips.asp">Bangkok</a></li>
				  <li> <a  href="/chiang-mai-day-trips.asp">Chiang Mai</a></li>
				  <li> <a  href="/chiang-rai-day-trips.asp">Chiang Rai</a></li>
				  <li> <a  href="/hua-hin-day-trips.asp">Hua Hin</a></li>
                  <li> <a  href="/kanchanaburi-day-trips.asp">Kanchanaburi</a></li>
                  <li> <a  href="/koh-chang-day-trips.asp">Koh Chang</a></li>
				  <li> <a  href="/koh-samui-day-trips.asp">Koh Samui</a></li>
				  <li> <a  href="/krabi-day-trips.asp">Krabi</a></li>
                  <li> <a  href="/pattaya-day-trips.asp">Pattaya</a></li>
                  <li> <a  href="/phang-nga-day-trips.asp">Phang Nga</a></li>
				  <li> <a  href="/phuket-day-trips.asp">Phuket</a></li>
                </td>
              </tr>
            </table>
<%
Case 2
%>
		  <a href="/bangkok-day-trips.asp"    title="Bangkok Tours, Day Trips, Sightseeing, Attraction, Package Tours, Show, Restaurant, Dinner"><u>Bangkok</u></a> |
		  <a href="/chiang-mai-day-trips.asp" title="Chiang Mai Tours, Day Trips, Trekking, Sightseeing, Attraction, Package Tours, Show, Restaurant, Dinner"><u>Chiang Mai</u></a> |
		  <a href="/chiang-rai-day-trips.asp" title="Chiang Rai Tours, Day Trips, Trekking, Sightseeing, Attraction, Package Tours, Show, Restaurant, Khan Toke Dinner"><u>Chiang Rai</u></a> |
		  <a href="/hua-hin-day-trips.asp"    title="Hua Hin Tours, Day Trips, Sightseeing, Attraction, Package Tours, Show, Restaurant, Dinner"><u>Hua Hin</u></a> |
          <a href="/kanchanaburi-day-trips.asp"  title="Kanchanaburi Tours, Day Trips, Sightseeing, Attraction, Package Tours, Show, Restaurant, Dinner"><u>Kanchanaburi</u></a> |
          <a href="/koh-chang-day-trips.asp"  title="Koh Chang Tours, Day Trips, Sightseeing, Attraction, Package Tours, Show, Restaurant, Dinner"><u>Koh Chang</u></a> |
          <a href="/koh-samui-day-trips.asp"  title="Koh Samui Tours, Day Trips, Sightseeing, Attraction, Package Tours, Show, Restaurant, Dinner"><u>Koh Samui</u></a> |
          <a href="/krabi-day-trips.asp"      title="Krabi Tours, Day Trips, Sightseeing, Attraction, Package Tours, Show, Restaurant, Dinner"><u>Krabi</u></a> |
		  <a href="/pattaya-day-trips.asp"    title="Pattaya Tours, Day Trips, Sightseeing, Attraction, Package Tours, Show, Restaurant, Dinner"><u>Pattaya</u></a> |
          <a href="/phang-nga-day-trips.asp"  title="Phang Nga Tours,Day Trips,Vacation,Nightlife,Spa,Massage,Speed Boat,Conoe,Kayak,Water Activities"><u>Phang Nga</u></a> |
          <a href="/phuket-day-trips.asp"     title="Phuket Tours,Day Trips,Vacation,Nightlife,Spa,Massage,Speed Boat,Conoe,Kayak,Water Activities"><u>Phuket</u></a>
<%
End Select
END FUNCTION
%>