using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using System.Text;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_review_post_edit : System.Web.UI.Page
    {
        public string qReviewId
        {
            get { return Request.QueryString["revId"]; }
        }

        public string qreview
        {
            get { return Request.QueryString["reviews"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                lblResult.Text = reviewResultFrom(this.qreview, int.Parse(this.qReviewId));
                
             }
        }

        public int StratIndexPage(int PageSize, int PageCurrent)
        {
            int IndexPage = (PageSize * PageCurrent) - PageSize;
            return IndexPage;
        }


        public string imgeReviewRatePath(byte bytRate)
        {
            string strPath = string.Empty;
            switch (bytRate)
            {
                case 0:
                    strPath = "review0s.png";
                    break;
                case 1:
                    strPath = "review1s.png";
                    break;
                case 2:
                    strPath = "review2s.png";
                    break;
                case 3:
                    strPath = "review3s.png";
                    break;
                case 4:
                    strPath = "review4s.png";
                    break;
                case 5:
                    strPath = "review5s.png";
                    break;
                case 6:
                    strPath = "review6s.png";
                    break;
                case 7:
                    strPath = "review7s.png";
                    break;
                case 8:
                    strPath = "review8s.png";
                    break;
                case 9:
                    strPath = "review9s.png";
                    break;
                case 10:
                    strPath = "review10s.png";
                    break;
            }
            return strPath;
        }
        
        public string reviewResultFrom(string ProductCat, int intreviewId)
        {
            StringBuilder strResult = new StringBuilder();
            
                    if (!string.IsNullOrEmpty(this.qreview) && !string.IsNullOrEmpty(this.qReviewId))
                    {
                        ProductReviews reviews_item = new ProductReviews();
                        reviews_item = ReviewManage.GetreviewById(int.Parse(this.qReviewId));

                        strResult.Append("<div class=\"review_item_block_edit\" >");
                        
                        strResult.Append("<p class=\"review_id_new\">#" + reviews_item.ReviewId + "</p>");
                        strResult.Append("<h1>" + reviews_item.ProductTitle + "&nbsp;&nbsp;</h1>");
                        strResult.Append("<p class=\"review_title\"><textarea name=\"review_title\" rows=\"2\" cols=\"80\" title=\"Edit Review Title\">" + reviews_item.Title + "</textarea></p>");

                        


                        //strResult.Append("<div id=\"review_item_" + reviews_item.ReviewId + "\" style=\"display:none;\">");

                        //strResult.Append("</div>");

                        //strResult.Append("</div>");

                        strResult.Append("<div class=\"review_detail\">");
                        strResult.Append("<div class=\"review_detail_detail\">");
                        strResult.Append("<p><textarea rows=\"5\"  name=\"review_detail\"  cols=\"80\" title=\"Edit Review Detail\" >" + reviews_item.Detail + "</textarea></p>");
                        strResult.Append("</div>");
                        strResult.Append("<div class=\"review_detail_rate_and_comment\">");
                        strResult.Append("<div class=\"review_detail_rate\">");
                        //review from
                        strResult.Append("<div id=\"review_box\">");
                        //strResult.Append("<p class=\"review_box_header\">Review</p> ");

                        strResult.Append("<div id=\"ratebox\">");
                        strResult.Append("<p class=\"rating_header\">Overall Rating</p>");


                        // OverAll

                        strResult.Append("<ul id=\"rating_table_overall\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                        strResult.Append("<div style=\"clear:both; height:5px;\"></div>");
                        


                        if (ProductCat != "golfs")
                        {
                            strResult.Append("<p  class=\"rating_header\">Service</p>");
                            // Service
                            strResult.Append("<ul id=\"rating_table_service\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");
                        }


                        if (ProductCat == "hotels" || ProductCat=="spa")
                        {
                            strResult.Append("<p  class=\"rating_header\">Location</p>");
                            // Location

                            strResult.Append("<ul id=\"rating_table_location\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");
                        }


                        if (ProductCat == "hotels" || ProductCat == "spa" || ProductCat == "health")
                        {
                            strResult.Append("<p class=\"rating_header\">Cleanliness</p>");
                            // Cleanliness
                            strResult.Append("<ul id=\"rating_table_cleanliness\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");
                        }


                        if (ProductCat == "hotels" || ProductCat == "spa")
                        {
                            strResult.Append("<p class=\"rating_header\">Rooms</p>");
                            // Rooms
                            strResult.Append("<ul id=\"rating_table_room\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");
                        }

                        if (ProductCat == "golfs")
                        {
                            strResult.Append("<p class=\"rating_header\">Fairway</p>");
                            // Fairway
                            strResult.Append("<ul id=\"rating_table_Fairway\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");
                        }

                        if (ProductCat == "golfs")
                        {
                            strResult.Append("<p class=\"rating_header\">Green</p>");
                            // Green
                            strResult.Append("<ul id=\"rating_table_green\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");
                        }

                        if (ProductCat == "golfs")
                        {
                            strResult.Append("<p class=\"rating_header\">Difficult</p>");
                            // Difficult
                            strResult.Append("<ul id=\"rating_table_difficult\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                        }

                        if (ProductCat == "golfs")
                        {
                            strResult.Append("<p class=\"rating_header\">Speed</p>");
                            // Speed
                            strResult.Append("<ul id=\"rating_table_speed\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                            
                        }

                        if (ProductCat == "golfs")
                        {
                            strResult.Append("<p class=\"rating_header\">Caddy</p>");
                            // caddy
                            strResult.Append("<ul id=\"rating_table_caddy\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                        }

                        if (ProductCat == "golfs")
                        {
                            strResult.Append("<p class=\"rating_header\">Clubhouse</p>");
                            // clubhouse
                            strResult.Append("<ul id=\"rating_table_clubhouse\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                        }

                        if (ProductCat != "hotels" && ProductCat != "health")
                        {
                            strResult.Append("<p class=\"rating_header\">Food</p>");
                            // Food
                            strResult.Append("<ul id=\"rating_table_health\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                        }

                        if (ProductCat == "show")
                        {
                            strResult.Append("<p class=\"rating_header\">Performance</p>");
                            // Performance
                            strResult.Append("<ul id=\"rating_table_performance\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                        }

                        if (ProductCat == "show" || ProductCat == "waters" || ProductCat == "daytrips")
                        {
                            strResult.Append("<p class=\"rating_header\">Punctuality</p>");
                            // Punctuality
                            strResult.Append("<ul id=\"rating_table_Punctuality\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                            
                        }

                        if (ProductCat == "health")
                        {
                            strResult.Append("<p class=\"rating_header\">diagnose_ability</p>");
                            // diagnose_ability
                            strResult.Append("<ul id=\"rating_table_ability\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                        }

                        if (ProductCat == "show" || ProductCat == "waters" || ProductCat == "daytrips")
                        {
                            strResult.Append("<p class=\"rating_header\">Pronunciation</p>");
                            // rating_table_Pronunciation
                            strResult.Append("<ul id=\"rating_table_Pronunciation\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                            
                        }

                        if (ProductCat == "waters" || ProductCat == "daytrips")
                        {
                            strResult.Append("<p class=\"rating_header\">Knowledge</p>");
                            // Knowledge
                            strResult.Append("<ul id=\"rating_table_Knowledge\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                            strResult.Append("<div style=\"clear:both; height:5px;\"></div>");

                        }

                        strResult.Append("<p class=\"rating_header\">Value for money</p>");
                        // Money
                        strResult.Append("<ul id=\"rating_table_money\"><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>");
                        strResult.Append("<div style=\"clear:both; height:5px;\"></div>");


                        strResult.Append("</div>");
                        strResult.Append("</div>");
                        //--------------------------
                        
                       
                        strResult.Append("</div>");
                        //strResult.Append("<div class=\"review_detail_comment\"");
                        //strResult.Append("<p><img src=\"../../images/plus_s.png\">&nbsp;<textarea rows=\"4\" name=\"review_positive\" cols=\"40\">" + reviews_item.Positive + "</textarea></p>");
                        //strResult.Append("<p><img src=\"../../images/minus.png\">&nbsp;<textarea rows=\"4\"  name=\"review_nagative\" cols=\"40\">" + reviews_item.Nagative + "</textarea></p>");
                        //strResult.Append("</div>");
                        strResult.Append("</div>");
                        strResult.Append("<div style=\"clear:both;\"></div>");
                        strResult.Append("<p class=\"review_by\">by&nbsp;<input type=\"text\" name=\"full_name\" title=\"Edit Full Name\" value=\"" + reviews_item.FullName + "\" />&nbsp;from&nbsp;<input type=\"text\" name=\"review_from\" title=\"Edit Customer Review From\" value=\"" + reviews_item.ReviewForm + "\" />&nbsp;Date Submit :(&nbsp;" + reviews_item.DateSubmit.ToString("MMM d, yyyy") + "&nbsp;)</p>");
                        strResult.Append("<div class=\"review_detail_information\">");

                        //if (reviews_item.DateTravel == null)
                        //    strResult.Append("<p><span class=\"review_head\">Datetravel</span> :&nbsp;(&nbsp;Not Identify &nbsp;)&nbsp;</p>");
                        //else
                        //{
                        //    DateTime dDateTravel = (DateTime)reviews_item.DateTravel;
                        //    strResult.Append("<p><span class=\"review_head\">Datetravel</span> :&nbsp;(&nbsp;" + dDateTravel.ToString("MMM d, yyyy") + "&nbsp;)&nbsp;</p>");
                        //}

                        //strResult.Append("<p><span class=\"review_head\">Country</span> &nbsp;: &nbsp;");
                        //Production.Country cCountry = new Production.Country();
                        //Dictionary<byte, string> ContryResult = cCountry.GetCountryAll();
                       
                        //strResult.Append("<select name=\"review_country\">");

                        ////if (reviews_item.CountryId == null)
                        //strResult.Append("<option value=\"0\">Not Identify</option>");
                        //foreach (KeyValuePair<byte, string> item in ContryResult)
                        //{
                        //    if (item.Key == reviews_item.CountryId)
                        //        strResult.Append("<option value=\"" + item.Key + "\" selected=\"selected\">" + item.Value + "</option>");
                        //    else
                        //        strResult.Append("<option value=\"" + item.Key + "\">" + item.Value + "</option>");
                        //}
                        //strResult.Append("</select>");
                        //strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                        ////strResult.Append("" + reviews_item.Countrytitle + "");




                        //IDictionary<byte, string> FromResult = ReviewManage.getReViewFrom();
                        //strResult.Append("<span class=\"review_head\">Group travel</span>&nbsp;: &nbsp;");
                        //strResult.Append("<select name=\"from_review\">");

                        //foreach (KeyValuePair<byte, string> item in FromResult)
                        //{
                        //    if (item.Key == reviews_item.FromId)
                        //        strResult.Append("<option value=\"" + item.Key + "\" selected=\"selected\">" + item.Value + "</option>");
                        //    else
                        //        strResult.Append("<option value=\"" + item.Key + "\">" + item.Value + "</option>");
                        //}

                        //strResult.Append("</select>");
                        //strResult.Append("</p>");
                        
                        
                        //strResult.Append("<p><span class=\"review_head\">This hotel was recommended for</span> &nbsp;: &nbsp;");
                        //strResult.Append("<select name=\"review_recom\">");
                        //IDictionary<byte, string> RecomResult = ReviewManage.getReViewRecommend();
                        //foreach (KeyValuePair<byte, string> item in RecomResult)
                        //{
                        //    if (item.Key == reviews_item.ReccomId)
                        //        strResult.Append("<option value=\"" + item.Key + "\" selected=\"selected\">" + item.Value + "</option>");
                        //    else
                        //        strResult.Append("<option value=\"" + item.Key + "\">" + item.Value + "</option>");
                        //}

                        
                        //strResult.Append("</select>");
                        //strResult.Append("</p>");
                        
                        strResult.Append("</div>");
                        strResult.Append("<p class=\"review_detail_link\">");
                        strResult.Append("<input type=\"button\" value=\"Save\" class=\"Extra_Button_green\" onclick=\"RevieweditSave('" + reviews_item.ReviewId + "')\" />&nbsp;&nbsp;");
                        strResult.Append("<input type=\"button\" value=\"Cancel\" class=\"Extra_Button_small_white\" onclick=\"RestoreTempReviewBlock('" + reviews_item.ReviewId + "')\" />");
                        
                        strResult.Append("</p>");
                        strResult.Append("</div>");

                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "overall", "<script>StarValue('rating_table_overall','" + reviews_item.RateOverAll + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "service", "<script>StarValue('rating_table_service','" + reviews_item.RateService + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "location", "<script>StarValue('rating_table_location','" + reviews_item.RateLocation + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "clean", "<script>StarValue('rating_table_cleanliness','" + reviews_item.RateClean + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "room", "<script>StarValue('rating_table_room','" + reviews_item.RateRoom + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "money", "<script>StarValue('rating_table_money','" + reviews_item.RateMoney + "');</script>", false);


                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "Fairway", "<script>StarValue('rating_table_Fairway','" + reviews_item.RateFairway + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "green", "<script>StarValue('rating_table_green','" + reviews_item.Rategreen + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "difficult", "<script>StarValue('rating_table_difficult','" + reviews_item.RateDifficult + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "speed", "<script>StarValue('rating_table_speed','" + reviews_item.RateSpeed + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "caddy", "<script>StarValue('rating_table_caddy','" + reviews_item.RateSpeed + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "clubhouse", "<script>StarValue('rating_table_clubhouse','" + reviews_item.RateClubhouse + "');</script>", false);

                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "Food", "<script>StarValue('rating_table_Food','" + reviews_item.RateFood + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "performance", "<script>StarValue('rating_table_performance','" + reviews_item.RatePerformance + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "Punctuality", "<script>StarValue('rating_table_Punctuality','" + reviews_item.RatePunctuality + "');</script>", false);


                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "ability", "<script>StarValue('rating_table_ability','" + reviews_item.RateDiagnose_ability + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "Pronunciation", "<script>StarValue('rating_table_Pronunciation','" + reviews_item.RatePronunciation + "');</script>", false);
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "Knowledge", "<script>StarValue('rating_table_Knowledge','" + reviews_item.RateKnowledge + "');</script>", false);
                        
                        //Counttotal = ReviewManage.ReviewListTotal(byte.Parse(this.qreview), byte.Parse(this.qreviewtype));
                        
                   
                   
                
            }

            
            return strResult.ToString();
            
        }

        
       
    }
}