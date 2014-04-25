using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.LinqProvider.Booking;
using Hotels2thailand.LinqProvider.Staff;
using Hotels2thailand.Booking;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for PrefixName
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class PrefixName : Hotels2BaseClass
    {
        public PrefixName()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //private LinqBookingDataContext dcBooking = new LinqBookingDataContext();

        public byte PrefixID { get; set; }
        public string Title { get; set; }


        public PrefixName GetPrefixById(byte intPrefixId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_prefix_name WHERE prefix_id=@prefix_id", cn);
                cmd.Parameters.Add("@prefix_id",SqlDbType.TinyInt).Value = intPrefixId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())

                    return (PrefixName)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
            
        }

        public List<object> GetPrefixAll()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_prefix_name",cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
           
        }

    }
}