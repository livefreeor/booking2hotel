<%
FUNCTION function_hotel_logo(intType)

	SELECT CASE intType
		Case 1
%>
<p><a href="javascript:popup('/thailand-hotels-low-rate.asp',200,280)"><img src="/images/Lowrate.gif" alt="Low Rate Guaranteed" border="0"></a></p>
<p><img src="/images/img_creditcard.gif"></p>
<p><img src="/images/secure_mastercard.gif"></p>
<p><img src="/images/Verified-by-Visa.gif"></p>
<p><img src="/images/verisign.gif"></p>
<p><img src="/images/logo_travel_license.gif"><br>
<b>Hotels2Thailand.com</b></font><br>
<font color="434343">is a registered travel <br>agent with the Tourism Authority of Thailand.<br>TAT License No. 11/3240</font> 
</p>
<%
		Case 2
	END SELECT

END FUNCTION
%>
