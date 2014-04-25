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
    public partial class admin_ajax_product_update_supplierPrice : System.Web.UI.Page
    {
        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }

        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }
        public string qListSup
        {
            get { return Request.QueryString["ListSup"]; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qSupplierId) && !string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qListSup))
                {
                    bool IdCompleted = false;
                    Product cProduct = new Product();

                    IdCompleted = cProduct.UpdateSupplierPrice(int.Parse(this.qProductId), short.Parse(this.qSupplierId));

                    string[] SupplierList = qListSup.Split(',');
                     
                    ProductSupplier cProductSupplier = new ProductSupplier();
                    
                    foreach (string sup in SupplierList)
                    {
                        bool result = false;
                        if (!string.IsNullOrEmpty(sup))
                        {
                            foreach (ProductSupplier CurrentSupList in cProductSupplier.getSupplierListByProductID(int.Parse(this.qProductId)))
                            {
                           
                                if (CurrentSupList.SupplierID.ToString() == sup)
                                    {
                                        result = true;
                                    }
                            
                            }

                            if (result == false)
                            {
                                cProductSupplier.InsertNewProductSupplier(int.Parse(this.qProductId), short.Parse(sup));
                            }
                        }

                        
                        
                    }

                    Response.Write(IdCompleted);
                    Response.End();



                }
            }
            
        }
        


        
    }
}