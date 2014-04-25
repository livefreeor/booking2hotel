<%
FUNCTION function_gen_optimize_string_golf(intDestination,intLocation,strDestination,strLocation,intProduct,strProduct,intPageType,intStringType)

	SELECT CASE intStringType
	
		CASE 1 'Meta
			SELECT CASE intPageType
				Case 1 'Destination
					function_gen_optimize_string_golf ="<meta name=""description"" content="""&strDestination&" Golf Courses All Golf Course in "&strDestination&""">" & VbCrlf
					function_gen_optimize_string_golf = function_gen_optimize_string_golf  & "<meta name=""keywords"" content="""&strDestination&" golf courses, golf courses in "&strDestination&", golf course in "&strDestination&" Thailand"">" & VbCrlf
				
				Case 2 'Location
					function_gen_optimize_string_golf ="<meta name=""description"" content="""&strLocation&" Golf Courses All Golf Course in "&strLocation&""">" & VbCrlf
					function_gen_optimize_string_golf = function_gen_optimize_string_golf  & "<meta name=""keywords"" content="""&strLocation&" golf courses, golf courses in "&strLocation&", golf course in "&strLocation&" Thailand"">" & VbCrlf
					
				Case 3 'Detail
					function_gen_optimize_string_golf ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Golf Course Online Booking"">" & VbCrlf
					function_gen_optimize_string_golf = function_gen_optimize_string_golf  & "<meta name=""keywords"" content="""&strProduct&" reservations, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
				
				Case 4 'Information
					function_gen_optimize_string_golf ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Information"">" & VbCrlf
					function_gen_optimize_string_golf = function_gen_optimize_string_golf  & "<meta name=""keywords"" content="""&strProduct&" Information, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
				
				Case 5 'Photo
					function_gen_optimize_string_golf ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Photo"">" & VbCrlf
					function_gen_optimize_string_golf = function_gen_optimize_string_golf  & "<meta name=""keywords"" content="""&strProduct&" Photo, "&strProduct&" pictures, "&strProduct&" "& strDestination&""">" & VbCrlf
					
				Case 6 'Review
					function_gen_optimize_string_golf ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Reviews"">" & VbCrlf
					function_gen_optimize_string_golf = function_gen_optimize_string_golf  & "<meta name=""keywords"" content="""&strProduct&" reviews, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
					
				Case 7 'Why
					function_gen_optimize_string_golf ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Reservation"">" & VbCrlf
					function_gen_optimize_string_golf = function_gen_optimize_string_golf  & "<meta name=""keywords"" content="""&strProduct&" reservations, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
					
				Case 8 'Contact
					function_gen_optimize_string_golf ="<meta name=""description"" content="""&StrProduct&" : "&strProduct&" Contact"">" & VbCrlf
					function_gen_optimize_string_golf = function_gen_optimize_string_golf  & "<meta name=""keywords"" content="""&strProduct&" contact, "&strProduct&" booking, "&strProduct&" "& strDestination&""">" & VbCrlf
			END SELECT
			
		Case 2 'Title
			SELECT CASE intPageType
				Case 1 'Destination
					function_gen_optimize_string_golf = strDestination & " Golf Courses : All Golf Course in " & strDestination
				Case 2 'Location
					function_gen_optimize_string_golf = strLocation & " Golf Courses , "& strDestination &" Golf Course and All Golf Course in Thailand"
				Case 3 'Detail
					function_gen_optimize_string_golf = strProduct & " " & strDestination & " : Thailand Golf Course"
				Case 4 'Information
					function_gen_optimize_string_golf = strProduct & " " & strDestination & " Information : Thailand Golf Courses"
				Case 5 'Photo
					function_gen_optimize_string_golf = strProduct & " " & strDestination & " Pictures : Thailand Golf Course"
				Case 6 'Review
					function_gen_optimize_string_golf = strProduct & " " & strDestination & " Reviews : Thailand Golf Courses"
				Case 7 'Why
					function_gen_optimize_string_golf = strProduct & " " & strDestination & " Why Choose Hotels2Thailand.com? : Thailand Golf Courses"
				Case 8 'Contact
					function_gen_optimize_string_golf = strProduct & " " & strDestination & " : Contact Us : Thailand Golf Courses"
			END SELECT
			
		Case 3 'H1
			SELECT CASE intPageType
				Case 1 'Destination
					function_gen_optimize_string_golf = strDestination & " Golf Courses" & VbCrlf
				Case 2 'Location
					function_gen_optimize_string_golf = strLocation & " " & strDestination & " Golf Course" & VbCrlf
				Case 3 'Detail
					function_gen_optimize_string_golf = strProduct
				Case 4 'Information
					function_gen_optimize_string_golf = strProduct & " Information"
				Case 5 'Photo
					function_gen_optimize_string_golf = strProduct & " Pictures"
				Case 6 'Review
					function_gen_optimize_string_golf = strProduct & " Reviews"
				Case 7 'Why
					function_gen_optimize_string_golf = strProduct & " : Why Choose Hotels2Thailand.com ?"
				Case 8 'Contact
				function_gen_optimize_string_golf = strProduct & " Contact"
			END SELECT
			
		Case 4 'Link Back To Home
			SELECT CASE intPageType
				Case 1 'Destination
					function_gen_optimize_string_golf = strDestination & " Golf Courses Thailand Golf Course"
				Case 2 'Location
					function_gen_optimize_string_golf = strLocation & " " & strDestination & " Golf Course"
				Case 3 'Detail
					function_gen_optimize_string_golf = strProduct & " ," & strDestination & " Golf Courses"
				Case 4 'Information
					function_gen_optimize_string_golf = strProduct & " , " & strDestination & " Golf Course"
				Case 5 'Photo
					function_gen_optimize_string_golf = strProduct & " , " & strDestination & " Golf Courses"
				Case 6 'Review
					function_gen_optimize_string_golf = strProduct & " , " & strDestination & " Golf Course"
				Case 7 'Why
					function_gen_optimize_string_golf = strProduct & " , " & strDestination & "  Golf Courses"
				Case 8 'Contact
					function_gen_optimize_string_golf = strProduct & " , " & strDestination & " Thailand Golf"
			END SELECT
			
	END SELECT

END FUNCTION
%>