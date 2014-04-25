<%
FUNCTION fnErrorMap (strError)
	SELECT CASE Trim(strError)
		Case "feb"
			fnErrorMap = "Please check your date in Febuary."
			
		Case "date_check_in"
			fnErrorMap = "Your check in date must be more than check out date."
			
		Case "date_cureent"
			fnErrorMap = "Your check in date must be more than currentdate."
			
		Case "out_rate"
			fnErrorMap = "Sorry your selected hotel is not available with your date. <br> Please select the other hotel or other date."
		
		Case "email"
			fnErrorMap = "Your email is invalid format.<br>Please recheck your email again."
			
		Case "blank"
			fnErrorMap = "Please fill all require fields."
			
	END SELECT
END FUNCTION
%>