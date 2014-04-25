<!-- METADATA TYPE="TypeLib" FILE="C:\Program Files\Common Files\System\ADO\msado20.tlb" -->
<!--#include file="./function_livechat.asp"-->
<!--#include file="./subUrlInjection.asp"-->
<!--#include file="./subFormInjection.asp"-->
<!--#include file="./function_mail_send.asp"--> 
<!--#include file="./sqlinjection_new.asp"-->
<!--#include file="./subWhoOn.asp"-->
<!--#include file="./function_add_zero.asp"-->

<%
Dim strConn
Dim strConn2 
Dim Conn
Dim intWebsiteID
Dim intCurrencyPrefix
Dim strCurrencyTitle
Dim dateCurrentConstant
Dim dateNextConstant
Dim datePreviousConstant
Dim intVatFactor
Dim strConnGolf
Dim ConnGolf
Dim strConnRW

Session.Timeout = 1200

intWebsiteID = 1
intVatFactor = 0.8496
'call subUrlInjection()
'call subFormInjection()

'
'response.Write "<div style=""border:1px solid #CCCCCCC;padding:10px;backgrounde:#F2F2F2"">Sorry, you can not access in the website for a moment. The system is being maintenance for a while. You will be able to access the website shortly.</div>"
'Response.End()

'strConn = "PROVIDER=SQLOLEDB;DATA SOURCE=bhtserver;UID=sa;PWD=;DATABASE=hotels2thailandonline"
'strConn = "Provider=MSDASQL; Driver={SQL Server}; Server=BHTSERVER\sqlexpress; Database=hotels2thailandonline; UID=sa;"

'#### Online #######

'strConn = "Provider=MSDASQL; Driver={SQL Server}; Server=74.86.253.60; Database=hotels2; UID=hotels2thailand; PWD=FdC$sdor#$;"
strConn = "Provider=MSDASQL; Driver={SQL Server}; Server=67.228.251.163; Database=hotels2thailandXXX; UID=ht2thXXX1; PWD=gfnvo]tryo]hko;"
'connectionString="DATA SOURCE=74.86.253.60;UID=ht2thXXX;PWD=gfnvo]tihvp]hko;DATABASE=hotels2thailandXXX" providerName="System.Data.SqlClient"/>

'####################

'strConn = "Provider=SQLOLEDB.1;User ID=sa;Password=;database=hotelsdb;Server=10.1.1.10;NETWORK=DBMSSOCN;"
'strConn = "Provider=SQLOLEDB.1;User ID=ht2th;Password=DBgdK9iLKl9iN$G5;database=hotels2thailanddb;Server=sql13.crystaltech.com;NETWORK=DBMSSOCN;"
'strConn = "Provider=SQLOLEDB.1;User ID=ht2th;Password=visabhtgdK9iLkl9iN;database=hotels2thailanddb;Server=216.197.96.14;NETWORK=DBMSSOCN;"
'strConn = "Provider=SQLOLEDB.1;User ID=hotels2thailand;Password=FdC$sdor#$;database=hotels2thailand;Server=74.86.253.58;NETWORK=DBMSSOCN;"

'strConnGolf = "PROVIDER=SQLOLEDB;DATA SOURCE=bluehouseserver;UID=sa;PWD=;DATABASE=golf2thailand(new)"
'<add name="golfdb" connectionString="User ID=gf2th_visa;Password=rnp8gpe9;database=golf2thailand;Server=63.134.207.131;NETWORK=DBMSSOCN;"/>
strConnGolf = "PROVIDER=SQLOLEDB;DATA SOURCE=63.134.207.131;UID=gf2th_visa;PWD=rnp8gpe9;DATABASE=golf2thailand"

Sub connOpen()
	Set Conn= server.CreateObject("ADODB.Connection")
	Conn.Open strConn
End Sub

Sub connClose()
	Conn.Close
	Set Conn = Nothing
End Sub

'strConnRW="Provider=SQLOLEDB.1;User ID=hotels2thailandwrite;Password=kfoutdk$or3$;database=hotels2thailand;Server=74.86.253.58;NETWORK=DBMSSOCN;"
strConnRW = "Provider=MSDASQL; Driver={SQL Server}; Server=67.228.251.163; Database=hotels2thailandXXX; UID=ht2thXXX1; PWD=gfnvo]tryo]hko;"
'strConn = "Provider=MSDASQL; Driver={SQL Server}; Server=74.86.253.60; Database=hotels2thailandXXX; UID=ht2thXXX; PWD=gfnvo]tihvp]hko;"
'strConnRW = "PROVIDER=SQLOLEDB;DATA SOURCE=bhtserver;UID=sa;PWD=;DATABASE=hotels2thailandonline"
'strConnRW = "Provider=MSDASQL; Driver={SQL Server}; Server=BHTSERVER\sqlexpress; Database=hotels2thailandonline; UID=sa;"

'----Conn Read Write
Sub connOpenRW()
	Set Conn= server.CreateObject("ADODB.Connection")
	Conn.Open strConnRW
End Sub
Sub connCloseRW()
	Conn.Close
	Set Conn = Nothing
End Sub
'----------------------------

Sub connGolfOpen()
	Set ConnGolf = server.CreateObject("ADODB.Connection")
	ConnGolf.Open strConnGolf 
End Sub

Sub connGolfClose()
	ConnGolf.Close
	Set ConnGolf  = Nothing
End Sub

'### Date And Time Setting ###
dateCurrentConstant = DateAdd("h",14,Now())
dateNextConstant = DateAdd("d",1,dateCurrentConstant)
datePreviousConstant = DateAdd("d",-1,dateCurrentConstant)
'### Date And Time Setting ###

'Deatination ID
CONST ConstDesIDBangkok = 30
CONST ConstDesIDPhuket = 31
CONST ConstDesIDChiangMai = 32
CONST ConstDesIDPattaya = 33
CONST ConstDesIDKohSamui = 34
CONST ConstDesIDKrabi = 35
CONST ConstDesIDChiangRai = 36
CONST ConstDesIDChaAm = 37
CONST ConstDesIDHuaHin = 38

CONST ConstDesIDPhitsanulok = 40

CONST ConstDesIDRayong = 42
CONST ConstDesIDMaeHongSon = 43
CONST ConstDesIDAyutthaya = 44
CONST ConstDesIDKanchanaburi = 45
CONST ConstDesIDKohChang = 46

CONST ConstDesIDPrachuap = 48
'CONST ConstDesIDPrachuapkhirikhan = 48
CONST ConstDesIDKohKood = 49
CONST ConstDesIDKohSamet = 50
CONST ConstDesIDPhangNga = 51
CONST ConstDesIDKhaoYai = 52
CONST ConstDesIDKohPhangan = 53
CONST ConstDesIDTrang = 54
CONST ConstDesIDChumphon = 55
CONST ConstDesIDNakornpathom = 56
CONST ConstDesIDNakornnayok = 57
CONST ConstDesIDChanthaburi = 58
'CONST ConstSamutprakarn = 59
CONST ConstDesIDSamutprakarn = 59
'CONST ConstLamphun = 60
CONST ConstDesIDLamphun = 60
CONST ConstDesIDPhetchaburi = 61
'CONST ConstRatchaburi = 62
CONST ConstDesIDRatchaburi = 62
CONST ConstDesIDNakhonratchasima = 63

CONST ConstOtherDestinations = 64
CONST ConstDesIDKohTao = 65
CONST ConstDesIDPhetchabun = 66
CONST ConstDesIDUthaiThani = 67
CONST ConstDesIDKhonkaen = 68
CONST ConstDesIDNakhonSiThammarat=69
CONST ConstDesIDSongkhla = 70
CONST ConstDesIDHatYai = 71
'CONST ConstSuratthani=72
CONST ConstDesIDSuratthani=72
'CONST ConstSukhothai = 73
CONST ConstDesIDSukhothai=73
'CONST ConstLampang = 74
CONST ConstDesIDLampang = 74
'CONST ConstTrat = 75
CONST ConstDesIDTrat = 75
'CONST ConstLoei = 76
CONST ConstDesIDLoei = 76
CONST ConstDesIDNongKhai = 77
CONST ConstDesIDUbonRatchaThani = 78
CONST ConstDesIDUdonThani = 79
CONST ConstDesIDRanong = 80
CONST ConstDesIDSatun = 81
CONST ConstDesIDChonburi = 82
CONST ConstDesIDTak = 83
CONST ConstDesIDNakhonPhanom = 84
CONST ConstDesIDNonthaburi = 86
CONST ConstDesIDKamphaengphet = 87
CONST ConstDesIDSamutSongkhram = 88
CONST ConstDesIDMukdahan = 89
CONST ConstDesIDPrachinburi = 90
CONST ConstDesIDSakonNakhon = 91
CONST ConstDesIDSurin = 92
CONST ConstDesIDSisaket = 93
CONST ConstDesIDSaraburi = 94

'Page Number
CONST ConstPageSearchMain = 10

'Currency
CONST ConstCurrency = "THB"
CONST ConstCurrencyDisplay = "Baht"

'Gateway
CONST ConstGatewayID = 3
CONST ConstGatewayTitle = "kbank"
'CONST ConstGatewayTitle = "asia"
CONST constMailServer = "mail.hotels2thailand.com"
'CONST constMailServer = "174.36.32.32"
CONST constReservationMail = "reservation@hotels2thailand.com"
Const constReservationName = "Hotels2Thailand.com Reservation Department"

Function getArray(sqlCommand)
	Dim arrList
	Dim rsCommand

	arrList=""
	Set rsCommand=server.CreateObject("adodb.recordset")
	rsCommand.Open sqlCommand,conn,1,3
	IF Not rsCommand.Eof Then
		arrList=rsCommand.getRows()
	End IF
	rsCommand.close()
	Set rsCommand=Nothing
	getArray=arrList
End Function

Function in_array(element, arr)
Dim i
    For i=0 To Ubound(arr)

        If Trim(arr(i)) = Trim(element) Then

            in_array = True

            Exit Function

        Else

            in_array = False

        End If 

    Next
End Function

FUNCTION fnGenPageID(intCatID,intContentID,intLangID)

    Dim strLangID
    Dim strCatID
    Dim strContentID
    Dim strPageID

    strLangID = function_add_zero(intLangID, 2)
    strCatID = function_add_zero(intCatID, 3)
    strContentID = function_add_zero(intContentID, 7)
    strPageID = "9" & strLangID & strCatID & strContentID

    fnGenPageID = strPageID

END FUNCTION

FUNCTION fnGenPageTrack(intCatID,intContentID,intLangID)

    Dim strScript
    Dim strPageID
    Dim strTrackURL
    Dim strAd
    Dim strAffID

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

    strPageID = fnGenPageID(intCatID,intContentID,intLangID)
    strTrackURL =  "http://track.hotels2thailand.com/application/track.aspx?ht2thPageID="&strPageID&"&ht2thAD="&strAd&"&affID="&strAffID&"&ht2thRefer=" & Server.URLEncode(Request.ServerVariables("HTTP_REFERER"))
    strScript = "<script language=""javascript"" src="""&strTrackURL&"""  type=""text/javascript""></script>"
    fnGenPageTrack = strScript

END FUNCTION 
%>






