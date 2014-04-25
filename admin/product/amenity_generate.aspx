<%@ Page Language="C#" AutoEventWireup="true" CodeFile="amenity_generate.aspx.cs" Inherits="admin_amenity_generate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-1.6.1.js"></script>
    
    <script type="text/javascript" language="javascript">

        
    </script>
   <style type="text/css">
       #dropTEmplateList
        {
         padding:4px;
         border:1px solid #97a8c4;
        }
        .aspNetDisabled
        {
             text-decoration:line-through;
             color:#8f8f8f;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div id="main"> 

        <div id="template_list">
          Select Template To Replace:  &nbsp;<asp:DropDownList ID="dropTEmplateList" runat="server" ClientIDMode="Static"  EnableTheming="false" ></asp:DropDownList>
          <asp:Button ID="btnSeach" runat="server" Text="Search Now" OnClick="btnSeach_Onclick" />
        </div>
        <div id="total">
        <p>Total Record To Replace:&nbsp;<asp:label  ForeColor="Red" ID="lblrec" runat="server"></asp:label></p>
        <p>Total Replace Completed:&nbsp;<asp:label ForeColor="Green" ID="lblrecom" runat="server"></asp:label></p>
        </div>
        <div id="Fac_list_to_replace"> 
         <asp:CheckBoxList ID="chkListFac" runat="server" ClientIDMode="Static" RepeatColumns="3" RepeatDirection="Horizontal"></asp:CheckBoxList>
        </div>

        <div id="btn"><asp:Button ID="btnReplace" runat="server" OnClick="btnReplace_Onclick" Text="Replace Now" Visible="false" /></div>
    </div>
    </form>
</body>
</html>
