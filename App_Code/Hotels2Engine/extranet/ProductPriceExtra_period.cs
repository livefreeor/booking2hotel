using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductPriceExtra_period : Hotels2BaseClass
    {
        public int Price_Period_Id { get; set; }
        public int ConditionId { get; set; }
        public decimal Price { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Status { get; set; }

        public bool IsSub_Sun { get; set; }
        public bool IsSub_Mon { get; set; }
        public bool IsSub_Tue { get; set; }
        public bool IsSub_Wed { get; set; }
        public bool IsSub_Thu { get; set; }
        public bool IsSub_Fri { get; set; }
        public bool IsSub_Sat { get; set; }
        public decimal Supplement { get; set; }
                
        public ProductPriceExtra_period getPricePackageByConditionId(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1  price_period_id,condition_id,price,date_start,date_end,status FROM tbl_product_option_condition_price_extranet_period WHERE condition_id=@condition_id AND status = 1",cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    return (ProductPriceExtra_period)MappingObjectFromDataReader(reader);
                else
                    return null;
            }

        }

        public IList<object> getPricePackageListByConditionId(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet_period WHERE condition_id=@condition_id AND status = 1", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                    
            }

        }


        public bool UpdatePriceExtra_periodById(int periodId)
        {
            int ret  = 0;
            //disable all price period by process 
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_price_extranet_period SET status = 0 WHERE price_period_id= @price_period_id", cn);
                cn.Open();
                cmd.Parameters.Add("@price_period_id", SqlDbType.Int).Value = periodId;
                ret = ExecuteNonQuery(cmd);
            }

            return (ret == 1);
        }

        public string UPdatePriceExtra_period_package(int intProductId, short shrSupplierId, int intConditionId, short shrStaffId)
        {
            string result = "False";
            ProductPriceExtra_period_longweek_end clongWeekend = new ProductPriceExtra_period_longweek_end();
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();

                SqlCommand cmdSelect = new SqlCommand("SELECT price_period_id,condition_id,date_start,date_end FROM tbl_product_option_condition_price_extranet_period WHERE condition_id=@condition_id", cn);
                cmdSelect.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                IDataReader readerSelect = ExecuteReader(cmdSelect);
                while (readerSelect.Read())
                {
                    //disable all price period by process 
                    using (SqlConnection cnupdate = new SqlConnection(this.ConnectionString))
                    {
                        SqlCommand cmddisableperiod = new SqlCommand("UPDATE tbl_product_option_condition_price_extranet_period SET status = 0 WHERE price_period_id= " + readerSelect[0] + "", cnupdate);
                        cnupdate.Open();
                        ExecuteNonQuery(cmddisableperiod);
                    }

                    //disable all longWeekend by Process
                    using (SqlConnection cnweek = new SqlConnection(this.ConnectionString))
                    {
                        SqlCommand cmdweek = new SqlCommand("UPDATE tbl_product_price_longweekend_extra_net SET status = 0 WHERE condition_id = @condition_id AND date_supplement BETWEEN '" + ((DateTime)readerSelect["date_start"]).ToString("yyyy-MM-dd") + "' AND '" + ((DateTime)readerSelect["date_end"]).ToString("yyyy-MM-dd") + "' ", cnweek);
                        cnweek.Open();
                        cmdweek.Parameters.Add("@condition_id", SqlDbType.Int).Value = (int)readerSelect["condition_id"];
                        ExecuteNonQuery(cmdweek);
                    }
                    
                    
                }
                readerSelect.Close();

                ProductsupplementExtranet cholidaySupplement = new ProductsupplementExtranet();
                

                //HttpContext.Current.Response.Write(HttpContext.Current.Request.Form["rate_result_checked"]);
                //HttpContext.Current.Response.End();
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["rate_result_checked"]))
                {
                    foreach (string RateItem in HttpContext.Current.Request.Form["rate_result_checked"].Split(','))
                    {

                        DateTime dDateStart_rate = HttpContext.Current.Request.Form["hd_rate_date_form_" + RateItem].Hotels2DateSplitYear("-");
                        DateTime dDateEnd_rate = HttpContext.Current.Request.Form["hd_rate_date_To" + RateItem].Hotels2DateSplitYear("-");



                        int DateDiff = dDateEnd_rate.Subtract(dDateStart_rate).Days;
                        DateTime dDateCurrent = DateTime.Now;

                        decimal Price = decimal.Parse(HttpContext.Current.Request.Form["hd_amount" + RateItem]);
                        //byte bytAllotment = byte.Parse(Request.Form["hd_allot" + RateItem]);
                        //byte bytCutoff = byte.Parse(Request.Form["hd_cutoff" + RateItem]);


                        string DateSurCharge = HttpContext.Current.Request.Form["hd_day_checked_sur" + RateItem];

                        decimal SurAmount = 0;
                        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["hd_surAmount" + RateItem]))
                            SurAmount = decimal.Parse(HttpContext.Current.Request.Form["hd_surAmount" + RateItem]);


                        string[] DaySurcharge = DateSurCharge.Hotels2RightCrl(1).Split(',');


                        bool IsSun = false; bool IsMon = false; bool IsTue = false; bool IsWed = false; bool IsThu = false; bool IsFri = false; bool IsSat = false;
                        if (DaySurcharge.Count() > 0)
                        {
                            foreach (string day in DaySurcharge)
                            {

                                switch (day)
                                {
                                    case "0":
                                        IsSun = true;
                                        break;
                                    case "1":
                                        IsMon = true;
                                        break;
                                    case "2":
                                        IsTue = true;
                                        break;
                                    case "3":
                                        IsWed = true;
                                        break;
                                    case "4":
                                        IsThu = true;
                                        break;
                                    case "5":
                                        IsFri = true;
                                        break;
                                    case "6":
                                        IsSat = true;
                                        break;

                                }


                            }
                        }


                        string DateHolidaySurCharge = HttpContext.Current.Request.Form["hd_holiday_Sur" + RateItem];

                        if (!string.IsNullOrEmpty(DateHolidaySurCharge))
                        {
                            string[] arrHolidaySurcharge = DateHolidaySurCharge.Hotels2RightCrl(1).Split('#');
                            // Response.Write(arrHolidaySurcharge[0]);
                            if (arrHolidaySurcharge.Count() > 0)
                            {
                                foreach (string holidayVal in arrHolidaySurcharge)
                                {
                                    string[] arrHolidayValue = holidayVal.Split(';');
                                    DateTime DAteHoliday = arrHolidayValue[0].Hotels2DateSplitYear("-");
                                    decimal HolidayChargeAmount = 0;
                                    if (!string.IsNullOrEmpty(arrHolidayValue[1]))
                                        HolidayChargeAmount = decimal.Parse(arrHolidayValue[1]);

                                    if (HolidayChargeAmount > 0)
                                    {
                                        cholidaySupplement = cholidaySupplement.GetSupplementDateByDate(intProductId, shrSupplierId, DAteHoliday, true);
                                        clongWeekend.InsertLongWeekend_supplement(intProductId, intConditionId, cholidaySupplement.DateTitle, DAteHoliday, HolidayChargeAmount);
                                    }
                                }
                            }
                        }

                        int PriceId = 0;
                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet_period (condition_id,price,date_start,date_end,sub_day_sun,sub_day_mon,sub_day_tue,sub_day_wed,sub_day_thu,sub_day_fri,sub_day_sat,supplement) VALUES (@condition_id,@price,@date_start,@date_end,@sub_day_sun,@sub_day_mon,@sub_day_tue,@sub_day_wed,@sub_day_thu,@sub_day_fri,@sub_day_sat,@supplement); SET @price_period_id=SCOPE_IDENTITY();", cn);
                        cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                        cmd.Parameters.Add("@price", SqlDbType.Money).Value = Price;
                        cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart_rate;
                        cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd_rate;


                        cmd.Parameters.Add("@sub_day_sun", SqlDbType.Bit).Value = IsSun;
                        cmd.Parameters.Add("@sub_day_mon", SqlDbType.Bit).Value = IsMon;
                        cmd.Parameters.Add("@sub_day_tue", SqlDbType.Bit).Value = IsTue;
                        cmd.Parameters.Add("@sub_day_wed", SqlDbType.Bit).Value = IsWed;
                        cmd.Parameters.Add("@sub_day_thu", SqlDbType.Bit).Value = IsThu;
                        cmd.Parameters.Add("@sub_day_fri", SqlDbType.Bit).Value = IsFri;
                        cmd.Parameters.Add("@sub_day_sat", SqlDbType.Bit).Value = IsSat;


                        cmd.Parameters.Add("@supplement", SqlDbType.Money).Value = SurAmount;

                        cmd.Parameters.Add("@price_period_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                        ExecuteNonQuery(cmd);

                        PriceId = (int)cmd.Parameters["@price_period_id"].Value;


                        Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                        cStaffPriceAc.InsertActivityRate_Period(intProductId, shrSupplierId, PriceId, intConditionId, Price, dDateStart_rate, dDateEnd_rate, IsSun, IsMon, IsTue, IsWed, IsThu, IsFri, IsSat, SurAmount, shrStaffId, StaffLogActionType.Insert, cn);

                    }
                }
                

                result = "True";
            }

            return result;
        }

        public int InsertPriceExtra_period(int intProductId, short shrSupplierId, int intConditionId, decimal decPrice, short shrStaffId, DateTime dDateStart, DateTime dDateEnd, bool bolIsSun, bool bolIsMon, bool bolIsTue, bool bolIsWed, bool bolIsthu, bool bolIsFri, bool bolIssat, decimal decSurcharge)
        {
            int ret = 0;
            int PriceId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet_period (condition_id,price,date_start,date_end,sub_day_sun,sub_day_mon,sub_day_tue,sub_day_wed,sub_day_thu,sub_day_fri,sub_day_sat,supplement) VALUES (@condition_id,@price,@date_start,@date_end,@sub_day_sun,@sub_day_mon,@sub_day_tue,@sub_day_wed,@sub_day_thu,@sub_day_fri,@sub_day_sat,@date_activity,@staff_id); SET @price_period_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = decPrice;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;


                cmd.Parameters.Add("@sub_day_sun", SqlDbType.Bit).Value = bolIsSun;
                cmd.Parameters.Add("@sub_day_mon", SqlDbType.Bit).Value = bolIsMon;
                cmd.Parameters.Add("@sub_day_tue", SqlDbType.Bit).Value = bolIsTue;
                cmd.Parameters.Add("@sub_day_wed", SqlDbType.Bit).Value = bolIsWed;
                cmd.Parameters.Add("@sub_day_thu", SqlDbType.Bit).Value = bolIsthu;
                cmd.Parameters.Add("@sub_day_fri", SqlDbType.Bit).Value = bolIsFri;
                cmd.Parameters.Add("@sub_day_sat", SqlDbType.Bit).Value = bolIssat;


                cmd.Parameters.Add("@supplement", SqlDbType.Money).Value = decSurcharge;

                cmd.Parameters.Add("@price_period_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                PriceId = (int)cmd.Parameters["@price_period_id"].Value;

                //Case Period Use Date start  fordate Period ONLY!!
                Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                cStaffPriceAc.InsertActivityRate_Period(intProductId, shrSupplierId, PriceId, intConditionId, decPrice, dDateStart, dDateEnd, bolIsSun, bolIsMon, bolIsTue, bolIsWed, bolIsthu, bolIsFri, bolIssat, decSurcharge,shrStaffId, StaffLogActionType.Insert, cn);

            }


            return PriceId;
        }



        public string InsertPriceExtra_Period_loadNewPackage(int intProductId, short shrSupplierId, int intConditionId, short shrStaffId)
        {
            string result = "False";
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();

                ProductsupplementExtranet cholidaySupplement = new ProductsupplementExtranet();
                ProductPriceExtra_period_longweek_end clongWeekend = new ProductPriceExtra_period_longweek_end();
                foreach (string RateItem in HttpContext.Current.Request.Form["rate_result_checked"].Split(','))
                {

                    DateTime dDateStart_rate = HttpContext.Current.Request.Form["hd_rate_date_form_" + RateItem].Hotels2DateSplitYear("-");
                    DateTime dDateEnd_rate = HttpContext.Current.Request.Form["hd_rate_date_To" + RateItem].Hotels2DateSplitYear("-");



                    int DateDiff = dDateEnd_rate.Subtract(dDateStart_rate).Days;
                    DateTime dDateCurrent = DateTime.Now;

                    decimal Price = decimal.Parse(HttpContext.Current.Request.Form["hd_amount" + RateItem]);
                    //byte bytAllotment = byte.Parse(Request.Form["hd_allot" + RateItem]);
                    //byte bytCutoff = byte.Parse(Request.Form["hd_cutoff" + RateItem]);


                    string DateSurCharge = HttpContext.Current.Request.Form["hd_day_checked_sur" + RateItem];

                    decimal SurAmount = 0;
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["hd_surAmount" + RateItem]))
                        SurAmount = decimal.Parse(HttpContext.Current.Request.Form["hd_surAmount" + RateItem]);


                    string[] DaySurcharge = DateSurCharge.Hotels2RightCrl(1).Split(',');


                    bool IsSun = false; bool IsMon = false; bool IsTue = false; bool IsWed = false; bool IsThu = false; bool IsFri = false; bool IsSat = false;
                    if (DaySurcharge.Count() > 0)
                    {
                        foreach (string day in DaySurcharge)
                        {

                            switch (day)
                            {
                                case "0":
                                    IsSun = true;
                                    break;
                                case "1":
                                    IsMon = true;
                                    break;
                                case "2":
                                    IsTue = true;
                                    break;
                                case "3":
                                    IsWed = true;
                                    break;
                                case "4":
                                    IsThu = true;
                                    break;
                                case "5":
                                    IsFri = true;
                                    break;
                                case "6":
                                    IsSat = true;
                                    break;

                            }


                        }
                    }


                    string DateHolidaySurCharge = HttpContext.Current.Request.Form["hd_holiday_Sur" + RateItem];
                    if (!string.IsNullOrEmpty(DateHolidaySurCharge))
                    {
                        string[] arrHolidaySurcharge = DateHolidaySurCharge.Hotels2RightCrl(1).Split('#');
                        // Response.Write(arrHolidaySurcharge[0]);
                        if (arrHolidaySurcharge.Count() > 0)
                        {
                            foreach (string holidayVal in arrHolidaySurcharge)
                            {
                                string[] arrHolidayValue = holidayVal.Split(';');
                                DateTime DAteHoliday = arrHolidayValue[0].Hotels2DateSplitYear("-");
                                decimal HolidayChargeAmount = 0;
                                if (!string.IsNullOrEmpty(arrHolidayValue[1]))
                                    HolidayChargeAmount = decimal.Parse(arrHolidayValue[1]);

                                if (HolidayChargeAmount > 0)
                                {
                                    cholidaySupplement = cholidaySupplement.GetSupplementDateByDate(intProductId, shrSupplierId, DAteHoliday, true);
                                    clongWeekend.InsertLongWeekend_supplement(intProductId, intConditionId,cholidaySupplement.DateTitle, DAteHoliday, HolidayChargeAmount);
                                }
                            }
                        }
                    }

                    int PriceId = 0;
                    SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet_period (condition_id,price,date_start,date_end,sub_day_sun,sub_day_mon,sub_day_tue,sub_day_wed,sub_day_thu,sub_day_fri,sub_day_sat,supplement) VALUES (@condition_id,@price,@date_start,@date_end,@sub_day_sun,@sub_day_mon,@sub_day_tue,@sub_day_wed,@sub_day_thu,@sub_day_fri,@sub_day_sat,@supplement); SET @price_period_id=SCOPE_IDENTITY();", cn);
                    cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                    cmd.Parameters.Add("@price", SqlDbType.Money).Value = Price;
                    cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart_rate;
                    cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd_rate;


                    cmd.Parameters.Add("@sub_day_sun", SqlDbType.Bit).Value = IsSun;
                    cmd.Parameters.Add("@sub_day_mon", SqlDbType.Bit).Value = IsMon;
                    cmd.Parameters.Add("@sub_day_tue", SqlDbType.Bit).Value = IsTue;
                    cmd.Parameters.Add("@sub_day_wed", SqlDbType.Bit).Value = IsWed;
                    cmd.Parameters.Add("@sub_day_thu", SqlDbType.Bit).Value = IsThu;
                    cmd.Parameters.Add("@sub_day_fri", SqlDbType.Bit).Value = IsFri;
                    cmd.Parameters.Add("@sub_day_sat", SqlDbType.Bit).Value = IsSat;


                    cmd.Parameters.Add("@supplement", SqlDbType.Money).Value = SurAmount;

                    cmd.Parameters.Add("@price_period_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    ExecuteNonQuery(cmd);
                    
                    PriceId = (int)cmd.Parameters["@price_period_id"].Value;


                    Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                    cStaffPriceAc.InsertActivityRate_Period(intProductId, shrSupplierId, PriceId, intConditionId, Price, dDateStart_rate, dDateEnd_rate, IsSun, IsMon, IsTue, IsWed, IsThu, IsFri, IsSat, SurAmount, shrStaffId, StaffLogActionType.Insert, cn);

                }

                result = "True";
            }


            return result;
        }
        public string InsertPriceExtra_Period_loadNewMeal(int intProductId, short shrSupplierId, int intConditionId, short shrStaffId)
        {
            string result = "False";
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();

                ProductsupplementExtranet cholidaySupplement = new ProductsupplementExtranet();
                ProductPriceExtra_period_longweek_end clongWeekend = new ProductPriceExtra_period_longweek_end();
                //foreach (string RateItem in HttpContext.Current.Request.Form["rate_result_checked"].Split(','))
                //{

                    DateTime dDateStart_rate = HttpContext.Current.Request.Form["hd_rate_DateStart"].Hotels2DateSplitYear("-");
                    DateTime dDateEnd_rate = HttpContext.Current.Request.Form["hd_rate_DateEnd"].Hotels2DateSplitYear("-");



                    int DateDiff = dDateEnd_rate.Subtract(dDateStart_rate).Days;
                    DateTime dDateCurrent = DateTime.Now;

                    decimal Price = decimal.Parse(HttpContext.Current.Request.Form["rate_amount"]);
                    //byte bytAllotment = byte.Parse(Request.Form["hd_allot" + RateItem]);
                    //byte bytCutoff = byte.Parse(Request.Form["hd_cutoff" + RateItem]);


                    bool IsSun = false; bool IsMon = false; bool IsTue = false; bool IsWed = false; bool IsThu = false; bool IsFri = false; bool IsSat = false;
                    decimal SurAmount = 0;
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["sur_amount"]))
                        SurAmount = decimal.Parse(HttpContext.Current.Request.Form["sur_amount"]);



                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["dayofWeek"]))
                    {
                        string DateSurCharge = HttpContext.Current.Request.Form["dayofWeek"];

                        string[] DaySurcharge = DateSurCharge.Split(',');


                        
                        if (DaySurcharge.Count() > 0)
                        {
                            foreach (string day in DaySurcharge)
                            {

                                switch (day)
                                {
                                    case "0":
                                        IsSun = true;
                                        break;
                                    case "1":
                                        IsMon = true;
                                        break;
                                    case "2":
                                        IsTue = true;
                                        break;
                                    case "3":
                                        IsWed = true;
                                        break;
                                    case "4":
                                        IsThu = true;
                                        break;
                                    case "5":
                                        IsFri = true;
                                        break;
                                    case "6":
                                        IsSat = true;
                                        break;

                                }


                            }
                        }
                    }
                    

                    ProductsupplementExtranet cSupExtra = new ProductsupplementExtranet();
                    IList<object> iListHoliday = cSupExtra.GetSupplementDateByDateRange(intProductId, shrSupplierId, dDateStart_rate, dDateEnd_rate, true);
                    foreach (ProductsupplementExtranet holiday in iListHoliday)
                    {
                        
                        DateTime DAteHoliday = HttpContext.Current.Request.Form["hd_date_supplement_" + holiday.SupplementID].Hotels2DateSplitYear("-");
                        decimal HolidayChargeAmount = 0;
                        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["supplement_amount_" + holiday.SupplementID]))
                            HolidayChargeAmount = decimal.Parse(HttpContext.Current.Request.Form["supplement_amount_" + holiday.SupplementID]);

                        if (HolidayChargeAmount > 0)
                        {
                            if (clongWeekend.GetCOuntLongWeekEndList_ByConditionIdAndDAteHoliday(intConditionId, DAteHoliday) == 0)
                            {
                                cholidaySupplement = cholidaySupplement.GetSupplementDateByDate(intProductId, shrSupplierId, DAteHoliday, true);
                                clongWeekend.InsertLongWeekend_supplement(intProductId, intConditionId, cholidaySupplement.DateTitle, DAteHoliday, HolidayChargeAmount);
                            }
                        }
                    }
                    

                    int PriceId = 0;


                    SqlCommand cmdck = new SqlCommand("SELECT COUNT(*) FROM tbl_product_option_condition_price_extranet_period WHERE condition_id=@condition_id AND date_start=@date_start AND date_end=@date_end AND status = 1 ", cn);
                    cmdck.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                   
                    cmdck.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart_rate;
                    cmdck.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd_rate;
                    int CountIsRec = (int)ExecuteScalar(cmdck);

                    if (CountIsRec == 0)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet_period (condition_id,price,date_start,date_end,sub_day_sun,sub_day_mon,sub_day_tue,sub_day_wed,sub_day_thu,sub_day_fri,sub_day_sat,supplement) VALUES (@condition_id,@price,@date_start,@date_end,@sub_day_sun,@sub_day_mon,@sub_day_tue,@sub_day_wed,@sub_day_thu,@sub_day_fri,@sub_day_sat,@supplement); SET @price_period_id=SCOPE_IDENTITY();", cn);
                        cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                        cmd.Parameters.Add("@price", SqlDbType.Money).Value = Price;
                        cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart_rate;
                        cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd_rate;


                        cmd.Parameters.Add("@sub_day_sun", SqlDbType.Bit).Value = IsSun;
                        cmd.Parameters.Add("@sub_day_mon", SqlDbType.Bit).Value = IsMon;
                        cmd.Parameters.Add("@sub_day_tue", SqlDbType.Bit).Value = IsTue;
                        cmd.Parameters.Add("@sub_day_wed", SqlDbType.Bit).Value = IsWed;
                        cmd.Parameters.Add("@sub_day_thu", SqlDbType.Bit).Value = IsThu;
                        cmd.Parameters.Add("@sub_day_fri", SqlDbType.Bit).Value = IsFri;
                        cmd.Parameters.Add("@sub_day_sat", SqlDbType.Bit).Value = IsSat;


                        cmd.Parameters.Add("@supplement", SqlDbType.Money).Value = SurAmount;

                        cmd.Parameters.Add("@price_period_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                        ExecuteNonQuery(cmd);

                        PriceId = (int)cmd.Parameters["@price_period_id"].Value;


                        Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                        cStaffPriceAc.InsertActivityRate_Period(intProductId, shrSupplierId, PriceId, intConditionId, Price, dDateStart_rate, dDateEnd_rate, IsSun, IsMon, IsTue, IsWed, IsThu, IsFri, IsSat, SurAmount, shrStaffId, StaffLogActionType.Insert, cn);
                    }
                //}

                result = "True";
            }


            return result;
        }
    }
}