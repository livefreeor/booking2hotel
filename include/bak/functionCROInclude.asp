<%
SUB functionCROInclude(strPageType,strPagePosition)

	Dim strCode
	Dim strClickTaleTop
	Dim strClickTaleButtom
	
	strClickTaleTop = VbCrlf
	strClickTaleTop = strClickTaleTop & "<!-- ClickTale Top part -->" & vbCrlf
	strClickTaleTop = strClickTaleTop & "<script type=""text/javascript"">" & vbCrlf
	strClickTaleTop = strClickTaleTop & "var WRInitTime = (new Date()).getTime();" & vbCrlf
	strClickTaleTop = strClickTaleTop & "</script>" & vbCrlf
	strClickTaleTop = strClickTaleTop & "<!-- ClickTale end of Top part -->" & vbCrlf
	
	strClickTaleButtom = VbCrlf
	strClickTaleButtom = strClickTaleButtom & "<!-- ClickTale Bottom part -->" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "<div id=""ClickTaleDiv"" style=""display: none;""></div>" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "<script type=""text/javascript"">" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "if (document.location.protocol != 'https:')" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "document.write(unescape(""%3Cscript%20src='http://s.clicktale.net/WRb6.js'%20type='text/javascript'%3E%3C/script%3E""));" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "</script>" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "<script type=""text/javascript"">" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "if (typeof ClickTale == 'function') ClickTale(18133, 0.0009, ""www02"");" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "</script>" & vbCrlf
	strClickTaleButtom = strClickTaleButtom & "<!-- ClickTale end of Bottom part -->" & vbCrlf
	
	
	SELECT CASE strPageType
		
		Case "home"
			IF strPagePosition="top" Then
				strCode = strClickTaleTop
			ELSEIF strPagePosition="buttom" Then
				strCode = strClickTaleButtom
			End IF
			
		Case "destination"
			IF strPagePosition="top" Then
				strCode = strClickTaleTop
			ELSEIF strPagePosition="buttom" Then
				strCode = strClickTaleButtom
			End IF
			
		Case "location"
			IF strPagePosition="top" Then
				strCode = strClickTaleTop
			ELSEIF strPagePosition="buttom" Then
				strCode = strClickTaleButtom
			End IF
			
		Case "promotion"
			IF strPagePosition="top" Then
				strCode = strClickTaleTop
			ELSEIF strPagePosition="buttom" Then
				strCode = strClickTaleButtom
			End IF
			
		Case "productDetail"
			IF strPagePosition="top" Then
				strCode = strClickTaleTop
			ELSEIF strPagePosition="buttom" Then
				strCode = strClickTaleButtom
			End IF
			
		Case "searchPeriod"
			IF strPagePosition="top" Then
				strCode = strClickTaleTop
			ELSEIF strPagePosition="buttom" Then
				strCode = strClickTaleButtom
			End IF
			
		Case "searchQuick"
			IF strPagePosition="top" Then
				strCode = strClickTaleTop
			ELSEIF strPagePosition="buttom" Then
				strCode = strClickTaleButtom
			End IF
			
	END SELECT

	Response.Write(strCode)

END SUB
%>