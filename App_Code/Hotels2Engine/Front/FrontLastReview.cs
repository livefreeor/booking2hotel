using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for FrontLastReview
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontLastReview:Hotels2BaseClass
    {
        public int ReviewID { get; set; }
        public string Detail { get; set; }
        public string Title { get; set; }
        public float Star { get; set; }
        public string Filename { get; set; }
        public string Fullname { get; set; }
        public string ReviewFrom { get; set; }
        public DateTime DateSubmit { get; set; }

        private byte _langID = 1;

        public byte LangID {
            set { _langID = value; }
        }

        public FrontLastReview()
        {
           
        }

        public List<FrontLastReview> GetLastReview(int numRow)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select top 5 r.review_id,r.detail,pc.title,";
                sqlCommand = sqlCommand + " (select top 1 spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + _langID + ") as second_lang,";
                sqlCommand = sqlCommand + " p.star,(d.folder_destination+'-hotels/'+pc.file_name_main) as file_name,r.full_name,r.review_from,r.date_submit";
                sqlCommand = sqlCommand + " from tbl_review_all r,tbl_product p,tbl_product_content pc,tbl_destination d";
                sqlCommand = sqlCommand + " where p.destination_id=d.destination_id and p.product_id=pc.product_id and p.product_id=r.product_id and r.cat_id=29 and pc.lang_id=1 and r.status=1 and status_bin=1";
                sqlCommand = sqlCommand + " order by r.review_id desc";

                
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<FrontLastReview> result = new List<FrontLastReview>();
                string productTitle = string.Empty;
                string productUrl = string.Empty;
                while (reader.Read())
                {
                    if (_langID == 1)
                    {
                        productTitle = reader["title"].ToString();
                        productUrl = reader["file_name"].ToString();
                    }
                    else {
                        productTitle = reader["second_lang"].ToString();
                        productUrl = reader["file_name"].ToString().Replace(".asp","-th.asp");
                        if (string.IsNullOrEmpty(productTitle))
                        {
                            productTitle = reader["title"].ToString();
                            //productUrl = reader["file_name"].ToString();
                        }
                    }

                    result.Add(new FrontLastReview
                    {
                        ReviewID = (int)reader["review_id"],
                        Detail = reader["detail"].ToString(),
                        Title = productTitle,
                        Star = (float)reader["star"],
                        Filename = productUrl,
                        Fullname = reader["full_name"].ToString(),
                        ReviewFrom = reader["review_from"].ToString(),
                        DateSubmit = (DateTime)reader["date_submit"]
                    });
                }
                return result;
            }

            
        }

        public List<FrontLastReview> GetLastReviewByProduct(int ProductID,int numRow)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select top " + numRow + " r.review_id,r.detail,pc.title,p.star,(d.folder_destination+'-hotels/'+pc.file_name_main) as file_name,r.full_name,r.review_from,r.date_submit";
                sqlCommand = sqlCommand + " from tbl_review_all r,tbl_product p,tbl_product_content pc,tbl_destination d";
                sqlCommand = sqlCommand + " where p.destination_id=d.destination_id and p.product_id=pc.product_id and p.product_id=r.product_id and r.cat_id=29 and pc.lang_id=1 and r.status=1 and status_bin=1";
                sqlCommand = sqlCommand + " and p.product_id=" + ProductID;
                sqlCommand = sqlCommand + " order by r.review_id desc";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<FrontLastReview> result = new List<FrontLastReview>();
                while (reader.Read())
                {
                    result.Add(new FrontLastReview
                    {
                        ReviewID = (int)reader["review_id"],
                        Detail = reader["detail"].ToString(),
                        Title = reader["title"].ToString(),
                        Star = (float)reader["star"],
                        Filename = reader["file_name"].ToString(),
                        Fullname = reader["full_name"].ToString(),
                        ReviewFrom = reader["review_from"].ToString(),
                        DateSubmit = (DateTime)reader["date_submit"]
                    });
                }
                return result;
            }

            
        }
        
    }
}