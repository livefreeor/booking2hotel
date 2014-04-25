using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for ProductPriceList
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class PriceSupplement
    {
        public int SupplementID { get; set; }
        public int ConditionID { get; set; }
        public short SupplierID { get; set; }
        public int PeriodID { get; set; }
        public short QuantityMin { get; set; }
        public short QuantityMax { get; set; }
        public decimal Supplement { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        private List<PriceSupplement> PriceSupplementList;

        public PriceSupplement()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<PriceSupplement> GetPriceSupplementList(int ProductID)
        {
            DataConnect objConn = new DataConnect();
            //string sqlCommand = "select psq.supplement_id,psq.condition_id,psq.supplier_id,psq.period_id,psq.quantity_min,psq.quantity_max,psq.supplement,pp.date_start,pp.date_end";
            //sqlCommand = sqlCommand + " from tbl_price_suplement_quantity psq,tbl_product_period pp";
            //sqlCommand = sqlCommand + " where pp.period_id=psq.period_id and psq.condition_id IN (" + ConditionGroup + ")";
            //sqlCommand = sqlCommand + " and pp.date_end>GETDATE()";

            string sqlCommand = "select psq.supplement_id,psq.condition_id,psq.supplier_id,psq.period_id,psq.quantity_min,psq.quantity_max,psq.supplement,pp.date_start,pp.date_end"; 
            sqlCommand = sqlCommand + " from tbl_price_suplement_quantity psq,tbl_product_period pp where pp.period_id=psq.period_id and psq.condition_id IN (";
            sqlCommand = sqlCommand + " select spoc.condition_id";
            sqlCommand = sqlCommand + " from tbl_product_option spo,tbl_product_option_condition spoc,tbl_product_period spp,tbl_product_option_condition_price spocp";
            sqlCommand = sqlCommand + " where spo.option_id=spoc.option_id and spoc.condition_id=spocp.condition_id and spocp.period_id=spp.period_id";
            sqlCommand = sqlCommand + " and spo.product_id="+ProductID+" and spp.date_end>GETDATE()";
            sqlCommand = sqlCommand + " ) and pp.date_end>GETDATE()";

            SqlDataReader reader = objConn.GetDataReader(sqlCommand);
            List<PriceSupplement> PriceSupplementList = new List<PriceSupplement>();

            while(reader.Read())
            {
                PriceSupplementList.Add(new PriceSupplement { 
                SupplementID=(int)reader["supplement_id"],
                ConditionID=(int)reader["condition_id"],
                SupplierID=(short)reader["supplier_id"],
                PeriodID=(int)reader["period_id"],
                QuantityMin=(short)reader["quantity_min"],
                QuantityMax=(short)reader["quantity_max"],
                Supplement = (decimal)reader["supplement"],
                DateStart=(DateTime)reader["date_start"],
                DateEnd=(DateTime)reader["date_end"]
                });
            }
            objConn.Close();
            return PriceSupplementList;

        }

        public void LoadPriceSupplementByProductID(int ProductID)
        {
            PriceSupplementList=GetPriceSupplementList(ProductID);
        }

        public decimal GetPriceSupplement(DateTime dateCheck,decimal price, int conditionID, int countItem)
        {
            decimal priceIncludeVat = price * Convert.ToDecimal(1.177);
            decimal priceResult = priceIncludeVat * countItem;

            if(PriceSupplementList.Count>0)
            {
                
                foreach (PriceSupplement item in PriceSupplementList)
                {
                    if (item.ConditionID == conditionID && (dateCheck.CompareTo(item.DateStart) >= 0 && item.DateEnd.CompareTo(dateCheck) >= 0))
                    {
                        if (countItem >= item.QuantityMin && countItem <= item.QuantityMax)
                        {
                            priceResult = priceResult + (item.Supplement * countItem);
                            break;
                        }
                    }
                }
            }
            return (priceResult / Convert.ToDecimal(1.177));
        }

        public decimal GetPriceQwnSupplement(DateTime dateCheck, decimal price, int conditionID, int countItem)
        {
            decimal priceIncludeVat = price;
            decimal priceResult = priceIncludeVat * countItem;

            if (PriceSupplementList.Count > 0)
            {

                foreach (PriceSupplement item in PriceSupplementList)
                {
                    if (item.ConditionID == conditionID && (dateCheck.CompareTo(item.DateStart) >= 0 && item.DateEnd.CompareTo(dateCheck) >= 0))
                    {
                        if (countItem >= item.QuantityMin && countItem <= item.QuantityMax)
                        {
                            priceResult = priceResult + (item.Supplement * countItem);
                            break;
                        }
                    }
                }
            }
            return priceResult;
        }
    }
}