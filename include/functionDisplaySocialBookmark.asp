<%
Function functionDisplaySocialBookmark(strTitle)
Dim arrSite
Dim intSite
Dim strUrl
Dim strIco
Dim strBookmark
strUrl="http://www.hotels2thailand.com"&request.ServerVariables("PATH_INFO")

arrSite=array("digg","fb","gg","live","su","yahoo","yahoo_bk","deli")
strBookmark="<div align=""center""><div align=""center"" style=""width:200px;""><fieldset><legend><span style=""font-size:12;font-weight:bold;color:#FF8040"">Social Bookmarking</span></legend><br>"
For intSite=0 to Ubound(arrSite)
	strIco="<img src=""/images/ico_bookmark/ico_sbm_"&arrSite(intSite)&".gif"" border=""0"">"
	Select Case arrSite(intSite)
		Case "digg"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://digg.com/submit?phase=2&url="&strUrl&"&title="&strTitle&"',550,500)"" title=""digg"">"&strIco&"</a>&nbsp;"
		Case "fb"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://www.facebook.com/share.php?src=bm&u="&strUrl&"&t="&strTitle&"',550,500)"" title=""facebook"">"&strIco&"</a>&nbsp;"
		Case "gg"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://www.google.com/bookmarks/mark?op=edit&bkmk="&strUrl&"&title="&strTitle&"',550,500)"" title=""google"">"&strIco&"</a>&nbsp;"
		Case "live"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://favorites.live.com/quickadd.aspx?url="&strUrl&"&title="&strTitle&"',550,500)"" title=""live favorites"">"&strIco&"</a>&nbsp;"
		Case "su"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://www.stumbleupon.com/submit?url="&strUrl&"&title="&strTitle&"',550,500)"" title=""StumbleUpon"">"&strIco&"</a>&nbsp;"
		Case "yahoo_bk"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://e.my.yahoo.com/config/edit_bookmark?.src=bookmarks&.folder=1&.name="&strTitle&"&.url="&strUrl&"&.save=+Save+',550,500)"" title=""Yahoo Bookmark"">"&strIco&"</a>&nbsp;"
		Case "yahoo"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://myweb.yahoo.com/myresults/bookmarklet?&u="&strUrl&"&t="&strTitle&"',550,500)"" title=""Yahoo MyWeb"">"&strIco&"</a>&nbsp;"
		Case "deli"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://del.icio.us/post?&url="&strUrl&"&title="&strTitle&"',550,500)"" title=""Delicious"">"&strIco&"</a>&nbsp;"
		Case "magnolia"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://digg.com/submit?phase=2&url="&strUrl&"&title="&strTitle&"',550,500)"">"&strIco&"</a>&nbsp;"
		Case "blogmarks"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://digg.com/submit?phase=2&url="&strUrl&"&title="&strTitle&"',550,500)"">"&strIco&"</a>&nbsp;"
		Case "diigo"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://digg.com/submit?phase=2&url="&strUrl&"&title="&strTitle&"',550,500)"">"&strIco&"</a>&nbsp;"
		Case "blinkbits"
			strBookmark=strBookmark&"<a href=""javascript:popup('http://digg.com/submit?phase=2&url="&strUrl&"&title="&strTitle&"',550,500)"">"&strIco&"</a>&nbsp;"
	End Select
Next
strBookmark=strBookmark&"</fieldset></div></div><br>"
functionDisplaySocialBookmark=strBookmark
End Function
%>