<%
FUNCTION function_gen_optimize_string_show_event(intDestination,intLocation,strDestination,strKeyword,intProduct,strProduct,intPageType,intStringType)

	SELECT CASE intStringType
	
		CASE 1 'Meta
			SELECT CASE intPageType
				Case 1 'Destination
				
					SELECT CASE intDestination
						Case 30 'Bangkok
							function_gen_optimize_string_show_event ="<meta name=""description"" content=""Bangkok Shows & Events : Siam Niramit, Joe Louis theater, Thai Boxing, Booking All Shows & Events in Bangkok"">" & VbCrlf
							function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content=""bangkok shows, bangkok events, siam niramit, joe louis theater, thai boxing"">" & VbCrlf
						Case 31 'Phuket
							function_gen_optimize_string_show_event ="<meta name=""description"" content=""Phuket Shows & Events : Phuket Fantasea, Thai Boxing, Booking All Shows & Events in Phuket"">" & VbCrlf
							function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content=""phuket shows, phuket events, phuket fantasea, thai boxing"">" & VbCrlf
						Case 33 'Pattaya
							function_gen_optimize_string_show_event ="<meta name=""description"" content=""Pattaya Shows & Events : Tiffany Show, Pattaya Alcazar Show, Alangkarn Pattaya, Booking All Shows & Events in Pattaya"">" & VbCrlf
							function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content=""pattaya shows, pattaya events, tiffany show, pattaya alcazar show, alangkarn pattaya"">" & VbCrlf
						Case Else
							function_gen_optimize_string_show_event ="<meta name=""description"" content="""&strDestination&" Shows & Events Booking All Shows & Events in Phuket "&strDestination&" with lower price."">" & VbCrlf
							function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content="""&strDestination&" shows,events "&strDestination&", trip in "&strDestination&" Thailand"">" & VbCrlf
					END SELECT

				Case 2 'Location
					
				Case 3 'Detail
					function_gen_optimize_string_show_event ="<meta name=""description"" content="""&StrProduct&" : "&strDestination & " Shows & Events, Online Booking With Special Low Price"">" & VbCrlf
					function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content="""&strProduct&", "&strDestination&" Shows & Events, "& strKeyword &""">" & VbCrlf
				
				Case 4 'Information
					function_gen_optimize_string_show_event ="<meta name=""description"" content="""&strProduct&" Information : "&strDestination&" Shows & Events "">" & VbCrlf
					function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content="""&strProduct&" Information, "& strKeyword &""">" & VbCrlf
				
				Case 5 'Photo
					function_gen_optimize_string_show_event ="<meta name=""description"" content="""&strProduct&" Photo : "&strDestination&" Shows & Events "">" & VbCrlf
					function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content="""&strProduct&" pictures, "& strKeyword &""">" & VbCrlf
					
				Case 6 'Review
					function_gen_optimize_string_show_event ="<meta name=""description"" content="""&strProduct&" Reviews : "&strDestination&" Shows & Events "">" & VbCrlf
					function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content="""&strProduct&" reviews, "& strKeyword &""">" & VbCrlf
					
				Case 7 'Why
					function_gen_optimize_string_show_event ="<meta name=""description"" content="""&strProduct&" Reservation : "&strDestination&" Shows & Events"">" & VbCrlf
					function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content="""&strProduct&" sightseeing, "& strKeyword &""">" & VbCrlf
					
				Case 8 'Contact
					function_gen_optimize_string_show_event ="<meta name=""description"" content="""&strProduct&" Contact "&strDestination&" Shows & Events"">" & VbCrlf
					function_gen_optimize_string_show_event = function_gen_optimize_string_show_event  & "<meta name=""keywords"" content="""&strProduct&" contact, "& strKeyword &""">" & VbCrlf
			END SELECT
			
		Case 2 'Title
			SELECT CASE intPageType
			
				Case 1 'Destination
					SELECT CASE intDestination
						Case 30 'Bangkok
							function_gen_optimize_string_show_event = "Bangkok Shows & Events : Siam Niramit, Joe Louis Theater, Thai Boxing All Shows & Events in Bangkok"
						Case 31 'Phuket
							function_gen_optimize_string_show_event = "Phuket Shows & Events : Phuket Fantasea, Thai Boxing, Booking and All Shows & Events in Phuket"
						Case 36 'Pattaya
							function_gen_optimize_string_show_event = "Pattaya Shows & Events : Tiffany Show, Pattaya Alcazar Show, Alangkarn Pattaya and All Shows & Events in Pattaya"
						Case Else
							function_gen_optimize_string_show_event = strDestination & " Shows & Events Booking All Shows & Events in " & strDestination
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_show_event = strProduct & " : " & strDestination
				Case 4 'Information
					function_gen_optimize_string_show_event = strProduct & "  Information : " & strDestination
				Case 5 'Photo
					function_gen_optimize_string_show_event = strProduct & " Pictures : " & strDestination
				Case 6 'Review
					function_gen_optimize_string_show_event = strProduct & " Reviews : " & strDestination
				Case 7 'Why
					function_gen_optimize_string_show_event = strProduct & " Why Choose Hotels2Thailand.com? : " & strDestination 
				Case 8 'Contact
					function_gen_optimize_string_show_event = strProduct & " Contact Us : " & strDestination
			END SELECT
			
		Case 3 'H1
			SELECT CASE intPageType
				Case 1 'Destination
					SELECT CASE intDestination
						Case 30 'Bangkok
							function_gen_optimize_string_show_event = strDestination & " Shows & Events" & VbCrlf
						Case 31 'Phuket
							function_gen_optimize_string_show_event = strDestination & " Shows & Events" & VbCrlf
						Case Else
							function_gen_optimize_string_show_event = strDestination & " Shows & Events" & VbCrlf
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_show_event = strProduct
				Case 4 'Information
					function_gen_optimize_string_show_event = strProduct & " Information"
				Case 5 'Photo
					function_gen_optimize_string_show_event = strProduct & " Pictures"
				Case 6 'Review
					function_gen_optimize_string_show_event = strProduct & " Reviews"
				Case 7 'Why
					function_gen_optimize_string_show_event = strProduct & " : Why Choose Hotels2Thailand.com ?"
				Case 8 'Contact
				function_gen_optimize_string_show_event = strProduct & " Contact"
			END SELECT
			
		Case 4 'Link Back To Home
			SELECT CASE intPageType
				Case 1 'Destination
					SELECT CASE intDestination
						Case 30 'Bangkok
							function_gen_optimize_string_show_event = strDestination & " Shows & Events" & VbCrlf
						Case 31 'Phuket
							function_gen_optimize_string_show_event = strDestination & " Shows & Events" & VbCrlf
						Case 36 'Pattaya
							function_gen_optimize_string_show_event = strDestination & " Shows & Events" & VbCrlf
						Case Else
							function_gen_optimize_string_show_event = strDestination & " Shows & Events" & VbCrlf
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_show_event = strProduct & " : " & strDestination & " Shows & Events"
				Case 4 'Information
					function_gen_optimize_string_show_event = strProduct & "  Information : " & strDestination & " Shows & Events"
				Case 5 'Photo
					function_gen_optimize_string_show_event = strProduct & " Pictures : " & strDestination & " Shows & Events"
				Case 6 'Review
					function_gen_optimize_string_show_event = strProduct & " Reviews : " & strDestination & " Shows & Events"
				Case 7 'Why
					function_gen_optimize_string_show_event = strProduct & " Why Choose Hotels2Thailand.com? : " & strDestination & " Shows & Events"
				Case 8 'Contact
					function_gen_optimize_string_show_event = strProduct & " Contact Us : " & strDestination & " Shows & Events"
			END SELECT
			
	END SELECT

END FUNCTION
%>