<%
FUNCTION function_gen_forum_last(intDestinationID,intType)

	Dim sqlForum
	Dim recForum
	Dim arrForum
	Dim intCountForum
	Dim strForum
	
	SELECT CASE intType
		Case 1 '### Home Page ###
		
			sqlForum = "SELECT TOP 3 fq.question_id,fq.destination_id,fq.title,fq.detail,d.title_en"
			sqlForum = sqlForum & " FROM tbl_forum_question fq, tbl_destination d"
			sqlForum = sqlForum & " WHERE d.destination_id=fq.destination_id AND fq.status=1"
			sqlForum = sqlForum & " ORDER BY question_id DESC"
			Set recForum = Server.CreateObject ("ADODB.Recordset")
			recForum.Open SqlForum, Conn,adOpenStatic,adLockreadOnly
				arrForum = recForum.GetRows()
			recForum.Close
			SET recForum=Nothing
		
			For intCountForum=0 To Ubound(arrForum,2)
			
				strForum = strForum & "<p class=""f13"">" & VbCrlf
				strForum = strForum & "<a href=""/Travel_Forum/topic.aspx?des_id="&arrForum(1,intCountForum)&"&key=&start=&end=&pr=0"" class=""l2"">"&arrForum(4,intCountForum)&" Forum</a><br />" & VbCrlf
				strForum = strForum & function_display_text(arrForum(3,intCountForum),"<a href=""/Travel_Forum/Q-Detail.aspx?q_id=" & arrForum(0,intCountForum) & """>Read More</a>",200,1) & VbCrlf
				strForum = strForum & "</p>" & VbCrlf
			
			Next

		Case 2 '### Home Page With Form Action ###
			sqlForum = "SELECT TOP 3 fq.question_id,fq.destination_id,fq.title,fq.detail,d.title_en"
			sqlForum = sqlForum & " FROM tbl_forum_question fq, tbl_destination d"
			sqlForum = sqlForum & " WHERE d.destination_id=fq.destination_id AND fq.status=1"
			sqlForum = sqlForum & " ORDER BY question_id DESC"
			Set recForum = Server.CreateObject ("ADODB.Recordset")
			recForum.Open SqlForum, Conn,adOpenStatic,adLockreadOnly
				arrForum = recForum.GetRows()
			recForum.Close
			SET recForum=Nothing
		
			For intCountForum=0 To Ubound(arrForum,2)
			
				strForum = strForum & "<p class=""f13"">" & VbCrlf
				strForum = strForum & "<form action=""/process/reDir.asp"" method=""post"">"
				strForum = strForum & "<input type=""hidden"" name=""intType"" value=""2"">" & VbCrlf
				strForum = strForum & "<input type=""hidden"" name=""id"" value="""&arrForum(0,intCountForum)&""">" & VbCrlf
				strForum = strForum & "<strong><font color=""#FF8040"">"&arrForum(4,intCountForum)&" Forum </font></strong><br />" & VbCrlf
				strForum = strForum & function_display_text(arrForum(3,intCountForum)," ",200,1) & VbCrlf
				strForum = strForum & "<input name="""" type=""image"" src=""/images/ico_readmore.gif"" />" & VbCrlf
				strForum = strForum & "</form>" & VbCrlf
				strForum = strForum & "</p>" & VbCrlf
			
			Next
				
		Case 3
	END SELECT
	
	function_gen_forum_last = strForum

END FUNCTION
%>