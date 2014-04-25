using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand;
using Hotels2thailand.Suppliers;

public partial class ajax_supplier_contact_quick : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
           
            Response.Write(getSupplierContactAddnew());
            Response.Flush();
        }
    }
    public string getSupplierContactAddnew()
    {
        IDictionary<int,string> idicPhoneCat = SupplierContact.getPhoneCat();

        StringBuilder result = new StringBuilder();

        result.Append("<form id=\"Contact_quick_add\" action=\"\" >");

        result.Append("<div class=\"formbox_head\">Contact Quick Add</div>");
        result.Append("<div class=\"formbox_body\">");
        result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Contact Name</td><td>&nbsp;<input type=\"text\" id=\"txtContactName\"  class=\"TextBox_Extra_normal_small\" style=\"width:250px;\" name=\"txtContactName\" /></td></tr>");
        result.Append("<tr style=\"background-color:#ffffff;\" ><td>&nbsp;Phone</td><td id=\"phoneinsertBlock\">");
        result.Append("<div  class=\"phoneinsert\" >");
        result.Append("<input =\"checkbox\" style=\"display:none;\" name=\"chkPhoneInSert\" checked=\"checked\" value=\"1\" />");
                    result.Append("<table>");
                        result.Append("<tr>");
                            result.Append("<td>");

                            result.Append("<select id=\"selPhoneCat_1\" OnChange =\"PhoneType('selPhoneCat_1');\" class=\"DropDownStyleCustom_small\" name=\"selPhoneCat_1\">");
                            result.Append("<option value=\"1\" >Phone</option>");
                            result.Append("<option value=\"2\" >Mobile</option>");
                            result.Append("<option value=\"3\" >Fax</option>");
                            result.Append("</select>");
                            result.Append("</td>");
                            result.Append("<td>");
                            result.Append("<input type=\"text\" id=\"txtCountryCode_1\" value=\"66\" class=\"TextBox_Extra_normal_small\" name=\"txtCountryCode_1\"  maxlength=\"3\"   style=\" width:30px; background-color:#faffbd\" />");
                            result.Append("</td>");
                            result.Append("<td>");
                            result.Append("<input type=\"text\" id=\"txtLocal_1\" value=\"2\" class=\"TextBox_Extra_normal_small\" name=\"txtLocal_1\"  maxlength=\"2\"   style=\" width:20px; background-color:#faffbd\"  />");
                                  
                            result.Append("</td>");
                            result.Append("<td><input type=\"text\" id=\"txtPhone_1\" class=\"TextBox_Extra_normal_small\" OnBlur=\"CheckValue(1);return false;\"  OnKeyUp=\"GetPhoneinsert(1);return false;\" name=\"txtPhone_1\"   style=\" width:200px;\" /></td>");
                           
                            result.Append("</tr>");
                            result.Append("</table>");
                            result.Append("</div>");
        result.Append("</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Email</td><td id=\"emailinsertBlock\">");
        result.Append("<div class=\"emailinsert\">");
        result.Append("&nbsp;<input type=\"checkbox\" checked=\"checked\" name=\"chkEmailInSert\" style=\"display:none;\" value=\"1\" />");
        result.Append("");
        result.Append("<input type=\"text\" id=\"txtEmail_1\" name=\"txtEmail_1\" OnKeyUp=\"GetEmailinsert(1);return false;\"  OnBlur=\"CheckValueEmail(1);return false;\" class=\"TextBox_Extra_normal_small\" style=\"width:250px;\" />");
        result.Append("</div>");
        result.Append("</td></tr>");
        result.Append("<tr style=\"background-color:#ffffff; height:25px;\" ><td>&nbsp;Select Dep</td><td>");
        SupplierContact cSupplierContact = new SupplierContact();
        IDictionary<int,string> idicDepart = cSupplierContact.getdicDepartment();
        result.Append("");
        foreach (KeyValuePair<int, string> item in idicDepart)
        {
            result.Append("<p class=\"dep\"><input type=\"checkbox\" name=\"chkDepartureQuick\" value=\"" + item.Key + "\" /> " + item.Value + "</p>");
        }
        
        result.Append("");
        result.Append("</td></tr>");
        result.Append("</table>");
        result.Append("</div>");
        result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"SupplierContacQuickSave();\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");
        
        result.Append("</form>");
       

        return result.ToString();
    }


    
}