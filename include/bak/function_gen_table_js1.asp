<%
Function 	function_gen_table_js1(arrList,arrCol,bodyColor,bgColor,txtTitle,txtColor,pColor,intType,strCate,intSort,arrList2,arrList3,psid,dest_id,curr,arrRate)
	Dim BCon
	Dim HBCon
	Dim HCCon
	Dim LCCon
	Dim max_col
	Dim intCol
	Dim intDest
	Dim intCount
	Dim PCCon
	Dim intStar
	Dim strRate
	Dim num1
	Dim num2
	Dim num3
	Dim num4
	Dim num5
	Dim num6
	Dim rCon
	Dim cateCon
	Dim cateCons
	Dim psidCon
	Dim cateLnk
	Dim strDetail
	Dim strDetail2
	Dim strDetail3
	Dim arrDest
	Dim funcCon
	Dim funcCon2
	Dim rateCon
	Dim sqlRate
	Dim colSpn 
	Dim picCon
	Dim Rate
	Dim func
	Dim func2
	Dim intList
	
	Dim sqlList5
	Dim strCol
	Dim strValue
	Dim sqlList4
	Dim arrList4
	Dim sqlHotel
	Dim rsHotel
	Dim arrHotel
	Dim strFileName
	Dim option_cat

	IF (strCate <> "29") Then
		colSpn = " colspan = ""2"""				
	End IF
	
	strRate 	= 0					
	BCon 		= ""
	HBCon		= ""
	HCCon 		= ""
	LCCon 		= ""
	'LCCon 		= "style=\'color:blue; font-size:12px;\'"
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
					picCon		=	"thailand-hotels-pic"
					option_cat= 38 
		Case "32" 'golf course
					cateCon 	= "Golf Course"
					cateCons	=	"Golf Courses"
					cateLnk	= "thailand-golf-courses.asp"
					picCon		=	"thailand-golf-pic"
					option_cat= 48
		Case "34" 'day trip
					cateCon 	= "Day Trip"
					cateCons	= "Day Trips"
					cateLnk	=	"thailand-day-trips.asp"
					picCon		=	"thailand-day-trips-pic"
					option_cat= 52
		Case "36" 'water activity
					cateCon 	= "Water Activity"
					cateCons 	=	"Water Activities"
					cateLnk 	=	"thailand-water-activity.asp"
					picCon		=	"thailand-water-activity-pic"
					option_cat= 53
		Case "38" 'show and event
					cateCon 	= "Show And Event"
					cateCons	= "Shows And Events"
					cateLnk 	= "thailand-show-event.asp" 
					picCon		=	"thailand-show-event-pic"
					option_cat= 54
		Case "39" 'health check up
					cateCon 	= "Health Check Up"
					cateCons 	= "Health Check Up"
					cateLnk 	=	"thailand-health-check-up.asp"
					picCon		=	"thailand-health-pic"
					option_cat= 55
	End Select
	
 Select Case intType		
	Case 1 'full
		response.write "document.write('case1.1');"

'	response.write "document.write('alert(getRefToDiv(""title_0""))')"
'	response.write "function function_get_currency(strCur){"&vbcrlf
'	response.write "for(var i=0;i<arrCur.length;i++){"&vbcrlf
'	response.write "if(arrCur[i][2]==strCur){"&vbcrlf
'	response.write "return arrCur[i][1]"&vbcrlf
'	response.write "}"&vbcrlf
'	response.write "}"&vbcrlf
'	response.write "}"&vbcrlf
'
''Create New Array Hotel	
'	response.write "var arrHotel=new Array;"&vbcrlf
'	For =0 to UBound(arrList,2)
'		response.write "arrHotel["&&"]=new Array;"&vbcrlf
'		response.write "arrHotel["&&"][0]="""&arrList(0,)&""";"&vbcrlf
'		response.write "arrHotel["&&"][1]="""&arrList(1,)&""";"&vbcrlf
'		response.write "arrHotel["&&"][2]="""&arrList(2,)&""";"&vbcrlf
'		response.write "arrHotel["&&"][3]="&arrList(3,)&";"&vbcrlf
'		response.write "arrHotel["&&"][4]="""&function_display_abf(arrList(4,))&""";"&vbcrlf
'		response.write "arrHotel["&&"][5]="""&arrList(5,)&""";"&vbcrlf
'		response.write "arrHotel["&&"][6]=""<img src=http://www.booking2hotels.com/images/star_rate"&int(arrList(6,))&".gif>"";"&vbcrlf
'		response.write "arrHotel["&&"][7]="""&arrList(7,)&""";"&vbcrlf
'	Next
''-----------------------	
''Generate Rate Table
'	
'	response.write "document.write('<table width=""100%"" border=""0"" cellpadding=""3"" cellspacing=""1"" bgcolor="""&bodyColor&""" style=""color:"&txtColor&";font-size:12px;font-family:Verdana, Arial, Helvetica, sans-serif"">');"
'	response.write "document.write('<tr style=""color:"&txtTitle&";font-weight:bold"">');"
'	response.write "document.write('<td>Title<br><a href=""#"" onClick=""function_sort_min(0)"">A-Z</a> | <a href=""#"" onClick=""function_sort_max(0)"">Z-A</a></td>');"
'	For intCol=0 to Ubound(arrCol)
'		response.write "document.write('<td>"&function_display_column(arrCol(intCol),1)&" "&function_display_column(arrCol(intCol),3)&" | "&function_display_column(arrCol(intCol),4)&"</td>');"
'	Next
'	'response.write "document.write('<td>Location<br><a href=""#"" onClick=""function_sort_min(2)"">A-Z</a> | <a href=""#"" onClick=""function_sort_max(2)"">Z-A</a></td>');"
'	'response.write "document.write('<td>Class<br><a href=""#"" onClick=""function_sort_min(6)"">L-H</a> | <a href=""#"" onClick=""function_sort_max(6)"">H-L</a></td>');"
'	'response.write "document.write('<td>Rate<br><a href=""#"" onClick=""function_sort_min(3)"">L-H</a> | <a href=""#"" onClick=""function_sort_max(3)"">H-L</a></td>');"
'	response.write "document.write('<td>ABF</td>');"
'	response.write "document.write('</tr>');"
'	
'	response.write "for(var i=0;i<arrHotel.length;i++){"&vbcrlf
'	response.write "document.write('<tr bgcolor="""&bgColor&""">');"&vbcrlf
'	response.write "document.write('<td><div id=""title_'+i+'""><a href=""http://www.hotels2thailand.com/'+arrHotel[i][1]+'-hotels/'+arrHotel[i][5]+'"">'+arrHotel[i][0]+'</a></div></td>');"&vbcrlf
'	For intCol=0 to Ubound(arrCol)
'		response.write "document.write('<td><div id="""&function_display_column(arrCol(intCol),1)&"_'+i+'"">'+"&function_display_column(arrCol(intCol),5)&"+'</div></td>');"
'	Next
'	'response.write "document.write('<td><div id=""location_'+i+'"">'+arrHotel[i][2]+'</div></td>');"&vbcrlf
''	response.write "document.write('<td><div id=""class_'+i+'"">'+arrHotel[i][6]+'</div></td>');"&vbcrlf
''	response.write "document.write('<td align=right><div id=""rate_'+i+'"">'+arrHotel[i][3]+'</div></td>');"&vbcrlf
'	response.write "document.write('<td><div id=""abf_'+i+'"">'+arrHotel[i][4]+'</div></td>');"&vbcrlf
'	response.write "document.write('</tr>');"
'	response.write "}"&vbcrlf
'	response.write "document.write('</table>');"&vbcrlf
'	'---------------------------------------------
'
'	response.write "function getRefToDiv(divID) {"&vbcrlf
'	response.write "if( document.getElementById ) { "&vbcrlf
'	response.write "return document.getElementById(divID); }"&vbcrlf
'	response.write "return false;"&vbcrlf
'	response.write "}"&vbcrlf
'	
'	response.write "function function_sort_min(sortType){"&vbcrlf
'	response.write "var row"&vbcrlf
'	response.write "var j"&vbcrlf
'	response.write "var key_value"&vbcrlf
'	response.write "var other_value"&vbcrlf
'	response.write "var new_key"&vbcrlf
'	response.write "var new_other"&vbcrlf
'	response.write "var swap_pos"&vbcrlf
'	response.write "var other_dimension"&vbcrlf
'	response.write "var varDim0"&vbcrlf
'	response.write "var varDim1"&vbcrlf
'	response.write "var varDim2"&vbcrlf
'	response.write "var varDim3"&vbcrlf
'	response.write "var varDim4"&vbcrlf
'	response.write "var varDim5"&vbcrlf
'	response.write "var varDim6"&vbcrlf
'	response.write "var varDim7"&vbcrlf
'	response.write "var new_varDim0"&vbcrlf
'	response.write "var new_varDim1"&vbcrlf
'	response.write "var new_varDim2"&vbcrlf
'	response.write "var new_varDim3"&vbcrlf
'	response.write "var new_varDim4"&vbcrlf
'	response.write "var new_varDim5"&vbcrlf
'	response.write "var new_varDim6"&vbcrlf
'	response.write "var new_varDim7"&vbcrlf
'	response.write "for (var row=0;row<arrHotel.length-1;row++){"&vbcrlf
' 	response.write "key_value=arrHotel[row][sortType]"&vbcrlf
'	response.write "varDim0=arrHotel[row][0]"&vbcrlf
'	response.write "varDim1=arrHotel[row][1]"&vbcrlf
'	response.write "varDim2=arrHotel[row][2]"&vbcrlf
'	response.write "varDim3=arrHotel[row][3]"&vbcrlf
'	response.write "varDim4=arrHotel[row][4]"&vbcrlf
'	response.write "varDim5=arrHotel[row][5]"&vbcrlf
'	response.write "varDim6=arrHotel[row][6]"&vbcrlf
'	response.write "varDim7=arrHotel[row][7]"&vbcrlf
'	response.write "new_key=arrHotel[row][sortType]"&vbcrlf
'	response.write "new_varDim0=arrHotel[row][0]"&vbcrlf
'	response.write "new_varDim1=arrHotel[row][1]"&vbcrlf
'	response.write "new_varDim2=arrHotel[row][2]"&vbcrlf
'	response.write "new_varDim3=arrHotel[row][3]"&vbcrlf
'	response.write "new_varDim4=arrHotel[row][4]"&vbcrlf
'	response.write "new_varDim5=arrHotel[row][5]"&vbcrlf
'	response.write "new_varDim6=arrHotel[row][6]"&vbcrlf
'	response.write "new_varDim7=arrHotel[row][7]"&vbcrlf
'	response.write "swap_pos=row"&vbcrlf
'	response.write "for (var j=row+1;j<arrHotel.length;j++){"&vbcrlf
'	response.write "if(arrHotel[j][sortType] < new_key){"&vbcrlf
'	response.write "swap_pos=j"&vbcrlf
'	response.write "new_key=arrHotel[j][sortType]"&vbcrlf
'	response.write "new_varDim0=arrHotel[j][0]"&vbcrlf
'	response.write "new_varDim1=arrHotel[j][1]"&vbcrlf
'	response.write "new_varDim2=arrHotel[j][2]"&vbcrlf
'	response.write "new_varDim3=arrHotel[j][3]"&vbcrlf
'	response.write "new_varDim4=arrHotel[j][4]"&vbcrlf
'	response.write "new_varDim5=arrHotel[j][5]"&vbcrlf
'	response.write "new_varDim6=arrHotel[j][6]"&vbcrlf
'	response.write "new_varDim7=arrHotel[j][7]"&vbcrlf
'	response.write "}"&vbcrlf
'	response.write "}"&vbcrlf
'	response.write "if(swap_pos!=row){"&vbcrlf
'	response.write "arrHotel[swap_pos][0]=varDim0"&vbcrlf
'	response.write "arrHotel[swap_pos][1]=varDim1"&vbcrlf
'	response.write "arrHotel[swap_pos][2]=varDim2"&vbcrlf
'	response.write "arrHotel[swap_pos][3]=varDim3"&vbcrlf
'	response.write "arrHotel[swap_pos][4]=varDim4"&vbcrlf
'	response.write "arrHotel[swap_pos][5]=varDim5"&vbcrlf
'	response.write "arrHotel[swap_pos][6]=varDim6"&vbcrlf
'	response.write "arrHotel[swap_pos][7]=varDim7"&vbcrlf
'	response.write "arrHotel[row][0]=new_varDim0"&vbcrlf
'	response.write "arrHotel[row][1]=new_varDim1"&vbcrlf
'	response.write "arrHotel[row][2]=new_varDim2"&vbcrlf
'	response.write "arrHotel[row][3]=new_varDim3"&vbcrlf
'	response.write "arrHotel[row][4]=new_varDim4"&vbcrlf
'	response.write "arrHotel[row][5]=new_varDim5"&vbcrlf
'	response.write "arrHotel[row][6]=new_varDim6"&vbcrlf
'	response.write "arrHotel[row][7]=new_varDim7"&vbcrlf
'	response.write "}"&vbcrlf
' 	response.write "}"&vbcrlf
'	response.write "intCount=0"&vbcrlf
' 	response.write "for(var x=0;x<arrHotel.length;x++){"&vbcrlf
'	response.write "myTitle = getRefToDiv(""title_"" + intCount); if (myTitle) myTitle.innerHTML =arrHotel[x][0]"&vbcrlf
'	response.write "myLocation = getRefToDiv(""location_"" + intCount); if (myLocation) myLocation.innerHTML =arrHotel[x][2]"&vbcrlf
'	response.write "myClass = getRefToDiv(""class_"" + intCount); if (myClass) myClass.innerHTML =arrHotel[x][6]"&vbcrlf
'	response.write "myAbf = getRefToDiv(""abf_"" + intCount); if (myAbf) myAbf.innerHTML =arrHotel[x][4]"&vbcrlf
'	response.write "myRate = getRefToDiv(""rate_"" + intCount); if (myRate) myRate.innerHTML =arrHotel[x][3]"&vbcrlf
'	response.write "intCount+=1	"&vbcrlf
' 	response.write "}"&vbcrlf
'	response.write "}"&vbcrlf
'
'	response.write "function function_sort_max(sortType){"&vbcrlf
'	response.write "var row"&vbcrlf
'	response.write "var j"&vbcrlf
'	response.write "var key_value"&vbcrlf
'	response.write "var other_value"&vbcrlf
'	response.write "var new_key"&vbcrlf
'	response.write "var new_other"&vbcrlf
'	response.write "var swap_pos"&vbcrlf
'	response.write "var other_dimension"&vbcrlf
'	response.write "var varDim0"&vbcrlf
'	response.write "var varDim1"&vbcrlf
'	response.write "var varDim2"&vbcrlf
'	response.write "var varDim3"&vbcrlf
'	response.write "var varDim4"&vbcrlf
'	response.write "var varDim5"&vbcrlf
'	response.write "var varDim6"&vbcrlf
'	response.write "var varDim7"&vbcrlf
'	response.write "var new_varDim0"&vbcrlf
'	response.write "var new_varDim1"&vbcrlf
'	response.write "var new_varDim2"&vbcrlf
'	response.write "var new_varDim3"&vbcrlf
'	response.write "var new_varDim4"&vbcrlf
'	response.write "var new_varDim5"&vbcrlf
'	response.write "var new_varDim6"&vbcrlf
'	response.write "var new_varDim7"&vbcrlf
'	response.write "for (var row=0;row<arrHotel.length-1;row++){"&vbcrlf
' 	response.write "key_value=arrHotel[row][sortType]"&vbcrlf
'	response.write "varDim0=arrHotel[row][0]"&vbcrlf
'	response.write "varDim1=arrHotel[row][1]"&vbcrlf
'	response.write "varDim2=arrHotel[row][2]"&vbcrlf
'	response.write "varDim3=arrHotel[row][3]"&vbcrlf
'	response.write "varDim4=arrHotel[row][4]"&vbcrlf
'	response.write "varDim5=arrHotel[row][5]"&vbcrlf
'	response.write "varDim6=arrHotel[row][6]"&vbcrlf
'	response.write "varDim7=arrHotel[row][7]"&vbcrlf
'	response.write "new_key=arrHotel[row][sortType]"&vbcrlf
'	response.write "new_varDim0=arrHotel[row][0]"&vbcrlf
'	response.write "new_varDim1=arrHotel[row][1]"&vbcrlf
'	response.write "new_varDim2=arrHotel[row][2]"&vbcrlf
'	response.write "new_varDim3=arrHotel[row][3]"&vbcrlf
'	response.write "new_varDim4=arrHotel[row][4]"&vbcrlf
'	response.write "new_varDim5=arrHotel[row][5]"&vbcrlf
'	response.write "new_varDim6=arrHotel[row][6]"&vbcrlf
'	response.write "new_varDim7=arrHotel[row][7]"&vbcrlf
'	response.write "swap_pos=row"&vbcrlf
'	response.write "for (var j=row+1;j<arrHotel.length;j++){"&vbcrlf
'	response.write "if(arrHotel[j][sortType] > new_key){"&vbcrlf
'	response.write "swap_pos=j"&vbcrlf
'	response.write "new_key=arrHotel[j][sortType]"&vbcrlf
'	response.write "new_varDim0=arrHotel[j][0]"&vbcrlf
'	response.write "new_varDim1=arrHotel[j][1]"&vbcrlf
'	response.write "new_varDim2=arrHotel[j][2]"&vbcrlf
'	response.write "new_varDim3=arrHotel[j][3]"&vbcrlf
'	response.write "new_varDim4=arrHotel[j][4]"&vbcrlf
'	response.write "new_varDim5=arrHotel[j][5]"&vbcrlf
'	response.write "new_varDim6=arrHotel[j][6]"&vbcrlf
'	response.write "new_varDim7=arrHotel[j][7]"&vbcrlf
'	response.write "}"&vbcrlf
'	response.write "}"&vbcrlf
'	response.write "if(swap_pos!=row){"&vbcrlf
'	response.write "arrHotel[swap_pos][0]=varDim0"&vbcrlf
'	response.write "arrHotel[swap_pos][1]=varDim1"&vbcrlf
'	response.write "arrHotel[swap_pos][2]=varDim2"&vbcrlf
'	response.write "arrHotel[swap_pos][3]=varDim3"&vbcrlf
'	response.write "arrHotel[swap_pos][4]=varDim4"&vbcrlf
'	response.write "arrHotel[swap_pos][5]=varDim5"&vbcrlf
'	response.write "arrHotel[swap_pos][6]=varDim6"&vbcrlf
'	response.write "arrHotel[swap_pos][7]=varDim7"&vbcrlf
'	response.write "arrHotel[row][0]=new_varDim0"&vbcrlf
'	response.write "arrHotel[row][1]=new_varDim1"&vbcrlf
'	response.write "arrHotel[row][2]=new_varDim2"&vbcrlf
'	response.write "arrHotel[row][3]=new_varDim3"&vbcrlf
'	response.write "arrHotel[row][4]=new_varDim4"&vbcrlf
'	response.write "arrHotel[row][5]=new_varDim5"&vbcrlf
'	response.write "arrHotel[row][6]=new_varDim6"&vbcrlf
'	response.write "arrHotel[row][7]=new_varDim7"&vbcrlf
'	response.write "}"&vbcrlf
' 	response.write "}"&vbcrlf
'	response.write "intCount=0"&vbcrlf
' 	response.write "for(var x=0;x<arrHotel.length;x++){"&vbcrlf
'	response.write "myTitle = getRefToDiv(""title_"" + intCount); if (myTitle) myTitle.innerHTML =arrHotel[x][0]"&vbcrlf
'	response.write "myLocation = getRefToDiv(""location_"" + intCount); if (myLocation) myLocation.innerHTML =arrHotel[x][2]"&vbcrlf
'	response.write "myClass = getRefToDiv(""class_"" + intCount); if (myClass) myClass.innerHTML =arrHotel[x][6]"&vbcrlf
'	response.write "myAbf = getRefToDiv(""abf_"" + intCount); if (myAbf) myAbf.innerHTML =arrHotel[x][4]"&vbcrlf
'	response.write "myRate = getRefToDiv(""rate_"" + intCount); if (myRate) myRate.innerHTML =arrHotel[x][3]"&vbcrlf
'	response.write "intCount+=1	"&vbcrlf
' 	response.write "}"&vbcrlf
'	response.write "}"&vbcrlf
				Case 2 'layout 5
					response.write "document.write('<table width = ""200""  "& BCon&" cellpadding = ""3"" cellspacing = ""0"" class=""fontSize"">');"										
					response.write "document.write('<tr height = ""25"" valign = ""middle"">');"
					response.write "document.write('<td "& HBCon&"><font "& HCCon&"><strong>Popular Destinations</strong></font></td>');"
					response.write "document.write('</tr>');"
					IF isArray(arrList) Then
					For intList=0 To Ubound(arrList,2)
						strDetail = replace(arrList(1,intList),"'","\'")
						strDetail = replace(strDetail,"""","\""")
						strDetail = replace(strDetail,vbCrLF,"") 
						select case strCate
							case "29"
								func = function_generate_hotel_link(arrList(0,intList),"",1)
							case "32"
								func = function_generate_golf_link(arrList(0,intList),"",1)
							case "34"
								func = function_generate_sightseeing_link(arrList(0,intList),"",1)
							case "36"
								func = function_generate_water_activity_link(arrList(0,intList),"",1)
							case  "38"
								func = function_generate_show_event_link(arrList(0,intList),"",1)
							case "39"
								func = function_generate_health_check_up_link(arrList(0,intList),"",1)
						end select
						response.write "document.write('<tr><td align = ""left"">&nbsp;<img src = ""http://www.booking2hotels.com/images/blue_bullet.gif"" border = ""0"" />');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"
						response.write "document.write('<strong><font "& LCCon&">"&strDetail&" "&cateCon&"</font></strong>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr>');"
					Next
					Else
						response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
					End IF
					response.write "document.write('</table>');"	
				Case 3,4,5 'Layout 1 2 3	
					Select Case intType
						Case 3
							max_col=2
						Case 4
							max_col=3
						Case 5
							max_col=4
					End Select
					intCol = 0
					'	Head
					response.write "document.write('<table width = ""460"" cellpadding = ""3"" cellspacing = ""0"" style = "" border =""0"" class=""fontSize"">');"
					response.write "document.write('<tr><td align = ""left""><strong><font "& HCCon&">Top Destinations for "&cateCon&"</font></strong></td></tr>');"
					response.write "document.write('</table><br>');"
					
					response.write "document.write('<table width = ""460"" cellpadding = ""3"" cellspacing = ""0"" "& BCon&">');"
					IF isArray(arrList) Then
					response.write "document.write('<tr>');"
					response.write "document.write('<td align = ""center""><table cellpadding = ""0"" cellspacing = ""0""><tr>');"
					For intList=0 To Ubound(arrList,2)	
						strDetail = replace(arrList(1,intList),"'","\'")
						strDetail = replace(strDetail,"""","\""")
						strDetail = replace(strDetail,vbCrLF,"") 
						select case strCate
							case "29"
								func = function_generate_hotel_link(arrList(0,intList),"",1)
							case "32"
								func = function_generate_golf_link(arrList(0,intList),"",1)
							case "34"
								func = function_generate_sightseeing_link(arrList(0,intList),"",1)
							case "36"
								func = function_generate_water_activity_link(arrList(0,intList),"",1)
							case  "38"
								func = function_generate_show_event_link(arrList(0,intList),"",1)
							case "39"
								func = function_generate_health_check_up_link(arrList(0,intList),"",1)
						end select
						IF intCol mod max_col=0 Then
							response.write "document.write('</tr><tr>');"
						End IF
								response.write "document.write('<td align = ""left"" width = ""220""><table cellpadding = ""3"" cellspacing = ""0"" border = ""0""><tr><td align = ""left"">');"	
								response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"	
								response.write "document.write('<img src=""http://www.booking2hotels.com/images/dest_"&strDetail&".jpg""  style = ""border : solid 1px #CCCCCC"" width = ""70"" height = ""53"" alt = """&strDetail&""">');"	
								response.write "document.write('</a>');"	
								response.write "document.write('</td><td align = ""left"" valign = ""bottom"">');"	
								response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank""  title = """&strDetail&""">');"
								response.write "document.write('<strong><font "& LCCon&"><u>"&strDetail&"</u></font></strong>');"
								response.write "document.write('</a>');"
								response.write "document.write('</td>');"	
								response.write "document.write('</tr></table></td>');"
						intCol = intCol + 1
					Next
					response.write "document.write('</tr>');"
					Else
						response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
					End IF
					response.write "document.write('</table>');"	
				Case 6 'Layout 4
					response.write "document.write('<table width = ""600"" cellpadding = ""3"" cellspacing = ""0"" border = ""0"" class=""fontSize"">');"	
					response.write "document.write('<tr><td>');"
					response.write "document.write('<strong><font "& HCCon&">Top Destination link for "&cateCon&"</font></strong>');"
					response.write "document.write('</td></tr>');"
					IF isArray(arrList) Then
					For intList=0 To Ubound(arrList,2)
						select case strCate
							case "29"
								func = function_generate_hotel_link(arrList(0,intList),"",1)
							case "32"
								func = function_generate_golf_link(arrList(0,intList),"",1)
							case "34"
								func = function_generate_sightseeing_link(arrList(0,intList),"",1)
							case "36"
								func = function_generate_water_activity_link(arrList(0,intList),"",1)
							case  "38"
								func = function_generate_show_event_link(arrList(0,intList),"",1)
							case "39"
								func = function_generate_health_check_up_link(arrList(0,intList),"",1)
						end select
						strDetail = replace(arrList(2,intList),"'","\'")
						strDetail = replace(strDetail,"""","\""")
						strDetail2 = replace(arrList(1,intList),"'","\'")
						strDetail2 = replace(strDetail2,"""","\""")
						response.write "document.write('<tr><td><br />');"
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""90%"">');"
						response.write "document.write('<tr><td valign = ""top"" width = ""18%"">');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"
						response.write "document.write('<img src = ""http://www.booking2hotels.com/images/dest_"&strDetail2&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""80"" height = ""61"" alt = """&strDetail2&""" />');"
						response.write "document.write('</a>');"
						response.write "document.write('</td>');"
						response.write "document.write('<td align = ""left"" valign = ""top"" style ="" line-height : 20px"">');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail2&""">');"
						response.write "document.write('<strong><font "& LCCon&">"&strDetail2&"</font></strong>');"
						response.write "document.write('</a>');"
						response.write "document.write('<br />');"
						response.write "document.write('"&MID(strDetail,1,200)&"');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank""><font "& LCCon&">');"
						response.write "document.write('...More</font>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr>');"
						response.write "document.write('<tr><td colspan = ""2"" align = ""right""><br />');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"
						response.write "document.write('<font "& LCCon&">View all "&cateCons&" in "&strDetail2&" >>&nbsp;&nbsp;</font>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr></table></td></tr>');"
					Next
					Else
						response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
					End IF
					response.write "document.write('</table>');"
				Case 7,8,9,10 'Layout 6 7 8 9
					Dim width
					Dim colSp
					Dim colors
					Dim colors2
					Dim temp
					colors 	= "#3899A0"
					colors2 = "#B2B219"
					Select Case intType
						Case 7
							max_col=2
							width = " width =""310"""
							colSp = " colspan =""2"""
						Case 8
							max_col=3
							width = " width =""460"""
							colSp = " colspan =""3"""
						Case 9
							max_col=4
							width = " width =""620"""
							colSp = " colspan =""4"""
						Case 10
							max_col=5
							width = " width =""780"""
							colSp = " colspan =""5"""
					End Select
					intCol=0
					response.write "document.write('<table "&width&" cellpadding = ""0"" cellspacing = ""0"" border = ""0"" class=""fontSize"">');"
					response.write "document.write('<tr><td "&colSp&" align = ""center""><strong><font "& HCCon&">Top Destinations of "&cateCon&"');"
					response.write "document.write('</font></strong><br /><br /></td></tr>');"
					IF isArray(arrList) Then
						response.write "document.write('<tr>');"
					For intList=0 To Ubound(arrList,2)
						strDetail = replace(arrList(1,intList),"'","\'")
						strDetail = replace(strDetail,"""","\""")		
						strDetail = replace(strDetail,vbCrLF,"") 
						select case strCate
							case "29"
								func 		= function_generate_hotel_link(arrList3(0,intList),"",1)
								func2 	= function_generate_hotel_link(arrList3(0,intList),"",4)
							case "32"
								func 		= function_generate_golf_link(arrList3(0,intList),"",1)
								func2 	= function_generate_golf_link(arrList3(0,intList),"",4)
							case "34"
								func 		= function_generate_sightseeing_link(arrList3(0,intList),"",1)
								func2 	= function_generate_sightseeing_link(arrList3(0,intList),"",4)
							case "36"
								func 		= function_generate_water_activity_link(arrList3(0,intList),"",1)
								func2 	= function_generate_water_activity_link(arrList3(0,intList),"",4)
							case  "38"
								func 		= function_generate_show_event_link(arrList3(0,intList),"",1)
								func2 	= function_generate_show_event_link(arrList3(0,intList),"",4)
							case "39"
								func 		= function_generate_health_check_up_link(arrList3(0,intList),"",1)
								func2 	= function_generate_health_check_up_link(arrList3(0,intList),"",4)
						end select
						IF intCol mod max_col=0 Then
							response.write "document.write('</tr><tr>');"
						End IF
						response.write "document.write('<td><table cellpadding = ""3"" cellspacing = ""0"" "& BCon&">');"
						response.write "document.write('<tr><td align = ""left"" valign = ""top"" background = ""http://www.booking2hotels.com/images/dest_"&strDetail&".jpg"" width = ""133"" height = ""101"">&nbsp;');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"
						response.write "document.write('<strong><font "& LCCon&">"&strDetail&"</font></strong>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr><br>');"
						response.write "document.write('<tr><td height = ""25"" bgcolor = """&colors&""" align = ""right"">');"
						select case intType
							case 8,10
								temp = colors
								colors = colors2
								colors2 = temp
							case 7
								if (intCol mod max_col = 0) then
									temp = colors
									colors = colors2
									colors2 = temp
								end if
							case 9
								if (intCol mod max_col <> 3) then
									temp = colors
									colors = colors2
									colors2 = temp
								end if
						end select							
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"
						response.write "document.write('<strong><font color = ""#FFFFFF"">More Info >></font></strong>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr></table></td>');"
						intCol=intCol+1
					Next
					response.write "document.write('<tr><td colspan = """&max_col&""" style=""height: 19px"">&nbsp;</td></tr>');"
					response.write "document.write('<tr><td colspan = """&max_col&""" align = ""right""><br />');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&cateLnk&psidCon&""" target=""_blank"" >');"
					response.write "document.write('<strong><font color = ""#5F9EA0""><u>View All Destinaions in Thailand >></u></font></strong>');"
					response.write "document.write('</a>');"
					response.write "document.write('&nbsp;&nbsp;');"
					response.write "document.write('</td></tr></tr>');"
					Else
						response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
					End IF
					response.write "document.write('</table>');"
			Case 11 'Layout 10
				response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""460"" class=""fontSize"">');"
				response.write "document.write('<tr><td align = ""left""><strong><font "& HCCon&">Top "&cateCon&" Destinations</font></strong></td></tr>');"
				response.write "document.write('</table><br>');"
				response.write "document.write('<table width = ""300"" cellpadding = ""5"" cellspacing = ""0"" "& BCon&">');"
				IF isArray(arrList) Then
				response.write "document.write('<tr><td align = ""left"">');"
				For intList=0 To Ubound(arrList,2)	
						strDetail = replace(arrList(1,intList),"'","\'")
						strDetail = replace(strDetail,"""","\""")
						select case strCate
							case "29"
								func = function_generate_hotel_link(arrList(0,intList),"",1)
							case "32"
								func = function_generate_golf_link(arrList(0,intList),"",1)
							case "34"
								func = function_generate_sightseeing_link(arrList(0,intList),"",1)
							case "36"
								func = function_generate_water_activity_link(arrList(0,intList),"",1)
							case  "38"
								func = function_generate_show_event_link(arrList(0,intList),"",1)
							case "39"
								func = function_generate_health_check_up_link(arrList(0,intList),"",1)
						end select
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"
					response.write "document.write('<strong><font "& LCCon&">"&strDetail&"</font></strong>');"
					response.write "document.write('</a>');"
					IF intList<>Ubound(arrList,2) Then
						response.write "document.write(' | ');"
					End IF
				Next
				response.write "document.write('</td></tr>');"
				response.write "document.write('<tr><td align = ""right"">');"
				response.write "document.write('<img src = ""http://www.booking2hotels.com/images/arrw_right.gif"" border = ""0"" />');"
				response.write "document.write('<a href=""http://www.hotels2thailand.com/"&cateLnk&psidCon&""" target=""_blank"" >');"
				response.write "document.write('View All Destinations');"
				response.write "document.write('</a>');"');"
				response.write "document.write('&nbsp;&nbsp;');"
				response.write "document.write('</td></tr>');"
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			Case 12,13 'Layout 11 12
				Dim wid 
				Select Case intType
					Case 12
						max_col=2
						wid=400
					Case 13
						max_col=3
						wid=605
				End Select
				intCol=0
				response.write "document.write('<table width = """&wid&""" cellpadding = ""0"" cellspacing = ""0"" class=""fontSize""><tr><td>');"
				response.write "document.write('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong><font "& HCCon&">Top Destinations</font></strong>');"
				response.write "document.write('</td></tr></table><br />');"
				response.write "document.write('<table width = """&wid&""" cellpadding = ""3"" cellspacing = ""0"" "& BCon&">');"
				IF isArray(arrList) Then
				response.write "document.write('<tr>');"
				For intList=0 To Ubound(arrList,2)
						strDetail = replace(arrList(1,intList),"'","\'")
						strDetail = replace(strDetail,"""","\""")
						select case strCate
							case "29"
								func = function_generate_hotel_link(arrList(0,intList),"",1)
							case "32"
								func = function_generate_golf_link(arrList(0,intList),"",1)
							case "34"
								func = function_generate_sightseeing_link(arrList(0,intList),"",1)
							case "36"
								func = function_generate_water_activity_link(arrList(0,intList),"",1)
							case  "38"
								func = function_generate_show_event_link(arrList(0,intList),"",1)
							case "39"
								func = function_generate_health_check_up_link(arrList(0,intList),"",1)
						end select
					IF intCol mod max_col=0 Then
						response.write "document.write('</tr><tr>');"
					End IF			
					response.write "document.write('<td align = ""center"" width = ""175""><br><table cellpadding = ""0"" cellspacing = ""0""><tr><td align = ""center"" width = ""175"">');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"
					response.write "document.write('<img src=""http://www.booking2hotels.com/images/dest_"&strDetail&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""145"" height = ""110"" alt = """&strDetail&""" />');"
					response.write "document.write('</a>');"');"
					response.write "document.write('<br>');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"
					response.write "document.write('<strong><font "& LCCon&">"&strDetail&"</font></strong>');"
					response.write "document.write('</a>');"');"
					response.write "document.write('</td></tr></table></td>');"
					intCol=intCol+1
				Next
				response.write "document.write('</tr>');"
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			Case 14 'Layout 13		
				response.write "document.write('<table width = ""400"" cellpadding = ""3"" cellspacing = ""0"" "& BCon&" class=""fontSize"">');"
				response.write "document.write('<tr><td align = ""center"" valign = ""top"" width = ""10"" "& HBCon&">');"
				response.write "document.write('<img src=""http://www.booking2hotels.com/images/blue_bullet.gif"" border = ""0"" />');"
				response.write "document.write('</td>');"
				response.write "document.write('<td align = ""left"" valign = ""top"" "& HBCon&"><strong><font "& HCCon&">Pupular Destinations</font></strong></td></tr>');"
				IF isArray(arrList) Then
				For intList=0 To Ubound(arrList,2)
						select case strCate
							case "29"
								func = function_generate_hotel_link(arrList(0,intList),"",1)
							case "32"
								func = function_generate_golf_link(arrList(0,intList),"",1)
							case "34"
								func = function_generate_sightseeing_link(arrList(0,intList),"",1)
							case "36"
								func = function_generate_water_activity_link(arrList(0,intList),"",1)
							case  "38"
								func = function_generate_show_event_link(arrList(0,intList),"",1)
							case "39"
								func = function_generate_health_check_up_link(arrList(0,intList),"",1)
						end select
					strDetail = replace(arrList(2,intList),"'","\'")
					strDetail = replace(strDetail,"""","\""")
					response.write "document.write('<tr><td align = ""center"" valign = ""top"">');"
					response.write "document.write('<img src=""http://www.booking2hotels.com/images/blue_bullet02.gif"" border = ""0"" /></td>');"
					response.write "document.write('<td align = ""left"" valign = ""top"" style = ""line-height : 20px"">');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&arrList(1,intList)&""">');"
					response.write "document.write('<strong><font "& LCCon&">"&arrList(1,intList)&"</font></strong>');"
					response.write "document.write('</a>');"
					response.write "document.write('<br />');"
					response.write "document.write('"&Mid(strDetail,1,200)&"');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"
					response.write "document.write('<font "& LCCon&"><u>...More');"
					response.write "document.write('</u></font>');"
					response.write "document.write('</a>');"
					response.write "document.write('</td></tr>');"
				Next
				response.write "document.write('<tr><td colspan = ""2"" align = ""right""><br />');"
				response.write "document.write('<a href=""http://www.hotels2thailand.com/"&cateLnk&psidCon&""" target=""_blank"" >');"
				response.write "document.write('<strong><font "& LCCon&"><u>View All Destinaions in Thailand >></u></font></strong>');"
				response.write "document.write('</a>');"
				response.write "document.write('&nbsp;&nbsp;');"
				response.write "document.write('<br /><br />');"
				response.write "document.write('</td></tr>');"
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center"" colspan='2'><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			Case 15,16 'Layout 14 15
				Select Case intType
					Case 15
						max_col=2
					Case 16
						max_col=3
				End Select
				intCol=0
				response.write "document.write('<table width = ""400"" cellpadding = ""0"" cellspacing = ""0"" class=""fontSize""><tr><td>');"
				response.write "document.write('<strong><font "& HCCon&">Popular Destinations</font></strong>');"
				response.write "document.write('</td></tr></table><br />');"
				response.write "document.write('<table width = ""400"" cellpadding = ""3"" cellspacing = ""0""  "& BCon&">');"
				IF isArray(arrList) Then
					response.write "document.write('<tr>');"
				For intList=0 To Ubound(arrList,2)
						select case strCate
							case "29"
								func = function_generate_hotel_link(arrList(0,intList),"",1)
							case "32"
								func = function_generate_golf_link(arrList(0,intList),"",1)
							case "34"
								func = function_generate_sightseeing_link(arrList(0,intList),"",1)
							case "36"
								func = function_generate_water_activity_link(arrList(0,intList),"",1)
							case  "38"
								func = function_generate_show_event_link(arrList(0,intList),"",1)
							case "39"
								func = function_generate_health_check_up_link(arrList(0,intList),"",1)
						end select
					strDetail = replace(arrList(2,intList),"'","\'")
					strDetail = replace(strDetail,"""","\""")
					IF intCol mod max_col=0 Then
						response.write "document.write('</tr><tr>');"
					End IF
					response.write "document.write('<td width = ""200"" align = ""left"" valign = ""top""><table width = ""100%"">');"
					response.write "document.write('<tr><td align = ""center""  style =""border-bottom : solid 3px #A7C9EC"">');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&arrList(1,intList)&""">');"
					response.write "document.write('<strong><font "& LCCon&">"&arrList(1,intList)&"</font></strong>');"
					response.write "document.write('</a>');"
					response.write "document.write('</td></tr><tr><td>');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&arrList(1,intList)&""">');"
					response.write "document.write('<img src = ""http://www.booking2hotels.com/images/dest_"&arrList(1,intList)&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""165"" height = ""125"" alt = """&arrList(1,intList)&""" />');"
					response.write "document.write('</a>');"
					response.write "document.write('<br>');"
					response.write "document.write('</td></tr><tr><td style = ""line-height : 20px"">"&MID(strDetail,1,200)&"');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"
					response.write "document.write('<font "& LCCon&"><u> ,More...</u></font>');"
					response.write "document.write('</a>');"
					response.write "document.write('</td></tr></table></td>');"
					intCol=intCol+1
				Next
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			Case 17 'Layout 16	
				Dim destCon
				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"" class=""fontSize"">');"
				response.write "document.write('<tr><td align = ""left"">');"
				response.write "document.write('<strong><font "& HCCon&">Pupular Destinations</font></strong>');"
				response.write "document.write('</td></tr></table><br />');"
				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0""  "& BCon&">');"
				response.write "document.write('<tr><td colspan = ""3"">&nbsp;</td></tr>');"
				IF isArray(arrList) Then
				response.write "document.write('<tr>');"
				For intCount=0 to Ubound(arrList3,2)
					strDetail = replace(arrList3(1,intCount),"'","\'")
					strDetail = replace(strDetail,"""","\""")
					IF intcount mod 3=0 Then
						response.write "document.write('</tr><tr>');"
					End IF
					For intList=0 to Ubound(arrList,2)
						IF arrList(0,intList)=arrList3(0,intCount) Then
							select case strCate
								case "29"
									func = function_generate_hotel_link(arrList(0,intList),"",1)
								case "32"
									func = function_generate_golf_link(arrList(0,intList),"",1)
								case "34"
									func = function_generate_sightseeing_link(arrList(0,intList),"",1)
								case "36"
									func = function_generate_water_activity_link(arrList(0,intList),"",1)
								case  "38"
									func = function_generate_show_event_link(arrList(0,intList),"",1)
								case "39"
									func = function_generate_health_check_up_link(arrList(0,intList),"",1)
							end select
							Exit for
						End IF
					Next
						response.write "document.write('<td width = ""300"" align = ""center"" valign = ""top"">');"
						response.write "document.write('<table width = ""90%"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
						response.write "document.write('<tr><td align = ""left"">');"		
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"				
						response.write "document.write('<strong><font "& LCCon&">"&strDetail&" "&cateCons&"</font></strong>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr>');"
						sqlHotel="select top 5 d.destination_id,l.location_id,l.title_en,Rtrim(l.files_name),d.title_en "
						sqlHotel=sqlHotel&" from tbl_location l,tbl_destination d"
						sqlHotel=sqlHotel&" where d.destination_id=l.destination_id and l.status=1 and d.destination_id ="&arrList3(0,intCount)
						sqlHotel=sqlHotel&"  and l.files_name is not null order by l.title_en asc"
						Set rsHotel=server.CreateObject("adodb.recordset")
						rsHotel.Open sqlHotel,conn,1,3
						IF Not rsHotel.EOF Then
							arrHotel=rsHotel.getRows()
						End IF
						rsHotel.close
						Set rsHotel=Nothing

						For intList =0 to Ubound(arrHotel,2)
								strDetail2 = replace(arrHotel(2,intList),"'","\'")
								strDetail2 = replace(strDetail2,"""","\""")
								strDetail3 = replace(arrHotel(3,intList),"'","\'")
								strDetail3 = replace(strDetail3,"""","\""")
								response.write "document.write('<tr><td align = ""left"">');"
								response.write "document.write('<img src =""http://www.booking2hotels.com/images/bullet_orange.gif"" border = ""0""/>');"
								response.write "document.write('<a href=""http://www.hotels2thailand.com/"&strDetail3&psidCon&""" target=""_blank"" title = """&strDetail2&""">');"
								response.write "document.write('<font "&LCCon&">"&strDetail2&"</font>');"
								response.write "document.write('</a>');"
								response.write "document.write('</td></tr>');"
						Next 
						response.write "document.write('</table><br></td>');"
				Next
				response.write "document.write('<td width = ""300"">&nbsp;</td>');"
				response.write "document.write('<tr><td colspan = ""3"" align = ""right"">');"
				response.write "document.write('<br />');"
				response.write "document.write('<a href = ""http://www.hotels2thailand.com/"&cateLnk&""" title = """&cateCons&" in Thailand"">');"
				response.write "document.write('<u>View Hotels All Destinations</u> >>');"
				response.write "document.write('</a>');"
				response.write "document.write('&nbsp;&nbsp;');"
				response.write "document.write('</td></tr>');"
				response.write "document.write('<tr><td colspan = ""3"">&nbsp;');"
				response.write "document.write('</td></tr>');"
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			Case 18 'Layout 17	
				response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" class=""fontSize"">');"
				response.write "document.write('<tr>');"
				response.write "document.write('<td align = ""left"">');"
				response.write "document.write('<strong><font "& HCCon&">Hot Destinations</font></strong>');"
				response.write "document.write('</td>');"
				response.write "document.write('</tr>');"
				response.write "document.write('</table><br />');"
				response.write "document.write('<table width = ""600"" cellpadding = ""3"" cellspacing = ""0"" "& LCCon&">');"
				response.write "document.write('<tr><td colspan = ""2"">&nbsp;</td></tr>');"
				IF isArray(arrList) Then
				response.write "document.write('<tr>');"
				For intCount=0 to Ubound(arrList3,2)
					strDetail = replace(arrList3(1,intCount),"'","\'")
					strDetail = replace(strDetail,"""","\""")
					IF intCount mod 2=0 Then
						response.write "document.write('</tr><tr>');"
					End IF
					For intList=0 to Ubound(arrList,2)
						IF arrList(0,intList)=arrList3(0,intCount) Then
							select case strCate
								case "29"
									func = function_generate_hotel_link(arrList(0,intList),"",1)
								case "32"
									func = function_generate_golf_link(arrList(0,intList),"",1)
								case "34"
									func = function_generate_sightseeing_link(arrList(0,intList),"",1)
								case "36"
									func = function_generate_water_activity_link(arrList(0,intList),"",1)
								case  "38"
									func = function_generate_show_event_link(arrList(0,intList),"",1)
								case "39"
									func = function_generate_health_check_up_link(arrList(0,intList),"",1)
							end select
							Exit for
						End IF
					Next
						response.write "document.write('<td width = ""50%"" align = ""center"" valign = ""top"">');"
						response.write "document.write('<table width = ""95%"" cellpadding = ""0"" cellspacing = ""0"" style ="" border-top : dotted 1px #cccccc"">');"
						response.write "document.write('<tr><td height = ""25"" align = ""left"" valign = ""middle"">');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"				
						response.write "document.write('<strong><font color = ""#333333"">"&strDetail&"</font></strong>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr>');"
						sqlHotel="select top 5 d.destination_id,l.location_id,l.title_en,Rtrim(l.files_name),d.title_en "
						sqlHotel=sqlHotel&" from tbl_location l,tbl_destination d"
						sqlHotel=sqlHotel&" where d.destination_id=l.destination_id and l.status=1 and d.destination_id ="&arrList3(0,intCount)
						sqlHotel=sqlHotel&" and l.files_name is not null order by l.title_en asc"
						Set rsHotel=server.CreateObject("adodb.recordset")
						rsHotel.Open sqlHotel,conn,1,3
						IF Not rsHotel.EOF Then
							arrHotel=rsHotel.getRows()
						End IF
						rsHotel.close
						Set rsHotel=Nothing

						For intList=0 to Ubound(arrHotel,2)
								strDetail2= replace(arrList3(1,intList),"'","\'")
								strDetail2 = replace(strDetail,"""","\""")
								response.write "document.write('<tr><td align = ""left"" valign = ""top"">');"
								response.write "document.write('<img src = ""http://www.booking2hotels.com/images/blue_bullet.gif"" border = ""0"" /> ');"
								response.write "document.write('<a href=""http://www.hotels2thailand.com/"&arrHotel(3,intList)&""&psidCon&""" target=""_blank"" title = """&arrHotel(2,intList)&""">');"
								response.write "document.write('<font "& LCCon&">"&arrHotel(2,intList)&"</font>');"
								response.write "document.write('</a>');"
								response.write "document.write('</td></tr>');"
						Next
					response.write "document.write('<tr>');"
					response.write "document.write('<td height = ""25"" valign = ""middle"" align = ""right"">');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"
					response.write "document.write('<strong><font color = ""#7F7F7F"">More Hotels in "&strDetail&"...</font></strong>');"
					response.write "document.write('</a>');"
					response.write "document.write('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>');"
					response.write "document.write('</tr>');"
					response.write "document.write('</table><br></td>');"		
				Next
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			Case 19 'Layout 19	
				Dim intSlash
				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"" class=""fontSize"">');"
				response.write "document.write('<tr><td><strong><font "& HCCon&">Popular Destination</font></strong></td></tr>');"
				response.write "document.write('</table><br />');"
				response.write "document.write('<table width = ""600"" cellpadding = ""3"" cellspacing = ""0""  style = "" border : solid 1px #"&bodyColor&""">');"
				IF isArray(arrList) Then
				For intCount=0 to Ubound(arrList3,2)
					strDetail = replace(arrList3(1,intCount),"'","\'")
					strDetail = replace(strDetail,"""","\""")
					For intList=0 to Ubound(arrList,2)
						IF arrList(0,intList)=arrList3(0,intCount) Then
							select case strCate
								case "29"
									func = function_generate_hotel_link(arrList(0,intList),"",1)
								case "32"
									func = function_generate_golf_link(arrList(0,intList),"",1)
								case "34"
									func = function_generate_sightseeing_link(arrList(0,intList),"",1)
								case "36"
									func = function_generate_water_activity_link(arrList(0,intList),"",1)
								case  "38"
									func = function_generate_show_event_link(arrList(0,intList),"",1)
								case "39"
									func = function_generate_health_check_up_link(arrList(0,intList),"",1)
							end select
							Exit for
						End IF
					Next
					response.write "document.write('<tr><td style = ""border-bottom : dashed 2px #CCCCCC"" width = ""120"" height = ""100"" align = ""center"" valign = ""middle"">');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"
					response.write "document.write('<img src = ""http://www.booking2hotels.com/images/dest_"&strDetail&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""112"" height = ""85"" alt = """&strDetail&""" />');"
					response.write "document.write('</a>');"
					response.write "document.write('</td>');"
					response.write "document.write('<td style = ""border-bottom : dashed 1px #CCCCCC"" align = ""left"" valign = ""middle"">');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"
					response.write "document.write('<strong><font "&LCCon&">"&strDetail&"</font></strong>');"
					response.write "document.write('</a>');"
					response.write "document.write('<br />');"
					sqlHotel="select top 10 d.destination_id,l.location_id,l.title_en,Rtrim(l.files_name),d.title_en "
					sqlHotel=sqlHotel&" from tbl_location l,tbl_destination d"
					sqlHotel=sqlHotel&" where d.destination_id=l.destination_id and l.status=1 and d.destination_id ="&arrList3(0,intCount)
					sqlHotel=sqlHotel&" and l.files_name is not null order by l.title_en asc"
					Set rsHotel=server.CreateObject("adodb.recordset")
					rsHotel.Open sqlHotel,conn,1,3
					IF Not rsHotel.EOF Then
						arrHotel=rsHotel.getRows()
					End IF
					rsHotel.close
					Set rsHotel=Nothing
					intSlash=0
					For intList=0 to Ubound(arrHotel,2)
							strFileName = replace(arrHotel(3,intList),"'","\'")
							strFileName = replace(strFileName ,"""","\""")
							strDetail2 = replace(arrHotel(2,intList),"'","\'")
							strDetail2 = replace(strDetail2,"""","\""")
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&strFileName&psidCon&""" target=""_blank"" title = """&strDetail2&""">');"
							response.write "document.write('<font "& LCCon&">"&strDetail2&"</font>');"
							response.write "document.write('</a>');"
							IF intSlash<> Ubound(arrHotel,2) Then
								response.write "document.write(' | ');"
							End IF					
							intSlash = intSlash + 1
					Next
					response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""100%""><tr>');"
					response.write "document.write('<td align = ""right""><br />');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&strDetail&""">');"
					response.write "document.write('<strong><font color = ""#7F7F7F"">View All Hotels in "&strDetail&">></font></strong>');"
					response.write "document.write('</a>');"
					response.write "document.write('&nbsp;&nbsp;');"
					response.write "document.write('</td></tr></table></td></tr>');"
				Next
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			Case 20 'Layout 20
				intCount=1
				response.write "document.write('<table width = ""350"" cellpadding = ""0"" cellspacing = ""0""  "& BCon&" class=""fontSize"">');"
				response.write "document.write('<tr>');"
				response.write "document.write('<td align = ""center""><br />');"
				
				response.write "document.write('<table cellpadding = ""3"" cellspacing = ""0"" border = ""0"" width = ""310"">');"
				response.write "document.write('<tr>');"
				response.write "document.write('<td align = ""left"" colspan = ""2"">');"
				response.write "document.write('<strong><font "& HCCon&">Top Destinations</font></strong>');"
				response.write "document.write('<br /><br /></td>');"
				response.write "document.write('</tr>');"
				IF isArray(arrList) Then
				For intList=0 to Ubound(arrList,2)
						select case strCate
							case "29"
								func 		= function_generate_hotel_link(arrList(0,intList),"",1)
								func2 	= function_generate_hotel_link(arrList(0,intList),"",4)
							case "32"
								func 		= function_generate_golf_link(arrList(0,intList),"",1)
								func2 	= function_generate_golf_link(arrList(0,intList),"",4)
							case "34"
								func 		= function_generate_sightseeing_link(arrList(0,intList),"",1)
								func2 	= function_generate_sightseeing_link(arrList(0,intList),"",4)
							case "36"
								func 		= function_generate_water_activity_link(arrList(0,intList),"",1)
								func2 	= function_generate_water_activity_link(arrList(0,intList),"",4)
							case  "38"
								func 		= function_generate_show_event_link(arrList(0,intList),"",1)
								func2 	= function_generate_show_event_link(arrList(0,intList),"",4)
							case "39"
								func 		= function_generate_health_check_up_link(arrList(0,intList),"",1)
								func2 	= function_generate_health_check_up_link(arrList(0,intList),"",4)
						end select
					response.write "document.write('<tr><td align = ""left"" >');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = "&func2&">');"
					response.write "document.write('<strong><font "&LCCon&">"&intCount&". <u>"&arrList(1,intList)&" "&cateCons&"</u></font></strong>');"
					response.write "document.write('</a>');"
					response.write "document.write('</td><td align = ""right"" >');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = "&func2&">');"
					response.write "document.write('<font "&LCCon&">From <strong>"&FormatNumber((arrList(2,intList)/arrList2(1,0))*intVatFactor,0)&" "&arrList2(2,0)&"</strong></font>');"
					response.write "document.write('</a>');"
					response.write "document.write('</td></tr>');"                            
					intCount=intCount+1
				Next
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center"" colspan='2'><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table><br />');"
				response.write "document.write('</td></tr></table>');"
			Case 21 'Layout 21
					response.write "document.write('<table width = ""640"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"" class=""fontSize"">');"
					response.write "document.write('<tr>');"
					response.write "document.write('<td align = ""left"">');"
					response.write "document.write('<strong><font "& HCCon&"><strong>Top Destinations for "&cateCon&"s</strong></font></strong>');"
					response.write "document.write('</td>');"
					response.write "document.write('</tr>');"
					response.write "document.write('</table><br />');"
					
					response.write "document.write('<table width = ""640"" cellpadding = ""0"" cellspacing = ""0""  "& BCon&">');"
					IF isArray(arrList) Then
					For intList=0 to UBound(arrList,2)
						select case strCate
							case "29"
								func 		= function_generate_hotel_link(arrList(0,intList),"",1)
								func2 	= function_generate_hotel_link(arrList(0,intList),"",4)
							case "32"
								func 		= function_generate_golf_link(arrList(0,intList),"",1)
								func2 	= function_generate_golf_link(arrList(0,intList),"",4)
							case "34"
								func 		= function_generate_sightseeing_link(arrList(0,intList),"",1)
								func2 	= function_generate_sightseeing_link(arrList(0,intList),"",4)
							case "36"
								func 		= function_generate_water_activity_link(arrList(0,intList),"",1)
								func2 	= function_generate_water_activity_link(arrList(0,intList),"",4)
							case  "38"
								func 		= function_generate_show_event_link(arrList(0,intList),"",1)
								func2 	= function_generate_show_event_link(arrList(0,intList),"",4)
							case "39"
								func 		= function_generate_health_check_up_link(arrList(0,intList),"",1)
								func2 	= function_generate_health_check_up_link(arrList(0,intList),"",4)
						end select

						strDetail = replace(func,"'","\'")
						strDetail = replace(strDetail,"""","\""")
						strDetail = replace(strDetail,vbCrLF,"") 

						sqlHotel="select top 3 p.product_id,p.title_en,p.files_name , "
						sqlHotel=sqlHotel&" isnull((select min(sop.price) min_price  "
						sqlHotel=sqlHotel&" from tbl_option_price sop, tbl_product_option spo "
						sqlHotel=sqlHotel&" where sop.option_id = spo.option_id and spo.status = 1 and spo.product_id = p.product_id and sop.price > 0 and spo.option_cat_id = "&option_cat&"),0)  min_price "
						sqlHotel=sqlHotel&" from tbl_product p,tbl_product_location ploc, tbl_location loc  "
						sqlHotel=sqlHotel&" where p.status=1 and p.destination_id="&arrList(0,intList)&" and p.product_id = ploc.product_id and ploc.location_id = loc.location_id " 
						sqlHotel=sqlHotel&" and loc.status = 1 and p.files_name is not null " 
						sqlHotel=sqlHotel&" and isnull((select min(sop.price) min_price  "
						sqlHotel=sqlHotel&" from tbl_option_price sop, tbl_product_option spo "
						sqlHotel=sqlHotel&" where sop.option_id = spo.option_id and spo.status = 1 and spo.product_id = p.product_id and sop.price > 0 and spo.option_cat_id = "&option_cat&"),0) > 0 "
						sqlHotel=sqlHotel&" order by min_price asc  "
						
						Set rsHotel=server.CreateObject("adodb.recordset")
						rsHotel.Open sqlHotel,conn,1,3
						response.write "document.write('<tr><td align = ""center"" valign = ""top""><br />');"	
						response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
						response.write "document.write('<tr><td valign = ""top"" align = ""left"" width = ""110"">');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"">');"			
						response.write "document.write('<img src=""http://www.booking2hotels.com/images/dest_"&arrList(1,intList)&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""98"" height = ""74"" alt = "&arrList(1,intList)&" />');"
						response.write "document.write('</a>');"
						response.write "document.write('</td>');"
						response.write "document.write('<td valign = ""middle"" align = ""left"">');"
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
						response.write "document.write('<tr height = ""20""><td>');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = """&func2&""">');"
						response.write "document.write('<strong><font "&LCCon&">"&arrList(1,intList)&"</font></strong>');"
						response.write "document.write('</a>');"
						response.write "document.write('</td></tr>');"
						Do while not rsHotel.Eof
						 	Dim strRsHot
							strRsHot = replace(rsHotel("title_en"),"'","\'")
							strRsHot = replace(strRsHot,"""","\""")
							strRsHot = replace(strRsHot,vbCrLF,"") 
							response.write "document.write('<tr height = ""20"">');"
							response.write "document.write('<td valign = ""top"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout24_arrow07.gif"" border = ""0""/> ');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&"/"&replace(rsHotel("files_name"),"'","\'")&psidCon&""" target=""_blank"" title = """&strRsHot&"""> ');"
							response.write "document.write('<font "&LCCon&">"&strRsHot&"</font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</td></tr>');"
							rsHotel.movenext
						Loop
						response.write "document.write('<tr><td align = ""center"">');"
						response.write "document.write('<table width = ""500"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
						response.write "document.write('<tr>');"
						response.write "document.write('<td align = ""right"" style ="" border-bottom : solid 1px #CCCCCC"">');"
						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = "&func2&">');"
						response.write "document.write('<strong><font color = ""#7F7F7F"">View all Hotels in "&arrList(1,intList)&" >></font></strong>');"
						response.write "document.write('</a>');"
						response.write "document.write('&nbsp;&nbsp;<br /><br />');"
						response.write "document.write('</td>');"
						response.write "document.write('</tr>');"
						response.write "document.write('</table></td></tr>');"
						rsHotel.close
						Set rsHotel=Nothing
						response.write "document.write('</table>');"
						response.write "document.write('</td></tr></table>');"
						response.write "document.write('</td></tr>');"
					Next
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			Case 22 'Layout 18
				Dim sqlHotelLocation
				Dim rsHotelLocation
				Dim arrHotelLocation
				Dim intLocation
				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"" class=""fontSize""><tr><td>');"
				response.write "document.write('<font color = ""red""><strong>Hot Destinations</strong></font>');"
				response.write "document.write('</td></tr></table><br />');"
				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"" style = ""border-bottom : solid 3px #CCCCCC"">');"
				IF isArray(arrList) Then
				For intCount=0 to Ubound(arrList3,2)	'	loop destination (title_en)
					strDetail = replace(arrList3(1,intCount),"'","\'")
					strDetail = replace(strDetail,"""","\""")
					For intList=0 to Ubound(arrList,2)
						IF arrList(0,intList)=arrList3(0,intCount) Then
							select case strCate
								case "29"
									func = function_generate_hotel_link(arrList(0,intList),"",1)
								case "32"
									func = function_generate_golf_link(arrList(0,intList),"",1)
								case "34"
									func = function_generate_sightseeing_link(arrList(0,intList),"",1)
								case "36"
									func = function_generate_water_activity_link(arrList(0,intList),"",1)
								case  "38"
									func = function_generate_show_event_link(arrList(0,intList),"",1)
								case "39"
									func = function_generate_health_check_up_link(arrList(0,intList),"",1)
							end select
							Exit for
						End IF
					Next
					response.write "document.write('<tr><td align = ""left"" height = ""20"" "& HBCon&" colspan = ""2"">');"
					response.write "document.write('&nbsp;&nbsp;');"
					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&".asp"&psidCon&""" target=""_blank"" title = "&strDetail&">');"
					response.write "document.write('<strong><font "& HCCon&">"&strDetail&"</font></strong>');"
					response.write "document.write('</a>');"
					response.write "document.write('</td>');"
					response.write "document.write('<tr><td colspan = ""2"">&nbsp;</td></tr>');"
					sqlHotel="select top 5 d.destination_id,l.location_id,l.title_en,Rtrim(l.files_name),d.title_en "
					sqlHotel=sqlHotel&" from tbl_location l,tbl_destination d"
					sqlHotel=sqlHotel&" where d.destination_id=l.destination_id and l.status=1 and d.destination_id ="&arrList3(0,intCount)
					sqlHotel=sqlHotel&"  and l.files_name is not null order by l.title_en asc"
					Set rsHotel=server.CreateObject("adodb.recordset")
					rsHotel.Open sqlHotel,conn,1,3
					IF Not rsHotel.EOF Then
						arrHotel=rsHotel.getRows()
					End IF
					rsHotel.close
					Set rsHotel=Nothing
					
					For intList=0 to Ubound(arrHotel,2)
							strDetail2 = replace(arrHotel(2,intList),"'","\'")
							strDetail2 = replace(strDetail2,"""","\""")
							response.write "document.write('<tr><td valign = ""top"" align = ""left""><strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&arrHotel(3,intList)&psidCon&""" target=""_blank"" title = """&strDetail&""">');"
							response.write "document.write('<font "& LCCon&"><u>"&strDetail2&"</u></font>');"
							response.write "document.write('</a>');"
							response.write "document.write('</strong></td>');"
							response.write "document.write('<td align = ""left"" valign = ""top"" style = ""line-height : 20px"">');"
							sqlHotelLocation="select top 10 p.title_en,p.files_name, " 
							sqlHotelLocation=sqlHotelLocation&" isnull((select min(sop.price) min_price  "
							sqlHotelLocation=sqlHotelLocation&" from tbl_option_price sop, tbl_product_option spo "
							sqlHotelLocation=sqlHotelLocation&" where sop.option_id = spo.option_id and spo.status = 1 and spo.product_id = p.product_id and sop.price > 0 and spo.option_cat_id = "&option_cat&"),0)  min_price "
							sqlHotelLocation=sqlHotelLocation&"from tbl_product p,tbl_product_location ploc ,tbl_location loc "
							sqlHotelLocation=sqlHotelLocation&"where p.product_id = ploc.product_id and loc.location_id = ploc.location_id and loc.status = 1"
							sqlHotelLocation=sqlHotelLocation&"and p.destination_id = "&arrList3(0,intCount)
							sqlHotelLocation=sqlHotelLocation&"and p.status = 1 and ploc.location_id ="&arrHotel(1,intList)&"  and p.files_name is not null "
							sqlHotelLocation=sqlHotelLocation&" and isnull((select min(sop.price) min_price  "
							sqlHotelLocation=sqlHotelLocation&" from tbl_option_price sop, tbl_product_option spo "
							sqlHotelLocation=sqlHotelLocation&" where sop.option_id = spo.option_id and spo.status = 1 and spo.product_id = p.product_id and sop.price > 0 and spo.option_cat_id = "&option_cat&"),0) > 0 "
							sqlHotelLocation=sqlHotelLocation&" order by min_price asc  "
							Set rsHotelLocation=server.CreateObject("adodb.recordset")
							rsHotelLocation.Open sqlHotelLocation,conn,1,3
							IF Not rsHotelLocation.EOF Then
								arrHotelLocation=rsHotelLocation.getRows()
							End IF
							rsHotelLocation.close
							Set rsHotelLocation=Nothing
							For intLocation = 0 to Ubound(arrHotelLocation,2)
								strFileName = replace(arrHotelLocation(1,intLocation),"'","\'")
								strFileName = replace(strFileName ,"""","\""")
								strDetail3 = replace(arrHotelLocation(0,intLocation),"'","\'")
								strDetail3 = replace(strDetail3,"""","\""")
								if (intLocation =0) then
									response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&"/"&strFileName&psidCon&""" target=""_blank"" title = """&strDetail3&""">');"
									response.write "document.write('<font "& LCCon&"><u>"&strDetail3&"</u></font>');"
									response.write "document.write('</a>');"
								else
									response.write "document.write('<a href=""http://www.hotels2thailand.com/"&func&"/"&strFileName&psidCon&""" target=""_blank"" title = """&strDetail3&""">');"
									response.write "document.write('<font "& LCCon&"><u>,"&strDetail3&"</u></font>');"
									response.write "document.write('</a>');"
								end if
							Next
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&arrHotel(3,intList)&psidCon&""" target=""_blank"" title = """&strDetail2&""">');"
							response.write "document.write('<font color = ""#7F7F7F""> More...</font><br><br>');"
							response.write "document.write('</a>');"
					Next
					response.write "document.write('</td></tr>');"
					response.write "document.write('<tr><td colspan = ""2"">&nbsp;</td></tr>');"
				Next
				Else
					response.write "document.write('<tr height = ""25""><td align = ""center""><strong><font color=""#FF0000"">Renewal Rate is Coming Soon!  Please contact to <a href=""mailto:reservation@hotels2thailand.com"">reservation@hotels2thailand.com</a></font></strong></td></tr>');"
				End IF
				response.write "document.write('</table>');"
			End Select 	'	End intType
End Function
%>