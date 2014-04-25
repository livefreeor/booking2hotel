<%@ Page Language="C#" AutoEventWireup="true" CodeFile="duplicate.aspx.cs" Inherits="Hotels2thailand.UI.test_duplicate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <div> Total Price Duplicate : <asp:Label ID="lblTotal" runat="server"></asp:Label></div>
        <table>
            <tr>
                <td>ConditionID : <asp:TextBox ID="txtCondition" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>date Start <asp:TextBox ID="txtdateStart" runat="server"></asp:TextBox></td>
                <td>date End<asp:TextBox ID="txtDateEnd" runat="server"></asp:TextBox></td>
            </tr>
        </table>

        <asp:Button ID="btnRun" runat="server" Text="Process" OnClick="btnRun_Click" />
    </div>
    </form>
</body>
</html>
