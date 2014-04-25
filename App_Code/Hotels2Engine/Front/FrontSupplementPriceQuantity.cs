using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
/// <summary>
/// Summary description for FrontSupplementPriceQuantity
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontSupplementPriceQuantity:Hotels2BaseClass
    {
        public int SupplementID { get; set; }
        public int ConditionID { get; set; }
        public short SupplierID { get; set; }
        public int PeriodID { get; set; }
        public short QuantityMin { get; set; }
        public short QuantityMax { get; set; }
        public decimal Supplement { get; set; }

        public FrontSupplementPriceQuantity()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<FrontSupplementPriceQuantity> LoadSupplementPriceByProductID(int ProductID,DateTime dateStart)
    {
        List<FrontSupplementPriceQuantity> results = new List<FrontSupplementPriceQuantity>();

        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            string sqlCommand = "select psq.supplement_id,psq.condition_id,psq.supplier_id,psq.period_id,psq.quantity_min,psq.quantity_max,psq.supplement";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_price_suplement_quantity psq,tbl_product_period pp";
            sqlCommand = sqlCommand + " where psq.period_id=pp.period_id ";
            sqlCommand = sqlCommand + " and p.supplier_price=pp.supplier_id and p.product_id=pp.product_id and p.supplier_price=psq.supplier_id";
            sqlCommand = sqlCommand + " and psq.condition_id IN";
            sqlCommand = sqlCommand + " (";
            sqlCommand = sqlCommand + " select spoc.condition_id ";
            sqlCommand = sqlCommand + " from tbl_product_option spo,tbl_product_option_condition spoc";
            sqlCommand = sqlCommand + " where spo.option_id=spoc.option_id and spo.product_id=" + ProductID;
            sqlCommand = sqlCommand + " )";
            sqlCommand = sqlCommand + " and " + dateStart.Hotels2DateToSQlString() + " between pp.date_start and pp.date_end";

            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                results.Add(new FrontSupplementPriceQuantity
                {
                    SupplementID = (int)reader["supplement_id"],
                    ConditionID = (int)reader["condition_id"],
                    SupplierID = (short)reader["supplier_id"],
                    PeriodID = (int)reader["period_id"],
                    QuantityMin = (short)reader["quantity_min"],
                    QuantityMax = (short)reader["quantity_max"],
                    Supplement = (decimal)reader["supplement"]
                });
            }
            return results;
        }
        
    }
    }
}