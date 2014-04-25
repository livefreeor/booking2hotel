<%
Sub sub_partner()
	Dim strRefer
	Dim strIP
	Dim url
	Dim check_url
	Dim rsAff
	Dim sqlAff
	Dim sqlAddSite
	Dim strSiteID
	Dim strCheck
	Dim sqlCheck
	Dim bolBlack
	Dim arrBlackSite
	Dim arrBlackKeyword
	Dim intCountBlackSite
	Dim intCountBlackKeyword
	
	Dim ConnRW
	Set ConnRW = server.CreateObject("ADODB.Connection")
	ConnRW.Open "Provider=SQLOLEDB.1;User ID=hotels2thailandwrite;Password=kfoutdk$or3$;database=hotels2;Server=74.86.253.60;NETWORK=DBMSSOCN;"
	
	strRefer = Request.ServerVariables("HTTP_REFERER")
	strRefer = Replace(strRefer,"'","''")
	strIP = Request.ServerVariables("REMOTE_ADDR")
	bolBlack = False
	
	'strRefer  = "google.co.th/search?hl=en&rlz=1T4AMSA_enTH253TH253&q=hotels2thailand&meta="
	
	'### Check Black List Keyword ###
	arrBlackSite = Array("google.","msn.","live.","yahoo.")
	arrBlackKeyword = Array("hotels2thailand","hotel2thailand")
	
	For intCountBlackSite=0 To Ubound(arrBlackSite)
		IF InStr(1,strRefer,arrBlackSite(intCountBlackSite)) Then
			For intCountBlackKeyword=0 To Ubound(arrBlackKeyword)
				IF InStr(1,strRefer,arrBlackKeyword(intCountBlackKeyword)) Then
					bolBlack = True
				End IF
			Next
		End IF
	Next
	'### Check Black List Keyword ###

	IF NOT bolBlack Then '### Check Black List Keyword ### IF01
		IF Request("psid")="" Then 'Link without site id
		
			url=strRefer
			url=mid(url,instr(url,"://")+3)
			'### Domain Name Only
			check_url=instr(url,"www.")
			IF check_url<>0 Then
				url=replace(url,"www.","")
			End IF
					
			IF instr(url,"/")>0 Then
				url=mid(url,1,instr(url,"/")-1)
			End IF
			IF url="" Then
				url="no ref"
			End IF
			'### 
			sqlAff = "SELECT  partner_id,site_id FROM tbl_aff_sites WHERE status=1 and url='"&url&"' and site_type=1"
	
			Set rsAff=server.CreateObject("adodb.recordset")
			rsAff.Open sqlAff,connRW,1,3
			IF rsAff.recordcount>0 Then
				Response.Cookies("site_id") = rsAff("site_id")
				Response.Cookies("site_id").Expires = DateAdd("YYYY",1,Date) 
				sqlAddSite = "INSERT INTO tbl_sites_stat (site_id,main_site_id,referer_ip,referer_url) VALUES ("&rsAff("site_id")&",1,'"&strIP&"','"&strRefer&"')"
				connRW.EXECUTE(sqlAddSite)
			End IF
			rsAff.close
			Set rsAff=Nothing
			
		Else 'Link with psid
			
			strSiteID = int(Request("psid"))
			sqlCheck="select count(site_id) from tbl_aff_sites where status=1 and site_id="&strSiteID
			IF int(trim(connRW.execute(sqlCheck).getString()))<>0 Then
				Response.Cookies("site_id") = strSiteID
				Response.Cookies("site_id").Expires = DateAdd("YYYY",1,Date)
				
				sqlAddSite = "INSERT INTO tbl_sites_stat (site_id,main_site_id,referer_ip,referer_url) VALUES ("&strSiteID&",1,'"&strIP&"','"&strRefer&"')"
				connRW.EXECUTE(sqlAddSite)
			End IF
		End IF
	End IF'### Check Black List Keyword ### IF01

	'### Advertise Check ###
	IF Cstr(Request("gg"))="1" Then
		Response.Cookies("advertise") = "google"
		Response.Cookies("advertise").Expires = DateAdd("m",1,Date)
	End IF
	'### Advertise Check ###
END Sub
%>