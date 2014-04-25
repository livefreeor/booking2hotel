

$(document).ready(function () {
    var disSiteMap = $("#SiteMapBar").html();

    
    if (GetValueQueryString("pid") == "" && GetValueQueryString("supid") == "") {
        $("#SiteMapBar a").filter(function (index) { return $(this).html() == "Main Panel" }).remove();
        $(".separator").filter(function (index) { return index == 0 }).remove();
    }

    $(".separator").filter(function (index) { return index == ($(".separator").length - 1) })
        .css("background-image", "url(../../images_extra/site_map_separator_active.jpg)");


    //var arrurl = window.location.href.split('.aspx')[0].split('/');
    //var file = arrurl[arrurl.length - 1];
   

    //if (file !== "member_detail" && file !== "member_list") {
        
    //    eraseCookie("log");
    //}

});


function clickButton(e, buttonid) {
    
    var evt = e ? e : window.event;
    var bt = document.getElementById(buttonid);
    if (bt) {
        if (evt.keyCode == 13) {
            bt.click();
            return false;
        }
    }
}

function ChangeHotelExtra() {
    $("<img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" />").insertAfter("#extra_hotels_change").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {
        $(this).remove();
    });

    $.get("../../extranet/ajax/ajax_extra_product_active_change.aspx", function (data) {
        DarkmanPopUp(450, data);
        
    });
}

function hiddenStaffAdminMenu(target) {
    $(".menu_staff_admin").hide();
}

function showDiv(id_name) {
    var target = document.getElementById(id_name);
    target.style.display = (target.style.display == "none") ? "block" : "none";

}

function showDivTwin(id_name, name_id) {
    var target = document.getElementById(id_name);
    var t2 = document.getElementById(name_id);
    target.style.display = (target.style.display == "none") ? "block" : "none";
    t2.style.display = (t2.style.display == "none") ? "block" : "none";
}

function DisableDiv(id_name) {
    var target = document.getElementById(id_name);
    target.style.display = "none";

}

function EnableDiv(id_name) {
  
    var target = document.getElementById(id_name);
    target.style.display = "block";

}

