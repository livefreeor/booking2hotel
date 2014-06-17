<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testmd5.aspx.cs" Inherits="test_testform" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="ff" runat="server">
     <asp:TextBox ID="txtKey" runat="server" Width="500px"></asp:TextBox>
<%--      <asp:TextBox ID="txtTest" runat="server"></asp:TextBox>--%>
     <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Onclick" Text="SAVE" />
    </asp:Panel>

    </form>
</body>
</html>
