<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="package_manage.aspx.cs" Inherits="Hotels2thailand.UI.extranet_package_package_manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
<script type="text/javascript" src="../../Scripts/extranet/package.js?ver=002"></script>
<script type="text/javascript" src="../../js/tinymce/jquery.tinymce.js"></script>
<link type="text/css" href="../../css/extranet/package.css" rel="stylesheet" />
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        RenderRichBox();
        var dropPack = $("#dropPackage").children("option").length;
        

        $("#Booking_DateStart").val("");
        BindDatePicker();


        DatepickerDual("rate_DateStart", "rate_DateEnd");

        var conval = $("#conditionTitle").val();
        if (conval == "1" || conval == "5") {
            $("#package_cancellation").slideUp('fast');
            $("#txt_day_charge_1").val("1");
        } else {
            $("#package_cancellation").slideDown('fast');
            $("#txt_day_charge_1").val("0");
        }

        $("#conditionTitle").change(function () {
            
            var iConnameId = $(this).val();
            if (iConnameId == "1" || iConnameId == "5") {
                $("#package_cancellation").slideUp('fast');
                $("#txt_day_charge_1").val("1");
            } else {
                $("#package_cancellation").slideDown('fast');
                $("#txt_day_charge_1").val("0");
            }



        });

    });

    function creat_new() {
        ClearValid();

        $("#hd_isCurrent").val("false");

        $("#dropPackage").unbind("change");
        $("#drop_adult_child").unbind("change");
        $("#conditionTitle").unbind("change");
        $("input[name='chk_adult_child']").unbind("change");

        $("#dropNight").val(1);

        $("#package_select").slideUp('600', function () {
            $("#package_insert").slideDown('600');
        });


      RollbackPackageDetail();
    }

    function creat_current() {
        ClearValid();

        var optionid = $("#dropPackage").val();
        var ConnameId = $("#conditionTitle").val();
        var NumGuest = $("#drop_adult_child").val();
        var Isadult = $("input[name='chk_adult_child']").val();
        GetPackageDetail(optionid, ConnameId, NumGuest, Isadult);

        $("#hd_isCurrent").val("true");


        

        $("#dropPackage").change(function () {

            var iOption = $(this).val();
            var iConnameId = $("#conditionTitle").val();
            var iNumGuest = $("#drop_adult_child").val();
            var iIsadult = $("input[name='chk_adult_child']").val();
         
            GetPackageDetail(iOption, iConnameId, iNumGuest, iIsadult);

            //ConditionDuplicate_check(iOption, iConnameId, iNumGuest, iIsadult);



        });

        $("#drop_adult_child").change(function () {
            var iOption = $("#dropPackage").val();
            var iConnameId = $("#conditionTitle").val();
            var iNumGuest = $(this).val();
            var iIsadult = $("input[name='chk_adult_child']").val();
            //alert(Isadult);
            //ConditionDuplicate_check(iOption, iConnameId, iNumGuest, iIsadult);
        });

        $("#conditionTitle").unbind("change");

        $("#conditionTitle").change(function () {
            var iOption = $("#dropPackage").val();
            var iConnameId = $(this).val();
            var iNumGuest = $("#drop_adult_child").val();
            var iIsadult = $("input[name='chk_adult_child']").val();
            //alert(Isadult);
            //ConditionDuplicate_check(iOption, iConnameId, iNumGuest, iIsadult);
         

            if (iConnameId == "1" || iConnameId == "5") {
                $("#package_cancellation").slideUp('fast');
                $("#txt_day_charge_1").val("1");
            } else {
                $("#package_cancellation").slideDown('fast');
                $("#txt_day_charge_1").val("0");
            }

            
        });

        $("input[name='chk_adult_child']").click(function () {

            var iOption = $("#dropPackage").val();
            var iConnameId = $("#conditionTitle").val();
            var iNumGuest = $("#drop_adult_child").val();
            var iIsadult = $(this).val();

           
            //alert(Isadult);
            //ConditionDuplicate_check(iOption, iConnameId, iNumGuest, iIsadult);
        });



        $("#package_insert").slideUp('600', function () {
            $("#package_select").slideDown('600');
        });

      
    }


    function BindDatePicker() {
        //$("#Booking_DateStart").val("");
        DatepickerDual("Booking_DateStart", "Booking_DateEnd");
        DatepickerDual("Stay_DateStart", "Stay_DateEnd");


    }


    function GetPackageDetail(iOption, iConnameId, iNumGuest, iIsadult) {
        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#package_night").ajaxStart(function () {

            $(this).show();

        }).ajaxStop(function () {
            $(this).remove();
        });

        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#package_detail").ajaxStart(function () {

            $(this).show();

        }).ajaxStop(function () {
            $(this).remove();
        });
        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#package_booking_period").ajaxStart(function () {

            $(this).show();

        }).ajaxStop(function () {
            $(this).remove();
        });

        $("<img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#package_stay_period").ajaxStart(function () {

            $(this).show();

        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_package_detail.aspx?oid=" + iOption + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            if (data != "method_invalid") {

                var arrVal = data.split(';');

                //$("#txtPackage").val(arrVal[0]);
                $("#dropNight").val(arrVal[1]);

                $("#Booking_DateStart").val(arrVal[2]);
                $("#Booking_DateEnd").val(arrVal[3]);
                $("#Stay_DateStart").val(arrVal[4]);
                $("#Stay_DateEnd").val(arrVal[5]);
                $("#package_detail_list").html(arrVal[6]);

                BindDatePicker();


                $.get("../ajax/ajax_package_detail_content.aspx?oid=" + iOption + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                    if (data != "method_invalid") {


                        $("#package_detail_list").html(data);
                        DisablePackageDetail();
                        //


                    } else {

                    }
                });

            } else {

            }
        });
    }
    function ConditionDuplicate_check(optionId,connameId,numGuest,isadult) {

        var result = 0;

        
        $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").appendTo("#package_condition").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });

        $.get("../ajax/ajax_condition_name_check_package.aspx?connid=" + connameId + "&oid=" + optionId + "&gus=" + numGuest + "&iadu=" + isadult + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
          
            if (data != "0") {
                result = data;
                ValidateAlert("package_condition", "*This condition and No.of Guest has been used. Please use the different condition.", "");
                //$("#hd_duplicate").val("no");
            }
            else {
                ValidateAlertClose("package_condition");
                //$("#hd_duplicate").val("yes");
            }

        });

        return result;
    }

    function redirect() {
        if (GetValueQueryString("pid") != "" && GetValueQueryString("supid") != "") {
            window.location.href = "package.aspx?pid=" + GetValueQueryString("pid") + "&supid=" + GetValueQueryString("supid");
        }
        else {
            window.location.href = "package.aspx";
        }

        
    }

    function ClearValid() {
        $("label[id^='valid_alert_']").remove();
    }

    function ChkDatePackageValid() {
        var result = 0;
        var ret = "no";
        var id = "package_Save";

        var text = "*Please recheck rate validity. Your rate must be added further than package validity.";
        var text2 = "*You need to put a price before.";
        var Y_top = $("#" + id).offset().top + 23;
        var X_left = $("#" + id).offset().left;
      
       
        var optionwidth = 0;
        var optionheight = 0;
        optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
        optionheight = $("#" + id).outerHeight() - 21;

        var DateStayPeriodEnd = $("#hd_Stay_DateEnd").val();

        var periodcount = $(".rate_result_list").length;

        var dateEndLast = $(".rate_result_list").filter(function (index) {

            return index == (periodcount - 1)
        }).find("input[name^='hd_rate_date_To']").val();
        
        if (periodcount> 0) {
           
            result = daydiff(parseDate(DateStayPeriodEnd), parseDate(dateEndLast));
        }
        

        if (result < 0 || result > 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                $("#Stay_DateEnd").css("background-color", "#ffebe8");
                //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        }
        else if (periodcount == 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text2 + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        }
        else {
            ret = "yes";
            $("#" + id).css("background-color", "#f7f7f7");
           
            $("#Stay_DateEnd").css("background-color", "#ffffff");
            // $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }


        return ret;
    }
    function SaveNewPackage() {
        
        //alert(ChkDatePackageValid());
        //return false;
       document.getElementById('btnLoad_package').disabled = 'true';

        tinyMCE.triggerSave();

      
        var DateBookingValid = DateCompareValid_ver2("package_booking_period", "Booking_DateStart", "Booking_DateEnd", "");
        var DateStayValid = DateCompareValid_ver2("package_stay_period", "Stay_DateStart", "Stay_DateEnd", "");

        
        //var PriceRequireNum = ValidateOptionMethod("txtPrice", "number");


       //var PackageDetail = ValidateOptionMethod("txt_package_detail", "required");

        var daycancel = DayancelCheckLoadTariff("package_Save", "");
        var cancel = CancelCheckLoadTariff("package_Save", "");

        //alert(cancel);
        //DayancelCheckLoadTariff("load_tariff_save", "") == "0" && CancelCheckLoadTariff("load_tariff_save", "") == "0"
        if ($("#hd_isCurrent").val() == "true") {
            var iOption = $("#dropPackage").val();
            var iConnameId = $("#conditionTitle").val();
            var iNumGuest = $("#drop_adult_child").val();
            var iIsadult = $("input[name='chk_adult_child']:checked").val();
           //alert(iOption + "--" + iConnameId + "===" + iNumGuest + "---" + iIsadult);
            var conditionCheck = "0";//ConditionDuplicate_check(iOption, iConnameId, iNumGuest, iIsadult);

           
            //alert(conditionCheck);

            if (DateBookingValid >= 0 && DateStayValid >= 0 && daycancel == "0" && cancel == "0" && conditionCheck == "0" && ChkDatePackageValid() == "yes") {
                $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#package_Save").ajaxStart(function () {
                    $(this).show();
                    // $("#btnLoad_package").unbind("click");


                }).ajaxStop(function () {
                    $(this).remove();

                });
               

                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                
                $.post("../ajax/ajax_package_insert_new.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

                    
                    if (data == "True") {

                        DarkmanPopUpAlertFn(450, "This package is added completely. Thank you.", "redirect();");

                    }
                    else {
                        DarkmanPopUpAlert(450, data);
                    }
                    if (data == "method_invalid") {
                        DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                    }
                });

            }
            else {

                $("#btnLoad_package").removeAttr("disabled");
                if (!PackageRequire) {
                    $('html, body').animate({ scrollTop: $("#txtPackage").offset().top - 100 }, 500);
                }
            }
        


        } else {

            var PackageRequire = ValidateOptionMethod("txtPackage", "required");
            if (DateBookingValid >= 0 && DateStayValid >= 0 && PackageRequire && daycancel == "0" && cancel == "0" && ChkDatePackageValid() == "yes") {
                $("<td><img class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" /></td>").insertBefore("#package_Save").ajaxStart(function () {
                    $(this).show();
                    // $("#btnLoad_package").unbind("click");


                }).ajaxStop(function () {
                    $(this).remove();

                });

               

                var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
                $.post("../ajax/ajax_package_insert_new.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {
                   
                    if (data == "True") {

                        DarkmanPopUpAlertFn(450, "This package is added completely. Thank you.", "redirect();");

                    }
                    else {
                        DarkmanPopUpAlert(450, data);
                    }
                    if (data == "method_invalid") {
                        DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                    }
                });

            }
            else {

                $("#btnLoad_package").removeAttr("disabled");
                if (!PackageRequire) {
                    $('html, body').animate({ scrollTop: $("#txtPackage").offset().top - 100 }, 500);
                }
            }
        
        }
       
    }

    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="mainExtranet_form">
    <div id="package_select" class="blogInsert" style="display:none;">   
    <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Select Package  
    <span id="creat_current"><a href="" class="link_creat" onclick="creat_new();return false;" >Create New Package</a></span></h4>
    <asp:DropDownList ID="dropPackage"  EnableTheming="false" runat="server"  ClientIDMode="Static"  CssClass="Extra_Drop" style="width:500px;"></asp:DropDownList>
      
       
    </div>
    <div id="package_insert" class="blogInsert" >
    <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" />Package Title
    <span id="Span1"><a href="" class="link_creat" onclick="creat_current();return false;" >Create from Current Package</a></span></h4>
    <asp:TextBox ID="txtPackage" runat="server" Width="600px" ClientIDMode="Static"   CssClass="Extra_textbox_big" EnableTheming="false"></asp:TextBox>
        
    </div>
    <div id="package_night" class="blogInsert">
    <h4><asp:Image ID="Image9" runat="server" ImageUrl="~/images/content.png" />Night of Package</h4>
    <fieldset ><legend>Night(s)</legend>
            <asp:DropDownList ID="dropNight"   EnableTheming="false"  runat="server" BackColor="#faffbd" ClientIDMode="Static"  Width="100" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset>
            
            </div>
    <div id="package_detail" class="blogInsert">
        <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" />Package Detail</h4>
        <div id="package_detail_insert">
        <%--<input type="text" style="width:600px" id="txt_package_detail" class="Extra_textbox_big" onkeypress="return clickButton(event,'txt_pack_detail_list');" />
     
     <input type="button" id="txt_pack_detail_list" value="Add" class="Extra_Button_small_blue" onclick="appendpackage();" />--%>

     <textarea id="txt_package_detail" rows="20" cols="5" class="mceEditor" style="width:650px"  name="txt_package_detail" ></textarea>

     </div>
     <div id="package_detail_list" style="display:none">
       
     </div>

    </div>

    <div id="package_booking_period" class="blogInsert">
    <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" />Period of Booking</h4>
        <table width="95%">
            <tr>
                <td align="right"><label>Date Range From </label></td>
                <td>
                <asp:TextBox ID="Booking_DateStart" runat="server" EnableTheming="false" CssClass="Extra_textbox_big" ReadOnly="true"  Width="120px" ClientIDMode="Static"></asp:TextBox>
                </td>
                <td align="right"><label> To </label></td>
                <td>
                
                <asp:TextBox ID="Booking_DateEnd" runat="server" EnableTheming="false" CssClass="Extra_textbox_big" ReadOnly="true"  Width="120px" ClientIDMode="Static">
                </asp:TextBox>
                </td>
                
            </tr>
      
            
            
            </table>
     </div>

     <div id="package_stay_period" class="blogInsert">
     <h4><asp:Image ID="Image5" runat="server" ImageUrl="~/images/content.png" />period of stay</h4>
        <table width="95%">
            <tr>
                <td align="right"><label>Date Range From </label></td>
                <td>
                <asp:TextBox ID="Stay_DateStart" runat="server" CssClass="Extra_textbox_big" EnableTheming="false"  ReadOnly="true"  Width="120px" ClientIDMode="Static">
                </asp:TextBox>
               </td>
                <td align="right"><label> To </label></td>
                <td>
                <asp:TextBox ID="Stay_DateEnd" runat="server" EnableTheming="false" CssClass="Extra_textbox_big" ReadOnly="true"   Width="120px" ClientIDMode="Static">
                </asp:TextBox>
                </td>
                
            </tr>
      
            
            
            </table>
     </div>

      
     <div id="package_condition" class="blogInsert">
        <h4><asp:Image ID="Image7" runat="server" ImageUrl="~/images/content.png" />Condition</h4>
        <table width="100%">
        <tr>
        <td>
        <fieldset >
        <legend>Select Rate Condition</legend>
        <asp:DropDownList ID="conditionTitle" EnableTheming="false" runat="server"  ClientIDMode="Static"  CssClass="Extra_Drop" style="width:500px;">
            
        </asp:DropDownList>
        
        <%--<asp:TextBox ID="txt_condition_name" runat="server"  CssClass="Extra_textbox" EnableTheming="false" style="width:500px;"></asp:TextBox>--%>
        
        </fieldset>
        <br />
        </td>
        <td align="left" valign="middle" ><div id="progresscheck"></div></td>
        </tr>
        <tr>
        <td colspan="2">
        <div  class="condition_field_box" style="width:150px">
        <fieldset ><legend>Package for</legend>
            <table>
                <tr><td><input type="radio" name="chk_adult_child" value="0" checked="checked"  /></td><td>For Adult</td></tr>
                <tr><td><input type="radio" name="chk_adult_child" value="1" /></td><td>For Child</td></tr>
            </table>
            </fieldset></div>
            <div class="condition_field_box"><fieldset ><legend>No.of guest</legend>
            <asp:DropDownList ID="drop_adult_child"  EnableTheming="false"  runat="server" ClientIDMode="Static"  Width="50px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            <%--<div  class="condition_field_box"><fieldset ><legend>Child</legend>
            <asp:DropDownList ID="drop_child" EnableTheming="false"  runat="server" ClientIDMode="Static"  Width="50px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>--%>
            
        </td>
        
        </tr>
    </table>
     </div>

     <div id="package_cancellation" class="blogInsert">
     <h4><asp:Image ID="Image6" runat="server" ImageUrl="~/images/content.png" />Cancellation</h4>
            
            <table cellpadding="5" cellspacing="1" width="100%" bgcolor="#d8dfea" border="0" style="text-align:center; margin:0px 0px 0px 0px;">
                <tr style="background-color:#edeff4;color:#333333;font-weight:bold;height:15px;line-height:15px;font-size:11px;"><td align="center" width="30%" >No. of Day Cancel</td>
                <td align="center" width="30%">Night(s) charge</td><td align="center" width="30%" colspan="2" >Percentage Charge</td>
                <td width="10%">Remove</td></tr>
            </table>

            <div id="cancel_list" class="period_list_item" >
            <asp:Literal ID="ltCancelDefault" runat="server"></asp:Literal>
                 
            </div>
    
    <a href="" style=" width:100%;margin-top:10px; display:block; text-align:center;text-decoration:underline;color:#608000;" onclick="addRule();return false;" ><img src="/images/plus_s.png" />&nbsp;add rules</a>

     </div>
     <%--<div id="package_price" class="blogInsert">
      <h4><asp:Image ID="Image8" runat="server" ImageUrl="~/images/content.png" />Price</h4>
        <asp:TextBox ID="txtPrice" runat="server" EnableTheming="false"  ClientIDMode="Static" CssClass="Extra_textbox_big_yellow"></asp:TextBox>
     </div>--%>

    <div id="load_rate">
     
        <%--<p class="extra_title">Add Rate</p>--%>
        <div id="rate_insert" class="blogInsert">
        <h4><asp:Image ID="Image10" runat="server" ImageUrl="~/images/content.png" /> Add Rate</h4>
            <table width="95%">
            <tr>
                <td align="right"><label>Date Range From </label></td>
                <td><input type="text" id="rate_DateStart" readonly="readonly" class="Extra_textbox" style="width:120px;"/></td>
                <td align="right"><label> To </label></td>
                <td><input type="text" id="rate_DateEnd" readonly="readonly" class="Extra_textbox" style="width:120px;"   /></td>
                <td align="right"><label>Amount</label></td>
                <td>
                <input type="text" id="rate_amount" class="Extra_textbox_yellow" style="width:80px;" /></td>
               
                <td align="right">
                
                <asp:CheckBox ID="sur_checked" runat="server"  ClientIDMode="Static" onclick="SurCharge_Checked();" />
                </td>
                <td align="left"><label>Surcharge includes</label></td>
                <td><input type="button" id="Button2" value="Add" onclick="AddRate(); return false;" class="Extra_Button_small_blue" /></td>
            </tr>
      
            <tr>
                <td colspan="10">
                    <div id="surcharge_amount" style="display:none;" >
                        <div id="dayofweek_surcharge">
                        <table>
                            <tr>
                                <td><label > Nomal Day Surcharge</label></td>
                                <td >
                                <div class="day_list" id="day_list">
                                    <p><input type="checkbox" id="Sun" value="0" name="dayofWeek" />Sun</p>
                                    <p><input type="checkbox" id="Mon" value="1" name="dayofWeek"/>Mon</p>
                                    <p><input type="checkbox" id="Tue" value="2" name="dayofWeek"/>Tue</p>
                                    <p><input type="checkbox" id="Wed" value="3" name="dayofWeek" />Wed</p>
                                    <p><input type="checkbox" id="Thu" value="4" name="dayofWeek" />Thu</p>
                                    <p><input type="checkbox" id="Fri" value="5" name="dayofWeek" />Fri</p>
                                    <p><input type="checkbox" id="Sat" value="6" name="dayofWeek" />Sat</p>
                                    </div>
                                </td>

                                <td ><label>Amount</label></td>
                                <td>
                                    <input type="text" id="sur_amount" class="Extra_textbox_yellow" style="width:80px; padding:2px;" />
                                </td>
                            </tr>
                        </table>
                        </div>
                        <div id="holiday_surcharge">
                            <p class="holiday_surcharge_head"><label> Holiday surcharge</label></p>
                            <div id="holiday_surcharge_charge">
                             
                            </div>
                        </div>
                        <div style= "text-align:right">
                        <input type="button" id="Button3" value="Add" onclick="AddRate(); return false;" class="Extra_Button_small_blue" />
                        </div>
                    </div>
                    </td>
            </tr>
            
            </table>
            
        </div>
        <div id="rate_load_result">
            <div id="rate_load_head"  style="display:none;">
                <table width="100%" >
                    <tr bgcolor="#96b4f3" align="center"><td width="15%">Date From</td><td width="15%">Date To</td>
                    <td width="10%">Amount</td>
                    <td width="10%">Surcharge</td><td width="20%">Day Surcharge</td><td width="10%">Holiday Surcharge</td><td width="5%">Delete</td>
                    </tr>
                </table>
            </div>
        </div>
     </div>

     <div id="package_Save" style="text-align:center; margin:15px 0px 0px ; padding:5px; border:1px solid #f7f3da; background-color:#fbfbf9;">
    <p>*Please check information and rate above before click to save</p>
    <%--<asp:Button ID="btnLoad_package" runat="server" Text="Save" CssClass="Extra_Button_green" EnableTheming="false"  OnClick="btnLoad_package_Onclick" />--%>
    <input type="button" id="btnLoad_package" onclick="SaveNewPackage();return false;" value="Save" class="Extra_Button_green" />
    <%--<asp:Button ID="btnload_tariff_save" runat="server" onc Text="Load new tariff" OnClick="btnload_tariff_save_onclick" CssClass="Extra_Button_green" />--%>
    
    </div>
    <asp:HiddenField ID="hd_isCurrent"  ClientIDMode="Static" runat="server" Value="false" />
</div>
</asp:Content>

