$.ajaxSetup({
    // Disable caching of AJAX responses
    cache: false
    //timeout:180000
});



function DarkmanProgress(id) {

    if (!$("#progress_block_main_" + id).length) {
        $("<p class=\"progress_block_main\" id=\"progress_block_main_" + id + "\"><img class=\"img_progress\" src=\"../../images_extra/preloader_blue.gif\" alt=\"Progress\" /></p>").insertBefore("#" + id + "").ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).remove();
        });
    }
}
function setCookieASPNET(c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) +
    ((expiredays == null) ? "" : ";expires=" + exdate.toUTCString());
}
function setCookie(name, value, expires, path, domain, secure) {
    var expDate = new Date();
    expDate.setDate(expDate.getDate() + expires);
    document.cookie = name + "=" + value +
    ((expires) ? ";	expires=" + expDate.toUTCString() : "") +
	((path) ? "; path=" + path : "") +
	((domain) ? "; domain=" + domain : "") +
	((secure) ? "; secure" : "");
    // ((expires) ? ";	expires=" + expires.toGMTString() : "")	+
}

function getCookieASPNET(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}

function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    } else {
        begin += 2;
    }
    var end = document.cookie.indexOf(";", begin);
    if (end == -1) {
        end = dc.length;
    }
    return unescape(dc.substring(begin + prefix.length, end));
}

function deleteCookie(name, path, domain) {
    if (getCookie(name)) {
        document.cookie = name + "=" +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
}


function GetQuerystringProductAndSupplierForBluehouseManage(option) {
    var qProductId = GetValueQueryString("pid");
    var qSupplierId = GetValueQueryString("supid");
    var queryREsult = "";
    var key = "";
    if (option == "append") {
        key = "&";
    } else if (option == "new") {
        key = "?";
    }
    if (qProductId != "" && qSupplierId != "") {
        queryREsult = key + "pid=" + qProductId + "&supid=" + qSupplierId;
    }

    return queryREsult;
}


/**
* jQuery.fn.sortElements
* --------------
* @param Function comparator:
*   Exactly the same behaviour as [1,2,3].sort(comparator)
*   
* @param Function getSortable
*   A function that should return the element that is
*   to be sorted. The comparator will run on the
*   current collection, but you may want the actual
*   resulting sort to occur on a parent or another
*   associated element.
*   
*   E.g. $('td').sortElements(comparator, function(){
*      return this.parentNode; 
*   })
*   
*   The <td>'s parent (<tr>) will be sorted instead
*   of the <td> itself.

"default" = function(a, b){
    return $(a).text() &gt; $(b).text() ? 1 : -1;
}
 
"numeric" = function(a, b){
    return parseInt($(a).text(), 10) &gt; parseInt($(b).text(), 10) ? 1 : -1;
}
 
"auto" = function(a, b){
    a = $(a).text();
    b = $(b).text();
 
    return (
        isNaN(a) || isNaN(b) ?
        a &gt; b : +a &gt; +b
    ) ?
        inverse ? -1 : 1 :
        inverse ? 1 : -1;
}*/
jQuery.fn.sortElements = (function () {

    var sort = [].sort;

    return function (comparator, getSortable) {

        getSortable = getSortable || function () { return this; };

        var placements = this.map(function () {

            var sortElement = getSortable.call(this),
                parentNode = sortElement.parentNode,

            // Since the element itself will change position, we have
            // to have some way of storing its original position in
            // the DOM. The easiest way is to have a 'flag' node:
                nextSibling = parentNode.insertBefore(
                    document.createTextNode(''),
                    sortElement.nextSibling
                );

            return function () {

                if (parentNode === this) {
                    throw new Error(
                        "You can't sort elements if any one is a descendant of another."
                    );
                }

                // Insert before flag:
                parentNode.insertBefore(this, nextSibling);
                // Remove flag:
                parentNode.removeChild(nextSibling);

            };

        });

        return sort.call(this, comparator).each(function (i) {
            placements[i].call(getSortable.call(this));
        });

    };

})();


function SelDataBind(num) {
    var Option = "";
    for (i = 1; i <= num; i++) {
        Option = Option + "<option value=\"" + i + "\">" + i + "</option>";
    }
    return Option;
}

// JavaScript Document

//Tooltip
this.tooltip = function () {

    xOffset = 10;
    yOffset = 20;


    $("a.tooltip").hover(function (e) {

        //ptext=$("#policyContent_"+this.id).html();
        ptext = $(this).find('.tooltip_content').html();

        tagContent = $(this).find('.tooltip_content').get(0).nodeName;
        if (tagContent == "SPAN" || tagContent == "DIV" || tagContent == "P" || tagContent == "IMG") {


            if (tagContent != "IMG") {
                $("body").append("<div id='tooltip'>" + ptext + "</div>");
                //$("#tooltip").css("width","400px");
            } else {
                $("body").append("<div id='tooltip'><img src='" + $(this).find('.tooltip_content').attr("src") + "'/></div>");
            }

            
            $("#tooltip")
				.css("top", (e.pageY - xOffset) + "px")
				.css("left", (e.pageX + yOffset) + "px")
				.fadeIn(100);
    
        }

    },
	function () {
	    $("#tooltip").remove();
	});

    $("a.tooltip").mousemove(function (e) {

        var mousex = e.pageX + 20; //Get X coodrinates
        var mousey = e.pageY + 20; //Get Y coordinates
        var tipWidth = $("#tooltip").width(); //Find width of tooltip
        var tipHeight = $("#tooltip").height(); //Find height of tooltip

        //Distance of element from the right edge of viewport
        var tipVisX = $(window).width() - (mousex + tipWidth);
        //Distance of element from the bottom of viewport
        var tipVisY = $(window).height() - (mousey + tipHeight);

        if (tipVisX < 20) { //If tooltip exceeds the X coordinate of viewport
            mousex = e.pageX - tipWidth - 20;
        } if (tipVisY < 20) { //If tooltip exceeds the Y coordinate of viewport
            mousey = e.pageY - tipHeight - 20;
        }
        $("#tooltip").css({ top: mousey, left: mousex });
    });

};

this.imgFloat = function () {

    xOffset = 10;
    yOffset = 20;
    $(".imgFloat").each(function () {
        $(this).hover(function () {
            $("body").append("<div id='imgFloat'></div>");
            var imgBig = $(this).attr('href');
            $("#imgFloat")
			.html("<img src='" + imgBig + "' class='preload'>")
			.css("top", (e.pageY) + "px")
			.css("left", (e.pageX) + "px")
			.fadeIn(100);
        },
		function () {
		    $("#imgFloat").remove();
		});

        $(this).mousemove(function (e) {
            $("#imgFloat")
			.css("top", (e.pageY - xOffset) + "px")
			.css("left", (e.pageX + yOffset) + "px");
        });
    });

};

function makeid() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

function post_to_url(path, params, method) {
    // cal function 
   // post_to_url('http://www.hotels2thailand.com/thailand-hotels-tell-thankyou.aspx', { 'pd': qProductId, 'm': data });

    method = method || "post"; // Set method to post by default, if not specified.

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);

    for (var key in params) {
        var hiddenField = document.createElement("input");
        hiddenField.setAttribute("type", "hidden");
        hiddenField.setAttribute("name", key);
        hiddenField.setAttribute("value", params[key]);

        form.appendChild(hiddenField);
    }

    document.body.appendChild(form);    // Not entirely sure if this is necessary
    form.submit();
}


function htmlEncode(value) {
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}

function GetValueQueryString(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}


function GetQueryCount(){
    var Url = $("window").context.URL.toString();
    var aa = Url.split("?")[1];
    if (aa != null) {
        var arrs = aa.split('&');
        var count = arrs.length;
        return count;
    }
}

// Get QuerySring
function GetQueryStringAll() {
    var Url = $("window").context.URL.toString();
    var hash = window.location.hash;
    var QueryString = null;
    if(!hash)
    var QueryString = Url.split("?")[1];
    

    return QueryString;
}

function GetUrlPathName() {
    return window.location.pathname;
}

function getHashVarsappend() {
    var vars = [], hash;
    var hashes = window.location.hash.slice(window.location.hash.indexOf('&') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function getHashVars() {
    var vars = [], hash;
    var hashes = window.location.hash.slice(window.location.hash.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function langswitch(langid) {
    $("#langswitch #imgLang").removeAttr("src");
    var srcEng = "../../images/flagENG.png";
    var srcTHai = "../../images/flagTH.png";
    if (langid == "1") {
        $("#lblCountry").html("ENG");
        $("#langswitch #imgLang").attr("src", srcEng);
    }
    else if (langid == "2") {
        $("#lblCountry").html("TH");
        $("#langswitch #imgLang").attr("src", srcTHai);
    }

}
function BindCallback(callback, objJS) {

    if (callback && typeof (callback) === "function") {
        callback(objJS);
    }
}
function DarkmanPopUpAlertFn_Callback(w, data, callback, objJS) {
    $("<div id=\"darkman_pop_alert\" ><div class=\"box_alert_head\">Notification.</div><div class=\"box_alert_body\">" + data + "</div><div class=\"box_alert_bottom\"><input type=\"button\" value=\"Ok\" id=\"darkman_alert_btn\" class=\"btStyleWhite\" style=\"width:80px\" /></div></div>").prependTo('body');
    var popWidth = w;
    var fade = w - 10;
    //Fade in the Popup and add close button
    $('#darkman_pop_alert').fadeIn().css({ 'width': Number(popWidth) });

    //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css
    var popMargTop = ($('#darkman_pop_alert').height() + 80) / 2;
    var popMargLeft = ($('#darkman_pop_alert').width() + 80) / 2;

    // var fadeHeight = $('#darkman_pop').height() + 20;
    var fadeHeight = $('#darkman_pop').height() - 10;

    //Apply Margin to Popup
    $('#darkman_pop_alert').css({
        'margin-top': -popMargTop,
        'margin-left': -popMargLeft
    });

    $("#darkman_alert_btn").click(function () {
        
        DarkmanPopUp_Close_alert();

        if (callback && typeof (callback) === "function") {
            callback(objJS);
        }
    });

    return false;
}

function DarkmanPopUpAlertFn(w, data, fn) {

    $("<div id=\"darkman_pop_alert\" ><div class=\"box_alert_head\">Notification.</div><div class=\"box_alert_body\">" + data + "</div><div class=\"box_alert_bottom\"><input type=\"button\" value=\"Ok\" onclick=\"DarkmanPopUp_Close_alert();" + fn + "\" class=\"btStyleWhite\" style=\"width:80px\" /></div></div>").prependTo('body');
    var popWidth = w;
    var fade = w - 10;
    //Fade in the Popup and add close button
    $('#darkman_pop_alert').fadeIn().css({ 'width': Number(popWidth) });

    //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css
    var popMargTop = ($('#darkman_pop_alert').height() + 80) / 2;
    var popMargLeft = ($('#darkman_pop_alert').width() + 80) / 2;

    // var fadeHeight = $('#darkman_pop').height() + 20;
    var fadeHeight = $('#darkman_pop').height() - 10;

    //Apply Margin to Popup
    $('#darkman_pop_alert').css({
        'margin-top': -popMargTop,
        'margin-left': -popMargLeft
    });

    

    return false;
}

function DarkmanPopUpAlert(w, data) {
    $("<div id=\"darkman_pop_alert\" ><div class=\"box_alert_head\">Notification.</div><div class=\"box_alert_body\">" + data + "</div><div class=\"box_alert_bottom\"><input type=\"button\" value=\"Ok\" onclick=\"DarkmanPopUp_Close_alert();\" class=\"btStyleWhite\" style=\"width:80px\" /></div></div>").prependTo('body');
    var popWidth = w;
    var fade = w - 10;
    //Fade in the Popup and add close button
    $('#darkman_pop_alert').fadeIn().css({ 'width': Number(popWidth) });

    //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css
    var popMargTop = ($('#darkman_pop_alert').height() + 80) / 2;
    var popMargLeft = ($('#darkman_pop_alert').width() + 80) / 2;

    // var fadeHeight = $('#darkman_pop').height() + 20;
    var fadeHeight = $('#darkman_pop').height() - 10;

    //Apply Margin to Popup
    $('#darkman_pop_alert').css({
        'margin-top': -popMargTop,
        'margin-left': -popMargLeft
    });

//    //Fade in Background
//    $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
//    $('#fade').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 

    return false;
}


function DarkmanPopUp_Close_alert() {
    $('#fade , #darkman_pop_alert').fadeOut(function () {
        $('#fade, #darkman_pop_alert').remove();  //fade them both out
    });
    return false;
}





function DarkmanPopUpComfirmCallback(w, data, callback, objJS) {
    
    
    $("<div id=\"darkman_pop\" ><div class=\"box_alert_head\">Notification.</div><div class=\"box_alert_body\">" + data + "</div><div class=\"box_alert_bottom\"><input type=\"button\" value=\"Yes\" id=\"darkman_confirm_btn\"  class=\"btStyle\" style=\"width:110px\" />&nbsp;<input type=\"button\" value=\"No\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\"width:80px\" /></div></div>").prependTo('body');
        var popWidth = w;
        var fade = w - 10;
        //Fade in the Popup and add close button
        $('#darkman_pop').fadeIn().css({ 'width': Number(popWidth) });

        //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css
        var popMargTop = ($('#darkman_pop').height() + 80) / 2;
        var popMargLeft = ($('#darkman_pop').width() + 80) / 2;

        // var fadeHeight = $('#darkman_pop').height() + 20;
        var fadeHeight = $('#darkman_pop').height() - 10;

        //Apply Margin to Popup
        $('#darkman_pop').css({
            'margin-top': -popMargTop,
            'margin-left': -popMargLeft
        });

        //    //Fade in Background
        //    $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
        //    $('#fade').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 
    
        $("#darkman_confirm_btn").click(function () {

            DarkmanPopUp_Close();

            if (callback && typeof (callback) === "function") {
                callback(objJS);
            }
        });
    return false;
}

function DarkmanPopUpComfirm(w, data, fn) {
    $("<div id=\"darkman_pop\" ><div class=\"box_alert_head\">Notification.</div><div class=\"box_alert_body\">" + data + "</div><div class=\"box_alert_bottom\"><input type=\"button\" value=\"Yes\" onclick='DarkmanPopUp_Close();" + fn + "' class=\"btStyle\" style=\"width:110px\" />&nbsp;<input type=\"button\" value=\"No\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\"width:80px\" /></div></div>").prependTo('body');
    var popWidth = w;
    var fade = w - 10;
    //Fade in the Popup and add close button
    $('#darkman_pop').fadeIn().css({ 'width': Number(popWidth) });

    //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css
    var popMargTop = ($('#darkman_pop').height() + 80) / 2;
    var popMargLeft = ($('#darkman_pop').width() + 80) / 2;

    // var fadeHeight = $('#darkman_pop').height() + 20;
    var fadeHeight = $('#darkman_pop').height() - 10;

    //Apply Margin to Popup
    $('#darkman_pop').css({
        'margin-top': -popMargTop,
        'margin-left': -popMargLeft
    });

//    //Fade in Background
//    $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
//    $('#fade').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 

    return false;
}



function DarkmanPopUp(w, data) {
    $("<div id=\"darkman_pop\" >" + data + "</div>").prependTo('body');
    var popWidth = w;
    var fade = w - 10;
    //Fade in the Popup and add close button
    $('#darkman_pop').fadeIn().css({ 'width': Number(popWidth) });

    //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css
    var popMargTop = ($('#darkman_pop').height() + 80) / 2;
    var popMargLeft = ($('#darkman_pop').width() + 80) / 2;

    // var fadeHeight = $('#darkman_pop').height() + 20;
    var fadeHeight = $('#darkman_pop').height() - 10;

    //Apply Margin to Popup
    $('#darkman_pop').css({
        'margin-top': -popMargTop,
        'margin-left': -popMargLeft
    });

    $("#darkman_pop").draggable({ scroll: true });
//    //Fade in Background
//    $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
//    $('#fade').css({'opacity':'0.8', 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 

    return false;

}

function DarkmanPopUp_Close() {
    $('#fade , #darkman_pop').fadeOut(function () {
        $('#fade, #darkman_pop').remove();  //fade them both out
    });
    return false;
}

//Valid
function ValidateAlert(id, text, position) {
    var Y_top = $("#" + id).offset().top + 5;
    var X_left = $("#" + id).offset().left;


    var optionwidth = 0;
    var optionheight = 0;

    if (position == "left") {
        optionwidth = $("#" + id).width() + 10;
        optionheight = $("#" + id).height();
        //alert(optionheight);
    }
    
    else {
        optionwidth = ($("#" + id).width() + 10) - $("#" + id).width();
        optionheight = $("#" + id).height() - $("#" + id).height() - $("#" + id).height();
    }
    

    if (!$("#valid_alert_" + id).length) {
        $("#" + id).css("background-color", "#f2ebbd");
        $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
        $("#valid_alert_" + id).css({ "top": (Y_top - optionheight) + "px", "left": (X_left + optionwidth) + "px" });
        $("#valid_alert_" + id).fadeIn('fast');
    }
}


function ValidateAlertClose(id) {
    $("#" + id).css("background-color", "#fbfbf9");
    $("#valid_alert_" + id).fadeOut('fast', function () {

        $(this).remove();
    });
}


function ValidateOptionMethod(id, optionMethod) {
    var Y_top = $("#" + id).offset().top + $("#" + id).outerHeight();
    var X_left = $("#" + id).offset().left;
    var result = true;
    var txtAlert = "";
    switch (optionMethod) {
        case "required":
            txtAlert = "*Please fill your detail."
            if ($("#" + id).val() == "") {
                if (!$("#valid_alert_require" + id).length) {
                    $("#" + id).css("background-color", "#ffebe8");
                    $("body").append("<label id=\"valid_alert_require" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                    $("#valid_alert_require" + id).css({ "top": Y_top + "px", "left": X_left + "px" });
                    $("#valid_alert_require" + id).fadeIn('fast');
                }

                result = false;
            } else {
                $("#" + id).css("background-color", "#ffffff");
                $("#valid_alert_require" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }

            $("#" + id).blur(function () {

                if ($("#" + id).val() == "") {
                    if (!$("#valid_alert_require" + id).length) {
                        $("#" + id).css("background-color", "#ffebe8");
                        $("body").append("<label id=\"valid_alert_require" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                        $("#valid_alert_require" + id).css({ "top": Y_top + "px", "left": X_left + "px" });
                        $("#valid_alert_require" + id).fadeIn('fast');
                    }

                } else {
                    $("#" + id).css("background-color", "#f9f9fb");
                    $("#valid_alert_require" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }
            });

            break;
        case "number":
            txtAlert = "*Requires numeric information only."
            var inputval = $("#" + id).val();

            var regExpr = new RegExp("^[0-9][0-9]*$");

            if (!regExpr.test(inputval)) {
                if (!$("#valid_alert_require" + id).length) {
                    $("#" + id).css("background-color", "#ffebe8");
                    $("body").append("<label id=\"valid_alert_require" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                    $("#valid_alert_require" + id).css({ "top": Y_top + "px", "left": X_left + "px" });
                    $("#valid_alert_require" + id).fadeIn('fast');
                }
                result = false;
            } else {
                $("#" + id).css("background-color", "#faffbd");
                $("#valid_alert_require" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }

            $("#" + id).keyup(function () {
                var inputval = $("#" + id).val();

                var regExpr = new RegExp("^[0-9][0-9]*$");

                //alert(regExpr.test(inputval));

                if (!regExpr.test(inputval)) {
                    if (!$("#valid_alert_require" + id).length) {
                        $("#" + id).css("background-color", "#ffebe8");
                        $("body").append("<label id=\"valid_alert_require" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                        $("#valid_alert_require" + id).css({ "top": Y_top + "px", "left": X_left + "px" });
                        $("#valid_alert_require" + id).fadeIn('fast');
                    }
                } else {
                    //alert(inputval);
                    $("#" + id).css("background-color", "#faffbd");
                    $("#valid_alert_require" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }
            });
            break;
        case "number0":
            txtAlert = "*You can not add rate \"0\"(zero)."
            var inputval = $("#" + id).val();

            var regExpr = new RegExp("^[0-9][0-9]*$");

            if (!regExpr.test(inputval) || parseInt(inputval) == 0 ) {
                if (!$("#valid_alert_require" + id).length) {
                    $("#" + id).css("background-color", "#ffebe8");
                    $("body").append("<label id=\"valid_alert_require" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                    $("#valid_alert_require" + id).css({ "top": Y_top + "px", "left": X_left + "px" });
                    $("#valid_alert_require" + id).fadeIn('fast');
                }
                result = false;
            } else {
                $("#" + id).css("background-color", "#faffbd");
                $("#valid_alert_require" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }

            $("#" + id).keyup(function () {
                var inputval = $("#" + id).val();

                var regExpr = new RegExp("^[0-9][0-9]*$");

                //alert(regExpr.test(inputval));

                if (!regExpr.test(inputval) || parseInt(inputval) == 0) {
                    if (!$("#valid_alert_require" + id).length) {
                        $("#" + id).css("background-color", "#ffebe8");
                        $("body").append("<label id=\"valid_alert_require" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                        $("#valid_alert_require" + id).css({ "top": Y_top + "px", "left": X_left + "px" });
                        $("#valid_alert_require" + id).fadeIn('fast');
                    }
                } else {
                    //alert(inputval);
                    $("#" + id).css("background-color", "#faffbd");
                    $("#valid_alert_require" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }
            });
            break;
        case "number0comma":
            txtAlert = "***"
            var inputval = $("#" + id).val();
            var widthVal = $("#" + id).width();

            var height = $("#" + id).height();
            var regExpr = new RegExp("^[0-9,][0-9,]*$");

            if (!regExpr.test(inputval) || parseInt(inputval) == 0) {
                if (!$("#valid_alert_require" + id).length) {
                    $("#" + id).css("background-color", "#ffebe8");
                    $("body").append("<label id=\"valid_alert_require" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                    $("#valid_alert_require" + id).css({ "top": (Y_top - height -5) + "px", "left": (X_left + widthVal + 10) + "px" });
                    $("#valid_alert_require" + id).fadeIn('fast');
                }
                result = false;
            } else {
                $("#" + id).css("background-color", "#faffbd");
                $("#valid_alert_require" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }

            $("#" + id).keyup(function () {
                var inputval = $("#" + id).val();

                var regExpr = new RegExp("^[0-9,][0-9,]*$");

                //alert(regExpr.test(inputval));

                if (!regExpr.test(inputval) || parseInt(inputval) == 0) {
                    if (!$("#valid_alert_require" + id).length) {
                        $("#" + id).css("background-color", "#ffebe8");
                        $("body").append("<label id=\"valid_alert_require" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + txtAlert + "</label>");
                        $("#valid_alert_require" + id).css({ "top": (Y_top - height - 5) + "px", "left": (X_left + widthVal + 10) + "px" });
                        $("#valid_alert_require" + id).fadeIn('fast');
                    }
                } else {
                    //alert(inputval);
                    $("#" + id).css("background-color", "#faffbd");
                    $("#valid_alert_require" + id).fadeOut('fast', function () {

                        $(this).remove();
                    });
                }
            });
            break;
        case "email":
            break;
    }

    return result;

}


function CheckPeriod_Overlap(callingPairId, dateForm, dateTo, checked, hiddenDatefrom, hiddenDateTo) {
    var result = 0;


    if (daydiff(parseDate(dateForm), parseDate(dateTo)) < 0) {
        result = result + 1;
    }

    $("input[name='" + checked + "']:checked").each(function () {
        var checkid = $(this).val();
        if (checkid != callingPairId) {

            var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
            var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();

             

            if (parseDate(dateForm) >= parseDate(DateStart) && parseDate(dateForm) <= parseDate(DateEnd)) {
                result = result + 1;
            }

            if (parseDate(dateTo) >= parseDate(DateStart) && parseDate(dateTo) <= parseDate(DateEnd)) {
                result = result + 1;
            }
        }
        


    });


    return result;
}

function CheckPeriod(dateForm, dateTo, checked, hiddenDatefrom, hiddenDateTo) {
    var result = 0;
    

    if (daydiff(parseDate(dateForm), parseDate(dateTo)) < 0) {
        result = result + 1;
    }

    $("input[name='" + checked + "']:checked").each(function () {

        var checkid = $(this).val();
        var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
        var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();

       

        if (parseDate(dateForm) >= parseDate(DateStart) && parseDate(dateForm) <= parseDate(DateEnd)) {
            result = result + 1;
        }

        if (parseDate(dateTo) >= parseDate(DateStart) && parseDate(dateTo) <= parseDate(DateEnd)) {
            result = result + 1;
        }


    });

    
    return result;
}



function PeriodValidCheck_overlap(id, CallingID, dateFormID, dateToID, position, checked, hiddenDatefrom, hiddenDateTo, option) {


    var ValDefault = {
        extendHeight: -20,
        extendWidth: 10,
        bgAlert: "#ffebe8",
        bgDefault: "#ffffff"
    }


    var opTs = jQuery.extend(ValDefault, option);

    var DateFrom = $("#"+dateFormID).val();
    var DateTo = $("#"+dateToID).val();

    
    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;

    var callingParent_DateStart = $("#" + dateFormID).parent().parent().stop().find(":text");
    var callingParent_DateEnd = $("#" + dateToID).parent().parent().stop().find(":text");

    
    var result = 0;
    var text = "*Please recheck the period you are filling in. It has to be different period.";
    var optionwidth = 0;
    var optionheight = 0;
    if (position == "left") {

        optionwidth = $("#" + id).width() + opTs.extendWidth;
        optionheight = $("#" + id).height();

        //alert(optionheight);
    }
    else {
        optionwidth = ($("#" + id).width() + opTs.extendWidth) - $("#" + id).width();
    }

    optionheight = $("#" + id).height() + opTs.extendHeight;

    var result = CheckPeriod_Overlap(CallingID, DateFrom, DateTo, checked, hiddenDatefrom, hiddenDateTo);

    if (result != 0) {
        if (!$("#valid_alert_" + id).length) {

            callingParent_DateStart.css("background-color", opTs.bgAlert);
            callingParent_DateEnd.css("background-color", opTs.bgAlert);
            //$("#" + id).css("background-color", "#ffebe8");
            $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }
    } else {
        callingParent_DateStart.css("background-color", opTs.bgDefault);
        callingParent_DateEnd.css("background-color", opTs.bgDefault);
        //$("#" + id).css("background-color", "#f7f7f7");
        $("#valid_alert_" + id).fadeOut('fast', function () {

            $(this).remove();
        });
    }


    $("#" + callingParent_DateStart.attr("id")).change(function () {
        var result = 0;
        var DateFrom = $("#" + dateFormID).val();
        var DateTo = $("#" + dateToID).val();
            var result = CheckPeriod_Overlap(CallingID, DateFrom, DateTo, checked, hiddenDatefrom, hiddenDateTo);

            if (result != 0) {
                if (!$("#valid_alert_" + id).length) {

                    callingParent_DateStart.css("background-color", opTs.bgAlert);
                    callingParent_DateEnd.css("background-color", opTs.bgAlert);
                    //$("#" + id).css("background-color", "#ffebe8");
                    $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                    $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                    $("#valid_alert_" + id).fadeIn('fast');
                }
            } else {
                callingParent_DateStart.css("background-color", opTs.bgDefault);
                callingParent_DateEnd.css("background-color", opTs.bgDefault);
                //$("#" + id).css("background-color", "#f7f7f7");
                $("#valid_alert_" + id).fadeOut('fast', function () {

                    $(this).remove();
                });
            }
            
    });


    $("#" + callingParent_DateEnd.attr("id")).change(function () {
        var result = 0;
        var DateFrom = $("#" + dateFormID).val();
        var DateTo = $("#" + dateToID).val();
        var result = CheckPeriod_Overlap(CallingID, DateFrom, DateTo, checked, hiddenDatefrom, hiddenDateTo);

        if (result != 0) {
            if (!$("#valid_alert_" + id).length) {

                callingParent_DateStart.css("background-color", opTs.bgAlert);
                callingParent_DateEnd.css("background-color", opTs.bgAlert);
                //$("#" + id).css("background-color", "#ffebe8");
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {
            callingParent_DateStart.css("background-color", opTs.bgDefault);
            callingParent_DateEnd.css("background-color", opTs.bgDefault);
            //$("#" + id).css("background-color", "#f7f7f7");
            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
    });
    

    return result;
}


function ClearAlert(id, bgcolor) {


     $("body label[id^='valid_alert_']").fadeOut('fast', function () {
            $(this).remove();
      });
     

     $("#" + id).css("background-color", bgcolor);
}


function PeriodValidCheck(id, dateFormID, dateToID, position, checked, hiddenDatefrom, hiddenDateTo,option) {
  
    var ValDefault = {
        extendHeight: 0,
        extendWidth: 10,
        bgAlert: "#ffebe8",
        bgDefault: "#f7f7f7"
    }
    
    var opTs = jQuery.extend(ValDefault, option);
    
    var DateFrom = $("#hd_" + dateFormID).val();
    var DateTo = $("#hd_" + dateToID).val();

    //alert(DateFrom + "++" + DateTo);

    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;
    var result = 0;
    var text = "*Please recheck the period you are filling in. It has to be different period.";
    var optionwidth = 0;
    var optionheight = 0;
    if (position == "left") {
        optionwidth = $("#" + id).width() + opTs.extendWidth;
        optionheight = $("#" + id).height();
        //alert(optionheight);
    }
    else {
        optionwidth = ($("#" + id).width() + opTs.extendWidth) - $("#" + id).width();
    }
    optionheight = $("#" + id).height() +opTs.extendHeight;

    ////check error This here !!!! 
    //alert(opTs.extendHeight);
    
      //  optionheight = optionheight + 
    
    result = CheckPeriod(DateFrom, DateTo, checked, hiddenDatefrom, hiddenDateTo);
    
    if (result != 0) {
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

    
    $("#" + dateFormID).change(function () {
        
        var result = 0;
        var DateFrom = $("#hd_" + dateFormID).val();
        var DateTo = $("#hd_" + dateToID).val();

        if (daydiff(parseDate(DateFrom), parseDate(DateTo)) < 0) {
            result = result + 1;
        }

        $("input[name='" + checked + "']:checked").each(function () {

            var checkid = $(this).val();

            var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
            var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();

            //alert(dateForm + "//" + dateTo);
            if (parseDate(DateFrom) >= parseDate(DateStart) && parseDate(DateFrom) <= parseDate(DateEnd)) {
                result = result + 1;
            }
            if (parseDate(DateTo) >= parseDate(DateStart) && parseDate(DateTo) <= parseDate(DateEnd)) {
                result = result + 1;
            }

        });

       
        if (result != 0) {
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
        //alert(result);
    });

    $("#" + dateToID).change(function () {
       
        var result = 0;
        var DateFrom = $("#hd_" + dateFormID).val();
        var DateTo = $("#hd_" + dateToID).val();

        if (daydiff(parseDate(DateFrom), parseDate(DateTo)) < 0) {
            result = result + 1;
        }

        $("input[name='" + checked + "']:checked").each(function () {

            var checkid = $(this).val();
            var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
            var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();


            if (parseDate(DateFrom) >= parseDate(DateStart) && parseDate(DateFrom) <= parseDate(DateEnd)) {
                result = result + 1;
            }
            if (parseDate(DateTo) >= parseDate(DateStart) && parseDate(DateTo) <= parseDate(DateEnd)) {
                result = result + 1;
            }

        });

        
        if (result != 0) {
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
        //alert(result);
    });
    //        $("#" + dateToID).change(function () {
    //            result = CheckPeriod(DateFrom, DateTo, checked);
    //            alert(result);
    //        });

    return result;
}

function CheckPeriod_single(dateForm, checked, hiddenDatefrom) {
    var result = 0;
    $("input[name='" + checked + "']:checked").each(function () {

        var checkid = $(this).val();
        var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
        //var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();


        var datediff = daydiff(parseDate(dateForm), parseDate(DateStart));
        if (datediff == 0) {
            result = result + 1;
        }

        //        if (parseDate(dateTo) >= parseDate(DateStart) && parseDate(dateTo) <= parseDate(DateEnd)) {
        //            result = result + 1;
        //        }
        //alert(result);


    });


    return result;
}

function PeriodValidCheck_Single(id, dateFormID, position, checked, hiddenDatefrom) {

    var DateFrom = $("#hd_" + dateFormID).val();
    //var DateTo = $("#hd_" + dateToID).val();

    //alert(DateFrom + "++" + DateTo);

    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;
    var result = 0;
    var text = "*Please recheck the date you are filling in. It has to be different date.";
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
    optionheight = $("#" + id).height();

    result = CheckPeriod_single(DateFrom, checked, hiddenDatefrom);
    
    if (result != 0) {
        if (!$("#valid_alert_" + id).length) {
            $("#" + id).css("background-color", "#ffebe8");
            $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }
    } else {

        $("#" + id).css("background-color", "#f7f7f7");
        $("#valid_alert_" + id).fadeOut('fast', function () {

            $(this).remove();
        });
    }

    $("#" + dateFormID).change(function () {
        var result = 0;
        var DateFrom = $("#hd_" + dateFormID).val();
        //var DateTo = $("#hd_" + dateToID).val();

        $("input[name='" + checked + "']:checked").each(function () {

            var checkid = $(this).val();

            var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
            //var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();

            //alert(dateForm + "//" + dateTo);
            

            var datediff = daydiff(parseDate(DateFrom), parseDate(DateStart));
            if (datediff == 0) {
                result = result + 1;
            }
//            if (parseDate(DateTo) >= parseDate(DateStart) && parseDate(DateTo) <= parseDate(DateEnd)) {
//                result = result + 1;
//            }

        });


        if (result != 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#f7f7f7");
            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
        //alert(result);
    });

    //        $("#" + dateToID).change(function () {
    //            result = CheckPeriod(DateFrom, DateTo, checked);
    //            alert(result);
    //        });

    return result;
}

function CheckPeriod_single_Gala(dateForm, checked, hiddenDatefrom, galaForName, hiddenGalaFor) {
    var result = 0;
    $("input[name='" + checked + "']:checked").each(function () {
        var CheckGalaFor = $("input[name='" + galaForName + "']:checked").val();
        var checkid = $(this).val();
        var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
        var GalaFor = $("input[name='" + hiddenGalaFor + checkid + "']").val();
        //var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();

        var GalarforVal = "True";
        if (CheckGalaFor == "1")
            GalarforVal = "False";

        var datediff = daydiff(parseDate(dateForm), parseDate(DateStart));
        
        if (datediff == 0 && GalarforVal == GalaFor) {
            result = result + 1;
        }

        //        if (parseDate(dateTo) >= parseDate(DateStart) && parseDate(dateTo) <= parseDate(DateEnd)) {
        //            result = result + 1;
        //        }
        //alert(result);


    });


    return result;
}

function PeriodValidCheck_Single_Gala(id, dateFormID, position, checked, hiddenDatefrom, galaForName ,hiddenGalaFor) {

    var DateFrom = $("#hd_" + dateFormID).val();
    //var DateTo = $("#hd_" + dateToID).val();

    //alert(DateFrom + "++" + DateTo);

    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;
    var result = 0;
    var text = "*Please recheck the date you are filling in. It has to be different date or condition.";
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
    optionheight = $("#" + id).height();

    result = CheckPeriod_single_Gala(DateFrom, checked, hiddenDatefrom, galaForName, hiddenGalaFor);

    

    if (result != 0) {
        if (!$("#valid_alert_" + id).length) {
            $("#" + id).css("background-color", "#ffebe8");
            $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }
    } else {

        $("#" + id).css("background-color", "#f7f7f7");
        $("#valid_alert_" + id).fadeOut('fast', function () {

            $(this).remove();
        });
    }

    $("#" + dateFormID).change(function () {
        var result = 0;
        var CheckGalaFor = $("input[name='" + galaForName + "']:checked").val();
        var DateFrom = $("#hd_" + dateFormID).val();
        
        //var DateTo = $("#hd_" + dateToID).val();

        $("input[name='" + checked + "']:checked").each(function () {

            var checkid = $(this).val();

            var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
            //var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();
            var GalaFor = $("input[name='" + hiddenGalaFor + checkid + "']").val();
            //alert(dateForm + "//" + dateTo);

            var GalarforVal = "True";
            if (CheckGalaFor == "1")
                GalarforVal = "False";

            var datediff = daydiff(parseDate(DateFrom), parseDate(DateStart));
            if (datediff == 0 && GalarforVal == GalaFor) {
                result = result + 1;
            }
            //            if (parseDate(DateTo) >= parseDate(DateStart) && parseDate(DateTo) <= parseDate(DateEnd)) {
            //                result = result + 1;
            //            }

        });


        if (result != 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#f7f7f7");
            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
        //alert(result);
    });

    

    //var opTs = jQuery.extend(ValDefault, option);

    $("input[name='" + galaForName + "']").click(function () {
        var result = 0;
        var CheckGalaFor = $("input[name='" + galaForName + "']:checked").val();
        var DateFrom = $("#hd_" + dateFormID).val();

        //var DateTo = $("#hd_" + dateToID).val();

        $("input[name='" + checked + "']:checked").each(function () {

            var checkid = $(this).val();

            var DateStart = $("input[name='" + hiddenDatefrom + checkid + "']").val();
            //var DateEnd = $("input[name='" + hiddenDateTo + checkid + "']").val();
            var GalaFor = $("input[name='" + hiddenGalaFor + checkid + "']").val();
            //alert(dateForm + "//" + dateTo);

            var GalarforVal = "True";
            if (CheckGalaFor == "1")
                GalarforVal = "False";

            var datediff = daydiff(parseDate(DateFrom), parseDate(DateStart));
            if (datediff == 0 && GalarforVal == GalaFor) {
                result = result + 1;
            }
            //            if (parseDate(DateTo) >= parseDate(DateStart) && parseDate(DateTo) <= parseDate(DateEnd)) {
            //                result = result + 1;
            //            }

        });


        if (result != 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#f7f7f7");
            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }
        //alert(result);
    });

    //        $("#" + dateToID).change(function () {
    //            result = CheckPeriod(DateFrom, DateTo, checked);
    //            alert(result);
    //        });

    return result;
}

function DateCompareValid(id, datestart, dateend, position) {
    var result = 0;

    var DateBookingstart = $("#hd_" + datestart).val();
    var DateBookingend = $("#hd_" + dateend).val();

    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;
    var result = 0;
    var text = "*Please check new date. Date must be later than date range from.";
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
    optionheight = $("#" + id).height();


    result = daydiff(parseDate(DateBookingstart), parseDate(DateBookingend));


    if (result < 0) {
        if (!$("#valid_alert_" + id).length) {
            $("#" + id).css("background-color", "#ffebe8");
            $("#" + id).next("div").stop().css("background-color", "#ffebe8");
            $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
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

    $("#" + datestart).change(function () {
        var DateBookingstart = $("#hd_" + datestart).val();
        var DateBookingend = $("#hd_" + dateend).val();
        var resultStart = 0;
        resultStart = daydiff(parseDate(DateBookingstart), parseDate(DateBookingend));


        if (resultStart < 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
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

    $("#" + dateend).change(function () {

        var DateBookingstart = $("#hd_" + datestart).val();
        var DateBookingend = $("#hd_" + dateend).val();
        var resultEnd = 0;

        resultEnd = daydiff(parseDate(DateBookingstart), parseDate(DateBookingend));

        if (resultEnd < 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                $("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
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

    return result;
}

function DateCompareValid_ver2(id, datestart, dateend, position) {
    var result = 0;

    var DateBookingstart = $("#hd_" + datestart).val();
    var DateBookingend = $("#hd_" + dateend).val();

    var Y_top = $("#" + id).offset().top + 23;
    var X_left = $("#" + id).offset().left;
    var result = 0;
    var text = "*Please check new date. Date must be later than date range from.";
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
    optionheight = $("#" + id).height();


    result = daydiff(parseDate(DateBookingstart), parseDate(DateBookingend));


    if (result < 0) {
        if (!$("#valid_alert_" + id).length) {
            $("#" + id).css("background-color", "#ffebe8");
            //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
            $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
            $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
            $("#valid_alert_" + id).fadeIn('fast');
        }
    } else {

        $("#" + id).css("background-color", "#f7f7f7");
       // $("#" + id).next("div").stop().css("background-color", "#f7f7f7");

        $("#valid_alert_" + id).fadeOut('fast', function () {

            $(this).remove();
        });
    }

    $("#" + datestart).change(function () {
        var DateBookingstart = $("#hd_" + datestart).val();
        var DateBookingend = $("#hd_" + dateend).val();
        var resultStart = 0;
        resultStart = daydiff(parseDate(DateBookingstart), parseDate(DateBookingend));


        if (resultStart < 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
                $("#valid_alert_" + id).css({ "top": (Y_top + optionheight) + "px", "left": (X_left + optionwidth) + "px" });
                $("#valid_alert_" + id).fadeIn('fast');
            }
        } else {

            $("#" + id).css("background-color", "#f7f7f7");
           // $("#" + id).next("div").stop().css("background-color", "#f7f7f7");
            $("#valid_alert_" + id).fadeOut('fast', function () {

                $(this).remove();
            });
        }

    });

    $("#" + dateend).change(function () {

        var DateBookingstart = $("#hd_" + datestart).val();
        var DateBookingend = $("#hd_" + dateend).val();
        var resultEnd = 0;

        resultEnd = daydiff(parseDate(DateBookingstart), parseDate(DateBookingend));

        if (resultEnd < 0) {
            if (!$("#valid_alert_" + id).length) {
                $("#" + id).css("background-color", "#ffebe8");
                //$("#" + id).next("div").stop().css("background-color", "#ffebe8");
                $("body").append("<label id=\"valid_alert_" + id + "\" style=\"position:absolute;color:red; display:none; font-size:11px; font-family:tahoma; \">" + text + "</label>");
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

    return result;
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}