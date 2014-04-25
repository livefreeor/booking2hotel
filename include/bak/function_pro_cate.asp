<%	
FUNCTION function_pro_cate(strCate,intDest)
			Select Case strCate ' product_category
				Case "29" 'hotel
					function_pro_cate = function_generate_hotel_link(intDest,"",5)
				Case "32" 'golf course
					function_pro_cate = function_generate_golf_link(intDest,"",5)
				Case "34" 'day trip
					function_pro_cate = function_generate_sightseeing_link(intDest,"",5)
				Case "36" 'water activity
					function_pro_cate = function_generate_water_activity_link(intDest,"",5)
				Case "38" 'show and event
					function_pro_cate = function_generate_show_event_link(intDest,"",5)
				Case "39" 'health check up
					function_pro_cate = function_generate_health_checkk_up_link(intDest,"",5)
			End Select	'	End strCate
END FUNCTION
%>