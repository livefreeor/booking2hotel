<%
Function functionDisplayTitlePromotionCampaign(productID,arrPromotion,intType)
	Dim intPromotion
	
	Select Case intType
	Case 1
		For intPromotion=0 to Ubound(arrPromotion,2)
			IF productID=arrPromotion(0,intPromotion) Then
				functionDisplayTitlePromotionCampaign="<font color=""#25790b""><strong>"&arrPromotion(1,intPromotion)&"</strong></font><br><br><font color=""#5e3a06""><strong>Promotion Included :</strong></font> <br><font color=""#047bb5"">"&arrPromotion(2,intPromotion)&"</font><br><br><font color=""#99600b""><font color=""red"">***</font> Special offer by Hotels2thailand.com and cannot be combined with any other offer.</font>"
			End IF
		Next
	Case 2
		
	End Select
	
End Function
%>

