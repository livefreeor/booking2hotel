<%
FUNCTION function_gen_optimize_string_health_check_up(intDestination,intLocation,strDestination,strKeyword,intProduct,strProduct,intPageType,intStringType)

	SELECT CASE intStringType
	
		CASE 1 'Meta
			SELECT CASE intPageType
				Case 1 'Destination
						function_gen_optimize_string_health_check_up ="<meta name=""description"" content="""&strDestination&" Health Check Up & Medical Tour "&strDestination&" Hospitals"">" & VbCrlf
						function_gen_optimize_string_health_check_up = function_gen_optimize_string_health_check_up  & "<meta name=""keywords"" content="""&strDestination&" health check up,hospital "&strDestination&", medical tour in "&strDestination&" Thailand"">" & VbCrlf

				Case 2 'Location
					
				Case 3 'Detail
					function_gen_optimize_string_health_check_up ="<meta name=""description"" content="""&StrProduct&" : "&strDestination & " Health Check Up"">" & VbCrlf
					function_gen_optimize_string_health_check_up = function_gen_optimize_string_health_check_up  & "<meta name=""keywords"" content="""&strProduct&", "&strDestination&" health chck up, "& strKeyword &""">" & VbCrlf
				
				Case 4 'Information
					function_gen_optimize_string_health_check_up ="<meta name=""description"" content="""&strProduct&" Information : "&strDestination&" health "">" & VbCrlf
					function_gen_optimize_string_health_check_up = function_gen_optimize_string_health_check_up  & "<meta name=""keywords"" content="""&strProduct&" health, "& strKeyword &""">" & VbCrlf
				
				Case 5 'Photo
					function_gen_optimize_string_health_check_up ="<meta name=""description"" content="""&strProduct&" Photo : "&strDestination&" medical tour "">" & VbCrlf
					function_gen_optimize_string_health_check_up = function_gen_optimize_string_health_check_up  & "<meta name=""keywords"" content="""&strProduct&" pictures, "& strKeyword &""">" & VbCrlf
					
				Case 6 'Review
					function_gen_optimize_string_health_check_up ="<meta name=""description"" content="""&strProduct&" Reviews : "&strDestination&" hospitals "">" & VbCrlf
					function_gen_optimize_string_health_check_up = function_gen_optimize_string_health_check_up  & "<meta name=""keywords"" content="""&strProduct&" reviews, "& strKeyword &""">" & VbCrlf
					
				Case 7 'Why
					function_gen_optimize_string_health_check_up ="<meta name=""description"" content="""&strProduct&" Reservation : "&strDestination&" medical tour"">" & VbCrlf
					function_gen_optimize_string_health_check_up = function_gen_optimize_string_health_check_up  & "<meta name=""keywords"" content="""&strProduct&" medical tour, "& strKeyword &""">" & VbCrlf
					
				Case 8 'Contact
					function_gen_optimize_string_health_check_up ="<meta name=""description"" content="""&strProduct&" Contact "&strDestination&" health check up"">" & VbCrlf
					function_gen_optimize_string_health_check_up = function_gen_optimize_string_health_check_up  & "<meta name=""keywords"" content="""&strProduct&" contact, "& strKeyword &""">" & VbCrlf
			END SELECT
			
		Case 2 'Title
			SELECT CASE intPageType
			
				Case 1 'Destination
						function_gen_optimize_string_health_check_up = strDestination & " Health Check Up & Medical Tour & " & strDestination & " Hospitals"
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_health_check_up = strProduct & " : " & strDestination
				Case 4 'Information
					function_gen_optimize_string_health_check_up = strProduct & "  Information : " & strDestination
				Case 5 'Photo
					function_gen_optimize_string_health_check_up = strProduct & " Pictures : " & strDestination
				Case 6 'Review
					function_gen_optimize_string_health_check_up = strProduct & " Reviews : " & strDestination
				Case 7 'Why
					function_gen_optimize_string_health_check_up = strProduct & " Why Choose Hotels2Thailand.com? : " & strDestination 
				Case 8 'Contact
					function_gen_optimize_string_health_check_up = strProduct & " Contact Us : " & strDestination
			END SELECT
			
		Case 3 'H1
			SELECT CASE intPageType
				Case 1 'Destination
						function_gen_optimize_string_health_check_up = strDestination & " Health Check Up & Medical Tour & " & strDestination & " Hospitals" & VbCrlf
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_health_check_up = strProduct
				Case 4 'Information
					function_gen_optimize_string_health_check_up = strProduct & " Information"
				Case 5 'Photo
					function_gen_optimize_string_health_check_up = strProduct & " Pictures"
				Case 6 'Review
					function_gen_optimize_string_health_check_up = strProduct & " Reviews"
				Case 7 'Why
					function_gen_optimize_string_health_check_up = strProduct & " : Why Choose Hotels2Thailand.com ?"
				Case 8 'Contact
				function_gen_optimize_string_health_check_up = strProduct & " Contact"
			END SELECT
			
		Case 4 'Link Back To Home
			SELECT CASE intPageType
				Case 1 'Destination
						function_gen_optimize_string_health_check_up = strDestination & " Shows & Events" & VbCrlf
					
				Case 2 'Location

				Case 3 'Detail
					function_gen_optimize_string_health_check_up = strProduct & " : " & strDestination & " Health Check Up & Medical Tour & Hospitals"
				Case 4 'Information
					function_gen_optimize_string_health_check_up = strProduct & "  Information : " & strDestination & " Health Check Up & Medical Tour & Hospitals"
				Case 5 'Photo
					function_gen_optimize_string_health_check_up = strProduct & " Pictures : " & strDestination & " Health Check Up & Medical Tour & Hospitals"
				Case 6 'Review
					function_gen_optimize_string_health_check_up = strProduct & " Reviews : " & strDestination & " Health Check Up & Medical Tour & Hospitals"
				Case 7 'Why
					function_gen_optimize_string_health_check_up = strProduct & " Why Choose Hotels2Thailand.com? : " & strDestination & " Health Check Up & Medical Tour & Hospitals"
				Case 8 'Contact
					function_gen_optimize_string_health_check_up = strProduct & " Contact Us : " & strDestination & " Health Check Up & Medical Tour & Hospitals"
			END SELECT
			
	END SELECT

END FUNCTION
%>