using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

using System.Web.Configuration;
/// <summary>
/// Summary description for BookingAcknowledge
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingAcknowledge : Hotels2BaseClass
    {
        public int BookingID { get; set; }
        public byte StatusExtranetID { get; set; }
        public string AcknowledgeID { get; set; }
        public short StaffID { get; set; }
        public DateTime DateConfirm { get; set; }


        public BookingAcknowledge()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string  GetBookingAcknowledgeByID(int BookingID)
        {
            string result = string.Empty;

            string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(connString))
            {
                string strCommand="select ISNULL(ba.acknowledge_id,'')";
                strCommand=strCommand+" from tbl_booking b,tbl_booking_acknowledge ba";
                strCommand=strCommand+" where b.booking_id=ba.booking_id and ba.status_extranet_id=b.status_extranet_id and b.booking_id="+BookingID;
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                result = (string)cmd.ExecuteScalar();
                
            }
            return result;
        }

        

        public int InsertByStaff(BookingAcknowledge data)
        {
            int result = 1;
            string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            switch (data.StatusExtranetID)
            {
                case 1:case 3:
                    
                     using (SqlConnection cn = new SqlConnection(connString))
                     {
                         SqlCommand cmd = new SqlCommand();
                         cmd.CommandText = "insert into tbl_booking_acknowledge (booking_id,status_extranet_id,acknowledge_id,staff_id,date_confirm) values(@booking_id,@status_extranet_id,@acknowledge_id,@staff_id,@date_confirm)";
                         cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                         cmd.Parameters.Add("@status_extranet_id", SqlDbType.TinyInt).Value = (data.StatusExtranetID+1);
                         cmd.Parameters.Add("@acknowledge_id", SqlDbType.VarChar).Value = data.AcknowledgeID;
                         cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = data.StaffID;
                         cmd.Parameters.Add("@date_confirm", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                         cmd.Connection = cn;
                         cn.Open();
                         ExecuteNonQuery(cmd);
                         result = data.BookingID;
                     }
                     using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                     {
                         SqlCommand cmd = new SqlCommand();
                         cmd.CommandText = "update tbl_booking set status_extranet_id=@status_extranet_id where booking_id=@booking_id";
                         cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                         cmd.Parameters.Add("@status_extranet_id", SqlDbType.TinyInt).Value = (data.StatusExtranetID + 1);
                         cmd.Connection = cn;
                         cn.Open();
                         cmd.ExecuteNonQuery();
                     }
                    break;
                case 2:
                case 4:
                    int checkStaff = 0;
                    using (SqlConnection cn = new SqlConnection(connString))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "select COUNT(booking_id) from tbl_booking_acknowledge where booking_id=@booking_id and status_extranet_id=@status_extranet_id and staff_id=@staff_id";
                        cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                        cmd.Parameters.Add("@status_extranet_id", SqlDbType.Int).Value = data.StatusExtranetID;
                        cmd.Parameters.Add("@staff_id", SqlDbType.TinyInt).Value = data.StaffID;
                        cmd.Connection = cn;
                        cn.Open();
                        checkStaff = (int)cmd.ExecuteScalar();
                    }

                    if (checkStaff != 0)
                    {
                        using (SqlConnection cn = new SqlConnection(connString))
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "update tbl_booking_acknowledge set acknowledge_id=@acknowledge_id where booking_id=@booking_id and status_extranet_id=@status_extranet_id";
                            cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                            cmd.Parameters.Add("@status_extranet_id", SqlDbType.TinyInt).Value = data.StatusExtranetID;
                            cmd.Parameters.Add("@acknowledge_id", SqlDbType.VarChar).Value = data.AcknowledgeID;
                            cmd.Connection = cn;
                            cn.Open();
                            ExecuteNonQuery(cmd);
                            result = data.BookingID;
                        }
                    }
                    else {
                        result = 0;
                    }
                    
                    break;
            }
            return result;

        }



        public int Insert(BookingAcknowledge data)
        {
            int result = 0;
            int ret = 0;
            string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(connString))
            {
                //HttpContext.Current.Response.Write("select COUNT(booking_id) from tbl_booking_acknowledge where booking_id=" + data.BookingID + " and status_extranet_id=" + data.StatusExtranetID);
                SqlCommand cmd = new SqlCommand("select COUNT(booking_id) from tbl_booking_acknowledge where booking_id=" + data.BookingID + " and status_extranet_id=" + data.StatusExtranetID, cn);
                cn.Open();
                result = (int)cmd.ExecuteScalar();
            }

            if (result == 0)
            {
                //HttpContext.Current.Response.Write("Create new record");
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    switch (data.StatusExtranetID)
                    {
                        case 1:case 3:

                            cmd.CommandText = "insert into tbl_booking_acknowledge (booking_id,status_extranet_id) values(@booking_id,"+data.StatusExtranetID+")";
                            cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                            break;
                        
                        case 2:
                        case 4:
                            cmd.CommandText = "insert into tbl_booking_acknowledge (booking_id,status_extranet_id,acknowledge_id,staff_id,date_confirm) values(@booking_id,@status_extranet_id,@acknowledge_id,@staff_id,@date_confirm)";
                            cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                            cmd.Parameters.Add("@status_extranet_id", SqlDbType.TinyInt).Value = data.StatusExtranetID;
                            cmd.Parameters.Add("@acknowledge_id", SqlDbType.VarChar).Value = data.AcknowledgeID;
                            cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = data.StaffID;
                            cmd.Parameters.Add("@date_confirm", SqlDbType.DateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                            break;
                    }
                    cmd.Connection = cn;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    ret = (int)cmd.Parameters["@booking_id"].Value;
                    
                }
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "update tbl_booking set status_extranet_id=@status_extranet_id where booking_id=@booking_id";
                    cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                    cmd.Parameters.Add("@status_extranet_id", SqlDbType.TinyInt).Value = data.StatusExtranetID;
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            else {
                ret = 0;
            }
           // HttpContext.Current.Response.Write(ret);
            return ret;
        }

        public int CountBookingExtraNet(byte bytExtra_net, int intProduct_id, short shrSupplier_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_extranet_acknowledge_cont", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallMoney).Value = shrSupplier_id;
                cmd.Parameters.Add("@status_extranet_id", SqlDbType.TinyInt).Value = bytExtra_net;
                cn.Open();
                int count = (int)ExecuteScalar(cmd);
                return count;
            }
        }

       
    }
}