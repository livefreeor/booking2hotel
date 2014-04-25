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
    public partial class admin_ajax_review_post_detail : Hotels2BasePageExtra_Ajax
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
                //Response.Write("KKK");
                Response.Write(reviewResult(this.qreview,int.Parse(this.qReviewId)));
                Response.Flush();
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

        public string reviewResult(string ProductCat, int intreviewId)
        {
            StringBuilder strResult = new StringBuilder();
            
                    if (!string.IsNullOrEmpty(this.qreview) && !string.IsNullOrEmpty(this.qReviewId))
                    {
                        ProductReviews reviews_item = new ProductReviews();
                        reviews_item = ReviewManage.GetreviewById(int.Parse(this.qReviewId));
                        strResult.Append("<div class=\"review_detail\" style=\"margin:0px 0px 0px 0px\">");
                        strResult.Append("<div class=\"review_detail_detail\"><br/>");
                        strResult.Append("<p>" + reviews_item.Detail + "</p>");
                        strResult.Append("</div>");
                        strResult.Append("<div class=\"review_detail_rate_and_comment\">");
                        strResult.Append("<div class=\"review_detail_rate\">");
                        strResult.Append("<table>");

                        switch (this.qreview)
                        {
                            case "hotels":
                                strResult.Append("<tr><td class=\"review_rate_head\">Service :</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateService) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Location : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateLocation) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Rooms : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateRoom) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Cleanliness : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateClean) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Value of :<br/> Money</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateMoney) + "\"></td></tr>");
                                break;

                            case "golfs":
                                strResult.Append("<tr><td class=\"review_rate_head\">Fairway :</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateFairway) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Green : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.Rategreen) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Difficult : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateDifficult) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Speed : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateSpeed) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Caddy : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateCaddy) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Clubhouse : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateClubhouse) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Food : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateFood) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Value of :<br/> Money</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateMoney) + "\"></td></tr>");
                                break;

                            case "daytrips":
                                strResult.Append("<tr><td class=\"review_rate_head\">Knowledge : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateKnowledge) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Service :</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateService) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Pronunciation : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RatePronunciation) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Punctuality : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RatePunctuality) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Food : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateFood) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Value of :<br/> Money</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateMoney) + "\"></td></tr>");
                                break;

                            case "waters":
                                strResult.Append("<tr><td class=\"review_rate_head\">Knowledge : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateKnowledge) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Service :</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateService) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Pronunciation : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RatePronunciation) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Punctuality : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RatePunctuality) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Food : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateFood) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Value of :<br/> Money</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateMoney) + "\"></td></tr>");
                                break;

                            case "show":
                                strResult.Append("<tr><td class=\"review_rate_head\">Performance :</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RatePerformance) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Punctuality : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RatePunctuality) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Service : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateService) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Value of :<br/> Money</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateMoney) + "\"></td></tr>");
                                break;

                            case "health":
                                strResult.Append("<tr><td class=\"review_rate_head\">Cleanliness :</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateClean) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Diagnose_ability : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateDiagnose_ability) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Service : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateService) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Value of :<br/> Money</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateMoney) + "\"></td></tr>");
                                break;

                            case "spa":
                                strResult.Append("<tr><td class=\"review_rate_head\">Service :</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateService) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Location : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateLocation) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Rooms : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateRoom) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Cleanliness : </td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateClean) + "\"></td></tr>");
                                strResult.Append("<tr><td class=\"review_rate_head\">Value of :<br/> Money</td><td><img src=\"../../images/" + imgeReviewRatePath(reviews_item.RateMoney) + "\"></td></tr>");
                                break;
                        
                        
                        }
                        strResult.Append("</table>");
                       
                        strResult.Append("</div>");
                        //strResult.Append("<div class=\"review_detail_comment\"");
                        //strResult.Append("<p><img src=\"../../images/plus_s.png\">&nbsp;" + reviews_item.Positive + "</p>");
                        //strResult.Append("<p><img src=\"../../images/minus.png\">&nbsp;" + reviews_item.Nagative + "</p>");
                        //strResult.Append("</div>");
                        strResult.Append("</div>");
                        strResult.Append("<div style=\"clear:both;\"></div>");
                        //strResult.Append("<div class=\"review_detail_information\">");
                        //if (reviews_item.DateTravel == null)
                        //    strResult.Append("<p><span class=\"review_head\">Datetravel</span> :&nbsp;(&nbsp;Not Identify &nbsp;)&nbsp;</p>");
                        //else
                        //{
                        //    DateTime dDateTravel = (DateTime)reviews_item.DateTravel;
                        //    strResult.Append("<p><span class=\"review_head\">Datetravel</span> :&nbsp;(&nbsp;" + dDateTravel.ToString("MMM d, yyyy") + "&nbsp;)&nbsp;</p>");
                        //}
                        //strResult.Append("<p><span class=\"review_head\">Country</span> &nbsp;: &nbsp;" + reviews_item.Countrytitle + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class=\"review_head\">Group travel</span>&nbsp;: &nbsp;" + reviews_item.FromTitle + "</p>");
                        //strResult.Append("<p><span class=\"review_head\">This hotel was recommended for</span> &nbsp;: &nbsp;" + reviews_item.ReccomTitle + "</p>");

                        //strResult.Append("</div>");
                        strResult.Append("<p class=\"review_detail_link\">");
                        strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id=\"aEdit\" href=\"javascript:getReviewEdit('" + reviews_item.ReviewId + "')\" >Edit</a>");

                        //Approve Page
                        if (reviews_item.Status && reviews_item.StatusBin)
                        {
                            strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id=\"aUnApprove" + reviews_item.ReviewId + "\" href=\"javascript:UpdatestatusConfirmBox('aUnApprove" + reviews_item.ReviewId + "','Are you sure to UnApprove This Review?','" + reviews_item.ReviewId + "','unapprove')\">UnApprove</a>");
                            strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id=\"aBin" + reviews_item.ReviewId + "\" href=\"javascript:UpdatestatusConfirmBox('aBin" + reviews_item.ReviewId + "','Are you sure to remove To The Bin?','" + reviews_item.ReviewId + "','bin')\">Bin</a>");
                        }

                        //New review & Unapprove Page
                        if (!reviews_item.Status && reviews_item.StatusBin)
                        {
                            strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a  id=\"aApprove" + reviews_item.ReviewId + "\" href=\"javascript:UpdatestatusConfirmBox('aApprove" + reviews_item.ReviewId + "','Are you sure to Approve This Review?','" + reviews_item.ReviewId + "','approve')\">Approve</a>");
                            strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id=\"aBin" + reviews_item.ReviewId + "\" href=\"javascript:UpdatestatusConfirmBox('aBin" + reviews_item.ReviewId + "','Are you sure to remove To The Bin?','" + reviews_item.ReviewId + "','bin')\">Bin</a>");
                        }
                        
                        //bin Page
                        if (!reviews_item.Status && !reviews_item.StatusBin)
                        {
                            strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a  id=\"aApprove" + reviews_item.ReviewId + "\" href=\"javascript:UpdatestatusConfirmBox('aApprove" + reviews_item.ReviewId + "','Are you sure to Approve This Review?','" + reviews_item.ReviewId + "','approve')\">Approve</a>");
                            strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id=\"aRemoveBin" + reviews_item.ReviewId + "\" href=\"javascript:UpdatestatusConfirmBox('aRemoveBin" + reviews_item.ReviewId + "','Are you sure to remove From The Bin?','" + reviews_item.ReviewId + "','unbin')\">Remove from the Bin</a>");
                            //strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id=\"aBin" + reviews_item.ReviewId + "\" href=\"javascript:UpdatestatusConfirmBox('aBin" + reviews_item.ReviewId + "','if you want me?','" + reviews_item.ReviewId + "','bin')\">Bin</a>");
                        }
                        

                        strResult.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id=\"aApprove" + reviews_item.ReviewId + "\" href=\"javascript:DisableDiv('review_item_" + reviews_item.ReviewId + "')\">Hide</a>");
                        strResult.Append("</p>");
                        strResult.Append("</div>");

                        //Counttotal = ReviewManage.ReviewListTotal(byte.Parse(this.qreview), byte.Parse(this.qreviewtype));
                        
                    }
             
            return strResult.ToString();
            
        }

        
       
    }
}