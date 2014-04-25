using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_facility_template_manage_save : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {

                Response.Write(SaveFac());
                Response.End();
                
            }
            
        }

        public string SaveFac()
        {
            string Iscompleted = "False";
            try
            {

                string[] arrVal = Request.Form["ChkInsert"].Split(',');
                ProductFacilitytempalte cFacTemplate = new ProductFacilitytempalte();
                foreach (string val in arrVal)
                {
                    string TitleEN = Request.Form["fac_eng_" + val].Trim().Hotels2SPcharacter_remove();
                    string TitleTh = Request.Form["fac_thai_" + val].Trim().Hotels2SPcharacter_remove(); ;
                    byte Cat_Id = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropFacCat"]);

                    
                   
                    cFacTemplate.InsertNewFacTemplate(Cat_Id, TitleEN, TitleTh);
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