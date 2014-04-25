<%
Function fnAddZero(strInput, intTotalLength)

	Dim strOutput, intCurrentLength, intLengthDifference, i

	strOutput = strInput
	intCurrentLength = CInt(Len(strInput))
	intTotalLength = CInt(intTotalLength)
	intLengthDifference = intTotalLength - intCurrentLength

	If intLengthDifference > 0 Then
		For i=1 To intLengthDifference
			strOutput = "0" & strOutput
		Next
	End If

	'return result to function
	fnAddZero  = strOutput

End Function

FUNCTION fnGenPageID(intCatID,intContentID,intLangID)

    Dim strLangID
    Dim strCatID
    Dim strContentID
    Dim strPageID

    strLangID = fnAddZero(intLangID, 2)
    strCatID = fnAddZero(intCatID, 3)
    strContentID = fnAddZero(intContentID, 7)
    strPageID = "9" & strLangID & strCatID & strContentID

    fnGenPageID = strPageID

END FUNCTION

FUNCTION fnGenPageTrack(intCatID,intContentID,intLangID)

    Dim strScript
    Dim strPageID
    Dim strTrackURL
    Dim strAd
    Dim strAffID
    Dim strBookingID
	Dim strCam
	Dim strInKeyword
	Dim strDateIn
	Dim strDateOut
	Dim strdesID
	Dim strLocID
	Dim strOtherQuery
	
	
    strAd = Request.QueryString("ht2thAD")

    IF Request.Cookies("site_id") <> "" AND NOT ISNULL(Request.Cookies("site_id")) Then
        strAd = "affiliate"
        strAffID = Request.Cookies("site_id")
    End IF

    IF Request.QueryString("tell") = "1" Then
        strAd = "tell"
    End IF

    IF Request.QueryString("mail") = "1" Then
        strAd = "mail"
    End IF

    IF Request.QueryString("gg") = "1" Then
        strAd = "google"
    End IF

    IF Request.QueryString("st") = "1" Then
        strAd = "smarttravel"
    End IF

	
	
    strCam = "&camID=" & Request.QueryString("camID")
    strInKeyword = "&keyword=" & Request.QueryString("keyword")
    strDateIn = "&datein=" & Request.QueryString("datein")
    strDateOut = "&dateout=" & Request.QueryString("dateout")
    strdesID = "&desID=" & Request.QueryString("desID")
    strLocID = "&locID=" & Request.QueryString("locID")
    strBookingID = "&bookingID=" & Request.QueryString("bookingID")
    
	
    strOtherQuery = strCam & strInKeyword & strdateIn & strdateOut &strDesId & strLocID & strBookingID


    strPageID = fnGenPageID(intCatID,intContentID,intLangID)

    strTrackURL =  "http://track.hotels2thailand.com/application/track.aspx?ht2thPageID="&strPageID&"&ht2thAD="&strAd&"&affID="&strAffID&strOtherQuery&"&ht2thRefer=" & Server.URLEncode(Request.ServerVariables("HTTP_REFERER"))
    strScript = "<script language=""javascript"" src="""&strTrackURL&"""  type=""text/javascript""></script>"
    fnGenPageTrack = strScript
	'fnGenPageTrack =""

END FUNCTION 
%>
