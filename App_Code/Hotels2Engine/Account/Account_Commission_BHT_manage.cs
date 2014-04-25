using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Account_Commission_BHT_manage
/// </summary>
/// 

namespace Hotels2thailand.Account
{
    public class Com_hotel_list:Hotels2BaseClass
    {
        public int ProductId { get; set; }
        public string  ProductCode { get; set; }
       // public byte ManageId { get; set; }
        public string ProductTitle { get; set; }
        public int BookingDue { get; set; }
        public int BookingNotDue { get; set; }
       

        public Com_hotel_list()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public IList<object> GetComHotelLsit()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_account_hotel_payment_list", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

       
        
        
    }

    public class Com_Booking_list : Hotels2BaseClass
    {
        public int BookingId { get; set; }
        public int BookingHotelId { get; set; }
        public string CustomerName { get; set; }
        //public string RoomTitle { get; set; } 
        public DateTime DateCheckin { get; set; }
        public DateTime DateCheckOut { get; set; }
        public byte ManageId { get; set; }
        public byte ComCat { get; set; }
        public decimal ComVal { get; set; }
        public byte DuePayment { get; set; }
        public short BookingStatusID { get; set; }
        public string BookingStatusTitle { get; set; }

       
        //public decimal  Price { get; set; }
        
        //public decimal CommissionPrice { get; set; }
        //public decimal PricePayment { get; set; }


        public Com_Booking_list()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        

        public IList<object> GetBookingToPaymentList(int intProuctId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_account_booking_list_payment", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProuctId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> GetBookingToPaymentList_hotel_manage(int intProuctId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_account_booking_list_payment_hotel_manage", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProuctId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public IList<object> GetBookingToPaymentList(string BookingList)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_account_bookingList_payment_sel", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@bookingList", SqlDbType.VarChar).Value = BookingList;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> GetBookingToPaymentList_hotel_manage(string BookingList, string strProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_account_bookingList_payment_hotel_manage_sel", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@bookingList", SqlDbType.VarChar).Value = BookingList;
                cmd.Parameters.Add("@product_id", SqlDbType.VarChar).Value = strProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        


    }
}