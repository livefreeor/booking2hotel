using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_condition_policy_list : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qConditionId
        {
            get { return Request.QueryString["cdid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qConditionId))
                {
                    Response.Write(GetPolicyList(int.Parse(this.qConditionId)));
                    //Response.Write(Request.QueryString["cdid"]);
                }
                Response.End();
            }
        }

        public string GetPolicyList(int intConditionId)
        {
            ProductConditionContentExtra cPolicyExtra = new ProductConditionContentExtra();
            List<object> ListPolicy = cPolicyExtra.GetListConditionDetail_policyByConditionId(intConditionId, 1);
            string itemInsert = string.Empty;
            int count = 0;
            foreach (ProductConditionContentExtra policy in ListPolicy)
            {
                int intContentId = policy.ContentId;


                itemInsert = itemInsert  + "<div class=\"policy_list_item\" id=\"policy_list_item_" + intContentId + "\">";
                itemInsert = itemInsert + "<table width=\"100%\"><tr>";
                itemInsert = itemInsert + "<td width=\"10px\"><img src=\"../../images/greenbt.png\" /><input style=\"display:none;\" checked=\"checked\"  type=\"checkbox\" name=\"policylist_to_update\" title=\""+ count +"\" value=\"" + intContentId + "\" /></td>";
                itemInsert = itemInsert + "<td width=\"100px\"><input type=\"textbox\" readonly=\"readonly\" name=\"policy_type_" + intContentId + "\" value=\"" + policy.Title + "\"  class=\"Extra_textbox\" /></td>";
                itemInsert = itemInsert + "<td width=\"87%\"><input type=\"textbox\" name=\"policy_" + intContentId + "\" value=\"" + policy.Detail + "\" style=\"width:500px\" class=\"Extra_textbox\" /></td>";
                itemInsert = itemInsert + "<td width=\"5%\"><img src=\"/images_extra/bin.png\" style=\"cursor:pointer;\" onclick=\"removeEle('policy_list_item_" + intContentId + "');\"  /></td>";
                itemInsert = itemInsert + "</tr>";
                itemInsert = itemInsert + "</table></div>";


                count = count + 1;
            }

            return itemInsert;
        }

        
    }
}