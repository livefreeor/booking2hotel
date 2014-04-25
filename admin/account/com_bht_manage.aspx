<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="com_bht_manage.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_com_bht_manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/account/bhtmanage.css" type="text/css" rel="Stylesheet" />
      <script type="text/javascript" language="javascript" src="../../Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <link href="../../css/account/bhtmanage.css" type="text/css" rel="Stylesheet" />
     <script type="text/javascript" language="javascript">
         function timedRefresh(timeoutPeriod) {
             setTimeout("location.reload(true);", timeoutPeriod);
         }

         $(document).ready(function () {

             timedRefresh(20000);
             //$("body").load(function () {
             //    alert("HELLO");
             //});
         });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h6>Bht Manage Commission : Hotel List</h6>
    <div class="tbl_account">
    <asp:Literal ID="ltHotelList" runat="server"></asp:Literal>
    </div>
</asp:Content>

