// JavaScript Document
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