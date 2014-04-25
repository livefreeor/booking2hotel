// JavaScript Document
$(document).ready(function(){
						   tooltip();
		imgFloat();
		sideMenu();
        quickSearch();
	setBoxSearhInitial()
	loadProductFeature();
	loadImageAds();
	 $("#ht2").focus(function () {
            $(this).removeClass("ht2Bak");
            return false;
        });

        $("#ht2").blur(function () {
            $(this).addClass("ht2Bak");
            return false;
        });
});

function loadImageAds()
{
	var urlPath="/vGenerator/recommendAds.aspx?dest="+$("#destDefault").val()+"&tpage="+$("#page_type").val();
	
	$("#wat_prakaew").html('<div align="center"><img src="/images/preloader_blue.gif"></div>');
	//alert(urlPath);
	$.ajax({
		url:urlPath,
		cache: false,
		dataType: "html",
		success: function(data, textStatus, XMLHttpRequest)
		{
			$("#wat_prakaew").html(data);
		}
	});	
}

function loadProductFeature()
{
var destination = $("#destDefault").val();

	var location =0;
	var category= $("#category").val();
	var langID=$("#ln").val();

	var urlPath="/vGenerator/page/getContent.aspx?page=1&order=8";
	urlPath=urlPath+"&dest="+destination+"&loc="+location+"&cate="+category+"&dType=2&ln="+langID;
	
	$("#content_left").html('<div align="center"><img src="/images/preloader_blue.gif"></div>');
	//alert(urlPath);
	$.ajax({
		url:urlPath,
		cache: false,
		dataType: "html",
		success: function(data, textStatus, XMLHttpRequest)
		{
			$("#content_left").html(data);
			tooltip();
		}
	});	
	
	$("#popular_list").html('<div align="center"><img src="/images/preloader_blue.gif"></div>');
	urlPath="/vGenerator/page/getContent.aspx?page=1&order=1";
	urlPath=urlPath+"&dest="+destination+"&loc="+location+"&cate="+category+"&dType=3&ln="+langID;
	$.ajax({
		url:urlPath,
		cache: false,
		dataType: "html",
		success: function(data, textStatus, XMLHttpRequest)
		{
			$("#popular_list").html(data);
			tooltip();
		}
	});		
}

function setBoxSearhInitial()
{
	var dateCurrent=new Date();
	dayInit=(dateCurrent.getMonth()+1)+"/"+(dateCurrent.getDate()+1)+"/"+dateCurrent.getFullYear();
	
	var cateDefault=$("#category").val();
	var dateInDefault='';
	var dateOutDefault='';
	var destDefault=$("#destDefault").val();
	var locDefault=0;
	//alert(destDefault);
	createCookie("pType",cateDefault,1);
	createCookie("dest",destDefault,1);
	createCookie("loc",locDefault,1);
	$("input[rel='datepicker']").fnDatePicker({lang : "en", date : dayInit});

	if(readCookie("pType"))
	{
		dateInDefault=readCookie("dateci");
		dateOutDefault=readCookie("dateco");
		if(dateInDefault!=null){
			$("#dateci").val(fnDateDisplay('en', dateInDefault));
			setDateHidden("dateci")
			
			if(dateOutDefault!=null){
				
				$("#dateco").val(fnDateDisplay('en', dateOutDefault));
				setDateHidden("dateco")
			
			}
			
			$('#dateci').trigger('change');
		}
	}

	getXMLLocationToDropDown(cateDefault);
	
}

function SearchSubmit(frm)
{
	var arrCookie=new Array("pType","dateci","dateco","dest","loc","orderBy","cur");
	var pType=$("#category").val()
	var dateci=$("#Hddateci").val();
	var dateco="";
	
	var dest=$("#dest").val();
	if(dest=="0")
	{
		alert("Please select your destination");
		return false;
	}
	var loc="0";
	if($("#loc").length){
		loc=$("#loc").val();
	}
	
	if(dateci=="")
	{
			alert("Please check your date");
			return false;
	}
	if($("#Hddateco").length)
	{
		dateco=$("#Hddateco").val();
		if(dateco=="")
		{
			alert("Please check your date");
			return false;
		}
	}
	
	 
	var cur=readCookie("cur");
	
	if(cur==null || cur=="")
	{
		cur="25";
	}
	
	var qString="pType="+pType+"&dateci="+dateci
	if(pType==29)
	{
		qString = qString+"&dateco="+dateco;
	}
	qString = qString+"&dest="+dest+"&loc="+loc+"&cur="+cur;
	
	location.href="search_process-th.aspx?"+qString;
}