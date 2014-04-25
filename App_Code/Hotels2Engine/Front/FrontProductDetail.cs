using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontProductDetail
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontProductDetail:Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string TitleDefault { get; set; }
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
        public double AverageReview { get; set; }
        public string Thumbnail { get; set; }
        public string Picture { get; set; }
        public string RateLowest { get; set; }
        public string Promotion { get; set; }
        public byte langID { get; set; }
        public int NumHotel { get; set; }
        public int NumProductOther { get; set; }
        public bool IsExtranet { get; set; }

        public FrontProductDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public FrontProductDetail(int ProductID, byte CategoryID, byte langID)
        {
            GetProductDetailByID(ProductID, CategoryID, langID);
        }

        public FrontProductDetail GetProductDetailByID(int ProductID,byte CategoryID, byte langID)
        {
           
            string subfixFile = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select count(product_id) as count_item from tbl_product_content where product_id="+ProductID+" and lang_id="+langID;
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                int countItem = (int)cmd.ExecuteScalar();
                if (countItem == 0)
                {
                    switch(langID)
                    {
                        case 1:
                            subfixFile = string.Empty;
                            break;
                        case 2:
                            subfixFile = "-th.asp";
                            break;
                    }
                    langID = 1; 
                }
                
            }

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select  p.product_id,p.product_code,p.cat_id,pc.title,";
                sqlCommand = sqlCommand + " (select top 1 spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + langID + ") as second_lang,";
                sqlCommand = sqlCommand + " pc.detail,";
                sqlCommand = sqlCommand + " (select top 1 spc.detail from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + langID + ") as second_lang_detail,";
                sqlCommand = sqlCommand + " p.destination_id,p.payment_type_id,pc.address,";
                sqlCommand = sqlCommand + " (select top 1 spc.address from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + langID + ") as second_lang_address,";
                sqlCommand = sqlCommand + " p.star,p.is_internet_free,p.is_internet_have,pc.detail_internet,d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main as file_name_main,pc.file_name_information,pc.file_name_photo,pc.file_name_pdf,pc.file_name_review,pc.file_name_why,pc.file_name_contact,p.is_internet_have,p.num_pic,p.is_new_pic,d.folder_destination,dn.title as destination_title,isnull(pl.location_id,0) as location_id,isnull(l.title,'') as location_title,isextranet,extranet_active";

                switch (CategoryID)
                {
                    case 29:
                        sqlCommand = sqlCommand + " ,isnull((select (sum(srh.rate_overall)+sum(srh.rate_service)+sum(srh.rate_location)+sum(srh.rate_room)+sum(srh.rate_clean)+sum(srh.rate_money))/cast(6 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as avg_review";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocp.rate/1.177 AS money) FROM tbl_product_option_condition_price spocp, tbl_product_option_condition spoc, tbl_product_option spo, tbl_product_period spp WHERE spp.period_id=spocp.period_id AND spo.option_id=spoc.option_id AND spoc.condition_id=spocp.condition_id AND spo.cat_id=38 AND spp.date_start<=GetDate() AND spp.date_end>=GetDate() AND spo.product_id=p.product_id ORDER BY spocp.rate ASC),0) AS rate_lowest";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocpe.price/1.177 AS money) FROM tbl_product_option_condition_price_extranet spocpe, tbl_product_option_condition spoc, tbl_product_option spo WHERE spo.option_id=spoc.option_id AND spoc.condition_id=spocpe.condition_id AND spo.cat_id=38 AND spocpe.date_price>=GETDATE() AND spo.product_id=p.product_id ORDER BY spocpe.price ASC),0) AS rate_lowest_extranet";
                        break;
                    case 32:
                        sqlCommand = sqlCommand + " ,isnull((select (sum(rate_overall)+sum(rate_fairway)+sum(rate_green)+sum(rate_difficult)+sum(rate_speed)+sum(rate_caddy)+sum(rate_clubhouse)+sum(rate_food)+sum(rate_money))/cast(9 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as avg_review";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocp.rate/1.177 AS money) FROM tbl_product_option_condition_price spocp, tbl_product_option_condition spoc, tbl_product_option spo, tbl_product_period spp WHERE spp.period_id=spocp.period_id AND spo.option_id=spoc.option_id AND spoc.condition_id=spocp.condition_id AND spo.cat_id=48 AND spp.date_start<=GetDate() AND spp.date_end>=GetDate() AND spo.product_id=p.product_id ORDER BY spocp.rate ASC),0) AS rate_lowest";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocpe.price/1.177 AS money) FROM tbl_product_option_condition_price_extranet spocpe, tbl_product_option_condition spoc, tbl_product_option spo WHERE spo.option_id=spoc.option_id AND spoc.condition_id=spocpe.condition_id AND spo.cat_id=48 AND spocpe.date_price>=GETDATE() AND spo.product_id=p.product_id ORDER BY spocpe.price ASC),0) AS rate_lowest_extranet";
                        break;
                    case 34:
                        sqlCommand = sqlCommand + " ,isnull((select (sum(srh.rate_overall)+sum(srh.rate_service)+sum(srh.rate_knowledge)+sum(srh.rate_punctuality)+sum(srh.rate_pronunciation)+sum(srh.rate_money)+sum(srh.rate_food))/cast(7 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as avg_review";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocp.rate/1.177 AS money) FROM tbl_product_option_condition_price spocp, tbl_product_option_condition spoc, tbl_product_option spo, tbl_product_period spp WHERE spp.period_id=spocp.period_id AND spo.option_id=spoc.option_id AND spoc.condition_id=spocp.condition_id AND spo.cat_id=52 AND spp.date_start<=GetDate() AND spp.date_end>=GetDate() AND spo.product_id=p.product_id ORDER BY spocp.rate ASC),0) AS rate_lowest";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocpe.price/1.177 AS money) FROM tbl_product_option_condition_price_extranet spocpe, tbl_product_option_condition spoc, tbl_product_option spo WHERE spo.option_id=spoc.option_id AND spoc.condition_id=spocpe.condition_id AND spo.cat_id=52 AND spocpe.date_price>=GETDATE() AND spo.product_id=p.product_id ORDER BY spocpe.price ASC),0) AS rate_lowest_extranet";
                        break;
                    case 36:
                        sqlCommand = sqlCommand + " ,isnull((select (sum(rate_overall)+sum(rate_knowledge)+sum(rate_service)+sum(rate_pronunciation)+sum(rate_punctuality)+sum(rate_food)+sum(rate_money))/cast(7 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as avg_review";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocp.rate/1.177 AS money) FROM tbl_product_option_condition_price spocp, tbl_product_option_condition spoc, tbl_product_option spo, tbl_product_period spp WHERE spp.period_id=spocp.period_id AND spo.option_id=spoc.option_id AND spoc.condition_id=spocp.condition_id AND spo.cat_id=53 AND spp.date_start<=GetDate() AND spp.date_end>=GetDate() AND spo.product_id=p.product_id ORDER BY spocp.rate ASC),0) AS rate_lowest";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocpe.price/1.177 AS money) FROM tbl_product_option_condition_price_extranet spocpe, tbl_product_option_condition spoc, tbl_product_option spo WHERE spo.option_id=spoc.option_id AND spoc.condition_id=spocpe.condition_id AND spo.cat_id=53 AND spocpe.date_price>=GETDATE() AND spo.product_id=p.product_id ORDER BY spocpe.price ASC),0) AS rate_lowest_extranet";
                        break;
                    case 38:
                        sqlCommand = sqlCommand + " ,isnull((select (sum(rate_overall)+sum(rate_performance)+sum(rate_punctuality)+sum(rate_service)+sum(rate_money))/cast(5 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as avg_review";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocp.rate/1.177 AS money) FROM tbl_product_option_condition_price spocp, tbl_product_option_condition spoc, tbl_product_option spo, tbl_product_period spp WHERE spp.period_id=spocp.period_id AND spo.option_id=spoc.option_id AND spoc.condition_id=spocp.condition_id AND spo.cat_id=54 AND spp.date_start<=GetDate() AND spp.date_end>=GetDate() AND spo.product_id=p.product_id ORDER BY spocp.rate ASC),0) AS rate_lowest";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocpe.price/1.177 AS money) FROM tbl_product_option_condition_price_extranet spocpe, tbl_product_option_condition spoc, tbl_product_option spo WHERE spo.option_id=spoc.option_id AND spoc.condition_id=spocpe.condition_id AND spo.cat_id=54 AND spocpe.date_price>=GETDATE() AND spo.product_id=p.product_id ORDER BY spocpe.price ASC),0) AS rate_lowest_extranet";
                        break;
                    case 39:
                        sqlCommand = sqlCommand + " ,isnull((select (sum(srh.rate_overall)+sum(srh.rate_service)+sum(srh.rate_diagnose_ability)+sum(srh.rate_clean)+sum(srh.rate_money))/cast(5 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as avg_review";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocp.rate/1.177 AS money) FROM tbl_product_option_condition_price spocp, tbl_product_option_condition spoc, tbl_product_option spo, tbl_product_period spp WHERE spp.period_id=spocp.period_id AND spo.option_id=spoc.option_id AND spoc.condition_id=spocp.condition_id AND spo.cat_id=55 AND spp.date_start<=GetDate() AND spp.date_end>=GetDate() AND spo.product_id=p.product_id ORDER BY spocp.rate ASC),0) AS rate_lowest";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocpe.price/1.177 AS money) FROM tbl_product_option_condition_price_extranet spocpe, tbl_product_option_condition spoc, tbl_product_option spo WHERE spo.option_id=spoc.option_id AND spoc.condition_id=spocpe.condition_id AND spo.cat_id=55 AND spocpe.date_price>=GETDATE() AND spo.product_id=p.product_id ORDER BY spocpe.price ASC),0) AS rate_lowest_extranet";
                        break;
                    case 40:
                        sqlCommand = sqlCommand + " ,isnull((select (sum(rate_overall)+sum(rate_clean)+sum(rate_diagnose_ability)+sum(rate_service)+sum(rate_money))/cast(5 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as avg_review";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocp.rate/1.177 AS money) FROM tbl_product_option_condition_price spocp, tbl_product_option_condition spoc, tbl_product_option spo, tbl_product_period spp WHERE spp.period_id=spocp.period_id AND spo.option_id=spoc.option_id AND spoc.condition_id=spocp.condition_id AND spo.cat_id=56 AND spp.date_start<=GetDate() AND spp.date_end>=GetDate() AND spo.product_id=p.product_id ORDER BY spocp.rate ASC),0) AS rate_lowest";
                        sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 CAST(spocpe.price/1.177 AS money) FROM tbl_product_option_condition_price_extranet spocpe, tbl_product_option_condition spoc, tbl_product_option spo WHERE spo.option_id=spoc.option_id AND spoc.condition_id=spocpe.condition_id AND spo.cat_id=56 AND spocpe.date_price>=GETDATE() AND spo.product_id=p.product_id ORDER BY spocpe.price ASC),0) AS rate_lowest_extranet";
                        break;
                }

                sqlCommand = sqlCommand + " ,(select top 1 spp.pic_code from tbl_product_pic spp where spp.product_id=p.product_id and spp.cat_id=1 and spp.type_id=6 and spp.status=1) as thumbnail";
                sqlCommand = sqlCommand + " ,(select top 1 spp.pic_code from tbl_product_pic spp where spp.product_id=p.product_id and spp.cat_id=1 and spp.type_id=1 and spp.status=1) as picture";
                sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 spc.title FROM tbl_promotion sp, tbl_promotion_content spc WHERE sp.promotion_id=spc.promotion_id AND sp.date_start<=GetDate() AND sp.date_end<=GetDate() AND sp.product_id=p.product_id AND spc.lang_id=1 ORDER BY sp.day_min ASC),'') AS promotion";
                sqlCommand = sqlCommand + " ,ISNULL((SELECT TOP 1 spc.title FROM tbl_promotion sp, tbl_promotion_content spc WHERE sp.promotion_id=spc.promotion_id AND sp.date_start<=GetDate() AND sp.date_end<=GetDate() AND sp.product_id=p.product_id AND spc.lang_id="+langID+" ORDER BY sp.day_min ASC),'') AS second_lang_promotion";
                sqlCommand = sqlCommand + " ,(SELECT COUNT(product_id) FROM tbl_product WHERE cat_id=29 AND status=1) AS num_hotel";
                sqlCommand = sqlCommand + " ,(SELECT COUNT(product_id) FROM tbl_product WHERE cat_id<>29 AND status=1) AS num_product_other";
                sqlCommand = sqlCommand + " from tbl_product p left outer join tbl_product_location pl on p.product_id = pl.product_id LEFT OUTER JOIN tbl_location l on pl.location_id = l.location_id,tbl_product_content pc,tbl_destination d,tbl_product_category pcat,tbl_destination_name dn";
                sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=dn.destination_id and p.cat_id=pcat.cat_id  and p.destination_id=d.destination_id and pc.lang_id=1 and dn.lang_id="+langID+"  and p.product_id=" + ProductID;
                //HttpContext.Current.Response.Write(sqlCommand + "<br /><br />");
                //HttpContext.Current.Response.End();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Prepare Data //
                    int intRateLow = (int)(decimal)reader["rate_lowest"];
                    int intRateLowExtranet = (int)(decimal)reader["rate_lowest_extranet"];
                    string strRateLow = "";

                    if ((bool)reader["extranet_active"])
                    {
                        strRateLow = intRateLowExtranet.ToString("#,###");
                    }
                    else
                    {
                        strRateLow = intRateLow.ToString("#,###");
                    }
                    // Prepare Data //
                    string productTitle = string.Empty;
                    string address = string.Empty;
                    string promotionTitle = string.Empty;
                    string productDetail = string.Empty;
                    string filemain = string.Empty;
                    string fileinfo = string.Empty;
                    string filephoto = string.Empty;
                    string filepdf = string.Empty;
                    string filereview = string.Empty;
                    string filewhy = string.Empty;
                    string filecontact = string.Empty;

                    if(langID==1)
                    {
                        productTitle = reader["title"].ToString();
                        productDetail=reader["detail"].ToString();
                        promotionTitle = reader["promotion"].ToString();
                        address=reader["address"].ToString();
                        if(string.IsNullOrEmpty(subfixFile))
                        {
                            filemain = reader["file_name_main"].ToString();
                            fileinfo = reader["file_name_information"].ToString();
                            filephoto = reader["file_name_photo"].ToString();
                            filepdf = reader["file_name_pdf"].ToString();
                            filereview = reader["file_name_review"].ToString();
                            filewhy = reader["file_name_why"].ToString();
                            filecontact = reader["file_name_contact"].ToString();
                        }else{
                            filemain = reader["file_name_main"].ToString().Replace(".asp",subfixFile);
                            fileinfo = reader["file_name_information"].ToString().Replace(".asp", subfixFile);
                            filephoto = reader["file_name_photo"].ToString().Replace(".asp", subfixFile);
                            filepdf = reader["file_name_pdf"].ToString().Replace(".asp", subfixFile);
                            filereview = reader["file_name_review"].ToString().Replace(".asp", subfixFile);
                            filewhy = reader["file_name_why"].ToString().Replace(".asp", subfixFile);
                            filecontact = reader["file_name_contact"].ToString().Replace(".asp", subfixFile);
                        }
                        
                    }else{
                        if (!string.IsNullOrEmpty(reader["second_lang"].ToString()))
                        {
                            productTitle = reader["second_lang"].ToString();
                            productDetail = reader["second_lang_detail"].ToString();
                            promotionTitle = reader["second_lang_promotion"].ToString();
                            address = reader["second_lang_address"].ToString();
                        }else{
                            productTitle = reader["title"].ToString();
                            productDetail = reader["detail"].ToString();
                            promotionTitle = reader["promotion"].ToString();
                            address = reader["address"].ToString();
                        }
                        

                        filemain = reader["file_name_main"].ToString().Replace(".asp","-th.asp");
                        fileinfo = reader["file_name_information"].ToString().Replace(".asp", "-th.asp");
                        filephoto = reader["file_name_photo"].ToString().Replace(".asp", "-th.asp");
                        filepdf = reader["file_name_pdf"].ToString().Replace(".asp", "-th.asp");
                        filereview = reader["file_name_review"].ToString().Replace(".asp", "-th.asp");
                        filewhy = reader["file_name_why"].ToString().Replace(".asp", "-th.asp");
                        filecontact = reader["file_name_contact"].ToString().Replace(".asp", "-th.asp");
                        if (string.IsNullOrEmpty(productTitle))
                        {
                            productTitle = reader["title"].ToString();

                            filemain = reader["file_name_main"].ToString();
                            fileinfo = reader["file_name_information"].ToString();
                            filephoto = reader["file_name_photo"].ToString();
                            filepdf = reader["file_name_pdf"].ToString();
                            filereview = reader["file_name_review"].ToString();
                            filewhy = reader["file_name_why"].ToString();
                            filecontact = reader["file_name_contact"].ToString();
                        }
                    }

                    this.ProductID = (int)reader["product_id"];
                    this.Title = productTitle;
                    this.TitleDefault = reader["title"].ToString();
                    this.Detail = productDetail;
                    this.Address = address;
                    this.DestinationID = (short)reader["destination_id"];
                    this.DestinationTitle = reader["destination_title"].ToString();
                    this.LocationID = (short)reader["location_id"];
                    this.LocationTitle = reader["destination_title"].ToString();
                    this.PaymentTypeID = (byte)reader["payment_type_id"];
                    this.CategoryID = (byte)reader["cat_id"];
                    this.Star = (float)reader["star"];
                    this.FileMain = filemain;
                    this.FileInfo = fileinfo;
                    this.FilePhoto = filephoto;
                    this.FilePDF = filepdf;
                    this.FileReview = filereview;
                    this.FileWhy = filewhy;
                    this.FileContact = filecontact;
                    this.FolderDestination = reader["folder_destination"].ToString();
                    this.ProductCode = reader["product_code"].ToString();
                    this.IsInternetFree = (bool)reader["is_internet_free"];
                    this.HasInternet = (bool)reader["is_internet_have"];
                    this.InternetDetail = reader["detail_internet"].ToString();
                    this.NumPic = (byte)reader["num_pic"];
                    this.IsNewPic = (bool)reader["is_new_pic"];
                    this.AverageReview = (double)reader["avg_review"];

                    if (!string.IsNullOrEmpty(reader["thumbnail"].ToString()))
                    {
                        this.Thumbnail = reader["thumbnail"].ToString();
                    }
                    else {
                        this.Thumbnail = "/thailand-" + Utility.GetProductType((byte)reader["cat_id"])[0, 3] + "-pic/" + reader["product_code"] + "_1.jpg";
                    }

                
                    if (!string.IsNullOrEmpty(reader["picture"].ToString()))
                    {
                        this.Picture = reader["picture"].ToString();
                    }
                    else {
                        this.Picture = "/thailand-hotels-pic/" + reader["product_code"] + "_a.jpg";
                    }
                    
                    this.RateLowest = strRateLow;
                    this.Promotion = promotionTitle;
                    this.NumHotel = (int)reader["num_hotel"];
                    this.NumProductOther = (int)reader["num_product_other"];
                    this.langID = langID;
                    this.IsExtranet = (bool)reader["extranet_active"];
                }
                return this;
            }
        }
    }
}