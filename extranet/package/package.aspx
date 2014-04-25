<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="package.aspx.cs" Inherits="Hotels2thailand.UI.extranet_package_package" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
<script type="text/javascript" src="../../Scripts/extranet/package.js?ver=003"></script>
<link type="text/css" href="../../css/extranet/package.css" rel="stylesheet" />
<script type="text/javascript" language="javascript">

    $(document).ready(function () {


        GetPromotionList(1);

        $("#extranet_tab li").click(function () {
            $("#extranet_tab li").removeClass("active");
            $(this).addClass("active");

            GetPromotionList($(this).attr("id"));
        });


    });

    function managePackage(packageId) {
       
        var qProductId = GetValueQueryString("pid");
        var qSupplierId = GetValueQueryString("supid");

        if (qProductId != "" && qSupplierId != "") {
            window.location.href = "package_manage_edit.aspx?oid=" + packageId  + "&pid=" + qProductId + "&supid=" + qSupplierId;
        } else {
        window.location.href = "package_manage_edit.aspx?oid=" + packageId;
        }

    }

    function delCondition(conditionId) {


        if ($("#img_progress").length) {
            $("#img_progress").remove();
        }


        $("<img id=\"img_progress\" class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#package_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_condition_del.aspx?conid=" + conditionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            alert(data);
            if (data == "True") {
                GetPromotionList(1);
            }

            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
    }

    function UpdateStatusPackage(optionId) {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#package_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_package_delete.aspx?oid=" + optionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
           
            if (data == "True") {
                $("#row_promotion_" + optionId).remove();
                DarkmanPopUpAlertFn(450, "this Package has been moved to bin successfully.", "GotoPromotionList(1);");

            }
            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });

    }

    function UpdateStatusPackageAndCheck(optionId) {

        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#package_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        //        $.get("../ajax/ajax_promotion_check_acitivate.aspx?pro=" + proid + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

        //            if (data == "True") {
        $.get("../ajax/ajax_package_delete.aspx?oid=" + optionId + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            if (data == "True") {
                $("#row_promotion_" + optionId).remove();
                DarkmanPopUpAlertFn(450, "Promotion has been activated successfully.", "GotoPromotionList(0);");

            }
            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });
        //            } else {
        //                DarkmanPopUpAlert(450, "<strong>You can not activate this promotion. Please recheck the condition.</strong><br/><br/><label style=\"font-size:11px;\">Minimum night and period of stay has been added in this condition. Please recheck.</label>");
        //            }
        // });



    }

    function GetPromotionList(status) {
       
        $("<p class=\"progress_block_main\"><img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" /></p>").insertBefore("#package_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        var StringUrl = "";

        var qProexpired = GetValueQueryString("exp");

        if (qProexpired == "") {
            StringUrl = "../ajax/ajax_package_list.aspx?status=" + status + GetQuerystringProductAndSupplierForBluehouseManage("append");
        } else {
            StringUrl = "../ajax/ajax_package_list.aspx?exp=" + qProexpired + GetQuerystringProductAndSupplierForBluehouseManage("append");
            $("#extranet_tab").hide();
        }

        
        $("#package_result").fadeOut("fast");
        $.get(StringUrl, function (data) {
            $("#package_result").html(data);

            $("#package_result").fadeIn("fast");
            tooltip();


        });
    }
</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<ul id="extranet_tab"><li class="active" id="1">Active</li><li id="0">Inactive</li></ul>

<div id="package_result">

</div>

</asp:Content>

