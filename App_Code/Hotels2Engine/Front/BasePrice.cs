using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// Summary description for BasePrice
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class BasePrice : Hotels2BaseClass
    {
        public decimal Price { get; set; }
        public decimal PriceRack{ get; set; }
        public decimal PriceOwn { get; set; }
        public DateTime DateCheck { get; set; }
        public int ConditionID { get; set; }


        public BasePrice()
        { 
        
        }
        public BasePrice(int conditionID,DateTime dateCheck)
        {
           
            ConditionID = conditionID;
            DateCheck = dateCheck;
            Price = 0;
            PriceOwn = 0;
            PriceRack = 0;
            Calculate();
        }

        

        
        public void Calculate()
        {
            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                string strCommand = "select pop.rate,pop.rate_own,pop.rate_rack";
                strCommand = strCommand + " from tbl_product_option po,tbl_product_option_condition poc,tbl_product_option_condition_price pop,tbl_product_period pp";
                strCommand = strCommand + " where po.option_id=poc.option_id and poc.condition_id=pop.condition_id and pop.period_id=pp.period_id";
                strCommand = strCommand + " and pop.condition_id=" + ConditionID + " and " + DateCheck.Hotels2DateToSQlString() + " Between pp.date_start and pp.date_end";

                SqlCommand cmd = new SqlCommand(strCommand,cn);
                cn.Open();

                SqlDataReader reader = reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    Price = (decimal)reader["rate"];
                    PriceOwn = (decimal)reader["rate_own"];
                    PriceRack = (decimal)reader["rate_rack"];


                }
            }


        }

       
    }
}