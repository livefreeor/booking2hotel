<%
FUNCTION function_gen_res_image(strProductCode,intNumPic,intResID)

	IF intNumPic>0 Then
		function_gen_res_image = "<img src="""& "/images_hotels_restaurant/" & strProductCode & "_r_" & intResID & ".jpg" &""">"
	Else
		function_gen_res_image = "&nbsp;"
	End IF
END FUNCTION
%>