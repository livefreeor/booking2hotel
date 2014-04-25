using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hotels2thailand.LinqProvider.Booking;
using Hotels2thailand.Booking;

/// <summary>
/// Summary description for Currency
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class Currency : Hotels2BaseClass
    {
        private LinqBookingDataContext dcBooking = new LinqBookingDataContext();

        public byte CurrencyID { get; set; }
        public string Title { get; set; }
        public float Prefix { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }

        public List<object> GetCurrencyAll()
        {
            var result = from item in dcBooking.tbl_currencies
                         where  item.status == true
                         orderby item.code 
                         select item;

            return MappingObjectFromDataContextCollection(result);
        }

        public Currency GetCurrencyById(int intCurrencyId)
        {
            var result = dcBooking.tbl_currencies.SingleOrDefault(bc => bc.currency_id == intCurrencyId);

            //HttpContext.Current.Response.Write(result.condition_title);
            //HttpContext.Current.Response.End();
            return (Currency)MappingObjectFromDataContext(result);
        }

        public bool UpdateCurrencyById(byte bytCurrencyID, float prefix)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_currency SET prefix=@prefix WHERE currency_id=@currency_id", cn);
                cmd.Parameters.Add("@currency_id", SqlDbType.TinyInt).Value = bytCurrencyID;
                cmd.Parameters.Add("@prefix", SqlDbType.Real).Value = prefix;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
       
        //public bool Update(BookingItem data)
        //{
        //    //HttpContext.Current.Response.Write(data.BookingItemID + "<br>");
        //    //HttpContext.Current.Response.Write(data.BookingID + "<br>");
        //    //HttpContext.Current.Response.Write(data.OptionID + "<br>");
        //    //HttpContext.Current.Response.Write(data.Unit + "<br>");
        //    //HttpContext.Current.Response.Write(data.PriceSupplier + "<br>");
        //    //HttpContext.Current.Response.Write(data.Price + "<br>");
        //    //HttpContext.Current.Response.Write(data.PromotionTitle + "<br>");
        //    //HttpContext.Current.Response.Write(data.PromotionDetail + "<br>");
        //    //HttpContext.Current.Response.Write(data.Detail + "<br>");
        //    //HttpContext.Current.Response.End();
        //    //return true;

        //    tbl_booking_item RsBooking = dcBooking.tbl_booking_items.SingleOrDefault(bi => bi.booking_item_id == data.BookingItemID);

        //    RsBooking.unit = data.Unit;
        //    RsBooking.price_supplier = data.PriceSupplier;
        //    RsBooking.price = data.Price;
        //    RsBooking.detail = data.Detail;

        //    dcBooking.SubmitChanges();
        //    return true;

        //}

        //========================= tbl_currency_log
        public int InsertCurrencyLog()
        {
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_currency_log (date_start,date_end) VALUES (@date_start,@date_end);SET @currency_log_id=SCOPE_IDENTITY();",cn);
                //cmd.Parameters.Add("", SqlDbType.Int).Value = bytCurrencyId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@currency_log_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();
                ExecuteNonQuery(cmd);
                int ret  = (int)cmd.Parameters["@currency_log_id"].Value;
                return ret;
            }
    
        }

        public int InsertLog(int log_id, byte bytCurrencyId)
        {
           
                using (SqlConnection cn2 = new SqlConnection(this.ConnectionString))
                {
                    Currency cCurrency = new Currency();
                    cCurrency = cCurrency.GetCurrencyById(bytCurrencyId);

                    SqlCommand cmd2 = new SqlCommand("INSERT INTO tbl_currency_temp (currency_log_id,currency_id,title,prefix,code,status)VALUES(@currency_log_id,@currency_id,@title,@prefix,@code,@status)", cn2);
                    cmd2.Parameters.Add("@currency_log_id", SqlDbType.Int).Value = log_id;
                    cmd2.Parameters.Add("@currency_id", SqlDbType.TinyInt).Value = bytCurrencyId;
                    cmd2.Parameters.Add("@title", SqlDbType.VarChar).Value = cCurrency.Title;
                    cmd2.Parameters.Add("@prefix", SqlDbType.Real).Value = cCurrency.Prefix;
                    cmd2.Parameters.Add("@code", SqlDbType.Char).Value = cCurrency.Code;
                    cmd2.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                    cn2.Open();
                    int ret = ExecuteNonQuery(cmd2);
                    return ret;
                }
            
        }

        //========================= tbl_currency_temp

    }
}