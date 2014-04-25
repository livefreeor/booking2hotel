<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="promotion.aspx.cs" Inherits="Hotels2thailand.UI.extranet_promotion" %>
<%@ Register  Src="~/Control/DatepickerCalendar_Extra.ascx" TagName="DatePicker_Add_Edit" TagPrefix="DateTime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
<link type="text/css" href="../../css/extranet/promotion_extra.css" rel="stylesheet" />
<link type="text/css" href="../../css/extranet/extrabeds.css" rel="stylesheet" />

<script type="text/javascript" language="javascript">
    $(document).ready(function () {


        GetPromotionList(1);

        $("#extranet_tab li").click(function () {
            $("#extranet_tab li").removeClass("active");
            $(this).addClass("active");

            GetPromotionList($(this).attr("id"));
        });


    });

    function managePromotion(promotionId, progroupId) {
        var qProductId = GetValueQueryString("pid");
        var qSupplierId = GetValueQueryString("supid");

        if (qProductId != "" && qSupplierId != "") {
            window.location.href = "promotion_manage.aspx?pg=" + progroupId + "&pro=" + promotionId + "&pid=" + qProductId + "&supid=" + qSupplierId;
        } else {
            window.location.href = "promotion_manage.aspx?pg=" + progroupId + "&pro=" + promotionId;
        }

    }

    

    function GetPromotionList(status) {

        $("<p class=\"progress_block_main\"><img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" /></p>").insertBefore("#promotion_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
        var StringUrl = "";

        var qProexpired = GetValueQueryString("exp");

        if (qProexpired == "") {
           StringUrl =  "../ajax/ajax_promotion_list.aspx?status=" + status + GetQuerystringProductAndSupplierForBluehouseManage("append");
       } else {
           StringUrl = "../ajax/ajax_promotion_list.aspx?exp=" + qProexpired + GetQuerystringProductAndSupplierForBluehouseManage("append");
           $("#extranet_tab").hide();
        }
        

        $("#promotion_result").fadeOut("fast");
        $.get(StringUrl, function (data) {
            $("#promotion_result").html(data);

            $("#promotion_result").fadeIn("fast");
            tooltip();


            $("input[name^='pro_code_hotel_']").focus(function () {
                var regExpr = new RegExp("^[a-zA-Z_0-9][a-zA-Z_0-9]*$");
                var proid = $(this).attr("id");
                var code = $("#" + proid).val();


                if (regExpr.test(code)) {
                    if (code != "") {
                        $(this).removeAttr("class");
                        $(this).addClass("Extra_textbox_hover");
                    }
                } else {
                    if (code != "") {
                        $(this).removeAttr("class");
                        $(this).addClass("Extra_textbox_alert");
                    }
                }

            });


            $("input[name^='pro_code_hotel_']").keyup(function () {
                var regExpr = new RegExp("^[a-zA-Z_0-9][a-zA-Z_0-9]*$");
                var proid = $(this).attr("id");
                var code = $("#" + proid).val();
                if (!regExpr.test(code)) {
                    if (code != "") {
                        DarkmanPopUpAlert(450, "please Insert Englist Only");
                        $(this).removeAttr("class");
                        $(this).addClass("Extra_textbox_alert");
                    }
                } else {
                    if (code != "") {
                        $(this).removeAttr("class");
                        $(this).addClass("Extra_textbox_hover");
                    }
                }
            });

            $("input[name^='pro_code_hotel_']").blur(function () {
                $(this).removeAttr("class");
                $(this).addClass("Extra_textbox");
                var regExpr = new RegExp("^[a-zA-Z_0-9][a-zA-Z_0-9]*$");
                var proid = $(this).attr("id");
                var code = $("#" + proid).val();

                
                if (regExpr.test(code)) {
                    
                    if (code != "") {
                        $.get("../ajax/ajax_promotion_procode.aspx?pro=" + proid + "&code=" + code + GetQuerystringProductAndSupplierForBluehouseManage("append"),
                        function (data) {

                        });
                    }
                } else {
                    if (code != "") {
                        $(this).removeAttr("class");
                        $(this).addClass("Extra_textbox_alert");
                        DarkmanPopUpAlert(450, "please Insert Englist Only");
                    }
                }


            });
        });
    }

    function UpdateStatusPrmotionAndCheck(proid) {
       
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#promotion_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

//        $.get("../ajax/ajax_promotion_check_acitivate.aspx?pro=" + proid + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            
//            if (data == "True") {
                $.get("../ajax/ajax_promotion_delete.aspx?pro=" + proid + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                    if (data == "True") {
                        $("#row_promotion_" + proid).remove();
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

    function UpdateStatusPrmotion(proid) {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#promotion_result").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_promotion_delete.aspx?pro=" + proid + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
           
            if (data == "True") {
                $("#row_promotion_" + proid).remove();
                DarkmanPopUpAlertFn(450, "Promotion has been moved to bin successfully.", "GotoPromotionList(1);");

            }
            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }
        });

    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<ul id="extranet_tab"><li class="active" id="1">Active</li><li id="0">Inactive</li></ul>

<div id="promotion_result">
<asp:Literal ID="ltrproLsit" runat="server"></asp:Literal>
</div>

</asp:Content>

