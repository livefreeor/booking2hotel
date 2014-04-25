<%
FUNCTION function_display_side_logo(intType)

	SELECT CASE intType
		Case 1 '### Home ###
%>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="f11">
<tr>
<td align="center">
<a href="http://www.hotels2thailand.com/coupon/free-coupon.aspx" target="_blank"><img src="images/coupon.jpg" alt="Hotels2thailand Discount Coupon" width="150" height="170" border="0" style="padding-left:13px"/></a><br />
</td>
</tr>
             <tr>
                <td align="center">
<table width="100%"border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#EFEFEF"  class="blue2">
                                  <tr>
									<td bgcolor="#FFFFFF" align="left" class="yellow2" ><a href="/partners" target="_blank"><img src="images/icon_affiliate02.gif" alt="Affiliate Program" width="32" height="29" border="0" /><img src="images/affiliate_program01.gif" alt="Affiliate Program" width="117" height="25" border="0" /></a></td>
								  </tr>
								  <tr>
									<td align="left" bgcolor="#FFFFFF" ><a href="/partners" target="_blank">Easily <span class="yellow2">Make Extra Money From Your Site. Earn 60% commission selling products you don't own!</span></a> </td>
				    </tr>
                  </table>
<br>
                                <table width="95%"border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#EFEFEF"  class="blue2">
                                  <tr>
									<td bgcolor="#FFFFFF" align="left" class="yellow2" ><a href="/Travel_Forum/home.aspx" target="_blank"><img src="./images/travel_forum.gif" alt="Thailand Travel Forum"  border="0" /></a><a href="/Travel_Forum/home.aspx" target="_blank"><img src="./images/baner_forum02.gif" alt="Thailand Travel Forum" width="96" height="25" border="0" /></a></td>
								  </tr>
								  <tr>
									<td align="left" bgcolor="#FFFFFF" ><a href="/Travel_Forum/home.aspx" target="_blank">Discussion forum about travel in Thailand. Get Thailand travel and vacation advice																								 																											 									</a></td>
								  </tr>
                  </table>
				<br />				</td>
              </tr>
             <!-- <tr>
                <td align="center"><a href="http://travel.wwte1.com/pubspec/scripts/eap.asp?goto=jump&eapid=18685-30001&lang=1033&ovrd=3&jurl=/pub/agent.dll?qscr=home" target="_blank" rel="nofollow" > <img src="./images/logo_flight.gif" alt="Flight Booking" border="0"></a></td>
              </tr>-->
<!--              <tr>
                <td align="center"><a href="http://www.hotels2thailand.com/members/order_check.asp" target="_blank"> 
                  <img src="images/check_booking01.gif" alt="Check Your Booking Status" border="0"></a></td>
              </tr>-->
             <tr>
                <td align="center">
                	<div id="checkbooking"></div>
                    <script language="javascript">
                    ajax_utility('/include/ajax/ajaxDisplaySideLogo.asp?id=1','checkbooking','<img src=/images/vista.gif>');
					</script>
                </td>
              </tr>
              
              <tr> 
                <td align="center"><a href="javascript:popup('/thailand-hotels-low-rate.asp',240,280)"> 
                  <img src="images/Lowrate.gif" alt="Low Rate Guaranteed" border="0"></a></td>
              </tr>
              <tr> 
                <td align="center"><img src="images/logo_travel_license.gif" width="60" height="60" alt="Thailand Travel License"></td>
              </tr>
              <tr> 
                <td><div align="center"><font color="003663"><b>Hotels2Thailand.com</b></font><br>
                    is a registered travel <br>
                    agent with the Tourism Authority of Thailand.<br>
                    TAT License No. 11/3240 </div><br /></td>
              </tr>
              <tr>
                <td align="center"><img src="images/img_creditcard.gif" alt="Visa Card, Master Card, Amex Card, Discover Card Are Accepted Here"></td>
              </tr>
              <tr>
                <td align="center"><img src="images/secure_mastercard.gif"></td>
              </tr>
             
              <tr>
                <td align="center"><img src="images/Verified-by-Visa.gif" width="84" height="60"></td>
              </tr>
               <tr>
                <td align="center"><img src="images/logo_jcb.gif"><br /><br /></td>
              </tr>
              <tr>
                <td align="center"><img src="images/verisign.gif" width="80" height="33"></td>
              </tr>
            </table>
<%
		Case 999
	END SELECT

END FUNCTION
%>