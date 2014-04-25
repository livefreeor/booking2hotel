using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Booking;
using Hotels2thailand.Staffs;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BookingActivity
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingAffiliate : Hotels2BaseClass
    {
        
        
        //
        public string getAffSiteUrl(int intSite_id)
        {
            string Site = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT url FROM tbl_aff_sites WHERE site_id = @site_id", cn);
                cmd.Parameters.Add("@site_id", SqlDbType.Int).Value = intSite_id;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Site =  reader[0].ToString();
                }
                else
                {
                    Site = "Please contact Rd Team!!";
                }
                return Site;
            }
        }

    }
}
