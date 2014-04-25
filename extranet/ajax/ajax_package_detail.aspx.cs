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
    public partial class admin_ajax_package_detail : Hotels2BasePageExtra_Ajax
    {
        
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qOptionId))
                    {
                        try
                        {

                            int intOPtionId = int.Parse(this.qOptionId);

                            Option cOption = new Option();
                            cOption = cOption.getOptionById(intOPtionId);
                            ProductOptionContent cOptionCOntent = new ProductOptionContent();
                            cOptionCOntent = cOptionCOntent.GetProductOptionContentById(intOPtionId, 1);


                            string result = cOptionCOntent.Title + ";" + cOption.Night + ";" + ((DateTime)cOption.BookintDateStart).ToString("yyyy-MM-dd") + ";" + ((DateTime)cOption.BookingDAteEnd).ToString("yyyy-MM-dd") + ";" + ((DateTime)cOption.StayDateStart).ToString("yyyy-MM-dd") + ";" + ((DateTime)cOption.StayDateEnd).ToString("yyyy-MM-dd") + ";" + cOptionCOntent.Detail;
                            Response.Write(result);

                        }
                        catch (Exception ex)
                        {
                            Response.Write("error:" + ex.Message + ex.StackTrace);
                            
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