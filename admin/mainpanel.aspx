<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="mainpanel.aspx.cs" Inherits="Hotels2thailand.UI.mainpanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="rightpan">
     
    	<a href="product/product-list.aspx?pdcid=29">
     	<div class="panelitem">
       		<p  style="background:url(../images/iconset.jpg) 0px 0px no-repeat;" class="iconset">
            </p>
        	<p class="paneltitle">Product List</p>
            <p class="paneldes">Manage all product </p>
        </div>
        </a>
        
        <asp:Panel ID="panelRd" runat="server">

        <a href="rd/rd_main.aspx">
        <div class="panelitem">
        	<p  style="background:url(../images/iconset.jpg) -60px -240px no-repeat;" class="iconset">
            </p>
        	<p class="paneltitle">R&D Setting</p>
            <p class="paneldes">Manage all product </p>
        </div>
        </a>
        </asp:Panel>

        <a href="booking/booking_list.aspx">
        <div class="panelitem">
        	<p  style="background:url(../images/iconsetnew.png) -60px 0px no-repeat;" class="iconset">
            </p>
        	<p class="paneltitle">ORDER CENTER(BHT Manage)</p>
            <p class="paneldes">Manage booking from booking engine BHT manage </p>
        </div>
        </a>
         <a href="booking/booking_list_b2b.aspx">
        <div class="panelitem">
        	<p  style="background:url(../images/iconsetnew.png) -60px 0px no-repeat;" class="iconset">
            </p>
        	<p class="paneltitle">ORDER CENTER(B2B)</p>
            <p class="paneldes">Manage booking from booking engine BHT manage with B2b</p>
        </div>
        </a>
        <asp:Panel ID="panel_account" runat="server">
        <a href="account/account_bookings_list.aspx">
        <div class="panelitem">
        	<p  style="background:url(../images/iconset.jpg) -60px -240px no-repeat;" class="iconset">
            </p>
        	<p class="paneltitle">Account</p>
            <p class="paneldes">Manage all Account of Booking Engine</p>
        </div>
        </a>
        </asp:Panel>

        <%--<a href="extranet/extranethotelList.aspx">
        <div class="panelitem">
        	<p  style="background:url(../images/iconset.jpg) -60px -240px no-repeat;" class="iconset">
            </p>
        	<p class="paneltitle">Booking Engine Staff setup And Hotel Manage</p>
            <p class="paneldes">Manage hotel for booking engine</p>
        </div>
        </a>--%>
        <a href="review/review_admin.aspx">
        <div class="panelitem">
        	<p  style="background:url(../images/iconset.jpg) -60px -240px no-repeat;" class="iconset">
            </p>
        	<p class="paneltitle">Review</p>
            <p class="paneldes">Manage Reviews all product </p>
        </div>
        </a>
        <div style="clear:both;"></div>
    </div>
    <div style="clear:both;"></div>
    
</asp:Content>

