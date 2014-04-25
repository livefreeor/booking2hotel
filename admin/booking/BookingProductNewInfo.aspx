<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingProductNewInfo.aspx.cs" Inherits="admin_booking_BookingProductNewInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" action="BookingProductNewProcess.aspx">
        <div>
        <asp:Label ID="lblGuest" runat="server" />
        <input type="submit" name="submit" id="submit" value="Insert" />
        </div>
    </form>
</body>
</html>
