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

public partial class ajax_supplier_contact_quick_save : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {

            Response.Write(ContactQuickSave());
            Response.End();

        }


    }

    public string ContactQuickSave()
    {
        string result = "false";
       


        try
        {
            //SupplierContact cSupContact = new SupplierContact();
            string DepSel = Request.Form["chkDepartureQuick"];
            string Chekphone = Request.Form["chkPhoneInSert"];
            string ChckEmail = Request.Form["chkEmailInSert"];


            string[] arrPhoneKey = Chekphone.Split(',');
            string[] arrEmailKey = ChckEmail.Split(',');
            string[] arrDepart = DepSel.Split(',');

            foreach (string dep in arrDepart)
            {
                int intContact = 0;
                intContact = intContact = SupplierContact.InsertStaffContact(short.Parse(Request.QueryString["supid"]),
                    byte.Parse(dep), Request.Form["txtContactName"], "", true);

                foreach (string phone in arrPhoneKey)
                {
                    string Phone = Request.Form["txtPhone_" + phone];
                    if (!string.IsNullOrEmpty(Phone))
                    {
                        SupplierContact.InsertStaffPhone(byte.Parse(Request.Form["selPhoneCat_" + phone]), intContact,
                        Request.Form["txtCountryCode_" + phone], Request.Form["txtLocal_" + phone],
                        Phone, true);
                    }
                    
                }

                foreach (string mail in arrEmailKey)
                {
                    string strMail = Request.Form["txtEmail_" + mail];
                    if (!string.IsNullOrEmpty(strMail))
                        SupplierContact.InsertStaffEmail(intContact, strMail, true);
                }


            }

            result = "true";
        }
        catch (Exception ex)
        {
            Response.Write("error! Please Contact RD team:" + ex.Message);
            Response.End();
        }
        return result;
    }

    
}