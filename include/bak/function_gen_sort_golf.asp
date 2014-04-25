<%
FUNCTION function_gen_sort_golf(strCurrent,strURL,intType)

	Dim strFeatureDESC
	Dim strPriceASC
	Dim strPriceDESC
	Dim strNameASC
	Dim strNameDESC
	Dim strClassASC
	Dim strClassDESC
	
	SELECT CASE intType
		Case 1
			strFeatureDESC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=featureDESC"">Featured Hotels"
			strPriceASC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=priceASC"">Price Low to High"
			strPriceDESC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=priceDESC"">Price High to Low"
			strNameASC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=nameASC"">Name A-Z"
			strNameDESC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=nameDESC"">Name Z-A"
			strClassASC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=classASC"">Hotel Class Low to High"
			strClassDESC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=classDESC"">Hotel Class High to Low"
			
			SELECT CASE strCurrent
				Case "featureDESC"
					strFeatureDESC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=featureDESC"" checked><font color=""#346494""><b>Featured Hotels</b></font>"
				Case "priceASC"
					strPriceASC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=priceASC"" checked><font color=""#346494""><b>Price Low to High</b></font>"
				Case "priceDESC"
					strPriceDESC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=priceDESC"" checked><font color=""#346494""><b>Price High to Low</b></font>"
				Case "nameASC"
					strNameASC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=nameASC"" checked><font color=""#346494""><b>Name A-Z</b></font>"
				Case "nameDESC"
					strNameDESC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=nameDESC"" checked><font color=""#346494""><b>Name Z-A</b></font>"
				Case "classASC"
					strClassASC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=classASC"" checked><font color=""#346494""><b>Hotel Class Low to High</b></font>"
				Case "classDESC"
					strClassDESC = "<input name=""url"" type=""radio"" value="""& strURL &"&sort=classDESC"" checked><font color=""#346494""><b>Hotel Class High to Low</b></font>"
			END SELECT
			
			function_gen_sort_golf = function_gen_sort_golf & "<table width=""100%"" cellpadding=""0""  cellspacing=""1"" bgcolor=""#FFFFFF"">" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<form action=""redirect.asp"" method=""get"">" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<tr>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<td rowspan=""2"" bgcolor=""#EDF5FE"">"& strFeatureDESC &"</td>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<td bgcolor=""#EDF5FE"">"& strPriceASC &"</td>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<td bgcolor=""#EDF5FE"">"& strNameASC &"</td>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<td rowspan=""2"" bgcolor=""#EDF5FE""><div align=""center""><input type=""image"" src=""/images/b_go.gif""></div></td>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "</tr>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<tr>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<td bgcolor=""#EDF5FE"">"& strPriceDESC &"</td>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "<td bgcolor=""#EDF5FE"">"& strNameDESC &"</td>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "</tr>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "</form>" & VbCrlf
			function_gen_sort_golf = function_gen_sort_golf & "</table>" & VbCrlf
			
	END SELECT
END FUNCTION
%>