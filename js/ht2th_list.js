var qsDefault="";
var pageCurrent=1;
var pageSize=10;
var pageSplit=10;
var pageSplitStart=1;
var pageSplitEnd=1;
var pageSplitCurrent=1;

this.orderPage=function(){
	if(GetValueQueryString("k") !=""){
		qsDefault="k="+GetValueQueryString("k")+"&";
	}
	
	var Url = $("window").context.URL.toString();

	if(window.location.hash)
	{	
		hash=location.hash.replace('#','');
		Url=Url.replace('#','');
		Url=Url.split('?').slice(0,1);
		window.location.href=Url+hash;
	}
	
	var sortBy = GetValueQueryString("sortby");
	var page=GetValueQueryString("page");
	
	if(sortBy==""){
		sortBy=1;
	}
	
	if(page==""){
		page=1;
	}else{
		pageCurrent=page;
	}
	
	if (GetQueryStringAll() == null) {
		
		sortBy=1;
		page=1;
	}
	$('input:radio[name="order_by"]').filter('[value="'+readCookie("sortby")+'"]').attr('checked', true);
	//for loop to set click method in radio input
	//$('input[name="order_by"]').filter(function(index){	
//		$(this).click(function(){		
//			//window.location.hash = "?sortby=" + $(this).val();
//			GetContent($(this).val());
//		});
//	});
	
	
	//loadPageNav();

	
	loadRateContent(page,sortBy,"pList");
	
}


function GetContent(sortBy)
{
	hash=window.location.hash
	
	if(GetQueryStringAll()==null)
	{
	 if(hash)
	 {
	 	//page=getHashVars()["page"];
		page=1;
		window.location.hash = "?"+qsDefault+"sortby=" + sortBy+"&page="+page;
	 }else{
	 	page=1
	 	window.location.hash = "?"+qsDefault+"sortby="+sortBy+"&page="+page;
	 }
	}else{
		
		var page=GetValueQueryString("page");
		if(page==""){
			page=1;
		}
		
		window.location.hash = "?"+qsDefault+"sortby="+sortBy+"&page="+page;
	}
	pageCurrent=page;
	loadRateContent(page,sortBy,"pList");
}

function GetContentPage(page)
{
	hash=window.location.hash
	if(GetQueryStringAll()==null)
	{
		if(hash)
		{
			sortBy=getHashVars()["sortby"];
			window.location.hash = "?"+qsDefault+"sortby="+sortBy+"&page="+page;
		}else{
			sortBy=1;
			window.location.hash = "?"+qsDefault+"sortby="+sortBy+"&page="+page;
			
		}
	}else{
		var sortBy=GetValueQueryString("sortby");
		if(sortBy==""){
			sortBy=1;
		}
		window.location.hash = "?"+qsDefault+"sortby="+sortBy+"&page="+page;
	}
	loadRateContent(page,sortBy,"pList");
}



function loadPageNav()
{
	var destination = readCookie("dest");
	var location =  readCookie("loc");
	var category=  readCookie("pType");
	var keyword="";
	
	if(GetValueQueryString("k")!="")
	{
		keyword="&keyword="+GetValueQueryString("k");
	}
	
	urlPath="/vGenerator/pageNav.aspx?dest="+destination+"&loc="+location+"&cate="+category+keyword;
	alert(urlPath);
	var pageFilename = window.location.pathname;
	var sortBy=GetValueQueryString("sortby");
	if(sortBy=="")
	{
		sortBy=1;
	}
	//alert(urlPath);
	$.ajax({
		url:urlPath,
		cache: false,
		dataType: "html",
		success: function(data, textStatus, XMLHttpRequest)
		{

			$(data).insertBefore('#pList');
			//$("#"+element+"").html(data);
			$(".number_old a").each(function(){
			$(this).attr('href',pageFilename+"?"+getQueryStringInit()+"&sortby="+sortBy+"&page="+$(this).text());
			//$(this).click(function(){
			//GetContentPage($(this).text())
			//location.href=pageFilename+"?"+getQueryStringInit()+"&sortby="+sortBy+"&page="+$(this).text();
		//});
	});
		}
	});	
}
function loadRateContent(page,sortBy,element)
{

	var destination = readCookie("dest");
	var location =  readCookie("loc");
	var category=  readCookie("pType");
	var sortBy= readCookie("sortby");

	if(sortBy==null || sortBy==""){
		sortBy=1;
	}
	if(category!=29)
	{
	location=0;
	}
	var keyword=GetValueQueryString("k")
	if(keyword !="")
	{
		urlPath="/vGenerator/page/getSearchList.aspx?keyword="+keyword+"&page="+page;
		//alert(urlPath);
	}else{
		urlPath="/vGenerator/page/getContent.aspx?page="+page+"&order="+sortBy;
		urlPath=urlPath+"&dest="+destination+"&loc="+location+"&cate="+category+"&dType=1";
	}

	

	//myData="Order:"+sortBy+"<br>";
//	myData=myData+"Page:"+page;
//	$("#test").html(myData);
	//alert(urlPath);
	$("#"+element+"").html('<div align="center"><img src="/images/preloader_blue.gif"></div>');

	$.get(urlPath,function(data){
						   
			$("#"+element+"").html(data);
			
			//alert($("#"+element+"").html());
	});
	
	//alert($("#"+element+"").html());
	//$.ajax({
//		url:urlPath,
//		cache: false,
//		dataType: "html",
//		success: function(data, textStatus, XMLHttpRequest)
//		{
//			
//			$("#"+element+"").html("hello");
//			generatePage();
//			tooltip();
//		}
//	});	
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

function generatePage()
{
 var pageFilename = window.location.pathname;
 var sortBy=GetValueQueryString("sortby");
 if(sortBy=="")
 {
	 sortBy=1;
}

$(".pageNav").find("span").each(function(){
		totalData=$(this).attr("class").split("_")[1];
		totalPage=Math.ceil(totalData/pageSize);
		
		pageSplitCurrent=Math.ceil(pageCurrent/pageSplit);
		pageSplitStart=(pageSplitCurrent*pageSplit)-pageSplit+1;
		pageSplitEnd=pageSplitCurrent*pageSplit;
		
		
		if(pageSplitEnd>totalPage)
		{
			pageSplitEnd=totalPage;
		}
		
		if(pageSplitStart>1)
		{
			$(this).append('<a href="'+pageFilename+"?"+getQueryStringInit()+"&sortby="+sortBy+"&page="+(pageSplitStart-1)+'" title="'+(pageSplitStart-1)+'" class="page_previous_next">Previous</a>');
		}
		
		for(intPage=pageSplitStart;intPage<=pageSplitEnd;intPage++)
		{
			$(this).append('<a href="'+pageFilename+"?"+getQueryStringInit()+"&sortby="+sortBy+"&page="+intPage+'" title="'+intPage+'">'+intPage+'</a>');
		}
		
		if(pageSplitEnd<totalPage)
		{
			$(this).append('<a href="'+pageFilename+"?"+getQueryStringInit()+"&sortby="+sortBy+"&page="+(pageSplitEnd+1)+'" title="'+(pageSplitEnd+1)+'" class="page_previous_next">Next</a>');
		}
		$(this).find("a").each(function(){
			$(this).click(function(){
				pageCurrent=$(this).attr("title");
				pageSplitCheck=Math.ceil(pageCurrent/pageSplit);
				
				if(pageSplitCheck!=pageSplitCurrent)
				{
					$(".pageNav").find("span").find("a").remove();
					generatePage();
				}else{
					changePage();
				}
				//GetContentPage(pageCurrent)
			});
			if(($(this).attr("title"))==pageCurrent)
			{
				$(this).removeClass("pageLink").addClass("pageLinkActive");
			}else{
				$(this).addClass("pageLink");
			}
			
		});
		
	});
}

function changePage()
{
	$(".pageNav").find("span").find("a").each(function(){
		$(this).removeClass();
		if(($(this).attr("title"))==pageCurrent)
			{
				$(this).remove("pageLink").addClass("pageLinkActive");
			}else{
				$(this).addClass("pageLink");
			}
	});
}

$().ready(function(){
		
		tooltip();
		imgFloat();
		sideMenu();
        quickSearch();
//		orderPage();

	
	var dateInDefault='';
	var dateOutDefault='';
	var locDefault=0;
	var destDefault=30;
	var cateDefault=29;
	
	if(GetValueQueryString("k")==""){
		
		destDefault=$("#destDefault").val();
		locDefault=$("#locDefault").val();
		cateDefault=$("#category").val();
	}
	var pageFilename = window.location.pathname;
	
	
	if(GetQueryStringAll()!=null && GetValueQueryString("k")=="")
	{
		setCookieConfig();
	}else{
		
		createCookie("pType",cateDefault,1);
		createCookie("dest",destDefault,1);
		createCookie("loc",locDefault,1);
		createCookie("sortby",1,1);
	}
	

	//urlDefault=GetQueryStringAll();
	
	var dateCurrent=new Date();
	dayInit=(dateCurrent.getMonth()+1)+"/"+(dateCurrent.getDate()+1)+"/"+dateCurrent.getFullYear();
	
	
	var sortby=readCookie("sortby");
	
	$('input:radio[name="order_by"]').each(function(){
		$(this).click(function(){
				location.href=pageFilename+"?"+getQueryStringInit()+"&sortby="+$(this).val()+"&page=1";
		});
	});
	
	$("input[rel='datepicker']").fnDatePicker({lang : "en", date : dayInit});
	

	if(readCookie("pType"))
	{
		cateDefault=readCookie("pType");
		dateInDefault=readCookie("dateci");
		dateOutDefault=readCookie("dateco");
		//destDefault=readCookie("dest");
		//locDefault=readCookie("loc");
		
		if(GetValueQueryString("k")==""){
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
		

		
		$('input:radio[name="pType"]').filter('[value="'+cateDefault+'"]').attr('checked', true);
	}

	getXMLLocationToDropDown(cateDefault);
	orderPage();
});

function getQueryStringInit()
{
		//var arrCookie=new Array("pType","dateci","dateco","dest","loc","orderBy","cur");
	var pType=$("#category").val();
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
	if(GetValueQueryString("k")==""){
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
	}
	 
	var cur=readCookie("cur");
	if(cur==null)
	{
		cur="thb";
	}
	
	var qString="pType="+pType+"&dateci="+dateci
	if(pType==29)
	{
		qString = qString+"&dateco="+dateco;
	}
	qString = qString+"&dest="+dest+"&loc="+loc+"&cur="+cur;
	
	return qString;
}

function SearchSubmit(frm)
{
	var arrCookie=new Array("pType","dateci","dateco","dest","loc","orderBy","cur");
	var pType=29;
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
	//alert(qString);
	location.href="search_process.aspx?"+qString;
}
