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

public partial class ajax_booking_activity : System.Web.UI.Page
{
    public string qBookingProductId
    {
        get
        {
            return Request.QueryString["bpid"];
        }
    }
    public string qBookingId
    {
        get
        {
            return Request.QueryString["bid"];
        }
    }

    public string qBookingType
    {
        get
        {
            return Request.QueryString["bt"];
        }
    }
    public short Current_StaffID
    {
        get
        {
            Hotels2thailand.UI.Hotels2BasePage cBasePage = new Hotels2thailand.UI.Hotels2BasePage();
            return cBasePage.CurrentStaffId;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.qBookingType))
            {
                //Response.Write(Request.Url.ToString());
                Response.Write(getBookingActivity());
                Response.Flush();
            }
        }
    }
    public string getBookingActivity()
    {
        BookingActivityDisplay cActivityDisplay = new BookingActivityDisplay();
        List<object> cActivityDisplayList =  new List<object>();
        string AcHead = "Booking Activity";
        if(this.qBookingType == "booking")
        {
            cActivityDisplayList = cActivityDisplay.GetActivityBookingList(int.Parse(this.qBookingId));
        }
        else if (this.qBookingType == "product")
        {
            cActivityDisplayList = cActivityDisplay.GetActivityBookingProductList(int.Parse(this.qBookingProductId));
            AcHead = "Booking Product Activity";
        }
        int count = 1;
        StringBuilder result = new StringBuilder();
        result.Append("<h4><img   src=\"../../images/content.png\" /> " + AcHead + "</h4>");
       
        result.Append("<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center;\">");
        //result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:20px;line-height:20px;\"><td colspan=\"4\" style=\"font-size:14px;font-weight:bold\">Product Activity</td></tr>");
        result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;height:20px;line-height:20px;\"><td style=\"width:5%\">No.</td><td style=\"width:10%\">Staff</td><td style=\"width:85%;\">Detail</td></tr>");
        string strDateAc = string.Empty;
        count = cActivityDisplayList.Count();
        foreach (BookingActivityDisplay acITem in cActivityDisplayList)
        {
            
            if(acITem.DateActivity.HasValue)
            {
            DateTime dDate = (DateTime)acITem.DateActivity;
            strDateAc= dDate.ToString("MMM dd, yyyy ; HH:mm ");
            }else
            {
                strDateAc = "N/A";
            }
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\"><td>" + count + "</td><td>" + acITem.StaffName + "</td><td style=\"text-align:left;padding:0px 0px 2px 2px;\">" + acITem.Detail + "<br/><p style=\"margin:2px 0px 0px 0px;padding:2px 0px 0px 0px;font-size:10px;border:0px; color:#3f5d9d; text-align:right;\">" + strDateAc + "</p></td></tr>");
            count = count - 1;
        }
        if (this.qBookingType == "booking")
        {
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\"><td colspan=\"3\"><a href=\"\" onclick=\"addnewactivity('" + this.qBookingId + "','" + this.qBookingType + "');return false;\">add new activity</a></td></tr>");
        }
        else if (this.qBookingType == "product")
        {
            result.Append("<tr style=\"background-color:#ffffff; height:25px;\"><td colspan=\"3\"><a href=\"\" onclick=\"addnewactivity('" + this.qBookingProductId + "','" + this.qBookingType + "');return false;\">add new activity</a></td></tr>");
        }
        result.Append("</table>");

        return result.ToString();
    }
    

   
}