using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using Hotels2thailand;
using Hotels2thailand.Front;
using Hotels2thailand.Production;
/// <summary>
/// Summary description for ReviewManage
/// </summary>
/// 
namespace Hotels2thailand.Reviews
{
    
    public class ReviewGenerate:Hotels2BaseClass
    {
        private byte _product_cat;
        public byte ProductCat
        {
            get { return _product_cat; }
            set { _product_cat = value; }
        }
        

        public ReviewGenerate(byte bytProductCat)
        {
            this.ProductCat = bytProductCat;
        }

        
        public List<object> LoadReviewListShowByCatId(int intProduct_id)
        {
            ProductReviews cReview = new ProductReviews();
            //HttpContext.Current.Response.Write(ReviewManage.Query(true, this.ProductCat, false, false, false));
            //HttpContext.Current.Response.End();
            StringBuilder query = new StringBuilder();
            query.Append("SELECT r.review_id, r.product_id, pc.title As ProductTitle, pc.file_name_main, pc.file_name_review, co.folder_destination, r.cus_id, r.country_id,");
            query.Append(" ISNULL((SELECT c.title FROM tbl_country c WHERE c.country_id=r.country_id),'Not Identify') AS CountryTitle, r.recommend_id, rc.title As Recommedtitle,");
            query.Append(" r.from_id, rf.title As FromTitle, r.title, r.cat_id, r.detail,");
            query.Append(" ISNULL(r.positive,'not Identify'), ISNULL(r.negative,'not Identify'), r.full_name, r.review_from, ISNULL(r.rate_overall,CAST(0 as TinyInt)), ISNULL(r.rate_service,CAST(0 as TinyInt)), ISNULL");
            query.Append(" (r.rate_location,CAST(0 as TinyInt)), ISNULL(r.rate_room,CAST(0 as TinyInt)), ISNULL(r.rate_clean,CAST(0 as TinyInt)),");
            query.Append(" ISNULL(r.rate_money,CAST(0 as TinyInt)), ISNULL(r.rate_fairway,CAST(0 as TinyInt)), ISNULL(r.rate_green,CAST(0 as TinyInt)), ISNULL(r.rate_difficult,CAST(0 as TinyInt)), ISNULL(r.rate_speed,CAST(0 as TinyInt)), ISNULL(r.rate_caddy,CAST(0 as TinyInt)), ISNULL");
            query.Append(" (r.rate_clubhouse,CAST(0 as TinyInt)), ISNULL(r.rate_food,CAST(0 as TinyInt)), ISNULL(r.rate_performance,CAST(0 as TinyInt)), ISNULL(r.rate_punctuality,CAST(0 as TinyInt)),");
            query.Append(" ISNULL(r.rate_diagnose_ability,CAST(0 as TinyInt)), ISNULL(r.rate_pronunciation,CAST(0 as TinyInt)), ISNULL(r.rate_knowledge,CAST(0 as TinyInt)), r.vote_all, r.vote_usefull, r.date_travel, r.date_submit,");
            query.Append(" r.status, r.status_bin, p.cat_id FROM tbl_review_all r, tbl_product p , tbl_product_content pc,");
            query.Append(" tbl_destination co, tbl_review_recommend rc, tbl_review_from rf WHERE r.product_id = p.product_id AND rc.recommend_id = r.recommend_id AND rf.from_id = ");
            query.Append(" r.from_id AND p.product_id = pc.product_id AND pc.lang_id = 1 AND p.destination_id = co.destination_id AND");
            query.Append(" r.status= @status AND r.status_bin=@status_bin AND p.product_id=@product_id ORDER BY date_submit DESC");
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                //cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = this.ProductCat;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@status_bin", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cn.Open();
                return  MappingObjectCollectionFromDataReader(ExecuteReader(cmd), cReview);
            }
        }


        public string GetReviewXMLByProductId(int ProductId)
        {
            StringBuilder result = new StringBuilder();
            List<object> ReviewList=this.LoadReviewListShowByCatId(ProductId);
            result.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            result.Append("<Reviews>\n");

            int avgRateOverAll = 0;
            int avgRateService = 0;
            int avgRateLocation = 0;
            int avgRateRoom = 0;
            int avgRateClean = 0;
            int avgRateMoney = 0;

            foreach(ProductReviews item in ReviewList)
            {
                avgRateOverAll = avgRateOverAll + item.RateOverAll;
                avgRateService = avgRateService + item.RateService;
                avgRateLocation = avgRateLocation + item.RateLocation;
                avgRateRoom = avgRateRoom + item.RateRoom;
                avgRateClean = avgRateClean + item.RateClean;
                avgRateMoney = avgRateMoney + item.RateMoney;
            }

            int countReview = ReviewList.Count;

            result.Append("<Overall>" + (int)(avgRateOverAll / countReview) + "</Overall>\n");
            result.Append("<Service>" + (int)(avgRateService / countReview) + "</Service>\n");
            result.Append("<Location>" + (int)(avgRateLocation / countReview) + "</Location>\n");
            result.Append("<Room>" + (int)(avgRateRoom / countReview) + "</Room>\n");
            result.Append("<Clearness>" + (int)(avgRateClean / countReview) + "</Clearness>\n");
            result.Append("<Money>" + (int)(avgRateMoney / countReview) + "</Money>\n");
            

            foreach (ProductReviews review in ReviewList)
            {
                double bytePoint = 0;
                
                

                bytePoint = (review.RateOverAll + review.RateService + review.RateLocation + review.RateRoom + review.RateClean + review.RateMoney) / 6;
                result.Append("<Review>\n");
                result.Append("<Title>" + HttpContext.Current.Server.HtmlEncode(review.Title).Replace("'", "&#39;").Hotels2SPcharacter_removeONe() + "</Title>\n");
                result.Append("<Detail>" + HttpContext.Current.Server.HtmlEncode(review.Detail).Replace("'", "&#39;").Hotels2SPcharacter_removeONe() + "</Detail>\n");
                result.Append("<Point>" + bytePoint + "</Point>\n");
                result.Append("<PostBy>" + HttpContext.Current.Server.HtmlEncode(review.FullName).Replace("'", "&#39;").Hotels2SPcharacter_removeONe() + "</PostBy>\n");
                result.Append("<Country>"+review.ReviewForm+"</Country>\n");
                result.Append("<DateReview>"+review.DateSubmit+"</DateReview>\n");
                result.Append("</Review>\n");
                
            }
            result.Append("</Reviews>\n");
            return result.ToString();
        }
        //public void GenrateReviewAllProduct()
        //{
        //    //bool IsCompleted = false;
        //    Hotels2thailand.Production.Destination cDestination = new Production.Destination();
        //    foreach (KeyValuePair<string, string> des in cDestination.GetDestinationAll())
        //    {
        //        Hotels2thailand.Production.Product cProduct = new Production.Product();
        //        if (cProduct.GetProductByDestionIdAndCatIdAll(short.Parse(des.Key), this.ProductCat).Count > 0)
        //        {
        //            foreach (int ProductId in cProduct.GetProductByDestionIdAndCatIdAll(short.Parse(des.Key), this.ProductCat))
        //            {
        //               HttpContext.Current.Response.Write(ProductId + "<br/>");
        //               HttpContext.Current.Response.Flush();
        //               ReviewGenerate cGen = new ReviewGenerate(this.ProductCat);
        //               HttpContext.Current.Response.Write(cGen.GenerateReviewPage(ProductId) + "<br/>");
        //               HttpContext.Current.Response.Flush();
        //               //try
        //               //{
                           
        //               //    //HttpContext.Current.Response.Flush();
        //               //}
        //               //catch { }
                       
        //            }
        //        }
        //    }
            
        //}

        

        public string GenerateReviewPage(int intProductId)
        {    
            string PageResult = string.Empty;
            string strHotelFileName = string.Empty;
            string strReviewFileName = string.Empty;
            string HotelReviewFilename = string.Empty;

            string HotelFilename = string.Empty;
            string FolderResult = string.Empty;
            string path = string.Empty;
            string HotelName = string.Empty;
            
                //bool Ret = false;

            

            //GenerateProductPage page = new GenerateProductPage();
            //string TemplateReviews = page.GetReviewTemplate(intProductId, this.ProductCat);

            List<object> result = LoadReviewListShowByCatId(intProductId);
            //string TemplateReviews = "test";
            if (result.Count() > 0)
            {
                
                

                    if (result.ToList()[0] != null)
                    {
                        ProductReviews objResult = (ProductReviews)result.ToList()[0];
                        

                        //FolderResult = objResult.FolderDestination + "-" + Utility.GetProductType(this.ProductCat)[0, 3];
                        HotelName = objResult.ProductTitle;
                        HotelFilename = objResult.FileName;
                        //HotelReviewFilename = objResult.ReivewsFileName;
                        //string HotelReviewFilename = result.ToList()[1].GetType().GetProperty("ReivewsFileName").GetValue(result.ToList()[1], null).ToString()
                        //path = HttpContext.Current.Server.MapPath("/" + FolderResult);

                        //strHotelFileName = path + "/" + HotelFilename;
                        //strReviewFileName = path + "/" + HotelReviewFilename;
                        PageResult = this.GetResultReviewListShowByProductId(result, HotelName, HotelFilename, intProductId);
                        //DirectoryInfo DestinationProduct = new DirectoryInfo(path);
                        ////FileInfo FileNameReview = new FileInfo(strFileName);
                        //PageResult = this.GetResultReviewListShowByProductId(result, HotelName, HotelFilename, intProductId);
                        //if (!DestinationProduct.Exists)
                        //{
                        //    DestinationProduct.Create();
                        //}


                        ////Get Main Template From Visa


                        //StreamWriter StrWer = default(StreamWriter);
                        //StrWer = File.CreateText(strReviewFileName);
                        //StrWer.Write(PageResult);
                        //StrWer.Close();

                        PageResult = this.GetResultReviewListShowByProductId(result, HotelName, HotelFilename, intProductId);

                    }
                
            }
            else
            {
                
                ProductContent cProductContent = new ProductContent();
                Destination cDestination = new Destination();
                
                //FolderResult = cDestination.GetDestinationFolderNameByProcutId(intProductId) + "-" + Utility.GetProductType(this.ProductCat)[0, 3];
                //path = HttpContext.Current.Server.MapPath("/" + FolderResult);

                HotelFilename = cProductContent.GetProductContentFileName(intProductId, 1)[1].ToString();
                HotelName = cProductContent.GetProductContentFileName(intProductId, 1)[0].ToString();

                //HotelReviewFilename = cProductContent.GetProductContentReviewFileName(intProductId, 1)[1].ToString();

                //strHotelFileName = path + "/" + HotelFilename;
                //strReviewFileName = path + "/" + HotelReviewFilename;

                //DirectoryInfo DestinationProduct = new DirectoryInfo(path);
                PageResult = this.GetResultReviewListShowByProductId(result, HotelName, HotelFilename, intProductId);
                //if (!DestinationProduct.Exists)
                //{
                //    DestinationProduct.Create();
                //}

                //StreamWriter StrWer = default(StreamWriter);
                //StrWer = File.CreateText(strReviewFileName);
                //StrWer.Write(PageResult);
                //StrWer.Close();
            }

            return PageResult;
        }

        private string RateAverage(int RateListTotal, int SumRate)
        {
            //double Average = (Convert.ToDouble(SumRate) / RateListTotal)*2;
            double Average = (Convert.ToDouble(SumRate) / RateListTotal) ;
            return Average.ToString("0.0");
        }

        private string RateAverageItem(byte bytProductCat, params int[] arrRate)
        {
            double SumRate = 0;
            double Average = 0;
            foreach (int rate in arrRate)
            {
                SumRate = SumRate + rate;
            }
            //Average = (SumRate / arrRate.Count())*2;
            Average = (SumRate / arrRate.Count());
            return Average.ToString("0.0");
            
        }

        private string RateAverageTotal(int RateListTotal, params int[] arrSumRate)
        {
            double SumRate = 0;
            double Average = 0;
            double AverageTotal = 0;
            foreach (int rate in arrSumRate)
            {
                SumRate = SumRate + rate;
                
            }
            Average = SumRate / RateListTotal;
            //AverageTotal = (Average / arrSumRate.Count())*2;
            AverageTotal = (Average / arrSumRate.Count()) ;
            return AverageTotal.ToString("0.0");
        }

        public string GetResultReviewListShowByProductId(List<object> result, string HotelName, string HotelNameFilename, int intPtoductId)
        {
            int ReviewAmount = result.Count();
            //ProductReviews objResult = (ProductReviews)result.ToList()[0];
            //string HotelName = objResult.ProductTitle;
            //string HotelNameFilename = objResult.FileName;

            //string HotelName = result.ToList()[1].GetType().GetProperty("ProductTitle").GetValue(result.ToList()[1], null).ToString();
            //string HotelNameFilename = result.ToList()[1].GetType().GetProperty("FileName").GetValue(result.ToList()[1], null).ToString();
            //ProductReviews cReview = new ProductReviews();
            //byte ProductCat = (byte)result.FirstOrDefault().GetType().GetProperty("ProductCat").GetValue(cReview, null);
            int Sum_OverAll = 0;
            int Sum_RateFairway = 0;
            int Sum_Rategreen = 0;
            int Sum_RateDifficult = 0;
            int Sum_RateSpeed = 0;
            int Sum_RateCaddy = 0;
            int Sum_RateClubhouse = 0;
            int Sum_RatePerformance = 0;
            int Sum_RateKnowledge = 0;
            int Sum_RateService = 0;
            int Sum_RatePronunciation = 0;
            int Sum_RatePunctuality = 0;
            int Sum_RateDiagnose_ability = 0;
            int Sum_RateFood = 0;
            int Sum_RateLocation = 0;
            int Sum_RateRoom = 0;
            int Sum_RateClean = 0;
            int Sum_RateMoney = 0;

            StringBuilder ReviewResult = new StringBuilder();
            ReviewResult.Append("<script type=\"text/javascript\" language=\"javascript\">");
            ReviewResult.Append("$(document).ready(function () {");
            ReviewResult.Append("var Totalwide = 149;");
            ReviewResult.Append("var TotalRateFull = 10;");
            ReviewResult.Append("$(\"#condition_bg div\").filter(function (index) {");
            ReviewResult.Append("if ($(this).attr(\"class\") == \"review_bar_rate_result\") {");
            ReviewResult.Append("var rate = $(this).html();");
            ReviewResult.Append("var wideresult = (rate * Totalwide) / TotalRateFull;");
            ReviewResult.Append("$(this).css(\"width\", \"\" + wideresult + \"px\");");
            ReviewResult.Append("$(this).html(\"\");");
            ReviewResult.Append("}");
            ReviewResult.Append("})");
            ReviewResult.Append("});");
            ReviewResult.Append("function PageBack(position_id) {");
            ReviewResult.Append("$('html, body').animate({ scrollTop: $(\"#\" + position_id).offset().top - 100 }, 500);");
            ReviewResult.Append("}");
            ReviewResult.Append("</script>");
            

            ReviewResult.Append("<div id=\"content_why\">");
            ReviewResult.Append("<div id=\"why_us_header\"><h4>Traveler Reviews</h4></div>");
            ReviewResult.Append("");
            if (result.Count() > 0)
            {
                ReviewResult.Append("<div id=\"review\">");
                ReviewResult.Append("<a href=\"" + HotelNameFilename + "\">" + HotelName + "</a> <span> Traveler Reviews</span>");
                ReviewResult.Append("<p>The guest reviews are submitted by our customers after their stay</p>");
                ReviewResult.Append("<div class=\"review_box\">");
                ReviewResult.Append("<div id=\"review_condition\">");
                ReviewResult.Append("<div id=\"condition_bg\">");
                ReviewResult.Append("");
                ReviewResult.Append("");

                Sum_OverAll = result.Sum(r => (byte)r.GetType().GetProperty("RateOverAll").GetValue(r, null));
                
                ReviewResult.Append("<div class=\"review_bar\">Overall");
                ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_OverAll) + "</div>");
                ReviewResult.Append("</div>");
                ReviewResult.Append("</div>");
                ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_OverAll) + "</div>");

               

                if (this.ProductCat == 32)
                {
                    Sum_RateFairway = result.Sum(r => (byte)r.GetType().GetProperty("RateFairway").GetValue(r, null));
                    Sum_Rategreen = result.Sum(r => (byte)r.GetType().GetProperty("Rategreen").GetValue(r, null));
                    Sum_RateDifficult = result.Sum(r => (byte)r.GetType().GetProperty("RateDifficult").GetValue(r, null));
                    Sum_RateSpeed = result.Sum(r => (byte)r.GetType().GetProperty("RateSpeed").GetValue(r, null));
                    Sum_RateCaddy = result.Sum(r => (byte)r.GetType().GetProperty("RateCaddy").GetValue(r, null));
                    Sum_RateClubhouse = result.Sum(r => (byte)r.GetType().GetProperty("RateClubhouse").GetValue(r, null));

                    ReviewResult.Append("<div class=\"review_bar\">FairWay");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateFairway) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateFairway) + "</div>");

                    ReviewResult.Append("<div class=\"review_bar\">Green");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_Rategreen) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_Rategreen) + "</div>");

                    ReviewResult.Append("<div class=\"review_bar\">Difficult");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateDifficult) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateDifficult) + "</div>");

                    ReviewResult.Append("<div class=\"review_bar\">Speed");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateSpeed) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateSpeed) + "</div>");

                    ReviewResult.Append("<div class=\"review_bar\">Caddy");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateSpeed) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateSpeed) + "</div>");

                    ReviewResult.Append("<div class=\"review_bar\">Club House");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateSpeed) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateSpeed) + "</div>");
                }
                    
                

                if (this.ProductCat == 38)
                {
                    Sum_RatePerformance = result.Sum(r => (byte)r.GetType().GetProperty("RatePerformance").GetValue(r, null));
                    //Performance
                    ReviewResult.Append("<div class=\"review_bar\">Performance");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\"></div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RatePerformance) + "</div>");
                }

                if (this.ProductCat == 34 || this.ProductCat == 36)
                {
                    Sum_RateKnowledge = result.Sum(r => (byte)r.GetType().GetProperty("RateKnowledge").GetValue(r, null));
                    //Knowledge
                    ReviewResult.Append("<div class=\"review_bar\">Knowledge");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateKnowledge) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateKnowledge) + "</div>");
                }

                if (this.ProductCat != 32 && this.ProductCat != 39)
                {
                    Sum_RateService = result.Sum(r => (byte)r.GetType().GetProperty("RateService").GetValue(r, null));
                    //Service
                    ReviewResult.Append("<div class=\"review_bar\">Service");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateService) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateService) + "</div>");
                }


                if (this.ProductCat == 34 || this.ProductCat == 36)
                {
                    Sum_RatePronunciation = result.Sum(r => (byte)r.GetType().GetProperty("RatePronunciation").GetValue(r, null));
                    //Pronunciation
                    ReviewResult.Append("<div class=\"review_bar\">Pronunciation");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RatePronunciation) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RatePronunciation) + "</div>");
                }

                if (this.ProductCat == 34 || this.ProductCat == 36 || this.ProductCat == 38)
                {
                    Sum_RatePunctuality = result.Sum(r => (byte)r.GetType().GetProperty("RatePunctuality").GetValue(r, null));
                    //Punctuality
                    ReviewResult.Append("<div class=\"review_bar\">Punctuality");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RatePunctuality) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RatePunctuality) + "</div>");
                }

                if (this.ProductCat == 39)
                {
                    Sum_RateDiagnose_ability = result.Sum(r => (byte)r.GetType().GetProperty("RateDiagnose_ability").GetValue(r, null));
                    //Diagnose Ability
                    ReviewResult.Append("<div class=\"review_bar\">Diagnose Ability");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateDiagnose_ability) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateDiagnose_ability) + "</div>");
                }

                if (this.ProductCat == 34 || this.ProductCat == 36 || this.ProductCat == 32)
                {
                    Sum_RateFood = result.Sum(r => (byte)r.GetType().GetProperty("RateFood").GetValue(r, null));
                    //Food
                    ReviewResult.Append("<div class=\"review_bar\">Food");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateFood) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateFood) + "</div>");
                }

                if (this.ProductCat == 29 || this.ProductCat == 40)
                {
                    Sum_RateLocation = result.Sum(r => (byte)r.GetType().GetProperty("RateLocation").GetValue(r, null));
                    Sum_RateRoom = result.Sum(r => (byte)r.GetType().GetProperty("RateRoom").GetValue(r, null));
                    //Location
                    ReviewResult.Append("<div class=\"review_bar\">Location");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateLocation) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateLocation) + "</div>");
                
                    // Rooms
                    ReviewResult.Append("<div class=\"review_bar\">Rooms");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateRoom) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateRoom) + "</div>");
                }

                if (this.ProductCat == 29 || this.ProductCat == 40)
                {
                    Sum_RateClean = result.Sum(r => (byte)r.GetType().GetProperty("RateClean").GetValue(r, null));
                    // Cleanliness
                    ReviewResult.Append("<div class=\"review_bar\">Cleanliness");
                    ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                    ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateClean) + "</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("</div>");
                    ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateClean) + "</div>");
                }


                Sum_RateMoney = result.Sum(r => (byte)r.GetType().GetProperty("RateMoney").GetValue(r, null));
                // Value for money
                ReviewResult.Append("<div class=\"review_bar\">Value for money");
                ReviewResult.Append("<div class=\"review_bar_rate_container\">");
                ReviewResult.Append("<div class=\"review_bar_rate_result\">" + RateAverage(ReviewAmount, Sum_RateMoney) + "</div>");
                ReviewResult.Append("</div>");
                ReviewResult.Append("</div>");
                ReviewResult.Append("<div class=\"score\">" + RateAverage(ReviewAmount, Sum_RateMoney) + "</div>");

                ReviewResult.Append("</div>");

                string RateTotal = string.Empty;
               
                switch(this.ProductCat)
                {
                        case 29 :
                        RateTotal = RateAverageTotal(ReviewAmount,Sum_OverAll,Sum_RateService,Sum_RateLocation,Sum_RateRoom,Sum_RateClean,Sum_RateMoney);
                    break;
                        case 32 :
                        RateTotal = RateAverageTotal(ReviewAmount,Sum_OverAll,Sum_RateFairway,Sum_Rategreen,Sum_RateDifficult,Sum_RateSpeed,Sum_RateCaddy,Sum_RateClubhouse,Sum_RateFood);
                    break;
                        case 34 :
                        RateTotal = RateAverageTotal(ReviewAmount,Sum_OverAll,Sum_RateKnowledge,Sum_RateService,Sum_RatePronunciation,Sum_RatePunctuality,Sum_RateFood,Sum_RateMoney);
                    break;
                        case 36 :
                        RateTotal = RateAverageTotal(ReviewAmount,Sum_OverAll,Sum_RateKnowledge,Sum_RateService,Sum_RatePronunciation,Sum_RatePunctuality,Sum_RateFood,Sum_RateMoney);
                    break;
                        case 38 :
                        RateTotal = RateAverageTotal(ReviewAmount,Sum_OverAll,Sum_RatePerformance,Sum_RatePunctuality,Sum_RateService,Sum_RateMoney);
                    break;
                        case 39 :
                        RateTotal = RateAverageTotal(ReviewAmount,Sum_OverAll,Sum_RateClean,Sum_RateDiagnose_ability,Sum_RateService,Sum_RateMoney);
                    break;
                        case 40 :
                        RateTotal = RateAverageTotal(ReviewAmount, Sum_OverAll, Sum_RateService, Sum_RateLocation, Sum_RateRoom, Sum_RateClean, Sum_RateMoney);
                    break;


                }
                
                //int ProductId = (int)result.GetType().GetProperty("product_id").GetValue(result,null);
                ReviewResult.Append("<div id=\"score_total\">\r\n");
                ReviewResult.Append("<p>" + RateTotal + "</p>\r\n");
                ReviewResult.Append("</div>\r\n");
                ReviewResult.Append("<div id=\"write_review\">Been here before? Tell us what you think! <br />\r\n");
                ReviewResult.Append("<a href=\"/review_write.aspx?pid=" + intPtoductId + "\">Write a Review...</a>\r\n");
                ReviewResult.Append("</div>\r\n");


                ReviewResult.Append("</div>\r\n");
                ReviewResult.Append("</div><br />\r\n");

                ReviewResult.Append("<div id=\"header_left\">\r\n");
                ReviewResult.Append("<h4>Individual guest reviews for Planet</h4>\r\n");
                ReviewResult.Append("<p>Reviews are ordered by date with a maximum of <span>" + ReviewAmount + " reviews</span> per page.</p>\r\n");
                ReviewResult.Append("</div>\r\n");

                ReviewResult.Append("<br class=\"clear-all\" />\r\n");
                ReviewResult.Append("<div id=\"review_header\"><h4>Showing 1 - " + ReviewAmount + " (Total " + ReviewAmount + ") </h4></div>\r\n");


                ReviewResult.Append("<div class=\"review_list_block\">\r\n");

                int Rowcount = 0;
                foreach (ProductReviews ReviewItem in result)
                {
                    if ((Rowcount % 2) == 0)
                    {
                        ReviewResult.Append("<div class=\"review_list_second\">\r\n");
                    }
                    else
                    {
                        ReviewResult.Append("<div class=\"review_list_first\">\r\n");
                    }
                    //ReviewResult.Append("<div class=\"review_list_first\">\r\n");
                    ReviewResult.Append("<div class=\"review_title\">" + ReviewItem.Title + "</div>\r\n");

                    ReviewResult.Append("<div class=\"review_user\">\r\n");
                    ReviewResult.Append("<div class=\"bg_top\">\r\n");
                    ReviewResult.Append("<div class=\"bg_left\"> <div class=\"review_user_icon\"> </div> </div>\r\n");
                    ReviewResult.Append("<div class=\"bg_right\">\r\n");

                    ReviewResult.Append("<div class=\"from\">by " + ReviewItem.FullName + " from " + ReviewItem.ReviewForm + "</div>\r\n");

                    if (ReviewItem.FromTitle != "Not Identify")
                    {
                        ReviewResult.Append("<div class=\"group\">" + ReviewItem.FromTitle + "</div>\r\n");
                    }

                    ReviewResult.Append("</div></div>\r\n");
                    ReviewResult.Append("<div class=\"bg_top\">\r\n");
                    ReviewResult.Append("<div class=\"bg_left\"> <div class=\"review_user_flag\">\r\n");

                    if (ReviewItem.CountryId != null)
                    {
                        ReviewResult.Append("<img src=\"theme_color/blue/images/flag/flag_" + ReviewItem.CountryId + ".jpg\" title=\"" + ReviewItem.Countrytitle + "\" />\r\n");
                    }

                    ReviewResult.Append("</div> </div>\r\n");


                    ReviewResult.Append("<div class=\"bg_right\">\r\n");

                    if (ReviewItem.CountryId != null)
                    {
                        ReviewResult.Append("<div class=\"country\">" + ReviewItem.Countrytitle + "</div>\r\n");
                    }

                    ReviewResult.Append("<div class=\"date\">" + ReviewItem.DateSubmit.ToString("MMM d, yyyy") + "</div>\r\n");
                    ReviewResult.Append("</div></div></div>\r\n");
                    ReviewResult.Append("<div class=\"review_txt\">\r\n");

                    if (!string.IsNullOrEmpty(ReviewItem.Detail))
                    {
                        if ((Rowcount % 2) == 0)
                        {
                            ReviewResult.Append("<div class=\"review_detail_icon_b\">\r\n");
                        }
                        else
                        {
                            ReviewResult.Append("<div class=\"review_detail_icon\">\r\n");
                        }


                        ReviewResult.Append("<div class=\"review_pos\">" + NewlineManage(ReviewItem.Detail) + "</div>\r\n");
                        ReviewResult.Append("</div>\r\n");
                    }

                    //if (ReviewItem.Positive != "not Identify")
                    //{
                    //    ReviewResult.Append("<div class=\"review_pos_icon\">\r\n");
                    //    ReviewResult.Append("<div class=\"review_pos\">" + ReviewItem.Positive + "</div>\r\n");
                    //    ReviewResult.Append("</div>\r\n");
                    //}

                    //if (ReviewItem.Nagative != "not Identify")
                    //{
                    //    ReviewResult.Append("<div class=\"review_neg_icon\">\r\n");
                    //    ReviewResult.Append("<div class=\"review_pos\">" + ReviewItem.Nagative + "</div>\r\n");
                    //    ReviewResult.Append("</div>");
                    //}

                    string RateTotalItem = string.Empty;
                    switch (this.ProductCat)
                    {
                        case 29:
                            RateTotalItem = RateAverageItem(ReviewItem.RateOverAll, ReviewItem.RateService, ReviewItem.RateLocation, ReviewItem.RateRoom, ReviewItem.RateClean, ReviewItem.RateMoney);
                            break;
                        case 32:
                            RateTotalItem = RateAverageItem(ReviewItem.RateOverAll, ReviewItem.RateFairway, ReviewItem.Rategreen, ReviewItem.RateDifficult, ReviewItem.RateSpeed, ReviewItem.RateCaddy, ReviewItem.RateClubhouse, ReviewItem.RateFood);
                            break;
                        case 34:
                            RateTotalItem = RateAverageItem(ReviewItem.RateOverAll, ReviewItem.RateKnowledge, ReviewItem.RateService, ReviewItem.RatePronunciation, ReviewItem.RatePunctuality, ReviewItem.RateFood, ReviewItem.RateMoney);
                            break;
                        case 36:
                            RateTotalItem = RateAverageItem(ReviewItem.RateOverAll, ReviewItem.RateKnowledge, ReviewItem.RateService, ReviewItem.RatePronunciation, ReviewItem.RatePunctuality, ReviewItem.RateFood, ReviewItem.RateMoney);
                            break;
                        case 38:
                            RateTotalItem = RateAverageItem(ReviewItem.RateOverAll, ReviewItem.RatePerformance, ReviewItem.RatePunctuality, ReviewItem.RateService, ReviewItem.RateMoney);
                            break;
                        case 39:
                            RateTotalItem = RateAverageItem(ReviewItem.RateOverAll, ReviewItem.RateClean, ReviewItem.RateDiagnose_ability, ReviewItem.RateService, ReviewItem.RateMoney);
                            break;
                        case 40:
                            RateTotalItem = RateAverageItem(ReviewItem.RateOverAll, ReviewItem.RateService, ReviewItem.RateLocation, ReviewItem.RateRoom, ReviewItem.RateClean, ReviewItem.RateMoney);
                            break;


                    }

                    if ((Rowcount % 2) == 0)
                    {
                        ReviewResult.Append("</div><div class=\"score_list\"> <div class=\"score\">" + RateTotalItem + "</div> </div>\r\n");
                    }
                    else
                    {
                        ReviewResult.Append("</div><div class=\"score_list_w\"> <div class=\"score\">" + RateTotalItem + "</div> </div>\r\n");
                    }
                    ReviewResult.Append("<div class=\"clear-all\"></div></div>\r\n");
                    

                    Rowcount = Rowcount + 1;
                }
                ReviewResult.Append("</div>\r\n");
                ReviewResult.Append("<br class=\"clear-all\" />\r\n ");
                ReviewResult.Append("");
            }
            else
            {
                ReviewResult.Append("<div id=\"review_blank\"><br /> ");
                ReviewResult.Append("<a href=\"" + HotelNameFilename + "\">" + HotelName + "</a><span> Traveler Reviews</span><br /> <br />   ");
                ReviewResult.Append("<div id=\"review_condition\">");
                ReviewResult.Append("<div id=\"write_review\">Been here before? Tell us what you think! <br />");
                ReviewResult.Append("<a href=\"/review_write.aspx?pid=" + intPtoductId + "\" class=\"review_buttom\"></a>");
                ReviewResult.Append("</div>");
                ReviewResult.Append("</div><!--review_condition-->");
                ReviewResult.Append("</div>");

            }

            ReviewResult.Append("</div>\r\n");
            ReviewResult.Append("<div class=\"backtotop_r\"><a href=\"javascript:void(0)\"  onclick=\"PageBack('review');\">Back to top</a></div>\r\n");
            ReviewResult.Append("</div>\r\n");
            
            


           //string ReviewPageResult = string.Empty;
           //ReviewPageResult = TemplateReviews.Replace("<--##@ReviewList##-->", ReviewResult.ToString());
           // //replace page title
           //ReviewPageResult = ReviewPageResult.Replace("<!--##product_title##-->", HotelName + " Reviews");

           //ReviewPageResult = ReviewPageResult.Replace("http://174.36.32.56", "http://www.hotels2thailand.com");
            
           //StringBuilder ReviewPageResult = new StringBuilder();
            return NewlineManage(ReviewResult.ToString());
           //return ReviewPageResult.ToString();
        }

        public string NewlineManage(string strinput)
        {
            return strinput.Replace("\r\n", "<br/>");
        }


    }
}