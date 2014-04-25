<%
FUNCTION fnRandom(intUpper,intLower,strAddedNum)
	Dim intRandomCount
	Dim intRand
	
	For intRandomCount=1 To 5000 'Gennerate Random Number
	
		Randomize
		intRand = Int((intUpper - intLower + 1)*Rnd() + intLower)

		IF NOT INSTR(strAddedNum,"|" & intRand & "|")>0 Then
			fnRandom = intRand
			Exit For
		End IF
	
	Next
	
END FUNCTION
%>