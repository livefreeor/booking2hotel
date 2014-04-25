using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;
using Hotels2thailand;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_rate_plan_condition_save : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {
                    
                    Response.Write(InsertNewRatePlan());

                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
            }
        }


        public string InsertNewRatePlan()
        {
            string result = "False";
            try
            {
                int intProductId = this.CurrentProductActiveExtra;
                short shrSupplier = this.CurrentSupplierId;
                ProductCondition_rate_plan cConditionRatePlan = new ProductCondition_rate_plan();
                if (!string.IsNullOrEmpty(Request.Form["checkbox_condition_check"]) && !string.IsNullOrEmpty(Request.Form["CountrySelected"]))
                {
                    string[] arrCountry = Request.Form["CountrySelected"].Split(',');
                    string[] arrConditionId = Request.Form["checkbox_condition_check"].Split(',');
                    foreach (string Country in arrCountry)
                    {
                        byte bytCountryId = byte.Parse(Country);
                        foreach (string conditionId in arrConditionId)
                        {
                            decimal decRatePlanValue = decimal.Parse(Request.Form["rate_plane_value_" + conditionId]);
                            byte bytRatPlanCat = byte.Parse(Request.Form["rate_plan_cat_" + conditionId]);
                            int bytConditionId = int.Parse(conditionId);
                            cConditionRatePlan.intSertRatePlan(this.CurrentProductActiveExtra, bytConditionId, bytCountryId, bytRatPlanCat, null, null, decRatePlanValue, true);
                        }
                    }

                }

                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write("error: " + ex.Message);
                Response.End();
            }
            
            return result;

        }
    }
}