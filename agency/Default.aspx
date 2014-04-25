<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="b2b_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style-b2b.css" rel="stylesheet" />
    <script type="text/javascript">
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <div id="logo">
                <img src="image/logo.png" />
            </div>
            <br class="clear-all" />
            <div id="divDetail" class="boxbody_b2b" runat="server">
                <asp:Label ID="lblHeader" CssClass="headertext" runat="server" Text="LOGIN" ></asp:Label>
                <br />
                <hr style="text-align: left; border-bottom-style: solid; color: #cccccc; width: 100%;" />
                <br />
                <center>
                <table border="0" >
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Username : 
                        </td>
                        <td>

                        </td>
                        <td>
                            <asp:TextBox ID="txtUsername" Width="190px" runat="server" CssClass="textboxLogin"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td>
                            Password : 
                        </td>
                        <td>

                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" Width="190px" runat="server" TextMode="Password" CssClass="textboxLogin"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:right">
                            <asp:Button ID="btnLogin" runat="server" CssClass="login" OnClick="btnLogin_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                </table>
                </center>
            </div>
        </div>
    </form>
</body>
</html>
