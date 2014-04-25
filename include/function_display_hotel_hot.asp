<!--#include virtual="/include/function_get_hotel_hot.asp"-->
<%
FUNCTION function_display_hotel_hot(intDestinationID,intLocationID,intType)

	Dim strHotelBangkok
	Dim strHotelChiangMai
	Dim strHotelHuaHin
	Dim strHotelSamui
	Dim strHotelKrabi
	Dim strHotelPattaya
	Dim strHotelPhangNga
	Dim strHotelPhuket
	Dim strHotelAll
	Dim sqlHot
	Dim recHot
	Dim arrHot
	Dim intCountHot
	Dim intCountDes
	Dim strOutPut
	Dim intPos
	
	SELECT CASE intType
		Case 1 '### Home Page ###
		
			strHotelBangkok = function_get_hotel_hot(30,1)
			strHotelChiangMai = function_get_hotel_hot(32,1)
			strHotelHuaHin = function_get_hotel_hot(38,1)
			strHotelSamui = function_get_hotel_hot(34,1)
			strHotelKrabi = function_get_hotel_hot(35,1)
			strHotelPattaya = function_get_hotel_hot(33,1)
			strHotelPhangNga = function_get_hotel_hot(51,1)
			strHotelPhuket = function_get_hotel_hot(31,1)
			
			strHotelAll = strHotelBangkok & "," & strHotelChiangMai & "," & strHotelHuaHin& "," & strHotelSamui & "," & strHotelKrabi & "," & strHotelPattaya & "," & strHotelPhangNga & "," & strHotelPhuket
			'strHotelAll = "52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,94" 'Use for local database

			'sqlHot = "SELECT p.product_id,p.product_code,p.title_en,p.files_name,p.destination_id,"
			'sqlHot = sqlHot & " ISNULL((SELECT TOP 1 spr.title FROM tbl_product_option spo,tbl_promotion spr WHERE  spo.option_id=spr.option_id and spo.product_id=p.product_id and spr.status=1 and date_end>getdate() ORDER BY spr.day_min ASC,spr.date_start ASC),'') as pr_title"
			'sqlHot = sqlHot & " FROM tbl_product p,tbl_destination d"
			'sqlHot = sqlHot & " WHERE p.destination_id=d.destination_id AND p.product_id IN ("&strHotelAll&")"
			'sqlHot = sqlHot & " ORDER by d.title_en ASC,p.point DESC"
			sqlHot = "st_hotel_hot " & "'" &strHotelAll& "'"
			'response.write "<font color=white>"&sqlHot&"</font>"
			Set recHot=Server.CreateObject("adodb.recordset")
			recHot.Open sqlHot,conn,adOpenStatic,adLockreadOnly
				arrHot = recHot.GetRows
			recHot.Close
			SET recHot = Nothing

					strOutPut = strOutPut & "<fieldset id=""tbl_hot_hotel"">" & VbCrlf
					strOutPut = strOutPut & "<legend>Hot Thailand Hotels</legend>" & VbCrlf
					strOutPut = strOutPut & "<DIV class=clearfloat id=recommend-hotel>" & VbCrlf
					strOutPut = strOutPut & "<DIV id=area-tab>" & VbCrlf
					strOutPut = strOutPut & "<UL id=tabs_label>" & VbCrlf
					strOutPut = strOutPut & "<LI><A href=""#"">Bangkok</A>" & VbCrlf
					strOutPut = strOutPut & "<LI><A href=""#"">Chiang Mai</A>" & VbCrlf
					strOutPut = strOutPut & "<LI><A href=""#"">Hua Hin</A>" & VbCrlf
					strOutPut = strOutPut & "<LI><A href=""#"">Koh Samui</A>" & VbCrlf
					strOutPut = strOutPut & "<LI><A href=""#"">Krabi</A>" & VbCrlf
					strOutPut = strOutPut & "<LI><A href=""#"">Pattaya</A>" & VbCrlf
					strOutPut = strOutPut & "<LI><A href=""#"">Phang Nga</A>" & VbCrlf
					strOutPut = strOutPut & "<LI><A href=""#"">Phuket</A></LI></UL>" & VbCrlf
					strOutPut = strOutPut & "<DIV class=line></DIV>" & VbCrlf
					strOutPut = strOutPut & "</DIV>" & VbCrlf
					strOutPut = strOutPut & "<DIV class=hotel id=tabs_detail>" & VbCrlf
					
			For intCountDes=1 To 8
					strOutPut = strOutPut & "<span>" & VbCrlf
					strOutPut = strOutPut & "<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""2"">" & VbCrlf
				For intCountHot=1 To 6
					intPos = (intCountHot + (6*(intCountDes-1)))-1
					
					IF intCountHot=1Then
					strOutPut = strOutPut & "<tr>" & VbCrlf
					End IF
					
					IF intCountHot=4 Then
					strOutPut = strOutPut & "</tr>" & VbCrlf
					strOutPut = strOutPut & "<tr>" & VbCrlf
					End IF
					
					strOutPut = strOutPut & "<td valign=""top""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3""><tr><td valign=""top"" width=""65""><br><a href=""/"&function_generate_hotel_link(arrHot(4,intPos),"",1)&"/"&arrHot(3,intPos)&"""><img src=""/thailand-hotels-pic/"&arrHot(1,intPos)&"_1.jpg"" border=""0"" alt="""&arrHot(2,intPos)&""" style=""border:1px solid #999999"" width=""62"" height=""52""></a></td><td width=""154""><br><font color=""#0066CC""><a href=""/"&function_generate_hotel_link(arrHot(4,intPos),"",1)&"/"&arrHot(3,intPos)&""">"&arrHot(2,intPos)&"</a></font></td></tr><tr><td colspan=""2"" width=""226""><img src=""/images/arrow06.gif"">&nbsp;<font color=""#006600"">"&arrHot(5,intPos)&"...</font></td></tr></table></td>" & VbCrlf
					
					IF intCountHot=6 Then
					strOutPut = strOutPut & "</tr>" & VbCrlf
					End IF
				Next
				
					strOutPut = strOutPut & "</table>" & VbCrlf
					strOutPut = strOutPut & "</span>" & VbCrlf
			Next


					strOutPut = strOutPut & "</DIV>" & VbCrlf
					strOutPut = strOutPut & "</DIV>" & VbCrlf
					strOutPut = strOutPut & "</fieldset>" & VbCrlf
					strOutPut = strOutPut & "<SCRIPT language=javascript>var obj_tabs = new cs_tabs_auto_swap(""obj_tabs"");obj_tabs.setup({tabs : ""tabs_label"",details : ""tabs_detail"",swap : ""auto"",time : 7});</SCRIPT>" & VbCrlf

		Case 2
		Case 3
	END SELECT

	function_display_hotel_hot = strOutput

END FUNCTION
%>