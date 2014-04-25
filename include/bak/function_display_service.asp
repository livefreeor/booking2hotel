<%
FUNCTION function_display_service(intType)

	SELECT CASE intType
		Case 1 '### Home Page ###
%>
<fieldset id="tbl_service">
          <legend>All Product</legend>
          <table width="100%" border="0" cellspacing="1" cellpadding="2" id="tbl_service">
  <tr>
    <td width="100"><a href="/thailand-hotels.asp"><img src="image/temp/hotels.jpg" width="77" height="69" alt="Thailand Hotels" border="0"></a></td>
    <td><a href="/thailand-hotels.asp"><strong>Hotels & Resorts</strong></a><br />
Book hotels in Thailand from our listed below from Great Hotels - a collection of destination's finest luxury 5 star hotels.
</td>
    <td width="100"><a href="thailand-golf-courses.asp"><img src="image/temp/golf_course.gif" width="76" height="69" alt="Thailand Golf Courses" border="0"></a></td>
    <td><a href="thailand-golf-courses.asp"><strong>Golf Course</strong></a>  <br />
Plan your own golfing tour of Thailand with our flexible programme covering many of the kingdom's best courses.</td>
  </tr>
  <tr>
    <td><a href="/thailand-day-trips.asp"><img src="image/temp/day_trips.jpg" width="77" height="66" alt="Thailand Day Trip" border="0"></a></td>
    <td>
    <a href="/thailand-day-trips.asp"><strong>Day Trips</strong></a>  <br />
If you are looking for One Day Tours and Activities, This is the right place!
    </td>
    <td><a href="/thailand-show-event.asp"><img src="image/temp/show_event.jpg" width="77" height="64" alt="Thailand Show & Events" border="0"></a></td>
    <td>
    <a href="/thailand-show-event.asp"><strong>Show & Event</strong> </a> <br />
You can book Siam Niramit, Phuket Fantasea, Pattaya Tiffany and all Show & Event in Thailand
    </td>
  </tr>
  <tr>
    <td><a href="/thailand-water-activity.asp"><img src="image/temp/water_activities.jpg" width="74" height="70" border="0"></a></td>
    <td><a href="/thailand-water-activity.asp"><strong>Water Activities</strong></a>  <br />
Water sports activity holiday in thailand. Diving, Fishing,Spped Boat Tour and all water activies in Thailand, You can book here </td>
    <td><a href="/thailand-health-check-up.asp"><img src="image/temp/health_check_up.jpg" width="77" height="71" alt="Thailand Medical Service" border="0"></a></td>
    <td>
    <a href="/thailand-health-check-up.asp"><strong>Health Check Up</strong></a><br />
Fall is the perfect time to do a health check-up to get you in top health.
    </td>
  </tr>
  <tr>
    <td><a href="/thailand-spa.asp"><img src="image/temp/spa_massage.jpg" width="78" height="65" border="0"></a></td>
    <td>
    <a href="/thailand-spa.asp"><strong>Spa & Massage</strong></a>  <br />
For your convenience Spa appointments may be booked on-line with us and up to one hour prior to your desired appointment time. 
    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>

		  </fieldset>
<%
		Case 2
		Case 3
	END SELECT

END FUNCTION
%>