<%
FUNCTION function_error_check_hotel()

	Dim bolOption
	Dim strQueryOption
	Dim strOptionID
	Dim arrOptionID
	Dim intOption
	Dim strAllOption
	Dim bolBed
	Dim bolAirport
	Dim bolExtra
	Dim intCount
	Dim strQueryBed
	Dim strBedID 
	Dim arrBedID
	Dim strQueryAirport
	Dim strAirportID
	Dim arrAirportID
	Dim strQueryExtra
	Dim strExtraID
	Dim arrExtraID
	Dim strQueryPeople
	Dim strQuery
	Dim strQueryFull
	Dim sqlProduct
	Dim recProduct
	Dim arrProduct
	Dim strRedirectPath
	Dim intDayCheckIn
	Dim intMonthCheckIn
	Dim intYearCheckIn
	Dim intDayCheckOut
	Dim intMonthCheckOut
	Dim intYearCheckOut
	Dim dateCheckIn
	Dim dateCheckOut
	Dim dateCurrent
	Dim dateCheckOutNext 
	Dim intNight
	Dim sqlMax
	Dim recMax
	Dim arrMax
	Dim intMaxAdult
	Dim intMaxExtraBed
	Dim intTotalExtraBed
	Dim sqlAirportMax
	Dim recAirportMax
	Dim arrAirportMax
	Dim intAirportMax
	Dim sqlRateOut
	Dim recRateOut
	Dim arrRateOut
	Dim bolRateOut
	Dim sqlGalaChristAdult
	Dim recGalaChristAdult
	Dim sqlGalaChristChild
	Dim recGalaChristchild
	Dim bolGalaChristChild
	Dim sqlGalaNewAdult
	Dim recGalaNewAdult
	Dim sqlGalaNewChild
	Dim recGalaNewchild
	Dim bolGalaNewChild
	Dim intGalaID
	
	'### SET OPTION ID ###
	IF Request("option_id")<>"" Then
		bolOption = True
		strQueryOption = Replace(Request("option_id")," ","")  'Destroy Space
		strOptionID = strQueryOption
		arrOptionID = Split(strQueryOption,",")
		intOption = Ubound(arrOPtionID)
		strQueryOption = "option_id=" & strQueryOption
		strAllOption = strOptionID
		
		For intCount=0 To Ubound(arrOptionID)
			IF Request("qty"&arrOPtionID(intCount))<>"" Then
				strQueryOption = strQueryOption & "&qty"&arrOPtionID(intCount) & "=" & Request("qty"&arrOPtionID(intCount))
			End IF
		Next
	Else
		bolOption = False
		strQueryOption = "option=blank"
		strOptionID = "0"
	End IF
	'### SET OPTION ID ###
	
	'### SET EXTRA BED ###
	IF Request("bed_id")<>"" Then
		bolBed = True
		strQueryBed = Replace(Request("bed_id")," ","")  'Destroy Space
		strBedID = strQueryBed
		arrBedID = Split(strQueryBed,",")
		strQueryBed = "bed_id=" & strQueryBed
		
		IF strAllOption <> "" Then
			strAllOption = strAllOption & "," & strBedID
		Else
			strAllOption = strBedID
		End IF
		
		For intCount=0 To Ubound(arrBedID)
			IF Request("bed"&arrBedID(intCount))<>"" Then
				strQueryBed = strQueryBed & "&bed"&arrBedID(intCount) & "=" & Request("bed"&arrBedID(intCount))
			End IF
		Next
	Else
		bolBed = False
		strQueryBed = "bed=blank"
		strBedID = "0"
	End IF
	'### SET EXTRA BED ###
	
	'### SET AIRPORT TRANSFER ###
	IF Request("airport_id")<>"" Then
		bolAirport = True
		strQueryAirport = Replace(Request("airport_id")," ","")  'Destroy Space
		strAirportID = strQueryAirport
		arrAirportID = Split(strQueryAirport,",")
		strQueryAirport = "airport_id=" & strQueryAirport
		
		IF strAllOption <> "" Then
			strAllOption = strAllOption & "," & strAirportID
		Else
			strAllOption = strAirportID
		End IF
		
		For intCount=0 To Ubound(arrAirportID)
			IF Request("airport"&arrAirportID(intCount))<>"" Then
				strQueryAirport = strQueryAirport & "&airport"&arrAirportID(intCount) & "=" & Request("airport"&arrAirportID(intCount))
			End IF
		Next
	Else
		bolAirport = False
		strQueryAirport = "airport=blank"
		strAirportID = "0"
	End IF
	'### SET AIRPORT TRANSFER ###
	
	'### SET EXTRA OPTION ###
	IF Request("extra_id")<>"" Then
		bolExtra = True
		strQueryExtra = Replace(Request("extra_id")," ","")  'Destroy Space
		strExtraID = strQueryExtra
		arrExtraID = Split(strQueryExtra,",")
		strQueryExtra = "extra_id=" & strQueryExtra
		
		IF strAllOption <> "" Then
			strAllOption = strAllOption & "," & strExtraID
		Else
			strAllOption = strExtraID
		End IF
		
		For intCount=0 To Ubound(arrExtraID)
			IF Request("extra"&arrExtraID(intCount))<>"" Then
				strQueryExtra = strQueryExtra & "&extra"&arrExtraID(intCount) & "=" & Request("extra"&arrExtraID(intCount))
			End IF
		Next
	Else
		bolExtra = False
		strQueryExtra = "extra=blank"
		strExtraID = 0
	End IF
	'### SET EXTRA OPTION ###
	
	'### SET ADULT AND CHILDREN ###
	strQueryPeople = "adult=" & Request("adult") & "&children=" & Request("children")
	'### SET ADULT AND CHILDREN ###
	
	'### SET QUERY STRING ###
	strQuery = strQueryoption & "&" & strQueryBed & "&" & strQueryAirport & "&" & strQueryExtra & "&" & strQueryPeople
	strQueryFull = strQuery  & "&product_id=" & Request("product_id") & "&ch_in_date=" & Request("ch_in_date") & "&ch_in_month=" & Request("ch_in_month") & "&ch_in_year=" & Request("ch_in_year")  & "&ch_out_date=" & Request("ch_out_date") & "&ch_out_month=" & Request("ch_out_month") & "&ch_out_year=" & Request("ch_out_year")
	strQueryFull = Replace(strQueryFull,"blank","")
	'### SET QUERY STRING ###
	
	'### SET PRPDUCT DETAIL ###
	sqlProduct = "SELECT product_id,title_en,destination_id,files_name,allotment_type FROM tbl_product WHERE product_id=" & Request("product_id")
	Set recProduct = Server.CreateObject ("ADODB.Recordset")
	recProduct.Open SqlProduct, Conn,adOpenStatic,adLockreadOnly
		arrProduct = recProduct.GetRows()
	recProduct.Close
	Set recProduct = Nothing 
	'### SET PRPDUCT DETAIL ###
	
	'### SET REDIRECT PATH ###
	strRedirectPath = "/"& function_generate_hotel_link(arrProduct(2,0),"",1)& "/"& arrProduct(3,0) & "?" & strQuery
	'### SET REDIRECT PATH ###
	
	'### Check Outing ###
	IF function_check_date_outing(1) Then
		Response.Redirect strRedirectPath & "&error=error09"
	End IF
	'### Check Outing ###
	
	'### ERROR ChHECK BLANK ###
	IF bolOption=FALSE AND bolBed=FALSE AND bolAirport=FALSE AND bolExtra=FALSE Then
		Response.Clear()
		Response.Redirect strRedirectPath & "&error=error10"
	End IF
	'### ERROR ChHECK BLANK ###
	
	'### ERROR CHECK DATE ###
	intDayCheckin = Request("ch_in_date")
	intMonthCheckin = Request("ch_in_month")
	intYearCheckin = Request("ch_in_year")
	intDayCheckout = Request("ch_out_date")
	intMonthCheckout = Request("ch_out_month")
	intYearCheckout = Request("ch_out_year")
		'### Error on Feb ###
		IF  Cint(intMonthCheckIn) = 2 Then
			IF Cint(intYearCheckIn) MOD 4 = 0 Then
				IF Cint(intDayCheckIn) > 29 Then
					Response.Redirect strRedirectPath & "&error=error02"
				End IF
			Else
				IF Cint(intDayCheckIn) > 28 Then
					Response.Redirect strRedirectPath & "&error=error03"
				End IF
			End IF
		End IF
		
		IF  Cint(intMonthCheckOut) = 2 Then
			IF Cint(intYearCheckOut) MOD 4 = 0 Then
				IF Cint(intDayCheckOut) > 29 Then
					Response.Redirect strRedirectPath & "&error=error04"
				End IF
			Else
				IF Cint(intDayCheckOut) > 28 Then
					Response.Redirect strRedirectPath & "&error=error05"
				End IF
			End IF
		End IF
		'### Error on Feb ###
		
		'### Error on 30 Day Month ###
		SELECT CASE intMonthCheckIn
			Case 4,6,9,11
				IF intDayCheckin>30 Then
					Response.Redirect strRedirectPath & "&error=error06"
				End IF
		END SELECT
		
		SELECT CASE intMonthCheckout
			Case 4,6,9,11
				IF intDayCheckOut>30 Then
					Response.Redirect strRedirectPath & "&error=error07"
				End IF
		END SELECT
		'### Error on 30 Day Month ###
	
		dateCheckIn = DateSerial(intYearCheckIn,intMonthCheckIn,intDayCheckIn)
		dateCheckOut = DateSerial(intYearCheckOut,intMonthCheckOut,intDayCheckOut)
		dateCheckOutNext = DateAdd("d",1,dateCheckOut)
		dateCurrent = DateSerial(Year(DateAdd("h",14,Now)),Month(DateAdd("h",14,Now)),Day(DateAdd("h",14,Now)))
		intNight = DateDiff("d",dateCheckIn,dateCheckOut)
		
		'### Date Check Out < Date Check In ###
		IF dateCheckOut <= dateCheckIn Then
			Response.Redirect strRedirectPath & "&error=error08"
		End IF
		'### Date Check Out < Date Check In ###
		
		'### Date Check In =< Current Date ###
		IF dateCheckIn =< dateCurrent Then
			Response.Redirect strRedirectPath & "&error=error09"
		End IF
		'### Date Check In < Current Date ###
		
		'Call (function_set_session_date(intDayCheckin,intMonthCheckin,intYearCheckin,intDayCheckout,intMonthCheckout,intYearCheckout))
	'### ERROR CHECK DATE ###
	
	'### ERROR CHECK ROOM MAX ADULT AND MAX EXTRA BED ###
	IF bolOption OR bolBed Then
		sqlMax = "SELECT option_id,option_cat_id,ISNULL(max_adult,1) AS max_adult,ISNULL(extra_bed,0) AS extra_bed"
		sqlMax = sqlMax & " FROM tbl_product_option"
		sqlMax = sqlMax & " WHERE option_id IN ("& strOptionID &") OR option_id IN ("& strBedID &")"
		
		Set recMax  = Server.CreateObject ("ADODB.Recordset")
		recMax.Open SqlMax , Conn,adOpenStatic,adLockreadOnly
			arrMax  = recMax.GetRows()
		recMax.Close
		Set recMax  = Nothing 
		
		intMaxAdult = function_check_max(arrMax,1)
		
		IF Cint(Request("adult"))>intMaxAdult Then
			Response.Redirect strRedirectPath & "&error=error11"
		End IF
		
		intMaxExtraBed = function_check_max(arrMax,2)
		intTotalExtraBed = function_check_max(arrMax,3)
	
		IF intTotalExtraBed>intMaxExtraBed Then
			Response.Redirect strRedirectPath & "&error=error12"
		End IF
	End IF
	'### ERROR CHECK ROOM MAX ADULT AND MAX EXTRA BED ###

	'### ERROR CHECK MAX MAX AIRPORT TRANSFER ###
	IF bolAirport Then
		sqlAirportMax = "SELECT option_id,option_cat_id,ISNULL(max_adult,1) AS max_adult"
		sqlAirportMax = sqlAirportMax & " FROM tbl_product_option"
		sqlAirportMax = sqlAirportMax & " WHERE option_id IN ("& strAirportID &")"
		
		Set recAirportMax  = Server.CreateObject ("ADODB.Recordset")
		recAirportMax.Open SqlAirportMax , Conn,adOpenStatic,adLockreadOnly
			arrAirportMax = recAirportMax.GetRows()
		recAirportMax.Close
		Set recAirportMax  = Nothing 
		
		intAirportMax = function_check_max(arrAirportMax,4)
		IF Cint(Request("adult"))>intAirportMax Then
			Response.Redirect strRedirectPath & "&error=error17"
		End IF
	End IF
	'### ERROR CHECK MAX MAX AIRPORT TRANSFER ###
	
	'### ERROR CHECK OUT OF RATE ###
	IF bolOption Then
		'### Check In ###
		sqlRateOut = "SELECT option_id  "
		sqlRateOut = sqlRateOut & " FROM tbl_option_price"
		sqlRateOut = sqlRateOut & " WHERE option_id IN ("&Request("option_id")&") AND (date_start<="&function_date_sql(Day(dateCheckin),Month(dateCheckin),Year(dateCheckin),1)&" AND date_end>="&function_date_sql(Day(dateCheckin),Month(dateCheckin),Year(dateCheckin),1)&")"
		Set recRateOut = Server.CreateObject ("ADODB.Recordset")
		recRateOut.Open sqlRateOut, Conn,adOpenStatic,adLockreadOnly
			IF NOT recRateOut.EOF Then
				arrRateOut = recRateOut.GetRows()
				bolRateOut = True
			Else
				bolRateOut = False
			End IF
		recRateOut.Close
		Set recRateOut = Nothing 
		
		IF NOT bolRateOut Then
			Response.Redirect strRedirectPath & "&error=error14"
		End IF
		
		IF bolRateOut Then 'IF Number of Price < Number of Option (1 Option 1 Price)
			IF Ubound(arrRateOut,2)<intOption Then
				Response.Redirect strRedirectPath & "&error=error14"
			End IF
		End IF
		'### Check In ###
		
		
		'### Check Out ###
		sqlRateOut = "SELECT option_id  "
		sqlRateOut = sqlRateOut & " FROM tbl_option_price"
		sqlRateOut = sqlRateOut & " WHERE option_id IN ("&Request("option_id")&") AND (date_start<="&function_date_sql(Day(dateCheckout),Month(dateCheckout),Year(dateCheckout),1)&" AND date_end>="&function_date_sql(Day(dateCheckout),Month(dateCheckout),Year(dateCheckout),1)&")"
		Set recRateOut = Server.CreateObject ("ADODB.Recordset")
		recRateOut.Open sqlRateOut, Conn,adOpenStatic,adLockreadOnly
			IF NOT recRateOut.EOF Then
				arrRateOut = recRateOut.GetRows()
				bolRateOut = True
			Else
				bolRateOut = False
			End IF
		recRateOut.Close
		Set recRateOut = Nothing 
		
		IF NOT bolRateOut Then
			Response.Redirect strRedirectPath & "&error=error14"
		End IF
		
		IF bolRateOut Then 'IF Number of Price < Number of Option (1 Option 1 Price)
			IF Ubound(arrRateOut,2)<intOption Then
				Response.Redirect strRedirectPath & "&error=error14"
			End IF
		End IF
		'### Check Out ###
		
	End IF
	Call (function_set_session_date(intDayCheckin,intMonthCheckin,intYearCheckin,intDayCheckout,intMonthCheckout,intYearCheckout))
	'### ERROR CHECK OUT OF RATE ###
	
	'### ERROR GALAR DINNER ###
	IF (datecheckIn<=DateSerial(Year(Date),12,24) AND datecheckOut>DateSerial(Year(Date),12,24)) OR (datecheckIn<=DateSerial(Year(Date),12,31) AND datecheckOut>DateSerial(Year(Date),12,31)) Then '###A1###

		bolGalaChristChild = False
		
		IF (datecheckIn<=DateSerial(Year(Date),12,24) AND datecheckOut>DateSerial(Year(Date),12,24)) Then '###A2###
		
			sqlGalaChristAdult = "SELECT option_id,title_en FROM tbl_product_option WHERE option_cat_id=47 AND status=1 AND (title_en LIKE '%christ%' OR title_en LIKE '%mas%') AND product_id=" & Request("product_id") & " order BY option_id ASC"
			
			Set recGalaChristAdult = Server.CreateObject ("ADODB.Recordset")
			recGalaChristAdult.Open sqlGalaChristAdult, Conn,adOpenStatic,adLockreadOnly
				
				IF NOT recGalaChristAdult.EOF Then 'ORDER GALA
					
					IF recGalaChristAdult.RecordCount>1 Then 'Find Adult Gala ID
						bolGalaChristChild = True
						recGalaChristAdult.MoveFirst
						While NOT recGalaChristAdult.EOF
							IF NOT InStr(1,recGalaChristAdult.Fields("title_en"), "child",1)>0  Then
								intGalaID = recGalaChristAdult.Fields("option_id")
							End IF
							recGalaChristAdult.MoveNext
						Wend
					Else
						intGalaID = recGalaChristAdult.Fields("option_id")
						bolGalaChristChild = False
					End IF
	
					IF function_array_check(intGalaID,arrExtraID,2) Then 'ORDER GALA
	
						IF Int(Request("extra"&intGalaID))<Int(Request("adult")) Then
							Response.Redirect strRedirectPath & "&error=error19"
						End IF
						
					Else 'NOT ORDER GALA
						Response.Redirect strRedirectPath & "&error=error18"
					End IF
	
				End IF
			recGalaChristAdult.Close
			Set recGalaChristAdult = Nothing 
			
			IF bolGalaChristChild AND Int(Request("children")) >0 Then 'Require Gala For Children
				
				sqlGalaChristChild = "SELECT TOP 1 * FROM tbl_product_option WHERE option_cat_id=47 AND status=1 AND (title_en LIKE '%mas%' AND title_en LIKE '%chil%') AND product_id=" & Request("product_id")
				Set recGalaChristChild = Server.CreateObject ("ADODB.Recordset")
				recGalaChristChild.Open sqlGalaChristChild, Conn,adOpenStatic,adLockreadOnly
				
				IF NOT recGalaChristChild.EOF Then 'ORDER GALA
	
					intGalaID = recGalaChristChild.Fields("option_id")
	
					IF function_array_check(intGalaID,arrExtraID,2) Then 'ORDER GALA
						IF Int(Request("extra"&intGalaID))<Int(Request("children")) Then
							Response.Redirect strRedirectPath & "&error=error21"
						End IF
					Else 'NOT ORDER GALA
						Response.Redirect strRedirectPath & "&error=error20"
					End IF
	
				End IF
			recGalaChristChild.Close
			Set recGalaChristChild = Nothing 
			
			End IF 'Require Gala For Children
			
		End IF '###A2###
		
		IF (datecheckIn<=DateSerial(Year(Date),12,31) AND datecheckOut>DateSerial(Year(Date),12,31)) Then
		
			sqlGalaNewAdult = "SELECT option_id,title_en FROM tbl_product_option WHERE option_cat_id=47 AND status=1 AND (title_en LIKE '%New%' AND title_en NOT LIKE '%Chinese%' ) AND product_id=" & Request("product_id") & " order BY option_id ASC"
			
			Set recGalaNewAdult = Server.CreateObject ("ADODB.Recordset")
			recGalaNewAdult.Open sqlGalaNewAdult, Conn,adOpenStatic,adLockreadOnly
				
				IF NOT recGalaNewAdult.EOF Then 'ORDER GALA
					
					IF recGalaNewAdult.RecordCount>1 Then 'Find Adult Gala ID
						bolGalaNewChild = True
						recGalaNewAdult.MoveFirst
						While NOT recGalaNewAdult.EOF
							IF NOT InStr(1,recGalaNewAdult.Fields("title_en"), "child",1)>0  Then
								intGalaID = recGalaNewAdult.Fields("option_id")
							End IF
							recGalaNewAdult.MoveNext
						Wend
					Else
						intGalaID = recGalaNewAdult.Fields("option_id")
						bolGalaNewChild = False
					End IF
	
					IF function_array_check(intGalaID,arrExtraID,2) Then 'ORDER GALA
						IF Int(Request("extra"&intGalaID))<Int(Request("adult")) Then
							Response.Redirect strRedirectPath & "&error=error23"
						End IF
						
					Else 'NOT ORDER GALA
						Response.Redirect strRedirectPath & "&error=error22"
					End IF
	
				End IF
			recGalaNewAdult.Close
			Set recGalaNewAdult = Nothing 
			
			IF bolGalaNewChild AND Int(Request("children")) >0 Then 'Require Gala For Children
				
				sqlGalaNewChild = "SELECT TOP 1 * FROM tbl_product_option WHERE option_cat_id=47 AND status=1 AND (title_en LIKE '%new%' AND title_en LIKE '%chil%' AND title_en NOT LIKE '%Chinese%' ) AND product_id=" & Request("product_id")
				Set recGalaNewChild = Server.CreateObject ("ADODB.Recordset")
				recGalaNewChild.Open sqlGalaNewChild, Conn,adOpenStatic,adLockreadOnly
				
				IF NOT recGalaNewChild.EOF Then 'ORDER GALA
	
					intGalaID = recGalaNewChild.Fields("option_id")
	
					IF function_array_check(intGalaID,arrExtraID,2) Then 'ORDER GALA
						IF Int(Request("extra"&intGalaID))<Int(Request("children")) Then
							Response.Redirect strRedirectPath & "&error=error25"
						End IF
					Else 'NOT ORDER GALA
						Response.Redirect strRedirectPath & "&error=error24"
					End IF
	
				End IF
			recGalaNewChild.Close
			Set recGalaNewChild = Nothing 
			
			End IF 'Require Gala For Children
			
		End IF
	
	End IF '###A1###
	'### ERROR GALAR DINNER ###
	
	function_error_check_hotel = strAllOption
END FUNCTION
%>