using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Member ;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_member_detail_update : Hotels2BasePageExtra_Ajax
    {
        
        public string qmemberId
        {
            get { return Request.QueryString["mid"]; }

        }

        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {

                if (this.IsStaffEdit())
                {

                    Response.Write(UpdateCusDetail());
                    Response.End();
                }
                else
                {
                    Response.Write("method_invalid");
                }



                //try
                //{
                   
                //}
                //catch (Exception ex)
                //{
                //    Response.Write(ex.Message + "--" + ex.StackTrace);
                //    Response.End();
                //}
                
                
            }
        }

        public bool UpdateCusDetail()
        {
            bool ret = true;
            Member_customer cMember = new Member_customer();
            int intCusid = int.Parse(Request.Form["cus_id"]);
            string strNamefull = Request.Form["txt_full_name"];
            string strEmail = Request.Form["txt_email"];
            byte bytCountry = byte.Parse(Request.Form["sel_country"]);
            string strPhone = Request.Form["txt_phone"];

            if (Request.Form["sel_bh_year"] == "0" || Request.Form["sel_bh_month"] == "0" || Request.Form["sel_bh_day"] == "0")
            {
                ret = cMember.UpdateCustomer(strNamefull, intCusid, strEmail, null, bytCountry);
            }
            else
            {
                DateTime dDateBirth = new DateTime(int.Parse(Request.Form["sel_bh_year"]), int.Parse(Request.Form["sel_bh_month"]),
                    int.Parse(Request.Form["sel_bh_day"]));

                ret = cMember.UpdateCustomer(strNamefull, intCusid, strEmail, dDateBirth, bytCountry);
            }
           
            //DateTime dDateBirth = Request.Form["hd_txt_date_birth"].Hotels2DateSplitYear("-");

            

           ret =  cMember.UpdatePhone(strPhone, intCusid);

           return ret;
        }
    }
}