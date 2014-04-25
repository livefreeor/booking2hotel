<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="rate_plan.aspx.cs" Inherits="Hotels2thailand.UI.extranet_rate_plan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>

<style type="text/css">
    #condition_box_main
    {
       margin:0px;
       padding:0px;
    }
    #condition_box
    {
        margin:15px 0px 0px 0px;
        padding:10px;
        
        background-color:#ebf0f3;
        border:1px solid #cccccc;
    }
     #condition_box_result
    {
        margin:0px 0px 0px 0px;
        padding:0px;
        
    }
    .room_title
    {
        margin:0px;
        padding:0px;
        color:#608000;
        font-size:14px;
    }
</style>
<script type="text/javascript"  language="javascript">

    $(document).ready(function () {

        GetConditionList();

        GetRatePlanList();


    });

    function GetConditionList() {
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader_blue_gray.gif\" alt=\"Progress\" /></td>").insertBefore("#condition_box_main").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_rate_plan_condition_select.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            $("#condition_box_main").html(data);
            $("input[name^='rate_plane_value_']").css("background-color", "#f0f0f0");
        });
    }

    function GetRatePlanList() {
        
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader_blue_gray.gif\" alt=\"Progress\" /></td>").insertBefore("#rate_plan_list").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_rate_plan_condition_list.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), function (data) {
            $("#rate_plan_list").html(data);
            tooltip();
        });
    }
    function inputrate(obj) {

        var objId = obj.id;
        var CheckBox = $("#" + obj.id);
        var DropCat = CheckBox.parent().next().find("select");
        var TxtVal = CheckBox.parent().next().next().find(":text");
        if (CheckBox.attr("checked") == "checked") {
            DropCat.removeAttr("disabled");
            TxtVal.removeAttr("disabled");
            TxtVal.css("background-color", "#faffbd");
            //#bdbdbd;
        } else {
            DropCat.val("1");
            TxtVal.val("");

            DropCat.attr("disabled", "disabled");
            TxtVal.attr("disabled", "disabled");
            TxtVal.css("background-color", "#f0f0f0");
        }
    }



    function rateInsertCheck() {
        var result = false;
        id = "condition_box";
        var Y_top = $("#" + id).offset().top;
        var X_left = $("#" + id).offset().left + 100;

        optionheight = $("#" + id).height();
        txtAlert = "*Select condition before adding rate."
        
        var countondition = 0;
        $("input[name='checkbox_condition_check']").each(function () {
            if ($(this).is(":checked")) {
                countondition = countondition + 1;
            }
        });

        if (countondition == 0) {
            $("#condition_box").css("background-color", "#ffebe8");

            $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": X_left + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }
        else {

            $("#condition_box").css("background-color", "#ebf0f3");
             $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
            result = true;
        }

        $("input[name='checkbox_condition_check']").each(function () {

            $(this).click(function () {
                countondition = 0;
                $("input[name='checkbox_condition_check']").each(function () {
                    if ($(this).is(":checked")) {
                        countondition = countondition + 1;
                    }
                });

                if (countondition > 0) {
                    $("#condition_box").css("background-color", "#ebf0f3");

                    $("#valid_alert_" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });

                }

                if (countondition == 0 && RatePlanInsertValueCheck() == true) {
                    $("#condition_box").css("background-color", "#ebf0f3");

                    $("#valid_alert_" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }


            });

        });

        return result;

    }

    function CountrySelectedCheck() {
        var result = false;

        id = "condition_box";
        var Y_top = $("#" + id).offset().top;
        var X_left = $("#" + id).offset().left + 100;
        txtAlert = "*Please select at least one country."
        optionheight = $("#" + id).height();
        var CountryList = "";
        var CountCountryList = $("#country_selected option").length;

        if (CountCountryList > 0) {
            $("#country_selected option").filter(function (index) {
                if (index != CountCountryList - 1) {
                    CountryList = CountryList + $(this).val() + ",";
                } else {
                    CountryList = CountryList + $(this).val();
                }

            });
            result = true;
            $("#condition_box").css("background-color", "#ebf0f3");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });

        } else {

            $("#condition_box").css("background-color", "#ffebe8");

            $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": X_left + "px" });
            $("#valid_alert_" + id).fadeIn('fast');

            result = false;
        }

        $("#CountrySelected").val(CountryList);

        return result;
    }

    function RatePlanInsertValueCheck() {
        var result = false
        var regExpr = new RegExp("^[0-9][0-9]*$");
        var CountREsult = 0;

        id = "condition_box";
        var Y_top = $("#" + id).offset().top;
        var X_left = $("#" + id).offset().left + 100;
        
        optionheight = $("#" + id).height();

        $("input[name='checkbox_condition_check']").each(function () {

            if ($(this).is(":checked")) {
                var CheckBox = $("#" + $(this).attr("id"));

                drop = CheckBox.parent().next().find("select").val();

                txtVal = CheckBox.parent().next().next().find(":text").val();

                if (CheckBox.attr("checked") == "checked") {

                    if (!regExpr.test(txtVal)) {
                        CountREsult = CountREsult + 1;
                        txtAlert = "*Requires numeric information only."
                    }

                    if (drop == "1" || drop == "3") {
                        if (parseInt(txtVal) == 0) {
                            CountREsult = CountREsult + 1;
                            txtAlert = "*0 Baht discount can not be added. Please change."
                        }
                    }

                    if (drop == "2" || drop == "4") {
                        if (parseInt(txtVal) == 100 || parseInt(txtVal) == 0) {
                            CountREsult = CountREsult + 1;
                            txtAlert = "*0% and 100% can not be added. Please change."
                        }

                    }

                } 

            }

        });


        if (CountREsult == 0) {

            result = true;

            $("#condition_box").css("background-color", "#ebf0f3");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });

        } else {
            $("#condition_box").css("background-color", "#ffebe8");

            $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": X_left + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }


        return result;
    }

    function SaveRatePlan() {
        
        if (CountrySelectedCheck() == true && rateInsertCheck() == true && RatePlanInsertValueCheck() == true) {
            $("<img class=\"img_progress\" src=\"../../images_extra/preloader_blue_gray.gif\" alt=\"Progress\" />").insertBefore("#condition_box_main").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_rate_plan_condition_save.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {



                if (data == "True") {
                    DarkmanPopUpAlert(450, "Your data is added to save.");
                    GetRatePlanList();
                    GetConditionList();

                    $("#country_selected option").each(function () {
                        $(this).remove();
                    });
                }
                if (data == "method_invalid") {
                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }

            });

        }
        else {
            
        }
    }

    
    function deleterateplan(id, country_id, country) {

//        $("<img class=\"img_progress\" src=\"../../images_extra/preloader_blue_gray.gif\" alt=\"Progress\" />").insertBefore("#condition_box_main").ajaxStart(function () {
//            $(this).show();
//        }).ajaxStop(function () {
//            $(this).remove();
//        });

        $("#country_count_" + country_id).remove();

        $.post("../ajax/ajax_rate_plan_condition_delete.aspx?plid=" + id + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            
            if (data == "True") {
                //DarkmanPopUpAlert(450, "Your data is removed");
                GetRatePlanList();

                $("#listCountry option").filter(function (index) { return $(this).val() == country_id }).remove;

                var option = "<option value=\"" + country_id + "\">" + country + "</option>";

                $("#listCountry").append(option);
                $("#listCountry option").each(function () { $(this).removeAttr("selected"); });
                $("#listCountry option").sortElements(function (a, b) {
                    return $(a).text() > $(b).text() ? 1 : -1;
                });
            }
            if (data == "method_invalid") {
                DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
            }

        });

    }

   

    

    function selectoneClick() {
        //
        var OptionSelected = $("#listCountry option").filter(function (index) { return $(this).attr("selected") == "selected"; });
        $("#country_selected").append(OptionSelected);
        $("#country_selected option").each(function () { $(this).removeAttr("selected"); });

        $("#country_selected option").sortElements(function (a, b) {
            return $(a).text() > $(b).text() ? 1 : -1;
        });

        var OptionSelected = $("#listCountry option");
        if (OptionSelected.length > 0) {
            $("#condition_box").css("background-color", "#ebf0f3");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
        
    }

    function selectallClick() {

        var OptionSelected = $("#listCountry option");
        if (OptionSelected.length > 0) {
            $("#listCountry option").each(function () {
                $("#country_selected").append($(this));
            });
            $("#country_selected option").sortElements(function (a, b) {
                return $(a).text() > $(b).text() ? 1 : -1;
            });

            $("#condition_box").css("background-color", "#ebf0f3");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
    }

    function removeoneClick() {
        //alert("KK");
        var OptionSelected = $("#country_selected option").filter(function (index) { return $(this).attr("selected") == "selected"; });
        $("#listCountry").append(OptionSelected);
        $("#listCountry option").each(function () { $(this).removeAttr("selected"); });
        $("#listCountry option").sortElements(function (a, b) {
            return $(a).text() > $(b).text() ? 1 : -1;
        });
    }
    function removeallClick() {
        //alert("HELLO");

        var OptionSelected = $("#country_selected option");
        if (OptionSelected.length > 0) {
            $("#country_selected option").each(function () {
                $("#listCountry").append($(this));
            });

            $("#listCountry option").sortElements(function (a, b) {
                return $(a).text() > $(b).text() ? 1 : -1;
            });
        }
    }

    

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="rate_control_insert"  class="blogInsert">
<h4><asp:Image ID="imgContent" runat="server" ImageUrl="~/images/content.png" /> Rate Plan Insert Box</h4>
    <input type="hidden" id="CountrySelected" name="CountrySelected" />
        <div id="list_box">
        <table  cellpadding="0" cellspacing="0">
        <tr><td valign="bottom"><label>Store Front</label></td><td></td><td valign="bottom"><label>Selected</label></td></tr>
        <tr>
        <td>
            <asp:ListBox ID="listCountry"  ClientIDMode="Static" runat="server" CssClass="Extra_drop_list">
            
            </asp:ListBox>
           
        </td>
        <td>
            <table width="60px">
   
            <tr><td align="center"><input type="button" value=">>" id="selectall"  title="select all" style=" width:40px;" name="selectall" onclick="selectallClick();return false;" /></td></tr>
            <tr><td align="center"><input type="button" value=">" id="selectone" title="select" style=" width:40px;" name="selectone" onclick="selectoneClick();return false;" /></td></tr>
            <tr><td align="center"><input type="button" value="<" id="removeone" title="remove" style=" width:40px;" name="removeone" onclick="removeoneClick();return false;" /></td></tr>
            <tr><td align="center"><input type="button" value="<<" id="removeall" title="remove all" style=" width:40px;" name="" onclick="removeallClick();return false;" /></td></tr>
            </table>
    
        </td>

        <td>

            <select  size="8" id="country_selected" class="Extra_drop_list" >
            
            </select> 
        </td>
        </tr>
        </table>
        </div>

        <div id="condition_box_main" >
            
        </div>


</div>

<div id="rate_plan_list_main" class="blogInsert">
<h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Rate Plan List</h4>
<div id="rate_plan_list"></div>
</div>

</asp:Content>

