<%
Function fnFacilitiesIcon (intFcID, intType)
	
	IF intType=1 Then 'Optioin Feature
		SELECT CASE intFcID
			Case 33 '33	Air Conditioned
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/airconditioned.gif"">"
			Case 34 '34	Security Safe
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/securitysafe.gif"">"
			Case 35 '35	IDD/Direct Dial Line
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/idddirectdialline.gif"">"
			Case 36 '36	Television
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/television.gif"">"
			Case 37 '37	Minibar/Fridge
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/minibarfridge.gif"">"
			Case 38 '38	Hot Water
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
			Case 39 '39	Fax
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/personalfax.gif"">"
			Case 43 '43	Coffee/Tea
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/coffeetea.gif"">"
			Case 44 '44	Security Keycard
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/securitykeycard.gif"">"
			Case 45 '45	Telephone Vioce Mail
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/telephonevoicemail.gif"">"
			Case 46 '46	Refrigerator
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
			Case 47 '47	Extra Bed / Pax
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
			Case 48 '48	Breakfast
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
			Case 49 '49	Airport Transfer
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
			Case 50 '50	Kitchenette
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
			Case 51 '51	Fan
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
			Case 52 '52 Internet Access
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/internetaccess.gif"">"
			Case 53 '53	Personal Computer
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
		END SELECT
	END IF
	
	IF intType=2 Then 'Product Feature
		SELECT CASE intFcID
			Case 36 '36	Internet Access
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/internetaccess.gif"">"
			Case 37 '37	Beauty Salon/Barber
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/beautysalonbarber.gif"">"
			Case 38 '38	Conference Facilities
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/conferencefacilities.gif"">"
			Case 39 '39	Fitness Centre
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/fitnesscentre.gif"">"
			Case 40 '40	Room Services
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/roomservices.gif"">"
			Case 41 '41	Banquet
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/banquet.gif"">"
			Case 42 '42	Business Centre
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/businesscentre.gif"">"
			Case 43 '43	Executive Floor
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/executivefloor.gif"">"
			Case 44 '44	Non-smoking Rooms
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/nonsmokingrooms.gif"">"
			Case 45 '45	Shopping
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/shopping.gif"">"
			Case 46 '46	Bar/Pub/Lounge
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/barpublounge.gif"">"
			Case 47 '47	Coffeeshop
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/coffeeshop.gif"">"
			Case 48 '48	Facilities for Disabled
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/facilitiesfordisabled.gif"">"
			Case 49 '49	Restaurant
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/restaurant.gif"">"
			Case 50 '50	Swimming Pool
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/swimmingpool.gif"">"
			Case 51 '51	Airport Transfers
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/airporttransfers.gif"">"
			Case 52 '52	Laundry
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/laundry.gif"">"
			Case 53 '53	Doctor
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/doctor.gif"">"
			Case 54 '54	Tour Desk
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tourdesk.gif"">"
			Case 55 '55	Baby sitting
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/babysitting.gif"">"
			Case 56 '56	Banquet
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/banquet.gif"">"
			Case 57 '57	Spa
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/spa.gif"">"
			Case 60 '60	Tennis
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tennis.gif"">"
			Case 61'61	Children Playground
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/childrenplayground.gif"">"
			Case 62 '62	Children Pool
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/childrenpool.gif"">"
			Case 63 '63	Golf
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/golf.gif"">"
			Case 64 '64	Beach Front
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/beach.gif"">"
			Case 65 '65	Sea View Room
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/seaviewrooms.gif"">"
			Case 67 '67	Balcony Rooms
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/balconyrooms.gif"">"
			Case 68 '68	Sauna
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tran.gif"">"
			Case 69 '69	City Centre
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/citycentre.gif"">"
			Case 78 '78	Reception Desk
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/roomservices.gif"">"
			Case 79 '79	Safety Box
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/securitysafe.gif"">"
			Case 80 '80	Tour & Excursion Information
				fnFacilitiesIcon = "<img src=""../image/hotels-detail-feature/tourdesk.gif"">"
			END SELECT
	End IF

	IF intType=3 Then 'Near By
		SELECT CASE intFcID
			Case 2 '2	Business Center
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/financialarea.gif"">"
			Case 3 '3	Airport
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/airport.gif"">"
			Case 4 '4	BTS Station
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/tran.gif"">"
			Case 5 '5	Public Transportation
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/publictransportation.gif"">"
			Case 6 '6	Shopping Arcade
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/ShoppingArcade.gif"">"
			Case 7 '7	Tourist Site
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/touristsite.gif"">"
			Case 8 '8	Golf Course
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/golf.gif"">"
			Case 9 '9	Health Massage
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/tran.gif"">"
			Case 10 '10	Golf Course
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/golf.gif"">"
			Case 11'11	National Convention Centre
				fnFacilitiesIcon = "<img src=""image/hotels-detail-feature/nationalconventioncentre.gif"">"
		END SELECT
	END IF
End Function
%>