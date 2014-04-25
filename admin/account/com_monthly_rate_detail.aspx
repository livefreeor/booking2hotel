<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="com_monthly_rate_detail.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_com_monthly_rate_detail" %>
<%@ Register  Src="~/Control/DatepickerCalendar.ascx" TagName="DatePicker_Add_Edit" TagPrefix="DateTime" %>
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
    <h6>Monthly Rate Manage Detail : Payment List And Make new  Invoid</h6>
    <br /><br />
    <asp:LinkButton ID="lnnewinvoid" runat="server" Text="Make new invoice" CssClass="ln_invoid" OnClick="lnnewinvoid_Click"></asp:LinkButton>
    <asp:Panel ID="panel_invoid_detail" runat="server" Visible="false" Width="80%" CssClass="invoid_box">
       <DateTime:DatePicker_Add_Edit ID="datePicker" runat="server" />
       <asp:RadioButtonList ID="radioISVat"   runat="server">
           <asp:ListItem Text="Vat Included" Selected="True" Value="1"></asp:ListItem>
           <asp:ListItem Text="No Vat" Value="0"></asp:ListItem>
       </asp:RadioButtonList>

        <asp:Button ID="btnMakeinvoid" runat="server" Text="Make invoid And Print Now" Width="250px" Height="30px" OnClick="btnMakeinvoid_Click" SkinID="Green" />

    </asp:Panel>
    <h1>Payment List</h1>
    <asp:GridView ID="gvPaymentList" runat="server" ClientIDMode="Static"  CssClass="tbl_account" GridLines="None" EnableModelValidation="false" Width="100%" CellPadding="0" CellSpacing="1" AutoGenerateColumns="false"  EnableTheming="false" ShowFooter="false" OnRowDataBound="gvPaymentList_RowDataBound">
        <EmptyDataTemplate>
            <p> No Payment</p>
        </EmptyDataTemplate>
        <HeaderStyle HorizontalAlign="Center" />
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Invoice No.(payment Id)">
                <ItemTemplate>
                    <asp:HyperLink ID="lnPrint" runat="server" Target="_blank" NavigateUrl='<%# String.Format("invoice_monthly_print.aspx?pid={0}&pay={1}",Eval("ProductId"), Eval("PaymentID")) %>' Text='<%# Eval("PaymentID") %>'></asp:HyperLink>
                   
                </ItemTemplate>
            </asp:TemplateField >
            <asp:TemplateField HeaderText="Months">
                <ItemTemplate>
                    <%# Eval("MonthsDetail") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total">
                <ItemTemplate>
                   <%# String.Format("{0:#,##.00}", Eval("ComVal")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Invoice">
                <ItemTemplate>
                    <%# String.Format("{0:dd MMM yyyy}", Eval("DateSubmit")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Confirm Payment (วันที่โรงแรม ชำระ)">
                <ItemTemplate>
                    <asp:Label ID="lblConfirmPayment" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Manage">
                     <ItemTemplate>
                            <asp:Button ID="btnCancel" runat="server"  CssClass="btStyleWhite_small" OnClick="btnCancel_Click" CommandArgument='<%# Eval("PaymentID")  %>' Text="Cancel" OnClientClick="return ConfirmOnCancel();" />
                          <asp:Button ID="btnPaid" runat="server"  CssClass="btStyleGreen_small"  OnClick="btnPaid_Click" CommandArgument='<%# Eval("PaymentID")  %>' Text="Paid" OnClientClick="return ConfirmPaid();" />
                     </ItemTemplate>

                </asp:TemplateField>
        </Columns>
     </asp:GridView>
</asp:Content>

