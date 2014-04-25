<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_policy.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_product_policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h6><asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
    <br />
    <asp:HyperLink ID="lnkCreate" runat="server" NavigateUrl="product_policy_add.aspx"><asp:Image ID="imgPlus" ImageUrl="~/images/plus.png" runat="server" /> Add New Policy</asp:HyperLink> 
    <br />
  <asp:Panel ID="panelPolicyCatSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Policy Category</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <asp:DropDownList ID="dropPolicyCat" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="dropPolicyCat_OnSelectedIndexChanged"></asp:DropDownList>
            
   </asp:Panel>
   <asp:Panel ID="panelPolicyList" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Policy List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
         <asp:GridView ID="GVpolicyCat" runat="server" ShowFooter="false" ShowHeader="false" DataKeyNames="Key" AutoGenerateColumns="false" OnRowDataBound="GVpolicyCat_OnRowDataBound" SkinID="Nostyle">
            <EmptyDataRowStyle   CssClass="alert_box" />
            <EmptyDataTemplate>
                       <div class="alert_inside_GridView">
                           <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                           <p class="alert_box_head">No Payment Plan Record </p>
                           <p  class="alert_box_detail">Please select at least one.</p>
                       </div>
            </EmptyDataTemplate>
            <Columns>
            
                <asp:TemplateField>
                    <ItemTemplate>
                        <h4><asp:Label ID="lblCatTitle" runat="server" Text='<%# Bind("Value") %>'></asp:Label></h4>
                        <asp:GridView ID="GVpolicyList" runat="server" ShowFooter="false" ShowHeader="false" AutoGenerateColumns="false" DataKeyNames="PolicyID" SkinID="NoSwapBackcolor" OnRowDataBound="GVpolicyList_OnRowDataBound">
                            <Columns>
                                <asp:TemplateField  ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                       <p style=" font-weight:bold; color:#3b59aa; margin:0px; padding:0px;"><asp:Label ID="lblNumpolicy" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>  </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="90%">
                                    <ItemTemplate>
                                    &nbsp;&nbsp;<asp:HyperLink ID="hkTitle" runat="server" Text='<%# Bind("Title") %>' NavigateUrl='<%# String.Format("~/admin/product/product_policy_add.aspx?polid={0}", Eval("PolicyID")) %>' ></asp:HyperLink>
                                        <asp:Label ID="lblPolicyPeriod" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
         </asp:GridView>
            
   </asp:Panel>
</asp:Content>


