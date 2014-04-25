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
    public partial class admin_ajax_gala_list : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //Response.Write("HELLO");
                Response.Write(getGalaList());
                Response.End();
                
            }
        }

        public string getGalaList()
        {
            StringBuilder result = new StringBuilder();
            try
            {
                Staffs.Staff cStaff = this.CurrentStaffobj;
                short shrSupplier = this.CurrentSupplierId;
                ProductGalaExtra cOptionGalaExtra = new ProductGalaExtra();
                List<object> IListGala = cOptionGalaExtra.GetGalaExtraNetList(this.CurrentProductActiveExtra, shrSupplier);

                if (IListGala.Count > 0)
                {
                    result.Append("<table class=\"tbl_acknow\" cellspacing=\"2\">");



                    result.Append("<tr class=\"header_field\"><th>Date Gala</th><th>Title</th><th>Amount</th><th>Edit</th><th>Delete</th></tr>");
                    int count = 1;
                    foreach (ProductGalaExtra gala in IListGala)
                    {

                        result.Append("<tr bgcolor=\"#ffffff\" align=\"center\">");
                        result.Append("<td width=\"15%\"><input type=\"checkbox\" checked=\"checked\" style=\"display:none;\" value=\"" + count + "\" name=\"galaList_checked\" />" + gala.DatePrice.ToString("dd-MMM-yyyy") + "<input type=\"hidden\" value=\"" + gala.DatePrice.ToString("yyyy-MM-dd") + "\" name=\"hd_gala_list_" + count + "\" />");
                        result.Append("<input type=\"hidden\" name=\"hd_for_adult_" + count + "\" value=\"" + gala.ForAdult+ "\" /></td>");
                        result.Append("<td width=\"50%\" align=\"left\">" + gala.Title + "</td>");
                        result.Append("<td width=\"15%\">" + gala.Price.ToString("#,0") + "</td>");
                        result.Append("<td width=\"10%\"><img src=\"../../images_extra/edit.png\" style=\"cursor:pointer;\" onclick=\"EditGala('" + gala.PriceId + "','" + gala.OptionId + "');return false;\" /></td>");
                        result.Append("<td width=\"10%\"><img src=\"../../images_extra/bin.png\" style=\"cursor:pointer;\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure you want to delete it? Ok, Cancel' ,'DelGala(" + gala.OptionId + ")');return false;\"  /></td>");

                        result.Append("");
                        result.Append("</tr>");
                        count = count + 1;

                    }
                    result.Append("");
                    result.Append("</table>");

                }
                else
                {
                    result.Append("<div class=\"box_empty\">");
                    result.Append("");
                    result.Append("<p><label>No Rate Gala</label> Please Insert new one.</p>");
                    result.Append("");
                    result.Append("</div>");
                }

            }
            catch (Exception ex)
            {
                result.Append(ex.Message);
            }
            return result.ToString();
        }
        
        
    }
}