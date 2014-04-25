using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net.Mail;


/// <summary>
/// Summary description for Hotels2Stringformat
/// </summary>
/// 
namespace Hotels2thailand
{
    public static class Hotels2MAilSender 
    {
        private static string _maildisplayBooking = "reservation@booking2hotel.com";
        
        public static bool Sendmail(string maildisplay, string mailNamedisplay, string mailTo, string Subject,string Bcc, string Mailbody)
        {
            bool success = false;
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(maildisplay, mailNamedisplay);
                if (mailTo.Split(';').Length > 0)
                {
                    string[] arrMail = mailTo.Split(';');
                    foreach(string mailitem in arrMail)
                    {
                        if (!string.IsNullOrEmpty(mailitem))
                            mail.To.Add(mailitem);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(mailTo))
                    mail.To.Add(mailTo);
                }
                
                if (!string.IsNullOrEmpty(Bcc))
                {
                    if (Bcc.Split(';').Length > 0)
                    {
                        string[] arrMail = Bcc.Split(';');
                        foreach (string mailitem in arrMail)
                        {
                            if (!string.IsNullOrEmpty(mailitem))
                                mail.Bcc.Add(mailitem);
                            //mail.To.Add(mailitem);
                        }
                    }
                    
                }

                mail.Subject = Subject;
                mail.Body = Mailbody;
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp.sendgrid.net";
                smtpClient.Credentials = new System.Net.NetworkCredential("bluehousetravel", "bhtg0ibPq");

                //smtpClient.Host = "mail.hotels2thailand.com";
                //smtpClient.Credentials = new System.Net.NetworkCredential("peerapong@hotels2thailand.com", "F=8fuieji;pq");

                
                smtpClient.Send(mail);
                success = true;
            }
            catch(Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message + "--" + ex.StackTrace);
                HttpContext.Current.Response.End();
                success = false;
            }

            return success;
        }

        public static bool SendmailAttachment(string maildisplay, string mailNamedisplay, string mailTo, string Subject, string Bcc, string Mailbody, string strAttachment)
        {
            bool success = false;
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(maildisplay, mailNamedisplay);
                if (mailTo.Split(';').Length > 0)
                {
                    string[] arrMail = mailTo.Split(';');
                    foreach (string mailitem in arrMail)
                    {
                        mail.To.Add(mailitem);
                    }
                }
                else
                {
                    mail.To.Add(mailTo);
                }

                if (!string.IsNullOrEmpty(Bcc))
                {
                    if (Bcc.Split(';').Length > 0)
                    {
                        string[] arrMail = Bcc.Split(';');
                        foreach (string mailitem in arrMail)
                        {
                            mail.Bcc.Add(mailitem);
                            //mail.To.Add(mailitem);
                        }
                    }

                }

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(strAttachment);
                mail.Attachments.Add(attachment);
                mail.Subject = Subject;
                mail.Body = Mailbody;
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp.sendgrid.net";
                smtpClient.Credentials = new System.Net.NetworkCredential("bluehousetravel", "bhtg0ibPq");

                smtpClient.Send(mail);
                success = true;
            }
            catch 
            {
                //HttpContext.Current.Response.Write("Mail Error: " + ex.Message + "<br />" + ex.StackTrace + "<br />");
                //HttpContext.Current.Response.End();
                success = false;
            }

            return success;
        }
        public static bool SendmailNormail(string maildisplay, string mailNamedisplay, string mailTo, string Subject, string Bcc, string Mailbody)
        {
            return Sendmail(maildisplay, mailNamedisplay, mailTo, Subject, Bcc, Mailbody);
        }


        public static bool SendmailBooking(string mailNamedisplay, string mailTo, string Subject, string Bcc, string Mailbody)
        {
            return Sendmail(_maildisplayBooking, mailNamedisplay, mailTo, Subject, Bcc, Mailbody);
        }


        public static bool SendmailBooking(string maildisplay , string mailNamedisplay, string mailTo, string Subject, string Bcc, string Mailbody)
        {
            return Sendmail(maildisplay, mailNamedisplay, mailTo, Subject, Bcc, Mailbody);
        }

    }
}