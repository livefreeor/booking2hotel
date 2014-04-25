using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;


using System.Data.SqlClient;

namespace Hotels2thailand.UI
{
    public partial class product_report : Hotels2BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                Destination dDestinaition = new Destination();
                dropDes.DataSource = dDestinaition.GetDestinationAll();
                dropDes.DataTextField = "Value";
                dropDes.DataValueField = "Key";
                dropDes.DataBind();

                ListItem item = new ListItem("All Destination", "0");
                dropDes.Items.Insert(0, item);

                ProductCategory cProductCat = new ProductCategory();

                dropCat.DataSource = cProductCat.GetProductCategory();
                dropCat.DataTextField = "Value";
                dropCat.DataValueField = "Key";
                dropCat.DataBind();
                
            }
        }



        

        
    }
}