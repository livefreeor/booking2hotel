<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax_review_insert_form.aspx.cs" Inherits="Hotels2thailand.UI.admin_ajax_review_insert_form" %>

<form action="review_write_pcs.aspx" method="post">
            <div id="review_box">
           	  <div class="review_box_header">Review</div>                
                
              <div class="review_rate_select_box">
              <ul id="ratingPan"><li><strong>Overall</strong><br/><input name="rate_overall" type="radio" class="star" value="1" />
<input name="rate_overall" type="radio" class="star" value="2" />
<input name="rate_overall" type="radio" class="star" value="3" />
<input name="rate_overall" type="radio" class="star" value="4" />
<input name="rate_overall" type="radio" class="star" value="5" />
<input name="rate_overall" type="radio" class="star" value="6" />
<input name="rate_overall" type="radio" class="star" value="7" />
<input name="rate_overall" type="radio" class="star" value="8" />
<input name="rate_overall" type="radio" class="star" value="9" />
<input name="rate_overall" type="radio" class="star" value="10" />
</li>
<li><strong>Service</strong><br/><input name="rate_service" type="radio" class="star" value="1" />
<input name="rate_service" type="radio" class="star" value="2" />
<input name="rate_service" type="radio" class="star" value="3" />
<input name="rate_service" type="radio" class="star" value="4" />
<input name="rate_service" type="radio" class="star" value="5" />
<input name="rate_service" type="radio" class="star" value="6" />
<input name="rate_service" type="radio" class="star" value="7" />
<input name="rate_service" type="radio" class="star" value="8" />
<input name="rate_service" type="radio" class="star" value="9" />
<input name="rate_service" type="radio" class="star" value="10" />
</li>
<li><strong>Location</strong><br/><input name="rate_location" type="radio" class="star" value="1" />
<input name="rate_location" type="radio" class="star" value="2" />
<input name="rate_location" type="radio" class="star" value="3" />
<input name="rate_location" type="radio" class="star" value="4" />
<input name="rate_location" type="radio" class="star" value="5" />
<input name="rate_location" type="radio" class="star" value="6" />
<input name="rate_location" type="radio" class="star" value="7" />
<input name="rate_location" type="radio" class="star" value="8" />
<input name="rate_location" type="radio" class="star" value="9" />
<input name="rate_location" type="radio" class="star" value="10" />
</li>
<li><strong>Cleanliness</strong><br/><input name="rate_cleanliness" type="radio" class="star" value="1" />
<input name="rate_cleanliness" type="radio" class="star" value="2" />
<input name="rate_cleanliness" type="radio" class="star" value="3" />
<input name="rate_cleanliness" type="radio" class="star" value="4" />
<input name="rate_cleanliness" type="radio" class="star" value="5" />
<input name="rate_cleanliness" type="radio" class="star" value="6" />
<input name="rate_cleanliness" type="radio" class="star" value="7" />
<input name="rate_cleanliness" type="radio" class="star" value="8" />
<input name="rate_cleanliness" type="radio" class="star" value="9" />
<input name="rate_cleanliness" type="radio" class="star" value="10" />
</li>
<li><strong>Rooms</strong><br/><input name="rate_rooms" type="radio" class="star" value="1" />
<input name="rate_rooms" type="radio" class="star" value="2" />
<input name="rate_rooms" type="radio" class="star" value="3" />
<input name="rate_rooms" type="radio" class="star" value="4" />
<input name="rate_rooms" type="radio" class="star" value="5" />
<input name="rate_rooms" type="radio" class="star" value="6" />
<input name="rate_rooms" type="radio" class="star" value="7" />
<input name="rate_rooms" type="radio" class="star" value="8" />
<input name="rate_rooms" type="radio" class="star" value="9" />
<input name="rate_rooms" type="radio" class="star" value="10" />
</li>
<li><strong>Value for money</strong><br/><input name="rate_value_for_money" type="radio" class="star" value="1" />
<input name="rate_value_for_money" type="radio" class="star" value="2" />
<input name="rate_value_for_money" type="radio" class="star" value="3" />
<input name="rate_value_for_money" type="radio" class="star" value="4" />
<input name="rate_value_for_money" type="radio" class="star" value="5" />
<input name="rate_value_for_money" type="radio" class="star" value="6" />
<input name="rate_value_for_money" type="radio" class="star" value="7" />
<input name="rate_value_for_money" type="radio" class="star" value="8" />
<input name="rate_value_for_money" type="radio" class="star" value="9" />
<input name="rate_value_for_money" type="radio" class="star" value="10" />
</li>
</ul>
<br class="clear-all" />
              </div>               

            <div class="rating_header_orage">* Review Title<br /><span>(Example: Excellent location with supreb food.)</span>
 <br />
            <input type="text" name="review_title" id="review_title" class="inputbox_rating" />
            </div>
            
            <div class="rating_header_orage">* Your Review <br /><span>(What did you like or dislike about this place and why?)</span><br />
            <textarea name="review_detail" rows="5" cols="45"></textarea>
            </div>
            <div class="rating_header_orage">&nbsp;&nbsp;Your Name<br />  
              <input type="text" name="cus_name" value="" class="inputbox_rating"/>
              <br /></div>
            
            <div class="rating_header_orage">&nbsp;&nbsp;Where are you from?  <br /><span>&nbsp;&nbsp;(Example: London, UK)</span><br />
            <input type="text" name="cus_from" value="" class="inputbox_rating"/>
            </div>
            
            <div id="rating_buttom"> 
            <input type="hidden" name="product" value="624"/><input type="hidden" name="category" value="29"/>
            <br /><input type="submit" name="btntest" class="rating_sumbit" value="" /> </div>
            
            </div><!--review_box-->
            </form>


