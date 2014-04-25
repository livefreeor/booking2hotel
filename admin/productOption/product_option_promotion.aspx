<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_option_promotion.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_option_promotion" %>
<%@ Register  Src="~/Control/DatepickerCalendar.ascx" TagName="DatePicker_Add_Edit" TagPrefix="DateTime" %>
<%@ Register Src="~/Control/Lang_Promotion_Box.ascx" TagName="Promotion_Content" TagPrefix="LangBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

        function fnChildCheckUnCheck(objId) {
            var chidCheck = document.getElementById(objId);
            if (chidCheck.checked == false) {
                chidCheck.parentNode.parentNode.style.backgroundColor = '#ffffff'
            }
            else {
                chidCheck.parentNode.parentNode.style.backgroundColor = '#daf3d5'
            }
        }

        function fnCheckUnCheckOnLoad(objId) {
            
//            var grd = document.getElementById("<%= GvRoomTypeSelectionList.ClientID %>");
//            var rdoArray = grd.getElementsByTagName("input");
            
            var checkCondition = document.getElementById(objId);
            
            if (checkCondition.type == 'checkbox') {
                checkCondition.checked = true;
                checkCondition.parentNode.parentNode.style.backgroundColor = '#daf3d5'
                checkCondition.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.display = "block";
                

                checkCondition.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.backgroundColor = '#eceff5';

                var CheckParent = checkCondition.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.getElementsByTagName('input');
                if (CheckParent[0].type == 'checkbox') {
                    CheckParent[0].checked = true;
                }
            }

            
           
        }

        function fnCheckUnCheck(objId) {
            var grd = document.getElementById("<%= GvRoomTypeSelectionList.ClientID %>");

            //Collect A
            var rdoArray = grd.getElementsByTagName("input");

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].type == 'checkbox') {
                    if (rdoArray[i].id == objId) {
                        var ChildDiv = rdoArray[i].parentNode.parentNode.getElementsByTagName("div");
                        var chidCheck = ChildDiv[0].getElementsByTagName("input");
                        if (rdoArray[i].checked) {
                            
                            ChildDiv[0].style.display = "block";
                            

                            
                            for (j = 0; j <= chidCheck.length - 1; j++) {
                                if (chidCheck[j].type == 'checkbox') {
                                    chidCheck[j].checked = true;
                                    chidCheck[j].parentNode.parentNode.style.backgroundColor = '#daf3d5'
                                }
                            }


                            rdoArray[i].parentNode.parentNode.style.backgroundColor = '#eceff5';
                            
                        }
                        else {
                            //var ChildDiv = rdoArray[i].parentNode.parentNode.getElementsByTagName("div");
                                ChildDiv[0].style.display = "none";

                            //var chidCheck = ChildDiv[0].getElementsByTagName("input");
                            for (j = 0; j <= chidCheck.length - 1; j++) {
                                if (chidCheck[j].type == 'checkbox') {
                                    chidCheck[j].checked = false;
                                    chidCheck[j].parentNode.parentNode.style.backgroundColor = '#ffffff'
                                }
                            }
                            rdoArray[i].parentNode.parentNode.style.backgroundColor = '#ffffff';

                        }
                        
                        
                    }
                   
                }
            }
        }




       
    </script>
   <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="txthead" runat="server"></asp:Label></h6>
    
    <%--AutoPostBack="true" OnSelectedIndexChanged="dropDownSupplierList_OnSelectedIndexChanged"--%>
    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Supplier Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:DropDownList ID="dropDownSupplierList" runat="server"  Width="400px"></asp:DropDownList>
    </asp:Panel>
    <asp:Panel ID="panelSupplierActive" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image6" runat="server" ImageUrl="~/images/content.png" /> For Supplier </h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <p style=" margin:0px; padding:0px; font-size:12px;  color:Black; font-weight:bold"><asp:Label ID="SupplierActive" runat="server"></asp:Label></p>
    </asp:Panel>
    <asp:Panel ID="panelPromotionDetail" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Prmotion Detail</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        
        <table>
            <tr>
                <td><h5>Programe</h5></td>
                <td><asp:TextBox ID="txtPrograme" runat="server" Width="800px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><h5>Promotion By</h5></td>
                <td><asp:DropDownList ID="dropPromotionCat" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><h5>Programe Start</h5></td>
                <td><DateTime:DatePicker_Add_Edit ID="controlProgrameStart" runat="server" /></td>
            </tr>
            <tr>
                <td><h5>Comment</h5></td>
                <td><asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="5" Width="800px"> </asp:TextBox></td>
            </tr>
             <tr>
                    <td><h5>Status</h5></td>
                    <td>
                        <asp:RadioButtonList ID="radioStatus" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Enable" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Disable" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
        </table>
        <br />
        <asp:Button ID="btnSaveProdetail" runat="server" SkinID="Green" Text="Save" OnClick="btnSaveProdetail_OnClick" />
        
    </asp:Panel>
    
     <asp:Panel ID="panelPromotionCOntent" runat="server" CssClass="productPanel">
        <LangBox:Promotion_Content ID="ContentLang" runat="server" />
     </asp:Panel>

     <asp:Panel ID="panelPromotioninformation" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Prmotion Information</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />

        <div style=" float:left; width:500px">
                <h5> <asp:Image ID="imgFlag" runat="server" ImageUrl="~/images/flag.png" />   Is This Programe applicable to any day of the week?</h5>
        <asp:RadioButton ID="radioIsWeekendYes" runat="server" GroupName="IsWeekend" Text="Yes, This programe for any day of week" TextAlign="Right" Checked="true" />
            <br /><p style=" margin:5px 0px 0px 0px; padding:0px"></p>
        <asp:RadioButton ID="radioIsWeekendNo" runat="server" GroupName="IsWeekend"  Text="No, Select day(s)"  TextAlign="Right" />
        <div id="DivdayofWeek" style=" display:none; margin:10px 0px 0px 20px;">
            <table >
                      <tr>
                        <td><h5>Day of Week</h5></td>
                        <td ><asp:CheckBoxList ID="chkDayofWeek" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Table"  TextAlign="Right" CssClass="chekBoxListStyles" >
                            <asp:ListItem Text="Sun" Value="true"> </asp:ListItem>
                            <asp:ListItem Text="Mon" Value="true"> </asp:ListItem>
                            <asp:ListItem Text="Tue" Value="true"> </asp:ListItem>
                            <asp:ListItem Text="Wed" Value="true"> </asp:ListItem>
                            <asp:ListItem Text="Thu" Value="true"> </asp:ListItem>
                            <asp:ListItem Text="Fri" Value="true"> </asp:ListItem>
                            <asp:ListItem Text="Sat" Value="true"> </asp:ListItem>
                        </asp:CheckBoxList></td>
                        
                      </tr> 
                      <tr>
                        <td><h5>Promotion Exception</h5></td>
                        <td>
                            <asp:DropDownList ID="dropPromotinonException" runat="server">
                            <asp:ListItem Text="Not Allow All Exception Day" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Allow Only Promotion, Not Apply On Weekend" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Allow All, Except Exception Day" Value="3" ></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                      </tr>          
            </table>
            </div>
            <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:20px 0px 5px 0px;padding:0px">Breakfast Policy</p>
            <asp:RadioButtonList ID="radioBreakfast" runat="server">
                <asp:ListItem Text="Breakfast Included" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Breakfast Not Included" Value="2"></asp:ListItem>
                <asp:ListItem Text="Breakfast Not Included- Compulsory Charge" Value="3"></asp:ListItem>
            </asp:RadioButtonList>
            <table>
                <tr>
                    <td><h5>Charge : </h5></td>
                    <td><asp:TextBox ID="txtBreakfastCharge" runat="server" Width="80px" BackColor="#faffbd" Text="0"></asp:TextBox></td>
                </tr>
            </table>

            <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:20px 0px 5px 0px;padding:0px">Promotion Period Valid</p>
                <table >
                      <tr>
                        <td valign="middle"><h5>Date Range</h5></td>
                        <td> <DateTime:DatePicker_Add_Edit ID="controlDateUsePro" runat="server" /></td>
                      </tr>
            </table>
        </div>
        <div style=" float:left; margin:0px; padding:0px">
            <table style=" margin:0px 0px 0px 0px;">
                <tr>
                    <td><h5>Minimum night of stay</h5></td>
                    <td><asp:DropDownList ID="dropDayMin" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><h5>Minimum Room of stay</h5></td>
                    <td><asp:DropDownList ID="dropRoomMin" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><h5>Advance booking require (min)</h5></td>
                    <td><asp:DropDownList ID="dropDayAdvanceMin" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><h5>Is public holiday applicable?</h5></td>
                    <td>
                        <asp:RadioButtonList ID="radioIsholiday" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td><h5>The number of re-use of promotions.</h5></td>
                    <td><asp:DropDownList ID="dropMaxset" runat="server"></asp:DropDownList></td>
                </tr>
                
            </table>

            <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:10px 0px 5px 0px;padding:0px">Promotion Time Preiod : 
            <asp:RadioButton ID="radioTimeproYes" runat="server" Text="Yes"  GroupName="proTimePeriod"/>
            <asp:RadioButton ID="radioTimeproNo" runat="server" Text="No"  GroupName="proTimePeriod" Checked="true"/></p>
            <div id="DivTimePeriod" style=" display:none">
            <table class="time_table_form">
                               
                                <tr>
                                    <td valign="middle"   style=" font-size:11px; font-weight:bold; width:35px;">Start</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsStart" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsStart" runat="server">
                                      <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                       <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                       <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                       <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:11px; font-weight:bold; width:35px;">End</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsEnd" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsEnd" runat="server">
                                       <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                       <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                       <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                       <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>
                                </tr>
            </table>
            </div>
        </div>
        
        <div style=" clear:both"></div>
            
            
            
            

            

            <br />
            <asp:Button ID="btnProInformationSave" runat="server" Text="Save" SkinID="Green" OnClick="btnProInformationSave_OnClick" />
     </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
     <asp:Panel ID="panelbenefit" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Promotion Discount & Benefit List</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Benefit List</p>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
        <ProgressTemplate>
            <asp:Image ID="imgProgress" runat="server" ImageUrl="~/images/progress_b.gif" />
        </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:GridView ID="GvProBenefitList" runat="server" ShowFooter="false" ShowHeader="true"
         EnableModelValidation="true" AutoGenerateColumns="false" DataKeyNames="BenefitID" OnRowDataBound ="GvProBenefitList_OnDataBound">
            <EmptyDataRowStyle   CssClass="alert_box" />
                    <EmptyDataTemplate>
                         <div class="alert_inside_GridView" >
                               <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                               <p class="alert_box_head">There are no Benefit List , Please Insert</p>
                               <p class="alert_box_detail">Please select at least one.</p>
                        </div>
                 </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Promotion Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:DropDownList ID="dropProType" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start discount night" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:DropDownList ID="dropStartDisnight" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No. of discounted night" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:DropDownList ID="dropNoDisNight" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Term of discount (% or Baht)" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <asp:TextBox Id="txtDisAmount" runat="server" Width="80px" BackColor="#faffbd" Text="0"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Pirority" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <asp:DropDownList ID="dropPirority" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Save" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="tbnBenefitSaveEdit" runat="server" SkinID="Blue_small" Text="Save" OnClick="tbnBenefitSaveEdit_OnClick" CommandArgument='<%# Eval("BenefitID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtnStatus" runat="server" OnClick="imgbtnStatus_StatusUPdate" CommandArgument='<%# Eval("BenefitID") + "," + Container.DataItemIndex + "," + Eval("Status") %>' />
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
        <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:20px 0px 5px 0px;padding:0px">InsertBox</p>
        <asp:Panel ID="panelAlertBox" runat="server" Visible="false" CssClass="alert_box">
             <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
           <p class="alert_box_head">There are no Benefit List , Please Insert</p>
             <p class="alert_box_detail">Please select at least one.</p>
        </asp:Panel>
        
        <table>
            <tr>
                <td><h5>Promotion Type</h5></td>
                <td><asp:DropDownList ID="dropproType" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><h5>Start discount night</h5></td>
                <td><asp:DropDownList ID="dropStartDisCountnight" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><h5>No. of discounted night</h5></td>
                <td><asp:DropDownList ID="dropNumDiscountnight" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><h5>Term of discount (% or Baht)</h5></td>
                <td><asp:TextBox ID="txtDiscount" runat="server" Width="80px"  BackColor="#faffbd" Text="0"></asp:TextBox></td>
            </tr>
            
        </table>
        <br />
            <asp:Button ID="btnBenefitSave" runat="server" Text="Save" SkinID="Green"  OnClick="btnBenefitSave_OnClick" />
     </asp:Panel>

     </ContentTemplate>
    </asp:UpdatePanel> 
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>--%>
      <asp:Panel ID="panelRoomSelect" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image5" runat="server" ImageUrl="~/images/content.png" /> Room Type  & Condition Selecttion</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        
        <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:10px 0px 5px 0px;padding:0px">Option List</p>
          
            
          <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image ID="imgProgressOptionList" runat="server" ImageUrl="~/images/progress_b.gif" />
            </ProgressTemplate>
          </asp:UpdateProgress>--%>
          <a name="conditionSel"></a>
        <asp:GridView ID="GvRoomTypeSelectionList" runat="server" ShowFooter="false"   ShowHeader="false" EnableModelValidation="false" AutoGenerateColumns="false" SkinID="Nostyle" DataKeyNames="OptionID" OnRowDataBound="GvRoomTypeSelectionList_OnRowDataBound">
            <EmptyDataRowStyle   CssClass="alert_box" />
                    <EmptyDataTemplate>
                         <div class="alert_inside_GridView" >
                               <asp:Image ID="imagAlert" runat="server" ImageUrl="~/images/alert_s.png"  CssClass="imageAlert" />
                               <p class="alert_box_head">No Option Lsit</p>
                               <p class="alert_box_detail">Please Insert New One.</p>
                        </div>
                 </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRoom" runat="server"  />
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>' CssClass="optiontitle"></asp:Label>
                        <div id="childConditionList" style=" display:none; ">
                            <asp:GridView ID="GVConditionSelectionLit" runat="server" ShowFooter="false" ShowHeader="false" EnableModelValidation="false" 
                            AutoGenerateColumns="false" SkinID="Nostyle" DataKeyNames="ConditionId" OnRowDataBound="GVConditionSelectionLit_OnRowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCondition" runat="server"  />
                                        <asp:Label ID="lblConditionTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <br />
            <asp:Button ID="btnRoomSelectionListSave" runat="server" Text="Save" SkinID="Green"  OnClick="btnRoomSelectionListSave_OnClick"/>
     </asp:Panel>
    <%--</ContentTemplate>
         </asp:UpdatePanel>--%>
    
        

    
        
    
</asp:Content>
