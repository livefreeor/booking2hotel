<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="com_hotel_manage_sel.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_com_hotel_manage_sel" %>

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

        function ConfirmPayment() {
            if (confirm("Are you sure to Make payment And Print Invoice ?") == true)
                return true;
            else
                return false;
        }

        function RadioCheck(rb) {
            var gv = document.getElementById("GvbhtBank");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }

        function chkBoxCheck() {
            var checkedCount = $("input[name='booking_checked']:checked").length;
            if (checkedCount == 0) {
                alert("Please select Booking least one!");
                return false;
            }
            else
                return true;
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

    <h6>Hotel Manage Commission : <asp:Literal ID="ltHotelName" runat="server"></asp:Literal></h6>
    <asp:Panel ID="panelPaymentSel" CssClass="tbl_account_block" runat="server" >

        <asp:Literal ID="ltBookingList" runat="server"></asp:Literal>
            <br /><br />
        <asp:Button ID="btnPay" runat="server" Text="Next>>" SkinID="Green"  OnClick="btnPay_Click" OnClientClick="return chkBoxCheck();" /> 
    </asp:Panel>
    <asp:Panel ID="panelPaymentSummary"  CssClass="tbl_account_block" runat="server" Visible="false" ClientIDMode="Static" >
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
                     
                    <div id="vat_value" >
                        <table>
                                <tr>
                                    <td>Vat value:</td>
                                    <td><asp:TextBox ID="txtVatval" runat="server" Text="7"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><asp:CheckBox ID="chktax" runat="server" Text="withholding tax( ภาษีหัก ณ ที่จ่าย)" Checked="true" /></td>
                                    <td><asp:TextBox ID="txtTaxValue" runat="server" Text="3"></asp:TextBox></td>
                                </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="bank_sel" style="display:none;">
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
                        </div>
                </td>
            </tr>
        </table>
        <br /><br />
        <asp:Button ID="btnback" runat="server" Text="<<Back" SkinID="White"  OnClick="btnback_Click"  /> 
        <p style="color:red">** กรุณาตรวจเช็ค ก่อน กด ปุ่มสีเขียว เพราะจะไม่สามารถแก้ไขได้แล้วนะครับ </p>
        <asp:Button ID="btnPayment" runat="server" Text="Save Payment And Print Page" Font-Size="16px" Width="300px" Height="50px"  SkinID="Green" OnClick="btnPayment_Click"  OnClientClick="return ConfirmPayment();"/>
    </asp:Panel>
     <asp:Panel ID="panelCompleted" runat="server" Visible="false">

        <p class="txt_completed">ระบบได้บันทึก รายการ payment เรียบร้อยแล้ว สามารถกด เพื่อ ปริ้นท์ ใบ Pay Slip  <asp:HyperLink ID="lnPrint" Target="_blank" runat="server" Text="Print Invoice"></asp:HyperLink>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:HyperLink ID="lnPrintBooking" runat="server" Target="_blank" Text="Print Booking Detail"></asp:HyperLink>
        </p>
    </asp:Panel>
</asp:Content>

