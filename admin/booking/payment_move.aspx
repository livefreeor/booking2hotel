<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="payment_move.aspx.cs" Inherits="Hotels2thailand.UI.admin_payment_move" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
   
    <script type="text/javascript" language="javascript" src="../../Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
    
    <script type="text/javascript" src="../../Scripts/darkman_utility.js"></script>
    <style type="text/css">
    
    .booking_paymeny_move_box
    {
        margin:0px;
        padding:10px;
         background:#ebf0f3;
        border: 1px solid #cccccc;
    }
    
    
    .payment_From, .payment_To
    {
        margin:15px 0px 0px 0px;
        padding:10px;
       
        background:#f7f7f7;
        border: 1px solid #d6d6d6;
    }
     .payment_From p, .payment_To p
     {
         margin:0px;
         padding:0px;
         font-size:14px;
         font-weight:bold;
     }
     .aff_head
     {
         color:#72ac58;
     }
     .payment_empty
     {
          margin:0px;
          padding:0px;
          border:1px solid #cccccc;
     }
     .payment_empty p
     {
          color:#4f6dad;
          font-size:14x;
          font-weight:bold;
     }
     .payment_head
     {
         margin:0px 0px 10px 0px;
         padding:0px;
     }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="booking_paymeny_move_box" >
    <table>
     <tr>
     <td>Booking From</td>
     <td><asp:TextBox ID="txtBookingFrom" runat="server" ></asp:TextBox>
      <asp:RequiredFieldValidator ID="reqBookingFrom" runat="server" ControlToValidate="txtBookingFrom" Display="Dynamic" Text="*required" ForeColor="Red"></asp:RequiredFieldValidator>
     </td>
     </tr>
     <tr><td>Booking To</td>
     <td><asp:TextBox ID="txtBookingTo" runat="server" ></asp:TextBox>
     <asp:RequiredFieldValidator ID="reqBookingTo" runat="server" ControlToValidate="txtBookingTo" Display="Dynamic" Text="*required" ForeColor="Red"></asp:RequiredFieldValidator>
     </td>
     </tr>
     <tr><td><asp:Button ID="Display_Payment" runat="server" SkinID="Green" OnClick="Display_Payment_OnClick"  Text="Display Payment" /></td></tr>
    </table>
  </div>   

     <asp:Panel  ID="panelPayment_From" runat="server"  CssClass="payment_From" Visible="false">
        <asp:Panel ID="panelHeadrom" runat="server" Visible="false" CssClass="payment_head">
        <p>BookingId: <asp:Label ID="lblBookingIdFrom" runat="server"></asp:Label>&nbsp;&nbsp;[<span class="aff_head">Affiliate:<asp:Image ID="imgAffFrom" runat="server" /></span>]</p>
        </asp:Panel>
        <asp:GridView ID="gridPaymentFrom" runat="server" SkinID="ProductList" OnRowDataBound="gridPaymentFrom_OnrowDataBound"  ShowFooter="false" AutoGenerateColumns="false" EnableModelValidation="false" DataKeyNames="PaymentId">
        <EmptyDataTemplate>
         <div class="payment_empty">
          <p>No Payment</p>
         </div>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField  HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                <asp:CheckBox ID="chkboxSelect" runat="server" ClientIDMode="Static" />
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                  <%# Container.DataItemIndex + 1 %> 
             </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Amount"  HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:#,0}") %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Confirm Payment"  HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                <asp:Label ID="lblIsPayment" runat="server"></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Confirm Settle"  HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                <asp:Label ID="lblIsSettle"  runat="server"></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
       
     </asp:Panel>
     
     <asp:panel ID="panelPaymeny_to" runat="server"  CssClass="payment_To" Visible="false">
      <asp:Panel ID="panelBookingTo" runat="server" Visible="false" CssClass="payment_head">
     <p>BookingId: <asp:Label ID="lblBookingIdTo" runat="server"></asp:Label>&nbsp;&nbsp;[<span class="aff_head">Affiliate:<asp:Image ID="imgaffTo" runat="server" /></span>]</p>
     </asp:Panel>
      <asp:GridView ID="gridPaymentTo" runat="server" SkinID="ProductList" OnRowDataBound="gridPaymentTo_OnrowDataBound" EnableModelValidation="false" ShowFooter="false" AutoGenerateColumns="false" DataKeyNames="PaymentId">
      <EmptyDataTemplate>
         <div class="payment_empty">
          <p>No Payment</p>
         </div>
        </EmptyDataTemplate>
       <Columns>
            <asp:TemplateField   HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                <asp:CheckBox ID="chkboxSelect" runat="server" ClientIDMode="Static" Enabled="false" />
             </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                  <%# Container.DataItemIndex + 1 %> 
             </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField  HeaderText="Amount"  HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:#,0}") %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Confirm Payment"  HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                <asp:Label ID="lblIsPayment" runat="server"></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Confirm Settle"  HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
                <asp:Label ID="lblIsSettle"  runat="server"></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
        </Columns>
      </asp:GridView>
     
     </asp:panel>

     <asp:Panel ID="panelbtnMove" runat="server" Visible="false">
     <div style=" text-align:center; margin:15px 0px 0px 0px;">
      <asp:Button ID="btnMoveMoney" runat="server" OnClick="btnMoveMoney_OnClick" Text="Move Now" SkinID="Green" Width="150px" Font-Size="14px" Height="50px" />
     </div>
     </asp:Panel>
</asp:Content>

