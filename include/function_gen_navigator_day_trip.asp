<%
FUNCTION function_gen_navigator_day_trip(intProductID,intLocationID,intDestinationID,intType)
	
	Dim strDestination
	Dim strDestinationLink
	Dim strDestinationPath
	Dim strLocation
	Dim strLocationLink
	Dim strProduct
	Dim strProductLink
	
	Dim sqlLocation
	Dim recLocation
	Dim arrLocation
	
	Dim sqlProduct
	Dim recProduct
	Dim arrProduct
	
	IF intDestinationID="" Then
		intDestinationID="0"
	End IF
	
	SELECT CASE Cstr(intDestinationID)
		Case 30 'Bangkok
			strDestination = "<font color=""#FE5400"">Bangkok Day Trips</font>"
			strDestinationLink = "<a href=""/bangkok-day-trips.asp"">Bangkok Day Trips</a>"
			strDestinationPath = "bangkok-day-trips"
		Case 31	'Phuket
			strDestination = "<font color=""#FE5400"">Phuket Day Trips</font>"
			strDestinationLink = "<a href=""/phuket-day-trips.asp"">Phuket Day Trips</a>"
			strDestinationPath = "phuket-day-trips"
		Case 32 'Chiang Mai
			strDestination = "<font color=""#FE5400"">Chiang Mai Day Trips</font>"
			strDestinationLink = "<a href=""/chiang-mai-day-trips.asp"">Chiang Mai Day Trips</a>"
			strDestinationPath = "chiang-mai-day-trips"
		Case 33 'Pattaya
			strDestination = "<font color=""#FE5400"">Pattaya Day Trips</font>"
			strDestinationLink = "<a href=""/pattaya-day-trips.asp"">Pattaya Day Trips</a>"
			strDestinationPath = "pattaya-day-trips"
		Case 34 'Koh Samui
			strDestination = "<font color=""#FE5400"">Koh Samui Day Trips</font>"
			strDestinationLink = "<a href=""/koh-samui-day-trips.asp"">Koh Samui Day Trips</a>"
			strDestinationPath = "koh-samui-day-trips"
		Case 35 'Krabi
			strDestination = "<font color=""#FE5400"">Krabi Day Trips</font>"
			strDestinationLink = "<a href=""/krabi-day-trips.asp"">Krabi Day Trips</a>"
			strDestinationPath = "krabi-day-trips"
		Case 36 'Chiang Rai
			strDestination = "<font color=""#FE5400"">Chiang Rai Day Trips</font>"
			strDestinationLink = "<a href=""/chiang-rai-day-trips.asp"">Chiang Rai Day Trips</a>"
			strDestinationPath = "chiang-rai-day-trips"
		Case 37 'Cha Am
			strDestination = "<font color=""#FE5400"">Cha Am Day Trips</font>"
			strDestinationLink = "<a href=""/cha-am-day-trips.asp"">Cha Am Day Trips</a>"
			strDestinationPath = "cha-am-day-trips"
		Case 38 'Hua Hin
			strDestination = "<font color=""#FE5400"">Hua Hin Day Trips</font>"
			strDestinationLink = "<a href=""/hua-hin-day-trips.asp"">Hua Hin Day Trips</a>"
			strDestinationPath = "hua-hin-day-trips"
		Case 42 'Rayong
			strDestination = "<font color=""#FE5400"">Rayong Day Trips</font>"
			strDestinationLink = "<a href=""/rayong-day-trips.asp"">Rayong Day Trips</a>"
			strDestinationPath = "rayong-day-trips"
		Case 43 'Mae Hong Son
			strDestination = "<font color=""#FE5400"">Mae Hong Son Day Trips</font>"
			strDestinationLink = "<a href=""/mae-hong-son-day-trips.asp"">Mae Hong Son Day Trips</a>"
			strDestinationPath = "mae-hong-son-day-trips"
		Case 45 'Kanchanaburi
			strDestination = "<font color=""#FE5400"">Kanchanaburi Day Trips</font>"
			strDestinationLink = "<a href=""/kanchanaburi-day-trips.asp"">Kanchanaburi Day Trips</a>"
			strDestinationPath = "kanchanaburi-day-trips"
		Case 46 'Koh Chang
			strDestination = "<font color=""#FE5400"">Koh Chang Day Trips</font>"
			strDestinationLink = "<a href=""/koh-chang-day-trips.asp"">Koh Chang Day Trips</a>"
			strDestinationPath = "koh-chang-day-trips"
		Case 48 'Prachuap Khiri khan
			strDestination = "<font color=""#FE5400"">Prachuap Khiri Khan Day Trips</font>"
			strDestinationLink = "<a href=""/prachuap-khiri-khan-day-trips.asp"">Prachuap Khiri Khan Day Trips</a>"
			strDestinationPath = "prachuap-khiri-khan-day-trips"
		Case 49 'Koh Kood
			strDestination = "<font color=""#FE5400"">Koh Kood Day Trips</font>"
			strDestinationLink = "<a href=""/koh-kood-day-trips.asp"">Koh KoodDay Trips</a>"
			strDestinationPath = "koh-kood-day-trips"
		Case 50 'Koh Samet
			strDestination = "<font color=""#FE5400"">Koh Samet Day Trips</font>"
			strDestinationLink = "<a href=""/koh-samet-day-trips.asp"">Koh Samet Day Trips</a>"
			strDestinationPath = "koh-samet-day-trips"
		Case 51 'Phang Nga
			strDestination = "<font color=""#FE5400"">Phang Nga Day Trips</font>"
			strDestinationLink = "<a href=""/phang-nga-day-trips.asp"">Phang Nga Day Trips</a>"
			strDestinationPath = "phang-nga-day-trips"
		Case 52 'khao Yai
			strDestination = "<font color=""#FE5400"">Khao Yai Day Trips</font>"
			strDestinationLink = "<a href=""/khao-yai-day-trips.asp"">Khao Yai Day Trips</a>"
			strDestinationPath = "khao-yai-day-trips"
		Case 53 'Koh Phangan
			strDestination = "<font color=""#FE5400"">Koh Phangan Day Trips</font>"
			strDestinationLink = "<a href=""/koh-phangan-day-trips.asp"">Koh Phangan Day Trips</a>"
			strDestinationPath = "koh-phangan-day-trips"
		Case 54 'Trang
			strDestination = "<font color=""#FE5400"">Trang Day Trips</font>"
			strDestinationLink = "<a href=""/trang-day-trips.asp"">Trang Day Trips</a>"
			strDestinationPath = "trang-day-trips"
		Case 55 'Chumphon
			strDestination = "<font color=""#FE5400"">Chumphon Day Trips</font>"
			strDestinationLink = "<a href=""/chumphon-day-trips.asp"">Chumphon Day Trips</a>"
			strDestinationPath = "chumphon-day-trips"
	END SELECT
	
	IF intProductID<>"" Then
		sqlProduct = "SELECT title_en,files_name FROM tbl_product WHERE product_id=" & intProductID
		Set recProduct = Server.CreateObject ("ADODB.Recordset")
		recProduct.Open SqlProduct, Conn,adOpenStatic,adLockreadOnly
			arrProduct = recProduct.GetRows()
			strProduct = "<font color=""#FE5400"">"&arrProduct(0,0)&"</font>"
			strProductLink = "<a href=""" & "/"& strDestinationPath &"/" & arrProduct(1,0) &""">"& arrProduct(0,0) &"</a>"
		recProduct.Close
		Set recProduct = Nothing 
	End IF
	
	SELECT CASE intType
		Case 1 'Home Page
		Case 2 'Thailand Day Trips Page
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> Thailand Day Trips"
		Case 3 'Destination Page
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <a href=""/thailand-day-trips.asp"">Thailand Day Trips</a> -> " & strDestination
		Case 4 'Location Page
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <a href=""/thailand-day-trips.asp"">Thailand Day Trips</a> -> " & strDestinationLink
		Case 5 'Product Detail Page
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <a href=""/thailand-day-trips.asp"">Thailand Day Trips</a> -> " & strDestinationLink & " -> " & strProduct
		Case 6 'Tell a Freind Page
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <a href=""/thailand-day-trips.asp"">Thailand Day Trips</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Tell A Friend</font>"
		Case 7 'Search Result Page
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Search Result</font>"
		Case 8 'Thailand Day Trips
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Day Trips</font>"
		Case 9 'Recent Viewed Day Trips
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Recent Viewed Day Trips</font>"
		Case 10 'Day Trips Page
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Day Trips</font>"
		Case 11 'Write Review
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <a href=""/thailand-day-trips.asp"">Thailand Day Trips</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Write Review</font>"
		Case 12 'Read Review
			function_gen_navigator_day_trip = "<a href=""/"">Home</a> -> <a href=""/thailand-day-trips.asp"">Thailand Day Trips</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Review</font>"

	
	END SELECT
	
END FUNCTION
%>