using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.Production;

using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_holiday_check : Hotels2BasePageExtra_Ajax
    {
        public string qDAte
        {
            get { return Request.QueryString["d"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {

                ProductsupplementExtranet cProductSupple = new ProductsupplementExtranet();
                DateTime dDate = this.qDAte.Hotels2DateSplitYear("-");
                int IsDuplicatHoliday = cProductSupple.IsSupplement(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDate);


                if (IsDuplicatHoliday > 0)
                    Response.Write(IsDuplicatHoliday.ToString());
                else
                    Response.Write("0");
                
                Response.End();
                
            }
        }


        
    }
}