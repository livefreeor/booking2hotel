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
    public partial class admin_ajax_product_location_insertForm : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
            {
                Product cProduct = new Product();
                short DestinationId = cProduct.GetProductById(int.Parse(this.qProductId)).DestinationID;
                Location cLocation = new Location();
                location_list.DataSource = cLocation.GetLocationListByDesId(DestinationId);
                location_list.DataTextField = "Title";
                location_list.DataValueField = "LocationID";
                
                location_list.DataBind();
                ProductLocation cProductLocation = new ProductLocation();

                List<object> ListItem = cProductLocation.getLocationListByProductId(int.Parse(this.qProductId));

                foreach (ListItem chkitem in location_list.Items)
                {
                    chkitem.Attributes.Add("id", chkitem.Value);
                    foreach (ProductLocation item in ListItem)
                    {
                        if (chkitem.Value == item.LocationID.ToString())
                        {
                            chkitem.Selected = true;

                           
                        }
                    }
                    
                }
            }
            
        }
        

       
        
    }
}