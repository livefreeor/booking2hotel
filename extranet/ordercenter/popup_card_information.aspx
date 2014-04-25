<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popup_card_information.aspx.cs" Inherits="Hotels2thailand.UI.extranet_ordercenter_popup_card_information" %>
<%@ Register Src="~/Control/DatepickerCalendar-single.ascx" TagName="datePicker" TagPrefix="Product" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../scripts/popup.js"></script>
    <link href="../../css/extranet/extranet_style_core.css" type="text/css" rel="Stylesheet" />
</head>
<body style=" background:#faf7dd;" >
    <form id="form1" runat="server">
    <div style=" width:500px; font-family:Tahoma; margin:0 auto; border:1px solid #e2d6a6;font-size:12px; color:#6a785a; background-color:#ffffff ; padding:20px;">
        <table width="100%" cellpadding="0" cellspacing="0">
         <tr>
          <td align="left">Input:</td>
          <td><asp:TextBox ID="txtInput" Width="100%" runat="server" TextMode="MultiLine" Rows="5" CssClass="Extra_textbox" EnableTheming="false"></asp:TextBox> </td>
         </tr>
         <tr>
          <td align="left">
            Output:
          </td>
          <td>
          
          <asp:Literal ID="ltCardDetail" runat="server"></asp:Literal>
          </td>
         </tr>
         <tr>
           <td colspan="2" align="center">
            <asp:Button ID="btnDecode" runat="server" Text="Decode" OnClick="btnDecode_Onclick" CssClass="Extra_Button_small_green" />
           </td>
         </tr>
        </table>
    </div>
    </form>
</body>
</html>
