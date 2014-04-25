<%
FUNCTION function_gen_review_last(intProductID,intType)

	Dim sqlReview
	Dim recReview
	Dim arrReview
	Dim strReview
	Dim intCountReview
	Dim bolReview
	
	SELECT CASE intType
		Case 1 '### Home Page ###
			sqlReview = "SELECT TOP 3 pr.title_en,pr.detail,p.title_en,pr.rate_overall,p.destination_id,pr.review_id,p.product_id,p.files_name"
			sqlReview = sqlReview & " FROM tbl_product_review pr,tbl_product p"
			sqlReview = sqlReview & " WHERE p.product_id=pr.product_id AND Len(pr.title_en)>10 AND pr.status=1 "
			sqlReview = sqlReview & " ORDER BY pr.review_id DESC"
			Set recReview = Server.CreateObject ("ADODB.Recordset")
			recReview.Open SqlReview, Conn,adOpenStatic,adLockreadOnly
				arrReview = recReview.GetRows()
			recReview.Close
			SET recReview=Nothing

			For intCountReview=0 To Ubound(arrReview,2)
				strReview = strReview  & "<p class=""f13"">" & VbCrlf
				strReview = strReview & "<a href=""/"&function_generate_hotel_link(arrReview(4,intCountReview) ,0,1)&"/"&arrReview(7,intCountReview) &""" class=""l2"">"&arrReview(2,intCountReview)&"</a><br>" & VbCrlf
				strReview = strReview & function_generate_star_rate(arrReview(3,intCountReview),arrReview(6,intCountReview),2) & "<br>" & VbCrlf
				strReview = strReview & function_display_text(arrReview(1,intCountReview),"<a href=""/review.asp?id="&arrReview(6,intCountReview) & "#"& arrReview(5,intCountReview) &""">Read More</a>",200,1) & VbCrlf
				strReview = strReview & "</p>" & VbCrlf
			Next

		Case 2 '### Hotel Detail ###
			sqlReview = "SELECT TOP 1 pr.title_en,pr.detail,p.title_en,pr.rate_overall,p.destination_id,pr.review_id,p.product_id,p.files_name"
			sqlReview = sqlReview & " FROM tbl_product_review pr,tbl_product p"
			sqlReview = sqlReview & " WHERE p.product_id=pr.product_id AND pr.status=1 AND p.product_id=" & intProductID
			sqlReview = sqlReview & " ORDER BY pr.review_id DESC"
			Set recReview = Server.CreateObject ("ADODB.Recordset")
			recReview.Open SqlReview, Conn,adOpenStatic,adLockreadOnly
				IF NOT recReview.EOF Then
					arrReview = recReview.GetRows()
					bolReview = True
				Else
					bolReview = False
				End IF
			recReview.Close
			SET recReview=Nothing

			IF bolReview Then
				strReview = strReview  & "<br />" & VbCrlf
		  		strReview = strReview  & "<fieldset id=""tbl_last_review"">" & VbCrlf
				strReview = strReview  & "<legend><strong>Lastest "& arrReview(2,0) &" Review</strong></legend>" & VbCrlf
				strReview = strReview  & "<span class=""f13"">" & VbCrlf
				strReview = strReview & "<strong><font color=""#FF8040"">"&arrReview(0,0)&"</font></strong><br />" & VbCrlf
				strReview = strReview & function_generate_star_rate(arrReview(3,intCountReview),arrReview(6,intCountReview),2) & "<br /><br />" & VbCrlf
				strReview = strReview & function_display_text(arrReview(1,intCountReview),"<a href=""/review.asp?id="&arrReview(6,intCountReview) & "#"& arrReview(5,intCountReview) &""">Read More</a>",1000,2) & VbCrlf
				strReview = strReview & "</span>" & VbCrlf
				strReview = strReview  & "</fieldset>" & VbCrlf
				
			End IF

		
		
		Case 3 '### Home Page With Form Action ###
			sqlReview = "SELECT TOP 3 pr.title_en,pr.detail,p.title_en,pr.rate_overall,p.destination_id,pr.review_id,p.product_id,p.files_name"
			sqlReview = sqlReview & " FROM tbl_product_review pr,tbl_product p"
			sqlReview = sqlReview & " WHERE p.product_id=pr.product_id AND Len(pr.title_en)>10 AND pr.status=1 "
			sqlReview = sqlReview & " ORDER BY pr.review_id DESC"
			Set recReview = Server.CreateObject ("ADODB.Recordset")
			recReview.Open SqlReview, Conn,adOpenStatic,adLockreadOnly
				arrReview = recReview.GetRows()
			recReview.Close
			SET recReview=Nothing

			For intCountReview=0 To Ubound(arrReview,2)
				strReview = strReview  & "<p class=""f13"">" & VbCrlf
				strReview = strReview & "<form action=""/process/reDir.asp"" method=""post"">"
				strReview = strReview & "<input type=""hidden"" name=""intType"" value=""1"">" & VbCrlf
				strReview = strReview & "<input type=""hidden"" name=""id"" value="""&arrReview(6,intCountReview)&""">" & VbCrlf
				strReview = strReview & "<input type=""hidden"" name=""position"" value="""&arrReview(5,intCountReview)&""">" & VbCrlf
				strReview = strReview & "<strong><font color=""#FF8040"">" & arrReview(2,intCountReview) & "</font></strong><br />" & VbCrlf
				strReview = strReview & function_generate_star_rate(arrReview(3,intCountReview),arrReview(6,intCountReview),2) & "<br>" & VbCrlf
				strReview = strReview & function_display_text(arrReview(1,intCountReview)," ",200,1) & VbCrlf
				strReview = strReview & "<input name="""" type=""image"" src=""/images/ico_readmore.gif"" />" & VbCrlf
				strReview = strReview & "</form>" & VbCrlf
				
				
				strReview = strReview & "</p>" & VbCrlf
			Next
			
		Case 4 '### Hotel Home ###
		Case 5 '### Day Trip Home ###
		Case 6 '### Day Trip Detail ###
	END SELECT
	
	function_gen_review_last = strReview

END FUNCTION
%>