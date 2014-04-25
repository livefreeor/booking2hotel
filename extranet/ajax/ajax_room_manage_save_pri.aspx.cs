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
using Hotels2thailand;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_room_manage_save_pri : Hotels2BasePageExtra_Ajax
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    Response.Write(Savepri());
                }
                else
                {
                    Response.Write("method_invalid");
                }
                
                Response.Flush();
            }
        }

    
        
        public string Savepri()
        {
            string result = "False";
            string [] fac = Request.QueryString["param"].Hotels2RightCrl(1).Split(',');

            foreach(string val in fac)
            {
                Option cOption = new Option();
                cOption.UpdatePriority(int.Parse(val.Split(';')[0]), byte.Parse(val.Split(';')[1]));

                result = "True";
            }

            return result;
        }
        
    }
}