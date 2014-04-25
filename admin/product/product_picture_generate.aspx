<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_picture_generate.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_picture_generate" %>
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
     .gHead{ font-size:36px; color:#3f5d9d; font-weight:bold; margin:0px; padding:0px;}
     .gfolderCheck{font-size:28px; color:#111111; font-weight:bold; margin-right:15px; margin:10px 0px 0px 0px; padding:0px}
     .gbtn{ float:right;}
     .gBox{  margin:20px 0px 0px 10px; padding:0px 0px 0px 70px;}
     .gBox a{ font-size:20px; color:#ffffff;}
     .room_check_item{ margin:0px 0px 0px 20px; padding:0px; 
     }
     .room_check_item p{ margin:0px; padding:0px;  text-align:left; font-size:14px; 
     }
     
     .gBox_item{ margin:0px 5px 0px 10px; padding:10px 5px 5px 5px; width:200px; float:left; height:50px; border:2px solid #2c5115; text-align:center; background-color:#72ac58;}
     #HotelCheckBox{ margin:20px 0px 0px 0px; padding:20px ;background:#f2f2f2;
    -webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    border-radius: 10px;
    
    -webkit-box-shadow: 0px 0px 10px #000;
    -moz-box-shadow: 0px 0px 10px #000;
    box-shadow: 0px 0px 10px #000;

 }
     #PictureSourceCheckBox{ margin:20px 0px 0px 0px; padding:20px ; background:#f2f2f2;-webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    border-radius: 10px;
    
    -webkit-box-shadow: 0px 0px 10px #000;
    -moz-box-shadow: 0px 0px 10px #000;
    box-shadow: 0px 0px 10px #000;
}

 #gBox{ margin:20px 0px 0px 0px; padding:20px ; background:#f2f2f2;-webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    border-radius: 10px;
    
    -webkit-box-shadow: 0px 0px 10px #000;
    -moz-box-shadow: 0px 0px 10px #000;
    box-shadow: 0px 0px 10px #000;
}
.pathUpload{margin:0px; padding:0px; font-size:14px;}
.pathUpload_span{margin:0px; padding:0px; font-weight:bold; color:#72ac58;font-size:14px;}
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
           
     <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="txthead" runat="server"></asp:Label></h6>   
    
     <div  id="HotelCheckBox">
        <p class="gbtn"><asp:Button ID="btnCheck" runat="server" SkinID="Green" Text="Check Now!!"  OnClick="btnCheck_OnClick" Width="200px" Height="40px" Font-Size="20px"  /></p>
        <p  class="gHead">Hotel Preparedness</p>
        <p class="gfolderCheck">1. Destination&nbsp;&nbsp;<asp:Image ID="imgDesCheck" runat="server" ImageUrl="~/images/false_b.png" /></p>
        <p class="gfolderCheck">2. Location&nbsp;&nbsp;<asp:Image ID="imgLocCheck" runat="server" ImageUrl="~/images/false_b.png" /></p>
        <p class="pathUpload"><span  class="pathUpload_span">Please Upload To >>>&nbsp;&nbsp;</span><asp:label ID="lblPathUpLoad" runat="server"></asp:label></p>
     </div>

     <div id="PictureSourceCheckBox">
        <p class="gbtn"><asp:Button ID="btnCheck2" runat="server" SkinID="Green" Text="Check Now!!"  OnClick="btnCheck2_OnClick" Width="200px" Height="40px" Font-Size="20px"  /></p>
        <p  class="gHead">Picture Path Summary</p>
        <p class="gfolderCheck">3. Hotel&nbsp;&nbsp;<asp:Image ID="imgHotelCheck" runat="server" ImageUrl="~/images/false_b.png" /> 
        
        <span  class="pathUpload_span">Please Rename Hotel Folder >>>
        </span><span class="pathUpload_span" style=" color:#111111; font-weight:normal;"><asp:Label ID="lblHotelFolder" runat="server"></asp:Label></span></p>
        <p class="gfolderCheck">4. Product&nbsp;&nbsp;<asp:Image ID="imgProductCheck" runat="server" ImageUrl="~/images/false_b.png" /> </p>
        <p class="gfolderCheck">5. Option&nbsp;&nbsp;<asp:Image ID="imgOptionCheck" runat="server" ImageUrl="~/images/false_b.png" /></p>
        <asp:Label ID="lblRoomList" runat="server"></asp:Label>
        <p class="gfolderCheck">6. Construction&nbsp;&nbsp;<asp:Image ID="imgConstructionCheck" runat="server" ImageUrl="~/images/false_b.png" /></p>  
     </div>

    
    <div class="gBox">
            <asp:Panel ID="panelgProduct"  CssClass="gBox_item"  runat="server" Visible="false" >
                <asp:HyperLink ID="gProductLink" runat="server" Text="Product Generate Page" NavigateUrl="~/admin/product/product_picture_generate_product.aspx"></asp:HyperLink>
            </asp:Panel>
            <asp:Panel ID="panelgOption"  CssClass="gBox_item"  runat="server"  Visible="false">
                <asp:HyperLink ID="gOptionLink" runat="server" Text="Option Generate Page" NavigateUrl="~/admin/product/product_picture_generate_option.aspx"></asp:HyperLink>
                
            </asp:Panel>
            <asp:Panel ID="panelgConstruction"  CssClass="gBox_item"  runat="server" Visible="false">
                 <asp:HyperLink ID="gConlink" runat="server" Text="Contruction Generate Page" NavigateUrl="~/admin/product/product_picture_generate_construction.aspx"></asp:HyperLink>
            </asp:Panel>
    </div>
   
    <div style="clear:both"></div>
        
        
</asp:Content>
