using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class ajax_product_option_holidays_sup_update : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qstatus
        {
            get { return Request.QueryString["status"]; }
        }

        public string qyear
        {
            get { return Request.QueryString["y"]; }
        }

        public short getCurrentSupplier
        {
            get
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                return cProduct.SupplierPrice;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId))
            {
                Response.Write(SupplierList());
                Response.End();
                
            }
            
        }

        public string SupplierList()
        {
            string strResult = "False";
            ProductOptionSupplementDate cSupplement = new ProductOptionSupplementDate();
            DateTime dDateYear = new DateTime(int.Parse(this.qyear), 9, 9);

            foreach (ProductOptionSupplementDate Sup in cSupplement.getOptionSuppleMentListCurrentYearBySupplierAndOptionId(short.Parse(this.qSupplierId), int.Parse(this.qOptionId), dDateYear, bool.Parse(this.qstatus)))
            {
                //Response.Write(Request.Form["txtSuptitle_" + Sup.SuppleMentId + ""] + "---VS--" + Sup.DateTitle + "<br/>");
                //Response.Write(Request.Form["hd_txtDateStart_" + Sup.SuppleMentId + ""] + "---VS--" + Sup.DateSupplement.ToString("yyyy-MM-dd") + "<br/>");
                //Response.Write(Request.Form["txtAmount_" + Sup.SuppleMentId + ""] + "---VS--" + Sup.SupplementAmount.ToString("#.##") + "<br/>");
               
                //Response.End();


                if (Request.Form["txtSuptitle_" + Sup.SuppleMentId + ""] == Sup.DateTitle && Request.Form["hd_txtDateStart_" + Sup.SuppleMentId + ""] == Sup.DateSupplement.ToString("yyyy-MM-dd")
                    && Request.Form["txtAmount_" + Sup.SuppleMentId + ""] == Sup.SupplementAmount.ToString("#.##"))
                {
                    strResult = "dont";

                }
                else
                {
                    cSupplement.UpdateOptionSupplement(Sup.SuppleMentId, Request.Form["txtSuptitle_" + Sup.SuppleMentId + ""], Request.Form["hd_txtDateStart_" + Sup.SuppleMentId + ""].Hotels2DateSplitYear("-"),
                       decimal.Parse(Request.Form["txtAmount_" + Sup.SuppleMentId + ""]));

                    strResult = "True";
                }

                
            }
            
            return strResult;
        }

        
        
    }
}