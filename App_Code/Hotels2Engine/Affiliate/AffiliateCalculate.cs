using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using System.Web.Configuration;

/// <summary>
/// Summary description for AffiliateCalculate
/// </summary>
/// 
namespace Hotels2thailand.Affiliate
{
    public class AffiliateCalculate
    {
        private int bookingID = 0;
        
        private string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;

        public AffiliateCalculate(int BookingID)
        {
            bookingID = BookingID;
        }

        public double getCommission()
        {
            using (SqlConnection cn = new SqlConnection(connString))
            {
                string sqlCommand = "select SUM(price_supplier) as price_own,SUM(price) as price";
                sqlCommand = sqlCommand + " from tbl_booking_item";
                sqlCommand = sqlCommand + " where booking_id=" + bookingID;
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                double price_comission = 0;
                if (reader.Read())
                {
                    price_comission = ((Convert.ToDouble((decimal)reader["price"]) * 0.97) - Convert.ToDouble((decimal)reader["price_own"]));
                    price_comission = ((price_comission * 40) / 100);
                }
                return price_comission;
            }
            
        }

        public void ReCalculateCommission()
        {
            using (SqlConnection cn = new SqlConnection(connString))
            {

                string sqlUpdateCommission = "update tbl_site_order set comission=" + getCommission() + " where booking_id=" + bookingID;
                SqlCommand cmd = new SqlCommand(sqlUpdateCommission, cn);
                cn.Open();
                cmd.ExecuteNonQuery();

            }
            
        }

        public void Insert(int siteID)
        {
            using (SqlConnection cn = new SqlConnection(connString))
            {
                string sqlInsertCommission = "insert into tbl_site_order(site_id,booking_id,main_site_id,comission,comission_percent,date_submit)";
                sqlInsertCommission = sqlInsertCommission + "values(" + siteID + "," + bookingID + ",1," + getCommission() + ",40," + DateTime.Now.Hotels2ThaiDateTime().Hotels2DateToSQlString() + ")";
                SqlCommand cmd = new SqlCommand(sqlInsertCommission, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            
        }
    }
}