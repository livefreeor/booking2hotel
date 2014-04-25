<%
FUNCTION function_numberic_translate(intInput,intType)

	Dim intThousand
	Dim intCal
	Dim bolThousand
	Dim intHundred
	Dim bolHundred
	Dim intTen
	Dim bolTen
	Dim intUnit
	Dim bolUnit
	Dim strThousand
	Dim strHundred
	Dim strTen
	Dim strUnit
	Dim strThousandHundred
	Dim strThousandTen
	Dim bolTenUnit
	Dim bolCombine
	
	bolThousand = False
	bolHundred = False
	bolTen = False
	bolUnit = False

	intCal = intInput
	
	SELECT CASE intType
		Case 1 'English
		
			intThousand = intCal/1000
			
			IF intThousand>=1 Then
				IF intThousand>=Round(intThousand) Then
					intThousand=Round(intThousand)
				Else
					intThousand=Round(intThousand)-1
				End IF
				intCal = intCal-(intThousand*1000)
				bolThousand = True
			End IF
			
			intHundred = intCal/100
			IF intHundred>=1 Then
				IF intHundred>=Round(intHundred) Then
					intHundred=Round(intHundred)
				Else
					intHundred=Round(intHundred)-1
				End IF
				intCal = intCal-(intHundred*100)
				bolHundred = True
			End IF
		
			intTen = intCal/10
			IF intTen>0 Then
				IF intTen>=Round(intTen) Then
					intTen=Round(intTen)
				Else
					intTen=Round(intTen)-1
				End IF
				intCal = intCal-(intTen*10)
				bolTen = True
			End IF
			
			IF intCal>0 Then
				intUnit = intCal
				bolUnit = True
			End IF

			IF bolThousand Then
				IF intThousand>=1 AND intThousand<=9 Then
					strThousand = function_numberic_map(intThousand,1,1) & " Thousand"
				ElseIF intThousand>=10 AND intThousand<=999 Then
					strThousand = function_numberic_translate_hundred(intThousand,1) & " Thousand"
				End IF
			End IF
			
			IF bolHundred Then
				strHundred = function_numberic_map(intHundred,1,intType) & " Hundred"
			End IF
			
			IF bolTen Then
				bolCombine = False
				SELECT CASE (intTen*10)+intUnit
					Case 11,12,13,14,15,16,17,18,19
						bolCombine = True
						bolTenUnit = True
						strTen = function_numberic_map((intTen*10)+intUnit,2,intType)
					Case Else
						IF NOT bolCombine Then
							IF NOT bolUnit Then
								strTen = function_numberic_map(intTen,2,intType)
								bolTenUnit = True
							Else
								strTen = function_numberic_map(intTen,2,intType) & " " & function_numberic_map(intUnit,1,intType)
								bolTenUnit = True
							End IF
						End IF
				END SELECT
			End IF
			
			IF bolTenUnit Then
				function_numberic_translate = strThousand & " " & strHundred & " and " & strTen
			ElseIF bolThousand AND bolHundred Then
				function_numberic_translate = strThousand & " and " & strHundred
			ElseIF  bolHundred AND NOT bolThousand Then
				function_numberic_translate = strHundred
			Else
				function_numberic_translate = strThousand
			End IF
			
		Case 2 'Thai
	END SELECT

END FUNCTION

FUNCTION function_numberic_map(intNumber,intType,intLang)
	SELECT CASE intLang
		Case 1'English

			SELECT CASE int(intType)
				Case 1 'Unit
					SELECT CASE intNumber
						Case 1
							function_numberic_map = "One"
						Case 2
							function_numberic_map = "Two"
						Case 3
							function_numberic_map = "Three"
						Case 4
							function_numberic_map = "Four"
						Case 5
							function_numberic_map = "Five"
						Case 6
							function_numberic_map = "Six"
						Case 7
							function_numberic_map = "Seven"
						Case 8
							function_numberic_map = "Eight"
						Case 9
							function_numberic_map = "Nine"
					END SELECT
					
				Case 2 'Ten
					SELECT CASE intNumber
						Case 1
							function_numberic_map = "Ten"
						Case 11
							function_numberic_map = "Eleven"
						Case 12
							function_numberic_map = "Twelve"
						Case 13
							function_numberic_map = "Thirteen"
						Case 14
							function_numberic_map = "Fourteen"
						Case 15
							function_numberic_map = "Fifteen"
						Case 16
							function_numberic_map = "Sixteen"
						Case 17
							function_numberic_map = "Seventeen"
						Case 18
							function_numberic_map = "Eighteen"
						Case 19
							function_numberic_map = "Nineteen"
						Case 2
							function_numberic_map = "Twenty"
						Case 3
							function_numberic_map = "Thirty"
						Case 4
							function_numberic_map = "Fourty"
						Case 5
							function_numberic_map = "Fifty"
						Case 6
							function_numberic_map = "Sixty"
						Case 7
							function_numberic_map = "Seventy"
						Case 8
							function_numberic_map = "Eighty"
						Case 9
							function_numberic_map = "Ninety"
					END SELECT
			END SELECT
			
		Case 2 'Thai
	END SELECT
END FUNCTION

FUNCTION function_numberic_translate_hundred(intInputNum,intLangType)
	Dim intCal
	Dim intHundred
	Dim bolHundred
	Dim intTen
	Dim bolTen
	Dim intUnit
	Dim bolUnit
	Dim strThousand
	Dim strHundred
	Dim strTen
	Dim strUnit
	Dim strThousandHundred
	Dim strThousandTen
	Dim bolCombine
	
	bolHundred = False
	bolTen = False
	bolUnit = False
	
	intCal = intInputNum
	
	SELECT CASE intLangType
		
		Case 1 'English
			intHundred = intCal/100
			
			IF intHundred>=1 Then
				IF intHundred>=Round(intHundred) Then
					intHundred=Round(intHundred)
				Else
					intHundred=Round(intHundred)-1
				End IF
				intCal = intCal-(intHundred*100)
				bolHundred = True
			End IF

			intTen = intCal/10
			IF intTen>=1 Then
				IF intTen>=Round(intTen) Then
					intTen=Round(intTen)
				Else
					intTen=Round(intTen)-1
				End IF
				intCal = intCal-(intTen*10)
				bolTen = True
			End IF
			
			IF intCal>0 Then
				intUnit = intCal
				bolUnit = True
			End IF
			
			IF bolHundred Then
				strHundred = function_numberic_map(intHundred,1,intLangType) & " Hundred"
			End IF
			
			IF bolTen Then
				bolCombine = False
				SELECT CASE (intTen*10)+intUnit
					Case 11,12,13,14,15,16,17,18,19
						bolCombine = True
						strTen = function_numberic_map((intTen*10)+intUnit,2,intLangType)
					Case Else
						IF NOT bolCombine Then
							IF NOT bolUnit Then
								strTen = function_numberic_map(intTen,2,intLangType)
							Else
								strTen = function_numberic_map(intTen,2,intLangType) & " " & function_numberic_map(intUnit,1,intLangType)
							End IF
						End IF
				END SELECT
			End IF
			
			IF bolHundred Then
				function_numberic_translate_hundred = strHundred & " " & strTen
			Else
				function_numberic_translate_hundred = strTen
			End IF
			
		Case 2 'Thai
	END SELECT

END FUNCTION
%>