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
    public partial class admin_ajax_product_location_save : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public string qLocationChecked
        {
            get { return Request.QueryString["CheckedLoc"]; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat) )
            {
                string[] arrCheck = this.qLocationChecked.Hotels2RightCrl(1).Split(',');
                bool Iscompleted = true;
                int ret = 0;
                ProductLocation cProductLocation = new ProductLocation();
                if (!string.IsNullOrEmpty(this.qLocationChecked))
                {
                   
                    cProductLocation.DeleteProductLocationNotInCustomCheck(int.Parse(this.qProductId), this.qLocationChecked.Hotels2RightCrl(1));

                    foreach (string Item in arrCheck)
                    {
                        if (cProductLocation.IsHaveLocation(short.Parse(Item), int.Parse(this.qProductId)) != 1)
                        {
                            ret = cProductLocation.Insert(int.Parse(this.qProductId), short.Parse(Item));

                        }
                    }

                }
                else
                {
                    cProductLocation.DeleteProductLocationAllByProductId(int.Parse(this.qProductId));
                }
                
                Response.Write(Iscompleted);
                Response.End();
            }
            
        }

        
        
    }
}