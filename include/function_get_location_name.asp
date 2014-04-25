<%
FUNCTION function_get_location_name(intLocation,intType)

	Dim sqlLocation
	
	
	sqlLocation = "SELECT title_en FROM tbl_location WHERE location_id=" & intLocation
	function_get_location_name = conn.Execute(sqlLocation).GetString
	
	function_get_location_name = Left(function_get_location_name,Len(function_get_location_name)-1)
	
END FUNCTION
%>