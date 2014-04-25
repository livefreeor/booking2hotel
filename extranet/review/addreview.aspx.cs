using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand.Reviews;


namespace Hotels2thailand.UI
{
    public partial class extranet_review_addreview : Hotels2BasePageExtra
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
               
            }
            
        }

        public void btnREviewSave_OnClick(object sender, EventArgs e)
        {

            int? cusID = null;
            //if (!string.IsNullOrEmpty(Request.Form["cus_id"]))
            //{
            //    cusID = int.Parse(Request.Form["cus_id"]);
            //}

            int ProductID = this.CurrentProductActiveExtra;
            //Response.Write(Request.Form["product"]);
            //Response.End();
            //byte CountryID=byte.Parse(Request.Form["country_id"]);
            //byte Recommend=byte.Parse(Request.Form["review_recommend"]);
            //byte ReviewFrom=byte.Parse(Request.Form["review_from"]);
            //byte Category = 29;
            string ReviewTitle = Request.Form["review_title"];
            string ReviewDetail = Request.Form["review_detail"];
            string Positive = "";
            string Negative = "";
            string CustomerName = Request.Form["cus_name"];
            string CustomerFrom = Request.Form["cus_from"];


            ReviewManage.HotelREviewInsert(
                    ProductID,
                    cusID,
                    null,
                    1,
                    1,
                    ReviewTitle,
                    ReviewDetail,
                    Positive,
                    Negative,
                    CustomerName,
                    CustomerFrom,
                    byte.Parse(Request.Form["rate_overall"]),
                    byte.Parse(Request.Form["rate_service"]),
                    byte.Parse(Request.Form["rate_location"]),
                    byte.Parse(Request.Form["rate_rooms"]),
                    byte.Parse(Request.Form["rate_cleanliness"]),
                    byte.Parse(Request.Form["rate_value_for_money"])
                    );

            string query = string.Empty;
            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
            {
                query = "?pid=" + this.qProductId + "&supid=" + this.qSupplierId;
            }

            Response.Redirect("review_list.aspx" + query);  

        }
        
    }
}