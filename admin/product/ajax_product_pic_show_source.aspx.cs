using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_pic_show_source : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(Request.QueryString["pt"]))
            {
                Response.Write("TESTssaaa");
                Response.End();
                byte bytTypeId = byte.Parse(Request.QueryString["pt"]);

                string type_title =  ProductPicType.getTypeTitleById(bytTypeId);
                //if(type_title.HotelsStringIsMatch())



            }
            
        }

        
    }
}