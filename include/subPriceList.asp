<%
Sub subPriceList (intSelected, intType)
	
	Dim str500, str1000, str1500, str2000, str2500, str3000, str3500, str4000, str200000
	
	IF intSelected <> "" Then
		intSelected = Cstr(Trim(intSelected))
		
		SELECT CASE intSelected
			Case "500"
				str500 = "selected"
			Case "1000"
				str1000 = "selected"
			Case "1500"
				str1500 = "selected"
			Case "2000"
				str2000 = "selected"
			Case "2500"
				str2500 = "selected"
			Case "3000"
				str3000 = "selected"
			Case "3500"
				str3500 = "selected"
			Case "4000"
				str4000 = "selected"
			Case "500000"
				str200000 = "selected"
		END SELECT
		
	End IF
	
	SELECT CASE intType
		Case 1 'Minimum
			Response.Write "<select name=""start_price"">" & VbCrlf
			Response.Write "<option value=""500"">Minimum</option>" & VbCrlf
			Response.Write "<option value=""500"" "&str500&">500 Baht TH (13 $US)</option>" & VbCrlf
			Response.Write "<option value=""1000"" "&str1000&">1000 Baht TH (25 $US)</option>" & VbCrlf
			Response.Write "<option value=""1500"" "&str1500&">1500 Baht TH (38 $US)</option>" & VbCrlf
			Response.Write "<option value=""2000"" "&str2000&">2000 Baht TH (50 $US)</option>" & VbCrlf
			Response.Write "<option value=""2500"" "&str2500&">2500 Baht TH (63 $US)</option>" & VbCrlf
			Response.Write "<option value=""3000"" "&str3000&">3000 Baht TH (75 $US)</option>" & VbCrlf
			Response.Write "<option value=""3500"" "&str3500&">3500 Baht TH (88 $US)</option>" & VbCrlf
			Response.Write "<option value=""4000"" "&str4000&">4000 Baht TH (100 $US)</option>" & VbCrlf
			Response.Write "</select>" & VbCrlf
		Case 2 'Maximum
			Response.Write "<select name=""end_price"">" & VbCrlf
			Response.Write "<option value=""500000"">Maximum</option>" & VbCrlf
			Response.Write "<option value=""1000"" "&str1000&">1000 Baht TH (25 $US)</option>" & VbCrlf
			Response.Write "<option value=""1500"" "&str1500&">1500 Baht TH (38 $US)</option>" & VbCrlf
			Response.Write "<option value=""2000"" "&str2000&">2000 Baht TH (50 $US)</option>" & VbCrlf
			Response.Write "<option value=""2500"" "&str2500&">2500 Baht TH (63 $US)</option>" & VbCrlf
			Response.Write "<option value=""3000"" "&str3000&">3000 Baht TH (75 $US)</option>" & VbCrlf
			Response.Write "<option value=""3500"" "&str3500&">3500 Baht TH (88 $US)</option>" & VbCrlf
			Response.Write "<option value=""500000"" "&str200000&">Over 4000 Baht TH (100 $US)</option>" & VbCrlf
			Response.Write "</select>" & VbCrlf		
	END SELECT
End Sub
%>
