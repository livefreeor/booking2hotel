<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierSelectionControl.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_SupplierSelectionControl" %>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<div class="supplierSeletionBox">
<style type="text/css">
            .supItemList a{ font-weight:normal;}
        </style>
    <center>
    <p>
    <asp:HyperLink ID="linkBTaddSuppWizard" runat="server" NavigateUrl="~/admin/supplier/supplier_add.aspx?wizard=supaddnew" ><asp:Image ID="Image1" ImageUrl="~/images/plus.png" runat="server"  /> if there are no supplier inList Please Click Add New One</asp:HyperLink> </p>
    
        <asp:DataList ID="ListAphabet" runat="server" 
            RepeatColumns="36" RepeatDirection="Horizontal"  HeaderStyle-BackColor="#e6e6e6"  HeaderStyle-CssClass="DlHeaderStyle"
            RepeatLayout="Table"  AlternatingItemStyle-BackColor="#eceff5" ItemStyle-BackColor="#ffffff"  Width="80%"
            OnItemCommand="ListAphabet_Command" ItemStyle-HorizontalAlign="Center" AlternatingItemStyle-HorizontalAlign="Center">
             <HeaderTemplate>Supplier Selection</HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton id="SelectButton" Text='<%# Eval("Value") %>' CommandName="Select" runat="server"/>
            </ItemTemplate>

        </asp:DataList>
            
        
        
        <div class="supItemList" style="margin:20px 0px 0px 100px; padding:0px; float:left;  vertical-align:top;" >
           <p>SupplierList</p>
          <asp:ListBox ID="ListBoxSupplierList" runat="server" Width="350px" Height="150px" BackColor="#fff9d7" CssClass="supItemList"></asp:ListBox>
        </div>
        <div class="btSelect" style=" height:100px;  width:40px; float:left; vertical-align:middle; margin:60px 5px 0px 5px; padding:0px 0px 0px 0px; text-align:center" >
            
            <p style="margin:5px 0px 0px 0px; padding:0px;"><asp:Button ID="selectone" 
                    runat="server" Text=">" SkinID="White_small" onclick="selectone_Click"  Height="60px"/></p>
            <p style="margin:5px 0px 0px 0px; padding:0px;"><asp:Button ID="delone" 
                    runat="server" Text="<" SkinID="White_small" onclick="delone_Click" Height="60px" /></p>
            
        </div>
        <div class="supItemList" style=" margin:20px 0px 0px 0px; padding:0px; float:left;  vertical-align:top;">
            <p>Selected</p>
            <asp:ListBox ID="ListBoxSelected" runat="server" Width="350px" Height="150px" BackColor="#fff9d7"  CssClass="supItemList"></asp:ListBox>
        </div>
        <div style=" clear:both"></div>
        
    </center>
 </div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>