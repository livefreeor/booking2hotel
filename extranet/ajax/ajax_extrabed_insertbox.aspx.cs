using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_extrabed_insertbox : Hotels2BasePageExtra_Ajax
    {
        public string qoptionCat
        {
            get { return Request.QueryString["opcat"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(getInsertBox());
                Response.End();
            }
        }

        public string getInsertBox()
        {
            StringBuilder result = new StringBuilder();
            try
            {
                // select Extrabed From ThisProduct 
                Option cOption = new Option();
                //List<object> Extrabed = cOption.GetProductOptionByCatIdAndProductId(this.CurrentProductActiveExtra, 39);
                //cOption = cOption.GetProductOptionTop1_Extrnet(this.CurrentProductActiveExtra, 39, this.CurrentSupplierId);

                byte bytOptionCat = byte.Parse(this.qoptionCat);

                string OptionTitle = string.Empty;
                switch (bytOptionCat)
                {
                    case 39:
                        OptionTitle = "Extra bed";
                        break;
                    case 44:case 43: case 45:
                        OptionTitle = "Transfer";
                        break;
                }
                IList<object> ListOption = cOption.GetProductOptionByCurrentSupplierANDProductIdANDCATID_OpenOnly(this.CurrentSupplierId, this.CurrentProductActiveExtra, bytOptionCat);


                result.Append("<form id=\"extra_bed_insertform\" action=\"\" >");
                if (ListOption.Count > 0)
                {
                    //result.Append("<input type=\"hidden\" name=\"hd_optionId\" id=\"hd_optionId\"  value=\"" + cOption.OptionID + "\" />");
                    
                    result.Append("<table>");
                   
                    result.Append("<tr>");
                    result.Append("<td ><label>Current " + OptionTitle + "</lable></td>");
                    result.Append("<td colspan=\"6\"><select id=\"drop_option\" name=\"drop_option\"  style=\"width:300px;\" class=\"Extra_Drop\">");
                    foreach (Option option in ListOption)
                    {
                        result.Append("<option value=\""+option.OptionID+"\">"+option.Title+"</option>");
                        
                    }
                    
                    result.Append("</select>");
                    result.Append("</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    
                    result.Append("<td><label>Date Range From</label></td>");
                    result.Append("<td><input type=\"text\" id=\"extrabed_date_start\" readonly=\"readonly\" name=\"extrabed_date_start\" class=\"Extra_textbox\" /></td>");
                    result.Append("<td>&nbsp;&nbsp;<label>To</label></td>");
                    result.Append("<td><input type=\"text\" id=\"extrabed_date_end\" readonly=\"readonly\" name=\"extrabed_date_end\" class=\"Extra_textbox\" /></td>");

                    result.Append("<td>&nbsp;&nbsp;<label>" + OptionTitle + " Rate</label></td>");
                    result.Append("<td><input type=\"text\" id=\"extrabed_amount_rate\" name=\"extrabed_amount_rate\" class=\"Extra_textbox_yellow\" /></td>");
                    result.Append("<td id=\"td_btnSaveRate\">&nbsp;&nbsp;&nbsp;<input type=\"button\" class=\"Extra_Button_small_blue\" value=\"Add\" id=\"btnInsertnewExtra\"  /></td>");
                    result.Append("</tr>");
                    result.Append("</table>");
                    
                }

                else
                {
                    result.Append("<div id=\"empty_extrabed\">");
                    result.Append("<p>there are no any item ; Please Insert new before </p>");
                    result.Append("");
                    result.Append("</div>");
                }

                result.Append("</form>");
            }
            catch (Exception ex)
            {
                Response.Write("error : " + ex.Message);
            }

            

            return result.ToString();
        }
    }
}