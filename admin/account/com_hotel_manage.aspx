<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="com_hotel_manage.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_com_hotel_manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.corner.js"></script>
  	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <link href="../../css/account/bht_invoice.css"  type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
    <link href="../../css/account/bhtmanage.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            timedRefresh(20000);
            
        });
        function timedRefresh(timeoutPeriod) {
            setTimeout("location.reload(true);", timeoutPeriod);
        }
        function ConfirmOnCancel() {
            if (confirm("Are you sure to Cancel this Payment?") == true)
                return true;
            else
                return false;
        }

        function ConfirmPaid() {
            if (confirm("Are you sure to paid ?") == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h6>Hotel Manage Commission : Hotel List <a  href="account_hotel_manage_payment.aspx" target="_blank">Pedding Payment</a></h6> 
    <asp:GridView ID="gvHotelLists" DataKeyNames="ProductId" ClientIDMode="Static"  CssClass="tbl_account" GridLines="None" runat="server" EnableModelValidation="false" Width="100%" CellPadding="0" CellSpacing="1" AutoGenerateColumns="false"  EnableTheming="false" OnRowDataBound="gvHotelLists_RowDataBound"  ShowFooter="false">
        <EmptyDataTemplate>
            No Hotel to Comission Process
        </EmptyDataTemplate>
        <HeaderStyle HorizontalAlign="Center" />
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField  HeaderText="No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Hotel Code">
                <ItemTemplate>
                    <asp:HyperLink ID="lnLink" runat="server" NavigateUrl='<%# String.Format("/admin/account/com_hotel_manage_sel.aspx?pid={0}",Eval("ProductId")) %>' Target="_blank"  Text='<%# Eval("ProductCode") %>'></asp:HyperLink>
                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Hotel Name"   ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                 <%# Eval("ProductTitle") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Num of Booking.">
                <ItemTemplate>
                     <%# Eval("NumBookingDue") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

