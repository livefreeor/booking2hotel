using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using System.Text;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_review_post_page_title : System.Web.UI.Page
    {
        
        public string qPtarget
        {
            get { return Request.QueryString["ptarget"]; }
        }
        public string qreview
        {
            get{ return Request.QueryString["reviews"]; }
        }

        public string qreviewtype
        {
            get{ return Request.QueryString["revType"]; }
        }

        public string qPageSize
        {
            get { return Request.QueryString["pSize"]; }
        }
        public string qPsplit
        {
            get
            { return Request.QueryString["psplit"]; }
        }



        private int _show_page_num = 10;
        public int ShowPageNum
        {
            get { return _show_page_num; }
            set { _show_page_num = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Response.Write(PageTitle());
                Response.End();
            }
         }

        public int StratIndexPage(int PageSize, int PageCurrent)
        {
            int IndexPage = (PageSize * PageCurrent) - PageSize;
            return IndexPage;
        }

        public byte ProductType(string strType)
        {
            byte Type = 0;
            switch (strType)
            {
                case "hotels":
                    Type = 29;
                    break;
                case "spa":
                    Type = 40;
                    break;
                case "golfs":
                    Type = 32;
                    break;
                case "daytrips":
                    Type = 34;
                    break;
                case "waters":
                    Type = 36;
                    break;
                case "show":
                    Type = 38;
                    break;
                case "health":
                    Type = 39;
                    break;
            }

            return Type;
        }

        public byte ReviewType(string Strtype)
        {
            byte Type = 0;
            switch (Strtype)
            {
                case "approve":
                    Type = 0;
                    break;
                case "waiting":
                    Type = 1;
                    break;
                case "bin":
                    Type = 2;
                    break;
            }

            return Type;
        }

        public int ListTotal()
        {
            int intListTotal = 0;
            intListTotal = ReviewManage.ReviewListTotal_BHTmanage(ProductType(this.qreview), ReviewType(this.qreviewtype));

            return intListTotal;
        }

       public string HeadTitle(string strQuery)
        {
            string HeadTitle = string.Empty;
            switch (strQuery)
            {
                case "hotels":
                    HeadTitle = "Hotel Review";
                    break;
                case "spa":
                    HeadTitle = "Spa Review";
                    break;
                case "golfs":
                    HeadTitle = "Golf Review";
                    break;
                case "health":
                    HeadTitle = "Health Check Up Review";
                    break;
                case "waters":
                    HeadTitle = "Water Activity Review";
                    break;
                case "daytrips":
                    HeadTitle = "Day Trip Review";
                    break;
                case "show":
                    HeadTitle = "Show&Event Review";
                    break;

            }
            return HeadTitle;
        }

       
        public string PageTitle()
        {
            StringBuilder PageTitle = new StringBuilder();
            try
            {
                
                PageTitle.Append("<p id=\"Review_page_title_head\">" + HeadTitle(this.qreview));
                PageTitle.Append("<span class=\"review_page_title_head_span\">(" + this.qreviewtype[0].ToString().ToUpper() + this.qreviewtype.Hotels2LeftClr(1) + ")</span>");
                PageTitle.Append("</p>");
                PageTitle.Append("<p id=\"Review_page_title_num\">Total Reviews : " + this.ListTotal() + "</p>");
                PageTitle.Append("<div style=\"clear:both\"></div>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "<br/>" + ex.StackTrace);
                Response.End();
            }
            
           
             return PageTitle.ToString() ;
        }
    }
}