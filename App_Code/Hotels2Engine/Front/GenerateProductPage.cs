using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GenerateProductPage
/// </summary>
/// 

namespace Hotels2thailand.Front
{
    public class GenerateProductPage:Hotels2BaseClass
    {
        
        private string layout;
        
        private ProductDetail detail;
        private short destinationID;
        
        private byte categoryID;
        private List<ProductDetail> productDetailList;
        private string pathDestination;
        private Navigator nav;
        private ProductReviewLast reviews;
        private ProductReviewLast review;
        private FrontProductRelate objProductRelate;

        private byte _langID=1;

        public byte LangID
        {
            set { _langID = value; }
        }

        public GenerateProductPage()
        {
            //destinationID = 31;
            //categoryID = 29;
            //locationID=short.Parse(Request.Form["locationID"]);
            //productID = 52;
           
        }

        private string GetStreamReader(string path)
        {

            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath(path));
            string read = objReader.ReadToEnd();
            objReader.Close();
            return read;
        }

        public void GenAllDestination(byte catID)
        {
            categoryID = catID;
            if (categoryID == 29)
            {
                nav = new Navigator();
                nav.LoadLocationLink();
            }
            else {
                nav = new Navigator();
                nav.LoadDestinationLink(categoryID);
            }

            reviews = new ProductReviewLast();
            reviews.LoadAllProductReviewLast(categoryID);
           
            objProductRelate = new FrontProductRelate();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select destination_id from tbl_destination";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    destinationID = (short)reader["destination_id"];
                    categoryID = catID;

                    objProductRelate.LoadAllProductInDestination(destinationID, categoryID);
                    LoadProductDetail();
                    GenPageAll();
                }
            }
            
        }


        private string RenderAllThailandHotel()
        {

            string result = string.Empty;
            int countItem = 0;
            bool hasLanguage = false;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select dn.title,dn.file_name from tbl_destination d,tbl_destination_name dn";
                sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id";
                sqlCommand = sqlCommand + " and dn.lang_id=" + _langID + " order by dn.title";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                result = result + "<div id=\"tophotel_footer\">\n";
                if(_langID==1)
                {
                    result = result + "<h4>All thailand hotels destination</h4> </div>\n";
                }else{
                    result = result + "<h4>โรงแรมทั้งหมดในประเทศไทย</h4> </div>\n";
                }
                
                result = result + "<table id=\"content_tophotel_footer_b\">\n";
                result = result + "<tr valign=\"top\">\n";

                while (reader.Read())
                {
                    hasLanguage = true;
                    if (countItem % 3 == 0)
                    {
                        result = result + "</tr><tr>\n";
                    }
                    result = result + "<td class=\"all_col\"> <a href=\"/" + reader["file_name"] + "\">" + reader["title"] + " Hotels</a> </td>\n";
                    countItem = countItem + 1;
                }
                result = result + "</tr>\n";
                result = result + "</table>\n";
                result = result + "<div id=\"bg_end_footer\"></div>\n";  
            }

            if (!hasLanguage)
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    string sqlCommand = "select dn.title,dn.file_name from tbl_destination d,tbl_destination_name dn";
                    sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id";
                    sqlCommand = sqlCommand + " and dn.lang_id=1 order by dn.title";
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    result = result + "<div id=\"tophotel_footer\">\n";
                    result = result + "<h4>All thailand hotels destination</h4> </div>\n";
                    result = result + "<table id=\"content_tophotel_footer_b\">\n";
                    result = result + "<tr valign=\"top\">\n";

                    while (reader.Read())
                    {
                        if (countItem % 3 == 0)
                        {
                            result = result + "</tr><tr>\n";
                        }
                        result = result + "<td class=\"all_col\"> <a href=\"/" + reader["file_name"] + "\">" + reader["title"] + " Hotels</a> </td>\n";
                        countItem = countItem + 1;
                    }
                    result = result + "</tr>\n";
                    result = result + "</table>\n";
                    result = result + "<div id=\"bg_end_footer\"></div>\n";
                }
            }
            return result;
        }

        private void LoadLayout()
        {
            //short destinationID = 30;
            //byte categoryID = 29;

            layout = GetStreamReader("/Layout/rate_old_version.html");
            //Replace Navigator
            string Keyword = Utility.GetKeywordReplace(layout, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");

            if(categoryID==29)
            {
                
                layout = layout.Replace(Keyword, nav.GenNavigator(detail.CategoryID, detail.LocationID, detail.Title));
            }else{
                layout = layout.Replace(Keyword, nav.GenNavigatorOtherProduct(detail.CategoryID, detail.DestinationID, detail.Title));
            }
            //-----------------

            //Replace Popular
            ProductList list = new ProductList(_langID);
            //HttpContext.Current.Response.Write(_langID+"Hello1");
            list.MaxRecord = 5;
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");
            layout = layout.Replace(Keyword, list.RenderPopularDestination(detail.DestinationID, categoryID));
            //--------------

            //Replace Recent
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@RecentProductStart##-->", "<!--##@RecentProductEnd##-->");
            layout = layout.Replace(Keyword, "");
            //-----------
            
            //Replace Product Detail
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductDetailStart##-->", "<!--##@ProductDetailEnd##-->");
            layout = layout.Replace(Keyword, GetProductDelail(detail));

            
            
            review = reviews.GetReviewByProductID(detail.ProductID);


            string reviewScrore = "";
            string reviewContent = "";
            string reviewLastContent = "";
            if (review != null)
            {
                reviewLastContent = review.Content;
                if (reviewLastContent.Length > 120)
                {
                    reviewLastContent = reviewLastContent.Substring(0, 119);
                }
                reviewScrore = reviewScrore + "<h3>" + Utility.GetHotelReviewText((int)review.AverageReview, _langID) + "," + (review.AverageReview).ToString("#,###.0") + "</h3>\n";
                reviewScrore = reviewScrore + "<div class=\"icon_review_blue" + ((int)review.AverageReview/2) + "\"></div> \n";
                reviewScrore = reviewScrore + "<p class=\"score_review_link\" style=\"line-height:24px\">Score from <a href=\""+detail.FileReview+"\"#infoPan>" + review.CountReview + " reviews</a> </p> \n";

                reviewContent = reviewContent + "<div id=\"review_bg\">\n";
                reviewContent = reviewContent + "<div class=\"review_tex\"><p><a href=\"" + detail.FileReview + "#infoPan\">Lasted review for " + detail.Title + "</a></p>\n";
                reviewContent = reviewContent + "“" + reviewLastContent + "</div>\n";
                reviewContent = reviewContent + "<div class=\"name_custom_review\"><span>" + review.Fullname + ",</span>" + review.ReviewFrom + "</div>\n";
                reviewContent = reviewContent + "</div>\n";



                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ReviewScoreStart##-->", "<!--##@ReviewScoreEnd##-->");
                layout = layout.Replace(Keyword, reviewScrore);

                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ReviewLastContentStart##-->", "<!--##@ReviewLastContentEnd##-->");
                layout = layout.Replace(Keyword, reviewContent);
            }
            else
            {


                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ReviewScoreStart##-->", "<!--##@ReviewScoreEnd##-->");



                reviewScrore = reviewScrore + "<p class=\"score_review_link\">Average user rating: Not yet rated | <a href=\"http://174.36.32.56/review_write.aspx?pid="+detail.ProductID+"\">Write Review</a></p>";

                layout = layout.Replace(Keyword, reviewScrore);
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ReviewLastContentStart##-->", "<!--##@ReviewLastContentEnd##-->");
                layout = layout.Replace(Keyword, "");
            }
            
            layout = layout.Replace("<!--##@DestDefault##-->", "<input type=\"hidden\" name=\"destDefault\" id=\"destDefault\" value=\"" + detail.DestinationID + "\" />");
            layout = layout.Replace("<!--##@LocDefault##-->", "<input type=\"hidden\" name=\"locDefault\" id=\"locDefault\" value=\"" + detail.LocationID + "\" />");
            layout = layout.Replace("<!--##@CategoryDefault##-->", "<input type=\"hidden\" name=\"category\" id=\"category\" value=\"" + detail.CategoryID + "\" />");
            layout = layout.Replace("<!--##@ProductDefault##-->", "<input type=\"hidden\" name=\"productDefault\" id=\"productDefault\" value=\"" + detail.ProductID + "\" />");

            objProductRelate = new FrontProductRelate();
            objProductRelate.LangID = _langID;
            objProductRelate.LoadAllProductInDestination(detail.DestinationID, detail.CategoryID);
            

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@HotelRelateIncludeStart##-->", "<!--##@HotelRelateIncludeEnd##-->");
            if (detail.CategoryID == 29)
            {
                layout = layout.Replace(Keyword, objProductRelate.RenderProductRelateforRatePage(detail));
            }
            else 
            {
                layout = layout.Replace(Keyword,"");
            }
            
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@AllHotelIncludeStart##-->", "<!--##@AllHotelIncludeEnd##-->");
            layout = layout.Replace(Keyword,objProductRelate.RenderProductRelateClassforRatePage(detail));
            Keyword = Utility.GetKeywordReplace(layout, "<!--###AllThailandHotelDestinationStart##-->", "<!--###AllThailandHotelDestinationEnd##-->");
            if(detail.CategoryID==29)
            {
            layout = layout.Replace(Keyword, RenderAllThailandHotel());
            }else{
                layout = layout.Replace(Keyword, "");
            }
            
            Keyword = Utility.GetKeywordReplace(layout, "<!--###ProductDetailBoxStart###-->", "<!--###ProductDetailBoxEnd###-->");

            string productContent = string.Empty;
            productContent = productContent + "<div id=\"tophotel_footer\"> <h2>"+detail.Title+" detail</h2> </div>\n";
            productContent=productContent+"<div class=\"content_tophotel_footer_b\">\n";
        	productContent=productContent+"<div class=\"hotels_detail\">\n";
            productContent=productContent+detail.Detail+"\n";
            productContent=productContent+"</div>\n";
            productContent=productContent+"</div>\n";

           
            
            if(categoryID==29){
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                   
                    int countItem = 0;

                    string sqlCommand = "select po.option_id,poc.title";
                    sqlCommand = sqlCommand + " from tbl_product_option po,tbl_product_option_content poc ";
                    sqlCommand = sqlCommand + " where po.option_id=poc.option_id and product_id = " + detail.ProductID + " and po.status=1 and cat_id=38";
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    productContent = productContent + "<table id=\"content_tophotel_footer_b\">\n";
                    productContent = productContent + "<tr valign=\"top\">\n";

                    while (reader.Read())
                    {
                        if (countItem % 3 == 0)
                        {
                            productContent = productContent + "</tr><tr>\n";
                        }
                        productContent = productContent + "<td class=\"all_col\">  <a href=\"" + detail.FileMain.Replace(".asp", "_room_" + reader["option_id"] + ".asp") + "\">" + reader["title"] + "</a> </td>\n";
                        countItem = countItem + 1;
                    }

                    productContent = productContent + "</tr>\n";
                    productContent = productContent + "</table>\n";
                    productContent = productContent + "<div id=\"bg_end_footer\"></div><br />\n";

                }

                
            }

            layout = layout.Replace(Keyword, productContent);

            string HotelGroup = "";
            LocationGroup objHotelGroup = new LocationGroup();
            List<LocationGroup> hotelGroupList = objHotelGroup.getLocationGroup(categoryID, detail.DestinationID, detail.LocationID, 1);
            int countCol = 0;

            foreach (LocationGroup item in hotelGroupList)
            {

                    if (countCol == 0)
                    {
                        HotelGroup = HotelGroup + "<div id=\"tophotel_footer\"><h4>Hotels in related area with "+detail.Title+"</h4> </div>\n";
                        HotelGroup = HotelGroup + "<table id=\"content_tophotel_footer_b\">\n";
                        HotelGroup = HotelGroup + "<tr valign=\"top\">\n";
                    }
                    if (countCol % 3 == 0)
                    {
                        HotelGroup = HotelGroup + "</tr><tr>\n";
                    }
                    HotelGroup = HotelGroup + "<td class=\"all_col\">  <a href=\"/" + item.PathName + "\">" + item.ProductTitle + "</a> </td>\n";
                    countCol = countCol + 1;

            }
            HotelGroup = HotelGroup + "</tr>\n";

            HotelGroup = HotelGroup + "</table>\n";
            HotelGroup = HotelGroup + "<div id=\"bg_end_footer\"></div><br />\n";
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@AllHotelGroupStart##-->", "<!--##@AllHotelGroupEnd##-->");
            layout = layout.Replace(Keyword,HotelGroup);
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@IteneraryStart##-->", "<!--##@IteneraryEnd##-->");
            layout = layout.Replace(Keyword,"");
            
            
            layout = layout.Replace("<!--##meta_description##-->",SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta,SEO_PageType.Detail,detail.CategoryID,detail.DestinationTitle,detail.LocationTitle,detail.Title));
            
            layout = layout.Replace("http://www.hotels2thailand.com", "http://174.36.32.56");

            //HttpContext.Current.Response.Write(layout);
            //HttpContext.Current.Response.End();
        }


        private string GetTabMenu(int typeMenu)
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
                    actBook = "booknow_ho";
                    break;
                case 2:
                    actInfo = "info_ho";
                    break;
                case 3:
                    actGallery = "gall_ho";
                    break;
                case 4:
                    actReview = "review_ho";
                    break;
                case 5:
                    actWhy = "why_ho";
                    break;
                case 6:
                    actContact = "end_ho";
                    break;
            }
            string result = "<a name=\"infoPan\"></a><div id=\"menu_rate\">\n";
            result = result + "<a href=\"" + detail.FileMain + "#infoPan\" class=\"" + actBook + "\">Book Now</a>\n";
            result = result + "<a href=\"" + detail.FileInfo + "#infoPan\" class=\"" + actInfo + "\">Hotel Information</a>\n";
            result = result + "<a href=\"" + detail.FilePhoto + "#infoPan\" class=\"" + actGallery + "\">Photo Gallery</a>\n";
            result = result + "<a href=\"" + detail.FileReview + "#infoPan\" class=\"" + actReview + "\">Traveler Reviews</a>\n";
            result = result + "<a href=\"" + detail.FileWhy + "#infoPan\" class=\"" + actWhy + "\">Why Us?</a>\n";
            result = result + "<a href=\"" + detail.FileContact + "#infoPan\" class=\"" + actContact + "\">Contact Us</a> \n";
            result = result + "</div>\n";
            return result;

        }
        private void LoadProductDetail()
        {
            ProductDetail productDetail = new ProductDetail();
            productDetailList = productDetail.GetProductList(destinationID,categoryID);

        }

        public string GetOtherProductPageTemplate(int ProductID,byte catID,byte langID)
        {
            string layoutPage = string.Empty;
            string Keyword = string.Empty;

            ProductDetail productDetail = new ProductDetail();
            detail = productDetail.GetProductByID(ProductID, 1);
            if(langID==1)
            {
                layoutPage = GetStreamReader("/Layout/rate_old_version.html");
            }else{
                layoutPage = GetStreamReader("/Layout-th/rate_old_version.html");
            }
            
            Keyword = Utility.GetKeywordReplace(layoutPage, "<!--##@ProductDetailStart##-->", "<!--##@ProductDetailEnd##-->");
            layoutPage = layoutPage.Replace(Keyword, "");

            Keyword = Utility.GetKeywordReplace(layoutPage, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            layoutPage = layoutPage.Replace(Keyword,"");

            Keyword = Utility.GetKeywordReplace(layoutPage, "<!--##@NavigatorStart##-->", "<!--##@NavigatorEnd##-->");

            categoryID = catID;
            if (categoryID == 29)
            {
                nav = new Navigator();
                nav.LoadLocationLink();
                layoutPage = layoutPage.Replace(Keyword, nav.GenNavigator(detail.CategoryID, detail.LocationID, detail.Title));
            }
            else
            {
                //HttpContext.Current.Response.Write("xx");
                nav = new Navigator();
                nav.LoadDestinationLink(categoryID);
                layoutPage = layoutPage.Replace(Keyword, nav.GenNavigatorOtherProduct(detail.CategoryID, detail.DestinationID, detail.Title));
            }

            //Replace Popular
            ProductList list = new ProductList();
            list.MaxRecord = 5;
            Keyword = Utility.GetKeywordReplace(layoutPage, "<!--##@PopularDestinationStart##-->", "<!--##@PopularDestinationEnd##-->");
            layoutPage = layoutPage.Replace(Keyword, list.RenderPopularDestination(detail.DestinationID, categoryID));
            //--------------

            //Replace Recent
            Keyword = Utility.GetKeywordReplace(layoutPage, "<!--##@RecentProductStart##-->", "<!--##@RecentProductEnd##-->");
            layoutPage = layoutPage.Replace(Keyword, "");
            //-----------

            //string tellForm = string.Empty;
            //tellForm = tellForm + "<table align=\"center\">";
            //tellForm = tellForm + "<tr><td>To send "+detail.Title+" to other people, provide their e-mail addresses below</td></tr>";
            //tellForm = tellForm + "<tr><td>Your Name:*<br/><input type=\"text\" name=\"fName\" id=\"fName\" class=\"inputTxt500\"></td></tr>";
            //tellForm = tellForm + "<tr><td>Your Email:*<br/><input type=\"text\" name=\"email\" id=\"email\" class=\"inputTxt500\"></td></tr>";
            //tellForm = tellForm + "<tr><td>Friend's Name:*<br/><input type=\"text\" name=\"friendName\" id=\"friendName\" class=\"inputTxt500\"></td></tr>";
            //tellForm = tellForm + "<tr><td>Friend's email address:*<br/><input type=\"text\" name=\"friendEmail\" id=\"friendEmail\" class=\"inputTxt500\"></td></tr>";
            //tellForm = tellForm + "<tr><td>Optional message:(up to 1000 characters in length)<br/>";
            //tellForm = tellForm + "<textarea rows=\"5\" class=\"inputTxt500\"></textarea>";
            //tellForm = tellForm + "</td></tr>";
            //tellForm = tellForm + "<tr><td align=\"center\"><input type=\"submit\" name=\"submit\" value=\"Send\"/></td></tr>";
            //tellForm = tellForm + "</table>";

            Keyword = Utility.GetKeywordReplace(layoutPage, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
            layoutPage = layoutPage.Replace(Keyword, "<!--##@InsertContent##-->");

            return layoutPage;
        }

        public string GetReviewTemplate(int ProductID,byte catID)
        {


            string layoutReview = string.Empty;
            layoutReview = GetStreamReader("/Layout/rate_old_version.html"); ;
            categoryID = catID;
            if (categoryID == 29)
            {
                nav = new Navigator();
                nav.LoadLocationLink();
            }
            else
            {
                //HttpContext.Current.Response.Write("xx");
                nav = new Navigator();
                nav.LoadDestinationLink(categoryID);
            }

            ProductDetail productDetail = new ProductDetail();
            detail = productDetail.GetProductByID(ProductID, 1);

            
            string pageContent=string.Empty;
            string Keyword = string.Empty;


            //detail=productDetail.GetProductByID(ProductID, 1)[0];
            

                reviews = new ProductReviewLast();
                reviews.GetReviewByID(ProductID,catID);

                LoadLayout();

                //Keyword = Utility.GetKeywordReplace(layoutReview, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
                //pageContent = layoutReview.Replace(Keyword, GetTabMenu(4));
                //Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
                //pageContent = pageContent.Replace(Keyword, "<--##@ReviewList##-->");
                //---------------
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
                pageContent = layout.Replace(Keyword, GetTabMenu(4));

                Keyword = Utility.GetKeywordReplace(pageContent, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
                pageContent = pageContent.Replace(Keyword, "<--##@ReviewList##-->");
                string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />";
                pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);
                pageContent = pageContent.Replace("DisplayRate();", "");
                pageContent = pageContent.Replace("<!--##IncludeASPFile##-->", WebStatPlugIn.TrackingIncludeFunction());
                pageContent = pageContent.Replace("<!--##Tracking##-->", WebStatPlugIn.TrackingScript(19, detail.ProductID, 1));
            return pageContent;
        }

        private void GenPageAll()
        {
            LinkGenerator link = new LinkGenerator();
            link.LoadData(_langID);
            pathDestination = link.GetFolderPath(destinationID, categoryID);
            //pathDestination = link.GetFolderPath(56, 29);
            if (!string.IsNullOrEmpty(pathDestination))
            {
                if (!Directory.Exists(pathDestination))
                {
                    DirectoryInfo di = Directory.CreateDirectory(pathDestination);
                    //Response.Write("Create Directory Successful");
                }

                foreach (ProductDetail item in productDetailList)
                {
                    detail = item;
                    LoadLayout();

                    GenPageProductRate();
                    GenPageProductInfo();
                    GenPageProductGallery();
                    GenPageProductWhy();
                    GenPageProductContact();
                    //Response.Write(detail.Title+"<br>");

                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }
        private string GetProductDelail(ProductDetail detail)
        {

            //detail = new ProductDetail(productID);
            //detail.Load();

            string result = "<div id=\"intro_rate\">\n";
        
            result=result+"<div class=\"product_detail\">\n";
        	result=result+"<div class=\"product_detail_rate\">\n";
            
            result=result+"<div class=\"product_detail_box\">\n";
       		result=result+"<div id=\"title\">\n";
    		result=result+"<div class=\"title_hotels\"><h2>"+detail.Title+"</h2></div>\n";
            if(detail.CategoryID==29){
            result = result + "<div class=\"class_star_" + Utility.GetHotelClassImage(detail.Star, 1) + "\"></div> \n";      
            }
   			result=result+"</div> \n";
            
            	
        		
            
           	result=result+"<div class=\"clear-all\"></div>  \n"; 

            //Display address
            if (detail.CategoryID == 29)
            {
            result=result+"<div class=\"address\">"+detail.Title+" Address : "+detail.Address+"</div> <br />\n";
            }
   	    	
              
            result=result+"<div class=\"score_title\">\n";
            result=result+"<!--##@ReviewScoreStart##-->\n";
            result=result+"<h3>Superb,9.3</h3>\n";
            result=result+"<div class=\"icon_review_blue\"></div>\n";              
            result=result+"<p class=\"score_review_link\" style=\"line-height:24px\">Score from <a href=\"#\">123 reviews</a> </p> \n";
            result=result+"<!--##@ReviewScoreEnd##-->\n";

            if(categoryID==29){
            result = result + "<div class=\"map\"><a href=\"http://174.36.32.56/thailand-hotels-map.aspx?pid="+detail.ProductID+"\" target=\"_blank\">Map</a></div>\n";
            }

            if (categoryID == 32)
            {
                result = result + "<div class=\"map\"><a href=\"http://174.36.32.56/thailand-golf-map.aspx?pid=" + detail.ProductID + "\" target=\"_blank\">Map</a></div>\n";
            }

            if (detail.HasInternet)
            {

                if (detail.IsInternetFree)
                {
                    result = result + "<div class=\"internet\"><a class=\"tooltip\">Internet<span class=\"tooltip_content\">Internet Free</span></a></div>\n";
                }
                else {
                    if (!string.IsNullOrEmpty(detail.InternetDetail))
                    {
                        result = result + "<div class=\"internet\"><a class=\"tooltip\">Internet<span class=\"tooltip_content\">" + detail.InternetDetail + "</span></a></div>\n";
                    }
                    else
                    {
                        result = result + "<div class=\"internet\"><a class=\"tooltip\">Internet<span class=\"tooltip_content\">Has Internet</span></a></div>\n";
                    }
                }
            }
            
              		
              
            result=result+"</div>\n";
            result=result+"<div class=\"clear-all\"></div>\n";
               
            result=result+"</div><!--product_detail_box-->  \n";      

      	    result=result+"<div class=\"icon_paylater\">\n";
            result = result + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_a.jpg\"/></a>\n";
                
                 
            
 
            result=result+"</div><!--icon_paylater-->  \n";

            
            result=result+"<div class=\"review_score\">\n";
            
            
               

            
   	 	 	result=result+"<div id=\"pic_small_fac\">\n";
            int maxPic = 1;
            if (!detail.IsNewPic)
            {
                for (int countPic = 1; countPic <= detail.NumPic; countPic++)
                {
                    result = result + "<a href=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_c_" + countPic + ".jpg\" class=\"imgFloat\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_b_" + countPic + ".jpg\" /></a>\n";
                    if (maxPic == 12)
                    {
                        break;
                    }
                    maxPic = maxPic + 1;
                }
            }
         	result=result+"</div>\n";
       
      
           result=result+"<br class=\"clear-all\" />\n";
        
           result=result+"<div class=\"see_more\"><a href=\""+detail.FilePhoto+"\">More Picture</a></div> \n";
       
                
           result=result+"</div> <!--review_score-->\n";
       
           result=result+"<div class=\"booknow_paylater\">\n";
           result = result + "<a href=\"#\" class=\"tooltip\"><img src=\"../theme_color/blue/images/icon/row_rate.png\" class=\"lowrates\"/><span class=\"tooltip_content\"><strong>Low Rate Guarantee</strong><br /> We save your money. Guarantee the rate you receive on our site is the best.<br /><br /><strong>High Security Website</strong><br />Ensure high security with SSL 128 Bit.  </span></a>";
           result=result+"</div>\n";
           result = result + "<div class=\"booknow_top\"><a href=\""+detail.FileMain+"#infoPan\"></a></div>\n";

           result=result+"<br class=\"clear-all\" />\n";

           result=result+"<!--##@NearByIncludeStart##-->\n";
           result=result+"<!--##@NearByIncludeEnd##-->  \n";
           result=result+"<!--##@ReviewLastContentStart##-->\n";
           result=result+"<!--##@ReviewLastContentEnd##--> \n";    
           result=result+"<br class=\"clear-all\" />\n";
           result=result+"</div><!--product_detail_content-->\n";
           result=result+"</div><!--product_detail-->\n"; 
        

        
   	       result=result+"<div class=\"clear-all\"></div>\n";
           result = result + "<!--##@IteneraryStart##-->";
           result=result+"<div class=\"product_detail\">\n";
           result=result+"<div class=\"product_detail_content\">\n";
           result=result+"<p><span>What to bring : </span>\n";
           result=result+" Swimming wear, shorts, Light T-shirt, Beach Towel, Sun Block, Sun Cap, Sunglasses, Camera, Extra Films, Light Deck Shoes and a little Money for Sundries</p>\n";

           result=result+"</div>\n";        
           result=result+"</div><!--product detail-->\n";
           result = result + "<!--##@IteneraryEnd##-->";
           result=result+"<div class=\"clear-all\"></div>\n";
   	       result=result+"</div>\n";

            return result;
        }

        private void GenPageProductRate()
        {
            //Replace TabMenu
            string pageContent;
            string Keyword = string.Empty;
            string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, "http://www.hotels2thailand.com/"+detail.FolderDestination+"-"+Utility.GetProductType(detail.CategoryID)[0,3]+"/"+detail.FileMain);
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(1));

            pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##meta_tag##-->",MetaTag);
            pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));

            //-----------------
            //Response.Write(pageContent);
            //Response.End();

            Keyword = Utility.GetKeywordReplace(pageContent, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");

            string boxChange = "<a name=\"anchor_search_box\"></a><div id=\"content_rate\">\n";
            boxChange = boxChange + "<div class=\"chack_rate\">\n";
            boxChange = boxChange + "<div class=\"dateSearchPan\">\n";
            boxChange = boxChange + "<P>Please enter to change date</P>\n";
            boxChange = boxChange + "<div id=\"changeDateBox\">\n";
            boxChange = boxChange + "<div class=\"chang_date\"><span>Check in</span>\n";
            boxChange = boxChange + "<input type=\"text\" name=\"dateStart2ci\" id=\"dateStart2ci\" rel=\"datepicker\" style=\"width:125px; float:left; \" />\n";
            boxChange = boxChange + "</div>\n";
            if (categoryID == 29)
            {
                boxChange = boxChange + "<div class=\"chang_date\"><span>Check out</span>\n";
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



            string jsInclude="<script type=\"text/javascript\" src=\"/js/front.ui.rate.js\"></script>" ;
            pageContent = pageContent.Replace("<!--##jsInclude##-->",jsInclude);


            GenerateFile gf;
            gf = new GenerateFile(pathDestination, detail.FileMain, pageContent);
            gf.CreateFile();

        }
        private void GenPageProductInfo()
        {
            //Replace TabMenu
            ProductRelate productRelate = new ProductRelate();

            //Get Facility
           

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select fac_id,title from tbl_facility_product";
                sqlCommand = sqlCommand + " where product_id=" + detail.ProductID + " and lang_id=1";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                string pageContent;
                string Keyword = string.Empty;
                string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
                MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp", "_info.asp"));

                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
                pageContent = layout.Replace(Keyword, GetTabMenu(2));
                pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Information, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
                pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
                pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
                pageContent = pageContent.Replace("<!--##meta_tag##-->", MetaTag);
                //-----------------
                pageContent = pageContent.Replace("<!--##Rate Include##-->", "");



                string boxInfo = string.Empty;
                boxInfo = boxInfo + "<div id=\"content_why\">\n";
                boxInfo = boxInfo + "<div id=\"why_us_header\"><h4>Hotels Information</h4></div>\n";
                boxInfo = boxInfo + "<div id=\"info\">\n";
                boxInfo = boxInfo + "<div class=\"info_print\">\n";
                boxInfo = boxInfo + "<a href=\"http://174.36.32.56/thailand-hotels-map.aspx?pid=" + detail.ProductID + "\" target=\"_blank\" class=\"info_map\">Map & Location</a>\n";
                //boxInfo = boxInfo + "<a href=\"http://www.hotels2thailand.com/coupon/free-coupon.aspx\" class=\"info_free\">Free Discount Coupon </a>\n";
                boxInfo = boxInfo + "<a href=\"#\" class=\"info_tell\">Tell your friend </a>\n";
                //boxInfo = boxInfo + "<a  href=\"http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain + "\" title=\"" + detail.Title + " - Hotels2thailand.com\" class=\"jQueryBookmark\">Bookmark </a>\n";
                boxInfo = boxInfo + "<a href=\"javascript:bookmark_us('http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain + "','" + detail.Title + " - Hotels2thailand.com')\">Bookmark</a>";
                boxInfo = boxInfo + "<a href=\"" + detail.FilePDF + "\" class=\"info_view\">View this page as PDF</a>\n";
                boxInfo = boxInfo + "<a href=\"thailand-hotels-print.aspx?pd=" + detail.ProductID + "\" class=\"info_version\">Print Version</a>\n";
                boxInfo = boxInfo + "<br class=\"clear-all\" />\n";
                boxInfo = boxInfo + "</div><br />\n";
                boxInfo = boxInfo + "<h4>LOCATION : " + detail.Title + " </h4>\n";
                boxInfo = boxInfo + "<p></p>  <br /> \n";


                boxInfo = boxInfo + "<h4>SERVICES & RECREATIONS : CENTARA DUANGTAWAN HOTEL CHIANG MAI </h4>\n";
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




                //Response.Write(boxInfo);
                Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
                pageContent = pageContent.Replace(Keyword, boxInfo);

                string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />";
                pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);
                GenerateFile gf;

                gf = new GenerateFile(pathDestination, detail.FileInfo, pageContent);
                gf.CreateFile();
            }
            

        }
        private void GenPageProductGallery()
        {
            //Replace TabMenu
            string pageContent;
            string Keyword = string.Empty;
            string jsRate = string.Empty;
            string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp", "_photo.asp"));

            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(3));
            pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Photo, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));

            pageContent = pageContent.Replace("<!--##meta_tag##-->",MetaTag);
            //-----------------
            string GalleryList = "<div id=\"content_why\">\n";
            GalleryList = GalleryList + "<div id=\"why_us_header\"><h4>Photo Gallery</h4></div>\n";
            GalleryList = GalleryList + "<div id=\"gallery\">\n";
            //GalleryList = GalleryList + "<h4 class=\"name\">HOTEL NAME</h4>\n";
            GalleryList = GalleryList + "<div class=\"hotel_thumb_old\">\n";
            for (int count = 1; count <= detail.NumPic; count++)
            {
                //GalleryList = GalleryList + "<a href=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_c_" + count + ".jpg\"  class=\"lightbox\"> <img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_b_" + count + ".jpg\" /></a>\n";
                GalleryList = GalleryList + "<a rel=\"gallery_list\" href=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_c_" + count + ".jpg\" title=\"\"><img alt=\"\" src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(categoryID)[0, 3] + "-pic/" + detail.ProductCode + "_b_" + count + ".jpg\" /></a>\n";
            }
            GalleryList = GalleryList + "</div></div></div>";

            Keyword = Utility.GetKeywordReplace(pageContent, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
            pageContent = pageContent.Replace(Keyword, GalleryList);

            
            string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />\n";
            //jsInclude=jsInclude+"<script type=\"text/javascript\" src=\"/lightbox/js/jquery.lightbox-0.5.js\"></script>\n";
            //jsInclude=jsInclude+"<link rel=\"stylesheet\" type=\"text/css\" href=\"/lightbox/css/jquery.lightbox-0.5.css\" media=\"screen\" />\n";
            jsInclude=jsInclude+"<script type=\"text/javascript\">\n";
            jsInclude = jsInclude + "$(document).ready(function(){ renderGallery(\"gallery_list\")})\n";
            jsInclude = jsInclude + "</script>\n";
            pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);
            pageContent = pageContent.Replace("DisplayRate();","");
            //pageContent = pageContent.Replace(Keyword, "");
            GenerateFile gf;

            gf = new GenerateFile(pathDestination, detail.FilePhoto, pageContent);
            gf.CreateFile();
        }
        private void GenPageProductReview() { }
        private void GenPageProductWhy()
        {
            //Replace TabMenu
            string pageContent;
            string Keyword = string.Empty;
            string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp","_why.asp"));


            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(5));
            pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Why, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));

            pageContent = pageContent.Replace("<!--##meta_tag##-->",MetaTag);
            //-----------------
            string whyContent = GetStreamReader("/Layout/rate_why_us_content_en.html");
            Keyword = Utility.GetKeywordReplace(pageContent, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");
            pageContent = pageContent.Replace(Keyword, whyContent);


            string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />";
            pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);

            GenerateFile gf;

            gf = new GenerateFile(pathDestination, detail.FileWhy, pageContent);
            gf.CreateFile();
            //HttpContext.Current.Response.Write(whyContent);
            //HttpContext.Current.Response.End();
        }

        private void GenPageProductContact()
        {

            //Replace TabMenu
            string pageContent;
            string Keyword = string.Empty;
            string MetaTag = SEO_Generate.Function_gen_optimize_string(SEO_StringType.Meta, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title);
            MetaTag = MetaTag + SEO_Generate.Function_gen_optimize_string(SEO_StringType.canonical, SEO_PageType.Detail, detail.CategoryID, "http://www.hotels2thailand.com/" + detail.FolderDestination + "-" + Utility.GetProductType(detail.CategoryID)[0, 3] + "/" + detail.FileMain.Replace(".asp", "_contact.asp"));


            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductTabStart##-->", "<!--##@ProductTabEnd##-->");
            pageContent = layout.Replace(Keyword, GetTabMenu(6));
            pageContent = pageContent.Replace("<!--##product_title##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Title, SEO_PageType.Contact, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##AltIndex##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.Link_Back_To_Home, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##H1##-->", SEO_Generate.Function_gen_optimize_string(SEO_StringType.H1, SEO_PageType.Detail, detail.CategoryID, detail.DestinationTitle, detail.LocationTitle, detail.Title));
            pageContent = pageContent.Replace("<!--##meta_tag##-->", MetaTag);
            
            //-----------------
            string whyContent = GetStreamReader("/Layout/rate_contact_content_en.html");
            
            Keyword = Utility.GetKeywordReplace(layout, "<!--##@ProductRateContentStart##-->", "<!--##@ProductRateContentEnd##-->");

            pageContent = pageContent.Replace(Keyword, whyContent);

            string jsInclude = "<link href=\"/theme_color/blue/style_why.css\" rel=\"stylesheet\" type=\"text/css\" />\n";
            jsInclude = jsInclude + "<link href=\"/css/site_contact.css\" type=\"text/css\" rel=\"Stylesheet\"  />\n";
            jsInclude = jsInclude + "<script type=\"text/javascript\" language=\"javascript\" src=\"/scripts/jquery.validate.min.js\"></script>\n";
            jsInclude = jsInclude + "<script type=\"text/javascript\" language=\"javascript\" src=\"/scripts/additional-methods.min.js\"></script>\n";
            jsInclude = jsInclude + "<script type=\"text/javascript\" language=\"javascript\" src=\"/scripts/contac.js\"></script>";
            pageContent = pageContent.Replace("<!--##jsInclude##-->", jsInclude);

            GenerateFile gf;
            gf = new GenerateFile(pathDestination, detail.FileContact, pageContent);
            gf.CreateFile();
        }
    }
}