<%
FUNCTION function_display_area_hotel(intDestinationID,intLocationID,strName,strAnchor,intType)

	Dim sqlCondition
	Dim strReturn
	Dim sqlAllHotel
	Dim recAllHotel
	Dim arrAllHotel
	Dim bolAllHotel
	Dim intAllHotel
	Dim intAllRow
	Dim intCountRow
	Dim intCountHotel
	Dim intHotelCount
	Dim intCountBlank
	
	SELECT CASE Cint(intDestinationID)
		Case 30 '### Bangkok ###
			SELECT CASE Cint(intLocationID)
				Case 90,189,70,125,62 '### Group1 ### China Town,Khaosan Road,Ratchadapisek,River Side,Suvarnabhumi Airport	
					sqlCondition = "location_id IN (90,189,70,125,62)"
				Case 59,124 '### Group2 ### Sukhumvit,Sathorn
					sqlCondition = "location_id IN (59,124)"
				Case 106,291,284,65,58,91 '### Group3 ### Chatuchak Market (JJ Market ),Don Muang Airport,Other Area,Pattanakarn,Petchburi Road,Silom
					sqlCondition = "location_id IN (106,291,284,65,58,91)"
				Case Else
					sqlCondition = "destination_id=30"
			END SELECT
			
		Case 31 '### Phuket ###
			SELECT CASE Cint(intLocationID)
				Case 68,81 '### Group1 ### Karon Beach,Patong Beach
					sqlCondition = "location_id IN (68,81)"
				Case  Else '### Group2 ### All Others
					sqlCondition = "destination_id=31 AND location_id NOT IN (68,81)"
			END SELECT
			
		Case 32 '### Chiang Mai ###
			sqlCondition = "destination_id=32"
			
		Case 33 '### Pattaya ###
			sqlCondition = "destination_id=33"
			
		Case 34 '### Koh Samui ###
			sqlCondition = "destination_id=34" '### Test For 177 External Link ###
			
		Case 35 '### Krabi ###
			sqlCondition = "destination_id=35"
					
		Case 36,40,43,60,66,73,74,76,83	'### North ### Chiang Rai,Phitsanulok,Mae Hong Son,Lamphun,Phetchabun,Sukhothai,Lampang,Loei,Tak
			sqlCondition = "destination_id IN (36,40,43,60,66,73,74,76,83)"
		
		Case 44,45,52,56,57,59,62,84,63,67,68,69,77,78,79 '### Middle+North East ### Ayutthaya,Kanchanaburi,Khao Yai,Nakornpathom,Nakornnayok,Samutprakarn,Ratchaburi,Nakhon Phanom,Nakhonratchasima,Uthai Thani,Khon Kaen,Nakhon Si Thammarat,Nong Khai,Ubon Ratchathani,Udon Thani
			sqlCondition = "destination_id IN (44,45,52,56,57,59,62,84,63,67,68,69,77,78,79)"

		Case 37,38,42,50 '### Sea1(Near Bangkok) ### Cha Am,Hua Hin,Rayong,Koh Samet
			sqlCondition = "destination_id IN (37,38,42,50)"

		Case 46,48,49,65,75,58,61 '### Sea2(East) ### Koh Chang,Prachuap Khiri Khan,Koh Kood,Koh Tao,Trat,Chanthaburi,Phetchaburi
			sqlCondition = "destination_id IN (46,48,49,65,75,58,61)"

		Case 51,53,54,55,64,70,71,72 '### Sea3(South) ### Phang Nga,Koh Phangan,Trang,Chumphon,Other Destinations,Songkhla,Hat Yai,Surat Thani	
			sqlCondition = "destination_id IN (51,53,54,55,64,70,71,72)"
		
		Case Else
			sqlCondition = "destination_id=" & intDestinationID
	END SELECT

	sqlAllHotel = "SELECT p.title_en,p.files_name,p.destination_id"
	sqlAllHotel = sqlAllHotel & " FROM tbl_product p,tbl_product_location pl"
	sqlAllHotel = sqlAllHotel & " WHERE p.status=1 AND p.product_cat_id=29 AND pl.product_id=p.product_id AND ("&sqlCondition&")"
	sqlAllHotel = sqlAllHotel & " ORDER BY p.title_en ASC"
	Set recAllHotel = Server.CreateObject ("ADODB.Recordset")
	recAllHotel.Open SqlAllHotel, Conn,adOpenStatic, adLockReadOnly
		IF NOT recAllHotel.EOF Then
			arrAllHotel = recAllHotel.GetRows()
			bolAllHotel = True
			intAllHotel = Ubound(arrAllHotel,2) + 1
			intAllRow =  (Ubound(arrAllHotel,2)+1)/4 + 1
		Else
			bolAllHotel = False
		End IF
	recAllHotel.Close
	Set recAllHotel = Nothing

	IF bolAllHotel Then
		strReturn = "<table cellspacing=1 cellpadding=2 width=""100%"" class=""f11"" border=""0"" bgcolor=""#e4e4e4"">" & VbCrlf
		strReturn = strReturn & "<tbody>" & VbCrlf
		strReturn = strReturn & "<tr>" & VbCrlf
		strReturn = strReturn & "<td bgcolor=""#FFFFFF""> " & VbCrlf
		strReturn = strReturn & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" class=""f11"">" & VbCrlf
		strReturn = strReturn & "<tr>" & VbCrlf
		strReturn = strReturn & "<td height=""25"" bgcolor=""EDF5FE""><div id=""hoteldetail""><h4>Hotels in relate area with "&strName&"</h4></div></td>" & VbCrlf
		strReturn = strReturn & "</tr>" & VbCrlf
		strReturn = strReturn & "</table>" & VbCrlf
		strReturn = strReturn & "<table id=""hotels_list"">" & VbCrlf
		strReturn = strReturn & "<tbody id=""All_Hotels"">" & VbCrlf
		
		For intCountRow=1 To intAllRow
		
			strReturn = strReturn & "<tr>" & VbCrlf
			
			For intCountHotel = 1 To 4
				intHotelCount = intHotelCount + 1
				IF intHotelCount=<intAllHotel Then
					strReturn = strReturn & "<td><li> <a href=""/" & function_generate_hotel_link(arrAllHotel(2,intHotelCount-1),"",1) & "/" &arrAllHotel(1,intHotelCount-1)&""">" & arrAllHotel(0,intHotelCount-1) & "</a></li></td>" & VbCrlf
				Else
					EXIT For
				End IF
			Next
			
			IF intCountHotel<4 Then
				For intCountBlank=intCountHotel To 4
					strReturn = strReturn & "<td>&nbsp;</td>" & VbCrlf
				Next
			End IF
			
			strReturn = strReturn & "</tr>" & VbCrlf
		
		Next
	
		strReturn = strReturn & "</tbody>" & VbCrlf
		strReturn = strReturn & "</table>" & VbCrlf
		strReturn = strReturn & "<div align=right><a href=""#"& strAnchor &"""><font color=""FE5400"">Back To Top</font></a></div>" & VbCrlf
		strReturn = strReturn & "</td>" & VbCrlf
		strReturn = strReturn & "</tr>" & VbCrlf
		strReturn = strReturn & "</tbody>" & VbCrlf
		strReturn = strReturn & "</table>" & VbCrlf
	End IF
	
	function_display_area_hotel = strReturn 
	
END FUNCTION
%>