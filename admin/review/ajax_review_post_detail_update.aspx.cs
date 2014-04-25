using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using Hotels2thailand.Front;
using System.Text;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_review_post_detail_update : System.Web.UI.Page
    {
        public string qStatus
        {
            get { return Request.QueryString["status"]; }
        }
        public string qStatusBin
        {
            get { return Request.QueryString["status_bin"]; }
        }

        public string qReviewId
        {
            get { return Request.QueryString["revId"]; }
        }

        public string qreview
        {
            get { return Request.QueryString["reviews"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qreview) && !string.IsNullOrEmpty(this.qReviewId) && !string.IsNullOrEmpty(this.qStatus) && !string.IsNullOrEmpty(this.qStatusBin))
                {
                    
                    Response.Write(UpdateStatus(int.Parse(this.qReviewId),bool.Parse(this.qStatus),bool.Parse(this.qStatusBin)));
                    Response.End();
                }
                
                
            }
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
            }

            return Type;
        }

        public string UpdateStatus(int ReviewId, bool Status, bool StatusBin)
        {
            string IsCompleted = "Incomplete";
            bool Update = ReviewManage.HotelsReviewUpdateStatus(ReviewId, Status, StatusBin);

            if (Update)
                IsCompleted = "Success";

            //GenerateProduct cGenerateProduct = new GenerateProduct();
            //try
            //{
                
            //    cGenerateProduct.GenerateReviewApprove(ReviewId,1);
            //    cGenerateProduct.GenerateReviewApprove(ReviewId,2);
            //    IsCompleted = "Success";
            //}
            //catch(Exception ex)
            //{
            //    //IsCompleted = "Incomplete";
            //    IsCompleted = ex.Message;
            //}

            
            return IsCompleted;

        }
        

    }
}