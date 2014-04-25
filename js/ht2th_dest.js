// JavaScript Document
$(document).ready(function(){
		
		tooltip();
		imgFloat();
		sideMenu();
        quickSearch();
//		orderPage();

	var cateDefault=29;
	
	var dateCurrent=new Date();
	dayInit=(dateCurrent.getMonth()+1)+"/"+(dateCurrent.getDate()+1)+"/"+dateCurrent.getFullYear();
	
	$("input[rel='datepicker']").fnDatePicker({lang : "en", date : dayInit});
	


	getXMLLocationToDropDown(cateDefault);				   
});
function SearchSubmit(frm)
{
	var arrCookie=new Array("pType","dateci","dateco","dest","loc","orderBy","cur");
	var pType=29
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