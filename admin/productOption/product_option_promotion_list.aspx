<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_option_promotion_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_option_promotion_list" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
    <script type="text/javascript" src="../../scripts/darkman_ui_uitility.js"></script>
    <style type="text/css">
    #tooltip 
    {
        width:500px;
        padding: 5px;
        background-color:#ffffff;
	    border-width:3px; border-color:#3b5998;
	    border-style:solid;
        text-align: left;
    
    }

    span.tip {
        border-bottom: 1px solid #eee;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script language="javascript" type="text/javascript">
      $(document).ready(function () {
          
      
      });
      function getOptionActive(proId,e) {
          tooltip('<img src="../../images/progress_b.gif">',e);
          //var checkCondition = document.getElementById(div);

          //checkCondition.style.display = (checkCondition.style.display == "none") ? "block" : "none";
          $.get("ajax_promotion_condition_list.aspx?proId=" + proId, function (data) {
              $("#tooltip").html(data);
          });

      }
        
  </script>
   <span></span>
   <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="txthead" runat="server"></asp:Label></h6>
     <p style=" margin:10px 0px 0px 0px; padding:0px 0px 0px 0px; width:100%; text-align:right">
    <asp:HyperLink ID="lnkCreate" runat="server" ><asp:Image ID="imgPlus" ImageUrl="~/images/plus.png" runat="server" /> Add New Promotion</asp:HyperLink>
   </p>
    
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     
     <ContentTemplate>
    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel" style="margin:0px 0px 0px 0px;padding-top:0px; " >
        <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Supplier Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        
        <asp:DropDownList ID="dropSupplier" runat="server" OnSelectedIndexChanged="dropSupplier_OnSelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
        
    </asp:Panel>
     
    <asp:Panel ID="panel1" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Promotion List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:HiddenField ID="hdStatus" runat="server"></asp:HiddenField>
           <div id="status_bar">
            <asp:LinkButton ID="lnActive" runat="server" Text="Active" OnClick="lnActive_OnClick"></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
            <asp:LinkButton ID="lnInactive" runat="server" Text="Inactive" OnClick="lnInactive_OnClick"></asp:LinkButton>
           </div>
         <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1" DynamicLayout="true">
         <ProgressTemplate>
            <asp:Image ID="imgProgress" runat="server" ImageUrl="~/images/progress_b.gif" />
         </ProgressTemplate>
         </asp:UpdateProgress>
    
    <asp:GridView ID="GVPromotionList" ClientIDMode="Static" runat="server" EnableModelValidation="false" ShowFooter="false" AutoGenerateColumns="false" DataKeyNames="PromotionId"  OnRowDataBound="GVPromotionList_OnRowDataBound">
        <EmptyDataRowStyle   CssClass="alert_box" />
                    <EmptyDataTemplate>
                         <div class="alert_inside_GridView" >
                               <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                               <p class="alert_box_head">There are no Promotion</p>
                               <p class="alert_box_detail">Insert New One</p>
                        </div>
                 </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Programe" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="61%" >
                <ItemTemplate>
                    <div style=" margin:0px; padding:0px; position:relative">
                    &nbsp;&nbsp;<asp:HyperLink ID="hlTitle" runat="server" Text='<%#Bind("Title") %>' NavigateUrl='<%# String.Format("~/admin/productOption/product_option_promotion.aspx?proId={0}", Eval("PromotionId")) + this.AppendCurrentQueryString() %>'></asp:HyperLink>
                        <%--<div id="optionproactive<%# Eval("PromotionId") %>" style=" display:none; margin:0px; padding:10px; width:600px; height:300px; border:1px solid #000000; background:#ffffff; position:absolute; top:15px; left:7px; z-index:50">
                                <asp:Image ID="imgprogress" runat="server" ImageUrl="~/images/progress_b.gif" />
                        </div>--%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Submit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <asp:Label ID="lblDateSubmit" runat="server" Text='<%# Bind("DateSubmit","{0:d-MMMM-yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Effective From   -   To" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="22%">
                <ItemTemplate>
                    <asp:Label ID="lblDateActive" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Period Stay From   -   To" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="22%">
                <ItemTemplate>
                    <asp:Label ID="lblDateStay" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtnStatus" runat="server" OnClick="imgbtnStatus_StatusUPdate" CommandArgument='<%# Eval("PromotionId") + "," + Container.DataItemIndex %>' />
                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                                TargetControlID="imgbtnStatus"  DisplayModalPopupID="ModalPopupExtender2" />
                                                                <br />
                                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                                                TargetControlID="imgbtnStatus" PopupControlID="Panel3" 
                                                                OkControlID="ButtonOks" 
                                                                CancelControlID="ButtonCancels" 
                                                                BackgroundCssClass="modalBackground"  />
                                                                <asp:Panel ID="Panel3" runat="server"  style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                                                                <p style="margin:0px; padding:0px; text-align:left; width:100%;  font-weight:bold; color:Black">Are you sure to Update Status</p>
                                                                <div style="text-align:right;margin:10px 0px 0px 0px; padding:0px;">
                                                                        <asp:Button ID="ButtonOks" runat="server" Text="OK"  SkinID="Green_small" />
                                                                        <asp:Button ID="ButtonCancels" runat="server" Text="Cancel" SkinID="White_small" />
                                                                </div>
                                                                </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
     
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
        

    
        
    
</asp:Content>
