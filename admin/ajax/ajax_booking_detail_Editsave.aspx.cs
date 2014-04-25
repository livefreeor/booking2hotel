using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_detail_Editsave : System.Web.UI.Page
    {
        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //Response.Write(Request.Form["booking_email"]);
                Response.Write(UpdateBookingDetail());
                Response.Flush();
            }
        }

        public bool UpdateBookingDetail()
        {
            bool result = true;

            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            int BookingId = int.Parse(this.qBookingId);
            
            string BookingName =      Request.Form["booking_name"];
            string BookingEmail=      Request.Form["booking_email"];
            byte bytCountryID =       byte.Parse(Request.Form["booking_country"]);

            DateTime dDAteArr = DateTime.Parse(Request.Form["hd_txtDateflighArr"]);
            int ArrHrs = int.Parse(Request.Form["Arr_Hours"]);
            int ArrMins = int.Parse(Request.Form["Arr_Mins"]);

            DateTime dDateTimeArr = new DateTime(dDAteArr.Year, dDAteArr.Month, dDAteArr.Day, ArrHrs, ArrMins, 0);


            DateTime dDAteDep = DateTime.Parse(Request.Form["hd_txtDateflighDep"]);
            int DepHrs = int.Parse(Request.Form["Dep_Hours"]);
            int DepMins = int.Parse(Request.Form["Dep_Mins"]);
            byte bytCusProfix = byte.Parse(Request.Form["customer_prefix"]);
            DateTime dDateTimeDep = new DateTime(dDAteDep.Year, dDAteDep.Month, dDAteDep.Day, DepHrs, DepMins, 0);

            string FlightArrNo = Request.Form["flight_num_arr"];
            string FlightDepNo = Request.Form["flight_num_dep"];

            string PhoneCountryCode = Request.Form["booking_phone_country_code"];
            string PhoneLocalCode = Request.Form["booking_phone_local_code"];
            string PhoneNum = Request.Form["booking_phone_number"];

            string MobileCountryCode = Request.Form["booking_mobile_country_code"];
            string MobileLocalCode = Request.Form["booking_mobile_local_code"];
            string MobileNum = Request.Form["booking_mobile_number"];

            string FAxCountryCode = Request.Form["booking_fax_country_code"];
            string FAxLocalCode = Request.Form["booking_fax_local_code"];
            string FAxNum = Request.Form["booking_fax_number"];


            if (result)
                if (cBookingDetail.GetBookingPhoneByBookingId(BookingId, 1) == null)
                    result = (cBookingDetail.INsertBookingPhoneByBookingIdAndPhoneCat(BookingId, 1, PhoneCountryCode, PhoneLocalCode, PhoneNum) == 1);
                else
                    result = cBookingDetail.UpdatePhoneBooking(BookingId, 1, PhoneCountryCode, PhoneLocalCode, PhoneNum);

            if (!result)
                return false;

            if (result)
            {
                if (cBookingDetail.GetBookingPhoneByBookingId(BookingId, 2) == null)
                    result = (cBookingDetail.INsertBookingPhoneByBookingIdAndPhoneCat(BookingId, 2, MobileCountryCode, MobileLocalCode, MobileNum) == 1);
                else
                    result = cBookingDetail.UpdatePhoneBooking(BookingId, 2, MobileCountryCode, MobileLocalCode, MobileNum);
            }

            if (!result)
                return false;

            if (result)
            {
                if (cBookingDetail.GetBookingPhoneByBookingId(BookingId, 3) == null)
                    result = (cBookingDetail.INsertBookingPhoneByBookingIdAndPhoneCat(BookingId, 3, FAxCountryCode, FAxLocalCode, FAxNum) == 1);
                else
                    result = cBookingDetail.UpdatePhoneBooking(BookingId, 3, FAxCountryCode, FAxLocalCode, FAxNum);
            }


            if (!result)
                return false;

            if (result)
                result = cBookingDetail.UpdateBookingDetail(BookingId, BookingName, BookingEmail, bytCountryID, FlightArrNo, dDateTimeArr, FlightDepNo, dDateTimeDep, bytCusProfix);

            return result;    
        }
    }
}