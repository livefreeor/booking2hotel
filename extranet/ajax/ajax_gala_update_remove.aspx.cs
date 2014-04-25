using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_gala_update_remove : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId 
        {
            get { return Request.QueryString["oid"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                if (this.IsstaffDelete())
                {
                    if (!string.IsNullOrEmpty(this.qOptionId))
                    {
                        Response.Write(RemoveGala());
                        //Response.Write("HELLO");
                    }
                    //Response.Write("HELLO");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }


        public string RemoveGala()
        {
            string result = "False";

            ProductGalaExtra cOptionGala = new ProductGalaExtra();
            int OptionId = int.Parse(this.qOptionId);
            try
            {
                bool ret = cOptionGala.RemoveOptionGala(this.CurrentProductActiveExtra, OptionId, false);
                result = ret.ToString();
            }
            catch (Exception ex)
            {
                result = "error: " + ex.Message;
            }

            return result;
        }
    }
}