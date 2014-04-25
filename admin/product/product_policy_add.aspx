<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_policy_add.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_product_policy_add" %>
<%@ Register Src="~/Control/DatepickerCalendar.ascx" TagName="PolicyDatePicker" TagPrefix="Product" %>
<%@ Register Src="~/Control/Lang_Policy_Content_Box.ascx" TagName="PolicyContentLang" TagPrefix="Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <h6><asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
    <asp:Panel ID="panelProductPolicyCat" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Policy Category</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />

        <asp:DropDownList ID="dropPolicyCat" runat="server" Width="400px" ></asp:DropDownList>

   </asp:Panel>
   <asp:Panel ID="panelProductPolicyType" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Policy Type</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />

        <asp:DropDownList ID="dropPolicyType" runat="server" Width="400px" OnSelectedIndexChanged="dropPolicyType_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

   </asp:Panel>
   <asp:Panel ID="panelProductPolicyinformation" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Information</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <div class="product_policy_information">
        <table>
            <tr>
                <td>
                    <p style="color:#3b59aa;font-size:11px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Title</p> 
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="400px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
        <tr><td><p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Date Range</p></td></tr>
            <tr>
                <td><Product:PolicyDatePicker ID="dDatePicker" runat="server" /></td>
            </tr>
        </table>
        
        <table>
            <tr>
                <td>
                <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Status</p> 
                </td>
                
            </tr>
            <tr>
                <td>
                   <asp:RadioButton ID="radioStatusTrue" runat="server" Checked="true" Text="Enable" GroupName="Status" />
        <asp:RadioButton ID="radioStatusFalse" runat="server" Text="Disable" GroupName="Status" /> 
                </td>
            </tr>
            
        </table>
        <br />
        <asp:Button ID="tbnInfSave" runat="server" Text="Save" SkinID="Green" OnClick="tbnInfSave_Onclick" />
        </div>
   </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        
   <asp:Panel ID="panelProductPolicyCancel" runat="server" CssClass="productPanel"  >
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Cancellation Policy Charge</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <div class="product_policy_information">
        <table>
            <tr><td><p style="color:#3b59aa;font-size:11px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Day Cancel</p></td><td><asp:DropDownList ID="dropCancel" runat="server"></asp:DropDownList></td></tr>
        </table>
        
        <table style=" float:left">
            <tr>
                <td><p style="color:#3b59aa;font-size:11px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Hotel Charges</p></td>
            </tr>
            <tr>
                <td><p style="color:#1c2a47;font-size:11px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Night Charge</p>
                <asp:TextBox ID="txtRoomHotel" runat="server" Width="80px" BackColor="#faffbd" Text="0"></asp:TextBox>
                </td>
                <td><p style="color:#1c2a47;font-size:11px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Charges of(%)</p>
                <asp:TextBox ID="txtPercentHotel" runat="server" Width="80px" BackColor="#faffbd" Text="0" MaxLength="3"></asp:TextBox>
                
                </td>
            </tr>
        </table>
        <table style=" float:left; margin:0px 0px 0px 20px;">
            <tr> 
                <td colspan="2"><p style="color:#3b59aa;font-size:11px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Bluehouse Charge</p></td>
            </tr>
            <tr>
                <td><p style="color:#1c2a47;font-size:11px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Night Charge</p>
                
                <asp:TextBox ID="txtRoomBht" runat="server" Width="80px"  BackColor="#faffbd" Text="0"></asp:TextBox>
                </td>
                <td>
                <p style="color:#1c2a47;font-size:11px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Charges of(%)</p>
                <asp:TextBox ID="txtPercentBHt" runat="server" Width="80px" BackColor="#faffbd" Text="0"  MaxLength="3">
                </asp:TextBox>
                </td>
            </tr>
        </table>
        <table style=" float:left; margin:46px 0px 0px 20px;">
            <tr>
                <td><asp:Button ID="btnCancelSave" runat="server" Text="Add" SkinID="Green_small" OnClick="btnCancelSave_OnClick" /></td>
            </tr>
        </table >
        <div style="clear:both"></div>
        <br />
        
        </div>
        <asp:UpdateProgress ID="progress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true" DisplayAfter="2">
            <ProgressTemplate>
                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/images/progress_b.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:GridView ID="GVCancelPolicyList" runat="server" ShowFooter="false" ShowHeader="true" EnableModelValidation="true" DataKeyNames="PolicyId"  AutoGenerateColumns="false" SkinID="ProductList" OnRowDataBound="GVCancelPolicyList_onRowDataBound"  >
            <Columns>
                <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Day Cancel" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDayCancel" runat="server"  Text='<%# Bind("DayCancel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hotel Charge (%)" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPerHotel" runat="server"  Text='<%# Bind("HotelChargePer","{0:0}")%>'></asp:Label>%
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hotel Room (night) Charge" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRoomhotel" runat="server"  Text='<%#Bind("HotelChargeRoom","{0:0}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bht Charge (%)" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:Label ID="lblPerBht" runat="server"  Text='<%#Bind("BHTChargePer","{0:0}") %>'>%</asp:Label>%
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bht Room (night) Charge" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRoombht" runat="server"  Text='<%#Bind("BHTChargeRoom","{0:0}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkBtDel" runat="server" Text="Delete" CommandArgument='<%# Eval("PolicyId")+ "," + Eval("DayCancel") %>' CommandName="cancelDel" OnClick="linkBtDel_OnClick"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   </asp:Panel>
   </ContentTemplate>
    </asp:UpdatePanel>
   <asp:Panel ID="panelProductPolicyContentLang" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image5" runat="server" ImageUrl="~/images/content.png" /> Content Language</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <Product:PolicyContentLang ID="PolicyContentLang" runat="server" />
   </asp:Panel>
</asp:Content>


