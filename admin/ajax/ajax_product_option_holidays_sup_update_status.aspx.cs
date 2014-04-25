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
    public partial class ajax_product_option_holidays_sup_update_status : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }
        public string qSupplementId
        {
            get { return Request.QueryString["sid"]; }
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
        //public string qProductCat
        //{
        //    get { return Request.QueryString["pdcid"]; }
        //}

        //public byte Current_StaffLangId
        //{
        //    get 
        //    { 
        //        Hotels2BasePage cBasePage = new Hotels2BasePage();
        //        return cBasePage.CurrenStafftLangId;
        //    }
        //}
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
            string strResult = "Empty";

            string CheckedStatus = Request.Form["checkbin"];
            if (!string.IsNullOrEmpty(CheckedStatus))
            {
                string[] arrSuppleId = CheckedStatus.Split(',');
                ProductOptionSupplementDate cSupDay = new ProductOptionSupplementDate();
                foreach (string Id in arrSuppleId)
                {

                    cSupDay = cSupDay.getOptionSuppleMentById(int.Parse(Id));
                    if (cSupDay.Status)
                        cSupDay.UpdateOptionSupplementStatus(int.Parse(Id), false);
                    else
                        cSupDay.UpdateOptionSupplementStatus(int.Parse(Id), true);
                }
                strResult = "True";
            }
            
            return strResult;
        }

        
        
    }
}