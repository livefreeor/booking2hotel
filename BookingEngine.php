<?php
ob_start();
@session_start();
//error_reporting(0);
?>
<?php

function getRealIpAddr()
{
    //if (!empty($_SERVER['HTTP_CLIENT_IP']))   //check ip from share internet
    //{
    //  $ip=$_SERVER['HTTP_CLIENT_IP'];
    //}
    //elseif (!empty($_SERVER['HTTP_X_FORWARDED_FOR']))   //to check ip is pass from proxy
    //{
    //  $ip=$_SERVER['HTTP_X_FORWARDED_FOR'];
    //}
    //else
    //{
    //  $ip=$_SERVER['REMOTE_ADDR'];
    //}
    //return $ip;
     $client  = @$_SERVER['HTTP_CLIENT_IP'];
    $forward = @$_SERVER['HTTP_X_FORWARDED_FOR'];
    $remote  = $_SERVER['REMOTE_ADDR'];

    if(filter_var($client, FILTER_VALIDATE_IP))
    {
        $ip = $client;
    }
    elseif(filter_var($forward, FILTER_VALIDATE_IP))
    {
        $ip = $forward;
    }
    else
    {
        $ip = $remote;
    }

    return $ip;
}

function GenDropDownQuantity($strName,$optionValue,$minNum,$maxNum,$numDefault,$displayType)
{
    $result="";
    switch($displayType)
    {
        case 1:
             $className = "class=\"ddPrice\"";
             break;
        case 2:
             $className = "class=\"ddPriceExtraBed\"";
             break;
        case 3:
             $className = "class=\"ddTransfer\"";
             break;
         case 4:
             $className = "class=\"ddPackage\"";
             break;
    }
    if ($displayType!=0)
    {
        $result = "<select name=\"" . $strName . "\" " . $className . " > ";
     }else{
        $result = "<select name=\"" . $strName . "\" class=\"". $strName . "\" id=\"" . $strName . "\"> ";
     }
     if($displayType!=0)
            {
                for ($countNum = $minNum; $countNum <= $maxNum; $countNum++)
                {
                    $result = $result . "<option value=\"" . $optionValue . "_" . $countNum . "\">" . $countNum . "</option> ";
                }
            }else{
                for ($countNum = $minNum; $countNum <= $maxNum; $countNum++)
                {
                    $result = $result . "<option value=\"" . $countNum . "\">" . $countNum . "</option> ";
                }
            }
            
            $result = $result . "</select> ";
            return $result;
}

function GenPricingtable($xmlSource,$pid)
{
    if($xmlSource->CountryID[0]=="208" && $pid=="592")
    {
        $result=$result."<div class=\"rateOptionList-outer\"> ";
        $result=$result."<br/><center><font style=\"font-size:14px; color:#333333;\">If you wish to make a reservation at Pathumwan Princess Hotel, you can go to <a href=\"http://pprincess.com\">pprincess.com</a></font></center><br/>";
        $result=$result."</div> ";
    }else{
        $result=$result.RenderCurrencyBox($xmlSource,$_SESSION['currencyID']);
        $result=$result."<div id=\"errorRoom\" class=\"errorMsg\"></div> ";
        $result=$result."<form id=\"FormBooking\" action=\"http://www.booking2hotels.com/book.aspx\" method=\"post\"> ";
        $result=$result.GenTableOption($xmlSource,$pid);
        $result=$result."<input type=\"hidden\" name=\"sid\" value=\"".$xmlSource->SupplierID[0]."\" /> ";
        $result=$result."<input type=\"hidden\" id=\"discount\" name=\"discount\" value=\"".$xmlSource->Discount[0]."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"hotel_id\" value=\"".$pid."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"cat_id\" value=\"29\" /> ";
        $result=$result."<input type=\"hidden\" name=\"date_start\" value=\"".$_SESSION['dateStart']."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"date_end\" value=\"".$_SESSION['dateEnd']."\" /> ";
        $result=$result."<input type=\"hidden\" id=\"currencyDisplay\" value=\"".$_SESSION['currencyID']."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"ln\" value=\"1\"/> ";
        $result=$result."<input type=\"hidden\" name=\"refCountry\" value=\"".$xmlSource->CountryID[0]."\"/> ";
        $result=$result."<input type=\"hidden\" name=\"mm\" value=\"".$xmlSource->IsMember[0]."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"mmid\" value=\"".$xmlSource->MemberID[0]."\" /> ";
        $result=$result."</form>";  
    }
    return $result;
}
function GenPricingtableBak($xmlSource,$pid)
{
    if($xmlSource->CountryID[0]=="208" && $pid=="592")
    {
        $result=$result."<div class=\"rateOptionList-outer\"> ";
        $result=$result."<br/><center><font style=\"font-size:14px; color:#333333;\">If you wish to make a reservation at Pathumwan Princess Hotel, you can go to <a href=\"http://pprincess.com\">pprincess.com</a></font></center><br/>";
        $result=$result."</div> ";
    }else{
        $result=$result.RenderCurrencyBox($xmlSource,$_SESSION['currencyID']);
        $result=$result."<div id=\"errorRoom\" class=\"errorMsg\"></div> ";
        $result=$result."<form id=\"FormBooking\" action=\"http://www.booking2hotels.com/book.aspx\" method=\"post\"> ";
        $result=$result.GenTableOption($xmlSource,$pid);
        $result=$result."<input type=\"hidden\" name=\"sid\" value=\"".$xmlSource->SupplierID[0]."\" /> ";
        $result=$result."<input type=\"hidden\" id=\"discount\" name=\"discount\" value=\"".$xmlSource->Discount[0]."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"hotel_id\" value=\"".$pid."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"cat_id\" value=\"29\" /> ";
        $result=$result."<input type=\"hidden\" name=\"date_start\" value=\"".$_SESSION['dateStart']."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"date_end\" value=\"".$_SESSION['dateEnd']."\" /> ";
        $result=$result."<input type=\"hidden\" id=\"currencyDisplay\" value=\"".$_SESSION['currencyID']."\" /> ";
        $result=$result."<input type=\"hidden\" name=\"ln\" value=\"1\"/> ";
        $result=$result."<input type=\"hidden\" name=\"refCountry\" value=\"".$xmlSource->CountryID[0]."\"/> ";
        $result=$result."</form>";  
    }
    return $result;
}

/*update renderprice*/
function GenTableOption($xmlSource,$pid)
{
    
    $discount=0;
    $result="";
    $rateRack=0;
    $rateDisplay=0;
    $rowCount=1;
    $hasRoomRate=false;
    $roomTotal=0;
    //echo "session = " . $_SESSION['currencyCode'];
        ///echo "session = ". $_SESSION['currencyID'];
    $currencyCode=$_SESSION['currencyCode'];
    
    $result=$result.GenTablePackageOption($xmlSource)."<br/>";
    
    
    $OptionCount = count($xmlSource->Options->Option);
    if($OptionCount > 0){
        $result=$result."<div class=\"rateOptionList-outer\">";
        $result=$result."<table id=\"rateOptionList\">";
        $result=$result."<tr><td></td></tr> ";
        $result=$result."<tr><th>Room Type</th><th width=\"180\">Conditions</th><th width=\"130\">Avg./Night</th><th width=\"100\">No. Rooms</th></tr> ";
        $result=$result."<tr><td colspan=\"4\" style=\"border-bottom:2px solid #d7d7d7\"></td></tr>";
    }
    
        
       
        
    foreach($xmlSource->Options->Option as $option)
    {
        $hasRoomRate=true;
        $resultRow="";
        //echo $option->PriceRack . "-----". (float)$_SESSION['currencyPrefix'];
        if((float)$_SESSION['currencyPrefix'] > 0)
        {
            $rateRack=  $option->PriceRack/(float)$_SESSION['currencyPrefix'];
            $rateDisplay= $option->Price/(float)$_SESSION['currencyPrefix'];
        }else
        {
            $rateRack=  (string)$option->PriceRack;
            $rateDisplay= (string)$option->Price;
        }
      
        if($rowCount>4)
        {
            $resultRow=$resultRow."<tr class=\"tr-price-hidden\"><td valign=\"top\"> ";
        }else{
            $resultRow=$resultRow."<tr><td valign=\"top\">";
        }
        if($option->OptionImage!="http://www.booking2hotels.com")
        {
            $resultRow=$resultRow."<img src=\"".html_entity_decode($option->OptionImage)."\" class=\"optionImg\"/> ";
        }
        
        if($option->ShowRoomDetail=="true")
        {
            $resultRow=$resultRow."<span class=\"optionTitle\"><a href=\"javascript:void(0)\" onclick=\"displayRoomDetail(".$option->attributes()->id.")\">".$option->OptionTitle."</a></span> ";
        }else{
            $resultRow=$resultRow."<span class=\"optionTitle\">".$option->OptionTitle."</span> ";   
        }
        
        
        $resultRow=$resultRow."<span class=\"promotionTitle\">".$option->PromotionTitle."</span> ";
        $resultRow=$resultRow."</td><td valign=\"top\"> ";
        $resultRow=$resultRow.str_replace("\n","",$option->ConditionDetail);
        $resultRow=$resultRow."<span class=\"optionAdult\">Max Adults: ".$option->MaxAdult."</span> ";
        $resultRow=$resultRow."<a href=\"javascript:void(0)\" class=\"tooltip\"><span class=\"conditionFloat\">View Conditions</span><span class=\"tooltip_content\">".$option->PolicyContent."</span></a> ";
        $resultRow=$resultRow."</td><td align=\"right\"> ";
        $discount=number_format((($rateRack-$rateDisplay)/$rateRack)*100);
        if((int)$option->Price!=(int)$option->PriceRack)
        {
            $resultRow=$resultRow."<span class=\"rackRate\">".$currencyCode." ".number_format($rateRack)."</span><br /> ";
            $resultRow=$resultRow."<span class=\"rackOwn\">".$currencyCode." ".number_format($rateDisplay)."</span><br /> ";
            if($option->MemberBenefit!="")
            {

                $resultRow=$resultRow." <a href=\"javascript:void(0)\" class=\"tooltip\"><span class=\"memberBenefit \">Special price for member</span><span class=\"tooltip_content\" style=\"width:160px\">".$option->MemberBenefit."</span></a><br /> ";
            }else{

                $resultRow=$resultRow." <span class=\"savePrice\">Save ".$discount."%</span><br /> ";
            }
            
        }else{
            $resultRow=$resultRow."<span class=\"rackOwn\">".$currencyCode." ".number_format($rateDisplay)."</span><br /> ";
        }
        

        if($option->RoomAvailable=="true")
            {
                $resultRow=$resultRow."<span class=\"roomAval\">Limited room available</span> ";
            }
            
        $resultRow=$resultRow."<a href=\"#\" class=\"link_show_daily_rate\" >Show Daily Rate</a>";
        $resultRow=$resultRow."</td><td align=\"center\">".GenDropDownQuantity("ddPrice",$option->ConditionValue,0,20,0,1)."</td></tr> ";
        
        /// Extend Daily Rate by darkman 
        
        if($rowCount>4)
        {
         $resultRow = $resultRow . "<tr class=\"tr-price-hidden\" style=\"margin:0px;padding:0px;\"><td colspan=\"4\" style=\"margin:0px;padding:0px;\">";
         }else{
         $resultRow = $resultRow . "<tr  style=\"margin:0px;padding:0px;\"><td colspan=\"4\" style=\"margin:0px;padding:0px;\">";
         }
         
         
             $resultRow = $resultRow . "<div class=\"div_dialy_rate_pan\" style=\"display:none;\">";
             $resultRow = $resultRow ."<table class=\"tbl_dialy_rate\"  cellpadding=\"0\" cellspacing=\"0\">";
            
             
             $arrDayofWeek = array("Sun","Mon","Tue","Wed","Thu","Fri","Sat");
             
             
             $resultRow = $resultRow . "<tr>";
             $intheadCount = 0;
              
             //$PerdayCount = $option->PricePerdays->PricePerday->count();
             $PerdayCount =  count($option->PricePerdays->PricePerday);
             //$PerdayCount =3;
             
             $dateCheck ;
             $NumDay = 0;
            foreach($option->PricePerdays->PricePerday as $priceperday)
            {
                //echo $priceperday->count;
                $dateCheck = new DateTime($priceperday->dm_date); 
                $NumDay = $dateCheck->format('N');
                if($NumDay > 6)
                {
                    $NumDay = 0;
                }
                
                if($PerdayCount <= 7)
                {
                    $resultRow = $resultRow ."<th>".$arrDayofWeek[$NumDay]. "<p class=\"date_daily\">".$dateCheck->format('(M d)')."</p></th>";
                }else
                {
                     $resultRow = $resultRow ."<th>".$arrDayofWeek[$NumDay]."</th>";
                }
             
                $intheadCount = $intheadCount  + 1; 
                if($intheadCount == 7){
                    break;
                }
            
            }
        
        if($PerdayCount < 7)
            {
            
              
                for($i = 0; $i < (7 - $intheadCount); $i++)
                 {
                    $NumDay = $NumDay + 1;
                    
                    
                    $dateAdd =  date('(M d)',strtotime($dateCheck->format('Y-m-d')) + (24*3600*($i +1)));
                    
                    
                    if($NumDay > 6)
                    {
                        $NumDay = 0;
                    }
                    if($PerdayCount > 7)
                    {
                        $resultRow = $resultRow ."<th>".$arrDayofWeek[$NumDay]."</th>";
                    }else
                    {
                         $resultRow = $resultRow ."<th>".$arrDayofWeek[$NumDay]."<p class=\"date_daily\">".$dateAdd."</p></th>";
                    }
                    
                 }
                
            }
        
        
        $resultRow = $resultRow ."</tr>";
        
        
        //echo 'HELO EHLOE HELOOs';
        $priceNormal;
        $pricePro;
        $priceAbf;
        $pribase;
        $dateCheck;
        $intCount = 0;
               $resultRow = $resultRow ."<tr>";
        foreach($option->PricePerdays->PricePerday as $priceperday)
        {
             //if($intCount == 7){
             //   $resultRow = $resultRow ."</tr>";
             //}
       
            $dateCheck = new DateTime($priceperday->dm_date); 
             
            $hiddenrate = "<input type=\"hidden\" value=\"Base:".$priceperday->dm_pricebase."#!#Pro:".$priceperday->dm_pricepro."#!#abf:".$priceperday->dm_priceAbf."\" />";
            
             $priceNormal ;
            // .number_format($rateDisplay)
            
            if((float)$_SESSION['currencyPrefix'] > 0)
            {
                $pribase=  $priceperday->dm_pricebase/(float)$_SESSION['currencyPrefix'];
                $pricePro=  $priceperday->dm_pricepro/(float)$_SESSION['currencyPrefix'];
                $priceAbf=  $priceperday->dm_priceAbf/(float)$_SESSION['currencyPrefix'];
                
            }else
            {
                $pribase=  $priceperday->dm_pricebase;
                $pricePro=  $priceperday->dm_pricepro;
                $priceAbf=  $priceperday->dm_priceAbf;
            }
             
            if($pricePro < $pribase ){
            
                if($pricePro == 0){
                    if($priceAbf > 0 ){
                         $resultRow = $resultRow ."<td><p class=\"normal_base_rate\">".number_format($pribase).
                    "</p><p class=\"normal_extra_rate\">".number_format($priceAbf)."</p>".$hiddenrate."</td>";
                    }else{
                         $resultRow = $resultRow ."<td><p class=\"normal_base_rate\">".number_format($pribase).
                    "</p><p class=\"normal_free_rate\">Free</p>".$hiddenrate."</td>";
                    }
                }else{
                    $resultRow = $resultRow ."<td><p class=\"normal_base_rate\">".number_format($pribase).
                    "</p><p class=\"normal_pro_rate\">".number_format($pricePro)."</p>".$hiddenrate."</td>";
                }
                
            }else{
                $resultRow = $resultRow ."<td><p class=\"normal_rate\">".number_format($pricePro).$hiddenrate."</p>";
            }
             
            
             $intCount = $intCount + 1;
             
             if($intCount == 7){
             if($PerdayCount ==$intCount){
                $resultRow = $resultRow . "</tr>";
             }else{
                
             }
                $resultRow = $resultRow . "</tr><tr>";
                $intCount = 0;
                
             }
             
        }
        
        //echo $intCount ."----".$PerdayCount ;
        
        if($PerdayCount < 7){
                if($intCount < 7 ){
                    for($i = 0; $i < (7 - $intCount); $i++)
                    {
                        $resultRow = $resultRow ."<td></td>";
                    }
                     $resultRow = $resultRow ."</tr>";
                }else{
                    //if ($intCount ==7)
                    //{
                    //    $resultRow = $resultRow ."</tr>";
                    //}else if($intCount > 7)
                    //{
                    //    $resultRow = $resultRow ."<tr>";
                    //}
                }
           }
       
            
         $resultRow = $resultRow ."</table>";
         $resultRow = $resultRow ."</div>";
         $resultRow = $resultRow ."</td></tr> ";
         
         
        ///-----
        if($rowCount>4)
        {
        $resultRow=$resultRow."<tr class=\"tr-price-hidden\"><td colspan=\"4\" style=\"border-bottom:1px solid #d2d2d2\"></td></tr> ";
        }else{
            $resultRow=$resultRow."<tr><td colspan=\"4\" style=\"border-bottom:1px solid #d2d2d2\"></td></tr> ";
        }
        
        // Fully book 
        if(($pid==3605 || $pid==3619|| $pid==3617 || $pid==3484 || $pid==3612|| $pid==3565|| $pid==3568|| $pid==3567 ) && ($option->RoomAvailable!="true")){
            $resultRow="";
        }else{
            $roomTotal=$roomTotal+1;
            $rowCount=$rowCount+1;
        }
        $result=$result.$resultRow;
        
        
    } //loop option
    
    $hiddenDiscount=(float)$xmlSource->Discount[0];
    //$hiddenDiscount=100;
    if($rowCount>4)
        {
            $txtExpand="<div id=\"expandBox\" class=\"expand\">Show more room type</div>";
        }
    if((float)$hiddenDiscount>0)
    {
        if($hiddenDiscount<1)
        {
            $hiddenDiscount=(100-((float)$hiddenDiscount*100));
        }
        
        if($xmlSource->Country=="Not Identify")
        {
            $result=$result."<tr><td>".$txtExpand."</td><td colspan=\"3\" align=\"right\"><span class=\"extraDiscount\">* Extra discount ".$hiddenDiscount." for lemited time</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr> ";
        }else{
            $result=$result."<tr><td>".$txtExpand."</td><td colspan=\"3\" align=\"right\"><span class=\"extraDiscount\">* Extra discount ".$hiddenDiscount."% for ".$xmlSource->Country[0]." People</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr> ";
        }
        
    }else{
        if($rowCount>5)
        {
            $result=$result."<tr><td colspan=\"4\" align=\"left\">".$txtExpand."</td></tr> ";
        }
        
    }
    
    $result=$result."</table> ";

    $result=$result."</div><br/> ";

    if($OptionCount>0){
        $result=$result."<span class=\"serviceChargeTitle\">*** Rate is not included 7% tax and 10% service charge.</span>";
    }
    
    
    
    $result=$result.GenTableExtraOption($xmlSource)."<br/>";
    
    $result=$result.GenGuestBox()."<br/>";
    $result=$result."<div id=\"submitPan\"> ";
    $result=$result."<input type=\"submit\" id=\"btnBooking\" name=\"btnBooking\" value=\"Continue to Checkout >>\" class=\"btnSubmitBooking\" /> ";
    $result=$result."</div> ";
    
    //if($roomTotal>0){
        
    //    $result=$result.GenTableExtraOption($xmlSource)."<br/>";
        
    //    $result=$result.GenGuestBox()."<br/>";
    //    $result=$result."<div id=\"submitPan\"> ";
    //    $result=$result."<input type=\"submit\" id=\"btnBooking\" name=\"btnBooking\" value=\"Continue to Checkout >>\" class=\"btnSubmitBooking\" /> ";
    //    $result=$result."</div> ";

    //}else{

    //    $result=$result."<div class=\"rateOptionList-outer\"> ";
    //    $result=$result."<br/><center><font style=\"font-size:14px; color:#333333;\">No rate is shown in your selected period. Please contact to our hotel staff.</font></center><br/>";
    //    $result=$result."</div> ";
    //}
    
    
    return $result;
}
/*Visa 21-12-2013*/

function GenTablePackageOption($xmlSource)
{
    
    $discount=0;
    $result="";
    $rateRack=0;
    $rateDisplay=0;
    $rowCount=1;
    
    $hasPackage=false;
    //$benefitDetail="<div id=\"benefitPan\" style=\"display:none;\">";
    $currencyCode=$_SESSION['currencyCode'];
    $result=$result."<div class=\"rateOptionList-outer\"> ";
    $result=$result."<table id=\"rateOptionList\"> ";
    $result=$result."<tr><td></td></tr> ";
    $result=$result."<tr><th>Special Package</th><th width=\"180\">Condition</th><th width=\"130\">Price</th><th width=\"100\">Quantity</th></tr> ";
    $result=$result."<tr><td colspan=\"4\" style=\"border-bottom:2px solid #d7d7d7\"></td></tr> ";
    foreach($xmlSource->Packages->Option as $option)
    {
        $hasPackage=true;
        $rateRack=$option->PriceRack/(float)$_SESSION['currencyPrefix'];
        $rateDisplay=$option->Price/(float)$_SESSION['currencyPrefix'];
        if($rowCount>4)
        {
            $result=$result."<tr class=\"tr-price-hidden\"><td valign=\"top\"> ";
        }else{
            $result=$result."<tr><td valign=\"top\"> ";
        }
        if($option->OptionImage!="http://www.booking2hotels.com")
        {
            $result=$result."<img src=\"".$option->OptionImage."\" class=\"optionImg\"/> ";
        }
        
        $result=$result."<span class=\"optionTitle\">".$option->OptionTitle."</span> ";
        //$result=$result."<span class=\"promotionTitle\"><a href=\"#benefitContent_".$option->ConditionID."_".$option->attributes()->id."\" class=\"benefitBox\" >View benefit</a></span> ";
        $result=$result."<span class=\"promotionTitle\"><a href=\"javascript:void(0)\" class=\"tooltip\">View benefit<span class=\"tooltip_content\">".$option->OptionDetail."</span></a></span> ";

        $result=$result."</td><td valign=\"top\"> ";
        $result=$result.str_replace("\n","",$option->ConditionDetail);
        
        //$benefitDetail=$benefitDetail."<div id=\"benefitContent_".$option->ConditionID."_".$option->attributes()->id."\">".$option->OptionDetail."</div>";
        
        if((int)$option->MaxAdult>0)
        {
            $result=$result."<span class=\"optionAdult\">Max Adult: ".$option->MaxAdult."</span> ";
            }else{
                $result=$result."<span class=\"optionAdult\">Max Child: ".$option->MaxChild."</span> ";
                }
        
        //if($option->MaxAdult>0)
//      {
//          $result=$result."<span class=\"optionAdult\">Max Adult: ".$option->MaxAdult."</span> ";
//      }else{
//          $result=$result."<span class=\"optionAdult\">Max Child: ".$option->MaxAdult."</span> ";
//      }
        
        $result=$result."<a href=\"javascript:void(0)\" class=\"tooltip\"><span class=\"conditionFloat\">View Condition</span><span class=\"tooltip_content\">".$option->PolicyContent."</span></a> ";
        $result=$result."</td><td align=\"right\"> ";
        $discount=number_format((($rateRack-$rateDisplay)/$rateRack)*100);
        if((int)$option->Price!=(int)$option->PriceRack)
        {
            $result=$result."<span class=\"rackRate\">".$currencyCode." ".number_format($rateRack)."</span><br /> ";
            $result=$result."<span class=\"rackOwn\">".$currencyCode." ".number_format($rateDisplay)."</span><br /> ";
            $result=$result." <span class=\"savePrice\">Save ".$discount."%</span><br /> ";
        }else{
            $result=$result."<span class=\"rackOwn\">".$currencyCode." ".number_format($rateDisplay)."</span><br /> ";
        }
        

        if($option->RoomAvailable=="true")
            {
                $result=$result."<span class=\"roomAval\">Limited room available</span> ";
            }
        $result=$result."</td><td align=\"center\">".GenDropDownQuantity("ddPackage_".$option->ConditionID."_".$option->attributes()->id,$option->ConditionValue,0,20,0,4)."</td></tr> ";
        if($rowCount>4)
        {
        $result=$result."<tr class=\"tr-price-hidden\"><td colspan=\"4\" style=\"border-bottom:1px solid #d2d2d2\"></td></tr> ";
        }else{
            $result=$result."<tr><td colspan=\"4\" style=\"border-bottom:1px solid #d2d2d2\"></td></tr> ";
        }
        $rowCount=$rowCount+1;
    }
    $hiddenDiscount=(float)$xmlSource->Discount[0];
    //$hiddenDiscount=100;
    if($rowCount>4)
        {
            $txtExpand="<div id=\"expandBox\" class=\"expand\">Show more room type</div>";
        }
    if((float)$hiddenDiscount>0)
    {
        if($hiddenDiscount<1)
        {
            $hiddenDiscount=(100-((float)$hiddenDiscount*100));
        }
        
        if($xmlSource->Country=="Not Identify")
        {
            $result=$result."<tr><td>".$txtExpand."</td><td colspan=\"3\" align=\"right\"><span class=\"extraDiscount\">* Extra discount ".$hiddenDiscount." for lemited time</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr> ";
        }else{
            $result=$result."<tr><td>".$txtExpand."</td><td colspan=\"3\" align=\"right\"><span class=\"extraDiscount\">* Extra discount ".$hiddenDiscount."% for ".$xmlSource->Country[0]." People</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr> ";
        }
        
    }else{
        if($rowCount>5)
        {
            $result=$result."<tr><td colspan=\"4\" align=\"left\">".$txtExpand."</td></tr> ";
        }
        
    }
    
    $result=$result."</table> ";
    $result=$result."</div> ";
    $result=$result."<span class=\"serviceChargeTitle\">*** Rate is not included 7% tax and 10% service charge.</span>";
    //$benefitDetail=$benefitDetail."</div>";
    //$result=$result.$benefitDetail;
    if(!$hasPackage)
    {
        $result="";
    }
    return $result;
}
function GenTableExtraOption($xmlSource)
{
    $result="";
    $rateDisplay=0;
    $hasExtra=false;
    $optionCate=0;
    
    $currencyCode=$_SESSION['currencyCode'];
    $result=$result."<div class=\"rateOptionList-outer\"> ";
    
    $result=$result."<table id=\"rateExtraOptionList\"> ";
    $result=$result."<tr><td colspan=\"3\"><div id=\"errorTransfer\" class=\"errorMsg\"></div></td></tr>";
    foreach($xmlSource->ExtraOption->Option as $option)
    {
        switch((int)$option->OptionCateID)
        {
            case 39:
                $optionCate=2;
            break;
            case 43: case 44:
                $optionCate=3;
            break;
        }
        $hasExtra=true;
        $rateDisplay=$option->Price/(float)$_SESSION['currencyPrefix'];
        $result=$result."<tr><td><span class=\"extraOptionTitle\">".$option->OptionTitle."</span></td><td align=\"right\"><span class=\"rackOwn\">".$currencyCode." ".number_format($rateDisplay)."</span></td><td width=\"90\" align=\"center\">".GenDropDownQuantity("ddPriceExtra_".$option->ConditionID."_".$option->attributes()->id,$option->ConditionValue,0,20,0,$optionCate)."</td></tr> ";
        $result=$result."<tr><td colspan=\"3\" style=\"border-bottom:1px solid #d2d2d2\"></td></tr> ";
    }
    
    foreach($xmlSource->Meals->Option as $option)
    {
        $hasExtra=true;
        $rateDisplay=$option->Price/(float)$_SESSION['currencyPrefix'];
        $result=$result."<tr><td><span class=\"extraOptionTitle\">".$option->OptionTitle."</span></td><td align=\"right\"><span class=\"rackOwn\">".$currencyCode." ".number_format($rateDisplay)."</span></td><td width=\"90\" align=\"center\">".GenDropDownQuantity("ddMeal_".$option->ConditionID."_".$option->attributes()->id,$option->ConditionValue,0,20,0,4)."</td></tr> ";
        $result=$result."<tr><td colspan=\"3\" style=\"border-bottom:1px solid #d2d2d2\"></td></tr> ";
    }
    
    //foreach($xmlSource->Meals->Option as $option)
//  {
//      $hasExtra=true;
//      $rateDisplay=$option->Price/(float)$_SESSION['currencyPrefix'];
//      $result=$result."<tr><td><span class=\"extraOptionTitle\">".$option->OptionTitle."</span></td><td align=\"right\"><span class=\"rackOwn\">".$currencyCode." ".number_format($rateDisplay)."</span></td><td width=\"90\" align=\"center\">".GenDropDownQuantity("ddMeal_".$option->ConditionID."_".$option->attributes()->id,$option->ConditionValue,0,20,0,4)."</td></tr> ";
//      $result=$result."<tr><td colspan=\"3\" style=\"border-bottom:1px solid #d2d2d2\"></td></tr> ";
//  }
    $result=$result."</table> ";
    $result=$result."</div> ";
    $result=$result."<span class=\"serviceChargeTitle\">*** Rate is not included 7% tax and 10% service charge.</span>";
    if(!$hasExtra)
    {
        $result="";
    }
    return $result;
}


function GenTableOptionNoDate($xmlSource,$pid)
{
    $data=$xmlSource->Title;
    $countryRef= $xmlSource->CountryRef;
    $productID= $pid;
    
    //$result="";
//  $result=$result."<table border=\"1\"><tr><td>Option Title</td></tr>";
//  if(count($data)>0)
//  {
//      for($intCount=0;$intCount<count($data);$intCount++)
//      {
//          $result=$result."<tr><td>".$xmlSource->Title[$intCount]."</td></tr>";
//      }
//  }else{
//      echo "No rate";
//  }
//  $result=$result."</table>";
    
    $result="";
    $result=$result."<div class=\"rateOptionList-outer\"> ";
    if($productID=="592" && $countryRef=="208")
    {
        $result=$result."<br/><center><font style=\"font-size:14px;color:#333333;\">If you wish to make a reservation at Pathumwan Princess Hotel, you can go to <a href=\"http://pprincess.com\">pprincess.com</a></font></center><br/>";
    }else{
        $result=$result."<table id=\"rateOptionTitleList\"> ";
        if(count($data)>0)
        {
            for($intCount=0;$intCount<count($data);$intCount++)
            {
                $result=$result."<tr><td><span class=\"extraOptionTitle\">".$xmlSource->Title[$intCount]."</span></td></tr> ";
                $result=$result."<tr><td style=\"border-bottom:1px solid #d2d2d2\"></td></tr> ";
            }
            
        }else{
            //echo "No rate";
            $result="<div class=\"rateOptionList-outer\"> ";
                $result=$result."<br/><center><font style=\"font-size:14px; color:#333333;\">We have not rate in this period please check again.</font></center><br/>";
                $result=$result."</div> ";
        }
        $result=$result."</table> ";
    }
    
    $result=$result."</div>";
//  $result=$result."<input type=\"hidden\" name=\"p\" value=\"".$productID."\"> ";
//  $result=$result."<input type=\"hidden\" name=\"r\" value=\"".$countryRef."\"> ";
    return $result;
}

function GenGuestBox()
{
    $result="";
    $result=$result."<div class=\"rateOptionList-outer\"> ";
    $result=$result."<table id=\"guestSelect\"> ";
    $result=$result."<tr><td height=\"40\"><span class=\"extraOptionTitle\">Adults: </span>".GenDropDownQuantity("adult","",1,20,0,0)."<span class=\"extraOptionTitle\">Children: </span>".GenDropDownQuantity("child","",0,20,0,0)."</td></tr> ";
    $result=$result."</table> ";
    $result=$result."</div> ";
    return $result;
}

function RenderCurrencyBox($xmlSource,$currencyDefault)
{
    $result="";
    //$result="<br/>";
    $result=$result."<form action=\"\" method=\"post\" id=\"changeCurrency\"> ";
    $result=$result."<table cellspacing=\"0\" id=\"currencyBox\" align=\"center\"><tr><td width=\"500\">&nbsp;</td><td>Change currency:</td><td width=\"24\"><img src=\"http://engine.booking2hotels.com/images/currency/flag_".$_SESSION['currencyID'].".jpg\" /></td><td width=\"61\"><select id=\"selCurrency\" name=\"selCurrency\">";
    foreach($xmlSource->Exchange->Currency as $currency)
    {
        if((int)$currency->CurrencyID==$currencyDefault)
        {
            $result=$result."<option value=\"".$currency->CurrencyID."\" selected=\"selected\">".$currency->CurrencyCode."</option> ";
        }else{
            $result=$result."<option value=\"".$currency->CurrencyID."\">".$currency->CurrencyCode."</option> ";
        }
        
    }
    $result=$result."</select></td></tr></table> ";
    $result=$result."</form> ";
    
    
    return $result;
}

function RenderRate($pid)
{
    if(!isset($_SESSION['currencyID']))
    {
        $_SESSION['currencyID']=25;
    }
    
    
   // echo $_GET["rateExchange"];
    
    if(isset($_GET["rateExchange"]))
    {
        $_SESSION['currencyID']=$_GET["rateExchange"];  
    }
    //Set Date Default
    if(isset($_GET["Hddateci"]))
    {
        $_SESSION['dateStart']=$_GET["Hddateci"];
        $_SESSION['dateEnd']=$_GET["Hddateco"];
    }else{
        if(!isset($_SESSION['dateStart']))
        {
            $_SESSION['dateStart']="";
            $_SESSION['dateEnd']="";
        }
    }
    
    $dateStart=$_SESSION['dateStart'];
    $dateEnd=$_SESSION['dateEnd'];
    
    $memberAuthen="0";
    if(isset($_GET["mm"])){
        $memberAuthen=$_GET["mm"];
    }
    
    //Default Currency
    if($dateStart!="")
    {
        
         $dataSource="http://www.booking2hotels.com/affiliate_include/AffiliateFeed.aspx?uip=".getRealIpAddr()."&pid=".$pid."&datein=".$dateStart."&dateout=".$dateEnd."&mm=".$memberAuthen;
        // $dataSource="http://www.booking2hotels.com/affiliate_include/AffiliateFeed.aspx?uip=127.0.0.1&pid=3449&datein=2012-11-23&dateout=2012-11-25";
        // echo $dataSource;    
        //echo $dataSource;
        $xmlString=file_get_contents($dataSource);
        //echo $xmlString;  
        
        //$xmlSource=simplexml_load_file($dataSource);
        $sxe = new SimpleXMLElement($xmlString);
        
        //$xmlSource=;
        $xmlSource=simplexml_load_string($xmlString);
        ////print_r($xmlSource);
        //exit();
        if($xmlSource->ErrorMessage[0]!="ValidDate" && $xmlSource->ErrorMessage[0]!="No Rate" && $xmlSource->ErrorMessage[0]!="ErrorVacationDate")
        {
            foreach($xmlSource->Exchange->Currency as $currency)
            {
            
                $currenyID = array((string)$currency->CurrencyID);
               //echo "code=". $_SESSION['currencyID'];
               // echo  $currenyID[] . "----" . $_SESSION['currencyID'] . "<br/>";
                if($currenyID[0]==$_SESSION['currencyID'])
                {
                   // echo 'ss= ' .$currenyID[0] . "----" . $_SESSION['currencyID'];
                    $varPrefix = (string)$currency->CurrencyPrefix;
                    $vatCode = (string)$currency->CurrencyCode;
                    $_SESSION['currencyPrefix']= $varPrefix;
                    $_SESSION['currencyCode']= $vatCode;
                    
                }
                
            
            }
            return GenPricingTable($xmlSource,$pid);
            
            ////return 'sss';
        }else{
            switch ($xmlSource->ErrorMessage[0]) {
                case "ValidDatevalue":
                    $result="<div class=\"rateOptionList-outer\"> ";
                    $result=$result."<br/><center><font style=\"font-size:14px; color:#333333;\">Please make the booking at least 24 hours in advance before check in date. (Thai Time)</font></center><br/>";
                    $result=$result."</div> ";
                    break;
                case "No Rate":
                    $result="<div class=\"rateOptionList-outer\"> ";
                    $result=$result."<br/><center><font style=\"font-size:14px; color:#333333;\">We have not rate in this period please check again.</font></center><br/>";
                    $result=$result."</div> ";
                break;
                case "ErrorVacationDate":
                    $result="<div class=\"rateOptionList-outer\">";
                    $result=$result.$xmlSource->ErrorDescription[0];
                    $result=$result."</div>";
                break;
            }
            
            return $result;
        }
        
    }else{
         
        $dataSource="http://www.booking2hotels.com/affiliate_include/AffiliateFeed.aspx?uip=".getRealIpAddr()."&pid=".$pid;
       //echo $dataSource;
        $xmlString=file_get_contents($dataSource);
        //echo $xmlString;  
        
        //$xmlSource=simplexml_load_file($dataSource);
        //$sxe = new SimpleXMLElement($xmlString);
        
        //$xmlSource=;
        $xmlSource=simplexml_load_string($xmlString);
        //$xmlSource=simplexml_load_file($dataSource);
        //return GenPricingTable($xmlSource,$pid);
        return GenTableOptionNoDate($xmlSource,$pid);
    }
    
   
    //echo $dateStart.' '.$dateEnd.' '.$_SESSION['currencyID'];
}
function clean($string) {
    
    //$string = str_replace(' ', '-', $string); // Replaces all spaces with hyphens.
    $string = str_replace("'", "&apos;", $string);
    $string = str_replace('\'', '\\\'', $string);
    //$string = preg_replace('/[^A-Za-z0-9\-]/', '', $string); // Removes special chars.

    return $string;
    //return preg_replace('/-+/', '-', $string); // Replaces multiple hyphens with single one.
}
//$dataSource="xml_data.xml";
//echo $_GET["Hddateci"];
//echo $_GET["pid"];
$returnHtml=RenderRate($_GET['pid']);

//echo $returnHtml;
//echo "document.write('".$returnHtml."')";
echo "jQuery('.b2hRateResult').html('".clean($returnHtml)."');";
//echo "jQuery('.b2hRateResult').html('".str_replace("'","&apos;",$returnHtml)."');";
echo "tooltip();";
echo "setEventToDropDown();";
echo "imgFloat();";
echo "fnSetControlDefault();";
echo "showdaily();"

//echo $xmlSource->Options->Option[3]->OptionTitle;
?>