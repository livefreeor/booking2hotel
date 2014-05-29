using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontProductDeposit
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontProductDeposit : Hotels2BaseClass
    {
        private int productID = 0;
        public short Deposit { get; set; }
        public byte DepositCateID { get; set; }

        public FrontProductDeposit(int ProductID)
        {
            productID = ProductID;
            Deposit = 0;
            DepositCateID = 100;

        }

        public void GetDeposit(DateTime dateStart)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = string.Empty;
                sqlCommand = sqlCommand + "select pdp.deposit,deposit_cat_id";
                sqlCommand = sqlCommand + " from tbl_product_booking_engine pben,tbl_product_deposit pdp";
                sqlCommand = sqlCommand + " where pben.product_id=pdp.product_id and pben.product_id=" + productID;
                sqlCommand = sqlCommand + " and "+dateStart.Hotels2DateToSQlString()+" between pdp.date_start and pdp.date_end and pdp.status=1";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Deposit = (short)reader["deposit"];
                    DepositCateID = (byte)reader["deposit_cat_id"];
                }
            }

        }
        public decimal GetPriceDeposit(decimal priceTotal, int quantity, int numNight) {

            decimal priceDeposit = priceTotal;
            if (this.Deposit != 0)
            {
                switch (this.DepositCateID)
                {
                    case 1:
                        priceDeposit = (decimal)(((double)priceTotal) * this.Deposit / 100);
                        priceDeposit = (priceDeposit);
                        break;
                    case 2:
                        priceDeposit = (priceTotal / quantity / numNight) * this.Deposit;
                        //if (priceDeposit > priceTotal)
                        //{
                        //    priceDeposit = priceTotal;
                        //}
                        priceDeposit = priceDeposit * quantity;
                        break;

                    case 3:
                        priceDeposit = this.Deposit * quantity;
                        
                        break;
                }
            }
            return priceDeposit;
        }
        public decimal GetPriceDeposit(int BookingID)
        {
            
            decimal paymentTotal = 0;
            decimal priceDeposit=0;
            decimal paymentTotalNightDeposit = 0;

                string sqlPayment = "select SUM(price) from tbl_booking_item where booking_id=" + BookingID;
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sqlPayment, cn);
                    cn.Open();
                    paymentTotal = (decimal)cmd.ExecuteScalar();
                }

                if (this.Deposit!=0)
                {
                    switch (this.DepositCateID)
                    {
                        case 1:
                            //percent
                            paymentTotal = (paymentTotal * this.Deposit) / 100;
                            break;
                        case 2:
                            //night
                            paymentTotalNightDeposit=0;
                            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
                            {
                                string sqlDeposit="select bi.price,";
                                sqlDeposit=sqlDeposit+" (price/unit/(select DATEDIFF(day,sbp.date_time_check_in,sbp.date_time_check_out) from tbl_booking_product sbp where sbp.booking_product_id=bi.booking_product_id)) as average_price";
                                sqlDeposit=sqlDeposit+" from tbl_booking_item bi where bi.booking_id="+BookingID;
                                SqlCommand cmd=new SqlCommand(sqlDeposit,cn);
                                cn.Open();
                                SqlDataReader reader=cmd.ExecuteReader();
                                while(reader.Read())
                                {
                                    priceDeposit = (decimal)reader["average_price"] * this.Deposit;

                                    if ((decimal)reader["price"] >= priceDeposit)
                                    {
                                        paymentTotalNightDeposit = paymentTotalNightDeposit + priceDeposit;
                                    }else{
                                        paymentTotalNightDeposit=paymentTotalNightDeposit+(decimal)reader["price"];
                                    }
                                }

                            }
                            paymentTotal=paymentTotalNightDeposit;
                            break;

                        case 3:
                            //fix
                            paymentTotalNightDeposit=0;
                            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
                            {
                                string sqlDeposit="select bi.price,";
                                sqlDeposit=sqlDeposit+" (price/unit/(select DATEDIFF(day,sbp.date_time_check_in,sbp.date_time_check_out) from tbl_booking_product sbp where sbp.booking_product_id=bi.booking_product_id)) as average_price";
                                sqlDeposit=sqlDeposit+" from tbl_booking_item bi where bi.booking_id="+BookingID;
                                SqlCommand cmd=new SqlCommand(sqlDeposit,cn);
                                cn.Open();
                                SqlDataReader reader=cmd.ExecuteReader();
                                while(reader.Read())
                                {
                                    priceDeposit = this.Deposit;

                                    if ((decimal)reader["price"] >= priceDeposit)
                                    {
                                        paymentTotalNightDeposit = paymentTotalNightDeposit + priceDeposit;
                                    }else{
                                        paymentTotalNightDeposit=paymentTotalNightDeposit+(decimal)reader["price"];
                                    }
                                }

                            }
                            paymentTotal=paymentTotalNightDeposit;
                            break;
                    }
                }
                return paymentTotal;
        }
    }
}