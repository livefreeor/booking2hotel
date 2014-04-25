using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_allotment_date_range : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("TEST");
            if (!string.IsNullOrEmpty(Request.QueryString["oid"]))
            {
                int intOptionId = int.Parse(Request.QueryString["oid"]);
                short shrSupplierId = short.Parse(Request.QueryString["supid"]);
                //Response.Write(intOptionId);
                Allotment cAllotment = new Allotment();
                ArrayList arr = cAllotment.getDateRangeByOptionId(intOptionId, shrSupplierId);

                lblDateStart.Text = Convert.ToDateTime(arr[0]).ToString("d-MMM-yyyy");
                lbldateEnd.Text = Convert.ToDateTime(arr[1]).ToString("d-MMM-yyyy");
                
            }
            else
            {
                //Request.QueryString["proId"].ToString();
                Response.Write("TESTssss");
            }
            
        }

       
    }
}