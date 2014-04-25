<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="account_deposit_detail.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_account_deposit_detail" %>

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
    <link href="../../css/account/bhtmanage.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">   

        $(document).ready(function () {

            
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h6>Deposit : <asp:Label ID="lblHotelname" runat="server"></asp:Label></h6>
    <br /><br />

    <h1>Deposit Availiable</h1>
    <div id="deposit_detail_hotel_list">
        <asp:Literal ID="dep_list" runat="server"></asp:Literal>
    </div>
    
    <div id="deposit_detail_repay">
        <asp:Literal ID="dep_repay" runat="server"></asp:Literal>
    </div>
</asp:Content>

