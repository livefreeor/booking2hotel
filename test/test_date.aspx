<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test_date.aspx.cs" Inherits="test_date" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:RadioButtonList ID="radioCat" runat="server" RepeatDirection="Vertical"></asp:RadioButtonList>
      <asp:Button ID="btnPdfHotel" runat="server" Text="Gen"  OnClick="btnPdfHotel_Onclick"/>
    </form>
</body>
</html>


