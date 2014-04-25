using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_detail : System.Web.UI.Page
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
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                TitleTextBox.Text = cProduct.Title;
                productCodeText.Text = cProduct.ProductCode;
                ddlStar.SelectedValue = cProduct.Star.ToString();
                ralistInternet.SelectedValue = cProduct.IsHaveInterner.ToString();
                ralistInternetFree.SelectedValue = cProduct.IsInternetFree.ToString();

                ddlPaymentType.SelectedValue = cProduct.PaymentTypeID.ToString();
                ddlDestination.SelectedValue = cProduct.DestinationID.ToString();

                LatitudeTextBox.Text = cProduct.Latitude;

                LongitudeTextBox.Text = cProduct.Longitude;
                commentTextBox.Text = cProduct.Comment;
                txtPhone.Text = cProduct.ProductPhone;
            }
            
        }

        
    }
}