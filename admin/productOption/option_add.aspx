<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="option_add.aspx.cs" Inherits="Hotels2thailand.UI.admin_productOption_option_add"  %>
<%@ Register Src="~/Control/Lang_Option_Content_Box.ascx" TagName="Lang_Option_Box" TagPrefix="Option" %>
<%@ Register Src="~/Control/Lang_Option_Facility_Box.ascx" TagName="Lang_Fac_option_Box" TagPrefix="Option" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/lert.css" rel="stylesheet" type="text/css"/>
    <script src="../../Scripts/lert.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">

        function Alertlightbox(messageinput) {

            var yes = new LertButton('Close Window', function () {
                //do nothing
            });

            var message = messageinput;
            var exampleLert = new Lert(
		message,
		[ yes],
		{
		    
		    
		});

            exampleLert.display();

        }

        

        function fnCheckUnCheck() {
            var grd = document.getElementById("<%= chkBoxSupplier.ClientID %>");

            //Collect A
            var rdoArray = grd.getElementsByTagName("input");
            var count = 0;
            for (i = 0; i <= rdoArray.length - 1; i++) {
               
                if (rdoArray[i].type == 'checkbox') {
                    
                    if (rdoArray[i].checked == true) {
                        rdoArray[i].parentNode.parentNode.style.backgroundColor = '#daf3d5';
                        count = count + 1;
                    }
                    else {
                        rdoArray[i].parentNode.parentNode.style.backgroundColor = '#ffffff';
                        
                    }

                }


            }
            if (count == 0) {

                Alertlightbox('Please Check Supplier');
                //Alertlightbox('<img src="../../images/alert.png" style="float:left;margin:0px; padding:2px;border:0px"><p class="alert_box_head">No Supplier Selected</p><p class="alert_box_detail">Please select at least one.</p>');
            }
        }
        


       
    </script>
    <h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="txthead" runat="server"></asp:Label></h6>
        <p style=" margin:0px; padding:0px; font-size:12px; font-weight:bold ">Option Title : <asp:Label ID="lblOptionTitle" runat="server"></asp:Label></p>

    <asp:Panel ID="panelSupplierSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> Supplier Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:CheckBoxList ID="chkBoxSupplier" runat="server"  RepeatDirection="Vertical" RepeatLayout="Table" CssClass="CheckBoxSupplier" Width="100%" OnDataBound="chkBoxSupplier_DataBound"   ></asp:CheckBoxList> 
        <asp:Panel ID="panelAlert" runat="server" CssClass ="alert_box" Visible="false">
                   <p class="alert_box_head">No Supplier Selected</p>
                   <p  class="alert_box_detail">Please select at least one.</p>
                 </asp:Panel>

        
    </asp:Panel>

    <asp:Panel ID="panelOptionCatSelect" CssClass="productPanel" runat="server"  >

    <h4><asp:Image ID="imgContentIcon" runat="server" ImageUrl="~/images/content.png" /> Option Category Selection</h4>
        <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
        <asp:DropDownList ID="dropCat" runat="server"  Width="300px" AutoPostBack="true" OnSelectedIndexChanged="dropCat_OnSelectedIndexChanged" ></asp:DropDownList>

            <asp:GridView ID="GvSupplierList" runat="server" DataKeyNames="Key" AutoGenerateColumns="false" SkinID="Nostyle" ShowFooter="false" ShowHeader="false" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <p style=" font-size:12px; padding:0px; margin:0px"><asp:Image ID="imgdot" runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;<asp:Label ID="lblSupplieroption" runat="server" Text='<%# Bind("Value") %>'  ></asp:Label></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        <div class="line" style="width:100%; margin:10px 0px 10px 0px; padding:0px;" ></div>
    
            

    <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Option Detail</h4>
        <p class="contentheadedetail">List Supplier of This Product, you can Change or Add Supplier to List </p><br />
    <asp:FormView ID="FvOptionAdd" runat="server" DataSourceID="ObjFvOption"  OnDataBound="FvOptionAdd_OnItemdataBound" DataKeyNames="OptionID"  OnItemUpdated="FvOptionAdd_OnItemUpdated"
        EnableModelValidation="True" Width="100%" OnItemInserting="FvOptionAdd_OnItemInserting" OnItemUpdating="FvOptionAdd_OnItemUpdating" >
        <EditItemTemplate>
        <table>
            <tr>
                <td><h5>Title</h5></td><td><asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>'   Width="600"/> </td>
            </tr>
             <tr>
                <td><h5>Size</h5></td><td><asp:TextBox ID="txtSize" runat="server" Text='<%# Bind("Size") %>' MaxLength="3"  Width="30px"/> *sq.m</td>
            </tr>
        </table>
            
            
            <asp:Panel ID="paneloptionExtra" runat="server">
            <div class="timeinput">
                <table style="float:left">
                    <tr>
                        
                        <td>
                            <table class="time_table_form">
                                <tr>
                                    <td colspan="2"><p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 0px 0px;padding:0px">Show or Service time</p></td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:12px; font-weight:bold; width:50px;">Start</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsStart" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsStart" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" style=" font-size:12px; font-weight:bold; width:50px;">End</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsEnd" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsEnd" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                </table>
                <table style="float:left;margin-left:60px">
                    <tr>
                        
                        <td>
                            <table  class="time_table_form">
                                <tr>
                                    <td colspan="2"><p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 0px 0px;padding:0px">Time to send</p></td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:12px; font-weight:bold; width:50px;">Start</td>
                                    <td> hrs:
                                      <asp:DropDownList ID="drphrssendStart" runat="server"></asp:DropDownList>
                                    </td>
                                    <td> mins:
                                      <asp:DropDownList ID="drpminssendStart" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:12px; font-weight:bold; width:50px;">End</td>
                                    <td> hrs:
                                      <asp:DropDownList ID="drphrssendEnd" runat="server"></asp:DropDownList>
                                    </td>
                                    <td> mins:
                                      <asp:DropDownList ID="drpminssendEnd" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div style=" clear:both"></div>
             </div>
            </asp:Panel>
            <div style="clear:both"></div>
            <br />
            <div  style="width:100%; text-align:left; "><asp:Button ID="Button1"  runat="server" CausesValidation="True" 
                CommandName="Update" Text="Save"  SkinID="Green" /></div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table>
            <tr>
                <td><h5>Title</h5></td><td><asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>'   Width="600"/> </td>
            </tr>
             <tr>
                <td><h5>Size</h5></td><td><asp:TextBox ID="txtSize" runat="server" Text='<%# Bind("Size") %>' MaxLength="3"  Width="30px"/> *sq.m</td>
            </tr>
            </table>
            <asp:Panel ID="paneloptionExtra" runat="server">
            <div class="timeinput">
                <table style="float:left">
                    <tr>
                        
                        <td>
                            <table class="time_table_form">
                                <tr>
                                    <td colspan="2"><p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 0px 0px;padding:0px">Show or Service time</p></td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:12px; font-weight:bold; width:50px;">Start</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsStart" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsStart" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:12px; font-weight:bold; width:50px;">End</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsEnd" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsEnd" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                </table>
                <table style="float:left;margin-left:60px">
                    <tr>
                        
                        <td>
                            <table class="time_table_form">
                                <tr>
                                    <td colspan="2"><p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 0px 0px;padding:0px">Time to send</p></td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:12px; font-weight:bold; width:50px;">Start</td>
                                    <td> hrs:
                                      <asp:DropDownList ID="drphrssendStart" runat="server"></asp:DropDownList>
                                    </td>
                                    <td> mins:
                                      <asp:DropDownList ID="drpminssendStart" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle"   style=" font-size:12px; font-weight:bold; width:50px;">End</td>
                                    <td> hrs:
                                      <asp:DropDownList ID="drphrssendEnd" runat="server"></asp:DropDownList>
                                    </td>
                                    <td> mins:
                                      <asp:DropDownList ID="drpminssendEnd" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div style=" clear:both"></div>
             </div>
            </asp:Panel>
            <div style="clear:both"></div>
            <br />
            <div  style="width:100%; text-align:left;margin:10px 0px 0px 0px; "><asp:Button ID="InsertButton"  runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Save" Width="100px" SkinID="Green"   OnClick="InsertButton_Onclick"  /></div>
            
        </InsertItemTemplate>
        
        
    </asp:FormView>
    <asp:ObjectDataSource ID="ObjFvOption" runat="server" 
        DataObjectTypeName="Hotels2thailand.ProductOption.Option" 
        InsertMethod="InsertOption" SelectMethod="getOptionById"  OnInserted="ObjFvOption_OnInserted"
        TypeName="Hotels2thailand.ProductOption.Option" UpdateMethod="UpdateOption" >
        <SelectParameters>
            <asp:QueryStringParameter Name="intOptionId" QueryStringField="oid" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </asp:Panel>

    <asp:Panel ID="panelWeekDay" runat="server" CssClass="productPanel">
       <h4><asp:Image ID="Image5" runat="server" ImageUrl="~/images/content.png" /> Option WeekDay</h4>
        <p class="contentheadedetail">Manage Priority, Status and Other</p><br />
        <asp:CheckBox ID="chkSun" runat="server"  Text="Sun" Checked="true"/>
        <asp:CheckBox ID="chkMon" runat="server"  Text="Mon" Checked="true"/>
        <asp:CheckBox ID="chkTue" runat="server"  Text="Tue" Checked="true"/>
        <asp:CheckBox ID="chkWed" runat="server"  Text="Wed" Checked="true"/>
        <asp:CheckBox ID="chkThu" runat="server"  Text="Thu" Checked="true"/>
        <asp:CheckBox ID="chkFri" runat="server"  Text="Fri" Checked="true"/>
        <asp:CheckBox ID="chkSat" runat="server"  Text="Sat" Checked="true"/>
        <br />
        <br />
        <asp:Button ID="btnWeekday" runat="server" Text="Save" SkinID="Green" OnClick="btnWeekday_OnClick" />
    </asp:Panel>

    <asp:Panel ID="panelOptionConfig" runat="server" CssClass="productPanel"  >
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Option Configuration</h4>
        <p class="contentheadedetail">Manage Priority, Status and Other</p><br />
            <div style="float:left; margin:0px 0px 0px 15px; padding:0xp 0px 0px 0px;">
                <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 0px 0px;padding:0px">Status</p>
                <asp:RadioButton ID="rbStatusEnable" runat="server" Checked="true" Text="Enable" GroupName="rbStatus"  />
                <asp:RadioButton ID="rbStatusDisable" runat="server" Text="Disable" GroupName="rbStatus"  />
            </div>    
            <div style="float:left; margin:0px 0px 0px 100px; padding:0xp 0px 0px 0px;">
            <p style="color:#3b59aa;font-size:12px;font-weight:bold; margin:5px 0px 0px 0px; padding:0px;">Show</p>
                    <asp:RadioButton ID="rbdetailShow" runat="server" Checked="true" Text="Enable"  GroupName="rbdetailShow" />
                    <asp:RadioButton ID="rbdetailnotShow" runat="server" Text="Disable" GroupName="rbdetailShow" />
            </div>       
            
             <div style=" clear:both"></div>  
             <div  style="width:100%; text-align:left;  margin:10px 0px 0px 0px;">
             <asp:Button ID="btConfig" runat="server" OnClick="btConfig_Onclik" Text="save" SkinID="Green"  /> 
             </div>
     </asp:Panel>
    <asp:Panel ID="panelOptionContentLang" runat="server" CssClass="productPanel">
        <Option:Lang_Option_Box ID="OptionLangBox" runat="server" />
    </asp:Panel>

    <%--<asp:Panel ID="panelItinerary" runat="server" CssClass="productPanel">
            <h4><asp:Image ID="Image9" runat="server" ImageUrl="~/images/content.png" /> Itinerary</h4>
            <p class="contentheadedetail">Add-Edit Condition</p><br />
             <asp:HyperLink ID="lnkItinerary" runat="server" Text="Click To Manage Itineray Page"></asp:HyperLink>
    </asp:Panel>--%>
    
    
    

     <asp:Panel ID="panelOptionFacility" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Amenity</h4>
        <p class="contentheadedetail">Manage Priority, Status and Other</p><br />

        <Option:Lang_Fac_option_Box ID="controlFacBox" runat="server" />
     </asp:Panel>
</asp:Content>

