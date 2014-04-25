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


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_package_update_detail : Hotels2BasePageExtra_Ajax
    {
        
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qOptionId))
                    {
                        string result = "False";
                        try
                        {
                            Option cOption = new Option();

                            ProductOptionContent cContent = new ProductOptionContent();

                            ProductConditionExtra cCondition = new ProductConditionExtra();

                            int intOPtionId = int.Parse(this.qOptionId);
                            string strPackageTitle= Request.Form["ctl00$ContentPlaceHolder1$txtPackage"];
                            string strPackageDetail = Request.Form["ctl00$ContentPlaceHolder1$txt_package_detail"];
                            byte bytNight = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropNight"]);

                           
                            DateTime dBookingStart = Request.Form["hd_Booking_DateStart"].Hotels2DateSplitYear("-");

                            DateTime dBookingEnd = Request.Form["hd_Booking_DateEnd"].Hotels2DateSplitYear("-");
                            DateTime dStayStart = Request.Form["hd_Stay_DateStart"].Hotels2DateSplitYear("-");
                            DateTime dStayEnd = Request.Form["hd_Stay_DateEnd"].Hotels2DateSplitYear("-");
                            cOption.UpdateOptionExtranet_package(this.CurrentProductActiveExtra, intOPtionId, strPackageTitle, bytNight, dBookingStart,
                                dBookingEnd, dStayStart, dStayEnd);

                            cContent.UpdateOptionContentLangExtranet(this.CurrentProductActiveExtra, intOPtionId, 1, strPackageTitle, strPackageDetail);


                            //GetProductCancelExtraListByConditionID
                            int intProductId = this.CurrentProductActiveExtra;
                            ProductCondition_Cancel_Extra cCancel = new ProductCondition_Cancel_Extra();
                            //ProductPriceExtra_period cPrice = new ProductPriceExtra_period();
                            foreach (ProductConditionExtra condition in cCondition.getConditionListByOptionId(intOPtionId, 1))
                            {
                               //cCancel.
                                foreach (ProductCondition_Cancel_Extra cancel in cCancel.GetProductCancelExtraListByConditionID(condition.ConditionId))
                                {
                                    cancel.UpdateConditionCancelExtraNet(intProductId, cancel.CancelID, dStayStart, dStayEnd);
                                }
                                //cPrice.UpdatePriceExtra_period_date_package(condition.ConditionId, dStayStart, dStayEnd);
                                
                            }


                            result = "True";

                            

                        }
                        catch (Exception ex)
                        {
                            Response.Write("error:" + ex.Message + "<br/>" + ex.StackTrace);
                            Response.End();
                        }

                        Response.Write(result);
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
               
            }
            
        }


        
    }
}