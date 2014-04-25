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
    public partial class admin_ajax_review_post_navigator : Hotels2BasePageExtra_Ajax
    {
        private byte _page_size = 50;
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
            get { return Request.QueryString["psplit"]; }
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
                Response.Write(PageNavigator());
                Response.End();
            }
         }

        public int StratIndexPage(int PageSize, int PageCurrent)
        {
            int IndexPage = (PageSize * PageCurrent) - PageSize;
            return IndexPage;
        }


        public string imgeReviewRatePath(byte bytRate)
        {
            string strPath = string.Empty;
            switch (bytRate)
            {
                case 1:
                    strPath = "review1s.png";
                    break;
                case 2:
                    strPath = "review2s.png";
                    break;
                case 3:
                    strPath = "review3s.png";
                    break;
                case 4:
                    strPath = "review4s.png";
                    break;
                case 5:
                    strPath = "review5s.png";
                    break;
            }
            return strPath;
        }
        
        

        public int StartSHowSplitPage(int splitNum)
        {
            int Start = (this.ShowPageNum * splitNum) - this.ShowPageNum;
            return Start;
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
        public int PageTotal()
        {
            int ListTotal = 0;
            int intPageTotal = 0;
            ListTotal = ReviewManage.ReviewListTotal_HotelManage(ProductType(this.qreview), ReviewType(this.qreviewtype), this.CurrentProductActiveExtra);
            intPageTotal = (int)Math.Ceiling((double)ListTotal / _page_size);
            return intPageTotal;
        }

        //public string AppendCurrentQueryString()
        //{
        //    StringBuilder strUrl = new StringBuilder();


        //    if (!string.IsNullOrEmpty(this.qreview))
        //        strUrl.Append("&reviews=" + this.qreview);


        //    if (!string.IsNullOrEmpty(this.qreviewtype))
        //        strUrl.Append("&revType=" + this.qreviewtype);

        //    return strUrl.ToString();
        //}


        public string PageNavigator()
        {

            string URl = "../../admin/review/review_admin.aspx";
            StringBuilder txtPageNav = new StringBuilder();
            if (this.PageTotal() > 1)
            {
                txtPageNav.Append("<div class=\"Page_nav_style\">");
                

                int NavogatorPageSplit = (int)Math.Ceiling((double)this.PageTotal() / this.ShowPageNum);

                

                if (string.IsNullOrEmpty(this.qPsplit) || int.Parse(this.qPsplit) == 1)
                {
                    txtPageNav.Append("<div class=\"Page_nav_prevoius_style_inactive\" ><a id=\"page_num_\" href=\"\" onclick=\"return false;\" > << Previous</a></div>");
                    txtPageNav.Append("<div class=\"Page_nav_pageList\" id=\"Page_nav_pageList\">");
                    int intSplite = 1;
                    int intSpliteNext = intSplite + 1;
                    

                    for (int pagenum = 1; pagenum <= this.PageTotal(); pagenum++)
                    {
                        if (pagenum <= this.ShowPageNum && pagenum <= this.PageTotal())
                        {
                            if (string.IsNullOrEmpty(this.qPsplit))
                            {
                                if (Request.QueryString.Count == 0)
                                    if (pagenum == int.Parse(this.qPtarget))
                                        txtPageNav.Append("<a class=\"Page_nav_pageList_activePage\" id=\"page_num_" + pagenum + "\"  href=\"" + URl + "?psplit=" + intSplite + "&ptarget=" + pagenum + this.AppendCurrentQueryString() + "\" onclick=\"getReviewListAjaxNavigator('" + intSplite + "','" + pagenum + "','page_num_" + pagenum + "');return false;\" >" + pagenum + "</a>");
                                    else
                                        txtPageNav.Append("<a id=\"page_num_" + pagenum + "\"  href=\"" + URl + "?psplit=" + intSplite + "&ptarget=" + pagenum + this.AppendCurrentQueryString() + "\" onclick=\"getReviewListAjaxNavigator('" + intSplite + "','" + pagenum + "','page_num_" + pagenum + "');return false;\" >" + pagenum + "</a>");
                                else
                                    if (pagenum == int.Parse(this.qPtarget))
                                        txtPageNav.Append("<a class=\"Page_nav_pageList_activePage\" id=\"page_num_" + pagenum + "\" href=\"" + URl + "?psplit=" + intSplite + "&ptarget=" + pagenum + this.AppendCurrentQueryString() + "\" onclick=\"getReviewListAjaxNavigator('" + intSplite + "','" + pagenum + "','page_num_" + pagenum + "');return false;\">" + pagenum + "</a>");
                                    else
                                        txtPageNav.Append("<a id=\"page_num_" + pagenum + "\" href=\"" + URl + "?psplit=" + intSplite + "&ptarget=" + pagenum + this.AppendCurrentQueryString() + "\" onclick=\"getReviewListAjaxNavigator('" + intSplite + "','" + pagenum + "','page_num_" + pagenum + "');return false;\">" + pagenum + "</a>");

                            }
                            else
                            {
                                if (int.Parse(this.qPsplit) == 1)
                                {
                                    if (pagenum == int.Parse(this.qPtarget))
                                        txtPageNav.Append("<a class=\"Page_nav_pageList_activePage\" id=\"page_num_" + pagenum + "\" href=\"" + URl + "?psplit=" + intSplite + "&ptarget=" + pagenum + this.AppendCurrentQueryString() + "\" onclick=\"getReviewListAjaxNavigator('" + intSplite + "','" + pagenum + "','page_num_" + pagenum + "');return false;\">" + pagenum + "</a>");
                                    else
                                        txtPageNav.Append("<a  id=\"page_num_" + pagenum + "\" href=\"" + URl + "?psplit=" + intSplite + "&ptarget=" + pagenum + this.AppendCurrentQueryString() + "\" onclick=\"getReviewListAjaxNavigator('" + intSplite + "','" + pagenum + "','page_num_" + pagenum + "');return false;\">" + pagenum + "</a>");
                                }
                            }

                        }
                    }
                    txtPageNav.Append("</div>");

                    if (string.IsNullOrEmpty(this.qPsplit))
                    {
                        if (NavogatorPageSplit > 1)
                        {
                            if (Request.QueryString.Count == 0)
                                txtPageNav.Append("<div class=\"Page_nav_next_style\" ><a id=\"page_num_" + (StartSHowSplitPage(intSpliteNext) + 1) + "\"  href=\""
                                + URl + "?" + "psplit=" + intSpliteNext + this.AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNext) + 1) + "\" onclick=\"getReviewListAjaxNavigator('" + intSpliteNext + "','" + (StartSHowSplitPage(intSpliteNext) + 1) + "','page_num_" + (StartSHowSplitPage(intSpliteNext) + 1) + "');return false;\"> Next >></a></div>");
                            else
                                txtPageNav.Append("<div class=\"Page_nav_next_style\" ><a id=\"page_num_" + (StartSHowSplitPage(intSpliteNext) + 1) + "\"  href=\""
                            + URl + "?" + "psplit=" + intSpliteNext + this.AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNext) + 1) + "\" onclick=\"getReviewListAjaxNavigator('" + intSpliteNext + "','" + (StartSHowSplitPage(intSpliteNext) + 1) + "','page_num_" + (StartSHowSplitPage(intSpliteNext) + 1) + "');return false;\"> Next >></a></div>");
                        }
                    }
                    else
                    {
                        if (int.Parse(this.qPsplit) == 1)
                        {
                            if (NavogatorPageSplit > 1)
                            {
                                txtPageNav.Append("<div class=\"Page_nav_next_style\"  ><a href=\"" + URl
                                + "?" + "psplit=" + intSpliteNext + this.AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNext) + 1) + "\" onclick=\"getReviewListAjaxNavigator('" + intSpliteNext + "','" + (StartSHowSplitPage(intSpliteNext) + 1) + "','page_num_" + (StartSHowSplitPage(intSpliteNext) + 1) + "');return false;\"> Next >></a></div>");
                            }
                        }
                    }


                }
                else
                {
                    
                    if (int.Parse(this.qPsplit) > 1 && int.Parse(this.qPsplit) <= NavogatorPageSplit)
                    {
                        int intSpliteNPrevious = int.Parse(this.qPsplit) - 1;
                        int intSpliteNext = int.Parse(this.qPsplit) + 1;
                        txtPageNav.Append("<div class=\"Page_nav_prevoius_style\"  ><a id=\"page_num_" + (StartSHowSplitPage(intSpliteNPrevious) + 1) + 
                            "\" href=\"" + URl + "?" + "psplit=" + intSpliteNPrevious + this.AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNPrevious) + 1) +
                            "\" onclick=\"getReviewListAjaxNavigator('" + intSpliteNPrevious + "','" + (StartSHowSplitPage(intSpliteNPrevious) + 1) + "','page_num_" + (StartSHowSplitPage(intSpliteNPrevious) + 1) + "');return false;\"> << Previous</a></div>");

                        txtPageNav.Append("<div class=\"Page_nav_pageList\" id=\"Page_nav_pageList\">");
                       
                        for (int pagenum = (StartSHowSplitPage(int.Parse(this.qPsplit)) + 1); pagenum <= this.PageTotal(); pagenum++)
                        {

                            if (pagenum <= (StartSHowSplitPage(int.Parse(this.qPsplit)) + this.ShowPageNum) && pagenum <= this.PageTotal())
                            {
                                if (pagenum == int.Parse(this.qPtarget))
                                    txtPageNav.Append("<a class=\"Page_nav_pageList_activePage\" id=\"page_num_" + pagenum + "\"  href=\"" + URl + "?psplit=" + (intSpliteNext - 1) + "&ptarget=" + pagenum + this.AppendCurrentQueryString() + "\" onclick=\"getReviewListAjaxNavigator('" + (intSpliteNext - 1) + "','" + pagenum + "','page_num_" + pagenum + "');return false;\">" + pagenum + "</a>");
                                else
                                    txtPageNav.Append("<a id=\"page_num_" + pagenum + "\"  href=\"" + URl + "?psplit=" + (intSpliteNext - 1) + "&ptarget=" + pagenum + this.AppendCurrentQueryString() + "\" onclick=\"getReviewListAjaxNavigator('" + (intSpliteNext - 1) + "','" + pagenum + "','page_num_" + pagenum + "');return false;\">" + pagenum + "</a>");
                            }
                        }
                        txtPageNav.Append("</div>");

                        if (intSpliteNext < NavogatorPageSplit + 1)
                        {

                            txtPageNav.Append("<div class=\"Page_nav_next_style\" ><a id=\"page_num_" + (StartSHowSplitPage(intSpliteNext) + 1) + "\" href=\"" + URl
                                + "?" + "psplit=" + intSpliteNext + this.AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNext) + 1) + "\" onclick=\"getReviewListAjaxNavigator('" + intSpliteNext + "','" + (StartSHowSplitPage(intSpliteNext) + 1) + "','page_num_" + (StartSHowSplitPage(intSpliteNext) + 1) + "');return false;\"> Next >></a></div>");

                        }
                    }
                }

                  
                txtPageNav.Append("</div>");
                txtPageNav.Append("<br /><br />");
                txtPageNav.Append("<div style=\"clear:both\"></div>");
        
            }

            return txtPageNav.ToString();
        }


    }
}