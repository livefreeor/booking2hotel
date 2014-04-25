<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainmenuControl.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Control_MainmenuControl" %>
    

    <asp:Panel ID="panelProductLevel" runat="server">
        <center>
        <asp:Panel ID="panel1" runat="server"> 
        <asp:HyperLink ID="lnkMarket" runat="server">Market</asp:HyperLink> |
        <asp:HyperLink ID="lnkPublickHolidays" runat="server">Public Holidays</asp:HyperLink> |
        <asp:HyperLink ID="lnkFac" runat="server">Facility Manage</asp:HyperLink> |
        <%--<asp:HyperLink ID="lnkLandmark" runat="server">Landmark</asp:HyperLink> | 
        <asp:HyperLink ID="lnkDestination" runat="server">Destination</asp:HyperLink> | 
        <asp:HyperLink ID="lnkLocation" runat="server">Location</asp:HyperLink> |--%>
        </asp:Panel>
        <br />
        </center>
    </asp:Panel>
    <asp:Panel ID="panelProductListLevel" runat="server">
        <center>
        <asp:Panel ID="panelProductLink" runat="server">
        <%--<asp:HyperLink ID="lnkPolicy" runat="server">1 : Policy</asp:HyperLink> | --%>
        <%--<asp:HyperLink ID="lnkOptionList" runat="server"> 2 : Product Option</asp:HyperLink> | --%>
       <%-- <asp:HyperLink ID="lnkPaymentPlan" runat="server">!!!Payment Plan</asp:HyperLink> |--%>
        <%--<asp:HyperLink ID="lnkAnnoucement" runat="server">3 : Annoucement</asp:HyperLink> |--%>
        <%--<asp:HyperLink ID="lnkProductMinStay" runat="server">4 : Minimum Stay</asp:HyperLink> |--%> 
       <%-- <asp:HyperLink ID="lnkConStruction" runat="server">5 : Construction</asp:HyperLink> |--%>
        <%--<asp:HyperLink ID="lnkGalaDinner" runat="server">6 : Gala Dinner</asp:HyperLink> |--%>
        <%--<asp:HyperLink ID="lnkItinerary" runat="server">7 : Itinerary</asp:HyperLink> |--%>
       <%-- <asp:HyperLink ID="lnkPicture" runat="server">8 : Picture</asp:HyperLink> | --%>
        <%--<asp:HyperLink ID="lnkPictureGen" runat="server">** Picture Auto Gen</asp:HyperLink> --%>
        
        </asp:Panel>
        </center>
    </asp:Panel>
    

    <asp:Panel ID="panelOptionLevel" runat="server">
        <center>
        <asp:HyperLink ID="lnkRate" runat="server">2.1 : Rate</asp:HyperLink> |
        <asp:HyperLink ID="lnkWeekday" runat="server">2.2 : Week End</asp:HyperLink> |
        <asp:HyperLink ID="lnkHoliday" runat="server">2.3 : Holiday</asp:HyperLink> | 
        <asp:HyperLink ID="lnkAllotment" runat="server">2.4 : Allotment</asp:HyperLink> |
        <asp:HyperLink ID="lnkPromotion" runat="server">2.5 : Promotion</asp:HyperLink>
    </center>
    </asp:Panel>

    <asp:Panel ID="panelOptionDetailLevel" runat="server">
        <center>
        <asp:HyperLink ID="OptionItinerary" runat="server">3.1 : Itinerary</asp:HyperLink> 
        </center>
    </asp:Panel>