using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;


namespace Hotels2thailand.UI
{
    public partial class extranet_review_review_list : Hotels2BasePageExtra
    {
        public string qPtarget
        {
            get{ return Request.QueryString["ptarget"]; }
        }
        public string qPageSplit
        {
            get { return Request.QueryString["psplit"]; }
        }
         public string qreview
        {
            get
            { return Request.QueryString["reviews"]; }
        }

        public string qreviewtype
        {
            get
            { return Request.QueryString["revType"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                PageDataBind();
                
                //if (Request.QueryString.Count == 0)
                //{
                //    h_hotels.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=hotels" + this.AppendCurrentQueryString();
                //    h_spa.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=spa" + this.AppendCurrentQueryString();
                //    h_golfs.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=golfs" + this.AppendCurrentQueryString();
                //    h_daytrips.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=daytrips" + this.AppendCurrentQueryString();
                //    h_waters.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=waters" + this.AppendCurrentQueryString();
                //    h_show.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=show" + this.AppendCurrentQueryString();
                //    h_health.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=health" + this.AppendCurrentQueryString();

                //    h_approve.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=hotels&revType=approve" + this.AppendCurrentQueryString();
                //    h_waiting.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=hotels&revType=waiting" + this.AppendCurrentQueryString();
                //    h_bin.NavigateUrl = "~/admin/review/Review_admin.aspx?reviews=hotels&revType=bin" + this.AppendCurrentQueryString();

                //}
                //else
                //{
                //    h_hotels.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?reviews=hotels";
                //    h_spa.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?reviews=spa";
                //    h_daytrips.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?reviews=daytrips";
                //    h_waters.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?reviews=waters";
                //    h_show.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?reviews=show";
                //    h_health.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?reviews=health";
                //    h_golfs.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?reviews=golfs";

                //    if (string.IsNullOrEmpty(this.qreviewtype))
                //        h_approve.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?revType=waiting" + this.AppendCurrentQueryString();
                //    else
                //    {

                //        h_approve.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?revType=waiting" + this.QueryStringFilter(Request.Url.ToString(), "revType,psplit,ptarget"); 
                //    }
                        
                    
                //    if (string.IsNullOrEmpty(this.qreviewtype))
                //        h_waiting.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?revType=waiting" + this.AppendCurrentQueryString();
                //    else
                //        h_waiting.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?revType=waiting" + this.QueryStringFilter(Request.Url.ToString(), "revType,psplit,ptarget");


                //    if (string.IsNullOrEmpty(this.qreviewtype))
                //    {
                //        h_bin.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?revType=bin" + this.AppendCurrentQueryString();
                //    }
                //    else
                //    {
                //        h_bin.NavigateUrl = Request.Url.ToString().Split('?')[0] + "?revType=bin" + this.QueryStringFilter(Request.Url.ToString(), "revType,psplit,ptarget");
                //    }
                    
                //}

            }
            
        }


        public void PageDataBind()
        {
           
                if (string.IsNullOrEmpty(this.qreview) && !string.IsNullOrEmpty(this.qreviewtype) && string.IsNullOrEmpty(this.qPtarget))
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>getReviewList('hotels','" + this.qreviewtype + "','1','" + qPageSplit + "');</script>", false);
                }

                if (string.IsNullOrEmpty(this.qreview) && !string.IsNullOrEmpty(this.qreviewtype) && !string.IsNullOrEmpty(this.qPtarget))
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>getReviewList('hotels','" + this.qreviewtype + "','" + this.qPtarget + "','" + qPageSplit + "');</script>", false);
                }


                if (string.IsNullOrEmpty(this.qreview) && string.IsNullOrEmpty(this.qreviewtype) && !string.IsNullOrEmpty(this.qPtarget))
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>getReviewList('hotels','waiting','" + this.qPtarget + "','" + qPageSplit + "');</script>", false);
                }


                if (!string.IsNullOrEmpty(this.qreview) && string.IsNullOrEmpty(this.qreviewtype) && !string.IsNullOrEmpty(this.qPtarget))
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>getReviewList('" + this.qreview + "','waiting','" + this.qPtarget + "','" + qPageSplit + "');</script>", false);
                }



                if (!string.IsNullOrEmpty(this.qreview) && string.IsNullOrEmpty(this.qreviewtype) && string.IsNullOrEmpty(this.qPtarget))
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>getReviewList('" + this.qreview + "','waiting','1','" + qPageSplit + "');</script>", false);
                }


                if (!string.IsNullOrEmpty(this.qreview) && !string.IsNullOrEmpty(this.qreviewtype) && string.IsNullOrEmpty(this.qPtarget))
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>getReviewList('" + this.qreview + "','" + this.qreviewtype + "','1','" + qPageSplit + "');</script>", false);
                }


                if (!string.IsNullOrEmpty(this.qreview) && !string.IsNullOrEmpty(this.qreviewtype) && !string.IsNullOrEmpty(this.qPtarget))
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>getReviewList('" + this.qreview + "','" + this.qreviewtype + "','" + this.qPtarget + "','" + qPageSplit + "');</script>", false);
                }
            }



        public int StratIndexPage(int PageSize, int PageCurrent)
        {
            int IndexPage = (PageSize * PageCurrent) - PageSize;
            return IndexPage;
        }  
        

       
        
    }
}