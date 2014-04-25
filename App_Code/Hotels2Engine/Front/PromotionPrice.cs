using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for ProductPriceList
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class PromotionPrice:Hotels2BaseClass
    {
        public int PromotionID { get; set; }
        public int ConditionID { get; set; }
        public byte DayMin { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public bool DayMon { get; set; }
        public bool DayTue { get; set; }
        public bool DayWed { get; set; }
        public bool DayThu { get; set; }
        public bool DayFri { get; set; }
        public bool DaySat { get; set; }
        public bool DaySun { get; set; }
        public byte IsWeekendAll { get; set; }
        public byte IsHolidayCharge { get; set; }
        public byte IsBreakfast { get; set; }
        public decimal BreakfastCharge { get; set; }
        public string OptionTitle { get; set; }
        public string ConditionTitle { get; set; }
        public int OptionID { get; set; }
        public byte NumAdult { get; set; }
        public byte MaxSet { get; set; }
        public int TotalSet { get; set; }
        public short OptionCategoryID { get; set; }
        public bool IsCancellation { get; set; }
       // private byte LangID;
        private DateTime DateStart;
        private DateTime DateEnd;
        private int numNight;
        private ProductPrice ProductPriceList;
        private List<PromotionPrice> PromotionPriceList;
        private List<PromotionBenefitDetail> promotionBenefitList;
        private byte CountryID=0;
        private FrontProductPriceExtranet ProductPriceExtranet;

        public PromotionPrice()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public PromotionPrice(ProductPrice productPriceList,DateTime dateStart, DateTime dateEnd)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            numNight = dateEnd.Subtract(dateStart).Days;
            //LangID=1;
            ProductPriceList = productPriceList;
            PromotionPriceList = new List<PromotionPrice>();
            promotionBenefitList = new List<PromotionBenefitDetail>();
        }

        public PromotionPrice(FrontProductPriceExtranet extranetPrice, DateTime dateStart, DateTime dateEnd,byte countryID)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            numNight = dateEnd.Subtract(dateStart).Days;
            //LangID = 1;
            CountryID = countryID;
            ProductPriceExtranet = extranetPrice;
            PromotionPriceList = new List<PromotionPrice>();
            promotionBenefitList = new List<PromotionBenefitDetail>();
        }

        public void LoadPromotionList()
        {
            
            DataConnect myConn = new DataConnect();
            //string sqlCommand = "select pm.promotion_id,pcd.condition_id,pm.day_min,pc.title,pc.detail,pm.day_mon,pm.day_tue,pm.day_wed,pm.day_thu,pm.day_fri,pm.day_sat,pm.day_sun,pm.is_weekend_all,pm.Is_holiday_charge,pm.Is_breakfast,pm.breakfast_charge,pb.day_discount_start,pb.day_discount_num,pb.discount,pb.priority,poc.title as condition_title,po.title as option_title,poc.num_adult,po.option_id,pm.max_set,pb.type_id,po.cat_id as option_cat_id,";
            //sqlCommand = sqlCommand + " (DATEDIFF(day," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString() + ")/pm.day_min) as set_total";
            //sqlCommand = sqlCommand + " from tbl_promotion pm,tbl_promotion_content pc,tbl_promotion_condition pcd,tbl_promotion_benefit pb,tbl_product_option_condition poc,tbl_product_option po,tbl_product p";
            //sqlCommand = sqlCommand + " where pm.promotion_id=pcd.promotion_id and pm.promotion_id=pc.promotion_id  and pm.promotion_id=pb.promotion_id and p.product_id=po.product_id  and p.supplier_price=pm.supplier_id and pcd.condition_id=poc.condition_id and poc.option_id=po.option_id";
            //sqlCommand = sqlCommand + " and pcd.condition_id IN (" + ProductPriceList.ConditionGroup + ")";
            //sqlCommand = sqlCommand + " and pm.status=1 ";
            //sqlCommand = sqlCommand + " and DATEDIFF(day," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString() + ")>=pm.day_min";
            //sqlCommand = sqlCommand + " and DATEDIFF(day,getDate()," + DateStart.Hotels2DateToSQlString() + ")>=pm.day_advance_num";
            //sqlCommand = sqlCommand + " and (Dateadd(hh,14,GETDATE()) between pm.date_start and pm.date_end)";
            //sqlCommand = sqlCommand + " and (" + DateStart.Hotels2DateToSQlString() + " between pm.date_use_start and pm.date_use_end)";
            //sqlCommand = sqlCommand + " and (" + DateEnd.Hotels2DateToSQlString() + " between pm.date_use_start and pm.date_use_end)";
            //sqlCommand = sqlCommand + " and pc.lang_id=1 and pb.status=1 and pcd.status=1";
            //sqlCommand = sqlCommand + " order by pcd.condition_id asc,pm.day_min desc,pb.priority asc";

            string sqlCommand = string.Empty;
            sqlCommand = sqlCommand = "select pm.promotion_id,pmcd.condition_id,pm.day_min,pmc.title,pmc.detail,pm.day_mon,pm.day_tue,pm.day_wed,pm.day_thu,pm.day_fri,pm.day_sat,pm.day_sun,pm.is_weekend_all,pm.Is_holiday_charge,pm.Is_breakfast,pm.breakfast_charge,pb.day_discount_start,pb.day_discount_num,pb.discount,pb.priority,poc.title as condition_title,po.title as option_title,poc.num_adult,po.option_id,pm.max_set,pb.type_id,po.cat_id as option_cat_id, (DATEDIFF(day,'2012-4-2','2012-4-6')/pm.day_min) as set_total ";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_product_option po,tbl_product_option_condition poc,tbl_promotion pm,tbl_promotion_content pmc,tbl_promotion_condition pmcd,tbl_promotion_benefit pb";
            sqlCommand = sqlCommand + " where p.product_id=po.product_id and po.option_id=poc.option_id and p.product_id=pm.product_id and pm.promotion_id=pmc.promotion_id and pm.promotion_id=pb.promotion_id and pmcd.condition_id=poc.condition_id and pmcd.promotion_id=pm.promotion_id";
            sqlCommand = sqlCommand + " and pmcd.condition_id IN (" + ProductPriceList.ConditionGroup + ") ";
            sqlCommand = sqlCommand + " and DATEDIFF(day,Dateadd(hh,14,GETDATE())," + DateStart.Hotels2DateToSQlString() + ")>=pm.day_advance_num";
            sqlCommand = sqlCommand + " and DATEDIFF(day," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString() + ")>=pm.day_min";
            sqlCommand = sqlCommand + " and (" + DateStart.Hotels2DateToSQlString() + " between pm.date_use_start and pm.date_use_end)";
            sqlCommand = sqlCommand + " and (" + DateEnd.Hotels2DateToSQlString() + " between pm.date_use_start and pm.date_use_end)";
            sqlCommand = sqlCommand + " and (Dateadd(hh,14,GETDATE()) between pm.date_start and pm.date_end)";
            sqlCommand = sqlCommand + " and pm.status=1 and pb.status=1";
            sqlCommand = sqlCommand + " order by pm.day_min desc";

            //HttpContext.Current.Response.Write(sqlCommand + "<br>");
           //HttpContext.Current.Response.End();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);
            
            int conditionTmp = 0;
            while (reader.Read())
            {
                if ((int)reader["condition_id"] != conditionTmp)
                {
                    PromotionPriceList.Add(new PromotionPrice
                    {
                        PromotionID = (int)reader["promotion_id"],
                        ConditionID = (int)reader["condition_id"],
                        DayMin = (byte)reader["day_min"],
                        Title = reader["title"].ToString(),
                        Detail = reader["detail"].ToString(),
                        DayMon = (bool)reader["day_mon"],
                        DayTue = (bool)reader["day_tue"],
                        DayWed = (bool)reader["day_wed"],
                        DayThu = (bool)reader["day_thu"],
                        DayFri = (bool)reader["day_fri"],
                        DaySat = (bool)reader["day_sat"],
                        DaySun = (bool)reader["day_sun"],
                        IsWeekendAll = (byte)reader["is_weekend_all"],
                        IsHolidayCharge = (byte)reader["is_holiday_charge"],
                        IsBreakfast = (byte)reader["Is_breakfast"],
                        BreakfastCharge = (decimal)reader["breakfast_charge"],
                        ConditionTitle = reader["condition_title"].ToString(),
                        OptionTitle = reader["option_title"].ToString(),
                        NumAdult = (byte)reader["num_adult"],
                        OptionID = (int)reader["option_id"],
                        MaxSet = (byte)reader["max_set"],
                        TotalSet = (int)reader["set_total"],
                        OptionCategoryID = (short)reader["option_cat_id"],
                        IsCancellation=false
                    });
                    conditionTmp = (int)reader[1];
                }
                promotionBenefitList.Add(new PromotionBenefitDetail { 
                PromotionID=(int)reader["promotion_id"],
                DayDiscountStart=(byte)reader["day_discount_start"],
                DayDiscountNum=(byte)reader["day_discount_num"],
                Discount=(decimal)reader["discount"],
                PromotionType=(byte)reader["type_id"]
                });

            }
            myConn.Close();
        }

        public void LoadExtranetPromotionList()
        {

            DataConnect myConn = new DataConnect();
            string sqlCommand = "select pm.promotion_id,pcd.condition_id,pm.day_min,pc.title,pc.detail,pm.day_mon,pm.day_tue,pm.day_wed,pm.day_thu,pm.day_fri,pm.day_sat,pm.day_sun,pm.is_weekend_all,pm.Is_holiday_charge,pm.Is_breakfast,pm.breakfast_charge,pb.day_discount_start,pb.day_discount_num,pb.discount,pb.priority,poc.title as condition_title,po.title as option_title,poc.num_adult,po.option_id,pm.max_set,pb.type_id,po.cat_id as option_cat_id,";
            sqlCommand = sqlCommand + " (DATEDIFF(day," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString() + ")/pm.day_min) as set_total,pm.iscancellation";
            sqlCommand = sqlCommand + " from tbl_promotion_extra_net pm,tbl_promotion_content_extra_net pc,tbl_promotion_condition_extra_net pcd,tbl_promotion_benefit_extra_net pb,tbl_product_option_condition_extra_net poc,tbl_product_option po";
            sqlCommand = sqlCommand + " where pm.promotion_id=pcd.promotion_id and pm.promotion_id=pc.promotion_id  and pm.promotion_id=pb.promotion_id and pcd.condition_id=poc.condition_id and poc.option_id=po.option_id";
            sqlCommand = sqlCommand + " and pcd.condition_id IN (" + ProductPriceExtranet.ConditionGroup+ ")";
            sqlCommand = sqlCommand + " and (pm.is_use_worldwide=1 or (select COUNT(promotion_id) from  tbl_promotion_country_extra_net spct_ex where spct_ex.promotion_id=pm.promotion_id and spct_ex.country_id="+CountryID+")>0)";
            sqlCommand = sqlCommand + " and pm.status=1 ";
            sqlCommand = sqlCommand + " and pm.day_min<=" + numNight;
            sqlCommand = sqlCommand + " and DATEDIFF(day,Dateadd(hh,7,GETDATE())," + DateStart.Hotels2DateToSQlString() + ")>=pm.day_advance_num";
            sqlCommand = sqlCommand + " and DATEDIFF(day,Dateadd(hh,7,GETDATE())," + DateStart.Hotels2DateToSQlString() + ")<=pm.day_last_minute_num";
            sqlCommand = sqlCommand + " and (Dateadd(hh,7,GETDATE()) between pm.date_start and Dateadd(d,1,pm.date_end))";
            sqlCommand = sqlCommand + " and (select COUNT(promotion_date_use_id)  from tbl_promotion_date_use_extra_net pdu where " + DateStart.Hotels2DateToSQlString() + " between pdu.date_use_start and pdu.date_use_end and pdu.promotion_id=pm.promotion_id)>0";
            sqlCommand = sqlCommand + " and (select COUNT(promotion_date_use_id)  from tbl_promotion_date_use_extra_net pdu where " + DateEnd.Hotels2DateToSQlString() + " between pdu.date_use_start and pdu.date_use_end and pdu.promotion_id=pm.promotion_id)>0";

            sqlCommand = sqlCommand + " and pc.lang_id=1 and pb.status=1 and pcd.status=1";
            sqlCommand = sqlCommand + " order by pcd.condition_id asc,";
            sqlCommand = sqlCommand + " (";
            sqlCommand = sqlCommand + " select (((" + numNight + "-(" + numNight + "%pm.day_min))/CAST(" + numNight + " as money)*spb.score_fac_1)+spb.score_fac_2)*(pm.promotion_score+pm.promotion_score_benefit)";
            sqlCommand = sqlCommand + " from tbl_promotion_benefit_extra_net spb ";
            sqlCommand = sqlCommand + " where spb.promotion_id=pm.promotion_id and spb.status=1";
            sqlCommand = sqlCommand + " )";
            sqlCommand = sqlCommand + " desc,";
            sqlCommand = sqlCommand + " pm.day_advance_num desc,pb.priority asc,pm.day_min desc";
            //HttpContext.Current.Response.Write(sqlCommand + "<br>");
            //HttpContext.Current.Response.End();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);

            int conditionTmp = 0;
            while (reader.Read())
            {
                if ((int)reader["condition_id"] != conditionTmp)
                {
                    PromotionPriceList.Add(new PromotionPrice
                    {
                        PromotionID = (int)reader["promotion_id"],
                        ConditionID = (int)reader["condition_id"],
                        DayMin = (byte)reader["day_min"],
                        Title = reader["title"].ToString(),
                        Detail = reader["detail"].ToString(),
                        DayMon = (bool)reader["day_mon"],
                        DayTue = (bool)reader["day_tue"],
                        DayWed = (bool)reader["day_wed"],
                        DayThu = (bool)reader["day_thu"],
                        DayFri = (bool)reader["day_fri"],
                        DaySat = (bool)reader["day_sat"],
                        DaySun = (bool)reader["day_sun"],
                        IsWeekendAll = (byte)reader["is_weekend_all"],
                        IsHolidayCharge = (byte)reader["is_holiday_charge"],
                        IsBreakfast = (byte)reader["Is_breakfast"],
                        BreakfastCharge = (decimal)reader["breakfast_charge"],
                        ConditionTitle = reader["condition_title"].ToString(),
                        OptionTitle = reader["option_title"].ToString(),
                        NumAdult = (byte)reader["num_adult"],
                        OptionID = (int)reader["option_id"],
                        MaxSet = (byte)reader["max_set"],
                        TotalSet = (int)reader["set_total"],
                        OptionCategoryID = (short)reader["option_cat_id"],
                        IsCancellation = (bool)reader["iscancellation"]
                    });
                    conditionTmp = (int)reader[1];
                }
                promotionBenefitList.Add(new PromotionBenefitDetail
                {
                    PromotionID = (int)reader["promotion_id"],
                    DayDiscountStart = (byte)reader["day_discount_start"],
                    DayDiscountNum = (byte)reader["day_discount_num"],
                    Discount = (decimal)reader["discount"],
                    PromotionType = (byte)reader["type_id"]
                });

            }
            myConn.Close();
        }

        public bool CheckPromotionAcceptOnHoliday(int PromotionID,DateTime DateStart,DateTime DateEnd)
        {
            bool result = true;
            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                string strCommand =string.Empty;
                strCommand=strCommand+ "select pm_ex.title,";
                strCommand = strCommand + "convert(int,(select count(supplement_date_id) from tbl_product_supplement_date_extra_net psd_ex where psd_ex.product_id=pm_ex.product_id and psd_ex.date_supplement >= " + DateStart.Hotels2DateToSQlString() + " and psd_ex.date_supplement <" + DateEnd.Hotels2DateToSQlString() + " and psd_ex.status=1)) as total_holiday";
                strCommand=strCommand+ " from tbl_promotion_extra_net pm_ex";
                strCommand=strCommand+ " where pm_ex.is_holiday_charge=0 and pm_ex.promotion_id="+PromotionID;
              
                SqlCommand cmd=new SqlCommand(strCommand,cn);
                cn.Open();
                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //Not accept holiday
                    if ((int)reader["total_holiday"]>0)
                    {
                        result = false;
                    }

                }
            }
            return result;
        }

        public List<PromotionPrice> GetPromotionPriceList()
        {
            LoadPromotionList();
            return PromotionPriceList;
        }
        public List<PromotionPrice> GetExtraPromotionPriceList()
        {
            LoadExtranetPromotionList();
            return PromotionPriceList;
        }

        public List<PromotionBenefitDetail> GetPromotionBenefitList()
        { 
            if(PromotionPriceList==null)
            {
                LoadPromotionList();
            }
            return promotionBenefitList;
        }

        public string GetPromotionXml()
        {
            string xml = string.Empty;
            xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            xml = xml + "<Policies>\n";

            return xml;
        }
    }
}