using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_product_list : Hotels2BasePageExtra_Ajax
    {
        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write("HELLO");
                Response.Flush();
            }
        }

        
       

    }
}
