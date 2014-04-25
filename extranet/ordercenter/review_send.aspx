<%@ Page Language="C#" AutoEventWireup="true" CodeFile="review_send.aspx.cs" Inherits="Hotels2thailand.UI.admin_review_send" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
 <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script type="text/javascript" language="javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<link href="../../css/extranet/extranet_style_core.css" type="text/css" rel="Stylesheet" />
</head>
<body style="background:#ebf0f3;">
<form id="form1" runat="server">
 <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1"  />
   <HTMLEditor:Editor runat="server"  Id="editor" Height="600px" AutoFocus="true" Width="100%" />
   
 <div class="mailform" style=" text-align:center; ">
    <table>
        <tr><td align="right">MailTo</td><td align="left"><asp:TextBox ID="txtMailTO" runat="server"  Width="300px" ></asp:TextBox></td></tr>
        <%--<tr><td align="right">Bcc</td><td align="left"><asp:TextBox ID="txtBcc" runat="server" Width="300px" ></asp:TextBox></td></tr>--%>
        <tr><td align="right">Subject</td><td align="left"><asp:TextBox ID="txtSubject" runat="server" Width="600px" ></asp:TextBox></td></tr>
    </table>
    <asp:Button runat="server" Text="Send Now" ID="submit" OnClick="submit_click" SkinID="Green" />
   
 </div>
 
   
  </form>
</body>
</html>

