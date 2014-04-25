<%
FUNCTION function_livechat(intType)

	'Call subWhoOn()
	'intType = 5 is close live chat by visa
	'intType=5

	SELECT CASE intType
		Case 1
%>
<table cellpadding="0" cellspacing="0" border="0" width="163">
	<tr>
		<td align="center">
	        <script type="text/javascript" src="http://74.86.230.181/JavaScript.ashx?fileMask=Optional/ChatScripting"></script>
            <script src="http://74.86.230.181/ChatScript.ashx?config=1&id=ControlID" type="text/javascript"></script>
		<br><div class="s"><font color="#3366CC">Online chat with our staff.</font></div>
		</td>
	</tr>
</table>

<%
		Case 2
%>
	        <script type="text/javascript" src="http://74.86.230.181/JavaScript.ashx?fileMask=Optional/ChatScripting"></script>
            <script src="http://74.86.230.181/ChatScript.ashx?config=1&id=ControlID" type="text/javascript"></script>
<%
		Case 3
%>
If you want to know room avibility or more information about hotel, please contact us via Live Chat  <br>
	        <script type="text/javascript" src="http://74.86.230.181/JavaScript.ashx?fileMask=Optional/ChatScripting"></script>
            <script src="http://74.86.230.181/ChatScript.ashx?config=1&id=ControlID" type="text/javascript"></script>
<%
		Case 4
%>
	        <script type="text/javascript" src="http://74.86.230.181/JavaScript.ashx?fileMask=Optional/ChatScripting"></script>
            <script src="http://74.86.230.181/ChatScript.ashx?config=1&id=ControlID" type="text/javascript"></script>
<%
	END SELECT
END FUNCTION
%>