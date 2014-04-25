<%
FUNCTION function_gen_navigator(intProductID,intLocationID,intDestinationID,intType)
	
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
			strDestination = "<font color=""#FE5400"">Bangkok Hotels</font>"
			strDestinationLink = "<a href=""/bangkok-hotels.asp"">Bangkok Hotels</a>"
			strDestinationPath = "bangkok-hotels"
		Case 31	'Phuket
			strDestination = "<font color=""#FE5400"">Phuket Hotels</font>"
			strDestinationLink = "<a href=""/phuket-hotels.asp"">Phuket Hotels</a>"
			strDestinationPath = "phuket-hotels"
		Case 32 'Chiang Mai
			strDestination = "<font color=""#FE5400"">Chiang Mai Hotels</font>"
			strDestinationLink = "<a href=""/chiang-mai-hotels.asp"">Chiang Mai Hotels</a>"
			strDestinationPath = "chiang-mai-hotels"
		Case 33 'Pattaya
			strDestination = "<font color=""#FE5400"">Pattaya Hotels</font>"
			strDestinationLink = "<a href=""/pattaya-hotels.asp"">Pattaya Hotels</a>"
			strDestinationPath = "pattaya-hotels"
		Case 34 'Koh Samui
			strDestination = "<font color=""#FE5400"">Koh Samui Hotels</font>"
			strDestinationLink = "<a href=""/koh-samui-hotels.asp"">Koh Samui Hotels</a>"
			strDestinationPath = "koh-samui-hotels"
		Case 35 'Krabi
			strDestination = "<font color=""#FE5400"">Krabi Hotels</font>"
			strDestinationLink = "<a href=""/krabi-hotels.asp"">Krabi Hotels</a>"
			strDestinationPath = "krabi-hotels"
		Case 36 'Chiang Rai
			strDestination = "<font color=""#FE5400"">Chiang Rai Hotels</font>"
			strDestinationLink = "<a href=""/chiang-rai-hotels.asp"">Chiang Rai Hotels</a>"
			strDestinationPath = "chiang-rai-hotels"
		Case 37 'Cha Am
			strDestination = "<font color=""#FE5400"">Cha Am Hotels</font>"
			strDestinationLink = "<a href=""/cha-am-hotels.asp"">Cha Am Hotels</a>"
			strDestinationPath = "cha-am-hotels"
		Case 38 'Hua Hin
			strDestination = "<font color=""#FE5400"">Hua Hin Hotels</font>"
			strDestinationLink = "<a href=""/hua-hin-hotels.asp"">Hua Hin Hotels</a>"
			strDestinationPath = "hua-hin-hotels"
		Case 40 'Phitsanulok
			strDestination = "<font color=""#FE5400"">Phitsanulok Hotels</font>"
			strDestinationLink = "<a href=""/phitsanulok-hotels.asp"">Phitsanulok Hotels</a>"
			strDestinationPath = "phitsanulok-hotels"
		Case 42 'Rayong
			strDestination = "<font color=""#FE5400"">Rayong Hotels</font>"
			strDestinationLink = "<a href=""/rayong-hotels.asp"">Rayong Hotels</a>"
			strDestinationPath = "rayong-hotels"
		Case 43 'Mae Hong Son
			strDestination = "<font color=""#FE5400"">Mae Hong Son Hotels</font>"
			strDestinationLink = "<a href=""/mae-hong-son-hotels.asp"">Mae Hong Son Hotels</a>"
			strDestinationPath = "mae-hong-son-hotels"
		Case 44 'Ayutthaya
			strDestination = "<font color=""#FE5400"">Ayutthaya Hotels</font>"
			strDestinationLink = "<a href=""/ayutthaya-hotels.asp"">Ayutthaya Hotels</a>"
			strDestinationPath = "ayutthaya-hotels"
		Case 45 'Kanchanaburi
			strDestination = "<font color=""#FE5400"">Kanchanaburi Hotels</font>"
			strDestinationLink = "<a href=""/kanchanaburi-hotels.asp"">Kanchanaburi Hotels</a>"
			strDestinationPath = "kanchanaburi-hotels"
		Case 46 'Koh Chang
			strDestination = "<font color=""#FE5400"">Koh Chang Hotels</font>"
			strDestinationLink = "<a href=""/koh-chang-hotels.asp"">Koh Chang Hotels</a>"
			strDestinationPath = "koh-chang-hotels"
		Case 48 'Prachuap Khiri khan
			strDestination = "<font color=""#FE5400"">Prachuap Khiri Khan Hotels</font>"
			strDestinationLink = "<a href=""/prachuap-khiri-khan-hotels.asp"">Prachuap Khiri Khan Hotels</a>"
			strDestinationPath = "prachuap-khiri-khan-hotels"
		Case 49 'Koh Kood
			strDestination = "<font color=""#FE5400"">Koh Kood Hotels</font>"
			strDestinationLink = "<a href=""/koh-kood-hotels.asp"">Koh KoodHotels</a>"
			strDestinationPath = "koh-kood-hotels"
		Case 50 'Koh Samet
			strDestination = "<font color=""#FE5400"">Koh Samet Hotels</font>"
			strDestinationLink = "<a href=""/koh-samet-hotels.asp"">Koh Samet Hotels</a>"
			strDestinationPath = "koh-samet-hotels"
		Case 51 'Phang Nga
			strDestination = "<font color=""#FE5400"">Phang Nga Hotels</font>"
			strDestinationLink = "<a href=""/phang-nga-hotels.asp"">Phang Nga Hotels</a>"
			strDestinationPath = "phang-nga-hotels"
		Case 52 'khao Yai
			strDestination = "<font color=""#FE5400"">Khao Yai Hotels</font>"
			strDestinationLink = "<a href=""/khao-yai-hotels.asp"">Khao Yai Hotels</a>"
			strDestinationPath = "khao-yai-hotels"
		Case 53 'Koh Phangan
			strDestination = "<font color=""#FE5400"">Koh Phangan Hotels</font>"
			strDestinationLink = "<a href=""/koh-phangan-hotels.asp"">Koh Phangan Hotels</a>"
			strDestinationPath = "koh-phangan-hotels"
		Case 54 'Trang
			strDestination = "<font color=""#FE5400"">Trang Hotels</font>"
			strDestinationLink = "<a href=""/trang-hotels.asp"">Trang Hotels</a>"
			strDestinationPath = "trang-hotels"
		Case 55 'Chumphon
			strDestination = "<font color=""#FE5400"">Chumphon Hotels</font>"
			strDestinationLink = "<a href=""/chumphon-hotels.asp"">Chumphon Hotels</a>"
			strDestinationPath = "chumphon-hotels"
		Case 57 'Nakornnayok
			strDestination = "<font color=""#FE5400"">Nakornnayok Hotels</font>"
			strDestinationLink = "<a href=""/nakornnayok-hotels.asp"">Nakornnayok Hotels</a>"
			strDestinationPath = "nakornnayok-hotels"
		Case 58 'Chanthaburi
			strDestination = "<font color=""#FE5400"">Chanthaburi Hotels</font>"
			strDestinationLink = "<a href=""/chanthaburi-hotels.asp"">Chanthaburi Hotels</a>"
			strDestinationPath = "chanthaburi-hotels"
		Case 60 'Lamphun
			strDestination = "<font color=""#FE5400"">Lamphun Hotels</font>"
			strDestinationLink = "<a href=""/lamphun-hotels.asp"">Lamphun Hotels</a>"
			strDestinationPath = "lamphun-hotels"
		Case 61 'Phetchaburi
			strDestination = "<font color=""#FE5400"">Phetchaburi Hotels</font>"
			strDestinationLink = "<a href=""/phetchaburi-hotels.asp"">Phetchaburi Hotels</a>"
			strDestinationPath = "phetchaburi-hotels"
		Case 62 'Ratchaburi
			strDestination = "<font color=""#FE5400"">Ratchaburi Hotels</font>"
			strDestinationLink = "<a href=""/ratchaburi-hotels.asp"">Ratchaburi Hotels</a>"
			strDestinationPath = "ratchaburi-hotels"
		Case 63 'Nakhonratchasima
			strDestination = "<font color=""#FE5400"">Nakhonratchasima Hotels</font>"
			strDestinationLink = "<a href=""/Nakhonratchasima-hotels.asp"">Nakhonratchasima Hotels</a>"
			strDestinationPath = "Nakhonratchasima-hotels"
		Case 65 'Koh Tao
			strDestination = "<font color=""#FE5400"">Koh Tao Hotels</font>"
			strDestinationLink = "<a href=""/koh-tao-hotels.asp"">Koh Tao Hotels</a>"
			strDestinationPath = "koh-tao-hotels"
		Case 66 'Phetchabun
			strDestination = "<font color=""#FE5400"">Phetchabun Hotels</font>"
			strDestinationLink = "<a href=""/phetchabun-hotels.asp"">Phetchabun Hotels</a>"
			strDestinationPath = "phetchabun-hotels"
		Case 67 'Uthai Thani
			strDestination = "<font color=""#FE5400"">Uthai Thani Hotels</font>"
			strDestinationLink = "<a href=""/uthai-thani-hotels.asp"">Uthai Thani Hotels</a>"
			strDestinationPath = "uthai-thani-hotels"
		Case 68 'Khonkaen
			strDestination = "<font color=""#FE5400"">Khonkaen Hotels</font>"
			strDestinationLink = "<a href=""/khonkaen-hotels.asp"">Khonkaen Hotels</a>"
			strDestinationPath = "khonkaen-hotels"
		Case 69 'NakhonSiThammarat
			strDestination = "<font color=""#FE5400"">Nakhon Si Thammarat Hotels</font>"
			strDestinationLink = "<a href=""/NakhonSiThammarat-hotels.asp"">NakhonSiThammarat Hotels</a>"
			strDestinationPath = "NakhonSiThammarat-hotels"
		Case 70 'Songkhla
			strDestination = "<font color=""#FE5400"">Songkhla Hotels</font>"
			strDestinationLink = "<a href=""/Songkhla-hotels.asp"">Songkhla Hotels</a>"
			strDestinationPath = "Songkhla-hotels"
		Case 71 'Hat Yai 
			strDestination = "<font color=""#FE5400"">Hat Yai Hotels</font>"
			strDestinationLink = "<a href=""/hat-yai-hotels.asp"">Hat Yai Hotels</a>"
			strDestinationPath = "hat-yai-hotels"
		Case 72 'Surat Thani
			strDestination = "<font color=""#FE5400"">Surat Thani Hotels</font>"
			strDestinationLink = "<a href=""/surat-thani-hotels.asp"">Surat Thani Hotels</a>"
			strDestinationPath = "surat-thani-hotels"
		Case 73 'Sukhothai
			strDestination = "<font color=""#FE5400"">Sukhothai Hotels</font>"
			strDestinationLink = "<a href=""/sukhothai-hotels.asp"">Sukhothai Hotels</a>"
			strDestinationPath = "sukhothai-hotels"
		Case 74 'Lampang
			strDestination = "<font color=""#FE5400"">Lampang Hotels</font>"
			strDestinationLink = "<a href=""/lampang-hotels.asp"">Lampang Hotels</a>"
			strDestinationPath = "lampang-hotels"
		Case 75 'Trat
			strDestination = "<font color=""#FE5400"">Trat Hotels</font>"
			strDestinationLink = "<a href=""/trat-hotels.asp"">Trat Hotels</a>"
			strDestinationPath = "trat-hotels"
		Case 76 'Loei
			strDestination = "<font color=""#FE5400"">Loei Hotels</font>"
			strDestinationLink = "<a href=""/loei-hotels.asp"">Loei Hotels</a>"
			strDestinationPath = "loei-hotels"
		Case 77 'Nong Khai
			strDestination = "<font color=""#FE5400"">Nong Khai Hotels</font>"
			strDestinationLink = "<a href=""/nong-khai-hotels.asp"">Nong Khai Hotels</a>"
			strDestinationPath = "nong-khai-hotels"
		Case 78 'Ubon Ratchathani
			strDestination = "<font color=""#FE5400"">Ubon Ratchathani Hotels</font>"
			strDestinationLink = "<a href=""/ubon-ratchathani-hotels.asp"">Ubon Ratchathani Hotels</a>"
			strDestinationPath = "ubon-ratchathani-hotels"
		Case 79 'Udon Thani
			strDestination = "<font color=""#FE5400"">Udon Thani Hotels</font>"
			strDestinationLink = "<a href=""/udon-thani-hotels.asp"">Udon Thani Hotels</a>"
			strDestinationPath = "udon-thani-hotels"
		Case 80 'Ranong
			strDestination = "<font color=""#FE5400"">Ranong Hotels</font>"
			strDestinationLink = "<a href=""/ranong-hotels.asp"">Ranong Hotels</a>"
			strDestinationPath = "ranong-hotels"
		Case 81 'Satun
			strDestination = "<font color=""#FE5400"">Satun Hotels</font>"
			strDestinationLink = "<a href=""/satun-hotels.asp"">Satun Hotels</a>"
			strDestinationPath = "satun-hotels"
		Case 82 'Chonburi
			strDestination = "<font color=""#FE5400"">Chonburi Hotels</font>"
			strDestinationLink = "<a href=""/chonburi-hotels.asp"">Chonburi Hotels</a>"
			strDestinationPath = "chonburi-hotels"
		Case 83 'Tak
			strDestination = "<font color=""#FE5400"">Tak Hotels</font>"
			strDestinationLink = "<a href=""/tak-hotels.asp"">Tak Hotels</a>"
			strDestinationPath = "tak-hotels"
		Case 84 'Nakhon Phanom
			strDestination = "<font color=""#FE5400"">Nakhon Phanom Hotels</font>"
			strDestinationLink = "<a href=""/Nakhonphanom-hotels.asp"">Nakhon Phanom Hotels</a>"
			strDestinationPath = "nakhonphanom-hotels"
		Case 86 'Nonthaburi
			strDestination = "<font color=""#FE5400"">Nonthaburi Hotels</font>"
			strDestinationLink = "<a href=""/nonthaburi-hotels.asp"">Nonthaburi Hotels</a>"
			strDestinationPath = "nonthaburi-hotels"
		Case 87 'Kamphaengphet
			strDestination = "<font color=""#FE5400"">Kamphaengphet Hotels</font>"
			strDestinationLink = "<a href=""/kamphaengphet-hotels.asp"">Kamphaengphet Hotels</a>"
			strDestinationPath = "kamphaengphet-hotels"
		Case 88 'Samut Songkhram
			strDestination = "<font color=""#FE5400"">Samut Songkhram Hotels</font>"
			strDestinationLink = "<a href=""/samut-songkhram-hotels.asp"">Samut Songkhram Hotels</a>"
			strDestinationPath = "samut-songkhram-hotels"
		Case 89 'Mukdahan
			strDestination = "<font color=""#FE5400"">Mukdahan Hotels</font>"
			strDestinationLink = "<a href=""/mukdahan-hotels.asp"">Mukdahan Hotels</a>"
			strDestinationPath = "mukdahan-hotels"
		Case 90 'Prachinburi
			strDestination = "<font color=""#FE5400"">Prachinburi Hotels</font>"
			strDestinationLink = "<a href=""/prachinburi-hotels.asp"">Prachinburi Hotels</a>"
			strDestinationPath = "prachinburi-hotels"
		Case 91 'Sakon Nakhon
			strDestination = "<font color=""#FE5400"">Sakon Nakhon Hotels</font>"
			strDestinationLink = "<a href=""/sakon-nakhon-hotels.asp"">Sakon Nakhon Hotels</a>"
			strDestinationPath = "sakon-nakhon-hotels"
		Case 92 'Surin
			strDestination = "<font color=""#FE5400"">Surin Hotels</font>"
			strDestinationLink = "<a href=""/surin-hotels.asp"">Surin Hotels</a>"
			strDestinationPath = "surin-hotels"
		Case 93 'Sisaket
			strDestination = "<font color=""#FE5400"">Sisaket Hotels</font>"
			strDestinationLink = "<a href=""/sisaket-hotels.asp"">Sisaket Hotels</a>"
			strDestinationPath = "sisaket-hotels"
		Case 94 'Saraburi
			strDestination = "<font color=""#FE5400"">Saraburi Hotels</font>"
			strDestinationLink = "<a href=""/saraburi-hotels.asp"">Saraburi Hotels</a>"
			strDestinationPath = "saraburi-hotels"
	END SELECT
	
	IF intLocationID<>"" Then
		sqlLocation = "SELECT title_en,files_name FROM tbl_location WHERE location_id=" & intLocationID
		Set recLocation = Server.CreateObject ("ADODB.Recordset")
		recLocation.Open SqlLocation, Conn,adOpenStatic,adLockreadOnly
			arrLocation = recLocation.GetRows()
			strLocation = "<font color=""#FE5400"">"&arrLocation(0,0) & " Hotels</font>"
			strLocationLink = "<a href=""" & "/" & arrLocation(1,0) &""">"& arrLocation(0,0) &"</a>"
		recLocation.Close
		Set recLocation = Nothing 
	End IF
	
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
		Case 2 'Thailand Hotels Page
			function_gen_navigator = "<a href=""/"">Home</a> -> Thailand Hotels"
		Case 3 'Destination Page
			function_gen_navigator = "<a href=""/"">Home</a> -> <a href=""/thailand-hotels.asp"">Thailand Hotels</a> -> " & strDestination
		Case 4 'Location Page
			function_gen_navigator = "<a href=""/"">Home</a> -> <a href=""/thailand-hotels.asp"">Thailand Hotels</a> -> " & strDestinationLink & " -> " & strLocation
		Case 5 'Product Detail Page
			function_gen_navigator = "<a href=""/"">Home</a> -> <a href=""/thailand-hotels.asp"">Thailand Hotels</a> -> " & strDestinationLink & " -> " & strLocationLink & " -> " & strProduct
		Case 6 'Tell a Freind Page
			function_gen_navigator = "<a href=""/"">Home</a> -> <a href=""/thailand-hotels.asp"">Thailand Hotels</a> -> " & strDestinationLink & " -> " & strLocationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Tell A Friend</font>"
		Case 7 'Search Result Page
			function_gen_navigator = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Search Result</font>"
		Case 8 'Thailand Hotels
			function_gen_navigator = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Hotels</font>"
		Case 9 'Recent Viewed Hotels
			function_gen_navigator = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Recent Viewed Hotels</font>"
		Case 10 'Hotels Page
			function_gen_navigator = "<a href=""/"">Home</a> -> <font color=""#FE5400"">Thailand Hotels</font>"
		Case 11 'Write Review
			function_gen_navigator = "<a href=""/"">Home</a> -> <a href=""/thailand-hotels.asp"">Thailand Hotels</a> -> " & strDestinationLink & " -> " & strLocationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Write Review</font>"
		Case 12 'Read Review
			function_gen_navigator = "<a href=""/"">Home</a> -> <a href=""/thailand-hotels.asp"">Thailand Hotels</a> -> " & strDestinationLink & " -> " & strLocationLink & " -> " & strProductLink & " -> <font color=""#FE5400"">Review</font>"
	
		Case 13 'Thailand Golf Courses Page
			function_gen_navigator = "<a href=""/"">Home</a> -> Thailand Golf Courses"
	
	END SELECT
	
END FUNCTION
%>