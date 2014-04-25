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
    public partial class admin_ajax_review_post : System.Web.UI.Page
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

                //Response.Write(Request.Url.ToString());
                //Response.End();
                Response.Write(HotelREviewConcat(PageResult()));
                Response.End();
            }
         }

        public int StratIndexPage(int PageSize, int PageCurrent)
        {
            int IndexPage = (PageSize * PageCurrent) - PageSize;
            return IndexPage;
        }


        public IList<object> PageResult()
        {
            
           //int totalPage = (int)Math.Ceiling((double)Total / PageSize);
            int IndexStart = StratIndexPage(_page_size, int.Parse(this.qPtarget));

            IList<object> obj = null;
            //int Counttotal = 0;

            if (!string.IsNullOrEmpty(this.qreview) && !string.IsNullOrEmpty(this.qreviewtype) && !string.IsNullOrEmpty(this.qPtarget))
            {
                obj = ReviewManage.GetReivewListBhtManage(false, ProductType(this.qreview), ReviewType(this.qreviewtype), true, IndexStart, _page_size);

            }
            return obj;
         }

        public string imgeReviewRatePath(byte bytRate)
        {
            string strPath = string.Empty;
            switch (bytRate)
            {
                case 0:
                    strPath = "review0s.png";
                    break;
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
                case 6:
                    strPath = "review6s.png";
                    break;
                case 7:
                    strPath = "review7s.png";
                    break;
                case 8:
                    strPath = "review8s.png";
                    break;
                case 9:
                    strPath = "review9s.png";
                    break;
                case 10:
                    strPath = "review10s.png";
                    break;
            }
            return strPath;
        }
        
        public string HotelREviewConcat(IList<object> ListObject) 
        {
            StringBuilder strResult = new StringBuilder();


            if (ListObject.Count > 0)
            {
                foreach (ProductReviewsShort item in ListObject)
                {

                    strResult.Append("<div class=\"review_item\" id=\"review_item_block_" + item.ReviewId + "\">");
                    strResult.Append("<p class=\"review_id_new\">#" + item.ReviewId + "</p>");
                    strResult.Append("<h1>" + item.ProductTitle + "&nbsp;&nbsp;<span class=\"review_rate_head\">Overall &nbsp;: &nbsp;<img src=\"../../images/" + imgeReviewRatePath(item.RateOverAll) + "\"></span></h1>");
                    strResult.Append("<p class=\"review_title\"><img src=\"../../images/wall-post.png\">&nbsp;" + item.Title + "</p>");
                    strResult.Append("<p class=\"review_by\" style=\"margin:2px 0px 0px 0px;\">by&nbsp;" + item.FullName + "&nbsp;from&nbsp;" + item.ReviewForm + "&nbsp;(&nbsp;" + item.DateSubmit.ToString("MMM d, yyyy") + "&nbsp;)&nbsp;" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:getReviewDetail('" + item.ReviewId + "')\">Manage>></a></p>");
                    //strResult.Append("<p><a href=\"javascript:getReviewDetail('"+item.ReviewId +"')\">More>></a></p>");

                    strResult.Append("<div id=\"review_item_" + item.ReviewId + "\" style=\"display:none;\">");

                    strResult.Append("</div>");

                    strResult.Append("</div>");
                }
            }
            else
            {
                strResult.Append("<div class=\"box_empty\">");
                strResult.Append("");
                strResult.Append("<p><label>No Review</label> In this status</p>");
                strResult.Append("");
                strResult.Append("</div>");
            }
                    
              
            return strResult.ToString();
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
        

    }
}