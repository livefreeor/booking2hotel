using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand;


/// <summary>
/// Summary description for FrontSiteAnalytic
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontSiteAnalytic : Hotels2BaseClass
    {
        public short SiteID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ProductID { get; set; }
        public string Detail { get; set; }
        public int AffSiteID { get; set; }

        public FrontSiteAnalytic()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string getAnalyticString(int ProductID)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select detail from tbl_site_analytic where product_id=" + ProductID;
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }
        public int Insert(FrontSiteAnalytic data)
        {

            int result = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "insert into tbl_site_analytic(title,url,product_id,detail,aff_site_id) ";
                sqlCommand = sqlCommand + " values(@title,@url,@product_id,@detail,@aff_site_id); SET @site_id = SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);

                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = data.Title;
                cmd.Parameters.Add("@url", SqlDbType.VarChar).Value = data.Url;
                cmd.Parameters.Add("@product_id", SqlDbType.SmallInt).Value = data.ProductID;
                cmd.Parameters.Add("@detail", SqlDbType.VarChar).Value = data.Detail;
                cmd.Parameters.Add("@aff_site_id", SqlDbType.SmallInt).Value = data.AffSiteID;
                cmd.Parameters.Add("@site_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                result = (int)cmd.Parameters["@site_id"].Value;
            }
            return result;
        }
    }
}
