<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="stafflist.aspx.cs" Inherits="Hotels2thailand.UI.extranet_stafflist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<link href="../../css/extranet/extranet_style_core.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin:15px 0px 0px 0px;">


 <asp:GridView ID="GvStaffList" runat="server"  EnableModelValidation="false" ShowFooter="false" DataKeyNames="Staff_Id" 
 AutoGenerateColumns="false" CssClass="tbl_acknow" GridLines="None" CellSpacing="1"   EnableTheming="false"   OnRowDataBound="GVstaffListRole_OnRowDataBound" >
 <HeaderStyle CssClass="header_field" /> 
 <RowStyle  BackColor="#ffffff" />
  <Columns>
  <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>
            <asp:Label ID="lblNum" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Full Name">
        <ItemTemplate>
            <asp:Label ID="lblFullname" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Email">
        <ItemTemplate>
            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField  HeaderText="Username">
        <ItemTemplate>
            <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField  HeaderText="Role">
        <ItemTemplate>
            <asp:Label ID="lblrole" runat="server" Text='<%# Bind("AuthorizeTitle") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>
            <asp:CheckBox ID="ChkActive" runat="server" Enabled="false" Checked='<%# Bind("Status") %>' />
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Manage" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>

             <asp:HyperLink ID="lnManageStaff" runat="server" >Manage</asp:HyperLink>
            
        </ItemTemplate>
    </asp:TemplateField>
  </Columns>
 </asp:GridView>

 </div>
</asp:Content>

