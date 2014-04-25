<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_option_list.aspx.cs" Inherits="Hotels2thailand.UI.admin_productOption_product_option_list" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register  Src="~/Control/DatepickerCalendar.ascx" TagName="DatePicker_Add_Edit" TagPrefix="DateTime" %>
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
    
    
    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Supplier Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        
        
        <asp:DropDownList ID="dropProductSup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropProductSup_OnSelectedIndexChanged"  Width="400px"></asp:DropDownList>
        
        
    </asp:Panel>
    <div class="option_add_left">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Option List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />

        <div style=" margin:5px 0px 5px 0px; padding:0px 0px 0px 0px;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                            <asp:HyperLink ID="lnkOptionCreate" runat="server" ><asp:Image ID="imgPlus" runat="server" ImageUrl="~/images/plus.png" /> Add New Option</asp:HyperLink>&nbsp;&nbsp;<asp:Image ID="imgDup" runat="server" ImageUrl="~/images/duplicate.png" />
                            <asp:LinkButton ID="DuplicatOption" runat="server" Text="Duplicate"></asp:LinkButton>
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="DuplicatOption"  DisplayModalPopupID="ModalPopupExtender1" /><br />
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="DuplicatOption" PopupControlID="PNL"   CancelControlID="ButtonClose" BackgroundCssClass="modalBackground"  />
                            <asp:Panel ID="PNL" runat="server"  style="display:none; width:500px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px; margin:0px">
                            <p style="margin:5px 0px 5px 0px; padding:0px; text-align:left; width:100%;  font-weight:bold; color:Black">Dupplicate Option</p>
                            
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:GridView ID="GvSupplierListDup" runat="server"   AutoGenerateColumns="false" ShowFooter="false"  ShowHeader="false" DataKeyNames="Key" OnRowDataBound="GvSupplierListDup_OnRowDataBound" SkinID="Nostyle" >
                                <Columns>
                                    <asp:TemplateField   ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            
                                            <p style=" margin:0px; padding:0px; color:Black; font-weight:bold"><asp:Label ID="lblSupTitle" runat="server" Text='<%# Eval("Value") %>'></asp:Label></p>
                                            <asp:GridView ID="GVoptionListDup" runat="server" ShowHeader="false" AutoGenerateColumns="false" ShowFooter="false" DataKeyNames="Key" OnRowDataBound="GVoptionListDup_OnRowDataBound" >
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:CheckBox ID="chkOptionDUp" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbloptionTitle" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3" DisplayAfter="2" DynamicLayout="true" >
                                <ProgressTemplate>
                                    <asp:Image ID="imgProgress"  runat="server" ImageUrl="~/images/progress_b.gif" />
                                </ProgressTemplate>
                                </asp:UpdateProgress>    
                                <br />
                            <asp:Button ID="btnDupli" runat="server"  Text="Add" SkinID="Green_small"  OnClick="btnDupli_OnClick" />
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="text-align:right;">
                                  <asp:Button ID="ButtonClose" runat="server" Text="Close"  SkinID="White_small"  />
                                 
                            </div>
                            </asp:Panel>
                           </ContentTemplate>
                            </asp:UpdatePanel>
        </div>

        
        <asp:GridView ID="GvOptionCat" runat="server" EnableModelValidation="true" AutoGenerateColumns="false" DataKeyNames="Key" SkinID="Nostyle"
         ShowHeader="false" OnRowDataBound="GvOptionCat_OnRowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                    <h4><asp:Label ID="lblCattitle" runat="server" Text='<%#Bind("Value") %>'></asp:Label></h4> 
                        <asp:GridView ID="gvChildOption" runat="server" AutoGenerateColumns="false" ShowHeader="false"  SkinID="Nostyle" OnRowDataBound="gvChildOption_OnRowDataBound" DataKeyNames="OptionID" >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <div style=" width:100%; text-align:left; margin:0px; padding:2px; height:20px; display:block;">
                                        <div style="float:left;margin:0px; padding:0px; width:220px">
                                        <asp:Image ID="imgBtGreen"  runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;
                                        <asp:HyperLink ID="lOption" runat="server" Text='<%# Bind("Title") %>' CssClass="sss" NavigateUrl='<%# String.Format("~/admin/productOption/option_add.aspx?oid={0}", Eval("OptionID")) + this.AppendCurrentQueryString() %>'></asp:HyperLink>
                                        </div>
                                        <div style="float:right;margin:0px; padding:0px;">
                                        <asp:DropDownList ID="drpPriority" runat="server" OnSelectedIndexChanged="drpPriority_OnSelectIndexChanged"  SkinID="DropCustomstyle"  AutoPostBack="true" Font-Size="11px" >
                                        </asp:DropDownList>
                                        </div>
                                        <div style="clear:both"></div>
                                    </div>
                                    <table  style=" width:100%; text-align:left; margin:0px; padding:1px;">
                                    <tr>
                                    <td style=" width:100%;margin:0px; padding:1px;"">
                                        
                                    </td>
                                    <td style=" width:100%; text-align:right;margin:0px; padding:1px;"">
                                        
                                    </td>
                                    </tr>
                                    </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <%--Right *******************************************--%>
    <div class="option_add_right">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Current Period</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />

    <div class="product_period_box" style="padding-left:30px">

   <asp:GridView ID="GVProductPeriod" runat="server" 
        EnableModelValidation="True" AutoGenerateColumns="false"  DataKeyNames="PeriodId"  OnRowDataBound="GVProductPeriod_OnRowDataBound"  SkinID="Nostyle" ShowHeader="false" 
        OnRowCommand="GVProductPeriod_OnRowCommand">
       
        <Columns>
            <asp:TemplateField>         
                <ItemTemplate>        
                    <h4><%# Container.DataItemIndex + 1 %> </h4>
                    <p><%# Eval("PeriodId")%></p>   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                <div class="product_period_box_List">
                <div style=" float:left"><DateTime:DatePicker_Add_Edit ID="DateTimePicker" runat="server" ClientIDMode="AutoID"   /></div>
                <div style=" float:left ;  margin:11px 0px 0px 0px" ><asp:Button ID="btUpdatePeriod"  runat="server" Text="Save"  SkinID="Green" Width="60px" CommandName="periodupdate" 
                        CommandArgument='<%# String.Format("{0}&{1}", Eval("PeriodId"),Container.DataItemIndex)%>'/>
                        
                </div>
                <div style=" clear: both"  ></div>    
                </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
</div>

<div class="line" style="width:500px" ></div>
<h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Insert Period Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
<div class="product_period_box_insert" style="padding-left:30px">

                <div style=" float:left"><DateTime:DatePicker_Add_Edit ID="DateTimePicker" runat="server" /></div>
                <div style=" float:left ;  margin:11px 0px 0px 0px" ><asp:Button ID="btPeriodSubmit" runat="server" Text="Save"  Width="60px" OnClick="btPeriodSubmit_OnClick" SkinID="Blue"/></div>
                <div style=" clear: both"  ></div>       
</div>

<div class="line" style="width:500px" ></div>
        
     </div>
    
    
        

    
        
    
</asp:Content>
