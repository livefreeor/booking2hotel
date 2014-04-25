<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="account_deposit.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_account_deposit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <link href="../../css/account/bhtmanage.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>

    <link href="../../css/account/deposit.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">   

        $(document).ready(function () {

            //$("#btnadd").click(function () {
            //    if ($("#deposit_insert").css("display") == "none") {
            //        $("#deposit_insert").slideDown('fast');
            //    }
            //    else {
            //        $("#deposit_insert").slideUp('fast');
            //    }
            //});
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h6>Account Deposit : Hotel List</h6>
    <br /><br />
    <asp:Button ID="btnadd"  runat="server" Text="Add New Deposit" CssClass="btStyleGreen" EnableTheming="false" OnClick="btnadd_Click" />
    <br /><br />
    <asp:Panel runat="server" ID="deposit_insert"  Visible="false" CssClass="deposit_insert">
        
       Booking Id: <asp:TextBox ID="txtBookingId" Width="150px" runat="server" EnableTheming="false" CssClass="TextBox_Extra_normal"></asp:TextBox>

        <asp:Button ID="btnSearch" OnClick="btnSearch_Click"  runat="server" Text="Search" CssClass="btStyleGreen" EnableTheming="false" />

        <asp:Panel ID="panelBookingDetail" runat="server" Visible="false" CssClass="panel_booking_detail">
            <h4>Booking ID: <asp:Label ID="lblBookingId" runat="server"></asp:Label></h4>
           <table>
               
               <tr>
                   <td>Hotel name :<asp:HyperLink ID="lnbookingDetail" Text="booking detail"  runat="server" Target="_blank"></asp:HyperLink></td>
                   <td><asp:Label ID="lblhotelName" runat="server"></asp:Label></td>
               </tr>
               <tr>
                   <td>Customer Name:</td>
                   <td><asp:Label ID="lblCustomerName" runat="server"></asp:Label></td>
               </tr>
               <tr>
                   <td>Checkin -Out:</td>
                   <td><asp:Label ID="lblChekinout" runat="server"></asp:Label></td>
               </tr>
               <tr>
                   <td>Booking Amount:</td>
                   <td><asp:Label ID="lblbookingAmount" runat="server"></asp:Label></td>
               </tr>

               <tr>
                   <td>Deposit Amount:</td>
                   <td><asp:TextBox ID="txtAmount" runat="server" Width="150"  EnableTheming="false" CssClass="textBoxStyle_color"  ></asp:TextBox></td>
               </tr>
               <tr>
                   <td>Hotel Staff</td>
                   <td><asp:TextBox ID="txtHotelstaff" runat="server" Width="150"  EnableTheming="false" CssClass="TextBox_Extra_normal"  ></asp:TextBox></td>
               </tr>
               <tr>
                   <td>Comment</td>
                   <td><asp:TextBox ID="txtComment" runat="server" Width="200" TextMode="MultiLine" Rows="4"  EnableTheming="false" CssClass="TextBox_Extra_normal"  ></asp:TextBox></td>
               </tr>
           </table>

           <asp:Button ID="btnDepSave" runat="server" Text="Assign to Deposit" OnClick="btnDepSave_Click" CssClass="btStyleGreen" EnableTheming="false" />
        </asp:Panel>
    </asp:Panel>


    <asp:Panel ID="deposit_hotel_list" runat="server" ClientIDMode="Static">
        <asp:Literal ID="dep_hotelList" runat="server"></asp:Literal>
    </asp:Panel>
</asp:Content>

