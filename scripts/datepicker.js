$(document).ready(function () {
    $("input[id$='txtDateStart']").datepicker({
        
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.png',
        buttonImageOnly: true,
        showButtonPanel: true

    });

    $("input[id$='txtDateEnd']").datepicker({
       
        yearRange: '2011:2020',
        changeMonth: true,
        changeYear: true,
        showOn: 'both',
        buttonImage: '../../images/calendaricon.png',
        buttonImageOnly: true,
        showButtonPanel: true

    });


    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

    function EndRequestHandler(sender, args) {
        $("input[id$='txtDateStart']").datepicker({
            minDate: 0,
            yearRange: '2011:2020',
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            buttonImage: '../../images/calendaricon.png',
            buttonImageOnly: true,
            showButtonPanel: true

        });
    }

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

    function EndRequestHandler(sender, args) {
        $("input[id$='txtDateEnd']").datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: 'button',
            buttonImage: '../../images/calendaricon.png',
            buttonImageOnly: true,
            showButtonPanel: true

        });
    }

});



function DuplicatDatFormat(source, destination) {


    var TextDateDisplay = document.getElementById(destination);
    var TextDate = document.getElementById(source);
    if (TextDate != null) {
        var date = TextDate.value.split('/');
        var dateYear = date[2];
        var dateMonth = date[0] - 1;
        var dateDate = date[1];

        //var Mname = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        var Mname = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        dDate = new Date(dateYear, dateMonth, dateDate);
        //var strMonth = Mname[dDate.getMonth()];

        var stringDateFormat = dDate.getDate() + "-" + Mname[dDate.getMonth()] + "-" + dDate.getFullYear();

        TextDateDisplay.value = stringDateFormat;
    }

}
