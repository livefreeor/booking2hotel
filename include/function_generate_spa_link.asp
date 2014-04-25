<%
FUNCTION function_generate_spa_link(intDestinationID,intLocationID,intType)
	
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
					function_generate_spa_link = "bangkok-spa"
				Case ConstDesIDPhuket
					function_generate_spa_link = "phuket-spa"
				Case ConstDesIDChiangMai
					function_generate_spa_link = "chiang-mai-spa"
				Case ConstDesIDPattaya
					function_generate_spa_link = "pattaya-spa"
				Case ConstDesIDKohSamui
					function_generate_spa_link = "koh-samui-spa"
				Case ConstDesIDKrabi
					function_generate_spa_link = "krabi-spa"
				Case ConstDesIDKohChang 
					function_generate_spa_link = "koh-chang-spa"
				Case ConstDesIDMaeHongSon
					function_generate_spa_link = "mae-hong-son-spa"
				Case ConstDesIDChiangRai
					function_generate_spa_link = "chiang-rai-spa"
				Case ConstDesIDChaAm
					function_generate_spa_link = "cha-am-spa"
				Case ConstDesIDHuaHin
					function_generate_spa_link = "hua-hin-spa"
				Case ConstDesIDRayong
					function_generate_spa_link = "rayong-spa"
				Case ConstDesIDPhangNga
					function_generate_spa_link = "phang-nga-spa"
				Case ConstDesIDKohSamet
					function_generate_spa_link = "koh-samet-spa"
				Case ConstDesIDKanchanaburi
					function_generate_spa_link = "kanchanaburi-spa"
				Case ConstDesIDPrachuap	
					function_generate_spa_link = "prachuap-khiri-khan-spa"
				Case ConstDesIDKhaoYai
					function_generate_spa_link = "khao-yai-spa"
				Case ConstDesIDKohKood
					function_generate_spa_link = "koh-kood-spa"
				Case ConstDesIDKohPhangan
					function_generate_spa_link = "koh-phangan-spa"
				Case ConstDesIDTrang
					function_generate_spa_link = "trang-spa"
				Case ConstDesIDChumphon
					function_generate_spa_link = "chumphon-spa"
					
					
			END SELECT
			
		Case 2 'Link To Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_spa_link = "<a href=""/bangkok-spa.asp"">Bangkok</a>"
				Case ConstDesIDPhuket
					function_generate_spa_link = "<a href=""/phuket-spa.asp"">Phuket</a>"
				Case ConstDesIDChiangMai
					function_generate_spa_link = "<a href=""/chiang-mai-spa.asp"">Chiang Mai</a>"
				Case ConstDesIDPattaya
					function_generate_spa_link = "<a href=""/pattaya-spa.asp"">Pattaya</a>"
				Case ConstDesIDKohSamui
					function_generate_spa_link = "<a href=""/koh-samui-spa.asp"">Koh Samui</a>"
				Case ConstDesIDKrabi
					function_generate_spa_link = "<a href=""/krabi-spa.asp"">Krabi</a>"
				Case ConstDesIDKohChang 
					function_generate_spa_link = "<a href=""/koh-chang-spa.asp"">Koh Chang</a>"
				Case ConstDesIDMaeHongSon
					function_generate_spa_link = "<a href=""/mae-hong-son-spa.asp"">Mae Hong Son</a>"
				Case ConstDesIDChiangRai
					function_generate_spa_link = "<a href=""/chiang-rai-spa.asp"">Chiang Rai</a>"
				Case ConstDesIDChaAm
					function_generate_spa_link = "<a href=""/cha-am-spa.asp"">Cha Am</a>"
				Case ConstDesIDHuaHin
					function_generate_spa_link = "<a href=""/hua-hin-spa.asp"">Hua Hin</a>"
				Case ConstDesIDRayong
					function_generate_spa_link = "<a href=""/rayong-spa.asp"">Rayong</a>"
				Case ConstDesIDPhangNga
					function_generate_spa_link = "<a href=""/phang-nga-spa.asp"">Phang Nga</a>"
				Case ConstDesIDKohSamet
					function_generate_spa_link = "<a href=""/koh-samet-spa.asp"">Koh Samet</a>"
				Case ConstDesIDKanchanaburi
					function_generate_spa_link = "<a href=""/kanchanaburi-spa.asp"">Kanchanaburi</a>"
				Case ConstDesIDPrachuap	
					function_generate_spa_link = "<a href=""/prachuap-khiri-khan-spa.asp"">Prachuap Khiri khan</a>"
				Case ConstDesIDKhaoYai
					function_generate_spa_link = "<a href=""/khao-yai-spa.asp"">Khao Yai</a>"
				Case ConstDesIDKohKood
					function_generate_spa_link = "<a href=""/koh-kood-spa.asp"">Koh Kood</a>"
				Case ConstDesIDKohPhangan
					function_generate_spa_link = "<a href=""/koh-phangan-spa"">Koh Phangan</a>"
				Case ConstDesIDTrang
					function_generate_spa_link = "<a href=""/trang-spa.asp"">Trang</a>"
				Case ConstDesIDChumphon
					function_generate_spa_link = "<a href=""/chumphon-spa.asp"">Chumphon</a>"

			END SELECT

		Case 4 'Show Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_spa_link = "Bangkok"
				Case ConstDesIDPhuket
					function_generate_spa_link = "Phuket"
				Case ConstDesIDChiangMai
					function_generate_spa_link = "Chiang Mai"
				Case ConstDesIDPattaya
					function_generate_spa_link = "Pattaya"
				Case ConstDesIDKohSamui
					function_generate_spa_link = "Koh Samui"
				Case ConstDesIDKrabi
					function_generate_spa_link = "Krabi"
				Case ConstDesIDKohChang 
					function_generate_spa_link = "Koh Chang"
				Case ConstDesIDMaeHongSon
					function_generate_spa_link = "Mae Hong Son"
				Case ConstDesIDChiangRai
					function_generate_spa_link = "Chiang Rai"
				Case ConstDesIDChaAm
					function_generate_spa_link = "Cha Am"
				Case ConstDesIDHuaHin
					function_generate_spa_link = "Hua Hin"
				Case ConstDesIDRayong
					function_generate_spa_link = "Rayong"
				Case ConstDesIDPhangNga
					function_generate_spa_link = "Phang Nga"
				Case ConstDesIDKohSamet
					function_generate_spa_link = "Koh Samet"
				Case ConstDesIDKanchanaburi
					function_generate_spa_link = "Kanchanaburi"
				Case ConstDesIDPrachuap	
					function_generate_spa_link = "Prachuap Khiri khan"
				Case ConstDesIDKhaoYai
					function_generate_spa_link = "Khao Yai"
				Case ConstDesIDKohKood
					function_generate_spa_link = "Koh Kood"
				Case ConstDesIDKohPhangan
					function_generate_spa_link = "Koh Phangan"
				Case ConstDesIDTrang
					function_generate_spa_link = "Trang"
				Case ConstDesIDChumphon
					function_generate_spa_link = "Chumphon"
			END SELECT
			
		Case 5 'Destination File
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_spa_link = "bangkok-spa.asp"
				Case ConstDesIDPhuket
					function_generate_spa_link = "phuket-spa.asp"
				Case ConstDesIDChiangMai
					function_generate_spa_link = "chiang-mai-spa.asp"
				Case ConstDesIDPattaya
					function_generate_spa_link = "pattaya-spa.asp"
				Case ConstDesIDKohSamui
					function_generate_spa_link = "koh-samui-spa.asp"
				Case ConstDesIDKrabi
					function_generate_spa_link = "krabi-spa.asp"
				Case ConstDesIDKohChang 
					function_generate_spa_link = "koh-chang-spa.asp"
				Case ConstDesIDMaeHongSon
					function_generate_spa_link = "mae-hong-son-spa.asp"
				Case ConstDesIDChiangRai
					function_generate_spa_link = "chiang-rai-spa.asp"
				Case ConstDesIDChaAm
					function_generate_spa_link = "cha-am-spa.asp"
				Case ConstDesIDHuaHin
					function_generate_spa_link = "hua-hin-spa.asp"
				Case ConstDesIDRayong
					function_generate_spa_link = "rayong-spa.asp"
				Case ConstDesIDPhangNga
					function_generate_spa_link = "phang-nga-spa.asp"
				Case ConstDesIDKohSamet
					function_generate_spa_link = "koh-samet-spa.asp"
				Case ConstDesIDKanchanaburi
					function_generate_spa_link = "kanchanaburi-spa.asp"
				Case ConstDesIDPrachuap	
					function_generate_spa_link = "prachuap-khiri-khan-spa.asp"
				Case ConstDesIDKhaoYai
					function_generate_spa_link = "khao-yai-spa.asp"
				Case ConstDesIDKohKood
					function_generate_spa_link = "koh-kood-spa.asp"
				Case ConstDesIDKohPhangan
					function_generate_spa_link = "koh-phangan-spa.asp"
				Case ConstDesIDTrang
					function_generate_spa_link = "trang-spa.asp"
				Case ConstDesIDChumphon
					function_generate_spa_link = "chumphon-spa.asp"
			END SELECT
			
	END SELECT
	
END FUNCTION
%>