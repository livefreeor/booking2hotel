﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_rd_location : Hotels2BasePage
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Destination cDestiantion = new Destination();
                GVDes.DataSource = cDestiantion.GetDestinationAll();

                GVDes.DataBind();
               
            }
        }



        

      
    }
}