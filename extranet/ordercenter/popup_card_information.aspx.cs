using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using Hotels2thailand.Front;
using Hotels2thailand;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class extranet_ordercenter_popup_card_information : Hotels2BasePageExtra
    {
        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                

            }
        }


        public string GetCardType(byte strType)
        {
            string result = string.Empty;
            switch (strType)
            {
                case 1:
                    result = "Visa";
                    break;
                case 2:
                    result = "Master Card";
                    break;
                case 3:
                    result = "JCB";
                    break;
            }

            return result;
        }
        public void btnDecode_Onclick(object sender, EventArgs e)
        {

            //cCard.ReadCreditCard();
            //CardEngine cCardEngine = new CardEngine();

            StringBuilder result = new StringBuilder();


            int intBookingId = int.Parse(this.qBookingId);

            //List<object> CardDetailList = cCardEngine.GetCardCodeToChage(intBookingId);
            

            PayLaterCard cCard = new PayLaterCard();
            cCard.ReadCreditCard(txtInput.Text.Hotels2DecryptedData_SecretKey_DES());
            result.Append("<p class=\"chead\">Booking number : " + cCard.BookingHotelID + "</p>");
            
            if (intBookingId == cCard.BookingID)
            {
                    result.Append("<table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\">");

                    result.Append("<tr>");
                    result.Append("<td class=\"hh\">Card Type</td><td class=\"hd\">" + GetCardType(cCard.CardType) + "</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td class=\"hh\">Card Number</td><td class=\"hd\">" + cCard.CardNumber + "</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td class=\"hh\">CVV</td><td class=\"hd\">" + cCard.CVV + "</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td class=\"hh\">Expired</td><td class=\"hd\">" + cCard.MonthExpire + "/" + cCard.YearExpire + "</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td class=\"hh\">Card Holder</td><td class=\"hd\">" + cCard.CardHolder + "</td>");
                    result.Append("</tr>");
                    result.Append("<tr>");
                    result.Append("<td class=\"hh\">Bank</td><td class=\"hd\">" + cCard.IssueBank + "</td>");
                    result.Append("</tr>");

                    result.Append("</table><br/><br/>");

            }
            else
            {
                result.Append("<table>");

                result.Append("<tr>");

                result.Append("<td>Please check email for \"Order Id\" must be match!</td>");
                result.Append("</tr>");

                result.Append("</table>");
            }

            ltCardDetail.Text = result.ToString();
        }
    }
}