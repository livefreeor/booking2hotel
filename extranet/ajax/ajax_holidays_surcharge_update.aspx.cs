using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class ajax_holidays_surcharge_updatee : Hotels2BasePageExtra_Ajax
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {

                if (this.IsStaffEdit())
                {
                    //Response.Write("HELLO");
                    Response.Write(SupplierList());
                }
                else
                {
                    Response.Write("method_invalid");
                }

                Response.End();


            }
            
            
        }

        public string SupplierList()
        {
            string strResult = "False";
            

            try
            {
                ProductsupplementExtranet cSupplement = new ProductsupplementExtranet();

                string[] HolidayList = Request.Form["checked_holiday_list"].Split(',');

                int intProductId = this.CurrentProductActiveExtra;
                short shrSupplierId = this.CurrentSupplierId;

                foreach (string holidayKey in HolidayList)
                {
                    string DateTitle = Request.Form["txtSuptitle_" + holidayKey];
                    DateTime dDAteholiday = Request.Form["hd_txtDateStart_" + holidayKey].Hotels2DateSplitYear("-");

                    cSupplement.UpdateOptionSupplement(intProductId, int.Parse(holidayKey), DateTitle, dDAteholiday);

                }

                strResult = "True";
            }
            catch (Exception ex)
            {
                Response.Write("error : " + ex.Message);
                Response.End();
            }
           
            
            return strResult;
        }

        
        
    }
}