<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="staffpageauthorize.aspx.cs" Inherits="Hotels2thailand.UI.admin_staffpageauthorize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        

        GetPageDataBind();


        $("#dropModule").change(function () {
            
            GetPageDataBind();
        });

        $("#btnREfresh").click(function () {
            
            $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#page_result").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_staff_authorize_page_refresh.aspx", post, function (data) {
                GetPageDataBind();

            });
        });
    });

    function GetPageDataBind() {
        $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#page_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_staff_authorize_page.aspx", post, function (data) {
            //            alert(data);
            $("#page_result").html(data);
        });

    }

 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div class="">
    <asp:DropDownList ID="dropMainModule" SkinID="DropCustomstyle" ClientIDMode="Static" AutoPostBack="true"  OnSelectedIndexChanged="dropMainModule_OnSelectedIndexChanged" runat="server"></asp:DropDownList>

    <asp:DropDownList ID="dropModule" SkinID="DropCustomstyle" ClientIDMode="Static" runat="server"></asp:DropDownList>

    <input type="button" value="Refresh" id="btnREfresh" class="btStyleGreen" />

    <a href="staffpageauthorize_action.aspx">Authorize Staff</a>
 </div>
 <div id="page_result">
    
 </div>
   
</asp:Content>

