

this.tooltip = function(){	
	
		xOffset = 10;
		yOffset = 20;		

		
	$("a.tooltip").hover(function(e){											  
		//this.t = this.title;
		//alert(this.id);
		
		//this.t = $("#policyContent_"+this.id).html();
		ptext=$("#policyContent_"+this.id).html();
		if(ptext!="")
		{
			$("body").append("<div id='tooltip'>"+ptext+"</div>");
		//alert($("#tooltip").html());
		$("#tooltip")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left",(e.pageX + yOffset) + "px")
			.fadeIn(100);
		}
				
    },
	function(){
			$("#tooltip").remove();		
    });
	
	$("a.tooltip").mousemove(function(e){
		$("#tooltip")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left",(e.pageX + yOffset) + "px");
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


	
