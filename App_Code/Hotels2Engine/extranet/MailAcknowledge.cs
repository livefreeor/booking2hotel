using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for MailAcknowledge
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class MailAcknowledge:Hotels2BaseClass
    {
        public MailAcknowledge()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void SendMailToHotelStaff(int ProductID,int BookingID,byte mailType)
        {
            

            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = string.Empty;

                sqlCommand = "SELECT sf.staff_id,sf.title,sf.email";
                sqlCommand = sqlCommand + " FROM  tbl_product p, tbl_staff sf, tbl_staff_product sp , tbl_staff_module_authorize_extranet smd";
                sqlCommand = sqlCommand + " WHERE p.product_id = sp.product_id AND sf.staff_id = sp.staff_id  AND smd.staff_id = sp.staff_id";
                sqlCommand = sqlCommand + " AND p.product_id = " + ProductID + " AND smd.method_id <> 5 AND  smd.module_id = 5 AND sf.status = 1";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                IDataReader rdStaff = cmd.ExecuteReader();
                string enParam = string.Empty;
                string mailBody = string.Empty;
                string mailSubject = string.Empty;

                while (rdStaff.Read())
                {
                    if (mailType==1)
                    {
                        enParam = Hotels2String.EncodeIdToURL("sid=" + rdStaff["staff_id"] + "&bid=" + BookingID + "&st=2");

                        mailSubject = "Hotels2 Extranet System: Confirmed Allotment Booking ID:" + BookingID;
                        mailBody = mailBody + "Dear Hotel Reservations,";
                        mailBody = mailBody + "<p>Your new booking has been confirmed to customer with inside allotment automatically. Please click on the link below to acknowledge this booking.<br/><br/>";
                        mailBody = mailBody + "Click to acknowledge <br/>";

                        mailBody = mailBody + "<a href=\"http://www.hotels2thailand.com/extranet_confirm.aspx?" + enParam + "\">http://www.hotels2thailand.com/extranet_confirm.aspx?" + enParam + "</a>";
                        mailBody = mailBody + "<p><br/>After this link is clicked to be acknowledged, hotel is responsible for honoring Blue House Travel’s booking.</p>";
                        mailBody = mailBody + "<br/><br/>--------------------------------------------------------------<br/>";
                        mailBody = mailBody + "Warmest Regards<br/>";
                        mailBody = mailBody + "Blue House Travel Co.,Ltd<br/>";
                        mailBody = mailBody + "Tel: 66-2-9300973, 66-2-9306050<br/>";
                        mailBody = mailBody + "Fax: 66-2-9306514, 66-2-9306825<br/>";
                        mailBody = mailBody + "http://www.hotels2thailand.com<br/>";
                        mailBody = mailBody + "Email: reservation@hotels2thailand.com</p>";
                    }else{
                        enParam = Hotels2String.EncodeIdToURL("sid=" + rdStaff["staff_id"] + "&bid=" + BookingID + "&st=4");
                        mailSubject = "Subject: Hotels2 Extranet System: CANCELLATION Booking ID: " + BookingID;
                        mailBody = mailBody + "Dear Hotel Reservations,";
                        mailBody = mailBody + "<p>Please note the CANCELLATION Booking ID:" + BookingID + "<br/>";
                        mailBody = mailBody + "This booking has been confirmed to CANCEL automatically. Please click on the link below to acknowledge the cancellation of this booking.<br/><br/>";
                        mailBody = mailBody + "Click to acknowledge <br/>";
                        mailBody = mailBody + "<a href=\"http://www.hotels2thailand.com/extranet_confirm.aspx?" + enParam + "\">http://www.hotels2thailand.com/extranet_confirm.aspx?" + enParam + "</a><br/><br/>";
                        mailBody = mailBody + "After this link is clicked to be acknowledged, hotel is responsible for honoring Blue House Travel’s booking.<br/>";
                        mailBody = mailBody + "<br/><br/>--------------------------------------------------------------<br/>";
                        mailBody = mailBody + "Warmest Regards<br/>";
                        mailBody = mailBody + "Blue House Travel Co.,Ltd<br/>";
                        mailBody = mailBody + "Tel: 66-2-9300973, 66-2-9306050<br/>";
                        mailBody = mailBody + "Fax: 66-2-9306514, 66-2-9306825<br/>";
                        mailBody = mailBody + "http://www.hotels2thailand.com<br/>";
                        mailBody = mailBody + "Email: reservation@hotels2thailand.com</p>";
                    }
                    
                    
                    Hotels2MAilSender.SendmailBooking("reservation@hotels2thailand.com", rdStaff["email"].ToString(), mailSubject, "peerapong@hotels2thailand.com", mailBody);
                    //Hotels2MAilSender.SendmailBooking("reservation@hotels2thailand.com","kiasa555@gmail.com", "Reservation confirm from hotels2thailand.com (ORDER ID:" + BookingID + ")", "", mailBody);
                }
            }

        }
    }
}