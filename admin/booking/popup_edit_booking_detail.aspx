<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popup_edit_booking_detail.aspx.cs" Inherits="Hotels2thailand.UI.extranet_ordercenter_popup_edit_booking_detail" %>
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
    <div style=" width:400px; font-family:Tahoma; margin:0 auto; border:1px solid #e2d6a6;font-size:12px; color:#6a785a; background-color:#ffffff ; padding:20px;">
        <table width="100%" cellpadding="0" cellspacing="5" >
                  <tr>
                  <td class="detail_head">Name:</td>
                  <td>
                  <asp:DropDownList ID="dropPrefix" runat="server" EnableTheming="false" CssClass="Extra_Drop"></asp:DropDownList>
                  <asp:TextBox ID="txtBookingName" runat="server" EnableTheming="false"  CssClass="Extra_textbox"></asp:TextBox>
                 </td>
                  </tr>
                  <tr>
                  <td class="detail_head">Email:</td>
                  <td>
                  <asp:TextBox ID="txtEmail" Width="250" runat="server" EnableTheming="false"  CssClass="Extra_textbox"></asp:TextBox>
                  </td>
                  </tr>
                  
                  <tr>
                  <td class="detail_head">Phone:</td>
                  <td>
                  <asp:TextBox ID="txtPhone" Width="250" EnableTheming="false"  CssClass="Extra_textbox" runat="server"></asp:TextBox>
                  </td>
                  </tr>
                  <tr>
                  <td class="detail_head">Mobile:</td>
                  <td><asp:TextBox ID="txtmobile" Width="250" EnableTheming="false"  CssClass="Extra_textbox" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                  <td class="detail_head">Country:</td>
                  <td><asp:DropDownList ID="dropCountry" EnableTheming="false" CssClass="Extra_Drop" runat="server"></asp:DropDownList></td>
                  </tr>
               <tr><td colspan="2" style="height:5px;"></td></tr>
                  <tr>
                  <td class="detail_head">Arrival Flight:</td>
                  <td>
                  <asp:TextBox ID="txtArrF" EnableTheming="false" CssClass="Extra_Drop" runat="server"></asp:TextBox>
                  <Product:datePicker ID="dDatePicker_arr" runat="server" />
                  hh:<asp:DropDownList ID="arrdropHH" EnableTheming="false" CssClass="Extra_Drop" runat="server"></asp:DropDownList>
                  mm:<asp:DropDownList ID="arrdropMM" EnableTheming="false" CssClass="Extra_Drop" runat="server"></asp:DropDownList>
                  </td>
                  </tr>
                   <tr><td colspan="2" style="height:5px;"></td></tr>
                  <tr>
                  <td class="detail_head">Daparture Flight:</td>
                  <td><asp:TextBox ID="txtDepF" EnableTheming="false" CssClass="Extra_Drop" runat="server"></asp:TextBox>
                  <Product:datePicker ID="dDatePicker_Dep" runat="server" />
                  hh:<asp:DropDownList ID="depdropHH" EnableTheming="false" CssClass="Extra_Drop" runat="server"></asp:DropDownList>
                  mm:<asp:DropDownList ID="depdropMM" EnableTheming="false" CssClass="Extra_Drop" runat="server"></asp:DropDownList></td>
                  </tr>
                     <tr>
                         <td class="detail_head">Transfer Request</td>
                         <td><asp:TextBox ID="txttransfer" runat="server" Width="250" Rows="5" TextMode="MultiLine" EnableTheming="false" CssClass="Extra_textbox"></asp:TextBox></td>
                     </tr>
                <tr><td colspan="2" style="height:5px;"></td></tr>
                  <tr>
                   <td colspan="2">
                   <asp:Button Id="btnSaveBookingDetail" runat="server" EnableTheming="false" CssClass="Extra_Button_small_blue" Text="Save" OnClick="btnSaveBookingDetail_Onclick" />
                   </td>
                  </tr>
                 </table>
    </div>
    </form>
</body>
</html>
