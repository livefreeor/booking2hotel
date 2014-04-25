using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;


using System.Data.SqlClient;

namespace Hotels2thailand.UI
{
    public partial class ReportBookingStat : Hotels2BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                

                if (!string.IsNullOrEmpty(this.qProductId))
                {
                   
                }
            }
        }



        

        
    }
}