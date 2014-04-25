
function isEmpty(elem, helperMsg){
	if(elem.value.length == 0){
		alert(helperMsg); elem.focus(); return false;
	}else{
		return true;
	}
}


function isNumeric(elem, helperMsg){
	var numericExpression = /^[0-9]+$/;
	if(elem.value.match(numericExpression)){
		return true;
	}else{
		alert(helperMsg); elem.focus(); return false;
	}
}


function isAlphabet(elem, helperMsg){
	var alphaExp = /^[a-zA-Z]+$/;
	if(elem.value.match(alphaExp)){
		return true;
	}else{
		alert(helperMsg); elem.focus(); return false;
	}
}


function isAlphanumeric(elem, helperMsg){
	var alphaExp = /^[0-9a-zA-Z]+$/;
	if(elem.value.match(alphaExp)){
		return true;
	}else{
		alert(helperMsg); elem.focus(); return false;
	}
}

function isArray(elem) {
   if (elem.constructor == Array){
      return true;
   }else{
      return false;
   }
}


function checkZero(elem, helperMsg){
	if(elem.value <= 0){
		elem.value=elem.value*(-1);
		alert(helperMsg); elem.focus(); return false;
	}else{
		return true;
	}
}

function lengthRestriction(elem, descript, min, max){
	var uInput = elem.value;
	if(uInput.length >= min && uInput.length <= max){
		return true;
	}else{
		alert("Insert"+descript+"Lenght "+min+" - " +max+ " Letters"); elem.focus(); return false;
	}
}


function madeSelection(elem, helperMsg){
	if(elem.value == "0"){
		alert(helperMsg); elem.focus(); return false;
	}else{
		return true;
	}
}


function emailValidator(elem, helperMsg){
	var emailExp = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
	if(elem.value.match(emailExp)){
		return true;
	}else{
		alert(helperMsg); elem.focus(); return false;
	}
}


function PopUpWindow(theURL,winName,features){
	window.open(theURL,winName,features);
}

function swapState(i){
	var cacheobj=document.getElementById("innermenu"+i).style
	if (cacheobj.visibility=="visible") {
		cacheobj.visibility="hidden";
		//document.getElementById('pictures'+i).src="/images/plus.gif";	
		document.getElementById('close_detail'+i).innerHTML="More detail...";
		cacheobj.display='none';	
	}
	else {
		cacheobj.visibility="visible";
		//document.getElementById('pictures'+i).src="/images/minus.gif";
		document.getElementById('close_detail'+i).innerHTML="Close detail ";
		cacheobj.display='';	
	}
}
