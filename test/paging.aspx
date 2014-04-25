<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paging.aspx.cs" Inherits="test_paging" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        ul {
            margin:0px ;
            padding:0px;
        }
        li {
            margin:0 0 0 5px;
            display:block;
            float:left;

        }
        .page_active {
            color:#f00;
            font-weight:bold;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     Total Page: <asp:TextBox ID="txtPagetotal" runat="server"></asp:TextBox><br />
        Cuurent Page: <asp:TextBox ID="txtCurrentPage" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btn_genPag" runat="server" OnClick="btn_genPag_Click" />
        <asp:Label ID="lblREsult" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
