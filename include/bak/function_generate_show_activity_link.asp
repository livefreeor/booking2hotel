<%
FUNCTION function_generate_show_activity_link(intDestinationID,intLocationID,intType)
	
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
					function_generate_show_activity_link = "bangkok-show-event"
				Case ConstDesIDPhuket
					function_generate_show_activity_link = "phuket-show-event"
				Case ConstDesIDChiangMai
					function_generate_show_activity_link = "chiang-mai-show-event"
				Case ConstDesIDPattaya
					function_generate_show_activity_link = "pattaya-show-event"
				Case ConstDesIDKohSamui
					function_generate_show_activity_link = "koh-samui-show-event"
				Case ConstDesIDKrabi
					function_generate_show_activity_link = "krabi-show-event"
				Case ConstDesIDKohChang 
					function_generate_show_activity_link = "koh-chang-show-event"
				Case ConstDesIDMaeHongSon
					function_generate_show_activity_link = "mae-hong-son-show-event"
				Case ConstDesIDChiangRai
					function_generate_show_activity_link = "chiang-rai-show-event"
				Case ConstDesIDChaAm
					function_generate_show_activity_link = "cha-am-show-event"
				Case ConstDesIDHuaHin
					function_generate_show_activity_link = "hua-hin-show-event"
				Case ConstDesIDRayong
					function_generate_show_activity_link = "rayong-show-event"
				Case ConstDesIDPhangNga
					function_generate_show_activity_link = "phang-nga-show-event"
				Case ConstDesIDKohSamet
					function_generate_show_activity_link = "koh-samet-show-event"
				Case ConstDesIDKanchanaburi
					function_generate_show_activity_link = "kanchanaburi-show-event"
				Case ConstDesIDPrachuap	
					function_generate_show_activity_link = "prachuap-khiri-khan-show-event"
				Case ConstDesIDKhaoYai
					function_generate_show_activity_link = "khao-yai-show-event"
				Case ConstDesIDKohKood
					function_generate_show_activity_link = "koh-kood-show-event"
				Case ConstDesIDKohPhangan
					function_generate_show_activity_link = "koh-phangan-show-event"
				Case ConstDesIDTrang
					function_generate_show_activity_link = "trang-show-event"
				Case ConstDesIDChumphon
					function_generate_show_activity_link = "chumphon-show-event"
					
					
			END SELECT
			
		Case 2 'Link To Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_show_activity_link = "<a href=""/bangkok-show-event.asp"">Bangkok</a>"
				Case ConstDesIDPhuket
					function_generate_show_activity_link = "<a href=""/phuket-show-event.asp"">Phuket</a>"
				Case ConstDesIDChiangMai
					function_generate_show_activity_link = "<a href=""/chiang-mai-show-event.asp"">Chiang Mai</a>"
				Case ConstDesIDPattaya
					function_generate_show_activity_link = "<a href=""/pattaya-show-event.asp"">Pattaya</a>"
				Case ConstDesIDKohSamui
					function_generate_show_activity_link = "<a href=""/koh-samui-show-event.asp"">Koh Samui</a>"
				Case ConstDesIDKrabi
					function_generate_show_activity_link = "<a href=""/krabi-show-event.asp"">Krabi</a>"
				Case ConstDesIDKohChang 
					function_generate_show_activity_link = "<a href=""/koh-chang-show-event.asp"">Koh Chang</a>"
				Case ConstDesIDMaeHongSon
					function_generate_show_activity_link = "<a href=""/mae-hong-son-show-event.asp"">Mae Hong Son</a>"
				Case ConstDesIDChiangRai
					function_generate_show_activity_link = "<a href=""/chiang-rai-show-event.asp"">Chiang Rai</a>"
				Case ConstDesIDChaAm
					function_generate_show_activity_link = "<a href=""/cha-am-show-event.asp"">Cha Am Hotels</a>"
				Case ConstDesIDHuaHin
					function_generate_show_activity_link = "<a href=""/hua-hin-show-event.asp"">Hua Hin Hotels</a>"
				Case ConstDesIDRayong
					function_generate_show_activity_link = "<a href=""/rayong-show-event.asp"">Rayong</a>"
				Case ConstDesIDPhangNga
					function_generate_show_activity_link = "<a href=""/phang-nga-show-event.asp"">Phang Nga</a>"
				Case ConstDesIDKohSamet
					function_generate_show_activity_link = "<a href=""/koh-samet-show-event.asp"">Koh Samet</a>"
				Case ConstDesIDKanchanaburi
					function_generate_show_activity_link = "<a href=""/kanchanaburi-show-event.asp"">Kanchanaburi</a>"
				Case ConstDesIDPrachuap	
					function_generate_show_activity_link = "<a href=""/prachuap-khiri-khan-show-event.asp"">Prachuap Khiri khan</a>"
				Case ConstDesIDKhaoYai
					function_generate_show_activity_link = "<a href=""/khao-yai-show-event.asp"">Khao Yai</a>"
				Case ConstDesIDKohKood
					function_generate_show_activity_link = "<a href=""/koh-kood-show-event.asp"">Koh Kood</a>"
				Case ConstDesIDKohPhangan
					function_generate_show_activity_link = "<a href=""/koh-phangan-show-event"">Koh Phangan</a>"
				Case ConstDesIDTrang
					function_generate_show_activity_link = "<a href=""/trang-show-event.asp"">Trang</a>"
				Case ConstDesIDChumphon
					function_generate_show_activity_link = "<a href=""/chumphon-show-event.asp"">Chumphon</a>"

			END SELECT

		Case 4 'Show Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_show_activity_link = "Bangkok"
				Case ConstDesIDPhuket
					function_generate_show_activity_link = "Phuket"
				Case ConstDesIDChiangMai
					function_generate_show_activity_link = "Chiang Mai"
				Case ConstDesIDPattaya
					function_generate_show_activity_link = "Pattaya"
				Case ConstDesIDKohSamui
					function_generate_show_activity_link = "Koh Samui"
				Case ConstDesIDKrabi
					function_generate_show_activity_link = "Krabi"
				Case ConstDesIDKohChang 
					function_generate_show_activity_link = "Koh Chang"
				Case ConstDesIDMaeHongSon
					function_generate_show_activity_link = "Mae Hong Son"
				Case ConstDesIDChiangRai
					function_generate_show_activity_link = "Chiang Rai"
				Case ConstDesIDChaAm
					function_generate_show_activity_link = "Cha Am"
				Case ConstDesIDHuaHin
					function_generate_show_activity_link = "Hua Hin"
				Case ConstDesIDRayong
					function_generate_show_activity_link = "Rayong"
				Case ConstDesIDPhangNga
					function_generate_show_activity_link = "Phang Nga"
				Case ConstDesIDKohSamet
					function_generate_show_activity_link = "Koh Samet"
				Case ConstDesIDKanchanaburi
					function_generate_show_activity_link = "Kanchanaburi"
				Case ConstDesIDPrachuap	
					function_generate_show_activity_link = "Prachuap Khiri khan"
				Case ConstDesIDKhaoYai
					function_generate_show_activity_link = "Khao Yai"
				Case ConstDesIDKohKood
					function_generate_show_activity_link = "Koh Kood"
				Case ConstDesIDKohPhangan
					function_generate_show_activity_link = "Koh Phangan"
				Case ConstDesIDTrang
					function_generate_show_activity_link = "Trang"
				Case ConstDesIDChumphon
					function_generate_show_activity_link = "Chumphon"
			END SELECT
			
		Case 5 'Destination File
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_show_activity_link = "bangkok-show-event.asp"
				Case ConstDesIDPhuket
					function_generate_show_activity_link = "phuket-show-event.asp"
				Case ConstDesIDChiangMai
					function_generate_show_activity_link = "chiang-mai-show-event.asp"
				Case ConstDesIDPattaya
					function_generate_show_activity_link = "pattaya-show-event.asp"
				Case ConstDesIDKohSamui
					function_generate_show_activity_link = "koh-samui-show-event.asp"
				Case ConstDesIDKrabi
					function_generate_show_activity_link = "krabi-show-event.asp"
				Case ConstDesIDKohChang 
					function_generate_show_activity_link = "koh-chang-show-event.asp"
				Case ConstDesIDMaeHongSon
					function_generate_show_activity_link = "mae-hong-son-show-event.asp"
				Case ConstDesIDChiangRai
					function_generate_show_activity_link = "chiang-rai-show-event.asp"
				Case ConstDesIDChaAm
					function_generate_show_activity_link = "cha-am-show-event.asp"
				Case ConstDesIDHuaHin
					function_generate_show_activity_link = "hua-hin-show-event.asp"
				Case ConstDesIDRayong
					function_generate_show_activity_link = "rayong-show-event.asp"
				Case ConstDesIDPhangNga
					function_generate_show_activity_link = "phang-nga-show-event.asp"
				Case ConstDesIDKohSamet
					function_generate_show_activity_link = "koh-samet-show-event.asp"
				Case ConstDesIDKanchanaburi
					function_generate_show_activity_link = "kanchanaburi-show-event.asp"
				Case ConstDesIDPrachuap	
					function_generate_show_activity_link = "prachuap-khiri-khan-show-event.asp"
				Case ConstDesIDKhaoYai
					function_generate_show_activity_link = "khao-yai-show-event.asp"
				Case ConstDesIDKohKood
					function_generate_show_activity_link = "koh-kood-show-event.asp"
				Case ConstDesIDKohPhangan
					function_generate_show_activity_link = "koh-phangan-show-event.asp"
				Case ConstDesIDTrang
					function_generate_show_activity_link = "trang-show-event.asp"
				Case ConstDesIDChumphon
					function_generate_show_activity_link = "chumphon-show-event.asp"
			END SELECT
			
	END SELECT
	
END FUNCTION
%>