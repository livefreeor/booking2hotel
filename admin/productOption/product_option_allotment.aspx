<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true"  CodeFile="product_option_allotment.aspx.cs" 
 Inherits="Hotels2thailand.UI.admin_product_option_allotment"  %>
<%@ Register Src="~/Control/DatepickerCalendar.ascx" TagName="WeekDAy_datePicker" TagPrefix="Product" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
    <link href="../../css/lert.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="../../Scripts/lert.js" ></script>	
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">
//        $(document).ready(function getDivDateRange(div, optionID) {
//            $("div[id$='" + div + "']").load("ajax_allotment_date_range.aspx?oid=" + optionID);
//
        //        });

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

        function SemiHacky() {
            __doPostBack('', '');
        }

        function checkAllotEdit() {
            var grds = document.getElementById("<%= GvRomAllotList.ClientID %>");
            
            var chk = grds.getElementsByTagName("input");
            for (i = 0; i < chk.length; i++) {

                if (chk[i].type == 'checkbox') {
                    var descheck = document.getElementById('ChekHead');
                    if (chk[i].id != 'ChekHead') {
                        if (descheck.checked) {
                            chk[i].checked = true;
                        }
                        else {
                            chk[i].checked = false;
                        }
                    }
                    
                    //chk[i].checked = (chk[i].checked == true) ? false : true;
                }
            }

        }
        
        function getDivDateRange(div,optionID) {
            $("#" + div + "").load("ajax_allotment_date_range.aspx?oid=" + optionID);

        }

        function showDivTwinUnCheckBox(id_name, name_id) {
            var target = document.getElementById(id_name);
            var t2 = document.getElementById(name_id);
            target.style.display = (target.style.display == "none") ? "block" : "none";
            t2.style.display = (t2.style.display == "none") ? "block" : "none";

            var grd = document.getElementById("<%= chkOptionList.ClientID %>");
            var rdoArray = grd.getElementsByTagName("input");

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].type == 'checkbox') {
                    rdoArray[i].checked = false;
                }

            }
        }
	</script>
    <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
    
    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Supplier Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:DropDownList ID="dropDownSupplierList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropDownSupplierList_OnSelectedIndexChanged" Width="400px"></asp:DropDownList>
    </asp:Panel>
    
    <asp:Panel ID="panelAllotaddEdit" runat="server" CssClass="productPanel">
     <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Allotment Date Range Add - Edit</h4>
        <p class="contentheadedetail">Manage allotment by date range</p><br />
        
        <div id="option_supplement_add_dropdown">
        <asp:DropDownList ID="ProductOptionList" runat="server" Width="400px"></asp:DropDownList>
        <a href="javaScript:showDivTwinUnCheckBox('option_supplement_add_checkbox_list','option_supplement_add_dropdown')">Advance Selection</a>
        </div>
        
        <div id="option_supplement_add_checkbox_list" style="display:none">
            <a href="javaScript:showDivTwinUnCheckBox('option_supplement_add_checkbox_list','option_supplement_add_dropdown')">Close</a>
            <asp:CheckBoxList ID="chkOptionList" runat="server" RepeatDirection="Vertical" RepeatLayout="Table"   >
                
            </asp:CheckBoxList>
        </div>
        <div class="option_week_day_add_date_picker">
        <table>
            <tr>
                <td valign="middle"><h5>Date Range</h5></td>
                <td><Product:WeekDAy_datePicker Id="DatePicker" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td ><h5 >Number of Rooms</h5></td>
                            <td><asp:DropDownList ID="dropNumRoom" runat="server"></asp:DropDownList></td>
                            <td style="padding-left:15px"><h5>day cut off</h5></td>
                            <td><asp:DropDownList ID="dropcutoff" runat="server"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="middle"><h5>Status</h5></td>
                <td>
                    <asp:RadioButtonList ID="raioStatus" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Table">
                        <asp:ListItem Text="Enable" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Disable" Value="0" ></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            
        </table>
            
        </div>
        <asp:Button ID="btnAllotmentSave" runat="server" Text="Save"  SkinID="Green"  OnClick="btnAllotmentSave_OnClick"/>
        </asp:Panel>

    <asp:Panel ID="panel1" runat="server" CssClass="productPanel">
        <div style="margin:0px; padding:0px; float:right">
            <table>
                <tr>
                    <td><Product:WeekDAy_datePicker ID="dateRangeEdit" runat="server" /></td>
                    <td valign="bottom"><asp:Button ID="btnNormalEdit" runat="server" SkinID="Green" Text="Go" OnClick="btnNormalEdit_OnClick" /></td>
                </tr>
            </table>
        </div>
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Rooms list of allotment.</h4>
        <p class="contentheadedetail">Show Room type which allotment Effective</p>
        
        <asp:GridView ID="GvRomAllotList" runat="server" ShowFooter="false" 
         OnRowDataBound="GvRomAllotList_OnRowDataBound" EnableModelValidation="false"  DataKeyNames="Key"  AutoGenerateColumns="false"  >
            <Columns>
                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <HeaderTemplate>
                    <asp:CheckBox ID="ChekHead" runat="server" ClientIDMode="Static" onclick="checkAllotEdit()" />
                </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEdit" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="No."  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Room Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="55%" ItemStyle-Font-Bold="true" >
                    <ItemTemplate>
                       
                        &nbsp;&nbsp;<asp:Label ID="lblroomtitle" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Range Allotment Active"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <p style=" margin:0px; padding:0px;  color:Black">
                        <asp:Label ID="lblDateStart" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;<asp:Label ID="lbldateEnd" runat="server"></asp:Label>
                        </p>
                        <%--<div id="dateRange<%#Eval("Key") %>" onload="getDivDateRange(dateRange<%#Eval("Key") %>);">
                        <asp:Image ID="imgprogress" runat="server" ImageUrl="~/images/progress.gif" />
                       </div>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
       
    </asp:Panel>
     

    

</asp:Content>
