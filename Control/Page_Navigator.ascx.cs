using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace Hotels2thailand.UI.Controls
{
    public partial class Page_Navigator : System.Web.UI.UserControl
    {
        public string qPsplit
        {
            get
            { return Request.QueryString["psplit"]; }
        }

        private int _Page_total = 0;
        public int PageTotal
        {
            get { return _Page_total; }
            set { _Page_total = value; }
        }

        private int _show_page_num = 10;
        public int ShowPageNum
        {
            get { return _show_page_num; }
            set { _show_page_num = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
                lblPageNavigator.Text = this.PageNavigator();
            
        }

        //public override void DataBind()
        //{
        //    PageNavigator();
        //    base.DataBind();
        //}

        public int StartSHowSplitPage(int splitNum)
        {
            int Start = (this.ShowPageNum * splitNum) - this.ShowPageNum;
            return Start;
        }

        public string PageNavigator()
        {
            StringBuilder txtPageNav = new StringBuilder();
            if (this.PageTotal > 0)
            {
                txtPageNav.Append("<div class=\"Page_nav_style\">");
               
                
                int NavogatorPageSplit = (int)Math.Ceiling((double)this.PageTotal / this.ShowPageNum);
                
                
                
                if (string.IsNullOrEmpty(this.qPsplit) || int.Parse(this.qPsplit) == 1)
                {

                    txtPageNav.Append("<div class=\"Page_nav_pageList\" id=\"Page_nav_pageList\">");
                    int intSplite = 1;
                    int intSpliteNext = intSplite + 1;
                    for (int pagenum = 1; pagenum <= this.PageTotal; pagenum++)
                    {
                         if (pagenum <= this.ShowPageNum && pagenum <= this.PageTotal)
                        {
                            if (string.IsNullOrEmpty(this.qPsplit))
                            {
                                if(Request.QueryString.Count == 0)
                                    txtPageNav.Append("<a  href=\"" + Request.Url.ToString() + "?psplit=" + intSplite + "&ptarget=" + pagenum +  (this.Page as Hotels2BasePage).AppendCurrentQueryString() +"\">" + pagenum  + "</a>");
                                else
                                    txtPageNav.Append("<a  href=\"" + Request.Url.ToString().Split('?')[0] + "?psplit=" + intSplite + "&ptarget=" + pagenum + (this.Page as Hotels2BasePage).AppendCurrentQueryString() + "\">" + pagenum + "</a>");

                            }
                            else
                            {
                                if (int.Parse(this.qPsplit) == 1 )
                                {
                                    txtPageNav.Append("<a  href=\"" + Request.Url.ToString().Split('?')[0] + "?psplit=" + intSplite + "&ptarget=" + pagenum + (this.Page as Hotels2BasePage).AppendCurrentQueryString() + "\">" + pagenum + "</a>");
                                }
                            }
                            
                        }
                    }
                    txtPageNav.Append("</div>");

                    if (string.IsNullOrEmpty(this.qPsplit) )
                    {
                        if (NavogatorPageSplit > 1)
                        {
                            if (Request.QueryString.Count == 0)
                                txtPageNav.Append("<div class=\"Page_nav_next_style\" ><a  href=\""
                                + Request.Url.ToString() + "?" + "psplit=" + intSpliteNext + (this.Page as Hotels2BasePage).AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNext)+1) + "\"> Next >></a></div>");
                            else
                                txtPageNav.Append("<div class=\"Page_nav_next_style\" ><a  href=\""
                            + Request.Url.ToString().Split('?')[0] + "?" + "psplit=" + intSpliteNext + (this.Page as Hotels2BasePage).AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNext)+1) + "\"> Next >></a></div>");
                        }
                    }
                    else
                    {
                        if (int.Parse(this.qPsplit) == 1 )
                        {
                            if (NavogatorPageSplit > 1)
                            {
                                txtPageNav.Append("<div class=\"Page_nav_next_style\"  ><a href=\"" + Request.Url.ToString().Split('?')[0]
                                + "?" + "psplit=" + intSpliteNext + (this.Page as Hotels2BasePage).AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNext)+1) + "\"> Next >></a></div>");
                            }
                        }
                    }

                   
                }
                else
                {
                    if (int.Parse(this.qPsplit) > 1 && (int.Parse(this.qPsplit) <= NavogatorPageSplit))
                    {
                        int intSpliteNPrevious = int.Parse(this.qPsplit) - 1;
                        int intSpliteNext = int.Parse(this.qPsplit) + 1;
                        txtPageNav.Append("<div class=\"Page_nav_prevoius_style\"  ><a  href=\"" + Request.Url.ToString().Split('?')[0] + "?" + "psplit=" + intSpliteNPrevious + (this.Page as Hotels2BasePage).AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNPrevious) + 1) + "\"> << Previous</a></div>");

                        txtPageNav.Append("<div class=\"Page_nav_pageList\" id=\"Page_nav_pageList\">");
                        for (int pagenum = (StartSHowSplitPage(int.Parse(this.qPsplit)) + 1); pagenum <= this.PageTotal; pagenum++)
                        {

                            if (pagenum <= (StartSHowSplitPage(int.Parse(this.qPsplit)) + this.ShowPageNum) && pagenum <= this.PageTotal)
                            {
                                txtPageNav.Append("<a  href=\"" + Request.Url.ToString().Split('?')[0] + "?psplit=" + (intSpliteNext - 1) + "&ptarget=" + pagenum + (this.Page as Hotels2BasePage).AppendCurrentQueryString() + "\">" + pagenum  + "</a>");
                            }
                        }
                        txtPageNav.Append("</div>");

                        if (intSpliteNext < NavogatorPageSplit + 1)
                        {

                            txtPageNav.Append("<div class=\"Page_nav_next_style\" ><a  href=\"" + Request.Url.ToString().Split('?')[0]
                                + "?" + "psplit=" + intSpliteNext + (this.Page as Hotels2BasePage).AppendCurrentQueryString() + "&ptarget=" + (StartSHowSplitPage(intSpliteNext)+1) + "\"> Next >></a></div>");
                            
                        }
                    }
                }
                
                
                txtPageNav.Append("</div>");

               
            }

            return txtPageNav.ToString();
        }
    }
}
