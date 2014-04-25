<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_option_gala.aspx.cs" Inherits="Hotels2thailand.UI.admin_productOption_product_option_gala" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register  Src="~/Control/DatepickerCalendar.ascx" TagName="DatePicker_Add_Edit" TagPrefix="Product" %>
<%@ Register Src="~/Control/Lang_Gala_Content_Box.ascx" TagName="Gala_Content_Lang" TagPrefix="Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
    <link href="../../css/lert.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="../../Scripts/lert.js" ></script>	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

        function fnCheckUnCheck(objId) {
            var grd = document.getElementById("<%= GVProductPeriod.ClientID %>");

            //Collect A
            var rdoArray = grd.getElementsByTagName("input");

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].type == 'radio') {
                    if (rdoArray[i].id != objId) {
                        rdoArray[i].checked = false;
                        rdoArray[i].parentNode.parentNode.style.backgroundColor = '#eceff5';
                    }
                    else {
                        rdoArray[i].parentNode.parentNode.style.backgroundColor = '#daf3d5';
                    }
                }
            }
        }

        function Alertlightbox(messageinput) {

            var yes = new LertButton('Close Window', function () {
                //do nothing
            });

            var message = messageinput;
            var exampleLert = new Lert(
		message,
		[yes],
		{


});

            exampleLert.display();

        }

//        

        function AlertlightboxNoPeriod() {
            Alertlightbox('<img src="../../images/alert.png" style="float:left;margin:0px; padding:2px;border:0px"><p class="alert_box_head">No Supplier Selected</p><p class="alert_box_detail">Please select at least one.</p>');
        }
    </script>

    
    
   <h6><asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
    
    
    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Supplier Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:DropDownList ID="dropProductSup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropProductSup_OnSelectedIndexChanged"  Width="400px"></asp:DropDownList>

        
    </asp:Panel>
    <div class="option_add_left">
        
        <asp:Panel ID="panel5" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            
            <asp:TextBox ID="txtTitle" runat="server"  Width="345px"></asp:TextBox>
            <p style=" margin:5px 0px 0px 0px; padding:0px;"></p>
            <asp:Button ID="btnSave" runat="server" SkinID="Green_small" Text="Save" OnClick="btnSave_Onclick" />
        </asp:Panel>

        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Option List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <%--<asp:HyperLink ID="lnkOptionCreate" runat="server" ><asp:Image ID="imgPlus" runat="server" ImageUrl="~/images/plus.png" /> Add New Option</asp:HyperLink>--%>
        
                        <asp:GridView ID="gvChildOption" runat="server" AutoGenerateColumns="false" ShowHeader="false"  SkinID="Nostyle" OnRowDataBound="gvChildOption_OnRowDataBound" DataKeyNames="OptionID" >
                            <EmptyDataRowStyle   CssClass="alert_box" />
                            <EmptyDataTemplate>
                                      <div class="alert_inside_GridView">
                                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                                       <p class="alert_box_head">No Payment Plan Record </p>
                                       <p  class="alert_box_detail">Please select at least one.</p>
                                     </div>
                                </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <div style=" width:100%; text-align:left; margin:0px; padding:2px 2px 10px 2px; display:block;border-bottom:1px solid #eeeee1;">
                                        <div style="float:left;margin:0px; padding:0px; width:220px">
                                        <asp:Image ID="imgBtGreen"  runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;
                                        <asp:HyperLink ID="lOption" runat="server" Text='<%# Bind("Title") %>' CssClass="sss" NavigateUrl='<%# String.Format("~/admin/productOption/product_option_gala.aspx?oid={0}", Eval("OptionID")) + this.AppendCurrentQueryString() %>'></asp:HyperLink>
                                        </div>
                                        <a href="javaScript:showDiv('gala<%# Eval("OptionID") %>')">
                                                <asp:Image ID="imgEdit" runat="server"  ImageUrl="~/images/edit.png" ImageAlign="Right" />
                                        </a>
                                            <div id="gala<%#Eval("OptionID") %>" style="display:none; margin:5px 0px 0px 0px; padding:0px;">
                                            <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' Width="345px"></asp:TextBox>
                                            <p style=" margin:2px 0px 0px 0px; padding:0px;"></p>
                                            <asp:Button ID="tbnSave" runat="server" Text="Save" SkinID="Green_small" CommandArgument='<%# Eval("OptionID") + "," + Container.DataItemIndex %>' CommandName="ancSave" OnClick="galaBtn_Cilck" />
                                            <asp:Button ID="tbnDis" runat="server" Text="Disable" SkinID="White_small" CommandArgument='<%# Eval("OptionID") + "," + Container.DataItemIndex %>' CommandName="ancDis" OnClick="galaBtn_Cilck" />
                                            </div>
                                       <div style="clear:both"></div>     
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
                    
    </div>
    <%--Right *******************************************--%>
    <div class="option_add_right">
        <h6><asp:Label ID="lblHeadtitle" runat="server"></asp:Label></h6>
        <asp:Panel ID="screenBlock" runat="server" CssClass="screen_block" Visible="false">
            <div    style="margin:150px 0px 0px 50px; width:80%" >
                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                       <p class="alert_box_head">No Payment Plan Record </p>
                       <p  class="alert_box_detail">Please select at least one.</p>
            </div>
            
        </asp:Panel>
        <asp:Panel ID="panelGalaDetail" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Gala Date Range</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <Product:DatePicker_Add_Edit ID="Datepicker" runat="server" /><br />
            <table style=" margin:0px; padding:0px;">
                <tr>
                    <td style="margin:0px; padding:0px 10px 0px 0px;"><p class="txtheadtitle">Gala For</p></td>
                    <td style="margin:0px; padding:0px 10px 0px 10px;"><p class="txtheadtitle">Num of Compulsory(days)</p></td>
                    <td style="margin:0px; padding:0px 10px 0px 10px;"><p class="txtheadtitle">IsCompulsory</p></td>
                </tr>
                <tr>
                    <td style="margin:0px; padding:0px 10px 0px 0px;">
                        <asp:DropDownList ID="dropGalaFor" runat="server">
                            <asp:ListItem Value="0"  Text="Adult"></asp:ListItem>
                            <asp:ListItem Value="1"  Text="Child"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="margin:0px; padding:0px 10px 0px 10px;">
                        <asp:DropDownList ID="dropCalculate" runat="server">
                            <asp:ListItem Value ="1" Text="Single Day Compulsory"></asp:ListItem>
                            <asp:ListItem Value ="0" Text="All Day Compulsory"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="margin:0px; padding:0px 10px 0px 10px;"> 
                        <asp:RadioButton ID="radioyes" runat="server" Text="Compulsory" GroupName="compulsory" Checked="true" />
                        <asp:RadioButton ID="radioNo" runat="server" Text="Not Compulsory" GroupName="compulsory" />
                    </td>
                </tr>
            </table>
            <table style=" margin:10px 0px 0px 0px; padding:0px;">
                <tr>
                    <td style="margin:0px; padding:0px 0px 0px 0px;"><p class="txtheadtitle">Market</p></td>
                </tr>
                <tr>
                    <td style="margin:0px; padding:0px 0px 0px 0px;">
                        <asp:DropDownList ID="dropMarket" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                </table>
            <br />
            <br />
            
            <asp:Button ID="btngalasave" runat="server" Text="Save" SkinID="Green" OnClick="btngalasave_OnClick" />
        </asp:Panel>

        <asp:Panel ID="panelRateAdd" runat="server" CssClass="productPanel">
            <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Price Manage</h4>
            <p class="contentheadedetail">Add-Edit Condition</p>
            
                <asp:Image ID="imgPlus" runat="server" ImageUrl="~/images/plus.png" /><a href="javaScript:showDiv('rate_period_add')">Insert New Rate</a>
                <br /><br />
                <div id="rate_period_add" style="display:none">
            <asp:GridView ID="GVProductPeriod" runat="server" EnableModelValidation="True" AutoGenerateColumns="false"  DataKeyNames="PeriodId"  OnRowDataBound="GVProductPeriod_OnRowDataBound"  
                 ShowHeader="true"  ShowFooter="false" SkinID="ProductList">
                 <EmptyDataRowStyle   CssClass="alert_box" />
                    <EmptyDataTemplate>
                         <div class="alert_inside_GridView" >
                               <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                               <p class="alert_box_head">Please Insert Period For this Supplier Before </p>
                               <p class="alert_box_detail">Please select at least one.</p>
                        </div>
                 </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate  >
                            <asp:RadioButton ID="radioPeriod" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="DateStart" runat="server" Text='<%# Bind("DateStart", "{0:MMM d, yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="DateEnd" runat="server" Text='<%# Bind("DateEnd","{0:MMM d, yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" Width="80px" BackColor="#faffbd" Text="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Own" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtOwn" runat="server" Width="80px" BackColor="#faffbd" Text="0" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rack" ItemStyle-Width="15%">
                        <ItemTemplate> 
                            <asp:TextBox ID="txtRack" runat="server" Width="80px" BackColor="#faffbd" Text="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

             <br />
           <asp:Button ID="tbnSaveRate"  runat="server" Text="Save"  SkinID="Green"  OnClick="tbnSaveRate_OnClick"/>

           </div>
            <br />
            <div class="rate_period_Current_price">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel5" DisplayAfter="1" DynamicLayout="true">
                <ProgressTemplate>
                    <asp:Image ID="imgprogress" runat="server" ImageUrl="~/images/progress_b.gif" />
                </ProgressTemplate>
                </asp:UpdateProgress>
              <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                  <ContentTemplate> 
                  <a name="current_rate"></a>
             <asp:GridView ID="GVRatePeriodCurrent" runat="server" ShowFooter="false" ShowHeader="true" AutoGenerateColumns="false" EnableModelValidation="True" DataKeyNames="PeriodId"  SkinID="ProductList"   
              OnRowDataBound="GVRatePeriodCurrent_OnRowDataBound" >
                <EmptyDataRowStyle   CssClass="alert_box" />
                    <EmptyDataTemplate>
                         <div class="alert_inside_GridView" >
                               <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                               <p class="alert_box_head">Please Insert Period For this Supplier Before </p>
                               <p class="alert_box_detail">Please select at least one.</p>
                        </div>
                 </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="No" ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                           <asp:LinkButton ID="lnkStatus" runat="server" Text='<%# Container.DataItemIndex + 1 %>' OnClick="lnkStatus_Onclick" CommandArgument='<%# Eval("PeriodId") + "," + Eval("ConditionId") %>' 
                            ToolTip="Click To Disable"  CommandName="rateDis"></asp:LinkButton>
                           <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="lnkStatus"  DisplayModalPopupID="ModalPopupExtender2" />
                                                                
                           <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="lnkStatus" PopupControlID="Panel3" OkControlID="ButtonOks" 
                            CancelControlID="ButtonCancels" BackgroundCssClass="modalBackground"  />
                            <asp:Panel ID="Panel3" runat="server"  style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                              <p style="margin:0px; padding:0px; text-align:left; width:100%;  font-weight:bold; color:Black">Are you sure to Disable this Rate</p>
                              <div style="text-align:right;">
                              <br />
                              <asp:Button ID="ButtonOks" runat="server" Text="OK"  SkinID="Green_small" />
                              <asp:Button ID="ButtonCancels" runat="server" Text="Cancel" SkinID="White_small" />
                              </div>
                           </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="DateStart" runat="server" Text='<%# Bind("DateStart", "{0:MMM d, yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="DateEnd" runat="server" Text='<%# Bind("DateEnd","{0:MMM d, yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" Width="60px" BackColor="#faffbd" Text='<%# Bind("RatePrice", "{0:0}") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Own" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtOwn" runat="server" Width="60px" BackColor="#faffbd"  Text='<%# Bind("RateOwn", "{0:0.00}") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rack" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRack" runat="server" Width="60px" BackColor="#faffbd"  Text='<%# Bind("RateRack", "{0:0}") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="Edit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="tbnSaveEdit" runat="server" OnClick="tbnSaveEdit_Onclick" CommandArgument='<%# Eval("PeriodId") + "," + DataBinder.Eval(Container, "DataItemIndex") + "," + Eval("ConditionId")  %>' Text="Save" SkinID="Green_small" CommandName="Editrate" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                </Columns>
             </asp:GridView>
                 
              </ContentTemplate>
              </asp:UpdatePanel>  
            </div>
        </asp:Panel>
        

        <asp:Panel ID="panelGalaContentLang" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image5" runat="server" ImageUrl="~/images/content.png" /> Gala Content Language</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <Product:Gala_Content_Lang ID="Content_Lang_box" runat="server" />
        </asp:Panel>

    </div>





    
    
        
    
        
    
</asp:Content>
