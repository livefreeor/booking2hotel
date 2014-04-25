<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="supplier_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_supplier_supplier_list" Title="Supplier List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
   
    <asp:HyperLink ID="supplierCreate" runat="server" NavigateUrl="supplier_add.aspx"> <asp:Image ID="Image1" ImageUrl="~/images/plus.png" runat="server" /> Add New Supplier</asp:HyperLink>
    <br /><br />
    <div class="product_list_sort_box" style="margin:0px 0px 10px 0px">
    <p>Status</p>
    <p><asp:DropDownList ID="dropStatus" runat="server" OnSelectedIndexChanged="dropStatus_OnSelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="1" Text="Enable" Selected="True"></asp:ListItem>
        <asp:ListItem Value="0" Text="Disable"></asp:ListItem>
    </asp:DropDownList></p>
    <p><span>Advance Search</span>&nbsp;&nbsp;<asp:TextBox ID="txtSearch" runat="server" Width="600px"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnAdvanceSearch" runat="server" Text="Search" SkinID="Blue" OnClick="txtSearch_OnClick" /></p>
    </div>
    <asp:GridView ID="gridSupplier" runat="server" AutoGenerateColumns="False"  EnableModelValidation="True" SkinID="ProductList"  OnRowDataBound="gridSupplier_OnRowDataBound"
       OnPageIndexChanging="gridSupplier_OnPageIndexChanging"  >
        <Columns>
            <asp:TemplateField HeaderText ="No." ItemStyle-Width ="5%" ItemStyle-HorizontalAlign="Center">         
                <ItemTemplate>        
                    <%# Container.DataItemIndex + 1 %>    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText ="Title" ItemStyle-Width ="65%"  ItemStyle-HorizontalAlign="Left">         
                <ItemTemplate> 
                    <asp:HyperLink ID="Hyperlbltitle"  Text='<%# Bind("SupplierTitle") %>' NavigateUrl='<%# String.Format("supplier_add.aspx?supid={0}", Eval("SupplierID")) %>'  runat="server"></asp:HyperLink>       
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText ="Contact" ItemStyle-HorizontalAlign="Center" ItemStyle-Width ="10%">         
                <ItemTemplate>    
                    <asp:HyperLink ImageUrl="~/images/bluehousestaff.gif" NavigateUrl='<%# String.Format("supplier_contact.aspx?supid={0}", Eval("SupplierID")) %>'  runat="server"></asp:HyperLink>    
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText ="Account" ItemStyle-HorizontalAlign="Center" ItemStyle-Width ="10%">         
                <ItemTemplate>    
                    <asp:HyperLink ImageUrl="~/images/account.png" NavigateUrl='<%# String.Format("supplier_account_list.aspx?supid={0}", Eval("SupplierID")) %>' runat="server"></asp:HyperLink>    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText ="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width ="10%">         
                <ItemTemplate>    
                      <asp:Image ID="imgStatus" runat="server" ImageUrl="~/images/true.png"  />
                        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/false.png" Visible='<%# Not Eval("Status") %>' /> --%> 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    

</asp:Content>
