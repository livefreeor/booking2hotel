<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ControlPanel.master" AutoEventWireup="true" CodeFile="staffpageauthorize_action.aspx.cs" Inherits="Hotels2thailand.UI.admin_staffpageauthorize_action" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
       

        GetPageDataBind();


        $("#drop_staff_cat").change(function () {

            GetPageDataBind();
        });


        $("#checkAll").click(function () {
            $("#page_result :checkbox").attr("checked", "checked");
        });
        $("#uncheckAll").click(function () {
            $("#page_result :checkbox").removeAttr("checked");
        });

        $("#reset").click(function () {
            

            GetPageDataBind();
        });

        

    });    

    function GetPageDataBind() {
        $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#page_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_staff_authorize_page_authorize.aspx", post, function (data) {

            $("#page_result").html(data);

            $("a[id^='a_checkAll_']").click(function () {
                $(this).parent().parent().find(":checkbox").attr("checked", "checked");

                return false;
            });

            $("a[id^='a_clearall_']").click(function () {
                $(this).parent().parent().find(":checkbox").removeAttr("checked");
                return false;
            });
        });

    }

    function SavePageAuthorize() {
        $("<p class=\"progress_block\"><img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" /></p>").insertBefore("#page_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

        $.post("../ajax/ajax_staff_authorize_page_authorize_save.aspx", post, function (data) {
            if (data == "true") {
           
                
                GetPageDataBind();
            }
            else {
                alert(data);
            }
           
        });
    }

    
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div>
 <asp:DropDownList ID="drop_staff_cat" SkinID="DropCustomstyle" runat="server" ClientIDMode="Static"></asp:DropDownList>
 <input type="button" id="checkAll" value="Check All"/>
  <input type="button" id="uncheckAll" value="UnCheck All"/>
   <input type="button" id="reset" value="REset"/>
 </div>

 <div id="page_result"></div>


</asp:Content>

