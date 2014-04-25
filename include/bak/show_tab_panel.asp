<%
	response.Write("hello joojube")
'						intCol = 0
'						max_col =2
'						
'						'For intCountDes=1 To 3
'						
'						strOutPut = strOutPut & "<span>" & VbCrlf
'						strOutPut = strOutPut & "<table border=""0"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
'						For intCountHot=1 To 6
'							intPos = 1	'(intCountHot + (6*(intCountDes-1)))-1
'							
'							IF intCountHot=1Then
'							strOutPut = strOutPut & "<tr>" & VbCrlf
'							End IF
'							
'							IF intCol mod max_col=0 Then
'							strOutPut = strOutPut & "</tr>" & VbCrlf
'							strOutPut = strOutPut & "<tr>" & VbCrlf
'							End IF
'							
'							IF (intCountHot = 5 OR intCountHot = 6) Then
'								strOutPut = strOutPut & "<td valign=""top""><span style=""display:none;""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3""><tr><td valign=""top"" width=""65""><br><a href=""/"&function_generate_hotel_link(arrHot(4,intCountHot),"",1)&"/"&arrHot(3,intCountHot)&"""><img src=""/thailand-hotels-pic/"&arrHot(1,intCountHot)&"_1.jpg"" border=""0"" alt="""&arrHot(2,intCountHot)&""" style=""border:1px solid #999999"" width=""62"" height=""52""></a></td><td width=""154""><br><font color=""#0066CC""><a href=""/"&function_generate_hotel_link(arrHot(4,intCountHot),"",1)&"/"&arrHot(3,intCountHot)&""">"&arrHot(2,intCountHot)&"</a></font></td></tr><tr><td colspan=""2"" width=""226""><img src=""/images/arrow06.gif"">&nbsp;<font color=""#006600"">"&arrHot(5,intCountHot)&"...</font></td></tr></table></span></td>" & VbCrlf
'							Else
'								strOutPut = strOutPut & "<td valign=""top""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3""><tr><td valign=""top"" width=""65""><br><a href=""/"&function_generate_hotel_link(arrHot(4,intCountHot),"",1)&"/"&arrHot(3,intCountHot)&"""><img src=""/thailand-hotels-pic/"&arrHot(1,intCountHot)&"_1.jpg"" border=""0"" alt="""&arrHot(2,intCountHot)&""" style=""border:1px solid #999999"" width=""62"" height=""52""></a></td><td width=""154""><br><font color=""#0066CC""><a href=""/"&function_generate_hotel_link(arrHot(4,intCountHot),"",1)&"/"&arrHot(3,intCountHot)&""">"&arrHot(2,intCountHot)&"</a></font></td></tr><tr><td colspan=""2"" width=""226""><img src=""/images/arrow06.gif"">&nbsp;<font color=""#006600"">"&arrHot(5,intCountHot)&"...</font></td></tr></table></td>" & VbCrlf
'							End IF
'							intCol = intCol + 1
							
							
						Next
						strOutPut = strOutPut & "</tr>" & VbCrlf
						
						strOutPut = strOutPut & "</table>" & VbCrlf
						strOutPut = strOutPut & "</span>" & VbCrlf
						response.Write(strOutPut)
						
						'Next
%>
