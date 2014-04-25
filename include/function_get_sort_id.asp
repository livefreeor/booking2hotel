<%
FUNCTION function_get_sort_id(strSort)
	SELECT CASE strSort
		Case "featureDESC"
			function_get_sort_id = 4
		Case "priceASC"
			function_get_sort_id = 3
		Case "priceDESC"
			function_get_sort_id = 7
		Case "nameASC"
			function_get_sort_id = 2
		Case "nameDESC"
			function_get_sort_id = 6
		Case "classASC"
			function_get_sort_id = 5
		Case "classDESC"
			function_get_sort_id = 8
	END SELECT

END FUNCTION
%>