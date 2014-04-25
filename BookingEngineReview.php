<?php
function GetDataSource($pid)
{
		$dataSource="http://www.booking2hotels.com/affiliate_include/AffiliateFeedReview.aspx?pid=".$pid;
		//$dataSource="data.xml";
		$xmlSource=simplexml_load_file($dataSource);
		return RenderProductReview($xmlSource);
}

function RenderProductReview($xmlSource)
{
	$result="";
	
	$result=$result."<div id=\"review-rightPan\">";
	$result=$result."<p><span class=\"fnLarge fnBlue\">".count($xmlSource->Review) ." reviews from our community</span></p>";
    $result=$result."<div id=\"review-overall\">";
    $result=$result."<p><span class=\"titleFloatLeft\">Overall</span><span class=\"review_".$xmlSource->Overall."\"></span></p><br class=\"clearAll\" />";
    $result=$result."<p><span class=\"titleFloatLeft\">Service</span><span class=\"review_".$xmlSource->Service."\"></span></p><br class=\"clearAll\" />";
    $result=$result."<p><span class=\"titleFloatLeft\">Location</span><span class=\"review_".$xmlSource->Location."\"></span></p><br class=\"clearAll\" />";
    $result=$result."<p><span class=\"titleFloatLeft\">Room</span><span class=\"review_".$xmlSource->Room."\"></span></p><br class=\"clearAll\" />";
    $result=$result."<p><span class=\"titleFloatLeft\">Cleaness</span><span class=\"review_".$xmlSource->Clearness."\"></span></p><br class=\"clearAll\" />";
    $result=$result."<p><span class=\"titleFloatLeft\">Value for money</span><span class=\"review_".$xmlSource->Money."\"></span></p><br class=\"clearAll\" />";
    $result=$result."</div>";
	$result=$result."</div><br/><br/>";
	$result=$result."<div id=\"review-leftPan\">";
	foreach($xmlSource->Review as $item)
	{
		$result=$result."<div class=\"product-review-item\">";
		$result=$result."<span class=\"fnMedium fnBlue fnBold\">".str_replace("\n","",$item->Title)."</span><br />";
		$result=$result."<div class=\"review_".$item->Point."\"></div> <span class=\"fnGrayLight fnSmall\">Reviewed ".date_format(date_create($item->DateReview), 'l jS F Y')."</span>";
		$result=$result."<p class=\"fnMedium\">".str_replace("\n","",$item->Detail)."</p>";
		$result=$result."</div>";
	}
	$result=$result."</div>";
	return $result;
}

$returnHtml=GetDataSource($_GET['pid']);
echo "jQuery('.b2hReviewResult').html('".str_replace("'","&apos;",$returnHtml)."');";
?>