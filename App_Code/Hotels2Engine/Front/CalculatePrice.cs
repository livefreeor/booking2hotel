using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// Summary description for CalculatePrice
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class CalculatePrice
    {
        private int _conditionID;
        private DateTime _dateStart;
        private DateTime _dateEnd;
        private decimal _price;
        private decimal _priceOwn;
        private decimal _priceRack;

        public CalculatePrice(int conditionID, DateTime dateStart, DateTime dateEnd)
        {
            _conditionID = conditionID;
            _dateStart = dateStart;
            _dateEnd = dateEnd;
            _price = 0;
            _priceOwn = 0;
            _priceRack = 0;
            PriceProcess();
        }

        public decimal GetPrice{
       
          get{  
              return _price;
            }
           
        }
        public decimal GetPriceRack
        {

            get
            {
                return _priceRack;
            }

        }
        public decimal GetPriceOwn
        {

            get
            {
                return _priceOwn;
            }

        }
        private void PriceProcess()
        {
            int numDay = (_dateEnd.Subtract(_dateStart).Days) - 1;
            for (int dayCount = 0; dayCount < numDay; dayCount++)
            {
                DateTime dateCheck = _dateStart.AddDays(dayCount);
                BasePrice price = new BasePrice(_conditionID, dateCheck);
                _price = _price + price.Price;
                _priceOwn = _priceOwn + price.PriceOwn;
                _priceRack = _priceRack + price.PriceRack;
            }   
        }

        public decimal getPriceDay(int OptionID, short SupplierID, DateTime dateCheck)
        {
            string[] arrDay = { "sun", "mon", "tue", "wed", "thu", "fri", "sat" };

            string sqlCommand = "select supplier_id,option_id,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat ";
            sqlCommand = sqlCommand + " from tbl_product_option_supplement_day posd";
            sqlCommand = sqlCommand + " where option_id=14 and supplier_id=74 and '2010-05-29' between date_start and date_end";

            SVConnect myDb = new SVConnect();

            decimal priceAdd = 0;
            SqlDataReader reader = reader = myDb.GetDataReader(sqlCommand);
            if (reader.Read())
            {
                int dayIndex = (int)dateCheck.DayOfWeek;
                priceAdd = decimal.Parse(reader[arrDay[dayIndex]].ToString());
            }
            return priceAdd;
        }
    }
}