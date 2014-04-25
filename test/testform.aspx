<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testform.aspx.cs" Inherits="test_testform" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="ff" runat="server">
     <asp:Literal ID="lbltxt" runat="server"></asp:Literal>
<%--      <asp:TextBox ID="txtTest" runat="server"></asp:TextBox>--%>
     <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Onclick" Text="next" />
    </asp:Panel>

    <asp:Panel ID="dd" runat="server" Visible="false">
    <asp:Literal  runat="server" ID="lblTest" ></asp:Literal>
     <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Onclick" Text="edit" />
    </asp:Panel>
    </form>
</body>
</html>
