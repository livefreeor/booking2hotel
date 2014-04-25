<%
Function function_display_abf(bolAbf)
	If bolAbf Then
		function_display_abf="<img src=http://www.booking2hotels.com/images/ok.gif>"
	Else
		function_display_abf="-"
	End IF
End Function
%>
<%
Function function_display_column(strType,intType)
	Dim strCol
	Dim strColTitle
	Dim strFnSortMin
	Dim strFnSortMax
	Dim strValue
	
	Select Case strType
		Case "l"
			strCol="location"
			strColTitle="Location"
			strFnSortMin="<a href=""#"" onclick=""function_sort_min(2)"">A-Z</a>"
			strFnSortMax="<a href=""#"" onclick=""function_sort_max(2)"">Z-A</a>"
			strValue="arrHotel[i][2]"
		Case "m"
			strCol="rate"
			strColTitle="Rate"
			strFnSortMin="<a href=""#"" onclick=""function_sort_min(3)"">L-H</a>"
			strFnSortMax="<a href=""#"" onclick=""function_sort_max(3)"">H-L</a>"
			strValue="arrHotel[i][3]"
		Case "c"
			strCol="class"
			strColTitle="Class"
			strFnSortMin="<a href=""#"" onclick=""function_sort_min(6)"">L-H</a>"
			strFnSortMax="<a href=""#"" onclick=""function_sort_max(6)"">H-L</a>"
			strValue="arrHotel[i][6]"
		Case "p"
			strCol="pic"
			strColTitle="Pic"
			strFnSortMin=""
			strFnSortMax=""
	End Select
		
	Select Case intType
	Case 1
		function_display_column=strColTitle
	Case 2
		function_display_column=strCol
	Case 3
		function_display_column=strFnSortMin
	Case 4
		function_display_column=strFnSortMax
	Case 5
		function_display_column=strValue
	End Select
End Function
%>
<%
Function function_gen_table_js(arrList,arrCol,bodyColor,bgColor,txtTitle,txtColor,pColor,intType,strCate,show,sortby,arrList2,arrList3)
	Dim BCon
	Dim HBCon
	Dim HCCon
	Dim LCCon
	Dim max_col
	Dim intCol
	Dim arrDest 
	Dim intDest
	Dim intCount
	Dim PCCon
	Dim intStar
	Dim strRate
	Dim num1
	Dim num2
	Dim rCon
	Dim cateCon
						
	strRate = 0					
	BCon = ""
	HBCon= ""
	HCCon = ""
	LCCon = ""
	PCCon = ""
	cateCon = ""
			
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
	
	Select Case strCate
		Case "29" 'hotel
					cateCon = "Hotels"
		Case "32" 'golf course
					cateCon = "Golf Courses"
		Case "34" 'day trip
					cateCon = "Day Trips"
		Case "36" 'water activity
					cateCon = "Water Activities"
		Case "38" 'show and event
					cateCon = "Shows And Events"
		Case "39" 'health check up
					cateCon = "Health Check Up"
	End Select
		
	Select Case show
		Case 1	'	all destination
			Select Case intType		
				Case 1 'full
					response.write "document.write('case1');"
		'			'Dim strCol
		'			'Dim strValue
		'			'Dim max_col
		'			'Dim intCol	
		'			'Dim arrDest
		'			'Dim intDest	
		'			'Dim intCount
		'			
		'			response.write "document.write('<table><tr><td>case1</td></tr></table>');"														 
		'			
		'			'response.write "document.write('alert(getRefToDiv(""title_0""))')"
		'			'	response.write "function function_get_currency(strCur){"&vbcrlf
		'			'	response.write "for(var i=0;i<arrCur.length;i++){"&vbcrlf
		'			'	response.write "if(arrCur[i][2]==strCur){"&vbcrlf
		'			'	response.write "return arrCur[i][1]"&vbcrlf
		'			'	response.write "}"&vbcrlf
		'			'	response.write "}"&vbcrlf
		'			'	response.write "}"&vbcrlf
		'			
		'			'Create New Array Hotel	
		'			'	response.write "var arrHotel=new Array;"&vbcrlf
		'			'	For intList=0 to UBound(arrList,2)
		'			'		response.write "arrHotel["&intList&"]=new Array;"&vbcrlf
		'			'		response.write "arrHotel["&intList&"][0]="""&arrList(0,intList)&""";"&vbcrlf
		'			'		response.write "arrHotel["&intList&"][1]="""&arrList(1,intList)&""";"&vbcrlf
		'			'		response.write "arrHotel["&intList&"][2]="""&arrList(2,intList)&""";"&vbcrlf
		'			'		response.write "arrHotel["&intList&"][3]="&arrList(3,intList)&";"&vbcrlf
		'			'		response.write "arrHotel["&intList&"][4]="""&function_display_abf(arrList(4,intList))&""";"&vbcrlf
		'			'		response.write "arrHotel["&intList&"][5]="""&arrList(5,intList)&""";"&vbcrlf
		'			'		response.write "arrHotel["&intList&"][6]=""<img src=http://www.booking2hotels.com/images/star_rate"&int(arrList(6,intList))&".gif>"";"&vbcrlf
		'			'		response.write "arrHotel["&intList&"][7]="""&arrList(7,intList)&""";"&vbcrlf
		'			'	Next
		'			''-----------------------	
		'			''Generate Rate Table
		'			'	
		'			'	response.write "document.write('<table width=""100%"" border=""0"" cellpadding=""3"" cellspacing=""1"" bgcolor="""&bodyColor&""" style=""color:"&txtColor&";font-size:12px;font-family:Verdana, Arial, Helvetica, sans-serif"">');"
		'			'	response.write "document.write('<tr style=""color:"&txtTitle&";font-weight:bold"">');"
		'			'	response.write "document.write('<td>Title<br><a href=""#"" onClick=""function_sort_min(0)"">A-Z</a> | <a href=""#"" onClick=""function_sort_max(0)"">Z-A</a></td>');"
		'			'	For intCol=0 to Ubound(arrCol)
		'			'		response.write "document.write('<td>"&function_display_column(arrCol(intCol),1)&" "&function_display_column(arrCol(intCol),3)&" | "&function_display_column(arrCol(intCol),4)&"</td>');"
		'			'	Next
		'			'	'response.write "document.write('<td>Location<br><a href=""#"" onClick=""function_sort_min(2)"">A-Z</a> | <a href=""#"" onClick=""function_sort_max(2)"">Z-A</a></td>');"
		'			'	'response.write "document.write('<td>Class<br><a href=""#"" onClick=""function_sort_min(6)"">L-H</a> | <a href=""#"" onClick=""function_sort_max(6)"">H-L</a></td>');"
		'			'	'response.write "document.write('<td>Rate<br><a href=""#"" onClick=""function_sort_min(3)"">L-H</a> | <a href=""#"" onClick=""function_sort_max(3)"">H-L</a></td>');"
		'			'	response.write "document.write('<td>ABF</td>');"
		'			'	response.write "document.write('</tr>');"
		'			'	
		'			'	response.write "for(var i=0;i<arrHotel.length;i++){"&vbcrlf
		'			'	response.write "document.write('<tr bgcolor="""&bgColor&""">');"&vbcrlf
		'			'	response.write "document.write('<td><div id=""title_'+i+'""><a href=""http://www.hotels2thailand.com/'+arrHotel[i][1]+'-hotels/'+arrHotel[i][5]+'"">'+arrHotel[i][0]+'</a></div></td>');"&vbcrlf
		'			'	For intCol=0 to Ubound(arrCol)
		'			'		response.write "document.write('<td><div id="""&function_display_column(arrCol(intCol),1)&"_'+i+'"">'+"&function_display_column(arrCol(intCol),5)&"+'</div></td>');"
		'			'	Next
		'			'	'response.write "document.write('<td><div id=""location_'+i+'"">'+arrHotel[i][2]+'</div></td>');"&vbcrlf
		'			''	response.write "document.write('<td><div id=""class_'+i+'"">'+arrHotel[i][6]+'</div></td>');"&vbcrlf
		'			''	response.write "document.write('<td align=right><div id=""rate_'+i+'"">'+arrHotel[i][3]+'</div></td>');"&vbcrlf
		'			'	response.write "document.write('<td><div id=""abf_'+i+'"">'+arrHotel[i][4]+'</div></td>');"&vbcrlf
		'			'	response.write "document.write('</tr>');"
		'			'	response.write "}"&vbcrlf
		'			'	response.write "document.write('</table>');"&vbcrlf
		'			'	'---------------------------------------------
		'			'
		'			'	response.write "function getRefToDiv(divID) {"&vbcrlf
		'			'	response.write "if( document.getElementById ) { "&vbcrlf
		'			'	response.write "return document.getElementById(divID); }"&vbcrlf
		'			'	response.write "return false;"&vbcrlf
		'			'	response.write "}"&vbcrlf
		'			'	
		'			'	response.write "function function_sort_min(sortType){"&vbcrlf
		'			'	response.write "var row"&vbcrlf
		'			'	response.write "var j"&vbcrlf
		'			'	response.write "var key_value"&vbcrlf
		'			'	response.write "var other_value"&vbcrlf
		'			'	response.write "var new_key"&vbcrlf
		'			'	response.write "var new_other"&vbcrlf
		'			'	response.write "var swap_pos"&vbcrlf
		'			'	response.write "var other_dimension"&vbcrlf
		'			'	response.write "var varDim0"&vbcrlf
		'			'	response.write "var varDim1"&vbcrlf
		'			'	response.write "var varDim2"&vbcrlf
		'			'	response.write "var varDim3"&vbcrlf
		'			'	response.write "var varDim4"&vbcrlf
		'			'	response.write "var varDim5"&vbcrlf
		'			'	response.write "var varDim6"&vbcrlf
		'			'	response.write "var varDim7"&vbcrlf
		'			'	response.write "var new_varDim0"&vbcrlf
		'			'	response.write "var new_varDim1"&vbcrlf
		'			'	response.write "var new_varDim2"&vbcrlf
		'			'	response.write "var new_varDim3"&vbcrlf
		'			'	response.write "var new_varDim4"&vbcrlf
		'			'	response.write "var new_varDim5"&vbcrlf
		'			'	response.write "var new_varDim6"&vbcrlf
		'			'	response.write "var new_varDim7"&vbcrlf
		'			'	response.write "for (var row=0;row<arrHotel.length-1;row++){"&vbcrlf
		'			' 	response.write "key_value=arrHotel[row][sortType]"&vbcrlf
		'			'	response.write "varDim0=arrHotel[row][0]"&vbcrlf
		'			'	response.write "varDim1=arrHotel[row][1]"&vbcrlf
		'			'	response.write "varDim2=arrHotel[row][2]"&vbcrlf
		'			'	response.write "varDim3=arrHotel[row][3]"&vbcrlf
		'			'	response.write "varDim4=arrHotel[row][4]"&vbcrlf
		'			'	response.write "varDim5=arrHotel[row][5]"&vbcrlf
		'			'	response.write "varDim6=arrHotel[row][6]"&vbcrlf
		'			'	response.write "varDim7=arrHotel[row][7]"&vbcrlf
		'			'	response.write "new_key=arrHotel[row][sortType]"&vbcrlf
		'			'	response.write "new_varDim0=arrHotel[row][0]"&vbcrlf
		'			'	response.write "new_varDim1=arrHotel[row][1]"&vbcrlf
		'			'	response.write "new_varDim2=arrHotel[row][2]"&vbcrlf
		'			'	response.write "new_varDim3=arrHotel[row][3]"&vbcrlf
		'			'	response.write "new_varDim4=arrHotel[row][4]"&vbcrlf
		'			'	response.write "new_varDim5=arrHotel[row][5]"&vbcrlf
		'			'	response.write "new_varDim6=arrHotel[row][6]"&vbcrlf
		'			'	response.write "new_varDim7=arrHotel[row][7]"&vbcrlf
		'			'	response.write "swap_pos=row"&vbcrlf
		'			'	response.write "for (var j=row+1;j<arrHotel.length;j++){"&vbcrlf
		'			'	response.write "if(arrHotel[j][sortType] < new_key){"&vbcrlf
		'			'	response.write "swap_pos=j"&vbcrlf
		'			'	response.write "new_key=arrHotel[j][sortType]"&vbcrlf
		'			'	response.write "new_varDim0=arrHotel[j][0]"&vbcrlf
		'			'	response.write "new_varDim1=arrHotel[j][1]"&vbcrlf
		'			'	response.write "new_varDim2=arrHotel[j][2]"&vbcrlf
		'			'	response.write "new_varDim3=arrHotel[j][3]"&vbcrlf
		'			'	response.write "new_varDim4=arrHotel[j][4]"&vbcrlf
		'			'	response.write "new_varDim5=arrHotel[j][5]"&vbcrlf
		'			'	response.write "new_varDim6=arrHotel[j][6]"&vbcrlf
		'			'	response.write "new_varDim7=arrHotel[j][7]"&vbcrlf
		'			'	response.write "}"&vbcrlf
		'			'	response.write "}"&vbcrlf
		'			'	response.write "if(swap_pos!=row){"&vbcrlf
		'			'	response.write "arrHotel[swap_pos][0]=varDim0"&vbcrlf
		'			'	response.write "arrHotel[swap_pos][1]=varDim1"&vbcrlf
		'			'	response.write "arrHotel[swap_pos][2]=varDim2"&vbcrlf
		'			'	response.write "arrHotel[swap_pos][3]=varDim3"&vbcrlf
		'			'	response.write "arrHotel[swap_pos][4]=varDim4"&vbcrlf
		'			'	response.write "arrHotel[swap_pos][5]=varDim5"&vbcrlf
		'			'	response.write "arrHotel[swap_pos][6]=varDim6"&vbcrlf
		'			'	response.write "arrHotel[swap_pos][7]=varDim7"&vbcrlf
		'			'	response.write "arrHotel[row][0]=new_varDim0"&vbcrlf
		'			'	response.write "arrHotel[row][1]=new_varDim1"&vbcrlf
		'			'	response.write "arrHotel[row][2]=new_varDim2"&vbcrlf
		'			'	response.write "arrHotel[row][3]=new_varDim3"&vbcrlf
		'			'	response.write "arrHotel[row][4]=new_varDim4"&vbcrlf
		'			'	response.write "arrHotel[row][5]=new_varDim5"&vbcrlf
		'			'	response.write "arrHotel[row][6]=new_varDim6"&vbcrlf
		'			'	response.write "arrHotel[row][7]=new_varDim7"&vbcrlf
		'			'	response.write "}"&vbcrlf
		'			' 	response.write "}"&vbcrlf
		'			'	response.write "intCount=0"&vbcrlf
		'			' 	response.write "for(var x=0;x<arrHotel.length;x++){"&vbcrlf
		'			'	response.write "myTitle = getRefToDiv(""title_"" + intCount); if (myTitle) myTitle.innerHTML =arrHotel[x][0]"&vbcrlf
		'			'	response.write "myLocation = getRefToDiv(""location_"" + intCount); if (myLocation) myLocation.innerHTML =arrHotel[x][2]"&vbcrlf
		'			'	response.write "myClass = getRefToDiv(""class_"" + intCount); if (myClass) myClass.innerHTML =arrHotel[x][6]"&vbcrlf
		'			'	response.write "myAbf = getRefToDiv(""abf_"" + intCount); if (myAbf) myAbf.innerHTML =arrHotel[x][4]"&vbcrlf
		'			'	response.write "myRate = getRefToDiv(""rate_"" + intCount); if (myRate) myRate.innerHTML =arrHotel[x][3]"&vbcrlf
		'			'	response.write "intCount+=1	"&vbcrlf
		'			' 	response.write "}"&vbcrlf
		'			'	response.write "}"&vbcrlf
		'			
		'				response.write "function function_sort_max(sortType){"&vbcrlf
		'				response.write "var row"&vbcrlf
		'				response.write "var j"&vbcrlf
		'				response.write "var key_value"&vbcrlf
		'				response.write "var other_value"&vbcrlf
		'				response.write "var new_key"&vbcrlf
		'				response.write "var new_other"&vbcrlf
		'				response.write "var swap_pos"&vbcrlf
		'				response.write "var other_dimension"&vbcrlf
		'				response.write "var varDim0"&vbcrlf
		'				response.write "var varDim1"&vbcrlf
		'				response.write "var varDim2"&vbcrlf
		'				response.write "var varDim3"&vbcrlf
		'				response.write "var varDim4"&vbcrlf
		'				response.write "var varDim5"&vbcrlf
		'				response.write "var varDim6"&vbcrlf
		'				response.write "var varDim7"&vbcrlf
		'				response.write "var new_varDim0"&vbcrlf
		'				response.write "var new_varDim1"&vbcrlf
		'				response.write "var new_varDim2"&vbcrlf
		'				response.write "var new_varDim3"&vbcrlf
		'				response.write "var new_varDim4"&vbcrlf
		'				response.write "var new_varDim5"&vbcrlf
		'				response.write "var new_varDim6"&vbcrlf
		'				response.write "var new_varDim7"&vbcrlf
		'				response.write "for (var row=0;row<arrHotel.length-1;row++){"&vbcrlf
		'			 	response.write "key_value=arrHotel[row][sortType]"&vbcrlf
		'				response.write "varDim0=arrHotel[row][0]"&vbcrlf
		'				response.write "varDim1=arrHotel[row][1]"&vbcrlf
		'				response.write "varDim2=arrHotel[row][2]"&vbcrlf
		'				response.write "varDim3=arrHotel[row][3]"&vbcrlf
		'				response.write "varDim4=arrHotel[row][4]"&vbcrlf
		'				response.write "varDim5=arrHotel[row][5]"&vbcrlf
		'				response.write "varDim6=arrHotel[row][6]"&vbcrlf
		'				response.write "varDim7=arrHotel[row][7]"&vbcrlf
		'				response.write "new_key=arrHotel[row][sortType]"&vbcrlf
		'				response.write "new_varDim0=arrHotel[row][0]"&vbcrlf
		'				response.write "new_varDim1=arrHotel[row][1]"&vbcrlf
		'				response.write "new_varDim2=arrHotel[row][2]"&vbcrlf
		'				response.write "new_varDim3=arrHotel[row][3]"&vbcrlf
		'				response.write "new_varDim4=arrHotel[row][4]"&vbcrlf
		'				response.write "new_varDim5=arrHotel[row][5]"&vbcrlf
		'				response.write "new_varDim6=arrHotel[row][6]"&vbcrlf
		'				response.write "new_varDim7=arrHotel[row][7]"&vbcrlf
		'				response.write "swap_pos=row"&vbcrlf
		'				response.write "for (var j=row+1;j<arrHotel.length;j++){"&vbcrlf
		'				response.write "if(arrHotel[j][sortType] > new_key){"&vbcrlf
		'				response.write "swap_pos=j"&vbcrlf
		'				response.write "new_key=arrHotel[j][sortType]"&vbcrlf
		'				response.write "new_varDim0=arrHotel[j][0]"&vbcrlf
		'				response.write "new_varDim1=arrHotel[j][1]"&vbcrlf
		'				response.write "new_varDim2=arrHotel[j][2]"&vbcrlf
		'				response.write "new_varDim3=arrHotel[j][3]"&vbcrlf
		'				response.write "new_varDim4=arrHotel[j][4]"&vbcrlf
		'				response.write "new_varDim5=arrHotel[j][5]"&vbcrlf
		'				response.write "new_varDim6=arrHotel[j][6]"&vbcrlf
		'				response.write "new_varDim7=arrHotel[j][7]"&vbcrlf
		'				response.write "}"&vbcrlf
		'				response.write "}"&vbcrlf
		'				response.write "if(swap_pos!=row){"&vbcrlf
		'				response.write "arrHotel[swap_pos][0]=varDim0"&vbcrlf
		'				response.write "arrHotel[swap_pos][1]=varDim1"&vbcrlf
		'				response.write "arrHotel[swap_pos][2]=varDim2"&vbcrlf
		'				response.write "arrHotel[swap_pos][3]=varDim3"&vbcrlf
		'				response.write "arrHotel[swap_pos][4]=varDim4"&vbcrlf
		'				response.write "arrHotel[swap_pos][5]=varDim5"&vbcrlf
		'				response.write "arrHotel[swap_pos][6]=varDim6"&vbcrlf
		'				response.write "arrHotel[swap_pos][7]=varDim7"&vbcrlf
		'				response.write "arrHotel[row][0]=new_varDim0"&vbcrlf
		'				response.write "arrHotel[row][1]=new_varDim1"&vbcrlf
		'				response.write "arrHotel[row][2]=new_varDim2"&vbcrlf
		'				response.write "arrHotel[row][3]=new_varDim3"&vbcrlf
		'				response.write "arrHotel[row][4]=new_varDim4"&vbcrlf
		'				response.write "arrHotel[row][5]=new_varDim5"&vbcrlf
		'				response.write "arrHotel[row][6]=new_varDim6"&vbcrlf
		'				response.write "arrHotel[row][7]=new_varDim7"&vbcrlf
		'				response.write "}"&vbcrlf
		'			 	response.write "}"&vbcrlf
		'				response.write "intCount=0"&vbcrlf
		'			 	response.write "for(var x=0;x<arrHotel.length;x++){"&vbcrlf
		'				response.write "myTitle = getRefToDiv(""title_"" + intCount); if (myTitle) myTitle.innerHTML =arrHotel[x][0]"&vbcrlf
		'				response.write "myLocation = getRefToDiv(""location_"" + intCount); if (myLocation) myLocation.innerHTML =arrHotel[x][2]"&vbcrlf
		'				response.write "myClass = getRefToDiv(""class_"" + intCount); if (myClass) myClass.innerHTML =arrHotel[x][6]"&vbcrlf
		'				response.write "myAbf = getRefToDiv(""abf_"" + intCount); if (myAbf) myAbf.innerHTML =arrHotel[x][4]"&vbcrlf
		'				response.write "myRate = getRefToDiv(""rate_"" + intCount); if (myRate) myRate.innerHTML =arrHotel[x][3]"&vbcrlf
		'				response.write "intCount+=1	"&vbcrlf
		'			 	response.write "}"&vbcrlf
		'				response.write "}"&vbcrlf
				Case 2 'layout 5
'					response.write "document.write('<table width = ""200""  "&BCon&" cellpadding = ""3"" cellspacing = ""0"">');"										
'					response.write "document.write('<tr height = ""25"" valign = ""middle"">');"
'					response.write "document.write('<td "& HBCon&"><font "& HCCon&"><strong>Popular Destinations</strong></font></td>');"
'					response.write "document.write('</tr>');"
'					For intList=0 To Ubound(arrList,2)
'						response.write "document.write('<tr><td align = ""left"">&nbsp;<img src = ""http://www.booking2hotels.com/images/blue_bullet.gif"" border = ""0"" />');"
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&""" target=""_blank"" title = """&replace(arrList(1,intList)," ","\")&""">');"
'						response.write "document.write('<strong><font "&LCCon&">"&replace(arrList(1,intList)," ","\")&" "&function_pro_cate(strCate,arrList(0,intList),2)&"</font></strong></td></tr>');"
'					Next
'					response.write "document.write('</table>');"	
					response.write "document.write('case1.2');"
'				Case 4,5,6 'Layout 1 2 3			
'					Select Case intType
'						Case 4
'							max_col=2
'						Case 5
'							max_col=3
'						Case 6
'							max_col=4
'					End Select
'					intCol=0
'					response.write "document.write('<table width = ""460"" cellpadding = ""3"" cellspacing = ""0"" style = "" border =""0"">');"
'					response.write "document.write('<tr><td align = ""left""><strong><font "& HCCon&">Top Destinations for Hotel</font></strong></td></tr>');"
'					response.write "document.write('</table><br>');"
'					response.write "document.write('<table width = ""460"" cellpadding = ""3"" cellspacing = ""0"" "&BCon&">');"
'					response.write "document.write('<tr>');"
'					For intList=0 To Ubound(arrList,2)	
'						IF intCol mod max_col=0 Then
'							response.write "document.write('</tr><tr>');"
'						End IF
'						response.write "document.write('<td align =""center""><table cellpadding = ""0"" cellspacing = ""0"">');"	
'						response.write "document.write('<tr><td align = ""left"" width = ""220""><table cellpadding = ""3"" cellspacing = ""0"" border = ""0""><tr><td align = ""left"">');"	
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&""" target=""_blank"">');"	
'						response.write "document.write('<img src=""http://www.booking2hotels.com/images/dest_"&arrList(1,intList)&".jpg""  style = ""border : solid 1px #CCCCCC"" width = ""70"" height = ""53"" alt = """&arrList(1,intList)&""">');"	
'						response.write "document.write('</a></td><td align = ""left"" valign = ""bottom"">');"	
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&""" target=""_blank""  title = """&arrList(1,intList)&""">');"
'						response.write "document.write('<strong><font "&LCCon&"><u>"&arrList(1,intList)&"</u></font></strong></a></td></tr></table></td></tr></table></td>');"		
'						intCol=intCol+1
'					Next
'					response.write "document.write('</tr>');"	
'					response.write "document.write('</table>');"	
'				Case 7 'Layout 4
'					response.write "document.write('<table width = ""600"" cellpadding = ""3"" cellspacing = ""0"" border = ""0"">');"	
'					response.write "document.write('<tr><td>');"
'					response.write "document.write('<strong><font "& HCCon&">Top Destination link for Hotel</font></strong>');"
'					response.write "document.write('</td></tr>');"
'					For intList=0 To Ubound(arrList,2)				
'						response.write "document.write('<tr><td><br /><table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""90%"">');"
'						response.write "document.write('<tr><td valign = ""top"" width = ""18%"">');"
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(0,intList),"",5)&""" target=""_blank"">');"
'						response.write "document.write('<img src = ""http://www.booking2hotels.com/images/dest_"&function_generate_hotel_link(arrList(0,intList),"",4)&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""80"" height = ""61"" alt = """&arrList(1,intList)&""" /></a>');"
'						response.write "document.write('</td>');"
'						response.write "document.write('<td align = ""left"" valign = ""top"" style ="" line-height : 20px"">');"
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(0,intList),"",5)&""" target=""_blank"" title = """&arrList(1,intList)&""">');"
'						response.write "document.write('<strong><font "&LCCon&">"&arrList(1,intList)&"</font></strong></a><br />');"
'						response.write "document.write('"&replace(arrList(2,intList),"'","\'")&"');" 
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(0,intList),"",5)&""" target=""_blank""><font "&LCCon&">More</font></a>');"
'						response.write "document.write('</td></tr>');"
'						response.write "document.write('<tr><td colspan = ""2"" align = ""right""><br />');"
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(0,intList),"",5)&""" target=""_blank"">');"
'						response.write "document.write('<font "&LCCon&">View all Hotels in Ayutthaya >>&nbsp;&nbsp;</font></a>');"
'						response.write "document.write('</td></tr></table></td></tr>');"				
'					Next
'				Case 8,9,10,11 'Layout 6 7 8 9
'					Dim width
'					Dim colSp
'					Dim colors
'					Dim colors2
'					Dim temp
'					
'					colors = Array("#3899A0", "#B2B219")
'					Select Case intType
'						Case 8
'							max_col=2
'							width = " width =""310"""
'							colSp = " colspan =""2"""
'							colors = "#3899A0"
'							colors2 = "#B2B219"
'						Case 9
'							max_col=3
'							width = " width =""460"""
'							colSp = " colspan =""3"""
'						Case 10
'							max_col=4
'							width = " width =""620"""
'							colSp = " colspan =""4"""
'						Case 11
'							max_col=5
'							width = " width =""780"""
'							colSp = " colspan =""5"""
'					End Select
'					intCol=0
'					response.write "document.write('<table "&width&" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
'					response.write "document.write('<tr><td "&colSp&" align = ""center""><strong><font "& HCCon&">Top Destinations of Hotel');"
'					response.write "document.write('</font></strong><br /><br /></td></tr><tr>');"
'					For intList=0 To Ubound(arrList,2)				
'						IF intCol mod max_col=0 Then
'							response.write "document.write('</tr><tr>');"
'						End IF
'						response.write "document.write('<td><table cellpadding = ""3"" cellspacing = ""0"" "&BCon&">');"
'						response.write "document.write('<tr><td align = ""left"" valign = ""top"" background = ""http://www.booking2hotels.com/images/dest_"&arrList(1,intList)&".jpg"" width = ""133"" height = ""101"">');"
'						response.write "document.write('&nbsp;<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&""" target=""_blank"" title = """&arrList(1,intList)&""">');"
'						response.write "document.write('<strong><font "&LCCon&">"&arrList(1,intList)&"</font></strong></a></td></tr><br>');"
'						if (intType <> 8) then
'							response.write "document.write('<tr><td height = ""25"" bgcolor = """&colors(intList mod 2)&""" align = ""right"">');"
'						else
'							response.write "document.write('<tr><td height = ""25"" bgcolor = """&colors&""" align = ""right"">');"
'							if (intCol mod 2) <> 1 then
'								temp = colors
'								colors = colors2
'								colors2 = temp
'							end if
'						end if
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&""" target=""_blank"">');"
'						response.write "document.write('<strong><font color = ""#FFFFFF"">More Info >></font></strong></a></td></tr></table></td>');"
'						intCol=intCol+1
'					Next
'					response.write "document.write('</tr>');"
'					response.write "document.write('</table>');"
'			Case 12 'Layout 10
'				response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""460"">');"
'				response.write "document.write('<tr><td align = ""left""><strong><font "& HCCon&">Top Hotel Destinations</font></strong></td></tr>');"
'				response.write "document.write('</table><br>');"
'				response.write "document.write('<table width = ""300"" cellpadding = ""5"" cellspacing = ""0"" "&BCon&">');"
'				response.write "document.write('<tr><td align = ""left"">');"
'						
'				For intList=0 To Ubound(arrList,2)			
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&""" target=""_blank"" title = """&arrList(1,intList)&""">');"
'					response.write "document.write('<strong><font "&LCCon&">"&arrList(1,intList)&"</font></strong></a>');"
'					IF intList<>Ubound(arrList,2) Then
'						response.write "document.write(' | ');"
'					End IF
'				Next
'				response.write "document.write('</td></tr>');"
'				response.write "document.write('<tr><td align = ""right"">');"
'				response.write "document.write('<img src = ""http://www.booking2hotels.com/images/arrw_right.gif"" border = ""0"" />');"
'				response.write "document.write('<a href=""http://www.hotels2thailand.com/thailand-hotels.asp"" target=""_blank"" >View All Destinations</a>&nbsp;&nbsp;');"
'				response.write "document.write('</td></tr>');"
'				response.write "document.write('</table>');"
'			Case 13,14 'Layout 11 12
'				Select Case intType
'					Case 13
'						max_col=2
'					Case 14
'						max_col=3
'				End Select
'				intCol=0
'				response.write "document.write('<table width = ""400"" cellpadding = ""0"" cellspacing = ""0""><tr><td>');"
'				response.write "document.write('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong><font "& HCCon&">Top Destinations</font></strong>');"
'				response.write "document.write('</td></tr></table><br />');"
'				response.write "document.write('<table width = ""400"" cellpadding = ""3"" cellspacing = ""0"" "&BCon&"><tr>');"
'				
'				For intList=0 To Ubound(arrList,2)
'					IF intCol mod max_col=0 Then
'						response.write "document.write('</tr><tr>');"
'					End IF			
'					response.write "document.write('<td align = ""center"" width = ""175""><br><table cellpadding = ""0"" cellspacing = ""0""><tr><td align = ""center"" width = ""175"">');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&""" target=""_blank"">');"
'					response.write "document.write('<img src=""http://www.booking2hotels.com/images/dest_"&arrList(1,intList)&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""145"" height = ""110"" alt = """&arrList(1,intList)&""" /></a>');"
'					
'					response.write "document.write('<br>');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&""" target=""_blank"" title = """&arrList(1,intList)&""">');"
'					response.write "document.write('<strong><font "& LCCon&">"&arrList(1,intList)&"</font></strong></a></td></tr></table></td>');"
'					intCol=intCol+1
'				Next
'				response.write "document.write('</tr></table>');"
'			Case 15 'Layout 13		
'				response.write "document.write('<table width = ""400"" cellpadding = ""3"" cellspacing = ""0"" "&BCon&">');"
'				response.write "document.write('<tr><td align = ""center"" valign = ""top"" width = ""10"" "& HBCon&">');"
'				response.write "document.write('<img src=""http://www.booking2hotels.com/images/blue_bullet.gif"" border = ""0"" />');"
'				response.write "document.write('</td>');"
'				response.write "document.write('<td align = ""left"" valign = ""top"" "& HBCon&"><strong><font "& HCCon&">Pupular Destinations</font></strong></td></tr>');"
'		
'				For intList=0 To Ubound(arrList,2)			
'					response.write "document.write('<tr><td align = ""center"" valign = ""top"">');"
'					response.write "document.write('<img src=""http://www.booking2hotels.com/images/blue_bullet.gif"" border = ""0"" /></td>');"
'					response.write "document.write('<td align = ""left"" valign = ""top"" style = ""line-height : 20px"">');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&arrList(1,intList)&""">');"
'					response.write "document.write('<strong><font "&LCCon&">"&arrList(1,intList)&"</font></strong></a><br />');"
'					response.write "document.write('"&replace(arrList(2,intList),"'","\'")&"');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank""><font "&LCCon&"><u>More</u></a>');"
'					response.write "document.write('</td></tr>');"
'				Next
'				response.write "document.write('</table>');"
'			Case 16,17 'Layout 14 15
'				Select Case intType
'					Case 16
'						max_col=2
'					Case 17
'						max_col=3
'				End Select
'				intCol=0
'				response.write "document.write('<table width = ""400"" cellpadding = ""0"" cellspacing = ""0""><tr><td>');"
'				response.write "document.write('<strong><font "& HCCon&">Popular Destinations</font></strong>');"
'				response.write "document.write('</td></tr></table><br />');"
'				
'				response.write "document.write('<table width = ""400"" cellpadding = ""3"" cellspacing = ""0""  "& BCon&"><tr>');"
'				For intList=0 To Ubound(arrList,2)
'					IF intCol mod max_col=0 Then
'						response.write "document.write('</tr><tr>');"
'					End IF
'					response.write "document.write('<td width = ""200"" align = ""left"" valign = ""top""><table width = ""100%""><tr><td align = ""center"">');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&arrList(1,intList)&""">');"
'					response.write "document.write('<strong><font "& LCCon&">"&arrList(1,intList)&"</font></strong></a><hr>');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&arrList(1,intList)&""">');"
'					response.write "document.write('<img src = ""http://www.booking2hotels.com/images/dest_"&arrList(1,intList)&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""165"" height = ""125"" alt = """&arrList(1,intList)&""" /></a><br>');"
'					response.write "document.write('</td></tr></tr><td style = ""line-height : 20px"">"&replace(arrList(2,intList),"'","\'")&"');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank""><font "&LCCon&"><u>More...</u></font></a>');"
'					response.write "document.write('</td></tr></table></td>');"
'					intCol=intCol+1
'				Next
'				response.write "document.write('</table>');"
'			Case 18 'Layout 16																		 
'				arrDest=split(request("id"),",")
'					
'				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
'				response.write "document.write('<tr>');"
'				response.write "document.write('<td align = ""left"">');"
'				response.write "document.write('<strong><font "& HCCon&">Pupular Destinations</font></strong>');"
'				response.write "document.write('</td>');"
'				response.write "document.write('</tr>');"
'				response.write "document.write('</table><br />');"
'		
'				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0""  "&BCon&">');"
'				response.write "document.write('<tr><td colspan = ""3"">&nbsp;</td></tr>');"
'				response.write "document.write('<tr>');"
'					
'				For intDest=0 to Ubound(arrDest)
'					IF intDest mod 3=0 Then
'						response.write "document.write('</tr><tr>');"
'					End IF
'						response.write "document.write('<td width = ""300"" align = ""center"" valign = ""top"">');"
'						response.write "document.write('<table width = ""90%"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
'						response.write "document.write('<tr><td align = ""left"">');"		
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&function_generate_hotel_link(arrDest(intDest),"",4)&""">');"				
'						response.write "document.write('<strong><font "& LCCon&">"&Replace(function_generate_hotel_link(arrDest(intDest),"",1),"-"," ")&"</font></strong></a></td></tr>');"
'		 
'						For intList=0 to Ubound(arrList,2)
'							IF arrList(0,intList)=int(arrDest(intDest)) Then
'								response.write "document.write('<tr><td align = ""left"">');"
'								response.write "document.write('<img src =""http://www.booking2hotels.com/images/bullet_orange.gif"" border = ""0""/>');"
'								response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&arrList(2,intList)&""">');"
'								response.write "document.write('<font "&LCCon&">"&arrList(2,intList)&"</font></a>');"
'								response.write "document.write('</td></tr>');"
'							End IF
'						Next 
'							
'						response.write "document.write('</table><br></td>');"
'				Next
'					response.write "document.write('</table>');"
'			Case 19 'Layout 17	
'				
'				arrDest=split(request("id"),",")
'				
'				response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
'				response.write "document.write('<tr>');"
'				response.write "document.write('<td align = ""left"">');"
'				response.write "document.write('<strong><font "& HCCon&"><strong>Hot Destinations</font></strong>');"
'				response.write "document.write('</td>');"
'				response.write "document.write('</tr>');"
'				response.write "document.write('</table><br />');"
'				
'				response.write "document.write('<table width = ""600"" cellpadding = ""3"" cellspacing = ""0"" "&LCCon&">');"
'				response.write "document.write('<tr><td colspan = ""2"">&nbsp;</td></tr>');"
'		
'				response.write "document.write('<tr>');"
'				For intDest=0 to Ubound(arrDest)
'					IF intDest mod 2=0 Then
'						response.write "document.write('</tr><tr>');"
'					End IF
'						response.write "document.write('<td width = ""50%"" align = ""center"" valign = ""top"">');"
'						response.write "document.write('<table width = ""95%"" cellpadding = ""0"" cellspacing = ""0"" style ="" border-top : dotted 1px #cccccc"">');"
'						response.write "document.write('<tr><td height = ""25"" align = ""left"" valign = ""middle"">');"
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&function_generate_hotel_link(arrDest(intDest),"",4)&""">');"				
'						response.write "document.write('<strong><font "& LCCon&">"&Replace(function_generate_hotel_link(arrDest(intDest),"",1),"-"," ")&"</font></strong></a></td></tr>');"
'						For intList=0 to Ubound(arrList,2)
'							IF arrList(0,intList)=int(arrDest(intDest)) Then
'								response.write "document.write('<tr><td align = ""left"" valign = ""top"">');"
'								response.write "document.write('<img src = ""http://www.booking2hotels.com/images/blue_bullet.gif"" border = ""0"" /> ');"
'								response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&arrList(2,intList)&""">');"
'								response.write "document.write('<font "&LCCon&">"&arrList(2,intList)&"</font></a>');"
'								response.write "document.write('</td></tr>');"
'							End IF
'						Next
'					response.write "document.write('<tr>');"
'					response.write "document.write('<td height = ""25"" valign = ""middle"" align = ""right"">');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&function_generate_hotel_link(arrDest(intDest),"",4)&""">');"
'					response.write "document.write('<strong><font color = ""#7F7F7F"">More Hotels in "&function_generate_hotel_link(arrDest(intDest),"",4)&"...</font></strong></a>');"
'					response.write "document.write('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>');"
'					response.write "document.write('</tr>');"
'					response.write "document.write('</table><br></td>');"		
'				Next
'					response.write "document.write('</table>');"
'			Case 20 'Layout 19		
'				arrDest=split(request("id"),",")
'				
'				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"">');"
'				response.write "document.write('<tr><td><strong><font "& HCCon&">Popular Destination</font></strong></td></tr>');"
'				response.write "document.write('</table><br />');"
'				
'				response.write "document.write('<table width = ""600"" cellpadding = ""3"" cellspacing = ""0"" >');"
'				For intDest=0 to Ubound(arrDest)
'					response.write "document.write('<tr><td style = ""border-bottom : dashed 2px #CCCCCC"" width = ""120"" height = ""100"" align = ""center"" valign = ""middle"">');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrDest(intDest),1)&".asp"" target=""_blank"">');"
'					response.write "document.write('<img src = ""http://www.booking2hotels.com/images/dest_"&function_generate_hotel_link(arrDest(intDest),"",4)&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""112"" height = ""85"" alt = """&function_generate_hotel_link(arrDest(intDest),"",4)&""" />');"
'					response.write "document.write('</a></td>');"
'					response.write "document.write('<td style = ""border-bottom : dashed 1px #CCCCCC"" align = ""left"" valign = ""middle"">');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrDest(intDest),1)&".asp"" target=""_blank"" title = """&function_generate_hotel_link(arrDest(intDest),"",4)&""">');"
'					response.write "document.write('<strong><font "&LCCon&">"&function_generate_hotel_link(arrDest(intDest),"",4)&"</font></strong></a><br />');"
'					intCount=1
'					For intList=0 to Ubound(arrList,2)
'						IF arrList(0,intList)=int(arrDest(intDest)) Then
'							IF intCount<>1 Then
'								response.write "document.write(' | ');"
'							End IF					
'							response.write "document.write('"&function_generate_hotel_link(arrDest(intDest),arrList(1,intList),3)&"');"
'							intCount=intCount+1
'						End IF
'					Next
'					response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""100%""><tr>');"
'					response.write "document.write('<td align = ""right""><br />');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrDest(intDest),1)&".asp"" target=""_blank"" title = """&function_generate_hotel_link(arrDest(intDest),"",4)&""">');"
'					response.write "document.write('<strong><font color = ""#7F7F7F"">View All Hotels in "&function_generate_hotel_link(arrDest(intDest),"",4)&">></font></strong></a> &nbsp;&nbsp;');"
'		
'					response.write "document.write('</td></tr></table></td></tr>');"
'				Next
'				response.write "document.write('</table>');"
'			Case 21 'Layout 20
'				intCount=1
'				
'				response.write "document.write('<table width = ""350"" cellpadding = ""0"" cellspacing = ""0""  "&BCon&">');"
'				response.write "document.write('<tr>');"
'				response.write "document.write('<td align = ""center""><br />');"
'				
'				response.write "document.write('<table cellpadding = ""3"" cellspacing = ""0"" border = ""0"" width = ""310"">');"
'				response.write "document.write('<tr>');"
'				response.write "document.write('<td align = ""left"" colspan = ""2"">');"
'				response.write "document.write('<strong><font "& HCCon&">Top Destinations</font></strong>');"
'				response.write "document.write('<br /><br /></td>');"
'				response.write "document.write('</tr>');"
'			
'				For intList=0 to Ubound(arrList,2)
'					response.write "document.write('<tr><td align = ""left"" >');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = "&function_generate_hotel_link(arrList(0,intList),"",4)&">');"
'					response.write "document.write('<strong><font "&LCCon&">"&intCount&". <u>"&function_generate_hotel_link(arrList(0,intList),"",4)&"</u></font></strong></a>');"
'					response.write "document.write('</td><td align = ""right"" >');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = "&function_generate_hotel_link(arrList(0,intList),"",4)&">');"
'					response.write "document.write('<font "&LCCon&">From <strong>"&FormatNumber(arrList(2,intList)/32.130001,0)&" USD</strong></font></a>');"
'					response.write "document.write('</td></tr>');"                            
'		
'					intCount=intCount+1
'				Next
'				response.write "document.write('</table><br />');"
'				response.write "document.write('</td></tr></table>');"
'			Case 22 'Layout 21
'				Dim sqlHotel
'				Dim rsHotel
'				
'					response.write "document.write('<table width = ""640"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
'					response.write "document.write('<tr>');"
'					response.write "document.write('<td align = ""left"">');"
'					response.write "document.write('<strong><font "& HCCon&"><strong>Top Destinations for Hotels</strong></font></strong>');"
'					response.write "document.write('</td>');"
'					response.write "document.write('</tr>');"
'					response.write "document.write('</table><br />');"
'					
'					response.write "document.write('<table width = ""640"" cellpadding = ""0"" cellspacing = ""0""  "& BCon&">');"
'					
'					For intList=0 to UBound(arrList,2)
'					sqlHotel="select top 3 product_id,title_en,files_name from tbl_product where status=1 and destination_id="&arrList(0,intList)
'					'response.write "document.write('"&sqlHotel&"<br>');"
'					Set rsHotel=server.CreateObject("adodb.recordset")
'					rsHotel.Open sqlHotel,conn,1,3
'					
'					response.write "document.write('<tr><td align = ""center"" valign = ""top""><br />');"	
'					response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
'					response.write "document.write('<tr><td valign = ""top"" align = ""left"" width = ""110"">');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"">');"			
'					response.write "document.write('<img src=""http://www.booking2hotels.com/images/dest_"&arrList(1,intList)&".jpg"" style = ""border : solid 1px #CCCCCC"" width = ""98"" height = ""74"" alt = "&function_generate_hotel_link(arrList(0,intList),"",4)&" /></a></td>');"
'					response.write "document.write('<td valign = ""middle"" align = ""left"">');"
'					response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
'					response.write "document.write('<tr height = ""20""><td>');"
'		
'					
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = """&function_generate_hotel_link(arrList(0,intList),"",4)&""">');"
'					response.write "document.write('<strong><font "&LCCon&">"&arrList(1,intList)&"</font></strong></a>');"
'					response.write "document.write('</td></tr>');"
'		
'					Do while not rsHotel.Eof
'						response.write "document.write('<tr height = ""20"">');"
'						response.write "document.write('<td valign = ""top"">');"
'						response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout24_arrow07.gif"" border = ""0""/> ');"
'						response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&"/"&rsHotel("files_name")&""" target=""_blank"" title = """&rsHotel("title_en")&"""> ');"
'						response.write "document.write('<font "&LCCon&">"&rsHotel("title_en")&"</font></a></td></tr>');"
'						rsHotel.movenext
'					Loop
'					response.write "document.write('<tr><td align = ""center"">');"
'					response.write "document.write('<table width = ""500"" cellpadding = ""0"" cellspacing = ""0"" border = ""0"">');"
'					response.write "document.write('<tr>');"
'					response.write "document.write('<td align = ""right"" style ="" border-bottom : solid 1px #CCCCCC"">');"
'					response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_pro_cate(strCate,arrList(0,intList),1)&".asp"" target=""_blank"" title = "&function_generate_hotel_link(arrList(0,intList),"",4)&">');"
'					response.write "document.write('<strong><font color = ""#7F7F7F"">View all Hotels in "&arrList(1,intList)&" >></font></strong></a>');"
'					response.write "document.write('&nbsp;&nbsp;<br /><br />');"
'					response.write "document.write('</td>');"
'					response.write "document.write('</tr>');"
'					response.write "document.write('</table></td></tr>');"
'								
'					rsHotel.close
'					Set rsHotel=Nothing
'					response.write "document.write('</table></td></tr></table></td></tr>');"
'				Next
'				response.write "document.write('</table>');"
'			Case 23 'Layout 18
'				
'				Dim sqlLocation
'				Dim rsLocation
'				Dim arrLocation
'				Dim intLocation
'				Dim intLocation_temp
'				Dim intDest_temp
'				Dim sqlHotelLocation
'				Dim rsHotelLocation
'				Dim arrHotelLocation
'				Dim intHotelLocation
'				
'				sqlList="select destination_id,title_en,short_detail from tbl_destination where destination_id IN (32,35,36,37,38)"
'				
'				sqlLocation="select d.destination_id,d.title_en,l.location_id,l.title_en"
'				sqlLocation=sqlLocation&" from tbl_location l,tbl_destination d"
'				sqlLocation=sqlLocation&" where l.destination_id=d.destination_id and d.destination_id IN (32,35,36,37,38) and l.status=1"
'				sqlLocation=sqlLocation&" and "
'				sqlLocation=sqlLocation&" (select count(sp.product_id)"
'				sqlLocation=sqlLocation&" from tbl_product sp,tbl_product_location spl"
'				sqlLocation=sqlLocation&" where sp.product_id=spl.product_id and sp.status=1 and spl.location_id=l.location_id"
'				sqlLocation=sqlLocation&" )<>0"
'				sqlLocation=sqlLocation&" order by d.title_en asc"
'				
'				Set rsLocation=server.CreateObject("adodb.recordset")
'				rsLocation.open sqlLocation,conn,1,3
'				arrLocation=rsLocation.getRows()
'				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0""><tr><td>');"
'				response.write "document.write('<font color = ""red""><strong>Hot Destinations</strong></font>');"
'				response.write "document.write('</td></tr></table><br />');"
'			
'				response.write "document.write('<table width = ""600"" cellpadding = ""0"" cellspacing = ""0"" style = ""border-bottom : solid 3px #CCCCCC"">');"
'				For intLocation=0 to Ubound(arrLocation,2)
'					sqlHotelLocation="select top 5 p.title_en,p.files_name"
'					sqlHotelLocation=sqlHotelLocation&" from tbl_product_location pl,tbl_product p"
'					sqlHotelLocation=sqlHotelLocation&" where pl.product_id=p.product_id and pl.location_id="&arrLocation(2,intLocation)&" and p.status=1"
'					Set rsHotelLocation=server.CreateObject("adodb.recordset")
'					rsHotelLocation.Open sqlHotelLocation,conn,1,3
'					arrHotelLocation=rsHotelLocation.getRows()
'					IF arrLocation(0,intLocation)<>intDest_temp then
'						response.write "document.write('<tr><td align = ""left"" height = ""20"" "& HBCon&" colspan = ""2"">');"
'						response.write "document.write('&nbsp;&nbsp;<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrLocation(0,intLocation),"",1)&".asp"" target=""_blank"" title = """&arrLocation(1,intLocation)&""">');"
'						response.write "document.write('<strong><font "& HCCon&">"&arrLocation(1,intLocation)&"</font></strong></a></td></tr>');"
'						response.write "document.write('<tr><td colspan = ""2"">&nbsp;</td></tr>');"	
'										
'						response.write "document.write('<tr><td valign = ""top"" align = ""left"">');"
'						'response.write "document.write('<strong><a href="http://www.hotels2thailand.com/bagkok-chatuchak-hotels.asp" target="_blank" title = "Bangkok Chatuchak Market (JJ Market )">');"
'						response.write "document.write('<font "&LCCon&"><u>"&arrLocation(3,intLocation)&"</u></font></strong></td>');"				
'						response.write "document.write('<td align = ""left"" valign = ""top"" style = ""line-height : 20px"">');"
'						
'						For intHotelLocation=0 to Ubound(arrHotelLocation,2)
'							'response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Sena-Place-Hotel.asp" target="_blank" title = "Sena Place Hotel Bangkok">');"
'							response.write "document.write('<font "&LCCon&"><u>"&replace(arrHotelLocation(0,intHotelLocation),"'","\'")&"</u></font>');"		
'							if intHotelLocation<>Ubound(arrHotelLocation,2) then
'								response.write "document.write(' , ');"
'							end IF
'						Next
'						response.write "document.write(',<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrLocation(0,intLocation),"",1)&".asp"" target=""_blank"" title = """&arrLocation(1,intLocation)&""">');"
'						response.write "document.write('<font color = ""#7F7F7F""> More...</font></a>');"
'						response.write "document.write('</td></tr>');"
'						response.write "document.write('<tr><td colspan = ""2"">&nbsp;</td></tr>');"
'					Else
'						response.write "document.write('<tr><td valign = ""top"" align = ""left"">');"
'						'response.write "document.write('<strong><a href="http://www.hotels2thailand.com/bagkok-chatuchak-hotels.asp" target="_blank" title = "Bangkok Chatuchak Market (JJ Market )">');"
'						response.write "document.write('<font "&LCCon&"><u>"&arrLocation(3,intLocation)&"</u></font></strong></td>');"				
'						response.write "document.write('<td align = ""left"" valign = ""top"" style = ""line-height : 20px"">');"
'						
'						For intHotelLocation=0 to Ubound(arrHotelLocation,2)
'							'response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Sena-Place-Hotel.asp" target="_blank" title = "Sena Place Hotel Bangkok">');"
'							response.write "document.write('<font "&LCCon&"><u>"&replace(arrHotelLocation(0,intHotelLocation),"'","\'")&"</u></font>');"		
'							if intHotelLocation<>Ubound(arrHotelLocation,2) then
'								response.write "document.write(' , ');"
'							end IF
'						Next
'						response.write "document.write(',<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrLocation(0,intLocation),"",1)&".asp"" target=""_blank"" title = """&arrLocation(1,intLocation)&""">');"
'						response.write "document.write('<font color = ""#7F7F7F""> More...</font></a>');"
'						response.write "document.write('</td></tr>');"
'						response.write "document.write('<tr><td colspan = ""2"">&nbsp;</td></tr>');"
'					End IF
'					intDest_temp=arrLocation(0,intLocation)
'					rsHotelLocation.close()
'					Set rsHotelLocation=Nothing
'				Next
'				response.write "document.write('</table>');"
			End Select 	'	End intType
		Case 2	' 	one destination on promotion
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
						response.write "document.write('<font "& HCCon&"><strong>Hotels Promotion in "&function_generate_hotel_link(arrList(6,intList),"",4)&"</strong></font>');"
						response.write "document.write('</td></tr></table>');"
						
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""10"" border = ""0"" width = ""550""><tr>');"
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
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\")&""">');"
							response.write "document.write('<font "& LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font></a>');"');"
							response.write "document.write('</strong>');"
							
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr height = ""5""><td colspan = ""2"">');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td colspan = ""2"" align = ""center"" valign = ""middle"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/thailand-hotels-pic/"&arrList(7,intList)&"_a.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""226"" height = ""190"" />');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td width = ""146"" align = ""left"" valign = ""top"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/promotion01.gif"" border = ""0"" alt = """&replace(arrList(8,intList),"'","\'")&"""/>&nbsp;"&replace(arrList(8,intList),"'","\'")&"');"
							response.write "document.write('</td><td align = ""right"" valign = ""top"">');"
							response.write "document.write('<font "&PCCon&">From<br />"&FormatNumber((arrList(3,intList)/arrList2(1,0))*intVatFactor,0)&"&nbsp;"&arrList2(2,0)&"</font>');"
							response.write "document.write('</td></tr></table>');"
							response.write "document.write('</td>');"
							intCol=intCol+1
						Next
						response.write "document.write('</tr></table>');"		
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
						response.write "document.write('<strong><font "& HCCon&">Hot Hotels in "&function_generate_hotel_link(arrList(6,intList),"",4)&"</font></strong>');"
						response.write "document.write('</td></tr>');"

 						For intList=0 To Ubound(arrList,2)	
							IF intCol mod max_col=0 Then
								response.write "document.write('</tr><tr>');"
							End IF
							response.write "document.write('<td valign = ""top"">');"	
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""97"">');"
							response.write "document.write('<tr><td valign = ""top"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/thailand-hotels-pic/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\")&""" width = ""90"" height = ""95"" /></a>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td align = ""center"" valign = ""top"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\")&""">');"
							response.write "document.write('<font "&LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font></a></td>');"
							response.write "document.write('</tr></table>');"
							response.write "document.write('</td>');"
							intCol=intCol+1					
						Next
						response.write "document.write('</tr></table>');"		
					Case 5
						response.write "document.write('<table width = ""400"" cellpadding = ""5"" cellspacing = ""0"" "&BCon&">');"
						response.write "document.write('<tr><td align = ""center""><strong><font "& HCCon&"> "&function_generate_hotel_link(arrList(6,intList),"",4)&" Hotels Promtion</font></strong>');"
						response.write "document.write('</td></tr>');"
					
						For intList = 0 to Ubound(arrList,2)
							response.write "document.write('<tr><td align = ""center""><table width = ""100%"" cellpadding = ""3"" cellspacing = ""0""><tr><td colspan = ""2"" align = ""left"">');"
							response.write "document.write('<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\")&""">');"
							response.write "document.write('<font  "&LCCon&">"&replace(arrList(0,intList),"'","\'")&"</font></a>');"
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
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/thailand-hotels-pic/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""90"" height = ""95"" />');"
							response.write "document.write('</a><br />');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('<a href = ""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target = ""_blank"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/aff_map.gif"" border = ""0"" />');"
							response.write "document.write('</a></td>');"
							response.write "document.write('<td>');"
							response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(9,intList)&""" target = ""_blank""><font color = ""#FF5D01"">View Map</font>');"
							response.write "document.write('</a></td></tr></table></td>');"
							response.write "document.write('<td align = ""left"" valign = ""top"" >');"
							response.write "document.write('<strong>');"
							response.write "document.write('<font color = "">From "&FormatNumber((arrList(3,intList)/arrList2(1,0))*intVatFactor,0)&"&nbsp;"&arrList2(2,0)&"</font>');"
							response.write "document.write('</strong><br />');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/promotion01.gif"" border = ""0"" alt = """&replace(arrList(8,intList),"'","\'")&"""/>&nbsp;"&replace(arrList(8,intList),"'","\'")&"<br />');"
							response.write "document.write('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"&Mid(arrList3(0,0),1,150)&"');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"">');"
							response.write "document.write('<font "&LCCon&"><u> More Detail</u></font>');"
							response.write "document.write('</a><br />');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >');"
							response.write "document.write('<tr><td colspan = ""2"">');"
							response.write "document.write('<font color = ""green"">Average User Rating : </font>');"
							if (arrList(10,intList) <> 0) then
								num1 = (arrList(11,intList)/arrList(10,intList))
								num2 = int(arrList(11,intList)/arrList(10,intList))
								select case FormatNumber(num1 - num2,1)
									case 0.0
										strRate = FormatNumber(num1,0)
									case 0.1,0.2,0.3,0.4
										strRate = FormatNumber(num1,0) + 0.4
									case 0.5
										strRate = FormatNumber(num1,0) + 0.5
									case 0.6,0.7,0.8,0.9
										strRate = FormatNumber(num1,0) + 0.6
								end select
							end if
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td valign = ""bottom"" height = ""22"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/rating_"&strRate&".gif"" border = ""0""/>');"
							response.write "document.write('</td>');"
							response.write "document.write('<td>');"
							response.write "document.write('<font color = ""green""> ("&strRate&" From 5.0)</font> <a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(9,intList)&""" target=""_blank""><u>(Review)</u></a><br />');"
							response.write "document.write('</td></tr></table></td></tr></table><br />');"
							
							response.write "document.write('</td></tr>');"
						Next
						response.write "document.write('</table>');"		
					Case 6
						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width  = ""600"">');"
						response.write "document.write('<tr><td align = ""center"" colspan = ""2"">');"
						response.write "document.write('<strong><font "& HCCon&">"&function_generate_hotel_link(arrList(6,intList),"",4)&" Promtion</font></strong></td>');"
						response.write "document.write('</tr></table><br />');"
						For intList = 0 to Ubound(arrList,2)
							response.write "document.write('<table cellpadding =""0"" cellspacing = ""5"" border = ""0"" width  = ""600""  "&BCon&">');"
							response.write "document.write('<tr><td width = ""125"" align = ""left"" valign = ""top"">');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"">');"
							response.write "document.write('<img src = ""http://www.hotels2thailand.com/thailand-hotels-pic/"&arrList(7,intList)&"_a.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""120"" height = ""80"" /></a>');"
							response.write "document.write('<br /><table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" ><tr><td>');"
							response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(9,intList)&"&"" target = ""_blank"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/aff_map.gif"" border = ""0"" /></a>');"
							response.write "document.write('</td><td>');"
							response.write "document.write('<a href = ""http://www.hotels2thailand.com/thailand-hotels-map.asp?id="&arrList(9,intList)&"&"" target = ""_blank"">');"
							response.write "document.write('<font color = ""#FF5D01"">View Map</font>');"
							response.write "document.write('</a></td></tr></table></td>');"
							response.write "document.write('<td align = ""left"" valign = ""top"">');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""100%"">');"
							response.write "document.write('<tr><td>');"
							response.write "document.write('<strong>');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
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
							response.write "document.write('<strong><font>From "&FormatNumber((arrList(3,intList)/arrList2(1,0))*intVatFactor,0)&"&nbsp;"&arrList2(2,0)&"</font></strong>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td colspan = ""2"">');"
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/promotion01.gif"" border = ""0"" alt = """&replace(arrList(8,intList),"'","\'")&"""/>&nbsp;<font color = ""#FD6500"">"&replace(arrList(8,intList),"'","\'")&"</font>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td colspan = ""2"">');"
							response.write "document.write('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"&Mid(arrList3(0,0),1,150)&"');"
							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank""><font "&LCCon&"><u> More Detail</u></font></a>');"
							response.write "document.write('</td></tr>');"
							response.write "document.write('<tr><td colspan = ""2"">');"
							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" >');"
							response.write "document.write('<tr><td>Average User Rating : </td>');"
							response.write "document.write('<td valign = ""bottom"" height = ""22"">');"
							if (arrList(10,intList) <> 0) then
								num1 = FormatNumber(arrList(11,intList)/arrList(10,intList),3)
								num2 = int(arrList(11,intList)/arrList(10,intList))
								'response.write "document.write('num1="&FormatNumber(num1-num2,1)&"');"	
								select case FormatNumber(num1 - num2,1)
									case 0.0
										strRate = FormatNumber(num1,0)
									case 0.1,0.2,0.3,0.4
										strRate = FormatNumber(num1,0) + 0.4
									case 0.5
										strRate = FormatNumber(num1,0) + 0.5
									case 0.6,0.7,0.8,0.9
										strRate = FormatNumber(num1,0) + 0.6
								end select
								if FormatNumber(num1 - num2,1) = 0.0  then
								 	rCon = int(arrList(11,intList)/arrList(10,intList))
								 else
								 	rCon = FormatNumber(arrList(11,intList)/arrList(10,intList),1)
								 end if
							end if
							response.write "document.write('<img src = ""http://www.booking2hotels.com/images/rating_"&strRate&".gif"" border = ""0""/>');"
							response.write "document.write('</td>');"
							response.write "document.write('<td><font color =""green""> ("&rCon&" From 5.0)</font><a href = ""http://www.hotels2thailand.com/review.asp?id="&arrList(9,intList)&""" target=""_blank""><u>(Review)</u><br /></td>');"
							
							response.write "document.write('</tr></table>	');"					
							response.write "document.write('</td></tr></table>');"
							response.write "document.write('</td></tr></table><br />');"
						Next
				End Select 	'	End intType
				'Case 3
			Case 3	' 	one destination
'				Select Case intType
'					Case 1
						response.write "document.write('ok');"
						response.write "document.write('"&show&"');"
		response.end
'						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""0"" border = ""0"" width = ""300"">');"
'						response.write "document.write('<tr height = ""25"">');"
'						response.write "document.write('<td align = ""left"" valign = ""top"">');"
'						response.write "document.write('<font "& HCCon&"><strong>"&cateCon&" in "&arrList(5,0)&"</strong></font>');"
'						response.write "document.write('</td></tr></table>');"
'						For intList = 0 to Ubound(arrList,2)
'							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""3"" width = ""310"" "& BCon&">');"
'							response.write "document.write('<tr><td align = ""left"" valign = ""top"">');"
'							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(11,intList),"",1)&"/"&replace(arrList(6,intList),"'","\'")&""" target=""_blank"" title = """&replace(arrList(0,intList),"'","\'")&""">');"
'							response.write "document.write('<font "& LCCon&">"""&replace(arrList(0,intList),"'","\'")&"""</font></a>');"
'							For intStar = 1 to (arrList(2,intList) +0.5)
'								if ((arrList(3,intList) - (arrList(3,intList) mod 10)) <> 0 and intStar = (arrList(3,intList) +0.5)) then 
'									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout1_star_half.gif"" border = ""0"" />');"
'								else
'									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout1_star.gif"" border = ""0"" />');"
'								end if
'							Next							 
'							response.write "document.write('</td><td width = ""55"" align = ""right"" valign = ""top"">');"
'							response.write "document.write('<font color = ""#7FFFD4"">From<br />');"
'							response.write "document.write('"&arrList2(2,0)&"&nbsp;"&FormatNumber(arrList(4,intList)/int(arrList2(1,0)),0)&"');"
'							response.write "document.write('</font></td></tr></table>');"
'						Next
'					Case 2
'						response.write "document.write('case3.1');"
'						response.write "document.write('<table height = ""25"" cellpadding = ""0"" cellspacing = ""0"" width = ""200"" "& HBCon&" "& BCon&">');"
'						response.write "document.write('<tr><td align = ""center"">');"
'						response.write "document.write('<strong><font "& HCCon&">"&cateCon&" in "&arrList(5,0)&"</font></strong>');"
'						response.write "document.write('</td></tr></table>');"
'						
'						response.write "document.write('<table cellpadding = ""0"" cellspacing = ""5"" width = ""200"" "& BCon&">');"
'						For intList = 0 to Ubound(arrList2)
'							response.write "document.write('<tr><td valign = ""top"">');"
'							response.write "document.write('<table cellpadding = ""0"" cellspacing = ""5"" width = ""100%"" style =""border-bottom : dashed 1px #A4B7DF"">');"
'							response.write "document.write('<tr><td width = ""20%"" align = ""center"">');"
'							response.write "document.write('<a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(11,intList),"",1)&"/"&replace(arrList(6,intList),"'","\'")&""" target=""_blank"">');"
'							response.write "document.write('<img src = ""http://www.hotels2thailand.com/thailand-hotels-pic/"&arrList(7,intList)&"_1.jpg"" border = ""0"" alt = """&replace(arrList(0,intList),"'","\'")&""" width = ""50"" height = ""53"" /></a>');"
'							For intStar = 1 to (arrList(2,intList) +0.5)
'								if ((arrList(2,intList) - (arrList(2,intList) mod 10)) <> 0 and intStar = (arrList(2,intList) +0.5)) then 
'									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout3_red_star_half.gif"" border = ""0"" />');"
'								else
'									response.write "document.write('<img src = ""http://www.booking2hotels.com/images/layout3_red_star.gif"" border = ""0"" />');"
'								end if
'							Next							 
'							response.write "document.write('</td>');"
'							response.write "document.write('<td align = ""center"" valign = ""top"">');"
'							response.write "document.write('<strong><a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"" title =  """&replace(arrList(0,intList),"'","\'")&"""><font "&LCCon&"><u>"&replace(arrList(0,intList),"'","\'")&"</u></font></a></strong><br />');"
'							response.write "document.write('<font color = ""#FF1493"">Price from "&arrList2(2,0)&"&nbsp;"&FormatNumber(arrList(3,intList)/int(arrList2(1,0)),0)&"</font>');"
'							response.write "document.write('</td></tr></table></td></tr>');"
'						Next
'						response.write "document.write('</table>');"
'					Case 3,4,5,6
						'response.write "document.write('case3.1');"
'						Select Case intType
'							Case 3
'								max_col=1
'							Case 4
'								max_col=2
'							Case 4
'								max_col=3
'							Case 4
'								max_col=4
'						End Select
'						intCol=0
'						response.write "document.write('<table cellpadding = ""4"" cellspacing = ""0"" width = ""200""  "& BCon&">');"
'						response.write "document.write('<tr><td align = ""center"" "& HBCon&">');"
'						response.write "document.write('<strong><font "& HCCon&">"&function_generate_hotel_ink(arrList(6,intList),"",4)&" Hotels</font></strong>');"
'						response.write "document.write('</td></tr>');"	
'						For intList = 0 to Ubound(arrList,2)
'							IF intCol mod max_col=0 Then
'								response.write "document.write('</tr><tr>');"
'							End IF
'							response.write "document.write('<tr><td align = ""center"" valign = ""top"">');"
'							response.write "document.write('<strong><a href=""http://www.hotels2thailand.com/"&function_generate_hotel_link(arrList(6,intList),"",1)&"/"&replace(arrList(5,intList),"'","\'")&""" target=""_blank"" title = ""Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"">');"
'							response.write "document.write('<font color="#5F9EA0">Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)</font></a></strong>');"
'							response.write "document.write('<br /><img src = "http://www.booking2hotels.com/images/layout4_star.gif" border = "0" />');"
'							For intStar = 1 to (arrList(2,intList) +0.5)
'								if ((arrList(2,intList) - (arrList(2,intList) mod 10)) <> 0 and intStar = (arrList(2,intList) +0.5)) then 
'									response.write "document.write('<img src = "http://www.booking2hotels.com/images/layout4_star.gif" border = "0" />');"
'								else
'									response.write "document.write('<img src = "http://www.booking2hotels.com/images/layout4_star.gif" border = "0" />');"
'								end if
'							Next							 							
'							response.write "document.write('<br /><font color = "#FF1493"><u>From 34 USD</u></font>');"
'							response.write "document.write('</td></tr>');"
'						Next
'						response.write "document.write('</table>');"			
'					Case 7
						'response.write "document.write('case3.1');"
'						response.write "document.write('<table cellpadding = "0" cellspacing = "0" width = "400"  style = " border : solid 1px #B8860B">');"
'						response.write "document.write('<tr><td height = "25" align = "left" bgcolor = "#006400">');"
'						response.write "document.write('<font color = "#5F9EA0"><strong>&nbsp;&nbsp;Hotels in Bangkok</strong></font></td>');"
'						response.write "document.write('</tr>');"
'						For intList = 0 to Ubound(arrList,2)
'							response.write "document.write('<tr><td align = "center">');"
'							response.write "document.write('<table cellpadding = "3" cellspacing = "0" border = "0" width = "400">');"
'							response.write "document.write('<tr><td align = "left" valign = "top">');"
'							response.write "document.write('<strong>&nbsp;2.</strong></td>');"
'							response.write "document.write('<td align = "left">');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" width = "100%">');"
'							response.write "document.write('<tr><td>');"
'							response.write "document.write('<strong><a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank" title = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"><font color="#7FFFD4">Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)</font></a></strong></td>');"
'							response.write "document.write('</tr>');"
'							response.write "document.write('<tr><td>');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" >');"
'							response.write "document.write('<tr><td>');"
'							response.write "document.write('Average User Rating :</td>');"
'							response.write "document.write('<td valign = "bottom" height = "22">');"
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/rating_3.4.gif" border = "0"/></td>');"
'							response.write "document.write('<td>');"
'							response.write "document.write('<font color = "green"> (3.1 From 5.0)</font> <a href = "http://www.hotels2thailand.com/review.asp?id=154" target="_blank"><u>(Review)</u></a><br /></td>');"
'							response.write "document.write('</tr></table></td></tr></table>');"
'							response.write "document.write('Hotel Class : <img src = "http://www.booking2hotels.com/images/layout5_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout5_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout5_star.gif" border = "0" /><br />');"
'							response.write "document.write('<font color = "#FF7F50">Price from USD 34</font><br /><br /></td>');"
'							response.write "document.write('</tr></table></td></tr>');"
'						Next
'						response.write "document.write('</table>');"					
'					Case 8
						'response.write "document.write('case3.1');"
'						response.write "document.write('<table cellpadding = "0" cellspacing = "0" width = "500"  style = " border : solid 1px #B8860B">');"
'						response.write "document.write('<tr height = "25"><td align = "center"><strong><font color = "#5F9EA0">Bangkok Hotels</font></strong></td></tr>');"
'						For intList = 0 to Ubound(arrList,2)
'							response.write "document.write('<tr><td align = "center">');"
'							response.write "document.write('<table width = "480" cellpadding = "3" cellspacing = "0" border = "0">');"
'							response.write "document.write('<tr><td align = "left">');"
'							response.write "document.write('<table width = "480" cellpadding = "5" cellspacing = "0" border = "0" bgcolor = "#006400">');"
'							response.write "document.write('<tr><td>');"
'							response.write "document.write('<strong><a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank" title = "Admiral Suites Bangkok"><font color="#7FFFD4"><u>Admiral Suites Bangkok</u></font></a></strong>');" 
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/layout6_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout6_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout6_star.gif" border = "0" />');"
'							response.write "document.write('</td><td align = "right">');"
'							response.write "document.write('<font color = "#FF7F50"><strong>From USD 77</strong></font>');"
'							response.write "document.write('</td></tr></table></td></tr>');"
'							response.write "document.write('<tr><td align = "left">');"
'							response.write "document.write('<table width = "480" cellpadding = "0" cellspacing = "0" border = "0">');"
'							response.write "document.write('<tr><td width = "80" align = "left" valign = "top" style="height: 38px">');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" width = "100%">
'							response.write "document.write('<tr><td>');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank"><img src = "http://www.hotels2thailand.com/thailand-hotels-pic/HBK161_1.jpg" border = "0" alt = "Admiral Suites Bangkok" width = "70" height = "74" /></a>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td height = "25">');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" ><tr><td><a href = "http://www.hotels2thailand.com/thailand-hotels-map.asp?id=838&" target = "_blank"><img src = "http://www.booking2hotels.com/images/aff_map.gif" border = "0" /></a></td><td><a href = "http://www.hotels2thailand.com/thailand-hotels-map.asp?id=838&" target = "_blank"><font color = "#FF5D01">View Map</font></a></td></tr></table></td>');"
'							response.write "document.write('</tr></table></td>');"
'							response.write "document.write('<td valign = "top" style="height: 38px">');"
'							response.write "document.write('Sukhumvit, ');"
'							response.write "document.write('Bangkok<br />');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" >');"
'							response.write "document.write('<tr><td>');"
'							response.write "document.write('Average User Rating : ');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td valign = "bottom" height = "22">');"
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/rating_4.gif" border = "0"/>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td>');"
'							response.write "document.write('<font color = "green"> (4 From 5.0)</font> <a href = "http://www.hotels2thailand.com/review.asp?id=838" target="_blank"><u>(Review)</u></a><br />');"
'							response.write "document.write('</td>');"
'							response.write "document.write('</tr></table>');"
'							response.write "document.write('Admiral Suites serviced apartments are located in Bangkok (Sukhumvit Area) and features apartments ranging from studios to one bedroom units. Each apartment is luxuriously furnished with pre... <a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank"><font color="#7FFFD4"><u>read more</u></font></a>');"
'							response.write "document.write('</td></tr></table></td>');"
'							
'							response.write "document.write('</tr></table><br />');"		
'						Next
'						response.write "document.write('</td></tr></table>');"
'					Case 9
						'response.write "document.write('case3.1');"
'						response.write "document.write('<table width = "300" cellpadding = "4" cellspacing = "0" style ="border : solid 1px #B8860B">');"
'						response.write "document.write('<tr><td colspan = "2" bgcolor = "#006400" align = "left">');"
'						response.write "document.write('<strong><font color = "#5F9EA0">&nbsp;Bangkok Hotels</font></strong>');"
'						response.write "document.write('</td></tr></table>');"
'						response.write "document.write('<table width = "300" cellpadding = "0" cellspacing = "6" style ="border : solid 1px #B8860B">');"
'						For intList = 0 to Ubound(arrList,2) 
'							response.write "document.write('<tr><td align = "left">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank" title = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"><font color="#7FFFD4"><u>Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)</u></font></a>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td align = "right" valign = "top" width = "20%">');"
'							response.write "document.write('<font color = "#FF7F50">USD 34</font>');"
'							response.write "document.write('</td></tr>');"
'						Next
'						response.write "document.write('<tr><td colspan = "2" align = "left">');"
'						response.write "document.write('<br />Click <u><a href="http://www.hotels2thailand.com/thailand-hotels.asp" target="_blank"><font color = "red">here</font></a></u> for more information');"
'						response.write "document.write('<br /><br /></td>');"
'						response.write "document.write('</tr></table>');"
'					Case 10
						'response.write "document.write('case3.1');"
'						response.write "document.write('<table cellpadding = "0" cellspacing = "0" width = "300"  style = " border : solid 1px #B8860B">');"
'						response.write "document.write('<tr height = "25">');"
'						response.write "document.write('<td colspan = "2" align = "left">');"
'						response.write "document.write('<strong><font color = "#5F9EA0">&nbsp;More Great Offers</font></strong>');"
'						response.write "document.write('</td></tr>');"
'						For
'							response.write "document.write('<tr><td align = "center" width = "15" valign = "top">');"
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/blue_bullet.gif" border = "0" />');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td align = "left">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank" title = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"><font color="#7FFFD4">Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)</font></a>');"
'							response.write "document.write('</td></tr>');"
'						Next
'						response.write "document.write('<tr><td colspan = "2">&nbsp;</td></tr>');"
'						response.write "document.write('</table>');"
'					Case 11
						'response.write "document.write('case3.1');"
'						response.write "document.write('<table cellspacing = "0" cellpadding = "0" border = "0">');"
'						response.write "document.write('<tr height = "25"><td><strong><font color = "#5F9EA0">Bangkok Hotels</font></strong></td></tr>');"
'						response.write "document.write('</table>');"
'						For
'							response.write "document.write('<table border = "0" cellpadding = "7" cellspacing = "0"  style = " border : solid 1px #B8860B" width = "410">');"
'							response.write "document.write('<tr><td align = "left" style =" border-bottom : dashed 1px #A1BFCD">');"
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/layout11_star_yellow.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout11_star_yellow.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout11_star_yellow.gif" border = "0" />');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td align = "left" style =" border-bottom : dashed 1px #A1BFCD">');"
'							response.write "document.write('<strong><a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank" title = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"><font color="#7FFFD4"><u>Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)</u></font></a></strong>');"
'							response.write "document.write('</td></tr>');"
'							response.write "document.write('<tr><td align = "center" valign = "middle" width = "5%">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank"><img src = "http://www.hotels2thailand.com/thailand-hotels-pic/HBK014_1.jpg" border = "0" alt = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)" width = "70" height = "74"  style = " border : solid 1px #B8860B" /></a>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td align = "left">');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" width = "100%">');"
'							response.write "document.write('<tr><td valign = "top">');"
'							response.write "document.write('<strong>Don Muang Airport</strong><br />');"
'							response.write "document.write('Airport Suites Hotel Bangkok (Comfort Suites Airport) has 120 well appointed, air conditioned rooms. All are comfortably sound proofed and protected b...  <u><a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank"><font color = "#A0110F"><u>More</u></font></a></u>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td valign = "middle">');"
'							response.write "document.write('<table width = "95" cellpadding = "0" cellspacing = "0"  style = " border : solid 1px #B8860B">');"
'							response.write "document.write('<tr><td align = "left" valign = "middle">');"
'							response.write "document.write('<br />&nbsp;From <font color = "#FF7F50"><strong>USD 34</strong></font>');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" ><tr><td><a href = "http://www.hotels2thailand.com/thailand-hotels-map.asp?id=154&" target = "_blank"><img src = "http://www.booking2hotels.com/images/aff_map.gif" border = "0" /></a></td><td><a href = "http://www.hotels2thailand.com/thailand-hotels-map.asp?id=154&" target = "_blank"><font color = "green">View Map</font></a></td></tr></table><br /></td>');"
'							response.write "document.write('</tr></table></td></tr></table></td></tr>');"
'							response.write "document.write('<tr><td align = "left" colspan = "2">');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" >');"
'							response.write "document.write('<tr><td>');"
'							response.write "document.write('<font color = "#858585">Average User Rating</font> : ');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td valign = "bottom" height = "22">');"
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/rating_3.4.gif" border = "0"/>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td>');"
'							response.write "document.write('<font color = "green"> (3.1 From 5.0)</font> <a href = "http://www.hotels2thailand.com/review.asp?id=154" target="_blank"><u>(Review)</u></a><br />');"
'							response.write "document.write('</td></tr></table></td></tr></table><br />');"
'						Next
'					Case 12
						'response.write "document.write('case3.1');"
'						response.write "document.write('<table width = "300" cellpadding = "3" cellspacing = "0">');"
'						response.write "document.write('<tr height = "25"><td align = "center"><font color = "#5F9EA0"><strong>Bangkok Hotels</strong></font></td></tr>');"
'						For
'							response.write "document.write('<tr><td valign = "top" style =" border-top : dashed 1px #B8860B">');"
'							response.write "document.write('<table width = "100%" cellpadding = "10" cellspacing = "0">');"
'							response.write "document.write('<tr><td align = "left" bgcolor = "#E8F4DE">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank" title = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"><font color="#7FFFD4">Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)</font></a>');"
'							response.write "document.write('<br />Don Muang Airport <img src = "http://www.booking2hotels.com/images/layout15_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout15_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout15_star.gif" border = "0" />');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td width = "90" bgcolor = "#CEE6B4" align = "center" valign = "middle">');"
'							response.write "document.write('<font color = "#FF7F50">From USD 34');"
'							response.write "document.write('</font></td></tr></table></td></tr>');"
'							response.write "document.write('<tr><td height = "5"></td></tr>');"
'						Next
'						response.write "document.write('</table>');"
'					Case 13,14
						'response.write "document.write('case3.1');"
'						Select Case intType
'							Case 2
'								max_col=2
'							Case 3
'								max_col=3
'						End Select
'						intCol=0
'						response.write "document.write('<table width = "200" cellpadding = "0" cellspacing = "0" border = "0">');"
'						response.write "document.write('<tr><td align = "left">');"
'						response.write "document.write('<strong><font color = "#5F9EA0">Hotels in Bangkok</font></strong>');"
'						response.write "document.write('</td></tr></table><br />');"
'						response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0">');"
'						For
'							IF intCol mod max_col=0 Then
'								response.write "document.write('</tr><tr>');"
'							End IF
'							response.write "document.write('<tr><td width = "200" align = "left" valign = "top"  style = " border : solid 1px #B8860B">');"
'							response.write "document.write('<table cellpadding = "2" cellspacing = "3" width = "100%" border = "0">');"
'							response.write "document.write('<tr><td colspan = "2" align = "left">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank" title = "Admiral Suites Bangkok"><font color="#7FFFD4">Admiral Suites Bangkok</font></a>');"
'							response.write "document.write('</td></tr>');"
'							response.write "document.write('<tr><td width = "30%">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank"><img src = "http://www.hotels2thailand.com/thailand-hotels-pic/HBK161_1.jpg" border = "0" alt = "Admiral Suites Bangkok" width = "50" height = "53" /></a>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td align = "left">');"
'							response.write "document.write('Sukhumvit<br />');"
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/layout22_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout22_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout22_star.gif" border = "0" /><br />');"
'							response.write "document.write('<font color = "#FF7F50">Price From 77 USD</font>');"
'							response.write "document.write('</td></tr></table></td>');"
'							response.write "document.write('<td width = "10"></td>');"
'							response.write "document.write('<td width = "200" align = "left" valign = "top"  style = " border : solid 1px #B8860B">');"
'							response.write "document.write('<table cellpadding = "2" cellspacing = "3" width = "100%" border = "0">');"
'							response.write "document.write('<tr><td colspan = "2" align = "left">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank" title = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"><font color="#7FFFD4">Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)</font></a>');"
'							response.write "document.write('</td></tr>');"
'							response.write "document.write('<tr><td width = "30%">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank"><img src = "http://www.hotels2thailand.com/thailand-hotels-pic/HBK014_1.jpg" border = "0" alt = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)" width = "50" height = "53" /></a>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td align = "left">');"
'							response.write "document.write('Don Muang Airport<br />');"
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/layout22_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout22_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout22_star.gif" border = "0" /><br />');"
'							response.write "document.write('<font color = "#FF7F50">Price From 34 USD</font>');"
'							response.write "document.write('</td></tr></table></td>');"
'							response.write "document.write('<td width = "10"></td></tr>');"
'							response.write "document.write('<tr><td height = "10" colspan = "4"></td></tr>');"
'						Next
'						response.write "document.write('</table>');"
'					Case 15
						'response.write "document.write('case3.1');"
'						response.write "document.write('<table width = "510" cellpadding = "0" cellspacing = "0" border = "0">');"
'						response.write "document.write('<tr><td align = "center">');"
'						response.write "document.write('<strong><font color = "#5F9EA0">Hotels in Bangkok</font></strong>');"
'						response.write "document.write('</td></tr></table><br />');"
'						response.write "document.write('<table cellpadding = "3" cellspacing = "0" border = "0">');"
'						For
'							response.write "document.write('<tr><td width = "250" valign = "top" align = "center"  style = " border : solid 1px #B8860B">');"
'							response.write "document.write('<table width = "100%" cellpadding = "3" cellspacing = "0" border = "0">');"
'							response.write "document.write('<tr><td colspan = "2" align = "left">');"
'							response.write "document.write('<strong><a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank" title = "Admiral Suites Bangkok"><font color="#7FFFD4">Admiral Suites Bangkok</font></a></strong>');"
'							response.write "document.write('&nbsp;<img src = "http://www.booking2hotels.com/images/star3.0.jpg" border = "0" />');"
'							response.write "document.write('</td></tr>');"
'							response.write "document.write('<tr><td width = "40%" valign = "top" align = "left">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank"><img src = "http://www.hotels2thailand.com/thailand-hotels-pic/HBK161_1.jpg" border = "0" alt = "Admiral Suites Bangkok" width = "90" height = "95" /></a>');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" ><tr><td><a href = "http://www.hotels2thailand.com/thailand-hotels-map.asp?id=154&" target = "_blank"><img src = "http://www.booking2hotels.com/images/aff_map.gif" border = "0" /></a></td><td><a href = "http://www.hotels2thailand.com/thailand-hotels-map.asp?id=154&" target = "_blank"><font color = "#FF5D01">View Map</font></a></td></tr></table></td>');"
'							response.write "document.write('<td align = "left" valign = "middle">');"
'							response.write "document.write('<font color = "#FF7F50"><strong>Special Offers</strong><br />From</font><br />');"
'							response.write "document.write('<div align = "center">');"
'							response.write "document.write('<font color = "#FF7F50" size = "3">77 USD</font><br /><a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank" title = "Admiral Suites Bangkok"><img src = "http://www.booking2hotels.com/images/layout28_book-now.gif" border = "0"/></a></div><br /><font color = "green">Average User Rating :<br /><img src = "http://www.booking2hotels.com/images/rating_4.gif" border = "0"/><br />(4 From 5.0)</font> <a href = "http://www.hotels2thailand.com/review.asp?id=838" target="_blank"><u>(Review)</u></a><br />');"
'							response.write "document.write('</td></tr></table></td><td width = "10"></td>');"
'							response.write "document.write('<td width = "250" valign = "top" align = "center"  style = " border : solid 1px #B8860B">');"
'							response.write "document.write('<table width = "100%" cellpadding = "3" cellspacing = "0" border = "0">');"
'							response.write "document.write('<tr><td colspan = "2" align = "left">');"
'							response.write "document.write('<strong><a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank" title = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"><font color="#7FFFD4">Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)</font></a></strong>');"
'							response.write "document.write('&nbsp;<img src = "http://www.booking2hotels.com/images/star3.0.jpg" border = "0" />');"
'							response.write "document.write('</td></tr>');"
'							response.write "document.write('<tr><td width = "40%" valign = "top" align = "left">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank"><img src = "http://www.hotels2thailand.com/thailand-hotels-pic/HBK014_1.jpg" border = "0" alt = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)" width = "90" height = "95" /></a>');"
'							response.write "document.write('<table cellpadding = "0" cellspacing = "0" border = "0" ><tr><td><a href = "http://www.hotels2thailand.com/thailand-hotels-map.asp?id=154&" target = "_blank"><img src = "http://www.booking2hotels.com/images/aff_map.gif" border = "0" /></a></td><td><a href = "http://www.hotels2thailand.com/thailand-hotels-map.asp?id=154&" target = "_blank"><font color = "#FF5D01">View Map</font></a></td></tr></table></td>');"
'							response.write "document.write('<td align = "left" valign = "middle">');"
'							response.write "document.write('<font color = "#FF7F50"><strong>Special Offers</strong><br />From</font><br />');"
'							response.write "document.write('<div align = "center">');"
'							response.write "document.write('<font color = "#FF7F50" size = "3">34 USD</font><br /><a href="http://www.hotels2thailand.com/bangkok-hotels/Comfort-Suites-Airport-Hotel.asp" target="_blank" title = "Airport Suites Hotel Bangkok (Ex.Comfort Suites Airport)"><img src = "http://www.booking2hotels.com/images/layout28_book-now.gif" border = "0"/></a></div><br /><font color = "green">Average User Rating :<br /><img src = "http://www.booking2hotels.com/images/rating_3.4.gif" border = "0"/><br />(3.1 From 5.0)</font> <a href = "http://www.hotels2thailand.com/review.asp?id=154" target="_blank"><u>(Review)</u></a><br />');"
'							response.write "document.write('</td></tr></table></td><td width = "10"></td>');"
'							response.write "document.write('</tr>');"
'							response.write "document.write('<tr><td height = "10"></td><td height = "10"></td></tr>');"
'						Next
'						response.write "document.write('</table>');"
'					Case 16
						'response.write "document.write('case3.1');"
'						response.write "document.write('<table cellspacing = "0" cellpadding = "0" border = "0" width = "200">');"
'						response.write "document.write('<tr><td align = "center" >');"
'						response.write "document.write('<strong><font color = "#5F9EA0">Hotels in Bangkok');"
'						response.write "document.write('</strong></td>');"
'						response.write "document.write('</tr></table><br />');"
'						For
'							response.write "document.write('<table cellspacing = "3" cellpadding = "0" border = "0" width = "165">');"
'							response.write "document.write('<tr><td width = "160" align = "left" colspan = "2"><strong>');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank" title = "Admiral Suites Bangkok"><font color="#7FFFD4">Admiral Suites Bangkok</font></a>');"
'							response.write "document.write('</strong></td></tr>');"
'							response.write "document.write('<tr><td width = "55" align = "left" valign = "top">');"
'							response.write "document.write('<a href="http://www.hotels2thailand.com/bangkok-hotels/admiral-suites-bangkok.asp" target="_blank"><img src = "http://www.hotels2thailand.com/thailand-hotels-pic/HBK161_1.jpg" border = "0" alt = "Admiral Suites Bangkok" width = "50" height = "53" /></a>');"
'							response.write "document.write('</td>');"
'							response.write "document.write('<td width = "105" align = "left" valign = "middle">');"
'							response.write "document.write('Sukhumvit<br />');"
'							response.write "document.write('<img src = "http://www.booking2hotels.com/images/layout23_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout23_star.gif" border = "0" /><img src = "http://www.booking2hotels.com/images/layout23_star.gif" border = "0" /><br />');"
'							response.write "document.write('<font color = "#FF7F50">From USD 77</font>');"
'							response.write "document.write('</td></tr>');"
'							response.write "document.write('<tr><td align = "left" colspan = "2">');"
'							response.write "document.write('Average User Rating :<br />');"
'							response.write "document.write('<font color = "green"><img src = "http://www.booking2hotels.com/images/rating_4.gif" border = "0"/><br />(4 from 5.0)</font>');"
'							 response.write "document.write('<a href = "http://www.hotels2thailand.com/review.asp?id=838" target="_blank"><u>(Review)</u></a>');"
'							response.write "document.write('</td></tr></table><br />');"
'						Next
'				End Select	'	End intType
		End Select	'	End show
End Function
'		>>sql
'		SELECT product_code,
'        (SELECT SUM(fairway+greens+caddies+clubhouse+food+value_of_money) FROM tbl_golf_comment gc WHERE gc.golf_course_id = pg.golf_course_id AND gc.status = 1) AS overall,
'        (SELECT COUNT(gc.golf_comment_id) FROM tbl_golf_comment gc WHERE gc.golf_course_id = pg.golf_course_id AND gc.status = 1) AS all_user
'        FROM tbl_product_group pg
'        WHERE product_code = 'xxxxx'
'
'		>> สูตร
'		rating = overall / (all_user * 6)
'------------------------------------
'hotel : tbl_product_review
'
'day trip , water activity :
'tbl_product_review_sightseeing
'
'show and event :
'tbl_product_review_show_event
'
'health check up :
'tbl_product_review_health_check_up
'-------------------------------------------
'>>sql
'SELECT product_code,
'        (SELECT SUM(fairway+greens+caddies+clubhouse+food+value_of_money) FROM tbl_golf_comment gc WHERE gc.golf_course_id = pg.golf_course_id AND gc.status = 1) AS overall,
'        (SELECT COUNT(gc.golf_comment_id) FROM tbl_golf_comment gc WHERE gc.golf_course_id = pg.golf_course_id AND gc.status = 1) AS all_user
'        FROM tbl_product_group pg
'        WHERE product_code = 'xxxxx'
'
'>> สูตร
'case goft 
'rating = overall / (all_user * 6)
'other case
'rating = overall / (all_user)
%>

