using System;
using System.Collections;
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
    public partial class admin_ajax_dash_board_allotment : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Response.Write(GetAllotmentDashBoard());
                Response.End();
                
            }
        }


        public string GetAllotmentDashBoard()
        {
            StringBuilder result = new StringBuilder();
            try
            {
                Allotment_DashBoard cAlollotment_DashBoard = new Allotment_DashBoard();
                Option cOption = new Option();
                string bulletMonth = "<img src=\"http://www.hotels2thailand.com/images/ico-square-small.png\" />&nbsp;";
                string bulletOption = "<img src=\"http://www.hotels2thailand.com/images_extra/dot_yellow.png\" />&nbsp;";

                string queryString = "";
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["supid"]))
                    queryString = "&pid=" + Request.QueryString["pid"] + "&supid=" + Request.QueryString["supid"];

                List<object> ListRoom = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
                foreach (Option room in ListRoom)
                {
                    IList<object> cListAllotment = cAlollotment_DashBoard.getAllotmentCheckDashBoard(room.OptionID, this.CurrentSupplierId);
                    if (cListAllotment.Count > 0)
                    {
                        int intCountdayAllotEmty = 0;
                        int intCountdayAllotAlmostExpired = 0;
                        int intCountdayAllotExpired = 0;
                        string MountName = string.Empty;
                        result.Append("<div class=\"alottment_room\">");
                        result.Append("<p class=\"room_name\">" + bulletOption +  room.Title + "</p>");
                        result.Append("<div class=\"allotment_month\">");
                        DateTime dDAtTemp = new DateTime(2010, 10, 10);

                        int countrecord = 0;
                        int count = 0;
                        string MonthNameTemp = string.Empty;
                        foreach (Allotment_DashBoard arrallot in cListAllotment)
                        {
                            DateTime dDateAllot = (DateTime)arrallot.DateAllot;
                            
                            if (dDateAllot >= DateTime.Now.Hotels2ThaiDateTime())
                            {

                                if (dDateAllot.Month != dDAtTemp.Month && dDateAllot != dDAtTemp)
                                {

                                    foreach (Allotment_DashBoard item in cListAllotment.Where(d => ((DateTime)d.GetType().GetProperty("DateAllot").GetValue(d, null)).Month == dDateAllot.Month && ((DateTime)d.GetType().GetProperty("DateAllot").GetValue(d, null)).Year == dDateAllot.Year && ((DateTime)d.GetType().GetProperty("DateAllot").GetValue(d, null)) >= DateTime.Now.Hotels2ThaiDateTime()))
                                    {
                                        if (!item.AllotTotal.HasValue)
                                            intCountdayAllotEmty = intCountdayAllotEmty + 1;
                                        else
                                        {
                                            if ((int)item.AllotTotal == 1)
                                                intCountdayAllotAlmostExpired = intCountdayAllotAlmostExpired + 1;
                                            if ((int)item.AllotTotal == 0)
                                                intCountdayAllotExpired = intCountdayAllotExpired + 1;
                                        }
                                    }

                                    string strAllot_Empty = "-";
                                    string strAllot_EmptyToolTip = "Sufficient allotment in this month";
                                    string strAllot1Day = "-";
                                    string strAllot1Day_ToolTip = "Sufficient allotment in this month";
                                    string strallotExp = "-";
                                    string strallotExp_ToopTip = "Sufficient allotment in this month";


                                    if (intCountdayAllotEmty > 0)
                                    {
                                        strAllot_Empty = intCountdayAllotEmty.ToString();
                                        strAllot_EmptyToolTip = "<strong>" +intCountdayAllotEmty + "</strong> day" + Hotels2String.IsFill_S(intCountdayAllotEmty) + " in total in this month has <strong>no allotment</strong>";
                 
                                    }

                                    if (intCountdayAllotAlmostExpired > 0)
                                    {
                                        strAllot1Day = intCountdayAllotAlmostExpired.ToString();
                                        strAllot1Day_ToolTip = "<strong>" +intCountdayAllotAlmostExpired.ToString() + "</strong> day" + Hotels2String.IsFill_S(intCountdayAllotAlmostExpired) + " in total in this month has <strong>1</strong> room left";
                                    }

                                    if (intCountdayAllotExpired > 0)
                                    {
                                        strallotExp = intCountdayAllotExpired.ToString();
                                        strallotExp_ToopTip = "<strong>" + intCountdayAllotExpired.ToString()  + "</strong> day" + Hotels2String.IsFill_S(intCountdayAllotAlmostExpired) + " in total in this month has <strong>0</strong> room left";
                                    }

                                    MountName = "<a class=\"link_month\" href=\"allotment/allotment_edit.aspx?oid=" + room.OptionID + "&month=" + dDateAllot.Month + "&y=" + dDateAllot.Year + queryString + "\" target=\"_Blank\" >" + dDateAllot.ToString("MMM yy") + ".</a>";

                                    if (intCountdayAllotEmty > 0 || intCountdayAllotAlmostExpired > 0 || intCountdayAllotExpired > 0)
                                    {
                                        result.Append("<div class=\"month_list\"><table cellpadding=\"0\" cellspacing=\"2\"><tr><td style=\"width:67px;margin:0px;padding:0px;\">" + bulletMonth + MountName + "&nbsp;</td>");

                                        result.Append("<td><a href=\"\" onclick=\"return false;\" class=\"tooltip\" style=\"background-color:#fdda72;text-decoration:none;\">" + strAllot_Empty);
                                        result.Append("<span class=\"tooltip_content\"><label style=\"font-size:11px;\">" + strAllot_EmptyToolTip + "</label></span>");
                                        result.Append("</a></td>");


                                        result.Append("<td><a href=\"\" onclick=\"return false;\" class=\"tooltip\" style=\"background-color:#ffce3a;text-decoration:none;\">" + strAllot1Day);
                                        result.Append("<span class=\"tooltip_content\" ><label style=\"font-size:11px;\">" + strAllot1Day_ToolTip + "</label></span>");

                                        result.Append("</a></td>");

                                        result.Append("<td><a href=\"\" onclick=\"return false;\" class=\"tooltip\" style=\"background-color:#fdbe14;text-decoration:none;\">" + strallotExp);

                                        result.Append("<span class=\"tooltip_content\" ><label style=\"font-size:11px;\">" + strallotExp_ToopTip + "</label></span>");
                                        result.Append("");
                                        result.Append("</a></td>");

                                        result.Append("</tr></table></div>");

                                        countrecord = countrecord + 1;
                                    }

                                    

                                    intCountdayAllotEmty = 0;
                                    intCountdayAllotAlmostExpired = 0;
                                    intCountdayAllotExpired = 0;
                                }

                                dDAtTemp = dDateAllot;
                                count = count + 1;
                            }

                            
                         }

                        if (countrecord == 0)
                        {
                            result.Append("<p class=\"sufficient_allotment\">" + bulletMonth + "Sufficient Allotment</p>");
                        }

                        result.Append("</div>");
                        result.Append("<div style=\"clear:both\"></div>");
                        result.Append("</div>");
                    }


                }


            }
            catch (Exception ex)
            {
                Response.Write("error: " + ex.Message);
                Response.End();
            }
               return result.ToString();
            
        }
        
       
        
    }
}