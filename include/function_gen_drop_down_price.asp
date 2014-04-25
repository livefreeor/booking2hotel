<%
FUNCTION function_gen_drop_down_price(intPrice,strPriceName,intType)
	
	Dim strSelect1
	Dim strSelect2
	Dim strSelect3
	Dim strSelect4
	Dim strSelect5
	Dim strSelect6
	Dim strSelect7
	Dim strSelect8
	Dim strSelect9
	
	SELECT CASE intType
		Case 1 'Min Price
			
			IF intPrice="" Then
				intPrice = "10"
			Else
				intPrice = Cstr(intPrice)
			End IF
			
			IF intPrice="400" Then
				strSelect1 = "selected"
			ElseIF intPrice="800" Then
				strSelect2 = "selected"
			ElseIF intPrice="1200" Then
				strSelect3 = "selected"
			ElseIF intPrice="1600 "Then
				strSelect4 = "selected"
			ElseIF intPrice="2000" Then
				strSelect5 = "selected"
			ElseIF intPrice="2400" Then
				strSelect6 = "selected"
			ElseIF intPrice="2800" Then
				strSelect7 = "selected"
			ElseIF intPrice="3200" Then
				strSelect8 = "selected"
			ElseIF intPrice="3600" Then
				strSelect9 = "selected"
			End IF
			
			function_gen_drop_down_price = "<select name="""& strPriceName &""">" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""400"" "& strSelect1 &">Any</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""800"" "& strSelect2 &">800 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""1200"" "& strSelect3 &">1200 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""1600"" "& strSelect4 &">1600 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""2000"" "& strSelect5 &">2000 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""2400"" "& strSelect6 &">2400 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""2800"" "& strSelect7 &">2800 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""3200"" "& strSelect8 &">3200 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""3600"" "& strSelect9 &">3600 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "</select>" & VbCrlf

		Case 2 'Max Price
			IF intPrice="" Then
				intPrice = "510000"
			Else
				intPrice = Cstr(intPrice)
			End IF
			
			IF intPrice="510000" Then
				strSelect1 = "selected"
			ElseIF intPrice="1200" Then
				strSelect2 = "selected"
			ElseIF intPrice="1600" Then
				strSelect3 = "selected"
			ElseIF intPrice="2000" Then
				strSelect4 = "selected"
			ElseIF intPrice="2400" Then
				strSelect5 = "selected"
			ElseIF intPrice="2800" Then
				strSelect6 = "selected"
			ElseIF intPrice="3200" Then
				strSelect7 = "selected"
			ElseIF intPrice="500000" Then
				strSelect8 = "selected"
			End IF
		
			function_gen_drop_down_price = "<select name="""& strPriceName &""">" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""510000"" "& strSelect1 &">Any</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""1200"" "& strSelect2 &">1200 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""1600"" "& strSelect3 &">1600 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""2000"" "& strSelect4 &">2000 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""2400"" "& strSelect5 &">2400 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""2800"" "& strSelect6 &">2800 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""3200"" "& strSelect7 &">3200 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "<option value=""500000"" "& strSelect8 &">Over 3200 Baht</option>" & VbCrlf
			function_gen_drop_down_price = function_gen_drop_down_price & "</select>" & VbCrlf
                      
	END SELECT
END FUNCTION
%>