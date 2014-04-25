<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" Theme="hotels2theme" AutoEventWireup="true" CodeFile="currency_edit.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_currency_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="border: solid #627aad 1px; width: 100%;">
  <tr>
    <td valign="top">
	<table border="0" align="center">
		<tr>
            <td>
		        <strong>Currency Reference</strong>
	            <li><a href="http://www.kasikornbank.com/TH/RatesAndFees/ForeignExchange/Pages/ForeignExchange.aspx">Kasikorn Bank</a></li>
	            <li><a href="http://www.xe.com/">Xe.com</a></li>
	        </td>
	    </tr>

	  <tr>
		<td><br /><font color="#FF6600"><strong>Period List </strong></font></td>
	  </tr>
	  <tr>
            <td>
            <div style="overflow:scroll; height:500px;">
                <asp:Label runat="server" ID="period_list"></asp:Label>
                </div>
	        </td>
      </tr>
	  <tr>
		<td>&nbsp;</td>
	  </tr>
	</table>
	</td>
    <td valign="top">
        <asp:GridView ID="GVCurrency" runat="server" AutoGenerateColumns="False"  DataKeyNames="CurrencyID" EnableModelValidation="false" >
            <Columns>
                <asp:TemplateField HeaderText ="No." ItemStyle-Width ="20px">         
                    <ItemTemplate>        
                        <%# Container.DataItemIndex + 1 %>    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText ="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText ="Code">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                 
                 <asp:TemplateField HeaderText ="Prefix">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPrefix" runat="server" Text='<%# Bind("Prefix") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField> 
                   
               <%-- <asp:BoundField DataField="CurrencyID" HeaderText="CurrencyID" 
                    SortExpression="CurrencyID" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Prefix" HeaderText="Prefix" 
                    SortExpression="Prefix" />
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                <asp:CheckBoxField DataField="Status" HeaderText="Status" 
                    SortExpression="Status" />--%>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnSaveEdit" runat="server" Text="Update" OnClick="btnSaveEdit_OnClick" SkinID="Green" />





	    <%--<asp:ObjectDataSource ID="ObjectCurrency" runat="server" 
            SelectMethod="GetCurrencyById" TypeName="Hotels2thailand.Booking.Currency">
            <SelectParameters>
                <asp:Parameter Name="intCurrencyId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
	</td>
  </tr>
</table>
</asp:Content>