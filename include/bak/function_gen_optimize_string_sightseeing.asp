<%
FUNCTION function_gen_optimize_string_sightseeing(intDestination,intLocation,strDestination,strKeyword,intProduct,strProduct,intPageType,intStringType)

	SELECT CASE intStringType
	
		CASE 1 'Meta
			SELECT CASE intPageType
				Case 1 'Destination
				
					SELECT CASE intDestination
						Case 30 'Bangkok
							function_gen_optimize_string_sightseeing ="<meta name=""description"" content=""Bangkok Tours, Day Trips, Vacation Package, NightLife, Floating Market, Grand Palace, Wat Arun, Wat Pho, Sightseeing and All Activities and Attractions in Bangkok"">" & VbCrlf
							function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content=""bangkok tours, bangkok day trips,bangkok vacation, bangkok night life, grand palace, floating market, weekend market, wat arun, wat pho, bangkok sight seeing, bangkok attraction"">" & VbCrlf
						Case 31 'Phuket
							function_gen_optimize_string_sightseeing ="<meta name=""description"" content=""Phuket Tours, Day Trips,Diving, Vacation, Trip, NightLife, Spa, Masaage, Speed Boat, Canoe, Kayak and All Activities and Attractions in Phuket"">" & VbCrlf
							function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content=""phukt tours, phuket day trips, phuket vacation, phukettrip, phuket nightlife, phuket spa, phuket massage, phuket speed boat, phuket conoe, phuket kayak, phuket water activities"">" & VbCrlf
						Case Else
							function_gen_optimize_string_sightseeing ="<meta name=""description"" content="""&strDestination&" daytrips, package tours, sightseeing "&strDestination&" and all activities in "&strDestination&" with lower price."">" & VbCrlf
							function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content="""&strDestination&" sightseeing, package tours in "&strDestination&", trip in "&strDestination&" Thailand"">" & VbCrlf
					END SELECT

				Case 2 'Location
					
				Case 3 'Detail
					function_gen_optimize_string_sightseeing ="<meta name=""description"" content="""&StrProduct&" : "&strDestination & " Tours, Day Trips, Vacation Package, Online Booking With Special Low Price"">" & VbCrlf
					function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content="""&strProduct&", "&strDestination&" sightseeing, "& strKeyword &""">" & VbCrlf
				
				Case 4 'Information
					function_gen_optimize_string_sightseeing ="<meta name=""description"" content="""&strProduct&" Information : "&strDestination&" Day Trips "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content="""&strProduct&" Information, "& strKeyword &""">" & VbCrlf
				
				Case 5 'Photo
					function_gen_optimize_string_sightseeing ="<meta name=""description"" content="""&strProduct&" Photo : "&strDestination&" Day Trips "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content="""&strProduct&" pictures, "& strKeyword &""">" & VbCrlf
					
				Case 6 'Review
					function_gen_optimize_string_sightseeing ="<meta name=""description"" content="""&strProduct&" Reviews : "&strDestination&" Day Trips "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content="""&strProduct&" reviews, "& strKeyword &""">" & VbCrlf
					
				Case 7 'Why
					function_gen_optimize_string_sightseeing ="<meta name=""description"" content="""&strProduct&" Reservation : "&strDestination&" Day Trips "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content="""&strProduct&" sightseeing, "& strKeyword &""">" & VbCrlf
					
				Case 8 'Contact
					function_gen_optimize_string_sightseeing ="<meta name=""description"" content="""&strProduct&" Contact "&strDestination&" Day Trips "&strDestination&" Attraction"">" & VbCrlf
					function_gen_optimize_string_sightseeing = function_gen_optimize_string_sightseeing  & "<meta name=""keywords"" content="""&strProduct&" contact, "& strKeyword &""">" & VbCrlf
			END SELECT
			
		Case 2 'Title
			SELECT CASE intPageType
			
				Case 1 'Destination
					SELECT CASE intDestination
						Case 30 'Bangkok
							function_gen_optimize_string_sightseeing = "Bangkok Tours, Day Trips, Attractions, Nightlife, Shopping, Dining, Gran Palace, Wat Pho, Wat Arun and All Activities in Bangkok"
						Case 31 'Phuket
							function_gen_optimize_string_sightseeing = "Phuket Tours, Day Trips, Attractions, Nightlife, Shopping, Dining, Diving, Spa, Canoe, Kayak and All Activities in Phuket"
						Case Else
							function_gen_optimize_string_sightseeing = strDestination & " Tours, Day Trips, Attractions, Nightlife, Shopping, Dining"
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_sightseeing = strProduct & " : " & strDestination & " Day Trips, Attraction, Package Tours"
				Case 4 'Information
					function_gen_optimize_string_sightseeing = strProduct & "  Information : " & strDestination & " Day Trips, Attraction, Package Tours"
				Case 5 'Photo
					function_gen_optimize_string_sightseeing = strProduct & " Pictures : " & strDestination & " Day Trips, Attraction, Package Tours"
				Case 6 'Review
					function_gen_optimize_string_sightseeing = strProduct & " Reviews : " & strDestination & " Day Trips, Attraction, Package Tours"
				Case 7 'Why
					function_gen_optimize_string_sightseeing = strProduct & " Why Choose Hotels2Thailand.com? : " & strDestination & " Day Trips, Attraction, Package Tours"
				Case 8 'Contact
					function_gen_optimize_string_sightseeing = strProduct & " Contact Us : " & strDestination & " Day Trips, Attraction, Package Tours"
			END SELECT
			
		Case 3 'H1
			SELECT CASE intPageType
				Case 1 'Destination
					SELECT CASE intDestination
						Case 30 'Bangkok
							function_gen_optimize_string_sightseeing = strDestination & " Tours, Day Trips" & VbCrlf
						Case 31 'Phuket
							function_gen_optimize_string_sightseeing = strDestination & " Tours, Day Trips" & VbCrlf
						Case Else
							function_gen_optimize_string_sightseeing = strDestination & " Tours, Day Trips" & VbCrlf
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_sightseeing = strProduct
				Case 4 'Information
					function_gen_optimize_string_sightseeing = strProduct & " Information"
				Case 5 'Photo
					function_gen_optimize_string_sightseeing = strProduct & " Pictures"
				Case 6 'Review
					function_gen_optimize_string_sightseeing = strProduct & " Reviews"
				Case 7 'Why
					function_gen_optimize_string_sightseeing = strProduct & " : Why Choose Hotels2Thailand.com ?"
				Case 8 'Contact
				function_gen_optimize_string_sightseeing = strProduct & " Contact"
			END SELECT
			
		Case 4 'Link Back To Home
			SELECT CASE intPageType
				Case 1 'Destination
					SELECT CASE intDestination
						Case 30 'Bangkok
							function_gen_optimize_string_sightseeing = strDestination & " Tours, Activities, Day Trips, Attractions, Sightseeing" & VbCrlf
						Case 31 'Phuket
							function_gen_optimize_string_sightseeing = strDestination & " Tours, Activities, Day Trips, Attractions, Sightseeing" & VbCrlf
						Case Else
							function_gen_optimize_string_sightseeing = strDestination & " Tours, Activities, Day Trips, Attractions, Sightseeing" & VbCrlf
					END SELECT
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_sightseeing = strProduct & " : " & strDestination & " Sightseeing Attraction Package Tours"
				Case 4 'Information
					function_gen_optimize_string_sightseeing = strProduct & "  Information : " & strDestination & " Sightseeing, Attraction, Package Tours"
				Case 5 'Photo
					function_gen_optimize_string_sightseeing = strProduct & " Pictures : " & strDestination & " Sightseeing, Attraction, Package Tours"
				Case 6 'Review
					function_gen_optimize_string_sightseeing = strProduct & " Reviews : " & strDestination & " Sightseeing, Attraction, Package Tours"
				Case 7 'Why
					function_gen_optimize_string_sightseeing = strProduct & " Why Choose Hotels2Thailand.com? : " & strDestination & " Sightseeing, Attraction, Package Tours"
				Case 8 'Contact
					function_gen_optimize_string_sightseeing = strProduct & " Contact Us : " & strDestination & " Sightseeing, Attraction, Package Tours"
			END SELECT
			
	END SELECT

END FUNCTION
%>