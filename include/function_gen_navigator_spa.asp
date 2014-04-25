<%
FUNCTION function_gen_navigator_spa(intProductID,intLocationID,intDestinationID,intType)
	
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
			strDestination = "<font color=""#FE5400"">Bangkok Spa</font>"
			strDestinationLink = "<a href=""/bangkok-spa.asp"">Bangkok Spa</a>"
			strDestinationPath = "bangkok-spa"
		Case 31	'Phuket
			strDestination = "<font color=""#FE5400"">Phuket Spa</font>"
			strDestinationLink = "<a href=""/phuket-spa.asp"">Phuket Spa</a>"
			strDestinationPath = "phuket-spa"
		Case 32 'Chiang Mai
			strDestination = "<font color=""#FE5400"">Chiang Mai Spa</font>"
			strDestinationLink = "<a href=""/chiang-mai-spa.asp"">Chiang Mai Spa</a>"
			strDestinationPath = "chiang-mai-spa"
		Case 33 'Pattaya
			strDestination = "<font color=""#FE5400"">Pattaya Spa</font>"
			strDestinationLink = "<a href=""/pattaya-spa.asp"">Pattaya Spa</a>"
			strDestinationPath = "pattaya-spa"
		Case 34 'Koh Samui
			strDestination = "<font color=""#FE5400"">Koh Samui Spa</font>"
			strDestinationLink = "<a href=""/koh-samui-spa.asp"">Koh Samui Spa</a>"
			strDestinationPath = "koh-samui-spa"
		Case 35 'Krabi
			strDestination = "<font color=""#FE5400"">Krabi Spa</font>"
			strDestinationLink = "<a href=""/krabi-spa.asp"">Krabi Spa</a>"
			strDestinationPath = "krabi-spa"
		Case 36 'Chiang Rai
			strDestination = "<font color=""#FE5400"">Chiang Rai Spa</font>"
			strDestinationLink = "<a href=""/chiang-rai-spa.asp"">Chiang Rai Spa</a>"
			strDestinationPath = "chiang-rai-spa"
		Case 37 'Cha Am
			strDestination = "<font color=""#FE5400"">Cha Am Spa</font>"
			strDestinationLink = "<a href=""/cha-am-spa.asp"">Cha Am Spa</a>"
			strDestinationPath = "cha-am-spa"
		Case 38 'Hua Hin
			strDestination = "<font color=""#FE5400"">Hua Hin Spa</font>"
			strDestinationLink = "<a href=""/hua-hin-spa.asp"">Hua Hin Spa</a>"
			strDestinationPath = "hua-hin-spa"
		Case 42 'Rayong
			strDestination = "<font color=""#FE5400"">Rayong Spa</font>"
			strDestinationLink = "<a href=""/rayong-spa.asp"">Rayong Spa</a>"
			strDestinationPath = "rayong-spa"
		Case 43 'Mae Hong Son
			strDestination = "<font color=""#FE5400"">Mae Hong Son Spa</font>"
			strDestinationLink = "<a href=""/mae-hong-son-spa.asp"">Mae Hong Son Spa</a>"
			strDestinationPath = "mae-hong-son-spa"
		Case 45 'Kanchanaburi
			strDestination = "<font color=""#FE5400"">Kanchanaburi Spa</font>"
			strDestinationLink = "<a href=""/kanchanaburi-spa.asp"">Kanchanaburi Spa</a>"
			strDestinationPath = "kanchanaburi-spa"
		Case 46 'Koh Chang
			strDestination = "<font color=""#FE5400"">Koh Chang Spa</font>"
			strDestinationLink = "<a href=""/koh-chang-spa.asp"">Koh Chang Spa</a>"
			strDestinationPath = "koh-chang-spa"
		Case 48 'Prachuap Khiri khan
			strDestination = "<font color=""#FE5400"">Prachuap Khiri Khan Spa</font>"
			strDestinationLink = "<a href=""/prachuap-khiri-khan-spa.asp"">Prachuap Khiri Khan Spa</a>"
			strDestinationPath = "prachuap-khiri-khan-spa"
		Case 49 'Koh Kood
			strDestination = "<font color=""#FE5400"">Koh Kood Spa</font>"
			strDestinationLink = "<a href=""/koh-kood-spa.asp"">Koh KoodSpa</a>"
			strDestinationPath = "koh-kood-spa"
		Case 50 'Koh Samet
			strDestination = "<font color=""#FE5400"">Koh Samet Spa</font>"
			strDestinationLink = "<a href=""/koh-samet-spa.asp"">Koh Samet Spa</a>"
			strDestinationPath = "koh-samet-spa"
		Case 51 'Phang Nga
			strDestination = "<font color=""#FE5400"">Phang Nga Spa</font>"
			strDestinationLink = "<a href=""/phang-nga-spa.asp"">Phang Nga Spa</a>"
			strDestinationPath = "phang-nga-spa"
		Case 52 'khao Yai
			strDestination = "<font color=""#FE5400"">Khao Yai Spa</font>"
			strDestinationLink = "<a href=""/khao-yai-spa.asp"">Khao Yai Spa</a>"
			strDestinationPath = "khao-yai-spa"
		Case 53 'Koh Phangan
			strDestination = "<font color=""#FE5400"">Koh Phangan Spa</font>"
			strDestinationLink = "<a href=""/koh-phangan-spa.asp"">Koh Phangan Spa</a>"
			strDestinationPath = "koh-phangan-spa"
		Case 54 'Trang
			strDestination = "<font color=""#FE5400"">Trang Spa</font>"
			strDestinationLink = "<a href=""/trang-spa.asp"">Trang Spa</a>"
			strDestinationPath = "trang-spa"
		Case 55 'Chumphon
			strDestination = "<font color=""#FE5400"">Chumphon Spa</font>"
			strDestinationLink = "<a href=""/chumphon-spa.asp"">Chumphon Spa</a>"
			strDestinationPath = "chumphon-spa"
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
		Case 2 'Thailand Spa Page
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> Thailand Spa"
		Case 3 'Destination Page
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <a href=""/thailand-spa.asp"">Thailand Spa</a> -> " & strDestination
		Case 4 'Location Page
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <a href=""/thailand-spa.asp"">Thailand Spa</a> -> " & strDestinationLink
		Case 5 'Product Detail Page
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <a href=""/thailand-spa.asp"">Thailand Spa</a> -> " & strDestinationLink & " -> " & strProduct
		Case 6 'Tell a Freind Page
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <a href=""/thailand-spa.asp"">Thailand Spa</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Tell A Friend</font>"
		Case 7 'Search Result Page
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Search Result</font>"
		Case 8 'Thailand Spa
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Spa</font>"
		Case 9 'Recent Viewed Spa
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Recent Viewed Spa</font>"
		Case 10 'Spa Page
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Spa</font>"
		Case 11 'Write Review
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <a href=""/thailand-spa.asp"">Thailand Spa</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Write Review</font>"
		Case 12 'Read Review
			function_gen_navigator_spa = "<a href=""/"">Home</a> -> <a href=""/thailand-spa.asp"">Thailand Spa</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Review</font>"

	
	END SELECT
	
END FUNCTION
%>