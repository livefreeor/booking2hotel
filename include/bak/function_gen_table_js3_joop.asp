<%
Function 	function_gen_table_js3_joop(arrList,arrCol,bodyColor,bgColor,txtTitle,txtColor,pColor,intType,strCate,intSort,arrList2,arrList3,psid,dest_id,curr,arrRate)
	Dim BCon
	Dim HBCon
	Dim HCCon
	Dim LCCon
	Dim PCCon
	Dim max_col
	Dim intList
	Dim intCol
	Dim intDest
	Dim intStar
	Dim strRate
	Dim num1
	Dim num2
	Dim num3
	Dim num4
	Dim num5
	Dim num6
	Dim cateCon
	Dim cateCons
	Dim psidCon
	Dim cateLnk
	Dim funcCon
	Dim funcCon2
	Dim rateCon
	Dim strDetail 
	
	Dim sqlRate
	Dim rsList5
	Dim colSpn 
	Dim picCon
	Dim Rate
	Dim Rate2
	Dim intCount
	Dim Table
	Dim strBody
							
	Dim intRat
	Dim ratStatus
	Dim data1
	Dim data2
	
	
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
					Case 1
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""300"">');"
						response.write "document.write('<tr height = ""25"">');"
						response.write "document.write('<td align = ""left"" valign = ""top"">');"
						response.write "document.write('<font "& HCCon&"><strong>"&cateCons&" in "&funcCon2&"</strong></font>');"
						response.write "document.write('</td></tr></table>');"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2)
							if (bodyColor = "") then
								response.write "document.write('<table cellpadding = ""0"" cellspacing = ""3"" width = ""310"" style ="" border-bottom : solid 1px #000000"">');"
							else
								response.write "document.write('<table cellpadding = ""0"" cellspacing = ""3"" width = ""310"" style ="" border-bottom : solid 1px #"&bodyColor&""">');"
							end if
							response.write "document.write('<tr><td align = ""left"" valign = ""top"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>');"
							response.write "document.write('</a>');"
							response.write "document.write('<br>');"
							IF (strCate = "29") Then
								For intStar = 1 to (arrList(3,intList)+0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout1_star_half.gif"" border = ""0"" />');"
									else
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout1_star.gif"" border = ""0"" />');"
									end if
								Next	
							End IF						 
							response.write "document.write('</td><td width = ""55"" align = ""right"" valign = ""top"">');"
							response.write "document.write('<font "& PCCon&">From<br />');"
							response.write "document.write('"&arrList2(2,0)&"&nbsp;"&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&"');"
							response.write "document.write('</font></td></tr></table>');"
						Next
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
					Case 2
						response.write "document.write('<table height = ""25"" cellpadding = ""0"" cellspacing = ""5"" width = ""200"" "& HBCon&" "& BCon&">');"
						response.write "document.write('<tr><td align = ""center"" colspan=""2"">');"
						response.write "document.write('<strong><font "& HCCon&">"&cateCons&" in "&funcCon2&"</font></strong>');"
						response.write "document.write('</td></tr></table>');"
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""5"" width = ""200"" "& BCon&">');"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2)
							response.write "document.write('<tr><td valign = ""top"">');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""5"" width = ""100%"" style =""border-bottom : dashed 1px #A4B7DF"">');"
							response.write "document.write('<tr><td width = ""20%"" align = ""center"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/"&picCon&"/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""50"" height = ""53"" />');"
							response.write "document.write('</a>');"
							response.write "document.write('<br>');"
							IF (strCate = "29") Then
								For intStar = 1 to (arrList(3,intList) +0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout3_red_star_half.gif"" border = ""0"" />');"
									else
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout3_red_star.gif"" border = ""0"" />');"
									end if
								Next			
							End IF				 
							response.write "document.write('</td>');"
							response.write "document.write('<td align = ""center"" valign = ""top"">');"
							response.write "document.write('<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title =  """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "&LCCon&"><u>"&replace(arrList(0,intList),"'","\'")&"</u></font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</strong><br />');"
							response.write "document.write('<font "& PCCon&">Price from "&arrList2(2,0)&"&nbsp;"&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&"</font>');"
							response.write "document.write('</td></tr></table></td></tr>');"
						Next
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"
					Case 3,4,5,6
						Dim wid
						Select Case intType
							Case 3
								max_col=1
								wid=200
							Case 4
								max_col=2
								wid=390
							Case 5
								max_col=3
								wid=585
							Case 6
								max_col=4
								wid=780
						End Select
						intCol=0	
						response.write "document.write('<table cellpadding = ""4"" cellspacing = ""0"" width = "&wid&" "& BCon&">');"
						response.write "document.write('<tr>');"
						response.write "document.write('<td align = ""center"" "& HBCon&" colspan = "&max_col&">');"
						response.write "document.write('<strong><font "& HCCon&">"&funcCon2&" "&cateCons&"</font></strong>');"
						response.write "document.write('</td></tr>');"
						IF isArray(arrList) Then
						response.write "document.write('<tr>');"
						For intList = 0 to Ubound(arrList,2)
							IF intCol mod max_col=0 Then
								response.write "document.write('</tr><tr>');"
							End IF
							response.write "document.write('<td align = ""center"" valign = ""top"">');"
							response.write "document.write('<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</strong>');"
							IF (strCate = "29") Then
								response.write "document.write('<br>');"
								For intStar = 1 to (arrList(3,intList) +0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout3_red_star_half.gif"" border = ""0"" />');"
									else
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout3_red_star.gif"" border = ""0"" />');"
									end if
								Next		
							End IF
							response.write "document.write('<br>');"
							response.write "document.write('<font "& PCCon&"><u>From "&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&" "&arrList2(2,0)&"</u></font>');"
							response.write "document.write('</td>');"
							intCol=intCol+1
						Next
						response.write "document.write('</tr>');"
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"			
					Case 7
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" width = ""400""  "& BCon&">');"
						response.write "document.write('<tr><td height = ""25"" align = ""left"" "& HBCon&">');"
						response.write "document.write('<font "& HCCon&"><strong>&nbsp;&nbsp;"&cateCons&" in "&funcCon2&"</strong></font></td>');"
						response.write "document.write('</tr>');"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2)
							response.write "document.write('<tr><td align = ""center"">');"
							response.write "document.write('<table cellpadding = ""3"" cellspacing = ""0"" border = ""0"" width = ""400"">');"
							response.write "document.write('<tr><td align = ""left"" valign = ""top"">');"
							response.write "document.write('<strong>&nbsp;"&intList+1&".</strong></td>');"
							response.write "document.write('<td align = ""left"">');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""100%"">');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</strong></td>');"
							response.write "document.write('</tr>');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('Average User Rating :</td>');"
							IF isArray(arrRate) Then
							For intRat = 0 To Ubound(arrRate,2)
								IF (arrList(11,intList) = arrRate(0,intRat)) Then
									data1 = arrRate(2,intRat)
									data2 = arrRate(1,intRat)
									Exit For
								End IF
							Next
							
							IF (strCate = "32") Then
								ratStatus = (data1 = 0) or (data2 = 0)
							End IF

							Else
									data1 = 0
									data2 = 0
									ratStatus = true
							End IF

							IF (ratStatus) Then
								response.write "document.write('<td>');"
								response.write "document.write('&nbsp;Not Yet Rating&nbsp;');"
								response.write "document.write('</td>');"
								response.write "document.write('<td>');"
								'response.write "document.write('<font color = ""green""> (0 From 5.0)</font><br />');"
								response.write "document.write('</td>');"
							else
								select case strCate
									case "29","34","36","38","39"
										num1 = FormatNumber((arrList(12,intList) / arrList(11,intList)),1)
										num2 = int(arrList(12,intList) / arrList(11,intList))
									case "32"
										num1 = FormatNumber((data2 / (data1*6)),1)
										num2 = int(data2 / (data1*6))
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
								response.write "document.write('<td valign = ""bottom"" height = ""22"">');"
								response.write "document.write('<img src = ""http://www.booking2hotels.com/images/rating_"&rateCon&".gif"" border = ""0""/>');"
								response.write "document.write('</td>');"
								response.write "document.write('<td>');"
								response.write "document.write('<font color = ""green""> ("&strRate&" From 5.0)</font>');"
								response.write "document.write('<a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(8,intList)&""" target=""_blank"">');"
								response.write "document.write('<u>(Review)</u>');"
								response.write "document.write('</a>');"
								response.write "document.write('<br /></td>');"
							end if
							response.write "document.write('</tr></table></td>');"
							response.write "document.write('</tr></table>');"
							IF (strCate = "29") Then
								response.write "document.write('"&cateCon&" Class : ');"
								For intStar = 1 to (arrList(3,intList)+0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout5_star_half.gif"" border = ""0"" />');"
									else
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout5_star.gif"" border = ""0"" />');"
									end if
								Next		
							response.write "document.write('<br>');"
							End IF					 
							response.write "document.write('<font "& PCCon&">Price from "&arrList2(2,0)&"&nbsp;"&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&"</font><br /><br />');"
							response.write "document.write('</td></tr></table></td></tr>');"
						Next
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"					
					Case 8
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" width = ""500""  "& BCon&">');"
						response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font "& HCCon&">"&funcCon2&" "&cateCons&"</font></strong></td></tr>');"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2)
							IF intList=45 Then
								Exit For
							End IF
							strDetail = arrList(10,intList)
							strDetail = replace(strDetail,"'","&acute;")
							strDetail = replace(strDetail,"""","&quot;") 
							strDetail = replace(strDetail,vbCrLF,"") 
							response.write "document.write('<tr><td align = ""center"">');"
							response.write "document.write('<table width = ""480"" cellpadding = ""3"" cellspacing = ""0"" border = ""0"">');"
							response.write "document.write('<tr><td align = ""left"">');"
							response.write "document.write('<table width = ""480"" cellpadding = ""5"" cellspacing = ""0"" border = ""0"" "& HBCon&">');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "& LCCon&"><u>"&replace(arrList(0,intList),"'","\'")&"</u></font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</strong>');" 
							IF (strCate = "29") Then
								For intStar = 1 to (arrList(3,intList)+0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout5_star_half.gif"" border = ""0"" />');"
									else
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout5_star.gif"" border = ""0"" />');"
									end if
								Next		
							End IF					 
							response.write "document.write('</td><td align = ""right"">');"
							response.write "document.write('<font "& PCCon&"><strong>From "&arrList2(2,0)&" "&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&"</strong></font>');"
							response.write "document.write('</td></tr></table></td></tr>');"
							response.write "document.write('<tr><td align = ""left"">');"
							response.write "document.write('<table width = ""480"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
							response.write "document.write('<tr><td width = ""80"" align = ""left"" valign = ""top"" style=""height: 38px"">');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""100%"">');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/"&picCon&"/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""70"" height = ""74"" />');"
							response.write "document.write('</a>');"
							response.write "document.write('</td></tr><tr>');"
							response.write "document.write('<td height = ""25"">');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" ><tr><td>');"
							IF (strCate = "29") Then
								response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">');"
								response.write "document.write('<img src = ""http://www.booking2hotels.com/images/aff_map.gif"" border = ""0"" />');"
								response.write "document.write('</a>');"
							ElseIF (strCate = "32") Then	
								response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-golf-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">');"
								response.write "document.write('<img src = ""http://www.booking2hotels.com/images/aff_map.gif"" border = ""0"" />');"
								response.write "document.write('</a>');"
							End IF
							response.write "document.write('</td><td>');"
							IF (strCate = "29") Then
								response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">');"
								response.write "document.write('<font color = ""#FF5D01"">View Map</font>');"
								response.write "document.write('</a>');"
							ElseIF (strCate = "32")  Then
								response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-golf-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">');"
								response.write "document.write('<font color = ""#FF5D01"">View Map</font>');"
								response.write "document.write('</a>');"
							End IF
							response.write "document.write('</td></tr></table>');"
							response.write "document.write('</td></tr></table></td>');"
							response.write "document.write('<td valign = ""top"" style=""height: 38px"">');"
							IF (strCate = "29") Then
								response.write "document.write('"&arrList(1,intList)&"');"
								response.write "document.write(',"&funcCon2&"<br />');"
							Else
								response.write "document.write('"&arrList(5,intList)&"');"
							End IF
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('Average User Rating : ');"
							response.write "document.write('</td>');"
							
							IF isArray(arrRate) Then
							
							For intRat = 0 To Ubound(arrRate,2)
								IF (arrList(11,intList) = arrRate(0,intRat)) Then
									data1 = arrRate(2,intRat)
									data2 = arrRate(1,intRat)
									Exit For
								End IF
							Next
							
							IF (strCate = "32") Then
								ratStatus = (data1 = 0) or (data2 = 0)
							End IF
							
							Else
									data1 = 0
									data2 = 0
									ratStatus = true
							End IF

							if ratStatus then
									response.write "document.write('<td>');"
									response.write "document.write('&nbsp;Not Yet Rating&nbsp;');"
									response.write "document.write('</td>');"
									response.write "document.write('<td>');"
									'response.write "document.write('<font color = ""green""> (0 From 5.0)</font><br />');"
									response.write "document.write('</td>');"
							else
								select case strCate
									case "29","34","36","38","39"
										num1 = FormatNumber((arrList(12,intList) / arrList(11,intList)),1)
										num2 = int(arrList(12,intList) / arrList(11,intList))
									case "32"
										num1 = FormatNumber((data2 / (data1*6)),1)
										num2 = int(data2 / (data1*6))
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
								response.write "document.write('<td valign = ""bottom"" height = ""22"">');"
								response.write "document.write('<img src = ""http://www.booking2hotels.com/images/rating_"&rateCon&".gif"" border = ""0""/>');"
								response.write "document.write('</td>');"
								response.write "document.write('<td>');"
								response.write "document.write('<font color = ""green""> ("&strRate&" From 5.0)</font>');"
								response.write "document.write('<a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(8,intList)&""" target=""_blank"">');"
								response.write "document.write('<u>(Review)</u>');"
								response.write "document.write('</a>');"
								response.write "document.write('<br /></td>');"
							end if
							response.write "document.write('</tr></table>');"
							response.write "document.write('"&Mid(strDetail,1,150)&"');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<font "& LCCon&"><u>...read more</u></font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</td></tr></table>');"
							response.write "document.write('</td></tr></table><br /></td></tr>');"	
						Next
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"
					Case 9
						if (bodyColor = "") then
							response.write "document.write('<table width = ""300"" cellpadding = ""4"" cellspacing = ""0"" style =""border :1px solid #000000"">');"
						else
							response.write "document.write('<table width = ""300"" cellpadding = ""4"" cellspacing = ""0"" "& BCon&">');"
						end if
						response.write "document.write('<tr><td colspan = ""2"" align = ""left"">');"
						response.write "document.write('<strong><font "& HCCon&">&nbsp;"&funcCon2&" "&cateCons&"</font></strong>');"
						response.write "document.write('</td></tr></table>');"
						if (bodyColor = "") then
							response.write "document.write('<table width = ""300"" cellpadding = ""0"" cellspacing = ""6"" style =""border : solid 1px #000000"">');"
						else
							response.write "document.write('<table width = ""300"" cellpadding = ""0"" cellspacing = ""6"" "& BCon&">');"
						end if
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2) 
							response.write "document.write('<tr><td align = ""left"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "& LCCon&"><u>"&replace(arrList(0,intList),"'","\'")&"</u></font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</td>');"
							response.write "document.write('<td align = ""right"" valign = ""top"" width = ""20%"">');"
							response.write "document.write('<font "& PCCon&">"&arrList2(2,0)&" "&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&"</font>');"
							response.write "document.write('</td></tr>');"
						Next
						response.write "document.write('<tr><td colspan = ""2"" align = ""left"">');"
						response.write "document.write('<br />Click <u>');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&cateLnk&psidCon&""" target=""_blank"">');"
						response.write "document.write('<font color = ""red"">here</font>');"
						response.write "document.write('</a>');"
						response.write "document.write('</u> for more information');"
						response.write "document.write('<br /><br /></td>');"
						response.write "document.write('</tr>');"
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"
					Case 10
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" width = ""300""  "& BCon&"');"
						response.write "document.write('<tr height = ""25"">');"
						response.write "document.write('<td colspan = ""2"" align = ""left"">');"
						response.write "document.write('<strong><font "& HCCon&">&nbsp;More Great Offers</font></strong>');"
						response.write "document.write('</td></tr>');"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2) 
							response.write "document.write('<tr><td align = ""center"" width = ""15"" valign = ""top"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/blue_bullet.gif"" border = ""0"" />');"
							response.write "document.write('</td>');"
							response.write "document.write('<td align = ""left"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</td></tr>');"
						Next
						response.write "document.write('<tr><td colspan = ""2"">&nbsp;</td></tr>');"
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"
					Case 11
						strBody = "<table cellspacing = ""0"" cellpadding = ""0"" border = ""0"">"
						strBody = strBody&"<tr height = ""25""><td><strong>Bangkok hotel</strong></td></tr>"
						strBody = strBody&"</table>"
						Table="<table border=""1""><tr><td><strong>Bangkok hotel</strong></td></tr></table>"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2)
							strDetail = arrList(10,intList)
							strDetail = replace(strDetail,"'","&acute;")
							strDetail = replace(strDetail,"""","&quot;") 
							strDetail = replace(strDetail,vbCrLF,"") 
							Table = "<table border = ""0"" cellpadding = ""7"" cellspacing = ""0""  "& BCon&" width = ""410""><tr>"
							IF (strCate = "29") Then
							Table = Table&"<td align = ""left"" style ="" border-bottom : dashed 1px #A1BFCD"">"
								For intStar = 1 to (arrList(3,intList)+0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
									Table = Table&"<img src = ""http://www.booking2hotels.com/images/layout11_star_half_yellow.gif"" border = ""0"" />"
									else
									Table = Table&"<img src = ""http://www.booking2hotels.com/images/layout11_star_yellow.gif"" border = ""0"" />"
									end if
								Next	
								Table = Table&"</td>"
							End IF	
							Table = Table&"<td align = ""left"" style ="" border-bottom : dashed 1px #A1BFCD"""&colSpn&"><strong>"
							Table = Table&"<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrLIst(0,intList),"'","\'")&""">"
							Table = Table&"<font "& LCCon&"><u>"&replace(arrList(0,intList),"'","\'")&"</u></font>"
							Table = Table&"</a></strong></td></tr><tr><td align = ""center"" valign = ""middle"" width = ""5%"">"
							Table = Table&"<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"">"
							Table = Table&"<img src = ""http://www.hotels2thailand.com/"&picCon&"/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""70"" height = ""74""  "& BCon&"/>"
							Table = Table&"</a></td></td><td align = ""left""><table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""100%""><tr><td valign = ""top"">"
							IF (strCate = "29") Then
								Table = Table&"<strong>"&arrList(1,intList)&"</strong><br />"
							End IF
							Table = Table&""&Mid(strDetail,1,150)&"<u>"
							Table = Table&"<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"">"
							Table = Table&"<font color = ""#A0110F""><u>...More</u></font></a></u></td><td valign = ""middle"">"
							Table = Table&"<table width = ""95"" cellpadding = ""0"" cellspacing = ""0""  "& BCon&"><tr><td align = ""left"" valign = ""middle"">"
							Table = Table&"<br />&nbsp;From <font "& PCCon&"><strong>"&arrList2(2,0)&" "&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&"</strong></font>"
							IF (strCate ="29") or (strCate = "32")Then
								Table = Table&"<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" ><tr><td>"
								if (strCate = "29") then
									Table = Table&"<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">"
								elseif (strCate = "32") then
									Table = Table&"<a href = ""http://www.hotels2thailand.com/thailand-golf-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">"
								end if
								Table = Table&"<img src = ""http://www.booking2hotels.com/images/aff_map.gif"" border = ""0"" />"
								Table = Table&"</a></td><td>"
								if (strCate = "29") then
									Table = Table&"<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">"
								elseif (strCate = "32") then
									Table = Table&"<a href = ""http://www.hotels2thailand.com/thailand-golf-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">"
								end if
								Table = Table&"<font color = ""green"">View Map</font>"
								Table = Table&"</a></td></tr></table>"
							End IF
							Table = Table&"&nbsp;<br></td>"
							Table = Table&"</tr></table></td></tr></table></td></tr><tr>"
							Table = Table&"<tr><td align = ""left"" colspan = ""2"">"
							Table = Table&"<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >"
							Table = Table&"<tr><td><font color = ""#858585"">Average User Rating</font> : </td>"
							
							IF isArray(arrRate) Then
							For intRat = 0 To Ubound(arrRate,2)
								IF (arrList(11,intList) = arrRate(0,intRat)) Then
									data1 = arrRate(2,intRat)
									data2 = arrRate(1,intRat)
									Exit For
								End IF
							Next
							
							IF (strCate = "32") Then
								ratStatus = (data1 = 0) or (data2 = 0)
							End IF
							
							Else
									data1 = 0
									data2 = 0
									ratStatus = true
							End IF

							if ratStatus then
								Table = Table&"<td>&nbsp;Not Yet Rating&nbsp;"
								Table = Table&"</td><td>"
								'Table = Table&"<font color = ""green""> (0 From 5.0)</font><br />"
								Table = Table&"</td>"
							else
								select case strCate 
									case "29","34","36","38","39"
										num1 = FormatNumber((arrList(12,intList) / arrList(11,intList)),1)
										num2 = int(arrList(12,intList) / arrList(11,intList))
									case "32"
										num1 = FormatNumber((data2 / (data1*6)),1)
										num2 = int(data2 / (data1*6))
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
								Table = Table&"<td valign = ""bottom"" height = ""22"">"
								Table = Table&"<img src = ""http://www.booking2hotels.com/images/rating_"&rateCon&".gif"" border = ""0""/>"
								Table = Table&"</td><td><font color = ""green""> ("&strRate&" From 5.0)</font>"
								if (strCate = "29") then
									Table = Table&"<a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(8,intList)&""" target=""_blank"">"
								elseif(strCate = "32") then
									Table = Table&"<a href = ""http://www.hotels2thailand.com/review_golf.asp?id="&arrList(8,intList)&""" target=""_blank"">"
								end if
								Table = Table&"<u>(Review)</u></a><br></td>"
							end if
							Table = Table&"</table></td></tr></table><br>"
							response.Write "document.write('"&Table&"');"
						Next
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
					Case 12
						response.write "document.write('<table width = ""300"" cellpadding = ""3"" cellspacing = ""0"">');"
						response.write "document.write('<tr height = ""25""><td align = ""center""><font "& HCCon&"><strong>"&funcCon2&" "&cateCons&"</strong></font></td></tr>');"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2) 
							response.write "document.write('<tr><td valign = ""top"" style ="" border-top : dashed 1px"">');"
							response.write "document.write('<table width = ""100%"" cellpadding = ""10"" cellspacing = ""0"">');"
							response.write "document.write('<tr><td align = ""left"" bgcolor = ""#E8F4DE"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
							response.write "document.write('<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>');"
							response.write "document.write('</a>');"
							IF (strCate = "29") Then
								response.write "document.write('<br />"&arrList(1,intList)&" ');"
								For intStar = 1 to (arrList(3,intList)+0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout15_star_half.gif"" border = ""0"" />');"
									else
																							
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout15_star.gif"" border = ""0"" />');"
									end if
								Next		
							End IF					 
							response.write "document.write('</td>');"
							response.write "document.write('<td width = ""90"" bgcolor = ""#CEE6B4"" align = ""center"" valign = ""middle"">');"
							response.write "document.write('<font "& PCCon&">From "&arrList2(2,0)&" ');"
							response.write "document.write('</font></td></tr></table></td></tr>');"
							response.write "document.write('<tr><td height = ""5""></td></tr>');"
						Next
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"
					Case 13,14
						Dim cSpan
						Select Case intType
							Case 13
								max_col=2
								cSpan=4
							Case 14
								max_col=3
								cSpan=6
						End Select
						intCol=0
						response.write "document.write('<table width = ""200"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
						response.write "document.write('<tr><td align = ""left"">');"
						response.write "document.write('<strong><font "& HCCon&">"&cateCons&" in "&funcCon2&"</font></strong>');"
						response.write "document.write('</td></tr></table><br />');"
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
						IF isArray(arrList) Then
						response.write "document.write('<tr>');"
						For intList = 0 to Ubound(arrList,2)
							IF intCol mod max_col=0 Then
								response.write "document.write('</tr>');"
								response.write "document.write('<tr><td height = ""10"" colspan = """&cSpan&"""></td></tr>');"
								response.write "document.write('<tr>');"
							End IF
							response.write "document.write('<td width = ""200"" align = ""left"" valign = ""top""  "& BCon&">');"
							response.write "document.write('<table cellpadding = ""2"" cellspacing = ""3"" width = ""100%"" border = ""0"">');"
							response.write "document.write('<tr><td colspan = """&max_col&""" align = ""left"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrLIst(0,intLIst),"'","\'")&""">');"
							response.write "document.write('<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td width = ""30%"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/"&picCon&"/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""50"" height = ""53"" />');"
							response.write "document.write('</a>');"
							response.write "document.write('</td>');"
							response.write "document.write('<td align = ""left"">');"
							IF (strCate = "29") Then
								response.write "document.write('"&arrList(1,intList)&"<br />');"
								For intStar = 1 to (arrList(3,intList)+0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout22_star_half.gif"" border = ""0"" />');"
									else
										response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout22_star.gif"" border = ""0"" />');"
									end if
								Next		
							End IF					 
							response.write "document.write('<br />');"
							response.write "document.write('<font "& PCCon&">Price From "&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&" "&arrList2(2,0)&"</font>');"
							response.write "document.write('</td></tr></table></td>');"
							response.write "document.write('<td width = ""10""></td>');"
							response.write "document.write('</td>');"
							intCol = intCol + 1
						Next
						response.write "document.write('</tr>');"
						Else
							response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
						End IF
						response.write "document.write('</table>');"
					Case 15
						Dim starCon
						Table="<table width = ""510"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">"
						Table=Table&"<tr><td align = ""center"">"
						Table=Table&"<strong><font "& HCCon&">"&cateCons&" in "&funcCon2&"</font></strong>"
						Table=Table&"</td></tr></table><br />"
						Table=Table&"<table cellpadding = ""3"" cellspacing = ""0"" border = ""0"">"
						IF isArray(arrList) Then
						Table=Table&"<tr>"
						intCol = 0
						For intList = 0 to Ubound(arrList,2)
							IF intCol mod 2=0 Then
								Table=Table&"</tr><tr><td height = ""10""></td><td height = ""10""></tr><tr>"
							End IF
							Table=Table&"<td width = ""250"" valign = ""top"" align = ""center""  "& BCon&">"
							Table=Table&"<table width = ""100%"" cellpadding = ""3"" cellspacing = ""0"" border = ""0"">"
							Table=Table&"<tr><td colspan = ""2"" align = ""left"">"
							Table=Table&"<strong><a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrLIst(0,intLIst),"'","\'")&""">"
							Table=Table&"<font "& LCCon&">"&replace(arrList(0,intLIst),"'","\'")&"</font>"
							Table=Table&"</a></strong>"
							IF (strCate = "29") Then
									select case int(arrList(3,intList))
										case 1
											starCon = "star1.0.jpg"
										case 2
											starCon = "star2.0.jpg"
										case 3
											starCon = "star3.0.jpg"
										case 4
											starCon = "star4.0.jpg"
										case 5
											starCon = "star5.0.jpg"
									end select
								Table=Table&"&nbsp;<img src = ""http://www.booking2hotels.com/images/"&starCon&""" border = ""0"" />"
							End IF
							Table=Table&"</td></tr>"
							Table=Table&"<tr><td width = ""40%"" valign = ""top"" align = ""left"">"
							Table=Table&"<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"">"
							Table=Table&"<img src = ""http://www.hotels2thailand.com/"&picCon&"/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""90"" height = ""95"" /></a>"
							IF (strCate = "29") or (strCate = "32") Then
								Table=Table&"<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" ><tr><td>"
								if (strCate = "29") then
									Table=Table&"<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">"
								elseif (strCate = "32") then
									Table=Table&"<a href = ""http://www.hotels2thailand.com/thailand-golf-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">"
								end if
								Table=Table&"<img src = ""http://www.booking2hotels.com/images/aff_map.gif"" border = ""0"" /></a></td><td>"
								if (strCate = "29") then
									Table=Table&"<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">"
								else
								 	Table=Table&"<a href = ""http://www.hotels2thailand.com/thailand-golf-map.asp?id="&arrList(8,intList)&""" target = ""_blank"">"
								end if
								Table=Table&"<font color = ""#FF5D01"">View Map</font></a></td></tr></table>"
							Else
							Table=Table&"&nbsp;</td>"
							End IF
							Table=Table&"<td align = ""left"" valign = ""middle"">"
							Table=Table&"<font><strong>Special Offers</strong><br />From</font><br />"
							Table=Table&"<div align = ""center"">"
							Table=Table&"<font size = ""3"" "& PCCon&">"&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&" "&arrList2(2,0)&"</font><br />"
							Table=Table&"<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrLIst(0,intLIst),"'","\'")&""">"
							Table=Table&"<img src = ""http://www.booking2hotels.com/images/layout28_book-now.gif"" border = ""0""/>"
							Table=Table&"</a></div><br /><font color = ""green"">Average User Rating :<br />"
							IF isArray(arrRate) Then
							For intRat = 0 To Ubound(arrRate,2)
								IF (arrList(11,intList) = arrRate(0,intRat)) Then
									data1 = arrRate(2,intRat)
									data2 = arrRate(1,intRat)
									Exit For
								End IF
							Next
							
							IF (strCate = "32") Then
								ratStatus = (data1 = 0) or (data2 = 0)
							End IF
							
							Else
									data1 = 0
									data2 = 0
									ratStatus = true
							End IF

							if ratStatus then
								Table=Table&"&nbsp;Not Yet Rating&nbsp;<br />"
								'Table=Table&"(0 From 5.0)</font><br />"
							else
								select case strCate
									case "29","34","36","38","39"
										num1 = FormatNumber((arrList(12,intList) / arrList(11,intList)),1)
										num2 = int(arrList(12,intList) / arrList(11,intList))
									case "32"
										num1 = FormatNumber((data2 / (data1*6)),1)
										num2 = int(data2 / (data1*6))
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
									Table=Table&"<img src = ""http://www.booking2hotels.com/images/rating_"&rateCon&".gif"" border = ""0""/><br />"
									Table=Table&"("&strRate&" From 5.0)</font>"
									Table=Table&"<a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(8,intList)&""" target=""_blank"">"
									Table=Table&"<u>(Review)</u></a><br />"
							end if
							Table=Table&"</td></tr></table></td><td width = ""10""></td>"
							intCol = intCol + 1
						Next
						Table=Table&"</tr>"
						Else
							Table=Table&"<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>"
						End IF
						Table=Table&"</table>"
						response.Write "document.write('"&Table&"');"
					Case 16
						strBody="<table cellspacing = ""0"" cellpadding = ""0"" border = ""0"" width = ""200"">"
						strBody=strBody&"<tr><td align = ""center"">"
						strBody=strBody&"<strong><font "& HCCon&">"&cateCons&" in "&funcCon2
						strBody=strBody&"</strong></td></tr></table><br />"
						response.Write "document.write('"&strBody&"');"
						IF isArray(arrList) Then
						For intList = 0 to Ubound(arrList,2)
							Table="<table cellspacing = ""3"" cellpadding = ""0"" border = ""0"" width = ""165"">"
							Table=Table&"<tr><td width = ""160"" align = ""left"" colspan = ""2""><strong>"
							Table=Table&"<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"" title = """&replace(arrLIst(0,intLIst),"'","\'")&""">"
							Table=Table&"<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font>"
							Table=Table&"</a></strong></td></tr><tr><td width = ""55"" align = ""left"" valign = ""top"">"
							Table=Table&"<a href=""http://www.hotels2thailand.com/"&funcCon&"/"&replace(arrList(6,intList),"'","\'")&psidCon&""" target=""_blank"">"
							Table=Table&"<img src = ""http://www.hotels2thailand.com/"&picCon&"/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""50"" height = ""53"" />"
							Table=Table&"</a></td><td width = ""105"" align = ""left"" valign = ""middle"">"
							IF (strCate = "29") Then
								Table=Table&""&arrList(1,intList)&"<br />"
								For intStar = 1 to (arrList(3,intList)+0.5)
									if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
										Table=Table&"<img src = ""http://www.booking2hotels.com/images/layout23_star_half.gif"" border = ""0"" />"
									else
										Table=Table&"<img src = ""http://www.booking2hotels.com/images/layout23_star.gif"" border = ""0"" />"
									end if
								Next		
							End IF		
							Table=Table&"<br><font "& PCCon&">From "&arrList2(2,0)&" "&FormatNumber((arrList(4,intList)/arrList2(1,0))*intVatFactor,0)&"</font>"
							Table=Table&"</td></tr><tr><td align = ""left"" colspan = ""2"">Average User Rating :<br />"
							IF isArray(arrRate) Then
							For intRat = 0 To Ubound(arrRate,2)
								IF (arrList(11,intList) = arrRate(0,intRat)) Then
									data1 = arrRate(2,intRat)
									data2 = arrRate(1,intRat)
									Exit For
								End IF
							Next
							
							IF (strCate = "32") Then
								ratStatus = (data1 = 0) or (data2 = 0)
							End IF
									
							Else
									data1 = 0
									data2 = 0
									ratStatus = true
							End IF

							if ratStatus then
								Table=Table&"<font color = ""green"">&nbsp;Not Yet Rating<br />"
								'Table=Table&"&nbsp;<br>(0 From 5.0)</font><br />"
							else
								select case strCate
										case "29","34","36","38","39"
											num1 = FormatNumber((arrList(12,intList) / arrList(11,intList)),1)
											num2 = int(arrList(12,intList) / arrList(11,intList))
										case "32"
											num1 = FormatNumber((data2 / (data1*6)),1)
											num2 = int(data2 / (data1*6))
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
								Table=Table&"<img src = ""http://www.booking2hotels.com/images/rating_"&rateCon&".gif"" border = ""0""/><br>"
								Table=Table&"<font color = ""green"">("&strRate&" From 5.0)</font><font>"
								Table=Table&"<a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(8,intList)&""" target=""_blank"">"
								Table=Table&"<u>(Review)</u></a>"
								Table=Table&""&arrList(1,intList)&"<br />"
							end if
							Table=Table&"</td></tr></table><br />"
							response.Write "document.write('"&Table&"');"
						Next
						Else
							Table=Table&"<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>"
						End IF
						'response.Write "document.write('"&Table&"');"
			End Select	'	End intType
End Function			
%>