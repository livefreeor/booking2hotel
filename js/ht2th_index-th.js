$(document).ready(function(){
		tooltip();
		sideMenu();
        quickSearch();
		
	var dateCurrent=new Date();
	dayInit=(dateCurrent.getMonth()+1)+"/"+(dateCurrent.getDate()+1)+"/"+dateCurrent.getFullYear();

	var cateDefault=29;
	var dateInDefault='';
	var dateOutDefault='';
	var destDefault=0;
	var locDefault=-1;
	
	
	$("#ht2").focus(function () {
            $(this).removeClass("ht2Bak");
            return false;
        });

        $("#ht2").blur(function () {
            $(this).addClass("ht2Bak");
            return false;
        });
	
	$('input:radio[name="pType"]').each(function(){
		$(this).click(function(){
			$("h4 span").html(getTitle($(this).val()));
			getXMLLocationToDropDown($(this).val());
		});
	});
	
	if(readCookie("pType"))
	{
	$("input[rel='datepicker']").fnDatePicker({lang : "en", date : dayInit});
		cateDefault=readCookie("pType");
		dateInDefault=readCookie("dateci");
		dateOutDefault=readCookie("dateco");
		destDefault=readCookie("dest");
		locDefault=readCookie("loc");
		if(dateInDefault!=null){
			$("#dateci").val(fnDateDisplay('en', dateInDefault));
			setDateHidden("dateci")
			if(dateOutDefault!=null){
				$("#dateco").val(fnDateDisplay('en', dateOutDefault));
				setDateHidden("dateco")
			}
			$('#dateci').trigger('change');
		}
		$('input:radio[name="pType"]').filter('[value="'+cateDefault+'"]').attr('checked', true);
	}else{
	$("input[rel='datepicker']").fnDatePicker({lang : "en", date : dayInit});
	}
	$("#textResult").html(getTitle(cateDefault));
	getXMLLocationToDropDown(cateDefault);
});

function SearchSubmit(frm)
{
	var arrCookie=new Array("pType","dateci","dateco","dest","loc","orderBy","cur");
	var pType=$("input[name='pType']:checked").val();
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
	location.href="search_process-th.aspx?"+qString;
}