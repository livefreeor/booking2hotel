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


public partial class ajax_booking_product_list_supplier_detail : System.Web.UI.Page
{
    public string qBookingProductId
    {
        get
        {
            return Request.QueryString["bpid"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getBookingProductSupplier());
            Response.Flush();
        }
    }
    public string getBookingProductSupplier()
    {
        BookingSupplierDisplay cSupDetail = new BookingSupplierDisplay();
        cSupDetail = cSupDetail.getSupplierDetailByBookingProductId(int.Parse(this.qBookingProductId));
        StringBuilder result = new StringBuilder();
        result.Append("<h4><img   src=\"../../images/content.png\" /> Supplier Detail</h4>");
        result.Append("<p class=\"contentheadedetail\">List Supplier of This Product, you can Change or Add Supplier to List</p><br />");
        result.Append("<table cellpadding=\"0\" id=\"supplier_detail_" + this.qBookingProductId + "\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center; font-size:12px;\">");
        result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:10px;line-height:10px;\"><td colspan=\"2\" style=\"font-size:14px;font-weight:bold\"></td></tr>");
        result.Append("<tr class=\"trRow\"><td >Title</td><td style=\"color:#333333\">" + cSupDetail.SupTitle + "</td></tr>");
       // result.Append("<tr class=\"trRowalten\"><td >Supplier Title Common</td><td style=\"color:#333333\">" + cSupDetail.SupTitle_common + "</td></tr>");
        result.Append("<tr class=\"trRow\"><td >Title Company</td><td style=\"color:#333333\">" + cSupDetail.SupTitle_company + "</td></tr>");

        result.Append("<tr class=\"trRow\"><td  >Property Address</td><td style=\"color:#333333\">" + cSupDetail.Address + "</td></tr>");
        result.Append("<tr class=\"trRowalten\"><td >Office Address</td><td style=\"color:#333333\">" + cSupDetail.Address_Office + "</td></tr>");
        result.Append("<tr class=\"trRow\"><td >Comment</td><td style=\"color:#333333;padding:3px 0px 3px 0px\">" + cSupDetail.Comment + "</td></tr>");
        result.Append("<tr class=\"trRowalten\"><td >Payment Type </td><td style=\"color:#333333\">" + cSupDetail.PaymentTitle + "</td></tr>");
        result.Append("<tr class=\"trRow\"><td>Payment Type DueDate</td><td style=\"color:#333333\">" + DueDateSup(cSupDetail.PaymentPolicyDueDate) + "</td></tr>");
        
        result.Append("<tr  class=\"trRowalten\"><td >Vat/Service/Local</td><td style=\"color:#333333\">[" + cSupDetail.TaxVAt + "][" + cSupDetail.TaxService + "][" + cSupDetail.TaxLocal + "]</td></tr>");
        //result.Append("<tr  class=\"trRow\"><td >Tax Service</td><td style=\"color:#333333\">" + cSupDetail.TaxService + "</td></tr>");
        //result.Append("<tr  class=\"trRowalten\"><td>Tax Local</td><td style=\"color:#333333\">" + cSupDetail.TaxLocal + "</td></tr>");
        result.Append("<tr  class=\"trRowalten\"><td class=\"supplier_detail_foot\" colspan=\"2\"><a href=\"\" onclick=\"supplier_contact_display('" + cSupDetail.SupplierId + "','" + this.qBookingProductId + "');return false;\">Show Contact</a> &nbsp;|&nbsp;<a href=\"\" onclick=\"supplier_bank_display('" + cSupDetail.SupplierId + "','" + this.qBookingProductId + "');return false;\">Show Bank Detail</a> </td></tr>");
        result.Append("</table>");

        return result.ToString();
    }


    public string DueDateSup(byte? byteDuedate)
    {
        string result = string.Empty;
        if (byteDuedate.HasValue)
        {
            result = byteDuedate.ToString();
        }
        else
        {
            result = "No Data";
        }

        return result;
    }
}