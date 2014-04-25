<%
FUNCTION function_get_location_id(intDestinationID,intType)

	intDestinationID = Cint(intDestinationID)
	
	SELECT CASE intDestinationID
		Case 30 'Bangkok
			function_get_location_id = "58,59,62,63,65,70,90,91,106,124,125,171,189,191,284,291"
		Case 31 'Phuket
			function_get_location_id = "60,61,68,69,71,76,79,87,89,107,147,173,178,188,228,234,267,295,296,372,381,392,401"
		Case 32 'Chiang Mai
			function_get_location_id = "72,77,80,81,82,83,85,86,144,175,226,231,232,233,334,344"
		Case 33 'Pattaya
			function_get_location_id = "92,93,94,95,186,245,253,468"
		Case 34 'Koh Samui
			function_get_location_id = "112,113,114,148,149,151,153,190,220,285,315,319,322,341,345,374,387,403,152"
		Case 35 'Krabi
			function_get_location_id = "116,117,118,119,170,242,246,269,287,243,386,465,467"
		Case 36 'Chiang Rai
			function_get_location_id = "154,158,159,167,192,193,194,378"
		Case 37 'Cha Am
			function_get_location_id = "156"
		Case 38 'Hua Hin
			function_get_location_id = "157"
		Case 40 'Phitsanulok
			function_get_location_id = "161,337,338"
		Case 42 'Rayong
			function_get_location_id = "162,163,164,168,204,205,206,239,241"
		Case 43 'Mae Hong Son
			function_get_location_id = "174,195,196,289"
		Case 44 'Ayutthaya
			function_get_location_id = "379"
		Case 45 'Kanchanaburi
			function_get_location_id = "218,219,240,251,252,288,329,332,449"
		Case 46 'Koh Chang
			function_get_location_id = "200,201,198,214,199,202,302,371,395,396,464"
		Case 48 'Prachaup Khiri Khan
			function_get_location_id = "209,210,211,212,223,229,235"
		Case 49 'Koh Kood
			function_get_location_id = "237,238,301,304,308,316,339,340"
		Case 50 'Koh Samet
			function_get_location_id = "207,236,244,249,250,356,454"
		Case 51 'Phang Nga
			function_get_location_id = "247,248,257,264,266,297,298,299,358,459,470"
		Case 52 'Khao Yai
			function_get_location_id = "268"
		Case 53 'Koh Phangan
			function_get_location_id = "270,271,272,273,274,280,281,300,305,307,310,312,313,314,317,325,408,409,440"
		Case 54 'Trang
			function_get_location_id = "275,277,278,279,283,348,394"
		Case 55 'Chumphon
			function_get_location_id = "276,290,335,343,426,309,375,428,456"
		Case 57 'Nakornnayok
			function_get_location_id = "433"
		Case 58 'Chanthaburi
			function_get_location_id = "367,368,369,370,442"
		Case 60 'Lamphun
			function_get_location_id = "446"
		Case 61 'Phetchaburi 
			function_get_location_id = "294,303,318,455,457"
		Case 62 'Ratchaburi
			function_get_location_id = "424,425"
		Case 63 'Nakhonratchasima
			function_get_location_id = "357,427,452"
		Case 65 'Koh Tao
			function_get_location_id = "323,324,327,328,330,331,336"
		Case 66 'Phetchabun
			function_get_location_id = "320,321"
		Case 67 'Uthai Thani
			function_get_location_id = "342,346"
		Case 68 'Khonkaen
			function_get_location_id = "351"
		Case 69 'NakhonSiThammarat
			function_get_location_id = "362,363,365"
		Case 70 'Songkhla
			function_get_location_id = "359,361"
		Case 71 'HatYai
			function_get_location_id = "360"
		Case 72 'Suratthani
			function_get_location_id = "373,376"
		Case 73 'Sukhothai
			function_get_location_id = "377,382,383"
		Case 74 'Lampang
			function_get_location_id = "380,384"
		Case 75 'Trat
			function_get_location_id = "388,389,390,397,398"
		Case 76 'Loei
			function_get_location_id = "399,437,453"
		Case 77 'Nong Khai
			function_get_location_id = "404"
		Case 78 'Ubon Ratchathani
			function_get_location_id = "406,412"
		Case 79 'Udon Thani
			function_get_location_id = "407"
		Case 80 'Ranong
			function_get_location_id = "410,461"
		Case 81 'Satun
			function_get_location_id = "411,415,450"
		Case 82 'Chonburi
			function_get_location_id = "416,417,418,422"
		Case 83 'Tak
			function_get_location_id = "419,420,421"
		Case 84 'Nakhon Phanom
			function_get_location_id = "423"
		Case 86 'Nonthaburi
			function_get_location_id = "429"
		Case 87 'Kamphaengphet
			function_get_location_id = "430"
		Case 88 'Samut Songkhram
			function_get_location_id = "431,432"
		Case 89 'Mukdahan
			function_get_location_id = "434"
		Case 90 'Prachinburi
			function_get_location_id = "435,438,439,458"
		Case 91 'Sakon Nakhon
			function_get_location_id = "443,466"
		Case 92 'Surin
			function_get_location_id = "444"
		Case 93 'Sisaket
			function_get_location_id = "445,448"
		Case 94 'Saraburi
			function_get_location_id = "451,462,463"
	END SELECT

END FUNCTION
%>