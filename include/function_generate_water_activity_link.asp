<%
FUNCTION function_generate_water_activity_link(intDestinationID,intLocationID,intType)
	
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
					function_generate_water_activity_link = "bangkok-water-activity"
				Case ConstDesIDPhuket
					function_generate_water_activity_link = "phuket-water-activity"
				Case ConstDesIDChiangMai
					function_generate_water_activity_link = "chiang-mai-water-activity"
				Case ConstDesIDPattaya
					function_generate_water_activity_link = "pattaya-water-activity"
				Case ConstDesIDKohSamui
					function_generate_water_activity_link = "koh-samui-water-activity"
				Case ConstDesIDKrabi
					function_generate_water_activity_link = "krabi-water-activity"
				Case ConstDesIDKohChang 
					function_generate_water_activity_link = "koh-chang-water-activity"
				Case ConstDesIDMaeHongSon
					function_generate_water_activity_link = "mae-hong-son-water-activity"
				Case ConstDesIDChiangRai
					function_generate_water_activity_link = "chiang-rai-water-activity"
				Case ConstDesIDChaAm
					function_generate_water_activity_link = "cha-am-water-activity"
				Case ConstDesIDHuaHin
					function_generate_water_activity_link = "hua-hin-water-activity"
				Case ConstDesIDRayong
					function_generate_water_activity_link = "rayong-water-activity"
				Case ConstDesIDPhangNga
					function_generate_water_activity_link = "phang-nga-water-activity"
				Case ConstDesIDKohSamet
					function_generate_water_activity_link = "koh-samet-water-activity"
				Case ConstDesIDKanchanaburi
					function_generate_water_activity_link = "kanchanaburi-water-activity"
				Case ConstDesIDPrachuap	
					function_generate_water_activity_link = "prachuap-khiri-khan-water-activity"
				Case ConstDesIDKhaoYai
					function_generate_water_activity_link = "khao-yai-water-activity"
				Case ConstDesIDKohKood
					function_generate_water_activity_link = "koh-kood-water-activity"
				Case ConstDesIDKohPhangan
					function_generate_water_activity_link = "koh-phangan-water-activity"
				Case ConstDesIDTrang
					function_generate_water_activity_link = "trang-water-activity"
				Case ConstDesIDChumphon
					function_generate_water_activity_link = "chumphon-water-activity"
				Case ConstDesIDSatun
					function_generate_water_activity_link = "satun-water-activity"
					
			END SELECT
			
		Case 2 'Link To Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_water_activity_link = "<a href=""/bangkok-water-activity.asp"">Bangkok</a>"
				Case ConstDesIDPhuket
					function_generate_water_activity_link = "<a href=""/phuket-water-activity.asp"">Phuket</a>"
				Case ConstDesIDChiangMai
					function_generate_water_activity_link = "<a href=""/chiang-mai-water-activity.asp"">Chiang Mai</a>"
				Case ConstDesIDPattaya
					function_generate_water_activity_link = "<a href=""/pattaya-water-activity.asp"">Pattaya</a>"
				Case ConstDesIDKohSamui
					function_generate_water_activity_link = "<a href=""/koh-samui-water-activity.asp"">Koh Samui</a>"
				Case ConstDesIDKrabi
					function_generate_water_activity_link = "<a href=""/krabi-water-activity.asp"">Krabi</a>"
				Case ConstDesIDKohChang 
					function_generate_water_activity_link = "<a href=""/koh-chang-water-activity.asp"">Koh Chang</a>"
				Case ConstDesIDMaeHongSon
					function_generate_water_activity_link = "<a href=""/mae-hong-son-water-activity.asp"">Mae Hong Son</a>"
				Case ConstDesIDChiangRai
					function_generate_water_activity_link = "<a href=""/chiang-rai-water-activity.asp"">Chiang Rai</a>"
				Case ConstDesIDChaAm
					function_generate_water_activity_link = "<a href=""/cha-am-water-activity.asp"">Cha Am Hotels</a>"
				Case ConstDesIDHuaHin
					function_generate_water_activity_link = "<a href=""/hua-hin-water-activity.asp"">Hua Hin Hotels</a>"
				Case ConstDesIDRayong
					function_generate_water_activity_link = "<a href=""/rayong-water-activity.asp"">Rayong</a>"
				Case ConstDesIDPhangNga
					function_generate_water_activity_link = "<a href=""/phang-nga-water-activity.asp"">Phang Nga</a>"
				Case ConstDesIDKohSamet
					function_generate_water_activity_link = "<a href=""/koh-samet-water-activity.asp"">Koh Samet</a>"
				Case ConstDesIDKanchanaburi
					function_generate_water_activity_link = "<a href=""/kanchanaburi-water-activity.asp"">Kanchanaburi</a>"
				Case ConstDesIDPrachuap	
					function_generate_water_activity_link = "<a href=""/prachuap-khiri-khan-water-activity.asp"">Prachuap Khiri khan</a>"
				Case ConstDesIDKhaoYai
					function_generate_water_activity_link = "<a href=""/khao-yai-water-activity.asp"">Khao Yai</a>"
				Case ConstDesIDKohKood
					function_generate_water_activity_link = "<a href=""/koh-kood-water-activity.asp"">Koh Kood</a>"
				Case ConstDesIDKohPhangan
					function_generate_water_activity_link = "<a href=""/koh-phangan-water-activity"">Koh Phangan</a>"
				Case ConstDesIDTrang
					function_generate_water_activity_link = "<a href=""/trang-water-activity.asp"">Trang</a>"
				Case ConstDesIDChumphon
					function_generate_water_activity_link = "<a href=""/chumphon-water-activity.asp"">Chumphon</a>"
				Case ConstDesIDSatun
					function_generate_water_activity_link = "<a href=""/satun-water-activity.asp"">Satun</a>"
			END SELECT

		Case 4 'Show Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_water_activity_link = "Bangkok"
				Case ConstDesIDPhuket
					function_generate_water_activity_link = "Phuket"
				Case ConstDesIDChiangMai
					function_generate_water_activity_link = "Chiang Mai"
				Case ConstDesIDPattaya
					function_generate_water_activity_link = "Pattaya"
				Case ConstDesIDKohSamui
					function_generate_water_activity_link = "Koh Samui"
				Case ConstDesIDKrabi
					function_generate_water_activity_link = "Krabi"
				Case ConstDesIDKohChang 
					function_generate_water_activity_link = "Koh Chang"
				Case ConstDesIDMaeHongSon
					function_generate_water_activity_link = "Mae Hong Son"
				Case ConstDesIDChiangRai
					function_generate_water_activity_link = "Chiang Rai"
				Case ConstDesIDChaAm
					function_generate_water_activity_link = "Cha Am"
				Case ConstDesIDHuaHin
					function_generate_water_activity_link = "Hua Hin"
				Case ConstDesIDRayong
					function_generate_water_activity_link = "Rayong"
				Case ConstDesIDPhangNga
					function_generate_water_activity_link = "Phang Nga"
				Case ConstDesIDKohSamet
					function_generate_water_activity_link = "Koh Samet"
				Case ConstDesIDKanchanaburi
					function_generate_water_activity_link = "Kanchanaburi"
				Case ConstDesIDPrachuap	
					function_generate_water_activity_link = "Prachuap Khiri khan"
				Case ConstDesIDKhaoYai
					function_generate_water_activity_link = "Khao Yai"
				Case ConstDesIDKohKood
					function_generate_water_activity_link = "Koh Kood"
				Case ConstDesIDKohPhangan
					function_generate_water_activity_link = "Koh Phangan"
				Case ConstDesIDTrang
					function_generate_water_activity_link = "Trang"
				Case ConstDesIDChumphon
					function_generate_water_activity_link = "Chumphon"
				Case ConstDesIDSatun
					function_generate_water_activity_link = "Satun"
			END SELECT
			
		Case 5 'Destination File
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_water_activity_link = "bangkok-water-activity.asp"
				Case ConstDesIDPhuket
					function_generate_water_activity_link = "phuket-water-activity.asp"
				Case ConstDesIDChiangMai
					function_generate_water_activity_link = "chiang-mai-water-activity.asp"
				Case ConstDesIDPattaya
					function_generate_water_activity_link = "pattaya-water-activity.asp"
				Case ConstDesIDKohSamui
					function_generate_water_activity_link = "koh-samui-water-activity.asp"
				Case ConstDesIDKrabi
					function_generate_water_activity_link = "krabi-water-activity.asp"
				Case ConstDesIDKohChang 
					function_generate_water_activity_link = "koh-chang-water-activity.asp"
				Case ConstDesIDMaeHongSon
					function_generate_water_activity_link = "mae-hong-son-water-activity.asp"
				Case ConstDesIDChiangRai
					function_generate_water_activity_link = "chiang-rai-water-activity.asp"
				Case ConstDesIDChaAm
					function_generate_water_activity_link = "cha-am-water-activity.asp"
				Case ConstDesIDHuaHin
					function_generate_water_activity_link = "hua-hin-water-activity.asp"
				Case ConstDesIDRayong
					function_generate_water_activity_link = "rayong-water-activity.asp"
				Case ConstDesIDPhangNga
					function_generate_water_activity_link = "phang-nga-water-activity.asp"
				Case ConstDesIDKohSamet
					function_generate_water_activity_link = "koh-samet-water-activity.asp"
				Case ConstDesIDKanchanaburi
					function_generate_water_activity_link = "kanchanaburi-water-activity.asp"
				Case ConstDesIDPrachuap	
					function_generate_water_activity_link = "prachuap-khiri-khan-water-activity.asp"
				Case ConstDesIDKhaoYai
					function_generate_water_activity_link = "khao-yai-water-activity.asp"
				Case ConstDesIDKohKood
					function_generate_water_activity_link = "koh-kood-water-activity.asp"
				Case ConstDesIDKohPhangan
					function_generate_water_activity_link = "koh-phangan-water-activity.asp"
				Case ConstDesIDTrang
					function_generate_water_activity_link = "trang-water-activity.asp"
				Case ConstDesIDChumphon
					function_generate_water_activity_link = "chumphon-water-activity.asp"
				Case ConstDesIDSatun
					function_generate_water_activity_link = "satun-water-activity.asp"
			END SELECT
			
	END SELECT
	
END FUNCTION
%>