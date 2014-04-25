<%
SUB sub_display_logo(intSiteID,strH1,strAltLogo,intType)

	Dim sqlLogo
	Dim recLogo
	Dim arrLogo
	Dim strLogo
	Dim bolPartnerLogo
	Dim strLogoTmp
	
	'### Check Partner Logo ###
	IF intSiteID="" OR ISNULL(intSiteID) Then
	
		bolPartnerLogo = False
		
	Else
	
		sqlLogo = "SELECT site_id,partner_id,url,image_logo,status_logo"
		sqlLogo = sqlLogo & " FROM tbl_aff_sites "
		sqlLogo = sqlLogo & " WHERE site_id=" & intSiteID
				
		Set recLogo = Server.CreateObject ("ADODB.Recordset")
		recLogo.Open SqlLogo, Conn,adOpenStatic,adLockreadOnly
			arrLogo = recLogo.GetRows()
		recLogo.Close
		Set recLogo = Nothing 
		
		IF arrLogo(4,0) AND NOT ISNULL(arrLogo(4,0)) AND arrLogo(4,0)<>"" Then
			bolPartnerLogo = True
		Else
			bolPartnerLogo = False
		End IF
		
	End IF
	'### Check Partner Logo ###
	
	SELECT CASE intType
	
		Case 1 '### Home Page ###
		
			IF NOT bolPartnerLogo Then
				strLogo = "<div id=""logo_home"" style=""float:left;padding:20px 0px 0px 25px;""><a href=""/""><img src=""/images/small_logo.gif"" border=""0"" alt="""&strAltLogo&"""></a></div>" & VbCrlf
				strLogo = strLogo &  "<div id=""homepage""><h1>"&strH1&"</h1></div>" & VbCrlf
				strLogo = strLogo &  "<div align=""right"">" & VbCrlf
				strLogo = strLogo &  "<a href=""http://www.hotels2thailand.com/krabi-hotels/Krabi-La-Playa-Resort.asp"" style=""background:none;""><img src=""/images/krabi-la-playa-promotion.jpg"" border=""0"" alt=""Krabi La Playa Resort ""></a><br />" & VbCrlf
				'strLogo=strLogo&"<div id=""kbank_ads""></div><script language=""javascript"">"
                'strLogo=strLogo&"ajax_utility('kbank_ads.html','kbank_ads','<img src=/images/vista.gif>');"
            	'strLogo=strLogo&"<\/script>"
				strLogo = strLogo &  "</div>" & VbCrlf
				strLogo = strLogo &  "<br style=""clear:both"" /><br />" & VbCrlf
			Else
				'strLogo = "<div style=""float:left;padding-bottom:0;border:1px solid #FF0000;height:80px;""><a href=""/""><img src=""images/small_logo_aff.gif"" border=""0"" /></a></div>" & VbCrlf
				'strLogo = strLogo  & "<div style=""float:right;padding-bottom:0;border:1px solid #FF0000;height:80px;""><a href=""/""><img src=""images/small_logo_aff.gif"" border=""0"" /></a></div>" & VbCrlf
				'strLogo = strLogo  & "<div style=""float:right;padding:-bottom:0;border:1px solid #FF0000;height:80px;""><a href=""/""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></a></div>" & VbCrlf
				'strLogo = strLogo  & "<br style=""clear:both"" /><br />" & VbCrlf
					
				strLogo = "<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""2"">" &VbCrlf
				strLogo = strLogo & "<tr>" &VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" &VbCrlf
				strLogo = strLogo & "<td align=""right"" valign=""top""><a href=""/"" title=""Hotels2Thailand.com Home""><img src=""/images/small_logo_aff.gif"" border=""0"" /></a></td>" &VbCrlf
				strLogo = strLogo & "</tr>" &VbCrlf
				strLogo = strLogo & "</table>" &VbCrlf
				'strLogo = strLogo  & "<br style=""clear:both"" /><br />" & VbCrlf
			End IF
				

			
		Case 2 '### Hotel Detail ###
		
			IF NOT bolPartnerLogo Then
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt=""" & strAltLogo & """></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldetail""><h1>" & strH1 & "</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""346494""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200"" valign=""top""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""346494""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF
			
		Case 3 '### Hotel List ###
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt="""&strAltLogo&"""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldestination""><h1>"&strH1&"</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""346494""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200"" valign=""top""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""346494""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF
		
		Case 4 '### Day Trip ###
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt="""&strAltLogo&"""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldetail""><h1>"&strH1&"</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""346494""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf

			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200"" valign=""top""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""346494""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF
			
		Case 5 '### Water Activity ###
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt="""&strAltLogo&"""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldetail""><h1>"&strH1&"</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center"">" & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""346494""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
      		
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200"" valign=""top""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""346494""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF

		Case 6 '### Golf Courses ###
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr> " & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt="""&strAltLogo&"""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldetail""><h1>"&strH1&"</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""346494""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
      
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200"" valign=""top""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""346494""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF
			
		Case 7 '### Show & Event ###
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt="""&strAltLogo&"""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldetail""><h1>"&strH1&"</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center"">" & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""346494""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""346494""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF


		Case 8 '### Health CheckUp ###
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"">" & VbCrlf
				strLogo = strLogo & "<tr> " & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt="""&strAltLogo&"""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldetail""><h1>"&strH1&"</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Thailand Health Check Up""><font color=""346494""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Thailand Health Check Up""><font color=""346494""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF

		Case 9 '###Promotion###
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"">" & VbCrlf
				strLogo = strLogo & "<tr> " & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt="""&strAltLogo&"""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldetail""><h1>"&strH1&"</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""346494""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table></td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""346494""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF
		
		Case 10 '###Shopping Cart###
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""250""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt=""Thailand Hotels""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center"">" & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_blue_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""346494""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table></td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200"" valign=""top""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_blue_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""346494""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/discount_thaialand_hotels.asp"" title=""Hot Promotion Hotels in Thailand""><font color=""FE5400""><b>Promotions</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF
			
		Case 11 '###Express Check Out ###
			IF NOT bolPartnerLogo Then
				strLogoTmp = "<div id=""logo_home"" style=""float:left;padding:20px 0px 0px 25px;""><a href=""/""><img src=""/images/small_logo.gif"" border=""0"" alt="""&strAltLogo&"""></a></div>" & VbCrlf
				strLogoTmp = strLogoTmp &  "<div id=""homepage""><h1>"&strH1&"</h1></div>" & VbCrlf
				strLogoTmp = strLogoTmp &  "<div align=""right"">" & VbCrlf
				strLogoTmp = strLogoTmp &  "<img src=""/images/banner_ht2th.jpg""><br />" & VbCrlf
				strLogoTmp = strLogoTmp &  "</div>" & VbCrlf
				strLogoTmp = strLogoTmp &  "<br style=""clear:both"" /><br />" & VbCrlf
			Else
				strLogoTmp = "<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""2"">" &VbCrlf
				strLogoTmp = strLogoTmp & "<tr>" &VbCrlf
				strLogoTmp = strLogoTmp & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" &VbCrlf
				strLogoTmp = strLogoTmp & "<td align=""right"" valign=""top""><a href=""/"" title=""Hotels2Thailand.com Home""><img src=""/images/small_logo_aff.gif"" border=""0"" /></a></td>" &VbCrlf
				strLogoTmp = strLogoTmp & "</tr>" &VbCrlf
				strLogoTmp = strLogoTmp & "</table>" &VbCrlf
			End IF
			
				sub_display_logo = strLogoTmp
		Case 12
			IF NOT bolPartnerLogo Then
				strLogo = strLogo & "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"">" & VbCrlf
				strLogo = strLogo & "<tr> " & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_001-1.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_h-1.gif"" bgcolor=""#0000FF"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/logo.gif"" width=""200"" height=""57"" border=""0"" alt="""&strAltLogo&"""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""right""><div id=""hoteldetail""><h1>"&strH1&"</h1></div></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-spa.asp"" title=""Thailand Spa""><font color=""346494""><b>Spa</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table></td>" & VbCrlf
				strLogo = strLogo & "<td width=""12""><img src=""/images/h_l_002.gif"" width=""12"" height=""82"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			Else
				strLogo = "<table width=""780"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""82"" bgcolor=""#FFFFFF"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "<td bgcolor=""#FFFFF""> " & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""57"" valign=""middle"">" & VbCrlf
				strLogo = strLogo & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" class=""f11"">" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td align=""left"" valign=""top""><img src=""/"&arrLogo(3,0)&""" border=""0"" /></td>" & VbCrlf
				strLogo = strLogo & "<td width=""200""><a href=""/""><img src=""/images/small_logo_aff.gif"" border=""0"" alt=""Hotels2Thailand Home""></a></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "<tr>" & VbCrlf
				strLogo = strLogo & "<td height=""25"" valign=""bottom"" align=""center""> " & VbCrlf
				strLogo = strLogo & "<table border=""0"" cellspacing=""0"" cellpadding=""0"">" & VbCrlf
				strLogo = strLogo & "<tr valign=""bottom"" class=""f13"">" & VbCrlf
				strLogo = strLogo & "<td height=""24"" width=""9""><img src=""/images/b_orange_L.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/"" title=""Thailand Hotels Home""><font color=""FE5400""><b>Home</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""50"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-hotels.asp"" title=""Hotels in Thailand""><font color=""FE5400""><b>Hotels</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""60"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-day-trips.asp"" title=""Thailand Travel, Package Tours, Day Trips, Sightseeing""><font color=""FE5400""><b>Day Trips</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""100"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-water-activity.asp"" title=""Thailand Water Activities, Diving, Canoes, Kayaks, Fishing, Speed Boat, Rafing""><font color=""FE5400""><b>Water Activities</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""80"" background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-golf-courses.asp"" title=""Thailand Golf Courses""><font color=""FE5400""><b>Golf Courses</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-show-event.asp"" title=""Thailand Show and Event""><font color=""FE5400""><b>Show & Event</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_o.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td background=""/images/bg_b_orange.gif"" valign=""middle"" align=""center""><a href=""/thailand-health-check-up.asp"" title=""Hospitals Medical and Health Check Up in Thailand ""><font color=""FE5400""><b>Health</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_o_b.gif"" width=""9"" height=""24"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""70""  background=""/images/bg_b_blue.gif"" valign=""middle"" align=""center""><a href=""/thailand-spa.asp"" title=""Thailand Spa""><font color=""346494""><b>Spa</b></font></a> </td>" & VbCrlf
				strLogo = strLogo & "<td width=""9""><img src=""/images/spacer_b_g.gif"" width=""9"" height=""23"" align=""absmiddle""></td>" & VbCrlf
				strLogo = strLogo & "<td width=""16"" background=""/images/bg_b_green01.gif"" valign=""middle"" align=""center""><a href=""/cart_view.asp"" title=""View Your Booking List""> <img src=""/images/ico_cart_orange.gif"" height=""14"" border=""0""></a></td>" & VbCrlf
				strLogo = strLogo & "<td align=""center"" valign=""middle"" background=""/images/bg_b_green01.gif""><a href=""/cart_view.asp"" title=""View Your Booking List""><font color=""#fe5400""><b>View Booking List</b></font></a></td>" & VbCrlf
				strLogo = strLogo & "<td width=""9"" height=""23""><img src=""/images/bg_green_r.gif"" width=""6"" height=""23""></td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
				strLogo = strLogo & "</td>" & VbCrlf
				strLogo = strLogo & "<td width=""12"">&nbsp;</td>" & VbCrlf
				strLogo = strLogo & "</tr>" & VbCrlf
				strLogo = strLogo & "</table>" & VbCrlf
			End IF
		Case 13
	END SELECT

	Response.Write strLogo

END SUB
%>