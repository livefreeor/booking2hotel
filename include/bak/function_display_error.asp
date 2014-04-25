<%
FUNCTION function_display_error(strError)

	Dim strBody	
	SELECT CASE strError
		Case "error01"
			function_display_error = "Error!, Please Select Your Destination"
		Case "error02"
			function_display_error = "Error!, Please Confirm Your Check In Date"
		Case "error03"
			function_display_error = "Error!, Please Confirm Your Check In Date"
		Case "error04"
			function_display_error = "Error!, Please Confirm Your Check Out Date"
		Case "error05"
			function_display_error = "Error!, Please Confirm Your Check Out Date"
		Case "error06"
			function_display_error = "Error!, Please Confirm Your Check In Date"
		Case "error07"
			function_display_error = "Error!, Please Confirm Your Check Out Date"
		Case "error08"
			function_display_error = "Check out date must be later than check in date"
		Case "error09"
			function_display_error = "Check in date must  be later than current date and"
			'strBody = strBody & "<font color=""#CC0000"">" & VbCrlf
'			strBody = strBody & "Since we will have an annual company trip between 27-29 August, 09. For any booking stay during this mentined period may not be able to be reserved. We will accept only bookings check in from 29 August, 09 afterwards. Our office will be resumed working as usual from 30 August, 2009 onwards.  Please note that all email enquiries will be replied to you immediately on August 30, 2009. " & VbCrlf
'			strBody = strBody & "<p>In case of any emergency case, you can dial to mobile phone, 66-8-91716825 (24 Hours) </p>" & VbCrlf
'			strBody = strBody & "<p>We are so sorry for any inconvenience you experienced and hope that you will come back to use our service on your next trip again.</p>" & VbCrlf
'			strBody = strBody & "</font>" & VbCrlf
'			function_display_error =function_display_error&strBody
		Case "error10"
			function_display_error = "Error!, Please select your room type and quantity"
		Case "error11"
			function_display_error = "Exceed adult, please select extra room or extrabed. <br>Please <a href=""javascript:popup('/thailand-hotels-rooms.asp',350,410)"">click here</a> to see room defination."
		Case "error12"
			function_display_error = "Exceed extrabed policy, please select more room"
		Case "error13"
			function_display_error = "Email Error!, please recheck your email"
		Case "error14"
			function_display_error = "Error!, we don't have rate for your period,<br>Please contactus at <a href=""mailto:support@hotels2thailand.com"">support@hotels2thailand.com</a>"
		Case "error15"
			function_display_error = "Error! Please fill in all required fields"
		Case "error16"
			function_display_error = "Error! Your email and repeat email does not match. Please recheck."
		Case "error17"
			function_display_error = "Exceed airport transfer policy, Please select more car."
		
		Case "error18"
			function_display_error = "Compulsory of X'mas Eve Gala Dinner during your stay has not yet ordered, please kindly order the Gala Dinner in order to complete the reservation."
		Case "error19"
			function_display_error = "Quantity of Compulsory X'mas Eve Gala Dinner during your stay was not ordered according to correct number of people, please kindly order the Gala Dinner equivalent to number of people in order to complete the reservation."
		Case "error20"
			function_display_error =  "Compulsory of X'mas Eve Gala Dinner  for children during your stay has not yet ordered, please kindly order the Gala Dinner for children in order to complete the reservation."
		Case "error21"
			function_display_error ="Quantity of Compulsory X'mas Eve Gala Dinner during your stay was not ordered according to correct number of children, please kindly order the Gala Dinner equivalent to number of children in order to complete the reservation."
			
			
		Case "error22"
			function_display_error = "Compulsory of New Year Gala Dinner during your stay has not yet ordered, please kindly order the Gala Dinner in order to complete the reservation."
		Case "error23"
			function_display_error = "Quantity of Compulsory New Year Gala Dinner during your stay was not ordered according to correct number of people, please kindly order the Gala Dinner equivalent to number of people in order to complete the reservation."
		Case "error24"
			function_display_error =  "Compulsory of New Year Gala Dinner  for children during your stay has not yet ordered, please kindly order the Gala Dinner for children in order to complete the reservation."
		Case "error25"
			function_display_error ="Quantity of CompulsoryNew Year Gala Dinner during your stay was not ordered according to correct number of children, please kindly order the Gala Dinner equivalent to number of children in order to complete the reservation."
		Case "error26"
			function_display_error ="Your order ID is not matched to your email. Please recheck."	
		Case "error27"
			function_display_error ="Our office is being on annual vacation during October 13 - October 15, 2006 for cerebration our 5th Anniversary of Hotels2thailand.com. The reservation system has been uninstalled temporarily to not permit for making the reservation for every booking stay during our day off period.( October 13 - October 15, 2006)."	
		Case "error28"
			function_display_error ="We don't have trip on your date"
		Case "error29"
			function_display_error="Please enter your passport number"
		Case "error30"
			function_display_error="Please complete your name"
		Case "error31"
			function_display_error = "Error!, Please Confirm Your Birth Date"
		Case "error32"
			function_display_error = "Error! Either phone or mobile number is required. Please input your contact number. "
		Case "error33"
			function_display_error = "Error! You entered the wrong code. "

	END SELECT

END FUNCTION

FUNCTION function_display_error_box (strError,intType)

	Dim strAnchor
	Dim strAnchorLink

	SELECT CASE strError
		Case "error01"
			strAnchor = ""
			strAnchorLink = ""
		Case "error02","error03","error04","error05","error06","error07","error08","error09","error14","error27","error28","error34"
			strAnchor = "#date"
			strAnchorLink = "<a name=""date""></a>"
		Case "error10"
			strAnchor = "#room"
			strAnchorLink = "<a name=""room""></a>"
		Case "error11"
			strAnchor = "#people"
			strAnchorLink = "<a name=""people""></a>"
		Case "error12","error17","error18","error19","error20","error21","error22","error23","error24","error25"
			strAnchor = "#option"
			strAnchorLink = "<a name=""option""></a>"
		Case "error15","error16","error31","error32,error33"
			strAnchor = "#detail"
			strAnchorLink = "<a name=""detail""></a>"

	END SELECT
			
	SELECT CASE intType
		Case 1 'Main Error
			function_display_error_box= "<table width=""65%"" border=""0"" cellspacing=""1"" cellpadding=""0"" bgcolor=""#CC0000"">" & VbCrlf
			function_display_error_box = function_display_error_box & "<tr><td bgcolor=""#FFFFF4"" class=""m"" align=""center"" valign=""middle"" height=""30""><font color=""#CC0000""><img src=""/images/ico_warning.gif"" width=""16"" height=""14"" align=""absmiddle""> &nbsp;There is a slight problem with your submission.(<a href="""&strAnchor&""">See below</a>)</font></td>" & VbCrlf
			function_display_error_box = function_display_error_box & "</table>" & VbCrlf
			
		Case 2 'Error Detail
			function_display_error_box= strAnchorLink  & "<table width=""65%"" border=""0"" cellspacing=""1"" cellpadding=""5"" bgcolor=""#CC0000"">" & VbCrlf
			function_display_error_box = function_display_error_box & "<tr><td bgcolor=""#FFFFF4"" class=""m"" align=""left"" valign=""middle"" height=""30""><font color=""#CC0000""><img src=""/images/ico_warning.gif"" width=""16"" height=""14"" align=""absmiddle""> &nbsp;"&function_display_error(strError)&"</font></td>" & VbCrlf
			function_display_error_box = function_display_error_box & "</table>" & VbCrlf
		
		
	END SELECT
	
END FUNCTION
%>