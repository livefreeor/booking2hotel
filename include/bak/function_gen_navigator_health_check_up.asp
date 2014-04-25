<%
FUNCTION function_gen_navigator_health_check_up(intProductID,intLocationID,intDestinationID,intType)
	
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
			strDestination = "<font color=""#FE5400"">Bangkok Medical Check Up</font>"
			strDestinationLink = "<a href=""/bangkok-health-check-up.asp"">Bangkok Medical Check Up</a>"
			strDestinationPath = "bangkok-health-check-up"
		Case 31	'Phuket
			strDestination = "<font color=""#FE5400"">Phuket Medical Check Up</font>"
			strDestinationLink = "<a href=""/phuket-health-check-up.asp"">Phuket Medical Check Up</a>"
			strDestinationPath = "phuket-health-check-up"
		Case 32 'Chiang Mai
			strDestination = "<font color=""#FE5400"">Chiang Mai Medical Check Up</font>"
			strDestinationLink = "<a href=""/chiang-mai-health-check-up.asp"">Chiang Mai Medical Check Up</a>"
			strDestinationPath = "chiang-mai-health-check-up"
		Case 33 'Pattaya
			strDestination = "<font color=""#FE5400"">Pattaya Medical Check Up</font>"
			strDestinationLink = "<a href=""/pattaya-health-check-up.asp"">Pattaya Medical Check Up</a>"
			strDestinationPath = "pattaya-health-check-up"
		Case 34 'Koh Samui
			strDestination = "<font color=""#FE5400"">Koh Samui Medical Check Up</font>"
			strDestinationLink = "<a href=""/koh-samui-health-check-up.asp"">Koh Samui Medical Check Up</a>"
			strDestinationPath = "koh-samui-health-check-up"
		Case 35 'Krabi
			strDestination = "<font color=""#FE5400"">Krabi Medical Check Up</font>"
			strDestinationLink = "<a href=""/krabi-health-check-up.asp"">Krabi Medical Check Up</a>"
			strDestinationPath = "krabi-health-check-up"
		Case 36 'Chiang Rai
			strDestination = "<font color=""#FE5400"">Chiang Rai Medical Check Up</font>"
			strDestinationLink = "<a href=""/chiang-rai-health-check-up.asp"">Chiang Rai Medical Check Up</a>"
			strDestinationPath = "chiang-rai-health-check-up"
		Case 37 'Cha Am
			strDestination = "<font color=""#FE5400"">Cha Am Medical Check Up</font>"
			strDestinationLink = "<a href=""/cha-am-health-check-up.asp"">Cha Am Medical Check Up</a>"
			strDestinationPath = "cha-am-health-check-up"
		Case 38 'Hua Hin
			strDestination = "<font color=""#FE5400"">Hua Hin Medical Check Up</font>"
			strDestinationLink = "<a href=""/hua-hin-health-check-up.asp"">Hua Hin Medical Check Up</a>"
			strDestinationPath = "hua-hin-health-check-up"
		Case 42 'Rayong
			strDestination = "<font color=""#FE5400"">Rayong Medical Check Up</font>"
			strDestinationLink = "<a href=""/rayong-health-check-up.asp"">Rayong Medical Check Up</a>"
			strDestinationPath = "rayong-health-check-up"
		Case 43 'Mae Hong Son
			strDestination = "<font color=""#FE5400"">Mae Hong Son Medical Check Up</font>"
			strDestinationLink = "<a href=""/mae-hong-son-health-check-up.asp"">Mae Hong Son Medical Check Up</a>"
			strDestinationPath = "mae-hong-son-health-check-up"
		Case 45 'Kanchanaburi
			strDestination = "<font color=""#FE5400"">Kanchanaburi Medical Check Up</font>"
			strDestinationLink = "<a href=""/kanchanaburi-health-check-up.asp"">Kanchanaburi Medical Check Up</a>"
			strDestinationPath = "kanchanaburi-health-check-up"
		Case 46 'Koh Chang
			strDestination = "<font color=""#FE5400"">Koh Chang Medical Check Up</font>"
			strDestinationLink = "<a href=""/koh-chang-health-check-up.asp"">Koh Chang Medical Check Up</a>"
			strDestinationPath = "koh-chang-health-check-up"
		Case 48 'Prachuap Khiri khan
			strDestination = "<font color=""#FE5400"">Prachuap Khiri Khan Medical Check Up</font>"
			strDestinationLink = "<a href=""/prachuap-khiri-khan-health-check-up.asp"">Prachuap Khiri Khan Medical Check Up</a>"
			strDestinationPath = "prachuap-khiri-khan-health-check-up"
		Case 49 'Koh Kood
			strDestination = "<font color=""#FE5400"">Koh Kood Medical Check Up</font>"
			strDestinationLink = "<a href=""/koh-kood-health-check-up.asp"">Koh KoodMedical Check Up</a>"
			strDestinationPath = "koh-kood-health-check-up"
		Case 50 'Koh Samet
			strDestination = "<font color=""#FE5400"">Koh Samet Medical Check Up</font>"
			strDestinationLink = "<a href=""/koh-samet-health-check-up.asp"">Koh Samet Medical Check Up</a>"
			strDestinationPath = "koh-samet-health-check-up"
		Case 51 'Phang Nga
			strDestination = "<font color=""#FE5400"">Phang Nga Medical Check Up</font>"
			strDestinationLink = "<a href=""/phang-nga-health-check-up.asp"">Phang Nga Medical Check Up</a>"
			strDestinationPath = "phang-nga-health-check-up"
		Case 52 'khao Yai
			strDestination = "<font color=""#FE5400"">Khao Yai Medical Check Up</font>"
			strDestinationLink = "<a href=""/khao-yai-health-check-up.asp"">Khao Yai Medical Check Up</a>"
			strDestinationPath = "khao-yai-health-check-up"
		Case 53 'Koh Phangan
			strDestination = "<font color=""#FE5400"">Koh Phangan Medical Check Up</font>"
			strDestinationLink = "<a href=""/koh-phangan-health-check-up.asp"">Koh Phangan Medical Check Up</a>"
			strDestinationPath = "koh-phangan-health-check-up"
		Case 54 'Trang
			strDestination = "<font color=""#FE5400"">Trang Medical Check Up</font>"
			strDestinationLink = "<a href=""/trang-health-check-up.asp"">Trang Medical Check Up</a>"
			strDestinationPath = "trang-health-check-up"
		Case 55 'Chumphon
			strDestination = "<font color=""#FE5400"">Chumphon Medical Check Up</font>"
			strDestinationLink = "<a href=""/chumphon-health-check-up.asp"">Chumphon Medical Check Up</a>"
			strDestinationPath = "chumphon-health-check-up"
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
		Case 2 'Thailand Medical Check Up Page
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> Thailand Medical Check Up"
		Case 3 'Destination Page
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <a href=""/thailand-health-check-up.asp"">Thailand Medical Check Up</a> -> " & strDestination
		Case 4 'Location Page
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <a href=""/thailand-health-check-up.asp"">Thailand Medical Check Up</a> -> " & strDestinationLink
		Case 5 'Product Detail Page
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <a href=""/thailand-health-check-up.asp"">Thailand Medical Check Up</a> -> " & strDestinationLink & " -> " & strProduct
		Case 6 'Tell a Freind Page
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <a href=""/thailand-health-check-up.asp"">Thailand Medical Check Up</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Tell A Friend</font>"
		Case 7 'Search Result Page
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Search Result</font>"
		Case 8 'Thailand Medical Check Up
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Medical Check Up</font>"
		Case 9 'Recent Viewed Medical Check Up
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Recent Viewed Medical Check Up</font>"
		Case 10 'Medical Check Up Page
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Medical Check Up</font>"
		Case 11 'Write Review
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <a href=""/thailand-health-check-up.asp"">Thailand Medical Check Up</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Write Review</font>"
		Case 12 'Read Review
			function_gen_navigator_health_check_up = "<a href=""/"">Home</a> -> <a href=""/thailand-health-check-up.asp"">Thailand Medical Check Up</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Review</font>"

	
	END SELECT
	
END FUNCTION
%>