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
    public partial class admin_ajax_member_list_update_activation : Hotels2BasePageExtra_Ajax
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
                    Response.Write(UpdateStatus());
                    Response.End();


                    //try
                    //{
                        
                    //}
                    //catch (Exception ex)
                    //{
                    //    Response.Write(ex.Message + "--" + ex.StackTrace);
                    //    Response.End();
                    //}

                    
                }
                else
                {
                    Response.Write("method_invalid");
                }

            }
        }

        public bool UpdateStatus()
        {
            bool ret = true;

            string[] CheckedCus = Request.Form["cus_checked"].Split(',');
            Member_customer cMember = new Member_customer();
            foreach(string item in CheckedCus)
            {

                int intCusid = int.Parse(item);

                ret = cMember.UpdateActivate(intCusid);
            }
            


            

           return ret;
        }
    }
}