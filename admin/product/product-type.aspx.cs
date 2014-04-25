using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_product_product_type : Hotels2BasePage
    {
        public string QueryType_Id
        {
            get
            {
                return Request.QueryString["ptid"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.QueryType_Id))
                {
                    //ProductCategory cProductCat = new ProductCategory();
                    //if (string.IsNullOrEmpty(DropProductCat.SelectedValue))
                    //{
                    //    lblhead.Text = cProductCat.GetProductCategoryByID(1).Title;
                    //}
                    //else
                    //{
                    //    lblhead.Text = cProductCat.GetProductCategoryByID(byte.Parse(DropProductCat.SelectedValue)).Title;
                    //}
                    
               }
            }
        }
    }
}