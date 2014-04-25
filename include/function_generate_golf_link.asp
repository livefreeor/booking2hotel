<%
FUNCTION function_generate_golf_link(intDestinationID,intLocationID,intType)
	
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
					function_generate_golf_link  = "bangkok-golf"
				Case ConstDesIDPhuket
					function_generate_golf_link  = "phuket-golf"
				Case ConstDesIDChiangMai
					function_generate_golf_link  = "chiang-mai-golf"
				Case ConstDesIDPattaya
					function_generate_golf_link  = "pattaya-golf"
				Case ConstDesIDKohSamui
					function_generate_golf_link  = "koh-samui-golf"
				Case ConstDesIDKrabi
					function_generate_golf_link  = "krabi-golf"
				Case ConstDesIDKohChang 
					function_generate_golf_link  = "koh-chang-golf"
				Case ConstDesIDMaeHongSon
					function_generate_golf_link  = "mae-hong-son-golf"
				Case ConstDesIDChiangRai
					function_generate_golf_link  = "chiang-rai-golf"
				Case ConstDesIDChaAm
					function_generate_golf_link  = "cha-am-golf"
				Case ConstDesIDHuaHin
					function_generate_golf_link  = "hua-hin-golf"
				Case ConstDesIDRayong
					function_generate_golf_link  = "rayong-golf"
				Case ConstDesIDPhangNga
					function_generate_golf_link  = "phang-nga-golf"
				Case ConstDesIDKohSamet
					function_generate_golf_link  = "koh-samet-golf"
				Case ConstDesIDKanchanaburi
					function_generate_golf_link  = "kanchanaburi-golf"
				Case ConstDesIDAyutthaya 
					function_generate_golf_link  = "ayutthaya-golf"
				Case ConstDesIDKhaoYai
					function_generate_golf_link  = "khao-yai-golf"
				Case ConstDesIDNakornnayok
					function_generate_golf_link  = "nakornnayok-golf"
				Case ConstDesIDNakornpathom 
					function_generate_golf_link  = "nakornpathom-golf"
				Case ConstDesIDRatchaburi
					function_generate_golf_link  = "ratchaburi-golf"
				Case ConstDesIDUthaiThani
					function_generate_golf_link  = "uthai-thani-golf"
				
				Case ConstDesIDSamutprakarn
					function_generate_golf_link  = "samutprakarn-golf"
				Case ConstDesIDSongkhla
					function_generate_golf_link  = "songkhla-golf"
						
			END SELECT
			
		Case 2 'Link To Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_golf_link  = "<a href=""/bangkok-golf.asp"">Bangkok</a>"
				Case ConstDesIDPhuket
					function_generate_golf_link  = "<a href=""/phuket-golf.asp"">Phuket</a>"
				Case ConstDesIDChiangMai
					function_generate_golf_link  = "<a href=""/chiang-mai-golf.asp"">Chiang Mai</a>"
				Case ConstDesIDPattaya
					function_generate_golf_link  = "<a href=""/pattaya-golf.asp"">Pattaya</a>"
				Case ConstDesIDKohSamui
					function_generate_golf_link  = "<a href=""/koh-samui-golf.asp"">Koh Samui</a>"
				Case ConstDesIDKrabi
					function_generate_golf_link  = "<a href=""/krabi-golf.asp"">Krabi</a>"
				Case ConstDesIDKohChang 
					function_generate_golf_link  = "<a href=""/koh-chang-golf.asp"">Koh Chang</a>"
				Case ConstDesIDMaeHongSon
					function_generate_golf_link  = "<a href=""/mae-hong-son-golf.asp"">Mae Hong Son</a>"
				Case ConstDesIDChiangRai
					function_generate_golf_link  = "<a href=""/chiang-rai-golf.asp"">Chiang Rai</a>"
				Case ConstDesIDChaAm
					function_generate_golf_link  = "<a href=""/cha-am-golf.asp"">Cha Am golf</a>"
				Case ConstDesIDHuaHin
					function_generate_golf_link  = "<a href=""/hua-hin-golf.asp"">Hua Hin golf</a>"
				Case ConstDesIDRayong
					function_generate_golf_link  = "<a href=""/rayong-golf.asp"">Rayong</a>"
				Case ConstDesIDPhangNga
					function_generate_golf_link  = "<a href=""/phang-nga-golf.asp"">Phang Nga</a>"
				Case ConstDesIDKohSamet
					function_generate_golf_link  = "<a href=""/koh-samet-golf.asp"">Koh Samet</a>"
				Case ConstDesIDKanchanaburi
					function_generate_golf_link  = "<a href=""/kanchanaburi-golf.asp"">Kanchanaburi</a>"
				Case ConstDesIDAyutthaya 
					function_generate_golf_link  = "<a href=""/ayutthaya-golf.asp"">Ayutthaya</a>"
				Case ConstDesIDKhaoYai
					function_generate_golf_link  = "<a href=""/khao-yai-golf.asp"">Khao Yai</a>"
				Case ConstDesIDNakornnayok
					function_generate_golf_link  = "<a href=""/nakornnayok.asp"">Nakornnayok</a>"
				Case ConstDesIDNakornpathom 
					function_generate_golf_link  = "<a href=""/nakornpathom-golf.asp"">Nakornpathom</a>"
				Case ConstDesIDRatchaburi
					function_generate_golf_link  = "<a href=""/ratchaburi-golf.asp"">Ratchaburi</a>"
				Case ConstDesIDUthaiThani
					function_generate_golf_link  = "<a href=""/uthai-thani-golf.asp"">UthaiThani</a>"
					
				Case ConstDesIDSamutprakarn
					function_generate_golf_link  = "<a href=""/samutprakarn-golf.asp"">Samutprakarn</a>"
				Case ConstDesIDSongkhla
					function_generate_golf_link  = "<a href=""/songkhla-golf.asp"">Songkhla</a>"
					
			END SELECT

		Case 4 'Show Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_golf_link  = "Bangkok"
				Case ConstDesIDPhuket
					function_generate_golf_link  = "Phuket"
				Case ConstDesIDChiangMai
					function_generate_golf_link  = "Chiang Mai"
				Case ConstDesIDPattaya
					function_generate_golf_link  = "Pattaya"
				Case ConstDesIDKohSamui
					function_generate_golf_link  = "Koh Samui"
				Case ConstDesIDKrabi
					function_generate_golf_link  = "Krabi"
				Case ConstDesIDKohChang 
					function_generate_golf_link  = "Koh Chang"
				Case ConstDesIDMaeHongSon
					function_generate_golf_link  = "Mae Hong Son"
				Case ConstDesIDChiangRai
					function_generate_golf_link  = "Chiang Rai"
				Case ConstDesIDChaAm
					function_generate_golf_link  = "Cha Am"
				Case ConstDesIDHuaHin
					function_generate_golf_link  = "Hua Hin"
				Case ConstDesIDRayong
					function_generate_golf_link  = "Rayong"
				Case ConstDesIDPhangNga
					function_generate_golf_link  = "Phang Nga"
				Case ConstDesIDKohSamet
					function_generate_golf_link  = "Koh Samet"
				Case ConstDesIDKanchanaburi
					function_generate_golf_link  = "Kanchanaburi"
				Case ConstDesIDAyutthaya 
					function_generate_golf_link  = "Ayutthaya"
				Case ConstDesIDKhaoYai
					function_generate_golf_link  = "Khao Yai"
				Case ConstDesIDNakornnayok
					function_generate_golf_link  = "Nakornnayok"
				Case ConstDesIDNakornpathom
					function_generate_golf_link  = "Nakornpathom"
				Case ConstDesIDRatchaburi
					function_generate_golf_link  = "Ratchaburi"
				Case ConstDesIDUthaiThani
					function_generate_golf_link  = "UthaiThani"
					
				Case ConstDesIDSamutprakarn
					function_generate_golf_link  = "Samutprakarn"
				Case ConstDesIDSongkhla
					function_generate_golf_link  = "Songkhla"
					
			END SELECT
			
		Case 5 'Destination File
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_golf_link  = "bangkok-golf.asp"
				Case ConstDesIDPhuket
					function_generate_golf_link  = "phuket-golf.asp"
				Case ConstDesIDChiangMai
					function_generate_golf_link  = "chiang-mai-golf.asp"
				Case ConstDesIDPattaya
					function_generate_golf_link  = "pattaya-golf.asp"
				Case ConstDesIDKohSamui
					function_generate_golf_link  = "koh-samui-golf.asp"
				Case ConstDesIDKrabi
					'function_generate_golf_link  = "krabi-golf.asp"
				Case ConstDesIDKohChang 
					'function_generate_golf_link  = "koh-chang-golf.asp"
				Case ConstDesIDMaeHongSon
					'function_generate_golf_link  = "mae-hong-son-golf.asp"
				Case ConstDesIDChiangRai
					function_generate_golf_link  = "chiang-rai-golf.asp"
				Case ConstDesIDChaAm
					function_generate_golf_link  = "cha-am-golf.asp"
				Case ConstDesIDHuaHin
					function_generate_golf_link  = "hua-hin-golf.asp"
				Case ConstDesIDRayong
					function_generate_golf_link  = "rayong-golf.asp"
				Case ConstDesIDPhangNga
					function_generate_golf_link  = "phang-nga-golf.asp"
				Case ConstDesIDKohSamet
					'function_generate_golf_link  = "koh-samet-golf.asp"
				Case ConstDesIDKanchanaburi
					function_generate_golf_link  = "kanchanaburi-golf.asp"
				Case ConstDesIDAyutthaya 
					function_generate_golf_link  = "ayutthaya-golf.asp"
				Case ConstDesIDKhaoYai
					function_generate_golf_link  = "khao-yai-golf.asp"
				Case ConstDesIDNakornnayok
					function_generate_golf_link  = "nakornnayok-golf.asp"
				Case ConstDesIDNakornpathom
					function_generate_golf_link  = "nakornpathom-golf.asp"
				Case ConstDesIDRatchaburi
					function_generate_golf_link  = "ratchaburi-golf.asp"
				Case ConstDesIDUthaiThani
					function_generate_golf_link  = "uthai-thani-golf.asp"
					
				Case ConstDesIDSamutprakarn
					function_generate_golf_link  = "samutprakarn-golf.asp"
				Case ConstDesIDSongkhla
					function_generate_golf_link  = "songkhla-golf.asp"
					
			END SELECT
			
	END SELECT
	
END FUNCTION
%>