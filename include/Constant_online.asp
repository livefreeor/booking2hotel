<!-- METADATA TYPE="TypeLib" FILE="C:\Program Files\Common Files\System\ADO\msado20.tlb" -->
<!--#include file="../include/function_livechat.asp"-->
<%
Dim strConn
Dim Conn
Dim intWebsiteID
Dim intCurrencyPrefix
Dim strCurrencyTitle
Dim dateCurrentConstant
Dim dateNextConstant
Dim datePreviousConstant

Dim strConnGolf
Dim ConnGolf
Session.Timeout = 1200

intWebsiteID = 1

'strConn = "PROVIDER=SQLOLEDB;DATA SOURCE=bluehouseserver;UID=sa;PWD=;DATABASE=hotelsdb(2006-10-17)"
'strConn = "Provider=SQLOLEDB.1;User ID=sa;Password=;database=hotelsdb;Server=10.1.1.10;NETWORK=DBMSSOCN;"
strConn = "Provider=SQLOLEDB.1;User ID=ht2th;Password=tnbhtpw;database=hotels2thailanddb;Server=sql13.crystaltech.com;NETWORK=DBMSSOCN;"

'strConnGolf = "PROVIDER=SQLOLEDB;DATA SOURCE=bluehouseserver;UID=sa;PWD=;DATABASE=golf2thailand(new)"
strConnGolf = "PROVIDER=SQLOLEDB;DATA SOURCE=SQLB2.webcontrolcenter.com;UID=gf2th_visa;PWD=rnp8gpe9;DATABASE=golf2thailand"

Sub connOpen()
	Set Conn= server.CreateObject("ADODB.Connection")
	Conn.Open strConn
End Sub

Sub connClose()
	Conn.Close
	Set Conn = Nothing
End Sub

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
CONST ConstDesIDRayong = 42
CONST ConstDesIDMaeHongSon = 43
CONST ConstDesIDAyutthaya = 44
CONST ConstDesIDKanchanaburi = 45
CONST ConstDesIDKohChang = 46
CONST ConstDesIDKohKood = 49
CONST ConstDesIDKohSamet = 50
CONST ConstDesIDPhangNga = 51
CONST ConstDesIDPrachuap = 48
CONST ConstDesIDKhaoYai = 52
CONST ConstDesIDKohPhangan = 53
CONST ConstDesIDTrang = 54
CONST ConstDesIDChumphon = 55
CONST ConstDesIDNakornpathom = 56
CONST ConstDesIDNakornnayok = 57
CONST ConstDesIDChanthaburi = 58
CONST ConstDesIDSamutprakarn = 59


'Page Number
CONST ConstPageSearchMain = 10

'Currency
CONST ConstCurrency = "THB"
CONST ConstCurrencyDisplay = "Baht"

'Gateway
CONST ConstGatewayID = 3
CONST ConstGatewayTitle = "kbank"
'CONST ConstGatewayTitle = "asia"
%>