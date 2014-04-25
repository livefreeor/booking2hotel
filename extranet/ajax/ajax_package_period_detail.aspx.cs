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
    public partial class admin_ajax_package_period_detail : Hotels2BasePageExtra_Ajax
    {
        
        
        public string qConditionId
        {
            get { return Request.QueryString["con"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                
                if (!string.IsNullOrEmpty(this.qConditionId))
                {
                    Response.Write(GetPeriodPrice());

                }

                
                
                Response.End();
            }
            
        }

        public string GetPeriodPrice()
        {

            string strListItem = "";

            //try
            //{
                int intConditionId = int.Parse(this.qConditionId);
                ProductPriceExtra_period cProductPricePeriod = new ProductPriceExtra_period();
                ProductsupplementExtranet cDateSupplement = new ProductsupplementExtranet();
                ProductPriceExtra_period_longweek_end cPriceLongweekend = new ProductPriceExtra_period_longweek_end();

                //Response.Write(cProductPricePeriod.getPricePackageListByConditionId(intConditionId).Count());
                //Response.End();
                foreach (ProductPriceExtra_period priceperiod in cProductPricePeriod.getPricePackageListByConditionId(intConditionId))
                {
                    int pricePeriodId = priceperiod.Price_Period_Id;
                    string DateFrom = priceperiod.DateStart.ToString("dd-MMM-yyyy");
                    string DateTo = priceperiod.DateEnd.ToString("dd-MMM-yyyy");

                    string hd_DateFrom = priceperiod.DateStart.ToString("yyyy-MM-dd");
                    string hd_Dateto = priceperiod.DateEnd.ToString("yyyy-MM-dd");


                    string cColorSun = ""; string cColorMon = ""; string cColorTue = ""; string cColorWed = ""; string cColorThu = "";
                    string cColorFri = ""; string cColorSat = "";
                    string DaySurVal = "";
                    if (priceperiod.IsSub_Sun) { cColorSun = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = "0,"; }
                    if (priceperiod.IsSub_Mon) { cColorMon = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "1,"; }
                    if (priceperiod.IsSub_Tue) { cColorTue = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "2,"; }
                    if (priceperiod.IsSub_Wed) { cColorWed = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "3,"; }
                    if (priceperiod.IsSub_Thu) { cColorThu = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "4,"; }
                    if (priceperiod.IsSub_Fri) { cColorFri = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "5,"; }
                    if (priceperiod.IsSub_Sat) { cColorSat = "style=\"color:#a20c0c;font-weight:bold\""; DaySurVal = DaySurVal + "6,"; }
                    string amount = priceperiod.Price.ToString("0.##");
                    string surAmount = priceperiod.Supplement.ToString("0.##");
                    string holidaySur = "No";
                    //string holidaySur = "No";

                    IList<object> iListLongweekend = cPriceLongweekend.GetLongWeekEndList_ByConditionId(intConditionId, priceperiod.DateStart, priceperiod.DateEnd);

                    

                    string holidaySurchargeHdList = "";

                    string strholidayDetail = "<table style=\"width:100%;font-size:11px;\">";
                    
                    foreach (ProductPriceExtra_period_longweek_end longweekend in iListLongweekend)
                    {
                        holidaySurchargeHdList = holidaySurchargeHdList + longweekend.DateSupplement.ToString("yyyy-MM-dd") + ";" + longweekend.Supplement + "#";

                        strholidayDetail = strholidayDetail + "<tr>";
                        strholidayDetail = strholidayDetail + "<td>" + longweekend.DateTitle + "(" + longweekend .DateSupplement.ToString("dd-MMM-yyyy")+ "): </td>";
                        strholidayDetail = strholidayDetail + "<td><strong>" + longweekend.Supplement.ToString("#.##") + "</strong> Baht of Surcharge</td>";
                        
                        strholidayDetail = strholidayDetail + "</tr>";
                       
                    }
                    strholidayDetail = strholidayDetail + "</table>";
                    if (iListLongweekend.Count() > 0)
                    {
                        holidaySur = "<a href=\"\" onclick=\"return false;\" style=\"font-weight:bold;color:#ba0c0c;\"  class=\"tooltip\" >Yes";
                        holidaySur = holidaySur + "<span class=\"tooltip_content\">";
                       holidaySur = holidaySur + strholidayDetail;
                        holidaySur = holidaySur + "</span>";
                        holidaySur = holidaySur + "</a>";
                    }

                    strListItem = strListItem + "<div class=\"rate_result_list\" id=\"rate_result_list_" + pricePeriodId + "\" >";
                    strListItem = strListItem + "<input type=\"checkbox\" id=\"checked_rate_result_" + pricePeriodId + "\" style=\"display:none;\" value=\"" + pricePeriodId + "\" name=\"rate_result_checked\" checked=\"checked\" />";
                    strListItem = strListItem + "<table width=\"100%\">";
                    strListItem = strListItem + "<tr align=\"center\">";
                    strListItem = strListItem + "<td width=\"15%\">" + DateFrom + "<input type=\"hidden\" name=\"hd_rate_date_form_" + pricePeriodId + "\" value=\"" + hd_DateFrom + "\" /></td>";
                    strListItem = strListItem + "<td width=\"15%\">" + DateTo + "<input type=\"hidden\" name=\"hd_rate_date_To" + pricePeriodId + "\" value=\"" + hd_Dateto + "\" /></td>";
                    strListItem = strListItem + "<td width=\"10%\">" + amount + "<input type=\"hidden\" name=\"hd_amount" + pricePeriodId + "\" value=\"" + amount + "\" /></td>";

                    strListItem = strListItem + "<td width=\"10%\">" + surAmount + "<input type=\"hidden\" name=\"hd_surAmount" + pricePeriodId + "\" value=\"" + surAmount + "\" /></td>";
                    strListItem = strListItem + "<td width=\"20%\"><div class=\"day_of_week_show\" id=\"day_of_week_show_" + pricePeriodId + "\"><" + cColorSun + "p>S</p><p " + cColorMon + ">M</p><p " + cColorTue + ">T</p><p " + cColorWed + ">W</p><p " + cColorThu + ">T</p><p " + cColorFri + ">F</p><p " + cColorSat + ">S</p></div><div style=\"clear:both;\"></div><input type=\"hidden\" name=\"hd_day_checked_sur" + pricePeriodId + "\" value=\"" + DaySurVal + "\" /></td>";
                    strListItem = strListItem + "<td width=\"10%\">" + holidaySur + "<input type=\"hidden\" name=\"hd_holiday_Sur" + pricePeriodId + "\" value=\"" + holidaySurchargeHdList + "\" /></td>";

                    strListItem = strListItem + "<td width=\"5%\"><img src=\"../../images_extra/bin.png\" onclick=\"removerate('rate_result_list_" + pricePeriodId + "');\" style=\"cursor:pointer;\"  /></td>";
                    strListItem = strListItem + "</tr>";
                    strListItem = strListItem + "</table>";
                    strListItem = strListItem + "</div>";
                }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message + "---" + ex.StackTrace);
            //    Response.End();
            //}
            


            return strListItem;
        }

    

    
        

    }
}