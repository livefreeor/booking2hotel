<%
FUNCTION function_gen_navigator_activity(intProductID,intLocationID,intDestinationID,intType)
	
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
			strDestination = "<font color=""#FE5400"">Bangkok Activities</font>"
			strDestinationLink = "<a href=""/bangkok-activity.asp"">Bangkok Activities</a>"
			strDestinationPath = "bangkok-sightseeing"
		Case 31	'Phuket
			strDestination = "<font color=""#FE5400"">Phuket Activities</font>"
			strDestinationLink = "<a href=""/phuket-activity.asp"">Phuket Activities</a>"
			strDestinationPath = "phuket-sightseeing"
		Case 32 'Chiang Mai
			strDestination = "<font color=""#FE5400"">Chiang Mai Activities</font>"
			strDestinationLink = "<a href=""/chiang-mai-activity.asp"">Chiang Mai Activities</a>"
			strDestinationPath = "chiang-mai-sightseeing"
		Case 33 'Pattaya
			strDestination = "<font color=""#FE5400"">Pattaya Activities</font>"
			strDestinationLink = "<a href=""/pattaya-activity.asp"">Pattaya Activities</a>"
			strDestinationPath = "pattaya-sightseeing"
		Case 34 'Koh Samui
			strDestination = "<font color=""#FE5400"">Koh Samui Activities</font>"
			strDestinationLink = "<a href=""/koh-samui-activity.asp"">Koh Samui Activities</a>"
			strDestinationPath = "koh-samui-sightseeing"
		Case 35 'Krabi
			strDestination = "<font color=""#FE5400"">Krabi Activities</font>"
			strDestinationLink = "<a href=""/krabi-activity.asp"">Krabi Activities</a>"
			strDestinationPath = "krabi-sightseeing"
		Case 36 'Chiang Rai
			strDestination = "<font color=""#FE5400"">Chiang Rai Activities</font>"
			strDestinationLink = "<a href=""/chiang-rai-activity.asp"">Chiang Rai Activities</a>"
			strDestinationPath = "chiang-rai-sightseeing"
		Case 37 'Cha Am
			strDestination = "<font color=""#FE5400"">Cha Am Activities</font>"
			strDestinationLink = "<a href=""/cha-am-activity.asp"">Cha Am Activities</a>"
			strDestinationPath = "cha-am-sightseeing"
		Case 38 'Hua Hin
			strDestination = "<font color=""#FE5400"">Hua Hin Activities</font>"
			strDestinationLink = "<a href=""/hua-hin-activity.asp"">Hua Hin Activities</a>"
			strDestinationPath = "hua-hin-sightseeing"
		Case 42 'Rayong
			strDestination = "<font color=""#FE5400"">Rayong Activities</font>"
			strDestinationLink = "<a href=""/rayong-activity.asp"">Rayong Activities</a>"
			strDestinationPath = "rayong-sightseeing"
		Case 43 'Mae Hong Son
			strDestination = "<font color=""#FE5400"">Mae Hong Son Activities</font>"
			strDestinationLink = "<a href=""/mae-hong-son-activity.asp"">Mae Hong Son Activities</a>"
			strDestinationPath = "mae-hong-son-sightseeing"
		Case 45 'Kanchanaburi
			strDestination = "<font color=""#FE5400"">Kanchanaburi Activities</font>"
			strDestinationLink = "<a href=""/kanchanaburi-activity.asp"">Kanchanaburi Activities</a>"
			strDestinationPath = "kanchanaburi-sightseeing"
		Case 46 'Koh Chang
			strDestination = "<font color=""#FE5400"">Koh Chang Activities</font>"
			strDestinationLink = "<a href=""/koh-chang-activity.asp"">Koh Chang Activities</a>"
			strDestinationPath = "koh-chang-sightseeing"
		Case 48 'Prachuap Khiri khan
			strDestination = "<font color=""#FE5400"">Prachuap Khiri Khan Activities</font>"
			strDestinationLink = "<a href=""/prachuap-khiri-khan-activity.asp"">Prachuap Khiri Khan Activities</a>"
			strDestinationPath = "prachuap-khiri-khan-sightseeing"
		Case 49 'Koh Kood
			strDestination = "<font color=""#FE5400"">Koh Kood Activities</font>"
			strDestinationLink = "<a href=""/koh-kood-activity.asp"">Koh KoodActivities</a>"
			strDestinationPath = "koh-kood-sightseeing"
		Case 50 'Koh Samet
			strDestination = "<font color=""#FE5400"">Koh Samet Activities</font>"
			strDestinationLink = "<a href=""/koh-samet-activity.asp"">Koh Samet Activities</a>"
			strDestinationPath = "koh-samet-sightseeing"
		Case 51 'Phang Nga
			strDestination = "<font color=""#FE5400"">Phang Nga Activities</font>"
			strDestinationLink = "<a href=""/phang-nga-activity.asp"">Phang Nga Activities</a>"
			strDestinationPath = "phang-nga-sightseeing"
		Case 52 'khao Yai
			strDestination = "<font color=""#FE5400"">Khao Yai Activities</font>"
			strDestinationLink = "<a href=""/khao-yai-activity.asp"">Khao Yai Activities</a>"
			strDestinationPath = "khao-yai-sightseeing"
		Case 53 'Koh Phangan
			strDestination = "<font color=""#FE5400"">Koh Phangan Activities</font>"
			strDestinationLink = "<a href=""/koh-phangan-activity.asp"">Koh Phangan Activities</a>"
			strDestinationPath = "koh-phangan-sightseeing"
		Case 54 'Trang
			strDestination = "<font color=""#FE5400"">Trang Activities</font>"
			strDestinationLink = "<a href=""/trang-activity.asp"">Trang Activities</a>"
			strDestinationPath = "trang-sightseeing"
		Case 55 'Chumphon
			strDestination = "<font color=""#FE5400"">Chumphon Activities</font>"
			strDestinationLink = "<a href=""/chumphon-activity.asp"">Chumphon Activities</a>"
			strDestinationPath = "chumphon-sightseeing"
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
		Case 2 'Thailand Activities Page
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> Thailand Activities"
		Case 3 'Destination Page
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-activity.asp"">Thailand Activities</a> -> " & strDestination
		Case 4 'Location Page
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-activity.asp"">Thailand Activities</a> -> " & strDestinationLink
		Case 5 'Product Detail Page
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-activity.asp"">Thailand Activities</a> -> " & strDestinationLink & " -> " & strProduct
		Case 6 'Tell a Freind Page
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-activity.asp"">Thailand Activities</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Tell A Friend</font>"
		Case 7 'Search Result Page
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Search Result</font>"
		Case 8 'Thailand Activities
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Activities</font>"
		Case 9 'Recent Viewed Activities
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Recent Viewed Activities</font>"
		Case 10 'Activities Page
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Activities</font>"
		Case 11 'Write Review
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-activity.asp"">Thailand Activities</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Write Review</font>"
		Case 12 'Read Review
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> <a href=""/thailand-activity.asp"">Thailand Activities</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Review</font>"
	
		Case 13 'Thailand Golf Courses Page
			function_gen_navigator_activity = "<a href=""/"">Home</a> -> Thailand Golf Courses"
	
	END SELECT
	
END FUNCTION
%>