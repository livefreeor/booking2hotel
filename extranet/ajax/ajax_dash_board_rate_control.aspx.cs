using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Booking;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_dash_board_rate_control : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Response.Write(RateControl());
                Response.End();
                
            }
        }


        public string RateControl() 
        {
            StringBuilder result = new StringBuilder();

            try
            {
                string bulletCondition = "<img src=\"http://www.hotels2thailand.com/images/ico-square-small.png\" />&nbsp;";
                string bulletOption = "<img src=\"http://www.hotels2thailand.com/images_extra/dot_yellow.png\" />&nbsp;";
               
                Option cOption = new Option();
                ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                IList<object> iListOption = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
                
                foreach (Option option in iListOption)
                {
                    string txtHidden = string.Empty;
                    int coditionCount = 0;
                    IList<object> ConditionExtraList = cConditionExtra.getConditionListByOptionIdPrice(option.OptionID, 1);

                    coditionCount = ConditionExtraList.Count();

                    if (coditionCount > 0)
                        result.Append("<div class=\"rate_control_room\" >");
                    else
                        result.Append("<div class=\"rate_control_room\" style=\"display:none;\" >");

                    result.Append("<p class=\"topic_rate\">" + bulletOption + "&nbsp;" + option.Title + "<a></a></p>");
                    result.Append("<ul class=\"ul ul_expand\">");

                    if (coditionCount > 0)
                    {
                        foreach (ProductConditionExtra conditionList in ConditionExtraList)
                        {
                            int MountExpired = 0;
                            string strRoomOnly = string.Empty;
                            if (conditionList.Breakfast == 0)
                                strRoomOnly = "(Room Only)";
                            string strExpiredDate = string.Empty;

                            if (conditionList.DateExpirePrice.HasValue)
                            {

                                DateTime dDate = (DateTime)conditionList.DateExpirePrice;
                                MountExpired = (int)DateTime.Now.Hotels2ThaiDateTime().Hotels2DateDiff(DateInterval.Month, dDate);



                                if (MountExpired < 3 && MountExpired > 0)
                                    strExpiredDate = "Rate expires in " + MountExpired + " month" + Hotels2String.IsFill_S(MountExpired) + "";
                                else if (MountExpired <= 0)
                                    strExpiredDate = "Rate is expired <label style=\"color:#c30e00\"><strong> [ " + dDate.ToString("dd-MMM-yyyy") + " ] </strong></label>";
                                else
                                    strExpiredDate = "<label style=\"color:#5c75a9\"><strong> [ " + dDate.ToString("dd-MMM-yyyy") + " ] </strong></label>";

                                result.Append("<li>" + bulletCondition + conditionList.Title + " <span>" + strRoomOnly + "" + "(For Adult " + conditionList.NumAult + ")</span> : <span class=\"span_alert\">" + strExpiredDate + "</span></li>");

                            }
                            else
                            {
                                strExpiredDate = "No rate ";
                                result.Append("<li>" + bulletCondition + conditionList.Title + " <span>" + strRoomOnly + "" + "(For Adult " + conditionList.NumAult + ")</span> : <span class=\"span_alert\">" + strExpiredDate + "</span></li>");
                            }
                            

                            

                        }
                    }

                    result.Append("</ul>");
                    result.Append("</div>");
                    
                }
                
                result.Append("</div>");
            }
            catch (Exception ex)
            {
                Response.Write("error:" + ex.Message);
                Response.End();
            }

            return result.ToString();
           
        }
        
        
    }
}