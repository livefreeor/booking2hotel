<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="sales_manage.aspx.cs" Inherits="Hotels2thailand.UI.admin_account_sales_manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/account/sales.css" type="text/css" rel="Stylesheet" />
    <link  href="../../css/productstyle3.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="left">
        <h4><img src="../../images/content.png"  alt="image_topic" />Sales</h4>
        <asp:GridView ID="gvSales" runat="server" ShowFooter="false"  ShowHeader="false" SkinID="Nostyle"  AutoGenerateColumns="false" DataKeyNames="SaleId">

            <Columns>
                <asp:TemplateField>
                     <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="hlSales" runat="server" NavigateUrl='<%# "sales_manage.aspx?sal="  + Eval("SaleId") %>' Text='<%# Bind("SaleName") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="right">
        <asp:Label ID="lblSaleName" runat="server" CssClass="sale_name"></asp:Label>
            <div id="status">
        <table>
         <tr><td>Name: </td>
         <td><asp:TextBox ID="txtSalesName" Width="450" runat="server"></asp:TextBox></td>

         </tr>
         
         <tr>
         <td>Commission Type: </td>
         <td><asp:DropDownList ID="dropComType" runat="server" Width="450" ></asp:DropDownList></td>
         </tr>
            <tr>
         <td><strong>Commission Value </strong></td>
         <td><asp:TextBox ID="txtComval" Width="200" runat="server" BackColor="#faffbd"></asp:TextBox></td>
         </tr>
         
          <tr><td>Phone </td>
         <td><asp:TextBox ID="txtPhone" runat="server" Width="450"></asp:TextBox><span style=" font-size:11px; color:Green"> * Require!</span></td>
         </tr>
             <tr><td>Fax </td>
         <td><asp:TextBox ID="txtFax" runat="server" Width="450"></asp:TextBox><span style=" font-size:11px; color:Green"> * Require!</span></td>
         </tr>
            <tr><td>Email </td>
         <td><asp:TextBox ID="txtmail" runat="server" Width="450"></asp:TextBox><span style=" font-size:11px; color:Green"> * Require!</span></td>
         </tr>
        
         <tr>
         <td>Comment: </td>
         <td><asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Rows="8" Width="450" ></asp:TextBox></td>
         </tr>
         <tr>
         <td colspan="2" align="center"><asp:Button ID="btnSaveStatus" runat="server" Text="Save" SkinID="Green" OnClick="btnSaveStatus_Click"  /></td>
         </tr>
        </table>
     </div>
    </div>

</asp:Content>

