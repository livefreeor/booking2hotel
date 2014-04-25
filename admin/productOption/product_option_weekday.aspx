<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_option_weekday.aspx.cs" Inherits="Hotels2thailand.UI.admin_productOption_product_option_weekday" %>
<%@ Register Src="~/Control/DatepickerCalendar.ascx" TagName="WeekDAy_datePicker" TagPrefix="Product" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
       

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
    <h6><asp:Label ID="Destitle" runat="server" ></asp:Label> : <asp:Label ID="txthead" runat="server"></asp:Label></h6>
    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Supplier Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:DropDownList ID="dropDownSupplierList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropDownSupplierList_OnSelectedIndexChanged" Width="400px"></asp:DropDownList>
    </asp:Panel>
    <asp:Panel ID="panelsupplementadd" runat="server" CssClass="productPanel">
     <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> SuppleMent Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        
        <div id="option_supplement_add_dropdown">
        <asp:DropDownList ID="ProductOptionList" runat="server" Width="400px"></asp:DropDownList>
        <a href="javaScript:showDivTwinUnCheckBox('option_supplement_add_checkbox_list','option_supplement_add_dropdown')">Advance Selection</a>
        </div>
        
        <div id="option_supplement_add_checkbox_list" style="display:none">
            <a href="javaScript:showDivTwinUnCheckBox('option_supplement_add_checkbox_list','option_supplement_add_dropdown')">Close</a>
            <asp:CheckBoxList ID="chkOptionList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3" >
                
            </asp:CheckBoxList>
        </div>
        <div class="option_week_day_add_date_picker">
        <table>
            <tr>
                <td valign="middle">Date Rank</td>
                <td><Product:WeekDAy_datePicker Id="DatePicker" runat="server" /></td>
            </tr>
        </table>
            
        </div>
        <div class="option_week_day_add_rate">
            <table>
                <tr>
                    <td valign="middle">Day of Week </td>
                    <td>
                        <table>
                        <tr>
                            <td>Sun</td>
                            <td>Mon</td>
                            <td>Tue</td>
                            <td>Wed</td>
                            <td>Thu</td>
                            <td>Fri</td>
                            <td>Sat</td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="txtSun" runat="server" Width="50px" Text="0" BackColor="#faffbd"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegtxtSun" runat="server" ControlToValidate="txtSun" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                            </td>
                            <td><asp:TextBox ID="txtMon" runat="server" Width="50px" Text="0" BackColor="#faffbd"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMon" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                            </td>
                            <td><asp:TextBox ID="txtTue" runat="server" Width="50px" Text="0" BackColor="#faffbd"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTue" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                            </td>
                            <td><asp:TextBox ID="txtWed" runat="server" Width="50px" Text="0" BackColor="#faffbd"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWed" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                            </td>
                            <td><asp:TextBox ID="txtThu" runat="server" Width="50px" Text="0" BackColor="#faffbd"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtThu" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                            </td>
                            <td><asp:TextBox ID="txtFri" runat="server" Width="50px" Text="0" BackColor="#faffbd"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtFri" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                            </td>
                            <td><asp:TextBox ID="txtSat" runat="server" Width="50px" Text="0" BackColor="#faffbd"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtSat" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                            </td>
                            <td><asp:Button ID="btSave" runat="server" Text="Save" SkinID="Green" OnClick="btSave_onClick"  ValidationGroup="day"/></td>
                        </tr>
                    </table>
                    </td>
                </tr>
            </table>
            
        </div>
    </asp:Panel>
   <asp:Panel ID="panelsupplementList" runat="server" CssClass="productPanel">
    <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> SupplementDate List</h4>
        <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List  <asp:DropDownList ID="DropoptionShowList" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="DropoptionShowList_OnSelectedIndexChanged"></asp:DropDownList></p><br />
       
        <asp:GridView ID="GVSupplementList" runat="server" DataKeyNames="SupplementDayId" EnableModelValidation="true" 
        AutoGenerateColumns="false"  OnRowDataBound="GVSupplementList_OnRowDataBound">
            <Columns>
                <asp:TemplateField  HeaderText="No." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="From - To" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="javaScript:showDiv('dDate<%# Eval("SupplementDayId") %>')"><asp:Label ID="lblDateStart" runat="server" Text='<%# Bind("DateStart","{0:MMM d, yyyy}") %>' ></asp:Label>&nbsp;&nbsp;-&nbsp;&nbsp;
                        <asp:Label ID="lblDateEnd" runat="server" Text='<%# Bind("DateEnd","{0:MMM d, yyyy}") %>' ></asp:Label></a>
                        <div id="dDate<%# Eval("SupplementDayId") %>" style="display:none">
                           <Product:WeekDAy_datePicker ID="dDatePickerEdit"  runat="server" ClientIDMode="AutoID" />
                           <asp:Button ID="btdDateSave" runat="server" Text="Save" SkinID="Green_small" CommandArgument='<%# Eval("SupplementDayId") + "," + Container.DataItemIndex %>'
                            CommandName="dDateEdit" OnClick="Supplement_OnClick"  />
                           
                        </div>
                    </ItemTemplate> 
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="Day Of Week" ItemStyle-Width="55%" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div class="option_week_day_add_rate">
            <table style=" padding:5px;">
                <tr>
                    <td>Sun</td>
                    <td>Mon</td>
                    <td>Tue</td>
                    <td>Wed</td>
                    <td>Thu</td>
                    <td>Fri</td>
                    <td>Sat</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtSun" runat="server" Width="50px"  BackColor="#faffbd" Text='<%#Bind("DaySun", "{0:0}") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegtxtSun" runat="server" ControlToValidate="txtSun" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                    </td>
                    <td><asp:TextBox ID="txtMon" runat="server" Width="50px"  BackColor="#faffbd" Text='<%#Bind("DayMon", "{0:0}") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMon" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                    </td>
                    <td><asp:TextBox ID="txtTue" runat="server" Width="50px"  BackColor="#faffbd" Text='<%#Bind("DayTue", "{0:0}") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTue" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                    </td>
                    <td><asp:TextBox ID="txtWed" runat="server" Width="50px" BackColor="#faffbd" Text='<%#Bind("DayWed", "{0:0}") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWed" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                    </td>
                    <td><asp:TextBox ID="txtThu" runat="server" Width="50px" BackColor="#faffbd" Text='<%#Bind("DayThu", "{0:0}") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtThu" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                    </td>
                    <td><asp:TextBox ID="txtFri" runat="server" Width="50px"  BackColor="#faffbd" Text='<%#Bind("DayFri", "{0:0}") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtFri" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                    </td>
                    <td><asp:TextBox ID="txtSat" runat="server" Width="50px" BackColor="#faffbd" Text='<%#Bind("DaySat", "{0:0}") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtSat" Display="Dynamic" ValidationExpression="^[0-9|-]+$" ValidationGroup="day" Text="**Number Only"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:Button ID="btEdit" runat="server" Text="Save" SkinID="Green_small" ValidationGroup="day"  CommandArgument='<%# Eval("SupplementDayId") + "," + Container.DataItemIndex %>' CommandName="DayAmountEdit" OnClick="Supplement_OnClick" />
                    </td>
                </tr>
            </table>
        </div>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField  HeaderText="Status" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        &nbsp;<asp:ImageButton ID="btDelete" runat="server" CommandName="dis" CommandArgument='<%# Eval("SupplementDayId") + "," + Eval("Status") + "," +  Container.DataItemIndex %>'  ToolTip="Click To Delete" SkinID="Blue_small" OnClick="btStatusUp_OnClick" />
                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btDelete"  DisplayModalPopupID="ModalPopupExtender1" />
                         <br />
                         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btDelete" PopupControlID="PNL" 
                           OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground"  />
                          <asp:Panel ID="PNL" runat="server"  style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998;  text-align:left; border-style:solid; padding:20px;">
                             Are you sure you want to Update Status?<br /><br />
                             <div style="text-align:right;">
                              <asp:Button ID="ButtonOk" runat="server" Text="OK"  SkinID="Green_small" />
                                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" SkinID="White_small" />
                             </div>
                             </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
