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
    public partial class admin_ajax_allotment_list : Hotels2BasePageExtra_Ajax
    {
        public string qRoomCustom
        {
            get { return Request.QueryString["custom"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {

                    Response.Write(allotmentList());
                    //Response.Write("HELLO");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }


        public string allotResultBodyList(int intOptionId, short shrSupplier)
        {
            StringBuilder result = new StringBuilder();
            
            DateTime dDateStart = Request.Form["hd_edit_date_start"].Hotels2DateSplitYear("-");
            DateTime dDateend = Request.Form["hd_edit_date_end"].Hotels2DateSplitYear("-");

            
            Allotment cAllotment = new Allotment(this.CurrentProductActiveExtra);

            
            List<object> listAllot = cAllotment.getAllotMentListByOptionId(intOptionId, shrSupplier, dDateStart, dDateend);
           
            int DateDiff = dDateend.Subtract(dDateStart).Days;
            DateTime dDateCurrent = DateTime.Now;

            
            for (int days = 0; days <= DateDiff; days++)
            {
                dDateCurrent = dDateStart.AddDays(days);
               
                int count = 0;
                string[] DayShortName = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
                DateTime dDateAllot = dDateCurrent;
                int intAllotTotal = 0;
                int intCutoff = 0;
                bool allotstatus = true;
                int intAllotId = 0;
                string CheckedBoxKeyValue = string.Empty;
                string Key = string.Empty;
                foreach (Allotment allot in listAllot)
                {
                    if (dDateCurrent.Date == allot.DateAllotment.Date)
                    {
                        intAllotId = allot.AllotmentId;
                        dDateAllot = allot.DateAllotment;
                        intAllotTotal = (int)allot.TotalQuantity;
                        intCutoff = allot.NumDateCutOff;
                        allotstatus = allot.Status;
                        Key = intAllotId.ToString();
                        CheckedBoxKeyValue = "<input type=\"checkbox\" value=\"" + intAllotId + "\" checked=\"checked\" name=\"chk_to_update\" style=\"display:none;\"/>";
                        count = count + 1;
                        break;
                    }
                }


                if (count == 0)
                {
                    Key = intOptionId + "%" + dDateAllot.ToString("yyyy-MM-dd");
                    CheckedBoxKeyValue = "<input type=\"checkbox\" value=\"" + Key + "\" checked=\"checked\" name=\"chk_to_insert\" style=\"display:none;\"/>";
                }
                

                int RowCount = days + 1;
                string ColorRow = "#ffffff";
                string ColorDrop = "#ffffff";
                if (RowCount % 2 == 0)
                    ColorRow = "#f2f2f2";

                if (intAllotTotal < 2)
                    ColorDrop = "#ffebe8";

                result.Append("<tr bgcolor=\"" + ColorRow + "\" align=\"center\">");
                result.Append("<td>" + dDateAllot.ToString("dd-MMM-yyyy") + CheckedBoxKeyValue + "</td>");

                result.Append("<td>" + DayShortName[(int)dDateAllot.DayOfWeek] + "</td>");
                result.Append("<td>");
                result.Append("<select name=\"room_allot_" + Key + "\" class=\"Extra_Drop\" style=\"background-color:" + ColorDrop + "\" >");
                result.Append("");
                foreach (KeyValuePair<int, string> num in this.dicGetNumberstart0(50))
                {
                    if (num.Key == intAllotTotal)
                        result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                    else
                        result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
                }

                result.Append("");
                result.Append("</select>");
                result.Append("</td>");


                result.Append("<td>");
                result.Append("<select name=\"cutoff_allot_" + Key + "\" class=\"Extra_Drop\" >");
                foreach (KeyValuePair<int, string> num in this.dicGetNumberstart0(90))
                {
                    if (num.Key == intCutoff)
                        result.Append("<option value=\"" + num.Key + "\" selected=\"selected\">" + num.Value + "</option>");
                    else
                        result.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
                }
                result.Append("</select>");
                result.Append("</td>");

                

                if (allotstatus)
                {
                    result.Append("<td>");
                    result.Append("<input type=\"radio\" name=\"radio_status_" + Key + "\" checked=\"checked\"  value=\"1\" /> Open ");
                    result.Append("<input type=\"radio\" name=\"radio_status_" + Key + "\"  value=\"0\" /> Close");
                    result.Append("</td>");
                }
                else
                {
                    result.Append("<td>");
                    result.Append("<input type=\"radio\" name=\"radio_status_" + Key + "\" value=\"1\" /> Open");
                    result.Append("<input type=\"radio\" name=\"radio_status_" + Key + "\" checked=\"checked\" value=\"0\" /> Close");
                    result.Append("</td>");
                }
                
                result.Append("</tr>");


            }
            
            return result.ToString();
        }

        public string allotmentList()
        {
            StringBuilder result = new StringBuilder();
            StringBuilder resulthead = new StringBuilder();
            try
            {

                int intOptionSelected = int.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropOption"]);


                resulthead.Append("<table class=\"tbl_acknow\" cellpadding=\"0\" cellspacing=\"2\">");
                resulthead.Append("<tr class=\"header_field\" >");
                resulthead.Append("<th align=\"center\">Date</th>");
                resulthead.Append("<th align=\"center\">Day</th>");
                resulthead.Append("<th align=\"center\">Available</th>");
                resulthead.Append("<th align=\"center\">Cut off</th>");
                resulthead.Append("<th align=\"center\">Close out</th>");
               
                resulthead.Append("</tr>");
                resulthead.Append("<tr bgcolor=\"#f5f6f6\" align=\"center\">");
                resulthead.Append("<td></td>");
                resulthead.Append("<td></td>");
                
                resulthead.Append("<td>");
                resulthead.Append("<select name=\"room_allot_autofill\" id=\"room_allot_autofill\" class=\"Extra_Drop\" >");
                foreach (KeyValuePair<int, string> num in this.dicGetNumberstart0(50))
                {
                    resulthead.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
                }
                resulthead.Append("</select>");
                resulthead.Append("&nbsp;<input type=\"button\" value=\"Auto Fill\" onclick=\"Autofill('room');return false;\"  class=\"Extra_Button_small_green\" />");
                resulthead.Append("</td>");

                resulthead.Append("<td>");
                resulthead.Append("<select name=\"cutoff_allot_autofill\" id=\"cutoff_allot_autofill\" class=\"Extra_Drop\" >");
                foreach (KeyValuePair<int, string> num in this.dicGetNumberstart0(90))
                {
                    resulthead.Append("<option value=\"" + num.Key + "\">" + num.Value + "</option>");
                }
                resulthead.Append("</select>");
                resulthead.Append("&nbsp;<input type=\"button\" value=\"Auto Fill\" onclick=\"Autofill('cutoff');return false;\"  class=\"Extra_Button_small_green\" />");
                resulthead.Append("</td>");

                resulthead.Append("<td>");
                resulthead.Append("<select name=\"close_out_autofill\" id=\"close_out_autofill\" class=\"Extra_Drop\" >");
                resulthead.Append("<option value=\"1\">Open</option>");
                resulthead.Append("<option value=\"0\">Close</option>");
                resulthead.Append("</select>");
                resulthead.Append("&nbsp;<input type=\"button\" value=\"Auto Fill\" onclick=\"Autofill('close');return false;\"  class=\"Extra_Button_small_green\" />");
                resulthead.Append("</td>");
                //resulthead.Append("<td><input type=\"button\" value=\"Auto Fill\" onclick=\"Autofill();return false;\"  class=\"Extra_Button_small_green\" /></td>");
                resulthead.Append("</tr>");
                

                Option cOption = new Option();
                short shrstaffSupplier = this.CurrentSupplierId;

                string RoomresultDisplay = "";
                if (!string.IsNullOrEmpty(this.qRoomCustom))
                {
                    RoomresultDisplay = this.qRoomCustom.Trim();
                }
                else
                {
                    if (intOptionSelected == 0)
                    {
                        List<object> ListRoom = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, shrstaffSupplier);
                        foreach (Option OptionItem in ListRoom)
                        {
                            RoomresultDisplay = RoomresultDisplay + OptionItem.OptionID + ",";
                        }
                    }
                    else
                    {
                        cOption = cOption.getOptionById(intOptionSelected);
                        RoomresultDisplay = cOption.OptionID + ",";
                    }
                }

               

                    result.Append(resulthead.ToString());

                    foreach (string OptionItem in RoomresultDisplay.Hotels2RightCrl(1).Trim().Split(','))
                    {

                        cOption = cOption.getOptionById(int.Parse(OptionItem));

                        result.Append("<tr bgcolor=\"#ffffff\" >");
                        result.Append("<td colspan=\"6\" style=\" font-weight:bold; font-size:12px; color:#333333;\"><img src=\"/images/dot.png\" />&nbsp;" + cOption.Title + "</td>");
                        result.Append("</tr>");

                        result.Append(allotResultBodyList(int.Parse(OptionItem), shrstaffSupplier));

                       
                    }
                    result.Append("</table>");
                


                    result.Append("<div id=\"condition_manage_save\" style=\"text-align:center; margin:15px 0px 0px ; padding:5px; border:1px solid #cccccc; background-color:#f2f2f2;\">");
    

                    result.Append("<input type=\"button\" id=\"btnSave\" value=\"Update Allotment\" onclick=\"SaveeditAllotment();return false;\" class=\"Extra_Button_green\" style=\"width:300px;\" />");
                    result.Append("&nbsp;<input type=\"button\" id=\"btnreset\" value =\"Reset\" onclick=\"resetAutofill();return false;\" class=\"Extra_Button_small_white\" />");
    
                    result.Append("</div>");
                

            }
            catch (Exception ex)
            {
                Response.Write("error#1# : allotList" + ex.Message);
                Response.End();
            }
            
            return result.ToString();
        }
        
    }
}