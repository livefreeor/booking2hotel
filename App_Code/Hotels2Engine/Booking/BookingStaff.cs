using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

/// <summary>
/// Summary description for BookingItem
/// </summary>
/// 

namespace Hotels2thailand.Booking
{
    public class BookingStaff : Hotels2BaseClass
    {

        public BookingStaff() { }
        public string GetStringEmail_mailtoForm(int intBookingId)
        {
            
            string result = string.Empty;
            foreach(string email in this.GetStringEmailSendMail(intBookingId))
            {
                result = result + "<a href=\"mailto:" + email + "\" />" + email + "</a>,";
            }

            return result.Hotels2RightCrl(1);
        }

        public string GetStringEmail_mailtoForm_showMail(string mail)
        {
            
            
            string result = string.Empty;
            if (!string.IsNullOrEmpty(mail))
            {
                if (mail.ToCharArray()[mail.ToCharArray().Count() - 1] == ';')
                {

                    mail = mail.Hotels2RightCrl(1);
                }

                foreach (string email in mail.Split(';'))
                {
                    result = result + "<a href=\"mailto:" + email + "\" />" + email + "</a>,";
                }

                return result.Hotels2RightCrl(1);
            }
            else
            {
               
                return string.Empty;
            }
            
        }


        public ArrayList GetStringEmailSendMail(int intBookingId)
        {
            ArrayList arrMail = new ArrayList();
            int intProductId = 0;
            short byteStatus = 0;
            byte bytManageId = 0;
            byte bytSales = 0;
            bool IsmailNotice = false;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 p.product_id, p.status_id, pe.manage_id,pe.sale_id,pe.is_mail_notice FROM tbl_booking_product bp, tbl_product p, tbl_product_booking_engine pe WHERE p.status = 1 AND bp.status = 1 AND p.product_id=bp.product_id  AND pe.product_id=p.product_id AND bp.booking_id = " + intBookingId + "", cn);
                IDataReader reader = ExecuteReader(cmd);

                if (reader.Read())
                {
                    intProductId = (int)reader[0];
                    byteStatus = (short)reader[1];
                    bytManageId = (byte)reader[2];
                    bytSales = (byte)reader[3];
                    IsmailNotice = (bool)reader[4];
                }

                reader.Close();

                if (byteStatus == 91)
                {
                    arrMail.Add("visa@hotels2thailand.com");
                    arrMail.Add("peerapong@hotels2thailand.com");
                    arrMail.Add("rinrada@hotels2thailand.com");
                    arrMail.Add("khajitpan.ka@hotels2thailand.com");
                    arrMail.Add("nalumon.sa@hotels2thailand.com");
                    //string[] arrMailBTH = { "visa@hotels2thailand.com", "peerapong@hotels2thailand.com", "peerapon.d@hotels2thailand.com", "rinrada@hotels2thailand.com" };

                }
                else
                {
                    if (bytManageId == 2)
                    {
                        if (IsmailNotice)
                        {
                            string sqlCommand = "SELECT sf.staff_id,sf.title,sf.email";
                            sqlCommand = sqlCommand + " FROM  tbl_product p, tbl_staff sf, tbl_staff_product sp , tbl_staff_module_authorize_extranet smd";
                            sqlCommand = sqlCommand + " WHERE p.product_id = sp.product_id AND sf.staff_id = sp.staff_id  AND smd.staff_id = sp.staff_id";
                            sqlCommand = sqlCommand + " AND p.product_id = " + intProductId + " AND smd.method_id NOT IN (5,6) AND  smd.module_id = 5 AND sf.status = 1";
                            SqlCommand cmd2 = new SqlCommand(sqlCommand, cn);
                            IDataReader reader1 = ExecuteReader(cmd2);
                            while (reader1.Read())
                            {
                                arrMail.Add(reader1["email"].ToString());
                            }
                        }

                        arrMail.Add("reservation@booking2hotels.com");

                        if (bytSales == 1)
                        {
                            arrMail.Add("niponasia888@gmail.com");
                        }
                    }
                    else
                    {
                        string sqlCommand = "SELECT sf.staff_id,sf.title,sf.email";
                        sqlCommand = sqlCommand + " FROM  tbl_product p, tbl_staff sf, tbl_staff_product sp , tbl_staff_module_authorize_extranet smd";
                        sqlCommand = sqlCommand + " WHERE p.product_id = sp.product_id AND sf.staff_id = sp.staff_id  AND smd.staff_id = sp.staff_id";
                        sqlCommand = sqlCommand + " AND p.product_id = " + intProductId + " AND smd.method_id NOT IN (5,6) AND  smd.module_id = 5 AND sf.status = 1";
                        SqlCommand cmd2 = new SqlCommand(sqlCommand, cn);
                        IDataReader reader1 = ExecuteReader(cmd2);
                        while (reader1.Read())
                        {
                            arrMail.Add(reader1["email"].ToString());
                        }
                    }
                   

                }
            }

            return arrMail;
        }

        //List staff Show email for customer to contact
        public ArrayList GetStringEmailSendMail_ShowMail(int intBookingId)
        {
            ArrayList arrMail = new ArrayList();
            int intProductId = 0;
            short byteStatus = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 p.product_id, p.status_id FROM tbl_booking_product bp, tbl_product p WHERE p.status = 1 AND bp.status = 1 AND p.product_id=bp.product_id AND bp.booking_id = " + intBookingId + "", cn);
                IDataReader reader = ExecuteReader(cmd);

                if (reader.Read())
                {
                    intProductId = (int)reader[0];
                    byteStatus = (short)reader[1];
                }

                reader.Close();

                if (byteStatus == 91)
                {
                    arrMail.Add("visa@hotels2thailand.com");
                    arrMail.Add("peerapong@hotels2thailand.com");
                    arrMail.Add("khajitpan.ka@hotels2thailand.com");
                    arrMail.Add("rinrada@hotels2thailand.com");
                    arrMail.Add("nalumon.sa@hotels2thailand.com");
                    //string[] arrMailBTH = { "visa@hotels2thailand.com", "peerapong@hotels2thailand.com", "peerapon.d@hotels2thailand.com", "rinrada@hotels2thailand.com" };

                }
                else
                {
                    string sqlCommand = "SELECT sf.staff_id,sf.title,sf.email";
                    sqlCommand = sqlCommand + " FROM  tbl_product p, tbl_staff sf, tbl_staff_product sp , tbl_staff_module_authorize_extranet smd";
                    sqlCommand = sqlCommand + " WHERE p.product_id = sp.product_id AND sf.staff_id = sp.staff_id  AND smd.staff_id = sp.staff_id";
                    sqlCommand = sqlCommand + " AND p.product_id = " + intProductId + " AND (smd.method_id = 4 OR smd.method_id = 7) AND  smd.module_id = 5 AND sf.status = 1";
                    SqlCommand cmd2 = new SqlCommand(sqlCommand, cn);
                    IDataReader reader1 = ExecuteReader(cmd2);
                    while (reader1.Read())
                    {
                        arrMail.Add(reader1["email"].ToString());
                    }

                }
            }

            return arrMail;
        }
    }
}