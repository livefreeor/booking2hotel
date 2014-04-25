// JavaScript Document
$.ajaxSetup({
    // Disable caching of AJAX responses
    cache: false
});

function tooltip (data, e) {
    
    xOffset = 10;
    yOffset = 20;

    $("body").append("<div id='tooltip' style=\"position:absolute;\">" + data + "</div>");
    

    $("#tooltip")
			.css("top", (e.pageY - xOffset) + "px")
			.css("left", (e.pageX + yOffset) + "px")
			.fadeIn('slow');

    document.onmousemove = getmouseposition;

     

}

function tooltip_remove(){
    $("#tooltip").remove();
}

function getmouseposition(e) {
    //var mousex = e.pageX - ($("#tooltip").width() / 2); //Get X coodrinates
    var mousex = e.pageX +10; //Get X coodrinates
    var mousey = e.pageY + 5; //Get Y coordinates

    $("#tooltip").css({ top: mousey, left: mousex });
}


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