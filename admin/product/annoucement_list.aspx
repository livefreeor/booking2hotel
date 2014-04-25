<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="annoucement_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_annoucement_list" %>
<%@ Register Src="~/Control/DatepickerCalendar.ascx" TagName="datePicker" TagPrefix="Product" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Lang_Annc_Content_Box.ascx" TagName="AnncLang_Box" TagPrefix="Product" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="product_announcement">
    <div class="product_announcement_left">
        <asp:Panel ID="panelAnnouncement_Add" CssClass="productPanel" runat="server"  >
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Insert Box</h4>
        <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
        <asp:TextBox ID="txtTitle"  runat="server" Width="200px"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server"  Text="Save" SkinID="Green_small" OnClick="btnSave_OnClick" />
        </asp:Panel>
        <asp:Panel ID="panelAnnList" CssClass="productPanel" runat="server"  >
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Announcement List</h4>
        <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
            <asp:GridView ID="GridAnnoucement" runat="server" AutoGenerateColumns="False"  EnableModelValidation="True"   DataKeyNames="AnnoucementID"  ShowHeader="false"  SkinID="Nostyle" 
             OnRowDataBound="GridAnnoucement_OnRowDataBound" >
            <EmptyDataRowStyle   CssClass="alert_box" />
            <EmptyDataTemplate>
                      <div class="alert_inside_GridView">
                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                       <p class="alert_box_head">No Payment Plan Record </p>
                       <p  class="alert_box_detail">Please select at least one.</p>
                     </div>
                </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText ="Title" ItemStyle-Width ="20px">         
                    <ItemTemplate>
                        <div class="product_announcement_left_Gv_item">
                            <asp:Image ID="imgBtGreen"  runat="server" ImageUrl="~/images/greenbt.png" />&nbsp; 
                            <asp:HyperLink ID="lnkTitle" runat="server" Text='<%# Bind("Title") %>' NavigateUrl='<%# String.Format("~/admin/product/annoucement_list.aspx?ancid={0}", Eval("AnnoucementID")) + this.AppendCurrentQueryString() %>' ></asp:HyperLink> 
                            <a href="javaScript:showDiv('anc<%# Eval("AnnoucementID") %>')">
                            <asp:Image ID="imgEdit" runat="server"  ImageUrl="~/images/edit.png" ImageAlign="Right" />
                            </a>
                        
                        <div id="anc<%#Eval("AnnoucementID") %>" style="display:none; margin:5px 0px 0px 0px; padding:0px;">
                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' Width="250px"></asp:TextBox>
                        <p style=" margin:2px 0px 0px 0px; padding:0px;"></p>
                        <asp:Button ID="tbnSave" runat="server" Text="Save" SkinID="Green_small" CommandArgument='<%# Eval("AnnoucementID") + "," + Container.DataItemIndex %>' CommandName="ancSave" OnClick="AnncBtn_Cilck" />
                        <asp:Button ID="tbnDis" runat="server" Text="Disable" SkinID="White_small" CommandArgument='<%# Eval("AnnoucementID") + "," + Container.DataItemIndex %>' CommandName="ancDis" OnClick="AnncBtn_Cilck" />
                        </div>
                        </div> 
                    </ItemTemplate>
                </asp:TemplateField>
                
        </Columns>
        </asp:GridView>

        </asp:Panel>
    
    </div>

    
    <div class="product_announcement_right">
        <asp:Panel ID="screenBlock" runat="server" CssClass="annc_screen_block" Visible ="false">
            <div class="alert_box"  style="margin:150px 0px 0px 50px;width:80%"" >
                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                       <p class="alert_box_head">No Payment Plan Record </p>
                       <p  class="alert_box_detail">Please select at least one.</p>
            </div>
            <%--<div style="width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px; z-index:52; margin:150px 0px 0px 150px;  ">
                 <p style="margin:0px; padding:0px; text-align:left; width:100%;  font-weight:bold; color:Black">PLEASE SELECT ONE  LEFT PAN BEFORE</p>
                 
            </div>--%>
        </asp:Panel>
        <p><asp:Label ID="lblAnnouncementActive" runat="server"></asp:Label></p>

        <asp:Panel ID="panelDateRange" CssClass="productPanel" runat="server"  >
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Date Range</h4>
        <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />

            <Product:datePicker ID="dDatePicker" runat="server"  /><br />

            <asp:Button ID="SaveDateRange" runat="server" Text="Save" SkinID="Green" OnClick="SaveDateRange_OnClick" />

        </asp:Panel>

        <asp:Panel ID="PanelLangContent" CssClass="productPanel" runat="server"  >
        <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Content Language</h4>
        <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
            <Product:AnncLang_Box ID="controlLang"  runat="server" />
        </asp:Panel>
        
    </div>
    <div style="clear:both"></div>
    </div>
    
   
</asp:Content>
