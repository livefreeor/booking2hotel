var map;
var latlng;
var markersArray = [];

  function generateMap(strLat,strLng) {
  
    latlng = new google.maps.LatLng(strLat,strLng);
	
    var myOptions = {
      zoom: 16,
      center: latlng,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
	
	//###Start Create Map
    	map = new google.maps.Map(document.getElementById("map_canvas"),myOptions);
	//###End Create Map
	
	
    addMarker(latlng);
	
	markersArray[0].trigger("click");


  }
  
function addMarker(location)
{
var image = '../images/motel-2.png';

		var marker = new google.maps.Marker({
		  position: location,
		  map: map,
		  icon:image
		});
		
		var contentInfo='<div class="mapPan">'+document.getElementById("mapContent").innerHTML+'</div>';

		var markerInfo=new google.maps.InfoWindow({content:contentInfo});

		markerInfo.open(map,marker);



		google.maps.event.addListener(marker,'click',function(){
			markerInfo.open(map,marker);
		});
		markersArray.push(marker);
		
}

function removeMark()
{
	for (i in markersArray) {
      markersArray[i].setMap(null);
    }

}

function showOverlays() {
  if (markersArray) {
    for (i in markersArray) {
      markersArray[i].setMap(map);
    }
  }
}

function clearMarker()
{
	if(markersArray)
	{
		for(i in markersArray)
		{
			markersArray[i].setMap(null);
		}
	}
	markersArray.length=0;
}
