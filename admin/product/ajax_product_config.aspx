<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax_product_config.aspx.cs" Inherits="Hotels2thailand.UI.admin_ajax_product_config" EnableViewState="false" EnableViewStateMac="false" EnableEventValidation="false" EnableSessionState="False" %>


    <form id="ProductCOnfig_form" runat="server">
        <div style="float:left; margin:0px 0px 0px 0px; padding:0xp 0px 0px 0px;">
                <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 0px 0px;padding:0px">Recommend Hotel</p>
                    <asp:RadioButton ID="rbStatusflag" runat="server" Checked="true" Text="Enable" GroupName="rbrec"  ClientIDMode="Static" />
                    <asp:RadioButton ID="rbStatusunflag" runat="server" Text="Disable" GroupName="rbrec"  ClientIDMode="Static"  />
                </div> 
                <div style="float:left; margin:0px 0px 0px 100px; padding:0xp 0px 0px 0px;">
                    <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 0px 0px;padding:0px">Status</p>
                    <asp:RadioButton ID="rbStatusEnable" runat="server" Checked="true" Text="Enable" GroupName="rbStatus"   ClientIDMode="Static" />
                    <asp:RadioButton ID="rbStatusDisable" runat="server" Text="Disable" GroupName="rbStatus"  ClientIDMode="Static" />
                </div>    
                <div style="float:left; margin:0px 0px 0px 100px; padding:0xp 0px 0px 0px;">
                <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 0px 0px;padding:0px">Process Status</p>
                <asp:DropDownList ID="drpstatusProcess" runat="server"  ClientIDMode="Static"></asp:DropDownList>
                </div>       
            
                 <div style=" clear:both"></div>  
                 <div  style="width:100%; text-align:left;  margin:10px 0px 0px 0px;">
                 <input type="button" name="btnSaveConfig" id="btnSaveConfig" style=" width:180px;"  value="Save Product Config" onclick="SaveProductConfig();" class="btStyleGreen" />
                 
         </div>
    </form>

