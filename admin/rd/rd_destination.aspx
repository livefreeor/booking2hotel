<%@ Page Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="rd_destination.aspx.cs" Inherits="Hotels2thailand.UI.admin_rd_destination" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<%--<script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>--%>
<script language="javascript" type="text/javascript">

    $(document).ready(function () {

        getDestinationList();

    });



    function getDestinationList() {
        
        $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#location_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        

        $.post("../ajax/ajax_rd_destination_list.aspx", function (data) {

            $("#location_list").html(data);

        });

//        $(".link_des").css("background-color", "#ffffff");
//        $("#" + key).css("background-color", "#ccccc1");

    }
    function Saveedit(key) {
        $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#location_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_rd_destination_save_edit.aspx?des=" + key, post, function (data) {
          
            if (data == "True") {

                getDestinationList(key);
            }

        });
    }

    function SaveDestination() {
        $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#location_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_rd_destination_add.aspx", post, function (data) {
           
            if (data == "True") {

                getDestinationList();
            }


        });
    }
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="location">
    <div id="location_add">
    <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Destination Add Box</h4>
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
            <td><input type="button"  value="Add" id="btn_save_loc" onclick="SaveDestination();" /></td>
        </tr>
        <tr><td colspan="7">
         <textarea name="txt_descrip_en" rows="5"  cols="20" style="width:100%"></textarea>
        </td></tr>
        <tr><td colspan="7">
         <textarea name="txt_descrip_th" rows="5"  cols="20" style="width:100%"></textarea>
        </td></tr>
     </table>
    </div>
    <div id="location_list">
    
    </div>
    

</div>
        
</asp:Content>
