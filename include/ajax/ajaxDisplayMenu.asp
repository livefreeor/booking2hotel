<%
SELECT CASE Cint(Request.QueryString("intType"))
	Case 1 '### 2nd menu on Homepage
		Response.Write "<a href=""/hotels-help-why-choose.asp""><u>Why choose Hotels2Thailand.com?</u></a> <font color=""346494""> | </font>" & VbCrlf
		Response.Write "<a href=""/hotels-help-why-low.asp""><u>Why our prices are low?</u></a> <font color=""346494""> | </font>" & VbCrlf
		Response.Write "<a href=""/thailand-hotels-testimonial.asp""><u>User Testimonial</u></a> <font color=""346494""> | </font>" & VbCrlf
		Response.Write "<a href=""/thailand-hotels-faq.asp""><u>FAQ</u></a> <font color=""346494""> | </font>" & VbCrlf
		 Response.Write "<a href=""/thailand-hotels-contact.asp""><u>Contact us</u></a>"
	Case 2
	Case 3
END SELECT
%>
