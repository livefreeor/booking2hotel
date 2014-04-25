<%
Function fnStar(intStar)

	Dim k
	
	For k=1 To 5
		IF k <= intStar Then
			fnStar = fnStar & "<img src=""image/star.gif""> "
		Else
			'fnStar = fnStar & "<img src=""image/star-gray.gif""> "
		End IF
	Next

End Function
%>