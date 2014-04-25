<%
FUNCTION function_gen_image_thumb(intProductID,strProductCode,intNumImage,intType)

	Dim intCount
	Dim arrImage()
	Dim intImage
	
	SELECT CASE intType
		Case 1
			ReDim arrImage(12)
			
			For intCount=1 To 12
			
				IF intCount<=intNumImage Then
					arrImage(intCount) = "<a href=""/thailand-hotels-pic.asp?pic="& intCount &"&id="& intProductID &""" target=""_new""><img src=""/thailand-hotels-pic/"& strProductCode & "_b_" & intCount &".jpg"" border=""0""></a>"
					intImage = intImage + 1
				Else
					arrImage(intCount) = "&nbsp;"
				End IF
			
			Next
		
		%>
		
						<table width="100%"  cellspacing="2" cellpadding="2">
						  <tr align="center" valign="middle">
							<td><%=arrImage(1)%></td>
							<td><%=arrImage(2)%></td>
							<td><%=arrImage(3)%></td>
							<td><%=arrImage(4)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(5)%></td>
							<td><%=arrImage(6)%></td>
							<td><%=arrImage(7)%></td>
							<td><%=arrImage(8)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(9)%></td>
							<td><%=arrImage(10)%></td>
							<td><%=arrImage(11)%></td>
							<td><%=arrImage(12)%></td>
						  </tr>
						</table>

<%
		Case 2
			ReDim arrImage(32)
			
			For intCount=1 To 32
			
				IF intCount<=intNumImage Then
					arrImage(intCount) = "<a href=""/thailand-hotels-pic.asp?pic="& intCount &"&id="& intProductID &"""><img src=""/thailand-hotels-pic/"& strProductCode & "_b_" & intCount &".jpg"" border=""0""></a>"
					intImage = intImage + 1
				Else
					arrImage(intCount) = "&nbsp;"
				End IF
			
			Next
%>
<table width="100%"  cellspacing="2" cellpadding="2">
    <tr align="center">
      <td><%=arrImage(1)%></td>
      <td><%=arrImage(2)%></td>
      <td><%=arrImage(3)%></td>
      <td><%=arrImage(4)%></td>
      <td><%=arrImage(5)%></td>
      <td><%=arrImage(6)%></td>
	  <td><%=arrImage(7)%></td>
	  <td><%=arrImage(8)%></td>
    </tr>
	<%IF intImage>8 Then%>
    <tr align="center">
      <td><%=arrImage(9)%></td>
      <td><%=arrImage(10)%></td>
      <td><%=arrImage(11)%></td>
      <td><%=arrImage(12)%></td>
      <td><%=arrImage(13)%></td>
      <td><%=arrImage(14)%></td>
	  <td><%=arrImage(15)%></td>
	  <td><%=arrImage(16)%></td>
    </tr>
	<%
		End IF
		IF intImage>16 Then
	%>
    <tr align="center">
      <td><%=arrImage(17)%></td>
      <td><%=arrImage(18)%></td>
      <td><%=arrImage(19)%></td>
      <td><%=arrImage(20)%></td>
      <td><%=arrImage(21)%></td>
      <td><%=arrImage(22)%></td>
	  <td><%=arrImage(23)%></td>
	  <td><%=arrImage(24)%></td>
    </tr>
	<%
		End IF
		IF intImage>24 Then
	%>
    <tr align="center">
      <td><%=arrImage(25)%></td>
      <td><%=arrImage(26)%></td>
      <td><%=arrImage(27)%></td>
      <td><%=arrImage(28)%></td>
      <td><%=arrImage(29)%></td>
      <td><%=arrImage(30)%></td>
	  <td><%=arrImage(31)%></td>
	  <td><%=arrImage(32)%></td>
    </tr>
	<%End IF%>
  </table>

<%
		Case 3 'Option Pictures (12->3*4)
			ReDim arrImage(12)
			
			For intCount=1 To 12
			
				IF intCount<=intNumImage Then
					arrImage(intCount) = "<a href=""/thailand-hotels-option-pic.asp?pic="& intCount &"&id="& intProductID &"""><img src=""/thailand-hotels-pic/"& strProductCode & "_" & intProductID & "_b_" & intCount &".jpg"" border=""0""></a>"
					intImage = intImage + 1
				Else
					arrImage(intCount) = "&nbsp;"
				End IF
			
			Next
		
		%>
		
						<table width="100%"  cellspacing="2" cellpadding="2">
						  <tr align="center" valign="middle">
							<td width="33%"><%=arrImage(1)%></td>
							<td width="33%"><%=arrImage(2)%></td>
							<td width="33%"><%=arrImage(3)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(4)%></td>
							<td><%=arrImage(5)%></td>
							<td><%=arrImage(6)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(7)%></td>
							<td><%=arrImage(8)%></td>
							<td><%=arrImage(9)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(10)%></td>
							<td><%=arrImage(11)%></td>
							<td><%=arrImage(12)%></td>
						  </tr>
						</table>
<%
		Case 4 'Option Pictures (12->6*2)
			ReDim arrImage(12)
			
			For intCount=1 To 12
			
				IF intCount<=intNumImage Then
					arrImage(intCount) = "<a href=""/thailand-hotels-option-pic.asp?pic="& intCount &"&id="& intProductID &"""><img src=""/thailand-hotels-pic/"& strProductCode & "_" & intProductID & "_b_" & intCount &".jpg"" border=""0""></a>"
					intImage = intImage + 1
				Else
					arrImage(intCount) = "&nbsp;"
				End IF
			
			Next
		
		%>
		
						<table width="100%"  cellspacing="2" cellpadding="2">
						  <tr align="center" valign="middle">
							<td width="17%"><%=arrImage(1)%></td>
							<td width="17%"><%=arrImage(2)%></td>
							<td width="17%"><%=arrImage(3)%></td>
							<td width="17%"><%=arrImage(4)%></td>
							<td width="17%"><%=arrImage(5)%></td>
							<td width="17%"><%=arrImage(6)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(7)%></td>
							<td><%=arrImage(8)%></td>
							<td><%=arrImage(9)%></td>
							<td><%=arrImage(10)%></td>
							<td><%=arrImage(11)%></td>
							<td><%=arrImage(12)%></td>
						  </tr>
						</table>
<%
	Case 5 'Product Pictures 32 (8*4)
			ReDim arrImage(32)
			
			For intCount=1 To 32
			
				IF intCount<=intNumImage Then
					arrImage(intCount) = "<a href=""/thailand-hotels-pic.asp?pic="& intCount &"&id="& intProductID &""" target=""_new""><img src=""/thailand-hotels-pic/"& strProductCode & "_b_" & intCount &".jpg"" border=""0""></a>"
					intImage = intImage + 1
				Else
					arrImage(intCount) = "&nbsp;"
				End IF
			
			Next
%>
						<table width="100%"  cellspacing="2" cellpadding="2">
						  <tr align="center" valign="middle">
							<td width="12%"><%=arrImage(1)%></td>
							<td width="12%"><%=arrImage(2)%></td>
							<td width="12%"><%=arrImage(3)%></td>
							<td width="12%"><%=arrImage(4)%></td>
							<td width="12%"><%=arrImage(5)%></td>
							<td width="12%"><%=arrImage(6)%></td>
							<td width="12%"><%=arrImage(7)%></td>
							<td width="12%"><%=arrImage(8)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(9)%></td>
							<td><%=arrImage(10)%></td>
							<td><%=arrImage(11)%></td>
							<td><%=arrImage(12)%></td>
							<td><%=arrImage(13)%></td>
							<td><%=arrImage(14)%></td>
							<td><%=arrImage(15)%></td>
							<td><%=arrImage(16)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(17)%></td>
							<td><%=arrImage(18)%></td>
							<td><%=arrImage(19)%></td>
							<td><%=arrImage(20)%></td>
							<td><%=arrImage(21)%></td>
							<td><%=arrImage(22)%></td>
							<td><%=arrImage(23)%></td>
							<td><%=arrImage(24)%></td>
						  </tr>
						  <tr align="center" valign="middle">
							<td><%=arrImage(25)%></td>
							<td><%=arrImage(26)%></td>
							<td><%=arrImage(27)%></td>
							<td><%=arrImage(28)%></td>
							<td><%=arrImage(29)%></td>
							<td><%=arrImage(30)%></td>
							<td><%=arrImage(31)%></td>
							<td><%=arrImage(32)%></td>
						  </tr>
						</table>
<%
	Case 6
	Case 7
	Case 8
	Case 9
	END SELECT
END FUNCTION
%>