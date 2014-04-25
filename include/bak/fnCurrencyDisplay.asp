<%
FUNCTION fnCurrencyDisplay(intMoney)
	
	Dim intMoneyTemp
	
	intMoneyTemp = intMoney
	
	IF Session("currency_id")<>"" Then
	
		SELECT CASE Session("currency_id")
			Case currTHBID
				intMoneyTemp = intMoneyTemp * 1
				intMoneyTemp = FormatNumber(intMoneyTemp,0)
			Case Else
				intMoneyTemp = intMoneyTemp / Session("currency_pr")
				intMoneyTemp = FormatNumber(intMoneyTemp,0)
		END SELECT
	
	Else
		intMoneyTemp = intMoneyTemp * 1
		intMoneyTemp = FormatNumber(intMoneyTemp,0)
	End IF
	
	fnCurrencyDisplay = intMoneyTemp
	
END FUNCTION
%>