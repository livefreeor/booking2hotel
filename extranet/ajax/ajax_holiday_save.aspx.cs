using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_holiday_save : Hotels2BasePageExtra_Ajax
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                    Response.Write(InsertHolidaySupplement());
                   //Response.Write("HELLO");

                    
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
                
            }
        }

        public string InsertHolidaySupplement()
        {
            string result = "False";
            ProductsupplementExtranet cSupplementExtra = new ProductsupplementExtranet();
            
            DateTime dDateHoliday = Request.Form["hd_date_insert_holiday"].Hotels2DateSplitYear("-");
            DateTime dDateHolidayEnd = Request.Form["hd_date_insert_holiday_end"].Hotels2DateSplitYear("-");

            string Datetitle = Request.Form["holiday_title"];
            if (Request.Form["date_type_insert"] == "1")
            {
                try
                {
                    cSupplementExtra.InsertOptionSupplement(this.CurrentSupplierId, this.CurrentProductActiveExtra, Datetitle, dDateHoliday);
                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("error: Single" + ex.Message);
                    Response.End();
                }
            }
            else
            {
                try
                {
                    int DateDiff = dDateHolidayEnd.Subtract(dDateHoliday).Days;
                    DateTime dDateCurrent = DateTime.Now;
                    for (int days = 0; days <= DateDiff; days++)
                    {
                        dDateCurrent = dDateHoliday.AddDays(days);
                        cSupplementExtra.InsertOptionSupplement(this.CurrentSupplierId, this.CurrentProductActiveExtra, Datetitle, dDateCurrent);
                    }

                    result = "True";
                }
                catch (Exception ex)
                {
                    Response.Write("error: Period" + ex.Message);
                    Response.End();
                }
            }


            

            
            return result;
        }
        
        
    }
}