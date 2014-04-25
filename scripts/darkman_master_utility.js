
function changein(object) {

    object.cells[0].className = "bgstyleleft";

    count = object.cells.length;

    for (i = 1; i <= count - 1; i++) {

        object.cells[i].className = "bgstyle";

    }

    object.cells[count - 1].className = "bgstyleright";

}


function changeout(object) {

    object.cells[0].className = "";

    count = object.cells.length;

    for (i = 1; i <= count - 1; i++) {

        object.cells[i].className = "";

    }

    object.cells[count - 1].className = "";

}

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


function insertattribute() {
    var txtareapage = document.getElementsByTagName("input");
    for (i = 0; i <= txtareapage.length - 1; i++) {

        txtareapage[i].setAttribute('onkeypress', 'disableEnterKey()');

    }

}
function enableEnterKey() {
    if (window.event.keyCode == 13) {

        event.returnValue = false;
        event.cancel = true;

    }
}

function disableEnterKey(e) {
    if (!e) e = window.event
    //        if (!e) {
    //            var obj = window.event.srcElement;
    //        }
    //        else {
    //            var obj = e.target;
    //        }
    //alert(obj.tagName);
    if ((e.target || e.srcElement).tagName == 'TEXTAREA') {
        //alert("TEST");
    } else {
        var key;
        if (window.event)
            key = window.event.keyCode; //IE
        else
            key = e.which; //firefox      

        return (key != 13);
    }

}

//    function disableEnterKey(e) {
//        var key;
//        if (window.event)
//            key = window.event.keyCode; //IE
//        else
//            key = e.which; //firefox      

//        return (key != 13);
//    }

//    function disableEnterKey(e) {
//        var evt = e ? e : window.event;
//        if (evt.keyCode == 13) {
//            //alert("HELLO");
//            evt.returnValue = false;
//            evt.cancel = true;
//        }
//    }

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