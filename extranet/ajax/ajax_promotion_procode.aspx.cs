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
    public partial class admin_ajax_promotion_procode : Hotels2BasePageExtra_Ajax
    {
        
        public string qPromotionID
        {
            get { return Request.QueryString["pro"]; }
        }

        public string qPromotionCode
        {
            get { return Request.QueryString["code"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qPromotionID) && !string.IsNullOrEmpty(this.qPromotionCode))
                    {
                        try
                        {

                            PromotionExxtranet cProExtra = new PromotionExxtranet();
                            Response.Write(cProExtra.UpdatePromotionProcode(int.Parse(this.qPromotionID), this.qPromotionCode, this.CurrentProductActiveExtra));
                        }
                        catch (Exception ex)
                        {
                            Response.Write("error:" + ex.Message);
                            Response.End();
                        }
                       
                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
            }
            
        }


        
    }
}