<%
FUNCTION function_gen_navigator_water_activity(intProductID,intLocationID,intDestinationID,intType)
	
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
			strDestination = "<font color=""#FE5400"">Bangkok Water Activities</font>"
			strDestinationLink = "<a href=""/bangkok-water-activity.asp"">Bangkok Water Activities</a>"
			strDestinationPath = "bangkok-day-trips"
		Case 31	'Phuket
			strDestination = "<font color=""#FE5400"">Phuket Water Activities</font>"
			strDestinationLink = "<a href=""/phuket-water-activity.asp"">Phuket Water Activities</a>"
			strDestinationPath = "phuket-day-trips"
		Case 32 'Chiang Mai
			strDestination = "<font color=""#FE5400"">Chiang Mai Water Activities</font>"
			strDestinationLink = "<a href=""/chiang-mai-water-activity.asp"">Chiang Mai Water Activities</a>"
			strDestinationPath = "chiang-mai-day-trips"
		Case 33 'Pattaya
			strDestination = "<font color=""#FE5400"">Pattaya Water Activities</font>"
			strDestinationLink = "<a href=""/pattaya-water-activity.asp"">Pattaya Water Activities</a>"
			strDestinationPath = "pattaya-day-trips"
		Case 34 'Koh Samui
			strDestination = "<font color=""#FE5400"">Koh Samui Water Activities</font>"
			strDestinationLink = "<a href=""/koh-samui-water-activity.asp"">Koh Samui Water Activities</a>"
			strDestinationPath = "koh-samui-day-trips"
		Case 35 'Krabi
			strDestination = "<font color=""#FE5400"">Krabi Water Activities</font>"
			strDestinationLink = "<a href=""/krabi-water-activity.asp"">Krabi Water Activities</a>"
			strDestinationPath = "krabi-day-trips"
		Case 36 'Chiang Rai
			strDestination = "<font color=""#FE5400"">Chiang Rai Water Activities</font>"
			strDestinationLink = "<a href=""/chiang-rai-water-activity.asp"">Chiang Rai Water Activities</a>"
			strDestinationPath = "chiang-rai-day-trips"
		Case 37 'Cha Am
			strDestination = "<font color=""#FE5400"">Cha Am Water Activities</font>"
			strDestinationLink = "<a href=""/cha-am-water-activity.asp"">Cha Am Water Activities</a>"
			strDestinationPath = "cha-am-day-trips"
		Case 38 'Hua Hin
			strDestination = "<font color=""#FE5400"">Hua Hin Water Activities</font>"
			strDestinationLink = "<a href=""/hua-hin-water-activity.asp"">Hua Hin Water Activities</a>"
			strDestinationPath = "hua-hin-day-trips"
		Case 42 'Rayong
			strDestination = "<font color=""#FE5400"">Rayong Water Activities</font>"
			strDestinationLink = "<a href=""/rayong-water-activity.asp"">Rayong Water Activities</a>"
			strDestinationPath = "rayong-day-trips"
		Case 43 'Mae Hong Son
			strDestination = "<font color=""#FE5400"">Mae Hong Son Water Activities</font>"
			strDestinationLink = "<a href=""/mae-hong-son-water-activity.asp"">Mae Hong Son Water Activities</a>"
			strDestinationPath = "mae-hong-son-day-trips"
		Case 45 'Kanchanaburi
			strDestination = "<font color=""#FE5400"">Kanchanaburi Water Activities</font>"
			strDestinationLink = "<a href=""/kanchanaburi-water-activity.asp"">Kanchanaburi Water Activities</a>"
			strDestinationPath = "kanchanaburi-day-trips"
		Case 46 'Koh Chang
			strDestination = "<font color=""#FE5400"">Koh Chang Water Activities</font>"
			strDestinationLink = "<a href=""/koh-chang-water-activity.asp"">Koh Chang Water Activities</a>"
			strDestinationPath = "koh-chang-day-trips"
		Case 48 'Prachuap Khiri khan
			strDestination = "<font color=""#FE5400"">Prachuap Khiri Khan Water Activities</font>"
			strDestinationLink = "<a href=""/prachuap-khiri-khan-water-activity.asp"">Prachuap Khiri Khan Water Activities</a>"
			strDestinationPath = "prachuap-khiri-khan-day-trips"
		Case 49 'Koh Kood
			strDestination = "<font color=""#FE5400"">Koh Kood Water Activities</font>"
			strDestinationLink = "<a href=""/koh-kood-water-activity.asp"">Koh KoodWater Activities</a>"
			strDestinationPath = "koh-kood-day-trips"
		Case 50 'Koh Samet
			strDestination = "<font color=""#FE5400"">Koh Samet Water Activities</font>"
			strDestinationLink = "<a href=""/koh-samet-water-activity.asp"">Koh Samet Water Activities</a>"
			strDestinationPath = "koh-samet-day-trips"
		Case 51 'Phang Nga
			strDestination = "<font color=""#FE5400"">Phang Nga Water Activities</font>"
			strDestinationLink = "<a href=""/phang-nga-water-activity.asp"">Phang Nga Water Activities</a>"
			strDestinationPath = "phang-nga-day-trips"
		Case 52 'khao Yai
			strDestination = "<font color=""#FE5400"">Khao Yai Water Activities</font>"
			strDestinationLink = "<a href=""/khao-yai-water-activity.asp"">Khao Yai Water Activities</a>"
			strDestinationPath = "khao-yai-day-trips"
		Case 53 'Koh Phangan
			strDestination = "<font color=""#FE5400"">Koh Phangan Water Activities</font>"
			strDestinationLink = "<a href=""/koh-phangan-water-activity.asp"">Koh Phangan Water Activities</a>"
			strDestinationPath = "koh-phangan-day-trips"
		Case 54 'Trang
			strDestination = "<font color=""#FE5400"">Trang Water Activities</font>"
			strDestinationLink = "<a href=""/trang-water-activity.asp"">Trang Water Activities</a>"
			strDestinationPath = "trang-day-trips"
		Case 55 'Chumphon
			strDestination = "<font color=""#FE5400"">Chumphon Water Activities</font>"
			strDestinationLink = "<a href=""/chumphon-water-activity.asp"">Chumphon Water Activities</a>"
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
		Case 2 'Thailand Water Activities Page
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> Thailand Water Activities"
		Case 3 'Destination Page
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-water-activity.asp"">Thailand Water Activities</a> -> " & strDestination
		Case 4 'Location Page
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-water-activity.asp"">Thailand Water Activities</a> -> " & strDestinationLink
		Case 5 'Product Detail Page
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-water-activity.asp"">Thailand Water Activities</a> -> " & strDestinationLink & " -> " & strProduct
		Case 6 'Tell a Freind Page
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-water-activity.asp"">Thailand Water Activities</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Tell A Friend</font>"
		Case 7 'Search Result Page
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Search Result</font>"
		Case 8 'Thailand Water Activities
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Water Activities</font>"
		Case 9 'Recent Viewed Water Activities
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Recent Viewed Water Activities</font>"
		Case 10 'Water Activities Page
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Water Activities</font>"
		Case 11 'Write Review
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-water-activity.asp"">Thailand Water Activities</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Write Review</font>"
		Case 12 'Read Review
			function_gen_navigator_water_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-water-activity.asp"">Thailand Water Activities</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Review</font>"

	
	END SELECT
	
END FUNCTION
%>