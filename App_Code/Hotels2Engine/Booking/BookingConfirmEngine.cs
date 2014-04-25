using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for BookingPaymentList
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingConfirmEngine : Hotels2BaseClass
    {

        //public bool IsConfirmTime(int intBookingProductId)
        //{
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //       bool  IsConfirmTime = false;
        //        SqlCommand cmd = new SqlCommand("SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 13 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC",cn);
        //        cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
        //        cn.Open();
        //        IDataReader reader = ExecuteReader(cmd);
        //        int count = 0;
        //        while (reader.Read())
        //        {
        //            count = count + 1;
        //        }
        //        if (count > 0)
        //            IsConfirmTime = true;
        //        return IsConfirmTime;
        //    }
        //}

        public int UpdateConfirmMailtracking(int intBookingId, byte bytConfirmCat)
        {

            int confirmId = 0;
            int IsHave = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM tbl_booking_confirm WHERE confirm_cat_id=@confirm_cat_id AND booking_id=@booking_id AND status = 1", cn);
                cmd1.Parameters.Add("@confirm_cat_id", SqlDbType.TinyInt).Value = bytConfirmCat;
                cmd1.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                IsHave = (int)ExecuteScalar(cmd1);

                
                if (IsHave > 0)
                {
                    SqlCommand cmd2 = new SqlCommand("UPDATE tbl_booking_confirm SET status = @status WHERE booking_id =@booking_id AND confirm_cat_id = @confirm_cat_id", cn);
                    cmd2.Parameters.Add("@confirm_cat_id", SqlDbType.TinyInt).Value = bytConfirmCat;
                    cmd2.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                    cmd2.Parameters.Add("@status", SqlDbType.Bit).Value = false;
                    ExecuteNonQuery(cmd2);
                }

                
                string query = string.Empty;
                query = "INSERT INTO tbl_booking_confirm (booking_id,confirm_cat_id,date_submit,status)VALUES(@Id,@confirm_cat_id,@date_submit,@status);SET @confirm_id=SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = intBookingId;
                cmd.Parameters.Add("@confirm_cat_id", SqlDbType.TinyInt).Value = bytConfirmCat;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@confirm_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                ExecuteNonQuery(cmd);
                confirmId = (int)cmd.Parameters["@confirm_id"].Value;
                

            }

            HttpContext.Current.Response.Write(IsHave);
            HttpContext.Current.Response.End();
            return confirmId;
        }


        //not staff_activity
        public int UpdateConfirmCustomer_OpenVoucher(int intBookingId)
        {

            int confirmId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string query = string.Empty;
                query = "INSERT INTO tbl_booking_confirm (booking_id,confirm_cat_id,date_submit,status)VALUES(@Id,@confirm_cat_id,@date_submit,@status);SET @confirm_id=SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = intBookingId;
                cmd.Parameters.Add("@confirm_cat_id", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@confirm_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                confirmId = (int)cmd.Parameters["@confirm_id"].Value;

            }


            return confirmId;
        }
        public int UpdateConfirmByCat(int intBookingId , int? intBookingProduct_Id, byte bytConfirmCat)
        {
            DateTime dDateSubmit = DateTime.Now.Hotels2ThaiDateTime();
            string tbl_to_Log = string.Empty;
            string Key = string.Empty;
            int ID = 0;
            int ret = 0;
             int confirmId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string query = string.Empty;
                BookingActivityDisplay cBookingActivity = new BookingActivityDisplay();
                switch (bytConfirmCat)
                {
                    
                        //Print Reciept
                    case 3:
                        //Avaliable
                    case 8:
                        //Confirm Fax
                    case 9:
                        //Payment To Supplier
                    case 10:
                        //Confirm Check in
                    case 11:
                        query = "INSERT INTO tbl_booking_product_confirm (booking_product_id,confirm_cat_id,date_submit,status)VALUES(@Id,@confirm_cat_id,@date_submit,@status);SET @confirm_id=SCOPE_IDENTITY();";
                        tbl_to_Log = "tbl_booking_product_confirm";
                        ID = (int)intBookingProduct_Id;
                        Key = "booking_product_id";
                        break;
                        //Confirm Open
                    case 4:
                        //Confirm Voucher
                    case 2:
                        query = "INSERT INTO tbl_booking_confirm (booking_id,confirm_cat_id,date_submit,status)VALUES(@Id,@confirm_cat_id,@date_submit,@status);SET @confirm_id=SCOPE_IDENTITY();";
                        tbl_to_Log = "tbl_booking_confirm";
                        ID = intBookingId;
                        Key = "booking_id";
                        //auto activity
                        cBookingActivity.InsertAutoActivity(BookingActivityType.ConfirmVoucher, intBookingId);
                        break;
                        //Confirm Completed
                    case 5:
                    //Confirm INput
                    case 18:
                        query = "INSERT INTO tbl_booking_confirm (booking_id,confirm_cat_id,date_submit,status)VALUES(@Id,@confirm_cat_id,@date_submit,@status);SET @confirm_id=SCOPE_IDENTITY();";
                        tbl_to_Log = "tbl_booking_confirm";
                        ID = intBookingId;
                        Key = "booking_id";

                        cBookingActivity.InsertAutoActivity(BookingActivityType.Confirminput, intBookingId);
                       
                        break;
                }
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = ID;
                cmd.Parameters.Add("@confirm_cat_id", SqlDbType.TinyInt).Value = bytConfirmCat;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = dDateSubmit;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@confirm_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                confirmId = (int)cmd.Parameters["@confirm_id"].Value;

            }

            //#Staff_Activity_Log==========================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Booking, StaffLogActionType.Insert, StaffLogSection.Booking, intBookingId,
                tbl_to_Log, Key+",confirm_cat_id,date_submit", "confirm_id", confirmId);
            //============================================================================================================================
            return ret;
        }

        public bool UpdateBookingProductConfirmCheckInTimeRollBack(int intBookingId, int? intBookingProduct_Id, byte bytConfirmCat)
        {
            DateTime dDateSubmit = DateTime.Now.Hotels2ThaiDateTime();
            string tbl_to_Log = string.Empty;
            string Key = string.Empty;
            int ID = 0;
            int ret = 0;
            //int confirmId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string query = string.Empty;
                BookingActivityDisplay cBookingActivity = new BookingActivityDisplay();
                switch (bytConfirmCat)
                {

                    //Print Reciept
                    case 3:
                    //Avaliable
                    case 8:
                    //Confirm Fax
                    case 9:
                    //Payment To Supplier
                    case 10:
                    //Confirm Check in
                    case 11:
                        query = "DELETE FROM tbl_booking_product_confirm WHERE booking_product_id=@Id AND  confirm_cat_id=@confirm_cat_id";
                        tbl_to_Log = "tbl_booking_product_confirm";
                        ID = (int)intBookingProduct_Id;
                        Key = "booking_product_id";
                        break;
                    //Confirm Open
                    case 4:
                    //Confirm Voucher
                    case 2:
                         query = "DELETE FROM tbl_booking_confirm WHERE booking_id=@Id AND confirm_cat_id=@confirm_cat_id";
                        tbl_to_Log = "tbl_booking_confirm";
                        ID = intBookingId;
                        Key = "booking_id";
                        //auto activity
                        cBookingActivity.InsertAutoActivity(BookingActivityType.ConfirmVoucher_rollback, intBookingId);
                        break;
                    //Confirm Completed
                    case 5:
                    //Confirm INput
                    case 18:
                        query = "DELETE FROM tbl_booking_confirm WHERE booking_id=@Id AND confirm_cat_id=@confirm_cat_id";
                        tbl_to_Log = "tbl_booking_confirm";
                        ID = intBookingId;
                        Key = "booking_id";

                        //auto activity
                        cBookingActivity.InsertAutoActivity(BookingActivityType.Confirminput_rollback, intBookingId);

                        break;
                }
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = ID;
                cmd.Parameters.Add("@confirm_cat_id", SqlDbType.TinyInt).Value = bytConfirmCat;
                
                cn.Open();
                ret = ExecuteNonQuery(cmd);
               

            }
            return (ret == 1);
           
            
        }
    }
}