<%
FUNCTION function_generate_star_rate(realStar,intProductID,intType)

	Dim intStarCount
	Dim intStar
	Dim realStarPartial
	Dim strStar
	
	'### WRITE FULL STAR ###
	For intStarCount=1 To 5
		IF intStarCount<=realStar Then
			strStar = strStar & "<img src=""/images/pic_star_rate.gif"">"
			intStar = intStar + 1
		End IF
	Next
	'### WRITE FULL STAR ###
	
	'### WRITE PARTIAL STAR ###
	IF intStar<5 Then
		realStarPartial = realStar-intStar
		
		IF realStarPartial >= 0.01 AND realStarPartial<0.33 Then
			strStar = strStar & "<img src=""/images/pic_star_rate_1_3.gif"">"
		ElseIF realStarPartial >= 0.33 AND realStarPartial<0.66 Then
			strStar = strStar & "<img src=""/images/pic_star_rate_1_2.gif"">"
		ElseIF realStarPartial >= 0.66 AND realStarPartial<0.99 Then
			strStar = strStar & "<img src=""/images/pic_star_rate_2_3.gif"">"
		ElseIF realStarPartial=0 Then
			strStar = strStar & "<img src=""/images/pic_star_rate_gray.gif"">"
		End IF
	'### WRITE PARTIAL STAR ###
	
	'### WRITE GRAY STAR ###
	IF 5-intStar>1 Then
		For intStarCount=2 To 5-intStar
			strStar = strStar & "<img src=""/images/pic_star_rate_gray.gif"">"
		Next
	End IF
	'### WRITE GRAY STAR ###
	End IF
	
	SELECT CASE intType
		Case 1 '###Write###
			Response.Write strStar
		Case 2 '###Return###
			function_generate_star_rate = strStar
	END SELECT
	
END FUNCTION
%>