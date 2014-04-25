// JavaScript Document
$.ajaxSetup({
    // Disable caching of AJAX responses
    cache: false
});

var offsetx = 12;
var offsety = 8;
function tooltip(tip) {
    if (!document.getElementById('tooltip')) newelement('tooltip');
    var lixlpixel_tooltip = document.getElementById('tooltip');
    lixlpixel_tooltip.innerHTML = tip;
    lixlpixel_tooltip.style.display = 'block';
    
    document.onmousemove = getmouseposition;
}
function newelement(newid) {
    if (document.createElement) {
        var el = document.createElement('div');
        el.id = newid;
        with (el.style) {
            display = 'none';
            position = 'absolute';
        }
        el.innerHTML = '&nbsp;';
        document.body.appendChild(el);
    }
}
var ie5 = (document.getElementById && document.all);
var ns6 = (document.getElementById && !document.all);
var ua = navigator.userAgent.toLowerCase();
var isapple = (ua.indexOf('applewebkit') != -1 ? 1 : 0);

function getmouseposition(e) {
    if (document.getElementById) {
        var iebody = (document.compatMode &&
        	document.compatMode != 'BackCompat') ?
        		document.documentElement : document.body;
        pagex = (isapple == 1 ? 0 : (ie5) ? iebody.scrollLeft : window.pageXOffset);
        pagey = (isapple == 1 ? 0 : (ie5) ? iebody.scrollTop : window.pageYOffset);
        mousex = (ie5) ? event.x : (ns6) ? clientX = e.clientX : false;
        mousey = (ie5) ? event.y : (ns6) ? clientY = e.clientY : false;
        var posx = 0;
        var posy = 0;
        if (!e)
            e = window.event;
        if (e.pageX || e.pageY) {
            posx = e.pageX;
            posy = e.pageY;
        }
        else if (e.clientX || e.clientY) {
            posx = e.clientX + document.body.scrollLeft
+ document.documentElement.scrollLeft;
            posy = e.clientY + document.body.scrollTop
+ document.documentElement.scrollTop;
        }

        var lixlpixel_tooltip = document.getElementById('tooltip');
//        lixlpixel_tooltip.innerHTML = posx + " yyyy" + posy + "|" + mousex + 'ttt' + mousey;
        lixlpixel_tooltip.style.left = (posx + pagex + offsetx) + 'px';
        lixlpixel_tooltip.style.top = (posy + pagey + offsety) + 'px';
    }
}

function exit() {
    document.getElementById('tooltip').style.display = 'none';
}



//var posx = 0;
//var posy = 0;
//if (!e)
//    e = window.event;
//if (e.pageX || e.pageY) {
//    posx = e.pageX;
//    posy = e.pageY;
//}
//else if (e.clientX || e.clientY) {
//    posx = e.clientX + document.body.scrollLeft
//+ document.documentElement.scrollLeft;
//    posy = e.clientY + document.body.scrollTop
//+ document.documentElement.scrollTop;
//}

//var tempX = 0
//var tempY = 0
// 
//// Main function to retrieve mouse x-y pos.s
//function getMouseXY(e) {
//    if (IE) { // grab the x-y pos.s if browser is IE
//        tempX = event.clientX + document.body.scrollLeft
//        tempY = event.clientY + document.body.scrollTop
//    } else {  // grab the x-y pos.s if browser is NS
//        tempX = e.pageX
//        tempY = e.pageY
//    }

//    var lixlpixel_tooltip = document.getElementById('tooltip');
//    lixlpixel_tooltip.style.left = (tempX + pagex + offsetx) + 'px';
//    lixlpixel_tooltip.style.top = (tempY + pagey + offsety) + 'px';
//}