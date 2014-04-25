<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rd_update_staff.aspx.cs" Inherits="rd_update_staff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
   Module: <asp:TextBox ID="txtModuleID" runat="server"></asp:TextBox> <br />
   Method: <asp:TextBox ID="txtMethod" runat="server"></asp:TextBox>

   <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Onclick" Text="UPdate" />
    </form>
</body>
</html>
