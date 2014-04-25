<%@ Page Language="C#" AutoEventWireup="true" CodeFile="com_sales_commission.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_com_sales_commission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
       
         table{  
             background-color:#808080;
             width:1200px;
                 font-size:12px;
                 font-family:Tahoma;
                 border-collapse: separate;
                 border-spacing: 2px;

         }
         

          tr:nth-of-type(odd){ /*odd rows*/
            background: #f1f1f1;
}

 tr:nth-of-type(even){ /*even rows*/
             background: #ffffff;
}
            td{
                padding: 2px;
                 
            }
        /*tr > td:nth-of-type(n+8) {
            font-weight:bold;
        }*/
        h1{
            margin:10px 0 10px  0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:DropDownList ID="dropMonth" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="DropYear" runat="server"></asp:DropDownList>
        <asp:Button ID="btnGO" runat="server" OnClick="btnGO_Click" Text="Go" />
    <div >
     
        <asp:Literal ID="ltroldCom" runat="server"></asp:Literal>
     
    </div>
    </form>
</body>
</html>
