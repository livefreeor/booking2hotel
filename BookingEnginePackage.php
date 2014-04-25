<?php
  header('Content-Type:  application/javascript; charset=UTF-8');
function GetDataSource($pid)
{
		
		//$dataSource="data.xml";
        //$data = 
      
        
        
        $dataSource="http://www.booking2hotels.com/affiliate_include/AffiliateFeedPackage.aspx?pid=".$pid;
		//$dataSource="data.xml";
		$xmlSource=simplexml_load_file($dataSource);
		return RenderProductPackage($xmlSource);
}




function RenderProductPackage($xmlSource)
{
    $result="";
	
    $count = 1;
    $PackageNode =  count($xmlSource->Package);
    if($PackageNode > 0){
        foreach($xmlSource->Package as $dataItem){
            $result=$result."<div class=\"item_list\">";
            $result=$result."<table class=\"tbl_list\" cellpadding=\"13\" cellspacing=\"0\">";
            $result=$result."<tr><td rowspan=\"2\" style=\"width:1px;\" class=\"tag\"></td><td style=\"width:695px;\" ><p class=\"Protitle\">";
            $result=$result.$dataItem->Title;
            $result=$result."</p>";
            if($dataItem->Detail != ""){
                $result=$result."<a href=\"\" onclick=\"ShowDetailPack('detail_".$count."');return false;\" class=\"show_detail\"  >Show detail>></a>";
                $result=$result."<div style=\"display:none;\" id=\"detail_".$count."\" class=\"packdetail\">";
                $result=$result.$dataItem->Detail;
                //$result=$result."HELLO";
                $result=$result."</div>";
            }
        
            $result=$result."</td></tr>";
            $result=$result."<tr>";
            $result=$result." <td valign=\"top\" style=\"width:799px;\">"; 
        
            //$BookingStart = new DateTime();
            //$BookingStart = $dataItem->Date_book_start;
        
            $BookingStart = new DateTime($dataItem->Date_book_start); 
            $BookingEnd = new DateTime($dataItem->Date_book_end); 
            $StayStart = new DateTime($dataItem->Date_stay_start); 
            $StayEnd = new DateTime($dataItem->Date_stay_end); 
        
            $result=$result."<p class=\"bookDate\">Book now until <span class=\"span_date\">".$BookingEnd->format('j M Y')."</span></p>";
            $result=$result."<p class=\"stayDate\">Period stay is applicable from <span class=\"span_date\">".$StayStart->format('j M Y')." - ".$StayEnd->format('j M Y')."</span></p>";
            //$result=$result."<!--<a class=\"linkDetail\" href=\"#\">More Detail>></a>-->";
            $result=$result."</td>";
            $result=$result." </tr>";
            $result=$result."</table>";
            $result=$result." </div>"; 
        
            $count = $count +1 ;
        }
    }else{
            $result=$result."<div class=\"empty\">";
            $result=$result."<p>No any package is added. </p>";
            $result=$result." </div>"; 
    }
    
    
                
    
    return $result;
}

$returnHtml=GetDataSource($_GET['pid']);

echo "jQuery('.b2hPromotionResult').html('".str_replace("'","&apos;",$returnHtml)."');";
//echo "$('.b2hPromotionResult').html('HELLO');";
?>