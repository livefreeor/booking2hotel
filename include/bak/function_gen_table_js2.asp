<%		
Function 	function_gen_table_js2(arrList,arrCol,bodyColor,bgColor,txtTitle,txtColor,pColor,intType,strCate,intSort,arrList2,arrList3,psid,dest_id,curr,arrRate)
	Dim BCon
	Dim HBCon
	Dim HCCon
	Dim LCCon
	Dim max_col
	Dim intCol
	Dim intDest
	Dim PCCon
	Dim intStar
	Dim strRate
	Dim num1
	Dim num2
	Dim rCon
	Dim cateCon
	Dim cateCons
	Dim intList
	Dim psidCon
	Dim cateLnk
	Dim strDetail
	
	Dim num3
	Dim num4
	Dim num5
	Dim num6
	Dim funcCon
	Dim funcCon2
	Dim rateCon
	
	Dim colSpn 
	Dim picCon
	Dim Rate
	
	IF (strCate <> "29") Then
		colSpn = " colspan = ""2"""				
	End IF
	
	strRate 	= 0					
	BCon 		= ""
	HBCon		= ""
	HCCon 		= ""
	LCCon		= ""
	PCCon		= ""
	cateCon 	= ""
			
	if (bodyColor <> "") then
		BCon = "style = "" border : solid 1px #"&bodyColor&""""
	end if
	
	if (bgColor <> "") then
		 HBCon = "bgcolor=#"&bgColor&""
	end if
				
	if (txtTitle <> "") then
		HCCon = "color=#"&txtTitle&""
	end if
	
	if (txtColor <> "") then
		LCCon = "color=#"&txtColor&""
	end if
	
	if (pColor <> "") then
		PCCon = "color=#"&pColor&""
	end if
	
	IF (psid = "") Then
		psidCon = " "
	Else
		psidCon = "?psid="&psid
	End IF
	
	Select Case strCate
		Case "29" 'hotel
					cateCon 	= "Hotel"
					cateCons	= "Hotels"
					cateLnk  	= "thailand-hotels.asp"
					funcCon	= function_generate_hotel_link(dest_id,"",1)
					funcCon2	= function_generate_hotel_link(dest_id,"",4)
					picCon		=	"thailand-hotels-pic"
		Case "32" 'golf course
					cateCon 	= "Golf Course"
					cateCons	=	"Golf Courses"
					cateLnk	= "thailand-golf-courses.asp"
					funcCon	= function_generate_golf_link(dest_id,"",1)
					funcCon2	= function_generate_golf_link(dest_id,"",4)
					picCon		=	"thailand-golf-pic"
		Case "34" 'day trip
					cateCon 	= "Day Trip"
					cateCons	= "Day Trips"
					cateLnk	=	"thailand-day-trips.asp"
					funcCon	= function_generate_sightseeing_link(dest_id,"",1)
					funcCon2	= function_generate_sightseeing_link(dest_id,"",4)
					picCon		=	"thailand-day-trips-pic"
		Case "36" 'water activity
					cateCon 	= "Water Activity"
					cateCons 	=	"Water Activities"
					cateLnk 	=	"thailand-water-activity.asp"
					funcCon	= function_generate_water_activity_link(dest_id,"",1)
					funcCon2	= function_generate_water_activity_link(dest_id,"",4)
					picCon		=	"thailand-water-activity-pic"
		Case "38" 'show and event
					cateCon 	= "Show And Event"
					cateCons	= "Shows And Events"
					cateLnk 	= "thailand-show-event.asp" 
					funcCon	= function_generate_show_event_link(dest_id,"",1)
					funcCon2	= function_generate_show_event_link(dest_id,"",4)
					picCon		=	"thailand-show-event-pic"
		Case "39" 'health check up
					cateCon 	= "Health Check Up"
					cateCons 	= "Health Check Up"
					cateLnk 	=	"thailand-health-check-up.asp"
					funcCon	= function_generate_health_check_up_link(dest_id,"",1)
					funcCon2	= function_generate_health_check_up_link(dest_id,"",4)
					picCon		=	"thailand-health-pic"
	End Select
		
				Select Case intType
					Case 1,2 	'	Layout 1,2
						Select Case intType
							Case 1
								max_col=1
							Case 2
								max_col=2
						End Select
						intCol=0
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""10"" border = ""0"" width = ""250"">');"
						response.write "document.write('<tr><td align = ""left"" valign = ""top"">');"
						response.write "document.write('<font "& HCCon&"><strong>"&cateCons&" Promotion in "&funcCon2&"</strong></font>');"
						response.write "document.write('</td></tr></table>');"
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""10"" border = ""0"" width = ""550"">');"
						response.write "document.write('<tr>');"
						IF isArray(arrList) Then
						For intList=0 To Ubound(arrList,2)	
							IF intCol mod max_col=0 Then
								response.write "document.write('</tr><tr>');"
							End IF
							response.write "document.write('<td align = ""left"" valign = ""top"">');"	
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""226"">');"
							response.write "document.write('<tr><td colspan = ""2"" align = ""left"" valign = ""top"">');"
							For intStar = 1 to (arrList(2,intList) +0.5)
								if ((arrList(2,intList) - (arrList(2,intList) mod 10)) <> 0 and intStar = (arrList(2,intList) +0.5)) then 
									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout2_red_star_half.gif"" border = ""0"" />');"
								else
									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout2_red_star.gif"" border = ""0"" />');"
								end if
							Next
							response.write "document.write('&nbsp;<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\")&""">');"
							response.write "document.write('<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>');"
							response.write "document.write('</a>');"');"
							response.write "document.write('</strong>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr height = ""5""><td colspan = ""2"">');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td colspan = ""2"" align = ""center"" valign = ""middle"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","\'")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/"&picCon&"/"&arrList(7,intList)&"_a.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""226"" height = ""190"" />');"
							response.write "document.write('</a>');"');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td width = ""146"" align = ""left"" valign = ""top"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/promotion01.gif"" border = ""0"" alt = """&replace(arrList(8,intList),"'","\'")&"""/>&nbsp;"&replace(arrList(8,intList),"'","\'")&"');"
							response.write "document.write('</td><td align = ""right"" valign = ""top"">');"
							response.write "document.write('<font "&PCCon&">From<br />"&FormatNumber((arrList(3,intList)/arrList2(1,0))*intVatFactor,0)&"&nbsp;"&arrList2(2,0)&"</font>');"
							response.write "document.write('</td></tr></table>');"
							response.write "document.write('</td>');"
							intCol=intCol+1
						Next
						response.write "document.write('</tr>');"	
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"	
					Case 3,4	'	Layout 3,4
						Select Case intType
							Case 3
								max_col=2
							Case 4
								max_col=3
						End Select
						intCol=0
						response.write "document.write('<table cellpadding = ""3"" cellspacing = ""0""  "& BCon&">');"
						response.write "document.write('<tr><td "& HBCon&" height = ""30"" colspan = """&max_col&""" align = ""center"">');"
						response.write "document.write('<strong><font "& HCCon&">Hot "&cateCons&" in "&funcCon2&"</font></strong>');"
						response.write "document.write('</td></tr>');"
						IF isArray(arrList) Then
 						For intList=0 To Ubound(arrList,2)	
							IF intCol mod max_col=0 Then
								response.write "document.write('</tr><tr>');"
							End IF
							response.write "document.write('<td valign = ""top"">');"	
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""97"">');"
							response.write "document.write('<tr><td valign = ""top"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","\'")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/"&picCon&"/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\")&""" width = ""90"" height = ""95"" /></a>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td align = ""center"" valign = ""top"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\")&""">');"
							response.write "document.write('<font "&LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font></a></td>');"
							response.write "document.write('</tr></table>');"
							response.write "document.write('</td>');"
							intCol=intCol+1					
						Next
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"		
					Case 5
						Dim strTest
						Dim intvvv
						response.write "document.write('<table width = ""400"" cellpadding = ""5"" cellspacing = ""0"" "&BCon&">');"
						response.write "document.write('<tr><td align = ""center""><strong><font "& HCCon&"> "&funcCon2&" "&cateCons&" Promotion</font></strong>');"
						response.write "document.write('</td></tr>');"
						IF isArray(arrList) Then
						response.write "document.write('<tr><td align = ""center"">');"
						For intList = 0 to Ubound(arrList,2)
							strDetail = arrList(12,intList)
							strDetail = replace(strDetail,"'","&acute;")
							strDetail = replace(strDetail,"""","&quot;") 
							strDetail = replace(strDetail,vbCrLF,"") 
							response.write "document.write('<table width = ""100%"" cellpadding = ""3"" cellspacing = ""0"">');"
							response.write "document.write('<tr><td colspan = ""2"" align = ""left"">');"
							response.write "document.write('<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","&#0039;")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","&#0039;")&""">');"
							response.write "document.write('<font  "&LCCon&">"&replace(arrList(0,intList),"'","&#0039;")&"</font></a>');"
							response.write "document.write('</strong>&nbsp; ');"
							For intStar = 1 to (arrList(2,intList) +0.5)
								if ((arrList(2,intList) - (arrList(2,intList) mod 10)) <> 0 and intStar = (arrList(2,intList) +0.5)) then 
									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout19_star_half.gif"" border = ""0"" />');"
								else
									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout19_star.gif"" border = ""0"" />');"
								end if
							Next
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td align = ""left"" width = ""20%"" valign = ""top"" >');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","&#0039;")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/thailand-hotels-pic/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","&#0039;")&""" width = ""90"" height = ""95"" />');"
							response.write "document.write('</a><br />');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(9,intList)&""" target = ""_blank"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/aff_map.gif"" border = ""0"" />');"
							response.write "document.write('</a>');"
							response.write "document.write('</td><td>');"
							response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(9,intList)&""" target = ""_blank""><font color = ""#FF5D01"">View Map</font>');"
							response.write "document.write('</a></td></tr></table>');"
							response.write "document.write('</td>');"
							response.write "document.write('<td align = ""left"" valign = ""top"" >');"
							response.write "document.write('<strong><font color = """">From "&FormatNumber((arrList(3,intList)/arrList2(1,0))*intVatFactor,0)&"&nbsp;"&arrList2(2,0)&"</font>');"
							response.write "document.write('</strong><br />');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/promotion01.gif"" border = ""0"" alt = """&Mid(replace(arrList(8,intList),"'","&#0039;"),1,100)&"""/>&nbsp;"&Mid(replace(arrList(8,intList),"'","&#0039;"),1,100)&"<br>');"
							response.write "document.write('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"&Mid(strDetail,1,80)&"');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","&#0039;")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<font "&LCCon&"><u>.... More Detail</u></font>');"
							response.write "document.write('</a><br />');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >');"
							response.write "document.write('<tr><td colspan = ""2"">');"
							response.write "document.write('<font color = ""green"">Average User Rating : </font>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td valign = ""bottom"" height = ""22"">');"
							IF (strCate = "32") Then
								Rate = isnull(arrRate(2,intList)) or (arrRate(2,intList) = 0) or (arrRate(1,intList) = 0) or isnull(arrList(1,intList))
								response.write "document.write('strcate32');"
							Else
								Rate = isnull(arrList(11,intList)) or (arrList(11,intList) = 0) or (arrList(10,intList) = 0) or isnull(arrList(10,intList))
							End IF
							if Rate then
								response.write "document.write('&nbsp;Not Yet Rating&nbsp;');"
								response.write "document.write('</td>');"
								response.write "document.write('<td>');"
								response.write "document.write('<font color = ""green""> (0 From 5.0)</font>');"
							else
								select case strCate
									case "29","34","36","38","39"
										num1 = FormatNumber((arrList(11,intList) / arrList(10,intList)),1)
										num2 = int(arrList(11,intList) / arrList(10,intList))
									case "32"
										num1 = FormatNumber((arrRate(1,intList) / (arrRate(2,intList)*6)),1)
										num2 = int(arrRate(1,intList) / (arrRate(2,intList)*6))
								end select
								num3 = FormatNumber((num1 - num2),1)
								IF (num3) = 0 Then
									strRate = num2
									rateCon = strRate
								Else
									strRate = num1
										select case num3
											case 0.1,0.2,0.3,0.4 
												rateCon = num2+0.4
											case 0.5
												rateCon = num2+0.5
											case 0.6,0.7,0.8,0.9
												rateCon = num2+0.6
										end select
								End IF
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/rating_"&rateCon&".gif"" border = ""0""/>');"
							response.write "document.write('</td>');"
							response.write "document.write('<td>');"
							response.write "document.write('<font color = ""green""> ("&strRate&" From 5.0)</font> <a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(9,intList)&psidCon&""" target=""_blank""><u>(Review)</u></a>');"
							end if
							response.write "document.write('</td>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('</table>');"
							response.write "document.write('</td></tr></table>');"
						Next
						response.write "document.write('</td></tr>');"	
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table><br />');"		
					Case 6
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width  = ""600"">');"
						response.write "document.write('<tr><td align = ""center"" colspan = ""2"">');"
						response.write "document.write('<strong><font "& HCCon&">"&funcCon2&" Promotion</font></strong></td>');"
						response.write "document.write('</tr></table><br />');"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2)
							strDetail = arrList(12,intList)
							strDetail = replace(strDetail,"'","&acute;")
							strDetail = replace(strDetail,"""","&quot;") 
							strDetail = replace(strDetail,vbCrLF,"") 
							response.write "document.write('<table cellpadding =""0"" cellspacing = ""5"" border = ""0"" width  = ""600""  "&BCon&">');"
							response.write "document.write('<tr><td width = ""125"" align = ""left"" valign = ""top"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","\'")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/thailand-hotels-pic/"&arrList(7,intList)&"_a.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""120"" height = ""80"" /></a>');"
							response.write "document.write('<br /><table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" ><tr><td>');"
							response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(9,intList)&""" target = ""_blank"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/aff_map.gif"" border = ""0"" /></a>');"
							response.write "document.write('</td><td>');"
							response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(9,intList)&""" target = ""_blank"">');"
							response.write "document.write('<font color = ""#FF5D01"">View Map</font>');"
							response.write "document.write('</a></td></tr></table></td>');"
							response.write "document.write('<td align = ""left"" valign = ""top"">');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""100%"">');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(dest_id,"",1)&"/"&replace(arrList(5,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "&LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>');"
							response.write "document.write('</a></strong>');"
							For intStar = 1 to (arrList(2,intList) +0.5)
								if ((arrList(2,intList) - (arrList(2,intList) mod 10)) <> 0 and intStar = (arrList(2,intList) +0.5)) then 
									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout21_star_half.gif"" border = ""0"" />');"
								else
									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout21_star.gif"" border = ""0"" />');"
								end if
							Next							 
							response.write "document.write('</td>');"
							response.write "document.write('<td align = ""right"">');"
							response.write "document.write('<strong><font "& PCCon&">From "&FormatNumber((arrList(3,intList)/arrList2(1,0))*intVatFactor,0)&"&nbsp;"&arrList2(2,0)&"</font></strong>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td colspan = ""2"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/promotion01.gif"" border = ""0"" alt = ""watting for detail_en""/>&nbsp;<font color = ""#FD6500"">"&replace(arrList(8,intList),"'","\'")&"</font>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td colspan = ""2"">');"
							response.write "document.write('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"&Mid(strDetail,1,200)&"');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(5,intList),"'","\'")&psidCon&""" target=""_blank""><font "&LCCon&"><u> More Detail</u></font></a>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td colspan = ""2"">');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >');"
							response.write "document.write('<tr><td>Average User Rating : </td>');"
							response.write "document.write('<td valign = ""bottom"" height = ""22"">');"
							IF (strCate = "32") Then
								Rate = isnull(arrRate(2,intList)) or (arrRate(2,intList) = 0) or (arrRate(1,intList) = 0) or isnull(arrList(1,intList))
								response.write "document.write('strcate32');"
							Else
								Rate = isnull(arrList(11,intList)) or (arrList(11,intList) = 0) or (arrList(10,intList) = 0) or isnull(arrList(10,intList))
							End IF
							if Rate then
								response.write "document.write('&nbsp;Not Yet Rating&nbsp;');"
								response.write "document.write('</td>');"
								response.write "document.write('<td><font color =""green""> (0 From 5.0)</font><br /></td>');"
							else
								select case strCate
									case "29","34","36","38","39"
										num1 = FormatNumber((arrList(11,intList) / arrList(10,intList)),1)
										num2 = int(arrList(11,intList) / arrList(10,intList))
									case "32"
										num1 = FormatNumber((arrRate(1,intList) / (arrRate(2,intList)*6)),1)
										num2 = int(arrRate(1,intList) / (arrRate(2,intList)*6))
								end select
								num3 = FormatNumber((num1 - num2),1)
								IF (num3) = 0 Then
									strRate = num2
									rateCon = strRate
								Else
									strRate = num1
										select case num3
											case 0.1,0.2,0.3,0.4 
												rateCon = num2+0.4
											case 0.5
												rateCon = num2+0.5
											case 0.6,0.7,0.8,0.9
												rateCon = num2+0.6
										end select
								End IF
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/rating_"&rateCon&".gif"" border = ""0""/>');"
							response.write "document.write('</td>');"
							response.write "document.write('<td>');"
							response.write "document.write('<font color = ""green""> ("&strRate&" From 5.0)</font> <a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(9,intList)&psidCon&""" target=""_blank""><u>(Review)</u></a>');"
							response.write "document.write('</td>');"
							end if
							response.write "document.write('</td></tr></table>');"
							response.write "document.write('</tr></table>	');"					
							response.write "document.write('</td></tr></table><br />');"
						Next
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
				End Select 	'	End intType
End Function
%>