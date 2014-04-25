<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Page_Navigator.ascx.cs" Inherits="Hotels2thailand.UI.Controls.Page_Navigator" %>
<style type="text/css">
    .Page_nav_style
    {
         margin:0px;
         padding:0px;
    }
    .Page_nav_style a:hover
    {
        color:#72ac58;
    }
    .Page_nav_prevoius_style
    {
        margin:0px;
        padding:0px;
        float:left;
    }
    .Page_nav_prevoius_style a
    {
        margin:0px;
        padding:3px;
        display:block;
        font-size:12px;
        color:#333333;
    }
    
    .Page_nav_pageList
    {
        margin:0px;
        padding:0px;
        
        float:left;
    }
    .Page_nav_pageList a
    {
        margin:0px 0px 0px 15px;
        padding:3px;
        float:left;
        display:block;
        font-size:12px;
       
    }
    
    
    .Page_nav_pageList_activePage
    {
        margin:0px 0px 0px 15px;
        padding:3px;
        float:left;
        display:block;
        font-size:12px;
        color:#ffffff;
        background-color:#3f5d9d;
    }
    
    .Page_nav_pageList a:hover
    {
        background-color:#3f5d9d;
        color:#ffffff;
        
    }
    .Page_nav_next_style
    {
        margin:0px;
        padding:0px;
        float:left;
    }
    .Page_nav_next_style a
    {
        margin:0px 0px 0px 15px;
        padding:3px;
        display:block;
        font-size:12px;
        color:#333333;
    }
    
</style>
 <asp:Label ID="lblPageNavigator" runat="server"></asp:Label>
