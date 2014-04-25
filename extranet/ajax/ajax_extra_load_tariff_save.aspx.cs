using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class extranet_ajax_ajax_extra_load_tariff_save : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string conditionName = Request.Form["txt_condition_name"];
            //Response.Write(conditionName);
            //Response.End();

            Response.Write(Loadtariff());
            Response.End(); 


            if (this.Page.IsPostBack)
            {
                
            }
           // Loadtariff();
              
            
        }

        public string Loadtariff()
        {
            string conditionName = Request.Form["txt_condition_name"];

            byte bytNumadult = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_adult"]);
            byte bytNumChild = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_child"]);
            byte bytNumExtra = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_extrabed"]);
            byte bytAbf = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_breakfast"]);

            int PolicyCount = int.Parse(Request.Form["hd_policy_count"]);

            return PolicyCount.ToString();
            
        }
    }
}