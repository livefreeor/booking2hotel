<%
FUNCTION function_site_map_destination_main()
	
	Dim arrDestinationID
	Dim intCount
	Dim sqlDestination
	Dim recDestination
	Dim arrDestination
	Dim intHotelCount
	Dim intHotelPage
	Dim intLinkCount
	
	arrDestinationID = Array(30,31,32,33,34,35,46)
	
	For intCount=0 To Ubound(arrDestinationID)
	
		sqlDestination = "SELECT product_id,destination_id,title_en,files_name"
		sqlDestination = sqlDestination & " FROM tbl_product "
		sqlDestination = sqlDestination & " WHERE status=1 AND files_name IS NOT NULL AND destination_id=" & arrDestinationID(intCount)
		sqlDestination = sqlDestination & " ORDER BY title_en ASC"
		
		Set recDestination = Server.CreateObject ("ADODB.Recordset")
		recDestination.Open SqlDestination, Conn,adOpenStatic,adLockreadOnly
			arrDestination = recDestination.GetRows()
		recDestination.Close
		Set recDestination = Nothing
		
		
		IF Ubound(arrDestination,2) MOD ConstPageSearchMain=0 Then
			intHotelPage = Ubound(arrDestination)/ConstPageSearchMain
		Else
			intHotelPage = (((Ubound(arrDestination,2))- (Ubound(arrDestination,2) MOD ConstPageSearchMain))/ConstPageSearchMain) + 1
		End IF
		
		For intLinkCount=1 To intHotelPage
			IF intLinkCount=1 Then
				function_site_map_destination_main = function_site_map_destination_main & "   <url>" & VbCrlf
				function_site_map_destination_main = function_site_map_destination_main & "      <loc>http://www.hotels2thailand.com/"&function_generate_hotel_link(arrDestinationID(intCount),"",5)&"</loc>" & VbCrlf
				function_site_map_destination_main = function_site_map_destination_main & "      <lastmod>"&function_date(Date,6)&"</lastmod>" & VbCrlf
				function_site_map_destination_main = function_site_map_destination_main & "      <changefreq>weekly</changefreq>" & VbCrlf
				function_site_map_destination_main = function_site_map_destination_main & "   </url>" & VbCrlf
			Else
				function_site_map_destination_main = function_site_map_destination_main & "   <url>" & VbCrlf
				function_site_map_destination_main = function_site_map_destination_main & "      <loc>http://www.hotels2thailand.com/thailand-hotels-more.asp?sort=featureDESC&amp;destination="&arrDestinationID(intCount)&"&amp;page="&intLinkCount & "</loc>" & VbCrlf
				function_site_map_destination_main = function_site_map_destination_main & "      <lastmod>"&function_date(Date,6)&"</lastmod>" & VbCrlf
				function_site_map_destination_main = function_site_map_destination_main & "      <changefreq>weekly</changefreq>" & VbCrlf
				function_site_map_destination_main = function_site_map_destination_main & "   </url>" & VbCrlf
			End IF
		Next
		
	Next
	
END FUNCTION
%>