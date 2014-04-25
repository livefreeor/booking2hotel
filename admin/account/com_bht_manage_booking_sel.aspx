<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="com_bht_manage_booking_sel.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_com_bht_manage_booking_sel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript" src="../../Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <link href="../../css/account/bhtmanage.css" type="text/css" rel="Stylesheet" />
     <script type="text/javascript" language="javascript">

         $(document).ready(function () {
                
             
            // $('input[name='']:checked', '#myForm').val()
             //if ($("#gvDeposit").length) {

             //    var dd = $("#gvDeposit tbody tr").length;
             //    var total = 0;

             //    $("#gvDeposit tbody tr").filter(function (index) {

             //        if (index > 0) {
             //            var depused = $(this).children().find("input[id$='txtDepUse']").val();

             //            total = Math.round(total + parseFloat(depused));
                         
             //        }
                     
             //    });

                 
             //    $("#depositSummary").val(total);
             //}
         });
         function validPaymentInsert() {
             var ret = false;
             var hotelbankSel = $("input[name$='hotelbank']:checked").length;
             var bhtbankSel = $("input[name$='bhtbank']:checked").length;
             if (hotelbankSel < 1 || bhtbankSel < 1) {
                 alert("Please Select Hotel Bank OR BHT Bank least One!!");

                 return false;
             }
             else {
                 return true;
             }

             

         }

         function DepositTotal() {
             
             var total = 0;

             $("#gvDeposit tbody tr").filter(function (index) {

                 //if (index > 0) {
                     var depused = $(this).children().find("input[id$='txtDepUse']").val();
                     var check = $(this).children().find("input[id$='chkDepSel']");
                     if (check.is(":checked")) {
                         
                         total = Math.round(total + parseFloat(depused));
                        
                     }
                     
                         
                 //}
                     
             });

                 
             $("#depositSummary").val(total);
         
         }

         function ConfirmOnDelete() {
             if (confirm("Are you sure?") == true)
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h6>Bht Manage Commission : <asp:Literal ID="ltHotelName" runat="server"></asp:Literal></h6>
    <asp:Panel ID="panelPaymentSel" CssClass="tbl_account_block" runat="server" >

    <asp:Literal ID="ltBookingList" runat="server"></asp:Literal>
        <br /><br />

        <asp:Button ID="btnPay" runat="server" Text="Next>>" SkinID="Green"  OnClick="btnPay_Click" OnClientClick="return chkBoxCheck();" /> 
    </asp:Panel>

    <asp:Panel ID="panelPaymentSummary"  CssClass="tbl_account_block" runat="server" Visible="false" ClientIDMode="Static" >
        <asp:Literal ID="ltPaylistSum" runat="server"></asp:Literal>
        <br /><br />

        <h1>Deposit</h1>
        <asp:GridView ID="gvDeposit" DataKeyNames="DepositID" ClientIDMode="Static"  CssClass="tbl_account" GridLines="None" runat="server" EnableModelValidation="false" Width="100%" CellPadding="0" CellSpacing="1" AutoGenerateColumns="false"  EnableTheming="false" OnRowDataBound="gvDeposit_RowDataBound"  ShowFooter="false"  >
            <EmptyDataTemplate>
                <p>No Deposit for this Hotel</p>
            </EmptyDataTemplate>
            <Columns>
                
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHead" runat="server" ClientIDMode="Static" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDepSel"  runat="server" value='<%# Eval("DepositID") %>'  ClientIDMode="Static" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deposit Code">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="lnDepID"  Target="_blank" Text='<%# "DEP" + Eval("DepositID") %>' NavigateUrl='<%# string.Format("/admin/account/account_deposit_detail.aspx?pid={0}", Eval("ProductID")) %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BookingID">
                    <ItemTemplate>
                        <asp:Label ID="lbldepTitleBooking" Text='<%# "เครดิตคงค้างที่ยังรอตัด Booking No. " + "<a href= \"/admin/account/account_booking_detail.aspx?bid="+Eval("BookingID")+"\" target=\"_Blank\" />"+Eval("ClassBookingDetail.BookingHotelId")+"</a> "   %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Customer Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCusNam" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Check-In">
                    <ItemTemplate>
                        <asp:Label ID="lblCheckIn" runat="server" Text='<%# string.Format("{0:dd-MMM-yyyy}", Eval("DateCheckIn")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Check-Out">
                    <ItemTemplate>
                        <asp:Label ID="lblCheckOut" runat="server" Text='<%# string.Format("{0:dd-MMM-yyyy}", Eval("DateCheckout")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Balance">
                    <ItemTemplate>
                        <asp:Label ID="lblDepBalance" runat="server" Text='<%# string.Format("{0:#,##0.00}",Eval("DepositUsed"))  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDepUse" runat="server" ClientIDMode="Static" Text='<%# string.Format("{0:0}",Eval("DepositUsed"))  %>' ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
        <div class="dep_total">
             Deposit Total: <asp:TextBox ID="depositSummary"   runat="server" ClientIDMode="Static" Width="150px"  EnableTheming="false" CssClass="textBoxStyle_color"></asp:TextBox>
        </div>
       
        <br /><br />
        <h1>Hotel Bank Selection</h1>
        <asp:GridView ID="GvHotelBank" DataKeyNames="AccountId" GridLines="None" EnableTheming="false" runat="server" EnableModelValidation="false" AutoGenerateColumns="false"  CssClass="tbl_account" OnRowDataBound="GvHotelBank_RowDataBound"  ClientIDMode="Static" ShowFooter="false"  CellPadding="0" CellSpacing="1"  Width="100%" >
            <EmptyDataTemplate>

                <asp:HyperLink ID="lnedit_hotel_bank" runat="server" Target="_blank" Text="Click to manage Account for this hotel" ></asp:HyperLink>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RadioButton ID="rioHotelBank" runat="server" GroupName="hotelbank" onclick="RadioCheck(this);" />
                        <asp:HiddenField ID="hd_bankId" runat="server" Value ='<%# Eval("BankId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblBankName"  runat="server" Text='<%# Eval("BankTitle") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblAccountName"  runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblAccountNum"  runat="server" Text='<%# Eval("AccountNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblAccountBranch"  runat="server" Text='<%# Eval("AccountBranch") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblAccountType"  runat="server" Text='<%# Eval("AccountTypeTitle") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            
            </asp:GridView>
            
        <br /><br />

        <h1>Bht Bank Selection</h1>
        <asp:GridView ID="GvbhtBank" DataKeyNames="AccountId" GridLines="None"  runat="server" EnableModelValidation="false" AutoGenerateColumns="false"  CssClass="tbl_account" EnableTheming="false" CellPadding="0" CellSpacing="1" Width="100%"   ShowFooter="false" ClientIDMode="Static"   OnRowDataBound="GvbhtBank_RowDataBound" >
            <EmptyDataTemplate>
                <p>Please Contact R&D Team to fix</p>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RadioButton ID="rioBhtbank" runat="server" ClientIDMode="Static" GroupName="bhtbank" onclick="RadioCheck(this);"  />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblbhtBankName"  runat="server" Text='<%# Eval("BankTitle") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblbhtAccountName"  runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblbhtAccountNum"  runat="server" Text='<%# Eval("AccountNum") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblbhtAccountBranch"  runat="server" Text='<%# Eval("AccountBranch") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblbhtAccountType"  runat="server" Text='<%# Eval("AccountTypeTitle") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
            
            </asp:GridView>
        <br /><br />
        <asp:Button ID="btnback" runat="server" Text="<<Back" SkinID="White"  OnClick="btnback_Click"  /> 
        <p style="color:red">** กรุณาตรวจเช็ค ก่อน กด ปุ่มสีเขียว เพราะจะไม่สามารถแก้ไขได้แล้วนะครับ จะต้อง เข้าไป ยกเลิก payment เท่านั้น</p>
        <asp:Button ID="btnPayment" runat="server" Text="Save Payment And Print Page" Font-Size="16px" Width="300px" Height="50px"  SkinID="Green" OnClick="btnPayment_Click"  OnClientClick="return validPaymentInsert();"/>
        
    </asp:Panel>

    <asp:Panel ID="panelCompleted" runat="server" Visible="false">

        <p class="txt_completed">ระบบได้บันทึก รายการ payment เรียบร้อยแล้ว สามารถกด เพื่อ ปริ้นท์ ใบ Pay Slip  <asp:HyperLink ID="lnPrint" runat="server" Text="Print Slip"></asp:HyperLink>
        </p>
    </asp:Panel>
</asp:Content>

