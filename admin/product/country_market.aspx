<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="country_market.aspx.cs" Inherits="Hotels2thailand.UI.admin_country_market" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/DatepickerCalendar.ascx" TagName="Datapicker" TagPrefix="calendar" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
   <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.7.1.min.js"></script>
   <script type="text/javascript" language="javascript" src="../../scripts/jquery-ui-1.8.18.custom.min.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/datepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/darkman_utility.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            getMarketContent();

        });

        function getMarketContent() {
            var qMargetId = GetValueQueryString("mrid");
            if (qMargetId != "") {
                $.get("ajax_market_content_box.aspx?mrid=" + qMargetId, function (data) {

                    $("#content_lang").html(data);
                });
            }
        }

        function SavemarketContent() {
            var qMargetId = GetValueQueryString("mrid");
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#content_lang").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
           
            $.post("ajax_market_content_save.aspx?mrid=" + qMargetId, post, function (data) {
                
                if (data == "True") {
                    getMarketContent();
                }
            });
        }

        function ContentSwitchLangDisplayMain(langId, DivResult) {
            
            $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#" + DivResult).ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            $.get("ajax_product_content_Lang_switch_main.aspx?LangId=" + langId, function (data) {

                if (data == "True") {
                    if ($("#content_lang").length == 1) {
                        getMarketContent();
                    }

                   

                    langswitch(langId);
                }
            });
        }

    </script>
   	<style type="text/css">
   	    #countrySel{ background:#f7f7f7;}
        .list_market tr td
        {
            width:180px;
            margin:5px 0px 0px 0px;
            padding:2px;
         }
         .linkadd{ font-weight:normal;}
         .txtMarket{ color:Black; font-weight:bold}
        .titleEdit{}
        .groupList{}
        .ratefor{ float:left; width:300px}
        .except{float:left}
    
  </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="panelProductLevel" runat="server">
        <center>
        <asp:HyperLink ID="lnkMarket" runat="server">Market Group Manage</asp:HyperLink>
       </center>
    </asp:Panel>
       <asp:Panel ID="panelInsertMarket" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Market insert</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:TextBox ID="txtTitle" runat="server" Width="500px" ></asp:TextBox>
        <asp:Button ID="btnInsert" runat="server" Text="Add" SkinID="Green" OnClick="btnInsert_Onclick" />
            
        </asp:Panel>

        <asp:Panel ID="panelInsertAddGroup" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Market Edit</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
           <p><asp:DropDownList ID="dropMarket" runat="server"  EnableTheming="false" CssClass="DropDownStyleCustom_big" Width="800px" OnSelectedIndexChanged="dropMarket_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;<a href="javaScript:showDiv('titleEdit')">Edit Title</a></p>
           <div id="titleEdit" style="display:none">
                <asp:TextBox ID="txttitledit" runat="server" Width="500px" ></asp:TextBox>
                <asp:Button ID="btntitleedit" runat="server" Text="save" SkinID="Green" OnClick="btnEdit_Onclick" />
           </div>
           <div id="countrySel" style="display:none">
               <a href="javaScript:showDiv('countrySel')">close</a>
                <asp:GridView ID="GVContinent" runat="server" EnableModelValidation="false" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false"  SkinID="Nostyle" OnRowDataBound="GVContinent_OnRowDataBound"  DataKeyNames="Key">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                   <h6> <%# Container.DataItemIndex + 1 %>&nbsp;&nbsp;<asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Value") %>'></asp:Label></h6>
                                    <asp:CheckBoxList ID="chkCountryList" runat="server" RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="list_market" ClientIDMode="Static"  >
                                    </asp:CheckBoxList>
                            </ItemTemplate>
                        </asp:TemplateField>
            
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnCountry" runat="server" Text="Save" SkinID="Blue"  OnClick="btnCountry_OnClick"/>
           </div>
           <div id="groupSelect" style="display:none">
           <a href="javaScript:showDiv('groupSelect')">close</a>
                <asp:CheckBoxList ID="chkListGroup" runat="server"></asp:CheckBoxList>
                <asp:Button ID="btnGroupsave" runat="server" Text="Save" SkinID="Blue" OnClick="btnGroupsave_OnClick"/>
           </div>
           <div style=" clear: both"></div>
           <br />
           <div id="content_lang">
           </div>
           <br />
           <div class="groupList">
                <div class="ratefor">
                <p style=" font-weight:bold; font-size:14px; color:Black ;margin:0px; padding:0px;"> Rate for</p>
                    <asp:LinkButton ID="addCountryFor" runat="server" Text="Add By Country" OnClick="MarketSelection_Onclick"  CommandArgument="1" CommandName="RateFor" CssClass="linkadd"></asp:LinkButton>&nbsp;|&nbsp;
                    <asp:LinkButton ID="addGroupFor" runat="server" Text="Add By Group" OnClick="MarketSelection_Onclick"  CommandArgument="0" CommandName="RateFor" CssClass="linkadd"></asp:LinkButton>
                    <br /><br />
                    <asp:GridView ID="GvFor" runat="server" ShowFooter="false" ShowHeader="false" EnableModelValidation="false" AutoGenerateColumns="false" OnRowDataBound="GvFor_OnRowDataBound" SkinID="Nostyle" DataKeyNames="MarketId">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                <p  style=" margin:0px; padding:0px;"><asp:Image ID="imgbullet" runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;
                                <asp:Label ID="lblmarketSelection" runat="server" CssClass="txtMarket"></asp:Label>&nbsp;&nbsp;
                                <asp:ImageButton ID="imgbtn" runat="server" ImageUrl="~/images/remove.png" CommandArgument='<%# Eval("MarketId")+","+ Eval("CountryId")+","+ Eval("GroupId") %>' CommandName="RemoveRateFor" OnClick="imgbtn_Onclick" /></p>
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                                TargetControlID="imgbtn"  DisplayModalPopupID="ModalPopupExtender2" />
                                                                
                                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                                                TargetControlID="imgbtn" PopupControlID="Panel3" 
                                                                OkControlID="ButtonOks" 
                                                                CancelControlID="ButtonCancels" 
                                                                BackgroundCssClass="modalBackground"  />
                                                                <asp:Panel ID="Panel3" runat="server"  style="display:none; width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                                                                <p style="margin:0px; padding:0px; text-align:left; width:100%;  font-weight:bold; color:Black">Are you sure to Delete?</p><br />
                                                                <div style="text-align:right;">
                                                                        <asp:Button ID="ButtonOks" runat="server" Text="OK"  SkinID="Green_small" />
                                                                        <asp:Button ID="ButtonCancels" runat="server" Text="Cancel" SkinID="White_small" />
                                                                </div>
                                                                </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="except">
                <p style="font-weight:bold; font-size:14px; color:Black; margin:0px; padding:0px;">Except</p>
                    <asp:LinkButton ID="addCountryExcept" runat="server" Text="Add By Country" OnClick="MarketSelection_Onclick"  CommandArgument="1" CommandName="Except" CssClass="linkadd"></asp:LinkButton>&nbsp;|&nbsp;
                    <asp:LinkButton ID="AddGroupExcept" runat="server" Text="Add By Group" OnClick="MarketSelection_Onclick"  CommandArgument="0" CommandName="Except" CssClass="linkadd"></asp:LinkButton>
                    <br /><br />
                    <asp:GridView ID="GVExcept" runat="server" EnableModelValidation="false" ShowFooter="false" ShowHeader="false" AutoGenerateColumns="false"  OnRowDataBound="GVExcept_OnRowDataBound" SkinID="Nostyle" DataKeyNames="MarketId">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <p style=" margin:0px; padding:0px;"><asp:Image ID="imgbullet" runat="server" ImageUrl="~/images/greenbt.png" />&nbsp;
                                    <asp:Label ID="lblmarketSelection" runat="server" CssClass="txtMarket"></asp:Label>&nbsp;&nbsp;
                                    <asp:ImageButton ID="imgbtn" runat="server" ImageUrl="~/images/remove.png" CommandArgument='<%# Eval("MarketId")+","+ Eval("CountryId")+","+ Eval("GroupId") %>' CommandName="RemoveExcept" OnClick="imgbtn_Onclick" /> </p>
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                                TargetControlID="imgbtn"  DisplayModalPopupID="ModalPopupExtender2" />
                                                                
                                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
                                                                TargetControlID="imgbtn" PopupControlID="Panel3" 
                                                                OkControlID="ButtonOks" 
                                                                CancelControlID="ButtonCancels" 
                                                                BackgroundCssClass="modalBackground"  />
                                                                <asp:Panel ID="Panel3" runat="server"  style="display:none;  width:200px; background-color:#f2f2f2; border-width:3px; border-color:#3b5998; border-style:solid; padding:20px;">
                                                                <p style="margin:0px; padding:0px; text-align:left; width:100%;  font-weight:bold; color:Black">Are you sure to Delete?</p><br />
                                                                <div style="text-align:right;">
                                                                        <asp:Button ID="ButtonOks" runat="server" Text="OK"  SkinID="Green_small" />
                                                                        <asp:Button ID="ButtonCancels" runat="server" Text="Cancel" SkinID="White_small" />
                                                                </div>
                                                                </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
           </div>
           <div style=" clear: both"></div>
        </asp:Panel>
</asp:Content>
