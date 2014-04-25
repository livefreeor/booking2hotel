<%
FUNCTION function_gen_optimize_string(intDestination,intLocation,strDestination,strLocation,intProduct,strProduct,intPageType,intStringType)

	SELECT CASE intStringType
	
		CASE 1 'Meta
			SELECT CASE intPageType
				Case 1 'Destination
					function_gen_optimize_string ="<meta name=""description"" content="""&strDestination&" Hotels - Reservation hotels in "&strDestination&" with lower price."">" & VbCrlf
					function_gen_optimize_string = function_gen_optimize_string  & "<meta name=""keywords"" content="""&strDestination&" hotels, hotels in "&strDestination&", hotels in "&strDestination&" Thailand"">" & VbCrlf
				
				Case 2 'Location
					function_gen_optimize_string ="<meta name=""description"" content="""&strLocation&" Hotels - Reservation hotels in "&strLocation&" with lower price."">" & VbCrlf
					function_gen_optimize_string = function_gen_optimize_string  & "<meta name=""keywords"" content="""&strLocation&" hotels, hotels in "&strLocation&", hotels in "&strLocation&" Thailand"">" & VbCrlf
					
				Case 3 'Detail
					function_gen_optimize_string ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Reservation"">" & VbCrlf
					function_gen_optimize_string = function_gen_optimize_string  & "<meta name=""keywords"" content="""&strProduct&" reservations, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
				
				Case 4 'Information
					function_gen_optimize_string ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Information"">" & VbCrlf
					function_gen_optimize_string = function_gen_optimize_string  & "<meta name=""keywords"" content="""&strProduct&" Information, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
				
				Case 5 'Photo
					function_gen_optimize_string ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Photo"">" & VbCrlf
					function_gen_optimize_string = function_gen_optimize_string  & "<meta name=""keywords"" content="""&strProduct&" Photo, "&strProduct&" pictures, "&strProduct&" "& strDestination&""">" & VbCrlf
					
				Case 6 'Review
					function_gen_optimize_string ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Reviews"">" & VbCrlf
					function_gen_optimize_string = function_gen_optimize_string  & "<meta name=""keywords"" content="""&strProduct&" reviews, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
					
				Case 7 'Why
					function_gen_optimize_string ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Reservation"">" & VbCrlf
					function_gen_optimize_string = function_gen_optimize_string  & "<meta name=""keywords"" content="""&strProduct&" reservations, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
					
				Case 8 'Contact
					function_gen_optimize_string ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Contact"">" & VbCrlf
					function_gen_optimize_string = function_gen_optimize_string  & "<meta name=""keywords"" content="""&strProduct&" contact, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
			END SELECT
			
		Case 2 'Title
			SELECT CASE intPageType
				Case 1 'Destination
					function_gen_optimize_string = strDestination & " Hotels and All Hotels in " & strDestination
				Case 2 'Location
					function_gen_optimize_string = strLocation & " Hotels , "& strDestination &" Hotels"
				Case 3 'Detail
					IF Instr(strProduct,strDestination)>0 Then
						function_gen_optimize_string = strProduct
					Else
						function_gen_optimize_string = strProduct & " " & strDestination
					End IF
				Case 4 'Information
					IF Instr(strProduct,strDestination)>0 Then
						function_gen_optimize_string = strProduct & " " & " Information"
					Else
						function_gen_optimize_string = strProduct & " " & strDestination & " Information"
					End IF
				Case 5 'Photo
					IF Instr(strProduct,strDestination)>0 Then
						function_gen_optimize_string = strProduct & " " & " Pictures"
					Else
						function_gen_optimize_string = strProduct & " " & strDestination & " Pictures"
					End IF
				Case 6 'Review
					IF Instr(strProduct,strDestination)>0 Then
						function_gen_optimize_string = strProduct & " " & " Reviews"
					Else
						function_gen_optimize_string = strProduct & " " & strDestination & " Reviews"
					End IF
				Case 7 'Why
					IF Instr(strProduct,strDestination)>0 Then
						function_gen_optimize_string = strProduct & " " & " Why Choose Hotels2Thailand.com?"
					Else
						function_gen_optimize_string = strProduct & " " & strDestination & " Why Choose Hotels2Thailand.com?"
					End IF
				Case 8 'Contact
					IF Instr(strProduct,strDestination)>0 Then
						function_gen_optimize_string = strProduct & " " & " Contact Us"
					Else
						function_gen_optimize_string = strProduct & " " & strDestination & " Contact Us"
					End IF
			END SELECT
			
		Case 3 'H1
			SELECT CASE intPageType
				Case 1 'Destination
					function_gen_optimize_string = strDestination & " Hotels Thailand" & VbCrlf
				Case 2 'Location
					function_gen_optimize_string = strLocation & " " & strDestination & " Hotels" & VbCrlf
				Case 3 'Detail
					function_gen_optimize_string = strProduct
				Case 4 'Information
					function_gen_optimize_string = strProduct & " Information"
				Case 5 'Photo
					function_gen_optimize_string = strProduct & " Pictures"
				Case 6 'Review
					function_gen_optimize_string = strProduct & " Reviews"
				Case 7 'Why
					function_gen_optimize_string = strProduct & " : Why Choose Hotels2Thailand.com ?"
				Case 8 'Contact
				function_gen_optimize_string = strProduct & " Contact Us"
			END SELECT
			
		Case 4 'Link Back To Home
			SELECT CASE intPageType
				Case 1 'Destination
					function_gen_optimize_string = strDestination & " Hotels Thailand"
				Case 2 'Location
					function_gen_optimize_string = strLocation & " " & strDestination & " Hotels Thailand"
				Case 3 'Detail
					function_gen_optimize_string = strProduct & " ," & strDestination & " Thailand"
				Case 4 'Information
					function_gen_optimize_string = strProduct & " , " & strDestination & " Hotels Thailand Hotels"
				Case 5 'Photo
					function_gen_optimize_string = strProduct & " , " & strDestination & " Hotels Thailand Hotels"
				Case 6 'Review
					function_gen_optimize_string = strProduct & " , " & strDestination & " Hotels Thailand Hotels"
				Case 7 'Why
					function_gen_optimize_string = strProduct & " , " & strDestination & " Hotels Thailand Hotels"
				Case 8 'Contact
					function_gen_optimize_string = strProduct & " , " & strDestination & " Hotels Thailand Hotels"
			END SELECT
			
	END SELECT

END FUNCTION
%>