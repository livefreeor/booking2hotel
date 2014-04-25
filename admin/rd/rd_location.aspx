<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="rd_location.aspx.cs" Inherits="Hotels2thailand.UI.admin_rd_location" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<%--<script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>--%>
<script language="javascript" type="text/javascript">

    $(document).ready(function () {

        getLocation(30);

    });



    function getLocation(key) {
        
        $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#location_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $("#hd_default_des").val(key);

        $.post("../ajax/ajax_rd_location_list.aspx?des=" + key, function (data) {

            $("#location_list").html(data);

        });

        $(".link_des").css("background-color", "#ffffff");
        $("#" + key).css("background-color", "#ccccc1");

    }
    function Saveedit(key) {
        $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#location_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_rd_location_save_edit.aspx?loc=" + key, post, function (data) {

            if (data == "True") {
                var DesId = $("#hd_default_des").val();
                getLocation(DesId);
            }

        });
    }

    function SaveLocation() {
        $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#location_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_rd_location_add.aspx", post, function (data) {
           

            if (data == "True") {
                var DesId = $("#hd_default_des").val();
                getLocation(DesId);
            }


        });
    }
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="leftPanVer2" id="destination"  style="width:150px;">
    <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" />Destination Select</h4>
   <asp:GridView ID="GVDes" SkinID="Nostyle" ShowFooter="false" ShowHeader="false" AutoGenerateColumns="false" runat="server" DataKeyNames="Key" EnableModelValidation="false">
   <Columns>
    <asp:TemplateField>
        <ItemTemplate>
         <a href="" class="link_des" id="<%# Eval("Key") %>" onclick="getLocation('<%# Eval("Key") %>');return false;" ><%# Eval("Value") %></a>
          
        </ItemTemplate>
    </asp:TemplateField>
   </Columns>
   </asp:GridView>
</div>
<input type="hidden" id="hd_default_des" name="hd_default_des" />
<div id="location" class="rightPanVer2">
    <div id="location_add">
    <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Location Add Box</h4>
     <table>
        <tr>
            <td>Title</td>
            <td><table>
                <tr>
                <td>EN</td>
                <td><input type="text" name="txt_title" /></td>
                </tr>
                <tr>
                <td>TH</td>
                <td><input type="text" name="txt_title_th" /></td>
                </tr>
            </table></td>
            <td>File Name</td>
            <td>
            <table>
                <tr>
                <td>EN</td>
                <td><input type="text" name="txt_filename" /></td>
                </tr>
                <tr>
                <td>TH</td>
                <td><input type="text" name="txt_filename_th" /></td>
                </tr>
            </table>
            </td>
            <td>Folder name</td>
            <td><input type="text" name="txt_folder" /></td>
            
            <td></td>
            <td><input type="button"  value="Add" id="btn_save_loc" onclick="SaveLocation();" /></td>
        </tr>
     </table>
    </div>
    <div id="location_list">
    
    </div>
    

</div>
        
</asp:Content>
