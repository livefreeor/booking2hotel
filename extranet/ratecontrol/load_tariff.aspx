<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_ExtranetControlPanel.master" AutoEventWireup="true" CodeFile="load_tariff.aspx.cs" Inherits="Hotels2thailand.UI.extranet_load_tariff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript" src="../../scripts/jquery-1.6.1.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/extranetmain.js"></script>
<script language="javascript" type="text/javascript" src="../../scripts/extranet/darkman_utility_extranet.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.ui.datepicker.js"></script>
<link type="text/css" href="../../css/datepickerCss/jquery.ui.all.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/darkman_datepicker.js"></script>
<link type="text/css" href="../../css/extranet/load_tariff.css" rel="stylesheet" />
<script type="text/javascript" src="../../Scripts/extranet/load_tariff.js"></script>
<script type="text/javascript" language="javascript">

    function SaveCondition() {
        
        if ($("#img_progress").length) {
            $("#img_progress").remove();
        }
        //alert(DayancelCheckLoadTariff("load_tariff_save", "")); //CancelCheckLoadTariff("load_tariff_save", "")
        if ($("#hd_duplicate").val() == "yes" && DayancelCheckLoadTariff("load_tariff_save", "") == "0" && CancelCheckLoadTariff("load_tariff_save", "") == "0") {


            $("<img id=\"img_progress\"  class=\"img_progress\" src=\"../../images_extra/preloader.gif\" alt=\"Progress\" />").insertBefore("#load_tariff_save").ajaxStart(function () {
                $(this).show();
                document.getElementById('btnload_tariff_save').disabled = 'true';
                //$("#btnload_tariff_save").css("display", "none");
                //$("#btnload_tariff_save").unbind("click");

            }).ajaxStop(function () {
                $(this).remove();
                document.getElementById('btnload_tariff_save').disabled = 'false';
                //$("#btnload_tariff_save").removeAttr("disabled");
            });

            var qPrductId = GetValueQueryString("pid");
            var qSupId = GetValueQueryString("supid");
            var OptionId = $("#dropRoom").val();

            var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

            $.post("../ajax/ajax_load_tariff_save.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new"), post, function (data) {

                
                if (data == "True") {
                    // DarkmanPopUpAlert(450, "Your data is added to save.");
                    if (qPrductId == "" && qSupId == "") {
                        window.location.href = "/extranet/ratecontrol/condition_control.aspx?oid=" + OptionId;
                    } else {
                        window.location.href = "/extranet/ratecontrol/condition_control.aspx?pid=" + qPrductId + "&supid=" + qSupId + "&oid=" + OptionId;
                    }

                }
                if (data == "method_invalid") {
                    DarkmanPopUpAlert(450, "Sorry !! You cannot Access this Action");
                }
            });


        }
        

    }


    function RateAddcheck() {
        
    }


    function DayancelCheckLoadTariff(id, position) {
        var resultDayMain = 0;
        var Y_top = $("#" + id).offset().top + 23;
        var X_left = $("#" + id).offset().left;
        //

        var text = "*You can not add the same no.of days cancel. Please recheck";
        var optionwidth = 0;
        var optionheight = 0;

        if (position == "left") {
            optionwidth = $("#" + id).width() + 10;
            optionheight = $("#" + id).height();
            //alert(optionheight);
        }
        else {
            optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
        }


        optionheight = $("#" + id).height() - 11;


        $(".period_list_item").each(function () {
            var cancelListId = $(this).attr("id");

            var MainChecked = $(this).find("input[name='period_list_Checked']:checked").val();
            var resultDay = 0;
            var countindex = 0;


            $(this).find(".cancel_list_item").each(function () {

                var CheckVal = $(this).find("input[name^='cencel_list_Checked_']:checked").val();

                var DayCancel = $(this).find(":selected").val();

                
                var CountDetect = $(this).parent().find(".cancel_list_item").filter(function (index) {
                    return $(this).find(":selected").val() == DayCancel && index != countindex;
                }).length;



                if (CountDetect > 0) {
                    resultDay = resultDay + CountDetect;
                    return false;
                }

                countindex = countindex + 1;



            });

            if (resultDay > 0) {
                resultDayMain = resultDayMain + 1;
                return false;
            }

        });



        if (resultDayMain > 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {
        
            $("#" + id).css("background-color", "#f7f7f7");
            $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }



        $(".cancel_list_item").each(function () {

            var CheckVal = $(this).find(":checked").val();

            

            $(this).find("select").stop().change(function () {

                //var DayCharge = $(this).val();
                var countindex = 0;
                var DayCancel = $(this).val();

                var CancelId = $(this).attr("id");
                var result1 = 0;
                var result2 = 0;
                $(".period_list_item").each(function () {

                    var cancelListId = $(this).attr("id");

                    var MainChecked = $(this).find("input[name='period_list_Checked']:checked").val();

                    var countindex = 0;


                    $(this).find(".cancel_list_item").each(function () {

                        var CheckVal = $(this).find("input[name^='cencel_list_Checked_']:checked").val();

                        var DayCancel = $(this).find(":selected").val();


                        var CountDetect = $(this).parent().find(".cancel_list_item").filter(function (index) {
                            return $(this).find(":selected").val() == DayCancel && index != countindex;
                        }).length;



                        if (CountDetect > 0) {
                            result1 = result1 + CountDetect;
                            return false;
                        }

                        countindex = countindex + 1;



                    });

                   

                    if (result1 > 0) {
                        result2 = result2 + 1;
                        return false;
                    }

                });

              

                if (result2 > 0) {
                    if (!$("#valid_alert_" + id).length) {
                        $("#" + id).css("background-color", "#ffebe8");
                        $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                        $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                        $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                        $("#valid_alert_" + id).fadeIn('fast');
                    }
                } else {
                    
                    $("#" + id).css("background-color", "#f7f7f7");
                    $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                    $("#valid_alert_" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }

            });


        });

        return resultDayMain;
    }


    function CancelCheckLoadTariff(id, position) {
        


        //        var DateBookingstart = $("#hd_" + datestart).val();
        //        var DateBookingend = $("#hd_" + dateend).val();

        var Y_top = $("#" + id).offset().top + 23;
        var X_left = $("#" + id).offset().left;
        //        
        var text = "*Please add the number of in No.of Night(s) charge or Percentage Charge.";
        var optionwidth = 0;
        var optionheight = 0;

        if (position == "left") {
            optionwidth = $("#" + id).width() + 10;
            optionheight = $("#" + id).height();
            //alert(optionheight);
        }
        else {
            optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
        }


        optionheight = $("#" + id).height() - 11;


        //result = daydiff(parseDate(DateBookingstart), parseDate(DateBookingend));
        var regExpr = new RegExp("^[0-9][0-9]*$");
        var resultCharge = 0;
        $(".cancel_list_item").each(function () {

            
            var CheckVal = $(this).find(":checked").val();

            var DayCharge = $(this).find("input[name^='txt_day_charge']").val();
            var DayPer = $(this).find("input[name^='txt_per_charge']").val();


            if (DayCharge == "0" && DayPer == "0") {
                resultCharge = resultCharge + 1;
            }

            if (DayCharge > 0 && DayPer > 0) {
                resultCharge = resultCharge + 1;
            }

            if (DayCharge == "" && DayPer == "") {
                resultCharge = resultCharge + 1;
            }

            if (!regExpr.test(DayCharge) || !regExpr.test(DayPer)) {
                resultCharge = resultCharge + 1;
            }


        });


        if (resultCharge > 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#f7f7f7");
            //$("#" + id).next("div").stop().css("background-color", "#f7f7f7");

            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }

            $("input[name^='txt_day_charge']").keyup(function () {

                var result1 = 0;
                $(".cancel_list_item").each(function () {
                    var DayCharge = $(this).find("input[name^='txt_day_charge']").val();
                    var DayPer = $(this).find("input[name^='txt_per_charge']").val(); //$("#txt_per_charge_" + CheckVal).val();
                    //alert(DayCharge + "--" + DayPer);
                    if (DayCharge == "0" && DayPer == "0") {
                        result1 = result1 + 1;
                    }

                    if (DayCharge > 0 && DayPer > 0) {
                        result1 = result1 + 1;
                    }

                    if (DayCharge == "" && DayPer == "") {
                        result1 = result1 + 1;
                    }

                    if (!regExpr.test(DayCharge) || !regExpr.test(DayPer)) {
                        result1 = result1 + 1;
                    }
                });

                
                if (result1 > 0) {
                    if (!$("#valid_alert_" + id).length) {
                        $("#" + id).css("background-color", "#ffebe8");
                        //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                        $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                        $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                        $("#valid_alert_" + id).fadeIn('fast');
                    }
                } else {

                    $("#" + id).css("background-color", "#f7f7f7");
                    //$("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                    $("#valid_alert_" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }

            });


            $("input[name^='txt_per_charge']").keyup(function () {

                var result2 = 0;
                 $(".cancel_list_item").each(function () {
                     var DayCharge = $(this).find("input[name^='txt_day_charge']").val(); //$("#txt_day_charge_" + CheckVal).val();
                     var DayPer = $(this).find("input[name^='txt_per_charge']").val();
                    //alert(DayCharge + "--" + DayPer);

                    if (DayCharge == "0" && DayPer == "0") {
                        result2 = result2 + 1;
                    }

                    if (DayCharge > 0 && DayPer > 0) {
                        result2 = result2 + 1;
                    }

                    if (DayCharge == "" && DayPer == "") {
                        result2 = result2 + 1;
                    }

                    if (!regExpr.test(DayCharge) || !regExpr.test(DayPer)) {
                        result2 = result2 + 1;
                    }

                });
               
                if (result2 > 0) {
                    if (!$("#valid_alert_" + id).length) {
                        $("#" + id).css("background-color", "#ffebe8");
                        //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                        $("body").after("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                        $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                        $("#valid_alert_" + id).fadeIn('fast');
                    }
                } else {

                    $("#" + id).css("background-color", "#f7f7f7");
                    //$("#" + id).next("div").stop().css("background-color", "#f7f7f7");

                    $("#valid_alert_" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }
            });

        return resultCharge;
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<input type="hidden" id="hd_duplicate" />
 <div id="mainExtranet_form">
    <div id="load_tariff_room_select">   
    <fieldset style="border:0px;">
     <legend style="color:#000000">Room Type</legend>
        <asp:DropDownList ID="dropRoom"  EnableTheming="false" runat="server"  ClientIDMode="Static"  CssClass="Extra_Drop" style="width:500px;"></asp:DropDownList>
        
       </fieldset>
    </div>
    <div id="load_tariff_condition" class="blogInsert">
    <h4><asp:Image ID="Image4" runat="server" ImageUrl="~/images/content.png" /> &nbsp;Condition Information</h4>
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
            <div><fieldset ><legend>Adult</legend>
            <asp:DropDownList ID="drop_adult"  EnableTheming="false"  runat="server" ClientIDMode="Static"  Width="50px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            <div><fieldset ><legend>Child</legend>
            <asp:DropDownList ID="drop_child" EnableTheming="false"  runat="server" ClientIDMode="Static"  Width="50px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            <div style="width:90px; margin-right:20px;"><fieldset ><legend>ABF</legend>
            <asp:DropDownList ID="drop_breakfast" EnableTheming="false"  runat="server" ClientIDMode="Static"  Width="90px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            <div><fieldset ><legend>Extra Bed</legend>
            <asp:DropDownList ID="drop_extrabed" EnableTheming="false" runat="server" ClientIDMode="Static"  Width="50px" CssClass="Extra_Drop"></asp:DropDownList>
            </fieldset></div>
            
            
        </td>
        
        </tr>
    </table>
        
        <br />
    </div>

    <div id="load_tariff_policy">
        
        <div id="Div1" class="blogInsert">
        <h4><asp:Image ID="Image2" runat="server" ImageUrl="~/images/content.png" /> Policy</h4>
        <table>
            <tr>
                <td>
                    <div id="policy_type" style=" float:left;"><fieldset ><legend>Policy Type</legend>
                        <asp:DropDownList ID="dropPolicyType" EnableTheming="false"  runat="server" ClientIDMode="Static" CssClass="Extra_Drop">
                        <asp:ListItem Text="Check-in" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Check-out" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Pets" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Child" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Custom" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                        </fieldset>
                    </div>
                    <div id="policy_type_custom" style="display:none; margin:0px 0px 0px 3px; float:left;"><fieldset ><legend>Custom Type</legend>
                        <asp:TextBox ID="txt_Type_custom" runat="server" EnableTheming="false" ClientIDMode="Static" CssClass="Extra_textbox_yellow" style="width:150px;" ></asp:TextBox>
                    </fieldset>
                    </div>
                </td>
                <td>
                <div><fieldset ><legend>Description</legend>
            <asp:TextBox ID="txt_policy" runat="server" EnableTheming="false" ClientIDMode="Static" CssClass="Extra_textbox" style="width:500px;" ></asp:TextBox>
            </fieldset></div>    
                </td>
                <td>
                <div>
                <input type="button" style="margin-top:17px;" value="Add"  class="Extra_Button_small_blue" onclick="appendPolicy();" />
                <%--<asp:Button ID="AddPolicy"  runat="server" style="margin-top:17px;"  Text="Add"   OnClientClick="appendPolicy();return false;"/>--%>
                </div>
                </td>
            </tr>
        </table>

        <div style="clear:both"></div>
        <div class="policy_list" id="policy_list">
           
            
        </div>
        </div>
        
       
        <div style="clear:both;"></div>

    </div>
    <div style="clear:both"></div>
    
    <div id="load_tariff_period_cancel">
    
        <%--<p class="extra_title">Cancellation</p>--%>

        <div id="period_insert" class="blogInsert">
           <h4><asp:Image ID="Image3" runat="server" ImageUrl="~/images/content.png" /> Cancellation</h4>
            <table>
            <tr><td><label>Date Range From </label></td><td><input type="text" id="period_Datestart" class="Extra_textbox" style="width:120px;"  /></td><td><label> To</label> 
            </td><td><input type="text" id="period_DateEnd" class="Extra_textbox" style="width:120px; "  /></td><td><input type="button" id="Button1" value="Add" onclick="AddPeriod();return false;" class="Extra_Button_small_blue" /></td></tr>
            
            </table>
         
        </div>
        <div id="period_list">
            
        </div>
     </div>
     
     
     <div id="load_rate">
     
        <%--<p class="extra_title">Add Rate</p>--%>
        <div id="rate_insert" class="blogInsert">
        <h4><asp:Image ID="Image1" runat="server" ImageUrl="~/images/content.png" /> Add Rate</h4>
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
                <td><input type="button" id="Button2" value="Add" onclick="AddRate();return false;" class="Extra_Button_small_blue" /></td>
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
                        <input type="button" id="Button3" value="Add" onclick="AddRate();return false;" class="Extra_Button_small_blue" />
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
    </div>


    <div id="load_tariff_save" style="text-align:center; margin:15px 0px 0px ; padding:5px; border:1px solid #f7f3da; background-color:#fbfbf9;">
    <p>*Please check information and rate above before click to save</p>
    <input type="button" id="btnload_tariff_save" onclick="SaveCondition();" value="Load new tariff" class="Extra_Button_green" />
    <%--<asp:Button ID="btnload_tariff_save" runat="server" onc Text="Load new tariff" OnClick="btnload_tariff_save_onclick" CssClass="Extra_Button_green" />--%>
    
    </div>
    





</asp:Content>

