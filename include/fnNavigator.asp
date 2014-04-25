<%
FUNCTION fnNavigator(intType, strDestination, intDestinationID, strCurrent)
	
	Dim strReferer
	Dim strDestinationLink
	Dim strDestinationFile
	Dim strDestinationDetail

	SELECT CASE Cint(intDestinationID)
		Case 30 'Bangkok
			strDestinationFile = "../bangkok-hotels.asp"
			strDestinationDetail = "/bangkok-hotels/"
		Case 31 'Phuket
			strDestinationFile = "../phuket-hotels.asp"
			strDestinationDetail = "/phuket-hotels/"
		Case 32 'Chiang Mai
			strDestinationFile = "../chiang-mai-hotels.asp"
			strDestinationDetail = "/chiang-mai-hotels/"
		Case 33 'Pattaya
			strDestinationFile = "../pattaya-hotels.asp"
			strDestinationDetail = "/pattaya-hotels/"
		Case 34 'Samui
			strDestinationFile = "../koh-samui-hotels.asp"
			strDestinationDetail = "/koh-samui-hotels/"
		Case 35 'Krabi
			strDestinationFile = "../krabi-hotels.asp"
			strDestinationDetail = "/krabi-hotels/"
	END SELECT
	
	SELECT CASE intType
		Case 1 'Navigation Hotel Detail
			strReferer =  Request.ServerVariables("HTTP_REFERER")
			IF inStr(1,strReferer,".asp",1)=0 OR inStr(1,strReferer,"localhost",1)=0  OR inStr(1,strReferer,"default.asp",1)>0 OR inStr(1,strReferer,strDestinationDetail,1)>0 Then
				strDestinationLink = "<a href="""& strDestinationFile &""">"& strDestination &" Hotels</a>"
			Else
				strDestinationLink = "<a href="""& strReferer &""">"& strDestination &" Hotels</a>"
			End If
			
	END SELECT
	
	fnNavigator = fnNavigator & "<a href=""../"">Home</a> -&gt; " &VbCrlf
	fnNavigator = fnNavigator & strDestinationLink & " -&gt; " &VbCrlf
	fnNavigator = fnNavigator & "<b>" & strCurrent & "</b>" &VbCrlf

END FUNCTION
%>