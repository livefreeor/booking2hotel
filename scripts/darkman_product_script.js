$(document).ready(function () {

   

    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    if (qProductId == "") {
        $("#InsertMode").show();
        $("#EditMode").hide();

    }
    else {
        $("#InsertMode").hide();
        $("#EditMode").show();

        $.get("ajax_product_supplierList_Manage.aspx?pid=" + qProductId, function (data) {
            $("#SupSelected_Result").html(data);
            $("#SupSelected_Result ul li:not(.sup_active)").filter(function () {
                return $(this).attr("class") != "sup_selected_inactive";
            }).addClass("sup_use");

        });

        //ProductDetail DataBind
        $.get("ajax_product_detail.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {

            $("#product_detail").html(data);

            //Product Payment Plan
            paymentPlandataBind();

        });
        

        //ProductConfig DataBind
        $.get("ajax_product_config.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
            $("#product_config").html(data);
        });

        //Product_content
        $.get("ajax_product_content_box.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
            $("#product_content_box").html(data);
        });

        //Product_Location
        $.get("ajax_product_location.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
            $("#product_location").html(data);

        });

        //Product_Style
        $.get("ajax_product_style.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
            $("#product_style").html(data);

        });

        //Product_LandMarkDrop
        $.get("ajax_product_landmark_cat_drop.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
            $("#landmark_drop_cat").html(data);

            var CatSelected = $("#landmark_drop_cat :selected").val();

            $.get("ajax_product_landmark_drop.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&LanCat=" + CatSelected, function (data) {
                $("#landmark_drop").html(data);
            });
        });

        //Product_LandMark
        $.get("ajax_product_landmark_list.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
            $("#Landmark_drop_List_manage").html(data);
        });

        //Product_facility_box
        $.get("ajax_product_facility_box.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
            $("#Product_facility_box").html(data);
            //$("#facility_textinput").find(" :text").stop().watermark({ watermarkText: 'Insert New Facility Here', watermarkCssClass: 'someCSS' })
        });
    }

});

function InsertTemplateSave() {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");

    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#CurrentFac").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var Checked = "";
    $("#location_list :checked").each(function () {

        Checked = Checked + $(this).attr("value") + ","

    });

    $("#Product_facility_box").after("<input type=\"hidden\" name=\"CheckedLoc\" value=\"" + Checked + "\" />");

    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    $.post("ajax_product_facility_template_save.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, post, function (data) {
        
        if (data == "True") {
            $.get("ajax_product_facility_box.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#Product_facility_box").html(data);
                //$("#facility_textinput").find(" :text").stop().watermark({ watermarkText: 'Insert New Facility Here', watermarkCssClass: 'someCSS' })
            });

            DarkmanPopUp_Close();
        }
    });

}

function InsertTemplate() {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#CurrentFac").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });
    $.get("ajax_product_facility_template.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {

        DarkmanPopUp(900, data);
    });

}

function ShowEditMode(element_edit, element_show) {

    var Y_top = $("#" + element_show).position().top - 90;
    var X_left = $("#" + element_show).position().left;
    $("#" + element_edit).css({ "top": Y_top + "px", "left": X_left + "px" });
    $("#" + element_edit).fadeIn('fast');
}

function ShowDisplayMode(element_edit, element_show) {
    
    $("#" + element_edit).fadeOut('fast');

}



function UpdateFacility(id, element_target) {
    
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#" + element_target).ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });


    var Vals = $("#itemedit_" + id).find(" :text").stop().attr("value");

    $.get("ajax_product_facility_update.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&facid=" + id + "&val=" + Vals, function (data) {
        
        if (data == "True") {

            $("#itemlist_" + id).find("a").stop().html(Vals);
            $("#itemedit_" + id).fadeOut('fast');
        }


    });
}

function DelProductFac(id) {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#dlCurrentFac").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });
    $.get("ajax_product_facility_del.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&facid=" + id, function (data) {
        if (data == "True") {
            $.get("ajax_product_facility_box.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#Product_facility_box").html(data);
            });
        }
    });
}

function ContentSwitchLangDisplay(langId, DivResult, ContentType) {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#" + DivResult).ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });
    
    $.get("ajax_product_content_Lang_switch.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&LangId=" + langId, function (data) {

        if (data == "True") {
            if ($("#product_content_box").length == 1) {
                //Product Content Box
                $.get("ajax_product_content_box.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                    $("#product_content_box").html(data);
                });
            }

            if ($("#Product_facility_box").length == 1) {
                //Product_facility_box
                $.get("ajax_product_facility_box.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                    $("#Product_facility_box").html(data);
                });
            }

            langswitch(langId);
        }
    });
}

function landmark_save_item(id) {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#landmark_list").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    $.post("ajax_product_landmark_update.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&landMarkId=" + id, post, function (data) {

        if (data == "True") {
            $.get("ajax_product_landmark_list.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#Landmark_drop_List_manage").html(data);
            });
        }
    });
}

function LandMarkDel(id) {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#landmark_list").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });



    $.get("ajax_product_landmark_del.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&landMarkId=" + id, function (data) {
        if (data == "True") {
            $.get("ajax_product_landmark_list.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#Landmark_drop_List_manage").html(data);
            });
        }
    });
}

function InsertNewLandmark() {
    var CatSelected = $("#landmark_drop :selected").val();
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");

    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#landmark_list").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    $.get("ajax_product_landmark_insert.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&landMarkId=" + CatSelected, function (data) {
        if (data == "have") {
            DarkmanPopUpAlert(400, "LandMark Existed !! Please Insert another landmark");

        }
        if (data == "ok") {
            $.get("ajax_product_landmark_list.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#Landmark_drop_List_manage").html(data);
            });
        }

    });
}



function LandMarkCat_drop() {

    var CatSelected = $("#landmark_drop_cat :selected").val();
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#Landmark_drop_insert").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    $.get("ajax_product_landmark_drop.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&LanCat=" + CatSelected, function (data) {
        $("#landmark_drop").html(data);
    });
}


function InsertNewStyle() {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#product_style").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });
    $.get("ajax_product_style_insertForm.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {

        DarkmanPopUp(550, data);
    });

}

function InsertNewLocation() {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").prependTo("#product_location").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });
    $.get("ajax_product_location_insertForm.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {

        DarkmanPopUp(550, data);
    });

}

function SaveProductStyle() {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");

    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#product_style").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var Checked = "";
    $("#Product_style_list :checked").each(function () {
        Checked = Checked + $(this).parent("span").attr("id") + ","
    });

   
    $.get("ajax_product_style_save.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&CheckedLoc=" + Checked, function (data) {
      
        if (data == "True") {
            $.get("ajax_product_style.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#product_style").html(data);

            });

            DarkmanPopUp_Close();
        }


    });

}

function SaveLocation() {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");

    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#product_location").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var Checked = "";
    $("#location_list :checked").each(function () {
        Checked = Checked + $(this).parent("span").attr("id") + ","
    });

    $.get("ajax_product_location_save.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&CheckedLoc=" + Checked, function (data) {

        if (data == "True") {
            $.get("ajax_product_location.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#product_location").html(data);

            });

            DarkmanPopUp_Close();
        }


    });



}

//function ContentSwitchLangDisplay(langId, DivResult, ContentType) {
//    var qProductId = GetValueQueryString("pid");
//    var qProductCat = GetValueQueryString("pdcid");
//    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertBefore("#" + DivResult).ajaxStart(function () {
//        $(this).show();
//    }).ajaxStop(function () {
//        $(this).remove();
//    });
//    $.get("ajax_product_content_Lang_switch.aspx?pdcid=" + qProductCat + "&pid=" + qProductId + "&LangId=" + langId, function (data) {

//        if (data == "True") {
//            $.get("ajax_product_content_box.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
//                $("#product_content_box").html(data);
//            });

//            langswitch(langId);
//        }
//    });
//}


function SaveProductConfig() {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#product_config").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var post = $("#ProductCOnfig_form").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    $.post("ajax_product_config_save.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, post, function (data) {
        if (data == "True") {

            $.get("ajax_product_config.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#product_config").html(data);
            });
        }
    });
}

function SaveProductContent() {

    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#product_content_box").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    $.post("ajax_product_content_save.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, post, function (data) {
        
        if (data == "True") {

            $.get("ajax_product_content_box.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#product_content_box").html(data);
            });
        }
    });
}

function SaveProductInformation() {
    var qProductId = GetValueQueryString("pid");
    var qProductCat = GetValueQueryString("pdcid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#product_detail").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });
    var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    $.post("ajax_product_detail_save.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, post, function (data) {
        if (data == "True") {
            $.get("ajax_product_detail.aspx?pdcid=" + qProductCat + "&pid=" + qProductId, function (data) {
                $("#product_detail").html(data);
                 paymentPlandataBind();
            });


        }



    });
}

//function Supplier_Selc_Save() {
//    //aspnetForm
//    var qProductId = GetValueQueryString("pid");
//    var SupId = $("input[name='Supplier_Selected_Default']:checked", '#form1').val();
//    
//    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#SupSelected_Result").ajaxStart(function () {
//        $(this).show();
//    }).ajaxStop(function () {
//        $(this).remove();
//    });
//    var ListSup = "";
//    $("#SupSelected_Result ul li").each(function () {
//        ListSup = ListSup + $(this).attr("id").split('_')[3] + ",";
//    })

//    $.get("ajax_product_update_supplierPrice.aspx?supid=" + SupId + "&pid=" + qProductId + "&ListSup=" + ListSup, function (data) {

//        if (data == "True") {
//            $.get("ajax_product_supplierList_Manage.aspx?pid=" + qProductId, function (data) {
//                $("#SupSelected_Result").html(data);
//                $("#SupSelected_Result ul li:not(.sup_active)").filter(function () {
//                    return $(this).attr("class") != "sup_selected_inactive";
//                }).addClass("sup_use");

//                $("div #aphabet").slideUp();
//                $("div #supplier_List_result").slideUp();
//            });
//        }
//    });
//}




function AddMoreProduct() {
    $("div #aphabet").insertBefore("#SupSelected_Result").slideDown();
    $("#aphabet").css({ "margin-top": "20px" });
    $("div #supplier_List_result").insertAfter("#aphabet").slideDown();

    $("#SupSelected_Result ul li").each(function () {
        var pureId = $(this).attr("id").split('_')[3];

        $("#supplier_List_result ul li").filter(function () {
            if ($(this).attr("id").split('_')[1] == pureId) {
                $(this).unbind("click");
                $(this).css({ "background-color": "#ca0202", "font-weight": "bold", "color": "#ffffff" });

                $(this).hover(function () {
                    $(this).css({ "background-color": "#ca0202", "font-weight": "bold", "color": "#ffffff" });
                }, function () {
                    $(this).css({ "background-color": "#ca0202", "font-weight": "bold", "color": "#ffffff" });
                });
            }
        });


    });
}



function removeElement(id) {
    $("#" + id).removeAttr("onclick");
    $("#" + id).remove();
    var pureId = id.split('_')[3];

    $("#supplier_List_result ul li").filter(function () {
        if ($(this).attr("id") == "sup_" + pureId) {
            $(this).css({ "background-color": "#ffffff", "color": "#3f5d9d", "font-weight": "normal" });
            $(this).hover(function () {
                $(this).css({ "background-color": "#3f5d9d", "font-weight": "normal", "color": "#ffffff" });
            }, function () {
                $(this).css({ "background-color": "#ffffff", "color": "#3f5d9d", "font-weight": "normal" });
            });
        }
    });


    $("#SupSelected_Result ul li").each(function () {
        if ($(this).attr("class") == "sup_selected") {
            $(this).css({ "background-color": "#ffffff", "color": "#333333", "border": "1px solid #edeff4" });
            $(this).children(":radio").removeAttr("checked");
        }
        if ($(this).attr("class") == "sup_active") {
            $(this).css({ "background-color": "#3f5d9d", "color": "#ffffff" });
            $(this).children(":radio").attr("checked", "checked");
            //$(this).children("a").show();

        }
        if ($(this).attr("class") == "sup_use") {
            $(this).css({ "background-color": "#edeff4", "color": "#3b5998" });
            $(this).children(":radio").removeAttr("checked");
        }
    });


}

function Sup_check(liId) {

//    $("#SupSelected_Result ul li :radio").removeAttr("checked");
    //    $("#SupSelected_Result ul li").css({ "background-color": "#ebebe4", "color": "#c7bbbb" });
   
    $("#SupSelected_Result ul li").each(function () {

        if ($(this).attr("id") == liId) {
            $(this).css({ "background-color": "#3f5d9d", "color": "#ffffff" });

            $(this).children(":radio").attr("checked", "checked");

            
            //$(this).children("a").hide();
        }
        else {
            $(this).css({ "background-color": "#edeff4", "color": "#3b5998" });

            if ($(this).attr("class") == "sup_selected") {
                $(this).css({ "background-color": "#ffffff", "color": "#333333", "border": "1px solid #edeff4" });

            }


            if ($(this).attr("class") == "sup_selected_inactive") {

                $(this).css({ "background-color": "#ebebe4", "color": "#c7bbbb" });
            }

           // $(this).children(":radio").removeAttr("checked");
            //$(this).children("a").show();
        }
    });

}




function DisableSupplier(id) {
    
    $("#" + id).removeAttr("onclick");
    var SupId = id.split('_')[3];
    var qProductId = GetValueQueryString("pid");
    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").insertAfter("#SupSelected_Result").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });



    $.get("ajax_product_disable_supplier.aspx?supid=" + SupId + "&pid=" + qProductId, function (data) {
       
        if (data == "True") {
            $.get("ajax_product_supplierList_Manage.aspx?pid=" + qProductId, function (data) {
                $("#SupSelected_Result").html(data);
                $("#SupSelected_Result ul li:not(.sup_active)").filter(function () {
                    return $(this).attr("class") != "sup_selected_inactive";
                }).addClass("sup_use");

                GetListSUpplierByAphabet("A");
            });
        }

    });


}