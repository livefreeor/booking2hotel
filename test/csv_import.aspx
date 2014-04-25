<%@ Page Language="C#" AutoEventWireup="true" CodeFile="csv_import.aspx.cs" Inherits="test_csv_import" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript"  src="../scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload id="fiUpload" runat="server"></asp:FileUpload>
		<input id="btnUpload" type="button" OnServerClick="btnUpload_OnClick"  value="Upload" runat="server" />
		<hr />
		<asp:Label id="lblText" runat="server"></asp:Label>
        <br />
        <br />
        <input type="text" name="productId"/>
        <asp:Button ID="btnImport" runat="server"  Text="Import Now" OnClick="btnImport_Click" />
    </div>
    </form>
</body>
</html>
