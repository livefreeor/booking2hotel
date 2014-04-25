<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="com_hotel_manage_detail.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_com_hotel_manage_detail" %>

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

        function ConfirmPayment() {
            if (confirm("Are you sure to change") == true)
                return true;
            else
                return false;
        }

        function getVal(obj) {

            var radioVa = $("input[name='ctl00$ContentPlaceHolder1$raidoVat']:checked").val();

            if (radioVa == "1") {
                $("#vat_value").show();

                $("#bank_sel").hide();
            }
            else {
                $("#vat_value").hide();
                $("#bank_sel").show();
            }

            //var gets = $("#" + obj.id).html();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h6>Hotel Manage Payment Edit : Invoice NO. <asp:Literal ID="liter" runat="server"></asp:Literal></h6>
    
        <asp:Literal ID="ltPaylistSum" runat="server"></asp:Literal>
        <br /><br />
        <table>
            <tr>
                <td colspan="2">
                    <asp:RadioButtonList ID="raidoVat" runat="server" ClientIDMode="Static"  onclick="getVal(this);">
                        <asp:ListItem Value="1" Text="Include Vat" Selected="True" ></asp:ListItem>
                        <asp:ListItem Value="0" Text="No Vat"></asp:ListItem>
                    </asp:RadioButtonList>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                     
                    <asp:panel  ID="vat_value" runat="server" ClientIDMode="Static" >
                        <table>
                                <tr>
                                    <td>Vat value:</td>
                                    <td><asp:TextBox ID="txtVatval" runat="server" Text="7"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><asp:CheckBox ID="chktax" runat="server" Text="withholding tax( ภาษีหัก ณ ที่จ่าย)"  /></td>
                                    <td><asp:TextBox ID="txtTaxValue" runat="server" Text="3"></asp:TextBox></td>
                                </tr>
                        </table>
                    </asp:panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:panel ID="bank_sel"  style="display:none;" runat="server" ClientIDMode="Static">
                        <table>
                             <tr>
                               <td>
                                   <asp:RadioButtonList ID="raidoBank" runat="server">
                                        <asp:ListItem Value="3" Text="Kasikron" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="SCB"></asp:ListItem>
                                    </asp:RadioButtonList>
                               </td>
                            </tr>
                        </table>
                        </asp:panel>
                </td>
            </tr>
        </table>
        <br /><br />
        <p><strong>Print:</strong><asp:HyperLink ID="hyinvoice" Target="_blank" runat="server" Text="invoice"></asp:HyperLink>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:HyperLink ID="hybookingList" runat="server" Text="booking detail"></asp:HyperLink></p>
        <p style="color:red">** กรุณาตรวจเช็ค ก่อน กด ปุ่มสีเขียว </p>
        <asp:Button ID="btnPayment" runat="server" Text="Save Payment" Font-Size="16px" Width="300px" Height="50px"  OnClick="btnPayment_Click" SkinID="Green"  OnClientClick="return ConfirmPayment();"/>
   
</asp:Content>

