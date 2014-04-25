<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="product_itinerary.aspx.cs" Inherits="Hotels2thailand.UI.admin_product_itinerary" %>
<%@ Register Src="~/Control/Lang_Itinerary_Content_Box.ascx" TagName="dateContentLang" TagPrefix="Product" %>
<%@ Register Src="~/Control/Lang_Itinerary_title_Content_Box.ascx" TagName="dateContentLangTitle" TagPrefix="Product" %>
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
<h6><asp:Image ID="imghead" runat="server" ImageUrl="~/images/imgheadtitle.png" />&nbsp;<asp:Label ID="txthead" runat="server"></asp:Label></h6>
<p style=" margin:0px; padding:0px; font-size:12px; font-weight:bold ">Option Title : <asp:Label ID="lblOptionTitle" runat="server"></asp:Label></p>
        <asp:Panel ID="panel2" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Itinerary Title </h4>
        <Product:dateContentLangTitle ID="controltitleLang" runat="server" />
        
            <asp:Panel ID="panelIsDefault" runat="server">
            <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Show Default ?</p>
            <asp:RadioButtonList ID="IsDefault" runat="server" OnSelectedIndexChanged="IsDefault_OnSelectedIndexChanged" AutoPostBack="true"  RepeatDirection="Horizontal">
                <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="False" Text="No"  Selected="True"></asp:ListItem>
            </asp:RadioButtonList>
            </asp:Panel>
        </asp:Panel>
    
        <asp:Panel ID="panel5" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" /> Itinerary Programe Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <p style="color:#3b59aa;font-size:12px; font-weight:bold; margin:5px 0px 5px 0px;padding:0px">Show Single Time ?</p>
            <asp:RadioButton ID="radioIsTimeCheckYes" runat="server" GroupName="TimeCheck" Text="Yes" TextAlign="Right"  />
            
            <asp:RadioButton ID="radioIsTimeCheckNo" runat="server" GroupName="TimeCheck"  Text="No "  TextAlign="Right" Checked="true" />
            <br />
            <p style=" margin:5px 0px 0px 0px; padding:0px"></p>
            <table class="time_table_form">
                               
                                <tr>
                                    <td valign="middle"   style=" font-size:11px; font-weight:bold; width:35px;">Open</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsStart" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsStart" runat="server">
                                      <%--<asp:ListItem Value="0" Text="00"></asp:ListItem>
                                       <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                       <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                       <asp:ListItem Value="45" Text="45"></asp:ListItem>--%>
                                      </asp:DropDownList>
                                    </td>
                                </tr>
                                
            </table>
            <div id="DivTimeService" style=" display:block">
                <table  class="time_table_form">
                    <tr>
                                    <td valign="middle"   style=" font-size:11px; font-weight:bold; width:35px;">Close</td>
                                    <td>hrs:
                                      <asp:DropDownList ID="drpHrsEnd" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>mins:
                                      <asp:DropDownList ID="drpMinsEnd" runat="server">
                                      <%-- <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                       <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                       <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                       <asp:ListItem Value="45" Text="45"></asp:ListItem>--%>
                                      </asp:DropDownList>
                                    </td>
                                </tr>
                </table>
            </div>
            <asp:Button ID="btnSave" runat="server" Text="Save" SkinID="Green"  OnClick="btnSave_Onclick"/>
        </asp:Panel>
        <asp:Panel ID="panel1" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Itinerary Programe</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>--%>
                <asp:GridView ID="GVItinerayList"  runat="server" ShowFooter="false" ShowHeader="true" EnableModelValidation="true" DataKeyNames="ItineraryItemID" OnRowDataBound="GVItinerayList_OnRowDataBound"  AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                               <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  HeaderText="From - To" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
                            <ItemTemplate>
                               <asp:Label ID="lblTimePrograme" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Detail" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50%">
                            <ItemTemplate>
                               <Product:dateContentLang ID="ContentLang" runat="server" ClientIDMode="AutoID"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                               <asp:ImageButton ID="imgBtDelete"  runat="server" ImageUrl="~/images/bin.png" CommandArgument='<%# Eval("ItineraryItemID") + "," + Container.DataItemIndex %>' CommandName="ItiDel" OnClick="imgBtDelete_Onclick" />
                               <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="imgBtDelete"  DisplayModalPopupID="ModalPopupExtender1" />
                         <br />
                         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imgBtDelete" PopupControlID="PNL" 
                           OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground"  />
                          <asp:Panel ID="PNL" runat="server"  style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998;  text-align:left; border-style:solid; padding:20px;">
                             Are you sure you want to Delete?<br /><br />
                             <div style="text-align:right;">
                              <asp:Button ID="ButtonOk" runat="server" Text="OK"  SkinID="Green_small" />
                            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" SkinID="White_small" />
                             </div>
                             </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
             <%--</ContentTemplate>
            </asp:UpdatePanel>  --%> 
        </asp:Panel>
   
   
</asp:Content>
