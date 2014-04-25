<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_point.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_config_product_point" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<style type="text/css">
.point_config_bar
{
    margin:0px 0px 20px 0px;
    padding:0px;
    width:100%;
}
.point_config_bar div
{
    margin:0px 0px 0px 15px;
    float:left;
}
.point_config_bar div p
{
     margin:0px;
    padding:0px;
    font-weight:bold;
    font-size:12px;
    
}
.btnSearchs
{
    margin:0px 0px 0px 0px;
    padding:14px 0px 0px 20px;
    
}
.product_list_point
{
    margin:20px 0px 0px 0px;
    padding:0px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="point_config_bar">
    <div>
    <p>Product Category</p>
        <asp:DropDownList ID="dropProductCat" runat="server"></asp:DropDownList>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div>
    <p>Destination</p>
        <asp:DropDownList ID="dropDestination" runat="server" OnSelectedIndexChanged="dropDestination_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </div>
   
    
     
    <div>
     <p>Location</p>
         <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
        <ProgressTemplate>
            <asp:Image ID="imgProGress" runat="server" ImageUrl="~/images/progress.gif" />
        </ProgressTemplate>
      </asp:UpdateProgress>  
        <asp:DropDownList ID="dropLocation" runat="server"></asp:DropDownList>
    </div>
    </ContentTemplate>
    
    </asp:UpdatePanel>
    <div class="btnSearchs" ><asp:Button ID="btnSearch" SkinID="Blue" runat="server" Text="Search" OnClick="btnSearch_OnClick" /></div>
 </div>
 <div style="clear:both"></div>
 <div class="product_list_point">
    <asp:GridView ID="GVProductList" runat="server" DataKeyNames="ProductID"  EnableModelValidation="false" AutoGenerateColumns="false" OnRowDataBound="GVProductList_OnrowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
             <%# Container.DataItemIndex + 1 %> 
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:HyperLink ID="hlProductCode" runat="server" Text='<%# Bind("ProductCode") %>' Font-Bold="true" NavigateUrl='<%# String.Format("~/admin/product/product.aspx?pid={0}&pdcid={1}", Eval("ProductID"),Eval("ProductCategoryID")) %>' Target="_blank" ></asp:HyperLink>
             <%--<asp:Label ID="lblCode" Text='<%# Eval("ProductCode") %>' runat="server"></asp:Label>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <asp:HyperLink ID="hlProducttitle" runat="server" Text='<%# Bind("Title") %>'  Target="_blank" ></asp:HyperLink> 
            <%--<asp:Label ID="lbltitle" Text='<%# Eval("Title") %>' runat="server"></asp:Label>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Hot Hotel" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:RadioButtonList ID="radioIsHot" runat="server"  RepeatDirection="Horizontal">
            <asp:ListItem Text="Hot" Value="True"></asp:ListItem>
            <asp:ListItem Text="normal" Value="False"></asp:ListItem>
            </asp:RadioButtonList>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Hotel Point" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:TextBox ID="txtPoint" runat="server" Width="50px"  Text='<%# Bind("PointPopular") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
         
    </Columns>
    </asp:GridView>
 </div>
 <br />
 <div>
    <asp:Button ID="btnSave" runat="server" Text="Save" SkinID="Green" OnClick="btnSave_Onclick" />
 </div>
</asp:Content>

