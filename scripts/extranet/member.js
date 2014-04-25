$(document).ready(function () {
    
    //$.darkmanExtend();
    //var Topleft = { "i_top": mytop, "i_left": myleft };

    GetMemberDiscountList();

    $("#addnewmember").click(function () {

        StringUrl = "../ajax/ajax_member_price_condition_sel.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");

        $.get(StringUrl, function (data) {

            $("#member_price_box_real").html(data);

            $("#member_price_box_real").slideToggle();

            DatepickerDual("member_date_start", "member_date_end");

            $("#save_price_member").click(function () {

                InsertmemberPrice();

                return false;
            });


            $("#cancelPrice_member").click(function () {
                $("#member_price_box_real").slideToggle();

                ClearAlert("member_price_box_real", "#ffffff");

            });

            

            //BindEvent Room CheckBox
            $("input[name='checkbox_room_check']").click(function () {
                var key = $(this).val();
                var checkchild = $("#condition_list" + key).find(":checkbox");
                if ($(this).is(":checked")) {
                    checkchild.attr("checked", "checked");

                } else {
                    checkchild.removeAttr("checked");
                }


            });

            //Bind Condition Check box 
            $("input[name='checkbox_condition_check']").click(function () {


                var parentCheck = $(this).parent().parent().parent().parent().parent().parent()
                    .find("input[name='checkbox_room_check']").stop();


                var DivChild = parentCheck.parent().next().attr("id");

                var CheckCount = $("#" + DivChild).find(":checked").length;

                if (CheckCount > 0) {
                    parentCheck.attr("checked", "checked");
                }
                else {
                    parentCheck.removeAttr("checked");
                }

            });
        });


        return false;
    });
});



function InsertmemberPrice() {

    var isNum = ValidateOptionMethod("member_amount", "number");

    var valid = PeriodValidCheck("member_price_box_real", "member_date_start", "member_date_end", "", "chk_discount_id", "hd_date_start_", "hd_date_end_", { extendHeight: 10, bgDefault: "#ffffff" });

    

    //var Countcheck = CheckboxCheckValidInsert("member_price_box_real", {extendHeight:10});
    
    //alert(CheckboxCheckValidInsert("member_price_box_real"));
    
    if (valid == 0 && CheckboxCheckValidInsert("member_price_box_real", { extendHeight: 10 }) > 0 && isNum) {

   // alert("HELLO");
        DarkmanProgress("member_price_list");

        StringUrl = "../ajax/ajax_member_price_insert.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");


        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
       
        $.post(StringUrl, post, function (data) {
            
            if (data != "method_invalid") {
                
                DarkmanPopUpAlertFn_Callback(450, "Your data is added to save.", function () {
                    $("#member_price_box_real").slideToggle();

                    ClearAlert("member_price_box_real", "#ffffff");

                    GetMemberDiscountList();
                });
                

            } else {
            }
        });
    }
}

function CheckboxCheckValidInsert(id,option) {

    //var id = "member_price_box_real";
    var ValDefault = {
        extendHeight: 0,
        extendWidth: 10,
        bgAlert: "#ffebe8",
        bgDefault:"#ffffff"
    }


    var opTs = jQuery.extend(ValDefault, option);

    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;
    var result = 0;
    var text = "*Please select at least one.";

    optionwidth = ($("#" + id).width() + opTs.extendWidth) - $("#" + id).width();
    optionheight = $("#" + id).height() + opTs.extendHeight;

    var CheckSel = $("input[name='checkbox_condition_check']:checked").length;
    
    if (CheckSel == 0) {
        if (!$("#valid_alert_" + id).length) {
            $("#" + id).css("background-color", opTs.bgAlert);
            $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }
    } else {

        $("#" + id).css("background-color", opTs.bgDefault);
        $("#valid_alert_" + id).fadeOut('fast', function () {

            $(this).remove();
        });
    }



    $("input[name='checkbox_condition_check']").click(function () {
        var CheckSel = $("input[name='checkbox_condition_check']:checked").length
        if (CheckSel == 0) {
           // alert(CheckSel);
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", opTs.bgAlert);
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", opTs.bgDefault);
            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
    });


    $("input[name='checkbox_room_check']").click(function () {
        var CheckSel = $("input[name='checkbox_condition_check']:checked").length
        if (CheckSel == 0) {
           // alert(CheckSel);
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", opTs.bgAlert);
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", opTs.bgDefault);
            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
    });

    return CheckSel;
}


function GetMemberDiscountList(callback,obj) {

    DarkmanProgress("member_price_list");
    StringUrl = "../ajax/ajax_member_price_list.aspx" + GetQuerystringProductAndSupplierForBluehouseManage("new");
    $.get(StringUrl, function (data) {

        $("#member_price_list").html(data);

        //Bind DatePicker
        $(".main_edit_table").each(function () {

            var idDateStart = $(this).find("input[name^='date_start_']").stop().attr("id");
            var idDateEnd = $(this).find("input[name^='date_end_']").stop().attr("id");


            DatepickerDual_nopic(idDateStart, idDateEnd);
        });

        //Bind  Remove Discount link (img >>bin)
        $("a[id^='link_remove_']").click(function () {

            var key = $(this).attr("href");

            var JsKey = { "key_id": key };
            //var JsKey = new Array({ "key": key });

            DarkmanPopUpComfirmCallback(400, "Are you sure to remove now?", function (key) {


                RemoveDiscount(key.key_id);

            }, JsKey);


            
            return false;
        });




        // Bind Event Clcik COndition Pan(img)!!
        $("a[id^='link_condition_']").click(function () {
            var key = $(this).attr("href");
            $(this).toggleClass("link_condition_up");


            if ($("#date_end_" + key).css("display") !== "none") {
                FormToggle(key);

                var ID = "main_edit_table_" + key;

                $("#" + ID).find("input[name^='date_end_']").css("background-color", "#ffffff");
                $("#" + ID).find("input[name^='date_start_']").css("background-color", "#ffffff");

                ClearAlert(ID, "#ffffff");
            }

            $("#condition_pan_" + key).slideToggle(300);

            RoolbackDataDefault(key);

            return false;
        });

        // Bind Event Clcik Edit Link(img)!!
        $("a[id^='link_edit_']").click(function () {
            var key = $(this).attr("href");
            // $(this).toggleClass("link_condition_up");




            if ($("#date_end_" + key).css("display") !== "inline") {
                if ($("#condition_pan_" + key).css("display") == "none") {
                    $("#condition_pan_" + key).slideToggle(300);
                }
                FormToggle(key);
            }


            return false;
        });


        //TempDateDEfault

        $(".discount_main_block_list").each(function () {
            var key = $(this).find("input[name='chk_discount_id']").stop().val();
            var dataDf = $(this).find("input,textarea,select,hidden").serialize();


            //alert(escape(dataDf));
            $("#div_tmp_" + key).html(escape(dataDf));
        });

        //Bind Edit Link
        $(".condition_pan_edit").click(function () {
            var key = $(this).attr("href");

            FormToggle(key);


            var ID = "main_edit_table_" + key;

            $("#" + ID).find("input[name^='date_end_']").css("background-color", "#ffffff");
            $("#" + ID).find("input[name^='date_start_']").css("background-color", "#ffffff");

            ClearAlert(ID, "#ffffff");

            RoolbackDataDefault(key);
            return false;
        });

        //Bind Hide Link
        $(".condition_pan_hide").click(function () {

            var key = $(this).attr("href");

            if ($("#date_end_" + key).css("display") !== "none") {
                FormToggle(key);

                var ID = "main_edit_table_" + key;

                $("#" + ID).find("input[name^='date_end_']").css("background-color", "#ffffff");
                $("#" + ID).find("input[name^='date_start_']").css("background-color", "#ffffff");

                ClearAlert(ID, "#ffffff");
            }

            $("#condition_pan_" + key).slideToggle(300);

            RoolbackDataDefault(key);
            return false;
        });


        //Bind Cancel btn
        $(".cancelEdit").click(function () {

            var key = $(this).attr("id");

            FormToggle(key);


            RoolbackDataDefault(key);

            var ID = "main_edit_table_" + key;

            $("#" + ID).find("input[name^='date_end_']").css("background-color","#ffffff");
            $("#" + ID).find("input[name^='date_start_']").css("background-color", "#ffffff");
            
            ClearAlert(ID, "#ffffff");

            return false;
        });

        //Bind RoomChecked Default
        $("input[name='checkbox_room_check']").each(function () {
            var key = $(this).val();
            var checkchild = $("#condition_list_" + key).find(":checked");
            if (checkchild.length > 0) {
                $(this).attr("checked", "checked");
                
            }
        });


        //BindEvent Room CheckBox
        $("input[name='checkbox_room_check']").click(function () {
            var key = $(this).val();
            var checkchild = $("#condition_list_" + key).find(":checkbox");
            if ($(this).is(":checked")) {
                checkchild.attr("checked", "checked");

            } else {
                checkchild.removeAttr("checked");
            }


        });



        //Bind Condition Check box 
        $("input[name^='checkbox_condition_check_']").click(function () {


            var parentCheck = $(this).parent().parent().parent().parent().parent().parent()
                .find("input[name='checkbox_room_check']").stop();


            var DivChild = parentCheck.parent().next().attr("id");

            var CheckCount = $("#" + DivChild).find(":checked").length;

            if (CheckCount > 0) {
                parentCheck.attr("checked", "checked");
            }
            else {
                parentCheck.removeAttr("checked");
            }

        });

        //Bind Event Keyup Update label
        ShowRealContent();

        BindCallback(callback, obj);
        
    });

   
}


function FormToggle(key) {

    $("#btn_price_edit_" + key + "> :input").toggle(300);
    $("#condition_pan_" + key).find(":checkbox,.dot_list").toggle(300);

    $("#main_edit_table_" + key).find(":text,span").toggle(300);

}

function ShowRealContent() {


    $("input[name^='date_start']").change(function () {
        //alert($(this).val())

        $(this).parent().find("span").stop().html($(this).val());
    });
    $("input[name^='date_end']").change(function () {
        //alert($(this).val())
        $(this).parent().find("span").stop().html($(this).val());
    }); 
    $("input[name^='discount_amount']").keyup(function () {
        //alert($(this).val())
        $(this).parent().find("span").stop().html($(this).val());
    });

}

function RoolbackDataDefault(key) {

    var def = $("#div_tmp_" + key).html();

    //chk_discount_id=6&date_start_6=27-Nov-2012&hd_date_start_6=2012-11-27&date_end_6=30-Nov-2012&hd_date_end_6=2012-11-30&discount_amount_6=50&checkbox_condition_check_6=2464&checkbox_condition_check_6=2144


    //chk_discount_id=6&date_start=27-Nov-2012&hd_date_start=2012-11-27&date_end=30-Nov-2012&hd_date_end=2012-11-30&discount_amount=50&checkbox_condition_check=2464&checkbox_condition_check=2144



    var newValue = unescape(def).replace(new RegExp("_" + key, "g"), "");

    var Jsdata = $.deparam(newValue);

    $("input[name='date_start_" + key + "']").val(Jsdata.date_start);
    $("input[name='hd_date_start_" + key + "']").val(Jsdata.hd_date_start);
    $("input[name='date_end_" + key + "']").val(Jsdata.date_end);
    $("input[name='hd_date_end_" + key + "']").val(Jsdata.hd_date_end);
    $("input[name='discount_amount_" + key + "']").val(Jsdata.discount_amount);

    var checked = Jsdata.checkbox_condition_check;


    var Checkbox = $("input[name='checkbox_condition_check_" + key + "']");

    Checkbox.removeAttr("checked");

    var arrChecked = String(Jsdata.checkbox_condition_check).split(',');
    for (i = 0; i < arrChecked.length; i++) {


        Checkbox.filter(function (index) {
            return $(this).val() == arrChecked[i]
        }).attr("checked", "checked");;


    }


    checkboxHeadREcheck();
}


function checkboxHeadREcheck() {


    //BindEvent Room CheckBox
    $("input[name='checkbox_room_check']").each(function () {
        var key = $(this).val();
        var checkchild = $("#condition_list_" + key).find(":checked").length;

        if (checkchild < 1) {
            $(this).removeAttr("checked");

        }
    });
}


function SaveEdit(disId) {


    var dateStart = "hd_date_start_" + disId;
    var dateEnd = "hd_date_end_" + disId;
   
    var ID = "main_edit_table_" + disId;


    var valid_overLap = PeriodValidCheck_overlap(ID, disId, dateStart, dateEnd, "", "chk_discount_id", "hd_date_start_", "hd_date_end_",
        {extendWidth:100});

   

    if (valid_overLap == 0) {

        DarkmanProgress("btn_price_edit_" + disId);

        StringUrl = "../ajax/ajax_member_price_update.aspx?dis_id=" + disId + GetQuerystringProductAndSupplierForBluehouseManage("append");

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
      
        $.post(StringUrl, post, function (data) {
            
            
            if (data != "method_invalid") {
                if (data == "True") {

                    
                    
                    
                   
                    DarkmanPopUpAlertFn_Callback(450, "Your data is updated to save.", function () {
                      
                        GetMemberDiscountList(function () {
                            var key = disId;
                            //$("#condition_pan_23").length();
                           // console.log();
                            //$("#condition_pan_23").slideToggle(300)
                            //$("link_condition_" + key).toggleClass("link_condition_up");
                            //$(this).toggleClass("link_condition_up");


                            if ($("#date_end_" + key).css("display") !== "none") {
                                FormToggle(key);

                                var ID = "main_edit_table_" + key;

                                $("#" + ID).find("input[name^='date_end_']").css("background-color", "#ffffff");
                                $("#" + ID).find("input[name^='date_start_']").css("background-color", "#ffffff");

                                ClearAlert(ID, "#ffffff");
                            }

                            $("#condition_pan_" + key).show();

                            RoolbackDataDefault(key);
                             //alert("HELLO");
                            //$(this).toggleClass("link_condition_up");
                            $("link_condition_" + key).toggleClass("link_condition_up");

                            //$("#condition_pan_" + key).show();
                        });
                       
                    });
                    //RoolbackDataDefault(key);

                    //FormToggle(disId);
                   // DarkmanPopUpAlert(450, "Your data is updated to save.");

                }
            } else {

            }
        });
    }
}

function RemoveDiscount(key) {

    DarkmanProgress("member_price_list");

    StringUrl = "../ajax/ajax_member_price_remove.aspx?dis_id=" + key + GetQuerystringProductAndSupplierForBluehouseManage("append");


    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    $.post(StringUrl, post, function (data) {

        if (data != "method_invalid") {
            if (data == "True") {
                DarkmanPopUpAlert(450, "Remove Completed!");
                GetMemberDiscountList();
            }
        } else {
        }
    });
}