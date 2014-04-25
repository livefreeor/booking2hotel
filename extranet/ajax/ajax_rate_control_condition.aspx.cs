using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_rate_control_condition : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qOptionId))
                {
                    Response.Write(ConditionDrop(int.Parse(this.qOptionId)));
                    //Response.Write("TEST");
                    Response.End();
                }
                
            }
        }

        public string ConditionDrop(int intOptionId)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                ProductConditionExtra cConditionExtra = new ProductConditionExtra();

                List<object> ConditionExtraList = cConditionExtra.getConditionListByOptionId(intOptionId, 1);
                if (ConditionExtraList.Count > 0)
                {
                    result.Append("<select name=\"rate_control_condition\" id=\"rate_control_condition\" class=\"Extra_Drop\" style=\"width:350px;\">");

                    foreach (ProductConditionExtra conditionList in ConditionExtraList)
                    {
                        result.Append("<option value=\"" + conditionList.ConditionId + "\" >" + conditionList.Title + Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast) + "</option>");
                    }

                    result.Append("</select>");
                }
                else
                {
                    result.Append("False");
                }

            }
            catch (Exception ex)
            {
                Response.Write("error#1# condition drop " + ex.Message);
                Response.End();
            }
            return result.ToString();
        }
    }
}