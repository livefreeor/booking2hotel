<%
FUNCTION function_gen_optimize_string_water_activity(intDestination,intLocation,strDestination,strKeyword,intProduct,strProduct,intPageType,intStringType)

	SELECT CASE intStringType
	
		CASE 1 'Meta
			SELECT CASE intPageType
				Case 1 'Destination
				
					SELECT CASE intDestination
						Case 31 'Phuket
							function_gen_optimize_string_water_activity ="<meta name=""description"" content=""Phuket Water Activities : Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking"">" & VbCrlf
							function_gen_optimize_string_water_activity = function_gen_optimize_string_water_activity  & "<meta name=""keywords"" content=""phukt tours, phuket, diving, canoe, kayak, boat trips, snorkeling, fishing, rafting"">" & VbCrlf
						Case Else
							function_gen_optimize_string_water_activity ="<meta name=""description"" content="""&strDestination&" Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking"">" & VbCrlf
							function_gen_optimize_string_water_activity = function_gen_optimize_string_water_activity  & "<meta name=""keywords"" content="""&strDestination&" tours, "&strDestination&", diving, canoe, kayak, boat trips, snorkeling, fishing, rafting"">" & VbCrlf
					END SELECT

				Case 2 'Location
					
				Case 3 'Detail
					function_gen_optimize_string_water_activity ="<meta name=""description"" content="""&StrProduct&" : "&strDestination & " Tours, Water Activities, Vacation Package, Online Booking With Special Low Price"">" & VbCrlf
					function_gen_optimize_string_water_activity = function_gen_optimize_string_water_activity  & "<meta name=""keywords"" content="""&strProduct&", "&strDestination&" Water Activities, "& strKeyword &""">" & VbCrlf
				
				Case 4 'Information
					function_gen_optimize_string_water_activity ="<meta name=""description"" content="""&strProduct&" Information : "&strDestination&" Water Activities "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_water_activity = function_gen_optimize_string_water_activity  & "<meta name=""keywords"" content="""&strProduct&" Information, "& strKeyword &""">" & VbCrlf
				
				Case 5 'Photo
					function_gen_optimize_string_water_activity ="<meta name=""description"" content="""&strProduct&" Photo : "&strDestination&" Water Activities "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_water_activity = function_gen_optimize_string_water_activity  & "<meta name=""keywords"" content="""&strProduct&" pictures, "& strKeyword &""">" & VbCrlf
					
				Case 6 'Review
					function_gen_optimize_string_water_activity ="<meta name=""description"" content="""&strProduct&" Reviews : "&strDestination&" Water Activities "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_water_activity = function_gen_optimize_string_water_activity  & "<meta name=""keywords"" content="""&strProduct&" reviews, "& strKeyword &""">" & VbCrlf
					
				Case 7 'Why
					function_gen_optimize_string_water_activity ="<meta name=""description"" content="""&strProduct&" Reservation : "&strDestination&" Water Activities "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_water_activity = function_gen_optimize_string_water_activity  & "<meta name=""keywords"" content="""&strProduct&" water activities, "& strKeyword &""">" & VbCrlf
					
				Case 8 'Contact
					function_gen_optimize_string_water_activity ="<meta name=""description"" content="""&strProduct&" Contact "&strDestination&" Water Activities "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_water_activity = function_gen_optimize_string_water_activity  & "<meta name=""keywords"" content="""&strProduct&" contact, "& strKeyword &""">" & VbCrlf
			END SELECT
			
		Case 2 'Title
			SELECT CASE intPageType
			
				Case 1 'Destination
					SELECT CASE intDestination
						Case 31 'Phuket
							function_gen_optimize_string_water_activity = "Phuket Water Activities : Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking"
						Case Else
							function_gen_optimize_string_water_activity = strDestination & " Water Activities : Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking"
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_water_activity = strProduct & " : " & strDestination & " Water Activities, Package Tours"
				Case 4 'Information
					function_gen_optimize_string_water_activity = strProduct & "  Information : " & strDestination & " Water Activities, Attraction, Package Tours"
				Case 5 'Photo
					function_gen_optimize_string_water_activity = strProduct & " Pictures : " & strDestination & " Water Activities, Attraction, Package Tours"
				Case 6 'Review
					function_gen_optimize_string_water_activity = strProduct & " Reviews : " & strDestination & " Water Activities, Attraction, Package Tours"
				Case 7 'Why
					function_gen_optimize_string_water_activity = strProduct & " Why Choose Hotels2Thailand.com? : " & strDestination & " Water Activities, Attraction, Package Tours"
				Case 8 'Contact
					function_gen_optimize_string_water_activity = strProduct & " Contact Us : " & strDestination & " Water Activities, Attraction, Package Tours"
			END SELECT
			
		Case 3 'H1
			SELECT CASE intPageType
				Case 1 'Destination
					SELECT CASE intDestination
						Case 31 'Phuket
							function_gen_optimize_string_water_activity = strDestination & " Tours, Water Activities" & VbCrlf
						Case Else
							function_gen_optimize_string_water_activity = strDestination & " Tours, Water Activities" & VbCrlf
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_water_activity = strProduct
				Case 4 'Information
					function_gen_optimize_string_water_activity = strProduct & " Information"
				Case 5 'Photo
					function_gen_optimize_string_water_activity = strProduct & " Pictures"
				Case 6 'Review
					function_gen_optimize_string_water_activity = strProduct & " Reviews"
				Case 7 'Why
					function_gen_optimize_string_water_activity = strProduct & " : Why Choose Hotels2Thailand.com ?"
				Case 8 'Contact
				function_gen_optimize_string_water_activity = strProduct & " Contact"
			END SELECT
			
		Case 4 'Link Back To Home
			SELECT CASE intPageType
				Case 1 'Destination
					SELECT CASE intDestination
						Case 31 'Phuket
							function_gen_optimize_string_water_activity = strDestination & " Tours, Water Activities, Day Trips, Attractions" & VbCrlf
						Case Else
							function_gen_optimize_string_water_activity = strDestination & " Tours, Water Activities, Day Trips, Attractions" & VbCrlf
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_water_activity = strProduct & " : " & strDestination & " Water Activities Attraction Package Tours"
				Case 4 'Information
					function_gen_optimize_string_water_activity = strProduct & "  Information : " & strDestination & " Water Activities, Attraction, Package Tours"
				Case 5 'Photo
					function_gen_optimize_string_water_activity = strProduct & " Pictures : " & strDestination & " Water Activities, Attraction, Package Tours"
				Case 6 'Review
					function_gen_optimize_string_water_activity = strProduct & " Reviews : " & strDestination & " Water Activities, Attraction, Package Tours"
				Case 7 'Why
					function_gen_optimize_string_water_activity = strProduct & " Why Choose Hotels2Thailand.com? : " & strDestination & " Water Activities, Attraction, Package Tours"
				Case 8 'Contact
					function_gen_optimize_string_water_activity = strProduct & " Contact Us : " & strDestination & " Water Activities, Attraction, Package Tours"
			END SELECT
			
	END SELECT

END FUNCTION
%>