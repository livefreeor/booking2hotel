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


public partial class ajax_booking_product_list_supplier_bank : System.Web.UI.Page
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
            //Response.Write(Request.Url.ToString());
            Response.Write(getBookingSupplierBank());
            Response.Flush();
        }
    }

    public string getBookingSupplierBank()
    {
        SupplierAccount cBank = new SupplierAccount();

       List<object> cBankList =  cBank.getSupplierAccountAllBySupplierID(short.Parse(this.qBookingSupplierId));

        StringBuilder result = new StringBuilder();
        
        result.Append("<div class=\"supplier_contact\">");
        
        result.Append("");
        result.Append("<p class=\"dep_name\">Bank Account Information</p>");
        result.Append("<div class\"contact_list\">");
        foreach (SupplierAccount BankItem in cBankList)
        {

            result.Append("<p><img src=\"../../images/dot.png\"/><a href=\"../../admin/supplier/supplier_account_list.aspx?supid=" + this.qBookingSupplierId + "&acid=" + BankItem .AccountId+ "\" target=\"_Blank\" >&nbsp;" + BankItem.BankTitle + "(" + BankItem.AccountTypeTitle + ")&nbsp;:&nbsp;" + BankItem.AccountName + "</a></p>");
            
        }
        result.Append("</div>");

        result.Append("<div style=\"clear:both;\"></div>");
        result.Append("</div>");
        return result.ToString();
    }
    

   
}