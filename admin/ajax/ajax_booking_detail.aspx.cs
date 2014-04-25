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


public partial class ajax_booking_detail : System.Web.UI.Page
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
            Response.Write(getBookingdetail());
            Response.Flush();
        }
    }


    public string getBookingdetail()
    {
        BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
        Status cStatus = new Status();
        cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));
        StringBuilder result = new StringBuilder();
        DateTime dfArr = new DateTime();
        DateTime dfDep = new DateTime();
        string FArr = "N/A";
        string FDep = "N/A";
        if(cBookingDetail.F_arr_Time.HasValue)
        {
         dfArr = (DateTime)cBookingDetail.F_arr_Time;
         FArr = dfArr.ToString("HH:mm , MMM dd, yyyy");
        }

        if(cBookingDetail.F_Dep_Time.HasValue)
        {
         dfDep = (DateTime)cBookingDetail.F_Dep_Time;
         FDep = dfDep.ToString("HH:mm , MMM dd, yyyy");
        }
       

        string FullName = "Full name";
        result.Append("<h4><img   src=\"../../images/content.png\" /> Booking Detail</h4>");
        result.Append("<p class=\"contentheadedetail\">List Supplier of This Product, you can Change or Add Supplier to List <a href=\"\" onclick=\"bookingdetailEdit();return false;\" >Edit</a></p><br />");
        result.Append("<table cellpadding=\"0\" id=\"booking_detail\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; font-size:12px;\">");
        //result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:20px;line-height:20px;\"><td colspan=\"2\" style=\"font-size:14px;font-weight:bold\"> Booking Detail <a href=\"\">[Edit]</a></td></tr>");
        result.Append("<tr class=\"trRow\"><td >Booking Status</td><td style=\"color:#333333;padding:5px 0px 5px 0px\">");
        result.Append("<p class=\"last_modify\"><span >LastModify : </span>" + cBookingDetail.DateModify.ToString("ddd, MMM dd, yyyy ; HH:mm") + "</p>");
        result.Append("&nbsp;<select id=\"booking_status_drop\" name=\"booking_status_drop\" style=\"width:250px;\" class=\"DropDownStyleCustom\" >");
        foreach (KeyValuePair<string, string> sitem in cStatus.GetStatusByCatId(2))
        {
            if (cBookingDetail.StatusId.ToString() == sitem.Key)
                result.Append("<option value=\"" + sitem.Key+ "\" selected=\"selected\">" + sitem.Value + "</option>");
            else
                result.Append("<option value=\"" + sitem.Key + "\">" + sitem.Value + "</option>");
        }

        result.Append("</select>");
        result.Append("&nbsp;<input type=\"button\"  onclick=\"UpdateStatus('" + cBookingDetail.BookingId + "','booking');return false;\" value=\"Go\" id=\"bookingStatus_Update\" class=\"btStyle\" style=\"width:50px;\" />");
        
        result.Append("</td></tr>");

        if (cBookingDetail.AffSiteId.HasValue)
        {
            result.Append("<tr class=\"trRow\"><td  style=\"color:#ef2d2d;font-weight:bold\">Affiliate Status</td><td style=\"color:#333333; padding:5px 0px 5px 0px\" >");

            result.Append("&nbsp;<select id=\"booking_statusAff_drop\" name=\"booking_statusAff_drop\" style=\"width:250px;\" class=\"DropDownStyleCustom\" >");
            foreach (KeyValuePair<string, string> sitem in cStatus.GetStatusByCatId(4))
            {
                if (cBookingDetail.StatusAffId.ToString() == sitem.Key)
                    result.Append("<option value=\"" + sitem.Key + "\" selected=\"selected\">" + sitem.Value + "</option>");
                else
                    result.Append("<option value=\"" + sitem.Key + "\">" + sitem.Value + "</option>");
            }

            result.Append("</select>");
            result.Append("&nbsp;<input type=\"button\"  onclick=\"UpdateStatus('" + cBookingDetail.BookingId + "','aff');return false;\" value=\"Go\" id=\"bookingStatusAff_Update\" class=\"btStyle\" style=\"width:50px;\" />");

            result.Append("</td></tr>");


        }
        if (cBookingDetail.CusId.HasValue)
            FullName = FullName + "&nbsp;<img src=\"/images/member.png\" alt=\"member\" />";
        result.Append("<tr class=\"trRowalten\"><td>");

        string strPrefix = string.Empty;
        if (cBookingDetail.PrefixId > 1)
            strPrefix = cBookingDetail.PrefixTitle + "&nbsp;";
        
        result.Append(FullName + "</td><td style=\"color:#333333\">" + strPrefix + "");



        
        result.Append(cBookingDetail.FullName + "</td></tr>");
        result.Append("<tr class=\"trRow\"><td >Email</td><td style=\"color:#333333\"><a href=\"mailto:" + cBookingDetail.Email + "\">" + cBookingDetail.Email + "</a></td></tr>");

        result.Append("<tr class=\"trRow\"><td  >Phone</td><td style=\"color:#333333\">" + cBookingDetail.BookingPhone + "</td></tr>");
        result.Append("<tr class=\"trRowalten\"><td >Mobile</td><td style=\"color:#333333\">" + cBookingDetail.BookingMobile + "</td></tr>");
        result.Append("<tr class=\"trRow\"><td >Fax</td><td style=\"color:#333333\">" + cBookingDetail.BookingFax + "</td></tr>");
        result.Append("<tr class=\"trRowalten\"><td >Country</td><td style=\"color:#333333\">" + cBookingDetail.CountryTitle);

        string style = "color:#6da71e";
        if (cBookingDetail.CountryId != cBookingDetail.CountryIdBytrackIp)
            style = "color:#ff2020";

        result.Append("&nbsp;<label style=\"font-weight:bold;" + style + "\">[ " + cBookingDetail.CountryTitleBytrackIP + " ] <a href=\"http://whois.domaintools.com/" + cBookingDetail.RefIP + "\" target=\"_Blank\" > " + cBookingDetail.RefIP + "</a><label>");


        result.Append("</td></tr>");
        result.Append("<tr class=\"trRow\"><td>Arrival Flight</td><td style=\"color:#333333\"><span style=\"font-weight:bold\">Nubmer:</span> " + cBookingDetail.F_arr_No + "&nbsp;<span style=\"font-weight:bold\">Time:</span>" + FArr + "</td></tr>");
        result.Append("<tr  class=\"trRowalten\"><td >Departure Flight</td><td style=\"color:#333333\"><span style=\"font-weight:bold\">Nubmer:</span> " + cBookingDetail.F_Dep_No + "&nbsp;<span style=\"font-weight:bold\">Time:</span>" + FDep + "</td></tr>");
        result.Append("<tr  class=\"trRow\"><td >Booking Receive</td><td style=\"color:#333333\">" + cBookingDetail.DateBookingREceive.ToString("ddd, MMM dd, yyyy ; HH:mm") + "</td></tr>");

        result.Append("<tr  class=\"trRowalten\"><td colspan=\"2\" style=\" padding:0px 0px 0px 0px;\">");


        result.Append("<table id=\"booking_detail_confirm\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; font-size:12px;\">");
        result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:15px;line-height:15px;\"><td colspan=\"3\" style=\"font-size:12px;font-weight:bold;color:#ffffff\">Booking Confirm</td></tr>");
        result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:15px;line-height:15px;\"><td colspan=\"2\" style=\"font-size:12px;font-weight:bold;color:#ffffff\">Confirm Open</td><td style=\"font-size:12px;font-weight:bold;color:#ffffff\">Confirm Completed</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:35px;\"><td>" + PicStatusNameConfirm(cBookingDetail.ConfirmOpen,4) + "</td><td>" + PicStatusNameConfirmOpenmail(cBookingDetail.ConfirmVoucher) + "</td><td>" + PicStatusNameConfirm(cBookingDetail.ConfirmCOmpleted,5) + "</td></tr>");
        result.Append("</table>");


        result.Append("</td></tr></table>");
      
        result.Append("");
        result.Append("");
        result.Append("");
        result.Append("");
        return result.ToString();
    }

    public string PicStatusNameConfirm(DateTime? DateConfirm, byte ConfirmCat)
    {
        string imageName = string.Empty;
        if (DateConfirm.HasValue)
        {
            DateTime dDate = (DateTime)DateConfirm;
            //imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; hh:mm tt") + "<img src=\"../../images/refresh.png\" onclick=\"confirmswitchback('" + intBookingProductId + "');return false;\" style=\"cursor:pointer;\" />";
            imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Swicth back Now!!?' ,'ConfirmSwitchBackBooking(" + ConfirmCat + ")');return false;\" style=\"cursor:pointer;\" />";
        }

        else
            imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Confirm Now!!??' ,'BookingConfirm(" + ConfirmCat + ")');return false;\">Confirm Now</a>";

        return imageName;
    }
    //public string PicStatusNameConfirm(DateTime? DateConfirm, byte ConfirmCat)
    //{
    //    string imageName = string.Empty;
    //    if (DateConfirm.HasValue)
    //    {
    //        DateTime dDate = (DateTime)DateConfirm;
    //        imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm");
    //    }

    //    else
    //        imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Confirm Now!!??' ,'BookingConfirm(" + ConfirmCat + ")');return false;\">Confirm Now</a>";

    //    return imageName;
    //}

    public string PicStatusNameConfirmOpenmail(DateTime? DateConfirm)
    {
        string imageName = string.Empty;
        if (DateConfirm.HasValue)
        {
            DateTime dDate = (DateTime)DateConfirm;
            imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm");
        }

        else
            imageName = "<img src=\"../../images/false.png\"/></br><p style=\"font-weight:bold;font-size:11px;margin:0px;padding:0px;color:#72ac58\">Open Voucher</p>";

        return imageName;
    }
}