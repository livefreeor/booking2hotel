using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class ajax_product_option_holidays_sup_select : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }
        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
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
            ProductSupplier cProductSup = new ProductSupplier();
            StringBuilder strResult = new StringBuilder();

            strResult.Append("<p id=\"optionListDisplay\">");

            strResult.Append("<select name=\"dropSup\" id=\"dropSup\" style=\"width:570px\" class=\"dropStyle\" onchange=\"SUpChang();\">");

            foreach (ProductSupplier item in cProductSup.getSupplierListByProductIDActive(int.Parse(this.qProductId)))
            {
                if (item.SupplierID == getCurrentSupplier)
                    strResult.Append("<option value=\"" + item.SupplierID + "\" selected=\"selected\" >" + item.SupplierTitle + "</option>");
                else
                    strResult.Append("<option value=\"" + item.SupplierID + "\">" + item.SupplierTitle + "</option>");
            }

            strResult.Append("</select>");
            strResult.Append("</p>");


            return strResult.ToString();
        }

        
        
    }
}