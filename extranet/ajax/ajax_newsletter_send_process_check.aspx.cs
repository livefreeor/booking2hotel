using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using System.Text;
using Hotels2thailand.Newsletters;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_newsletter_send_process_check : Hotels2BasePageExtra_Ajax
    {
        public string qNewId
        {
            get { return Request.QueryString["ID"]; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                NewsletterSending cNewSending = new NewsletterSending();
                Response.Write(cNewSending.GetCountSendingListCompleted(this.CurrentProductActiveExtra));
                Response.End();
                
            }
        }

       

    }
}