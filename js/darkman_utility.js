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
    $("<div id=\"darkman_pop\" ><div class=\"box_alert_head\"></div><div class=\"box_alert_body\">" + data + "</div><div class=\"box_alert_bottom\"><input type=\"button\" value=\"Ok\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\"width:80px\" /></div></div>").prependTo('body');
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
    $('#fade').css({ 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 

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

    //Fade in Background
    $('body').prepend('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
    $('#fade').css({ 'filter': 'alpha(opacity=80)', 'margin-top': -popMargTop - 15, 'margin-left': -popMargLeft - 15, 'width': Number(fade), 'height': Number(fadeHeight) }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 

    return false;


}

function DarkmanPopUp_Close() {
    $('#fade , #darkman_pop').fadeOut(function () {
        $('#fade, #darkman_pop').remove();  //fade them both out
    });
    return false;
}