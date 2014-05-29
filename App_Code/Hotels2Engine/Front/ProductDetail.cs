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
    public class ProductDetail
    {
        public int ProductID { get; set; }
        
        public string Title { get; set; }
        public string Address { get; set; }
        public short DestinationID { get; set; }
        public string DestinationTitle { get; set; }
        public short LocationID { get; set; }
        public string LocationTitle { get; set; }
        public byte PaymentTypeID { get; set; }
        public byte CategoryID { get; set; }
        public string ProductCode { get; set; }
        public string Detail { get; set; }
        public float Star { get; set; }
        public string FileMain { get; set; }
        public string FileInfo { get; set; }
        public string FilePhoto { get; set; }
        public string FilePDF { get; set; }
        public string FileReview { get; set; }
        public string FileWhy { get; set; }
        public string FileContact { get; set; }
        public string FolderDestination { get; set; }
        public bool IsInternetFree { get; set; }
        public bool HasInternet { get; set; }
        public string InternetDetail { get; set; }
        public byte NumPic { get; set; }
        public bool IsNewPic { get; set; }
       
        private byte _categoryID;

        public ProductDetail()
        { 
        
        }
        public ProductDetail(int productID)
        {
            ProductID = productID;
        }
        public ProductDetail GetProductByID(int productID, int langID)
        {
            DataConnect myConn = new DataConnect();

            string sqlCommand = "select  p.product_id,p.product_code,p.cat_id,pc.title,pc.detail,p.destination_id,p.payment_type_id,pc.address,p.star,p.is_internet_free,p.is_internet_have,pc.detail_internet,pc.file_name_main,pc.file_name_information,pc.file_name_photo,pc.file_name_pdf,pc.file_name_review,pc.file_name_why,pc.file_name_contact,p.is_internet_have,p.num_pic,p.is_new_pic,d.folder_destination,";
            sqlCommand = sqlCommand + " isnull((";
            sqlCommand = sqlCommand + " select top 1 location_id from tbl_product_location spl where spl.product_id=p.product_id";
            sqlCommand = sqlCommand + " ),'1') as location_id";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d";
            sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=d.destination_id  and pc.lang_id=" + langID + " and p.product_id=" + productID;
            //sqlCommand = sqlCommand + " and p.status=1";
            //HttpContext.Current.Response.Write(sqlCommand + "<br>");
            //HttpContext.Current.Response.Flush();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);
            //ProductDetail Item = new ProductDetail();
            //List<ProductDetail> Items = new List<ProductDetail>();

            //while (reader.Read())
            //{
            //    Items.Add(new ProductDetail
            //    {
            //        ProductID = (int)reader["product_id"],
            //        Title = reader["title"].ToString(),
            //        Detail = reader["detail"].ToString(),
            //        Address = reader["address"].ToString(),
            //        DestinationID = (short)reader["destination_id"],
            //        LocationID = (short)reader["location_id"],
            //        PaymentTypeID = (byte)reader["payment_type_id"],
            //        CategoryID = (byte)reader["cat_id"],
            //        Star = (float)reader["star"],
            //        FileMain = reader["file_name_main"].ToString(),
            //        FileInfo = reader["file_name_information"].ToString(),
            //        FilePhoto = reader["file_name_photo"].ToString(),
            //        FilePDF = reader["file_name_pdf"].ToString(),
            //        FileReview = reader["file_name_review"].ToString(),
            //        FileWhy = reader["file_name_why"].ToString(),
            //        FileContact = reader["file_name_contact"].ToString(),
            //        FolderDestination=reader["folder_destination"].ToString(),
            //        ProductCode = reader["product_code"].ToString(),
            //        HasInternet = (bool)reader["is_internet_have"],
            //        NumPic = (byte)reader["num_pic"],
            //        IsNewPic = (bool)reader["is_new_pic"]
            //    });
            //}
            if(reader.Read()){
                    this.ProductID = (int)reader["product_id"];
                    this.Title = reader["title"].ToString();
                    this.Detail = reader["detail"].ToString();
                    this.Address = reader["address"].ToString();
                    this.DestinationID = (short)reader["destination_id"];
                    this.LocationID = (short)reader["location_id"];
                    this.PaymentTypeID = (byte)reader["payment_type_id"];
                    this.CategoryID = (byte)reader["cat_id"];
                    this.Star = (float)reader["star"];
                    this.FileMain = reader["file_name_main"].ToString();
                    this.FileInfo = reader["file_name_information"].ToString();
                    this.FilePhoto = reader["file_name_photo"].ToString();
                    this.FilePDF = reader["file_name_pdf"].ToString();
                    this.FileReview = reader["file_name_review"].ToString();
                    this.FileWhy = reader["file_name_why"].ToString();
                    this.FileContact = reader["file_name_contact"].ToString();
                    this.FolderDestination = reader["folder_destination"].ToString();
                    this.ProductCode = reader["product_code"].ToString();
                    this.IsInternetFree=(bool)reader["is_internet_free"];
                    this.HasInternet = (bool)reader["is_internet_have"];
                    this.InternetDetail=reader["detail_internet"].ToString();
                    this.NumPic = (byte)reader["num_pic"];
                    this.IsNewPic = (bool)reader["is_new_pic"];
            }
            myConn.Close();
            return this;
        }

        public List<ProductDetail> GetProductList(short destinationID,byte categoryID)
        {
            DataConnect myConn = new DataConnect();

            //string sqlCommand = "select  p.product_id,p.product_code,p.cat_id,pc.title,pc.detail,p.destination_id,p.payment_type_id,pc.address,p.star,p.is_internet_have,pc.file_name_main,pc.file_name_information,pc.file_name_photo,pc.file_name_pdf,pc.file_name_review,pc.file_name_why,pc.file_name_contact,p.is_internet_have,p.num_pic,p.is_new_pic,d.folder_destination,";
            //sqlCommand = sqlCommand + " isnull((";
            //sqlCommand = sqlCommand + " select top 1 location_id from tbl_product_location spl where spl.product_id=p.product_id";
            //sqlCommand = sqlCommand + " ),'1') as location_id";
            //sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d";
            //sqlCommand = sqlCommand + " where p.product_id=pc.product_id  and p.destination_id=d.destination_id and pc.lang_id=1 and p.status=1 and p.cat_id=" + categoryID + " and p.destination_id=" + destinationID;

            //if (categoryID == 29)
            //{
            //    sqlCommand = sqlCommand + " and isnull((";
            //    sqlCommand = sqlCommand + " select top 1 location_id from tbl_product_location spl where spl.product_id=p.product_id";
            //    sqlCommand = sqlCommand + " ),'1')>1";
            //}

            string sqlCommand = "select  p.product_id,p.product_code,p.cat_id,pc.title,pc.detail,p.destination_id,p.payment_type_id,pc.address,p.star,p.is_internet_free,p.is_internet_have,pc.detail_internet,pc.file_name_main,pc.file_name_information,pc.file_name_photo,pc.file_name_pdf,pc.file_name_review,pc.file_name_why,pc.file_name_contact,p.is_internet_have,p.num_pic,p.is_new_pic,d.folder_destination,d.title as destination_title,isnull(pl.location_id,0) as location_id,isnull(l.title,'') as location_title";
            sqlCommand = sqlCommand + " from tbl_product p left outer join tbl_product_location pl on p.product_id = pl.product_id LEFT OUTER JOIN tbl_location l on pl.location_id = l.location_id,tbl_product_content pc,tbl_destination d";
            sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=d.destination_id and pc.lang_id=1 and p.cat_id=" + categoryID + " and p.status=1 and p.destination_id=" + destinationID;

            //sqlCommand = sqlCommand + " and pl.location_id=null";
            if (categoryID == 29)
            {
                sqlCommand = sqlCommand + " and pl.location_id is not null";
            }

            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.End();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);

            List<ProductDetail> Items=new List<ProductDetail>();

            while(reader.Read())
            {
                Items.Add(new ProductDetail 
                {
                    ProductID = (int)reader["product_id"],
                    Title = reader["title"].ToString(),
                    DestinationTitle=reader["destination_title"].ToString(),
                    Detail=reader["detail"].ToString(),
                    Address = reader["address"].ToString(),
                    DestinationID = (short)reader["destination_id"],
                    LocationID = (short)reader["location_id"],
                    LocationTitle=reader["location_title"].ToString(),
                    PaymentTypeID = (byte)reader["payment_type_id"],
                    CategoryID = (byte)reader["cat_id"],
                    Star = (float)reader["star"],
                    FileMain = reader["file_name_main"].ToString(),
                    FileInfo = reader["file_name_information"].ToString(),
                    FilePhoto = reader["file_name_photo"].ToString(),
                    FilePDF = reader["file_name_pdf"].ToString(),
                    FileReview = reader["file_name_review"].ToString(),
                    FileWhy = reader["file_name_why"].ToString(),
                    FileContact=reader["file_name_contact"].ToString(),
                    FolderDestination = reader["folder_destination"].ToString(),
                    ProductCode=reader["product_code"].ToString(),
                    IsInternetFree = (bool)reader["is_internet_free"],
                    HasInternet=(bool)reader["is_internet_have"],
                    InternetDetail=reader["detail_internet"].ToString(),
                    NumPic=(byte)reader["num_pic"],
                    IsNewPic=(bool)reader["is_new_pic"]
                });
            }
            
            myConn.Close();
            return Items;
        }

        public void Load()
        {
            DataConnect myConn = new DataConnect();

            string sqlCommand = "select  p.product_id,p.product_code,p.cat_id,pc.title,p.destination_id,p.payment_type_id,pc.address,p.star,p.is_internet_have,p.is_internet_free,pc.detail_internet,pc.file_name_main,pc.file_name_information,pc.file_name_photo,pc.file_name_pdf,pc.file_name_review,pc.file_name_why,pc.file_name_contact,p.num_pic,p.is_new_pic,d.folder_destination,";
            sqlCommand=sqlCommand+" (";
            sqlCommand=sqlCommand+" select top 1 location_id from tbl_product_location spl where spl.product_id=p.product_id";
            sqlCommand=sqlCommand+" )as location_id";
            sqlCommand=sqlCommand+" from tbl_product p,tbl_product_content pc,tbl_destination d";
            sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.product_id=" + ProductID + " and p.destination_id=d.destination_id and pc.lang_id=1";

            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.End();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);

            if (reader.Read())
            {
                ProductID = (int)reader["product_id"];
                Title = reader["title"].ToString();
                Detail = reader["detail"].ToString();
                Address = reader["address"].ToString();
                DestinationID = (short)reader["destination_id"];
                LocationID=(short)reader["location_id"];
                PaymentTypeID = (byte)reader["payment_type_id"];
                CategoryID = (byte)reader["cat_id"];
                Star = (float)reader["star"];
                FileMain = reader["file_name_main"].ToString();
                FileInfo = reader["file_name_information"].ToString();
                FilePhoto = reader["file_name_photo"].ToString();
                FilePDF = reader["file_name_pdf"].ToString();
                FileReview = reader["file_name_review"].ToString();
                FileWhy = reader["file_name_why"].ToString();
                FileContact=reader["file_name_contact"].ToString();
                FolderDestination = reader["folder_destination"].ToString();
                ProductCode=reader["product_code"].ToString();
                HasInternet = (bool)reader["is_internet_have"];
                IsInternetFree=(bool)reader["is_internet_free"];
                InternetDetail=reader["detail_internet"].ToString();
                NumPic = (byte)reader["num_pic"];
                IsNewPic=(bool)reader["is_new_pic"];
            }
            else {
                HttpContext.Current.Response.Write("No Data");
            }
            myConn.Close();
        }

        public List<ProductDetail> LoadAll()
        {
            return new List<ProductDetail>();
        }

        public string RenderProductDetail() {
            
            string productDisplay = "<div id=\"intro_rate\">";
            productDisplay = productDisplay + "<div id=\"title\">";
    		productDisplay = productDisplay+"<div class=\"title_hotels\"><h2>"+Title+"</h2></div>";
       		productDisplay = productDisplay+"<div class=\"class_star\"></div>";                  
       		productDisplay = productDisplay+"</div>"; 
            
            productDisplay = productDisplay+"<div class=\"internet\"><p>Internet</p></div>";
        	productDisplay = productDisplay+"<div class=\"map\"><a href=\"#\">View Map</a></div>";
            
            productDisplay = productDisplay+"<div class=\"clear-all\"></div>";   
     	    productDisplay = productDisplay+"<div class=\"address\">132 Loykroh Road, Chang Klan, Muang, Chiang Mai 50100, Thailand</div>";       
      	    productDisplay = productDisplay+"<div class=\"icon_paylater\"><a href=\"#\"><img src=\"theme_color/blue/images/imgaes_rate/overview.jpg\"title=\"---\"/></a></div>";
            
            productDisplay = productDisplay+"<div class=\"review_score\">";
        	productDisplay = productDisplay+"<div class=\"score_title\"><h3>Superb,9.3</h3></div>";        	
            productDisplay = productDisplay+"<div class=\"icon_review\"></div>";
            
            productDisplay = productDisplay+"<div class=\"clear-all\"></div>";   
            productDisplay = productDisplay+"<p class=\"score_review_link\">Score from <a href=\"#\">123 reviews</a> </p>";  
            
         	productDisplay = productDisplay+"<div id=\"nearby\"><h5>Nearby Attraction :</h5>";
          	productDisplay = productDisplay+"<ul>";
            productDisplay = productDisplay+"<li>- Doi Suthep 20km by car </li>";
            productDisplay = productDisplay+"<li>- Chiang Mai Zoo 20km by car</li>";
            productDisplay = productDisplay+"<li>- Doi Suthep 20km by car</li>";           
           	productDisplay = productDisplay+"</ul>";
            productDisplay = productDisplay+"<a href=\"#\">+More</a>";
         	productDisplay = productDisplay+"</div>";       
            productDisplay = productDisplay+"</div>";
        
            productDisplay = productDisplay+"<div id=\"review_bg\">";
        	productDisplay = productDisplay+"<div class=\"review_tex\">“ Nice, well maintained hotel. All London hotels are expensive, but this wasn't too bad compared to what some other hotels charge. I would stay there again,... </div>";
            productDisplay = productDisplay+"<div class=\"name_custom_review\">Brian, Singapore June 9,2010</div>";        
            productDisplay = productDisplay+"</div>";
        
            productDisplay = productDisplay+"<div id=\"pic_small_fac\">";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(CategoryID)[0, 3] + "-pic/" + ProductCode + "_1.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(CategoryID)[0, 3] + "-pic/HBK452_2.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(CategoryID)[0, 3] + "-pic/HBK452_3.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(CategoryID)[0, 3] + "-pic/HBK452_4.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(CategoryID)[0, 3] + "-pic/HBK452_5.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(CategoryID)[0, 3] + "-pic/HBK452_6.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(CategoryID)[0, 3] + "-pic/HBK452_7.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType(CategoryID)[0, 3] + "-pic/HBK452_8.jpg\" /></a>";
        
            productDisplay = productDisplay+"</div>"; 
 
   	        productDisplay = productDisplay+"<div class=\"clear-all\"></div>";
   	        productDisplay = productDisplay+"</div>";
                return productDisplay;
        }

        public string RenderProductDetailNew() {
            string productDisplay = "<div id=\"intro_rate\">";
            productDisplay = productDisplay + "<div id=\"title\">";
            productDisplay = productDisplay + "<div class=\"title_hotels\"><h2>" + Title + "</h2></div>";
            productDisplay = productDisplay + "<div class=\"class_star\"></div>";
            productDisplay = productDisplay + "</div>";

            productDisplay = productDisplay + "<div class=\"internet\"><p>Internet</p></div>";
            productDisplay = productDisplay + "<div class=\"map\"><a href=\"#\">View Map</a></div>";

            productDisplay = productDisplay + "<div class=\"clear-all\"></div>";
            productDisplay = productDisplay + "<div class=\"address\">"+Address+"</div>";
            productDisplay = productDisplay + "<div class=\"icon_paylater\"><a href=\"#\"><img src=\"theme_color/blue/images/imgaes_rate/overview.jpg\"title=\"---\"/></a></div>";

            productDisplay = productDisplay + "<div class=\"review_score\">";
            productDisplay = productDisplay + "<div class=\"score_title\"><h3>Superb,9.3</h3></div>";
            productDisplay = productDisplay + "<div class=\"icon_review\"></div>";

            productDisplay = productDisplay + "<div class=\"clear-all\"></div>";
            productDisplay = productDisplay + "<p class=\"score_review_link\">Score from <a href=\"#\">123 reviews</a> </p>";

            productDisplay = productDisplay + "<div id=\"nearby\"><h5>Nearby Attraction :</h5>";
            productDisplay = productDisplay + "<ul>";
            productDisplay = productDisplay + "<li>- Doi Suthep 20km by car </li>";
            productDisplay = productDisplay + "<li>- Chiang Mai Zoo 20km by car</li>";
            productDisplay = productDisplay + "<li>- Doi Suthep 20km by car</li>";
            productDisplay = productDisplay + "</ul>";
            productDisplay = productDisplay + "<a href=\"#\">+More</a>";
            productDisplay = productDisplay + "</div>";
            productDisplay = productDisplay + "</div>";

            productDisplay = productDisplay + "<div id=\"review_bg\">";
            productDisplay = productDisplay + "<div class=\"review_tex\">“ Nice, well maintained hotel. All London hotels are expensive, but this wasn't too bad compared to what some other hotels charge. I would stay there again,... </div>";
            productDisplay = productDisplay + "<div class=\"name_custom_review\">Brian, Singapore June 9,2010</div>";
            productDisplay = productDisplay + "</div>";

            productDisplay = productDisplay + "<div id=\"pic_small_fac\">";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/" + ProductCode + "_1.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/HBK452_2.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/HBK452_3.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/HBK452_4.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/HBK452_5.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/HBK452_6.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/HBK452_7.jpg\" /></a>";
            productDisplay = productDisplay + "<a href=\"#\"><img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/HBK452_8.jpg\" /></a>";

            productDisplay = productDisplay + "</div>";

            productDisplay = productDisplay + "<div class=\"clear-all\"></div>";
            productDisplay = productDisplay + "</div>";
            return productDisplay;
        }
        public string RenderTab(int tabActive)
        { 
            string displayTab="<div id=\"menu_rate\">";       
            displayTab=displayTab+"<a href=\""+FileMain+"\" class=\"booknow\">Book Now</a>";
            displayTab=displayTab+"<a href=\""+FileInfo+"\" class=\"information\">Hotel Information</a>";
            displayTab=displayTab+"<a href=\""+FilePhoto+"\" class=\"gall\">Photo Gallery</a>";
            displayTab=displayTab+"<a href=\""+FileReview+"\" class=\"review\">Traveler Reviews</a>";
            displayTab=displayTab+"<a href=\""+FileWhy+"\" class=\"why\">Why Us?</a>";
            displayTab=displayTab+"<a href=\"#\" class=\"end\">Contact Us</a>";
            displayTab = displayTab + "</div>";
            return displayTab;
        }
    }
}