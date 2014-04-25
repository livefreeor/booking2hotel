<?php
function GetDataSource($pid)
{
		
		//$dataSource="data.xml";
        //$data = 
      
        
        
        $dataSource="http://www.booking2hotels.com/affiliate_include/AffiliateFeedPromotion.aspx?pid=".$pid.'&uid=' .getRealIpAddr();
		//$dataSource="data.xml";
		$xmlSource=simplexml_load_file($dataSource);
		return RenderProductPromotion($xmlSource);
}


function getRealIpAddr()
{
    if (!empty($_SERVER['HTTP_CLIENT_IP']))   //check ip from share internet
    {
      $ip=$_SERVER['HTTP_CLIENT_IP'];
    }
    elseif (!empty($_SERVER['HTTP_X_FORWARDED_FOR']))   //to check ip is pass from proxy
    {
      $ip=$_SERVER['HTTP_X_FORWARDED_FOR'];
    }
    else
    {
      $ip=$_SERVER['REMOTE_ADDR'];
    }
    return $ip;
}

function RenderProductPromotion($xmlSource)
{
    $result="";
	$PromotionNode =  count($xmlSource->Promotion);
    
    if($PromotionNode > 0){
        foreach($xmlSource->Promotion as $dataItem){
            $result=$result."<div class=\"item_list\">";
            $result=$result."<table class=\"tbl_list\" cellpadding=\"13\" cellspacing=\"0\">";
            $result=$result."<tr><td rowspan=\"2\" style=\"width:1px;\" class=\"tag\"></td><td style=\"width:695px;\" ><p class=\"Protitle\">";
            $result=$result.$dataItem->Title;
            $result=$result."</p>";
        
            $BenefitCount =  count($dataItem->PromotionShow->List->item);
            //echo $BenefitCount. "<br\>";
                if($BenefitCount > 0)
                {
                    $result=$result."<div class=\"ac_block\">";
                    $result=$result."<p class=\"item_head\">Special Benefits:</p>";
                    $result=$result."<ul>";
                    //$count = 1;
                    foreach($dataItem->PromotionShow->List->item as $benefitItem)
                    {
                        //echo $benefitItem;
                        $result=$result."<li>".$benefitItem."</li>";
                   
                    }
                    $result=$result."</ul>";
                    $result=$result."</div>";
                    
                }
             $RoomCount =  count($dataItem->RoomTypes->Room);
             if($RoomCount > 0){
                    $result=$result."<div class=\"ac_block\">";
                    $result=$result."<p class=\"item_head\">Room type applies:</p>";
                    $result=$result."<ul>";
                    foreach($dataItem->RoomTypes->Room as $roomitem)
                    {
                        $result=$result."<li>".$roomitem->OptionTitle."</li>";
                    }
                    $result=$result."</ul>";
                    $result=$result." </div>";
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
        }
    }else{
            $result=$result."<div class=\"empty\">";
            $result=$result."<p>No any promotion is added.</p>";
            $result=$result." </div>"; 
    }
    
    
                
    
    return $result;
}

$returnHtml=GetDataSource($_GET['pid']);

echo "jQuery('.b2hPromotionResult').html('".str_replace("'","&apos;",$returnHtml)."');";
//echo "$('.b2hPromotionResult').html('HELLO');";
?>