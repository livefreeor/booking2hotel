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
    public partial class ajax_booking_list_body : Hotels2BasePageExtra_Ajax
    {
        public string qBokingStatus
        {
            get
            {
                return Request.QueryString["bs"];
            }
        }
        public string qBokingProductStatus
        {
            get
            {
                return Request.QueryString["bps"];
            }
        }

        public string qBokingListype
        {
            get
            {
                return Request.QueryString["lTpye"];
            }
        }
        public string qPage
        {
            get { return Request.QueryString["page"]; }
        }
        public string qBokingOrderBy
        {
            get
            {
                return Request.QueryString["order"];
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

           
            if (!this.Page.IsPostBack)
            {
                Response.Write(BookingListBody(this.qBokingStatus, this.qBokingProductStatus, this.qBokingListype));
                Response.End();
            }

        }


        public string BookingListBody(string strBookingStatus, string strBookingProductStatus, string BookingListType)
        {
            string result = string.Empty;

            try
            {

                short shrBookingStatus = short.Parse(strBookingStatus);
                short shrBookingProductStatus = short.Parse(strBookingProductStatus);
                BookingList cBookingList = new BookingList();
                IList<object> ListBookingProductList = null;

                int page = 1;
                if (!string.IsNullOrEmpty(this.qPage))
                    page = int.Parse(this.qPage);

                switch (BookingListType)
                {
                    case "1":
                        ListBookingProductList = cBookingList.GetBookingListOrderCenter(shrBookingStatus, shrBookingProductStatus, 1, page, this.qBokingOrderBy, this.CurrentProductActiveExtra);
                        break;
                    case "2":
                        ListBookingProductList = cBookingList.GetBookingList_Status_OrderOpen(shrBookingStatus, shrBookingProductStatus, 1, page, this.qBokingOrderBy);
                        break;
                    case "3":
                        ListBookingProductList = cBookingList.GetBookingList_Status_CheckinDueDate(shrBookingStatus, shrBookingProductStatus, 1, page, 0, this.qBokingOrderBy);
                        break;
                    case "4":
                        ListBookingProductList = cBookingList.GetBookingListOrderCenterDuplicate(shrBookingStatus, shrBookingProductStatus, 1, page, this.qBokingOrderBy);
                        break;

                }

                result = result + "<table cellpadding=\"0\" cellspacing=\"2\"  style=\"margin:7px 0px 0px 0px;\"  class=\"tbl_acknow\"  >";
                result = result + "<tr class=\"header_field\" align=\"center\" >";
                result = result + "<th width=\"5%\">ID</th><th width=\"10%\" >Customer</th><th width=\"25%\" >Product</th><th width=\"15%\">Receive</th>";
                result = result + "<th width=\"20%\">In-Out</th><th width=\"10%\">Hotel Price</th>";
                result = result + "<th width=\"5%\">1</th><th width=\"5%\">2</th><th width=\"5%\">3</th>";
                result = result + "</tr>";

                if (ListBookingProductList.Count > 0)
                {
                    decimal PriceSales = 0;
                    decimal PriceSup = 0;
                    decimal PriceTotal = 0;

                    int intBookingIdTemp = 0;
                    string AppendQueryString = string.Empty;
                    if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                        AppendQueryString = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId;

                    foreach (BookingList BpItem in ListBookingProductList)
                    {
                        PriceTotal = 0;
                        string strRisk = string.Empty;

                        int DueDateCheckin = BpItem.CheckIn.Subtract(DateTime.Now).Days;


                        if (BpItem.CountryId != BpItem.CountryIdFromIP)
                            strRisk = "style=\"color:#bf0000; height:40px;\"";


                        result = result + "<tr style=\"background-color:#ffffff;\">";

                        //result = result + "<td>" + GetBookingNumberAndSignal(BpItem.BookingID, BpItem.BookingAffsiteID, BpItem.PaymentTypeID, BpItem.IsExtranet, strBookingProductStatus, DueDateCheckin, BpItem.CheckOut, BpItem.BookingLang, BpItem.CountryId) + "</td>";
                        result = result + "<td><a href=\"booking_detail.aspx?bid=" + BpItem.BookingID + AppendQueryString + "\">" + BpItem.BookingHotelID + "</a></td>";
                        //result = result + "<td>" + BpItem.BookingHotelID + "</td>";
                        result = result + "<td style=\"font-size:11px;\"><label " + strRisk + ">" + BpItem.BookingName + "</label></td>";
                       // result = result + "<td class=\"gateway_item\">" + Hotels2String.getImageGateWay(BpItem.GateWay) + "</td>";

                        result = result + "<td style=\"text-align:left;padding-left:5px;\">" + BpItem.ProductTitle;
                        if (BpItem.SupplierCatId == 1)
                            result = result + "<br/><span style=\"color:#3f5d9d;font-weight:bold\">[" + BpItem.SupplierTitle + "]</span></td>";

                        result = result + "</td>";

                        result = result + "<td  style=\"font-size:11px;\" align=\"center\" >" + BpItem.BookingReceive.ToString("ddd,MMM dd, yyyy") + "</td>";

                        result = result + "<td valign=\"middle\" align=\"center\"><p class=\"chk_in\" style=\"font-size:10px;\">" + BpItem.CheckIn.ToString("ddd,MMM dd, yyyy") + "</p><p class=\"chk_out\" style=\"font-size:10px;\">" + BpItem.CheckOut.ToString("ddd,MMM dd, yyyy") + "<p></td>";



                        PriceSales = decimal.Parse(BpItem.PriceSales.Split(';')[0]);

                        PriceSup = decimal.Parse(BpItem.PriceSales.Split(';')[1]);



                        result = result + "<td style=\"font-weight:bold;color:#52555d;font-size:11px;\">" + PriceSales.ToString("#,###.00") + "</td>";

                        //result = result + "<td>" + BpItem.CheckIn.Subtract(DateTime.Now).Days + "</td>";
                        //result = result + "<td>" + CheckInsupplierDueDate(BpItem.SupplierDayPayment, PriceSup, BpItem.CheckIn, BpItem.CheckOut
                        //     , BpItem.SupplierPaymentType) + "</td>";
                        //result = result + "<td><label style=\"font-weight:bold;color:#3f5d9d;font-size:11px;\">" + PriceSup + "</label></td>";
                        foreach (BookingList price in ListBookingProductList)
                        {
                            if (BpItem.BookingID == price.BookingID)
                            {
                                PriceTotal = PriceTotal + decimal.Parse(price.PriceSales.ToString().Split(';')[0]);
                            }
                        }



                        result = result + "<td><img src=\"http://hotels2thailand.com/images/" + PicStatusName_ByBookingBalance(BpItem.Payment, BpItem.PaymentTypeID, PriceTotal) + "\"/></td>";
                        //result = result + "<td>" + PriceTotal.ToString() + "</td>";
                        result = result + "<td><img src=\"http://hotels2thailand.com/images/" + PicStatusName(BpItem.Confirm_input) + "\"/></td>";

                        result = result + "<td><img src=\"http://hotels2thailand.com/images/" + PicStatusName(BpItem.Confirm_Open) + "\"/></td>";
                        result = result + "</tr>";

                        intBookingIdTemp = BpItem.BookingID;

                    }



                }
                else
                {
                    result = result + "<tr><td colspan=\"16\" align=\"center\">No DATA</td></tr>";
                }

                result = result + "</table>";


            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message + "<br/>" + ex.StackTrace);
                Response.End();
            }

            return result;
        }

        public string CheckInsupplierDueDate(byte? duedate, decimal SupplierPrice, DateTime dDateCheckIn, DateTime dDateCheckOut, short SupPaymentType)
        {
            string result = string.Empty;
            int intdueDate = 0;
            byte Duedate = 0;

            string color = string.Empty;
            if (duedate.HasValue)
            {
                Duedate = (byte)duedate;

                if (SupPaymentType != 3)
                {
                    if (SupPaymentType == 1)
                        intdueDate = dDateCheckIn.Subtract(DateTime.Now).Days;
                    else
                        intdueDate = dDateCheckOut.Subtract(DateTime.Now).Days;



                    if (intdueDate <= Duedate)
                        color = "#db4f03";
                    else
                        color = "#3f5d9d";
                }
                else
                {
                    color = "#3f5d9d";
                }
                //result = color.ToString();[" + Duedate + "]<strong style=\"color:#db4f03;\">[?]</strong>
                result = "<label style=\"font-weight:bold;color:" + color + ";font-size:11px;\">" + SupplierPrice.Hotels2Currency() + "</label>";

            }
            else
            {
                result = "<label style=\"font-weight:bold;color:#3f5d9d;font-size:11px;\">" + SupplierPrice.Hotels2Currency() + "</label>";
            }


            return result;

        }

        public string PicStatusName(int intValcheck)
        {
            string imageName = "false.png";
            if (intValcheck > 0)
                imageName = "true.png";

            return imageName;
        }

        public string PicStatusName_ByBookingBalance(decimal SumBookingPaid, byte bytPaymentPlanType, decimal PriceSales)
        {

            string imageName = "false.png";
            if (bytPaymentPlanType == 2)
                imageName = "book_now_nocharge.png";

            if (SumBookingPaid >= 0)
            {

                if (SumBookingPaid == PriceSales)
                    imageName = "true.png";
                if (SumBookingPaid < PriceSales)
                    imageName = "credit.png";
                if (SumBookingPaid > PriceSales)
                    imageName = "true_plus.png";
            }


            return imageName;
        }


        public string PicStatusName_ByBookingPayment(decimal SumBookingSettle, decimal PriceSales)
        {

            string imageName = "false.png";

            if (SumBookingSettle > 0)
            {

                if (SumBookingSettle == PriceSales)
                    imageName = "true.png";
                if (SumBookingSettle < PriceSales)
                    imageName = "credit.png";
                if (SumBookingSettle > PriceSales)
                    imageName = "true_plus.png";
            }


            return imageName;
        }

        public string GetBookingNumberAndSignal(int intBookingId, int? BookingAffsiteID, byte bytPaymentId, bool IsExtranet, string BookintProductId,
            int CheckDuedate, DateTime dDateCheckOut, byte bytBookingLang, short CountryID)
        {

            string Result = string.Empty;

            string ThaiSign = "";
            if (bytBookingLang == 2)
            {
                ThaiSign = ThaiSign + "<div style=\"width:3px;height:40px;float:left\">";
                ThaiSign = ThaiSign + "<p style=\"height:15%; margin:0px;padding:0px; background-color:#bf0000\"></p>";
                ThaiSign = ThaiSign + "<p style=\"height:10%; margin:0px;padding:0px; background-color:#ffffff\"></p>";
                ThaiSign = ThaiSign + "<p style=\"height:50%; margin:0px;padding:0px; background-color:#012f49\"></p>";
                ThaiSign = ThaiSign + "<p style=\"height:10%; margin:0px;padding:0px; background-color:#ffffff\"></p>";
                ThaiSign = ThaiSign + "<p style=\"height:15%; margin:0px;padding:0px; background-color:#bf0000\"></p>";
            }


            ThaiSign = ThaiSign + "</div>";

            string DueCheckincolor = string.Empty;

            if (BookintProductId != "10")
            {
                if (CheckDuedate <= 7)
                    DueCheckincolor = "style=\"color:#bf0000;\"";
                else
                    DueCheckincolor = "style=\"color:#3f5d9d;\"";
            }

            if (dDateCheckOut.Subtract(DateTime.Now).Days <= 0)
                DueCheckincolor = "style=\"color:#4f8406;\"";

            string strBookingId = "<a " + DueCheckincolor + " href=\"booking_detail.aspx?bid=" + intBookingId + "\" target=\"_blank\">" + intBookingId + "</a>";
            string strAffSign = "<img src=\"http://www.booking2hotels.com/images/aff_sign.png\" alt=\"Affiliate\" title=\"Affiliate\" / >";
            string strBookNow = "<img src=\"http://www.booking2hotels.com/images/paylater_sign.png\" alt=\"Booknow Pay Later\" title=\"Booknow Pay Later\" / >";
            string strExtraNet = "<img src=\"http://www.booking2hotels.com/images/extranet_sign.png\" alt=\"Extra Net\" title=\"Extra Net\" / >";

            string strBr = "<br/>";
            string strSpace = "&nbsp;";

            // if Normal booking
            if (!BookingAffsiteID.HasValue && bytPaymentId == 1 && !IsExtranet)
            {
                Result = "<p style=\"margin:15px 0px 0px 0px;\">" + strBookingId + "</p>";
            }
            //if Extranet Only
            if (!BookingAffsiteID.HasValue && bytPaymentId == 1 && IsExtranet)
            {
                Result = strExtraNet + strBr + strBookingId;
            }
            // if Booknow Only
            if (!BookingAffsiteID.HasValue && bytPaymentId == 2 && !IsExtranet)
            {
                Result = strBookNow + strBr + strBookingId;
            }

            //if Aff Only
            if (BookingAffsiteID.HasValue && bytPaymentId == 1 && !IsExtranet)
            {
                Result = strAffSign + strBr + strBookingId;
            }

            //if Aff & booknow Only
            if (BookingAffsiteID.HasValue && bytPaymentId == 2 && !IsExtranet)
            {
                Result = strAffSign + strSpace + strBookNow + strBr + strBookingId;
            }

            //if Aff & Extranet Only
            if (BookingAffsiteID.HasValue && bytPaymentId == 1 && IsExtranet)
            {
                Result = strAffSign + strSpace + strExtraNet + strBr + strBookingId;
            }

            //if Booknow & Extranet Only
            if (!BookingAffsiteID.HasValue && bytPaymentId == 2 && IsExtranet)
            {
                Result = strBookNow + strSpace + strExtraNet + strBr + strBookingId;
            }

            //if Booknow & Extranet & Aff
            if (BookingAffsiteID.HasValue && bytPaymentId == 2 && IsExtranet)
            {
                Result = strAffSign + strSpace + strBookNow + strSpace + strExtraNet + strBr + strBookingId;
            }

            return ThaiSign + "<div style=\"margin:8px 0px 0px 0px;\">" + Result + "</div>";
        }

    }
}