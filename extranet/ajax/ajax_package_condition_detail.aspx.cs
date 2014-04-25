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
    public partial class admin_ajax_package_condition_detail : Hotels2BasePageExtra_Ajax
    {
        
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qConditionId
        {
            get { return Request.QueryString["con"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qConditionId))
                    {
                        try
                        {

                            int intCOnditionId = int.Parse(this.qConditionId);

                            string Price = string.Empty;
                            string NumGuest = string.Empty;

                            ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                            cConditionExtra = cConditionExtra.getConditionByConditionId(intCOnditionId);


                            //ConditionDataBind
                            byte NumAdult = cConditionExtra.NumAult;
                            byte NumChild = cConditionExtra.NumChild;
                            bool IsAdult = cConditionExtra.IsAdult;

                            if (IsAdult)
                                NumGuest = NumAdult.ToString();
                            else
                                NumGuest = NumChild.ToString();



                            //Price 
                            ProductPriceExtra_period cConditionPrice = new ProductPriceExtra_period();
                            cConditionPrice = cConditionPrice.getPricePackageByConditionId(intCOnditionId);
                            Price = cConditionPrice.Price.ToString("#.##");


                            string result = NumGuest + ";" + Price;
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