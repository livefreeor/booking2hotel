using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;

/// <summary>
/// Summary description for Promotion
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class PromotionCatAndType:Hotels2BaseClass
    {
        

        public IDictionary<byte, string> GetPromotionCat()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cat_id,title FROM tbl_promotion_cat", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                IDictionary<byte, string> dicProCat = new Dictionary<byte, string>();
                
                    while (reader.Read())
                    {
                        dicProCat.Add((byte)reader["cat_id"], reader["title"].ToString());
                    };
                    
               
                return dicProCat;
            }
        }

        public IDictionary<byte, string> GetPromotionType()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT type_id,title FROM tbl_promotion_type", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                IDictionary<byte, string> dicProCat = new Dictionary<byte, string>();

                while (reader.Read())
                {
                    dicProCat.Add((byte)reader["type_id"], reader["title"].ToString());
                };


                return dicProCat;
            }
        }

        
    }
}