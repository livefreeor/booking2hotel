<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="review_site_admin_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_review_site_admin_list"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/reviewstyle.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../css/jquery.fastconfirm.css"/>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
  <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
	<script src="../../Scripts/jquery.fastconfirm.js" type="text/javascript"></script>
     <script type="text/javascript" src="../../Scripts/darkman_utility.js"></script>
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/javascript">
       $(document).ready(function () {

           
           $("#site_review_list tr:even").css({ "background-color": "#ffffff", "color": "#3f5d9d" });
           $("#site_review_list tr:odd").css({ "background-color": "#eceff5", "color": "#3f5d9d" });

       });
       
      
   </script>
   
    <asp:Label ID="lblresult" runat="server"></asp:Label>

</asp:Content>

