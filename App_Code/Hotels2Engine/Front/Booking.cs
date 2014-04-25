using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.LinqProvider.Booking;
/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class Booking:Hotels2BaseClass
    {

        public int BookingID { get; set; }
        public byte CountryID { get; set; }
        public short StatusID { get; set; }
        public short StatusAffiliateID { get; set; }
        public int CustomerID { get; set; }
        public byte LanguageID { get; set; }
        public Nullable<int> AffiliateSiteID { get; set; }
        public byte PrefixID { get; set; }
        public string NameFull { get; set; }
        public string Email { get; set; }
        public string FlightArrivalNumber { get; set; }
        public string FlightDepartureNumber { get; set; }
        public Nullable<DateTime> FlightArrivalTime { get; set; }
        public Nullable<DateTime> FlightDepartureTime { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateModify { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public string CommentAffiliate { get; set; }
        public string RefererIP { get; set; }
        public byte PaymentTypeID { get; set; }
        public bool IsExtranet { get; set; }
        public short refCountry { get; set; }
        public short Deposit { get; set; }
        public byte DepositCategory { get; set; }
        public decimal DepositPrice { get; set; }
        public bool IsMember { get; set; }

        public int? AgentcyId { get; set; }

        private LinqBookingDataContext dcBooking = new LinqBookingDataContext();
        public Booking()
        {
            AffiliateSiteID = null;
            StatusAffiliateID = 1;
            FlightArrivalTime = null;
            FlightDepartureTime = null;
           
            Status = true;
            DateSubmit = DateTime.Now;
            DateModify = DateTime.Now;

        }

        public int Insert(Booking data)
        {
            int ret = 0;
            string ipRefer = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            short countryRef = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string[] ipReferSplit = ipRefer.Split('.');
                string sqlCommand = "spIPtoCountry " + ipReferSplit[0] + "," + ipReferSplit[1] + "," + ipReferSplit[2] + "," + ipReferSplit[3];
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                countryRef = (short)cmd.ExecuteScalar();
            }

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string command="insert into tbl_booking(country_id,status_id,status_affiliate_id,cus_id,lang_id,aff_site_id,prefix_id,name_full,email,flight_arrival_number,flight_departure_number,flight_arrival_time,flight_departure_time,date_submit,date_modify,status,comment,comment_affiliate,refer_ip,payment_type_id,is_extranet,refer_country,deposit,deposit_cat_id,price_deposit,is_member,agency_id)";
                command = command + " values(@country_id,@status_id,@status_affiliate_id,@cus_id,@lang_id,@aff_site_id,@prefix_id,@name_full,@email,@flight_arrival_number,@flight_departure_number,@flight_arrival_time,@flight_departure_time,@date_submit,@date_modify,@status,@comment,@comment_affiliate,@refer_ip,@payment_type_id,@is_extranet,@refer_country,@deposit,@deposit_cat_id,@price_deposit,@is_member,@agency_id); SET @booking_id = SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(command, cn);

                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = data.CountryID;
                cmd.Parameters.Add("@status_id", SqlDbType.SmallInt).Value = data.StatusID;
                cmd.Parameters.Add("@status_affiliate_id", SqlDbType.SmallInt).Value = data.StatusAffiliateID;
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = data.CustomerID;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = data.LanguageID;
                if (data.AffiliateSiteID.HasValue)
                {
                    cmd.Parameters.Add("@aff_site_id", SqlDbType.Int).Value = data.AffiliateSiteID;
                }
                else {
                    cmd.Parameters.Add("@aff_site_id", SqlDbType.Int).Value = DBNull.Value;
                }
                
                cmd.Parameters.Add("@prefix_id", SqlDbType.TinyInt).Value = data.PrefixID;
                cmd.Parameters.Add("@name_full", SqlDbType.NVarChar).Value = data.NameFull;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = data.Email;
                
                
                

                if (!string.IsNullOrEmpty(data.FlightArrivalNumber))
                {
                    cmd.Parameters.Add("@flight_arrival_number", SqlDbType.VarChar).Value = data.FlightArrivalNumber;
                }else{
                    cmd.Parameters.Add("@flight_arrival_number", SqlDbType.VarChar).Value = DBNull.Value;
                }
                if (!string.IsNullOrEmpty(data.FlightDepartureNumber))
                {
                    cmd.Parameters.Add("@flight_departure_number", SqlDbType.VarChar).Value = data.FlightDepartureNumber;
                }
                else
                {
                    cmd.Parameters.Add("@flight_departure_number", SqlDbType.VarChar).Value = DBNull.Value;
                }
                if (data.FlightArrivalTime.HasValue)
                {
                    cmd.Parameters.Add("@flight_arrival_time", SqlDbType.DateTime).Value = data.FlightArrivalTime;
                
                }else{
                    cmd.Parameters.Add("@flight_arrival_time", SqlDbType.DateTime).Value = DBNull.Value;
                }

                if (data.FlightDepartureTime.HasValue)
                {
                    cmd.Parameters.Add("@flight_departure_time", SqlDbType.DateTime).Value = data.FlightDepartureTime;
                }else{
                    cmd.Parameters.Add("@flight_departure_time", SqlDbType.DateTime).Value = DBNull.Value;
                }
                
                cmd.Parameters.Add("@date_submit", SqlDbType.DateTime).Value = data.DateSubmit;
                cmd.Parameters.Add("@date_modify", SqlDbType.DateTime).Value = data.DateModify;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = data.Status;

                if (!string.IsNullOrEmpty(data.Comment))
                {
                    cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = data.Comment;
                }else{
                    cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (!string.IsNullOrEmpty(data.CommentAffiliate))
                {
                    cmd.Parameters.Add("@comment_affiliate", SqlDbType.NVarChar).Value = data.CommentAffiliate;
                }
                else
                {
                    cmd.Parameters.Add("@comment_affiliate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                
                cmd.Parameters.Add("@refer_ip", SqlDbType.VarChar).Value = data.RefererIP;
                cmd.Parameters.Add("@payment_type_id", SqlDbType.TinyInt).Value = data.PaymentTypeID;
                cmd.Parameters.Add("@is_extranet", SqlDbType.Bit).Value = data.IsExtranet;
                cmd.Parameters.Add("@refer_country", SqlDbType.SmallInt).Value = countryRef;
                cmd.Parameters.Add("@deposit", SqlDbType.SmallInt).Value = data.Deposit;
                cmd.Parameters.Add("@deposit_cat_id", SqlDbType.TinyInt).Value = data.DepositCategory;
                cmd.Parameters.Add("@price_deposit",SqlDbType.Money).Value = data.DepositPrice;
                cmd.Parameters.Add("@is_member",SqlDbType.Bit).Value = data.IsMember;


                if (data.AgentcyId.HasValue)
                {
                    cmd.Parameters.Add("@agency_id", SqlDbType.Int).Value = data.AgentcyId;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@agency_id", DBNull.Value);
                }

                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@booking_id"].Value;
                
            }

            return ret;
        }

        //public int Insert(Booking data)
        //{
        //    tbl_booking booking = new tbl_booking
        //    {
        //        country_id = data.CountryID,
        //        status_id = data.StatusID,
        //        status_affiliate_id = data.StatusAffiliateID,
        //        cus_id = data.CustomerID,
        //        lang_id = data.LanguageID,
        //        aff_site_id = data.AffiliateSiteID,
        //        prefix_id = data.PrefixID,
        //        name_full = data.NameFull,
        //        email = data.Email,
        //        flight_arrival_number = data.FlightArrivalNumber,
        //        flight_departure_number = data.FlightDepartureNumber,
        //        flight_arrival_time = data.FlightArrivalTime,
        //        flight_departure_time = data.FlightDepartureTime,
                
        //        date_submit = data.DateSubmit,
        //        date_modify = data.DateModify,
        //        status = data.Status,
        //        comment = data.Comment,
        //        comment_affiliate = data.CommentAffiliate,
        //        refer_ip=data.RefererIP,
        //        //PaymentTypeID=data.PaymentTypeID
        //    };
        //    dcBooking.tbl_bookings.InsertOnSubmit(booking);
        //    dcBooking.SubmitChanges();
        //    return booking.booking_id;
        //}

        public bool Update(Booking data)
        {
            tbl_booking RsBooking = dcBooking.tbl_bookings.Single(b=>b.booking_id==data.BookingID);
              RsBooking.country_id=data.CountryID;
              RsBooking.status_id=data.StatusID;
              RsBooking.status_affiliate_id=data.StatusAffiliateID;
              RsBooking.cus_id=data.CustomerID;
              RsBooking.lang_id=data.LanguageID;
              RsBooking.aff_site_id=data.AffiliateSiteID;
              RsBooking.prefix_id=data.PrefixID;
              RsBooking.name_full=data.NameFull;
              RsBooking.email=data.Email;
              RsBooking.flight_arrival_number=data.FlightArrivalNumber;
              RsBooking.flight_departure_number=data.FlightDepartureNumber;
              RsBooking.flight_arrival_time=data.FlightArrivalTime;
              RsBooking.flight_departure_time=data.FlightDepartureTime;
              
              RsBooking.date_modify=data.DateModify;
              RsBooking.status=data.Status;
              RsBooking.comment=data.Comment;
              RsBooking.comment_affiliate=data.CommentAffiliate;
              //RsBooking.PaymentTypeID = data.PaymentTypeID;
              dcBooking.SubmitChanges();
              return true;
        }

        public string BookingEncode(int bookingID)
        {
            return "";
        }
        public string BookingDecode(string bookEncode)
        {
            return "";
        }
        
    }
}