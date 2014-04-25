using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class ajax_product_option_holidays_list : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qstatus
        {
            get { return Request.QueryString["status"]; }
        }

        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }

        public string qyear
        {
            get { return Request.QueryString["y"]; }
        }
        public short getCurrentSupplier
        {
            get
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                return cProduct.SupplierPrice;
            }
        }
        //public string qProductCat
        //{
        //    get { return Request.QueryString["pdcid"]; }
        //}

        //public byte Current_StaffLangId
        //{
        //    get 
        //    { 
        //        Hotels2BasePage cBasePage = new Hotels2BasePage();
        //        return cBasePage.CurrenStafftLangId;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && string.IsNullOrEmpty(this.qSupplierId)  && string.IsNullOrEmpty(this.qOptionId) && string.IsNullOrEmpty(this.qyear) && string.IsNullOrEmpty(this.qstatus))
            {
                //Response.Write(Request.Url.ToString());
                Response.Write(OptionActiveLsit(getCurrentSupplier, int.Parse(qProductId)));
                
            }
            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId) && string.IsNullOrEmpty(this.qOptionId) && string.IsNullOrEmpty(this.qyear) && string.IsNullOrEmpty(this.qstatus))
            {
                //Response.Write(Request.Url.ToString());
                short SupplierId = short.Parse(this.qSupplierId);
                Response.Write(OptionActiveLsit(SupplierId, int.Parse(qProductId)));

            }
            if (!string.IsNullOrEmpty(this.qProductId) && string.IsNullOrEmpty(this.qSupplierId) && !string.IsNullOrEmpty(this.qOptionId) && !string.IsNullOrEmpty(this.qyear) && string.IsNullOrEmpty(this.qstatus))
            {
                //Response.Write(Request.Url.ToString());
                DateTime dDateYear = new DateTime(int.Parse(this.qyear), 9, 9);
                Response.Write(SupplementList(getCurrentSupplier, int.Parse(qOptionId), dDateYear, true));

            }

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId) && !string.IsNullOrEmpty(this.qOptionId) && !string.IsNullOrEmpty(this.qyear) && string.IsNullOrEmpty(this.qstatus))
            {
                //Response.Write(Request.Url.ToString());
                DateTime dDateYear = new DateTime(int.Parse(this.qyear), 9, 9);
                Response.Write(SupplementList(short.Parse(this.qSupplierId), int.Parse(qOptionId), dDateYear, true));

            }

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId) && !string.IsNullOrEmpty(this.qOptionId) && !string.IsNullOrEmpty(this.qyear) && !string.IsNullOrEmpty(this.qstatus))
            {
                //Response.Write(Request.Url.ToString());
                DateTime dDateYear = new DateTime(int.Parse(this.qyear), 9, 9);
                bool Status = bool.Parse(this.qstatus);
                Response.Write(SupplementList(short.Parse(this.qSupplierId), int.Parse(qOptionId), dDateYear, Status));

            }
            Response.End();
            
        }

        public string OptionActiveLsit(short supplierId, int intProductId)
        {
            StringBuilder strResult = new StringBuilder();
            Option cOption = new Option();

            strResult.Append("<p id=\"optionListDisplay\">");
            strResult.Append("<select name=\"dropOptionList\" id=\"dropOptionList\" class=\"dropStyle\"  onchange=\"SuppleMentList();\">");

            foreach (Option item in cOption.GetProductOptionByCurrentSupplierANDProductId(supplierId, intProductId))
            {
               strResult.Append("<option value=\"" + item.OptionID + "\"  >" + item.Title + "</option>");
            }

            strResult.Append("</select>");

            strResult.Append("&nbsp;Year Select&nbsp;<select name=\"supList_dropyear\" id=\"supList_dropyear\" class=\"dropStyle\" style=\"width:150px;\" onchange=\"SuppleMentList_YearChang();\">");

            foreach (KeyValuePair<string, string> item in Hotels2DateTime.GetYearList())
            {
                if (item.Key == DateTime.Now.Year.ToString())
                    strResult.Append("<option value=\"" + item.Key + "\" selected=\"selected\" >" + item.Key + "</option>");
                else
                    strResult.Append("<option value=\"" + item.Key + "\">" + item.Key + "</option>");
            }

            strResult.Append("</select>");
            strResult.Append("<a href=\"\" id=\"holiday_Enable\" class=\"holiday_Enable\" onclick=\"SupListStatus('True','holiday_Enable');return false;\" >Active</a><a href=\"\"  id=\"holiday_Disable\" class=\"holiday_Disable\" onclick=\"SupListStatus('False','holiday_Disable');return false;\">Bin</a></p>");
            

            return strResult.ToString();
        }

        public string SupplementList(short supplierId, int intOptionId, DateTime dDateYear, bool bolStatus)
        {
            StringBuilder strResult = new StringBuilder();
            ProductOptionSupplementDate cSupplement = new ProductOptionSupplementDate();

            
            strResult.Append("<div id=\"holidayCurrentList\">");
            strResult.Append("<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center\">");
            strResult.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold\">");
            strResult.Append("<td  width=\"5%\">No.</td><td width=\"45%\">Title</td><td width=\"20%\" >Date</td><td  width=\"20%\">Amount</td><td  width=\"25%\">Move To Bin</td>");
            strResult.Append("</tr>");
            int countNum = 1;
            foreach (ProductOptionSupplementDate Sup in cSupplement.getOptionSuppleMentListCurrentYearBySupplierAndOptionId(supplierId, intOptionId, dDateYear, bolStatus))
            {
                strResult.Append("<tr style=\"background-color:#ffffff;\">");
                strResult.Append("<td>" + countNum + "</td>");
                strResult.Append("<td><input type=\"text\" name=\"txtSuptitle_" + Sup.SuppleMentId + "\" id=\"txtSuptitle_" + Sup.SuppleMentId + "\" class=\"TextBox_Extra\" value=\"" + Sup.DateTitle + "\" style=\" width:250px; padding:2px;\" /></td>");
                //strResult.Append("<td style=\"text-align:left;padding:0px 0px 0px 5px\">" + Sup.DateTitle + "</td>");
                //strResult.Append("<td>" + hol.HolidayDate.ToString("yyyy-MM-dd") + "</td>");
                strResult.Append("<td><input type=\"text\" name=\"txtDateStart_" + Sup.SuppleMentId + "\" id=\"txtDateStart_" + Sup.SuppleMentId + "\" value=\"" + Sup.DateSupplement.ToString("yyyy-MM-dd") + "\" class=\"TextBox_Extra_normal\" style=\" width:120px; padding:2px;\" /></td>");
                strResult.Append("<td><input type=\"text\" name=\"txtAmount_" + Sup.SuppleMentId + "\" id=\"txtAmount_" + Sup.SuppleMentId + "\" class=\"TextBox_Extra\" value=\"" + Sup.SupplementAmount.ToString("#.##") + "\" style=\" width:120px; padding:2px;\" /></td>");
                strResult.Append("<td><input type=\"checkbox\" name=\"checkbin\" id=\"checkbin_" + Sup.SuppleMentId + "\" value=\"" + Sup.SuppleMentId + "\" ></td>");
                strResult.Append("</tr>");

                countNum = countNum + 1;
            }
            strResult.Append("</table><br/>");
            if (countNum - 1 > 0)
            {
                strResult.Append("<input type=\"button\" value=\"Save\" class=\"btStyleGreen\" onclick=\"SupplementUpdate();return false;\" />&nbsp;<input type=\"button\" value=\"Move To Bin\" onclick=\"SupplementUpdatestatus();return false;\" class=\"btStyleRed\" style=\"float:right;\" />");
            }
            strResult.Append("</div>");

            return strResult.ToString();
        }

        


        
    }
}