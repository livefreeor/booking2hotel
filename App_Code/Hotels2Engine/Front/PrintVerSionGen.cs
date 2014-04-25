using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class PrintVerSionGen : Hotels2BaseClass
    {
        public string pageStructureGenerate(int intProduct_id)
        {
            StringBuilder strResult = new StringBuilder();
            Product cProduct = new Product();
            ProductContent cProductContent = new ProductContent();
            ProductConstruction cConst = new ProductConstruction();
            cProductContent = cProductContent.GetProductContentById(intProduct_id, 1);
            cProduct = cProduct.GetProductById(intProduct_id);

            //HttpContext.Current.Response.Write(cProductContent.Title);
            //HttpContext.Current.Response.End();
            //strResult.Append("<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeFile=\"thailand-hotels-print.aspx.cs\" Inherits=\"thailand_hotels_print\" %>\r\n");
            strResult.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
            strResult.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
            strResult.Append("<head runat=\"server\">\r\n");
            strResult.Append("<title>Hotels 2 Thailand : Expert Thailand Travel</title>\r\n");
            strResult.Append("<link href=\"theme_color/blue/style_print.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n");
            //strResult.Append("<link href=\"theme_color/blue/layout.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n");
            strResult.Append("</head>\r\n");
            strResult.Append("<body>\r\n");
            strResult.Append("<div id=\"main\">\r\n");
            strResult.Append("<div id=\"header_print\">\r\n");
            strResult.Append("<a href=\"http://www.hotels2thailand.com\"><img src=\"/theme_color/blue/images/layout_mail/mail_logo.jpg\" /></a>\r\n");
            strResult.Append("</div>\r\n");
            // Detail ---- 
            strResult.Append("<div id=\"content_line_print\">\r\n");
            strResult.Append("<div class=\"sub_head_print\"></div>\r\n");
            strResult.Append("<div class=\"content\">\r\n");
            strResult.Append("<div class=\"title_hotels\"><h1>" + cProductContent.Title + "</h1></div>\r\n");
            strResult.Append("<div class=\"class_star_s_" + Utility.GetHotelClassImage(cProduct.Star, 1) + "\"></div>\r\n");
            strResult.Append("<div class=\"clear-all\"></div>\r\n");
            strResult.Append("<div class=\"address\">" + cProductContent.Address + "</div>\r\n");
            strResult.Append("<img src=\"../thailand-hotels-pic/" + cProduct.ProductCode + "_a.jpg\" class=\"hotel_img\" />\r\n");
            strResult.Append("<p class=\"detail\">Cher Resort Cha Am Detail :</p>\r\n");
            strResult.Append("<p class=\"hotel_detail\">" + cProductContent.Detail + "</p>\r\n");
            strResult.Append("<br class=\"clear-all\" />\r\n");
            strResult.Append("</div> \r\n");
            strResult.Append("<div id=\"end_print\"></div>\r\n");
            strResult.Append("</div>\r\n");
            //----------------------------
            strResult.Append("\r\n");
            strResult.Append("<div id=\"content_line\">\r\n");
            strResult.Append("<div class=\"sub_head_print\"></div>\r\n");
            strResult.Append("<div class=\"content\">\r\n");
           


            // Facility-----------------
            ProductFacility cFac = new ProductFacility();
            List<object> listFac = cFac.getFacilityByProductId(intProduct_id, 1);
            if (listFac.Count > 0)
            {
                
                strResult.Append("<div class=\"box\">\r\n");
                strResult.Append("<div class=\"line_top\"><span>" + cProductContent.Title + " Service & Recreation</span></div>\r\n");
                strResult.Append("<div class=\"line_menu\"></div>\r\n");

                strResult.Append("<ul class=\"remarks_list_fac\">\r\n");
                foreach (ProductFacility item in listFac)
                {
                    strResult.Append("<li>" + item.Title + "</li>\r\n");
                }
               
                strResult.Append("</ul>\r\n");
                
                
                strResult.Append("<br class=\"clear-all\" />\r\n");
                strResult.Append("<div class=\"backtotop\"><a href=\"#\">Back to top</a></div>\r\n");
                strResult.Append("<div class=\"line_buttom\"></div>\r\n");
                strResult.Append("</div>\r\n");
            }
            //----------------------------

            // constuction-----------------
            Dictionary<string, string> iDicCon = cConst.getProductConstructionByProductId(intProduct_id, 1);
            if (iDicCon.Count > 0)
            {
                strResult.Append("<div class=\"box\">\r\n");
                strResult.Append("<div class=\"line_top\"><span>" + cProductContent.Title + " Dining & Restaurants</span></div>\r\n");
                strResult.Append("<div class=\"line_menu\"></div>\r\n");

                foreach (KeyValuePair<string, string> item in iDicCon)
                {
                    strResult.Append("<p class=\"service\">" + item.Key + "</p>\r\n");
                    strResult.Append("<p class=\"inbox\">"+ item.Value+"</p>\r\n");
                }

                strResult.Append("<div class=\"backtotop\"><a href=\"#\">Back to top</a></div>\r\n");
                strResult.Append("<div class=\"line_buttom\"></div><br/><br/>\r\n");
                strResult.Append("</div>\r\n");
            }
            
            //----------------------------

            // RalateHotel-----------------
            Dictionary<int, string> iDicProduct = cProductContent.getProductsameLocation(intProduct_id, 1);
            if (iDicProduct.Count > 0)
            {
                strResult.Append("<div class=\"box\">\r\n");
                strResult.Append("<div class=\"line_top\"><span>" + cProductContent.Title + " Related Hotels </span></div>\r\n");
                strResult.Append("<div class=\"line_menu\"></div>\r\n");
                strResult.Append("<ul class=\"remarks_list_hotel\">\r\n");
                foreach (KeyValuePair<int, string> item in iDicProduct)
                {
                    strResult.Append("<li>" + item.Value + "</li>\r\n");
                    
                }
                strResult.Append("</ul>\r\n");
                strResult.Append("<br class=\"clear-all\"/>\r\n");
                strResult.Append("<div class=\"backtotop\"><a href=\"#\">Back to top</a></div>\r\n");
                strResult.Append("<div class=\"line_buttom\"></div>\r\n");
                strResult.Append("</div><br/>\r\n");
                
            }
            //----------------------------
            
            strResult.Append("<br class=\"clear-all\"/>\r\n");
            strResult.Append("</div>\r\n");
            strResult.Append("</div>\r\n");
            
            //strResult.Append("</div>\r\n");
            strResult.Append("<div id=\"footer\">\r\n");
            strResult.Append("<div id=\"content\">\r\n");
            strResult.Append("<div id=\"tophotel\">Top Hotel Destinations   </div>\r\n");
            strResult.Append("<div id=\"content_tophotel\">\r\n");
            strResult.Append("<ul><li class=\"head_bue\"><a href=\"http://www.hotels2thailand.com/bangkok-hotels.asp\">Bangkok Hotels</a></li>\r\n");
            strResult.Append("<li><a href=\"#\">Pratunam Hotel</a> </li>\r\n");
            strResult.Append("<li><a href=\"#\">Silom Hotels</a></li>\r\n");
            strResult.Append("<li><a href=\"#\">Sukhumvit Hotels</a></li></ul>\r\n");
            strResult.Append("<ul><li class=\"head_bue\"><a href=\"#\">Chiang Mai Hotels</a> </li>\r\n");
            strResult.Append("<li><a href=\"#\">Chiang Mai City Hotels</a> </li>\r\n");
            strResult.Append("<li><a href=\"#\">Hangdong Hotels</a></li>\r\n");
            strResult.Append("<li><a href=\"#\">Sansai Hotels</a></li></ul>\r\n");
            strResult.Append("<ul><li class=\"head_bue\"><a href=\"#\">Koh Samui Hotels</a></li>\r\n");
            strResult.Append("<li><a href=\"#\">Bo Phut Beach Resort</a> </li>\r\n");
            strResult.Append("<li><a href=\"#\">Chaweng Beach Resort</a></li>\r\n");
            strResult.Append("<li><a href=\"#\">Lamai Beach Rresort</a></li></ul>\r\n");
            strResult.Append("<ul><li class=\"head_bue\"><a href=\"#\">Koh Samui Hotels</a></li>\r\n");
            strResult.Append("<li><a href=\"#\">Bo Phut Beach Resort</a> </li>\r\n");
            strResult.Append("<li><a href=\"#\">Chaweng Beach Resort</a></li>\r\n");
            strResult.Append("<li><a href=\"#\">Lamai Beach Rresort</a></li></ul>\r\n");
            strResult.Append("<div class=\"clear-all\"></div>\r\n");
            strResult.Append("</div></div><!--bgwrite--></div>\r\n");

            strResult.Append("</div>\r\n");
            strResult.Append("<div class=\"end_main\"></div>\r\n");


            strResult.Append("<div id=\"copyright\">\r\n");
            strResult.Append("Copyright © 1996-2010 Hotels 2 Thailand .com. All rights reserved.<br />\r\n");
            strResult.Append("hotels2thailand.com is a registered travel agent with the Tourism Authority of Thailand. TAT License No. 11/3240\r\n");
            strResult.Append("</div>\r\n");
            strResult.Append("\r\n");
            strResult.Append("\r\n");
            strResult.Append("\r\n");
            strResult.Append("\r\n");
            strResult.Append("\r\n");
            strResult.Append("\r\n");
            strResult.Append("</body>\r\n");
            strResult.Append("</html>\r\n");

            return strResult.ToString();
        }

        //public string PageDetailGen(int intProduct_id)
        //{
        //    StringBuilder strResult = new StringBuilder();
        //    Product cProduct = new Product();
        //    ProductContent cProductContent = new ProductContent();
        //    cProductContent = cProductContent.GetProductContentById(intProduct_id, 1);
        //    cProduct = cProduct.GetProductById(intProduct_id);
        //    strResult.Append("<div id=\"content_line_print\">\r\n");
        //    strResult.Append("<div class=\"sub_head_print\"></div>\r\n");
        //    strResult.Append("<div class=\"content\">\r\n");
        //    strResult.Append("<div class=\"title_hotels\"><h1>" + cProductContent.Title + "</h1></div>\r\n");
        //    strResult.Append("<div class=\"class_star_s_" + Utility.GetHotelClassImage(cProduct.Star, 1) + "\"></div>\r\n");
        //    strResult.Append("<div class=\"clear-all\"></div>\r\n");
        //    strResult.Append("<div class=\"address\">" + cProductContent.Address + "</div>\r\n");
        //    strResult.Append("<img src=\"../thailand-hotels-pic/" + cProduct.ProductCode + ".jpg\" class=\"hotel_img\" />\r\n");
        //    strResult.Append("<p class=\"detail\">Cher Resort Cha Am Detail :</p>\r\n");
        //    strResult.Append("<p class=\"hotel_detail\">" + cProductContent.Detail + "</p>\r\n");
        //    strResult.Append("<br class=\"clear-all\" />\r\n");
        //    strResult.Append("</div> \r\n");
        //    strResult.Append("<div id=\"end_print\"></div>\r\n");
        //    strResult.Append("</div>\r\n");

            
        //    return strResult.ToString();
        //}

        public string PageLandMarkGen(int intProduct_id)
        {
            StringBuilder strResult = new StringBuilder();
            ProductLandmark cPLandMark = new ProductLandmark();
            strResult.Append("<div class=\"box\">\r\n");
            strResult.Append("<div class=\"line_top\"><span>Cher Resort Cha Am Nearby Attraction</span></div>\r\n");
            strResult.Append("<div class=\"line_menu\"></div>\r\n");
            strResult.Append("<ul class=\"remarks_list\">\r\n");
            strResult.Append("<li>Hua Hin 14 Km. (By car): 5 Mintues.</li>\r\n");
            strResult.Append("<li>Marriott Courtyard Hotel (By car):2 Mintues.</li>\r\n");
            strResult.Append("</ul>\r\n");
            strResult.Append("<div class=\"backtotop\"><a href=\"#\">Back to top</a></div>\r\n");
            strResult.Append("<div class=\"line_buttom\"></div>\r\n");
            strResult.Append("</div><br/><br/>\r\n");

            return strResult.ToString();
        }
    }
}