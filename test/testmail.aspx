<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testmail.aspx.cs" Inherits="test_testmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Booking Id:
    <asp:TextBox ID="txtBookingId" runat="server"></asp:TextBox>
        <br />
        Booking Payment ID:
        <asp:TextBox ID="txtPayment" runat="server"></asp:TextBox>
    <asp:RadioButtonList ID="radMailType" runat="server">
     <asp:ListItem Value="1">mail recieve normal</asp:ListItem>
<asp:ListItem Value="5">mail recieve No Allot</asp:ListItem>
     <asp:ListItem Value="2">mail recieve Notice</asp:ListItem>
     <asp:ListItem Value="3">mail resubmit</asp:ListItem>
      <asp:ListItem Value="4">mail recieve Notice OFFLINE</asp:ListItem>

        <asp:ListItem Value="8">mail Voucher</asp:ListItem>
        <asp:ListItem Value="9">mail Voucher Berkeley</asp:ListItem>
        <asp:ListItem Value="6">mail  reset Pass</asp:ListItem>
        <asp:ListItem Value="7">mail Activattion</asp:ListItem>
        <asp:ListItem Value="10">mail Review</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Button ID="btnGen" Text="Gen" runat="server" OnClick="btnGen_Onclick" />
    <asp:Button ID="btnStaffMail" runat="server" Text="Show Email Reciever" OnClick="btnStaffMail_Click" />
    <asp:Label ID="lblREsult" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
