using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Suppliers;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_supplierList : System.Web.UI.Page
    {
        public string qSup_Aphbet 
        {
            get { return Request.QueryString["apha"]; }
        }

        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qSup_Aphbet))
                {
                    string strApha = "A";
                    strApha = this.qSup_Aphbet;
                    Response.Write(GetSupplierListByAphbet(strApha));
                    Response.End();
                }
            }
            
        }
        public string GetSupplierListByAphbet(string aphabet)
        {
            StringBuilder strREsult = new StringBuilder();
            Supplier clSupplier = new Supplier();
            strREsult.Append("<ul>");
            if (string.IsNullOrEmpty(this.qProductId))
            {
                foreach (Supplier sup in clSupplier.getListSupplierByAlphabet(aphabet))
                {
                    strREsult.Append("<li id=\"sup_" + sup.SupplierId + "\"><img src=\"../../images/greenbt.png\" alt=\"" + sup.SupplierTitle + "\">&nbsp;" + sup.SupplierTitle + "</li>");
                }
            }
            else
            {
                foreach (Supplier sup in clSupplier.getListSupplierByAlphabet(aphabet))
                {
                    strREsult.Append("<li id=\"sup_" + sup.SupplierId + "\"><img src=\"../../images/greenbt.png\" alt=\"" + sup.SupplierTitle + "\">&nbsp;" + sup.SupplierTitle + "</li>");
                }
            }
            
            strREsult.Append("</ul>");
            
            
            
            return strREsult.ToString();
        }

        
    }
}