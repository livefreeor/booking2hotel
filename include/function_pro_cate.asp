<%	
FUNCTION function_pro_cate(strCate,int(intDest),intCate)
	Select int(intCate)
		Case 1
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
		Case 2
			Select Case strCate
				Case "29" 'hotel
					function_pro_cate = "thailand-hotels-pic"
				Case "32" 'golf course
					function_pro_cate = "thailand-golf-pic"
				Case "34" 'day trip
					function_pro_cate = "thailand-day-trips-pic"
				Case "36" 'water activity
					function_pro_cate = "thailand-water-activity-pic"
				Case "38" 'show and event
					function_pro_cate = "thailand-show-event-pic"
				Case "39" 'health check up
					function_pro_cate = "thailand-health-pic"
			End Select	'	End strCate
	End Select 	'	End intCate		
END FUNCTION
%>