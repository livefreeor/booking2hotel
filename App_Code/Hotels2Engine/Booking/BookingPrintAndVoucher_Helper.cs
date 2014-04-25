using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.Staffs;
using Hotels2thailand.Suppliers;
using Hotels2thailand.ProductOption;


/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public partial class BookingPrintAndVoucher_Helper
    {
        private string _mainsite = "http://www.hotels2thailand.com";
        protected string DateTimeCheck(DateTime? dDate)
        {
            string dDAtestring = string.Empty;
            if (dDate.HasValue)
            {
                DateTime dDAteTime = (DateTime)dDate;
                dDAtestring = dDAteTime.ToString("MMMM dd, yyyy");
            }
            return dDAtestring;
        }
        protected string DateTimeCheck(DateTime? dDate, byte bytBookingLang)
        {
            string dDAtestring = string.Empty;
            if (dDate.HasValue)
            {
                DateTime dDAteTime = (DateTime)dDate;
                switch (bytBookingLang)
                {
                    case 1:
                        dDAtestring = dDAteTime.ToString("MMMM dd, yyyy");
                        break;
                    case 2:
                        System.IFormatProvider format = new System.Globalization.CultureInfo("th-TH");
                        dDAtestring = dDAteTime.ToString("ddd dd MMMM yyyy", format);
                        break;
                }
               
            }
            return dDAtestring;
        }
        protected string DateTimeCheck(DateTime? dDate, DateTime? dDateconfirmCheckIn, byte bytBookingLang)
        {
            string dDAtestring = string.Empty;
            DateTime dDateResult = DateTime.Now;
            string Confirm = "<span class=\"hlstyle\"> (Requested date, not yet confirm)</span>";
            if (dDate.HasValue)
            {
                if (dDateconfirmCheckIn.HasValue)
                {
                    Confirm = "<span class=\"hlstyle\">(Confirmed)</span>";
                    DateTime dDAteTimeConfirm = (DateTime)dDateconfirmCheckIn;
                    dDateResult = dDAteTimeConfirm;
                }
                else
                {
                    DateTime dDAteTime = (DateTime)dDate;
                    dDateResult = dDAteTime;
                }

                switch (bytBookingLang)
                {
                    case 1:
                        dDAtestring = dDateResult.ToString("MMMM dd, yyyy");
                        break;
                    case 2:
                        System.IFormatProvider format = new System.Globalization.CultureInfo("th-TH");
                        dDAtestring = dDateResult.ToString("ddd dd MMMM yyyy", format);
                        break;
                }

            }
            return dDAtestring + "<br/>"+Confirm;
        }
        protected string DateTimeCheck(DateTime? dDate, DateTime? dDateconfirmCheckIn)
        {
            string dDAtestring = string.Empty;
            string Confirm = "<span class=\"hlstyle\">(Requested date, not yet confirm)</span>";
            if (dDate.HasValue)
            {
                if (dDateconfirmCheckIn.HasValue)
                {
                    Confirm = "<span class=\"hlstyle\">(Confirmed)</span>";
                    DateTime dDAteTimeConfirm = (DateTime)dDateconfirmCheckIn;
                    dDAtestring = dDAteTimeConfirm.ToString("MMMM dd, yyyy");
                }
                else
                {
                    DateTime dDAteTime = (DateTime)dDate;
                    dDAtestring = dDAteTime.ToString("MMMM dd, yyyy");
                }
               
            }
            return dDAtestring + Confirm;
        }
        protected string DateTimeCheck_TimeOnly(DateTime? dDate)
        {
            string dDAtestring = string.Empty;

            if (dDate.HasValue)
            {
                BookingConfirmEngine cConfirm = new BookingConfirmEngine();

                DateTime dDAteTime = (DateTime)dDate;
                dDAtestring = dDAteTime.ToString("HH:mm");
            }
            return dDAtestring;
        }

        protected string DateTimeCheck_TimeOnly(DateTime? dDate, DateTime? dDateconfirmCheckIn)
        {
            string dDAtestring = string.Empty;
            string Confirm = "<span class=\"hlstyle\">(Requested time, not yet confirm)</span>";
            if (dDate.HasValue)
            {
                if (dDateconfirmCheckIn.HasValue)
                {
                    Confirm = "<span class=\"hlstyle\">(Confirmed)</span>";
                    DateTime dDAteTimeConfirm = (DateTime)dDateconfirmCheckIn;
                    dDAtestring = dDAteTimeConfirm.ToString("HH:mm tt");
                }
                else
                {
                    DateTime dDAteTime = (DateTime)dDate;
                    dDAtestring = dDAteTime.ToString("HH:mm tt");
                }

            }
            return dDAtestring + "+<br/>+" +Confirm;
        }

        protected string DateTimeCheck_TimeOnly(DateTime? dDate, DateTime? dDateconfirmCheckIn, byte bytBookingLang)
        {
            string dDAtestring = string.Empty;
            DateTime dDateResult = DateTime.Now;
            string Confirm = "<span class=\"hlstyle\"> (Requested time, not yet confirm)</span>";
            if (dDate.HasValue)
            {
                if (dDateconfirmCheckIn.HasValue)
                {
                    Confirm = "<span class=\"hlstyle\">(Confirmed)</span>";
                    DateTime dDAteTimeConfirm = (DateTime)dDateconfirmCheckIn;
                    dDateResult = dDAteTimeConfirm;
                }
                else
                {
                    DateTime dDAteTime = (DateTime)dDate;
                    dDateResult = dDAteTime;
                }
                
            }

            switch (bytBookingLang)
            {
                case 1:
                    dDAtestring = dDateResult.ToString("HH:mm tt");
                    break;
                case 2:
                    System.IFormatProvider format = new System.Globalization.CultureInfo("th-TH");
                    dDAtestring = dDateResult.ToString("HH:mm tt", format);
                    break;
            }

            return dDAtestring + "<br/>"+Confirm;
        }
        protected string DateTimeChekFullTime(DateTime? dDate, DateTime? dDateconfirmCheckIn)
        {

            string dDAtestring = string.Empty;
            string Confirm = "<span class=\"hlstyle\">(On Request)</span>";
            if (dDate.HasValue)
            {
                if (dDateconfirmCheckIn.HasValue)
                {
                    Confirm = "<span class=\"hlstyle\">(Confirmed)</span>";
                    DateTime dDAteTimeConfirm = (DateTime)dDateconfirmCheckIn;
                    dDAtestring = dDAteTimeConfirm.ToString("HH:mm tt");
                }
                else
                {
                    DateTime dDAteTime = (DateTime)dDate;
                    dDAtestring = dDAteTime.ToString("HH:mm tt");
                }

            }
            return dDAtestring + "<br/>"+Confirm;
        }
        protected string DateTimeChekFullDateAndTime(DateTime? dDate, DateTime? dDateconfirmCheckIn, byte bytBookingLang)
        {

            string dDAtestring = string.Empty;
            DateTime dDateResult = DateTime.Now;
            string Confirm = "<span class=\"hlstyle\">(On Request)</span>";
            if (dDate.HasValue)
            {
                if (dDateconfirmCheckIn.HasValue)
                {
                    Confirm = "<span class=\"hlstyle\">(Confirmed)</span>";
                    DateTime dDAteTimeConfirm = (DateTime)dDateconfirmCheckIn;
                    dDateResult = dDAteTimeConfirm;
                }
                else
                {
                    DateTime dDAteTime = (DateTime)dDate;
                    dDateResult = dDAteTime;
                }

                switch (bytBookingLang)
                {
                    case 1:
                        dDAtestring = dDateResult.ToString("HH:mm ; MMMM dd, yyyy");
                        break;
                    case 2:
                        System.IFormatProvider format = new System.Globalization.CultureInfo("th-TH");
                        dDAtestring = dDateResult.ToString("HH:mm ; ddd dd MMMM yyyy", format);
                        break;
                }

            }
            return dDAtestring + Confirm;
        }
        protected string DateTimeChekFullDateAndTime(DateTime? dDate, DateTime? dDateconfirmCheckIn)
        {

            string dDAtestring = string.Empty;
            string Confirm = "<span class=\"hlstyle\">(On Request)</span>";
            if (dDate.HasValue)
            {
                if (dDateconfirmCheckIn.HasValue)
                {
                    Confirm = "<span class=\"hlstyle\">(Confirmed)</span>";
                    DateTime dDAteTimeConfirm = (DateTime)dDateconfirmCheckIn;
                    dDAtestring = dDAteTimeConfirm.ToString("HH:mm ; MMMM dd, yyyy");
                }
                else
                {
                    DateTime dDAteTime = (DateTime)dDate;
                    dDAtestring = dDAteTime.ToString("HH:mm ; MMMM dd, yyyy");
                }
                
            }
            return dDAtestring + Confirm;
        }

        protected string DateTimeChekFullDateAndTime(DateTime? dDate)
        {

            string dDAtestring = string.Empty;
            
            if (dDate.HasValue)
            {

                DateTime dDAteTime = (DateTime)dDate;
                dDAtestring = dDAteTime.ToString("HH:mm tt; MMMM dd, yyyy");
            }
            return dDAtestring;
        }

        protected string DateTimeChekFullDateAndTime(DateTime? dDate, byte bytBookingLang)
        {

            string dDAtestring = string.Empty;
            DateTime dDateResult = DateTime.Now;
            if (dDate.HasValue)
            {

                DateTime dDAteTime = (DateTime)dDate;
                dDateResult = dDAteTime;
            }

            switch (bytBookingLang)
            {
                case 1:
                    dDAtestring = dDateResult.ToString("HH:mm tt MMMM dd, yyyy");
                    break;
                case 2:
                    System.IFormatProvider format = new System.Globalization.CultureInfo("th-TH");
                    dDAtestring = dDateResult.ToString("HH:mm tt ddd dd MMMM yyyy", format);
                    break;
            }

            return dDAtestring;
        }
        protected string DateTimeNightTotal(DateTime? dDateSin, DateTime? dDAteOut)
        {
            string dDAtestring = string.Empty;
            if (dDateSin.HasValue && dDAteOut.HasValue)
            {
                DateTime dDAteTimeIN = (DateTime)dDateSin;
                DateTime dDAteTimeOut = (DateTime)dDAteOut;
                long lnight = dDAteTimeIN.Hotels2DateDiff(DateInterval.Day, dDAteTimeOut);
                dDAtestring = lnight.ToString();
            }

            return dDAtestring;
        }

        //private string GetNumGestString(byte bytProductCat, byte bytNumAdult, byte bytNumChild, byte bytNumGolf)
        //{
        //    StringBuilder TiemResult = new StringBuilder();
        //    TiemResult.Append("<td colspan=\"2\" align=\"left\" bgcolor=\"#FFFFFF\">");
        //    switch (bytProductCat)
        //    {
        //        case 32:
        //            TiemResult.Append("<strong>Golfer</strong> :  " + bytNumGolf);
        //            break;
        //        default:
        //            TiemResult.Append("<strong>Adult</strong> :  " + bytNumAdult + " <strong>Child</strong> : " + bytNumChild);

        //            break;
        //    }

        //    TiemResult.Append("</td>");


        //    return TiemResult.ToString();
        //}

        protected string StaffStampPic()
        {
            string Img = "<img src=\"" + _mainsite + "/images_staffs/s_hut.jpg\">";
            StaffSessionAuthorize cStaffAuthorize = new StaffSessionAuthorize();

            //if (cStaffAuthorize.CurrentStaffId != null)
            //{
                string strPathPic = "/images_staffs/StaffPic_stamp_" + cStaffAuthorize.CurrentStaffId + ".jpg";
                FileInfo fileFileName = new FileInfo(HttpContext.Current.Server.MapPath(strPathPic));

                if (fileFileName.Exists)
                {
                    Img = "<img src=\"" + _mainsite + "" + strPathPic + "\">";
                }

            //}
            return Img;
        }

        protected string StaffName()
        {
            string StaffName= "Ms.Ithitiya Manichpong";
            StaffSessionAuthorize cStaffAuthorize = new StaffSessionAuthorize();

            //if (cStaffAuthorize.CurrentStaffId != null)
            //{
                string strPathPic = "/images_staffs/StaffPic_stamp_" + cStaffAuthorize.CurrentStaffId + ".jpg";
                FileInfo fileFileName = new FileInfo(HttpContext.Current.Server.MapPath(strPathPic));

                if (fileFileName.Exists)
                {
                    StaffName = cStaffAuthorize.CurrentClassStaff.Title;
                }
            //}
            return StaffName;
        }

        protected string WordingTranslate(byte bytLangId, byte bytWordingType)
        {
            string result = string.Empty;
            
            switch (bytWordingType)
            {
                case 1 :
                    string[] arrKey1 = {"Baht","บาท"};
                    result = arrKey1[bytLangId - 1];
                    break;
                case 2:
                    string[] arrKey2 = {"Total","จำนวนเงินทั้งหมด"};
                    result = arrKey2[bytLangId - 1];
                    break;
            }

            return result;
        }
    }
}