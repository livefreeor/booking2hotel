using System;
using System.Collections.Generic;
using System.Web;
using Hotels2thailand.Booking;

using System.Data.SqlClient;
using System.Data;
using System.Collections;

using System.Text;


/// <summary>
/// Summary description for BookingItemShow
/// </summary>
/// 

namespace Hotels2thailand.Booking
{
    public class BookingdetailDisplay : Hotels2BaseClass
    {
        public int BookingId { get; set; }
        public int BookingHotelId { get; set; }
        public int BookingProductId { get; set; }
        public byte CountryId { get; set; }
        public short CountryIdBytrackIp { get; set; }
        public string CountryTitle { get; set; }
        public string CountryTitleBytrackIP { get; set; }
        public short StatusId { get; set; }
        public short StatusAffId { get; set; }
        public int? CusId { get; set; }
        public byte LangId { get; set; }
        public int? AffSiteId { get; set; }
        public byte PrefixId { get; set; }
        public string PrefixTitle { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string F_arr_No { get; set; }

      
        public DateTime? F_arr_Time { get; set; }
        public string F_Dep_No { get; set; }
        public DateTime? F_Dep_Time { get; set; }
        public DateTime DateBookingREceive { get; set; }
        public DateTime DateModify { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public string CommentAff { get; set; }
        public string RefIP { get; set; }
        public byte PaymentTypeID { get; set; }

        public short? Deposit { get; set; }
        public byte? DepositCat { get; set; }
        public decimal? PriceDeposit { get; set; }
        public string HOtelIdNo { get; set; }

        public string BookingPhone { get; set; }
        public string BookingMobile { get; set; }
        public string BookingFax { get; set; }
        public string StatusTitle { get; set; }
        public string StatusAffTitle { get; set; }

        public byte StatusExtranetId { get; set; }
        
        public DateTime? ConfirmOpen { get; set; }
        public DateTime? ConfirmVoucher { get; set; }
        public DateTime? ConfirmCOmpleted { get; set; }
        public DateTime? ConfirmInput { get; set; }
        
        //public int AckCountConfirm { get; set; }
        //public int AckCountCancel { get; set; }
        public bool ActiveExtranet { get; set; }
        public int ExtranetProductId { get; set; }

        public int? BookingAgencyID { get; set; }

        //public string ItemShowCondition(int intBookingProductId, short staffId)
        //{

        //}
        public int GetBookingHotelBybookingIDANdProductId(int intBookingID,int ProductId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT booking_hotel_id FROM tbl_booking_hotels WHERE booking_id = @booking_id AND product_id = @product_id", cn);
                
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cn.Open();
                return (int)ExecuteScalar(cmd);

            }
        }

        public BookingdetailDisplay GetBookingDetailListByBookingId(int intBookingID)
        {
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("bk_order_booking_detail", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                if(reader.Read())
                    return (BookingdetailDisplay)MappingObjectFromDataReader(reader);
                else
                return null;
               
            }
        }

        public DateTime? GetBookingProductConfirmPaymentToSupplier(int intBookingProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT MAX(bpcon.date_submit) FROM tbl_booking_product_confirm bpcon WHERE bpcon.booking_product_id = @booking_product_id AND bpcon.confirm_cat_id = 10 AND bpcon.status = 1", cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cn.Open();

                return (ExecuteScalar(cmd) == DBNull.Value ? (DateTime?)null : (DateTime)ExecuteScalar(cmd));
            }
        }

        public byte GetBookingLang(int intBookingID)
        {
            byte bytLangId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT lang_id FROM tbl_booking WHERE booking_id =@booking_id",cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    bytLangId = (byte)reader[0];
                }
                return bytLangId;
            }
        }

        public int CheckBookingStatus(int intBookingID, string BookingMail)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(COUNT(booking_id),0) FROM tbl_booking WHERE booking_id=@booking_id AND email=@email", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = BookingMail;
                cn.Open();
                int ret = (int)ExecuteScalar(cmd);
                return ret;
            }
        }

        public bool UpdateHOtelBookingNumber(int intBookingId, string StrHotelNo)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking SET hotel_order_no=@hotel_order_no WHERE booking_id = @booking_id", cn);
                cmd.Parameters.Add("@hotel_order_no", SqlDbType.NVarChar).Value = StrHotelNo;
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                return (ExecuteNonQuery(cmd) == 1);
            }
        }

        public string[] GetBookingPhoneByBookingId(int intBookingID, byte PhoneCat)
        {
            StringBuilder query = new StringBuilder();
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT bf.code_country , bf.code_local , bf.number_phone FROM tbl_booking_phone bf WHERE bf.booking_id=@booking_id  AND bf.cat_id = @cat_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = PhoneCat;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    string[] Phone = {reader[0].ToString(),reader[1].ToString(),reader[2].ToString()};
                    return Phone;
                }
                else
                    return null;

            }
        }

        public bool UpdateBookingStatusExtranet(int intBookingID, byte bytStatus)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking SET status_extranet_id=@status_extranet_id WHERE booking_id=@booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cmd.Parameters.Add("@status_extranet_id", SqlDbType.TinyInt).Value = bytStatus;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
                return (ret == 1);

            }
        }

        // fucntion Close Booking // Close = 1 , Open = 0 
        public bool UpdateBookingstatus(int intBookingID)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking SET status=@status WHERE booking_id=@booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
                return (ret == 1);

            }
        }
        public bool UpdateBookingstatus(int intBookingID, bool bolStatus)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking SET status=@status WHERE booking_id=@booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
                return (ret == 1);

            }
        }

        public int INsertBookingPhoneByBookingIdAndPhoneCat(int intBookingID, byte PhoneCat, string CountryCode, string LocalCode, string Number)
        {
            
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_booking_phone (booking_id,cat_id,code_country,code_local,number_phone) VALUES (@booking_id,@cat_id,@code_country,@code_local,@number_phone);SET @phone_id=SCOPE_IDENTITY();", cn);

                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;

                //cmd.Parameters.AddWithValue("@booking_id", intBookingID);

                //cmd.Parameters.AddWithValue("@booking_id", DBNull.Value);



                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = PhoneCat;
                cmd.Parameters.Add("@code_country", SqlDbType.VarChar).Value = CountryCode;
                cmd.Parameters.Add("@code_local", SqlDbType.VarChar).Value = LocalCode;
                cmd.Parameters.Add("@number_phone", SqlDbType.VarChar).Value = Number;

                cmd.Parameters.Add("@phone_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

               
                ret = ExecuteNonQuery(cmd);

                int intff = (int)cmd.Parameters["@phone_id"].Value;
                return intff;

            }
        }

       

        public bool UpdatePhoneBooking(int intBookingID, byte PhoneCat, string CountryCode, string LocalCode, string Number)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_phone SET  code_country=@code_country , code_local=@code_local, number_phone=@number_phone WHERE booking_id=@booking_id  AND cat_id = @cat_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = PhoneCat;
                cmd.Parameters.Add("@code_country", SqlDbType.VarChar).Value = CountryCode;
                cmd.Parameters.Add("@code_local", SqlDbType.VarChar).Value = LocalCode;
                cmd.Parameters.Add("@number_phone", SqlDbType.VarChar).Value = Number;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                return (ret == 1);
                
            }
        }

        public bool UpdateBookingDetail(int intBookingID, string strBookingName, string strEmail, byte bytCountryID, string strFlightArrNum, DateTime dFlightArrTime, string strFlightDepNum, DateTime dFlightDepTime, byte bytPrefixId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking SET  name_full=@name_full , email=@email, country_id=@country_id , flight_arrival_number=@flight_arrival_number, flight_departure_number=@flight_departure_number, flight_arrival_time=@flight_arrival_time, flight_departure_time=@flight_departure_time, date_modify=@date_modify, prefix_id=@prefix_id WHERE booking_id=@booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cmd.Parameters.Add("@name_full", SqlDbType.NVarChar).Value = strBookingName;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryID;
                cmd.Parameters.Add("@flight_arrival_number", SqlDbType.VarChar).Value = strFlightArrNum;
                cmd.Parameters.Add("@flight_departure_number", SqlDbType.VarChar).Value = strFlightDepNum;
                cmd.Parameters.Add("@flight_arrival_time", SqlDbType.SmallDateTime).Value = dFlightArrTime;
                cmd.Parameters.Add("@flight_departure_time", SqlDbType.SmallDateTime).Value = dFlightDepTime;
                cmd.Parameters.Add("@date_modify", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@prefix_id", SqlDbType.TinyInt).Value = bytPrefixId;
                //cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = PhoneCat;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                return (ret == 1);

            }
        }
        
    }
}