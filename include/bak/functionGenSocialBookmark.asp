<%
Function functionGenSocialBookmark()
Dim arrLogo
Dim intCount
Dim strBookmark

arrLogo=array("ico_sbm_live.gif","ico_sbm_su.gif","ico_sbm_yahoo.gif","ico_sbm_yahoo_bk.gif","ico_sbm_deli.gif","ico_sbm_digg.gif","ico_sbm_fb.gif","ico_sbm_gg.gif")
strBookmark=""
For intCount=0 to Ubound(arrLogo)
	strBookmark=strBookmark&"<img src="""">"
Next
End Function
%>