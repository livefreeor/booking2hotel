<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popup_edit_product_detail.aspx.cs" Inherits="Hotels2thailand.UI.extranet_ordercenter_popup_edit_product_detail" %>
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
<body style=" background:#faf7dd;">
    <form id="form1" runat="server">
    <div style=" width:700px; font-family:Tahoma; margin:0 auto; border:1px solid #e2d6a6; background-color:#ffffff ;font-size:12px; color:#6a785a; padding:20px;">
        <table cellpadding="0" cellspacing="5">
         <tr>
          <td>Check In Date</td>
          <td><Product:datePicker ID="dDatePicker_checkin"  runat="server" /></td>
         </tr>
         <tr>
         <td>Check Out date</td>
         <td><Product:datePicker ID="dDatePicker_checkout" runat="server" /></td>
         </tr>
         <tr>
          <td>Adult</td>
          <td><asp:DropDownList ID="dropAdult" EnableTheming="false" CssClass="Extra_Drop" runat="server" ></asp:DropDownList></td>
         </tr>
         <tr>
         <td>Child</td>
         <td><asp:DropDownList ID="dropChild" EnableTheming="false" CssClass="Extra_Drop" runat="server" ></asp:DropDownList></td>
         </tr>
        </table>

         <asp:GridView ID="GVitemList" runat="server" EnableTheming="false" GridLines="None" CssClass="tbl_acknow" AutoGenerateColumns="false" DataKeyNames="BookingItemID" EnableModelValidation="true"  OnRowDataBound="GVitemList_OnRowDataBound" >
                 <HeaderStyle BackColor="#f2ebbd" ForeColor="#4a5b54"  />
                 <Columns>
                 
                  <asp:TemplateField HeaderText="No.">
                   
                    <ItemTemplate> <%# Container.DataItemIndex + 1 %> </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Room Type" >
                    <ItemTemplate>
                      <asp:Label ID="lblRoomtype" runat="server"></asp:Label>
                     <%--<asp:DropDownList ID="GVdropRoomType" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="GVdropRoomType_OnSelectedIndexChanged"></asp:DropDownList>--%>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Condition" >
                    <ItemTemplate>
                    <asp:Label ID="lblCondition" runat="server"></asp:Label>
                     <%--<asp:DropDownList ID="GVdropCondition" runat="server"></asp:DropDownList>--%>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                     <asp:DropDownList ID="GvdropQuantity" EnableTheming="false" CssClass="Extra_Drop" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                     <asp:TextBox ID="GvtxtPrice" EnableTheming="false" Width="80"  CssClass="Extra_textbox" runat="server"></asp:TextBox> Baht
                    </ItemTemplate>
                  </asp:TemplateField>
                     <asp:TemplateField HeaderText="Sup Price">
                    <ItemTemplate>
                     <asp:TextBox ID="GvtxtPriceSup" EnableTheming="false" Width="80"  CssClass="Extra_textbox" runat="server"></asp:TextBox> Baht
                    </ItemTemplate>
                  </asp:TemplateField>
                 <%-- <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                    <asp:ImageButton ID="GvimgREmove" runat="server" ImageUrl="~/images_extra/bin.png" OnClick="GvimgREmove_ONclick"  />
                    </ItemTemplate>
                  </asp:TemplateField>--%>
                 </Columns>
                </asp:GridView>

        <%--<table cellpadding="0" cellspacing="0">
        <tr>
         <th>Room Type</th>
         <th>Condition</th>
         <th>Unit</th>
         <th>Price</th>
        </tr>
        <tr>
        <td><asp:DropDownList ID="dropOption" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropOption_OnSelectedIndexChanged"></asp:DropDownList></td>
        <td><asp:DropDownList ID="dropCondition" runat="server" ></asp:DropDownList></td>
        <td><asp:DropDownList ID="dropUnit" runat="server"></asp:DropDownList></td>
        <td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
        
        </tr>
         <tr><td colspan="4">
         <asp:Button ID="btnInsertNewItem" runat="server" OnClick="btnInsertNewItem_Onclick" Text="Add New Now" />
         </td></tr>
        </table>--%>
        

        <div>
         <asp:Button ID="mainSave" runat="server" EnableTheming="false" CssClass="Extra_Button_small_blue" Text="Save & Calculate Now" OnClick="mainSave_Onclick"  />
        </div>
    </div>
    </form>
</body>
</html>
