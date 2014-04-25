<%
Function function_show_link(Byval strSql,strLCol,strBCol,strHBCol,strHCol,strCol,strPsid,strShow,strSql2,strCate,cShow,arrdest_id)
	Dim recList
	Dim arrList
	Dim arrList2
	Dim arrcols
	Dim intCols
	Dim intArr
	Dim s_pic
	Dim s_n
	Dim s_hc
	Dim s_loc
	Dim s_mp
	Dim strPic
	Dim strH
	Dim strC
	Dim strL
	Dim strP
	Dim star	
	Dim BCon
	Dim HBCon
	Dim HCCon
	Dim LCCon
	Dim picCon
	Dim nCon
	Dim locCon
	Dim mpCon
	Dim num
	Dim proCateCon
	Dim func
	Dim psidCon
	Dim cateLnk
	Dim funcCon
	Dim strDetail
	Dim strDetail2

	BCon = ""
	HBCon= ""
	HCCon = ""
	LCCon = ""

	psidCon = ""
	if (strPsid <> "") then
		psidCon = "?psid="&strPsid&""
	end if		

	if (strBCol <> "") then
		 BCon = "bgcolor=#"&strBCol&""
	end if
	if (strHBCol <> "") then
		 HBCon = "bgcolor=#"&strHBCol&""
	end if
			
	if (strHCol <> "") then
		HCCon = "color=#"&strHCol&""
	end if
	if (strLCol <> "") then
		LCCon = "color=#"&strLCol&""
	end if
			
	Select Case strCate
		Case "29" 'hotel
					pro_cate 		= "hotels"
					cateLnk		= "hotels"
					proCateCon 	= "Hotels"
					func				= "thailand-hotels.asp"
					funcCon 		= function_generate_hotel_link(arrdest_id,"",1)
		Case "32" 'golf course
					pro_cate 		= "golf-courses"
					cateLnk		= "golf"
					proCateCon 	= "Golf Courses"
					func				= "thailand-golf-courses.asp"
					funcCon 		= function_generate_golf_link(arrdest_id,"",1)
		Case "34" 'day trip
					pro_cate 		= "day-trips"	
					cateLnk		= "day-trips"
					proCateCon 	= "Day Trips"	
					func				= "thailand-day-trips.asp"
					funcCon 		= function_generate_sightseeing_link(arrdest_id,"",1)
		Case "36" 'water activity
					pro_cate		= "water-activity"	
					cateLnk		= "water-activity"
					proCateCon 	= "Water Activities"
					func				= "thailand-water-activity.asp"
					funcCon 		= function_generate_water_activity_link(arrdest_id,"",1)
		Case "38" 'show and event
					pro_cate 		= "show-event"	
					cateLnk		= "show-event"
					proCateCon 	= "Shows and Events"
					func				= "thailand-show-event.asp"
					funcCon 		= function_generate_show_event_link(arrdest_id,"",1)
		Case "39" 'health check up
					pro_cate 		= "health-check-up"	
					cateLnk		= "health"
					proCateCon 	= "Health Check Up"	
					func				= "thailand-health-check-up.asp"
					funcCon 		= function_generate_health_check_up_link(arrdest_id,"",1)
	End Select
	
	Set recList = server.CreateObject("ADODB.recordset")
		recList.Open strSql , Conn , 1 , 3
	if (recList.eof) then
		response.write "document.write('<table width=""100%"" cellpadding=""2"" cellspacing=""1"""&BCon&">');"
		response.write "document.write('<tr>');"
		response.write "document.write('<td "& HBCon&" align=""left"">strSql not found</td>');"
		response.write "document.write('</tr>');"
		response.write "document.write('</tatle>');"
	else
		arrList = recList.GetRows()			
		recList.Close()
		Set recList = nothing
	
	Select Case int(strShow)		'	All Destination
		Case 1	
			Select Case cShow
				Case 1
					response.write "document.write('<table width=""100%"" cellpadding=""2"" cellspacing=""1"" "& BCon&" class=""fontSize"">');"
					response.write "document.write('<tr>');"
					response.write "document.write('<td "& HBCon&"  align=""left"">');"
					response.write "document.write('<strong><a href=""http://www.hotels2thailand.com/"&func&psidCon&""" target=""_blank""><font "& HCCon&">"&proCateCon&" in Thailand</font></a></strong>');"
					response.write "document.write('</td>');"
					response.write "document.write('</tr>');"
					IF isArray(arrList) Then
					response.write "document.write('<tr>');"
					response.write "document.write('<td bgcolor=""#FFFFFF"" align=""left"">');"
						For intArr = 0 to Ubound(arrList,2)
						strDetail = replace(arrList(1,intArr),"'","\'")
						strDetail = replace(strDetail,"""","\""")
						strDetail = replace(strDetail," ","-")
						response.write "document.write('<li><a href=""http://www.hotels2thailand.com/"&strDetail&"-"&cateLnk&".asp"&psidCon&""" target=""_blank""><font "&LCCon&">"&arrList(1,intArr)&" "&pro_cate&"</font></a></li>');"
						next
					response.write "document.write('</td>');"
					response.write "document.write('</tr>');"
					Else
						response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
					End IF
					response.write "document.write('</table>');"
				Case 2	'	one destination on promotion	, one column
					response.write "document.write('<table width=""100%"" cellpadding=""2"" cellspacing=""1"""&BCon&" class=""fontSize"">');"	
					response.write "document.write('<tr>');"				
					response.write "document.write('<td "& HBCon&" align=""left""><strong><font "& HCCon&">"&proCateCon&"</font></strong></td>');"
					response.write "document.write('</tr>');"
					IF isArray(arrList) Then
					For intArr = 0 to Ubound(arrList,2)
						strDetail = replace(arrList(4,intArr),"'","\'")
						strDetail = replace(strDetail,"""","\""")
						strDetail2 = replace(arrList(0,intArr),"'","\'")
						strDetail2= replace(strDetail2,"""","\""")
						response.write "document.write('<tr><td bgcolor=""#FFFFFF"" align=""left"">');"
						response.write "document.write('<a href = ""http://www.hotels2thailand.com/"&funcCon&"/"&strDetail&psidCon&""" target = ""_blank"">');"
						response.write "document.write('<font "&LCCon&">"&strDetail2&"</font>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr>');"
					Next
					Else
						response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
					End IF
				response.write "document.write('</table>');"
			End Select
		Case 2	
			Set recList = server.CreateObject("ADODB.recordset")
			recList.Open strSql2, Conn , 1 , 3
			if (recList.eof) then
				response.write "document.write('"&strSql2&"');"
			else
				arrList2 = recList.GetRows()				
				recList.Close()
				Set recList = nothing
			end if
			arrCols = split(strCol,",")					
			strPic = ""
			strH = ""
			strC = ""
			strL = ""
			strP = ""

			For intCols=0 to Ubound(arrCols)
				Select Case arrCols(intCols)
					Case "pic"
						strPic = strPic&"<td align=""center"" "& HBCon&"><strong><font "& HCCon&">Picture</font></strong></td>"
						s_pic = 1
					Case "name"
						strH = strH&"<td align=""center"" "& HBCon&"><strong><font "& HCCon&">"&procateCon&"</font></strong></td>"
						s_n = 1
					Case "hc"
						strC = strC&"<td align=""center"" "& HBCon&"><strong><font "& HCCon&">Class</font></strong></td>"
						s_hc = 1
					Case "loc"
						strL = strL&"<td align=""center"" "& HBCon&"><strong><font "& HCCon&">Location</font></strong></td>"
						s_loc = 1
					Case "mp"
						strP = strP&"<td align=""center"" "& HBCon&"><strong><font "& HCCon&">Price</font></strong></td>"
						s_mp = 1
				End Select
			Next

			response.write "document.write('<table width=""100%"" cellpadding=""2"" cellspacing=""1"""&BCon&" class=""fontSize"">');"
			response.write "document.write('<tr>');"		
			response.write "document.write('"&strPic&"');"
			response.write "document.write('"&strH&"');"
			response.write "document.write('"&strC&"');"
			response.write "document.write('"&strL&"');"
			response.write "document.write('"&strP&"');"		
			response.write "document.write('</tr>');"
			IF isArray(arrList) Then
			For intArr = 0 to Ubound(arrList,2)
				strDetail = replace(arrList(5,intArr),"'","\'")
				strDetail = replace(strDetail,"""","\""")
				strDetail2 = replace(arrList(1,intArr),"'","\'")
				strDetail2 = replace(strDetail2,"""","\""")
				num = arrList(2,intArr) - (arrList(2,intArr) mod 10)							
				response.write "document.write('<tr>');"
				if (s_pic = 1) then
					response.write "document.write('<td bgcolor=""#FFFFFF"" align=""center""><a href=""http://www.hotels2thailand.com/"&funcCon&"/"&strDetail&psidCon&"""  target=""_blank""><img src =""http://www.hotels2thailand.com/thailand-"&cateLnk&"-pic/"&arrList(0,intArr)&"_b_1.jpg"" border = ""0"" alt = """&strDetail2&"""></a></td>');"
				end if
				if (s_n = 1) then
					response.write "document.write('<td bgcolor=""#FFFFFF"" align=""left""><a href=""http://www.hotels2thailand.com/"&funcCon&"/"&strDetail&psidCon&"""  target=""_blank""><font "&LCCon&">"&strDetail2&"</font></a></td>');"		
				end if
				if (s_hc = 1) then
					response.write "document.write('<td bgcolor=""#FFFFFF"" align=""left"">');"		
					if arrList(2,intArr) - int(arrList(2,intArr)) <> 0 then
						for star = 1 to arrList(2,intArr) 
							response.Write "document.write('<img src =""http://www.booking2hotels.com/images/layout5_star.gif"" border = ""0"">');"		
						next
						response.Write "document.write('<img src =""http://www.booking2hotels.com/images/layout5_star_half.gif"" border = ""0"">');"		
					else
						for star = 1 to arrList(2,intArr)  
							response.Write "document.write('<img src =""http://www.booking2hotels.com/images/layout5_star.gif"" border = ""0"">');"		
						next
					end if
					response.write "document.write('</td>');"
				end if
				if (s_loc = 1) then
					response.write "document.write('<td bgcolor=""#FFFFFF"" align=""left"">"&arrList(6,intArr)&"</td>');"		
				end if
				if (s_mp = 1) and (arrList(3,intArr) = 0) then
					response.write "document.write('<td bgcolor=""#FFFFFF"" align=""right"">"&arrList(3,intArr)&" "&arrList2(2,0)&"</td>');"	
				elseif (s_mp = 1) then
					response.write "document.write('<td bgcolor=""#FFFFFF"" align=""right"">"&FormatNumber((arrList(3,intArr)/arrList2(1,0))*intVatFactor,0)&" "&arrList2(2,0)&" hello</td>');"		
				end if
				response.write "document.write('</tr>');"
			Next
			Else
				response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
			End IF
			response.write "document.write('</table>');"
		End Select
	end if 	'	getRows
End Function

%>