using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for PayLaterCard
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class PayLaterCard
    {
        public byte CardType { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public byte MonthExpire { get; set; }
        public short YearExpire { get; set; }
        public string CardHolder { get; set; }
        public string IssueBank { get; set; }
        public int BookingID { get; set; }
		public int BookingHotelID { get; set; }
        public PayLaterCard()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string CombindDataCard(byte cardType, string cardNumber, string cvv, byte monthExpire, short yearExpire, string cardHolder, string issueBank,int bookingID,int bookingHotelID)
        {
            string result = "";
            string cardTypeFormat = ("0" + cardType).StringRight(2);
            string cardNumberFormat=("44"+cardNumber);
            string cvvFormat = cvv;
            string monthExpireFormat = ("0" + monthExpire).StringRight(2);
            string yearExpireFormat = yearExpire.ToString()+"12";
            result = cardTypeFormat + cardNumberFormat + cvvFormat + monthExpireFormat + yearExpireFormat + "#123#" + cardHolder + "#123#" + issueBank+"#123#"+bookingID+"#123#"+bookingHotelID;
            result = result.Hotel2EncrytedData_SecretKey_DES();
           // result = result.Hotels2DecryptedData_SecretKey_DES();
            return result;
        }

        public void ReadCreditCard(string cardData)
        {
            string[] arrCard = Regex.Split(cardData, "#123#");
            string cardDataPart = arrCard[0].ToString();
            string cardHolder = arrCard[1].ToString();
            string cardBank = arrCard[2].ToString();
            
            this.CardType = Convert.ToByte(cardDataPart.Substring(0, 2));
            this.CardNumber = cardDataPart.Substring(4, 16);
            this.CVV = cardDataPart.Substring(20, 3);
            this.MonthExpire = Convert.ToByte(cardDataPart.Substring(23, 2));
            this.YearExpire = Convert.ToInt16(cardDataPart.Substring(25, 4));
            this.CardHolder = cardHolder;
            this.IssueBank = cardBank;
            this.BookingID = int.Parse(arrCard[3]);
            this.BookingHotelID = int.Parse(arrCard[4]);
        }
    }
}