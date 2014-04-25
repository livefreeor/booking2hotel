$(document).ready(function(){

var langID=$("#ln").val();
	 $.get("/ipCountry.aspx",function(data){
	 
	 	if(data!=null){
			if(data=="208" && langID==1)
			{
	
				pathFile=window.location.pathname;
				var popDetail="";
				 if(readCookie("siteLn")==null)
				 {
					popDetail=popDetail+"<div id=\"page\">";
					popDetail=popDetail+"<div id=\"popupLang\">";
					popDetail=popDetail+"<table width=\"550\" style=\"margin:0px 20px;\" id=\"selLang\">";
					popDetail=popDetail+"<tr><td colspan=\"2\" style=\"border-bottom:1px solid #D7D7D7; padding-bottom:10px;\"><img src=\"/images/popupLogo.jpg\" class=\"imgLogo\" /><br /></td></tr>";
					popDetail=popDetail+"<tr>";
					popDetail=popDetail+"<td style=\"font-size:12px; font-family:Verdana, Geneva, sans-serif\">";
					popDetail=popDetail+"<br />";
					popDetail=popDetail+"Are you aiming to use Thai Version?  We also have Thai sites to offer where ";
					popDetail=popDetail+"you can get the prices & promotions tailored just for you.";
					popDetail=popDetail+"</td>";
					popDetail=popDetail+"</tr>";
					popDetail=popDetail+"<tr><td align=\"center\" style=\"padding:20px 0px; text-align:center;\">";
					popDetail=popDetail+"<a href=\"javascript:void(0)\" id=\"enLang\"><img src=\"/images/en_flag_b.gif\" class=\"boxShadow\" /></a><a href=\"javascript:void(0)\" id=\"thLang\"><img src=\"/images/th_flag_b.gif\" style=\"margin-left:40px;\" class=\"boxShadow\" /></a>";
					popDetail=popDetail+"</td></tr>";
					popDetail=popDetail+"<tr><td align=\"right\">";
					popDetail=popDetail+"<a \"javascript:void(0)\" id=\"clShow\">Don\'t show again</a>";
					popDetail=popDetail+"</td></tr>";
					popDetail=popDetail+"</table>";
					popDetail=popDetail+"</div>";
					popDetail=popDetail+"</div>";
					$("body").append(popDetail);
					
					DarkmanPopUp_front(590,$("#popupLang").html());
					$("#thLang").click(function()
				 {
				 
					createCookie("siteLn",2,30);
					if(langID==2)
					{
						DarkmanPopUp_Close_front();
					}else{
					
						urlFull="http://thai.hotels2thailand.com"+pathFile.replace(".asp","-th.asp");
						
						location.href=urlFull;
					}
				 });
				 $("#enLang").click(function()
				 {
					createCookie("siteLn",1,30);
					DarkmanPopUp_Close_front();
				 });
				 
				 $("#clShow").click(function()
				 {
					createCookie("siteLn",1,30);
					
					DarkmanPopUp_Close_front();
				 });
				 }
				 
				 
				}
		
			}

	}); 
});
