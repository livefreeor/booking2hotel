<%
FUNCTION function_generate_health_check_up_link(intDestinationID,intLocationID,intType)
	
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
					function_generate_health_check_up_link = "bangkok-health-check-up"
				Case ConstDesIDPhuket
					function_generate_health_check_up_link = "phuket-health-check-up"
				Case ConstDesIDChiangMai
					function_generate_health_check_up_link = "chiang-mai-health-check-up"
				Case ConstDesIDPattaya
					function_generate_health_check_up_link = "pattaya-health-check-up"
				Case ConstDesIDKohSamui
					function_generate_health_check_up_link = "koh-samui-health-check-up"
				Case ConstDesIDKrabi
					function_generate_health_check_up_link = "krabi-health-check-up"
				Case ConstDesIDKohChang 
					function_generate_health_check_up_link = "koh-chang-health-check-up"
				Case ConstDesIDMaeHongSon
					function_generate_health_check_up_link = "mae-hong-son-health-check-up"
				Case ConstDesIDChiangRai
					function_generate_health_check_up_link = "chiang-rai-health-check-up"
				Case ConstDesIDChaAm
					function_generate_health_check_up_link = "cha-am-health-check-up"
				Case ConstDesIDHuaHin
					function_generate_health_check_up_link = "hua-hin-health-check-up"
				Case ConstDesIDRayong
					function_generate_health_check_up_link = "rayong-health-check-up"
				Case ConstDesIDPhangNga
					function_generate_health_check_up_link = "phang-nga-health-check-up"
				Case ConstDesIDKohSamet
					function_generate_health_check_up_link = "koh-samet-health-check-up"
				Case ConstDesIDKanchanaburi
					function_generate_health_check_up_link = "kanchanaburi-health-check-up"
				Case ConstDesIDPrachuap	
					function_generate_health_check_up_link = "prachuap-khiri-khan-health-check-up"
				Case ConstDesIDKhaoYai
					function_generate_health_check_up_link = "khao-yai-health-check-up"
				Case ConstDesIDKohKood
					function_generate_health_check_up_link = "koh-kood-health-check-up"
				Case ConstDesIDKohPhangan
					function_generate_health_check_up_link = "koh-phangan-health-check-up"
				Case ConstDesIDTrang
					function_generate_health_check_up_link = "trang-health-check-up"
				Case ConstDesIDChumphon
					function_generate_health_check_up_link = "chumphon-health-check-up"
					
					
			END SELECT
			
		Case 2 'Link To Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_health_check_up_link = "<a href=""/bangkok-health-check-up.asp"">Bangkok</a>"
				Case ConstDesIDPhuket
					function_generate_health_check_up_link = "<a href=""/phuket-health-check-up.asp"">Phuket</a>"
				Case ConstDesIDChiangMai
					function_generate_health_check_up_link = "<a href=""/chiang-mai-health-check-up.asp"">Chiang Mai</a>"
				Case ConstDesIDPattaya
					function_generate_health_check_up_link = "<a href=""/pattaya-health-check-up.asp"">Pattaya</a>"
				Case ConstDesIDKohSamui
					function_generate_health_check_up_link = "<a href=""/koh-samui-health-check-up.asp"">Koh Samui</a>"
				Case ConstDesIDKrabi
					function_generate_health_check_up_link = "<a href=""/krabi-health-check-up.asp"">Krabi</a>"
				Case ConstDesIDKohChang 
					function_generate_health_check_up_link = "<a href=""/koh-chang-health-check-up.asp"">Koh Chang</a>"
				Case ConstDesIDMaeHongSon
					function_generate_health_check_up_link = "<a href=""/mae-hong-son-health-check-up.asp"">Mae Hong Son</a>"
				Case ConstDesIDChiangRai
					function_generate_health_check_up_link = "<a href=""/chiang-rai-health-check-up.asp"">Chiang Rai</a>"
				Case ConstDesIDChaAm
					function_generate_health_check_up_link = "<a href=""/cha-am-health-check-up.asp"">Cha Am Hotels</a>"
				Case ConstDesIDHuaHin
					function_generate_health_check_up_link = "<a href=""/hua-hin-health-check-up.asp"">Hua Hin Hotels</a>"
				Case ConstDesIDRayong
					function_generate_health_check_up_link = "<a href=""/rayong-health-check-up.asp"">Rayong</a>"
				Case ConstDesIDPhangNga
					function_generate_health_check_up_link = "<a href=""/phang-nga-health-check-up.asp"">Phang Nga</a>"
				Case ConstDesIDKohSamet
					function_generate_health_check_up_link = "<a href=""/koh-samet-health-check-up.asp"">Koh Samet</a>"
				Case ConstDesIDKanchanaburi
					function_generate_health_check_up_link = "<a href=""/kanchanaburi-health-check-up.asp"">Kanchanaburi</a>"
				Case ConstDesIDPrachuap	
					function_generate_health_check_up_link = "<a href=""/prachuap-khiri-khan-health-check-up.asp"">Prachuap Khiri khan</a>"
				Case ConstDesIDKhaoYai
					function_generate_health_check_up_link = "<a href=""/khao-yai-health-check-up.asp"">Khao Yai</a>"
				Case ConstDesIDKohKood
					function_generate_health_check_up_link = "<a href=""/koh-kood-health-check-up.asp"">Koh Kood</a>"
				Case ConstDesIDKohPhangan
					function_generate_health_check_up_link = "<a href=""/koh-phangan-health-check-up"">Koh Phangan</a>"
				Case ConstDesIDTrang
					function_generate_health_check_up_link = "<a href=""/trang-health-check-up.asp"">Trang</a>"
				Case ConstDesIDChumphon
					function_generate_health_check_up_link = "<a href=""/chumphon-health-check-up.asp"">Chumphon</a>"

			END SELECT

		Case 4 'Show Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_health_check_up_link = "Bangkok"
				Case ConstDesIDPhuket
					function_generate_health_check_up_link = "Phuket"
				Case ConstDesIDChiangMai
					function_generate_health_check_up_link = "Chiang Mai"
				Case ConstDesIDPattaya
					function_generate_health_check_up_link = "Pattaya"
				Case ConstDesIDKohSamui
					function_generate_health_check_up_link = "Koh Samui"
				Case ConstDesIDKrabi
					function_generate_health_check_up_link = "Krabi"
				Case ConstDesIDKohChang 
					function_generate_health_check_up_link = "Koh Chang"
				Case ConstDesIDMaeHongSon
					function_generate_health_check_up_link = "Mae Hong Son"
				Case ConstDesIDChiangRai
					function_generate_health_check_up_link = "Chiang Rai"
				Case ConstDesIDChaAm
					function_generate_health_check_up_link = "Cha Am"
				Case ConstDesIDHuaHin
					function_generate_health_check_up_link = "Hua Hin"
				Case ConstDesIDRayong
					function_generate_health_check_up_link = "Rayong"
				Case ConstDesIDPhangNga
					function_generate_health_check_up_link = "Phang Nga"
				Case ConstDesIDKohSamet
					function_generate_health_check_up_link = "Koh Samet"
				Case ConstDesIDKanchanaburi
					function_generate_health_check_up_link = "Kanchanaburi"
				Case ConstDesIDPrachuap	
					function_generate_health_check_up_link = "Prachuap Khiri khan"
				Case ConstDesIDKhaoYai
					function_generate_health_check_up_link = "Khao Yai"
				Case ConstDesIDKohKood
					function_generate_health_check_up_link = "Koh Kood"
				Case ConstDesIDKohPhangan
					function_generate_health_check_up_link = "Koh Phangan"
				Case ConstDesIDTrang
					function_generate_health_check_up_link = "Trang"
				Case ConstDesIDChumphon
					function_generate_health_check_up_link = "Chumphon"
			END SELECT
			
		Case 5 'Destination File
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_health_check_up_link = "bangkok-health-check-up.asp"
				Case ConstDesIDPhuket
					function_generate_health_check_up_link = "phuket-health-check-up.asp"
				Case ConstDesIDChiangMai
					function_generate_health_check_up_link = "chiang-mai-health-check-up.asp"
				Case ConstDesIDPattaya
					function_generate_health_check_up_link = "pattaya-health-check-up.asp"
				Case ConstDesIDKohSamui
					function_generate_health_check_up_link = "koh-samui-health-check-up.asp"
				Case ConstDesIDKrabi
					function_generate_health_check_up_link = "krabi-health-check-up.asp"
				Case ConstDesIDKohChang 
					function_generate_health_check_up_link = "koh-chang-health-check-up.asp"
				Case ConstDesIDMaeHongSon
					function_generate_health_check_up_link = "mae-hong-son-health-check-up.asp"
				Case ConstDesIDChiangRai
					function_generate_health_check_up_link = "chiang-rai-health-check-up.asp"
				Case ConstDesIDChaAm
					function_generate_health_check_up_link = "cha-am-health-check-up.asp"
				Case ConstDesIDHuaHin
					function_generate_health_check_up_link = "hua-hin-health-check-up.asp"
				Case ConstDesIDRayong
					function_generate_health_check_up_link = "rayong-health-check-up.asp"
				Case ConstDesIDPhangNga
					function_generate_health_check_up_link = "phang-nga-health-check-up.asp"
				Case ConstDesIDKohSamet
					function_generate_health_check_up_link = "koh-samet-health-check-up.asp"
				Case ConstDesIDKanchanaburi
					function_generate_health_check_up_link = "kanchanaburi-health-check-up.asp"
				Case ConstDesIDPrachuap	
					function_generate_health_check_up_link = "prachuap-khiri-khan-health-check-up.asp"
				Case ConstDesIDKhaoYai
					function_generate_health_check_up_link = "khao-yai-health-check-up.asp"
				Case ConstDesIDKohKood
					function_generate_health_check_up_link = "koh-kood-health-check-up.asp"
				Case ConstDesIDKohPhangan
					function_generate_health_check_up_link = "koh-phangan-health-check-up.asp"
				Case ConstDesIDTrang
					function_generate_health_check_up_link = "trang-health-check-up.asp"
				Case ConstDesIDChumphon
					function_generate_health_check_up_link = "chumphon-health-check-up.asp"
			END SELECT
			
	END SELECT
	
END FUNCTION
%>