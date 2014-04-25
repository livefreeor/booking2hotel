<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_option_rate.aspx.cs" Inherits="Hotels2thailand.UI.admin_productOption_product_option_rate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
   	    .worldwide
   	    {
   	        margin:0px;
            padding:0px;
   	    }
        .worldwide a
        {
            font-weight:normal;
            margin:0px;
            padding:0px 0px 0px 5px;
        }
        .list_market 
        {
            width:100px;
            margin:5px 0px 0px 0px;
            padding:2px;
        }
        .radioType{ margin:0px 0px 0px 430px; padding:0px;}
  </style>
  <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.2.min.js"></script>
  <script type="text/javascript" language="javascript">
      var VatFactor = 7;
      function Calculate() {
          if ($("#Normal").attr("checked") == true) {
              $("#GVProductPeriod #txtOwn").each(function () {
                  var Own = parseInt($(this).val());
                  var Pricetotal = Own;
                  var OwnTotal = Own;

                  if ($("#GVProductPeriod_ComBox #Vat").attr("checked") == true) {
                      OwnTotal = Own + ((Own * VatFactor) / 100);
                  }

                  if ($("#GVProductPeriod_ComBox #GVProductPeriod_com").val() != "" || $("#GVProductPeriod_ComBox #GVProductPeriod_com").val() != "0") {
                      var Com = $("#GVProductPeriod_ComBox #GVProductPeriod_com").val();
                      Pricetotal = Math.round(OwnTotal + ((OwnTotal * Com) / 100));
                  }

                  $(this).parent("td").parent("tr").find("#txtPrice").stop().attr("value", Pricetotal);
                  $(this).val(OwnTotal);
              });
          }

          if ($("#Selling").attr("checked") == true) {
              $("#GVProductPeriod #txtPrice").each(function () {
                  var Price = parseInt($(this).val());
                  var Owntotal = Price;

                  var PerBox = $("#GVProductPeriod_ComBox_Sell #GVProductPeriod_per");
                  var NetBox = $("#GVProductPeriod_ComBox_Sell #GVProductPeriod_net");



                  if (PerBox.val() != "" && NetBox.val() == "0") {
                      Owntotal = Price - ((Price * PerBox.val()) / 100);
                      $(this).parent("td").parent("tr").find("#txtOwn").stop().attr("value", Owntotal);
                  }



                  if (PerBox.val() == "0" && NetBox.val() != "") {
                      Owntotal = Price - NetBox.val();
                      $(this).parent("td").parent("tr").find("#txtOwn").stop().attr("value", Owntotal);
                  }


              });
          }


      }

      function Calculate_current() {
          if ($("#Normal_current").attr("checked") == true) {
              $("#ContentPlaceHolder1_GVRatePeriodCurrent #txtOwn").each(function () {
                  var Own = parseInt($(this).val());
                  var Pricetotal = Own;
                  var OwnTotal = Own;

                  if ($("#GVRatePeriodCurrent_ComBox #Vat_current").attr("checked") == true) {
                      OwnTotal = Own + ((Own * VatFactor) / 100);
                  }

                  if ($("#GVRatePeriodCurrent_ComBox #GVRatePeriodCurrent_com").val() != "" || $("#GVRatePeriodCurrent_ComBox #GVRatePeriodCurrent_com").val() != "0") {
                      var Com = $("#GVRatePeriodCurrent_ComBox #GVRatePeriodCurrent_com").val();
                      Pricetotal = Math.round(OwnTotal + ((OwnTotal * Com) / 100));
                      
                  }

                  $(this).parent("td").parent("tr").find("#txtPrice").stop().attr("value", Pricetotal);
                  $(this).val(OwnTotal);
              });
          }

          if ($("#Selling_current").attr("checked") == true) {

              $("#ContentPlaceHolder1_GVRatePeriodCurrent #txtPrice").each(function () {
                  var Price = parseInt($(this).val());
                  var Owntotal = Price;

                  var PerBox = $("#GVRatePeriodCurrent_ComBox_Sell #GVRatePeriodCurrent_per");
                  var NetBox = $("#GVRatePeriodCurrent_ComBox_Sell #GVRatePeriodCurrent_net");

                  if (PerBox.val() != "" && NetBox.val() == "0") {
                      Owntotal = Price - ((Price * PerBox.val()) / 100);
                      $(this).parent("td").parent("tr").find("#txtOwn").stop().attr("value", Owntotal);
                  }

                  if (PerBox.val() == "0" && NetBox.val() != "") {
                      Owntotal = Price - NetBox.val();
                      $(this).parent("td").parent("tr").find("#txtOwn").stop().attr("value", Owntotal);
                  }
            });
               
            }


      }

  </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function Checkrate() {
            alert("PLEASE CHECK RATE INPUT !! <br/>Rate Price must more than Rate Own");
        }

    </script>
    <h6><asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>

    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image8" runat="server" ImageUrl="~/images/content.png" /> Supplier Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:DropDownList ID="dropProductSup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropProductSup_OnSelectedIndexChanged"  Width="400px"></asp:DropDownList>
    </asp:Panel>
   
    <asp:Panel ID="panelOptionSelection" runat="server" CssClass="productPanel">
            <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Option Selection</h4>
            <p class="contentheadedetail">Add-Edit Condition</p><br />
            <asp:DropDownList ID="dropoption" runat="server" Width="400px" OnSelectedIndexChanged="dropoption_OnSelectedIndexChanged" AutoPostBack="true" Font-Size="14px"></asp:DropDownList>
            <asp:GridView ID="GvSupplierList" runat="server" OnRowDataBound="GvSupplierList_OnRowDataBound" DataKeyNames="Key" AutoGenerateColumns="false" SkinID="Nostyle" ShowFooter="false" ShowHeader="false" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <p style=" font-size:12px; padding:0px; margin:0px"><asp:Image ID="imgdot" runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;<asp:Label ID="lblSupplieroption" runat="server" Text='<%# Bind("Value") %>'  ></asp:Label></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            
            
    <div class="option_add_left" style=" width:250px">
        <asp:Panel ID="panelconditionInsertBox" runat="server" CssClass="productPanel">
            <h4><asp:Image ID="Image6" runat="server" ImageUrl="~/images/content.png" /> Condition Insert Box</h4>
            <p class="contentheadedetail">Add-Edit Condition</p><br />
            <asp:TextBox ID="txtConditionInsert" runat="server" Width="250px"></asp:TextBox>
            <p style=" margin:2px; padding:0px;"></p>
            <asp:Button ID="btninsertSave" runat="server" Text="Save" SkinID="Green_small"   OnClick="btninsertSave_OnClick"/>
        </asp:Panel>
        <asp:Panel ID="panelConditionList" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> condition List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
            <asp:GridView ID="ConditionList" runat="server" EnableModelValidation="true" AutoGenerateColumns="false"  SkinID="Nostyle"
         ShowHeader="false" DataKeyNames="ConditionId" OnRowDataBound="ConditionList_OnRowDatabound">
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
                    <asp:Image ID="imgBtGreen"  runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;
                        <asp:HyperLink ID="hptitle" runat="server" Text='<%# Bind("Title") %>' ></asp:HyperLink>
                     </div>
                    </ItemTemplate>
                </asp:TemplateField>
             </Columns>
             </asp:GridView>
        </asp:Panel>
    </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    <div class="option_add_right"  style=" width:660px">
    <asp:Panel ID="screenBlock" runat="server" CssClass="Product_rate_screen_block" Visible="false">
            <div  style="margin:150px 0px 0px 50px; width:80% ">
                       <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                       <p class="alert_box_head">Condition Selection</p>
                       <p  class="alert_box_detail">Please Select Condition </p>
            </div>
            
        </asp:Panel>
    <h6><asp:Label ID="lblHeadtitle" runat="server"></asp:Label></h6>
    
    <div class="product_option_rate">
        
        <asp:Panel ID="panelConditionAdd" runat="server" CssClass="productPanel">
            <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Condition Information</h4>
            <p class="contentheadedetail">Add-Edit Condition</p><br />
            <table>
                <tr>
                    <td><h5>Title</h5></td>
                    <td><asp:TextBox ID="txtTitle" runat="server" Width="500px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td><h5>Breakfast</h5></td>
                    <td>
                        <asp:DropDownList ID="dropBreakfast" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td><h5>Num Adult</h5></td>
                    <td><asp:DropDownList ID="dropNumaDult" runat="server"></asp:DropDownList></td>
                    <td style=" width:60px; text-align:right"><h5>Num Child</h5></td>
                    <td><asp:DropDownList ID="dropNumchild" runat="server"></asp:DropDownList></td>
                    <td style=" width:60px; text-align:right"><h5>Num Extra</h5></td>
                    <td><asp:DropDownList ID="dropNumExpired" runat="server"></asp:DropDownList></td>
                    
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <h5>Status</h5>
                    </td>
                    <td>
                        <asp:RadioButton ID="radioStatusEnable"  runat="server" Text="Enable" Checked="true" GroupName="Conditionstatus" />
                        <asp:RadioButton ID="radioStatusDisable"  runat="server" Text="Disable" GroupName="Conditionstatus" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <h5>Transfer</h5>
                    </td>
                    <td>
                        <asp:RadioButton ID="radioTransferYes"  runat="server" Text="Yes"  GroupName="Conditiontrans" />
                        <asp:RadioButton ID="radioTransferNo"  runat="server" Text="No" Checked="true" GroupName="Conditiontrans" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="ConditionSave" runat="server" Text="Save" SkinID="Green" OnClick="ConditionSave_Onclick" />
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Panel ID="panelMarket" runat="server" CssClass="productPanel">
            <h4><asp:Image ID="Image5" runat="server" ImageUrl="~/images/content.png" /> Market Manage</h4>
            <p class="contentheadedetail">Add-Edit Condition</p><br />
            <div class="product_option_rate_market" >
               <p class="worldwide"> <asp:DropDownList ID="dropMarket" runat="server"  AutoPostBack="true" ></asp:DropDownList> &nbsp;&nbsp;<asp:HyperLink ID="hplmarketManage" runat="server"  Target="_blank" Text="Click Manage Market"></asp:HyperLink></p>
             </div>
            
            <br />
            <asp:Button ID="btmarketSave" runat="server" SkinID="Green" Text="Save"  OnClick="btmarketSave_Onclick" />
            </asp:Panel>
            </ContentTemplate>
          </asp:UpdatePanel>

        

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                
        <asp:Panel ID="panelPolicyAdd" runat="server" CssClass="productPanel">
            <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Condition Policy</h4>
            <p class="contentheadedetail">Add-Edit Condition</p><br />
            
            <table>
                <tr>
                    <td><asp:DropDownList ID="dropPolicyCat" runat="server" OnSelectedIndexChanged="dropPolicyCat_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                    
                    <td><asp:Button ID="btnPolicyAdd" runat="server" Text="Save"  SkinID="Green_small" OnClick="btnPolicyAdd_Onclick" /></td>
                </tr>
            </table>
            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1" DynamicLayout="true">
         <ProgressTemplate>
            <asp:Image ID="imgProgress" runat="server" ImageUrl="~/images/progress.gif" />
         </ProgressTemplate>
         </asp:UpdateProgress>
             <asp:CheckBoxList ID="chkPolicy" runat="server"></asp:CheckBoxList>
            <asp:GridView ID="GvPolicyUsed" runat="server" EnableModelValidation="True" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false" DataKeyNames="PolicyId" SkinID="ProductList" >
             
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%">
                                <ItemTemplate>
                                   <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="85%">
                        <ItemTemplate>
                          <p style="color:#333333; margin:0px; padding:2px 0px 2px 5px; font-weight:bold"><asp:Label ID="lblPolicytitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDatePeriod" runat="server" Text='<%# Eval("Datestart","{0:dd-MMM-yyyy}") + "&nbsp;&nbsp;-&nbsp;&nbsp;" + Eval("DateEnd","{0:dd-MMM-yyyy}") %>' ></asp:Label></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%">
                        <ItemTemplate>
                          <asp:LinkButton ID="btnDel" runat="server" CommandName="polDel" CommandArgument='<%# Eval("PolicyId") %>' Text="Delete" OnClick="btnDel_Onclick"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
        </asp:Panel>

         </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:Panel ID="panelRateAdd" runat="server" CssClass="productPanel">
            <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Price Manage</h4>
            <p class="contentheadedetail">Add-Edit Condition</p><br />
            <asp:DropDownList ID="dropSupplier" runat="server" Width="400px" OnSelectedIndexChanged="dropSupplier_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <br /><br />

                <asp:Image ID="imgPlus" runat="server" ImageUrl="~/images/plus.png" /><a href="javaScript:showDiv('rate_period_add')">Insert New Rate</a>
                <br /><br />

                <div id="rate_period_add" style="display:none">
                <input type="radio" id="Selling" value="1"  name="calType" />&nbsp;Selling Rate
                <input type="radio" id="Normal" value="0" name="calType" checked="checked" class="radioType" />&nbsp;Normal Rate
                <br /><br />
                 <div id="rate_cal_box" style="margin:0px 0px 0px 0px; padding:0px 0px 0px 0px;">

                     <div id="GVProductPeriod_ComBox_Sell" style="margin:0px 0px 5px 0px; padding:0px 0px 0px 0px; float:left">
                         Per&nbsp;<input id="GVProductPeriod_per" type="text"  value="0" style="width:30px;" class="TextBox_Extra_normal" onkeyup="CalComTotal_PeriodRate_Per();"   />&nbsp;&nbsp;
                         Net&nbsp;<input  id="GVProductPeriod_net" type="text"  value="0" style="width:30px;" class="TextBox_Extra_normal" onkeyup="CalComTotal_PeriodRate_Net();"  />
                     </div>
                     <div style="margin:0px 0px 5px 120px; padding:0px 0px px 0px;float:left">
                     <input type="button" class="btStyleGreen" value="Calculate Now" onclick="Calculate();"  />
                     </div>
                     <div id="GVProductPeriod_ComBox" style="margin:0px 0px 5px 120px; padding:0px 0px 0px 0px; float:left">
                        Coms&nbsp;<input id="GVProductPeriod_com" type="text" value="0"  style="width:30px;" class="TextBox_Extra_normal" onkeyup="CalComTotal_PeriodRate();"   />&nbsp;&nbsp;
                        Vat 7%&nbsp;<input id="Vat" type="checkbox" />
                     </div>
                     
                 </div>
                 <div style="clear:both"></div>
                 <asp:GridView ID="GVProductPeriod" runat="server" EnableModelValidation="True" AutoGenerateColumns="false"  DataKeyNames="PeriodId"  OnRowDataBound="GVProductPeriod_OnRowDataBound"  
                 ShowHeader="true"  ShowFooter="false" SkinID="ProductList" ClientIDMode="Static">
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
                            <asp:CheckBox ID="chkPeriod" runat="server" Checked="true" />
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
                            <asp:TextBox ID="txtPrice" runat="server" Width="80px" BackColor="#faffbd" Text="0" ClientIDMode="Static"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Own" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtOwn" runat="server" Width="80px" BackColor="#faffbd" Text="0" ClientIDMode="Static" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rack" ItemStyle-Width="15%">
                        <ItemTemplate> 
                            <asp:TextBox ID="txtRack" runat="server" Width="80px" BackColor="#faffbd" Text="0" ClientIDMode="Static"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                <br />
                <asp:Button ID="tbnSaveRate"  runat="server" Text="Save"  SkinID="Green"  OnClick="tbnSaveRate_OnClick"  />
                </div>
            <br />
            <div class="rate_period_Current_price">

                <input type="radio" id="Selling_current" value="1"  name="calTypePeriod" />&nbsp;Selling Rate
                <input type="radio" id="Normal_current" value="0" name="calTypePeriod" checked="checked" class="radioType" />&nbsp;Normal Rate
                <br /><br />
                 <div id="rate_cal_box_current" style="margin:0px 0px 0px 0px; padding:0px 0px 0px 0px;">

                     <div id="GVRatePeriodCurrent_ComBox_Sell" style="margin:0px 0px 5px 0px; padding:0px 0px 0px 0px; float:left">
                         Per&nbsp;<input id="GVRatePeriodCurrent_per" type="text"  value="0" style="width:30px;" class="TextBox_Extra_normal" onkeyup="CalComTotal_CurrentRate_Per();"   />&nbsp;&nbsp;
                         Net&nbsp;<input  id="GVRatePeriodCurrent_net" type="text"  value="0" style="width:30px;" class="TextBox_Extra_normal" onkeyup="CalComTotal_CurrentRate_Net();"  />
                     </div>
                     <div style="margin:0px 0px 5px 120px; padding:0px 0px 0px 0px;float:left">
                     <input type="button" class="btStyleGreen" value="Calculate Now" onclick="Calculate_current();"  />
                     </div>
                     
                     <div id="GVRatePeriodCurrent_ComBox" style="margin:0px 0px 5px 120px; padding:0px 0px 0px 0px; float:left">

                         Coms&nbsp;<input id="GVRatePeriodCurrent_com" type="text" value="0" style="width:30px;" class="TextBox_Extra_normal" onkeyup="CalComTotal_CurrentRate();"   />&nbsp;&nbsp;
                            Vat 7%&nbsp;<input id="Vat_current" type="checkbox" onclick="" />

                     </div>
                     
                 </div>
                 <div style="clear:both"></div>

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
                            <asp:TextBox ID="txtPrice" runat="server" Width="60px" BackColor="#faffbd" Text='<%# Bind("RatePrice", "{0:0}") %>' ClientIDMode="Static"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Own" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtOwn" runat="server" Width="60px" BackColor="#faffbd"  Text='<%# Bind("RateOwn", "{0:0.00}") %>' ClientIDMode="Static"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rack" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRack" runat="server" Width="60px" BackColor="#faffbd"  Text='<%# Bind("RateRack", "{0:0}") %>' ClientIDMode="Static"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="Edit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="tbnSaveEdit" runat="server" OnClick="tbnSaveEdit_Onclick"  CommandArgument='<%# Eval("PeriodId") + "," + DataBinder.Eval(Container, "DataItemIndex") %>' Text="Save" SkinID="Green_small" CommandName="Editrate" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="Quantity Rate" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:Button ID="SupQuan" runat="server"  Text="Add" SkinID="Green_small"  />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="SupQuan"  DisplayModalPopupID="ModalPopupExtender1" /><br />
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="SupQuan" PopupControlID="PNL"   CancelControlID="ButtonClose" BackgroundCssClass="modalBackground"  />
                            <asp:Panel ID="PNL" runat="server"  style="display:none; width:500px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                            <p style="margin:5px 0px 0px 0px; padding:0px; text-align:left; width:100%;  font-weight:bold; color:Black">Option Quantity List</p>
                            
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:GridView ID="GVSupQuan" runat="server"  AutoGenerateColumns="false" ShowFooter="false" DataKeyNames="SupplementID"  OnRowDataBound="GVSupQuan_OnrowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dropMin" runat="server" Width="70px"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dropMax" runat="server" Width="70px"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSup" runat="server" Width="60px" BackColor="#faffbd" Text='<%# Bind("SupAmount", "{0:0}") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSupQuanEdit" runat="server"  Text="Save" SkinID="Green_small" CommandArgument='<%# Eval("SupplementID") + "," + DataBinder.Eval(Container, "DataItemIndex") %>' OnClick="btnSupQuanEdit_Onclick" CommandName="SupQuanEdit"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3" DisplayAfter="2" DynamicLayout="true" >
                                <ProgressTemplate>
                                    <asp:Image ID="imgProgress"  runat="server" ImageUrl="~/images/progress_b.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>    
                            <%--CancelControlID="ButtonCancel"--%>
                            <div class="sup_quantity_insert_box">
                            <p>Insert Box</p>
                            <table >
                            <tr>
                                <td>Min</td><td>Max</td><td>(-)Amount</td>
                            </tr>
                            <tr>
                                <td><asp:DropDownList ID="dropMinInsert" runat="server" Width="70px"></asp:DropDownList></td>
                                <td><asp:DropDownList ID="dropMaxInsert" runat="server" Width="70px"></asp:DropDownList></td>
                                <td><asp:TextBox ID="txtAmountInsert" runat="server" Width="60px" BackColor="#faffbd" Text="0" ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                <asp:Button ID="SupQuanAdd" runat="server"  Text="Add" SkinID="Green_small" CommandArgument='<%# Eval("PeriodId") + "," + DataBinder.Eval(Container, "DataItemIndex") %>' OnClick="tbnSaveEdit_Onclick" CommandName="addquan" />
                                </td>
                            </tr>
                            </table>
                            
                            
                            </div>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="text-align:right;">
                                  <asp:Button ID="ButtonClose" runat="server" Text="Close"  SkinID="White_small" />
                            </div>
                            </asp:Panel>
                           </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
             </asp:GridView>
              
            </div>
           
        </asp:Panel>
       </div>  
    </div>
    
</asp:Content>
