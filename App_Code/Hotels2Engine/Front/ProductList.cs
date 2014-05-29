using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ProductPriceList
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class ProductList
    {
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public short DestinationID { get; set; }
        public string DestinationTitle{ get; set; }
        public string LocationTitle { get; set; }
        public string DestinationFileName{ get; set; }
        public string LocationFileName { get; set; }
        public short ProductCategory { get; set; }
        public float Star { get; set; }
        public bool HasInternet { get; set; }
        public bool IsInternetFree { get; set; }
        public string DetailInternet { get; set; }
        public double ReviewAverate { get; set; }
        public int ReviewCount { get; set; }
        public int PromotionID { get; set; }
        public string PromotionTitle { get; set; }
        public string FileName { get; set; }
        public byte CategoryID { get; set; }
        public string ProductCode { get; set; }
        public decimal PriceStart { get; set; }
        public int ConditionID { get; set; }
        public int OptionID { get; set; }
        public byte PointPopular { get; set; }
        public string ImageFeature { get; set; }
        public string ImagePopular { get; set; }
        public bool IsExtranet { get; set; }

        private int pageSize = 10;
        private int maxRecord = 10;
        private int totalRecord = 0;
        private int pageCurrent = 1;
        private byte LangID = 1;

        private fnCurrency currency;

        public int MaxRecord {
            set { maxRecord = value; }
        }

        public int PageSize
        {
            set { pageSize = value; }
        }

        public int PageCurrent
        {
            set { pageCurrent = value; }
        }
        
        private List<ProductList> productList;

        public ProductList()
        {
            currency = new fnCurrency();
            currency.GetCurrency();
        }

        public ProductList(byte langID)
        {
            LangID = langID;
            currency = new fnCurrency();
            currency.GetCurrency();
           
        }

        public List<ProductList> GetProductList(short DestinationID,short LocationID,byte categoryID,string dateStart,string dateEnd,byte sortBy)
        {
            string strCondition = string.Empty;
            if(LocationID !=0)
            {
                strCondition = strCondition + " and pl.location_id=" + LocationID;  
            }

            string recordMax = "";
            if(maxRecord!=0){
                recordMax = "top " + maxRecord;
            }

            string orderBy = "";

            switch (sortBy)
            {
                case 0:
                    orderBy = " order by pc.title asc";
                    break;
                case 1:
                    orderBy = " order by p.point_popular desc";
                    break;
                case 2:
                    orderBy = " order by p.star desc";
                    break;
            }
            
            string sqlCommand = string.Empty;
            //if (categoryID==29)
            //{
                //sqlCommand = "select " + recordMax + " p.product_id,pc.title,p.star,p.is_internet_have,10 as review,pc.file_name_main,p.cat_id,p.destination_id,p.product_code,p.point_popular,dn.title as destination_title,ln.title as location_title,";
                //sqlCommand= sqlCommand+" ISNULL((";
                //sqlCommand = sqlCommand + " select MIN(spocp.rate)";
                //sqlCommand = sqlCommand + " from tbl_product sp,tbl_product_period spp,tbl_product_option spo,tbl_product_option_condition spoc,tbl_product_option_condition_price spocp";
                //sqlCommand = sqlCommand + " where sp.product_id=spp.product_id and sp.product_id=spo.product_id and spo.option_id=spoc.option_id and spoc.condition_id=spocp.condition_id";
                //sqlCommand = sqlCommand + " and spo.status=1 and spoc.status=1 and spp.date_end>GETDATE() and spo.cat_id=38 and spo.product_id=p.product_id";
                //sqlCommand = sqlCommand + " ),0) as priceStart";
                //sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn,tbl_product_location pl,tbl_location l,tbl_location_name ln";
                //sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=d.destination_id and pl.product_id=p.product_id and p.status=1 and dn.lang_id=1 and d.destination_id=dn.destination_id and d.destination_id=" + DestinationID + " and l.location_id=ln.location_id and pl.location_id=l.location_id and ln.lang_id=1 and p.cat_id=" + categoryID;
                //sqlCommand = sqlCommand + strCondition;
                //sqlCommand = sqlCommand + orderBy;

            //sqlCommand = "fr_product_list_count '" + DestinationID + "','" + LocationID + "','"+categoryID+"'";
            
            DataConnect myConn = new DataConnect();
            //totalRecord = myConn.ExecuteScalar(sqlCommand);


            sqlCommand = "fr_list_2 '" + DestinationID + "','" + LocationID + "','" + categoryID + "'," + dateStart+ "," + dateEnd + "," + pageCurrent + "," + pageSize + ","+sortBy+","+LangID+",0";

            
            //HttpContext.Current.Response.Flush();
            //}
            //else
            //
            //    sqlCommand = "select top 10 p.product_id,pc.title,p.star,p.is_internet_have,10 as review,pc.file_name_main,p.cat_id,p.destination_id,p.product_code,p.point_popular,dn.title as destination_title,'' as location_title,";
            //    sqlCommand = sqlCommand + " ISNULL((";
            //    sqlCommand = sqlCommand + " select MIN(spocp.rate)";
            //    sqlCommand = sqlCommand + " from tbl_product sp,tbl_product_period spp,tbl_product_option spo,tbl_product_option_condition spoc,tbl_product_option_condition_price spocp";
            //    sqlCommand = sqlCommand + " where sp.product_id=spp.product_id and sp.product_id=spo.product_id and spo.option_id=spoc.option_id and spoc.condition_id=spocp.condition_id";
            //    sqlCommand = sqlCommand + " and spo.status=1 and spoc.status=1 and spp.date_end>GETDATE() and spo.cat_id=38 and spo.product_id=p.product_id";
            //    sqlCommand = sqlCommand + " ),0) as priceStart ";
            //    sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn";
            //    sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=d.destination_id  and p.status=1 and dn.lang_id=1 and d.destination_id=dn.destination_id and d.destination_id=" + DestinationID + " and dn.lang_id=1 and p.cat_id=" + categoryID;
            //}
            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.End();

            SqlDataReader reader = myConn.GetDataReader(sqlCommand);
            productList = new List<ProductList>();
            string defaultTitle = string.Empty;
            string locationFile = string.Empty;
            string destinationFile = string.Empty;
            string fileName = string.Empty;

            while(reader.Read())
            {
                defaultTitle = reader["title"].ToString();
                destinationFile = reader["destination_filename"].ToString();
                locationFile = reader["location_filename"].ToString();
                fileName = reader["file_name_main"].ToString();

                if(LangID==2){
                    defaultTitle = reader["second_lang"].ToString();
                    fileName = reader["file_name_main"].ToString().Replace(".asp","-th.asp");
                    if (string.IsNullOrEmpty(defaultTitle))
                    {
                        defaultTitle = reader["title"].ToString();
                        //fileName = reader["file_name_main"].ToString();
                    }
                    //HttpContext.Current.Response.Write(fileName + "<br>");
                }
                productList.Add(new ProductList
                { 
                ProductID=(int)reader["product_id"],
                ProductTitle = defaultTitle,
                DestinationID=(short)reader["destination_id"],
                DestinationTitle=reader["destination_title"].ToString(),
                LocationTitle=reader["location_title"].ToString(),
                DestinationFileName = destinationFile,
                LocationFileName = locationFile,
                Star=(float)reader["star"],
                HasInternet=(bool)reader["is_internet_have"],
                IsInternetFree=(bool)reader["is_internet_free"],
                DetailInternet=reader["detail_internet"].ToString(),
                ReviewAverate = Convert.ToDouble(reader["review_avg"]),
                ReviewCount = (int)reader["review_count"],
                PromotionID=(int)reader["promotion_id"],
                PromotionTitle=reader["promotion_title"].ToString(),
                FileName = fileName,
                CategoryID = (byte)reader["cat_id"],
                ProductCode=reader["product_code"].ToString(),
                PriceStart = (decimal)reader["priceStart"],
                 ConditionID=(int)reader["conditionID"],
                 OptionID=(int)reader["optionID"],
                 PointPopular=(byte)reader["point_popular"],
                 ImageFeature=reader["pic_feature"].ToString(),
                 ImagePopular=reader["pic_popular"].ToString(),
                 IsExtranet=(bool)reader["isextranet"]
               
                
                }
             
            );
                
            }
            myConn.Close();

            return productList;
        }

        public int CountRecord(short DestinationID, short LocationID, byte categoryID, string dateStart, string dateEnd)
        {
            int result=0;
            DataConnect myConn = new DataConnect();
            //totalRecord = myConn.ExecuteScalar(sqlCommand);
            string sqlCommand = string.Empty;
            sqlCommand = "fr_list_2 '" + DestinationID + "','" + LocationID + "','" + categoryID + "'," + dateStart + "," + dateEnd + ",1,10,1,"+LangID+",1";
            //HttpContext.Current.Response.Write(sqlCommand);
            result=myConn.ExecuteScalar(sqlCommand);
            return result;
        }

        public List<ProductList> GetSearchProductList(string keyword,int pageCurrent)
        {
            DataConnect objConn=new DataConnect();
            string sqlCommand = "fr_quick_search '"+keyword+"',"+pageCurrent+","+pageSize+",1";
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);
            productList = new List<ProductList>();

            while (reader.Read())
            {
                productList.Add(new ProductList
                {
                    ProductID = (int)reader["product_id"],
                    ProductTitle = reader["title"].ToString(),
                    DestinationID = (short)reader["destination_id"],
                    DestinationTitle = reader["destination_title"].ToString(),
                    DestinationFileName = reader["destination_filename"].ToString(),
                    LocationFileName = reader["location_filename"].ToString(),
                    LocationTitle = "",
                    Star = (float)reader["star"],
                    HasInternet = (bool)reader["is_internet_have"],
                    IsInternetFree = (bool)reader["is_internet_free"],
                    DetailInternet = reader["detail_internet"].ToString(),
                    ReviewAverate = Convert.ToDouble(reader["review_avg"]),
                    ReviewCount = (int)reader["review_count"],
                    PromotionID = (int)reader["promotion_id"],
                    PromotionTitle = reader["promotion_title"].ToString(),
                    FileName = (string)reader["file_name_main"],
                    CategoryID = (byte)reader["cat_id"],
                    ProductCode = reader["product_code"].ToString(),
                    PriceStart = (decimal)reader["priceStart"],
                    ConditionID=(int)reader["conditionID"],
                 OptionID=(int)reader["optionID"]

                }

            );

            }
            objConn.Close();

            return productList;
        }

        public string RenderProductPopular()
        {
            string result = string.Empty;

            if(productList.Count>0)
            {
            
            
            LinkGenerator link = new LinkGenerator();
            link.LoadData(LangID);

            ProductReviewLast reviews = new ProductReviewLast();
            reviews.LoadAllProductReviewLast(1);

            

            string reviewDisplay = string.Empty;
            string reviewDisplayContent = string.Empty;

            int countItem=0;
            if(LangID==1)
            {
                result = "<div id=\"popular_header\"><h2>Popular Right Now In " + productList[0].DestinationTitle + "</h2></div>  \n";
            }else{
                result = "<div id=\"popular_header\"><h2>โรงแรมยอดฮิตที่สุดใน" + productList[0].DestinationTitle + "</h2></div>  \n";
            }
            
            result=result+"<div class=\"last_viewed\"> \n";
        	result=result+"<div class=\"bg_top_colright\"></div> \n";
            result=result+"<div id=\"popular_list\"> \n";

            string rateDisplay = string.Empty;
            ProductReviewLast review;

            foreach(ProductList item in productList){
                if (countItem % 2 == 0)
                {
                    result = result + "<div id=\"last_view_list\"> \n";
                }
                else 
                {
                    result = result + "<div id=\"last_view_list_bg\"> \n";
                }

                review = reviews.GetReviewByProductID(item.ProductID);

                if (review != null)
                {
                    reviewDisplay = "<div class=\"icon_review\"></div>\n";
                    if (LangID == 1)
                    {
                        reviewDisplay = reviewDisplay + "<a href=\"#\" class=\"score_review_link\">" + review.AverageReview + " Reviews</a>\n";
                    }
                    else {
                        reviewDisplay = reviewDisplay + "<a href=\"#\" class=\"score_review_link\">" + review.AverageReview + " รีวิว</a>\n";
                    }
                    
                    reviewDisplayContent = "<div id=\"reviews\">\n";
                    //reviewDisplayContent = reviewDisplayContent + "<p>“ Nice, well maintained hotel. All London hotels are expensive, but this wasn't too bad compared to what some other hotels charge... </p></div>\n";
                    //reviewDisplayContent = reviewDisplayContent + "<div class=\"name_custom_review\">" + review.Fullname + ", flag_" + review.CountryID + " " + review.DateSubmit.ToString("MMM,dd yyyy") + "</div>\n";

                }
                else
                {
                    if (LangID == 1)
                    {
                        reviewDisplay = "Not yet review\n";
                    }
                    else {
                        reviewDisplay = "ยังไม่มีรีวิว\n";
                    }
                    
                    //reviewDisplayContent = "";
                }

                if (LangID == 1)
                {
                    result = result + "<div class=\"hotels_name\"><a href=\"" + item.FileName + "\">" + item.ProductTitle + "</a> \n";
                }
                else {
                    result = result + "<div class=\"hotels_name\"><a href=\"" + item.FileName + "\">" + item.ProductTitle + "</a> \n";
                }
                
       		    
                result=result+"<div class=\"class_star_s_"+Utility.GetHotelClassImage(item.Star,1)+"\"></div> </div><!--hotels name-->   \n";       		 
                result=result+"<br class=\"clear-all\" /> \n";
                result=result+"<img src=\"theme_color/blue/images/test/browse_p1.jpg\" /> \n";
                result=result+"<div class=\"detail_top\"> \n";
                //result = result + "<div class=\"icon_review\"></div> \n";
                //result = result + "<a href=\"#\" class=\"score_review_link\">" + reviewDisplay + " Reviews</a> \n";
                result = result + reviewDisplay;

                if (item.HasInternet)
                {
                    result = result + "<div class=\"internet\"></div>\n";
                }

                if (item.PriceStart == 0)
                {
                    rateDisplay = "N/A";
                }
                else {
                    if (currency.CurrencyID == 25)
                    {
                        rateDisplay = currency.CurrencyCode+" "+ item.PriceStart.ToString("#,###");   
                    }
                    else
                    {
                        rateDisplay = currency.CurrencyCode + " " + item.PriceStart.ToString("#,###.##");
                    }
                    
                }

                result=result+"</div><!--top--> \n";
                result=result+"<div class=\"detail_buttom\"> \n";
                if(LangID==1)
                {
                    result=result+"<div class=\"map\"><a href=\"#\">View Map</a></div> \n";
                    result = result + "<div class=\"detail_price\"><div class=\"thb\">" + rateDisplay + "</div>per night avg </div><!--price--> \n";
                    result = result + "<a href=\"" + item.FileName + "\" class=\"more\">More..</a>   \n";
                }else{
                    result=result+"<div class=\"map\"><a href=\"#\">ดูแผนที่</a></div> \n";
                    result = result + "<div class=\"detail_price\"><div class=\"thb\">" + rateDisplay + "</div>ต่อคืน</div><!--price--> \n";
                    result = result + "<a href=\"" + item.FileName + "\" class=\"more\">อ่านต่อ</a>   \n";
                }
                
                
                result=result+"</div><!--buttom--> \n";
          	    result=result+"<br class=\"clear-all\" /> \n";
                result=result+"</div><!--1--> \n";
                countItem = countItem + 1;


            }
            result=result+"</div><!--popular_list--> \n"; 
            result=result+"<div class=\"bg_down_colrigh\"></div> \n";
        
            result=result+"</div><!--popular right now -->   \n";
            if (LangID == 1)
            {
                result = result + "<div class=\"backtotop_b\"><a href=\"#\">Views All Destination</a></div> \n";
            }
            else {
                result = result + "<div class=\"backtotop_b\"><a href=\"#\">ดูโรงแรมทั้งหมด</a></div> \n";
            }
            
            }
            return result;
        }

        public string RenderAllProductDestination()
        {
            string data = string.Empty;
            //if(productList.Count>0)
            //{
            
            //int columnMax = 3;
            //int countItem = productList.Count-1;
            //int indexStart = 0;
            //int indexEnd = 0;
            //int indexCount = 0;

            //int piece = countItem % columnMax;
            //int itemCol = (int)(countItem / columnMax);
            //int itemTotal = 0;
            
            LinkGenerator link=new LinkGenerator();

            link.LoadData(LangID);

            //data = data = "<div id=\"tophotel_footer\">\n";
            //data = data + "<h3>All Hotels in " + productList[0].DestinationTitle+ "</h3>\n";
            //data = data + "</div>\n";
            //data = data + "<div id=\"content_tophotel_footer_b\">\n";
            //for (int countCol = 1; countCol <= columnMax; countCol++)
            //{
            //    data = data + "<ul class=\"all_col_" + countCol + "\">\n";
            //    if (piece != 0)
            //    {
            //        itemTotal = itemCol + 1;
            //        piece = piece - 1;
            //    }
            //    else
            //    {
            //        itemTotal = itemCol;
            //    }

            //    indexStart = indexCount;
            //    indexEnd = itemTotal + indexCount;

            //    for (int itemCount = indexStart; itemCount < indexEnd; itemCount++)
            //    {
                    
            //        data = data + "<li class=\"ball_green_b\">  <a href=\""+productList[itemCount].FileName+"\">" + productList[itemCount].ProductTitle + "</a></li>\n";
            //        indexCount = indexCount + 1;
            //    }
            //    data = data + "</ul>\n";
            //}
            //data = data + "<div class=\"clear-all\"></div>\n";
            //data = data + "</div>\n";
            //}

            int ProductTemp = 0;

            if(productList.Count!=0){
                int countItem=0;
                if (LangID==1)
                {
                    data = data + "<div id=\"tophotel_footer\"><h3>All Hotels in " + productList[0].DestinationTitle + "</h3> </div>\n";
                }else{
                    data = data + "<div id=\"tophotel_footer\"><h3>โรงแรมทั้งหมดใน " + productList[0].DestinationTitle + "</h3> </div>\n";
                }
                
     	        data = data + "<table id=\"content_tophotel_footer_b\">\n";
        	    data = data + "<tr valign=\"top\">\n";

             foreach(ProductList item in productList)
                {

                    if(item.ProductID!=ProductTemp)
                    {
                        if (countItem % 3 == 0)
                        {
                            data = data + "</tr><tr>";
                        }
                        data = data + "<td class=\"all_col\">  <a href=\""+item.FileName+"\">"+item.ProductTitle+"</a> </td>\n";
                        countItem = countItem + 1;
                    }
                    ProductTemp = item.ProductID;
                }
                
                data = data + "</tr>\n";
                data = data + "</table>\n";
                data = data + "<div id=\"bg_end_footer\"></div><br />\n";
            }
            return data;
        }

        public List<ProductList> GetProductFeature(short DestinationID, byte categoryID)
        {
            string strCondition = string.Empty;

            //string sqlCommand = "select top " + maxRecord + " p.product_id,pc.title,p.star,p.is_internet_have,10 as review,pc.file_name_main,p.cat_id,p.destination_id,p.product_code,p.point_popular,dn.title as destination_title,";
            //sqlCommand = sqlCommand + " ISNULL((";
            //sqlCommand = sqlCommand + " select MIN(spocp.rate)";
            //sqlCommand = sqlCommand + " from tbl_product sp,tbl_product_period spp,tbl_product_option spo,tbl_product_option_condition spoc,tbl_product_option_condition_price spocp";
            //sqlCommand = sqlCommand + " where sp.product_id=spp.product_id and sp.product_id=spo.product_id and spo.option_id=spoc.option_id and spoc.condition_id=spocp.condition_id";
            //sqlCommand = sqlCommand + " and spo.status=1 and spoc.status=1 and spp.date_end>GETDATE() and spo.cat_id=38 and spo.product_id=p.product_id";
            //sqlCommand = sqlCommand + " ),0) as priceStart";
            //sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn,tbl_product_location pl";
            //sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=d.destination_id and d.destination_id=dn.destination_id and pl.product_id=p.product_id and p.status=1 and d.destination_id=" + DestinationID + " and p.cat_id=" + categoryID;
            //sqlCommand = sqlCommand + " and p.flag_feature=0";
            //sqlCommand = sqlCommand + "order by p.point_popular desc";

           // HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            string sqlCommand = "fr_product_list '" + DestinationID + "','0','" + categoryID + "','','',1," + maxRecord + ",5";
            
            DataConnect myConn = new DataConnect();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);
            productList = new List<ProductList>();

            while (reader.Read())
            {
                productList.Add(new ProductList
                {
                    ProductID = (int)reader["product_id"],
                    ProductTitle = reader["title"].ToString(),
                    DestinationID = (short)reader["destination_id"],
                    DestinationTitle = reader["destination_title"].ToString(),
                    LocationTitle=reader["location_title"].ToString(),
                    DestinationFileName = reader["destination_filename"].ToString(),
                    LocationFileName = reader["location_filename"].ToString(),
                    Star = (float)reader["star"],
                    HasInternet = (bool)reader["is_internet_have"],
                    IsInternetFree = (bool)reader["is_internet_free"],
                    DetailInternet = reader["detail_internet"].ToString(),
                    ReviewAverate = Convert.ToDouble(reader["review_avg"]),
                    ReviewCount = (int)reader["review_count"],
                    PromotionID = (int)reader["promotion_id"],
                    PromotionTitle = reader["promotion_title"].ToString(),
                    FileName = (string)reader["file_name_main"],
                    CategoryID = (byte)reader["cat_id"],
                    ProductCode = reader["product_code"].ToString(),
                    PriceStart = (decimal)reader["priceStart"],
                    ConditionID = (int)reader["conditionID"],
                    OptionID = (int)reader["optionID"]
                });
                
            }
            myConn.Close();

            return productList;
        }

        public string RenderList()
        {
            LinkGenerator link = new LinkGenerator();
            link.LoadData(LangID);
            string result = string.Empty;
            int countItem = 1;
           

                //result = result + "<ul id=\"hotel_lsit\">";
            foreach (ProductList item in productList)
            {
                result = result + "<li id=\"bg_hotel_list\">\n";
                result = result + "<div class=\"bg_box_top\"></div>\n";
                result = result + "<div class=\"bg_pic\"><a href=\"\"><img src=\"" + Utility.rootPath() + "/thailand-" + Utility.GetProductType(item.CategoryID)[0, 3] + "-pic/" + item.ProductCode + "_1.jpg\" /></a></div>\n";

                result = result + "<div class=\"hotels_detail\">";
                result = result + "<a href=\"" + item.FileName + "\" class=\"hotel_name\">" + countItem + ". " + item.ProductTitle + "</a>\n";
                result = result + "<div class=\"class_star_" + Utility.GetHotelClassImage(item.Star, 1) + "\"></div>\n";
                result = result + "<br class=\"clear-all\" />\n";

                result = result + "<div class=\"icon_review\"></div>\n";
                result = result + "<div class=\"score_review_link\"><a href=\"#\">reviews</a></div>\n";
                if (item.HasInternet)
                {
                    result = result + "<div class=\"internet\"></div>\n";
                }
                result = result + "<div class=\"clear-all\"></div>\n";
                //result = result + "<div class=\"promotion\">Minimum Stay 2 Nights, Save 500 Baht /Night During on <br />1 Mar - 31 October 10.</div> ";
                result = result + "<br class=\"clear-all\" />\n";
                result = result + "<ul id=\"nearby\"><h5>Nearby Attraction :</h5>\n";
                result = result + "<li>- Doi Suthep 20km by car </li>\n";
                result = result + "<li>- Chiang Mai Zoo 20km by car</li>\n";
                result = result + "<a href=\"#\">+More</a>\n";
                result = result + "</ul>";
                result = result + "<div class=\"map\"></div>\n";
                result = result + "<a href=\"#\">+More</a>\n";
                result = result + "</div>\n";

                result = result + "<div id=\"hotels_price\"><div class=\"thb\">"+currency.CurrencyCode+"</div>\n";
                result = result + "<div class=\"price\">"+PriceStart.ToString()+"</div>\n";
                result = result + "per night avg\n";
                result = result + "<div class=\"list_booknow\"><a href=\"#\"></a></div>\n";
                result = result + "</div>\n";

                result = result + "<br class=\"clear-all\" />\n";
                result = result + "<div class=\"bg_box_down\"><a href=\"#\" class=\"show_rooms\"></a></div>\n";
                result = result + "</li>\n";
            }
            return result;
            
        }

        public string RenderItemFeature()
        {
            string result = string.Empty;
            if (productList.Count > 0)
            {
            LinkGenerator link = new LinkGenerator();
            link.LoadData(LangID);
            

            ProductReviewLast reviews = new ProductReviewLast();
            reviews.LoadAllProductReviewLast(1);

            
            
            
            result = result + "<div class=\"content_top_bg\">\n";
                if(LangID==1)
                {
                    result = result + "<div class=\"head_featured\"><h2>Featured Hotels In " + productList[0].DestinationTitle + "</h2></div>\n";
                    result = result + "<div class=\"head_landmarks\"><h2>" + productList[0].DestinationTitle + " Landmarks</h2></div>\n";
                }else{
                    result = result + "<div class=\"head_featured\"><h2>โรงแรมน่าสนใจใน" + productList[0].DestinationTitle + "</h2></div>\n";
                    result = result + "<div class=\"head_landmarks\"><h2>" + productList[0].DestinationTitle + " Landmarks</h2></div>\n";
                }
            
            result = result + "</div>\n";
  		    result = result + "<div id=\"content_left\">\n";

            
            string reviewDisplay;
            string reviewDisplayContent;

            string rateDisplay = string.Empty;
            string reviewContent;
            ProductReviewLast review;
            foreach(ProductList item in productList){
                //HttpContext.Current.Response.Write(item.PriceStart+"Hello");
                review = reviews.GetReviewByProductID(item.ProductID);
                if (review != null)
                {
                    reviewContent=review.Content;
                    if(reviewContent.Length>100)
                    {
                        reviewContent = reviewContent.Substring(0,99);
                    }
                    reviewDisplay = "<div class=\"icon_review\"></div>\n";
                    reviewDisplay=reviewDisplay+"<a href=\"#\" class=\"score_review_link\">"+review.AverageReview+" Reviews</a>\n";
                    reviewDisplayContent = "<div id=\"reviews\">\n";
                    reviewDisplayContent = reviewDisplayContent + "<p>“ "+reviewContent+"... </p></div>\n";
                    reviewDisplayContent = reviewDisplayContent + "<div class=\"name_custom_review\">" + review.Fullname + ", flag_" + review.CountryID + " " + review.DateSubmit.ToString("MMM,dd yyyy") + "</div>\n";

                }
                else {
                    reviewDisplay = "Not yet review\n";
                    reviewDisplayContent = "";
                }
                
                result = result + "<ul class=\"bg_block\">\n";
                result = result + "<div class=\"bg_block_top\"></div>\n";
                result = result + "<div id=\"images\">\n";
                result = result + "<div class=\"bg_pic\"><img src=\"" + Utility.rootPath() + "/thailand-" + Utility.GetProductType(item.CategoryID)[0, 3] + "-pic/" + item.ProductCode + "_1.jpg\" /></div>\n";
                result = result + "<div class=\"map\"></div>\n";
                result = result + "<a href=\"#\">View Map</a> \n";
                result = result + "</div><!--images-->\n";
                result = result + "<div id=\"browse_detail\">\n";
        	    result = result + "<a href=\"" + item.FileName + "\">"+item.ProductTitle+"</a>\n";
                result = result + "<div class=\"class_star_s_" + Utility.GetHotelClassImage(item.Star, 1) + "\"></div>\n";
                result = result + "<br class=\"clear-all\"/>\n";
                //result = result + "<div class=\"icon_review\"></div>\n";
                //result = result + "<a href=\"#\" class=\"score_review_link\">"+review.AverageReview+" Reviews</a>\n";
                result = result + reviewDisplay;
                if (item.HasInternet)
                {
                    result = result + "<div class=\"internet\"></div>\n";
                }
                result = result + "<br class=\"clear-all\" />\n";
                //result = result + "<ul class=\"nearby\"><h5>Nearby Attractions : </h5>\n";
                //result = result + "<li>- Big C Supermarket : 15 m by walk </li>\n";
                //result = result + "<li>- Lotus Supermarken : 5 m by walk </li>\n";
                //result = result + "<a href=\"#\">+More</a> \n";   
                //result = result + "</ul>\n";
                if (item.PriceStart == 0)
                {
                    rateDisplay = "N/A";
                }
                else
                {
                    if (currency.CurrencyID == 25)
                    {
                        rateDisplay = currency.CurrencyCode + " " + item.PriceStart.ToString("#,###");
                    }
                    else
                    {
                        rateDisplay = currency.CurrencyCode + " " + item.PriceStart.ToString("#,###.##");
                    }
                }

                result = result + "</div><!--detail-->\n";
                result = result + "<div id=\"detail_price\"><div class=\"thb\">"+rateDisplay+"</div>per night avg\n";
                result = result + "<a href=\"" + item.FileName + "\">More..</a>  \n";
                result = result + "</div><!--price-->\n";
                //result = result + "<div id=\"reviews\">\n";
                //result = result + "<p>“ Nice, well maintained hotel. All London hotels are expensive, but this wasn't too bad compared to what some other hotels charge... </p></div>\n";
                //result = result + "<div class=\"name_custom_review\">" + review.Fullname + ", flag_"+review.CountryID+" " + review.DateSubmit.ToString("MMM,dd yyyy") + "</div>\n";
                result = result + reviewDisplayContent;
                result = result + "<br class=\"clear-all\" />\n";
                result = result + "<div class=\"bg_block_down\"></div>\n";
          	    result = result + "</ul><!--1-->\n"; 
            }
            result = result + "</div>";
            }
            return result;
        }

        public string RenderPopular(ProductList item,ProductDetail detail,int RowBg)
        {
            LinkGenerator link = new LinkGenerator();
            link.LoadData(LangID);
            string rateDisplay = string.Empty;

            string result = string.Empty;
            if (RowBg==1)
            {
                result= "<div id=\"last_view_list\">\n";
            }else{
                result= "<div id=\"last_view_list_bg\">\n";
            }

            result = result + "<div class=\"hotels_name\"><a href=\"" + link.GetProductPath(detail.DestinationID, detail.CategoryID, detail.FileMain) + "\">" + item.ProductTitle + "</a>\n";
            result = result + "<div class=\"class_star_s_" + Utility.GetHotelClassImage(item.Star, 1) + "\"></div> </div><!--hotels name-->\n";        		 
            result=result+"<br class=\"clear-all\" />\n";
            result=result+"<img src=\"theme_color/blue/images/test/browse_p1.jpg\" />\n";
            result=result+"<div class=\"detail_top\">\n";
            result=result+"<div class=\"icon_review\"></div>\n";
            result=result+"<a href=\"#\" class=\"score_review_link\">32 Reviews</a>\n";
            if (item.HasInternet)
            {
                result = result + "<div class=\"internet\"></div>\n";
            }

            if (item.PriceStart == 0)
            {
                rateDisplay = "N/A";
            }
            else
            {
                rateDisplay = "THB " + string.Format("{0:0,0}", item.PriceStart);
            }
            result=result+"</div><!--top-->\n";
            result=result+"<div class=\"detail_buttom\">\n";
            result=result+"<div class=\"map\"><a href=\"#\">View Map</a></div>\n";
            result = result + "<div class=\"detail_price\"><div class=\"thb\">" + rateDisplay + "</div>per night avg </div><!--price-->\n";
            result = result + "<a href=\"" + link.GetProductPath(detail.DestinationID, detail.CategoryID, detail.FileMain) + "\" class=\"more\">More..</a>\n";  
            result=result+"</div><!--buttom-->\n";
            result=result+"<br class=\"clear-all\" />\n";
            result=result+"</div>\n";
            return result;
        }


        public string RenderListNew()
        {
            string itemList=string.Empty;
            LinkGenerator link = new LinkGenerator();
            link.LoadData(LangID);

            string fileCurrent = HttpContext.Current.Request.UrlReferrer.ToString().Split('?')[0];
            PageNavigator pn = new PageNavigator(totalRecord, pageSize, 1, fileCurrent);

            itemList = itemList + "<div class=\"total_hotel\"><h4>" + totalRecord + " " + Utility.GetProductType(productList[0].CategoryID)[0, 1] + " in " + productList[0].LocationTitle + "</h4></div>\n";
            itemList = itemList + "<div class=\"pre_next\">\n";
            itemList = itemList + pn.DisplayPage();
            itemList = itemList + "</div><!--pre_next--> \n";
            itemList = itemList + "<br class=\"clear-all\" /><!--pim-->\n";

            
            ProductReviewLast reviews = new ProductReviewLast();
            reviews.LoadAllProductReviewLast(1);
            ProductReviewLast review;
            int countItem = 1;
            string reviewDisplay = string.Empty;
            string rateDisplay = string.Empty;

            foreach (ProductList item in productList)
            {
                review = reviews.GetReviewByProductID(item.ProductID);
                if (review != null)
                {

                    reviewDisplay = "<div class=\"icon_review\"></div>\n";
                    reviewDisplay = reviewDisplay + "<div class=\"score_review_link\"><a href=\"#\">"+review.CountReview+" reviews</a></div>\n";
                }
                else {
                    reviewDisplay = "Not yet review\n";
                }

                if (item.PriceStart == 0)
                {
                    rateDisplay = "<div id=\"hotels_price\"><div class=\"thb\">"+currency.CurrencyCode+"</div>\n";
                    rateDisplay = rateDisplay + "<div class=\"price\">N/A</div>\n";
                }
                else
                {
                    if (currency.CurrencyID == 25)
                    {
                        rateDisplay = "<div id=\"hotels_price\"><div class=\"thb\">"+currency.CurrencyCode+"</div>\n";
                        rateDisplay = rateDisplay + "<div class=\"price\">" + item.PriceStart.ToString("#,###") + "</div>\n";
                    }
                    else
                    {
                        rateDisplay = "<div id=\"hotels_price\"><div class=\"thb\">" + currency.CurrencyCode + "</div>\n";
                        rateDisplay = rateDisplay + "<div class=\"price\">" + item.PriceStart.ToString("#,###.##") + "</div>\n";
                    }
                    
                }

                    itemList = itemList + "<li id=\"bg_hotel_list\">\n";
         	        itemList=itemList+"<div class=\"bg_box_top\"></div> \n";
             		itemList=itemList+"<div class=\"bg_pic_old\"><img src=\"http://www.hotels2thailand.com/thailand-"+Utility.GetProductType(item.CategoryID)[0,3]+"-pic/"+item.ProductCode+"_1.jpg\" /></div>\n";
                    itemList=itemList+"<div class=\"hotels_detail_old\">\n";
                    itemList = itemList + "<a href=\"" + item.FileName + "\" class=\"hotel_name\">"+countItem+". " + item.ProductTitle + "</a>\n";
                    itemList = itemList + "<div class=\"class_star_s_" + Utility.GetHotelClassImage(item.Star, 1) + "\"></div>\n";
                    itemList=itemList+"<br class=\"clear-all\" />\n";
                    //itemList=itemList+"<div class=\"icon_review\"></div>\n";
                    //itemList=itemList+"<div class=\"score_review_link\"><a href=\"#\">reviews</a></div>\n";
                    itemList = itemList + reviewDisplay;

                    if(item.HasInternet){
                        itemList=itemList+"<div class=\"internet\"></div>\n";
                    }
                    itemList=itemList+"<div class=\"clear-all\"></div>\n";
                    //itemList=itemList+"<div class=\"promotion_old\">Minimum Stay 2 Nights, Save 500 Baht /Night During on <br />1 Mar - 31 October 10.</div>\n";                    
                    itemList=itemList+"<br class=\"clear-all\" />\n";
          			itemList=itemList+"<ul id=\"nearby\">";
                    //itemList = itemList + "<h5>Nearby Attraction :</h5>\n";
            		//itemList=itemList+"<li>- Doi Suthep 20km by car </li>\n";
               		//itemList=itemList+"<li>- Chiang Mai Zoo 20km by car</li>\n";
                    itemList=itemList+"<a href=\"#\">+More</a> \n";   
           			itemList=itemList+"</ul>\n";
                    itemList=itemList+"<div class=\"map\"></div>\n";
                    itemList=itemList+"<a href=\"#\" class=\"list_map\">View Map</a>\n";
                    itemList=itemList+"</div>\n";
                    //itemList=itemList+"<div id=\"hotels_price\"><div class=\"thb\">THB</div>\n";
                	//itemList=itemList+"<div class=\"price\">4,460</div>\n";
                    itemList = itemList + rateDisplay;
                    itemList=itemList+"per night avg\n";
                    itemList = itemList + "<div class=\"list_booknow\"><a href=\"" + item.FileName + "\" class=\"hotel_name\"></a></div>\n";
                    itemList=itemList+"</div>\n";
                    itemList=itemList+"<br class=\"clear-all\" />\n";
         	        itemList=itemList+"<div class=\"bg_box_down\"><a href=\"#\" class=\"show_rooms\"></a></div>\n";	                                                     
       	            itemList=itemList+"</li>\n";
                    countItem = countItem + 1;
            }
            return itemList;
        }

        public string RenderPopularDestination(short destinationID,byte category)
        {
            //LinkGenerator link = new LinkGenerator();
            //link.LoadData();
            string sqlCommand = string.Empty;
            string popularList = "";


            DataConnect connect = new DataConnect();
            SqlDataReader reader;
            string destinationTemp = string.Empty;



            //sqlCommand = "select top 4 pc.title,p.star,pc.file_name_main,dn.title as destination_title,p.product_code,";
            //sqlCommand = sqlCommand + "(select top 1 spl.product_id  from tbl_product_location spl where spl.product_id=p.product_id)as location_id,";
            //sqlCommand = sqlCommand + " (";
            //sqlCommand = sqlCommand + " select top 1 pic_code from tbl_product_pic spp where spp.product_id=p.product_id and spp.cat_id=1";
            //sqlCommand = sqlCommand + " )";
            //sqlCommand = sqlCommand + " from tbl_product p,tbl_destination d,tbl_destination_name dn,tbl_product_content pc";
            //sqlCommand = sqlCommand + " where p.destination_id=d.destination_id and d.destination_id=dn.destination_id and p.product_id=pc.product_id and pc.lang_id=1 and p.cat_id=" + category + " and p.destination_id=" + destinationID;
            //sqlCommand = sqlCommand + " and p.status=1";

            sqlCommand = "select top 5 p.title as product_title,pc.title,p.star,d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main as file_name_main,dn.title as destination_title,p.product_code,";
            sqlCommand = sqlCommand + " (select top 1  spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id="+LangID+") as second_lang,";
            sqlCommand = sqlCommand + " (select top 1 spl.product_id  from tbl_product_location spl where spl.product_id=p.product_id)as location_id,";
            sqlCommand = sqlCommand + " (select top 1 spp.pic_code from tbl_product_pic spp where spp.product_id=p.product_id and spp.cat_id=1 and spp.type_id=7 and spp.status=1) as picture";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_destination d,tbl_destination_name dn,tbl_product_content pc,tbl_product_category pcat";
            sqlCommand = sqlCommand + " where p.destination_id=d.destination_id and d.destination_id=dn.destination_id and p.cat_id=pcat.cat_id and p.product_id=pc.product_id and dn.lang_id=" + LangID + " and pc.lang_id=1 and p.cat_id=" + category + " and p.destination_id=" + destinationID;
            sqlCommand = sqlCommand + " and p.status=1";
            sqlCommand = sqlCommand + " order by point_popular desc";
            //HttpContext.Current.Response.Write(sqlCommand+"hellO");
            //HttpContext.Current.Response.Flush();
            reader = connect.GetDataReader(sqlCommand);
            int countItem = 0;
            string imagePath = string.Empty;

            while (reader.Read())
            {
                if (reader["destination_title"].ToString() != destinationTemp)
                {

                    destinationTemp = reader["destination_title"].ToString();
                    //popularList = popularList + "<div class=\"head_warp\"></div>\n";
	                //popularList = popularList + "<div id=\"head_pop\">\n";
                    //popularList = popularList + "<div class=\"title\">Popular Destination</div> <div class=\"txt\">Most Popular Hotels in " + destinationTemp + "</div> </div>\n";
                    popularList = popularList + "<div class=\"head_warp\"></div>\n";
	                if(LangID==1)
                    {
                        popularList = popularList + "<div id=\"head_pop\"> <h3>Most popular <br/>" + Utility.GetProductType(category)[0, 1].ToLower() + " <br /> in " + destinationTemp + "</h3> </div>\n";
                    }else{
                        popularList = popularList + "<div id=\"head_pop\"> <h3>" + Utility.GetProductType(category)[0, 4].ToLower() + "ยอดฮิตใน<br />" + destinationTemp + "</h3> </div>\n";
                        //switch(category)
                        //{
                        //    case 29:
                        //    popularList = popularList + "<div id=\"head_pop\"> <h3>โรงแรมยอดฮิตใน<br /> " + destinationTemp + "</h3> </div>\n";
                        //    break;
                        //    case 32:
                        //    popularList = popularList + "<div id=\"head_pop\"> <h3>สนามกอล์ฟยอตฮิตใน<br/> " + destinationTemp + "</h3> </div>\n";
                        //    break;
                        //    case 34:
                        //    popularList = popularList + "<div id=\"head_pop\"> <h3>กิจกรรทท่องเที่ยวยอดฮิตใน<br/>" + destinationTemp + "</h3> </div>\n";
                        //    break;
                        //    case 36:
                        //    popularList = popularList + "<div id=\"head_pop\"> <h3>กิจกรรมทางน้ำยอดฮิตใน<br/>" + destinationTemp + "</h3> </div>\n";
                        //    break;
                        //    case 38:
                        //    popularList = popularList + "<div id=\"head_pop\"> <h3>การแสดงโชว์ยอดฮิตใน<br/>" + destinationTemp + "</h3> </div>\n";
                        //    break;
                        //    case 39:
                        //    popularList = popularList + "<div id=\"head_pop\"> <h3>สถานที่ตรวจสุขภาพยอดฮิตใน<br/>" + destinationTemp + "</h3> </div>\n";
                        //    break;
                        //    case 40:
                        //    popularList = popularList + "<div id=\"head_pop\"> <h3>สปายอดฮิตใน<br/>" + destinationTemp + "</h3> </div>\n";
                        //    break;
                        //}
                        
                    }
                    
                    popularList = popularList + "<ul class=\"end_pop_location\">\n";
                }

                if (!string.IsNullOrEmpty(reader["picture"].ToString()))
                {
                    imagePath = "<img src=\"" + reader["picture"] + "\">";
                }
                else
                {
                    imagePath = "<img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(category)[0, 3] + "-pic/" + reader["product_code"] + "_1.jpg\" alt=\"" + reader["product_title"] + "\" width=\"45\" height=\"40\" />";
                    //imagePath = "<img src=\"/pictures/untitle_45_40.jpg\">";
                }
                if(countItem % 2==0){
                    
                    popularList = popularList + "<li>"+imagePath+"\n";
                    
                }else{
                    popularList = popularList + "<li class=\"bg_pop\">"+imagePath+"\n";
                    //popularList = popularList + "<li class=\"bg_pop\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(category)[0, 3] + "-pic/" + reader["product_code"] + "_b_1.jpg\" />\n";
                }
                //popularList = popularList + "<h4><a href=\"" + link.GetProductPath(destinationID, category, reader["file_name_main"].ToString()) + "\">" + reader["title"] + "</a></h4>\n";
                //popularList = popularList + "<div class=\"class_star_s_" + Utility.GetHotelClassImage((float)reader["star"], 1) + "\"></div>\n";
                //popularList = popularList + "<div class=\"clear-all\"></div>\n";
                //popularList = popularList + "</li> \n";
                if(LangID==1)
                {
                    popularList = popularList + "<h4><a href=\"/" + reader["file_name_main"] + "\">" + reader["title"] + "</a></h4>\n";
                }else{
                    if (string.IsNullOrEmpty(reader["second_lang"].ToString()))
                    {
                        popularList = popularList + "<h4><a href=\"/" + reader["file_name_main"].ToString().Replace(".asp", "-th.asp") + "\">" + reader["title"] + "</a></h4>\n";
                    }
                    else {
                        popularList = popularList + "<h4><a href=\"/" + reader["file_name_main"].ToString().Replace(".asp", "-th.asp") + "\">" + reader["second_lang"] + "</a></h4>\n";
                    }
                    
                }

                

                if(category==29)
                {
                popularList = popularList + "<div class=\"class_star_s_" + Utility.GetHotelClassImage((float)reader["star"], 1) + "\"></div>\n";
                }
                popularList = popularList + "<div class=\"clear-all\"></div>\n";
                popularList = popularList + "</li>";
                countItem = countItem + 1;
            }
            popularList = popularList + "</ul>";
            connect.Close();
            return popularList;
        }

    }
}