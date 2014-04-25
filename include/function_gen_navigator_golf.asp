<%
FUNCTION function_gen_navigator_golf(intProductID,intLocationID,intDestinationID,intType)
	
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
			strDestination = "<font color=""#FE5400"">Bangkok Golf</font>"
			strDestinationLink = "<a href=""/bangkok-golf.asp"">Bangkok Golf</a>"
			strDestinationPath = "bangkok-golf"
		Case 31	'Phuket
			strDestination = "<font color=""#FE5400"">Phuket Golf</font>"
			strDestinationLink = "<a href=""/phuket-golf.asp"">Phuket Golf</a>"
			strDestinationPath = "phuket-golf"
		Case 32 'Chiang Mai
			strDestination = "<font color=""#FE5400"">Chiang Mai Golf</font>"
			strDestinationLink = "<a href=""/chiang-mai-golf.asp"">Chiang Mai Golf</a>"
			strDestinationPath = "chiang-mai-golf"
		Case 33 'Pattaya
			strDestination = "<font color=""#FE5400"">Pattaya Golf</font>"
			strDestinationLink = "<a href=""/pattaya-golf.asp"">Pattaya Golf</a>"
			strDestinationPath = "pattaya-golf"
		Case 34 'Koh Samui
			strDestination = "<font color=""#FE5400"">Koh Samui Golf</font>"
			strDestinationLink = "<a href=""/koh-samui-golf.asp"">Koh Samui Golf</a>"
			strDestinationPath = "koh-samui-golf"
		Case 35 'Krabi
			strDestination = "<font color=""#FE5400"">Krabi Golf</font>"
			strDestinationLink = "<a href=""/krabi-golf.asp"">Krabi Golf</a>"
			strDestinationPath = "krabi-golf"
		Case 36 'Chiang Rai
			strDestination = "<font color=""#FE5400"">Chiang Rai Golf</font>"
			strDestinationLink = "<a href=""/chiang-rai-golf.asp"">Chiang Rai Golf</a>"
			strDestinationPath = "chiang-rai-golf"
		Case 37 'Cha Am
			strDestination = "<font color=""#FE5400"">Cha Am Golf</font>"
			strDestinationLink = "<a href=""/cha-am-golf.asp"">Cha Am Golf</a>"
			strDestinationPath = "cha-am-golf"
		Case 38 'Hua Hin
			strDestination = "<font color=""#FE5400"">Hua Hin Golf</font>"
			strDestinationLink = "<a href=""/hua-hin-golf.asp"">Hua Hin Golf</a>"
			strDestinationPath = "hua-hin-golf"
		Case 42 'Rayong
			strDestination = "<font color=""#FE5400"">Rayong Golf</font>"
			strDestinationLink = "<a href=""/rayong-golf.asp"">Rayong Golf</a>"
			strDestinationPath = "rayong-golf"
		Case 43 'Mae Hong Son
			strDestination = "<font color=""#FE5400"">Mae Hong Son Golf</font>"
			strDestinationLink = "<a href=""/mae-hong-son-golf.asp"">Mae Hong Son Golf</a>"
			strDestinationPath = "mae-hong-son-golf"
		Case 45 'Kanchanaburi
			strDestination = "<font color=""#FE5400"">Kanchanaburi Golf</font>"
			strDestinationLink = "<a href=""/kanchanaburi-golf.asp"">Kanchanaburi Golf</a>"
			strDestinationPath = "kanchanaburi-golf"
		Case 46 'Koh Chang
			strDestination = "<font color=""#FE5400"">Koh Chang Golf</font>"
			strDestinationLink = "<a href=""/koh-chang-golf.asp"">Koh Chang Golf</a>"
			strDestinationPath = "koh-chang-golf"
		Case 50 'Koh Samet
			strDestination = "<font color=""#FE5400"">Koh Samet Golf</font>"
			strDestinationLink = "<a href=""/koh-samet-golf.asp"">Koh Samet Golf</a>"
			strDestinationPath = "koh-samet-golf"
		Case 51 'Phang Nga
			strDestination = "<font color=""#FE5400"">Phang Nga Golf</font>"
			strDestinationLink = "<a href=""/phang-nga-golf.asp"">Phang Nga Golf</a>"
			strDestinationPath = "phang-nga-golf"
		Case 52 'Khao Yai
			strDestination = "<font color=""#FE5400"">Khao Yai Golf</font>"
			strDestinationLink = "<a href=""/khao-yai-golf.asp"">Khao Yai Golf</a>"
			strDestinationPath = "khao-yai-golf"
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
		Case 2 'Thailand golf Page
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> Thailand golf"
		Case 3 'Destination Page
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <a href=""/thailand-golf-courses.asp"">Thailand Golf</a> -> " & strDestination
		Case 4 'Location Page
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <a href=""/thailand-golf-courses.asp"">Thailand Golf</a> -> " & strDestinationLink & " -> " & strLocation
		Case 5 'Product Detail Page
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <a href=""/thailand-golf-courses.asp"">Thailand Golf</a> -> " & strDestinationLink & " -> " & strProduct
		Case 6 'Tell a Freind Page
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <a href=""/thailand-golf-courses.asp"">Thailand Golf</a> -> " & strDestinationLink & " -> " & strLocationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Tell A Friend</font>"
		Case 7 'Search Result Page
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Search Result</font>"
		Case 8 'Thailand golf
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand golf</font>"
		Case 9 'Recent Viewed golf
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Recent Viewed golf</font>"
		Case 10 'golf Page
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand golf</font>"
		Case 11 'Write Review
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <a href=""/thailand-golf-courses.asp"">Thailand Golf</a> -> " & strDestinationLink &  " -> " & strProductLink & " -> <font color=""#FE5400"">Write Review</font>"
		Case 12 'Read Review
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> <a href=""/thailand-golf-courses.asp"">Thailand Golf</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Review</font>"
		Case 13 'Thailand Golf Courses Page
			function_gen_navigator_golf = "<a href=""/"">Home</a> -> Thailand Golf Courses"
	
	END SELECT
	
END FUNCTION
%>