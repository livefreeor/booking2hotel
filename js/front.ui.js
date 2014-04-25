
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
	
	if (GetQueryStringAll() != null) {
		
		$('input[name="order_by"]').filter(
		function(index){
			return $(this).attr("value") == sortBy 
		}).attr('checked', true);
		
	}else{
		sortBy=1;
		page=1;
	}
	
	//for loop to set click method in radio input
	$('input[name="order_by"]').filter(function(index){	
		$(this).click(function(){		
			//window.location.hash = "?sortby=" + $(this).val();
			GetContent($(this).val());
		});
	});
	
	
	//loadPageNav();

	

		if ($("#category").val() != 29) {
			$("#ddLocation").hide();
			$("#btntest").attr("value", "Search For " + ProductTitle($("#category").val()));
		}else {
			$("#ddLocation").show();
			$("#btntest").attr("value", "Search For Hotels");
        }
	 
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
	var destination = $("#destDefault").val();
	var location = $("#locDefault").val();
	var category= $("#category").val();
	var keyword="";
	
	if(GetValueQueryString("k")!="")
	{
		keyword="&keyword="+GetValueQueryString("k");
	}
	
	urlPath="/vGenerator/pageNav.aspx?dest="+destination+"&loc="+location+"&cate="+category+keyword;
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
			$(this).attr('href','javascript:void(0)');
			$(this).click(function(){
			GetContentPage($(this).text())
		});
	});
		}
	});	
}
function loadRateContent(page,sortBy,element)
{
	var destination = $("#destDefault").val();
	var location = $("#locDefault").val();
	var category= $("#category").val();
	
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
	$.ajax({
		url:urlPath,
		cache: false,
		dataType: "html",
		success: function(data, textStatus, XMLHttpRequest)
		{
			$("#"+element+"").html(data);
			generatePage();
			tooltip();
		}
	});	
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
			$(this).append('<a href="javascript:void(0)" title="'+(pageSplitStart-1)+'" class="page_previous_next">Previous</a>');
		}
		
		for(intPage=pageSplitStart;intPage<=pageSplitEnd;intPage++)
		{
			$(this).append('<a href="javascript:void(0)" title="'+intPage+'">'+intPage+'</a>');
		}
		
		if(pageSplitEnd<totalPage)
		{
			$(this).append('<a href="javascript:void(0)" title="'+(pageSplitEnd+1)+'" class="page_previous_next">Next</a>');
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
				GetContentPage(pageCurrent)
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