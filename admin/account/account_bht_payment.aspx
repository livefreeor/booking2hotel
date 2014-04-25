<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="account_bht_payment.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_account_bht_payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript" src="../../Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <link href="../../css/account/bhtmanage.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $("input[name$='txtFee']").keyup(function () {
                var total = 0;
                var childTR = $(this).parent().parent().parent().find("tr");
                childTR.each(function () {
                    var fee = $(this).find("input[name$='txtFee']");
                    if (fee.val() != "") {
                        total = Math.round(total + parseFloat(fee.val()));
                    }
                });
                var childTF = $(this).parent().parent().parent().parent().find("tfoot tr td");
                var txtFeetotal = childTF.find("span[id$='lblFeeamount']");
                var spanFeeTotal = childTF.find("input[id$='txtFeeAmount']");

                txtFeetotal.html(total);
                spanFeeTotal.val(total);
                //lblFeeamount
                //txtFeeAmount
            });

           
        });

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

    <div class="tbl_account_block">
        

        <p class="pay_list_head">สรุปรายการจ่ายของ วันที่ <asp:Label runat="server" ID="bolCompleted" ></asp:Label></p>

        <asp:GridView ID="GvBhtAccount" runat="server" CellPadding="0" CellSpacing="0" Width="100%"  DataKeyNames="AccountId" EnableTheming="false" GridLines="None"  EnableModelValidation="false" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false"   OnRowDataBound="GvBhtAccount_RowDataBound" >
            <EmptyDataTemplate>
                <p>No Payment to Pay</p>
            </EmptyDataTemplate>
            
            <Columns>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Label ID="lblAccountTitle" runat="server" CssClass="bht_account_title"></asp:Label>
                        <asp:GridView ID="GvbhtBank" DataKeyNames="PaymentID" GridLines="None"  runat="server" EnableModelValidation="false" AutoGenerateColumns="false"  CssClass="tbl_account" EnableTheming="false" CellPadding="0" CellSpacing="1" Width="100%"   ShowFooter="true" ClientIDMode="Static"   OnRowDataBound="GvbhtBank_RowDataBound"    >
        <HeaderStyle HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Center" />
           <FooterStyle HorizontalAlign="Right" />
            <Columns>
                
                <asp:TemplateField HeaderText="No.">
                     <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                     </ItemTemplate>
                    <FooterTemplate>
                      รวม
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Slip ID" >
                     <ItemTemplate>
                         <asp:HyperLink ID="lnPaySlip" runat="server" NavigateUrl='<%# String.Format("slip_print.aspx?pid={0}&pay={1}", Eval("ProductId"),Eval("PaymentID")) %>' Text='<%# Eval("PaymentID") %>' Target="_blank"></asp:HyperLink>
                        
                     </ItemTemplate>
                    <FooterTemplate>
                         <asp:Label ID="lblTotalCost" runat="server" ></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hotel Name">
                     <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("ProductTitle") %>'></asp:Label>
                     </ItemTemplate>
                    <FooterTemplate>
                         รวม
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                     <ItemTemplate>
                             <asp:Label ID="lblAmount" runat="server" ></asp:Label>
                     </ItemTemplate>
                    <FooterTemplate>
                         <asp:Label ID="lblFeeamount" ClientIDMode="Static" runat="server"  Text="0" ></asp:Label>
                        <asp:TextBox ID="txtFeeAmount" runat="server"  ClientIDMode="Static"   Width="80px" CssClass="textBoxStyle_color txtfee" Text="0"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bank">
                     <ItemTemplate>
                             <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankTitle") %>'></asp:Label>
                     </ItemTemplate>
                    <FooterTemplate>
                        
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Account Name">
                     <ItemTemplate>
                             <asp:Label ID="lblAccountName" runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                     </ItemTemplate>
                    <FooterTemplate>

                    </FooterTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Account Number">
                     <ItemTemplate>
                             <asp:Label ID="lblAccountNum" runat="server" Text='<%# Eval("AccountNum") %>'></asp:Label>
                     </ItemTemplate>
                    <FooterTemplate>

                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch">
                     <ItemTemplate>
                             <asp:Label ID="lblAccountBranch" runat="server" Text='<%# Eval("AccountBranch") %>'></asp:Label>
                     </ItemTemplate>
                    <FooterTemplate>

                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fee">
                     <ItemTemplate>
                            <asp:TextBox ID="txtFee" runat="server" Text="0" Width="80px" CssClass="textBoxStyle_color"></asp:TextBox>
                     </ItemTemplate>
                    <FooterTemplate>

                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Manage">
                     <ItemTemplate>
                            <asp:Button ID="btnCancel" runat="server"  CssClass="btStyleWhite_small" OnClick="btnCancel_Click" CommandArgument='<%# Eval("PaymentID")  %>' Text="Cancel" OnClientClick="return ConfirmOnCancel();" />
                          <asp:Button ID="btnPaid" runat="server"  CssClass="btStyleGreen_small"  OnClick="btnPaid_Click" CommandArgument='<%# Eval("PaymentID")  %>' Text="Paid" OnClientClick="return ConfirmPaid();" />
                     </ItemTemplate>
                    <FooterTemplate>

                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
         </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        

    </div>
</asp:Content>

