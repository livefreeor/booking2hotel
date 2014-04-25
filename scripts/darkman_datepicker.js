function DatepickerDual_smallpic(datefirst, datesec) {
    var today = new Date();
    var defaultDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();
    var todayNext = new Date();
    todayNext.setDate(todayNext.getDate() + 1);

    var defaultDateNext = todayNext.getFullYear() + "-" + (todayNext.getMonth() + 1) + "-" + todayNext.getDate();

    if ($("#" + datefirst).val() == "" && $("#" + datesec).val() == "") {

        $("#" + datefirst).val(defaultDate);
        $("#" + datesec).val(defaultDateNext);
    }


    if ($("#hd_" + datefirst).length) {
        $("#hd_" + datefirst).remove();
        $("#" + datefirst).val("");

    }

    if ($("#hd_" + datesec).length) {
        $("#hd_" + datesec).remove();
        $("#" + datesec).val("");

    }

    $("#" + datefirst).after("<p style='display:none;'><input id='hd_" + datefirst + "' type='hidden' name='hd_" + datefirst + "' /></p>");

    $("#" + datesec).after("<p style='display:none;'><input id='hd_" + datesec + "' type='hidden' name='hd_" + datesec + "' /></p>");

    $("input[id$='" + datefirst + "']").datepicker({
        minDate: 0,
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.gif',
        buttonImageOnly: true,
        showButtonPanel: true,
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + datefirst + "",
        altFormat: "yy-mm-dd"

    });

    // minDate: 1,
    $("input[id$='" + datesec + "']").datepicker({
        minDate: 0,
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.gif',
        buttonImageOnly: true,
        showButtonPanel: true,
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + datesec + "",
        altFormat: "yy-mm-dd"

    });

    setDefaultDateToday(datefirst);
    setDefaultDateToday(datesec);
}
function DatepickerDual_nopic(datefirst, datesec) {
    var today = new Date();
    var defaultDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();
    var todayNext = new Date();
    todayNext.setDate(todayNext.getDate() + 1);

    var defaultDateNext = todayNext.getFullYear() + "-" + (todayNext.getMonth() + 1) + "-" + todayNext.getDate();

    if ($("#" + datefirst).val() == "" && $("#" + datesec).val() == "") {

        $("#" + datefirst).val(defaultDate);
        $("#" + datesec).val(defaultDateNext);
    }

    $("#hd_" + datefirst).remove();
    $("#hd_" + datesec).remove();

    //if ($("#hd_" + datefirst).length) {
    //    $("#hd_" + datefirst).remove();
    //    $("#" + datefirst).val("");

    //}

    //if ($("#hd_" + datesec).length) {
    //    $("#hd_" + datesec).remove();
    //    $("#" + datesec).val("");

    //}

    $("#" + datefirst).after("<p style='display:none;'><input id='hd_" + datefirst + "' type='hidden' name='hd_" + datefirst + "' /></p>");

    $("#" + datesec).after("<p style='display:none;'><input id='hd_" + datesec + "' type='hidden' name='hd_" + datesec + "' /></p>");

    $("input[id$='" + datefirst + "']").datepicker({
        minDate: 0,
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + datefirst + "",
        altFormat: "yy-mm-dd"

    });

    // minDate: 1,
    $("input[id$='" + datesec + "']").datepicker({
        minDate: 0,
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + datesec + "",
        altFormat: "yy-mm-dd"

    });

    setDefaultDateToday(datefirst);
    setDefaultDateToday(datesec);
}
function DatepickerDual(datefirst, datesec) {
    var today = new Date();
    var defaultDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();
    var todayNext = new Date();
    todayNext.setDate(todayNext.getDate() + 1);

    var defaultDateNext = todayNext.getFullYear() + "-" + (todayNext.getMonth() + 1) + "-" + todayNext.getDate();

    if ($("#" + datefirst).val() == "" && $("#" + datesec).val() == "") {
        
        $("#" + datefirst).val(defaultDate);
        $("#" + datesec).val(defaultDateNext);
    }

    $("#hd_" + datefirst).remove();
    $("#hd_" + datesec).remove();

    //if ($("#hd_" + datefirst).length) {
    //    $("#hd_" + datefirst).remove();
    //    $("#" + datefirst).val("");

    //}

    //if ($("#hd_" + datesec).length) {
    //    $("#hd_" + datesec).remove();
    //    $("#" + datesec).val("");

    //}

    $("#" + datefirst).after("<p style='display:none;'><input id='hd_" + datefirst + "' type='hidden' name='hd_" + datefirst + "' /></p>");

    $("#" + datesec).after("<p style='display:none;'><input id='hd_" + datesec + "' type='hidden' name='hd_" + datesec + "' /></p>");

    $("input[id$='" + datefirst + "']").datepicker({
        minDate: 0,
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.png',
        buttonImageOnly: true,
        showButtonPanel: true,
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + datefirst + "",
        altFormat: "yy-mm-dd"

    });
    
    // minDate: 1,
    $("input[id$='" + datesec + "']").datepicker({
        minDate: 0,
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.png',
        buttonImageOnly: true,
        showButtonPanel: true,
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + datesec + "",
        altFormat: "yy-mm-dd"

    });

    setDefaultDateToday(datefirst);
    setDefaultDateToday(datesec);
}
function DatepickerDual_noMin(datefirst, datesec) {
    var today = new Date();
    var defaultDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();
    var todayNext = new Date();
    todayNext.setDate(todayNext.getDate() + 1);

    var defaultDateNext = todayNext.getFullYear() + "-" + (todayNext.getMonth() + 1) + "-" + todayNext.getDate();

    if ($("#" + datefirst).val() == "" && $("#" + datesec).val() == "") {

        $("#" + datefirst).val(defaultDate);
        $("#" + datesec).val(defaultDateNext);
    }

    $("#hd_" + datefirst).remove();
    $("#hd_" + datesec).remove();

    //if ($("#hd_" + datefirst).length) {
    //    $("#hd_" + datefirst).remove();
    //    $("#" + datefirst).val("");

    //}

    //if ($("#hd_" + datesec).length) {
    //    $("#hd_" + datesec).remove();
    //    $("#" + datesec).val("");

    //}

    $("#" + datefirst).after("<p style='display:none;'><input id='hd_" + datefirst + "' type='hidden' name='hd_" + datefirst + "' /></p>");

    $("#" + datesec).after("<p style='display:none;'><input id='hd_" + datesec + "' type='hidden' name='hd_" + datesec + "' /></p>");

    $("input[id$='" + datefirst + "']").datepicker({
      
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.png',
        buttonImageOnly: true,
        showButtonPanel: true,
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + datefirst + "",
        altFormat: "yy-mm-dd"

    });

    // minDate: 1,
    $("input[id$='" + datesec + "']").datepicker({
      
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.png',
        buttonImageOnly: true,
        showButtonPanel: true,
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + datesec + "",
        altFormat: "yy-mm-dd"

    });

    setDefaultDateToday(datefirst);
    setDefaultDateToday(datesec);
}
function DatePicker_smallPicture(id) {
    if ($("#hd_" + id).length) {
        $("#hd_" + id).remove();

    }

    $("#" + id).after("<p style='display:none;'><input id='hd_" + id + "' type='hidden' name='hd_" + id + "' /></p>");

    // minDate: 1,
    $("input[id$='" + id + "']").datepicker({
       
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.gif',
        buttonImageOnly: true,
        showButtonPanel: true,
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + id + "",
        altFormat: "yy-mm-dd"

    });

    setDefaultDateToday(id);
}

function DatePicker_manual(id) {

    if ($("#hd_" + id).length) {
        $("#hd_" + id).remove();
        
    }

    $("#" + id).after("<p style='display:none;'><input id='hd_" + id + "' type='hidden' name='hd_" + id + "' /></p>");

    // minDate: 1,
    $("input[id$='" + id + "']").datepicker({
        minDate: 0,
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.png',
        buttonImageOnly: true,
        showButtonPanel: true,
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + id + "",
        altFormat: "yy-mm-dd"

    });

    setDefaultDateToday(id);

    //    DuplicatDatFormat(id);

    //    $("#" + id).change(function () {

    //        DuplicatDatFormat(id);

    //    });

}

function getMonthName(monthIndex) {
    var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return Mname[monthIndex];
}

function DatePicker(id) {

    if ($("#hd_" + id).length) {
        $("#hd_" + id).remove();
        $("#" + id).val("");
        
    } 

        $("#" + id).after("<p style='display:none;'><input id='hd_" + id + "' type='hidden' name='hd_" + id + "' /></p>");
        
       // minDate: 1,
        $("input[id$='" + id + "']").datepicker({
            
            yearRange: '2011:2020',
            changeMonth: true,
            changeYear: true,
            showOn: 'both',
            buttonImage: '../../images/calendaricon.png',
            buttonImageOnly: true,
            showButtonPanel: true,
            dateFormat: 'dd-M-yy',
            altField: "#hd_" + id + "",
            altFormat: "yy-mm-dd"

        });

        setDefaultDateToday(id);
}

function DatePicker_nopic(id) {

    if ($("#hd_" + id).length) {
        $("#hd_" + id).remove();
        $("#" + id).val("");

    }

    $("#" + id).after("<p style='display:none;'><input id='hd_" + id + "' type='hidden' name='hd_" + id + "' /></p>");

    // minDate: 1,
    $("input[id$='" + id + "']").datepicker({

        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        
        dateFormat: 'dd-M-yy',
        altField: "#hd_" + id + "",
        altFormat: "yy-mm-dd"

    });

    setDefaultDateToday(id);
}

function setDefaultDateToday(id) {
    if ($("#" + id).val() == "") {
        var today = new Date();
        var defaultDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();

        $("#" + id).val(defaultDate);

    }

    DuplicatDatFormat(id);
}


function parseDate(str) {
    var mdy = str.split('-');
    return new Date(mdy[0], mdy[1] - 1, mdy[2]);
}

function daydiff(first, second) {
    return (second - first) / (1000 * 60 * 60 * 24)
}

function LastDayOfMonth(Year, Month) {
    return (new Date((new Date(Year, Month + 1, 1)) - 1)).getDate();
}

//alert(daydiff(parseDate($('#first').val()), parseDate($('#second').val())));



function DisplayDate(destination) {
    var TextDateDisplay = destination; 
    //var TextDate = document.getElementById(source);
    if (TextDateDisplay != null) {
        var date = TextDateDisplay.value.split('-');
        var dateYear = date[0];
        var dateMonth = date[1] - 1;
        var dateDate = date[2];

        //var Mname = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        dDate = new Date(dateYear, dateMonth, dateDate);
        //var strMonth = Mname[dDate.getMonth()];

        var stringDateFormat = dDate.getDate() + "-" + Mname[dDate.getMonth()] + "-" + dDate.getFullYear();

        TextDateDisplay.value = stringDateFormat;
    }
}

function DuplicatDatFormat(destination) {

    var DesVal = $("#" + destination).val();

    // set  default value for hidden field
    if ($("#hd_" + destination).length) {
            $("#hd_" + destination).val(DesVal);
    }
   

    var TextDateDisplay = document.getElementById(destination);
    //var TextDate = document.getElementById(source);
    if (TextDateDisplay != null) {
        var date = TextDateDisplay.value.split('-');
        var dateYear = date[0];
        var dateMonth = date[1] - 1;
        var dateDate = date[2];

        //var Mname = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        dDate = new Date(dateYear, dateMonth, dateDate);
        //var strMonth = Mname[dDate.getMonth()];

        var stringDateFormat = dDate.getDate() + "-" + Mname[dDate.getMonth()] + "-" + dDate.getFullYear();

        TextDateDisplay.value = stringDateFormat;
    }

}