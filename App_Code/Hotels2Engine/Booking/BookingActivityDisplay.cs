using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Hotels2thailand.Booking;

/// <summary>
/// Summary description for BookingActivityShow
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public enum BookingActivityType : int
    {
        Confirminput = 1,
        Confirminput_rollback = 2,
        resubmit = 3,
        Confirmpayment = 4,
        Confirmpayment_rollback = 5,
        ConfirmVoucher = 6,
        ConfirmVoucher_rollback = 7,
        updateStatus = 8,
        CloseBooking= 9,
        OpenBooking = 10,
        CloseBookingAndreview = 11,
        SendVoucherBy_System = 12,
        ChanngPaymentBankId_By_system =13,
        ConfirmSettle = 14,
        ConfirmSettle_rollback = 15,
        ConfirmPaymentSup =16,
        ConfirmPaymentSup_rollback = 17
      
      

    }

    public class BookingActivityDisplay : Hotels2BaseClass
    {
        public int ActivityID { get; set; }
        public int BookingId { get; set; }
        public int? BookingProductId { get; set; }
        public short StaffId { get; set; }
        public string StaffName { get; set; }
        public string Detail { get; set; }
        public DateTime? DateActivity { get; set; }
        public bool IsPublic { get; set; }

     

        public int InsertAutoActivity(BookingActivityType type, int bookingId, string Param = "", string Param2 = "")
        {
            string txtresult = string.Empty;
            
            BookingPaymentDisplay cBookingPayment = new BookingPaymentDisplay();
            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();

            switch (type)
            {
                case BookingActivityType.Confirminput:
                    txtresult = "Confirm input to hotel system : Hotel Booking ID = " + cBookingDetail.GetBookingDetailListByBookingId(bookingId).HOtelIdNo;
                    break;
                case BookingActivityType.Confirminput_rollback:
                    txtresult = "Confirm input is cancelled.";
                    break;
                case BookingActivityType.resubmit:
                    txtresult = "Send Resubmit with Payment id =" + cBookingPayment.intGetBookingPaymentBank(int.Parse(Param)); 
                    break;
                case BookingActivityType.Confirmpayment:

                    txtresult = "Confirm Payment with Payment id=" + cBookingPayment.intGetBookingPaymentBank(int.Parse(Param)); 
                    break;
                case BookingActivityType.Confirmpayment_rollback:
                    txtresult = "Confirmation of payment with payment id= " + cBookingPayment.intGetBookingPaymentBank(int.Parse(Param)) + " is cancelled.";
                    break;
                case BookingActivityType.ConfirmSettle:
                    txtresult = "Confirm Settle with Payment id=" + cBookingPayment.intGetBookingPaymentBank(int.Parse(Param));
                    break;
                case BookingActivityType.ConfirmSettle_rollback:
                    txtresult = "Confirmation of Settle with payment id= " + cBookingPayment.intGetBookingPaymentBank(int.Parse(Param)) + " is cancelled.";
                    break;
                case BookingActivityType.ConfirmVoucher:
                    txtresult = "Send voucher confirmation";
                    break;
                case BookingActivityType.ConfirmPaymentSup:
                    txtresult = "Set Confirm Payment To Hotel by BHT's Account Dep.";
                    break;
                case BookingActivityType.ConfirmPaymentSup_rollback:
                    txtresult = "Set Confirm Payment To Hotel is cancelled. by BHT's Account Dep.";
                    break;
                case BookingActivityType.ConfirmVoucher_rollback:
                    txtresult = "Sending voucher confirmation is cancelled.";
                    break;
                case BookingActivityType.updateStatus:
                    txtresult = "Update Booking Status to: \"" + cBookingDetail.GetBookingDetailListByBookingId(bookingId).StatusTitle + "\"";
                    break;
                case BookingActivityType.CloseBooking:
                    txtresult = "Remove Booking";
                    break;
                case BookingActivityType.OpenBooking:
                    txtresult = "Retrieve Booking";
                    break;
                case BookingActivityType.CloseBookingAndreview:
                    txtresult = "Send email review to customer and remove booking.";
                    break;
                case BookingActivityType.SendVoucherBy_System:
                    txtresult = "Send voucher confirmation";
                    break;
                case BookingActivityType.ChanngPaymentBankId_By_system:
                    txtresult = "Payment ID " + Param + " was changed to Payment ID " + Param2;
                    break;
            }

           return this.InsertNewActivityBooking(bookingId, txtresult);
        }

        //string sqlBookingList;
        //string strBookingListTable;

        public List<object> GetActivityBookingList(int intBookingId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("Select ba.activity_id, ba.booking_id,ba.booking_product_id, ba.staff_id");
            query.Append(" ,(Select ss.title from tbl_staff ss where ss.staff_id = ba.staff_id) staff");
            query.Append(" , ba.detail, ba.date_submit from tbl_booking_activity ba where ba.booking_product_id IS NULL And ba.booking_id = @booking_id AND ba.is_public =1 ORDER BY ba.date_submit DESC, ba.activity_id DESC");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetActivityBookingList_account(int intBookingId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("Select ba.activity_id, ba.booking_id,ba.booking_product_id, ba.staff_id");
            query.Append(" ,(Select ss.title from tbl_staff ss where ss.staff_id = ba.staff_id) staff");
            query.Append(" , ba.detail, ba.date_submit from tbl_booking_activity ba where ba.booking_product_id IS NULL And ba.booking_id = @booking_id  ORDER BY ba.date_submit DESC, ba.activity_id DESC");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetActivityBookingProductList(int intBookingProductId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("select ba.activity_id, ba.booking_id,ba.booking_product_id, ba.staff_id");
            query.Append(" ,(select ss.title from tbl_staff ss where ss.staff_id = ba.staff_id) staff");
            query.Append(" , ba.detail, ba.date_submit from tbl_booking_activity ba where ba.booking_product_id = @booking_product_id AND ba.is_public = 1 ORDER BY ba.date_submit");
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int InsertNewActivityBooking(int intBookingId, string strDetail)
        {
            Staffs.StaffSessionAuthorize cstaffId = new Staffs.StaffSessionAuthorize();
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_booking_activity (booking_id,staff_id,detail,date_submit) VALUES(@booking_id,@staff_id,@detail,@date_submit);SET @activity_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = cstaffId.CurrentStaffId;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime() ;
                cmd.Parameters.Add("@activity_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                int iden = (int)cmd.Parameters["@activity_id"].Value;
                return ret;
            }
        }

        public int InsertNewActivityBookingProduct(int intBookingId, int intBookingProduct, string strDetail)
        {
            Staffs.StaffSessionAuthorize cstaffId = new Staffs.StaffSessionAuthorize();
            int ret = 0;
             
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_booking_activity (booking_id,booking_product_id,staff_id,detail,date_submit) VALUES(@booking_id,@booking_product_id,@staff_id,@detail,@date_submit);SET @activity_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProduct;
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = cstaffId.CurrentStaffId;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@activity_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                int iden = (int)cmd.Parameters["@activity_id"].Value;
            }
             
             return ret;  
        }
    }
}