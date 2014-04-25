<%
FUNCTION function_generate_star(strStar,intDisplayType)
	
	Dim intStarCount
	Dim star
	SELECT CASE intDisplayType
		Case 1
			
'			strStar = Cint(strStar)
'			
'			For intStarCount=1 TO strStar
'				Response.Write "<img src=""/images/pic_star.gif"">"
'			Next
			For intStarCount=1 to int(strStar)
					star=star&"<img src='/images/pic_star.gif'>"
				Next
			
				IF cdbl(strStar) / int(strStar) <> 1 then
					star=star&"<img src='/images/pic_star_half.gif'>"
				End IF
			function_generate_star=star
			
		Case 2
			strStar = Cint(strStar)
			
			For intStarCount=1 TO strStar
				function_generate_star = function_generate_star & "<img src=""/images/pic_star_rate.gif"">"
			Next
			
	END SELECT

END FUNCTION
%>