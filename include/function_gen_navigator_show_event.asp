<%
FUNCTION function_gen_navigator_show_event(intProductID,intLocationID,intDestinationID,intType)
	
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
			strDestination = "<font color=""#FE5400"">Bangkok Shows & Events</font>"
			strDestinationLink = "<a href=""/bangkok-show-event.asp"">Bangkok Shows & Events</a>"
			strDestinationPath = "bangkok-show-event"
		Case 31	'Phuket
			strDestination = "<font color=""#FE5400"">Phuket Shows & Events</font>"
			strDestinationLink = "<a href=""/phuket-show-event.asp"">Phuket Shows & Events</a>"
			strDestinationPath = "phuket-show-event"
		Case 32 'Chiang Mai
			strDestination = "<font color=""#FE5400"">Chiang Mai Shows & Events</font>"
			strDestinationLink = "<a href=""/chiang-mai-show-event.asp"">Chiang Mai Shows & Events</a>"
			strDestinationPath = "chiang-mai-show-event"
		Case 33 'Pattaya
			strDestination = "<font color=""#FE5400"">Pattaya Shows & Events</font>"
			strDestinationLink = "<a href=""/pattaya-show-event.asp"">Pattaya Shows & Events</a>"
			strDestinationPath = "pattaya-show-event"
		Case 34 'Koh Samui
			strDestination = "<font color=""#FE5400"">Koh Samui Shows & Events</font>"
			strDestinationLink = "<a href=""/koh-samui-show-event.asp"">Koh Samui Shows & Events</a>"
			strDestinationPath = "koh-samui-show-event"
		Case 35 'Krabi
			strDestination = "<font color=""#FE5400"">Krabi Shows & Events</font>"
			strDestinationLink = "<a href=""/krabi-show-event.asp"">Krabi Shows & Events</a>"
			strDestinationPath = "krabi-show-event"
		Case 36 'Chiang Rai
			strDestination = "<font color=""#FE5400"">Chiang Rai Shows & Events</font>"
			strDestinationLink = "<a href=""/chiang-rai-show-event.asp"">Chiang Rai Shows & Events</a>"
			strDestinationPath = "chiang-rai-show-event"
		Case 37 'Cha Am
			strDestination = "<font color=""#FE5400"">Cha Am Shows & Events</font>"
			strDestinationLink = "<a href=""/cha-am-show-event.asp"">Cha Am Shows & Events</a>"
			strDestinationPath = "cha-am-show-event"
		Case 38 'Hua Hin
			strDestination = "<font color=""#FE5400"">Hua Hin Shows & Events</font>"
			strDestinationLink = "<a href=""/hua-hin-show-event.asp"">Hua Hin Shows & Events</a>"
			strDestinationPath = "hua-hin-show-event"
		Case 42 'Rayong
			strDestination = "<font color=""#FE5400"">Rayong Shows & Events</font>"
			strDestinationLink = "<a href=""/rayong-show-event.asp"">Rayong Shows & Events</a>"
			strDestinationPath = "rayong-show-event"
		Case 43 'Mae Hong Son
			strDestination = "<font color=""#FE5400"">Mae Hong Son Shows & Events</font>"
			strDestinationLink = "<a href=""/mae-hong-son-show-event.asp"">Mae Hong Son Shows & Events</a>"
			strDestinationPath = "mae-hong-son-show-event"
		Case 45 'Kanchanaburi
			strDestination = "<font color=""#FE5400"">Kanchanaburi Shows & Events</font>"
			strDestinationLink = "<a href=""/kanchanaburi-show-event.asp"">Kanchanaburi Shows & Events</a>"
			strDestinationPath = "kanchanaburi-show-event"
		Case 46 'Koh Chang
			strDestination = "<font color=""#FE5400"">Koh Chang Shows & Events</font>"
			strDestinationLink = "<a href=""/koh-chang-show-event.asp"">Koh Chang Shows & Events</a>"
			strDestinationPath = "koh-chang-show-event"
		Case 48 'Prachuap Khiri khan
			strDestination = "<font color=""#FE5400"">Prachuap Khiri Khan Shows & Events</font>"
			strDestinationLink = "<a href=""/prachuap-khiri-khan-show-event.asp"">Prachuap Khiri Khan Shows & Events</a>"
			strDestinationPath = "prachuap-khiri-khan-show-event"
		Case 49 'Koh Kood
			strDestination = "<font color=""#FE5400"">Koh Kood Shows & Events</font>"
			strDestinationLink = "<a href=""/koh-kood-show-event.asp"">Koh KoodShows & Events</a>"
			strDestinationPath = "koh-kood-show-event"
		Case 50 'Koh Samet
			strDestination = "<font color=""#FE5400"">Koh Samet Shows & Events</font>"
			strDestinationLink = "<a href=""/koh-samet-show-event.asp"">Koh Samet Shows & Events</a>"
			strDestinationPath = "koh-samet-show-event"
		Case 51 'Phang Nga
			strDestination = "<font color=""#FE5400"">Phang Nga Shows & Events</font>"
			strDestinationLink = "<a href=""/phang-nga-show-event.asp"">Phang Nga Shows & Events</a>"
			strDestinationPath = "phang-nga-show-event"
		Case 52 'khao Yai
			strDestination = "<font color=""#FE5400"">Khao Yai Shows & Events</font>"
			strDestinationLink = "<a href=""/khao-yai-show-event.asp"">Khao Yai Shows & Events</a>"
			strDestinationPath = "khao-yai-show-event"
		Case 53 'Koh Phangan
			strDestination = "<font color=""#FE5400"">Koh Phangan Shows & Events</font>"
			strDestinationLink = "<a href=""/koh-phangan-show-event.asp"">Koh Phangan Shows & Events</a>"
			strDestinationPath = "koh-phangan-show-event"
		Case 54 'Trang
			strDestination = "<font color=""#FE5400"">Trang Shows & Events</font>"
			strDestinationLink = "<a href=""/trang-show-event.asp"">Trang Shows & Events</a>"
			strDestinationPath = "trang-show-event"
		Case 55 'Chumphon
			strDestination = "<font color=""#FE5400"">Chumphon Shows & Events</font>"
			strDestinationLink = "<a href=""/chumphon-show-event.asp"">Chumphon Shows & Events</a>"
			strDestinationPath = "chumphon-show-event"
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
		Case 2 'Thailand Shows & Events Page
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> Thailand Shows & Events"
		Case 3 'Destination Page
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <a href=""/thailand-show-event.asp"">Thailand Shows & Events</a> -> " & strDestination
		Case 4 'Location Page
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <a href=""/thailand-show-event.asp"">Thailand Shows & Events</a> -> " & strDestinationLink
		Case 5 'Product Detail Page
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <a href=""/thailand-show-event.asp"">Thailand Shows & Events</a> -> " & strDestinationLink & " -> " & strProduct
		Case 6 'Tell a Freind Page
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <a href=""/thailand-show-event.asp"">Thailand Shows & Events</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Tell A Friend</font>"
		Case 7 'Search Result Page
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Search Result</font>"
		Case 8 'Thailand Shows & Events
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Shows & Events</font>"
		Case 9 'Recent Viewed Shows & Events
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Recent Viewed Shows & Events</font>"
		Case 10 'Shows & Events Page
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Shows & Events</font>"
		Case 11 'Write Review
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <a href=""/thailand-show-event.asp"">Thailand Shows & Events</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Write Review</font>"
		Case 12 'Read Review
			function_gen_navigator_show_event = "<a href=""/"">Home</a> -> <a href=""/thailand-show-event.asp"">Thailand Shows & Events</a> -> " & strDestinationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Review</font>"

	
	END SELECT
	
END FUNCTION
%>