using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for PriceBase
/// </summary>
public class PriceBase:Hotels2BaseClass
{
    public int ProductID { get; set; }
    public string ProductCode { get; set; }
    public short SupplierPrice { get; set; }
    public string ProductTitle { get; set; }
    public int ConditionID { get; set; }
    public string ConditionTitle { get; set; }
    public int OptionID { get; set; }
    public string OptionDetail{ get; set; }
    public string OptionTitle { get; set; }
    public byte NumAdult { get; set; }
    public byte NumChild { get; set; }
    public byte NumExtra { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public decimal Rate { get; set; }
    public decimal RateOwn { get; set; }
    public decimal RateRack { get; set; }
    public short OptionCategoryID { get; set; }
    public byte IsBreakfast { get; set; }
    public bool IsRoomShow { get; set; }
    public int PeriodID { get; set; }
    public string OptionPicture { get; set; }
    public byte MarketID { get; set; }
    public string MarketTitle{ get; set; }
    public byte Breakfast { get; set; }
    public byte HasTransfer { get; set; }
    private byte _langID = 1;
    public byte LangID
    {
        set { _langID = value; }
    }

    private string conditionGroup = string.Empty;

    public PriceBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<PriceBase> GetPriceBase(int ProductID, int OptionCateID, int ProductTransferID, DateTime DateStart, DateTime DateEnd)
    {

        string condition = string.Empty;
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            int ProductCheck = ProductID;


            switch (OptionCateID)
            {
                case 1:
                    condition = " and po.cat_id=38";
                    break;
                case 2:
                    condition = " and po.cat_id IN (39,40)";
                    break;
                case 3:
                    ProductCheck = ProductTransferID;
                    condition = " and po.cat_id IN (43,44)";
                    break;
                case 4:
                    //Green fee
                    condition = " and po.cat_id=48";
                    break;
                case 5:
                    //Day trip
                    condition = " and po.cat_id=52";
                    break;
                case 6:
                    //Water
                    condition = " and po.cat_id=53";
                    break;
                case 7:
                    //Show
                    condition = " and po.cat_id=54";
                    break;
                case 8:
                    //Healt
                    condition = " and po.cat_id=55";
                    break;
                case 9:
                    //Spa
                    condition = " and po.cat_id=56";
                    break;
                case 99:
                    condition = "";
                    break;
            }

            //string sqlCommand = "select  p.product_id,p.supplier_price,p.title as product_title,poc.condition_id,poc.title as condition_title,po.option_id,po.title as option_title,poc.num_adult,poc.num_children,poc.num_extra,pp.date_start,pp.date_end,pocp.rate,pocp.rate_own,pocp.rate_rack,po.cat_id";
            //sqlCommand = sqlCommand + " from tbl_product_supplier ps,tbl_product p,tbl_product_period pp,tbl_product_option po,tbl_product_option_condition poc,tbl_product_option_condition_price pocp";
            //sqlCommand = sqlCommand + " where ps.product_id=p.product_id and ps.product_id=" + ProductCheck + " and ps.product_id=pp.product_id and p.supplier_price=pp.supplier_id and poc.option_id=po.option_id and poc.condition_id=pocp.condition_id and pp.period_id = pocp.period_id " + condition + " and ps.status=1";
            //sqlCommand = sqlCommand + " and ((pp.date_start<=" + DateStart.Hotels2DateToSQlString() + " and pp.date_end>=" + DateStart.Hotels2DateToSQlString() + ") or (pp.date_start<=" + DateEnd.Hotels2DateToSQlString() + " and pp.date_end>=" + DateEnd.Hotels2DateToSQlString() + ") or (pp.date_start>=" + DateStart.Hotels2DateToSQlString() + " and pp.date_end<=" + DateEnd.Hotels2DateToSQlString() + "))";
            //sqlCommand = sqlCommand + " and pocp.status=1 and po.status=1";
            //sqlCommand = sqlCommand + " order by po.priority asc,poc.condition_id asc,pp.date_start asc";
            string sqlCommand = "fr_base_price2 '" + ProductID + "','" + OptionCateID + "'," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString()+",'"+_langID+"'";

            if (OptionCateID == 3)
            {
                sqlCommand = "fr_base_price2 '" + ProductCheck + "','" + OptionCateID + "'," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString() + ",'" + _langID + "'";
               
            }

            //HttpContext.Current.Response.Write(sqlCommand+"<br>");
            //HttpContext.Current.Response.End();
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<PriceBase> PriceList = new List<PriceBase>();
            int conditionTmp = 0;
            conditionGroup = "";
            string optionTitle = string.Empty;

            while (reader.Read())
            {

                //if (optionCate != 31)
                //{
                //SupplierID = (short)reader["supplier_price"];
                //}
                if (_langID==1)
                {
                    optionTitle = reader["option_title"].ToString();
                }else{
                    optionTitle = reader["second_lang"].ToString();
                    if(string.IsNullOrEmpty(optionTitle))
                    {
                        optionTitle = reader["option_title"].ToString();
                    }
                }

                PriceList.Add(new PriceBase
                {
                    ProductID = (int)reader["product_id"],
                    ProductCode = reader["product_code"].ToString(),
                    SupplierPrice = (short)reader["supplier_price"],
                    ProductTitle = reader["product_title"].ToString(),
                    ConditionID = (int)reader["condition_id"],
                    ConditionTitle = reader["condition_title"].ToString(),
                    OptionID = (int)reader["option_id"],
                    OptionDetail = reader["option_detail"].ToString(),
                    OptionTitle = optionTitle,
                    NumAdult = (byte)reader["num_adult"],
                    NumChild = (byte)reader["num_children"],
                    NumExtra = (byte)reader["num_extra"],
                    DateStart = (DateTime)reader["date_start"],
                    DateEnd = (DateTime)reader["date_end"],
                    Rate = (decimal)reader["rate"],
                    RateOwn = (decimal)reader["rate_own"],
                    RateRack = (decimal)reader["rate_rack"],
                    OptionCategoryID = (short)reader["cat_id"],
                    IsBreakfast = (byte)reader["breakfast"],
                    IsRoomShow = (bool)reader["isshow"],
                    PeriodID = (int)reader["period_id"],
                    OptionPicture = reader["picture"].ToString(),
                    MarketID = (byte)reader["market_id"],
                    MarketTitle = reader["market_title"].ToString(),
                    Breakfast = (byte)reader["breakfast"],
                    HasTransfer=(byte)reader["has_transfer"]

                });

                if ((int)reader["condition_id"] != conditionTmp)
                {
                    conditionGroup = conditionGroup + ((int)reader["condition_id"]).ToString() + ",";
                    //HttpContext.Current.Response.Write(((int)reader["condition_id"]).ToString() + "<br>");
                }

                conditionTmp = (int)reader["condition_id"];
            }
            return PriceList;
        }
        
    }

    public string GetConditionGroup()
    {
        return this.conditionGroup;
    }
}