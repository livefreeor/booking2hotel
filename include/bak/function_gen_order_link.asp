<%
FUNCTION function_gen_order_link(intOrderID,intProductcatID,intOrderProductID,intOrderItemID,intType)

	Dim strFile

	IF int(intOrderID)>=67316 Then
	'IF int(intOrderID)>=57988 Then
		SELECT CASE int(intProductCatID)
			Case 29 'Hotel
				SELECT CASE intType
					Case 1 'Booking
						strFile = "/dual/print_booking_hotel.asp?id=" & intOrderProductID
					Case 2 'Voucher For Supplier
						strFile = "/voucher_hotel.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
					Case 3 'Voucher For Customer
					strFile = "/voucher_hotel.asp"
				END SELECT
			
			Case 30 'Air Tricket
				SELECT CASE intType
					Case 1 'Booking
					Case 2 'Voucher For Supplier
					Case 3 'Voucher For Customer
				END SELECT
				
			Case 31 'Airport Transfer
				SELECT CASE intType
					Case 1 'Booking
						strFile = "/dual/print_booking_trans.asp?id=" & intOrderProductID
					Case 2 'Voucher For Supplier
						strFile = "/voucher_trans.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
					Case 3 'Voucher For Customer
						strFile = "/voucher_trans.asp"
				END SELECT
				
			Case 32 'Golf Courses
				SELECT CASE intType
					Case 1 'Booking
						strFile = "/dual/print_booking_golf.asp?id=" & intOrderProductID
					Case 2 'Voucher For Supplier
						strFile = "/voucher_golf.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
					Case 3 'Voucher For Customer
						strFile = "/voucher_golf.asp"
				END SELECT
				
			Case 33 'Tour Package
				SELECT CASE intType
					Case 1 'Booking
					Case 2 'Voucher For Supplier
					Case 3 'Voucher For Customer
				END SELECT
				
			Case 34 'Sightseeing
				SELECT CASE intType
					Case 1 'Booking
						strFile = "/dual/print_booking_sight.asp?id=" & intOrderProductID
					Case 2 'Voucher For Supplier
						strFile = "/voucher_sight.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
					Case 3 'Voucher For Customer
						strFile = "/voucher_sight.asp"
				END SELECT
				
			Case 35 'Adventure
				SELECT CASE intType
					Case 1 'Booking
					Case 2 'Voucher For Supplier
					Case 3 'Voucher For Customer
				END SELECT
				
			Case 36 'Water Activity
				SELECT CASE intType
					Case 1 'Booking
						strFile = "/dual/print_booking_water_activity.asp?id=" & intOrderProductID
					Case 2 'Voucher For Supplier
						strFile = "/voucher_water_activity.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
					Case 3 'Voucher For Customer
						strFile = "/voucher_water_activity.asp"
				END SELECT
				
			Case 37 'Restaurant
				SELECT CASE intType
					Case 1 'Booking
					Case 2 'Voucher For Supplier
					Case 3 'Voucher For Customer
				END SELECT
				
			Case 38 'Show
				SELECT CASE intType
					Case 1 'Booking
						strFile = "/dual/print_booking_show_event.asp?id=" & intOrderProductID
					Case 2 'Voucher For Supplier
						strFile = "/voucher_show_event.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
					Case 3 'Voucher For Customer
						strFile = "voucher_show_event.asp"
				END SELECT
			Case 39 'Health Check Up
				SELECT CASE intType
					Case 1 'Booking
						strFile = "/dual/print_booking_health_check_up.asp?id=" & intOrderProductID
					Case 2 'Voucher For Supplier
						strFile = "/voucher_health_check_up.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
					Case 3 'Voucher For Customer
						strFile = "voucher_health_check_up.asp"
				END SELECT	
			Case 40 'Spa
				SELECT CASE intType
					Case 1 'Booking
						strFile = "/dual/print_booking_spa.asp?id=" & intOrderProductID
					Case 2 'Voucher For Supplier
						strFile = "/voucher_spa.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
					Case 3 'Voucher For Customer
						strFile = "voucher_spa.asp"
				END SELECT	
			Case 999 'Temp
		END SELECT
		
	Else
		SELECT CASE intType
			Case 1 'Booking
				strFile = "/dual/print_booking.asp?id=" & intOrderProductID
			Case 2 'Voucher For Supplier
				strFile = "/voucher_ht2th.asp?pr=xxxxx" & function_add_zero(intOrderProductID, 5) 
			Case 3 'Voucher For Customer
				strFile = "/voucher_ht2th.asp"
		END SELECT
	End IF
	
	function_gen_order_link = strFile
	
END FUNCTION
%>