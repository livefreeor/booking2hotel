﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax_product_style_insertForm.aspx.cs" Inherits="Hotels2thailand.UI.admin_ajax_product_style_insertForm" EnableViewState="false" EnableViewStateMac="false" EnableEventValidation="false" EnableSessionState="False" %>

    
    <form id="Product_style_insert" runat="server">
    <div class="formbox">
    
    <p class="formbox_head">Insert Product Style</p>
    <asp:CheckBoxList ID="Product_style_list" runat="server"  RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="checkBox_list"  ClientIDMode="Static" >
        
    </asp:CheckBoxList>
    <p class="formbox_buttom"><input type="button" value="Save" onclick="SaveProductStyle();"  class="btStyleGreen" />&nbsp;<input type="button" value="Cancel" onclick="DarkmanPopUp_Close();" class="btStyleWhite" style=" width:80px" /></p>
    </div>
    </form>




     

    

    

