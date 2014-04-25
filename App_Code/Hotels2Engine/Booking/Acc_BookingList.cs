using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;


/// <summary>
/// Summary description for BookingItem
/// </summary>
/// 

namespace Hotels2thailand.Booking
{
    public class Acc_BookingList : Hotels2BaseClass
    {
        public int BookingID { get; set; }
       
        public int BookingProductId { get; set; }
        public string BookingName { get; set; }
        //public string CusName { get; set; }
        //public string CusMail { get; set; }
        public byte PaymentTypeID { get; set; }
        public string ProductTitle { get; set; }
        public DateTime BookingReceive { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public short SupplierId { get; set; }

        public byte HotelManageID { get; set; }
        public byte SaleID { get; set; }
        public byte? ComCat { get; set; }
        public decimal? ComVal { get; set; }
        public byte SupplierCatId { get; set; }
        public string SupplierTitle { get; set; }
        //public short SupplierPaymentType { get; set; }
        public bool Status { get; set; }
        //public string BookingStatusTitle { get; set; }
        //public string BookingProductStatusTitle { get; set; }
        public string PriceSales { get; set; }
        //public decimal PriceSupplier { get; set; }

        public decimal Payment { get; set; }
        public decimal Settle { get; set; }
        //public int Open_Order { get; set; }
        //public int Check_Available { get; set; }
        //public int Fax_Return { get; set; }
        
        //public int Check_In { get; set; }
        public int Order_Complete { get; set; }
        public int Confirm_input { get; set; }
        public int Confirm_Open { get; set; }
        public int Payment_to_supplier { get; set; }

        //public int Receive_Receipt { get; set; }

        public string StatusTitle { get; set; }

        public bool BookingStatus { get; set; }
        //public int? BookingAffsiteID { get; set; }
        //public bool IsExtranet { get; set; }

        public short CountryId { get; set; }
        public short CountryIdFromIP { get; set; }
        public byte BookingLang { get; set; }
        public string GateWay { get; set; }
        //public byte? SupplierDayPayment { get; set; }
        //public int CheckinDueDate { get; set; }
        public int BookingHotelID { get; set; }
        public decimal? PriceDeposit { get; set; }


        public List<object> GetBookingListOrderCenter(short bookingStatus, short bookingProductStatus, byte queryType, int PageActive, string OrderBy, int intProductId)
        {
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_normalList", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = queryType;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = OrderBy;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetBookingListOrderCenter_bhtManage(short bookingStatus, short bookingProductStatus, byte queryType, int PageActive, string OrderBy)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_acc_order_booking_list_normalList_bhtmanage", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = queryType;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = OrderBy;
               // cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int GetBookingListOrderCenter_Count(short bookingStatus, short bookingProductStatus, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_normalList", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }
        public int GetBookingListOrderCenter_Count_bhtManage(short bookingStatus, short bookingProductStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_acc_order_booking_list_normalList_bhtmanage", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                //cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public ArrayList GetBookingListOrderCenter_SumPrice(short bookingStatus, short bookingProductStatus, int intProductId)
        {
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                ArrayList arrPriceSum = new ArrayList();
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_normalList", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 3;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    arrPriceSum.Add(reader[0]);
                    arrPriceSum.Add(reader[1]);
                }
                return arrPriceSum;
            }
        }

        public ArrayList GetBookingListOrderCenter_SumPrice_bhtMange(short bookingStatus, short bookingProductStatus)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                ArrayList arrPriceSum = new ArrayList();
                SqlCommand cmd = new SqlCommand("bk_acc_order_booking_list_normalList_bhtmanage", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 3;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                //cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    arrPriceSum.Add(reader[0]);
                    arrPriceSum.Add(reader[1]);
                }
                return arrPriceSum;
            }
        }

        //------------------------
        public List<object> GetBookingListOrderCenterDuplicate(short bookingStatus, short bookingProductStatus, byte queryType, int PageActive, string OrderBy)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_duplicate_booking", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = queryType;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = OrderBy;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int GetBookingListOrderCenterDuplicate_Count(short bookingStatus, short bookingProductStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_duplicate_booking", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public ArrayList GetBookingListOrderCenterDuplicate_SumPrice(short bookingStatus, short bookingProductStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                ArrayList arrPriceSum = new ArrayList();
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_duplicate_booking", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 3;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    arrPriceSum.Add(reader[0]);
                    arrPriceSum.Add(reader[1]);
                }
                return arrPriceSum;
            }
        }
        //-----------------


        //--------------------------
        public List<object> GetBookingList_Status_OrderOpen(short bookingStatus, short bookingProductStatus, byte queryType, int PageActive, string OrderBy)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_openorder", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = queryType;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = OrderBy;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int GetBookingList_Status_OrderOpen_Count(short bookingStatus, short bookingProductStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_openorder", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public ArrayList GetBookingList_Status_OrderOpen_SumPrice(short bookingStatus, short bookingProductStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                ArrayList arrPriceSum = new ArrayList();
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_openorder", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 3;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    arrPriceSum.Add(reader[0]);
                    arrPriceSum.Add(reader[1]);
                }
                return arrPriceSum;
            }
        }

        //-------------------------
        public List<object> GetBookingList_Status_CheckinDueDate(short bookingStatus, short bookingProductStatus, byte queryType, int PageActive, int intdueDate, string OrderBy)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_booknow_due_date", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = queryType;
                cmd.Parameters.Add("@CoundateCheck", SqlDbType.Int).Value = intdueDate;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = OrderBy;

                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int GetBookingList_Status_CheckinDueDate_Count(short bookingStatus, short bookingProductStatus, int intdueDate)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_booknow_due_date", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@CoundateCheck", SqlDbType.Int).Value = intdueDate;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public ArrayList GetBookingList_Status_CheckinDueDate_SumPrice(short bookingStatus, short bookingProductStatus, int intdueDate)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                ArrayList arrPriceSum = new ArrayList();
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_booknow_due_date", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 3;
                cmd.Parameters.Add("@CoundateCheck", SqlDbType.Int).Value = intdueDate;
                cmd.Parameters.Add("@b_status_id", SqlDbType.SmallInt).Value = bookingStatus;
                cmd.Parameters.Add("@bp_status_id", SqlDbType.SmallInt).Value = bookingProductStatus;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    arrPriceSum.Add(reader[0]);
                    arrPriceSum.Add(reader[1]);
                }
                return arrPriceSum;
            }
        }
        //--------------------------
        public List<object> GetBookingListHistory(int bookingId, string Email, string strNamefull, byte queryType, byte PageActive, string OrderBy, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_history", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = queryType;
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = bookingId;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@name_full", SqlDbType.NVarChar).Value = strNamefull;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = OrderBy;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetBookingListHistory_BHTmange(int bookingId, string Email, string strNamefull, byte queryType, byte PageActive, string OrderBy)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_history_bhtmanage", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = queryType;
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = bookingId;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@name_full", SqlDbType.NVarChar).Value = strNamefull;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = OrderBy;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        

        public int GetBookingListHistory_Count(int bookingId, string Email, string strNamefull, byte queryType, byte PageActive, string OrderBy, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                ArrayList arrPriceSum = new ArrayList();
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_history", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = bookingId;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@name_full", SqlDbType.NVarChar).Value = strNamefull;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public int GetBookingListHistory_Count_BHTmange(int bookingId, string Email, string strNamefull, byte queryType, byte PageActive, string OrderBy)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                ArrayList arrPriceSum = new ArrayList();
                SqlCommand cmd = new SqlCommand("bk_order_booking_list_history_bhtmanage", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = bookingId;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@name_full", SqlDbType.NVarChar).Value = strNamefull;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public List<object> GetBookingList_member_history(int PageActive, string strOrderBy, int intCusId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("member_booking_history", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = intCusId;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = strOrderBy;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int GetBookingList_member_history_count(int intCusId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("member_booking_history", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = intCusId;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public List<object> GetBookingList_member_pedding(int PageActive,string strOrderBy, int intCusId,int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("member_pendding_booking", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = PageActive;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 1;
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = intCusId;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = strOrderBy;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int GetBookingList_member_pedding_count(int intCusId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("member_pendding_booking", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@queryType", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = intCusId;
                cmd.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "date_submit";
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

    }
}