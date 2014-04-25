$.ajaxSetup({
        // Disable caching of AJAX responses
        cache: false
    });

var monthEN = Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
var monthTH = Array("มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม");

arrPtype=new Array;
arrPtype["0"] = new Array;
arrPtype["0"][0] = "29";
arrPtype["0"][1] = "HOTELS";
		
arrPtype["1"] = new Array;
arrPtype["1"][0] = "32";
arrPtype["1"][1] = "Golf Courses";
		
arrPtype["2"] = new Array;
arrPtype["2"][0] = "34";
arrPtype["2"][1] = "Day Trips";
		
arrPtype["3"] = new Array;
arrPtype["3"][0] = "36";
arrPtype["3"][1] = "Water Activities";
		
arrPtype["4"] = new Array;
arrPtype["4"][0] = "38";
arrPtype["4"][1] = "Shows & Events";

arrPtype["5"] = new Array;
arrPtype["5"][0] = "39";
arrPtype["5"][1] = "Health Check Up";

arrPtype["6"] = new Array;
arrPtype["6"][0] = "40";
arrPtype["6"][1] = "Spa";


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
			if(coObj!=$(this).id){
			
			var checkOut=$("#"+coObj);
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
	//alert(setYearRange)
	$("#" + objId).datepicker({
		minDate: minDate,
		numberOfMonths : 2,
		changeMonth : true,
		changeYear : true,
		yearRange: setYearRange,
		dateFormat : 'd MM yy',
		altField:'#Hd'+objId,
		altFormat:'yy-mm-dd',
		buttonImage: '../images/ico_calendar_new.jpg', 
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

function fnDateDisplay(lang, dateText)
{
	var objResult="";
	var monthName="";
	if(dateText!="")
	{
		
		arrDate=dateText.split('-');

		monthName=monthEN[parseInt(arrDate[1],10)-1];

		objResult=arrDate[2]+" "+monthName+" "+arrDate[0];
	}
	return objResult;
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

function createCookie(name,value,days) {
	if (days) {
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}

function readCookie(name) {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++) {
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

function eraseCookie(name) {
	createCookie(name,"",-1);
}

function validDate(dateInput){
 date=dateInput;
 //checkFormat1=(/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(date))
 checkFormat1=(/^\d{4}\-\d{1,2}\-\d{1,2}$/.test(date))

 date=date.split('-')
 d=new Date(date[0],date[1]-1,date[2])
 //checkFormat2=(1*date[0]==d.getDate() && 1*date[1]==(d.getMonth()+1) && 1*date[2]==d.getYear())
 checkFormat2=(1*date[2]==d.getDate() && 1*date[1]==(d.getMonth()+1) && 1*date[0]==d.getYear())

 	if (checkFormat1 && checkFormat2){
	  return true
	}else{
		 return false
	}

}

function getXMLLocationToDropDown(productCate)
{
	
var category=null;
			$.ajax({
                 type: "GET",
                 url: "/location.xml",
                 dataType: "xml",
                 success: function(xml) {
                   categoryRoot=$(xml).find('ProductCategory').filter(function(){
				   	return $(this).attr('id')==productCate
				   });
				   
				   getDestinationDropDown(categoryRoot)
				   
                 }
				 
             });
}

function getDestinationDropDown(categoryRoot)
{
	
	//var destinationFirst=categoryRoot.find('Destination').stop().attr('id');
	var destinationFirst=0;
	var destinationList='<select id="dest" class="dd_dest">';
	destinationList=destinationList+'<option value="0">Select Destination</option>';
	categoryRoot.find('Destination').each(function(){
	destinationList=destinationList+'<option value="'+$(this).attr('id')+'">'+$(this).attr('title')+'</option>';	
	});
	destinationList=destinationList+'</select>';
	
	$("#ddDestination").html(destinationList);

	//set event onchange

	if(categoryRoot.attr('id')==29)
	{
		
		$("#ddLocation").show();
		$(".dateCheckoutBox").show();
		$("#dest").change(function(){
			getLocationDropDown(categoryRoot,$(this).val())
		});
		if(readCookie("dest")!=null)
		{

			$("#dest").val(readCookie("dest"));
			getLocationDropDown(categoryRoot,readCookie("dest"))
			if(readCookie("loc")!=null){
				$("#loc").val(readCookie("loc"));
			}
		}else{
			getLocationDropDown(categoryRoot,destinationFirst)
		}
		
	}else{
			if(readCookie("dest")!=null)
			{
				$("#dest").val(readCookie("dest"));
	
			}
			$("#ddLocation").hide();
			//alert($(".dateCheckoutBox").length);
			$(".dateCheckoutBox").hide();
	}
	
}
function getLocationDropDown(categoryRoot,destination_id)
{
	
	
	locationList=categoryRoot.find('Destination').filter(function(){
		return $(this).attr('id')==destination_id
	});
	
	var ddLocation='<select id="loc" class="dd_loc">';
	if(destination_id==0)
	{
		ddLocation=ddLocation+'<option value="-1">Select Location</option>';
	}else{
		ddLocation=ddLocation+'<option value="0">All</option>';
	}
	
	locationList.find('Location').each(function(){
		ddLocation=ddLocation+'<option value="'+$(this).attr('id')+'">'+$(this).text()+'</option>';
	});
	ddLocation=ddLocation+'</select>';
	$("#ddLocation").html(ddLocation);
	
	
}

function readLocation(xmlDestination)
{

	xmlDestination.children('Location').each(function(){
						alert($(this).text());
						});
	//alert(xmlDestination.Child.length);
	
}

function setCookieConfig()
{
	
	var arrCookie=new Array("pType","dateci","dateco","dest","loc","sortby","cur");
	var result="";

	for(countCookie=0;countCookie<arrCookie.length;countCookie++)
	{

		if(GetValueQueryString(arrCookie[countCookie])!="")
		{
			if(arrCookie[countCookie]=="dateIn" || arrCookie[countCookie]=="dateOut")
			{
				if(validDate(GetValueQueryString(arrCookie[countCookie])))
				{
					createCookie(arrCookie[countCookie],GetValueQueryString(arrCookie[countCookie]),1)
				}
			}else{
				createCookie(arrCookie[countCookie],GetValueQueryString(arrCookie[countCookie]),1)
			}
			//createCookie(arrCookie[countCookie],GetValueQueryString(arrCookie[countCookie]),1)
			result=result+arrCookie[countCookie]+": "+readCookie(arrCookie[countCookie])+"<br>";
		}else{
			createCookie(arrCookie[countCookie],"",1)
		}
	}
	//$("#result").html(result);
}

function getTitle(cate_id)
{
	var result='undefined';
	for(i=0;i<arrPtype.length;i++)
	{
		if(arrPtype[i][0]==cate_id)
		{
			result=arrPtype[i][1];
		}
	}
	return result;
}

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

this.sideMenu=function(){
$("#main_menu ul li").each(function () {
            var ProductCat = $(this).find("a").stop().attr("id");
            var $DivShow = $(this).find("div").stop();
            $.get("/admin/front/ajax_front_destination_show.aspx?pdcid=" + ProductCat+"&ln=1", function (data) {
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


var RefUrl = document.referrer;
            $.get("/admin/affiliate/ajax_affiliate_site_stat.aspx?Url_ref=" + RefUrl, function (data) {
                //alert(data);
            });