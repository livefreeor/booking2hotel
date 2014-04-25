using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_supplierList_Manage : System.Web.UI.Page
    {
        public string qProductId 
        {
            get { return Request.QueryString["pid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qProductId))
                {
                    Response.Write(getSupplierListByProductID());
                    Response.End();
                }
            }
            
        }
        public string getSupplierListByProductID()
        {
            StringBuilder strREsult = new StringBuilder();
            ProductSupplier cProductSupplier = new ProductSupplier();
            Product cProduct = new Product();
            short SupplierProice = cProduct.GetProductById(int.Parse(this.qProductId)).SupplierPrice;
            strREsult.Append("<ul>");
            foreach (ProductSupplier PsupList in cProductSupplier.getSupplierListByProductID(int.Parse(this.qProductId)))
            {
                if (SupplierProice == PsupList.SupplierID)
                    strREsult.Append("<li id=\"sel_list_sup_" + PsupList.SupplierID + "\" onclick=\"Sup_check('sel_list_sup_" + PsupList.SupplierID + "');\" class=\"sup_active\"><input type=\"radio\" name=\"Supplier_Selected_Default\" checked=\"checked\" value=\"" + PsupList.SupplierID + "\" />" + PsupList.SupplierTitle + "<a href=\"../../admin/supplier/supplier_add.aspx?supid=" + PsupList.SupplierID + "\" target=\"_Blank\" style=\"color:#ffffff;font-weight:bold;\">Go</a></li>");
                else
                    if (PsupList.Status)
                        strREsult.Append("<li id=\"sel_list_sup_" + PsupList.SupplierID + "\" onclick=\"Sup_check('sel_list_sup_" + PsupList.SupplierID + "');\" ><input type=\"radio\" name=\"Supplier_Selected_Default\" value=\"" + PsupList.SupplierID + "\" />" + PsupList.SupplierTitle + "<a href=\"../../admin/supplier/supplier_add.aspx?supid=" + PsupList.SupplierID + "\" target=\"_Blank\" style=\"font-weight:bold;\">Go</a><a href=\"\" onclick=\"DisableSupplier('sel_list_sup_" + PsupList.SupplierID + "');return false;\" >Click to Disable!!</a></li>");
                    else
                        strREsult.Append("<li id=\"sel_list_sup_" + PsupList.SupplierID + "\" class=\"sup_selected_inactive\" ><input type=\"radio\"  disabled=\"disabled\"  name=\"Supplier_Selected_Default\" value=\"" + PsupList.SupplierID + "\" />" + PsupList.SupplierTitle + "<a href=\"../../admin/supplier/supplier_add.aspx?supid=" + PsupList.SupplierID + "\" target=\"_Blank\" style=\"font-weight:bold;\">Go</a><a href=\"\" onclick=\"DisableSupplier('sel_list_sup_" + PsupList.SupplierID + "');return false;\" >Click to Enable!!</a></li>");

            }
            strREsult.Append("</ul>");
            return strREsult.ToString();
        }
        

        
    }
}