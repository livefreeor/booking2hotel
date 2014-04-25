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
using Hotels2thailand.Suppliers;


public partial class ajax_booking_product_list_supplier_contact : System.Web.UI.Page
{
    public string qBookingSupplierId
    {
        get
        {
            return Request.QueryString["supid"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Response.Write(getBookingProductSupplierContact());
            Response.Flush();
        }
    }
    public string getBookingProductSupplierContact()
    {
        SupplierContactPhoneEmail SupContact = new SupplierContactPhoneEmail();
       
        StringBuilder result = new StringBuilder();
        
        result.Append("<div class=\"supplier_contact\">");
        result.Append("<div class=\"supplier_contact_res\">");
        result.Append("");
        result.Append("<p class=\"dep_name\">Sales & Reservation</p>");
        foreach (KeyValuePair<int, string> Res in SupContact.GetStaffContact(short.Parse(this.qBookingSupplierId), "1,3"))
        {
            result.Append("<p class=\"contact_name\"><img src=\"../../images/dot.png\"/>&nbsp;" + Res.Value + "</p>");
            result.Append("<div class\"contact_list\">");
            
            foreach (KeyValuePair<int, string> Phone in SupContact.GetContactPhone(Res.Key,"1"))
            {
                result.Append("<p class=\"contact_list_item\"> <span style=\"color:#3f5d9d\">Phone: </span>" + Phone.Value + "</p>");
               
            }
            
            result.Append("</div>");

            result.Append("<div class\"contact_list\">");

            foreach (KeyValuePair<int, string> Phone in SupContact.GetContactPhone(Res.Key,  "2"))
            {
                result.Append("<p class=\"contact_list_item\"> <span style=\"color:#3f5d9d\">Cell Phone: </span>" + Phone.Value + "</p>");
                
            }

            result.Append("</div>");

            result.Append("<div class\"contact_list\">");

            foreach (KeyValuePair<int, string> Phone in SupContact.GetContactPhone(Res.Key, "3"))
            {
                result.Append("<p class=\"contact_list_item\"> <span style=\"color:#3f5d9d\">Fax: </span>" + Phone.Value + "</p>");
                
            }

            result.Append("</div>");

            result.Append("<div class\"contact_list\">");

            IDictionary<int, string> idicEmail = SupContact.GetContactEmail(Res.Key);

            string mailTo = string.Empty;
            string MailList = string.Empty;
            foreach (KeyValuePair<int, string> Email in idicEmail)
            {

                MailList = MailList + "<p class=\"contact_list_item\"><span style=\"color:#3f5d9d\">Email :</span><a href=\"mailto:#%#\">" + Email.Value + "</a></p>";

                mailTo = mailTo + Email.Value + ";";
                
            }
            result.Append(MailList.Replace("#%#", mailTo));
            result.Append("</div>");
        }
        
        
        result.Append("</div>");
        result.Append("<div class=\"supplier_contact_acc\">");
        result.Append("<p class=\"dep_name\">Account</p>");
        foreach (KeyValuePair<int, string> Acc in SupContact.GetStaffContact(short.Parse(this.qBookingSupplierId), "2"))
        {
            result.Append("<p class=\"contact_name\"><img src=\"../../images/dot.png\"/>&nbsp;" + Acc.Value + "</p>");
            result.Append("<div class\"contact_list\">");

            foreach (KeyValuePair<int, string> Phone in SupContact.GetContactPhone(Acc.Key,"1"))
            {
                result.Append("<p class=\"contact_list_item\"><span style=\"color:#3f5d9d\">Phone: </span>" + Phone.Value + "</p>");

            }

            result.Append("</div>");

            result.Append("<div class\"contact_list\">");

            foreach (KeyValuePair<int, string> Phone in SupContact.GetContactPhone(Acc.Key, "2"))
            {
                result.Append("<p class=\"contact_list_item\"><span style=\"color:#3f5d9d\">Cell Phone: </span>" + Phone.Value + "</p>");

            }

            result.Append("</div>");

            result.Append("<div class\"contact_list\">");

            foreach (KeyValuePair<int, string> Phone in SupContact.GetContactPhone(Acc.Key,  "3"))
            {
                result.Append("<p class=\"contact_list_item\"> <span style=\"color:#3f5d9d\">Fax: </span>" + Phone.Value + "</p>");

            }

            result.Append("</div>");

            result.Append("<div class\"contact_list\">");


            string mailTo = string.Empty;
            string MailList = string.Empty;
            foreach (KeyValuePair<int, string> Email in SupContact.GetContactEmail(Acc.Key))
            {

                MailList = MailList + "<p class=\"contact_list_item\"><span style=\"color:#3f5d9d\">Email :</span><a href=\"mailto:#%#\">" + Email.Value + "</a></p>";

                mailTo = mailTo + Email.Value + ";";
                
                

            }
            result.Append(MailList.Replace("#%#", mailTo));
            result.Append("</div>");
        }
        result.Append("");
        result.Append("");
        
        result.Append("</div>");
        result.Append("<div style=\"clear:both;\"></div>");
        result.Append("</div>");
        return result.ToString();
    }
    

   
}