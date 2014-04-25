/*Include Local Scroll 1.2.7 + Scroll To 1.4.2*/
;(function($){var l=location.href.replace(/#.*/,'');var g=$.localScroll=function(a){$('body').localScroll(a)};g.defaults={duration:1e3,axis:'y',event:'click',stop:true,target:window,reset:true};g.hash=function(a){if(location.hash){a=$.extend({},g.defaults,a);a.hash=false;if(a.reset){var e=a.duration;delete a.duration;$(a.target).scrollTo(0,a);a.duration=e}i(0,location,a)}};$.fn.localScroll=function(b){b=$.extend({},g.defaults,b);return b.lazy?this.bind(b.event,function(a){var e=$([a.target,a.target.parentNode]).filter(d)[0];if(e)i(a,e,b)}):this.find('a,area').filter(d).bind(b.event,function(a){i(a,this,b)}).end().end();function d(){return!!this.href&&!!this.hash&&this.href.replace(this.hash,'')==l&&(!b.filter||$(this).is(b.filter))}};function i(a,e,b){var d=e.hash.slice(1),f=document.getElementById(d)||document.getElementsByName(d)[0];if(!f)return;if(a)a.preventDefault();var h=$(b.target);if(b.lock&&h.is(':animated')||b.onBefore&&b.onBefore.call(b,a,f,h)===false)return;if(b.stop)h.stop(true);if(b.hash){var j=f.id==d?'id':'name',k=$('<a> </a>').attr(j,d).css({position:'absolute',top:$(window).scrollTop(),left:$(window).scrollLeft()});f[j]='';$('body').prepend(k);location=e.hash;k.remove();f[j]=d}h.scrollTo(f,b).trigger('notify.serialScroll',[f])}})(jQuery);
(function(c){var a=c.scrollTo=function(f,e,d){c(window).scrollTo(f,e,d)};a.defaults={axis:"xy",duration:parseFloat(c.fn.jquery)>=1.3?0:1};a.window=function(d){return c(window)._scrollable()};c.fn._scrollable=function(){return this.map(function(){var e=this,d=!e.nodeName||c.inArray(e.nodeName.toLowerCase(),["iframe","#document","html","body"])!=-1;if(!d){return e}var f=(e.contentWindow||e).document||e.ownerDocument||e;return c.browser.safari||f.compatMode=="BackCompat"?f.body:f.documentElement})};c.fn.scrollTo=function(f,e,d){if(typeof e=="object"){d=e;e=0}if(typeof d=="function"){d={onAfter:d}}if(f=="max"){f=9000000000}d=c.extend({},a.defaults,d);e=e||d.speed||d.duration;d.queue=d.queue&&d.axis.length>1;if(d.queue){e/=2}d.offset=b(d.offset);d.over=b(d.over);return this._scrollable().each(function(){var l=this,j=c(l),k=f,i,g={},m=j.is("html,body");switch(typeof k){case"number":case"string":if(/^([+-]=)?\d+(\.\d+)?(px|%)?$/.test(k)){k=b(k);break}k=c(k,this);case"object":if(k.is||k.style){i=(k=c(k)).offset()}}c.each(d.axis.split(""),function(q,r){var s=r=="x"?"Left":"Top",u=s.toLowerCase(),p="scroll"+s,o=l[p],n=a.max(l,r);if(i){g[p]=i[u]+(m?0:o-j.offset()[u]);if(d.margin){g[p]-=parseInt(k.css("margin"+s))||0;g[p]-=parseInt(k.css("border"+s+"Width"))||0}g[p]+=d.offset[u]||0;if(d.over[u]){g[p]+=k[r=="x"?"width":"height"]()*d.over[u]}}else{var t=k[u];g[p]=t.slice&&t.slice(-1)=="%"?parseFloat(t)/100*n:t}if(/^\d+$/.test(g[p])){g[p]=g[p]<=0?0:Math.min(g[p],n)}if(!q&&d.queue){if(o!=g[p]){h(d.onAfterFirst)}delete g[p]}});h(d.onAfter);function h(n){j.animate(g,e,d.easing,n&&function(){n.call(this,f,d)})}}).end()};a.max=function(j,i){var h=i=="x"?"Width":"Height",e="scroll"+h;if(!c(j).is("html,body")){return j[e]-c(j)[h.toLowerCase()]()}var g="client"+h,f=j.ownerDocument.documentElement,d=j.ownerDocument.body;return Math.max(f[e],d[e])-Math.min(f[g],d[g])};function b(d){return typeof d=="object"?d:{top:d,left:d}}})(jQuery);

function DisplayRate()
	{
		$("#RoomPeriod").html('<div id="preload_rate" align="center"><img src="/images/preloader_blue.gif"></div>');
		var pid=$("#productDefault").val();
		var cat_id=$("#category").val();
		//alert("/rate_new.aspx?pid="+pid+"&cat_id="+cat_id);
		$.ajax({
			url:"/rate_new.aspx?pid="+pid+"&cat_id="+cat_id+"&ln=1",
			cache: false,
			dataType: "html",
			success: function(data, textStatus, XMLHttpRequest)
			{
				$("#RoomPeriod").html(data);
				tooltip();
				lightboxFrame();
				setEventToDropDown();
				setDropDownCurrency();
			}
		});	
	}
	function checkRate(productID)
	{

		if($("#HddateStart2ci").val()==""){
			alert('You must select date before.');
			return false;
		}else{
			$("#RoomPeriod").html('<div id="preload_rate" align="center"><img src="/images/preloader_blue.gif"></div>');
			$.get("/changDateCheck.aspx?dayIn="+$("#HddateStart2ci").val()+"&dayOut="+$("#HddateStart2co").val(), function(data)
			
			{
				if(data=="success")
				{	
				//loadRateContent(productID,'RoomPeriod')
				DisplayRate();
				}
			});
		}
	}

	function changCurrency(curID)
	{
	
		$.get("/vGenerator/test_set_session.aspx?cur="+curID, function(data)		
		{
			if(data=="success")
			{	
				DisplayRate();
			}
		});
	}
	
	//Check Max Adult
	
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

	if(selAdult>(roomMaxAdult+sumExtraBed))
	{
			$.scrollTo("#errorRoom", 800, {offset: {left: 0, top: -200}, onAfter: function () {
			$('<div></div>').html("Exceed adult, please select extra room or extrabed. Please click here to see room defination.").dialog({ modal: true, title: 'Oops! there was an error with your submission', width: 400, buttons: { Ok: function() { $(this).dialog('close') } } });
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
			$('<div></div>').html("Exceed airport transfer policy, Please select more car.").dialog({ modal: true, title: 'Oops! there was an error with your submission', width: 400, buttons: { Ok: function() { $(this).dialog('close') } } });
			}});
			return false;
		}
	}
	}else{
		if((roomMaxAdult+roomMaxChild)==0){
		$.scrollTo("#errorRoom", 800, {offset: {left: 0, top: -200}, onAfter: function () {
			$('<div></div>').html("Please select option before.").dialog({ modal: true, title: 'Oops! there was an error with your submission', width: 400, buttons: { Ok: function() { $(this).dialog('close') } } });
		}});
		return false;
		}
		}
	
	
	$("#FormBooking").submit();
}

function setDropDownCurrency()
{
	$("#currencyBox").change(function(){
		$("#currencyDisplay").val($(this).attr("value"));
		changCurrency($(this).attr("value"));
	});
	
	$("#currencyBox option").each(function(){
		$(this)
		.css('background-image','url(/images/currency/flag_'+$(this).attr("value")+'.jpg)')
		.css('background-repeat','no-repeat');
		if($("#currencyDisplay").val()==$(this).attr("value"))
		{
			$(this).attr("selected","selected");
			$("#flag_currency").attr("src","/images/currency/flag_"+$(this).attr("value")+".jpg");
		}
	});
}

function showBoxDateSearch()
{
	$(".chack_rate").show();
}