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
            $.get("/admin/front/ajax_front_destination_show.aspx?pdcid=" + ProductCat, function (data) {
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
        window.location.href = "/hotels_search.asp?k=" + scrh;
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
                Title = "Golf Courses";
                break;
            case "34":
                Title = "Day Trips";
                break;
            case "36":
                Title = "Water Activities";
                break;
            case "38":
                Title = "Shows & Events";
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

//Set Side Menu
this.sideMenu=function(){
$("#main_menu ul li").each(function () {
            var ProductCat = $(this).find("a").stop().attr("id");
            var $DivShow = $(this).find("div").stop();
            $.get("/admin/front/ajax_front_destination_show.aspx?pdcid=" + ProductCat, function (data) {
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
//GenLocation
this.dropdownLocation=function(){
	genDropDownDestination("ddDestination");
	}
//
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


function CreateCalendar()
{
	var dateCurrent=new Date();
	dayInit=(dateCurrent.getMonth()+1)+"/"+dateCurrent.getDate()+"/"+dateCurrent.getFullYear();
	$("input[rel='datepicker']").fnDatePicker({lang : "en", date : dayInit});

}
var monthEN = Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
//var monthTH = Array("มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม");

(function ($) {
	$.fn.fnDatePicker = function(options) {
		var defaults = {
			lang : "en"
		};
		
		var options = $.extend({}, defaults, options);
		return this.each(function() {
			var $this = $(this);
			var minChkOut;// = new Date(options.date);
			var dteTmp = new Date(options.date);
				dteTmp.setDate(dteTmp.getDate());

			/*Find checkout id*/
			var coObj = $this.attr("id").replace("ci", "co");

			/*Create Hidden Input*/
			$(this).after('<input type="hidden" name="Hd'+this.id+'" id="Hd'+this.id+'">');

			setDateHidden($this.attr("id"));

			
			fnDateInit ($this.attr("id"), dteTmp, options.lang, true);
			
			if($this.val() != ""){
				//change date to eng before assign new date
				minChkOut = new Date(fnDateSwapLang($this.val(), "en"));
				minChkOut.setDate(minChkOut.getDate() + 1);
			} else {
				minChkOut = dteTmp;
			}
			
			var checkOut=$("#"+coObj);
			//alert(checkOut.attr("id"));
			if(checkOut.attr("id")!=null){
			checkOut.after('<input type="hidden" name="Hd'+coObj+'" id="Hd'+coObj+'">');
			setDateHidden(coObj);
			fnDateInit (coObj, minChkOut, options.lang, false);
			}
		});
	
	}
})(jQuery);



function fnSetDate(inpDate) {
	var dteTmp = new Date(inpDate);
	var opDate; /* Mon - Sat, book next day */
	if (/[1-5]/i.test(dteTmp.getDay())) {
		if (dteTmp.getHours() < 14) {
			dteTmp.setDate(dteTmp.getDate() + 1)
		} else {
			dteTmp.setDate(dteTmp.getDate() + 2)
		}
	} else if (/[6]/i.test(dteTmp.getDay())) {
		if (dteTmp.getHours() < 14) {
			dteTmp.setDate(dteTmp.getDate() + 2)
		} else {
			dteTmp.setDate(dteTmp.getDate() + 3)
		}
	} else if (/[0]/i.test(dteTmp.getDay())) {
		dteTmp.setDate(dteTmp.getDate() + 2)
	}
	return dteTmp;
}


function fnDateInit (objId, minDate, lang, isCallBack) {
	var dateCurrent=new Date();
	var setYearRange=dateCurrent.getFullYear()+":"+(dateCurrent.getFullYear()+1)
	$("#" + objId).datepicker({
		minDate: minDate,
		numberOfMonths : 2,
		changeMonth : true,
		changeYear : true,
		yearRange: '2011:2012',
		dateFormat : 'd MM yy',
		altField:'#Hd'+objId,
		altFormat:'yy-mm-dd',
		buttonImage: '/images/ico_calendar_green1.jpg', 
		buttonImageOnly: true,
		showOn: 'both'
	});
	
	if (isCallBack) {
		$("#" + objId).bind({
			change : function (){
				fnDateOnSelect(objId, lang, $("#" + objId).val());
			}
		});
	}
}



function fnDateOnSelect(objId, lang, dateText) {
	var $objId = $("#" + objId);
	var $objCoID = $("#" + objId.replace("ci", "co"));
	//if (lang == 'th') {
		dateText = fnDateSwapLang(dateText, 'en')
	//}
	var chkIn = new Date(dateText);
	var chkOut;
	
	/*Set minimum date of checkout */
	var minChkOut = chkIn;
	minChkOut.setDate(minChkOut.getDate()+1);

	$objCoID.datepicker("option", "minDate", minChkOut);
	
	if ($objCoID.val() == "") {
		$objCoID.datepicker( "setDate" , minChkOut);
	}
}



function fnDateSwapLang(dateText, toLang) {
	var arrDate = dateText.split(" ");
	var monthIndex = arrDate[1];
	var i = 0;
	var opDate;
	var lang;
	
	//Check input lang
	if($.inArray(monthIndex, monthEN) != -1) {
		lang = "en";
	} else if($.inArray(monthIndex, monthTH) != -1) {
		lang = "th";
	}
	//Check output lang same input lang or lang undefined
	if((lang == toLang) || (lang == undefined)){
		return dateText;
	}
	
	/*Find index */
	for (i = 0; i <= 11; i++) {
		if (monthIndex == eval("month" + lang.toUpperCase() + "[i]")) {
			monthIndex = i;
			break;
		}
	}
	opDate = arrDate[0] + " " + eval("month" + toLang.toUpperCase() + "[monthIndex]") + " " + arrDate[2];
	
	return opDate;
}


function fnDateMonth(lang, indexMonth) {
	var month
	if (lang == 'en') {
		month = monthEN;
	} else if (lang == 'th') {
		month = monthTH
	}
	return month[indexMonth];
}
function setDateHidden(objInput)
{
	var valInput=$("#"+objInput).val();
	if(valInput!="")
	{
		$("#Hd"+objInput).val(getHiddenDateFormat(valInput));
	}
}
function getHiddenDateFormat(dateInput)
{
	var arrDate=dateInput.split(" ");
	
	for(i=0;i<monthEN.length;i++)
	{
		if(arrDate[1]==monthEN[i]){
			
			return arrDate[2]+"-"+(i+1)+"-"+arrDate[0];
			break;
		}
	}
}

function bookmark_us(url, title){

if (window.sidebar) // firefox
    window.sidebar.addPanel(title, url, "");
else if(window.opera && window.print){ // opera
    var elem = document.createElement('a');
    elem.setAttribute('href',url);
    elem.setAttribute('title',title);
    elem.setAttribute('rel','sidebar');
    elem.click();
} 
else if(document.all)// ie
    window.external.AddFavorite(url, title);
}

function CalcKeyCode(aChar) {
  var character = aChar.substring(0,1);
  var code = aChar.charCodeAt(0);
  return code;
}

function checkNumber(val) {
  var strPass = val.val();
  var strLength = strPass.length;
  var lchar = val.val().charAt((strLength) - 1);
  var cCode = CalcKeyCode(lchar);

  if (cCode < 48 || cCode > 57 ) {
    var myNumber = val.val().substring(0, (strLength) - 1);
    val.val(myNumber);
  }
  return false;
}

$().ready(function(){
				   var pid=$("#productDefault").val();

				   if($(".icon_paylater").length>0)
				   {
					   $.ajax({
							url:"/checkPayLater.aspx?pid="+pid,
							cache: false,
							dataType: "html",
							success: function(data, textStatus, XMLHttpRequest)
							{
								//$("#RoomPeriod").html(data);
								if(data!="")
								{
									paylater_content="<span class=\"tooltip_content\">";
									paylater_content=paylater_content+"<p>"+data+"</p>";
									paylater_content=paylater_content+"</span>";
									
									$("#top").css('background-image', 'url(/theme_color/blue/images/layout/bg-head_paylater.jpg)');
									$(".icon_paylater>a").addClass("tooltip");
									$(".icon_paylater>a>img").after(paylater_content);
									$(".icon_paylater>a").append('<img src="/theme_color/blue/images/icon/note_red.png" class="note" />');
									
								}

							}
						});
					   
						
					}
				   
});
var RefUrl = document.referrer;
            $.get("/admin/affiliate/ajax_affiliate_site_stat.aspx?Url_ref=" + RefUrl, function (data) {
                //alert(data);
            });