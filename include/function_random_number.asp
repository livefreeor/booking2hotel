<%
FUNCTION function_random_number(intMin,intMax,intType)

	SELECT CASE intType
		Case 1
			function_random_number = Int((IntMax - intMin + 1)*Rnd() + intMin)
		
		Case 2
		
	END SELECT
	
END FUNCTION
%>