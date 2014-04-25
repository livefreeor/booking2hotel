<%
FUNCTION function_generate_hotel_link(intDestinationID,intLocationID,intType)
	
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
					function_generate_hotel_link = "bangkok-hotels"
				Case ConstDesIDPhuket
					function_generate_hotel_link = "phuket-hotels"
				Case ConstDesIDChiangMai
					function_generate_hotel_link = "chiang-mai-hotels"
				Case ConstDesIDPattaya
					function_generate_hotel_link = "pattaya-hotels"
				Case ConstDesIDKohSamui
					function_generate_hotel_link = "koh-samui-hotels"
				Case ConstDesIDKrabi
					function_generate_hotel_link = "krabi-hotels"
				Case ConstDesIDKohChang 
					function_generate_hotel_link = "koh-chang-hotels"
				Case ConstDesIDMaeHongSon
					function_generate_hotel_link = "mae-hong-son-hotels"
				Case ConstDesIDChiangRai
					function_generate_hotel_link = "chiang-rai-hotels"
				Case ConstDesIDChaAm
					function_generate_hotel_link = "cha-am-hotels"
				Case ConstDesIDHuaHin
					function_generate_hotel_link = "hua-hin-hotels"
				Case ConstDesIDRayong
					function_generate_hotel_link = "rayong-hotels"
				Case ConstDesIDPhangNga
					function_generate_hotel_link = "phang-nga-hotels"
				Case ConstDesIDKohSamet
					function_generate_hotel_link = "koh-samet-hotels"
				Case ConstDesIDKanchanaburi
					function_generate_hotel_link = "kanchanaburi-hotels"
				Case ConstDesIDPrachuap	
					function_generate_hotel_link = "prachuap-khiri-khan-hotels"
				Case ConstDesIDKhaoYai
					function_generate_hotel_link = "khao-yai-hotels"
				Case ConstDesIDKohKood
					function_generate_hotel_link = "koh-kood-hotels"
				Case ConstDesIDKohPhangan
					function_generate_hotel_link = "koh-phangan-hotels"
				Case ConstDesIDTrang
					function_generate_hotel_link = "trang-hotels"
				Case ConstDesIDChumphon
					function_generate_hotel_link = "chumphon-hotels"
				Case ConstDesIDPhetchaburi
					function_generate_hotel_link = "phetchaburi-hotels"
				Case ConstDesIDKohTao
					function_generate_hotel_link = "koh-tao-hotels"
				Case ConstDesIDPhetchabun
					function_generate_hotel_link = "phetchabun-hotels"
				Case ConstDesIDPhitsanulok
					function_generate_hotel_link = "phitsanulok-hotels"
				Case ConstDesIDUthaiThani
					function_generate_hotel_link = "uthai-thani-hotels"
				Case ConstDesIDKhonkaen
					function_generate_hotel_link = "khonkaen-hotels"
				Case ConstDesIDNakhonratchasima
					function_generate_hotel_link = "Nakhonratchasima-hotels"
				Case ConstDesIDNakhonSiThammarat
					function_generate_hotel_link = "NakhonSiThammarat-hotels"
				Case ConstDesIDSongkhla
					function_generate_hotel_link = "Songkhla-hotels"
				Case ConstDesIDHatYai
					function_generate_hotel_link = "hat-yai-hotels"
				Case ConstDesIDChanthaburi
					function_generate_hotel_link = "chanthaburi-hotels"
				Case ConstDesIDAyutthaya
					function_generate_hotel_link = "ayutthaya-hotels"
				Case ConstOtherDestinations
					function_generate_hotel_link = "other_destination-hotels"
				Case ConstDesIDSuratthani
					function_generate_hotel_link = "suratthani-hotels"
				Case ConstDesIDSukhothai
					function_generate_hotel_link = "sukhothai-hotels"	
				Case ConstDesIDLampang
					function_generate_hotel_link = "lampang-hotels"	
				Case ConstDesIDTrat
					function_generate_hotel_link = "trat-hotels"	
				Case ConstDesIDLoei
					function_generate_hotel_link = "loei-hotels"	
				Case ConstDesIDNongKhai
					function_generate_hotel_link = "nong-khai-hotels"	
				Case ConstDesIDUbonRatchaThani
					function_generate_hotel_link = "ubon-ratchathani-hotels"	
				Case ConstDesIDUdonThani
					function_generate_hotel_link = "udon-thani-hotels"	
				Case ConstDesIDRanong
					function_generate_hotel_link = "ranong-hotels"	
				Case ConstDesIDSatun
					function_generate_hotel_link = "satun-hotels"	
				Case ConstDesIDChonburi
					function_generate_hotel_link = "chonburi-hotels"	
				Case ConstDesIDTak
					function_generate_hotel_link = "tak-hotels"	
				Case ConstDesIDNakhonPhanom
					function_generate_hotel_link = "nakhonphanom-hotels"	
				Case ConstDesIDRatchaburi
					function_generate_hotel_link = "ratchaburi-hotels"	
				Case ConstDesIDNonthaburi
					function_generate_hotel_link = "Nonthaburi-hotels"	
				Case ConstDesIDKamphaengphet
					function_generate_hotel_link = "kamphaengphet-hotels"	
				Case ConstDesIDSamutSongkhram
					function_generate_hotel_link = "samut-songkhram-hotels"	
				Case ConstDesIDNakornnayok
					function_generate_hotel_link = "nakornnayok-hotels"	
				Case ConstDesIDMukdahan
					function_generate_hotel_link = "mukdahan-hotels"	
				Case ConstDesIDPrachinburi
					function_generate_hotel_link = "prachinburi-hotels"	
				Case ConstDesIDSakonNakhon
					function_generate_hotel_link = "sakon-nakhon-hotels"	
				Case ConstDesIDSurin
					function_generate_hotel_link = "surin-hotels"	
				Case ConstDesIDSisaket
					function_generate_hotel_link = "sisaket-hotels"	
				Case ConstDesIDLamphun
					function_generate_hotel_link = "lamphun-hotels"	
				Case ConstDesIDSaraburi
					function_generate_hotel_link = "saraburi-hotels"	
			END SELECT
			
		Case 2 'Link To Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_hotel_link = "<a href=""/bangkok-hotels.asp"">Bangkok</a>"
				Case ConstDesIDPhuket
					function_generate_hotel_link = "<a href=""/phuket-hotels.asp"">Phuket</a>"
				Case ConstDesIDChiangMai
					function_generate_hotel_link = "<a href=""/chiang-mai-hotels.asp"">Chiang Mai</a>"
				Case ConstDesIDPattaya
					function_generate_hotel_link = "<a href=""/pattaya-hotels.asp"">Pattaya</a>"
				Case ConstDesIDKohSamui
					function_generate_hotel_link = "<a href=""/koh-samui-hotels.asp"">Koh Samui</a>"
				Case ConstDesIDKrabi
					function_generate_hotel_link = "<a href=""/krabi-hotels.asp"">Krabi</a>"
				Case ConstDesIDChiangRai
					function_generate_hotel_link = "<a href=""/chiang-rai-hotels.asp"">Chiang Rai</a>"
				Case ConstDesIDChaAm
					function_generate_hotel_link = "<a href=""/cha-am-hotels.asp"">Cha Am Hotels</a>"
				Case ConstDesIDHuaHin
					function_generate_hotel_link = "<a href=""/hua-hin-hotels.asp"">Hua Hin Hotels</a>"
				Case ConstDesIDPhitsanulok
					function_generate_hotel_link = "<a href=""/phitsanulok-hotels.asp"">Phitsanulok</a>"
				Case ConstDesIDRayong
					function_generate_hotel_link = "<a href=""/rayong-hotels.asp"">Rayong</a>"
				Case ConstDesIDMaeHongSon
					function_generate_hotel_link = "<a href=""/mae-hong-son-hotels.asp"">Mae Hong Son</a>"
				Case ConstDesIDAyutthaya
					function_generate_hotel_link = "<a href=""/ayutthaya-hotels.asp"">Ayutthaya</a>"
				Case ConstDesIDKanchanaburi
					function_generate_hotel_link = "<a href=""/kanchanaburi-hotels.asp"">Kanchanaburi</a>"
				Case ConstDesIDKohChang 
					function_generate_hotel_link = "<a href=""/koh-chang-hotels.asp"">Koh Chang</a>"
				Case ConstDesIDPrachuap	
					function_generate_hotel_link = "<a href=""/prachuap-khiri-khan-hotels.asp"">Prachuap Khiri khan</a>"
				Case ConstDesIDKohKood
					function_generate_hotel_link = "<a href=""/koh-kood-hotels.asp"">Koh Kood</a>"
				Case ConstDesIDKohSamet
					function_generate_hotel_link = "<a href=""/koh-samet-hotels.asp"">Koh Samet</a>"
				Case ConstDesIDPhangNga
					function_generate_hotel_link = "<a href=""/phang-nga-hotels.asp"">Phang Nga</a>"
				Case ConstDesIDKhaoYai
					function_generate_hotel_link = "<a href=""/khao-yai-hotels.asp"">Khao Yai</a>"
				Case ConstDesIDKohPhangan
					function_generate_hotel_link = "<a href=""/koh-phangan-hotels.asp"">Koh Phangan</a>"
				Case ConstDesIDTrang
					function_generate_hotel_link = "<a href=""/trang-hotels.asp"">Trang</a>"
				Case ConstDesIDChumphon
					function_generate_hotel_link = "<a href=""/chumphon-hotels.asp"">Chumphon</a>"
				Case ConstDesIDChanthaburi
					function_generate_hotel_link = "<a href=""/chanthaburi-hotels.asp"">Chanthaburi</a>"
				Case ConstDesIDPhetchaburi
					function_generate_hotel_link = "<a href=""/phetchaburi-hotels.asp"">Phetchaburi</a>"
				Case ConstDesIDNakhonratchasima
					function_generate_hotel_link = "<a href=""/Nakhonratchasima-hotels.asp"">Nakhonratchasima</a>"
				Case ConstDesIDKohTao
					function_generate_hotel_link = "<a href=""/koh-tao-hotels.asp"">Koh Tao</a>"
				Case ConstDesIDPhetchabun
					function_generate_hotel_link = "<a href=""/phetchabun-hotels.asp"">Phetchabun</a>"
				Case ConstDesIDUthaiThani
					function_generate_hotel_link = "<a href=""/uthai-thani-hotels.asp"">Uthai Thani</a>"
				Case ConstDesIDKhonkaen
					function_generate_hotel_link = "<a href=""/khonkaen-hotels.asp"">Khonkaen</a>"
				Case ConstDesIDNakhonSiThammarat
					function_generate_hotel_link = "<a href=""/NakhonSiThammarat-hotels.asp"">Nakhon Si Thammarat</a>"
				Case ConstDesIDSongkhla
					function_generate_hotel_link = "<a href=""/Songkhla-hotels.asp"">Songkhla</a>"
				Case ConstDesIDHatYai 
					function_generate_hotel_link = "<a href=""/Hat-Yai -hotels.asp"">Hat Yai </a>"
				Case ConstDesIDSuratthani
					function_generate_hotel_link = "<a href=""/suratthani-hotels.asp"">Suratthani</a>"
				Case ConstDesIDSukhothai
					function_generate_hotel_link = "<a href=""/sukhothai-hotels.asp"">Sukhothai</a>"
				Case ConstDesIDLampang
					function_generate_hotel_link = "<a href=""/lampang-hotels.asp"">Lampang</a>"
				Case ConstDesIDTrat
					function_generate_hotel_link = "<a href=""/trat-hotels.asp"">Trat</a>"
				Case ConstDesIDLoei
					function_generate_hotel_link = "<a href=""/loei-hotels.asp"">Loei</a>"
				Case ConstDesIDNongKhai
					function_generate_hotel_link = "<a href=""/nong-khai-hotels.asp"">Nong Khai</a>"
				Case ConstDesIDUbonRatchaThani
					function_generate_hotel_link = "<a href=""/ubon-ratchathani-hotels.asp"">Ubon Ratchathani</a>"
				Case ConstDesIDUdonThani
					function_generate_hotel_link = "<a href=""/udon-thani-hotels.asp"">Udon Thani</a>"
				Case ConstDesIDRanong
					function_generate_hotel_link = "<a href=""/ranong-hotels.asp"">Ranong</a>"
				Case ConstDesIDSatun
					function_generate_hotel_link = "<a href=""/satun-hotels.asp"">Satun</a>"
				Case ConstDesIDChonburi
					function_generate_hotel_link = "<a href=""/chonburi-hotels.asp"">Chonburi</a>"
				Case ConstDesIDTak
					function_generate_hotel_link = "<a href=""/tak-hotels.asp"">Tak</a>"
				Case ConstDesIDNakhonPhanom
					function_generate_hotel_link = "<a href=""/nakhonphanom-hotels.asp"">Nakhon Phanom</a>"
				Case ConstDesIDRatchaburi
					function_generate_hotel_link = "<a href=""/ratchaburi-hotels.asp"">Ratchaburi</a>"
				Case ConstDesIDNonthaburi
					function_generate_hotel_link = "<a href=""/Nonthaburi-hotels.asp"">Nonthaburi</a>"
				Case ConstDesIDKamphaengphet
					function_generate_hotel_link = "<a href=""/kamphaengphet-hotels.asp"">Kamphaengphet</a>"
				Case ConstDesIDSamutSongkhram
					function_generate_hotel_link = "<a href=""/samut-songkhram-hotels.asp"">Samut Songkhram</a>"
				Case ConstDesIDNakornnayok
					function_generate_hotel_link = "<a href=""/nakornnayok-hotels.asp"">Nakornnayok</a>"
				Case ConstDesIDMukdahan
					function_generate_hotel_link = "<a href=""/mukdahan-hotels.asp"">Mukdahan</a>"
				Case ConstDesIDPrachinburi
					function_generate_hotel_link = "<a href=""/prachinburi-hotels.asp"">Prachinburi</a>"
				Case ConstDesIDSakonNakhon
					function_generate_hotel_link = "<a href=""/sakon-nakhon-hotels.asp"">SakonNakhon</a>"
				Case ConstDesIDSurin
					function_generate_hotel_link = "<a href=""/surin-hotels.asp"">Surin</a>"
				Case ConstDesIDSisaket
					function_generate_hotel_link = "<a href=""/sisaket-hotels.asp"">Sisaket</a>"
				Case ConstDesIDLamphun
					function_generate_hotel_link = "<a href=""/lamphun-hotels.asp"">Lamphun</a>"
				Case ConstDesIDSaraburi
					function_generate_hotel_link = "<a href=""/saraburi-hotels.asp"">Saraburi</a>"
			END SELECT
			
		Case 3 'Link To Location
			SELECT CASE intLocationID
				'### Bangkok ###
				Case 58 ' New Petchburi Road
					function_generate_hotel_link = "<a href=""/bangkok-new-petchburi-road-hotels.asp"">New Petchburi Road</a>"
				Case 59 'Sukhumvit
					function_generate_hotel_link = "<a href=""/bangkok-sukhumvit-hotels.asp"">Sukhumvit</a>"
				Case 62 'Airport
					function_generate_hotel_link = "<a href=""/bangkok-airport-hotels.asp"">Suvarnabhumi Airport</a>"
				Case 63 'Pratunam
					function_generate_hotel_link = "<a href=""/bangkok-pratunam-hotels.asp"">Pratunam</a>"
				Case 65 'Pattanakarn
					function_generate_hotel_link = "<a href=""/bangkok-pattanakarn-hotels.asp"">Pattanakarn</a>"
				Case 70 'Ratchadapisek
					function_generate_hotel_link = "<a href=""/bangkok-ratchadapisek-hotels.asp"">Ratchadapisek</a>"
				Case 90 'China Town
					function_generate_hotel_link = "<a href=""/bangkok-china-town-hotels.asp"">China Town</a>"
				Case 91 'Silom
					function_generate_hotel_link = "<a href=""/bangkok-silom-hotels.asp"">Silom</a>"
				Case 124 'Sathorn
					function_generate_hotel_link = "<a href=""/bangkok-sathorn-hotels.asp"">Sathorn</a>"
				Case 125 'River Side
					function_generate_hotel_link = "<a href=""/bangkok-river-side-hotels.asp"">River Side</a>"
				Case 143 'Rama 9 Road
					function_generate_hotel_link = "<a href="""">Rama 9 Road</a>"
				Case 171 'Siam
					function_generate_hotel_link = "<a href=""/bangkok-siam-hotels.asp"">Siam</a>"
				Case 106 'Chatuchak
					function_generate_hotel_link = "<a href=""/bagkok-chatuchak-hotels.asp"">Chatuchak</a>"
				Case 189 'Chatuchak
					function_generate_hotel_link = "<a href=""/bangkok-khaosan-hotels.asp"">Khaosan Road</a>"
				Case 191 'Wireless Road
					function_generate_hotel_link = "<a href=""/bangkok-wireless-road-hotels.asp"">Wireless Road</a>"
				Case 284 'Other
					function_generate_hotel_link = "<a href=""/bangkok-other-hotels.asp"">Other Area</a>"
				Case 291 'Donmuang
					function_generate_hotel_link = "<a href=""/bangkok-don-muang-airport-hotels.asp"">Don Muang Airport</a>"

					
				'### Phuket ###
				Case 60 'Bang Tao
					function_generate_hotel_link = "<a href=""/phuket-bang-tao-hotels.asp"">Bang Tao</a>"
				Case 61 'Patong
					function_generate_hotel_link = "<a href=""/phuket-patong-beach-hotels.asp"">Patong Beach</a>"
				Case 68 'Karon Beach
					function_generate_hotel_link = "<a href=""/phuket-karon-beach-hotels.asp"">Karon Beach</a>"
				Case 69 'Kata Beach
					function_generate_hotel_link = "<a href=""/phuket-kata-beach-hotels.asp"">Kata Beach</a>"
				Case 71 'Phuket Town
					function_generate_hotel_link = "<a href=""/phuket-phuket-town-hotels.asp"">Phuket Town </a>"
				Case 76 'Cape Panwa
					function_generate_hotel_link = "<a href=""/phuket-cape-panwa-hotels.asp"">Cape Panwa</a>"
				Case 79 'NaiYang
					function_generate_hotel_link = "<a href=""/phuket-nai-yang-beach-hotels.asp"">Nai Yang Beach</a>"
				Case 87 'Kamala
					function_generate_hotel_link = "<a href=""/phuket-kamala-beach-hotels.asp"">Kamala Beach</a>"
				Case 89 'Lyan
					function_generate_hotel_link = "<a href=""/phuket-layan-beach-hotels.asp"">Layan Beach</a>"
				Case 107 'Rawai
					function_generate_hotel_link = "<a href=""/phuket-rawai-beach-hotels.asp"">Rawai Beach</a>"
				Case 147 'Nai Harn
					function_generate_hotel_link = "<a href=""/phuket-nai-harn-beach-hotels.asp"">Nai Harn Beach</a>"
				Case 173 'Koh Yao
					function_generate_hotel_link = "<a href=""/phuket-koh-yao-hotels.asp"">Koh Yao</a>"
				Case 178 'Surin Beach
					function_generate_hotel_link = "<a href=""/phuket-surin-beach-hotels.asp"">Surin Beach</a>"
				Case 188 'Koh Lon
					function_generate_hotel_link = "<a href=""/phuket-koh-lon-island-hotels.asp"">Koh Lon Island</a>"
				Case 234 'Naitorn Beach
					function_generate_hotel_link = "<a href=""/phuket-naitorn-beach-hotels.asp"">Naitorn Beach</a>"
				Case 228 'Phuket Airport
					function_generate_hotel_link = "<a href=""/phuket-airport-hotels.asp"">Phuket Airport</a>"
				Case 267 'Coral Island
					function_generate_hotel_link = "<a href=""/phuket-coral-island.asp"">Coral Island</a>"
				Case 372 'Cape Yamu
					function_generate_hotel_link = "<a href=""/phuket-cape-yamu-hotels.asp"">Cape Yamu</a>"
				Case 381 'Po Bay
					function_generate_hotel_link = "<a href=""/phuket-po-bay-hotels.asp"">Po Bay</a>"
				Case 392	'Koh Racha Yai
					function_generate_hotel_link = "<a href=""/phuket-koh-racha-yai-hotels.asp"">Koh Racha Yai</a>"
				Case 401	'Chalong Bay
					function_generate_hotel_link = "<a href=""/phuket-chalong-bay-hotels.asp"">Chalong Bay</a>"
				Case 295	'Mai Khao Beach
					function_generate_hotel_link = "<a href=""/phuket-mai-khao-beach-hotels.asp"">Mai Khao Beach</a>"
				Case 296	'Racha Island
					function_generate_hotel_link = "<a href=""/phuket-racha-island-hotels.asp"">Racha Island</a>"

				'### Chiang Mai ###
				Case 72 'Chiang Mai City
					function_generate_hotel_link = "<a href=""/chiang-mai-town-hotels.asp"">Chiang Mai City</a>"
				Case 77 'Hang Dong
					function_generate_hotel_link = "<a href=""/chiang-mai-hangdong-hotels.asp"">Hangdong</a>"
				Case 80 'Rail Way
					function_generate_hotel_link = "<a href=""/chiang-mai-railway-road-hotels.asp"">Railway Road</a>"
				Case 81 'Huay Kaew
					function_generate_hotel_link = "<a href=""/chiang-mai-huay-kaew-hotels.asp"">Huay Kaew</a>"
				Case 82 'Sridonchai
					function_generate_hotel_link = "<a href=""/chiang-mai-sridonchai-road-hotels.asp"">Sridonchai Road</a>"
				Case 83 'Mae Rim
					function_generate_hotel_link = "<a href=""/chiang-mai-mae-rim-hotels.asp"">Mae Rim</a>"
				Case 85 'Parton
					function_generate_hotel_link = "<a href=""chiang-mai-parton-hotels.asp/"">Parton</a>"
				Case 86 'Mae Ai
					function_generate_hotel_link = "<a href=""/chiang-mai-mae-ai-hotels.asp"">Mae Ai</a>"
				Case 334 'San Kamphaeng
					function_generate_hotel_link = "<a href=""/chiang-mai-san-kamphaeng-hotels.asp"">San Kamphaeng</a>"
				Case 144 'Angkhang
					function_generate_hotel_link = "<a href=""/chiang-mai-angkhang-hotels.asp"">Angkhang</a>"
				Case 175 'Sansai
					function_generate_hotel_link = "<a href=""/chiang-mai-sansai-hotels.asp"">Sansai</a>"
				Case 226 'Saraphee
					function_generate_hotel_link = "<a href=""/chiang-mai-saraphee-hotels.asp"">Saraphee</a>"
				Case 231 'Mae Rim-Samoeng
					function_generate_hotel_link = "<a href=""/chiang-mai-mae-rim-samoeng-hotels.asp"">Mae Rim Samoeng</a>"
				Case 232 'Ampur Muang
					function_generate_hotel_link = "<a href=""/chiang-mai-ampur-muang-hotels.asp"">Ampur Muang</a>"
				Case 233 'A.Maetang
					function_generate_hotel_link = "<a href=""/chiang-mai-a.maetang-hotels.asp"">A.Maetang</a>"
				Case 344 'Doi Saket
					function_generate_hotel_link = "<a href=""/chiang-mai-doi-saket-hotels.asp"">Doi Saket</a>"
				Case 436 'Chiang Dao
					function_generate_hotel_link = "<a href=""/chiang-mai-chiang-dao-hotels.asp"">Chiang Dao</a>"

					
				'### Pattaya ###
				Case 92 'North Pattaya
					function_generate_hotel_link = "<a href=""/pattaya-north-pattaya-hotels.asp"">North Pattaya</a>"
				Case 93 'South Pattaya
					function_generate_hotel_link = "<a href=""/pattaya-sout-pattaya-hotels.asp"">South Pattaya </a>"
				Case 94 'Jomtien Beach
					function_generate_hotel_link = "<a href=""/pattaya-jomtien-pattaya-hotels.asp"">Jomtien Beach</a>"
				Case 95 'Pattaya City
					function_generate_hotel_link = "<a href=""/pattaya-city-hotels.asp"">Pattaya City</a>"
				Case 186 'Tambon Pong
					function_generate_hotel_link = "<a href=""/pattaya-tambon-pong.asp"">Tambon Pong</a>"
				Case 245 'Wongamart Beach
					function_generate_hotel_link = "<a href=""/pattaya-wongamart-beach-hotels.asp"">Wongamart Beach</a>"
				Case 253 'Phra Tamnuk Hill
					function_generate_hotel_link = "<a href=""/pattaya-phra-tamnuk-hill-hotels.asp"">Phra Tamnuk Hill</a>"
				Case 468 'Naklua
					function_generate_hotel_link = "<a href=""/pattaya-naklua-hotels.asp"">Naklua</a>"

				'### Samui ###
				Case 112 'Lamai Beach
					function_generate_hotel_link = "<a href=""/koh-samui-lamai-hotels.asp"">Lamai Beach</a>"
				Case 113 'Chaweng Beach
					function_generate_hotel_link = "<a href=""/koh-samui-chaweng-hotels.asp"">Chaweng Beach</a>"
				Case 114 'Bo Phut Beach
					function_generate_hotel_link = "<a href=""/koh-samui-bo-phut-hotels.asp"">Bo Phut Beach</a>"
				Case 148 'Maenam Beach
					function_generate_hotel_link = "<a href=""/koh-samui-maenam-beach-hotels.asp"">Maenam Beach</a>"
				Case 149 'Chaweng Noi Beach
					function_generate_hotel_link = "<a href=""/koh-samui-chaweng-noi-beach-hotels.asp"">Chaweng Noi Beach</a>"
				Case 151 'Bo Phut Village
					function_generate_hotel_link = "<a href=""/koh-samui-bo-phut-village-hotels.asp"">Bo Phut Village</a>"
				Case  153 'Choengmon Beach
					function_generate_hotel_link = "<a href=""/koh-samui-choengmon-beach-hotels.asp"">Choengmon Beach</a>"
				Case  190 'Laem Set Beach
					function_generate_hotel_link = "<a href=""/koh-samui-laem-set-beach-hotels.asp"">Laem Set Beach</a>"
				Case  220 'Had Bang Ma kham
					function_generate_hotel_link = "<a href=""/koh-samui-had-bang-ma-kham.asp"">Had Bang Ma Kham</a>"
				Case  285 'Laem Nan Beach
					function_generate_hotel_link = "<a href=""/koh-samui-laem-nan-beach-hotels.asp"">Laem Nan Beach</a>"
				Case  315 'Natien Beach
					function_generate_hotel_link = "<a href=""/koh-samui-natien-beach-hotels.asp"">Natien Beach</a>"
				Case  319 'Lipa noi Beach
					function_generate_hotel_link = "<a href=""/koh-samui-lipa-noi-beach-hotels.asp"">Lipa Noi Beach</a>"
				Case  322 'Tongson Bay
					function_generate_hotel_link = "<a href=""/koh-samui-tongson-bay-hotels.asp"">Tongson Bay</a>"
				Case  341 'Thong Yang Beach
					function_generate_hotel_link = "<a href=""/koh-samui-thong-yang-beach-hotels.asp"">Thong Yang Beach</a>"
				Case  345 'Bang Rak Beach
					function_generate_hotel_link = "<a href=""/koh-samui-bang-rak-beach-hotels.asp"">Bang Rak Beach</a>"
				Case  374 'Bang Rak Beach
					function_generate_hotel_link = "<a href=""/koh-samui-thong-tanote-beach-hotels.asp"">Thong Tanote Beach</a>"
				Case  387 'Phang Ka Beach
					function_generate_hotel_link = "<a href=""/koh-samui-phang-ka-beach-hotels.asp"">Phang Ka Beach</a>"
				Case  403 'Big Buddha Beach
					function_generate_hotel_link = "<a href=""/koh-samui-big-buddha-beach-hotels.asp"">Big Buddha Beach</a>"
				Case  152 'Taling Ngam Beach
					function_generate_hotel_link = "<a href=""/koh-samui-taling-ngam-beach-hotels.asp"">Taling Ngam Beach</a>"

				'### Krabi ###
				Case 116 'Krabi City
					function_generate_hotel_link = "<a href=""/krabi-city-hotels.asp"">Krabi City</a>"
				Case 117 'Koh Lanta
					function_generate_hotel_link = "<a href=""/krabi-koh-lanta-hotels.asp"">Koh Lanta</a>"
				Case 118 'Phi Phi Island
					function_generate_hotel_link = "<a href=""/krabi-phi-phi-island-hotels.asp"">Phi Phi Island</a>"
				Case 119 'Ao Nang
					function_generate_hotel_link = "<a href=""/krabi-ao-nang-hotels.asp"">Ao Nang</a>"
				Case 170 'Klong Muang Beach
					function_generate_hotel_link = "<a href=""/krabi-klong-muang-beach-hotels.asp"">Klong Muang Beach</a>"
				Case 242 'Railay Beach
					function_generate_hotel_link = "<a href=""/krabi-railay-beach-hotels.asp"">Railay Beach</a>"
				Case 243 'Tonsai Bay
					function_generate_hotel_link = "<a href=""/krabi-tonsai-bay-hotels.asp"">Tonsai Bay</a>"
				Case 246 'Nopparat Beach
					function_generate_hotel_link = "<a href=""/krabi-nopparat-beach.asp"">Nopparat Beach</a>"
				Case 269 'Koh Ngai
					function_generate_hotel_link = "<a href=""/krabi-koh-ngai.asp"">Koh Ngai</a>"
				Case 287 'Pranang Cape
					function_generate_hotel_link = "<a href=""/krabi-pranang-cape-hotels.asp"">Pranang Cape</a>"
				Case 347 'Klong Muang
					function_generate_hotel_link = "<a href=""/krabi-klong-muang-hotels.asp"">Klong Muang</a>"
				Case 386 'Had Yao
					function_generate_hotel_link = "<a href=""/krabi-had-yao-hotels.asp"">Had Yao</a>"
				Case 465 'Klong Thom
					function_generate_hotel_link = "<a href=""/krabi-klong-thom-hotels.asp"">Klong Thom</a>"
                Case 467 'Koh Klang
					function_generate_hotel_link = "<a href=""/krabi-koh-klang-hotels.asp"">Koh Klang</a>"
			
				'### Koh Chang ###	
				Case 198	'Klong Son
					function_generate_hotel_link = "<a href=""/koh-chang-klong-son.asp"">Klong Son</a>"
				Case 199	'Tha Nam Beach
					function_generate_hotel_link = "<a href=""/koh-chang-tha-nam-beach.asp"">Tha Nam Beach</a>"
				Case 200	'Kai Bae Beach
					function_generate_hotel_link = "<a href=""/koh-chang-kai-bae-beach.asp"">Kai Bae Beach</a>"
				Case 201	'Klong Prao Beach
					function_generate_hotel_link = "<a href=""/koh-chang-klong-prao-beach.asp"">Klong Prao Beach</a>"
				Case 202	'White Sand Beach
					function_generate_hotel_link = "<a href=""/koh-chang-white-sand-beach.asp"">White Sand Beach</a>"
				Case 214	'Koh Kood
					function_generate_hotel_link = "<a href=""/koh-chang-koh-kood.asp"">Koh Kood</a>"
				Case 302	'Ao Bang Bao
					function_generate_hotel_link = "<a href=""/koh-chang-ao-bang-bao-beach.asp"">Ao Bang Bao</a>"
				Case 371	'Salak Kok Bay
					function_generate_hotel_link = "<a href=""/salak-kok-bay-hotels.asp"">Salak Kok Bay</a>"
				Case 395	'Salakphet Bay
					function_generate_hotel_link = "<a href=""/koh-chang-salakphet-bay-hotels.asp"">Salakphet Bay</a>"
				Case 396	'Bai Lan Bay
					function_generate_hotel_link = "<a href=""/koh-chang-bai-lan-bay-hotels.asp"">Bai Lan Bay</a>"
				Case 464	'Kong Kang Bay
					function_generate_hotel_link = "<a href=""/koh-chang-kong-kang-bay-hotels.asp"">Kong Kang Bay</a>"

				'### Mae Hong Son ##
				Case 174	'Mae Hong Son City
					function_generate_hotel_link = "<a href=""/mae-hong-son-city-hotels.asp"">Mae Hong Son City</a>"
				Case 195	'Mae Sot
					function_generate_hotel_link = "<a href=""/mae-hong-son-mae-sot-hotels.asp"">Mae Sot</a>"
				Case 196	'Pai
					function_generate_hotel_link = "<a href=""/mae-hong-son-pai-hotels.asp"">Pai</a>"
				Case 289	'Mae Sariang
					function_generate_hotel_link = "<a href=""/mae-hong-son-mae-sariang-hotels.asp"">Mae Sariang</a>"

				'### Chiang Rai #####
				Case 154	'Golden Triangle
					function_generate_hotel_link = "<a href=""/chiang-rai-golden-triangle-hotels.asp"">Golden Triangle</a>"
				Case 158	'Mae Chan
					function_generate_hotel_link = "<a href=""/chiang-rai-mae-chan-hotels.asp"">Mae Chan</a>"
				Case 159	'Mae Sai
					function_generate_hotel_link = "<a href=""/chiang-rai-mae-sai-hotels.asp"">Mai Sai</a>"
				Case 167	'Chiang Saen
					function_generate_hotel_link = "<a href=""/chiang-rai-chiang-saen.asp"">Chiang Saen</a>"
				Case 192	'Chiang Rai City
					function_generate_hotel_link = "<a href=""/chiang-rai-city-hotels.asp"">Chiang Rai City</a>"
				Case 193	'Kok River
					function_generate_hotel_link = "<a href=""chiang-rai-kok-river-hotels.asp/"">Kok River</a>"
				Case 194	'Doi Mae Salong
					function_generate_hotel_link = "<a href=""/chiang-rai-doi-mae-salong-hotels.asp"">Doi Mae Salong</a>"
				Case 378	'Chiang Khong
					function_generate_hotel_link = "<a href=""/chiang-rai-chiang-khong-hotels.asp"">Chiang Khong</a>"				

				'### Hua Hin ###
				Case 157	'Hua Hin
					function_generate_hotel_link = "<a href=""/hua-hin-city-hotels.asp"">Hua Hin</a>"
					
				'### Cha Am ###
				Case 156	'Cha Am
					function_generate_hotel_link = "<a href=""/cha-am-city-hotels.asp"">Cha Am</a>"
					
				'### Rayong ###
				Case 162	'Ban Chang
					function_generate_hotel_link = "<a href=""/rayong-ban-chang-hotels.asp"">Ban Chang</a>"
				Case 163	'Pae Klaeng
					function_generate_hotel_link = "<a href=""/rayong-pae-klaeng-hotels.asp"">Pae Klaeng</a>"
				Case 164	'Kram
					function_generate_hotel_link = "<a href=""/rayong-pae-kram-hotels.asp"">Kram</a>"
				Case 168	'Klaeng
					function_generate_hotel_link = "<a href=""/rayong-pae-kleang-hotels.asp"">Kleang</a>"
				Case 204	'Had Noina Beach
					function_generate_hotel_link = "<a href=""/rayong-had-noina-beach-hotels.asp"">Noina Beach (Koh Samet)</a>"
				Case 205	'Rayong City
					function_generate_hotel_link = "<a href=""/rayong-city-hotels.asp"">Rayong City</a>"
				Case 206	'Mae Rumphung Beach
					function_generate_hotel_link = "<a href=""/rayong-mae-rumphung-beach-hotels.asp"">Mae Rumphung Beach</a>"
				Case 239	'Laem Mae Pim
					function_generate_hotel_link = "<a href=""/rayong-laem-mae-pim-hotels.asp"">Laem Mae Phim</a>"
				Case 241	'Phala Beach
					function_generate_hotel_link = "<a href=""/rayong-phala-beach-hotels.asp"">Phala Beach</a>"

				'### Phang Nga ###
				Case 247
					function_generate_hotel_link = "<a href=""/phang-nga-natai-beach.asp"">Natai Beach</a>"
				Case 248
					function_generate_hotel_link = "<a href=""/phang-nga-khao-lak-hotels.asp"">Khao Lak</a>"
				Case 257
					function_generate_hotel_link = "<a href=""/phang-nga-koh-yao-noi.asp"">Koh Yao Noi</a>"
				Case 264	'Kuraburi
					function_generate_hotel_link = "<a href=""/phang-nga-kuraburi-hotels.asp"">Kuraburi</a>"
				Case 266
					function_generate_hotel_link = "<a href=""/phang-nga-koh-kor-khao.asp"">Koh Kor Khao</a>"
				Case 297
					function_generate_hotel_link = "<a href=""/phang-nga-bor-saen-hotels.asp"">Bor Saen</a>"
				Case 298
					function_generate_hotel_link = "<a href=""/phang-nga-nang-thong-beach-hotels.asp"">Nang Thong Beach</a>"
				Case 299
					function_generate_hotel_link = "<a href=""/phang-nga-pilai-beach-hotels.asp"">Pilai Beach</a>"
				Case 358	'Pakarang Beach
					function_generate_hotel_link = "<a href=""/phang-nga-pakarang-beach-hotels.asp"">Pakarang Beach</a>"
				Case 459	'Tai Muang
					function_generate_hotel_link = "<a href=""/phang-nga-tai-muang-hotels.asp"">Tai Muang</a>"
				Case 470	'Koh Yao Yai
					function_generate_hotel_link = "<a href=""/phang-nga-koh-yao-yai-hotels.asp"">Koh Yao Yai</a>"

				'### Koh Samet ###
				Case 207
					function_generate_hotel_link = "<a href=""/koh-samet-ao-prao-beach-hotels.asp"">Ao Prao Beach</a>"
				Case 236
					function_generate_hotel_link = "<a href=""/koh-samet-ao-ku-hotels.asp"">Ao Ku</a>"
				Case 244
					function_generate_hotel_link = "<a href=""/koh-samet-vongduean-beach-hotels.asp"">Vongduean Beach</a>"
				Case 249
					function_generate_hotel_link = "<a href=""/koh-samet-saikaew-beach-hotels.asp"">Saikaew Beach</a>"
				Case 250
					function_generate_hotel_link = "<a href=""/koh-samet-ao-noina-hotels.asp"">Ao Noina</a>"
				Case 356
					function_generate_hotel_link = "<a href=""/koh-samet-ao-pagarang-hotels.asp"">Ao Pagarang</a>"
				Case 454
					function_generate_hotel_link = "<a href=""/koh-samet-ao-cho-hotels.asp"">Ao Cho</a>"

				'### KanchanaBuri
				Case 218
					function_generate_hotel_link = "<a href=""/kanchanaburi-saiyoke-hotels.asp"">Saiyoke</a>"
				Case 219
					function_generate_hotel_link = "<a href=""/kanchanaburi-kaeng-sean-hotels.asp"">Kaeng Sean</a>"
				Case 240
					function_generate_hotel_link = "<a href=""/kanchanaburi-city-hotels.asp"">Kanchanaburi City</a>"
				Case 251
					function_generate_hotel_link = "<a href=""/kanchanaburi-thamakham-hotels.asp"">Thamakham</a>"
				Case 252
					function_generate_hotel_link = "<a href=""/kanchanaburi-kwai-yai-hotels.asp"">Kwai Yai</a>"
				Case 288
					function_generate_hotel_link = "<a href=""/kanchanaburi-kwai-noi-hotels.asp"">Kwai Noi River</a>"
				Case 329
					function_generate_hotel_link = "<a href=""/kanchanaburi-thong-pha-poom-hotels.asp"">Thong Pha Poom</a>"
				Case 332
					function_generate_hotel_link = "<a href=""/kanchanaburi-sungklaburi-hotels.asp"">Sungklaburi</a>"
				Case 449
					function_generate_hotel_link = "<a href=""/kanchanaburi-srinakarin-national-park-hotels.asp"">Srinakarin National Park</a>"

				'### Khao Yai
				Case 268 'Khao Yai
					function_generate_hotel_link = "<a href=""/khao-yai-khao-yai-hotels.asp"">Khao Yai</a>"

				'### Prachuap Khiri khan
				Case 209 'Bang Saphan
					function_generate_hotel_link = "<a href=""/prachuap-khiri-khan-bang-saphan-hotels.asp"">Bang Saphan</a>"
				Case 210 'Thap Sakae
					function_generate_hotel_link = "<a href=""/prachuap-khiri-khan-thap-sakae-hotels.asp"">Thap Sakae</a>"
				Case 211 'Pranburi
					function_generate_hotel_link = "<a href=""/prachuap-khiri-khan-pranburi-hotels.asp"">Pranburi</a>"
				Case 212 'Koh Talu
					function_generate_hotel_link = "<a href=""/prachuap-khiri-khan-koh-talu-hotels.asp"">Koh Talu</a>"
				Case 223 'BanKrut Beach
					function_generate_hotel_link = "<a href=""/prachuap-khiri-khan-bankrut-beach-hotels.asp"">BanKrut Beach</a>"
				Case 229 'Khao Sam Roi Yot
					function_generate_hotel_link = "<a href=""/prachuap-khiri-khan-khao-sam-roi-yot-hotels.asp"">Khao Sam Roi Yot</a>"
				Case 235 'Koh Ngai
					function_generate_hotel_link = "<a href=""/prachuap-khiri-khan-city-hotels.asp"">Prachuap Khiri khan City</a>"
					
				'### Koh Kood
				Case 237 'Klong Chao
					function_generate_hotel_link = "<a href=""/koh-kood-klong-chao-hotels.asp"">Klong Chao</a>"
				Case 238 'Bang Bao Beach
					function_generate_hotel_link = "<a href=""/koh-kood-bang-bao-beach-hotels.asp"">Bang Bao Beach</a>"
				Case 301 'Ao Tapao
					function_generate_hotel_link = "<a href=""/koh-kood-ao-tapao-hotels.asp"">Ao Tapao</a>"
				Case 304 'Koh Mak
					function_generate_hotel_link = "<a href=""/koh-kood-koh-mak-hotels.asp"">Koh Mak</a>"
				Case 316'Ao Noi
					function_generate_hotel_link = "<a href=""/koh-kood-ao-noi-hotels.asp"">Ao Noi</a>"
				Case 339 'Ao Klong Jark 
					function_generate_hotel_link = "<a href=""/koh-kood-ao-klong-jark-hotels.asp"">Ao Klong Jark</a>"
				Case 340 'Ao Ngarm Ko
					function_generate_hotel_link = "<a href=""/koh-kood-ao-ngarm-ko-hotels.asp"">Ao Ngarm Ko</a>"
				Case 308 'Ao Yai Kee
					function_generate_hotel_link = "<a href=""/koh-kood-ao-yai-kee-hotels.asp"">Ao Yai Kee</a>"

				'### Koh Phangan
				Case 270 'Rin Nai Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-rin-nai-beach-hotels.asp"">Rin Nai Beach</a>"
				Case 271 'Thong Nai Pan Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-thong-nai-pan-beach-hotels.asp"">Thong Nai Pan Beach</a>"
				Case 272 'Mae Haad Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-mae-haad-beach-hotels.asp"">Mae Haad Beach</a>"
				Case 273 'Salad Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-salad-beach-hotels.asp"">Salad Beach</a>"
				Case 274 'Bantai Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-banthai-beach-hotels.asp"">Bantai Beach</a>"
				Case 280 'Yao Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-yao-beach-hotels.asp"">Yao Beach</a>"
				Case 281 'Koh Maa
					function_generate_hotel_link = "<a href=""/koh-phangan-koh-ma-hotels.asp"">Koh Maa</a>"
				Case 300 'Haad Dao Deuk
					function_generate_hotel_link = "<a href=""/koh-phangan-haad-dao-deuk-hotels.asp"">Haad Dao Deuk</a>"
				Case 305 'Thong Sala
					function_generate_hotel_link = "<a href=""/koh-phangan-thong-sala-hotels.asp"">Thong Sala</a>"
				Case 307 'Rin Nok Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-rin-nok-beach-hotels.asp"">Rin Nok Beach</a>"
				Case 310 'Yuan Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-yuan-beach-hotels.asp"">Yuan Beach</a>"
				Case 312 'Ao Chao Phao
					function_generate_hotel_link = "<a href=""/koh-phangan-ao-chao-phao-hotels.asp"">Ao Chao Phao</a>"
				Case 313 'Ao Nai Wok
					function_generate_hotel_link = "<a href=""/koh-phangan-ao-nai-wok-hotels.asp"">Ao Nai Wok</a>"
				Case 314 'Ao Chaloklum
					function_generate_hotel_link = "<a href=""/koh-phangan-ao-chaloklum-hotels.asp"">Ao Chaloklum</a>"
				Case 317 'Haad Leela
					function_generate_hotel_link = "<a href=""/koh-phangan-haad-leela-hotels.asp"">Haad Leela</a>"
				Case 325 'Ao Bang Charu
					function_generate_hotel_link = "<a href=""/koh-phangan-bang-charu-hotels.asp"">Ao Bang Charu</a>"
				Case 408 'Plai Laem Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-plai-laem-beach-hotels.asp"">Plai Laem Beach</a>"
				Case 409 'Haad Tian Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-haad-tian-beach-hotels.asp"">Haad Tian Beach</a>"
				Case 440 'Srithanu Beach
					function_generate_hotel_link = "<a href=""/koh-phangan-srithanu-beach-hotels.asp"">Srithanu Beach</a>"

				'### Trang ###
				Case 275 'Chang Lang Beach
					function_generate_hotel_link = "<a href=""/trang-chang-lang-beach-hotels.asp"">Chang Lang Beach</a>"
				Case 277 'Trang City
					function_generate_hotel_link = "<a href=""/trang-city-hotels.asp"">Trang City</a>"
				Case 278 'Pak Meng Beach
					function_generate_hotel_link = "<a href=""/trang-pak-meng-beach-hotels.asp"">Pak Meng Beach</a>"
				Case 279 'Koh Sukorn
					function_generate_hotel_link = "<a href=""/trang-koh-sukorn-hotels.asp"">Koh Sukorn</a>"
				Case 283 'Koh Mook
					function_generate_hotel_link = "<a href=""/trang-koh-mook-hotels.asp"">Koh Mook</a>"
				Case 348 'Koh Libong
					function_generate_hotel_link = "<a href=""/trang-koh-libong-hotels.asp"">Koh Libong</a>"
				Case 394 'Kradan Island
					function_generate_hotel_link = "<a href=""/trang-kradan-island-hotels.asp"">Kradan Island</a>"


				'### Chumhon ###
				Case 276 'Thung Wua Laen Beach
					function_generate_hotel_link = "<a href=""/chumphon-thung-wua-laen-beach-hotels.asp"">Thung Wua Laen Beach</a>"
				Case 290 'Arunothai Beach
					function_generate_hotel_link = "<a href=""/chumphon-arunothai-beach-hotels.asp"">Arunothai Beach</a>"
				Case 335 'Chumporn City
					function_generate_hotel_link = "<a href=""/chumphon-city-hotels.asp"">Chumporn City</a>"
				Case 343 'Langsuan
					function_generate_hotel_link = "<a href=""/chumphon-langsuan-hotels.asp"">Langsuan</a>"
				Case 426 'Thung Makham Bay
					function_generate_hotel_link = "<a href=""/chumphon-thung-makham-bay-hotels.asp"">Thung Makham Bay</a>"
				Case 309 'Paknam Tako
					function_generate_hotel_link = "<a href=""/chumporn-paknam-tako-hotels.asp"">Paknam Tako</a>"
				Case 375 'Paradonpab Beach
					function_generate_hotel_link = "<a href=""/chumporn-paradonpab-beach-hotels.asp"">Paradonpab Beach</a>"
				Case 428 'Sai Ree Beach
					function_generate_hotel_link = "<a href=""/chumphon-sai-ree-beach-hotels.asp"">Sai Ree Beach</a>"
				Case 456 'Bangburd Beach
					function_generate_hotel_link = "<a href=""/chumphon-bangburd-beach-hotels.asp"">Bangburd Beach</a>"

				'### Phetchaburi ###
				Case 303 'Haad Chao Samran
					function_generate_hotel_link = "<a href=""/phetchaburi-haad-chao-samran-hotels.asp"">Haad Chao Samran</a>"
				Case 294 'Kaeng Krachan 
					function_generate_hotel_link = "<a href=""/phetchaburi-kaeng-krachan-hotels.asp"">Kaeng Krachan</a>"
				Case 318 'Puk Tien Beach
					function_generate_hotel_link = "<a href=""/phetchaburi-puk-tien-beach-hotels.asp"">Puk Tien Beach</a>"
				Case 455 'Phetchaburi City
					function_generate_hotel_link = "<a href=""/phetchaburi-city-hotels.asp"">Phetchaburi City</a>"
				Case 457 'Nongyapong
					function_generate_hotel_link = "<a href=""/phetchaburi-nongyapong-hotels.asp"">Nongyapong</a>"

				'### Koh Tao ###
				Case 324 'Chalok Baan Kao Bay
					function_generate_hotel_link = "<a href=""/koh-tao-chalok-baan-kao-hotels.asp"">Chalok Baan Kao Bay</a>"
				Case 327 'Mae Haad Beach
					function_generate_hotel_link = "<a href=""/koh-tao-mae-haad-beach-hotels.asp"">Mae Haad Beach</a>"
				Case 323 'Sairee Beach
					function_generate_hotel_link = "<a href=""/koh-tao-sairee-beach-hotels.asp"">Sairee Beach</a>"
				Case 328 'Tanote Beach
					function_generate_hotel_link = "<a href=""/koh-tao-tanote-beach-hotels.asp"">Tanote Beach</a>"
				Case 330 'Jansom Beach
					function_generate_hotel_link = "<a href=""/koh-tao-jansom-beach-hotels.asp"">Jansom Beach</a>"
				Case 331 'Thian Og Bay
					function_generate_hotel_link = "<a href=""/koh-tao-thian-og-bay-hotels.asp"">Thian Og Bay</a>"
				Case 336 'Koh  Nangyuan
					function_generate_hotel_link = "<a href=""/koh-nangyuan-hotels.asp"">Koh  Nangyuan</a>"

				'### Phetchabun ###
				Case 321 'Khao Kho
					function_generate_hotel_link = "<a href=""/phetchabun-khao-kho-hotels.asp"">Khao Kho</a>"
				Case 320 'Phetchabun City
					function_generate_hotel_link = "<a href=""/phetchabun-city-hotels.asp"">Phetchabun City</a>"
				
				'### Chanthaburi
				Case 367 'Chaolao Beach
					function_generate_hotel_link = "<a href=""/chantaburi-chaolao-beach-hotels.asp"">Chaolao Beach</a>"
				Case 368 'Chanthaburi City
					function_generate_hotel_link = "<a href=""/chantaburi-city-hotels.asp"">Chaolao City</a>"
				Case 369 'Kaenghangmaew
					function_generate_hotel_link = "<a href=""/chanthaburi-kaenghangmaew-hotels.asp"">Kaenghangmaew</a>"
				Case 370 'Kung Wiman Beach
					function_generate_hotel_link = "<a href=""/chanthaburi-kung-wiman-beach-hotels.asp"">Kung Wiman Beach</a>"
				'Case 441 'Kung Kra Ben Bay
					'function_generate_hotel_link = "<a href=""/chanthaburi-kung-kra-ben-bay-hotels.asp"">Kung Kra Ben Bay</a>"
				Case 442 'Laem Sadet Beach
					function_generate_hotel_link = "<a href=""/chanthaburi-laem-sadet-beach-hotels.asp"">Laem Sadet Beach</a>"
				
				'### Uthai Thani ###
				Case 342 'Ban Rai
					function_generate_hotel_link = "<a href=""/ban-rai-hotels.asp"">Ban Rai</a>"
				Case 346 'Uthai thani city
					function_generate_hotel_link = "<a href=""/uthai-thani-city-hotels.asp"">Uthai Thani City</a>"

				'#### Khon kaen ###
				Case 351	'Khonkaen City
					function_generate_hotel_link = "<a href=""/khonkaen-city-hotels.asp"">Khonkaen City</a>"
				
				'### Phitsanulok
				Case 161	'Phitsanulok City
					function_generate_hotel_link = "<a href=""/phitsanulok-city-hotels.asp"">Phitsanulok City</a>"
				Case 337	'Phromphiram
					function_generate_hotel_link = "<a href=""/phitsanulok-phromphiram-hotels.asp"">Phromphiram</a>"
				Case 338	'Wangthong
					function_generate_hotel_link = "<a href=""/phitsanulok-wangthong-hotels.asp"">Wangthong</a>"
				
				'### Nakhonratchasima
				Case 357 'Nakhonratchasima City
				function_generate_hotel_link = "<a href=""/Nakhonratchasima-City-hotels.asp"">Nakhonratchasima City</a>"
				Case 427 'Pak Chong
				function_generate_hotel_link = "<a href=""/Nakhonratchasima-pak-chong-hotels.asp"">Pak Chong</a>"
				Case 452 'Wang Nam Khieo
				function_generate_hotel_link = "<a href=""/Nakhonratchasima-Wang-Nam-Khieo-hotels.asp"">Wang Nam Khieo</a>"

				'### NakhonSiThammarat
				Case 362
				function_generate_hotel_link = "<a href=""/Khanom-Beach-hotels.asp"">Khanom Beach</a>"
				Case 363
				function_generate_hotel_link = "<a href=""/sichon-Beach-hotels.asp"">Sichon Beach</a>"
				Case 365
				function_generate_hotel_link = "<a href=""/nakhon-si-thamarat-city-hotels.asp"">Nakhon Si Thamarat</a>"
				
				'### Songkhla
				Case 361
				function_generate_hotel_link = "<a href=""/Samila-Beach-hotels.asp"">Samila</a>"
				Case 359
				function_generate_hotel_link = "<a href=""/Songkhla-City-hotels.asp"">Songkhla</a>"
				
				'### HatYai
				Case 360	'Hat Yai
				function_generate_hotel_link = "<a href=""/Hat-Yai-city-hotels.asp"">Hat Yai</a>"		
				
				'### Suratthani
				Case 373	'Suratthani City
				function_generate_hotel_link = "<a href=""/suratthani-city-hotels.asp"">Suratthani City</a>"
                Case 376	'Khao Sok
				function_generate_hotel_link = "<a href=""/suratthani-khao-sok-hotels.asp"">Khao Sok</a>"
				
				'### Sukhothai
				Case 377	'Sukhothai City
				function_generate_hotel_link = "<a href=""/sukhothai-city-hotels.asp"">Sukhothai City</a>"
				Case 382	'Sukhothai-Historical Park
				function_generate_hotel_link = "<a href=""/sukhothai-historical-park-hotels.asp"">Sukhothai-Historical Park</a>"
				Case 383	'Sukhothai-Airport
				function_generate_hotel_link = "<a href=""/sukhothai-airport-hotels.asp"">Sukhothai-Airport</a>"

				'### Ayutthaya
				Case 379	'Ayutthaya
				function_generate_hotel_link = "<a href=""/ayutthaya-city-hotels.asp"">Ayutthaya</a>"
				
				'### Lampang
				Case  380	'Lampang City
				function_generate_hotel_link = "<a href=""/lampang-city-hotels.asp"">Lampang City</a>"
				Case  384	'Chaeson
				function_generate_hotel_link = "<a href=""/lampang-chaeson-hotels.asp"">Chaeson</a>"	
				
				'### Trat ###	
				Case 388	'Tub Tim Beach
					function_generate_hotel_link = "<a href=""/trat-tub-tim-beach.asp"">Tub Tim Beach</a>"
				Case 389	'Trat City
					function_generate_hotel_link = "<a href=""/trat-city-hotels.asp"">Trat City</a>"
				Case 390	'Laem Ngop
					function_generate_hotel_link = "<a href=""/trat-laem-ngop-hotels.asp"">Laem Ngop</a>"
				Case 397	'Khlong Yai
					function_generate_hotel_link = "<a href=""/trat-khlong-yai-hotels.asp"">Khlong Yai</a>"
				Case 398	'Rayang Island
					function_generate_hotel_link = "<a href=""/trat-rayang-island-hotels.asp"">Rayang Island</a>"
				
				'### Loei ###	
				Case 399	'Loei City
					function_generate_hotel_link = "<a href=""/loei-city-hotels.asp"">Loei City</a>"
				Case 437	'Dan Sai
					function_generate_hotel_link = "<a href=""/loei-dan-sai-hotels.asp"">Dan Sai</a>"
				Case 453	'Pak Chom
					function_generate_hotel_link = "<a href=""/loei-pak-chom-hotels.asp"">Pak Chom</a>"

				'### Nong Khai ###	
				Case 404	'Nong Khai City
					function_generate_hotel_link = "<a href=""/nong-khai-city-hotels.asp"">Nong Khai City</a>"
			
				'### Ubon Ratchathani ###	
				Case 406	'Ubon Ratchathani City
					function_generate_hotel_link = "<a href=""/ubon-ratchathani-city-hotels.asp"">Ubon Ratchathani City</a>"
				Case 412	'Khongjiam
					function_generate_hotel_link = "<a href=""/ubon-ratchathani-khongjiam-hotels.asp"">Khongjiam</a>"

				'### Udon Thani ###	
				Case 407	'Udon Thani City
					function_generate_hotel_link = "<a href=""/udon-thani-city-hotels.asp"">Udon Thani City</a>"

				'### Ranong ###	
				Case 410	'Ranong City
					function_generate_hotel_link = "<a href=""/ranong-city-hotels.asp"">Ranong City</a>"
				Case 461	'Koh Phayam
					function_generate_hotel_link = "<a href=""/ranong-koh-phayam-hotels.asp"">Koh Phayam</a>"
						
				'### Satun ###	
				Case 411	'Satun City
					function_generate_hotel_link = "<a href=""/satun-city-hotels.asp"">Satun City</a>"
				Case 415	'Koh Lipe
					function_generate_hotel_link = "<a href=""/satun-koh-lipe-hotels.asp"">Koh Lipe</a>"
				Case 450	'Koh Bulon Lae
					function_generate_hotel_link = "<a href=""/satun-koh-bulon-lae-hotels.asp"">Koh Bulon Lae</a>"

				'### Chonburi ###
				Case 416	'Chonburi City
					function_generate_hotel_link = "<a href=""/chonburi-city-hotels.asp"">Chonburi City</a>"
				Case 417	'Sriracha
					function_generate_hotel_link = "<a href=""/chonburi-sriracha-hotels.asp"">Sriracha</a>"
				Case 418	'Bangsaen Beach
					function_generate_hotel_link = "<a href=""/chonburi-bangsaen-beach-hotels.asp"">Bangsaen Beach</a>"
				Case 422	'Koh Larn
					function_generate_hotel_link = "<a href=""/chonburi-koh-larn-hotels.asp"">Koh Larn</a>"

				'### Tak ###		
				Case 419	'Tak City
					function_generate_hotel_link = "<a href=""/tak-city-hotels.asp"">Tak City</a>"
				Case 420	'Mae Sot
					function_generate_hotel_link = "<a href=""/tak-mae-sot-hotels.asp"">Mae Sot</a>"
				Case 421	'Umphang
					function_generate_hotel_link = "<a href=""/tak-umphang-hotels.asp"">Umphang</a>"
					
				'### Nakhon Phanom ###		
				Case 423	'Nakhon Phanom City
					function_generate_hotel_link = "<a href=""/nakhonphanom-city-hotels.asp"">Nakhon Phanom City</a>"

				'### Ratchaburi ###		
				Case 424	'Suan Phueng
					function_generate_hotel_link = "<a href=""/ratchaburi-suan-phueng-hotels.asp"">Suan Phueng</a>"
				Case 425	'Damnoen Saduak Floating Market
					function_generate_hotel_link = "<a href=""/ratchaburi-damnoen-saduak-floating-market-hotels.asp"">Damnoen Saduak Floating Market</a>"
		
				'### Nonthaburi ###		
				Case 429	'Nonthaburi City 
					function_generate_hotel_link = "<a href=""/nonthaburi-city-hotels.asp"">Nonthaburi City</a>"

				'### Kamphaengphet ###		
				Case 430	'Kamphaengphet City
					function_generate_hotel_link = "<a href=""/kamphaengphet-city-hotels.asp"">Kamphaengphet City</a>"
					
				'### Samut Songkhram ###		
				Case 431	'Amphawa
					function_generate_hotel_link = "<a href=""/samut-songkhram-amphawa-hotels.asp"">Amphawa</a>"
				Case 432	'Samut Songkhram City
					function_generate_hotel_link = "<a href=""/samut-songkhram-city-hotels.asp"">Samut Songkhram City</a>"
			
				'### Nakornnayok ###	
				Case 433	'Nakornnayok City
					function_generate_hotel_link = "<a href=""/nakornnayok-city-hotels.asp"">Nakornnayok City</a>"
					
				'### Mukdahan ###
				Case 434	'Mukdahan  City
					function_generate_hotel_link = "<a href=""/mukdahan-city-hotels.asp"">Mukdahan  City</a>"
			
				'### Prachinburi ###
				Case 435	'Prachinburi City
					function_generate_hotel_link = "<a href=""/prachinburi-city-hotels.asp"">Prachinburi City</a>"
				Case 438	'Srimahaphote
					function_generate_hotel_link = "<a href=""/prachinburi-srimahaphote-hotels.asp"">Srimahaphote</a>"
				Case 439	'Nadee
					function_generate_hotel_link = "<a href=""/prachinburi-nadee-hotels.asp"">Nadee</a>"
				Case 458	'Kabinburi
					function_generate_hotel_link = "<a href=""/prachinburi-kabinburi-hotels.asp"">Kabinburi</a>"
					
				'### Sakon Nakhon ###
				Case 443	'Sakon Nakhon City
					function_generate_hotel_link = "<a href=""/sakon-nakhon-city-hotels.asp"">Sakon Nakhon City</a>"
			    Case 466	'Phuphan
					function_generate_hotel_link = "<a href=""/sakon-nakhon-phuphan-hotels.asp"">Phuphan</a>"

				'### Surin ###
				Case 444	'Surin City
					function_generate_hotel_link = "<a href=""/surin-city-hotels.asp"">Surin City</a>"

				'### Sisaket ###
				Case 445	'Sisaket City
					function_generate_hotel_link = "<a href=""/sisaket-city-hotels.asp"">Sisaket City</a>"
				Case 448	'Kantharalak
					function_generate_hotel_link = "<a href=""/sisaket-kantharalak-hotels.asp"">Kantharalak</a>"

				'### Lamphun ###
				Case 446	'Lamphun City
					function_generate_hotel_link = "<a href=""/lamphun-city-hotels.asp"">Lamphun City</a>"

				'### Saraburi ###
				Case 451	'Muak Lek
					function_generate_hotel_link = "<a href=""/saraburi-muak-lek-hotels.asp"">Muak Lek</a>"
				Case 462	'Saraburi City
					function_generate_hotel_link = "<a href=""/saraburi-city-hotels.asp"">Saraburi City</a>"
				Case 463	'Wang Muang
					function_generate_hotel_link = "<a href=""/saraburi-wang-muang-hotels.asp"">Wang Muang</a>"
			END SELECT


		Case 4 'Show Destination
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_hotel_link = "Bangkok"
				Case ConstDesIDPhuket
					function_generate_hotel_link = "Phuket"
				Case ConstDesIDChiangMai
					function_generate_hotel_link = "Chiang Mai"
				Case ConstDesIDPattaya
					function_generate_hotel_link = "Pattaya"
				Case ConstDesIDKohSamui
					function_generate_hotel_link = "Koh Samui"
				Case ConstDesIDKrabi
					function_generate_hotel_link = "Krabi"
				Case ConstDesIDKohChang 
					function_generate_hotel_link = "Koh Chang"
				Case ConstDesIDMaeHongSon
					function_generate_hotel_link = "Mae Hong Son"
				Case ConstDesIDChiangRai
					function_generate_hotel_link = "Chiang Rai"
				Case ConstDesIDChaAm
					function_generate_hotel_link = "Cha Am"
				Case ConstDesIDHuaHin
					function_generate_hotel_link = "Hua Hin"
				Case ConstDesIDRayong
					function_generate_hotel_link = "Rayong"
				Case ConstDesIDPhangNga
					function_generate_hotel_link = "Phang Nga"
				Case ConstDesIDKohSamet
					function_generate_hotel_link = "Koh Samet"
				Case ConstDesIDKanchanaburi
					function_generate_hotel_link = "Kanchanaburi"
				Case ConstDesIDPrachuap	
					function_generate_hotel_link = "Prachuap Khiri khan"
				Case ConstDesIDKhaoYai
					function_generate_hotel_link = "Khao Yai"
				Case ConstDesIDKohKood
					function_generate_hotel_link = "Koh Kood"
				Case ConstDesIDKohPhangan
					function_generate_hotel_link = "Koh Phangan"
				Case ConstDesIDTrang
					function_generate_hotel_link = "Trang"
				Case ConstDesIDChumphon
					function_generate_hotel_link = "Chumphon"
				Case ConstDesIDPhetchaburi
					function_generate_hotel_link = "Phetchaburi"
				Case ConstDesIDKohTao
					function_generate_hotel_link = "Koh Tao"
				Case ConstDesIDPhetchabun
					function_generate_hotel_link = "Phetchabun"
				Case ConstDesIDPhitsanulok
					function_generate_hotel_link = "Phitsanulok"
				Case ConstDesIDUthaithani
					function_generate_hotel_link = "Uthai Thani"
				Case ConstDesIDKhonkaen
					function_generate_hotel_link = "Khonkaen"
				Case ConstDesIDNakhonratchasima
					function_generate_hotel_link = "Nakhonratchasima"
				Case ConstDesIDNakhonSiThammarat
					function_generate_hotel_link = "NakhonSiThammarat"
				Case ConstDesIDSongkhla
					function_generate_hotel_link = "Songkhla"
				Case ConstDesIDHatYai
					function_generate_hotel_link = "Hat Yai"
				Case ConstDesIDAyutthaya
					function_generate_hotel_link = "Ayutthaya"
				Case ConstDesIDRatchaburi
					function_generate_hotel_link = "Ratchaburi"
				Case ConstOtherDestinations
					function_generate_hotel_link = "Other Destinations"
				Case ConstDesIDSuratthani
					function_generate_hotel_link = "Suratthani"
				Case ConstDesIDSukhothai
					function_generate_hotel_link = "Sukhothai"		
				Case ConstDesIDLampang
					function_generate_hotel_link = "Lampang"	
				Case ConstDesIDTrat
					function_generate_hotel_link = "Trat"		
				Case ConstDesIDLoei
					function_generate_hotel_link = "Loei"		
				Case ConstDesIDNongKhai
					function_generate_hotel_link = "Nong Khai"		
				Case ConstDesIDUbonRatchaThani
					function_generate_hotel_link = "Ubon Ratchathani"		
				Case ConstDesIDUdonThani
					function_generate_hotel_link = "Udon Thani"		
				Case ConstDesIDRanong
					function_generate_hotel_link = "Ranong"		
				Case ConstDesIDSatun
					function_generate_hotel_link = "Satun"	
				Case ConstDesIDChonburi	
					function_generate_hotel_link = "Chonburi"	
				Case ConstDesIDChanthaburi
					function_generate_hotel_link = "Chanthaburi"	
				Case ConstDesIDTak
					function_generate_hotel_link = "Tak"	
				Case ConstDesIDNakhonPhanom
					function_generate_hotel_link = "Nakhon Phanom"	
				Case ConstDesIDNonthaburi
					function_generate_hotel_link = "Nonthaburi"	
				Case ConstDesIDKamphaengphet
					function_generate_hotel_link = "Kamphaengphet"	
				Case ConstDesIDSamutSongkhram
					function_generate_hotel_link = "Samut Songkhram"	
				Case ConstDesIDNakornnayok
					function_generate_hotel_link = "Nakornnayok"	
				Case ConstDesIDMukdahan
					function_generate_hotel_link = "Mukdahan"	
				Case ConstDesIDPrachinburi
					function_generate_hotel_link = "Prachinburi"	
				Case ConstDesIDSakonNakhon
					function_generate_hotel_link = "Sakon Nakhon"	
				Case ConstDesIDSurin
					function_generate_hotel_link = "Surin"	
				Case ConstDesIDSisaket
					function_generate_hotel_link = "Sisaket"
				Case ConstDesIDLamphun
					function_generate_hotel_link = "Lamphun"
				Case ConstDesIDSaraburi
					function_generate_hotel_link = "Saraburi"
			END SELECT
			
		Case 5 'Destination File
			SELECT CASE intDestinationID
				Case ConstDesIDBangkok
					function_generate_hotel_link = "bangkok-hotels.asp"
				Case ConstDesIDPhuket
					function_generate_hotel_link = "phuket-hotels.asp"
				Case ConstDesIDChiangMai
					function_generate_hotel_link = "chiang-mai-hotels.asp"
				Case ConstDesIDPattaya
					function_generate_hotel_link = "pattaya-hotels.asp"
				Case ConstDesIDKohSamui
					function_generate_hotel_link = "koh-samui-hotels.asp"
				Case ConstDesIDKrabi
					function_generate_hotel_link = "krabi-hotels.asp"
				Case ConstDesIDKohChang 
					function_generate_hotel_link = "koh-chang-hotels.asp"
				Case ConstDesIDMaeHongSon
					function_generate_hotel_link = "mae-hong-son-hotels.asp"
				Case ConstDesIDChiangRai
					function_generate_hotel_link = "chiang-rai-hotels.asp"
				Case ConstDesIDChaAm
					function_generate_hotel_link = "cha-am-hotels.asp"
				Case ConstDesIDHuaHin
					function_generate_hotel_link = "hua-hin-hotels.asp"
				Case ConstDesIDRayong
					function_generate_hotel_link = "rayong-hotels.asp"
				Case ConstDesIDPhangNga
					function_generate_hotel_link = "phang-nga-hotels.asp"
				Case ConstDesIDKohSamet
					function_generate_hotel_link = "koh-samet-hotels.asp"
				Case ConstDesIDKanchanaburi
					function_generate_hotel_link = "kanchanaburi-hotels.asp"
				Case ConstDesIDPrachuap	
					function_generate_hotel_link = "prachuap-khiri-khan-hotels.asp"
				Case ConstDesIDKhaoYai
					function_generate_hotel_link = "khao-yai-hotels.asp"
				Case ConstDesIDKohKood
					function_generate_hotel_link = "koh-kood-hotels.asp"
				Case ConstDesIDKohPhangan
					function_generate_hotel_link = "koh-phangan-hotels.asp"
				Case ConstDesIDTrang
					function_generate_hotel_link = "trang-hotels.asp"
				Case ConstDesIDChumphon
					function_generate_hotel_link = "chumphon-hotels.asp"
				Case ConstDesIDPhetchaburi
					function_generate_hotel_link = "phetchaburi-hotels.asp"
				Case ConstDesIDKohTao
					function_generate_hotel_link = "koh-tao-hotels.asp"
				Case ConstDesIDPhetchabun
					function_generate_hotel_link = "phetchabun-hotels.asp"
				Case ConstDesIDPhitsanulok
					function_generate_hotel_link = "phitsanulok-hotels.asp"
				Case ConstDesIDUthaiThani
					function_generate_hotel_link = "uthai-thani-hotels.asp"
				Case ConstDesIDKhonkaen
					function_generate_hotel_link = "khonkaen-hotels.asp"
				Case ConstDesIDNakhonratchasima
					function_generate_hotel_link = "Nakhonratchasima-hotels.asp"
				Case ConstDesIDNakhonSiThammarat
					function_generate_hotel_link = "NakhonSiThammarat-hotels.asp"
				Case ConstDesIDSongkhla
					function_generate_hotel_link = "Songkhla-hotels.asp"
				Case ConstDesIDHatYai 
					function_generate_hotel_link = "Hat-Yai-hotels.asp"
				Case ConstDesIDSuratthani
					function_generate_hotel_link = "suratthani-hotels.asp"
				Case ConstDesIDLampang
					function_generate_hotel_link = "Lampang-hotels.asp"
				Case ConstDesIDSukhothai
					function_generate_hotel_link = "sukhothai-hotels.asp"
				Case ConstDesIDAyutthaya
					function_generate_hotel_link = "ayutthaya-hotels.asp"
				Case ConstDesIDTrat
					function_generate_hotel_link = "trat-hotels.asp"
				Case ConstDesIDLoei
					function_generate_hotel_link = "loei-hotels.asp"
				Case ConstDesIDChanthaburi
					function_generate_hotel_link = "chanthaburi-hotels.asp"
				Case ConstDesIDNongKhai
					function_generate_hotel_link = "nong-khai-hotels.asp"
				Case ConstDesIDUbonRatchaThani
					function_generate_hotel_link = "ubon-ratchathani-hotels.asp"
				Case ConstDesIDUdonThani
					function_generate_hotel_link = "udon-thani-hotels.asp"
				Case ConstDesIDRanong
					function_generate_hotel_link = "ranong-hotels.asp"
				Case ConstDesIDSatun
					function_generate_hotel_link = "satun-hotels.asp"
				Case ConstDesIDChonburi
					function_generate_hotel_link = "chonburi-hotels.asp"
				Case ConstDesIDTak
					function_generate_hotel_link = "tak-hotels.asp"
				Case ConstDesIDNakhonPhanom
					function_generate_hotel_link = "nakhonphanom-hotels.asp"
				Case ConstDesIDRatchaburi
					function_generate_hotel_link = "ratchaburi-hotels.asp"
				Case ConstDesIDNonthaburi
					function_generate_hotel_link = "nonthaburi-hotels.asp"
				Case ConstDesIDKamphaengphet
					function_generate_hotel_link = "kamphaengphet-hotels.asp"
				Case ConstDesIDSamutSongkhram
					function_generate_hotel_link = "samut-songkhram-hotels.asp"
				Case ConstDesIDNakornnayok
					function_generate_hotel_link = "nakornnayok-hotels.asp"
				Case ConstDesIDMukdahan
					function_generate_hotel_link = "mukdahan-hotels.asp"
				Case ConstDesIDPrachinburi
					function_generate_hotel_link = "prachinburi-hotels.asp"
				Case ConstDesIDSakonNakhon
					function_generate_hotel_link = "sakon-nakhon-hotels.asp"
				Case ConstDesIDSurin
					function_generate_hotel_link = "surin-hotels.asp"
				Case ConstDesIDSisaket
					function_generate_hotel_link = "sisaket-hotels.asp"
				Case ConstDesIDLamphun
					function_generate_hotel_link = "lamphun-hotels.asp"
				Case ConstDesIDSaraburi
					function_generate_hotel_link = "saraburi-hotels.asp"
					
			END SELECT
	END SELECT
	
END FUNCTION
%>