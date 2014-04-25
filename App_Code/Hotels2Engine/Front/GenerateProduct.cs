using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Front;
using System.IO;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using Hotels2thailand;
using Hotels2thailand.Reviews;
/// <summary>
/// Summary description for GenerateProduct
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class GenerateProduct:Hotels2BaseClass
    {
        private Navigator nav;
        private FrontProductRelate objProductRelate;
        private string pathDestination = string.Empty;
        private string layout = string.Empty;
        private List<FrontProductPicture> pictureList;
        private string ggMainImage = string.Empty;
        private byte LangID = 1;
        private string urlMain = string.Empty;

        private string GetStreamReader(string path)
        {

            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath(path));
            string read = objReader.ReadToEnd();
            objReader.Close();
            return read;
        }

        public GenerateProduct()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void GenerateProductByID(int ProductID, byte CategoryID, byte langID)
        {
            LangID = langID;
            FrontProductDetail detail = new FrontProductDetail();
            nav = new Navigator();
            nav.LangID = langID;
            objProductRelate = new FrontProductRelate();
            objProductRelate.LangID = langID;
            detail.GetProductDetailByID(ProductID, CategoryID, langID);
            GenerateFileProduct(detail, langID);
            
            
            if(detail.CategoryID==29)
            {
                GenerateOption room = new GenerateOption();
                room.GenertarateRoomDetail(detail.ProductID,langID);
            }
            

        }

        public void GenerateProductByCategory(byte CategoryID,byte langID)
        {
            //Default Language
            LangID = langID;

            //int ProductID = int.Parse(Request.QueryString["pid"]);
            //

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select product_id,title from tbl_product where  cat_id=" + CategoryID + " and product_id <> 56 and status=1 order by title asc";
                //HttpContext.Current.Response.Write(sqlCommand);
                //HttpContext.Current.Response.End();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();



                FrontProductDetail detail = new FrontProductDetail();
                nav = new Navigator();
                nav.LangID = LangID;
                objProductRelate = new FrontProductRelate();
                objProductRelate.LangID = LangID;
                string layout = string.Empty;

                while (reader.Read())
                {

                    //try
                    //{
                    
                    detail.GetProductDetailByID((int)reader["product_id"], CategoryID, LangID);
                    GenerateFileProduct(detail, LangID);
                    if (CategoryID == 29)
                    {
                        GenerateOption room = new GenerateOption();
                        room.GenertarateRoomDetail((int)reader["product_id"], langID);
                    }
                    //}
                    //catch
                    //{

                    //}

                }
            }
            
        }

        public void GenerateReviewApprove(int reviewID,byte LangID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select top 1 product_id,cat_id from tbl_review_all where review_id=" + reviewID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    GeneratePageGeneral generalPage = new GeneratePageGeneral();
                    generalPage.LangID = LangID;
                    generalPage.GenPageIndex();
                    FrontProductDetail detail = new FrontProductDetail();
                    nav = new Navigator();
                    nav.LangID = LangID;
                    objProductRelate = new FrontProductRelate();
                    objProductRelate.LangID = LangID;
                    detail.GetProductDetailByID((int)reader["product_id"], (byte)reader["cat_id"], LangID);
                    GenerateFileProduct(detail, LangID);

                }
            }
            

        }

        public string GetOtherProductPageTemplate(FrontProductDetail detail, byte langID)
        {
            string result = string.Empty;
            string layout = string.Empty;
            string flagLanguage = string.Empty;
            if (LangID == 1)
            {
                layout = GetStreamReader("/Layout/rate_old_version.html");
            }
            else
            {
                layout = GetStreamReader("/Layout-TH/rate_old_version.html");
            }

            string otherDetail = string.Empty;

            //Generate Navigator
            string Keyword = Utility.GetKeywordReplace(layout, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");

            if (detail.CategoryID == 29)
            {

                layout = layout.Replace(Keyword, nav.GenNavigator(detail.CategoryID, detail.LocationID, detail.Title));
            }
            else
            {
                layout = layout.Replace(Keyword, nav.GenNavigatorOtherProduct(detail.CategoryID, detail.DestinationID, detail.Title));
            }
            //End Generate Navigator
            
            ProductList list = new ProductList(LangID);
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");
            layout = layout.Replace(Keyword, list.RenderPopularDestination(detail.DestinationID, detail.CategoryID));

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductDetailStart##-->", "<!--##@ProductDetailEnd##-->");
            layout = layout.Replace(Keyword, "<!--##@InsertContent##-->");
            //layout = RenderLastReviewContent(layout, detail);
            Keyword = Utility.GetKeywordReplace(layout, "<!--###ReviewOtherStart###-->", "<!--###ReviewOtherEnd###-->");
            layout = layout.Replace(Keyword, "");
            layout = RenderProductFooterLink(layout, detail);



            if (detail.CategoryID == 29)
            {
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@IteneraryStart##-->", "<!--##@IteneraryEnd##-->");
                layout = layout.Replace(Keyword, "");
            }
            else
            {
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@IteneraryStart##-->", "<!--##@IteneraryEnd##-->");
                otherDetail = otherDetail + "<div class=\"product_detail\">\n";
                otherDetail = otherDetail + "<div class=\"product_detail_content\">\n";
                otherDetail = otherDetail + "<p>" + Hotels2XMLContent.Hotels2XMlReader_IgnoreError(detail.Detail) + "</p>\n";
                otherDetail = otherDetail + "</div>\n";
                otherDetail = otherDetail + "</div>\n";
                layout = layout.Replace(Keyword, otherDetail);
            }


            Keyword = Utility.GetKeywordReplace(layout, "<!--##@RecentProductStart##-->", "<!--##@RecentProductEnd##-->");
            layout = layout.Replace(Keyword, "");

            layout = layout.Replace("<!--##@DestDefault##-->", "<input type=\"hidden\" name=\"destDefault\" id=\"destDefault\" value=\"" + detail.DestinationID + "\" />");
            layout = layout.Replace("<!--##@LocDefault##-->", "<input type=\"hidden\" name=\"locDefault\" id=\"locDefault\" value=\"" + detail.LocationID + "\" />");
            layout = layout.Replace("<!--##@CategoryDefault##-->", "<input type=\"hidden\" name=\"category\" id=\"category\" value=\"" + detail.CategoryID + "\" />");
            layout = layout.Replace("<!--##@ProductDefault##-->", "<input type=\"hidden\" name=\"productDefault\" id=\"productDefault\" value=\"" + detail.ProductID + "\" />");
            if (LangID == 1)
            {
                layout = layout.Replace("<!--###categoryTitle###-->", Utility.GetProductType(detail.CategoryID)[0, 1].ToLower());
            }
            else
            {
                layout = layout.Replace("<!--###categoryTitle###-->", Utility.GetProductType(detail.CategoryID)[0, 4]);
            }
            Keyword = Utility.GetKeywordReplace(layout, "<!--###FlagLanguageStart###-->", "<!--###FlagLanguageEnd###-->");
            if (LangID == 1)
            {
                flagLanguage = "<a href=\"" + detail.FileMain + "\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"" + detail.FileMain.Replace(".asp", "-th.asp") + "\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";

            }
            else
            {
                flagLanguage = "<a href=\"" + detail.FileMain.Replace("-th.asp", ".asp") + "\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"" + detail.FileMain + "\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";
            }

            layout = layout.Replace(Keyword, flagLanguage);
            return layout;
        }

        public void GenerateFileProduct(FrontProductDetail detail, byte langID)
        {

            if (langID == 1)
            {
                urlMain = "http://www.hotels2thailand.com/";
            }
            else
            {
                urlMain = "http://thai.hotels2thailand.com/";
            }

            if (detail.CategoryID == 29)
            {

                nav.LoadLocationLinkByProductID(detail.ProductID, langID);
            }
            else
            {
                nav.LoadDestinationLinkByProductID(detail, langID);
            }

            objProductRelate.LoadAllProductInDestination(detail.DestinationID, detail.CategoryID);

            pathDestination = detail.FileMain.Split('/')[0].ToString();

            pathDestination = HttpContext.Current.Server.MapPath("/" + pathDestination);
            detail.FileMain = detail.FileMain.Split('/')[1].ToString();

            layout = getLayoutMain(detail);

            
            //layout = layout.Replace(Keyword, getReviewOther(detail.ProductID));

            layout = layout.Replace("http://174.36.32.56", "http://www.hotels2thailand.com");
            GenPageProductRate(layout, detail);
            GenPageProductInfo(layout, detail);
            GenPageProductGallery(layout, detail);
            GenPageProductReview(layout, detail);
            GenPageProductContact(layout, detail);
            GenPageProductWhy(layout, detail);
            
            HttpContext.Current.Response.Flush();

        }

        public string RenderReviewOther(int ProductID)
        {

            FrontLastReview review = new FrontLastReview();
            List<FrontLastReview> lastreview = review.GetLastReviewByProduct(ProductID, 4);
            string reviewOther = string.Empty;
            reviewOther = reviewOther + "<div class=\"latest_reviews_more\">\n";
            if(LangID==1)
            {
            reviewOther = reviewOther + "<h3>Latest Reviews</h3>\n";
            }else{
            reviewOther = reviewOther + "<h3>รีวิวล่าสุด</h3>\n";
            }
            
            int countReview;
            countReview = 1;

            string fileName;
            string reviewContent;

            foreach (FrontLastReview item in lastreview)
            {

                if (countReview > 1)
                {

                    fileName = item.Filename.Replace(".asp", "_review.asp");
                    reviewContent = item.Detail;

                    if (countReview % 2 == 0)
                    {
                        reviewOther = reviewOther + "<div class=\"review\">\n";
                    }
                    else
                    {
                        reviewOther = reviewOther + "<div class=\"review_b\">\n";
                    }


                    //reviewOther = reviewOther + "<div class=\"title_review\"><a href=\"/" + fileName + "\">" + item.Title + "</a></div>\n";
                    reviewOther = reviewOther + "<div class=\"detail_review\">\n";
                	if(LangID==1)
                    {
                        reviewOther = reviewOther + "<div class=\"by_review\"><span>by : </span>"+item.Fullname+"</div>\n";
                        reviewOther = reviewOther + "<div class=\"by_review\"><span>from : </span>"+item.ReviewFrom+"</div>\n";
                        reviewOther = reviewOther + "<div class=\"by_review\"><span>date: </span>" + item.DateSubmit.ToString("MMMM dd yyyy") + "</div>\n";
                    }else{
                        reviewOther = reviewOther + "<div class=\"by_review\"><span>เขียนโดย : </span>"+item.Fullname+"</div>\n";
                        reviewOther = reviewOther + "<div class=\"by_review\"><span>จาก : </span>"+item.ReviewFrom+"</div>\n";
                        reviewOther = reviewOther + "<div class=\"by_review\"><span>วันที่เขียน: </span>" + item.DateSubmit.ToString("MMMM dd yyyy") + "</div>\n";
                    }
                    reviewOther = reviewOther + "</div>\n";
                    reviewOther = reviewOther + "<div class=\"content_review\">" + item.Detail.Replace("\r\n","<br/>") + "</div>\n";
                    reviewOther = reviewOther + "<div class=\"clear-all\"></div> \n";          
                    reviewOther = reviewOther + "</div>\n";
                    //reviewOther = reviewOther + "<div class=\"review\">\n";
                    //reviewOther = reviewOther + "<div class=\"title_review\"><a href=\"javascript:void(0)\" onclick=\"window.location.href='" + item.Filename + "'\">" + item.Title + "</a></div>\n";
                    //reviewOther = reviewOther + "<div class=\"detail_review\">\n";
                    //reviewOther = reviewOther + "<div class=\"by_review\"><span>by : </span>" + item.Fullname + "</div>\n";
                    //reviewOther = reviewOther + "<div class=\"by_review\"><span>from : </span>" + item.ReviewFrom + "</div>\n";
                    //reviewOther = reviewOther + "<div class=\"by_review\"><span>date: </span>" + item.DateSubmit.ToString("MMMM dd yyyy") + "</div>\n";
                    //reviewOther = reviewOther + "</div>\n";
                    //reviewOther = reviewOther + "<div class=\"content_review\">" + item.Detail + "</div>\n";
                    //reviewOther = reviewOther + "<div class=\"clear-all\"></div>\n";       
                    //reviewOther = reviewOther + "</div>\n";
                    //reviewOther = reviewOther + "</div>\n";
                    
                }
                countReview = countReview + 1;

            }
            reviewOther = reviewOther + "</div><br />\n";

            //HttpContext.Current.Response.Write(reviewOther);
            //HttpContext.Current.Response.End();
            
            return reviewOther;
        }

        public string getLayoutMain(FrontProductDetail detail)
        {
            
            string result = string.Empty;
            string layout = string.Empty;
            string flagLanguage = string.Empty;
            if(LangID==1)
            {
                layout=GetStreamReader("/Layout/rate_old_version.html");
            }else{
                layout=GetStreamReader("/Layout-TH/rate_old_version.html");
            }
            
            string otherDetail = string.Empty;

            //Generate Navigator
            string Keyword = Utility.GetKeywordReplace(layout, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");

            if (detail.CategoryID == 29)
            {

                layout = layout.Replace(Keyword, nav.GenNavigator(detail.CategoryID, detail.LocationID, detail.Title));
            }
            else
            {
                layout = layout.Replace(Keyword, nav.GenNavigatorOtherProduct(detail.CategoryID, detail.DestinationID, detail.Title));
            }
            //End Generate Navigator
            
            ProductList list = new ProductList(LangID);
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");
            layout = layout.Replace(Keyword, list.RenderPopularDestination(detail.DestinationID, detail.CategoryID));

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductDetailStart##-->", "<!--##@ProductDetailEnd##-->");
            layout = layout.Replace(Keyword, RenderProductDelailMain(detail));
            layout = RenderLastReviewContent(layout, detail);
            Keyword = Utility.GetKeywordReplace(layout, "<!--###ReviewOtherStart###-->", "<!--###ReviewOtherEnd###-->");
            layout = layout.Replace(Keyword,RenderReviewOther(detail.ProductID));
            layout = RenderProductFooterLink(layout, detail);

            

            if (detail.CategoryID == 29)
            {
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@IteneraryStart##-->", "<!--##@IteneraryEnd##-->");
                layout = layout.Replace(Keyword, "");
            }
            else
            {
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@IteneraryStart##-->", "<!--##@IteneraryEnd##-->");
                otherDetail = otherDetail + "<div class=\"product_detail\">\n";
                otherDetail = otherDetail + "<div class=\"product_detail_content\">\n";
                otherDetail = otherDetail + "<p>" + Hotels2XMLContent.Hotels2XMlReader_IgnoreError(detail.Detail) + "</p>\n";
                otherDetail = otherDetail + "</div>\n";
                otherDetail = otherDetail + "</div>\n";
                layout = layout.Replace(Keyword, otherDetail);
            }


            Keyword = Utility.GetKeywordReplace(layout, "<!--##@RecentProductStart##-->", "<!--##@RecentProductEnd##-->");
            layout = layout.Replace(Keyword, "");

            layout = layout.Replace("<!--##@DestDefault##-->", "<input type=\"hidden\" name=\"destDefault\" id=\"destDefault\" value=\"" + detail.DestinationID + "\" />");
            layout = layout.Replace("<!--##@LocDefault##-->", "<input type=\"hidden\" name=\"locDefault\" id=\"locDefault\" value=\"" + detail.LocationID + "\" />");
            layout = layout.Replace("<!--##@CategoryDefault##-->", "<input type=\"hidden\" name=\"category\" id=\"category\" value=\"" + detail.CategoryID + "\" />");
            layout = layout.Replace("<!--##@ProductDefault##-->", "<input type=\"hidden\" name=\"productDefault\" id=\"productDefault\" value=\"" + detail.ProductID + "\" />");
            if(LangID==1)
            {
                layout = layout.Replace("<!--###categoryTitle###-->",Utility.GetProductType(detail.CategoryID)[0,1].ToLower());
            }else{
                layout = layout.Replace("<!--###categoryTitle###-->", Utility.GetProductType(detail.CategoryID)[0, 4]);
            }
            Keyword = Utility.GetKeywordReplace(layout, "<!--###FlagLanguageStart###-->", "<!--###FlagLanguageEnd###-->");
            if (LangID == 1)
            {
                flagLanguage = "<a href=\"" + detail.FileMain + "\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"" + detail.FileMain.Replace(".asp", "-th.asp") + "\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";

            }
            else
            {
                flagLanguage = "<a href=\"" + detail.FileMain.Replace("-th.asp", ".asp") + "\"><img src=\"../theme_color/blue/images/flag/flagENG.png\" width=\"25\" height=\"20\" style=\"float:left; margin:0 7px 0 25px;\" /></a>";
                flagLanguage = flagLanguage + "<a href=\"" + detail.FileMain + "\"><img src=\"../theme_color/blue/images/flag/flagTH.png\"  style=\"float:left; margin:0px;\" /></a>";
            }

            layout = layout.Replace(Keyword, flagLanguage);
            return layout;
        }

        private string RenderProductFooterLink(string layout, FrontProductDetail detail)
        {
            string result = string.Empty;
            string Keyword = Utility.GetKeywordReplace(layout, "<!--##@RenderFooterLinkStart##-->", "<!--##@RenderFooterLinkEnd##-->");
            switch (detail.CategoryID)
            {

                case 29:
                    
                    result = result + RenderProductDetail(layout, detail);
                    
                    //result = result + RenderReviewOther(layout, detail);
                    
                    result = result + RenderAllProductInLocation(layout, detail);
                    result = result + RenderReviewOther(detail.ProductID);
                    result = result + RenderAllProductRelateHotelClass(layout, detail);

                    if (detail.LocationID != 63)
                    {
                        result = result + RenderGroupProduct(layout, detail);
                    }

                    result = result + RenderAllDetainationInThailand(layout, detail);
                    
                    layout = layout.Replace(Keyword, result);

                    break;
                default:

                    result = result + RenderAllProductInDestination(layout, detail);
                    result = result + RenderAllDestinationInProduct(layout, detail);

                    if (detail.DestinationID != 30)
                    {
                        result = result + RenderAllHotelInOtherProduct(layout, detail);
                    }
                    switch (detail.CategoryID)
                    {
                        case 32:
                        case 34:
                        case 36:
                        case 39:
                        case 40:
                            result = result + RenderAllOtherProduct(38);
                            break;
                    }
                    result = result + RenderAllDetainationInThailand(layout, detail);

                    layout = layout.Replace(Keyword, result);
                    break;
            }

            return layout;
        }

        private string RenderAllOtherProduct(byte categoryID)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string strCommand = string.Empty;
                strCommand = strCommand + "select p.product_id,pc.title,";
                strCommand = strCommand + " (select top 1 spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id="+LangID+") as second_lang,";
                strCommand = strCommand + " (select top 1 sdn.title from tbl_destination_name sdn where sdn.destination_id=d.destination_id and sdn.lang_id=" + LangID + ") as second_lang_destination,";
                strCommand = strCommand + " (d.folder_destination+'-'+pcat.folder_cat+'.asp') as destination_file_name,(d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main) as file_name,p.destination_id,dn.title as destination_title,p.star";
                strCommand = strCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn,tbl_product_category pcat";
                strCommand = strCommand + " where d.destination_id=dn.destination_id and d.destination_id=p.destination_id and p.product_id=pc.product_id and p.cat_id= pcat.cat_id";
                strCommand = strCommand + " and dn.lang_id=1 and pc.lang_id=1 and p.status=1";
                strCommand = strCommand + " and p.cat_id=" + categoryID;
                strCommand = strCommand + " order by dn.title asc,pc.title asc";

                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int countCol = 0;
                short destinationTmp = 0;

                string productTitle = string.Empty;
                string productUrl = string.Empty;
                string destinationTitle = string.Empty;
                string destinationUrl = string.Empty;

                while (reader.Read())
                {
                    
                    destinationUrl = reader["destination_file_name"].ToString();
                    destinationTitle = reader["destination_title"].ToString();
                    productTitle = reader["title"].ToString();
                    productUrl=reader["file_name"].ToString();

                    if(LangID!=1)
                    {
                        destinationTitle = reader["second_lang_destination"].ToString();
                        destinationUrl = destinationUrl.Replace(".asp", "-th.asp");
                        productTitle=reader["second_lang"].ToString();
                        productUrl = productUrl.Replace(".asp","-th.asp");

                        if (string.IsNullOrEmpty(destinationTitle))
                        {
                            destinationTitle = reader["destination_title"].ToString();
                        }
                        if (string.IsNullOrEmpty(productTitle))
                        {
                            productTitle = reader["title"].ToString();
                        }
                    }

                    if (countCol == 0)
                    {
                        destinationTmp = (short)reader["destination_id"];
                        result = result + "<div id=\"tophotel_footer\">\n";
                        if(LangID==1)
                        {
                        result = result + "<h4>All Thailand " + Utility.GetProductType(categoryID)[0, 1] + "</h4> </div>\n";
                        }else{
                        result = result + "<h4>"+Utility.GetProductType(categoryID)[0, 4] + "ทั้งหมดในประเทศไทย</h4> </div>\n";
                        }
                        
                        result = result + "<table id=\"content_tophotel_footer_b\">\n";
                        if(LangID==1)
                        {
                            result = result + "<tr><td class=\"daytrips\" style=\"padding-top:5px;\" colspan=\"3\"><a href=\"/" + destinationUrl + "\">" + destinationTitle + " " + Utility.GetProductType(categoryID)[0, 1] + "</a></td></tr>\n";
                        }else{
                            
                            result = result + "<tr><td class=\"daytrips\" style=\"padding-top:5px;\" colspan=\"3\"><a href=\"/" + destinationUrl + "\">" + destinationTitle + " " + Utility.GetProductType(categoryID)[0, 4] + "</a></td></tr>\n";
                        }
                        

                    } 

                    if ((short)reader["destination_id"] != destinationTmp)
                    {
                        if(LangID==1)
                        {
                            result = result + "</tr><tr><td class=\"daytrips\" style=\"padding-top:5px;\" colspan=\"3\"><a href=\"/" + destinationUrl + "\">" + destinationTitle + " " + Utility.GetProductType(categoryID)[0, 1] + "</a></td></tr><tr>\n";
                        }else{
                            result = result + "</tr><tr><td class=\"daytrips\" style=\"padding-top:5px;\" colspan=\"3\"><a href=\"/" + destinationUrl + "\">" + destinationTitle + " " + Utility.GetProductType(categoryID)[0, 4] + "</a></td></tr><tr>\n";
                        }
                        
                        countCol = 0;
                    }

                    if (countCol % 3 == 0)
                    {
                        result = result + "</tr><tr>\n";
                    }

                    if(LangID==1)
                    {
                        result = result + "<td class=\"all_col\">  <a href=\"/" + productUrl + "\">" + productTitle + "</a> </td>";
                    }else{
                        result = result + "<td class=\"all_col\">  <a href=\"/" + productUrl + "\">" + productTitle + "</a> </td>";
                    }
                    
                    countCol = countCol + 1;
                    destinationTmp = (short)reader["destination_id"];
                }
            }

            result = result + "</tr>\n";
            result = result + "</table> \n";
            result = result + "<div id=\"bg_end_footer\"></div>\n";

            return result;
        }

        private string RenderAllProductInLocation(string layout, FrontProductDetail detail)
        {
            string result = string.Empty;
            //string Keyword = Utility.GetKeywordReplace(layout, "<!--##@HotelRelateIncludeStart##-->", "<!--##@HotelRelateIncludeEnd##-->");
            if (detail.CategoryID == 29)
            {
                result = objProductRelate.RenderProductRelateforRatePage(detail);
                //layout = layout.Replace(Keyword, objProductRelate.RenderProductRelateforRatePage(detail));
            }
            else
            {
                //layout = layout.Replace(Keyword, "");
            }
            return result;
        }
        private string RenderAllProductInDestination(string layout, FrontProductDetail detail)
        {
            string result = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pc.title,";
                sqlCommand = sqlCommand + " (select spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + LangID + ") as second_lang,";
                sqlCommand = sqlCommand + " d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main as filename,dn.title as destination_title";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn,tbl_product_category pcat";
                sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.cat_id=pcat.cat_id and p.destination_id=dn.destination_id and d.destination_id=dn.destination_id";
                sqlCommand = sqlCommand + " and dn.lang_id="+LangID+" and pc.lang_id=1 and p.status=1 and p.cat_id=" + detail.CategoryID;
                sqlCommand = sqlCommand + " and p.destination_id=" + detail.DestinationID;
                sqlCommand = sqlCommand + " order by pc.title asc";
                //HttpContext.Current.Response.Write(sqlCommand+"<br>");
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                int countCol = 0;

                string filePath = string.Empty;
                string defaultTitle = string.Empty;
                while (reader.Read())
                {

                    if (countCol == 0)
                    {
                        if(LangID==1)
                        {
                            result = result + "<div id=\"tophotel_footer\"> <h3>All " + Utility.GetProductType(detail.CategoryID)[0, 1] + " in " + reader["destination_title"] + "</h3> </div>\n";
                        }else{
                            result = result + "<div id=\"tophotel_footer\"> <h3>" + Utility.GetProductType(detail.CategoryID)[0, 4] + "ทั้งหมดใน" + reader["destination_title"] + "</h3> </div>\n";
                        }
                        
                        result = result + "<table id=\"content_tophotel_footer_b\">\n";
                        result = result + "<tr valign=\"top\">\n";
                    }
                    if (countCol % 3 == 0)
                    {
                        result = result + "</tr><tr>\n";
                    }

                    filePath = reader["filename"].ToString();
                    if (LangID == 1)
                    {
                        defaultTitle = reader["title"].ToString();
                    }
                    else
                    {
                        defaultTitle = reader["second_lang"].ToString();
                        filePath = filePath.Replace(".asp", "-th.asp");
                        if (string.IsNullOrEmpty(defaultTitle))
                        {
                            defaultTitle = reader["title"].ToString();
                        }
                        
                    }
                    result = result + "<td class=\"all_col\">  <a href=\"/" + filePath + "\">" + defaultTitle + "</a> </td>\n";
                    countCol = countCol + 1;


                }
                result = result + "</tr>\n";

                result = result + "</table>\n";
                result = result + "<div id=\"bg_end_footer\"></div><br />\n";
                string Keyword = Utility.GetKeywordReplace(layout, "<!--###AllThailandHotelDestinationStart##-->", "<!--###AllThailandHotelDestinationEnd##-->");
                layout = layout.Replace(Keyword, result);
                return result;
            }
            
        }
        private string RenderAllHotelInOtherProduct(string layout, FrontProductDetail detail)
        {
            string result = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                string sqlCommand = "select pc.title,";
                sqlCommand = sqlCommand + " (select top 1 spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id="+LangID+") as second_lang,";
                sqlCommand = sqlCommand + " d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main as filename,dn.title as destination_title";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn,tbl_product_category pcat";
                sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.cat_id=pcat.cat_id and p.destination_id=dn.destination_id and d.destination_id=dn.destination_id";
                sqlCommand = sqlCommand + " and dn.lang_id="+LangID+" and pc.lang_id=1 and p.status=1 and p.cat_id=29";
                sqlCommand = sqlCommand + " and p.destination_id=" + detail.DestinationID;
                sqlCommand = sqlCommand + " order by pc.title asc";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int countCol = 0;


                while (reader.Read())
                {

                    if (countCol == 0)
                    {
                        if(LangID==1)
                        {
                            result = result + "<div id=\"tophotel_footer\"> <h3>All Hotels in " + reader["destination_title"] + "</h3> </div>\n";
                        }else{
                            result = result + "<div id=\"tophotel_footer\"> <h3>โรงแรมทั้งหมดใน" + reader["destination_title"] + "</h3> </div>\n";
                        }
                        
                        result = result + "<table id=\"content_tophotel_footer_b\">\n";
                        result = result + "<tr valign=\"top\">\n";
                    }
                    if (countCol % 3 == 0)
                    {
                        result = result + "</tr><tr>\n";
                    }
                    if(LangID==1)
                    {
                    result = result + "<td class=\"all_col\">  <a href=\"/" + reader["filename"] + "\">" + reader["title"] + "</a> </td>\n";
                    }else{
                    result = result + "<td class=\"all_col\">  <a href=\"/" + reader["filename"].ToString().Replace(".asp","-th.asp") + "\">" + reader["second_lang"] + "</a> </td>\n";
                    }
                    
                    countCol = countCol + 1;


                }
                result = result + "</tr>\n";

                result = result + "</table>\n";
                result = result + "<div id=\"bg_end_footer\"></div><br />\n";

                return result;
            }
            
        }
        private string RenderAllDestinationInProduct(string layout, FrontProductDetail detail)
        {
            string result = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select distinct(d.destination_id),dn.title,";
                switch (detail.CategoryID)
                {
                    case 29:
                        sqlCommand = sqlCommand + "dn.file_name as file_name";
                        break;
                    case 32:
                        sqlCommand = sqlCommand + "dn.file_name_golf as file_name";
                        break;
                    case 34:
                        sqlCommand = sqlCommand + "dn.file_name_day_trip as file_name";
                        break;
                    case 36:
                        sqlCommand = sqlCommand + "dn.file_name_water_activity as file_name";
                        break;
                    case 38:
                        sqlCommand = sqlCommand + "dn.file_name_show_event as file_name";
                        break;
                    case 39:
                        sqlCommand = sqlCommand + "dn.file_name_health_check_up as file_name";
                        break;
                    case 40:
                        sqlCommand = sqlCommand + "dn.file_name_spa as file_name";
                        break;
                }
                sqlCommand = sqlCommand + " from tbl_product p,tbl_destination d,tbl_destination_name dn";
                sqlCommand = sqlCommand + " where p.destination_id=d.destination_id and d.destination_id=dn.destination_id";
                sqlCommand = sqlCommand + " and dn.lang_id="+LangID+" and p.cat_id=" + detail.CategoryID;
                sqlCommand = sqlCommand + " order by dn.title asc";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                int countCol = 0;


                while (reader.Read())
                {

                    if (countCol == 0)
                    {
                        if(LangID==1)
                        {
                            result = result + "<div id=\"tophotel_footer\"> <h3>All " + Utility.GetProductType(detail.CategoryID)[0, 1] + " Destination</h3> </div>\n";
                        }else{
                            result = result + "<div id=\"tophotel_footer\"> <h3>จังหวัดที่มี" + Utility.GetProductType(detail.CategoryID)[0, 4] + "</h3> </div>\n";
                        }
                        
                        result = result + "<table id=\"content_tophotel_footer_b\">\n";
                        result = result + "<tr valign=\"top\">\n";
                    }
                    if (countCol % 3 == 0)
                    {
                        result = result + "</tr><tr>\n";
                    }
                    result = result + "<td class=\"all_col\">  <a href=\"/" + reader["file_name"] + "\">" + reader["title"] + "</a> </td>\n";
                    countCol = countCol + 1;


                }
                result = result + "</tr>\n";

                result = result + "</table>\n";
                result = result + "<div id=\"bg_end_footer\"></div><br />\n";

                return result;
            }
            
        }
        private string RenderAllProductRelateHotelClass(string layout, FrontProductDetail detail)
        {

            //string Keyword = Utility.GetKeywordReplace(layout, "<!--##@AllHotelIncludeStart##-->", "<!--##@AllHotelIncludeEnd##-->");
            //layout = layout.Replace(Keyword, objProductRelate.RenderProductRelateClassforRatePage(detail));
            return objProductRelate.RenderProductRelateClassforRatePage(detail);
        }

        private string RenderAllDetainationInThailand(string layout, FrontProductDetail detail)
        {
            string result = string.Empty;
            int countItem = 0;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select dn.title,dn.file_name from tbl_destination d,tbl_destination_name dn";
                sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id";
                sqlCommand = sqlCommand + " and dn.lang_id="+LangID+" order by dn.title";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                result = result + "<div id=\"tophotel_footer\">\n";
                if(LangID==1)
                {
                    result = result + "<h4>All Thailand Hotels Destination</h4> </div>\n";
                }else{
                    result = result + "<h4>สถานที่ตั้งโรงแรมทั้งหมดในประเทศไทย</h4> </div>\n";
                }
                
                result = result + "<table id=\"content_tophotel_footer_b\">\n";
                result = result + "<tr valign=\"top\">\n";

                while (reader.Read())
                {
                    if (countItem % 3 == 0)
                    {
                        result = result + "</tr><tr>\n";
                    }
                    if(LangID==1)
                    {
                    result = result + "<td class=\"all_col\"> <a href=\"/" + reader["file_name"] + "\">" + reader["title"] + " Hotels</a> </td>\n";
                    }else{
                    result = result + "<td class=\"all_col\"> <a href=\"/" + reader["file_name"] + "\">โรงแรมใน" + reader["title"] + "</a> </td>\n";
                    }
                    
                    countItem = countItem + 1;
                }
                result = result + "</tr>\n";
                result = result + "</table>\n";
                result = result + "<div id=\"bg_end_footer\"></div>\n";

                return result;
            }

            
        }

        private string RenderGroupProduct(string layout, FrontProductDetail detail)
        {
            string result = "";
            LocationGroup objHotelGroup = new LocationGroup();
            List<LocationGroup> hotelGroupList = objHotelGroup.getLocationGroup(detail.CategoryID, detail.DestinationID, detail.LocationID, LangID);
            int countCol = 0;

            foreach (LocationGroup item in hotelGroupList)
            {

                if (countCol == 0)
                {
                    if(LangID==1)
                    {
                       
                        result = result + "<div id=\"tophotel_footer\"><h4>Hotels in related area with " + detail.Title + "</h4> </div>\n";
                    }else{
                        
                        result = result + "<div id=\"tophotel_footer\"><h4>โรงแรมทั้งหมดที่อยู่บริเวณใกล้เคียงกับโรงแรม" + detail.Title + "</h4> </div>\n";
                    }
                    
                    result = result + "<table id=\"content_tophotel_footer_b\">\n";
                    result = result + "<tr valign=\"top\">\n";
                }
                if (countCol % 3 == 0)
                {
                    result = result + "</tr><tr>\n";
                }
                result = result + "<td class=\"all_col\">  <a href=\"/" + item.PathName + "\">" + item.ProductTitle + "</a> </td>\n";
                countCol = countCol + 1;

            }
            result = result + "</tr>\n";

            result = result + "</table>\n";
            result = result + "<div id=\"bg_end_footer\"></div><br />\n";
            //string Keyword = Utility.GetKeywordReplace(layout, "<!--##@AllHotelGroupStart##-->", "<!--##@AllHotelGroupEnd##-->");
            //layout = layout.Replace(Keyword, result);
            return result;
        }

        private string RenderProductDetail(string layout, FrontProductDetail detail)
        {
            string result = string.Empty;
            //string Keyword = Utility.GetKeywordReplace(layout, "<!--###ProductDetailBoxStart###-->", "<!--###ProductDetailBoxEnd###-->");
            switch (detail.CategoryID)
            {
                case 29:
                    //string productContent = string.Empty;
                    if(LangID==1)
                    {
                        result = result + "<div id=\"tophotel_footer\"> <h2>" + detail.Title + " detail</h2> </div>\n";
                    }else{
                        result = result + "<div id=\"tophotel_footer\"> <h2>รายละเอียดโรงแรม" + detail.Title + "( "+detail.TitleDefault+" )</h2> </div>\n";
                    }
                    
                    result = result + "<div class=\"content_tophotel_footer_b\">\n";
                    result = result + "<div class=\"hotels_detail\">\n";
                    result = result + Hotels2XMLContent.Hotels2XMlReader_IgnoreError(detail.Detail) + "\n";
                    result = result + "</div>\n";
                    result = result + "</div>\n";
                    if (detail.CategoryID == 29)
                    {
                        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                        {
                            int countItem = 0;

                            string sqlCommand = "";
                            sqlCommand = sqlCommand + " select po.option_id,poc.title,";
                            sqlCommand = sqlCommand + " (select spoc.title  from tbl_product_option_content spoc where spoc.option_id=po.option_id and spoc.lang_id=" + LangID + ") as second_lang";
                            sqlCommand = sqlCommand + " from tbl_product p,tbl_product_option po,tbl_product_option_content poc ,tbl_product_option_supplier pos";
                            sqlCommand = sqlCommand + " where p.product_id=po.product_id and po.option_id=poc.option_id and pos.supplier_id=p.supplier_price and pos.option_id=po.option_id and po.product_id = " + detail.ProductID + " and poc.lang_id=1 and po.status=1 and po.cat_id=38";

                            //HttpContext.Current.Response.Write(sqlCommand+"<br>");
                            //HttpContext.Current.Response.Flush();
                            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                            cn.Open();

                            SqlDataReader reader = cmd.ExecuteReader();

                            result = result + "<table id=\"content_tophotel_footer_b\">\n";
                            result = result + "<tr valign=\"top\">\n";

                            string defaultTitle = string.Empty;

                            while (reader.Read())
                            {
                                if (countItem % 3 == 0)
                                {
                                    result = result + "</tr><tr>\n";
                                }

                                if(LangID==1)
                                {
                                    defaultTitle = reader["title"].ToString();
                                }else{
                                    defaultTitle = reader["second_lang"].ToString();
                                    if (string.IsNullOrEmpty(defaultTitle))
                                    {
                                        defaultTitle = reader["title"].ToString();
                                    }
                                }
                                result = result + "<td class=\"all_col\">  <a href=\"" + detail.FileMain.Replace(".asp", "_room_" + reader["option_id"] + ".asp") + "\">" + defaultTitle + "</a> </td>\n";
                                countItem = countItem + 1;
                            }

                            result = result + "</tr>\n";
                            result = result + "</table>\n";
                            result = result + "<div id=\"bg_end_footer\"></div><br />\n";
                        }

                        
                    }
                    //layout = layout.Replace(Keyword,productContent);
                    break;
                default:
                    //layout = layout.Replace(Keyword, "");
                    break;
            }
            return result;
        }

        private string RenderProductDelailMain(FrontProductDetail detail)
        {

            //detail = new ProductDetail(productID);
            //detail.Load();

            string result = "<div id=\"intro_rate\">\n";

            result = result + "<div class=\"product_detail\">\n";
            result = result + "<div class=\"product_detail_rate\">\n";

            result = result + "<div class=\"product_detail_box\">\n";
            result = result + "<div id=\"title\">\n";
            result = result + "<div class=\"title_hotels\"><h2>" + detail.Title + "</h2></div>\n";
            if (detail.CategoryID == 29)
            {
                result = result + "<div class=\"class_star_" + Utility.GetHotelClassImage(detail.Star, 1) + "\"></div> \n";
            }
            result = result + "</div> \n";




            result = result + "<div class=\"clear-all\"></div>  \n";
            if(LangID!=1)
            {
                result = result + "<div class=\"subTitleEng\">( " + detail.TitleDefault + " )</div>  \n";
            }
            
            //Display address
            if (detail.CategoryID == 29)
            {
                if(LangID==1)
                {
                    result = result + "<div class=\"address\">" + detail.Title + " Address : " + detail.Address + "</div> <br />\n";
                }else{
                    result = result + "<div class=\"address\">" + detail.Title + " ที่อยู่ : " + detail.Address + "</div> <br />\n";
                }
                
            }


            result = result + "<div class=\"score_title\">\n";
            result = result + "<!--##@ReviewScoreStart##-->\n";
            result = result + "<h3>Superb,9.3</h3>\n";
            result = result + "<div class=\"icon_review_blue\"></div>\n";

            result = result + "<p class=\"score_review_link\" style=\"line-height:24px\">Score from <a href=\"#\">123 reviews</a> </p> \n";
            result = result + "<!--##@ReviewScoreEnd##-->\n";

            if (detail.CategoryID == 29)
            {
                if(LangID==1)
                {
                result = result + "<div class=\"map\"><a href=\"http://174.36.32.56/thailand-hotels-map.aspx?pid=" + detail.ProductID + "\" target=\"_blank\">Map</a></div>\n";
                }else{
                result = result + "<div class=\"map\"><a href=\"http://174.36.32.56/thailand-hotels-map.aspx?pid=" + detail.ProductID + "&ln=2\" target=\"_blank\">แผนที่</a></div>\n";
                }
                
            }

            if (detail.CategoryID == 32)
            {
                if (LangID == 1)
                {
                    result = result + "<div class=\"map\"><a href=\"http://174.36.32.56/thailand-golf-map.aspx?pid=" + detail.ProductID + "\" target=\"_blank\">Map</a></div>\n";
                }
                else {
                    result = result + "<div class=\"map\"><a href=\"http://174.36.32.56/thailand-golf-map.aspx?pid=" + detail.ProductID + "&ln=2\" target=\"_blank\">แผนที่</a></div>\n";
                }
                
            }

            if (detail.HasInternet)
            {

                if (detail.IsInternetFree)
                {
                    if (LangID == 1)
                    {
                        result = result + "<div class=\"internet\"><a class=\"tooltip\">Internet<span class=\"tooltip_content\">Free internet</span></a></div>\n";
                    }
                    else {
                        result = result + "<div class=\"internet\"><a class=\"tooltip\">Internet<span class=\"tooltip_content\">ฟรีอินเตอร์เน็ต</span></a></div>\n";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(detail.InternetDetail))
                    {
                        result = result + "<div class=\"internet\"><a class=\"tooltip\">Internet<span class=\"tooltip_content\">" + detail.InternetDetail + "</span></a></div>\n";
                    }
                    else
                    {
                        result = result + "<div class=\"internet\"><a class=\"tooltip\">Internet<span class=\"tooltip_content\">อินเตอร์เน็ตไร้สาย (เสียค่าใช้จ่าย)</span></a></div>\n";
                    }
                }
            }



            result = result + "</div>\n";
            result = result + "<div class=\"clear-all\"></div>\n";

            result = result + "</div><!--product_detail_box-->  \n";

            result = result + "<div class=\"icon_paylater\">\n";

            if(detail.IsNewPic){
                
                result = result + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com" + detail.Picture + "\"/></a>\n";
            }else{

                result = result + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "-pic/" + detail.ProductCode + "_a.jpg\"/></a>\n";
            }

            ggMainImage = "<img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "-pic/" + detail.ProductCode + "_1.jpg\" class=\"photo\"/>";




            result = result + "</div><!--icon_paylater-->  \n";


            result = result + "<div class=\"review_score\">\n";



            result = result + "<div id=\"pic_small_fac\">\n";
            int maxPic = 1;

            if(detail.IsNewPic)
            {
                FrontProductPicture productPicture = new FrontProductPicture();
                pictureList = productPicture.GetProductImageList(detail.ProductID, 1, 7);

                foreach(FrontProductPicture item in pictureList)
                {
                    result = result + "<a href=\"http://www.hotels2thailand.com" + item.PicturePath.Replace("thumb_45_40", "larg_300_200") + "\" rel=\"nofollow\" onclick=\"return false;\" class=\"imgFloat\"><img src=\"http://www.hotels2thailand.com" + item.PicturePath + "\" /></a>\n";
                    if (maxPic == 12)
                    {
                        break;
                    }
                    maxPic = maxPic + 1; 
                }

            }else{
                
                
                //if (!detail.IsNewPic)
                //{
                for (int countPic = 1; countPic <= detail.NumPic; countPic++)
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath("/thailand-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "-pic/" + detail.ProductCode + "_b_" + countPic + ".jpg")))
                    {
                        result = result + "<a href=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "-pic/" + detail.ProductCode + "_c_" + countPic + ".jpg\" rel=\"nofollow\" onclick=\"return false;\" class=\"imgFloat\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "-pic/" + detail.ProductCode + "_b_" + countPic + ".jpg\" /></a>\n";
                    }
                    
                    if (maxPic == 12)
                    {
                        break;
                    }
                    maxPic = maxPic + 1;
                }
                //}
               
            }
            result = result + "</div>\n";
            


            result = result + "<br class=\"clear-all\" />\n";

            if(LangID==1)
            {
                result = result + "<div class=\"see_more\"><a href=\"" + detail.FilePhoto + "#infoPan\">More Pictures</a></div> \n";
            }else{
                result = result + "<div class=\"see_more\"><a href=\"" + detail.FilePhoto + "#infoPan\">ดูภาพทั้งหมด</a></div> \n";
            }
            


            result = result + "</div> <!--review_score-->\n";

            result = result + "<div class=\"booknow_paylater\">\n";
            if(LangID==1)
            {
            result = result + "<a href=\"#\" class=\"tooltip\"><img src=\"../theme_color/blue/images/icon/row_rate.png\" class=\"lowrates\"/><span class=\"tooltip_content\">\n";
            result = result + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br />\n";
            result = result + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>";
            

            }else{
            result = result + "<a href=\"#\" class=\"tooltip\"><img src=\"../theme_color/blue/images/icon/row_rate_th.png\" class=\"lowrates\"/><span class=\"tooltip_content\">\n";
            result = result + "<p><strong>รับประกันถูกที่สุด</strong> - หากท่านพบว่าราคาที่อื่นถูกกว่า ท่านสามารถเเจ้งกลับทางเราได้ภายใน 24 ชม นับจากที่ท่านจองกับเรา เราจะคืนส่วนต่างให้กับท่านทันที</p><br />\n";
            result = result + "<p><strong>ระบบการชำระเงินออนไลน์ที่มีความปลอดภัยสูง</strong> - เวปไซต์โฮเทลทูเลือกใช้ระบบ SSL 256 Bit ซึ่งถือได้ว่าเป็นระบบเทคโนโลยีที่ทันสมัยที่สุดทางด้านความปลอดภัยบนเวปไซต์ </p>\n";
            }
            
            result = result + "</span></a>\n";
            result = result + "</div>\n";
            result = result + "<div class=\"booknow_top\"><a href=\"" + detail.FileMain + "#infoPan\"></a></div>\n";

            result = result + "<br class=\"clear-all\" />\n";

            result = result + "<!--##@NearByIncludeStart##-->\n";
            result = result + "<!--##@NearByIncludeEnd##-->  \n";
            result = result + "<!--##@ReviewLastContentStart##-->\n";
            result = result + "<!--##@ReviewLastContentEnd##--> \n";
            result = result + "<br class=\"clear-all\" />\n";
            result = result + "</div><!--product_detail_content-->\n";
            result = result + "</div><!--product_detail-->\n";



            result = result + "<div class=\"clear-all\"></div>\n";
            result = result + "<!--##@IteneraryStart##-->";
            result = result + "<div class=\"product_detail\">\n";
            result = result + "<div class=\"product_detail_content\">\n";
            result = result + "<p><span>What to bring : </span>\n";
            result = result + " Swimming wear, shorts, Light T-shirt, Beach Towel, Sun Block, Sun Cap, Sunglasses, Camera, Extra Films, Light Deck Shoes and a little Money for Sundries</p>\n";

            result = result + "</div>\n";
            result = result + "</div><!--product detail-->\n";
            result = result + "<!--##@IteneraryEnd##-->";
            result = result + "<div class=\"clear-all\"></div>\n";
            result = result + "</div>\n";
            //Response.Write(detail.ProductID);


            return result;
        }

        private string RenderLastReviewContent(string layout, FrontProductDetail detail)
        {
            string reviewScrore = "";
            string reviewContent = "";
            string reviewLastContent = "";
            string Keyword;
            ProductReviewLast review = new ProductReviewLast();
            review.GetProductReviewLast(detail);

            string ggReview = string.Empty;

            if (review.LastReviewID > 0)
            {

                reviewLastContent = review.Content;
                if (reviewLastContent.Length > 120)
                {
                    reviewLastContent = reviewLastContent.Substring(0, 119);
                }

                reviewScrore = reviewScrore + "<h3>" + Utility.GetHotelReviewText((int)review.AverageReview,LangID) + "," + review.AverageReview.ToString("#,###.##") + "</h3>\n";
                reviewScrore = reviewScrore + "<div class=\"icon_review_blue" + ((int)detail.AverageReview) + "\"></div> \n";
                if(LangID==1)
                {
                reviewScrore = reviewScrore + "<p class=\"score_review_link\" style=\"line-height:24px\">Score from <a href=\"" + detail.FileReview + "#infoPan\">" + review.CountReview + " reviews</a> </p> \n";
                }else{
                reviewScrore = reviewScrore + "<p class=\"score_review_link\" style=\"line-height:24px\">จากรีวิวทั้งหมด <a href=\"" + detail.FileReview + "#infoPan\">" + review.CountReview + " รีวิว</a> </p> \n";
                }
                

                reviewContent = reviewContent + "<div id=\"review_bg\">\n";
                if(LangID==1)
                {
                    reviewContent = reviewContent + "<div class=\"review_tex\"><p><a href=\"" + detail.FileReview + "#infoPan\">Lasted review for " + detail.Title + "</a></p>\n";
                }else{
                    reviewContent = reviewContent + "<div class=\"review_tex\"><p><a href=\"" + detail.FileReview + "#infoPan\">รีวิวล่าสุดของ" + detail.Title + "</a></p>\n";
                
                }
                
                reviewContent = reviewContent + "“" + reviewLastContent + "</div>\n";
                reviewContent = reviewContent + "<div class=\"name_custom_review\"><span>" + review.Fullname + ",</span>" + review.ReviewFrom + "</div>\n";
                reviewContent = reviewContent + "</div>\n";

                using(SqlConnection cn=new SqlConnection(this.ConnectionString))
                {
                    string strCommand=string.Empty;

                    if(detail.IsExtranet)
                    {
                        strCommand = strCommand + "select Convert(money,isnull(tmp.min_rate,0)) as min_rate,Convert(money,isnull(tmp.min_rate,0))/(select sc.prefix from tbl_currency sc where sc.currency_id=1) as rate_usd,tmp.pic_code";
                        strCommand = strCommand + " from";
                        strCommand = strCommand + " (";
                        strCommand = strCommand + " select top 1 pocp_ex.price as min_rate,";
                        strCommand = strCommand + " (select top 1 spic.pic_code from tbl_product_pic spic where spic.product_id=" + detail.ProductID + " and spic.cat_id=1 and spic.type_id=7 and spic.status=1) as pic_code";
                        strCommand = strCommand + " from tbl_product_option op,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_price_extranet pocp_ex";
                        strCommand = strCommand + " where op.option_id=poc_ex.option_id and poc_ex.condition_id =pocp_ex.condition_id";
                        strCommand = strCommand + " and pocp_ex.date_price>=GETDATE() and op.product_id=" + detail.ProductID + " and op.cat_id=38";
                        strCommand = strCommand + " and op.status=1 and poc_ex.status=1";
                        strCommand = strCommand + " order by pocp_ex.price asc";
                        strCommand = strCommand + " ) as tmp";
                    
                    }else{
                        strCommand = strCommand + "select Convert(money,isnull(tmp.min_rate,0)) as min_rate,Convert(money,isnull(tmp.min_rate,0))/(select sc.prefix from tbl_currency sc where sc.currency_id=1) as rate_usd,tmp.pic_code";
                        strCommand = strCommand + " from";
                        strCommand = strCommand + " (";
                        strCommand = strCommand + " select min(spocp.rate) as min_rate,";
                        strCommand = strCommand + " (select top 1 spic.pic_code from tbl_product_pic spic where spic.product_id=" + detail.ProductID + " and spic.cat_id=1 and spic.type_id=7 and spic.status=1) as pic_code";
                        strCommand = strCommand + " from tbl_product sp,tbl_product_period spp,tbl_product_option spo,tbl_product_option_condition spoc,tbl_product_option_condition_price spocp";
                        strCommand = strCommand + " where sp.product_id=spp.product_id and sp.supplier_price=spp.supplier_id and sp.product_id=spo.product_id and spo.option_id=spoc.option_id and spoc.condition_id=spocp.condition_id and spp.period_id=spocp.period_id";
                        strCommand = strCommand + " and spo.status=1 and spoc.status=1 and spo.cat_id=38  and sp.product_id=" + detail.ProductID + " and spp.date_end>=GETDATE()";
                        strCommand = strCommand + " ) as tmp";
                    }

                    //HttpContext.Current.Response.Write(strCommand);
                    //HttpContext.Current.Response.Flush();
                    SqlCommand cmd=new SqlCommand(strCommand,cn);
                    cn.Open();
                    SqlDataReader rdRate=cmd.ExecuteReader();
                   
                    if (rdRate.Read())
                    {
                        if ((decimal)rdRate["min_rate"] > 0)
                        {
                        ggReview=ggReview+"<div class=\"hreview-aggregate\">\n";
                        ggReview=ggReview+"<span class=\"item\">\n";
                        ggReview=ggReview+"<span class=\"fn\">"+detail.Title+"</span>\n";
                        if (!string.IsNullOrEmpty(rdRate["pic_code"].ToString()))
                        {
                            ggReview = ggReview + "<img src=\"http://www.hotels2thailand.com" + rdRate["pic_code"] + "\" class=\"photo\" />\n";
                        }
                        else {
                            ggReview = ggReview + ggMainImage;
                        }
                        ggReview=ggReview+"</span>\n";
                        ggReview=ggReview+"<span class=\"rating\">\n";
                        ggReview=ggReview+"<span class=\"average\">"+review.AverageReview.ToString("#,###")+"</span> out of\n";
                        ggReview=ggReview+"<span class=\"best\">10</span>\n";
                        ggReview=ggReview+"</span>\n";
                        ggReview = ggReview + "<span class=\"pricerange\">From " + string.Format("{0:#,0}",Utility.ExcludeVat(Convert.ToDecimal(rdRate["min_rate"]))) + " Baht (" + string.Format("{0:#,0}", rdRate["rate_usd"]) + " USD)</span>\n";
                        ggReview=ggReview+"<span class=\"votes\">"+review.CountReview+"</span> ratings.\n";
                        ggReview=ggReview+"<span class=\"count\">5</span> user reviews.\n";
                        ggReview=ggReview+"</div> \n";
                        }
                    }
                    //HttpContext.Current.Response.Write(strCommand + "<br>");
                    //HttpContext.Current.Response.Flush();
                    
                }
                

                reviewContent = reviewContent + ggReview;

                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ReviewScoreStart##-->", "<!--##@ReviewScoreEnd##-->");
                layout = layout.Replace(Keyword, reviewScrore);

                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ReviewLastContentStart##-->", "<!--##@ReviewLastContentEnd##-->");
                layout = layout.Replace(Keyword, reviewContent);
            }
            else
            {


                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ReviewScoreStart##-->", "<!--##@ReviewScoreEnd##-->");


                if(LangID==1)
                {
                    reviewScrore = reviewScrore + "<p class=\"score_review_link\">Average user rating: Not yet rated | <a href=\"http://174.36.32.56/review_write.aspx?pid=" + detail.ProductID + "\">Write Review</a></p>";
                }else{
                    reviewScrore = reviewScrore + "<p class=\"score_review_link\">ผลประเมินรีวิว :ยังไม่มีท่านใดรีวิว | <a href=\"http://174.36.32.56/review_write.aspx?pid=" + detail.ProductID + "\">เขียนรีวิว</a></p>";
                }
                

                layout = layout.Replace(Keyword, reviewScrore);
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ReviewLastContentStart##-->", "<!--##@ReviewLastContentEnd##-->");
                layout = layout.Replace(Keyword, "");
            }
            return layout;
        }
        private string GetTabMenu(int typeMenu, FrontProductDetail detail)
        {
            string actBook = "booknow";
            string actInfo = "information";
            string actGallery = "gall";
            string actReview = "review";
            string actWhy = "why";
            string actContact = "end";

            switch (typeMenu)
            {

                case 1:
                    //actBook = "booknow_ho";
                    actBook = "booknow";
                    break;
                case 2:
                    //actInfo = "info_ho";
                    actInfo = "information";    
                    break;
                case 3:
                    //actGallery = "gall_ho";
                    actGallery = "gall";
                    break;
                case 4:
                    //actReview = "review_ho";
                    actReview = "review";
                    break;
                case 5:
                    //actWhy = "why_ho";
                    actWhy = "why";
                    break;
                case 6:
                    //actContact = "end_ho";
                    actContact = "end";
                    break;
            }
            string result = "<a name=\"infoPan\"></a><div id=\"menu_rate\">\n";
            if(LangID==1)
            {
                result = result + "<a href=\"" + detail.FileMain + "#infoPan\" class=\"" + actBook + "\">Book Now</a>\n";
                if (detail.CategoryID == 29)
                {
                    result = result + "<a href=\"" + detail.FileInfo + "#infoPan\" class=\"" + actInfo + "\">Hotel Information</a>\n";
                }
                else
                {
                    result = result + "<a href=\"#\" class=\"" + actInfo + "\">Information</a>\n";
                }

                result = result + "<a href=\"" + detail.FilePhoto + "#infoPan\" class=\"" + actGallery + "\">Photo Gallery</a>\n";
                result = result + "<a href=\"" + detail.FileReview + "#infoPan\" class=\"" + actReview + "\">Traveler Reviews</a>\n";
                result = result + "<a href=\"" + detail.FileWhy + "#infoPan\" class=\"" + actWhy + "\">Why Us?</a>\n";
                result = result + "<a href=\"" + detail.FileContact + "#infoPan\" class=\"" + actContact + "\">Contact Us</a> \n";
            }else{
                result = result + "<a href=\"" + detail.FileMain + "#infoPan\" class=\"" + actBook + "\">จองทันที</a>\n";
                if (detail.CategoryID == 29)
                {
                    result = result + "<a href=\"" + detail.FileInfo + "#infoPan\" class=\"" + actInfo + "\">ข้อมูลโรงแรม</a>\n";
                }
                else
                {
                    result = result + "<a href=\"#\" class=\"" + actInfo + "\">ข้อมูล</a>\n";
                }

                result = result + "<a href=\"" + detail.FilePhoto + "#infoPan\" class=\"" + actGallery + "\">แกลลอรี่</a>\n";
                result = result + "<a href=\"" + detail.FileReview + "#infoPan\" class=\"" + actReview + "\">รีวิวผู้เข้าพัก</a>\n";
                result = result + "<a href=\"" + detail.FileWhy + "#infoPan\" class=\"" + actWhy + "\">ทำไมต้องโฮเทลทู</a>\n";
                result = result + "<a href=\"" + detail.FileContact + "#infoPan\" class=\"" + actContact + "\">ติดต่อเรา</a> \n";
            }
            
            result = result + "</div>\n";
            return result;

        }
        private void GenPageProductRate(string layout, FrontProductDetail detail)
        {
            //Replace TabMenu
            string pageContent;
            string Keyword = string.Empty;
            string MetaTag = string.Empty;
            string pageTitle = string.Empty;
            string pageH1 = string.Empty;
            string PageALT = string.Empty;

            //MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            //MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain);
            //Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            
            switch (detail.LocationID)
            {
                case 284: case 125: case 59: case 62: case 245: case 93: case 123: case 60: case 296: case 119: case 117: case 113: case 175: case 360: // All new meta
                    HttpContext.Current.Response.Write(LangID + "hello1<br>");
                    MetaTag = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, LangID, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title, detail.Promotion, detail.RateLowest, detail.NumHotel.ToString("#,###"), detail.NumProductOther.ToString("#,###"), detail.PaymentTypeID.ToString());
                    MetaTag = MetaTag + SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, urlMain + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain);
                    pageTitle = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Detail, LangID, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title, detail.Promotion, detail.RateLowest, detail.NumHotel.ToString("#,###"), detail.NumProductOther.ToString("#,###"), detail.PaymentTypeID.ToString());
                    PageALT = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                    pageH1 = SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                    break;

                case 106: case 63: case 70: case 94: case 92: case 76: case 178: case 61: case 246: case 114: case 83: // New meta with 100% title
                    HttpContext.Current.Response.Write(LangID + "hello2<br>");
                    MetaTag = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, LangID, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title, detail.Promotion, detail.RateLowest, detail.NumHotel.ToString("#,###"), detail.NumProductOther.ToString("#,###"), detail.PaymentTypeID.ToString());
                    MetaTag = MetaTag + SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, urlMain + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain);
                    pageTitle = detail.Title;
                    PageALT = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                    pageH1 = SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                    break;

                default:
                    
                    if (detail.CategoryID == 29) // old version
                    {
                        HttpContext.Current.Response.Write(detail.Title + "hello3<br>");
                        //MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                        //MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain);
                        MetaTag = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, LangID, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title, detail.Promotion, detail.RateLowest, detail.NumHotel.ToString("#,###"), detail.NumProductOther.ToString("#,###"), detail.PaymentTypeID.ToString());
                        MetaTag = MetaTag + SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, urlMain + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain);

                        pageTitle = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                        PageALT = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                        pageH1 = SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                    }
                    else // all new version
                    {
                        HttpContext.Current.Response.Write(LangID + "hello4<br>");
                        MetaTag = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, LangID, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title, detail.Promotion, detail.RateLowest, detail.NumHotel.ToString("#,###"), detail.NumProductOther.ToString("#,###"), detail.PaymentTypeID.ToString());
                        MetaTag = MetaTag + SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, urlMain + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain);
                        pageTitle = SEO_GenerateV2.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Detail, LangID, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title, detail.Promotion, detail.RateLowest, detail.NumHotel.ToString("#,###"), detail.NumProductOther.ToString("#,###"), detail.PaymentTypeID.ToString());
                        PageALT = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                        pageH1 = SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                    }
                    break;
            }

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(1, detail));
            pageContent = pageContent.Replace("<!--##product_title##-->", pageTitle);
            pageContent = pageContent.Replace("<!--##meta_tag##-->", MetaTag);
            pageContent = pageContent.Replace("<!--##AltIndex##-->", PageALT);
            pageContent = pageContent.Replace("<!--##H1##-->", pageH1);

            //-----------------
            //Response.Write(pageContent);
            //Response.End();

            Keyword = Utility.GetKeywordReplace(pageContent, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");

            string boxChange = "<a name=\"anchor_search_box\"></a><div id=\"content_rate\">\n";
            boxChange = boxChange + "<div class=\"chack_rate\">\n";
            boxChange = boxChange + "<div class=\"dateSearchPan\">\n";
            if(LangID==1)
            {
                boxChange = boxChange + "<P>Please enter to change date</P>\n";
            }else{
                boxChange = boxChange + "<P>กรุณาเลือกวันที่ต้องการเข้าพัก</P>\n";
            }
           
            boxChange = boxChange + "<div id=\"changeDateBox\">\n";
            if (detail.CategoryID == 29)
            {
                if (LangID == 1)
                {
                    boxChange = boxChange + "<div class=\"chang_date\"><span>Check in</span>\n";
                }
                else {
                    boxChange = boxChange + "<div class=\"chang_date\"><span>เช็คอิน</span>\n";
                }
                
            }
            else
            {
                if(LangID==1)
                {
                boxChange = boxChange + "<div class=\"chang_date\"><span>Select date </span>\n";
                }else{
                boxChange = boxChange + "<div class=\"chang_date\"><span>เลือกวัน </span>\n";
                }
                
            }
            boxChange = boxChange + "<input type=\"text\" name=\"dateStart2ci\" id=\"dateStart2ci\" rel=\"datepicker\" style=\"width:125px; float:left; \" />\n";
            boxChange = boxChange + "</div>\n";
            if (detail.CategoryID == 29)
            {
                if(LangID==1)
                {
                    boxChange = boxChange + "<div class=\"chang_date\"><span>Check out</span>\n";
                }else{
                    boxChange = boxChange + "<div class=\"chang_date\"><span>เช็คเอ้าท์</span>\n";
                }
                
                boxChange = boxChange + "<input type=\"text\" name=\"dateStart2co\" id=\"dateStart2co\" rel=\"\" style=\"width:125px; float:left;\" /> </div>\n";
            }
            else {
                if(LangID==1)
                {
                boxChange = boxChange + "<div  style=\"display:none\"><span>Check out</span>\n";
                }else{
                boxChange = boxChange + "<div  style=\"display:none\"><span>เช็คเอ้าท์</span>\n";
                }
                
                boxChange = boxChange + "<input type=\"text\" name=\"dateStart2co\" id=\"dateStart2co\" rel=\"\" style=\"width:125px; float:left;\" /> </div>\n";
            }
            boxChange = boxChange + "<div class=\"chang_date_b\"><a href=\"javascript:void(0)\" onclick=\"checkRate(" + detail.ProductID + ")\"></a></div>\n";
            boxChange = boxChange + "</div>\n";
            boxChange = boxChange + "</div>\n";
            boxChange = boxChange + "<br class=\"clear-all\" />\n";
            boxChange = boxChange + "</div>\n";
            boxChange = boxChange + "<div id=\"RoomPeriod\"></div>\n";
            boxChange = boxChange + "</div>\n";
            //---
            //string boxChange = "<a name=\"anchor_search_box\">";
            //boxChange=boxChange+"<div id=\"content_rate\">\n";
            //boxChange=boxChange+"<div class=\"chack_rate\">\n";
            //        boxChange=boxChange+"<div class=\"dateSearchPan\">\n";
            //        boxChange=boxChange+"<P>Please enter the dates of your stay  </P>\n";
            //            boxChange=boxChange+"<div id=\"changeDateBox\">\n";
            //                boxChange=boxChange+"<div class=\"chang_date\"><span>Check-in date</span>\n";
            //                boxChange=boxChange+"<input type=\"text\" name=\"dateStart2\" id=\"dateStart2\" class=\"vPicker\" rel=\"\" style=\"width:125px; float:left; \" />\n";
            //               boxChange=boxChange+" </div>\n";
            // if(categoryID==29){
            //                boxChange=boxChange+"<div class=\"chang_date\"><span>Check-out date</span>\n";
            //                boxChange=boxChange+"<input type=\"text\" name=\"dateEnd\" id=\"dateEnd2\" class=\"vPicker\" rel=\"\" style=\"width:125px; float:left;\" /> \n";
            //                boxChange=boxChange+"</div>\n";   
            // }
            // boxChange = boxChange + "<div class=\"chang_date_b\"><a href=\"javascript:void(0)\" onclick=\"checkRate(" + detail.ProductID + ")\"></a></div>\n";
            //          boxChange=boxChange+"</div> \n"; 
            //        boxChange=boxChange+"</div>\n";
            //        boxChange=boxChange+"<br class=\"clear-all\" />\n";
            //    boxChange=boxChange+"</div>\n";
            //boxChange = boxChange + "</div>\n";


            pageContent = pageContent.Replace(Keyword, boxChange);


            //string boxChange = "<div id=\"content_rate\">";
            //boxChange = boxChange + "<div class=\"chack_rate\">";
            //boxChange = boxChange + "<div id=\"annoucementDisplay\"></div>";
            //boxChange = boxChange + "<p>Please enter the dates of your stay to check availability</p>";
            //boxChange = boxChange + "<div class=\"chang_date\"> Check-in date <input type=\"text\" name=\"dateStart2\" id=\"dateStart2\" /><img src=\"../images/ico_calendar_new.jpg\" id=\"cal_in\" onclick=\"pickCalendar('dateStart2');\" /></div>";
            //boxChange = boxChange + "<div class=\"chang_date\"> Check-out date <input type=\"text\" name=\"dateEnd2\" id=\"dateEnd2\" /><img src=\"../images/ico_calendar_new.jpg\" id=\"cal_out\"  onclick=\"pickCalendar('dateEnd2');\" /> </div>";
            //boxChange = boxChange + "<div class=\"chang_date_b\"><a href=\"javascript:void(0)\" onclick=\"checkRate(" + detail.ProductID + ")\"></a></div>";
            //boxChange = boxChange + "<br class=\"clear-all\" />";
            //boxChange = boxChange + "</div></div>";



            //string jsInclude = "<script type=\"text/javascript\" src=\"/js/front.ui.rate.js\"></script>";
            //pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);

            pageContent = pageContent.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
            pageContent = pageContent.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(16, detail.ProductID, 1));

            GenerateFile gf;
            gf = new GenerateFile(pathDestination, detail.FileMain, pageContent);
            gf.LangID = LangID;
            gf.CreateFile();

        }
        private void GenPageProductInfo(string layout, FrontProductDetail detail)
        {
            //Replace TabMenu
            ProductRelate productRelate = new ProductRelate();

            //Get Facility
           

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select fac_id,title from tbl_facility_product";
                sqlCommand = sqlCommand + " where product_id=" + detail.ProductID + " and lang_id="+LangID;
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                string pageContent;
                string Keyword = string.Empty;
                string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Information, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Information, detail.CategoryID, urlMain + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp", "_info.asp"));

                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
                pageContent = layout.Replace(Keyword, GetTabMenu(2, detail));
                pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Information, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
                pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Information, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
                pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Information, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
                pageContent = pageContent.Replace("<!--##meta_tag##-->", MetaTag);
                //-----------------
                pageContent = pageContent.Replace("<!--##Rate Include##-->", "");



                string boxInfo = string.Empty;
                boxInfo = boxInfo + "<div id=\"content_why\">\n";
                if(LangID==1)
                {
                    boxInfo = boxInfo + "<div id=\"why_us_header\"><h4>Hotels Information</h4></div>\n";
                }else{
                    boxInfo = boxInfo + "<div id=\"why_us_header\"><h4>ข้อมูลโรงแรม</h4></div>\n";
                }
                
                boxInfo = boxInfo + "<div id=\"info\">\n";
                boxInfo = boxInfo + "<div class=\"info_print\">\n";
                if(LangID==1)
                {
                    boxInfo = boxInfo + "<a href=\"http://174.36.32.56/thailand-hotels-map.aspx?pid=" + detail.ProductID + "\" target=\"_blank\" class=\"info_map\">Map & Location</a>\n";
                    //boxInfo = boxInfo + "<a href=\"http://www.hotels2thailand.com/coupon/free-coupon.aspx\" class=\"info_free\">Free Discount Coupon </a>\n";
                    boxInfo = boxInfo + "<a href=\"/thailand-hotels-tell.aspx?pd=" + detail.ProductID + "&ln=1\" class=\"info_tell\">Tell your friend </a>\n";
                    //boxInfo = boxInfo + "<a  href=\"http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain + "\" title=\"" + detail.Title + " - Hotels2thailand.com\" class=\"jQueryBookmark\">Bookmark </a>\n";
                    boxInfo = boxInfo + "<a href=\"javascript:bookmark_us('http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain + "','" + detail.Title + " - Hotels2thailand.com')\" class=\"jQueryBookmark\">Bookmark</a>";
                    boxInfo = boxInfo + "<a href=\"" + detail.FilePDF + "\" class=\"info_view\">View this page as PDF</a>\n";
                    boxInfo = boxInfo + "<a href=\"/thailand-hotels-print.aspx?pd=" + detail.ProductID + "\" class=\"info_version\">Print Version</a>\n";
                }else{
                    boxInfo = boxInfo + "<a href=\"http://174.36.32.56/thailand-hotels-map.aspx?pid=" + detail.ProductID + "&ln=2\" target=\"_blank\" class=\"info_map\">แผนที่และสถานที่ตั้ง</a>\n";
                    //boxInfo = boxInfo + "<a href=\"http://thai.hotels2thailand.com/coupon/free-coupon.aspx\" class=\"info_free\">Free Discount Coupon </a>\n";
                    boxInfo = boxInfo + "<a href=\"/thailand-hotels-tell.aspx?pd=" + detail.ProductID + "&ln=2\" class=\"info_tell\">ต้องการบอกต่อเพื่อน</a>\n";
                    //boxInfo = boxInfo + "<a  href=\"http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain + "\" title=\"" + detail.Title + " - Hotels2thailand.com\" class=\"jQueryBookmark\">Bookmark </a>\n";
                    boxInfo = boxInfo + "<a href=\"javascript:bookmark_us('http://thai.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain + "','" + detail.Title + " - Hotels2thailand.com')\" class=\"jQueryBookmark\">บันทึกหน้านี้ไว้</a>";
                    boxInfo = boxInfo + "<a href=\"" + detail.FilePDF + "\" class=\"info_view\">ดูหน้านี้ในรูปแบบไฟล์ PDF</a>\n";
                    boxInfo = boxInfo + "<a href=\"/thailand-hotels-print-th.aspx?pd=" + detail.ProductID + "\" class=\"info_version\">ปริ้นท์เวอร์ชั่น</a>\n";
                }
                
                boxInfo = boxInfo + "<br class=\"clear-all\" />\n";
                boxInfo = boxInfo + "</div><br />\n";
                if(LangID==1)
                {
                    boxInfo = boxInfo + "<h4>LOCATION : " + detail.Title + " </h4>\n";
                    boxInfo = boxInfo + "<p></p>  <br /> \n";
                    boxInfo = boxInfo + "<h4>SERVICES & RECREATIONS : " + detail.Title + " </h4>\n";
                    boxInfo = boxInfo + "<div id=\"service_bg\"> <div class=\"service\">Facilities </div> </div><!--bg-->\n";
                    boxInfo = boxInfo + "<ul>\n";
                    while (reader.Read())
                    {
                        boxInfo = boxInfo + "<li>" + reader["title"] + "</li>\n";
                    }
                    boxInfo = boxInfo + "</ul>\n";
                    boxInfo = boxInfo + "<br class=\"clear-all\" /><br />\n";
                    boxInfo = boxInfo + "</div><!--info-->\n";
                    boxInfo = boxInfo + "<div class=\"backtotop_r\"><a href=\"#\">Back to top</a></div>\n";
                    boxInfo = boxInfo + "</div>\n";
                }else{
                    boxInfo = boxInfo + "<h4>ตำแหน่ง : " + detail.Title + " </h4>\n";
                    boxInfo = boxInfo + "<p></p>  <br /> \n";
                    boxInfo = boxInfo + "<h4>บริการต่างๆ & สันทนาการ : " + detail.Title + " </h4>\n";
                    boxInfo = boxInfo + "<div id=\"service_bg\"> <div class=\"service\">สิ่งอำนวยความสะดวก </div> </div><!--bg-->\n";
                    boxInfo = boxInfo + "<ul>\n";
                    while (reader.Read())
                    {
                        boxInfo = boxInfo + "<li>" + reader["title"] + "</li>\n";
                    }
                    boxInfo = boxInfo + "</ul>\n";
                    boxInfo = boxInfo + "<br class=\"clear-all\" /><br />\n";
                    boxInfo = boxInfo + "</div><!--info-->\n";
                    boxInfo = boxInfo + "<div class=\"backtotop_r\"><a href=\"#\">กลับด้านบน</a></div>\n";
                    boxInfo = boxInfo + "</div>\n";
                }
                




                //Response.Write(boxInfo);
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
                pageContent = pageContent.Replace(Keyword, boxInfo);
                pageContent = pageContent.Replace("DisplayRate();", "");

                string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />";
                pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);
                pageContent = pageContent.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
                pageContent = pageContent.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(17, detail.ProductID, 1));
                GenerateFile gf;

                gf = new GenerateFile(pathDestination, detail.FileInfo, pageContent);
                gf.LangID = LangID;
                gf.CreateFile();
            }
            

        }

        private void GenPageProductGallery(string layout, FrontProductDetail detail)
        {
            //Replace TabMenu
            string pageContent;
            string Keyword = string.Empty;
            string jsRate = string.Empty;
            string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Photo, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp", "_photo.asp"));

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(3, detail));
            pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Photo, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Photo, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Photo, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));

            pageContent = pageContent.Replace("<!--##meta_tag##-->", MetaTag);
            //-----------------
            string GalleryList = string.Empty;
           
            //GalleryList = GalleryList + "<h4 class=\"name\">HOTEL NAME</h4>\n";
            

            if(detail.IsNewPic)
            {
                
                FrontProductPicture picture = new FrontProductPicture();
                List<FrontProductPicture> pictureList = picture.GetProductImageLarge(detail.ProductID);

                GalleryList = GalleryList + "<div id=\"content_why\">\n";
                GalleryList = GalleryList + "<div id=\"why_us_header\"><h4>Photo Gallery</h4></div>\n";
                GalleryList = GalleryList + "<div id=\"gallery\">\n";
                GalleryList = GalleryList + picture.GenGelleryTable(pictureList, GalleryDisplay.Product);
                GalleryList = GalleryList + picture.GenGelleryTable(pictureList, GalleryDisplay.Option);
                GalleryList = GalleryList + picture.GenGelleryTable(pictureList, GalleryDisplay.Constuction);
                GalleryList = GalleryList + picture.GenGelleryTable(pictureList, GalleryDisplay.Itinerary);
                GalleryList = GalleryList + "</div><!--gallery--> \n";
                GalleryList = GalleryList + "<div class=\"backtotop_r\"><a href=\"#\">Back to top</a></div>\n";
                GalleryList = GalleryList + "</div>\n";
                //foreach (FrontProductPicture item in pictureList)
                //{
                //    GalleryList = GalleryList + "<a rel=\"gallery_list\" href=\"http://www.hotels2thailand.com" + item.PicturePath.Replace("thumb_45_40", "larg_300_200") + "\" title=\"" + item.Title + "\"><img alt=\"\" src=\"http://www.hotels2thailand.com" +item.PicturePath +"\" /></a>\n";
                //}
            }else{
                GalleryList = GalleryList + "<div id=\"content_why\">\n";
                GalleryList = GalleryList + "<div id=\"why_us_header\"><h4>Photo Gallery</h4></div>\n";
                GalleryList = GalleryList + "<div id=\"gallery\">\n";
                GalleryList = GalleryList + "<div class=\"hotel_thumb_old\">\n";
                for (int count = 1; count <= detail.NumPic; count++)
                {
                    //GalleryList = GalleryList + "<a href=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_c_" + count + ".jpg\"  class=\"lightbox\"> <img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_b_" + count + ".jpg\" /></a>\n";
                    GalleryList = GalleryList + "<a rel=\"gallery_list\" href=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "-pic/" + detail.ProductCode + "_c_" + count + ".jpg\" title=\"\"><img alt=\"\" src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "-pic/" + detail.ProductCode + "_b_" + count + ".jpg\" /></a>\n";
                }
                GalleryList = GalleryList + "</div></div></div>";
            }
           
            

            Keyword = Utility.GetKeywordReplace(pageContent, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
            pageContent = pageContent.Replace(Keyword, GalleryList);


            string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />\n";
            //jsInclude=jsInclude+"<script type=\"text/javascript\" src=\"/lightbox/js/jquery.lightbox-0.5.js\"></script>\n";
            //jsInclude=jsInclude+"<link rel=\"stylesheet\" type=\"text/css\" href=\"/lightbox/css/jquery.lightbox-0.5.css\" media=\"screen\" />\n";
            jsInclude = jsInclude + "<script type=\"text/javascript\">\n";
            jsInclude = jsInclude + "$(document).ready(function(){ renderGallery(\"gallery_list\")})\n";
            jsInclude = jsInclude + "</script>\n";
            pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);
            pageContent = pageContent.Replace("DisplayRate();", "");
            pageContent = pageContent.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
            pageContent = pageContent.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(18, detail.ProductID, 1));
            //pageContent = pageContent.Replace(Keyword, "");
            GenerateFile gf;

            gf = new GenerateFile(pathDestination, detail.FilePhoto, pageContent);
            gf.LangID = LangID;
            gf.CreateFile();
        }
        private void GenPageProductReview(string layout, FrontProductDetail detail)
        {

            
            string pageContent;
            string Keyword = string.Empty;
            string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Review, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Review, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp", "_review.asp"));


            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(4, detail));
            pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Review, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Review, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Review, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));

            pageContent = pageContent.Replace("<!--##meta_tag##-->", MetaTag);
            //-----------------
            ReviewGenerate review = new ReviewGenerate(detail.CategoryID);

            string reviewAll = review.GenerateReviewPage(detail.ProductID);
            Keyword = Utility.GetKeywordReplace(pageContent, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
            pageContent = pageContent.Replace(Keyword, reviewAll);

            
            string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />";
            pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);
            pageContent = pageContent.Replace("DisplayRate();", "");
            pageContent = pageContent.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
            pageContent = pageContent.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(19, detail.ProductID, 1));
            GenerateFile gf;

            gf = new GenerateFile(pathDestination, detail.FileReview, pageContent);
            gf.LangID = LangID;
            gf.CreateFile();

        }
        private void GenPageProductWhy(string layout, FrontProductDetail detail)
        {
            //Replace TabMenu
            string pageContent;
            string Keyword = string.Empty;
            string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Why, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Why, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp", "_why.asp"));


            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(5, detail));
            pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Why, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Why, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Why, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));

            pageContent = pageContent.Replace("<!--##meta_tag##-->", MetaTag);
            //-----------------
            string whyContent = string.Empty;
            if(LangID==1)
            {
            whyContent=GetStreamReader("/Layout/rate_why_us_content_en.html");
            }else{
            whyContent=GetStreamReader("/Layout-TH/rate_why_us_content_th.html");
            }
            

            Keyword = Utility.GetKeywordReplace(pageContent, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
            pageContent = pageContent.Replace(Keyword, whyContent);


            string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />";
            pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);
            pageContent = pageContent.Replace("DisplayRate();", "");
            pageContent = pageContent.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
            pageContent = pageContent.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(20, detail.ProductID, 1));
            GenerateFile gf;

            gf = new GenerateFile(pathDestination, detail.FileWhy, pageContent);
            gf.LangID = LangID;
            gf.CreateFile();
            //HttpContext.Current.Response.Write(whyContent);
            //HttpContext.Current.Response.End();
        }

        private void GenPageProductContact(string layout, FrontProductDetail detail)
        {

            //Replace TabMenu
            string pageContent;
            string Keyword = string.Empty;
            string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Contact, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Contact, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp", "_contact.asp"));


            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(6, detail));
            pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Contact, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Contact, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Contact, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##meta_tag##-->", MetaTag);

            //-----------------
            string whyContent = string.Empty;
            if(LangID==1)
            {
            whyContent=GetStreamReader("/Layout/rate_contact_content_en.html");
            }else{
            whyContent=GetStreamReader("/Layout/rate_contact_content_th.html");
            }
            

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");

            pageContent = pageContent.Replace(Keyword, whyContent);

            string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />\n";
            jsInclude = jsInclude + "<link href=\"/css/site_contact.css\" type=\"text/css\" rel=\"Stylesheet\"  />\n";
            jsInclude = jsInclude + "<script type=\"text/javascript\" language=\"javascript\" src=\"/scripts/jquery.validate.min.js\"></script>\n";
            jsInclude = jsInclude + "<script type=\"text/javascript\" language=\"javascript\" src=\"/scripts/additional-methods.min.js\"></script>\n";
            jsInclude = jsInclude + "<script type=\"text/javascript\" language=\"javascript\" src=\"/scripts/contac.js\"></script>";
            pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);
            pageContent = pageContent.Replace("DisplayRate();", "");
            pageContent = pageContent.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
            pageContent = pageContent.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(21, detail.ProductID, 1));
            GenerateFile gf;
            gf = new GenerateFile(pathDestination, detail.FileContact, pageContent);
            gf.LangID = LangID;
            gf.CreateFile();
        }
    }
}