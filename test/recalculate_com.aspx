<%@ Page Language="C#" AutoEventWireup="true" CodeFile="recalculate_com.aspx.cs" Inherits="test_recalculate_com" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btngetAll" runat="server" OnClick="GetAll_Click" Text="GetAll" />
        <asp:Button ID="btngetoddy" runat="server" OnClick="btnget_oddy_Click" Text="Get Oddy" />

        <br />
        <asp:Button ID="btnrecalAll" runat="server" OnClick="btnrecalAll_Click" Text="Recal GetAll" />
        <asp:Button ID="btnReacll_Oddy" runat="server" OnClick="btnReacll_Oddy_Click" Text="Recal Get Oddy" />
    </div>
    </form>
</body>
</html>
