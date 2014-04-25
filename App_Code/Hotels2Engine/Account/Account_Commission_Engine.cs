using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using Hotels2thailand.Booking;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for Account_Commission_Engine
/// </summary>
/// 
namespace Hotels2thailand.Account
{
    public class Booking_Cal_Com
    {
        public int BookingItemID { get; set; }
        public int BookingId { get; set; }
        public int BookingProductID { get; set; }
        
        public decimal PriceSales { get; set; }
    }
    public class Account_Commission_Engine:Hotels2BaseClass
    {

        public int BookingId { get; set; }
        
        public decimal ComVal { get; set; }
        public byte? Comcat { get; set; }
        
        public decimal Price { get; set; }
        public decimal PriceCom  { get; set; }
        public decimal PriceSup { get; set; }

        private decimal _price_all_bookin = 1;
        public decimal PriceTotalAllBooking { get { return _price_all_bookin; } set { _price_all_bookin = value; } }
       
        

        public Account_Commission_Engine(int intBookingId, decimal bytComVal, byte? bytComCat)
        {

            this.ComVal = bytComVal;
            this.BookingId = intBookingId;
            this.Comcat = bytComCat;
            
        }

        

        public void LoadBhtManage()
        {
            this.Price = GetPriceFromBookingEngine(this.BookingId);
            this.PriceCom = (this.Price * this.ComVal) / 100;
            this.PriceSup = this.Price - this.PriceCom;
        }

        public void LoadPriceTotalAllBooking_for_CommssionStep(IList<object> iListBooking, int ProductId)
        {
            
            string strBookingId = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                foreach (Com_Booking_list item in iListBooking)
                    strBookingId = strBookingId + item.BookingId  + ",";


                strBookingId = strBookingId.Hotels2RightCrl(1);

                SqlCommand cmd = new SqlCommand("SELECT SUM(price) FROM tbl_booking_item WHERE status = 1 AND booking_id IN (" + strBookingId + ")",cn);
                cn.Open();
                this.PriceTotalAllBooking = (decimal)ExecuteScalar(cmd);

                int intRevID = 0;
                // selec Com Step
                SqlCommand cmd1 = new SqlCommand("SELECT TOP 1 revenue_id FROM tbl_revenue WHERE product_id =@product_id AND cat_id = 3", cn);
                cmd1.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                
                IDataReader reader = ExecuteReader(cmd1, CommandBehavior.SingleRow);
                if (reader.Read())
                    intRevID = (int)reader[0];
                reader.Close();

                decimal decComret = 0;
                if (intRevID > 0)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT ISNULL(MAX(commission) ,0) FROM tbl_revenue_commission WHERE CAST(@revenue AS money) BETWEEN revenue_start AND revenue_end AND revenue_id =@revenue_id ", cn);
                    cmd2.Parameters.Add("@revenue_id", SqlDbType.Int).Value = intRevID;
                    cmd2.Parameters.Add("@revenue", SqlDbType.Money).Value = this.PriceTotalAllBooking;

                    decComret = (decimal)ExecuteScalar(cmd2);
                    if (decComret > 0)
                        this.ComVal = decComret;
                    
                }
                //= iListBooking.Join(',', v => v.GetType().GetProperty("BookingId").GetValue(v, null));
            }



        }

        public void CalculateCommissionHotelManage(IList<object> iListBooking, int ProductId)
        {
            // BookingItemDisplay cBookingItem = new BookingItemDisplay();
            //cBookingItem.getBookingItemListByBookingProductId
            decimal SumPriceSupplier = 0;
            decimal SumPriceSale = 0;
            decimal SumPriceCom = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT booking_item_id,booking_id,booking_product_id,price FROM tbl_booking_item WHERE booking_id = @booking_id", cn);
                cmd.Parameters.AddWithValue("@booking_id", this.BookingId);
                IList<Booking_Cal_Com> ilistitem = new List<Booking_Cal_Com>();
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                    ilistitem.Add(new Booking_Cal_Com
                    {
                        BookingItemID = (int)reader[0],
                        BookingId = (int)reader[1],
                        BookingProductID = (int)reader[2],
                        PriceSales = (decimal)reader[3]
                    });

                reader.Close();

                decimal PricCom = 0;
                decimal PriceSupplier = 0;
                //decimal factorMultiple = 100;

                

                foreach (Booking_Cal_Com item in ilistitem)
                {
                    if (this.Comcat.HasValue)
                    {
                        if ((byte)this.Comcat == 2) // case Commission Monthly
                        {
                            this.ComVal = 3;
                        }
                        if ((byte)this.Comcat == 3) // case Commission Step
                        {
                            LoadPriceTotalAllBooking_for_CommssionStep(iListBooking, ProductId);
                        }
                        

                    }

                    PricCom = (item.PriceSales * this.ComVal) / 100;
                    PriceSupplier = item.PriceSales - PricCom;

                    SqlCommand cmdupdate = new SqlCommand("UPDATE tbl_booking_item SET price_supplier = @price_supplier WHERE booking_item_id = @booking_item_id", cn);
                    cmdupdate.Parameters.AddWithValue("@booking_item_id", item.BookingItemID);
                    cmdupdate.Parameters.AddWithValue("@price_supplier", PriceSupplier);


                    if (ExecuteNonQuery(cmdupdate) > 0)
                    {
                        SumPriceSupplier = SumPriceSupplier + PriceSupplier;
                        SumPriceSale = SumPriceSale + item.PriceSales;
                        SumPriceCom = SumPriceCom + PricCom;

                    }
                }

            }

            this.Price = SumPriceSale;
            this.PriceCom = SumPriceCom;
            this.PriceSup = SumPriceSupplier;
        }

        public void CalculateCommissionBhtManage()
        {
           // BookingItemDisplay cBookingItem = new BookingItemDisplay();
            //cBookingItem.getBookingItemListByBookingProductId
            decimal SumPriceSupplier = 0;
            decimal SumPriceSale = 0;
            decimal SumPriceCom = 0;
            decimal PriceDeposit = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmdCheckDep = new SqlCommand("SELECT ISNULL(price_deposit,0) FROM tbl_booking WHERE booking_id = @booking_id ",cn);
                cmdCheckDep.Parameters.Add("@booking_id", SqlDbType.Int).Value = this.BookingId;
                cn.Open();
                PriceDeposit = (decimal)ExecuteScalar(cmdCheckDep);


                SqlCommand cmd = new SqlCommand("SELECT booking_item_id,booking_id,booking_product_id,price FROM tbl_booking_item WHERE booking_id = @booking_id",cn);
                cmd.Parameters.AddWithValue("@booking_id", this.BookingId);
                IList<Booking_Cal_Com> ilistitem = new List<Booking_Cal_Com>();
                
                 
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                    ilistitem.Add(new Booking_Cal_Com {
                        BookingItemID = (int)reader[0],
                        BookingId = (int)reader[1],
                        BookingProductID = (int)reader[2],
                        PriceSales = (decimal)reader[3]
                    });

                reader.Close();

                decimal PricCom = 0;
                decimal PriceSupplier = 0;
                //decimal factorMultiple = 100;


                if (this.Comcat.HasValue)
                {
                    if ((byte)this.Comcat == 2)
                    {
                        this.ComVal = 3;
                    }
                }

                foreach (Booking_Cal_Com item in ilistitem)
                {
                    

                    PricCom = (item.PriceSales * this.ComVal) / 100;
                    PriceSupplier = item.PriceSales - PricCom;

                    SqlCommand cmdupdate = new SqlCommand("UPDATE tbl_booking_item SET price_supplier = @price_supplier WHERE booking_item_id = @booking_item_id", cn);
                    cmdupdate.Parameters.AddWithValue("@booking_item_id", item.BookingItemID);
                    cmdupdate.Parameters.AddWithValue("@price_supplier", PriceSupplier);


                    if (ExecuteNonQuery(cmdupdate) > 0)
                    {
                        SumPriceSupplier = SumPriceSupplier + PriceSupplier;
                        SumPriceSale = SumPriceSale + item.PriceSales;
                        SumPriceCom = SumPriceCom + PricCom;

                    }
                }

            }

            if (PriceDeposit > 0)
            {
                this.Price = PriceDeposit;
                this.PriceCom = (PriceDeposit * this.ComVal) / 100;
                this.PriceSup = PriceDeposit - this.PriceCom;
            }
            else
            {
                this.Price = SumPriceSale;
                this.PriceCom = SumPriceCom;
                this.PriceSup = SumPriceSupplier;
            }

            
        }

        private decimal GetPriceFromBookingEngine(int intBookingId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SUM(price)  FROM tbl_booking_item WHERE booking_id = @booking_id AND status = 1", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();

                return (decimal)ExecuteScalar(cmd);
            }
        }

    }
}