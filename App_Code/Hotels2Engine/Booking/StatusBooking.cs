using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for StatusBooking
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class StatusBooking : Hotels2BaseClass
    {
        public StatusBooking()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public bool UpdateBookingStatus(int intBookingId, short bytstatusId)
        {
            //HttpContext.Current.Response.Write(intBookingId + "----" + bytstatusId);
            //HttpContext.Current.Response.End();


            short shrStatusProductId = 0;
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking SET status_id=@status_id , date_modify=@date_modify WHERE booking_id = @booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cmd.Parameters.Add("@status_id", SqlDbType.SmallInt).Value = bytstatusId;
                cmd.Parameters.Add("@date_modify", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                switch(bytstatusId)
                {
                    case 68 :
                        shrStatusProductId = 10;
                        break;
                    case 71 :
                        shrStatusProductId = 11;
                        break;
                    case 72 :
                        shrStatusProductId = 12;
                        break;
                    case 83 :
                        shrStatusProductId = 13;
                        break;
                    case 85 :
                        shrStatusProductId = 15;
                        break;
                    case 92:
                        shrStatusProductId = 93;
                        break;
                    case 30:
                        shrStatusProductId = 17;
                        break;
                    case 94:
                        shrStatusProductId = 95;
                        break;
                    case 96:
                        shrStatusProductId = 97;
                        break;
                    case 98:
                        shrStatusProductId = 22;
                        break;
                }

                SqlCommand cmdp = new SqlCommand("UPDATE tbl_booking_product SET status_id = @status_id WHERE  booking_id = @booking_id", cn);
                cmdp.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cmdp.Parameters.Add("@status_id", SqlDbType.SmallInt).Value = shrStatusProductId;

                ret = ExecuteNonQuery(cmdp);
                return (ret == 1);
            }
        }

        public bool UpdateBookingStatusAff(int intBookingId, short bytstatusId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking SET status_affiliate_id=@status_affiliate_id, date_modify=@date_modify WHERE booking_id = @booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cmd.Parameters.Add("@status_affiliate_id", SqlDbType.SmallInt).Value = bytstatusId;
                cmd.Parameters.Add("@date_modify", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
        public bool UpdateProductBookingStatus(int intBokingProductId, short bytstatusId)
        {
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_product SET status_id=@status_id WHERE booking_product_id = @booking_product_id", cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBokingProductId;
                cmd.Parameters.Add("@status_id", SqlDbType.SmallInt).Value = bytstatusId;
                //cmd.Parameters.Add("@date_modify", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
        

    }
}