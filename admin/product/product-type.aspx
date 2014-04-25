<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product-type.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_product_type" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/ProductTypeLangBox.ascx" TagName="ProductTypeLangBox" TagPrefix="Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <h6><asp:Label ID="lblhead" runat="server"></asp:Label></h6>
    <div class="Gvproducttype">
    <div class="productTypeCat">
          <h2> Product Category</h2>
        <asp:DropDownList ID="DropProductCat" runat="server" 
        DataSourceID="ObjdropProductCat" DataTextField="Value" DataValueField="Key"  
        AutoPostBack="True"></asp:DropDownList>
    <asp:ObjectDataSource ID="ObjdropProductCat" runat="server" 
        SelectMethod="GetProductCategory" TypeName="Hotels2thailand.Production.ProductCategory">
    </asp:ObjectDataSource>
    </div>
      
    <div class="insertproductType" >
                     <h2><asp:Image ID="Image1" ImageUrl="~/images/plus.png" runat="server" /> Create New ProductType</h2>
                     <asp:TextBox ID="txtDep" runat="server"></asp:TextBox>
                     <asp:Button ID="btDepinsert" Text="Add" SkinID="Green" runat="server"  ValidationGroup="txtDepG" /> 
                     
                </div>
    <div class="productTypeList">
    <asp:GridView ID="GvProductType" runat="server" AutoGenerateColumns="False" ShowFooter="false" ShowHeader="false"
        DataSourceID="ObjGvProductType" EnableModelValidation="True" DataKeyNames="Key"  SkinID="Nostyle">
        <Columns>
            <asp:TemplateField>
               <ItemTemplate>
               <asp:Image ID="imgDot" runat="server" ImageUrl="~/images/dot.png" />
                <asp:HyperLink Id="lblTypeTitle" runat="server" NavigateUrl='<%# String.Format("product-type.aspx?ptid={0}", Eval("Key")) %>' 
                Text='<%# Bind("Value") %>'>
                </asp:HyperLink>
               </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ObjGvProductType" runat="server" 
        SelectMethod="getProducttypeListByProductCat" 
        TypeName="Hotels2thailand.Production.ProductType">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropProductCat" Name="bytPcat" 
                PropertyName="SelectedValue" Type="Byte"  />
        </SelectParameters>
    </asp:ObjectDataSource>
    
</div>

    <div class="insertproducttypename">
        <Product:ProductTypeLangBox ID="controlPTypeLangBox" runat="server"></Product:ProductTypeLangBox>
    </div>
    <div style=" clear:both"></div>
</asp:Content>

