<%
FUNCTION function_display_health_check_up_list(intProductID,intProductCatID,intDestinationID,strTitle,strDetail,strfilesName,strProductCode,intNumber,intType)

	Dim strReturn
	Dim fso

	strReturn = "<table width=""98%"" border=""0"" cellspacing=""1"" cellpadding=""3"" bgcolor=""FFDAAE"" class=""f11"">" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td bgcolor=""#FFFFFF"">" & VbCrlf
	strReturn = strReturn & "<table cellspacing=0 cellpadding=0 width=""100%"" border=""0"" class=""f11"">" & VbCrlf
	strReturn = strReturn & "<tbody>" & VbCrlf
	strReturn = strReturn & "<tr bgcolor=""EDF5FE"">" & VbCrlf
	strReturn = strReturn & "<td class=l2 height=""21"" bgcolor=""EDF5FE"">" & VbCrlf
	strReturn = strReturn & "<div align=left><b><font color=""434343"">"&intNumber&".) </font></b>" & VbCrlf
	strReturn = strReturn & "<a class=nu href=""/"&function_generate_health_check_up_link(intDestinationID,"",1)&"/"&strFilesname&"""><font color=#003366><b><font color=""#ff6600"">"&strTitle&"</font></b></font></a> </div>" & VbCrlf
	strReturn = strReturn & "</td>" & VbCrlf
	strReturn = strReturn & "<td>" & VbCrlf
	strReturn = strReturn & "</td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr bgcolor=""#ffffff"">" & VbCrlf
	strReturn = strReturn & "<td colspan=""2"">" & VbCrlf
	strReturn = strReturn & "<table cellspacing=""3"" cellpadding=""0"" width=""100%"" class=""f11"">" & VbCrlf
	strReturn = strReturn & "<tbody>" & VbCrlf
	strReturn = strReturn & "<tr valign=""top"">" & VbCrlf
	strReturn = strReturn & "<td valign=""middle"">&nbsp;</td>" & VbCrlf
	strReturn = strReturn & "<td>&nbsp;</td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr valign=top>" & VbCrlf
	strReturn = strReturn & "<td width=""160"" align=""center"" valign=""middle"">" & VbCrlf
	strReturn = strReturn & "<div align=""center"">" & VbCrlf
	strReturn = strReturn & "<table width=""106"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""119"" background=""/images/bg_picture_90_95.gif"">" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td valign=""top"">" & VbCrlf
	strReturn = strReturn & "<table width=""62"" border=""0"" cellspacing=""0"" cellpadding=""0"" height=""52"">" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td valign=""top""><a href=""/"&function_generate_health_check_up_link(intDestinationID,"",1)&"/"&strFilesname&"""><img src=""thailand-health-pic/"&strProductCode&"_1.jpg"" alt="""&strTitle&""" border=""0""></a></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "</table></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "</table>" & VbCrlf
	strReturn = strReturn & "</td>" & VbCrlf
	strReturn = strReturn & "<td width=""400"">" & VbCrlf
	
	set fso = createobject("scripting.filesystemobject")
	
	if fso.FileExists (server.mappath("thailand-health-pic/"&strProductCode&"_logo.gif")) then 
	strReturn = strReturn & "<a href=""/"&function_generate_health_check_up_link(intDestinationID,"",1)&"/"&strFilesname&"""><img src=""thailand-health-pic/"&strProductCode&"_logo.gif"" alt="""&strTitle&""" border=""0""></a><br><br>" & VbCrlf
	end if
	
	strReturn = strReturn & function_display_hotel_detail(strDetail,"",1) & VbCrlf
	strReturn = strReturn & "<br><br>" & VbCrlf
	strReturn = strReturn & "<a href=""/"&function_generate_health_check_up_link(intDestinationID,"",1)&"/"&strFilesname&"""><img src=""/images/b_book_now.gif"" border=""0""></a>" & VbCrlf
	strReturn = strReturn & "</td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr>" & VbCrlf
	strReturn = strReturn & "<td colspan=2>&nbsp;</td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "</tbody>" & VbCrlf
	strReturn = strReturn & "</table></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "<tr bgcolor=#ffffff>" & VbCrlf
	strReturn = strReturn & "<td colspan=2 align=""center"">" & VbCrlf
	strReturn = strReturn & function_display_rate_health(intProductID,1) & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "</table></td>" & VbCrlf
	strReturn = strReturn & "</tr>" & VbCrlf
	strReturn = strReturn & "</table>" & VbCrlf

	function_display_health_check_up_list = strReturn

END FUNCTION
%>