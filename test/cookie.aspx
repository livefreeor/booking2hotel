﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cookie.aspx.cs" Inherits="test_cookie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblcookie" runat="server"></asp:Label>
    <asp:Button ID="ff" runat="server" Text="Clear Cookie" OnClick="ff_onclick" />
    </div>
    </form>
</body>
</html>
