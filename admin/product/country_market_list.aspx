<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="country_market_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_country_market_list" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p><asp:Image ID="imgplus" runat="server" ImageUrl="~/images/plus.png" />&nbsp;<asp:HyperLink ID="imgLinkNewMarket" runat="server" Text="Add New Market"></asp:HyperLink></p>
    <asp:GridView ID="GVMarketList" runat="server" EnableModelValidation="false" AutoGenerateColumns="false" ShowFooter="false"  DataKeyNames="Key" SkinID="ProductList" >
        <Columns>
            <asp:TemplateField  HeaderText="No." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Market" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="90%">
                <ItemTemplate>
                   &nbsp; &nbsp;<asp:HyperLink ID="linkMarget" runat="server" Text='<%# Eval("Value") %>' NavigateUrl='<%# String.Format("~/admin/product/country_market.aspx?mrid={0}", Eval("Key")) + this.AppendCurrentQueryString() %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    
    
</asp:Content>
