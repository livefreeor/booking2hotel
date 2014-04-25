<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="extranetManage.aspx.cs" Inherits="Hotels2thailand.UI.admin_extranet_extranetManage" %>
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
<div class="option_add_right">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Current Commission Period</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />

    <div class="product_period_box" style="padding-left:30px">

   <asp:GridView ID="GvCommssionPeriod" runat="server" 
        EnableModelValidation="True" AutoGenerateColumns="false"  DataKeyNames="Commission_id"  OnRowDataBound="GvCommssionPeriod_OnRowDataBound"  SkinID="Nostyle" ShowHeader="false" 
        OnRowCommand="GvCommssionPeriod_OnRowCommand">
       
        <Columns>
            <asp:TemplateField>         
                <ItemTemplate>        
                    <h4><%# Container.DataItemIndex + 1 %> </h4>
                    <p><%# Eval("Commission_id")%></p>   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                <div  style="width:1000px;">
                <div style=" float:left"><DateTime:DatePicker_Add_Edit ID="DateTimePicker" runat="server" ClientIDMode="AutoID"   /></div>
                <div style=" float:left;margin:18px 0px 0px 0px"><asp:TextBox ID="txtCom" runat="server" Text='<%#  Bind("Commission")%>'></asp:TextBox></div>
                <div style=" float:left ;  margin:18px 0px 0px 5px" ><asp:Button ID="btUpdatePeriod"  runat="server" Text="Save"  SkinID="Green" Width="60px" CommandName="periodupdate" CommandArgument='<%# String.Format("{0}&{1}", Eval("Commission_id"),Container.DataItemIndex)%>'/>
                </div>
                <div style=" clear: both"  ></div>    
                </div>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    
</div>

<div class="line" style="width:500px" ></div>
<h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Period Commission Insert Box</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
<div style="padding-left:30px">

                <div style=" float:left"><DateTime:DatePicker_Add_Edit ID="DateTimePicker" runat="server" /></div>
                
                <div style=" clear: both"  ></div> 
                <br />
                <div >commssion : <asp:TextBox ID="txtCom" runat="server"></asp:TextBox></div>    
                <br />
                <div  ><asp:Button ID="btPeriodSubmit" runat="server" Text="Save"  Width="60px" OnClick="btPeriodSubmit_OnClick" SkinID="Blue"/></div>
                  
</div>

<div class="line" style="width:500px" ></div>
        
     </div>
</asp:Content>

