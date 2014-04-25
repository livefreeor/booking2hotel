$.ajaxSetup({
    // Disable caching of AJAX responses
    cache: false,
    timeout: 180000
});

function makeid() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

Number.prototype.formatMoney = function (c, d, t) {
    var n = this, c = isNaN(c = Math.abs(c)) ? 2 : c, d = d == undefined ? "," : d, t = t == undefined ? "." : t, s = n < 0 ? "-" : "", i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};


function post_to_url(path, params, method) {
    // cal function 
   // post_to_url('http://www.hotels2thailand.com/thailand-hotels-tell-thankyou.aspx', { 'pd': qProductId, 'm': data });

    method = method || "post"; // Set method to post by default, if not specified.

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);
    form.setAttribute("target", "_blank");

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

function DarkmanPopUpAlert(w, data) {
    $("<div id=\"darkman_pop\" style=\"position:fixed\" ><div class=\"box_alert_head\">Hotels2thailand Notification.</div><div class=\"box_alert_body\">" + data + "</div><div class=\"box_alert_bottom\"><input type=\"button\" value=\"Ok\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\"width:80px\" /></div></div>").prependTo('body');
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

    //Fade in Background
    $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
    $('#fade').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 
   
    return false;
}

function FadeOptimize() {

    var popMargTop = $('#darkman_pop').offset().top;
    var popMargLeft = $('#darkman_pop').offset().left;
    var fade = $('#darkman_pop').width() - 10;
    var fadeHeight = $('#darkman_pop').height() - 10;


    $('#fade').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 50, 'margin-left': -popMargLeft + 130, 'width': Number(fade), 'height': Number(fadeHeight) });  //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading
    return false;
}

function DarkmanPopUpComfirm(w, data, fn) {
    
    $("<div id=\"darkman_pop\" style=\"position:fixed\" ><div class=\"box_alert_head\">Hotels2thailand Notification.</div><div class=\"box_alert_body\">" + data + "</div><div class=\"box_alert_bottom\"><input type=\"button\" value=\"Ok\" onclick='DarkmanPopUp_Close();" + fn + "' class=\"btStyle\" style=\"width:110px\" />&nbsp;<input type=\"button\" value=\"cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\"width:80px\" /></div></div>").prependTo('body');
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

    //Fade in Background
    $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
    $('#fade').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 

    return false;
}

function DarkmanPopUp(w, data) {
    $("<div id=\"darkman_pop\" style=\"border:15px solid #333333;position:fixed\" >" + data + "</div>").prependTo('body');
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

    //Fade in Background
//    $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
//    $('#fade').css({'opacity':'0.8', 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies
    $("#darkman_pop").draggable({ scroll: true });
    
    return false;


}


function DarkmanPopUp_front(w, data) {

    $("<div id=\"darkman_pop\" style=\"position:fixed\" >" + data + "</div>").prependTo('body');
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

    //Fade in Background
    $('body').prepend('<div id="fade2"></div>'); //Add the fade layer to bottom of the body tag.
    $('#fade2').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
        //$('#fade2').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)' }).fadeIn();  //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies
    //$("#darkman_pop").draggable({ scroll: true });

    return false;


}
function DarkmanPopUp_Close_front() {
    $('#fade2 , #darkman_pop').fadeOut(function () {
        $('#fade2, #darkman_pop').remove();  //fade them both out
    });
    return false;
}
function DarkmanPopUp_Close() {
    $('#fade , #darkman_pop').fadeOut(function () {
        $('#fade, #darkman_pop').remove();  //fade them both out
    });
    return false;
}

