using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.DataAccess;
using Hotels2thailand.Front;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CancellationExtranet
/// </summary>
public class CancellationExtranet:Hotels2BaseClass
{

    public int CancelID { get; set; }
    public int ConditionID { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Title { get; set; }
    public byte DayCancel { get; set; }
    public byte ChargePercent { get; set; }
    public byte ChargeNight { get; set; }

    private int productID;
    private DateTime dateCheck;

    public CancellationExtranet()
    {
    }

    public CancellationExtranet(int ProductID, DateTime DateCheck)
	{
        productID = ProductID;
        dateCheck = DateCheck;
	}

    public List<CancellationExtranet> GetCancellation()
    {
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            string strCommand = "select poccl_ex.cancel_id,poccl_ex.condition_id,poccl_ex.date_start,poccl_ex.date_end,poccl_ex.title,pocct_ex.day_cancel,pocct_ex.charge_percent,pocct_ex.charge_night";
            strCommand = strCommand + " from tbl_product_option po,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_cancel_extra_net poccl_ex,tbl_product_option_condition_cancel_content_extra_net pocct_ex";
            strCommand = strCommand + " where po.option_id=poc_ex.option_id and poc_ex.condition_id=poccl_ex.condition_id and poccl_ex.cancel_id=pocct_ex.cancel_id";
            strCommand = strCommand + " and po.product_id=" + productID + " and " + dateCheck.Hotels2DateToSQlString() + " between poccl_ex.date_start and poccl_ex.date_end and poc_ex.status=1 and poccl_ex.status=1 and pocct_ex.status=1";
            strCommand = strCommand + " and poc_ex.condition_name_id NOT IN (1,5)";
            strCommand = strCommand + " order by poccl_ex.condition_id asc,pocct_ex.day_cancel asc";
            SqlCommand cmd = new SqlCommand(strCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<CancellationExtranet> listResult = new List<CancellationExtranet>();

            while (reader.Read())
            {
                listResult.Add(new CancellationExtranet
                {
                    CancelID = (int)reader["cancel_id"],
                    ConditionID = (int)reader["condition_id"],
                    DateStart = (DateTime)reader["date_start"],
                    DateEnd = (DateTime)reader["date_end"],
                    Title = reader["title"].ToString(),
                    DayCancel = (byte)reader["day_cancel"],
                    ChargePercent = (byte)reader["charge_percent"],
                    ChargeNight = (byte)reader["charge_night"]
                });
            }
            return listResult;
        }
        

    }

}