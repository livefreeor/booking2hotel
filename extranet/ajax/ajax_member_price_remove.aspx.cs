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
    public partial class admin_ajax_member_price_remove : Hotels2BasePageExtra_Ajax
    {
        
        public string qDisId
        {
            get { return Request.QueryString["dis_id"]; }

        }

        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {

                if (this.IsstaffDelete())
                {
                    Response.Write(UpdateDicount());
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
            
            
            return cMemberPrice.RemoveDiscount(int.Parse(this.qDisId),  this.CurrentProductActiveExtra);
           // return 1;
        }
        
    }
}