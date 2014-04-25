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
    public partial class admin_ajax_member_price_insert : Hotels2BasePageExtra_Ajax
    {
        
        public string qmemberId
        {
            get { return Request.QueryString["mid"]; }

        }

        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {

                if (this.IsStaffAdd())
                {
                    //Response.Write("DDDD");
                    Response.Write(InsertConditionPriceAndDAte());
                    Response.End();
                }
                else
                {
                    Response.Write("method_invalid");
                }

                
            }
        }


        public int InsertConditionPriceAndDAte()
        {
            MemberPrice cMemberPrice = new MemberPrice();

            DateTime dDateStart = Request.Form["hd_member_date_start"].Hotels2DateSplitYear("-");
            DateTime dDateEnd = Request.Form["hd_member_date_end"].Hotels2DateSplitYear("-");

            decimal decAmount = decimal.Parse(Request.Form["member_amount"]);


           return cMemberPrice.InsertDiscount(this.CurrentProductActiveExtra, decAmount, dDateStart, dDateEnd);
           // return 1;
        }
        
    }
}