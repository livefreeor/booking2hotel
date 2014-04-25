using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontOptionPackage
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontOptionPackage : Hotels2BaseClass
    {
        public int OptionID { get; set; }
        public int ConditionID { get; set; }
        public string ConditionTitle { get; set; }
        public string OptionTitle { get; set; }
        public string OptionDetail { get; set; }
        public string OptionImage { get; set; }
        public string PolicyDisplay { get; set; }
        public string PolicyContent { get; set; }
        public decimal Price { get; set; }
        public byte NumAdult { get; set; }
        public byte NumChild { get; set; }
        public byte Breakfast { get; set; }
        public bool IsAdult { get; set; }

        public short SupplierPrice { get; set; }

        private int _productID;
        private DateTime _dateStart;
        private DateTime _dateEnd;

        public FrontOptionPackage()
        {

        }

        public FrontOptionPackage(int ProductID, DateTime DateStart, DateTime DateEnd)
        {
            _productID = ProductID;
            _dateStart = DateStart;
            _dateEnd = DateEnd;
        }

        public List<FrontOptionPackage> GetPackageList()
        {
            List<FrontOptionPackage> objResultList = new List<FrontOptionPackage>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlDateStart = _dateStart.Hotels2DateToSQlString();
                string sqlDateEnd = _dateEnd.Hotels2DateToSQlString();

                string strCommand = string.Empty;
                strCommand = strCommand + "select po.option_id,po.title,poct.detail,poc_ex.condition_id,poc_ex.title as condition_title,poc_ex.num_adult,poc_ex.num_children,poc_ex.breakfast,pocp_exp.price,pocp_exp.sub_day_sun,pocp_exp.sub_day_mon,pocp_exp.sub_day_tue,pocp_exp.sub_day_wed,pocp_exp.sub_day_thu,pocp_exp.sub_day_fri,pocp_exp.sub_day_sat,pocp_exp.supplement,poc_ex.is_adult,";
                strCommand = strCommand + " Convert(money,ISNULL((select top 1 splong.supplement from tbl_product_price_longweekend_extra_net splong where splong.condition_id=poc_ex.condition_id and splong.date_supplement between " + sqlDateStart + " and " + sqlDateEnd + " and splong.status=1),0)) as sub_long_weekend, p.supplier_price";
                strCommand = strCommand + " from tbl_product p,tbl_product_option po,tbl_product_option_content poct,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_price_extranet_period pocp_exp";
                strCommand = strCommand + " where p.product_id=po.product_id and po.option_id=poct.option_id and po.option_id=poc_ex.option_id and poc_ex.condition_id=pocp_exp.condition_id";
                strCommand = strCommand + " and p.product_id=" + _productID;
                strCommand = strCommand + " and pocp_exp.status=1 and poc_ex.status=1 and po.status=1";
                strCommand = strCommand + " and DATEDIFF(day," + sqlDateStart + "," + sqlDateEnd + ")=po.night";
                strCommand = strCommand + " and (Dateadd(hh,14,GETDATE()) between po.booking_period_start and po.booking_period_end)";
                strCommand = strCommand + " and (" + sqlDateStart + " between po.stay_period_start and po.stay_period_end)";
                strCommand = strCommand + " and (" + sqlDateEnd + " between po.stay_period_start and po.stay_period_end)";
                strCommand = strCommand + " and (" + sqlDateStart + " between pocp_exp.date_start and pocp_exp.date_end)";
                strCommand = strCommand + " and (" + sqlDateEnd + " between pocp_exp.date_start and pocp_exp.date_end)";

                //HttpContext.Current.Response.Write(strCommand);
                
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                string[] arrDay = {"sun", "mon", "tue", "wed", "thu", "fri", "sat" };
                decimal priceHoliday = 0;
                int intNight=_dateEnd.Subtract(_dateStart).Days;
                while (reader.Read())
                {
                    priceHoliday = 0;
                    if ((decimal)reader["sub_long_weekend"] > 0)
                    {
                        priceHoliday = (decimal)reader["sub_long_weekend"];
                    }
                    else {
                        //for (int countDay = 0; countDay < arrDay.Count(); countDay++)
                        //{
                            //if ((bool)reader["sub_day_" + arrDay[countDay]])
                            //{
                               // priceHoliday = (decimal)reader["supplement"];
                                //break;
                            //}
                        //}
                        for (int countDay = 0; countDay < intNight;countDay++ )
                        {
                            if ((bool)reader["sub_day_" + arrDay[(int)_dateStart.AddDays(countDay).DayOfWeek]])
                            {
                               priceHoliday = (decimal)reader["supplement"];
                               break;
                            }
                        }
                    }

                    
                    objResultList.Add(new FrontOptionPackage
                    {
                        OptionID = (int)reader["option_id"],
                        ConditionID = (int)reader["condition_id"],
                        ConditionTitle=reader["condition_title"].ToString(),
                        OptionTitle = HttpContext.Current.Server.HtmlEncode(reader["title"].ToString()),
                        OptionDetail = HttpContext.Current.Server.HtmlEncode(reader["detail"].ToString().Replace("'","&apos;").Replace("\n","").Replace("\r","")),
                        OptionImage="",
                        PolicyDisplay = "",
                        PolicyContent="",
                        Price = (decimal)reader["price"] + priceHoliday,
                        NumAdult = (byte)reader["num_adult"],
                        NumChild = (byte)reader["num_children"],
                        Breakfast=(byte)reader["breakfast"],
                        IsAdult=(bool)reader["is_adult"],
                        SupplierPrice = (short)reader["supplier_price"]
                    }
                    );
                }


            }
            return objResultList;
        }
    }
}