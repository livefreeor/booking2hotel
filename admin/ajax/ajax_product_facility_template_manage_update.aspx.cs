using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_facility_template_manage_update : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Write(UpdateFac());
            Response.End();
        }


        public string UpdateFac()
        {
            string Iscompleted = "False";
            try
            {
                
                string[] arrVal = Request.Form["chk_fac_toUpdate"].Split(',');

                ProductFacilitytempalte cFacTemplate = new ProductFacilitytempalte();
                foreach (string val in arrVal)
                {
                    string TitleEN = Request.Form["facUpdate_EN_" + val].Trim().Hotels2SPcharacter_remove();
                    string TitleTh = Request.Form["facUpdate_TH_" + val].Trim().Hotels2SPcharacter_remove();
                  
                    cFacTemplate.UpdateFacility(int.Parse(val), TitleEN, TitleTh);
                }

                Iscompleted = "true";

            }
            catch (Exception ex)
            {
                Iscompleted = ex.Message;
            }

            return Iscompleted;


        }
        
    }
}