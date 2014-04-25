using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_landmark_list : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
            {
                Response.Write(Result());
                Response.End();
            }
            
        }

        public string Result()
        {
            StringBuilder strResult = new StringBuilder();
            ProductLandmark cProductLandmark = new ProductLandmark();
            
            

            strResult.Append("<h4>LandMark List</h4>");
            strResult.Append("<div id=\"landmark_list\">");
            
            foreach(ProductLandmark LandmarkItem in cProductLandmark.geProductLandmarkListbyProduct(int.Parse(this.qProductId)))
            {
                strResult.Append("<div class=\"landmark_list_item\" id=\"landmark_list_item_" + LandmarkItem.LandmarkID + "\">");
                strResult.Append("<p class=\"landmark_title\">" + LandmarkItem.LandmarkTitle + "&nbsp;<span>(" + LandmarkItem.LandmarkTitleCat + ")</span><a href=\"\" onclick=\"LandMarkDel('" + LandmarkItem.LandmarkID + "');return false;\" title=\"Remove\">Remove</a></p>");
                strResult.Append("<div style=\" clear:both;\"></div>");
                strResult.Append("<div class=\"landmark_item_td\">");

                strResult.Append("<select name=\"transport_cat_" + LandmarkItem.LandmarkID + "\" class=\"DropDownStyleCustom\" >");
                foreach (KeyValuePair<byte, string> item in ProductTransportCat.getDictransportAll())
                {
                    if(item.Key == LandmarkItem.TransportId)
                        strResult.Append("<option value=\"" + item.Key + "\" selected=\"selected\" >" + item.Value + "</option>");
                    else
                        strResult.Append("<option value=\"" + item.Key + "\">" + item.Value + "</option>");
                }
                strResult.Append("</select>");
                strResult.Append("</div>");


                double douTime = Convert.ToDouble(LandmarkItem.TransportTime);
                double douDistance = Convert.ToDouble(LandmarkItem.TransportDistance);
                int intTotalTimeHrs = (int)(douTime / 60);
                int intTotalTimeMins = (int)(douTime % 60);

                int intDistanceKM = (int)(douDistance / 1000);
                decimal decDimtanceMeter = (decimal)((douDistance / 1000) - (int)(LandmarkItem.TransportDistance / 1000));
                string strDimtanceMeter = decDimtanceMeter.ToString("#0.000");
                double DistanceMeter = double.Parse(strDimtanceMeter) * 1000;

                strResult.Append("<div class=\"landmark_item_td\">");
                strResult.Append("<p>");
                strResult.Append("<span>Time :</span><input type=\"text\" id=\"txthrs_" + LandmarkItem.LandmarkID + "\" title=\"hrs.\" name=\"txthrs_" + LandmarkItem.LandmarkID + "\" value=\"" + intTotalTimeHrs.ToString() + "\" maxlength=\"2\" class=\"TextBox_Extra\" style=\"width:30px; padding:2px;\" />&nbsp;");
                strResult.Append("<input type=\"text\" id=\"txtmins_" + LandmarkItem.LandmarkID + "\" title=\"mins.\"  name=\"txtmins_" + LandmarkItem.LandmarkID + "\" value=\"" + intTotalTimeMins.ToString("00") + "\" maxlength=\"2\" class=\"TextBox_Extra\"  style=\"width:30px;padding:2px;\" />");
                strResult.Append("</p>");
                
                strResult.Append("</div>");

                strResult.Append("<div class=\"landmark_item_td\">");

                strResult.Append("<p>");
                strResult.Append("<span>Distance :</span><input type=\"text\" id=\"txtKm_" + LandmarkItem.LandmarkID + "\" title=\"KM.\" name=\"txtKm_" + LandmarkItem.LandmarkID + "\" value=\"" + intDistanceKM.ToString() + "\" maxlength=\"2\" class=\"TextBox_Extra\" style=\"width:30px;padding:2px;\" />&nbsp;");
                strResult.Append("<input type=\"text\" id=\"txtMeter_" + LandmarkItem.LandmarkID + "\" title=\"Meter.\" name=\"txtMeter_" + LandmarkItem.LandmarkID + "\" value=\"" + DistanceMeter.ToString("000") + "\" maxlength=\"3\" class=\"TextBox_Extra\" style=\"width:30px;padding:2px;\" />");
                strResult.Append("</p>");
                strResult.Append("</div>");

                strResult.Append("<div class=\"landmark_item_td\"><input type=\"button\" value=\"save\" class=\"btStyleGreen\" onclick=\"landmark_save_item('" + LandmarkItem.LandmarkID + "');\" /></div>");
                strResult.Append("<div style=\" clear:both;\"></div>");
                strResult.Append("</div>");
            }
            
            strResult.Append("</div>");
            return strResult.ToString();
        }

        
        
    }
}