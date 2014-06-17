using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;
/// <summary>
/// Summary description for PaymentMethod
/// </summary>
/// 
namespace Hotels2thailand.Front
{
public class PaymentInfo:Hotels2BaseClass
{
    public int BookingID { get; set; }
    public int BookingHotelID { get; set; }
    public int PaymentID { get; set; }
    public int PaymentBankID { get; set; }
    public byte GatewayID { get; set; }
    public string MerchantID { get; set; }
    public string TerminalID { get; set; }
    public string BackgroundUrl { get; set; }
    public string ResponseUrl { get; set; }
    public string UrlRedirect { get; set; }
    public decimal Amount { get; set; }
    public byte ManageID { get; set; }
    public string WebsiteName { get; set; }
    public string FolderName { get; set; }
    public byte CurrencyID { get; set; }

    public string ProfileID { get; set; }
    public string Access_Key { get; set; }
    public string Secret_Key { get; set; }
    public string CountryTitle { get; set; }

    public DateTime BookingSubmit { get; set; }
    public DateTime Date_check_in { get; set; }
    public DateTime Date_check_out { get; set; }

    public string DestinationTitle { get; set; }

    public string Producttitle { get; set; }

    public string MD5 { get; set; }
    public PaymentInfo()
    {
    }

    public PaymentInfo(int paymentBankID)
	{
        PaymentBankID = paymentBankID;
	}
    public PaymentInfo getPaymentInfo()
    {
        PaymentInfo result = null;
        using(SqlConnection cn=new SqlConnection(this.ConnectionString))
        {
            string sqlCommand = string.Empty;
            sqlCommand = sqlCommand + "select top 1 bpay.booking_id,bpay.booking_payment_id,bh.booking_hotel_id,bpb.booking_payment_bank_id,pbe.currency_id";
            sqlCommand = sqlCommand + " ,bpay.amount,bpay.gateway_id,pbe.merchant_id,pbe.teminal_id,pbe.url_return,pbe.url_update,pbe.url_site_redirect,pbe.manage_id,pbe.web_site_name,pbe.folder,pbe.profile_id,pbe.access_key,pbe.secret_key, con.title as countryTitle, b.date_submit, bp.date_time_check_in, bp.date_time_check_out, des.title AS desTitle, p.title AS ProTitle,pbe.md5";
            sqlCommand = sqlCommand + " from tbl_booking_product bp,tbl_booking_payment bpay,tbl_booking_payment_bank bpb,tbl_booking_hotels bh,tbl_product_booking_engine pbe,tbl_product p, tbl_booking b, tbl_country con,tbl_destination des";
            sqlCommand = sqlCommand + " where bp.booking_id=bpay.booking_id and bpay.booking_payment_id=bpb.booking_payment_id and p.destination_id = des.destination_id  AND b.booking_id = bp.booking_id AND b.country_id = con.country_id and bh.booking_id=bp.booking_id and pbe.product_id=bp.product_id and p.product_id=pbe.product_id and bpb.booking_payment_bank_id=" + PaymentBankID;
            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.End();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                if(reader.Read())
                {
                    result = new PaymentInfo
                    {
                        PaymentID=(int)reader["booking_payment_id"],
                        PaymentBankID = (int)reader["booking_payment_bank_id"],
                        BookingID=(int)reader["booking_id"],
                        BookingHotelID=(int)reader["booking_hotel_id"],
                        GatewayID=(byte)reader["gateway_id"],
                        MerchantID=reader["merchant_id"].ToString(),
                        TerminalID = reader["teminal_id"].ToString(),
                        BackgroundUrl=reader["url_update"].ToString(),
                        ResponseUrl = reader["url_return"].ToString(),
                        UrlRedirect=reader["url_site_redirect"].ToString(),
                        Amount=(decimal)reader["amount"],
                        ManageID=(byte)reader["manage_id"],
                        WebsiteName = reader["web_site_name"].ToString(),
                        FolderName=reader["folder"].ToString(),
                        CurrencyID=(byte)reader["currency_id"],
                        ProfileID = reader["profile_id"].ToString(),
                        Access_Key = reader["access_key"].ToString(),
                        Secret_Key = reader["secret_key"].ToString(),
                        CountryTitle = reader["countryTitle"].ToString(),
                        BookingSubmit = (DateTime)reader["date_submit"],
                        Date_check_in = (DateTime)reader["date_time_check_in"],
                        Date_check_out = (DateTime)reader["date_time_check_out"],
                        DestinationTitle = reader["desTitle"].ToString(),
                        Producttitle = reader["ProTitle"].ToString(),
                        MD5 = reader["md5"].ToString()
                    };
                }
    
                
        }
        return result;
    }
}
}