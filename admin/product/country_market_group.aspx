<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="country_market_group.aspx.cs" Inherits="Hotels2thailand.UI.country_market_group" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
   <style type="text/css">
      
        
        .list_market tr td
        {
            width:180px;
            margin:5px 0px 0px 0px;
            padding:2px;
            
        }
    
  </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">

    function fnCheckUnCheck() {
        var grd = document.getElementById("<%= GVContinent.ClientID %>");

        //Collect A
        var rdoArray = grd.getElementsByTagName("input");
        var count = 0;
        for (i = 0; i <= rdoArray.length - 1; i++) {

            if (rdoArray[i].type == 'checkbox') {

                if (rdoArray[i].checked == true) {
                    rdoArray[i].checked = false;
                    rdoArray[i].parentNode.style.backgroundColor = '#ffffff';
                }
                else {
                    rdoArray[i].checked = true;
                    rdoArray[i].parentNode.style.backgroundColor = '#daf3d5';
                }
             }
         }

    }

    function fnCheckUnCheckDefault() {
        var grd = document.getElementById("<%= GVContinent.ClientID %>");

        //Collect A
        var rdoArray = grd.getElementsByTagName("input");
        var count = 0;
        for (i = 0; i <= rdoArray.length - 1; i++) {

            if (rdoArray[i].type == 'checkbox') {

                if (rdoArray[i].checked == true) {
                    rdoArray[i].parentNode.style.backgroundColor = '#daf3d5';
                }
                else {
                    rdoArray[i].parentNode.style.backgroundColor = '#ffffff';

                }
            }
         }

    }

    function fnCheckUnCheckcolor() {
        var grd = document.getElementById("<%= GVContinent.ClientID %>");

        //Collect A
        var rdoArray = grd.getElementsByTagName("input");
        var count = 0;
        for (i = 0; i <= rdoArray.length - 1; i++) {

            if (rdoArray[i].type == 'checkbox') {

                if (rdoArray[i].checked == true) {
                    rdoArray[i].parentNode.style.backgroundColor = '#daf3d5';
                }
                else {
                    rdoArray[i].parentNode.style.backgroundColor = '#ffffff';
                }

            }


        }

    }
        
    </script>
    <asp:Panel ID="panelInsertGroup" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Market Group insert</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <asp:TextBox ID="txtTitle" runat="server" Width="500px" ></asp:TextBox>
        <asp:Button ID="btnInsert" runat="server" Text="Add" SkinID="Green" OnClick="btnInsert_Onclick" />

    </asp:Panel>
    <asp:Panel ID="panelInsertGroupSelection" runat="server" CssClass="productPanel">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Market Group Selection</h4>
        <p class="contentheadedetail">Manage Supplement, you can select date supplement from </p><br />
        <p><asp:DropDownList ID="dropGroup" runat="server" Width="500px" OnSelectedIndexChanged="dropGroup_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> <a href="javaScript:showDiv('titleEdit')">Edit Title</a></p>
        <div id="titleEdit" style=" display:none">
            <asp:TextBox ID="txttitleEdit" runat="server" Width="300px" ></asp:TextBox>
            <asp:Button ID="btnTitleEdit" runat="server" Text="Update" SkinID="Green_small" OnClick="btnEdit_Onclick" />
        </div>
        <p><a href="javaScript:fnCheckUnCheck()">Check ALL</a></p>
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

    <br />
    <asp:Button ID="btnSaveSelection" runat="server" Text="Save" SkinID="Green" OnClick="btnSaveSelection_OnClick" />
    </asp:Panel>
    
    
    
    
    
    
</asp:Content>
