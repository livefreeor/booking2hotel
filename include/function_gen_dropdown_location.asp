<%
FUNCTION function_gen_dropdown_location(intDestination,intLocation,strName,strJava,intType)
	Dim strBangkokSelect
	Dim strPhuketSelect
	Dim strChiangMaiSelect
	Dim strPattayaSelect
	Dim strKohSamuiSelect
	Dim strKrabiSelect
	Dim strKohChangSelect
	Dim strChiangRaiSelect
	Dim strChaAmSelect
	Dim strHuaHinSelect
	Dim strPhitsanulokSelect
	Dim strRayongSelect
	Dim strMaeHongSonSelect
	Dim strKanchanaburiSelect
	Dim strPrachuapSelect
	Dim strKohKoodSelect
	Dim strKohSametSelect
	Dim strPhangNgaSelect
	Dim strKhaoYaiSelect
	Dim strKohPhanganSelect
	Dim strTrangSelect
	Dim strChumphonSelect
	Dim strPhetchaburiSelect
	Dim strKohTaoSelect
	Dim strPhetchabunSelect
	Dim strUthaiThaniSelect
	Dim strKhonkaenSelect
	Dim strNakhonratchasimaSelect
	Dim strNakhonSiThammaratSelect
	Dim strSongkhlaSelect
	Dim strHatYaiSelect
	Dim strChanthaburiSelect
	Dim strSuratthaniSelect
	Dim strSukhothaiSelect
	Dim strAyutthayaSelect
	Dim strLampangSelect
	Dim strNakornpathomSelect
	Dim strNakornnayokSelect
	Dim strSamutprakarnSelect
	Dim strLamphunSelect
	Dim strRatchaburiSelect
	Dim strTratSelect
	Dim strLoeiSelect
	Dim strNongKhaiSelect
	Dim strUbonRatchathaniSelect
	Dim strUdonThaniSelect
	Dim strRanongSelect
	Dim strSatunSelect
	Dim strChonburiSelect
	Dim strTakSelect
	Dim strNakhonPhanomSelect
	Dim strNonthaburiSelect
	Dim strKamphaengphetSelect
	Dim strSamutSongkhramSelect
	Dim strMukdahanSelect
	Dim strPrachinburiSelect
	Dim strSakonNakhonSelect
	Dim strSurinSelect
	Dim strSisaketSelect
	Dim strSaraburiSelect
	
	Dim strSelect1
	Dim strSelect2
	Dim strSelect3
	Dim strSelect4
	Dim strSelect5
	Dim strSelect6
	Dim strSelect7
	Dim strSelect8
	Dim strSelect9
	Dim strSelect10
	Dim strSelect11
	Dim strSelect12
	Dim strSelect13
	Dim strSelect14
	Dim strSelect15
	Dim strSelect16
	Dim strSelect17
	Dim strSelect18
	Dim strSelect19
	Dim strSelect20
	Dim strSelect21
	Dim strSelect22
	Dim strSelect23
	Dim strSelect24
	Dim strSelect25
	Dim strSelect26
	Dim strSelect27
	Dim strSelect28
	Dim strSelect29
	Dim strSelect30
	Dim strSelect31
	Dim strSelect32
	Dim strSelect33
	Dim strSelect34
	Dim strSelect35
	Dim strSelect36

	Dim strDestinationNone
	
	IF intDestination="" OR intDestination="none" Then
		intDestination = 0
		strDestinationNone = "<option value=""none"">Select Destination</option>" & VbCrlf
	Else
		intDestination = Cint(intDestination)
	End IF

	IF intLocation="" OR intLocation="none" Then
		intLocation = 0
	Else
		intLocation = Cint(intLocation)
	End IF

	SELECT CASE intType
		Case 1'Destination
			IF intDestination=ConstDesIDBangkok Then	'	30
				strBangkokSelect = "selected"
			ElseIF intDestination=ConstDesIDPhuket Then	'	31
				strPhuketSelect = "selected"
			ElseIF intDestination=ConstDesIDChiangMai Then	'	32
				strChiangMaiSelect = "selected"
			ElseIF intDestination=ConstDesIDPattaya Then	'	33
				strPattayaSelect = "selected"
			ElseIF intDestination=ConstDesIDKohSamui Then	'	34
				strKohSamuiSelect = "selected"
			ElseIF intDestination=ConstDesIDKrabi Then	'	35
				strKrabiSelect = "selected"
			ElseIF intDestination=ConstDesIDChiangRai Then	'	36
				strChiangRaiSelect = "selected"
			ElseIF intDestination=ConstDesIDChaAm Then	'	37
				strChaAmSelect = "selected"
			ElseIF intDestination=ConstDesIDHuaHin Then	'	38
				strHuahinSelect = "selected"
			ElseIF intDestination=ConstDesIDPhitsanulok Then	'	40
				strPhitsanulokSelect = "selected"
			ElseIF intDestination=ConstDesIDRayong Then	'	42
				strRayongSelect = "selected"
			ElseIF intDestination=ConstDesIDMaeHongSon Then	'	43
				strMaeHongSonSelect = "selected"
			ElseIF intDestination=ConstDesIDAyutthaya Then		'	44
				strAyutthayaSelect = "selected"
			ElseIF intDestination=ConstDesIDKanchanaburi Then	'	45
				strKanchanaburiSelect = "selected"
			ElseIF intDestination=ConstDesIDKohChang Then		'	46
				strKohChangSelect = "selected"
			ElseIF intDestination=ConstDesIDPrachuap Then	'	48
				strPrachuapSelect = "selected"
			ElseIF intDestination=ConstDesIDKohKood Then	'	49
				strKohKoodSelect = "selected"
			ElseIF intDestination=ConstDesIDKohSamet Then	'	50
				strKohSametSelect = "selected"
			ElseIF intDestination=ConstDesIDPhangNga Then	'	51
				strPhangNgaSelect = "selected"
			ElseIF intDestination=ConstDesIDKhaoYai Then	'	52
				strKhaoYaiSelect = "selected"
			ElseIF intDestination=ConstDesIDKohPhangan Then	'	53
				strKohPhanganSelect = "selected"
			ElseIF intDestination=ConstDesIDTrang Then	'	54
				strTrangSelect = "selected"
			ElseIF intDestination=ConstDesIDChumphon Then	'	55
				strChumphonSelect = "selected"
			ElseIF intDestination=ConstDesIDNakornnayok Then	'	57
				strNakornnayokSelect = "selected"
			ElseIF intDestination=ConstDesIDChanthaburi Then	'	58
				strChanthaburiSelect = "selected"
			ElseIF intDestination=ConstDesIDLamphun Then	'	60
				strLamphunSelect = "selected"
			ElseIF intDestination=ConstDesIDPhetchaburi Then	'	61
				strPhetchaburiSelect = "selected"
			ElseIF intDestination=ConstDesIDRatchaburi Then	'	62
				strRatchaburiSelect = "selected"
			ElseIF intDestination=ConstDesIDNakhonratchasima Then	'	63
				strNakhonratchasimaSelect = "selected"
			ElseIF intDestination=ConstDesIDKohTao Then	'	65
				strKohTaoSelect = "selected"
			ElseIF intDestination=ConstDesIDPhetchabun Then	'	66
				strPhetchabunSelect = "selected"
			ElseIF intDestination=ConstDesIDUthaiThani Then	'	67
				strUthaiThaniSelect = "selected"
			ElseIF intDestination=ConstDesIDKhonkaen Then	'	68
				strKhonkaenSelect = "selected"
			ElseIF intDestination=ConstDesIDNakhonSiThammarat Then	'	69
				strNakhonSiThammaratSelect = "selected"
			ElseIF intDestination=ConstDesIDSongkhla Then	'	70
				strSongkhlaSelect = "selected"
			ElseIF intDestination=ConstDesIDHatYai Then	'	71
				strHatYaiSelect = "selected"
			ElseIF intDestination=ConstDesIDSuratthani Then	'	72
				strSuratthaniSelect = "selected"
			ElseIF intDestination=ConstDesIDSukhothai Then	'	73
				strSukhothaiSelect = "selected"
			ElseIF intDestination=ConstDesIDLampang Then	'	74
				strLampangSelect = "selected"
			ElseIF intDestination=ConstDesIDTrat Then	'	75
				strTratSelect = "selected"
			ElseIF intDestination=ConstDesIDLoei Then	'	76
				strLoeiSelect = "selected"
			ElseIF intDestination=ConstDesIDNongKhai Then	'	77
				strNongKhaiSelect = "selected"
			ElseIF intDestination=ConstDesIDUbonRatchaThani Then	'	78
				strUbonRatchathaniSelect = "selected"
			ElseIF intDestination=ConstDesIDUdonThani Then	'	79
				strUdonThaniSelect = "selected"
			ElseIF intDestination=ConstDesIDRanong Then	'	80
				strRanongSelect = "selected"
			ElseIF intDestination=ConstDesIDSatun Then	'	81
				strSatunSelect = "selected"
			ElseIF intDestination=ConstDesIDChonburi Then	'	82
				strChonburiSelect = "selected"
			ElseIF intDestination=ConstDesIDTak Then	'	83
				strTakSelect = "selected"
			ElseIF intDestination=ConstDesIDNakhonPhanom Then	'	84
				strNakhonPhanomSelect = "selected"
			ElseIF intDestination=ConstDesIDNonthaburi Then	'	86
				strNonthaburiSelect = "selected"
			ElseIF intDestination=ConstDesIDKamphaengphet Then	'	87
				strKamphaengphetSelect = "selected"
			ElseIF intDestination=ConstDesIDSamutSongkhram Then	'	88
				strSamutSongkhramSelect = "selected"
			ElseIF intDestination=ConstDesIDMukdahan Then	'	89
				strMukdahanSelect = "selected"
			ElseIF intDestination=ConstDesIDPrachinburi Then	'	90
				strPrachinburiSelect = "selected"
			ElseIF intDestination=ConstDesIDSakonNakhon Then	'	91
				strSakonNakhonSelect = "selected"
			ElseIF intDestination=ConstDesIDSurin Then	'	92
				strSurinSelect = "selected"
			ElseIF intDestination=ConstDesIDSisaket Then	'	93
				strSisaketSelect = "selected"
			ElseIF intDestination=ConstDesIDSaraburi Then	'	94
				strSaraburiSelect = "selected"
			End IF
			
			' 	Sort by name
			function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & strDestinationNone & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""44"" "& strAyutthayaSelect&">Ayutthaya</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""30"" "& strBangkokSelect &">Bangkok</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""37"" "& strChaAmSelect &">Cha Am</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""58"" "& strChanthaburiSelect &">Chanthaburi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""36"" "& strChiangRaiSelect &">Chiang Rai</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""32"" "& strChiangMaiSelect &">Chiang Mai</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""82"" "& strChonburiSelect &">Chonburi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""55"" "& strChumphonSelect &">Chumphon</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""71"" "& strHatYaiSelect &">Hat Yai</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""38"" "& strHuaHinSelect &">Hua Hin</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""87"" "& strKamphaengphetSelect &">Kamphaengphet</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""45"" "& strKanchanaburiSelect &">Kanchanaburi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""52"" "& strKhaoYaiSelect &">Khao Yai</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""68"" "& strKhonkaenSelect &">Khonkaen</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""46"" "& strKohChangSelect &">Koh Chang</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""49"" "& strKohKoodSelect &">Koh Kood</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""53"" "& strKohPhanganSelect &">Koh Phangan</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""50"" "& strKohSametSelect &">Koh Samet</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""34"" "& strKohSamuiSelect &">Koh Samui</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""65"" "& strKohTaoSelect &">Koh Tao</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""35"" "& strKrabiSelect &">Krabi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""74"" "& strLampangSelect &">Lampang</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""60"" "& strLamphunSelect &">Lamphun</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""76"" "& strLoeiSelect &">Loei</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""43"" "& strMaeHongSonSelect &">Mae Hong Son</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""89"" "& strMukdahanSelect &">Mukdahan</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""57"" "& strNakornnayokSelect &">Nakornnayok</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""84"" "& strNakhonPhanomSelect &">Nakhon Phanom</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""63"" "& strNakhonratchasimaSelect &">Nakhonratchasima</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""69"" "& strNakhonSiThammaratSelect &">Nakhon Si Thammarat</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""77"" "& strNongKhaiSelect &">Nong Khai</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""86"" "& strNonthaburiSelect &">Nonthaburi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""33"" "& strPattayaSelect &">Pattaya</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""51"" "& strPhangNgaSelect &">Phang Nga</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""66"" "& strPhetchabunSelect &">Phetchabun</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""61"" "& strPhetchaburiSelect &">Phetchaburi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""40"" "& strPhitsanulokSelect &">Phitsanulok</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""31"" "& strPhuketSelect &">Phuket</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""90"" "& strPrachinburiSelect &">Prachinburi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""48"" "& strPrachuapSelect &">Prachuap Khiri khan</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""62"" "& strRatchaburiSelect &">Ratchaburi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""80"" "& strRanongSelect &">Ranong</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""42"" "& strRayongSelect &">Rayong</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""91"" "& strSakonNakhonSelect &">Sakon Nakhon</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""88"" "& strSamutSongkhramSelect &">Samut Songkhram</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""81"" "& strSatunSelect &">Satun</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""93"" "& strSisaketSelect &">Sisaket</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""70"" "& strSongkhlaSelect &">Songkhla</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""94"" "& strSaraburiSelect &">Saraburi</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""73"" "& strSukhothaiSelect &">Sukhothai</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""72"" "& strSuratthaniSelect &">Suratthani</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""92"" "& strSurinSelect &">Surin</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""83"" "& strTakSelect &">Tak</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""54"" "& strTrangSelect&">Trang</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""75"" "& strTratSelect &">Trat</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""78"" "& strUbonRatchathaniSelect &">Ubon Ratchathani</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""79"" "& strUdonThaniSelect &">Udon Thani</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""67"" "& strUthaiThaniSelect &">UthaiThani</option>" & VbCrlf
			function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
		
		Case 2'Location
'			Case 31 'Phuket
'				function_get_location_id = "60,61,68,69,71,76,79,87,89,103,107,108,123,146,147,173,178,184,188,228,234,372"
'			Case 32 'Chiang Mai
'				function_get_location_id = "72,77,80,81,82,83,85,86,88,144,175,183,226,334,344"
'			Case 33 'Pattaya
'				function_get_location_id = "92,93,94,95,182,186"
'			Case 34 'Koh Samui
'				function_get_location_id = "112,113,114,148,149,150,151,152,153,190,220,315,319,322,341,345,374"
'			Case 35 'Krabi
'				function_get_location_id = "116,117,118,119,170,185,242,246,269,287,347"
'			Case 36 'Chiang Rai
'				function_get_location_id = "154,158,159,167,192,193,194,378"
'			Case 37 'Cha Am
'				function_get_location_id = "156"
'			Case 38 'Hua Hin
'				function_get_location_id = "157"
'			Case 40 'Phitsanulok
'				function_get_location_id = "161,337,338"
'			Case 42 'Rayong
'				function_get_location_id = "162,163,164,168,204,205,206,239,241"
'			Case 43 'Mae Hong Son
'				function_get_location_id = "174,195,196,289"
'			Case 45 'Kanchanaburi
'				function_get_location_id = "181,218,219,240,251,252,288,329,332"
'			Case 46 'Koh Chang
'				function_get_location_id = "200,201,198,214,215,199,202,302,371"
'			Case 48 'Prachaup Khiri Khan
'				function_get_location_id = "209,210,211,212,223,229,235"
'			Case 49 'Koh Kood
'				function_get_location_id = "237,238,301,304,308,316,339,340"
'			Case 50 'Koh Samet
'				function_get_location_id = "207,236,244,249,250"
'			Case 51 'Phang Nga
'				function_get_location_id = "247,248,257,266,297,298,299"
'			Case 52 'Khao Yai
'				function_get_location_id = "268"
'			Case 53 'Koh Phangan
'				function_get_location_id = "270,271,272,273,274,280,281,300,305,307,310,312,313,314,317,325"
'			Case 54 'Trang
'				function_get_location_id = "275,277,278,279,283"
'			Case 55 'Chumphon
'				function_get_location_id = "276,290,309,335,343"
'			Case 61 'Phetchaburi 
'				function_get_location_id = "294,303,318"
'			Case 65 'Koh Tao
'				function_get_location_id = "323,324,327"
'			Case 66 'Phetchabun
'				function_get_location_id = "320,321"
'			Case 67 'Uthai Thani
'				function_get_location_id = "342,346"
'			Case 68 'Khonkaen
'				function_get_location_id = "351"
'			Case 63 'Nakhonratchasima
'				function_get_location_id = "357"
'			Case 69 'NakhonSiThammarat
'				function_get_location_id = "362,363,365"
'			Case 70 'Songkhla
'				function_get_location_id = "359,361"
'			Case 71 'HatYai
'				function_get_location_id = "360"
'			Case 58 'Chanthaburi
'				function_get_location_id = "367,368,369,370"
'			Case 72 'Suratthani
'				function_get_location_id = "373"
'			Case 73 'Sukhothai
'				function_get_location_id = "377"
'			Case 44 'Ayutthaya
'				function_get_location_id = "379"
'			Case 74 'Lampang
'				function_get_location_id = "380"

			SELECT Case intDestination
			
				Case 0
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""none"">Select Location</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDAyutthaya
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=379  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""379"" "& strSelect2 &">Ayutthaya City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDBangkok
		'			Case 30 'Bangkok
		'				function_get_location_id = "58,59,62,63,64,65,66,67,70,73,90,91,105,106,122,124,125,128,131,143,145,171,179,189,191,284,291"
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=106  Then
						strSelect2 = "selected"
					ElseIF intLocation=90  Then
						strSelect3 = "selected"
					ElseIF intLocation=291  Then
						strSelect4 = "selected"
					ElseIF intLocation=189  Then
						strSelect5 = "selected"
					ElseIF intLocation=65  Then
						strSelect6 = "selected"
					ElseIF intLocation=58  Then
						strSelect7 = "selected"
					ElseIF intLocation=63  Then
						strSelect8 = "selected"
					ElseIF intLocation=70  Then
						strSelect9 = "selected"
					ElseIF intLocation=125  Then
						strSelect10 = "selected"
					ElseIF intLocation=124  Then
						strSelect11 = "selected"
					ElseIF intLocation=171  Then
						strSelect12 = "selected"
					ElseIF intLocation=91  Then
						strSelect13 = "selected"
					ElseIF intLocation=59  Then
						strSelect14 = "selected"
					ElseIF intLocation=62 Then
						strSelect15 = "selected"
					ElseIF intLocation=191  Then
						strSelect16 = "selected"
					ElseIF intLocation=284  Then
						strSelect17 = "selected"
					End IF
					'Location order by alphabet
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""106"" "& strSelect2 &">Chatuchak Market</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""90"" "& strSelect3 &">China Town</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""291"" "& strSelect4 &">Don Muang Airport</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""189"" "& strSelect5 &">Khaosan Road</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""65"" "& strSelect6 &">Pattanakarn</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""58"" "& strSelect7 &">Petchburi Road</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""63"" "& strSelect8 &">Pratunam</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""70"" "& strSelect9 &">Rachadapisek</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""125"" "& strSelect10 &">River Side</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""124"" "& strSelect11 &">Sathorn</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""171"" "& strSelect12 &">Siam</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""91"" "& strSelect13 &">Silom</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""59"" "& strSelect14 &">Sukhumvit</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""62"" "& strSelect15 &">Suvarnabhumi Airport</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""191"" "& strSelect16 &">Wireless Road</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""284"" "& strSelect17 &">Other Area</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
				
				Case ConstDesIDPhuket
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=60  Then
						strSelect2 = "selected"
					ElseIF intLocation=76  Then
						strSelect3 = "selected"
					ElseIF intLocation=87  Then
						strSelect4 = "selected"
					ElseIF intLocation=68  Then
						strSelect5 = "selected"
					ElseIF intLocation=69 Then
						strSelect6 = "selected"
					ElseIF intLocation=173  Then
						strSelect7 = "selected"
					ElseIF intLocation=79  Then
						strSelect8 = "selected"
					ElseIF intLocation=147  Then
						strSelect9 = "selected"
					ElseIF intLocation=61  Then
						strSelect10 = "selected"
					ElseIF intLocation=71  Then
						strSelect11 = "selected"
					ElseIF intLocation=107  Then
						strSelect12 = "selected"
					ElseIF intLocation=178  Then
						strSelect13 = "selected"
					ElseIF intLocation=234  Then
						strSelect14 = "selected"
					ElseIF intLocation=89  Then
						strSelect15 = "selected"
					ElseIF intLocation=188  Then
						strSelect16 = "selected"
					ElseIF intLocation=228  Then
						strSelect17 = "selected"
					ElseIF intLocation=267  Then
						strSelect18 = "selected"
					ElseIF intLocation=372  Then
						strSelect19 = "selected"
					ElseIF intLocation=381 Then
						strSelect20 = "selected"
					ElseIF intLocation=392  Then
						strSelect21 = "selected"
					ElseIF intLocation=401  Then
						strSelect22 = "selected"
					ElseIF intLocation=295  Then
						strSelect23 = "selected"
					ElseIF intLocation=296  Then
						strSelect24 = "selected"
					End IF
				
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""60"" "& strSelect2 &">Bang Tao</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""76"" "& strSelect3 &">Cape Panwa</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""87"" "& strSelect4 &">Kamala Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""68"" "& strSelect5 &">Karon Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""69"" "& strSelect6 &">Kata Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""173"" "& strSelect7 &">Koh Yao</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""234"" "& strSelect14 &">Naitorn Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""79"" "& strSelect8 &">Nai Yang Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""147"" "& strSelect9 &">NaiHarn Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""61"" "& strSelect10 &">Patong Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""71"" "& strSelect11 &">Phuket Town</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""107"" "& strSelect12 &">Rawai Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""178"" "& strSelect13 &">Surin Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""89"" "& strSelect15 &">Layan Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""188"" "& strSelect16 &">Koh Lon Island</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""228"" "& strSelect17 &">Phuket Airport</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""267"" "& strSelect18 &">Coral Island</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""372"" "& strSelect19 &">Cape Yamu</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""381"" "& strSelect20 &">Po Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""392"" "& strSelect21 &">Koh Racha Yai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""401"" "& strSelect22 &">Chalong Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""295"" "& strSelect23 &">Mai Khao Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""296"" "& strSelect24 &">Racha Island</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDChiangMai
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=72  Then
						strSelect2 = "selected"
					ElseIF intLocation=77  Then
						strSelect3 = "selected"
					ElseIF intLocation=81  Then
						strSelect4 = "selected"
					ElseIF intLocation=86  Then
						strSelect5 = "selected"
					ElseIF intLocation=83 Then
						strSelect6 = "selected"
					ElseIF intLocation=85  Then
						strSelect7 = "selected"
					ElseIF intLocation=80  Then
						strSelect8 = "selected"
					ElseIF intLocation=175  Then
						strSelect9 = "selected"
					ElseIF intLocation=82  Then
						strSelect10 = "selected"
					ElseIF intLocation=334  Then
						strSelect11 = "selected"
					ElseIF intLocation=226  Then
						strSelect12 = "selected"
					ElseIF intLocation=344  Then
						strSelect13 = "selected"
					ElseIF intLocation=233  Then
						strSelect14 = "selected"
					ElseIF intLocation=232  Then
						strSelect15 = "selected"
					ElseIF intLocation=144  Then
						strSelect16 = "selected"
					ElseIF intLocation=231  Then
						strSelect17 = "selected"
					End IF
				
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""233"" "& strSelect14 &">A.Maetang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""232"" "& strSelect15 &">Ampur Muang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""144"" "& strSelect16 &">Angkhang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""72"" "& strSelect2 &">Chiang Mai City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""344"" "& strSelect13 &">Doi Saket</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""77"" "& strSelect3 &">Hangdong</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""81"" "& strSelect4 &">Huay Kaew</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""86"" "& strSelect5 &">Mae Ai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""83"" "& strSelect6 &">Mae Rim</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""231"" "& strSelect17 &">Mae Rim Samoeng</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""85"" "& strSelect7 &">Parton</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""80"" "& strSelect8 &">Railway Road</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""334"" "& strSelect11 &">San Kamphaeng</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""175"" "& strSelect9 &">Sansai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""226"" "& strSelect12 &">Saraphee</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""82"" "& strSelect10 &">Sridonchai Road</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDPattaya
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=94  Then
						strSelect2 = "selected"
					ElseIF intLocation=95  Then
						strSelect3 = "selected"
					ElseIF intLocation=93  Then
						strSelect4 = "selected"
					ElseIF intLocation=186  Then
						strSelect5 = "selected"
					ElseIF intLocation=92  Then
						strSelect6 = "selected"
					ElseIF intLocation=253  Then
						strSelect7 = "selected"
					ElseIF intLocation=245  Then
						strSelect8 = "selected"
					ElseIF intLocation=468  Then
						strSelect9 = "selected"	
					End IF
				
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""94"" "& strSelect2 &">Jomtien Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""468"" "& strSelect9 &">Naklua</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""92"" "& strSelect6 &">North Pattaya</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""95"" "& strSelect3 &">Pattaya city</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""253"" "& strSelect7 &">Phra Tamnuk Hill</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""93"" "& strSelect4 &">South Pattaya</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""186"" "& strSelect5 &">Tambon Pong</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""245"" "& strSelect8 &">Wongamart Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf


				Case ConstDesIDPrachuap
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=209  Then
						strSelect2 = "selected"
					ElseIF intLocation=210  Then
						strSelect3 = "selected"
					ElseIF intLocation=211  Then
						strSelect4 = "selected"
					ElseIF intLocation=223  Then
						strSelect5 = "selected"
					ElseIF intLocation=229  Then
						strSelect6 = "selected"
					ElseIF intLocation=235  Then
						strSelect7 = "selected"
					ElseIF intLocation=212  Then
						strSelect8 = "selected"
					End IF
				
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""209"" "& strSelect2 &">Bangsaphan</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""210"" "& strSelect3 &">Thap Sakae</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""211"" "& strSelect4 &">Pranburi</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""223"" "& strSelect5 &">BanKrut Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""229"" "& strSelect6 &">Khao Sam Roi Yot</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""235"" "& strSelect7 &">Prachuap Khiri khan City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""212"" "& strSelect8 &">Koh Talu</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDHatYai
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=360  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""360"" "& strSelect2 &">Hat Yai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDKhaoYai
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=268  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""268"" "& strSelect2 &">Khao Yai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
				
				Case ConstDesIDKohSamui
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=114  Then
						strSelect2 = "selected"
					ElseIF intLocation=151  Then
						strSelect3 = "selected"
					ElseIF intLocation=113  Then
						strSelect4 = "selected"
					ElseIF intLocation=153  Then
						strSelect5 = "selected"
					ElseIF intLocation=112 Then
						strSelect6 = "selected"
					ElseIF intLocation=148  Then
						strSelect7 = "selected"
					ElseIF intLocation=149  Then
						strSelect8 = "selected"
					ElseIF intLocation=315  Then
						strSelect9 = "selected"
					ElseIF intLocation=319  Then
						strSelect10 = "selected"
					ElseIF intLocation=322  Then
						strSelect11 = "selected"
					ElseIF intLocation=345  Then
						strSelect12 = "selected"
					ElseIF intLocation=190 Then
						strSelect13 = "selected"
					ElseIF intLocation=220  Then
						strSelect14 = "selected"
					ElseIF intLocation=285  Then
						strSelect15 = "selected"
					ElseIF intLocation=341  Then
						strSelect16 = "selected"
					ElseIF intLocation=374  Then
						strSelect17 = "selected"
					ElseIF intLocation=387  Then
						strSelect18 = "selected"
					ElseIF intLocation=403  Then
						strSelect19 = "selected"
					ElseIF intLocation=152  Then
						strSelect20 = "selected"
					End IF
				
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""345"" "& strSelect12 &">Bang Rak Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""114"" "& strSelect2 &">Bo Phut Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""151"" "& strSelect3 &">Bo Phut Village</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""113"" "& strSelect4 &">Chaweng Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""149"" "& strSelect8 &">Chaweng Noi Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""153"" "& strSelect5 &">Choengmon Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""112"" "& strSelect6 &">Lamai Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""319"" "& strSelect10 &">Lipa Noi Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""148"" "& strSelect7 &">Maenam Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""315"" "& strSelect9 &">Natien Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""322"" "& strSelect11 &">Tongson Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""190"" "& strSelect13 &">Laem Set Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""220"" "& strSelect14 &">Had Bang Ma kham</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""285"" "& strSelect15 &">Laem Nan Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""341"" "& strSelect16 &">Thong Yang Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""374"" "& strSelect17 &">Thong Tanote Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""387"" "& strSelect18 &">Phang Ka Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""403"" "& strSelect19 &">Big Buddha Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""152"" "& strSelect20 &">Taling Ngam Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDKrabi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=119  Then
						strSelect2 = "selected"
					ElseIF intLocation=117  Then
						strSelect3 = "selected"
					ElseIF intLocation=116  Then
						strSelect4 = "selected"
					ElseIF intLocation=118 Then
						strSelect5 = "selected"
					ElseIF intLocation=269 Then
						strSelect6 = "selected"
					'ElseIF intLocation=347 Then
						'strSelect7 = "selected"
					ElseIF intLocation=242  Then
						strSelect8 = "selected"
					ElseIF intLocation=246 Then
						strSelect9 = "selected"
					ElseIF intLocation=386 Then
						strSelect10 = "selected"
					ElseIF intLocation=243 Then
						strSelect11 = "selected"
					ElseIF intLocation=287 Then
						strSelect12 = "selected"
					ElseIF intLocation=170 Then
						strSelect13 = "selected"
					ElseIF intLocation=465 Then
						strSelect14 = "selected"
					End IF
				
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""119"" "& strSelect2 &">Ao Nang</option>" & VbCrlf
					'function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""347"" "& strSelect7 &">Klong Muang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""170"" "& strSelect13 &">Klong Muang Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""465"" "& strSelect14 &">Klong Thom</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""117"" "& strSelect3 &">Koh Lanta</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""269"" "& strSelect6 &">Koh Ngai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""116"" "& strSelect4 &">Krabi City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""118"" "& strSelect5 &">Phi Phi Island</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""242"" "& strSelect8 &">Railay Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""246"" "& strSelect9 &">Nopparat Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""386"" "& strSelect10 &">Had Yao</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""243"" "& strSelect11 &">Tonsai Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""287"" "& strSelect12 &">Pranang Cape</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
					
				Case ConstDesIDKohChang
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=200  Then
						strSelect2 = "selected"
					ElseIF intLocation=201  Then
						strSelect3 = "selected"
					ElseIF intLocation=198  Then
						strSelect4 = "selected"
					ElseIF intLocation=214  Then
						strSelect5 = "selected"
					ElseIF intLocation=199 Then
						strSelect6 = "selected"
					ElseIF intLocation=202 Then
						strSelect7 = "selected"
					ElseIF intLocation=302 Then
						strSelect8 = "selected"
					ElseIF intLocation=371 Then
						strSelect9 = "selected"
					ElseIF intLocation=395 Then
						strSelect10 = "selected"
					ElseIF intLocation=396 Then
						strSelect11 = "selected"
					ElseIF intLocation=464 Then
						strSelect12 = "selected"
					End IF

					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""200"" "& strSelect2 &">Kai Bae Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""201"" "& strSelect3 &">Klong Prao Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""198"" "& strSelect4 &">Koh Chang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""214"" "& strSelect5 &">Koh Kood</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""464"" "& strSelect12 &">Kong Kang Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""199"" "& strSelect6 &">Tha Nam Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""202"" "& strSelect7 &">White Sand Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""302"" "& strSelect8 &">Ao Bang Bao</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""371"" "& strSelect9 &">Salak Kok Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""395"" "& strSelect10 &">Salakphet Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""396"" "& strSelect11 &">Bai Lan  Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDChanthaburi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=367  Then
						strSelect2 = "selected"
					ElseIF intLocation=368  Then
						strSelect3 = "selected"
					ElseIF intLocation=369  Then
						strSelect4 = "selected"
					ElseIF intLocation=370  Then
						strSelect5 = "selected"
					ElseIF intLocation=442  Then
						strSelect6 = "selected"
					'ElseIF intLocation=441  Then
					'	strSelect7 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect6 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""367"" "& strSelect1 &">Chaolao Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""368"" "& strSelect2 &">Chanthaburi City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""369"" "& strSelect4 &">Kaenghangmaew</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""370"" "& strSelect5 &">Kung Wiman Beach</option>" & VbCrlf
					'function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""441"" "& strSelect6 &">Kung Kra Ben Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""442"" "& strSelect6 &">Laem Sadet Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDChiangRai
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=192  Then
						strSelect2 = "selected"
					ElseIF intLocation=193  Then
						strSelect3 = "selected"
					ElseIF intLocation=194  Then
						strSelect4 = "selected"
					ElseIF intLocation=154  Then
						strSelect5 = "selected"
					ElseIF intLocation=158  Then
						strSelect6 = "selected"
					ElseIF intLocation=159  Then
						strSelect7 = "selected"
					ElseIF intLocation=167  Then
						strSelect8 = "selected"
					ElseIF intLocation=378  Then
						strSelect9 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""192"" "& strSelect2 &">Chiang Rai City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""194"" "& strSelect4 &">Doi Mae Salong</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""193"" "& strSelect3 &">Kok River</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""154"" "& strSelect5 &">Golden Triangle</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""158"" "& strSelect6 &">Mae Chan</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""159"" "& strSelect7 &">Mae Sai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""167"" "& strSelect8 &">Chiang Saen</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""378"" "& strSelect9 &">Chiang Khong</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDMaeHongSon
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=195  Then
						strSelect2 = "selected"
					ElseIF intLocation=196  Then
						strSelect3 = "selected"
					ElseIF intLocation=174  Then
						strSelect4 = "selected"
					ElseIF intLocation=289  Then
						strSelect5 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""195"" "& strSelect2 &">Mae Sot</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""196"" "& strSelect4 &">Pai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""174"" "& strSelect4 &">Mae Hong Son City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""289"" "& strSelect5 &">Mae Sariang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDHuaHin
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=157  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""157"" "& strSelect2 &">Hua Hin</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDChaAm
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=156  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""156"" "& strSelect2 &">Cha Am</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
				Case ConstDesIDPhangNga
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=248  Then
						strSelect2 = "selected"
					ElseIF intLocation=297  Then
						strSelect3 = "selected"
					ElseIF intLocation=298  Then
						strSelect4 = "selected"
					ElseIF intLocation=299  Then
						strSelect5 = "selected"
					ElseIF intLocation=247  Then
						strSelect6 = "selected"
					ElseIF intLocation=257  Then
						strSelect7 = "selected"
					ElseIF intLocation=266  Then
						strSelect8 = "selected"
					ElseIF intLocation=264  Then
						strSelect9 = "selected"
					ElseIF intLocation=358  Then
						strSelect10 = "selected"
					ElseIF intLocation=459  Then
						strSelect11 = "selected"
					ElseIF intLocation=470  Then
						strSelect12 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""297"" "& strSelect3 &">Bor Saen</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""248"" "& strSelect2 &">Khao Lak</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""257"" "& strSelect7 &">Koh Yao Noi</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""270"" "& strSelect12 &">Koh Yao Yai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""266"" "& strSelect8 &">Koh Kor Khao</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""264"" "& strSelect9 &">Kuraburi</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""298"" "& strSelect4 &">Nang Thong Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""247"" "& strSelect6 &">Natai Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""358"" "& strSelect10 &">Pakarang Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""299"" "& strSelect5 &">Pilai Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""358"" "& strSelect10 &">Pakarang Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""459"" "& strSelect11 &">Tai Muang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
				Case ConstDesIDRayong
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=162  Then
						strSelect2 = "selected"
					ElseIF intLocation=164  Then
						strSelect3 = "selected"
					ElseIF intLocation=163  Then
						strSelect4 = "selected"
					ElseIF intLocation=205  Then
						strSelect5 = "selected"
					ElseIF intLocation=168  Then
						strSelect6 = "selected"
					ElseIF intLocation=204  Then
						strSelect7 = "selected"
					ElseIF intLocation=206  Then
						strSelect8 = "selected"
					ElseIF intLocation=239  Then
						strSelect9 = "selected"
					ElseIF intLocation=241  Then
						strSelect10 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""162"" "& strSelect2 &">Ban Chang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""164"" "& strSelect3 &">Kram</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""163"" "& strSelect4 &">Pae Klaeng</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""205"" "& strSelect5 &">Rayong City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""168"" "& strSelect6 &">Klaeng</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""204"" "& strSelect7 &">Had Noina Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""206"" "& strSelect8 &">Mae Rumphung Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""239"" "& strSelect9 &">Laem Mae Pim</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""241"" "& strSelect10 &">Phala Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
				Case ConstDesIDKohSamet
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=236  Then
						strSelect2 = "selected"
					ElseIF intLocation=207  Then
						strSelect3 = "selected"
					ElseIF intLocation=244  Then
						strSelect4 = "selected"
					ElseIF intLocation=249  Then
						strSelect5 = "selected"
					ElseIF intLocation=250  Then
						strSelect6 = "selected"
					ElseIF intLocation=356  Then
						strSelect7 = "selected"
					ElseIF intLocation=454  Then
						strSelect8 = "selected"
					End IF

					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""236"" "& strSelect2 &">Ao Ku</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""207"" "& strSelect3 &">Ao Prao Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""244"" "& strSelect4 &">Vongduean Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""249"" "& strSelect5 &">Saikaew Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""250"" "& strSelect6 &">Ao Noina</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""356"" "& strSelect7 &">Ao Pagarang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""454"" "& strSelect8 &">Ao Cho</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
				Case ConstDesIDKanchanaburi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=252  Then
						strSelect2 = "selected"
					ElseIF intLocation=218  Then
						strSelect3 = "selected"
					ElseIF intLocation=240  Then
						strSelect4 = "selected"
					ElseIF intLocation=251 Then
						strSelect5 = "selected"
					ElseIF intLocation=329 Then
						strSelect6 = "selected"
					ElseIF intLocation=332 Then
						strSelect7 = "selected"
					ElseIF intLocation=219 Then
						strSelect8 = "selected"
					ElseIF intLocation=288 Then
						strSelect9 = "selected"
					ElseIF intLocation=449 Then
						strSelect10 = "selected"
					End IF

					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""240"" "& strSelect4 &">Kanchanaburi City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""252"" "& strSelect2 &">Kwai Yai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""218"" "& strSelect3 &">Sai Yok</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""251"" "& strSelect5 &">Thamakham</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""329"" "& strSelect6 &">Thong Pha Poom</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""332"" "& strSelect7 &">Sungklaburi</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""219"" "& strSelect8 &">Kaeng Sean</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""288"" "& strSelect9 &">Kwai Noi River</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""449"" "& strSelect10 &">Srinakarin National Park</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
				Case ConstDesIDKohKood
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=238  Then
						strSelect2 = "selected"
					ElseIF intLocation=237  Then
						strSelect3 = "selected"
					ElseIF intLocation=301  Then
						strSelect4 = "selected"
					ElseIF intLocation=308  Then
						strSelect5 = "selected"
					ElseIF intLocation=304  Then
						strSelect6 = "selected"
					ElseIF intLocation=316  Then
						strSelect7 = "selected"
					ElseIF intLocation=339  Then
						strSelect8 = "selected"
					ElseIF intLocation=340  Then
						strSelect9 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""316"" "& strSelect7 &">Ao Noi</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""339"" "& strSelect8 &">Ao Klong Jark</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""340"" "& strSelect9 &">Ao Ngarm Ko</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""301"" "& strSelect4 &">Ao Tapao</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""308"" "& strSelect5 &">Ao Yai Kee</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""238"" "& strSelect2 &">Bang Bao Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""237"" "& strSelect3 &">Klong Chao</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""304"" "& strSelect6 &">Koh Mak</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				
				Case ConstDesIDKohPhangan
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=274  Then
						strSelect2 = "selected"
					ElseIF intLocation=281  Then
						strSelect3 = "selected"
					ElseIF intLocation=272 Then
						strSelect4 = "selected"
					ElseIF intLocation=270 Then
						strSelect5 = "selected"
					ElseIF intLocation=273  Then
						strSelect6 = "selected"
					ElseIF intLocation=280  Then
						strSelect7 = "selected"
					ElseIF intLocation=312  Then
						strSelect8 = "selected"
					ElseIF intLocation=313  Then
						strSelect9 = "selected"
					ElseIF intLocation=300  Then
						strSelect10 = "selected"
					ElseIF intLocation=307 Then
						strSelect11 = "selected"
					ElseIF intLocation=271  Then
						strSelect12 = "selected"
					ElseIF intLocation=305  Then
						strSelect13 = "selected"
					ElseIF intLocation=310  Then
						strSelect14 = "selected"
					ElseIF intLocation=314  Then
						strSelect15 = "selected"
					ElseIF intLocation=325  Then
						strSelect16 = "selected"
					ElseIF intLocation=408  Then
						strSelect17 = "selected"
					ElseIF intLocation=409Then
						strSelect18 = "selected"
					ElseIF intLocation=440Then
						strSelect19 = "selected"
					ElseIF intLocation=317Then
						strSelect20 = "selected"
					End IF
			
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""325"" "& strSelect16 &">Ao Bang Charu</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""314"" "& strSelect15 &">Ao Chaloklum</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""312"" "& strSelect8 &">Ao Chao Phao</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""313"" "& strSelect9 &">Ao Nai Wok</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""274"" "& strSelect2 &">Banthai Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""300"" "& strSelect10 &">Haad Dao Deuk</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""317"" "& strSelect20 &">Haad Leela</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""281"" "& strSelect3 &">Koh Maa</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""272"" "& strSelect4 &">Mae Haad Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""270"" "& strSelect5 &">Rin Nai Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""307"" "& strSelect11 &">Rin Nok Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""273"" "& strSelect6 &">Salad Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""271"" "& strSelect12 &">Thong Nai Pan Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""305"" "& strSelect13 &">Thong Sala</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""280"" "& strSelect7 &">Yao Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""310"" "& strSelect14 &">Yuan Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""408"" "& strSelect17 &">Plai Laem Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""409"" "& strSelect18 &">Haad Tian Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""440"" "& strSelect19 &">Srithanu Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDTrang
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=275  Then
						strSelect2 = "selected"
					ElseIF intLocation=283  Then
						strSelect3 = "selected"
					ElseIF intLocation=279 Then
						strSelect4 = "selected"
					ElseIF intLocation=278 Then
						strSelect5 = "selected"
					ElseIF intLocation=277  Then
						strSelect6 = "selected"
					ElseIF intLocation=394  Then
						strSelect7 = "selected"
					ElseIF intLocation=348  Then
						strSelect8 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""275"" "& strSelect2 &">Chang Lang Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""348"" "& strSelect8 &">Koh Libong</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""283"" "& strSelect3 &">Koh Mook</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""279"" "& strSelect4 &">Koh Sukorn</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""278"" "& strSelect5 &">Pak Meng Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""277"" "& strSelect6 &">Trang City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""394"" "& strSelect7 &">Kradan  Island</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDChumphon
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=276  Then
						strSelect2 = "selected"
					ElseIF intLocation=290  Then
						strSelect3 = "selected"
					ElseIF intLocation=343  Then
						strSelect4 = "selected"
					ElseIF intLocation=335  Then
						strSelect5 = "selected"
					ElseIF intLocation=426  Then
						strSelect6 = "selected"
					ElseIF intLocation=309  Then
						strSelect7 = "selected"
					ElseIF intLocation=375  Then
						strSelect8 = "selected"
					ElseIF intLocation=428  Then
						strSelect9 = "selected"
					ElseIF intLocation=456  Then
						strSelect10 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""290"" "& strSelect3 &">Arunothai Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""343"" "& strSelect4 &">Langsuan</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""276"" "& strSelect2 &">Thung Wua Laen Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""335"" "& strSelect5 &">Chumphon City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""426"" "& strSelect6 &">Thung Makham Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""309"" "& strSelect7 &">Paknam Tako</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""375"" "& strSelect8 &">Paradonpab Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""428"" "& strSelect9 &">Sai Ree Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""456"" "& strSelect10 &">Bangburd Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
				Case ConstDesIDPhetchaburi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=294  Then
						strSelect2 = "selected"
					ElseIF intLocation=303  Then
						strSelect3 = "selected"
					ElseIF intLocation=318  Then
						strSelect4 = "selected"
					ElseIF intLocation=455  Then
						strSelect5 = "selected"
					ElseIF intLocation=457  Then
						strSelect6 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""303"" "& strSelect3 &">Haad Chao Samran</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""294"" "& strSelect2 &">Kaeng Krachan</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""457"" "& strSelect6 &">Nongyapong</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""318"" "& strSelect4 &">Puk Tien Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""455"" "& strSelect5 &">Phetchaburi City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
				
				Case ConstDesIDKohTao
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=324 Then
						strSelect2 = "selected"
					ElseIF intLocation=327  Then
						strSelect3 = "selected"
					ElseIF intLocation=323  Then
						strSelect4 = "selected"
					ElseIF intLocation=328 Then
						strSelect5 = "selected"
					ElseIF intLocation=330  Then
						strSelect6 = "selected"
					ElseIF intLocation=331  Then
						strSelect7 = "selected"
					ElseIF intLocation=336  Then
						strSelect8 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""324"" "& strSelect2 &">Chalok Baan Kao Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""327"" "& strSelect3 &">Mae Haad Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""323"" "& strSelect4 &">Sairee Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""328"" "& strSelect5 &">Tanote Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""330"" "& strSelect6 &">Jansom Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""331"" "& strSelect7 &">Thian Og Bay</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""336"" "& strSelect8 &">Koh  Nangyuan</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDPhetchabun
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=321 Then
						strSelect2 = "selected"
					ElseIF intLocation=320  Then
						strSelect3 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""321"" "& strSelect2 &">Khao Kho</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""320"" "& strSelect3 &">Phetchabun City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDPhitsanulok
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=161 Then
						strSelect2 = "selected"
					ElseIF intLocation=337  Then
						strSelect3 = "selected"
					ElseIF intLocation=338  Then
						strSelect4 = "selected"
					End IF

					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""161"" "& strSelect2 &">Phitsanulok City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""337"" "& strSelect3 &">Phromphiram</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""323"" "& strSelect4 &">Wangthong</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDUthaiThani
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=342 Then
						strSelect2 = "selected"
					ElseIF intLocation=346  Then
						strSelect3 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""342"" "& strSelect2 &">Ban Rai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""346"" "& strSelect3 &">Uthai Thani City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
				Case ConstDesIDKhonkaen
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=351  Then
						strSelect2 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""351"" "& strSelect2 &">Khonkaen</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDNakhonratchasima
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=357  Then
						strSelect2 = "selected"
					ElseIF intLocation=427  Then
						strSelect3 = "selected"
					ElseIF intLocation = 452 Then
						strSelect4 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""357"" "& strSelect2 &">Nakhonratchasima City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""427"" "& strSelect3 &">Pak Chong</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""452"" "& strSelect4 &">Wang Nam Khieo</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDNakhonSiThammarat
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=362  Then
						strSelect2 = "selected"
					ElseIF intLocation=363  Then
						strSelect3 = "selected"
					ElseIF intLocation=365  Then
						strSelect4 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""362"" "& strSelect2 &">Khanom Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""363"" "& strSelect3 &">Sichon Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""365"" "& strSelect4 &">Nakhon Si Thammarat  City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDSongkhla
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=359  Then
						strSelect2 = "selected"
					ElseIF intLocation=361  Then
						strSelect3 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""359"" "& strSelect2 &">Songkhla City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""361"" "& strSelect3 &">Samila Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDHatYai
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=360  Then
						strSelect2 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""360"" "& strSelect2 &">Hat Yai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
				
				Case ConstDesIDSuratthani
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=373  Then
						strSelect2 = "selected"
					ElseIF intLocation=376  Then
						strSelect3 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""373"" "& strSelect2 &">Surat Thani City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""376"" "& strSelect3 &">Khao Sok</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDSukhothai
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=377  Then
						strSelect2 = "selected"
					ElseIF intLocation=382  Then
						strSelect3 = "selected"
					ElseIF intLocation=383  Then
						strSelect4 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""377"" "& strSelect2 &">Sukhothai City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""382"" "& strSelect3 &">Sukhothai Historical Park</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""383"" "& strSelect4 &">Sukhothai Airport</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDLampang
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=380  Then
						strSelect2 = "selected"
					ElseIF intLocation=384  Then
						strSelect3 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""380"" "& strSelect2 &">Lampang City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""384"" "& strSelect3 &">Chaeson</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDTrat
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=388  Then
						strSelect2 = "selected"
					ElseIF intLocation=389  Then
						strSelect3 = "selected"
					ElseIF intLocation=390  Then
						strSelect4 = "selected"
					ElseIF intLocation=397  Then
						strSelect5 = "selected"
					ElseIF intLocation=398  Then
						strSelect6 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""388"" "& strSelect2 &">Tub Tim Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""389"" "& strSelect3 &">Trat City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""390"" "& strSelect4 &">Laem Ngop</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""397"" "& strSelect5 &">Khlong Yai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""398"" "& strSelect6 &">Rayang Island</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDLoei
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=399  Then
						strSelect2 = "selected"
					ElseIF intLocation=437  Then
						strSelect3 = "selected"
					ElseIF intLocation=453  Then
						strSelect4 = "selected"
					End IF
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""399"" "& strSelect2 &">Loei City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""437"" "& strSelect3 &">Dan Sai</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""453"" "& strSelect4 &">Pak Chom</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDNongKhai
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=404  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""404"" "& strSelect2 &">Nong Khai City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDUbonRatchaThani
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=406  Then
						strSelect2 = "selected"
					ElseIF intLocation=412  Then
						strSelect3 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""406"" "& strSelect2 &">Ubon Ratchathani City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""412"" "& strSelect3 &">Khongjiam</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
	
				Case ConstDesIDUdonThani
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=407  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""407"" "& strSelect2 &">Udon Thani City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDRanong
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=410  Then
						strSelect2 = "selected"
					ElseIF intLocation=461  Then
						strSelect3 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""461"" "& strSelect3 &">Koh Phayam</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""410"" "& strSelect2 &">Ranong City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDSatun
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=411  Then
						strSelect2 = "selected"
					ElseIF intLocation=415  Then
						strSelect3 = "selected"
					ElseIF intLocation=450  Then
						strSelect4 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""411"" "& strSelect2 &">Satun City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""415"" "& strSelect3 &">Koh Lipe</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""450"" "& strSelect4 &">Koh Bulon Lae</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDChonburi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=416  Then
						strSelect2 = "selected"
					ElseIF intLocation=417  Then
						strSelect3 = "selected"
					ElseIF intLocation=418  Then
						strSelect4 = "selected"
					ElseIF intLocation=422  Then
						strSelect5 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""416"" "& strSelect2 &">Chonburi City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""417"" "& strSelect3 &">Sriracha</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""418"" "& strSelect4 &">Bangsaen Beach</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""422"" "& strSelect5 &">Koh Larn</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDTak
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=419  Then
						strSelect2 = "selected"
					ElseIF intLocation=420  Then
						strSelect3 = "selected"
					ElseIF intLocation=421  Then
						strSelect4 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""419"" "& strSelect2 &">Tak City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""420"" "& strSelect3 &">Mae Sot</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""421"" "& strSelect4 &">Umphang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDNakhonPhanom
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=423  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""423"" "& strSelect2 &">Nakhon Phanom City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
					
				Case ConstDesIDRatchaburi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=424  Then
						strSelect2 = "selected"
					ElseIF intLocation=425  Then
						strSelect3 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""424"" "& strSelect2 &">Suan Phueng</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""425"" "& strSelect3 &">Damnoen Saduak Floating Market</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
	
				Case ConstDesIDNonthaburi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=429  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""429"" "& strSelect2 &">Nonthaburi</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDKamphaengphet
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=430  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""430"" "& strSelect2 &">Kamphaengphet</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
				
				Case ConstDesIDSamutSongkhram
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=431  Then
						strSelect2 = "selected"
					ElseIF intLocation=432  Then
						strSelect3 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""431"" "& strSelect2 &">Amphawa</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""432"" "& strSelect2 &">Samut Songkhram City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDNakornnayok
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=433  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""433"" "& strSelect2 &">Nakornnayok City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
				
				Case ConstDesIDMukdahan
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=434  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""434"" "& strSelect2 &">Mukdahan  City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
		
				Case ConstDesIDPrachinburi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=435  Then
						strSelect2 = "selected"
					ElseIF intLocation=438  Then
						strSelect3 = "selected"
					ElseIF intLocation=439  Then
						strSelect4 = "selected"
					ElseIF intLocation=458  Then
						strSelect5 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""458"" "& strSelect5 &">Kabinburi</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""439"" "& strSelect4 &">Nadee</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""435"" "& strSelect2 &">Prachinburi City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""438"" "& strSelect3 &">Srimahaphote</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDSakonNakhon
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=443  Then
						strSelect2 = "selected"
                    ElseIF intLocation=446  Then
						strSelect3 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""443"" "& strSelect2 &">Sakon Nakhon City</option>" & VbCrlf
                    function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""446"" "& strSelect3 &">Phuphan</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDSurin
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=444  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""444"" "& strSelect2 &">Surin City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf
	
				Case ConstDesIDSisaket
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=445  Then
						strSelect2 = "selected"
					ElseIF intLocation=448  Then
						strSelect3 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""445"" "& strSelect2 &">sisaket City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""448"" "& strSelect3 &">Kantharalak</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDLamphun
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=446  Then
						strSelect2 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""446"" "& strSelect2 &">Lamphun City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case ConstDesIDSaraburi
					IF intlocation=0 Then
						strSelect1 = "selected"
					ElseIF intLocation=451  Then
						strSelect2 = "selected"
					ElseIF intLocation=462  Then
						strSelect3 = "selected"
					ElseIF intLocation=463  Then
						strSelect4 = "selected"
					End IF
					
					function_gen_dropdown_location = "<select name="""& strName &""" "& strJava &">" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""0"" "& strSelect1 &">Any</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""451"" "& strSelect2 &">Muak Lek</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""462"" "& strSelect3 &">Saraburi City</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "<option value=""463"" "& strSelect4 &">Wang Muang</option>" & VbCrlf
					function_gen_dropdown_location = function_gen_dropdown_location & "</select>" & VbCrlf

				Case 999 'Temp
				
			END SELECT

	END SELECT

END FUNCTION
%>