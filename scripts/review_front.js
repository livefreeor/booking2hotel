$(document).ready(function () {
    var ga = document.createElement('script'); ga.type = 'text/javascript';
    //ga.src = 'http://localhost:6363/review_write.aspx';
   ga.src = 'http://www.booking2hotels.com/review_write.aspx';
    //var s = document.getElementsByTagName('script')[3]; s.parentNode.insertBefore(ga, s);

    

    $("#review_block_main").append(ga);


});

function DataBindClick() {
    $("#btnReviewSubmit").click(function () {
        var ProducId = $("#product").val();
        //alert(ProducId);

        var post = $("#review_form_front").find("input,textarea,select,hidden").serialize();
        var ga = document.createElement('script'); ga.type = 'text/javascript';
        //ga.src = "http://localhost:6363/review_write_pcs.aspx?" + post + "&pid=" + ProducId;

        ga.src = "http://www.booking2hotels.com/review_write_pcs.aspx?" + post + "&pid=" + ProducId;

        //document.write("http://localhost:6363/review_write_pcs.aspx?" + post + "&pid=" + ProducId);
        $("#review_block_main").html(ga);
        //$("#review_block_main").append(ga);

    });
    
}