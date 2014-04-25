using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Booking;
/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{

    public class BookingPhone
    {
        private LinqBookingDataContext dcPhone = new LinqBookingDataContext();

        public int PhoneID { get; set; }
        public byte CategoryID { get; set; }
        public int BookingID { get; set; }
        public string CodeCountry { get; set; }
        public string CodeLocal { get; set; }
        public string NumberPhone { get; set; }

        public BookingPhone()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int Insert(BookingPhone data) {
            tbl_booking_phone phone = new tbl_booking_phone { 
            //phone_id=data.PhoneID,
            cat_id=data.CategoryID,
            booking_id=data.BookingID,
            code_country=data.CodeCountry,
            code_local=data.CodeLocal,
            number_phone=data.NumberPhone
            };
            dcPhone.tbl_booking_phones.InsertOnSubmit(phone);
            dcPhone.SubmitChanges();
            return phone.phone_id;
        }
    }
}