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
using System.Text.RegularExpressions;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_newsletter_del_sel : Hotels2BasePageExtra_Ajax
    {
        public string qStatusID
        {
            get { return Request.QueryString["temp"]; }
        }

        public string qNewsID
        {
            get { return Request.QueryString["ID"]; }
        }

        public string qMailCat
        {
            get { return Request.QueryString["mc"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                if (string.IsNullOrEmpty(Request.QueryString["temp"]))
                {
                    Response.Redirect("Newsletter_exception.aspx");
                }
                else
                {
                    Newsletter cNews = new Newsletter();
                    // update to status_id = 6 == Deleted

                    foreach (string newsid in Request.Form["chk_news"].Split(','))
                    {
                        cNews.UpdateStatusIdNews(int.Parse(newsid), 6);
                    }

                    Response.Write("True");
                    Response.End();
                }
                

            }
        }
    
        

    }
}