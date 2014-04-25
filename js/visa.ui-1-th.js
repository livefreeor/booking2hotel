// JavaScript Document
$.ajaxSetup({
        // Disable caching of AJAX responses
        cache: false
    });
//Tooltip
this.tooltip = function(){	
	
		xOffset = 10;
		yOffset = 20;		

		
	$("a.tooltip").hover(function(e){											  

		//ptext=$("#policyContent_"+this.id).html();
		ptext=$(this).find('.tooltip_content').html();
		tagContent=$(this).find('.tooltip_content').get(0).nodeName;
		if(tagContent=="SPAN" || tagContent=="DIV" || tagContent=="P" || tagContent=="IMG")
		{
			
			
			if(tagContent!="IMG")
			{
				$("body").append("<div id='tooltip'>"+ptext+"</div>");
				//$("#tooltip").css("width","400px");
			}else{
				$("body").append("<div id='tooltip'><img src='"+$(this).find('.tooltip_content').attr("src")+"'/></div>");
			}
			
		//alert($("#tooltip").html());
			$("#tooltip")
				.css("top",(e.pageY - xOffset) + "px")
				.css("left",(e.pageX + yOffset) + "px")
				.fadeIn('slow');
				if(ptext.length>50)
				{
					$("#tooltip").css("width","400px");
				}else{
					
					$("#tooltip").css("width",$("#tooltip").width()+"px");
				}
		}
				
    },
	function(){
			$("#tooltip").remove();		
    });
	
	$("a.tooltip").mousemove(function(e){
		
		textContent=$(this).find('.tooltip_content').html();
		var mousex = e.pageX-($("#tooltip").width()/2) ; //Get X coodrinates
		var mousey = e.pageY +20 ; //Get Y coordinates
		
		$("#tooltip").css({  top: mousey, left: mousex });
	});
	
};

this.imgFloat = function(){	
	
	xOffset=10;
	yOffset=20;	
	$(".imgFloat").each(function(){
		$(this).hover(function(){
		$("body").append("<div id='imgFloat'></div>");
		var imgBig=$(this).attr('href');
		$("#imgFloat")
			.html("<img src='"+imgBig+"' class='preload'>")
			.css("top",(e.pageY) + "px")
			.css("left",(e.pageX) + "px")
			.fadeIn(100);
		},
		function(){
		$("#imgFloat").remove();
		});
		
		$(this).mousemove(function(e){
		$("#imgFloat")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left",(e.pageX + yOffset) + "px");
		});
	});
	
};
//End Tooltip

//DatePicker

function CreateCalendar(element)
{
	
	$("."+element).find(".vPicker").each(function(index){
		if(!$('#Hd'+this.id).length)
		{
			dateInputDefault=$(this).attr("rel");
			$(this).after('<input type="hidden" name="Hd'+this.id+'" id="Hd'+this.id+'">');
			if($('#Hd'+this.id).val()!=""){
				$('#Hd'+this.id).val(dateInputDefault);
				$(this).val(getFormatDate(dateInputDefault,1))
				//alert($(this).val());
			}else{
				if(dateInputDefault!="")
				{
					$('#Hd'+this.id).val(dateInputDefault);
					$(this).val(getFormatDate(dateInputDefault,1))
				}else{
					$(this).val("");
				}
			
			}
			
		}
		
		if(index==1)
		{
			var dateSet=new Date();
			setDatePickerInput(this.id,"",2,1);
			
		}else{
			var dateSet=new Date();
			setDatePickerInput(this.id,"",1,1);
		}
		
	});
}

function getDateDiff(dateStart,dateEnd)
{
	return (Math.ceil((dateEnd.getTime()-dateStart.getTime())/(24*60*60*1000)));
}
function getFormatDate(dateInput,typeFormat)
{

	var arrMonth = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
	
	var dateSplit =dateInput.split("-");
    var dateYear = dateSplit[0];
    var dateMonth = dateSplit[1] - 1;
    var dateDate = dateSplit[2];
	
	dDate = new Date(dateYear, dateMonth, dateDate);	
	var stringDateFormat = dDate.getDate() + "-" + arrMonth[dDate.getMonth()] + "-" + dDate.getFullYear();
	return stringDateFormat;
}

function setDatePickerInput(elemInput,dateDefault,dateMin,typeDisplay){
	$("#"+elemInput).datepicker({
	minDate:dateMin,
	changeMonth:true,
	changeYear:true,
	dateFormat:'dd-M-yy',
	altField:'#Hd'+elemInput,
	altFormat:'yy-mm-dd',
	showOn: 'both',
    buttonImage: '/images/ico_calendar_green1.jpg',
    buttonImageOnly: true,
    showButtonPanel: true
		});
}
//End DatePicker

//GenLocation
this.dropdownLocation=function(){
	genDropDownDestination("ddDestination");
	}
//
//Set Side Menu
this.sideMenu=function(){
$("#main_menu ul li").each(function () {
            var ProductCat = $(this).find("a").stop().attr("id");
            var $DivShow = $(this).find("div").stop();
            $.get("/admin/front/ajax_front_destination_show.aspx?pdcid=" + ProductCat+"&ln=2", function (data) {
                $DivShow.html(data);
            });
        });


        $("#main_menu ul li").hover(function () {
            var ProductCat = $(this).find("a").stop().attr("id");
            var $DivShow = $(this).find("div").stop();
            var Y_top = $(this).offset().top;
            var X_left = $(this).offset().left + 200;
            $DivShow.css({ "top": Y_top + "px", "left": X_left + "px" });
            $DivShow.show();

            $("#main_menu ul li").children("div").filter(function () {
                return $(this).attr("id") != "div_" + ProductCat;
            }).hide();
            
        }, function () {
            var ProductCatOut = $(this).find("a").stop().attr("id");
            var $DivShowOut = $(this).children("div");
            $DivShowOut.hide();
        })
}
//
//Quick Search
this.quickSearch=function(){
	var result = $.get("/admin/front/ajax_front_search_suggest.aspx", function (data) {
            arrData = data.split(";!;");
            $("#txtSearch").autocomplete(arrData);
        });

			$("#txtSearch").focus(function(){
				if($("#txtSearch").val()=="Quick Search"){
					$("#txtSearch").val("");
				}
			});
			
			$("#txtSearch").blur(function(){
				if($("#txtSearch").val()==""){
					$("#txtSearch").val("Quick Search");
				}
			});
			
			$("#btnQuick").click(function(){
				SearchClick()
			});

}
function SearchClick() {
        var scrh = $("#txtSearch").val();
        window.location.href = "/hotels_search-th.asp?k=" + scrh;
}
//Quick Search  End
//Search Panal Main

function SearchPanalMain()
{
	$("#btn_hotel").attr("value", "Search For Hotels");
        $("#menu_hotsearch :radio").each(function () {
            var Value = $(this).attr("value");


            $(this).click(function () {
                $("#h4 span").html(ProductTitle(Value));

                if (Value != 29) {
                    $("#ddLocation_li").hide();
                    $("#dateEnd_li").hide();
                    $("#btn_hotel").attr("value", "Search For " + ProductTitle(Value));

                }
                else {
                    $("#ddLocation_li").show();
                    $("#dateEnd_li").show();
                    $("#btn_hotel").attr("value", "Search For Hotels");
                }
            })
        })
}
function ProductTitle(catId) {

        var Title = "";
        switch (catId) {
            case "29":
                Title = "Hotels";
                break;
            case "32":
                Title = "Golf Course";
                break;
            case "34":
                Title = "Day Trips";
                break;
            case "36":
                Title = "Water Activity";
                break;
            case "38":
                Title = "Show & Event";
                break;
            case "39":
                Title = "Health Check Up";
                break;
            case "40":
                Title = "Spa";
                break;
        }
        return Title;
    }
//
this.lightboxFrame=function()
{
	$(".lightBoxFrame").fancybox({
		'width'				: 650,

        'autoScale'     	: false,
        'transitionIn'		: 'none',
		'transitionOut'		: 'none',
		'type'				: 'iframe'
	});
}
function renderGallery(elementGroup)
{
$("a[rel="+elementGroup+"]").fancybox({
				'transitionIn'		: 'none',
				'transitionOut'		: 'none',
				'titlePosition' 	: 'over',
				'titleFormat'		: function(title, currentArray, currentIndex, currentOpts) {
					return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
				}
			});
}
var RefUrl = document.referrer;
            $.get("/admin/affiliate/ajax_affiliate_site_stat.aspx?Url_ref=" + RefUrl, function (data) {
                //alert(data);
            });