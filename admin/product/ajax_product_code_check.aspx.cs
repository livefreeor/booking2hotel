using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_code_check : System.Web.UI.Page
    {
        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }

        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$txtHotelCode"]))
                {
                    Product cProduct = new Product();

                    //Response.Write(Request.Form["ctl00$ContentPlaceHolder1$txtHotelCode"]);
                    //Response.Write(Request.Form["ctl00$ContentPlaceHolder1$txtHotelCode"].ToString().Trim());
                    Response.Write(cProduct.CheckProductCode(Request.Form["ctl00$ContentPlaceHolder1$txtHotelCode"]).ToString().Trim()); 
                    Response.End();
                }
            }
            
        }
        


        
    }
}