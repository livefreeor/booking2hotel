using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for fnCurrency
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class fnCurrency:Hotels2BaseClass
    {
        public byte CurrencyID { get; set; }
        public string CurrencyTitle { get; set; }
        public float CurrencyPrefix { get; set; }
        public string CurrencyCode { get; set; }

        private byte _currency = 25;
        public fnCurrency()
        {
            if (HttpContext.Current == null)
                return;

            if (HttpContext.Current.Session["bhtCurrencyID"] != null)
            {
                _currency = Convert.ToByte(HttpContext.Current.Session["bhtCurrencyID"]);
            }
        }

        public void GetCurrency()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string strCommand = "select top 1 currency_id,title,prefix,code  from tbl_currency where currency_id =" + _currency;
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    this.CurrencyID = (byte)reader["currency_id"];
                    this.CurrencyTitle = reader["title"].ToString();
                    this.CurrencyPrefix = (float)reader["prefix"];
                    this.CurrencyCode = reader["code"].ToString();
                }
            }
            
        }

        public decimal ChangeCurrency(decimal price)
        {
            return (price / Convert.ToDecimal(CurrencyPrefix));
        }
    }
}