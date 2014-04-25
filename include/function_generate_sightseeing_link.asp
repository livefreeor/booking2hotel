<%
FUNCTION function_generate_sightseeing_link(intDestinationID,intLocationID,intType)
	
	IF intDestinationID <> "" Then
		intDestinationID = Cint(intDestinationID)
	End IF

	IF (intLocationID <> "") AND (intLocationID <> "none") Then
		intLocationID = Cint(intLocationID)
	End IF

	SELECT CASE intType
		Case 1 'Link to Hotel Detail
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_sightseeing_link = "bangkok-day-trips"
				Case ConstDesIDPhuket
					function_generate_sightseeing_link = "phuket-day-trips"
				Case ConstDesIDChiangMai
					function_generate_sightseeing_link = "chiang-mai-day-trips"
				Case ConstDesIDPattaya
					function_generate_sightseeing_link = "pattaya-day-trips"
				Case ConstDesIDKohSamui
					function_generate_sightseeing_link = "koh-samui-day-trips"
				Case ConstDesIDKrabi
					function_generate_sightseeing_link = "krabi-day-trips"
				Case ConstDesIDKohChang 
					function_generate_sightseeing_link = "koh-chang-day-trips"
				Case ConstDesIDMaeHongSon
					function_generate_sightseeing_link = "mae-hong-son-day-trips"
				Case ConstDesIDChiangRai
					function_generate_sightseeing_link = "chiang-rai-day-trips"
				Case ConstDesIDChaAm
					function_generate_sightseeing_link = "cha-am-day-trips"
				Case ConstDesIDHuaHin
					function_generate_sightseeing_link = "hua-hin-day-trips"
				Case ConstDesIDRayong
					function_generate_sightseeing_link = "rayong-day-trips"
				Case ConstDesIDPhangNga
					function_generate_sightseeing_link = "phang-nga-day-trips"
				Case ConstDesIDKohSamet
					function_generate_sightseeing_link = "koh-samet-day-trips"
				Case ConstDesIDKanchanaburi
					function_generate_sightseeing_link = "kanchanaburi-day-trips"
				Case ConstDesIDPrachuap	
					function_generate_sightseeing_link = "prachuap-khiri-khan-day-trips"
				Case ConstDesIDKhaoYai
					function_generate_sightseeing_link = "khao-yai-day-trips"
				Case ConstDesIDKohKood
					function_generate_sightseeing_link = "koh-kood-day-trips"
				Case ConstDesIDKohPhangan
					function_generate_sightseeing_link = "koh-phangan-day-trips"
				Case ConstDesIDTrang
					function_generate_sightseeing_link = "trang-day-trips"
				Case ConstDesIDChumphon
					function_generate_sightseeing_link = "chumphon-day-trips"
					
					
			END SELECT
			
		Case 2 'Link To Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_sightseeing_link = "<a href=""/bangkok-day-trips.asp"">Bangkok</a>"
				Case ConstDesIDPhuket
					function_generate_sightseeing_link = "<a href=""/phuket-day-trips.asp"">Phuket</a>"
				Case ConstDesIDChiangMai
					function_generate_sightseeing_link = "<a href=""/chiang-mai-day-trips.asp"">Chiang Mai</a>"
				Case ConstDesIDPattaya
					function_generate_sightseeing_link = "<a href=""/pattaya-day-trips.asp"">Pattaya</a>"
				Case ConstDesIDKohSamui
					function_generate_sightseeing_link = "<a href=""/koh-samui-day-trips.asp"">Koh Samui</a>"
				Case ConstDesIDKrabi
					function_generate_sightseeing_link = "<a href=""/krabi-day-trips.asp"">Krabi</a>"
				Case ConstDesIDKohChang 
					function_generate_sightseeing_link = "<a href=""/koh-chang-day-trips.asp"">Koh Chang</a>"
				Case ConstDesIDMaeHongSon
					function_generate_sightseeing_link = "<a href=""/mae-hong-son-day-trips.asp"">Mae Hong Son</a>"
				Case ConstDesIDChiangRai
					function_generate_sightseeing_link = "<a href=""/chiang-rai-day-trips.asp"">Chiang Rai</a>"
				Case ConstDesIDChaAm
					function_generate_sightseeing_link = "<a href=""/cha-am-day-trips.asp"">Cha Am Hotels</a>"
				Case ConstDesIDHuaHin
					function_generate_sightseeing_link = "<a href=""/hua-hin-day-trips.asp"">Hua Hin Hotels</a>"
				Case ConstDesIDRayong
					function_generate_sightseeing_link = "<a href=""/rayong-day-trips.asp"">Rayong</a>"
				Case ConstDesIDPhangNga
					function_generate_sightseeing_link = "<a href=""/phang-nga-day-trips.asp"">Phang Nga</a>"
				Case ConstDesIDKohSamet
					function_generate_sightseeing_link = "<a href=""/koh-samet-day-trips.asp"">Koh Samet</a>"
				Case ConstDesIDKanchanaburi
					function_generate_sightseeing_link = "<a href=""/kanchanaburi-day-trips.asp"">Kanchanaburi</a>"
				Case ConstDesIDPrachuap	
					function_generate_sightseeing_link = "<a href=""/prachuap-khiri-khan-day-trips.asp"">Prachuap Khiri khan</a>"
				Case ConstDesIDKhaoYai
					function_generate_sightseeing_link = "<a href=""/khao-yai-day-trips.asp"">Khao Yai</a>"
				Case ConstDesIDKohKood
					function_generate_sightseeing_link = "<a href=""/koh-kood-day-trips.asp"">Koh Kood</a>"
				Case ConstDesIDKohPhangan
					function_generate_sightseeing_link = "<a href=""/koh-phangan-day-trips"">Koh Phangan</a>"
				Case ConstDesIDTrang
					function_generate_sightseeing_link = "<a href=""/trang-day-trips.asp"">Trang</a>"
				Case ConstDesIDChumphon
					function_generate_sightseeing_link = "<a href=""/chumphon-day-trips.asp"">Chumphon</a>"

			END SELECT

		Case 4 'Show Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_sightseeing_link = "Bangkok"
				Case ConstDesIDPhuket
					function_generate_sightseeing_link = "Phuket"
				Case ConstDesIDChiangMai
					function_generate_sightseeing_link = "Chiang Mai"
				Case ConstDesIDPattaya
					function_generate_sightseeing_link = "Pattaya"
				Case ConstDesIDKohSamui
					function_generate_sightseeing_link = "Koh Samui"
				Case ConstDesIDKrabi
					function_generate_sightseeing_link = "Krabi"
				Case ConstDesIDKohChang 
					function_generate_sightseeing_link = "Koh Chang"
				Case ConstDesIDMaeHongSon
					function_generate_sightseeing_link = "Mae Hong Son"
				Case ConstDesIDChiangRai
					function_generate_sightseeing_link = "Chiang Rai"
				Case ConstDesIDChaAm
					function_generate_sightseeing_link = "Cha Am"
				Case ConstDesIDHuaHin
					function_generate_sightseeing_link = "Hua Hin"
				Case ConstDesIDRayong
					function_generate_sightseeing_link = "Rayong"
				Case ConstDesIDPhangNga
					function_generate_sightseeing_link = "Phang Nga"
				Case ConstDesIDKohSamet
					function_generate_sightseeing_link = "Koh Samet"
				Case ConstDesIDKanchanaburi
					function_generate_sightseeing_link = "Kanchanaburi"
				Case ConstDesIDPrachuap	
					function_generate_sightseeing_link = "Prachuap Khiri khan"
				Case ConstDesIDKhaoYai
					function_generate_sightseeing_link = "Khao Yai"
				Case ConstDesIDKohKood
					function_generate_sightseeing_link = "Koh Kood"
				Case ConstDesIDKohPhangan
					function_generate_sightseeing_link = "Koh Phangan"
				Case ConstDesIDTrang
					function_generate_sightseeing_link = "Trang"
				Case ConstDesIDChumphon
					function_generate_sightseeing_link = "Chumphon"
			END SELECT
			
		Case 5 'Destination File
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_sightseeing_link = "bangkok-day-trips.asp"
				Case ConstDesIDPhuket
					function_generate_sightseeing_link = "phuket-day-trips.asp"
				Case ConstDesIDChiangMai
					function_generate_sightseeing_link = "chiang-mai-day-trips.asp"
				Case ConstDesIDPattaya
					function_generate_sightseeing_link = "pattaya-day-trips.asp"
				Case ConstDesIDKohSamui
					function_generate_sightseeing_link = "koh-samui-day-trips.asp"
				Case ConstDesIDKrabi
					function_generate_sightseeing_link = "krabi-day-trips.asp"
				Case ConstDesIDKohChang 
					function_generate_sightseeing_link = "koh-chang-day-trips.asp"
				Case ConstDesIDMaeHongSon
					function_generate_sightseeing_link = "mae-hong-son-day-trips.asp"
				Case ConstDesIDChiangRai
					function_generate_sightseeing_link = "chiang-rai-day-trips.asp"
				Case ConstDesIDChaAm
					function_generate_sightseeing_link = "cha-am-day-trips.asp"
				Case ConstDesIDHuaHin
					function_generate_sightseeing_link = "hua-hin-day-trips.asp"
				Case ConstDesIDRayong
					function_generate_sightseeing_link = "rayong-day-trips.asp"
				Case ConstDesIDPhangNga
					function_generate_sightseeing_link = "phang-nga-day-trips.asp"
				Case ConstDesIDKohSamet
					function_generate_sightseeing_link = "koh-samet-day-trips.asp"
				Case ConstDesIDKanchanaburi
					function_generate_sightseeing_link = "kanchanaburi-day-trips.asp"
				Case ConstDesIDPrachuap	
					function_generate_sightseeing_link = "prachuap-khiri-khan-day-trips.asp"
				Case ConstDesIDKhaoYai
					function_generate_sightseeing_link = "khao-yai-day-trips.asp"
				Case ConstDesIDKohKood
					function_generate_sightseeing_link = "koh-kood-day-trips.asp"
				Case ConstDesIDKohPhangan
					function_generate_sightseeing_link = "koh-phangan-day-trips.asp"
				Case ConstDesIDTrang
					function_generate_sightseeing_link = "trang-day-trips.asp"
				Case ConstDesIDChumphon
					function_generate_sightseeing_link = "chumphon-day-trips.asp"
			END SELECT
			
	END SELECT
	
END FUNCTION
%>