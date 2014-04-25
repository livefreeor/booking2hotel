<%
FUNCTION function_site_map_location_main()
	
	Dim arrLocationID
	Dim intCount
	Dim sqlLocation
	Dim recLocation
	Dim arrLocation
	Dim intHotelCount
	Dim intHotelPage
	Dim intLinkCount
	
	arrLocationID = Array(30,31,32,33,34,35,46)
	
	
		sqlLocation = "SELECT location_id,destination_id,files_name,"
		sqlLocation = sqlLocation & " (SELECT COUNT(*) FROM tbl_product_location pl,tbl_product p WHERE p.product_id=pl.product_id AND p.status=1 AND pl.location_id=l.location_id) AS num_hotels"
		sqlLocation = sqlLocation & " FROM tbl_location l"
		sqlLocation = sqlLocation & " WHERE status=1 AND files_name IS NOT NULL AND destination_id IN (30,31,32,33,34,35,46)"
		sqlLocation = sqlLocation & " AND (SELECT COUNT(*) FROM tbl_product_location pl,tbl_product p WHERE p.product_id=pl.product_id AND p.status=1 AND pl.location_id=l.location_id)>0"
		sqlLocation = sqlLocation & " ORDER BY destination_id ASC"

		Set recLocation = Server.CreateObject ("ADODB.Recordset")
		recLocation.Open sqlLocation, Conn,adOpenStatic,adLockreadOnly
			arrLocation = recLocation.GetRows()
		recLocation.Close
		Set recLocation = Nothing
		
		For intCount=0 To Ubound(arrLocation,2)		
		
			IF arrLocation(3,intCount) > ConstPageSearchMain Then
				intHotelPage = (((arrLocation(3,intCount))- (arrLocation(3,intCount) MOD ConstPageSearchMain))/ConstPageSearchMain) + 1
			Else
				intHotelPage = 1
			End IF
			
			For intLinkCount=1 To intHotelPage
				IF intLinkCount=1 Then
					function_site_map_location_main = function_site_map_location_main & "   <url>" & VbCrlf
					function_site_map_location_main = function_site_map_location_main & "      <loc>http://www.hotels2thailand.com/"&arrLocation(2,intCount)&"</loc>" & VbCrlf
					function_site_map_location_main = function_site_map_location_main & "      <lastmod>"&function_date(Date,6)&"</lastmod>" & VbCrlf
					function_site_map_location_main = function_site_map_location_main & "      <changefreq>weekly</changefreq>" & VbCrlf
					function_site_map_location_main = function_site_map_location_main & "   </url>" & VbCrlf
				Else
					function_site_map_location_main = function_site_map_location_main & "   <url>" & VbCrlf
					function_site_map_location_main = function_site_map_location_main & "      <loc>http://www.hotels2thailand.com/thailand-hotels-more-location.asp?sort=featureDESC&amp;destination="&arrLocation(1,intCount)&"&amp;location="&arrLocation(0,intCount)&"&amp;page="&intLinkCount&"</loc>" & VbCrlf
					function_site_map_location_main = function_site_map_location_main & "      <lastmod>"&function_date(Date,6)&"</lastmod>" & VbCrlf
					function_site_map_location_main = function_site_map_location_main & "      <changefreq>weekly</changefreq>" & VbCrlf
					function_site_map_location_main = function_site_map_location_main & "   </url>" & VbCrlf
				End IF
			Next
			
		Next
END FUNCTION
%>