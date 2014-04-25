<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="confirm_fullcahrge_send.aspx.cs" Inherits="Hotels2thailand.UI.admin_confirm_fullcahrge_send" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
 <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
<script type="text/javascript" language="javascript" src="../../scripts/darkman_utility.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <HTMLEditor:Editor runat="server"  Id="editor" Height="600px" AutoFocus="true" Width="100%" />
   
 <div class="mailform" style=" text-align:center; ">
    <table>
        <tr><td align="right">MailTo</td><td align="left"><asp:TextBox ID="txtMailTO" runat="server"  Width="300px" ></asp:TextBox></td></tr>
        <tr><td align="right">Bcc</td><td align="left"><asp:TextBox ID="txtBcc" runat="server" Width="300px" ></asp:TextBox></td></tr>
        <tr><td align="right">Subject</td><td align="left"><asp:TextBox ID="txtSubject" runat="server" Width="600px" ></asp:TextBox></td></tr>
    </table>
    <asp:Button runat="server" Text="Send Now" ID="submit" OnClick="submit_click" SkinID="Green" />
    
 </div>
 
   
</asp:Content>

