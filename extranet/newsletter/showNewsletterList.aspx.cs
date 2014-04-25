using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Newsletters;
using System.Text.RegularExpressions;


namespace Hotels2thailand.UI
{
    public partial class ShowNewsletterList : Hotels2BasePageExtra
    {
        public string qStatusID
        {
            get { return Request.QueryString["temp"]; }
        }

        public string qMailCat
        {
            get { return Request.QueryString["mc"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
        }

       
    }
}
