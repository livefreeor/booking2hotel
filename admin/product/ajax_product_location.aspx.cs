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
    public partial class admin_ajax_product_location : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public byte Current_StaffLangId
        {
            get 
            { 
                Hotels2BasePage cBasePage = new Hotels2BasePage();
                return cBasePage.CurrenStafftLangId;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
            {
                Response.Write(LocationResult());
                Response.End();
                
            }
            
        }

        public string LocationResult()
        {
            ProductLocation cProductLocation = new ProductLocation();
            StringBuilder REsult = new StringBuilder();
            REsult.Append("<h4>Location &nbsp;<a href=\"\" onclick=\"InsertNewLocation();return false;\"><img src=\"../../images/plus.png\"></a></h4>");
            REsult.Append("<ul>");
            

            foreach (ProductLocation item in cProductLocation.getLocationListByProductId(int.Parse(this.qProductId)))
            {
                REsult.Append("<li>");
                REsult.Append(item.LocationTitle);
                REsult.Append("</li>");
            }
            
            REsult.Append("</ul>");
           
            return REsult.ToString();
        }
        
    }
}