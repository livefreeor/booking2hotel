<%
Function function_page_pointer(arrlist,page_num,intPagesize,inttype)
Dim start_pointer
Dim intList
Dim total_page
Dim Last_page
Dim end_pointer
page_num=int(page_num)
	Select Case inttype
		Case 1
			IF page_num>1 Then
				start_pointer=((page_num*intPagesize)-intPagesize)
			Else
				start_pointer=0
			End IF
			function_page_pointer=start_pointer
		Case 2
			intList=Ubound(arrList,2)
			total_page=intList\intPagesize
			IF intList mod intPagesize<>0 Then
				total_page=total_page+1
			End IF
			Last_page=total_page
			IF page_num<>Last_page Then
				end_pointer=(page_num*intPagesize)-1
			Else
				end_pointer=intList		
			End IF
			function_page_pointer=end_pointer
	End Select
End Function
%>
