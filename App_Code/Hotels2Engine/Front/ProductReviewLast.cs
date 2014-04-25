using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ProductReviewLast
/// </summary>
/// 

namespace Hotels2thailand.Front
{

    public class ProductReviewLast
    {
        public int ProductID { get; set; }
        public int LastReviewID { get; set; }
        public int CountReview { get; set; }
        public double AverageReview { get; set; }
        public string Fullname { get; set; }
        public string Content { get; set; }
        public DateTime DateSubmit { get; set; }
        public byte CountryID { get; set; }
        public string ReviewFrom { get; set; }

        private List<ProductReviewLast> productReviews;

        public ProductReviewLast()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public ProductReviewLast GetProductReviewLast(FrontProductDetail detail)
        {
            string sqlCommand = "SELECT p.product_id, rh.review_id as last_review,rh.detail,rh.review_from,";
            sqlCommand = sqlCommand + " (select count(srh.review_id) from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1) as review_count,";
            switch(detail.CategoryID)
            {
                case 29:
                    sqlCommand = sqlCommand + " isnull((select (sum(srh.rate_overall)+sum(srh.rate_service)+sum(srh.rate_location)+sum(srh.rate_room)+sum(srh.rate_clean)+sum(srh.rate_money))/cast(6 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as average_review,rh.full_name,rh.date_submit,";
                    break;
                case 32:
                    sqlCommand = sqlCommand + " isnull((select (sum(rate_overall)+sum(rate_fairway)+sum(rate_green)+sum(rate_difficult)+sum(rate_speed)+sum(rate_caddy)+sum(rate_clubhouse)+sum(rate_food)+sum(rate_money))/cast(9 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as average_review,rh.full_name,rh.date_submit,";
                    
                    break;
                case 34:case 36:
                    sqlCommand = sqlCommand + " isnull((select (sum(rate_overall)+sum(rate_knowledge)+sum(rate_service)+sum(rate_pronunciation)+sum(rate_punctuality)+sum(rate_food)+sum(rate_money))/cast(7 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as average_review,rh.full_name,rh.date_submit,";
                    break;

                case 38:
                    sqlCommand = sqlCommand + " isnull((select (sum(rate_overall)+sum(rate_performance)+sum(rate_punctuality)+sum(rate_service)+sum(rate_money))/cast(5 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as average_review,rh.full_name,rh.date_submit,";
                    break;
                case 39:case 40:
                    sqlCommand = sqlCommand + " isnull((select (sum(rate_overall)+sum(rate_clean)+sum(rate_diagnose_ability)+sum(rate_service)+sum(rate_money))/cast(5 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1),0) as average_review,rh.full_name,rh.date_submit,";
                    break;
            }
            
            sqlCommand = sqlCommand + " ISNULL(rh.country_id,'0') as country_id";
            sqlCommand = sqlCommand + " FROM tbl_product p ,tbl_review_all rh";
            sqlCommand = sqlCommand + " WHERE p.product_id = rh.product_id AND p.product_id=" + detail.ProductID;
            sqlCommand = sqlCommand + " AND rh.review_id = (SELECT TOP 1 rh.review_id FROM tbl_review_all rh WHERE p.product_id = rh.product_id AND rh.detail<>'' AND rh.status = 1 AND rh.status_bin = 1 ORDER BY rh.date_submit DESC)";

            //HttpContext.Current.Response.Write(sqlCommand);
            DataConnect objConn = new DataConnect();
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            if (reader.Read())
            {
                this.ProductID = (int)reader["product_id"];
                this.LastReviewID = (int)reader["last_review"];
                this.CountReview = (int)reader["review_count"];
                this.AverageReview = (double)reader["average_review"];
                this.Fullname = reader["full_name"].ToString();
                this.Content = reader["detail"].ToString();
                this.DateSubmit = (DateTime)reader["date_submit"];
                this.CountryID = (byte)reader["country_id"];
                this.ReviewFrom = reader["review_from"].ToString();
            }
            objConn.Close();
            return this;
        }

        public void LoadAllProductReviewLast(byte ProductType)
        {
            string sqlCommand = "SELECT p.product_id, rh.review_id as last_review,rh.detail,rh.review_from,";
            sqlCommand = sqlCommand + " (select count(srh.review_id) from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1) as review_count,";
            sqlCommand = sqlCommand + " (select (sum(srh.rate_overall)+sum(srh.rate_service)+sum(srh.rate_location)+sum(srh.rate_room)+sum(srh.rate_clean)+sum(srh.rate_money))/cast(6 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1) as average_review,rh.full_name,rh.date_submit,";
            sqlCommand = sqlCommand + " ISNULL(rh.country_id,'0') as country_id";
            sqlCommand = sqlCommand + " FROM tbl_product p ,tbl_review_all rh";
            sqlCommand = sqlCommand + " WHERE p.cat_id = " + ProductType + "  AND p.product_id = rh.product_id ";
            sqlCommand = sqlCommand + " AND rh.review_id = (SELECT TOP 1 rh.review_id FROM tbl_review_all rh WHERE p.product_id = rh.product_id AND rh.detail<>'' AND rh.status = 1 AND rh.status_bin = 1 ORDER BY rh.date_submit DESC)";

            DataConnect objConn = new DataConnect();
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            productReviews = new List<ProductReviewLast>();

            while (reader.Read())
            {
                productReviews.Add(new ProductReviewLast
                {
                    ProductID = (int)reader["product_id"],
                    LastReviewID = (int)reader["last_review"],
                    CountReview = (int)reader["review_count"],
                    AverageReview = (double)reader["average_review"],
                    Fullname = reader["full_name"].ToString(),
                    Content = reader["detail"].ToString(),
                    DateSubmit = (DateTime)reader["date_submit"],
                    CountryID = (byte)reader["country_id"],
                    ReviewFrom = reader["review_from"].ToString()
                });
            }

            objConn.Close();
        }
        public void GetReviewByID(int ProductID, byte CategoryID)
        {
            string sqlCommand = "SELECT p.product_id, rh.review_id as last_review,rh.detail,rh.review_from,";
            sqlCommand = sqlCommand + " (select count(srh.review_id) from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1) as review_count,";
            sqlCommand = sqlCommand + " (select (sum(srh.rate_overall)+sum(srh.rate_service)+sum(srh.rate_location)+sum(srh.rate_room)+sum(srh.rate_clean)+sum(srh.rate_money))/cast(6 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1) as average_review,rh.full_name,rh.date_submit,";
            sqlCommand = sqlCommand + " ISNULL(rh.country_id,'0') as country_id";
            sqlCommand = sqlCommand + " FROM tbl_product p ,tbl_review_all rh";
            sqlCommand = sqlCommand + " WHERE p.cat_id = " + CategoryID + "  AND p.product_id = rh.product_id AND p.product_id=" + ProductID;
            sqlCommand = sqlCommand + " AND rh.review_id = (SELECT TOP 1 rh.review_id FROM tbl_review_all rh WHERE p.product_id = rh.product_id AND rh.detail<>'' AND rh.status = 1 AND rh.status_bin = 1 ORDER BY rh.date_submit DESC)";

            DataConnect objConn = new DataConnect();
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            productReviews = new List<ProductReviewLast>();

            while (reader.Read())
            {
                productReviews.Add(new ProductReviewLast
                {
                    ProductID = (int)reader["product_id"],
                    LastReviewID = (int)reader["last_review"],
                    CountReview = (int)reader["review_count"],
                    AverageReview = (double)reader["average_review"],
                    Fullname = reader["full_name"].ToString(),
                    Content = reader["detail"].ToString(),
                    DateSubmit = (DateTime)reader["date_submit"],
                    CountryID = (byte)reader["country_id"],
                    ReviewFrom = reader["review_from"].ToString()
                });
            }

            objConn.Close();

        }
        public void LoadReviewByProductGroup(string ProductGroup)
        {
            if (string.IsNullOrEmpty(ProductGroup))
            {
                ProductGroup = "0";
            }
            string sqlCommand = "SELECT p.product_id, rh.review_id as last_review,rh.detail,rh.review_from,";
            sqlCommand = sqlCommand + " (select count(srh.review_id) from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1) as review_count,";
            sqlCommand = sqlCommand + " (select (sum(srh.rate_overall)+sum(srh.rate_service)+sum(srh.rate_location)+sum(srh.rate_room)+sum(srh.rate_clean)+sum(srh.rate_money))/cast(6 as float)/count(srh.review_id)  from tbl_review_all srh where srh.product_id=p.product_id and srh.status=1 and srh.status_bin=1) as average_review,rh.full_name,rh.date_submit,";
            sqlCommand = sqlCommand + " ISNULL(rh.country_id,'0') as country_id";
            sqlCommand = sqlCommand + " FROM tbl_product p ,tbl_review_all rh";
            sqlCommand = sqlCommand + " WHERE p.product_id = rh.product_id AND p.product_id IN (" + ProductGroup + ")";
            sqlCommand = sqlCommand + " AND rh.review_id = (SELECT TOP 1 rh.review_id FROM tbl_review_all rh WHERE p.product_id = rh.product_id AND rh.detail<>'' AND rh.status = 1 AND rh.status_bin = 1 ORDER BY rh.date_submit DESC)";


            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.End();

            DataConnect objConn = new DataConnect();
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            productReviews = new List<ProductReviewLast>();

            while (reader.Read())
            {
                productReviews.Add(new ProductReviewLast
                {
                    ProductID = (int)reader["product_id"],
                    LastReviewID = (int)reader["last_review"],
                    CountReview = (int)reader["review_count"],
                    AverageReview = (double)reader["average_review"],
                    Fullname = reader["full_name"].ToString(),
                    Content = reader["detail"].ToString(),
                    DateSubmit = (DateTime)reader["date_submit"],
                    CountryID = (byte)reader["country_id"],
                    ReviewFrom = reader["review_from"].ToString()
                });
            }

            objConn.Close();

        }
        public ProductReviewLast GetReviewByProductID(int ProductID)
        {
            ProductReviewLast result = null;
            foreach (ProductReviewLast item in productReviews)
            {
                if (item.ProductID == ProductID)
                {
                    result = item;
                }
            }
            return result;
        }
    }
}