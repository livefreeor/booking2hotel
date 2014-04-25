using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Member ;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_member_price_update : Hotels2BasePageExtra_Ajax
    {
        
        public string qDisId
        {
            get { return Request.QueryString["dis_id"]; }

        }

        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {

                if (this.IsStaffEdit())
                {
                    try
                    {
                        Response.Write(UpdateDicount());
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message + "--" + ex.StackTrace);
                    }
                    Response.End();
                }
                else
                {
                    Response.Write("method_invalid");
                }

                
            }
        }


        public bool UpdateDicount()
        {
            MemberPrice cMemberPrice = new MemberPrice();
            
            DateTime dDateStart = Request.Form["hd_date_start_" + this.qDisId].Hotels2DateSplitYear("-");
            DateTime dDateEnd = Request.Form["hd_date_end_"+this.qDisId].Hotels2DateSplitYear("-");

            decimal decAmount = decimal.Parse(Request.Form["discount_amount_" + this.qDisId]);


           return cMemberPrice.UpdateDisCount(int.Parse(this.qDisId),dDateStart, dDateEnd,decAmount, this.CurrentProductActiveExtra);
           // return 1;
        }
        
    }
}