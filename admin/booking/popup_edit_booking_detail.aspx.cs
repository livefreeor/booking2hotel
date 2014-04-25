using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class extranet_ordercenter_popup_edit_booking_detail : Hotels2BasePage
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
                PrefixName cPrefix = new PrefixName();
                dropPrefix.DataSource = cPrefix.GetPrefixAll();
                dropPrefix.DataTextField = "Title";
                dropPrefix.DataValueField = "PrefixID";
                dropPrefix.DataBind();

                Country cCountry = new Country();
                dropCountry.DataSource = cCountry.GetCountryAll();
                dropCountry.DataTextField = "Value";
                dropCountry.DataValueField = "Key";
                dropCountry.DataBind();

                arrdropHH.DataSource = this.dicGetTimEHrs(23);
                arrdropHH.DataTextField = "Value";
                arrdropHH.DataValueField = "Key";
                arrdropHH.DataBind();
                arrdropMM.DataSource = this.dicGetTimEHrs(59);
                arrdropMM.DataTextField = "Value";
                arrdropMM.DataValueField = "Key";
                arrdropMM.DataBind();
                depdropHH.DataSource = this.dicGetTimEHrs(59);
                depdropHH.DataTextField = "Value";
                depdropHH.DataValueField = "Key";
                depdropHH.DataBind();
                depdropMM.DataSource = this.dicGetTimEHrs(59);
                depdropMM.DataTextField = "Value";
                depdropMM.DataValueField = "Key";
                depdropMM.DataBind();

             


                string PhoneCountryCode = string.Empty;
                string PhoneLocalCode = string.Empty;
                string PhoneNum = string.Empty;

                string MobileCountryCode = string.Empty;
                string MobileLocalCode = string.Empty;
                string MobileNum = string.Empty;

                BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();

                string[] Phone = cBookingDetail.GetBookingPhoneByBookingId(int.Parse(this.qBookingId), 1);
                if (Phone != null)
                {
                    PhoneCountryCode = Phone[0];
                    PhoneLocalCode = Phone[1];
                    PhoneNum = Phone[2];
                }

                string[] PhoneMobile = cBookingDetail.GetBookingPhoneByBookingId(int.Parse(this.qBookingId), 2);
                if (PhoneMobile != null)
                {
                    MobileCountryCode = PhoneMobile[0];
                    MobileLocalCode = PhoneMobile[1];
                    MobileNum = PhoneMobile[2];
                }

                cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));

                dropPrefix.SelectedValue = cBookingDetail.PrefixId.ToString();
                txtBookingName.Text = cBookingDetail.FullName;
                txtEmail.Text = cBookingDetail.Email;
                txtPhone.Text = PhoneNum;
                txtmobile.Text = MobileNum;
                dropCountry.SelectedValue = cBookingDetail.CountryId.ToString();
                txtArrF.Text = cBookingDetail.F_arr_No;

                if (cBookingDetail.F_arr_Time.HasValue)
                {
                    DateTime dDateArr = (DateTime)cBookingDetail.F_arr_Time;
                    dDatePicker_arr.DateStart = dDateArr;
                    arrdropHH.SelectedValue = dDateArr.Hour.ToString();
                    arrdropMM.SelectedValue = dDateArr.Minute.ToString();
                }
                else
                    dDatePicker_arr.DateStart = DateTime.Now.Hotels2ThaiDateTime();

                dDatePicker_arr.DataBind();


                txtDepF.Text = cBookingDetail.F_Dep_No;
                if (cBookingDetail.F_Dep_Time.HasValue)
                {
                    DateTime dDateDep = (DateTime)cBookingDetail.F_Dep_Time;
                    dDatePicker_Dep.DateStart = dDateDep;
                    arrdropHH.SelectedValue = dDateDep.Hour.ToString();
                    arrdropMM.SelectedValue = dDateDep.Minute.ToString();
                }
                else
                    dDatePicker_Dep.DateStart = DateTime.Now.Hotels2ThaiDateTime();

                BookingItemDisplay cBookingItem = new BookingItemDisplay();

                txttransfer.Text = cBookingItem.GetBookingTransferDetail(int.Parse(this.qBookingId));

            }
        }

        public void btnSaveBookingDetail_Onclick(object sender, EventArgs e)
        {


            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            int BookingId = int.Parse(this.qBookingId);

            string BookingName = txtBookingName.Text;
            string BookingEmail = txtEmail.Text;
            byte bytCountryID = byte.Parse(dropCountry.SelectedValue);

            DateTime dDAteArr = dDatePicker_arr.GetDatetStart;
            int ArrHrs = int.Parse(arrdropHH.SelectedValue);
            int ArrMins = int.Parse(arrdropMM.SelectedValue);

            DateTime dDateTimeArr = new DateTime(dDAteArr.Year, dDAteArr.Month, dDAteArr.Day, ArrHrs, ArrMins, 0);


            DateTime dDAteDep = dDatePicker_Dep.GetDatetStart;
            int DepHrs = int.Parse(depdropHH.SelectedValue);
            int DepMins = int.Parse(depdropMM.SelectedValue);
            byte bytCusProfix = byte.Parse(dropPrefix.SelectedValue);
            DateTime dDateTimeDep = new DateTime(dDAteDep.Year, dDAteDep.Month, dDAteDep.Day, DepHrs, DepMins, 0);

            string FlightArrNo = txtArrF.Text;
            string FlightDepNo = txtDepF.Text;

            string PhoneCountryCode = string.Empty;
            string PhoneLocalCode = string.Empty;
            string PhoneNum = txtPhone.Text;

            string MobileCountryCode = string.Empty;
            string MobileLocalCode = string.Empty;
            string MobileNum = txtmobile.Text;

            string FAxCountryCode = string.Empty;
            string FAxLocalCode = string.Empty;
            string FAxNum = string.Empty;



            if (cBookingDetail.GetBookingPhoneByBookingId(BookingId, 1) == null)
                cBookingDetail.INsertBookingPhoneByBookingIdAndPhoneCat(BookingId, 1, PhoneCountryCode, PhoneLocalCode, PhoneNum);
            else
                cBookingDetail.UpdatePhoneBooking(BookingId, 1, PhoneCountryCode, PhoneLocalCode, PhoneNum);




            if (cBookingDetail.GetBookingPhoneByBookingId(BookingId, 2) == null)
                cBookingDetail.INsertBookingPhoneByBookingIdAndPhoneCat(BookingId, 2, MobileCountryCode, MobileLocalCode, MobileNum);
            else
                cBookingDetail.UpdatePhoneBooking(BookingId, 2, MobileCountryCode, MobileLocalCode, MobileNum);


            if (cBookingDetail.GetBookingPhoneByBookingId(BookingId, 3) == null)
                cBookingDetail.INsertBookingPhoneByBookingIdAndPhoneCat(BookingId, 3, FAxCountryCode, FAxLocalCode, FAxNum);
            else
                cBookingDetail.UpdatePhoneBooking(BookingId, 3, FAxCountryCode, FAxLocalCode, FAxNum);



            cBookingDetail.UpdateBookingDetail(BookingId, BookingName, BookingEmail, bytCountryID, FlightArrNo, dDateTimeArr, FlightDepNo, dDateTimeDep, bytCusProfix);

            cBookingItem.UpdateBookingItemTransferDetail(BookingId, txttransfer.Text);

            ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>window.close();if (window.opener && !window.opener.closed) {window.opener.location.reload();}</script>", false);
            
             
        }
    }
}