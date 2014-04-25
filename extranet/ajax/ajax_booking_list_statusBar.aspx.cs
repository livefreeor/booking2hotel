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

public partial class ajax_booking_list_statusBar : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            //if (!string.IsNullOrEmpty(this.qBokingStatus) && !string.IsNullOrEmpty(this.qBokingProductStatus))
            //{
                Response.Write(BookingListHead(this.qBokingStatus));
                Response.End();
            //}
            
           
        }
    }


    public string BookingListHead(string strBookingStatus)
    {
        StringBuilder result = new StringBuilder();
        //result.Append("<p class=\"BookingStatusHead\">" + Status.GetStatusTitleById(short.Parse(strBookingStatus)) + "</p>\r\n");
        //result.Append("<div id=\"BookingProductsStatusBar\" style=\"margin:0px;padding:0px\">");

        //result.Append(GenBookingStatusProduct());

        //result.Append("</div>\r\n");
        result.Append("<div class=\"ProcessCheckTitel\"><p><span>1</span>Payment </p><p><span>2</span>Input</p><p><span>3</span>Confirm</p><div style=\"clear:both;\"></div></div>");
        //result.Append("<p><span>6</span>Payment to supplier</p><p><span>7</span>Check In</p><p><span>8</span>Order Complete</p><p><span>9</span>Receive Receipt</p>\r\n");
        result.Append("\r\n");
        return result.ToString();
    }

    protected string GenBookingStatusProduct()
    {
        short shrStatusID = 0;
        if (string.IsNullOrEmpty(this.qBokingStatus))
            shrStatusID = 68;
        else
            shrStatusID = short.Parse(this.qBokingStatus);

        Status cStatus = new Status();
        StringBuilder cResult = new StringBuilder();
        cResult.Append("<p class=\"status_head\">Booking Product Status</p>");
        int count = 1;
        
        List<object> StatusProduct = cStatus.GetStatusByCatIdBooking(3);
        foreach (Status item in StatusProduct)
        {
            if (count == StatusProduct.Count)
                cResult.Append("<p><a href=\"booking_list.aspx?bs=" + shrStatusID + "&bps=" + item.StatusID + "\" id=\"" + item.StatusID + "\" onclick=\"Getpage('" + shrStatusID + "','" + item.StatusID + "');return false;\">" + item.Title + "</a></p>");
            else
                cResult.Append("<p><a href=\"booking_list.aspx?bs=" + shrStatusID + "&bps=" + item.StatusID + "\" id=\"" + item.StatusID + "\" onclick=\"Getpage('" + shrStatusID + "','" + item.StatusID + "');return false;\">" + item.Title + "</a> &nbsp;|&nbsp;</p>");

            count = count + 1;
        }

        return cResult.ToString();

    }
    
}