using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand;

public partial class ajax_acc_booking_quick_move_status : System.Web.UI.Page
{
        public string qBokingStatus
        {
            get
            {
                return Request.QueryString["bs"];
            }
        }


        public string qBokingStatusTo
        {
            get
            {
                return Request.QueryString["bst"];
            }
        }

        

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                //try
                //{
                    StringBuilder result = new StringBuilder();
                    Status cStatus = new Status();

                    result.Append("<select id=\"Status_Change\" name=\"Status_Change\"  class=\"DropDownStyleCustom\" >");


                    foreach (Status status in cStatus.GetStatusByCatIdBookingForAcccount(2))
                    {
                        result.Append("<option value=\"" + status.StatusID + "\">" + status.Title + "</option>");

                    }
                    result.Append("</select>");

                    Response.Write(result.ToString());
                    //Response.Write("HELLOSSSS");
                    Response.End();

                //}
                //catch (Exception ex)
                //{
                //    Response.Write(ex.Message + "--" + ex.StackTrace);
                //    Response.End();
                //}
            }else
            {
                
                string strBookingStatus = this.qBokingStatus;
                short shrStatusToUpdate = short.Parse(this.qBokingStatusTo);
                string strBookingId = Request.Form["booking_list_checked_" + strBookingStatus];
                StatusBooking cStatus = new StatusBooking();
                if (!string.IsNullOrEmpty(strBookingId))
                {
                    //cStatus.UpdateBookingStatus(225875, 71);
                    bool result = false;

                    foreach (string bookingid in strBookingId.Split(','))
                    {

                        result = cStatus.UpdateBookingStatus(int.Parse(bookingid), shrStatusToUpdate);
                    }

                    Response.Write(result);
                  
                    
                }
                else
                {
                    Response.Write("nosel");
                }

                Response.End();
               
            }


            
        }


       
}