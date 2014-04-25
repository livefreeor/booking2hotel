<%
FUNCTION function_xml_correct(strInput,intType)
	
	SELECT CASE intType
		Case 1 'Simle Text
			function_xml_correct = Replace(strInput,"&"," and ")
		Case 2 'URL
			function_xml_correct = Replace(strInput,"&","%26")
		END SELECT

END FUNCTION
%>