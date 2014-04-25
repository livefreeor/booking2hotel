<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fac_del.aspx.cs" Inherits="admin_product_fac_del" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-1.6.1.js"></script>
    <script type="text/javascript" language="javascript">

        
    </script>
    <style type="text/css">
        #input_block
        {
            float:left;
            margin:0px;
            padding:0px;
             height:600px;
              width:600px;
              overflow:auto;
        }
        #del_result
        {
            float:left;
            margin:0px;
            padding:0px;
             height:600px;
              width:600px;
              overflow:auto;
        }
        #del_btn
        {
            float:left;
            margin:0px;
            padding:0px;
            height:600px;
              width:150px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div id="main"> 

        
        <div id="input_block" >
         
        
         <asp:Literal ID="lblREsult" runat="server"></asp:Literal>
        </div>
        <div id="del_btn">
        <asp:Button ID="Del"  runat="server" Text="Delete Now!!" OnClick="Del_Onclick"/>
        </div>

        <div id="del_result">
            <asp:Literal ID="lblDelResult" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>
