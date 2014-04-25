<%
FUNCTION function_display_destination_hotel(intDestinationID,strDestination,strAnchor,intType)

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
	
	sqlAllHotel = "st_hotel_detail_all_hotel_destination " & intDestinationID
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
	
	SELECT CASE intType
	Case 1 '### Hotel Detail ###
		strReturn = "<table cellspacing=1 cellpadding=2 width=""100%"" class=""f11"" border=""0"" bgcolor=""#e4e4e4"">" & VbCrlf
		strReturn = strReturn & "<tbody>" & VbCrlf
		strReturn = strReturn & "<tr>" & VbCrlf
		strReturn = strReturn & "<td bgcolor=""#FFFFFF""> " & VbCrlf
		strReturn = strReturn & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" class=""f11"">" & VbCrlf
		strReturn = strReturn & "<tr>" & VbCrlf
		strReturn = strReturn & "<td height=""25"" bgcolor=""EDF5FE""><div id=""hoteldetail""><h4>All Hotels in "& strDestination &"</h4></div></td>" & VbCrlf
		strReturn = strReturn & "</tr>" & VbCrlf
		strReturn = strReturn & "</table>" & VbCrlf
		strReturn = strReturn & "<table id=""hotels_list"">" & VbCrlf
		strReturn = strReturn & "<tbody id=""All_Hotels"">" & VbCrlf
		
		For intCountRow=1 To intAllRow
		
			strReturn = strReturn & "<tr>" & VbCrlf
			
			For intCountHotel = 1 To 4
				intHotelCount = intHotelCount + 1
				IF intHotelCount=<intAllHotel Then
					strReturn = strReturn & "<td><li> <a href=""/" & function_generate_hotel_link(intDestinationID,"",1) & "/" &arrAllHotel(1,intHotelCount-1)&""">" & arrAllHotel(0,intHotelCount-1) & "</a></li></td>" & VbCrlf
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
		
	Case 2'### Destination Page ###
		strReturn = "<table cellspacing=1 cellpadding=2 width=""100%"" class=""f11"" border=""0"" bgcolor=""#e4e4e4"">" & VbCrlf
		strReturn = strReturn & "<tbody>" & VbCrlf
		strReturn = strReturn & "<tr>" & VbCrlf
		strReturn = strReturn & "<td bgcolor=""#FFFFFF""> " & VbCrlf
		strReturn = strReturn & "<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" id=""tbl_hotel_destination"">" & VbCrlf
		strReturn = strReturn & "<tr>" & VbCrlf
		strReturn = strReturn & "<td height=""25""><h2>All Hotels in "& strDestination &"</h2></td>" & VbCrlf
		strReturn = strReturn & "</tr>" & VbCrlf
		strReturn = strReturn & "</table>" & VbCrlf
		strReturn = strReturn & "<table id=""tbl_hotel_destination"">" & VbCrlf
		strReturn = strReturn & "<tbody>" & VbCrlf
		
		For intCountRow=1 To intAllRow
		
			strReturn = strReturn & "<tr>" & VbCrlf
			
			For intCountHotel = 1 To 4
				intHotelCount = intHotelCount + 1
				IF intHotelCount=<intAllHotel Then
					strReturn = strReturn & "<td><li> <a href=""/" & function_generate_hotel_link(intDestinationID,"",1) & "/" &arrAllHotel(1,intHotelCount-1)&""">" & arrAllHotel(0,intHotelCount-1) & "</a></li></td>" & VbCrlf
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
		strReturn = strReturn & "</td>" & VbCrlf
		strReturn = strReturn & "</tr>" & VbCrlf
		strReturn = strReturn & "</tbody>" & VbCrlf
		strReturn = strReturn & "</table>" & VbCrlf
	Case 3
	END SELECT
	
	function_display_destination_hotel = strReturn
					
END FUNCTION
%>
