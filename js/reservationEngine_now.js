$.ajaxSetup({
        // Disable caching of AJAX responses
        cache: false
    });

var monthEN = Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
var monthTH = Array("มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม");



function setDateHidden(objInput)
{
	var valInput=$("#"+objInput).val();
	if(valInput!="")
	{
		$("#Hd"+objInput).val(getHiddenDateFormat(valInput));
	}
}

function ShowDetailPack(id) {

    $("#" + id).slideToggle("fast");
    //alert(id);

    return false;
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
		altFormat:'yy-mm-d'
//		buttonImage: '../images/ico_calendar.png', 
//		buttonImageOnly: true,
		//showOn: 'both'
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
//y-m-d
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


function setCookieConfig()
{
	
	var arrCookie=new Array("dateci","dateco");
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

		}else{
			createCookie(arrCookie[countCookie],"",1)
		}
	}
	//$("#result").html(result);
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
			textContent='';
    });
	
	$("a.tooltip").mousemove(function(e){
		
		textContent=$(this).find('.tooltip_content').html();
		//var mousex = e.pageX-($("#tooltip").width()/2) ; //Get X coodrinates
		var mousex = e.pageX ; //Get X coodrinates
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
;(function($){var l=location.href.replace(/#.*/,'');var g=$.localScroll=function(a){$('body').localScroll(a)};g.defaults={duration:1e3,axis:'y',event:'click',stop:true,target:window,reset:true};g.hash=function(a){if(location.hash){a=$.extend({},g.defaults,a);a.hash=false;if(a.reset){var e=a.duration;delete a.duration;$(a.target).scrollTo(0,a);a.duration=e}i(0,location,a)}};$.fn.localScroll=function(b){b=$.extend({},g.defaults,b);return b.lazy?this.bind(b.event,function(a){var e=$([a.target,a.target.parentNode]).filter(d)[0];if(e)i(a,e,b)}):this.find('a,area').filter(d).bind(b.event,function(a){i(a,this,b)}).end().end();function d(){return!!this.href&&!!this.hash&&this.href.replace(this.hash,'')==l&&(!b.filter||$(this).is(b.filter))}};function i(a,e,b){var d=e.hash.slice(1),f=document.getElementById(d)||document.getElementsByName(d)[0];if(!f)return;if(a)a.preventDefault();var h=$(b.target);if(b.lock&&h.is(':animated')||b.onBefore&&b.onBefore.call(b,a,f,h)===false)return;if(b.stop)h.stop(true);if(b.hash){var j=f.id==d?'id':'name',k=$('<a> </a>').attr(j,d).css({position:'absolute',top:$(window).scrollTop(),left:$(window).scrollLeft()});f[j]='';$('body').prepend(k);location=e.hash;k.remove();f[j]=d}h.scrollTo(f,b).trigger('notify.serialScroll',[f])}})(jQuery);
(function(c){var a=c.scrollTo=function(f,e,d){c(window).scrollTo(f,e,d)};a.defaults={axis:"xy",duration:parseFloat(c.fn.jquery)>=1.3?0:1};a.window=function(d){return c(window)._scrollable()};c.fn._scrollable=function(){return this.map(function(){var e=this,d=!e.nodeName||c.inArray(e.nodeName.toLowerCase(),["iframe","#document","html","body"])!=-1;if(!d){return e}var f=(e.contentWindow||e).document||e.ownerDocument||e;return c.browser.safari||f.compatMode=="BackCompat"?f.body:f.documentElement})};c.fn.scrollTo=function(f,e,d){if(typeof e=="object"){d=e;e=0}if(typeof d=="function"){d={onAfter:d}}if(f=="max"){f=9000000000}d=c.extend({},a.defaults,d);e=e||d.speed||d.duration;d.queue=d.queue&&d.axis.length>1;if(d.queue){e/=2}d.offset=b(d.offset);d.over=b(d.over);return this._scrollable().each(function(){var l=this,j=c(l),k=f,i,g={},m=j.is("html,body");switch(typeof k){case"number":case"string":if(/^([+-]=)?\d+(\.\d+)?(px|%)?$/.test(k)){k=b(k);break}k=c(k,this);case"object":if(k.is||k.style){i=(k=c(k)).offset()}}c.each(d.axis.split(""),function(q,r){var s=r=="x"?"Left":"Top",u=s.toLowerCase(),p="scroll"+s,o=l[p],n=a.max(l,r);if(i){g[p]=i[u]+(m?0:o-j.offset()[u]);if(d.margin){g[p]-=parseInt(k.css("margin"+s))||0;g[p]-=parseInt(k.css("border"+s+"Width"))||0}g[p]+=d.offset[u]||0;if(d.over[u]){g[p]+=k[r=="x"?"width":"height"]()*d.over[u]}}else{var t=k[u];g[p]=t.slice&&t.slice(-1)=="%"?parseFloat(t)/100*n:t}if(/^\d+$/.test(g[p])){g[p]=g[p]<=0?0:Math.min(g[p],n)}if(!q&&d.queue){if(o!=g[p]){h(d.onAfterFirst)}delete g[p]}});h(d.onAfter);function h(n){j.animate(g,e,d.easing,n&&function(){n.call(this,f,d)})}}).end()};a.max=function(j,i){var h=i=="x"?"Width":"Height",e="scroll"+h;if(!c(j).is("html,body")){return j[e]-c(j)[h.toLowerCase()]()}var g="client"+h,f=j.ownerDocument.documentElement,d=j.ownerDocument.body;return Math.max(f[e],d[e])-Math.min(f[g],d[g])};function b(d){return typeof d=="object"?d:{top:d,left:d}}})(jQuery);
$(document).ready(function(){
	if($("#memberLinkPan"))
	{
		AuthenCheck();
	}  
	
	$("#btnCheck").click(function(){
				//alert($('#Hddateci').val()+" "+$('#Hddateco').val());
				if($('#Hddateci').val()=="" || $('#Hddateco').val()=="")
				{
					alert("Please check your date");
					return false;
				}else{
					createCookie("dateci",$('#Hddateci').val(),1)
					createCookie("dateco",$('#Hddateco').val(),1)
					btnCheck=readCookie("dateci");
					dateco=readCookie("dateco");
					
				}
				getRateTable();
		});
		$("#btnBook").click(function(){
				//alert($('#Hddateci').val()+" "+$('#Hddateco').val());
				if($('#Hddateci').val()=="" || $('#Hddateco').val()=="")
				{
					alert("Please check your date");
					return false;
				}else{
					createCookie("dateci",$('#Hddateci').val(),1)
					createCookie("dateco",$('#Hddateco').val(),1)
					btnCheck=readCookie("dateci");
					dateco=readCookie("dateco");
					
				}
				location.href=$("#frmCheckRate").attr('action');
				//$("#frmCheckRate").submit();
				//$("#frmCheckRate").submit();
		});
		getRateTable();
		//if($('#selCurrency')!=null){
//			$('#rateExchange').val($('#selCurrency').val());
//			}
if(readCookie("cur")!=null)
{
	$('#rateExchange').val(readCookie("cur"));
}
	
			
	var dateCurrent=new Date();
		dayInit=(dateCurrent.getMonth()+1)+"/"+(dateCurrent.getDate())+"/"+dateCurrent.getFullYear();
		$("input[rel='datepicker']").fnDatePicker({lang : "en", date : dayInit});
		dateInDefault=readCookie("dateci");
		dateOutDefault=readCookie("dateco");

		if(dateInDefault!=null){
			$("#dateci").val(fnDateDisplay('en', dateInDefault));
			setDateHidden("dateci")
			if(dateOutDefault!=null){
				$("#dateco").val(fnDateDisplay('en', dateOutDefault));
				setDateHidden("dateco")
			}
			//alert($("#Hddateco").val());
			$('#dateci').trigger('change');
		
			getRateTable();
			
			
		}
		
		
});

function getRateTable()
{
    //console.log('sss');
	mmAuthen="";
	if(readCookie("member")!=null)
	{
		mmAuthen=readCookie("member")
	}
	
	$('.b2hRateResult').html('<table align="center" style=" margin-top:80px;line-height:24px;"><tr><td><img src="http://engine.booking2hotels.com//images/preloading2.gif"></td><td style=" font-size:24px; color:#222222; ">Loading...</td></tr></table>');
	var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
	ga.src = 'http://www.supernoom.com/engineBooking2/BookingEngine.php?' + $("#frmCheckRate").serialize() + '&mm=' + mmAuthen;
    //http://www.modern-garages.com/BookingEngine.php
	//ga.src = 'http://www.modern-garages.com/BookingEngine.php?Hddateci=2013-9-27&dateci=2013-9-27&Hddateco=2013-9-28&dateco=2013-9-28&rateExchange=25&pid=3605';
	//ga.src = 'http://www.modern-garages.com/BookingEngine.php?Hddateci=2013-9-27&dateci=2013-9-27&Hddateco=2013-9-28&dateco=2013-9-28&rateExchange=25&pid=3605';
	//alert(ga.src);
	//ga.src = 'http://www.361thailand.com/BookingEngine.php?'+$("#frmCheckRate").serialize();
	$(".b2hRateResult").append(ga);
		

}

function fnSetControlDefault()
{
    $('#selCurrency').change(function () {

        $('#rateExchange').val($('#selCurrency').val());
        createCookie("cur", $('#selCurrency').val(), 1);
        getRateTable();

        //createCookie("cur", $('#selCurrency').val(), 1);
                
        //getRateTable();

				//console.log('333');
				//alert();
				//document.getElementById("frmCheckRate").submit();
			});
			$('#expandBox').click(function(){
				$('.tr-price-hidden').slideToggle('fast');
				$(this).toggleClass('contract');
				if($(this).text()=='Show more room type')
				{
					$(this).text('Hide additional room type');
				}else{
					$(this).text('Show more room type');
				}
			});
}
	//$(document).ready(function()
//	{
//		var hpd=$("#hdp").val();
//		urldest="renderRate.php?pid="+hpd;
//		if(readCookie("dateci")!=null)
//		{
//			dateci=readCookie("dateci");
//			dateco=readCookie("dateco");
//			urldest="renderRate.php?pid="+hpd+"&dateci="+dateci+"&dateco="+dateco
//		}
//		
//		RateResult(urldest);
//		$("#btnCheck").click(function(){
//				//alert($('#Hddateci').val()+" "+$('#Hddateco').val());
//				if($('#Hddateci').val()=="" || $('#Hddateco').val()=="")
//				{
//					alert("Please check your date");
//					return false;
//				}else{
//					createCookie("dateci",$('#Hddateci').val(),1)
//					createCookie("dateco",$('#Hddateco').val(),1)
//					dateci=readCookie("dateci");
//					dateco=readCookie("dateco");
//					urldest="renderRate.php?pid="+hpd+"&dateci="+dateci+"&dateco="+dateco
//					RateResult(urldest);
//				}
//		});
		
		//var dateCurrent=new Date();
//		dayInit=(dateCurrent.getMonth()+1)+"/"+(dateCurrent.getDate()+1)+"/"+dateCurrent.getFullYear();
//		$("input[rel='datepicker']").fnDatePicker({lang : "en", date : dayInit});
//		dateInDefault=readCookie("dateci");
//		dateOutDefault=readCookie("dateco");
//
//		if(dateInDefault!=null){
//			$("#dateci").val(fnDateDisplay('en', dateInDefault));
//			setDateHidden("dateci")
//			if(dateOutDefault!=null){
//				$("#dateco").val(fnDateDisplay('en', dateOutDefault));
//				setDateHidden("dateco")
//			}
//			$('#dateci').trigger('change');
//		}
//		
//	});
	function RateResult(urldest)
	{
		$("#RateResult").html('<div id=\"preloadRate\"><img src="../images/ajax-loader.gif"/><br/>Loading...</div>');
		$.ajax({
						url:urldest,
						cache: false,
						dataType: "html",
						success: function(data, textStatus, XMLHttpRequest)
						{
							$("#RateResult").html(data);
							setEventToDropDown();
							tooltip();
			
						}
					});
	}
		
var roomMaxAdult=0;
var roomMaxChild=0;
var transferMaxAdult=0;
var roomExtraBedMax=0;
var sumExtraBed=0;

function setEventToDropDown()
{
				$(".ddPrice").each(function(){
					$(this).change(function(){
					calMaxAdult();
					});
				});
				
				$(".ddPackage").each(function(){
					$(this).change(function(){
					calMaxAdult();
					});
				});
				
				$(".ddTransfer").each(function(){
					$(this).change(function(){
					calMaxTransfer();
					});
				});
				
				$(".ddPriceExtraBed").each(function(){
					$(this).change(function(){
					calMaxExtraBed();
					});
				});
								
				$("#btnBooking").click(function(){
					checkBooking();
					return false;
				});
				calMaxAdult();
				calMaxTransfer();
}

function calMaxTransfer()
{
	var max_adult_transfer=0;
	$(".ddTransfer").each(function(){
		optionQty=$(this).find("option:selected").stop().text().split(" ")[0];
		arrValue=$(this).find("option:selected").stop().val().split("_");
		optionValue=arrValue[3];
		max_adult_transfer=max_adult_transfer+(optionValue*optionQty);
	});
	transferMaxAdult=max_adult_transfer;
}

function calMaxAdult()
{
	var max_adult_room=0;
	var max_child_room=0;
	var max_room_extrabed=0;
	
	$(".ddPrice").each(function(){
		arrValue=$(this).find("option:selected").stop().val().split("_");
		max_adult_room=max_adult_room+(arrValue[3]*arrValue[6]);
		max_child_room=max_child_room+(arrValue[4]*arrValue[6]);
		max_room_extrabed=max_room_extrabed+(arrValue[5]*arrValue[6])
	});
	
	$(".ddPackage").each(function(){
		arrValue=$(this).find("option:selected").stop().val().split("_");
		max_adult_room=max_adult_room+(arrValue[3]*arrValue[6]);
		max_child_room=max_child_room+(arrValue[4]*arrValue[6]);
		max_room_extrabed=max_room_extrabed+(arrValue[5]*arrValue[6])
	});
	
	roomMaxAdult=max_adult_room;
	roomMaxChild=max_child_room;
	roomExtraBedMax=max_room_extrabed;
	
}

function calMaxExtraBed()
{
	var total_extrabed=0;

	$(".ddPriceExtraBed").each(function(){
		arrValue=$(this).find("option:selected").stop().val().split("_");
		total_extrabed=total_extrabed+(arrValue[3]*arrValue[6]);
	});
	
	sumExtraBed=total_extrabed;
}

function checkBooking()
{
	$(".errorMsg").each(function(){
		$(this).text("").removeClass("errorMessage");				 
	});
	if($("#adult").val()!=null){

	//return false;
	selAdult=parseInt($("#adult option:selected").val());
	selChild=parseInt($("#child option:selected").val());
	//$("#errorRoom").removeClass("errorMsg");
	//$("#errorTransfer").removeClass("errorMsg");

	if(selAdult>(roomMaxAdult+sumExtraBed) && (selAdult+selChild)>transferMaxAdult)
	{
			$.scrollTo("#errorRoom", 800, {offset: {left: 0, top: -200}, onAfter: function () {
			$('<div></div>').html("Exceed adult, please select extra room or extra bed. Please click here to see room definition.").dialog({ modal: true, title: 'Oops! there was an error with your submission', width: 400, buttons: { Ok: function() { $(this).dialog('close') } } });
			}});
			return false;
	}
	
	if(roomExtraBedMax<sumExtraBed){
		$.scrollTo("#errorRoom", 800, {offset: {left: 0, top: -200}, onAfter: function () {
			$('<div></div>').html("Exceed extra bed policy, Please select more room.").dialog({ modal: true, title: 'Oops! there was an error with your submission', width: 400, buttons: { Ok: function() { $(this).dialog('close') } } });
			}});
			return false;
	}
	
	if(transferMaxAdult>0)
	{
		if((selAdult+selChild)>transferMaxAdult)
		{
			$.scrollTo("#errorTransfer", 800, {offset: {left: 0, top: -200}, onAfter: function () {
			$('<div></div>').html("Capacity is exceed, please select more vehicle.").dialog({ modal: true, title: 'Oops! there was an error with your submission', width: 400, buttons: { Ok: function() { $(this).dialog('close') } } });
			}});
			return false;
		}
	}
	}else{
		if((roomMaxAdult+roomMaxChild)==0){
		$.scrollTo("#errorRoom", 800, {offset: {left: 0, top: -200}, onAfter: function () {
			$('<div></div>').html("Please select option first.").dialog({ modal: true, title: 'Oops! there was an error with your submission', width: 400, buttons: { Ok: function() { $(this).dialog('close') } } });
		}});
		return false;
		}
		}
	
	if($("#category").val()==32)
	{
		if($("#tee_hour").val()=="" || $("#tee_min").val()=="")
		{
			$.scrollTo("#errorRoom", 800, {offset: {left: 0, top: -200}, onAfter: function () {
			$('<div></div>').html("Please select your approximate tee-off time").dialog({ modal: true, title: 'Oops! there was an error with your submission', width: 400, buttons: { Ok: function() { $(this).dialog('close') } } });
		}});
			return false;
		}
	}
	
	$("#FormBooking").submit();
	//document.getElementById("FormBooking").submit();
}

var windowObjectReference;
	var strWindowFeatures = "menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes";
	 
	function openMemberLoginForm()
	{
	  window.open("http://www.booking2hotels.com/member/memberLogin.html", "myWindowName", "width=600, height=350,titlebar=no,toolbar=no,location=no");
	}
	
	function openMemberSignupForm()
	{
	  window.open("http://www.booking2hotels.com/member/signup2.html", "myWindowName", "width=600, height=500,titlebar=no,toolbar=no,location=no");
	}
	
	function signOutMember()
	{
		var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
		ga.src = 'http://www.booking2hotels.com/member/memberSignout.aspx';
		$("#memberLinkPan").append(ga);
	}
	
	function authenComplete(){
		//$("#memberLinkPan").html("test");
		
		$("#memberLinkPan").html('Welcome '+readCookie("member_name")+'<br/><a href="javascript:signOutMember();">Sign out</a>');
		$("#frmCheckRate").append('<input type="hidden" id="mm" name="mm" value="'+readCookie("member")+'"/>');
		//createCookie("member",cid,1)
	}
	
	function authenFail()
	{
		eraseCookie("member");
		eraseCookie("member_name");
		//alert($("#frmCheckRate").serialize());
		$("#memberLinkPan").html('<a href="javascript:b2hLoginBox();">Member login</a><br/><a href="javascript:openMemberSignupForm();">Sign Up</a><br/><a href="javascript:b2hForgotBox();">Forgot Password?</a>');
	}
	
	/**/
	function modalBox(w, data) {

    $("<div id=\"modalPan\" style=\"position:fixed\" >" + data + "</div>").prependTo('body');
    var popWidth = w;
    var fade = w - 10;
    //Fade in the Popup and add close button
    $('#modalPan').fadeIn().css({ 'width': Number(popWidth) });

    //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css
    var popMargTop = ($('#modalPan').height() + 80) / 2;
    var popMargLeft = ($('#modalPan').width() + 80) / 2;

    // var fadeHeight = $('#darkman_pop').height() + 20;
    var fadeHeight = $('#modalPan').height() - 10;

    //Apply Margin to Popup
    $('#modalPan').css({
        'margin-top': -popMargTop,
        'margin-left': -popMargLeft
    });

    //Fade in Background
    $('body').prepend('<div id="modalPanBG"></div>'); //Add the fade layer to bottom of the body tag.
    $('#modalPanBG').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
        //$('#fade2').css({ 'opacity': '0.8', 'filter': 'alpha(opacity=80)' }).fadeIn();  //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies
    //$("#darkman_pop").draggable({ scroll: true });

    return false;


}
function ModalBoxClose() {
    $('#modalPanBG , #modalPan').fadeOut(function () {
        $('#modalPanBG, #modalPan').remove();  //fade them both out
    });
    return false;
}


function AuthenCheck()
{
	if(readCookie("member")!=null)
	{
		authenComplete();
		
	}else{
		authenFail();
	}
	//var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
//	ga.src = 'http://www.booking2hotels.com/member/memberLoginCheck.aspx';
//	$("#frmSignup").append(ga);
}

function loginComplete(cid,cname)
{
	createCookie("member",cid,1)
	createCookie("member_name",cname,1)
	ModalBoxClose();
	location.reload();
}

function forgetMemberSendComplete()
{
	$("#registerForm").html('An email with the requested information has been sent. <a href=\"javascript:void(0)\" onclick=\"ModalBoxClose();\">close</a>');
}

function forgetMemberSendFail()
{
	$("#proceedBox").removeClass().addClass('errorSignupContent').html('Please check your email again.').show();
}

function loginFail()
{
	$("#password").val('');
	$("#proceedBox").removeClass().addClass('errorSignupContent').html('username or password incorrect!').show();
}	
	
function b2hLoginBox(){

	var strForm="<form id=\"frmSignup\" method=\"post\">";
    strForm+="<div style=\"height:50px;padding-left:10px; color:#ffffff; background-color:#2d4473\"><h3 class=\"fnLarge\">Member Login</h3></div>";
    strForm+="<div id=\"proceedBox\"></div>";
    strForm+="<div id=\"registerForm\">";
    	
        strForm+="<p><label>Your Email: </label><input type=\"text\" id=\"email\" name=\"email\" class=\"text\" /></p>";
        strForm+="<p><label>Password: </label><input type=\"password\" id=\"password\" name=\"password\" class=\"text\"/></p>";
        strForm+="<p><label>&nbsp;</label>By creating an account, I accept 361thailand Terms of Service <br />and Privacy Policy. </p>";
        strForm+="<p><label>&nbsp;</label><input type=\"button\" name=\"btnSubmit\" class=\"btnSimply\" id=\"btnSubmit\" value=\"Login\" /><input type=\"button\" name=\"btnCancel\" class=\"btnSimply\" id=\"btnCancel\" value=\"Cancel\" /></p>";
        
    strForm+="</div>";
    strForm+="<input type=\"hidden\" name=\"pid\" value=\"3449\" />";
	strForm+="</form><br/><br/>";
	modalBox(600,strForm);
	$("#btnSubmit").click(function(){
			$("#proceedBox").hide();	
			bolCheck=true;
			var email = $("input#email").val();  
			var cPass=$("input#password").val(); 
			//var dataString = 'email='+ email + '&password=' + cPass;  
			var dataString =$("#frmSignup").serialize();
			
			var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
	ga.src = 'http://www.booking2hotels.com/member/member-login-pcs.aspx?'+dataString;
	$("body").append(ga);
	
			
			

			//alert($("#frmSignup").serialize());
		});
		$("#btnCancel").click(function(){
			ModalBoxClose();
			});
}
function b2hForgotBox(){

	var strForm="<form id=\"frmForgot\" method=\"post\">";
    strForm+="<div style=\"height:50px;padding-left:10px; color:#ffffff; background-color:#2d4473\"><h3 class=\"fnLarge\">Forgot Password?</h3></div>";
    strForm+="<div id=\"proceedBox\"></div>";
    strForm+="<div id=\"registerForm\">";
    strForm+="<p><label>Your Email: </label><input type=\"text\" id=\"email\" name=\"em\" class=\"text\" /></p>";
   strForm+="<p><label>&nbsp;</label><input type=\"button\" name=\"btnForgotSubmit\" class=\"btnSimply\" id=\"btnForgotSubmit\" value=\"Submit\" /><input type=\"button\" name=\"btnCancel\" class=\"btnSimply\" id=\"btnCancel\" value=\"Cancel\" /></p>";
    strForm+="</div>";
    strForm+="<input type=\"hidden\" name=\"pid\" value=\"3449\" />";
	strForm+="</form><br/><br/>";
	modalBox(600,strForm);
	$("#btnForgotSubmit").click(function(){
		$("#proceedBox").hide();	
			bolCheck=true;
			var dataString = $("#frmForgot").serialize(); 
			
			var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
	ga.src = 'http://www.booking2hotels.com/member/MemberForgotPassword.aspx?'+dataString;
	//alert(ga.src);
	$("#modalPan").append(ga);
	
			
			

			//alert($("#frmSignup").serialize());
		});
		$("#btnCancel").click(function(){
			ModalBoxClose();
		});
}
function displayRoomDetail(opid)
{
		var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
			ga.src = 'http://www.booking2hotels.com/affiliate_include/RoomDetail.aspx?opid='+opid;
			$("body").append(ga);
}

function showdaily() {
    $(".link_show_daily_rate").click(function () {
        var Lnk = $(this);
        
       // var ss = $(this).parent().get(0).tagName;
        //.find(".div_dialy_rate_pan").stop().html();
        $(this).parent().parent().next().find(".div_dialy_rate_pan").stop().slideToggle('fast', function () {
           // alert($(this).css("display"));
            if ($(this).css("display") == "block") {
                Lnk.html("Hide");
            } else {
                
                Lnk.html("Show Daily Rate");
            }
        });
        
    return false;
    });
    
}