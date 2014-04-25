<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_picture_generate_product.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_picture_generate_product" %>
<%@ Register Src="~/Control/DatepickerCalendar-single.ascx" TagName="datePicker" TagPrefix="Product" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
    <style type="text/css">
     .gHead{ font-size:34px; color:#3f5d9d; font-weight:bold; margin:0px; padding:0px;}
     .gfolderCheck{font-size:28px; color:#111111; font-weight:bold; margin-right:15px;}
     .gbtn{ position:absolute; top:220px; left:500px;}
     .gBox{}
     .gBox a{ font-size:20px; color:#ffffff;}
     .gBox_item{ margin:0px 5px 0px 10px; padding:10px 5px 5px 5px; width:200px; float:left; height:50px; border:2px solid #2c5115; text-align:center; background-color:#72ac58;}
     .gpictype{}
     .gpicList{}
     .pic_item_gen
     {
         display:block;
         float:left;
         padding:5px;
         margin:0px 0px 0px 0px;
     }
     .pic_box_gen
     {
         padding:0px 0px 10px 0px;
         margin:0px 0px 0px 0px;
          border-bottom:1px solid #cccccc;
           background-color:#f2f2f2;
     }
     .gpictype{ margin:10px 0px 0px 0px; padding:0px; font-weight:bold; font-size:14px;}
     .ItemEmpty{margin:10px 0px 0px 0px; padding:50px; background-color:#f2f2f2;  -webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    border-radius: 10px; -webkit-box-shadow: 0px 0px 10px #000;
    -moz-box-shadow: 0px 0px 10px #000;
    box-shadow: 0px 0px 10px #000;}
     .ItemEmpty p{margin:10px 0px 0px 0px; padding:0px; font-size:14px; color:#3f5d9d; font-weight:bold;}
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

    </script>
           
     <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="txthead" runat="server"></asp:Label></h6>   
    <asp:Panel ID="panelinsert" runat="server" CssClass="productPanel">
    <p  class="gHead">Product Picture Generate Source  <asp:Button ID="GenPic" runat="server" Text="RENAME & GENERATE NOW!!" OnClick="GenPic_ONclick"  SkinID="Green"   Width="250px" Height="35px" Font-Size="14px" /></p>

    
    <p class="gpictype">OverView</p>
    <div id="gpicListOverView" class="pic_box_gen">
        <asp:Label ID="lblOver" runat="server"></asp:Label>
        <div style="clear:both"></div>
    </div>
    <p class="gpictype">Product_Browse</p>
    <div id="gpicListPB"  class="pic_box_gen">
        <asp:Label ID="lblproduct_browse" runat="server"></asp:Label>
        <div style="clear:both"></div>
    </div>
    <p class="gpictype">Feature_Hotel</p>
    <div id="gpicListFH"  class="pic_box_gen">
        <asp:Label ID="lblfeature_hotel" runat="server"></asp:Label>
        <div style="clear:both"></div>
    </div>
    <p class="gpictype">Popular_Small</p>
    <div id="gpicListPs"  class="pic_box_gen">
        <asp:Label ID="lblpopular_small" runat="server"></asp:Label>
        <div style="clear:both"></div>
    </div>
    <p class="gpictype">Popular_Larg</p>
    <div id="gpicListPl"  class="pic_box_gen">
        <asp:Label ID="lblpopular_large" runat="server"></asp:Label>
        <div style="clear:both"></div>
    </div>
    <p class="gpictype">Thumb&Larg</p>
    <div id="gpicListTl"  class="pic_box_gen">
        <asp:Label ID="lbllarge" runat="server"></asp:Label>
        <div style="clear:both"></div>
    </div>
     <p class="gpictype">Thumb&Larg</p>
    <div id="gpicListT2"  class="pic_box_gen">
        <asp:Label ID="lblthumb" runat="server"></asp:Label>
        <div style="clear:both"></div>
    </div>
        
        
    </asp:Panel>
    <asp:Panel ID="panelEmpty" runat="server" Visible="false" CssClass="ItemEmpty">
    <p>No Picture In Folder Or Generate Completed</p>
    </asp:Panel>
    
        
   
   
</asp:Content>
