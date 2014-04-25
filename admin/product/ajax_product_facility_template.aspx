<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax_product_facility_template.aspx.cs" Inherits="Hotels2thailand.UI.admin_ajax_product_facility_template" EnableViewState="false" EnableViewStateMac="false" EnableEventValidation="false" EnableSessionState="False" %>

    
    <form id="Location_insert" runat="server">
    <div class="formbox">
    
    <p class="formbox_head">Insert Product Location</p>
    <asp:CheckBoxList ID="location_list" runat="server"  RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="checkBox_list"  ClientIDMode="Static" >
        
    </asp:CheckBoxList>
    <p class="formbox_buttom"><input type="button" value="Save" onclick="InsertTemplateSave();"  class="btStyleGreen" />&nbsp;<input type="button" value="Cancel" onclick="DarkmanPopUp_Close();" class="btStyleWhite" style=" width:80px" /></p>
    </div>
    </form>




     

    

    

